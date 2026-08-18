// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipProvider.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Computes implied Relationships from the generated constraint table, the registered guards and the
    /// model-library index.
    /// </summary>
    /// <remarks>
    /// Nothing computed here is attached to the model: every product is a detached Relationship carrying
    /// isImplied, so isImpliedIncluded stays false and the model remains a faithful match to what was read.
    /// Results are memoised for the lifetime of this instance, which is therefore scoped to one model — the
    /// object graph is mutable and the SDK offers no invalidation hook.
    /// </remarks>
    public class ImpliedRelationshipProvider : IImpliedRelationshipProvider
    {
        /// <summary>
        /// The memoised implied Specializations, keyed by the Type they were computed for.
        /// </summary>
        private readonly Dictionary<IType, IReadOnlyList<ISpecialization>> specializationsByType = [];

        /// <summary>
        /// The index used to resolve the library Types the constraints target.
        /// </summary>
        private readonly ILibraryTypeIndex libraryTypeIndex;

        /// <summary>
        /// The registry consulted for conditional constraints.
        /// </summary>
        private readonly IImpliedRuleGuardRegistry guardRegistry;

        /// <summary>
        /// The factory creating the detached Relationships.
        /// </summary>
        private readonly IImpliedRelationshipFactory factory;

        /// <summary>
        /// The reducer applying the KerML 8.4.2 redundancy rules.
        /// </summary>
        private readonly IImpliedSpecializationReducer reducer;

        /// <summary>
        /// The configured behaviour.
        /// </summary>
        private readonly ImpliedRelationshipOptions options;

        /// <summary>
        /// The hand-coded rules for constraints the generated table cannot express.
        /// </summary>
        private readonly IReadOnlyList<IImpliedRelationshipRule> rules;

        /// <summary>
        /// The constraint names the hand-coded rules cover.
        /// </summary>
        private readonly HashSet<string> ruleConstraintNames;

        /// <summary>
        /// The constraints this provider cannot compute, settled once at construction.
        /// </summary>
        /// <remarks>
        /// Every input is fixed for the lifetime of the instance, so the answer is too. Computing it per
        /// access allocated a fresh list on a property that reads as a field — and <see cref="IsCoveredByRule" />
        /// consults it per constraint, so the copy was on a hot path rather than an occasional one.
        /// </remarks>
        private readonly IReadOnlyList<string> notCoveredConstraints;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImpliedRelationshipProvider" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Types the constraints target.</param>
        /// <param name="guardRegistry">The registry of guards for conditional constraints.</param>
        /// <param name="factory">The factory creating the detached Relationships.</param>
        /// <param name="reducer">The reducer applying the redundancy rules.</param>
        /// <param name="options">The configured behaviour.</param>
        /// <param name="rules">The hand-coded rules for constraints the generated table cannot express.</param>
        /// <exception cref="ArgumentNullException">Thrown when any argument is null.</exception>
        public ImpliedRelationshipProvider(ILibraryTypeIndex libraryTypeIndex, IImpliedRuleGuardRegistry guardRegistry, IImpliedRelationshipFactory factory, IImpliedSpecializationReducer reducer, ImpliedRelationshipOptions options, IEnumerable<IImpliedRelationshipRule> rules)
        {
            if (rules == null)
            {
                throw new ArgumentNullException(nameof(rules));
            }

            this.libraryTypeIndex = libraryTypeIndex ?? throw new ArgumentNullException(nameof(libraryTypeIndex));
            this.guardRegistry = guardRegistry ?? throw new ArgumentNullException(nameof(guardRegistry));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
            this.reducer = reducer ?? throw new ArgumentNullException(nameof(reducer));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.rules = rules.ToList();
            this.ruleConstraintNames = [..this.rules.Select(rule => rule.ConstraintName)];

            var uncovered = this.options.EnableLibrarySpecializations
                ? ImpliedRelationshipTable.NotCovered
                : ImpliedRelationshipTable.AllConstraintNames;

            this.notCoveredConstraints = [..uncovered
                .Where(constraint => !this.ruleConstraintNames.Any(ruleConstraintName => constraint.Contains(ruleConstraintName, StringComparison.Ordinal)))];
        }

        /// <summary>
        /// Gets the names of the semantic constraints this provider cannot yet compute.
        /// </summary>
        /// <remarks>
        /// The manifest is the table's own not-covered list, minus the constraints a registered hand-coded
        /// rule supplies. When library specializations are disabled the answer widens to every constraint,
        /// since nothing table-driven is computed at all.
        /// </remarks>
        public IReadOnlyList<string> NotCoveredConstraints => this.notCoveredConstraints;

        /// <summary>
        /// Returns the implied Relationships required of the supplied Element.
        /// </summary>
        /// <param name="element">The Element to compute implied Relationships for.</param>
        /// <returns>The detached implied Relationships; empty when none are required.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        /// <exception cref="MissingImpliedRuleGuardException">Thrown when a conditional constraint has no registered guard.</exception>
        /// <exception cref="UnresolvedLibraryTypeException">Thrown when a targeted library Type is not indexed.</exception>
        public IReadOnlyList<IRelationship> GetImpliedRelationships(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            var relationships = new List<IRelationship>();

            if (element is IType type)
            {
                relationships.AddRange(this.GetImpliedSpecializations(type));
            }

            // Specializations are already accounted for above — a Redefinition, Subsetting and FeatureTyping
            // are all Specializations, so re-adding them here would double-count.
            relationships.AddRange(this.ApplyRules(element).Where(relationship => relationship is not ISpecialization));

            return relationships;
        }

        /// <summary>
        /// Returns the implied Specializations required of the supplied Type, after 8.4.2 redundancy reduction.
        /// </summary>
        /// <param name="type">The Type to compute implied Specializations for.</param>
        /// <returns>The detached implied Specializations; empty when none are required.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type" /> is null.</exception>
        /// <exception cref="MissingImpliedRuleGuardException">Thrown when a conditional constraint has no registered guard.</exception>
        /// <exception cref="UnresolvedLibraryTypeException">Thrown when a targeted library Type is not indexed.</exception>
        public IReadOnlyList<ISpecialization> GetImpliedSpecializations(IType type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (this.specializationsByType.TryGetValue(type, out var memoised))
            {
                return memoised;
            }

            var candidates = new List<ISpecialization>();

            if (this.options.EnableLibrarySpecializations)
            {
                candidates.AddRange(ImpliedRelationshipTable.QueryImpliedLibrarySpecializations(type)
                    .Where(rule => this.Applies(rule, type))
                    .Select(rule => this.CreateSpecializationOrNull(rule, type))
                    .Where(specialization => specialization != null));
            }

            var ruleSpecializations = this.ApplyRules(type).OfType<ISpecialization>().ToList();

            // A rule may return a Specialization whose SPECIFIC is a nested Element rather than the Type
            // under evaluation — the result parameter of an Expression, the multiplicity of a Definition,
            // the trigger of a TransitionUsage. Reduction compares candidates against THIS Type's declared
            // generals and against each other, so admitting those would let a coincidental match on an
            // unrelated Element's general discard a valid Specialization. They bypass reduction entirely.
            var nonRedefinitions = ruleSpecializations.Where(specialization => specialization is not IRedefinition).ToList();
            var notReducible = nonRedefinitions.Where(specialization => !ReferenceEquals(specialization.Specific, type)).ToList();

            candidates.AddRange(nonRedefinitions.Where(specialization => ReferenceEquals(specialization.Specific, type)));

            // Redundancy reduction is deliberately NOT applied to Redefinitions: KerML 8.4.2 exempts them
            // because a Redefinition carries semantics beyond basic Specialization.
            IReadOnlyList<ISpecialization> reduced = this.options.ReduceRedundantSpecializations
                ? this.reducer.Reduce(type, candidates)
                : candidates;

            IReadOnlyList<ISpecialization> result =
            [
                ..reduced,
                ..notReducible,
                ..ruleSpecializations.OfType<IRedefinition>()
            ];

            this.specializationsByType[type] = result;

            return result;
        }

        /// <summary>
        /// Returns the implied Redefinitions required of the supplied Feature.
        /// </summary>
        /// <param name="feature">The Feature to compute implied Redefinitions for.</param>
        /// <returns>The detached implied Redefinitions; empty when none are required.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="feature" /> is null.</exception>
        /// <remarks>
        /// Redefinition constraints relate two Features of the USER model rather than a user Type and a
        /// library Type, so none is expressible in the generated table; each is supplied by a hand-coded
        /// rule. Constraints with no registered rule are reported by <see cref="NotCoveredConstraints" />.
        /// </remarks>
        public IReadOnlyList<IRedefinition> GetImpliedRedefinitions(IFeature feature)
        {
            return feature == null
                ? throw new ArgumentNullException(nameof(feature))
                : this.ApplyRules(feature).OfType<IRedefinition>().ToList();
        }

        /// <summary>
        /// Asserts whether the named semantic constraint is computed by this provider.
        /// </summary>
        /// <param name="constraintName">The constraint name, for example checkPortUsageSpecialization.</param>
        /// <returns>True when the constraint is computed, false when it is listed as not covered.</returns>
        public bool IsConstraintCovered(string constraintName)
        {
            return !string.IsNullOrWhiteSpace(constraintName)
                   && !this.NotCoveredConstraints.Any(notCovered => notCovered.Contains(constraintName, StringComparison.Ordinal));
        }

        /// <summary>
        /// Runs every registered hand-coded rule against an Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The implied Relationships the rules produced, in registration order.</returns>
        private IReadOnlyList<IRelationship> ApplyRules(IElement element) => [..this.rules.SelectMany(rule => ApplyRule(rule, element))];

        /// <summary>
        /// Applies one rule, degrading to no contribution when the library Type it targets cannot be resolved.
        /// </summary>
        /// <param name="rule">The rule to apply.</param>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The rule's implied Relationships, or empty when its library target is unresolvable.</returns>
        /// <remarks>
        /// An unresolvable target has two causes and only one of them is the caller's to fix. Libraries not
        /// loaded is a misconfiguration; a constraint naming a Feature that no library declares — a defect in
        /// the specification OCL, or a path the index cannot express — is not. Throwing treats both alike and
        /// costs the ENTIRE document: the exception escapes the writer mid-write and the output is truncated.
        /// <para>Degrading costs one Relationship instead. A name that would have been shortened through it
        /// falls back to a longer — never an invalid — form, which is the same failure mode the writer
        /// already accepts elsewhere.</para>
        /// </remarks>
        private static IReadOnlyList<IRelationship> ApplyRule(IImpliedRelationshipRule rule, IElement element)
        {
            try
            {
                return rule.Apply(element);
            }
            catch (UnresolvedLibraryTypeException)
            {
                return [];
            }
        }

        /// <summary>
        /// Asserts whether a table row applies to an Element, consulting the registered guard when the row
        /// is conditional.
        /// </summary>
        /// <param name="rule">The table row under evaluation.</param>
        /// <param name="element">The Element the row was matched against.</param>
        /// <returns>True when the constraint applies.</returns>
        /// <exception cref="MissingImpliedRuleGuardException">Thrown when the row is conditional and no guard is registered.</exception>
        private bool Applies(ImpliedLibrarySpecialization rule, IElement element)
        {
            if (!rule.RequiresGuard)
            {
                return true;
            }

            if (!this.guardRegistry.HasGuard(rule.ConstraintName))
            {
                throw new MissingImpliedRuleGuardException(rule.ConstraintName, rule.DeclaringMetaclassName);
            }

            return this.guardRegistry.GetGuard(rule.ConstraintName).Applies(element);
        }

        /// <summary>
        /// Creates the implied Specialization for a table row, degrading to null when its library Type is
        /// unresolvable — see <see cref="ApplyRule" /> for why this does not throw.
        /// </summary>
        /// <param name="rule">The table row.</param>
        /// <param name="type">The Type under evaluation.</param>
        /// <returns>The Specialization, or <c>null</c>.</returns>
        private ISpecialization CreateSpecializationOrNull(ImpliedLibrarySpecialization rule, IType type)
        {
            try
            {
                return this.CreateSpecialization(rule, type);
            }
            catch (UnresolvedLibraryTypeException)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates the Specialization a table row implies for a Type.
        /// </summary>
        /// <param name="rule">The table row to realise.</param>
        /// <param name="type">The Type the Specialization specializes from.</param>
        /// <returns>The detached Specialization, or null when the row's kind does not match the Type.</returns>
        /// <exception cref="UnresolvedLibraryTypeException">Thrown when the targeted library Type is not indexed.</exception>
        private ISpecialization CreateSpecialization(ImpliedLibrarySpecialization rule, IType type)
        {
            if (!this.libraryTypeIndex.TryGetType(rule.TargetLibraryName, out var libraryType))
            {
                throw new UnresolvedLibraryTypeException(rule.TargetLibraryName, rule.ConstraintName);
            }

            return QueryKind(rule) switch
            {
                ImpliedRelationshipKind.Subclassification when type is IClassifier specificClassifier && libraryType is IClassifier generalClassifier =>
                    this.factory.CreateImpliedSubclassification(specificClassifier, generalClassifier),
                ImpliedRelationshipKind.Subsetting when type is IFeature specificFeature && libraryType is IFeature generalFeature =>
                    this.factory.CreateImpliedSubsetting(specificFeature, generalFeature),
                _ => null
            };
        }

        /// <summary>
        /// Determines whether a table row implies a Subclassification or a Subsetting.
        /// </summary>
        /// <param name="rule">The table row to classify.</param>
        /// <returns>The kind of Relationship the row implies.</returns>
        /// <remarks>
        /// The OCL does not carry the distinction, so the generator emits the set of metaclasses whose
        /// constraints imply Subclassification; everything else implies Subsetting.
        /// </remarks>
        private static ImpliedRelationshipKind QueryKind(ImpliedLibrarySpecialization rule)
        {
            return ImpliedRelationshipTable.SubclassificationMetaclasses.Contains(rule.DeclaringMetaclassName)
                ? ImpliedRelationshipKind.Subclassification
                : ImpliedRelationshipKind.Subsetting;
        }
    }
}
