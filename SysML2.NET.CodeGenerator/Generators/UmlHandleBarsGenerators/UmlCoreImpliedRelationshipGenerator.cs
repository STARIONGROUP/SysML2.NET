// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreImpliedRelationshipGenerator.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using SysML2.NET.CodeGenerator.Extensions;

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.HandleBars;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using ClassHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.ClassHelper;
    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;
    using PropertyHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper;

    /// <summary>
    /// Generates the table of implied library <c>Specializations</c> that KerML 1.0 §8.4.2 allows a tool to
    /// insert in order to satisfy the <i>specialization constraints</i> of the abstract syntax.
    /// </summary>
    /// <remarks>
    /// Only the machine-readable half of each constraint is generated: the constrained metaclass and the
    /// qualified name of the library Type, both taken from the constraint's OCL body. The half that the OCL
    /// does NOT carry — whether the implied Relationship is a <c>Subclassification</c> or a
    /// <c>Subsetting</c> — lives only in the specification tables (KerML Tables 8 and 10, SysML Tables
    /// 31-33) and is hard-coded in the Handlebars template.
    /// <para>
    /// Constraints whose OCL is not a bare or guarded <c>specializesFromLibrary</c> call are NOT generated;
    /// they are emitted into a manifest on the generated class so that no semantic constraint is silently
    /// dropped while the hand-coded arms are still outstanding.
    /// </para>
    /// </remarks>
    public class UmlCoreImpliedRelationshipGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// The name of the Handlebars template that emits the table.
        /// </summary>
        private const string ImpliedRelationshipTemplateName = "core-implied-relationship-table-template";

        /// <summary>
        /// The name of the file to write into the output directory.
        /// </summary>
        private const string OutputFileName = "ImpliedRelationshipTable.cs";

        /// <summary>
        /// The name of the template rendering the generated guards.
        /// </summary>
        private const string ImpliedGuardsTemplateName = "core-implied-guards-template";

        /// <summary>
        /// The name of the file the generated guards are written to.
        /// </summary>
        private const string GuardsOutputFileName = "GeneratedImpliedRuleGuards.cs";

        /// <summary>
        /// Generates the <see cref="OutputFileName"/> file in the supplied <paramref name="outputDirectory"/>.
        /// </summary>
        /// <param name="xmiReaderResult">
        /// The <see cref="XmiReaderResult"/> with the loaded UML model
        /// </param>
        /// <param name="outputDirectory">
        /// The target directory
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            await this.GenerateImpliedRelationshipTable(xmiReaderResult, outputDirectory);
            await this.GenerateImpliedRuleGuards(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Generates the <see cref="GuardsOutputFileName"/> file in the supplied <paramref name="outputDirectory"/>.
        /// </summary>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult"/> carrying the abstract syntax.</param>
        /// <param name="outputDirectory">The directory the file is written to.</param>
        /// <returns>The rendered content.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public async Task<string> GenerateImpliedRuleGuards(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            var payload = QueryImpliedRelationshipPayload(xmiReaderResult);

            var template = this.Templates[ImpliedGuardsTemplateName];
            var rendered = template(payload);
            rendered = this.CodeCleanup(rendered);

            await WriteAsync(rendered, outputDirectory, GuardsOutputFileName);

            return rendered;
        }

        /// <summary>
        /// Renders the table, writes it to <paramref name="outputDirectory"/> and returns the generated
        /// source for assertion in expected-output tests.
        /// </summary>
        /// <param name="xmiReaderResult">
        /// The <see cref="XmiReaderResult"/> with the loaded UML model
        /// </param>
        /// <param name="outputDirectory">
        /// The target directory
        /// </param>
        /// <returns>
        /// The generated C# source, after <c>CodeCleanup</c>
        /// </returns>
        public async Task<string> GenerateImpliedRelationshipTable(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            var payload = QueryImpliedRelationshipPayload(xmiReaderResult);

            var template = this.Templates[ImpliedRelationshipTemplateName];
            var rendered = template(payload);
            rendered = this.CodeCleanup(rendered);

            await WriteAsync(rendered, outputDirectory, OutputFileName);

            return rendered;
        }

        /// <summary>
        /// Register the custom Handlebars helpers used by the template.
        /// </summary>
        protected override void RegisterHelpers()
        {
            this.Handlebars.RegisterStringHelper();
            this.Handlebars.RegisterPropertyHelper();
            this.Handlebars.RegisterClassHelper();
            NamedElementHelper.RegisterNamedElementHelper(this.Handlebars);
            PropertyHelper.RegisterPropertyHelper(this.Handlebars);
            ClassHelper.RegisterClassHelper(this.Handlebars);
        }

        /// <summary>
        /// Register the code template.
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate(ImpliedRelationshipTemplateName);
            this.RegisterTemplate(ImpliedGuardsTemplateName);
        }

        /// <summary>
        /// Builds the payload: one entry per metaclass that carries at least one generatable specialization
        /// constraint, with the constraints of its supertypes FLATTENED IN, plus the manifest of constraints
        /// that still need hand-coding.
        /// </summary>
        /// <remarks>
        /// Flattening at generation time is deliberate: a <c>PartUsage</c> is subject to the specialization
        /// constraints of <c>OccurrenceUsage</c>, <c>Usage</c>, <c>Feature</c> and <c>Type</c> as well as its
        /// own, and resolving that at run time would mean walking the metaclass hierarchy on every query.
        /// Note that this does NOT pre-apply the §8.4.2 redundancy rules — rule 1 asks whether one implied
        /// target is a subtype of another, and the targets are library elements that are not present in
        /// these XMI files, so that reduction can only happen at run time.
        /// </remarks>
        /// <param name="xmiReaderResult">
        /// The <see cref="XmiReaderResult"/> with the loaded UML model
        /// </param>
        /// <returns>
        /// The payload consumed by the template
        /// </returns>
        private static ImpliedRelationshipPayload QueryImpliedRelationshipPayload(XmiReaderResult xmiReaderResult)
        {
            var rules = xmiReaderResult.QueryImpliedRelationshipRules();

            var generatable = rules
                .Where(rule => rule.Form is ImpliedRuleForm.UnconditionalLibrarySpecialization or ImpliedRuleForm.GuardedLibrarySpecialization)
                .ToLookup(rule => rule.MetaclassName, StringComparer.Ordinal);

            var classesByName = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(package => package.PackagedElement.OfType<IClass>())
                .GroupBy(umlClass => umlClass.Name, StringComparer.Ordinal)
                .ToDictionary(group => group.Key, group => group.First(), StringComparer.Ordinal);

            var metaclasses = classesByName.Values
                .Select(umlClass => CreateMetaclassRules(umlClass, generatable))
                .Where(metaclass => metaclass.Rules.Count > 0)
                .OrderByDescending(metaclass => metaclass.InheritanceDepth)
                .ThenBy(metaclass => metaclass.MetaclassName, StringComparer.Ordinal)
                .ToList();

            var notCovered = rules
                .Where(rule => rule.Form is ImpliedRuleForm.RequiresHandCoding or ImpliedRuleForm.SpecificationTbd)
                .Select(rule => new NotCoveredConstraint
                {
                    ConstraintName = rule.ConstraintName,
                    MetaclassName = rule.MetaclassName,
                    Category = rule.Category.ToString(),
                    Reason = rule.Form == ImpliedRuleForm.SpecificationTbd ? "specification body is TBD" : "OCL is not a specializesFromLibrary call"
                })
                .ToList();

            var allConstraintNames = rules
                .Select(rule => rule.ConstraintName)
                .Distinct(StringComparer.Ordinal)
                .OrderBy(constraintName => constraintName, StringComparer.Ordinal)
                .ToList();

            var interfaceFqnByName = classesByName.ToDictionary(entry => entry.Key, entry => entry.Value.QueryFullyQualifiedTypeName(), StringComparer.Ordinal);

            var enumerationFqnByName = xmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(package => package.PackagedElement.OfType<IEnumeration>())
                .GroupBy(enumeration => enumeration.Name, StringComparer.Ordinal)
                .ToDictionary(group => group.Key, group => group.First().QueryFullyQualifiedTypeName(), StringComparer.Ordinal);

            var guards = rules
                .Where(rule => rule.Form == ImpliedRuleForm.GuardedLibrarySpecialization)
                .Select(rule => new
                {
                    rule.ConstraintName,
                    Expression = ImpliedGuardParser.Parse(rule.GuardExpression),
                    DeclaringInterfaceFqn = interfaceFqnByName.TryGetValue(rule.MetaclassName, out var declaringFqn) ? declaringFqn : null
                })
                .Select(candidate => new ImpliedGuardPayload
                {
                    ConstraintName = candidate.ConstraintName,
                    Ocl = candidate.Expression.Ocl,
                    Predicate = ImpliedGuardEmitter.Emit(candidate.Expression, candidate.DeclaringInterfaceFqn, interfaceFqnByName, enumerationFqnByName)
                })
                .Where(guard => guard.Predicate != null)
                .GroupBy(guard => guard.ConstraintName, StringComparer.Ordinal)
                .Select(group => group.First())
                .OrderBy(guard => guard.ConstraintName, StringComparer.Ordinal)
                .ToList();

            var conditionalConstraintNames = rules
                .Where(rule => rule.Form == ImpliedRuleForm.GuardedLibrarySpecialization)
                .Select(rule => rule.ConstraintName)
                .Distinct(StringComparer.Ordinal)
                .OrderBy(constraintName => constraintName, StringComparer.Ordinal)
                .ToList();

            return new ImpliedRelationshipPayload
            {
                Metaclasses = metaclasses,
                NotCovered = notCovered,
                AllConstraintNames = allConstraintNames,
                ConditionalConstraintNames = conditionalConstraintNames,
                Guards = guards
            };
        }

        /// <summary>
        /// Collects the generatable specialization constraints that apply to a metaclass — its own plus every
        /// one inherited from a general classifier — ordered most-general first so the emitted array reads
        /// from the root of the hierarchy downwards.
        /// </summary>
        /// <param name="umlClass">
        /// The metaclass to project
        /// </param>
        /// <param name="generatable">
        /// The generatable rules, keyed by declaring metaclass name
        /// </param>
        /// <returns>
        /// The metaclass entry, possibly with an empty rule list
        /// </returns>
        private static ImpliedMetaclassRules CreateMetaclassRules(IClass umlClass, ILookup<string, ImpliedRelationshipRule> generatable)
        {
            var generalClassifiers = umlClass.QueryAllGeneralClassifiers().ToList();

            var applicable = generalClassifiers
                .Select(general => general.Name)
                .Append(umlClass.Name)
                .Distinct(StringComparer.Ordinal)
                .SelectMany(name => generatable[name])
                .OrderBy(rule => rule.ConstraintName, StringComparer.Ordinal)
                .Select(rule => new ImpliedLibraryRule
                {
                    ConstraintName = rule.ConstraintName,
                    DeclaringMetaclassName = rule.MetaclassName,
                    TargetLibraryName = rule.TargetLibraryName,
                    RequiresGuard = rule.Form == ImpliedRuleForm.GuardedLibrarySpecialization
                })
                .ToList();

            return new ImpliedMetaclassRules
            {
                MetaclassName = umlClass.Name,
                InterfaceFqn = umlClass.QueryFullyQualifiedTypeName(),
                InheritanceDepth = generalClassifiers.Count,
                IsAbstract = umlClass.IsAbstract,
                Rules = applicable
            };
        }
    }

    /// <summary>
    /// The payload consumed by the implied-relationship-table template.
    /// </summary>
    public class ImpliedRelationshipPayload
    {
        /// <summary>
        /// Gets the metaclasses carrying at least one generatable specialization constraint, ordered
        /// most-derived first so the emitted switch matches the narrowest interface first.
        /// </summary>
        public IReadOnlyList<ImpliedMetaclassRules> Metaclasses { get; init; }

        /// <summary>
        /// Gets the semantic constraints that could not be generated, emitted as a manifest so that none is
        /// silently dropped.
        /// </summary>
        public IReadOnlyList<NotCoveredConstraint> NotCovered { get; init; }

        /// <summary>
        /// Gets the names of every semantic constraint found in the model, covered or not, so a consumer can
        /// report what it does not compute without hard-coding a list.
        /// </summary>
        public IReadOnlyList<string> AllConstraintNames { get; init; }

        /// <summary>
        /// Gets the names of the constraints whose application is conditional, i.e. every row that requires
        /// a guard.
        /// </summary>
        public IReadOnlyList<string> ConditionalConstraintNames { get; init; }

        /// <summary>
        /// Gets the conditional constraints whose guard OCL was mechanically translated into a predicate.
        /// </summary>
        public IReadOnlyList<ImpliedGuardPayload> Guards { get; init; }
    }

    /// <summary>
    /// One conditional constraint whose guard was translated into a C# predicate.
    /// </summary>
    public class ImpliedGuardPayload
    {
        /// <summary>
        /// Gets the constraint the guard decides.
        /// </summary>
        public string ConstraintName { get; init; }

        /// <summary>
        /// Gets the guard OCL, emitted as the generated member's doc comment.
        /// </summary>
        public string Ocl { get; init; }

        /// <summary>
        /// Gets the C# boolean expression over a parameter named <c>element</c>.
        /// </summary>
        public string Predicate { get; init; }
    }

    /// <summary>
    /// The generatable specialization constraints that apply to one metaclass.
    /// </summary>
    public class ImpliedMetaclassRules
    {
        /// <summary>
        /// Gets the metaclass name, e.g. <c>PartUsage</c>.
        /// </summary>
        public string MetaclassName { get; init; }

        /// <summary>
        /// Gets the fully qualified POCO interface name, e.g.
        /// <c>SysML2.NET.Core.POCO.Systems.Parts.IPartUsage</c>.
        /// </summary>
        public string InterfaceFqn { get; init; }

        /// <summary>
        /// Gets the number of general classifiers, used to order the emitted switch most-derived first.
        /// </summary>
        public int InheritanceDepth { get; init; }

        /// <summary>
        /// Gets a value indicating whether the metaclass is abstract.
        /// </summary>
        public bool IsAbstract { get; init; }

        /// <summary>
        /// Gets the applicable rules, own and inherited, ordered by constraint name.
        /// </summary>
        public IReadOnlyList<ImpliedLibraryRule> Rules { get; init; }
    }

    /// <summary>
    /// A single implied library specialization.
    /// </summary>
    public class ImpliedLibraryRule
    {
        /// <summary>
        /// Gets the name of the constraint the rule was extracted from.
        /// </summary>
        public string ConstraintName { get; init; }

        /// <summary>
        /// Gets the metaclass the constraint is declared on, which may be a supertype of the metaclass the
        /// rule is emitted for. The relationship KIND is decided from this name, not from the inheriting
        /// metaclass.
        /// </summary>
        public string DeclaringMetaclassName { get; init; }

        /// <summary>
        /// Gets the qualified name of the library Type that must be specialized.
        /// </summary>
        public string TargetLibraryName { get; init; }

        /// <summary>
        /// Gets a value indicating whether the constraint's OCL guards the specialization, in which case the
        /// rule only applies when a hand-written predicate says so.
        /// </summary>
        public bool RequiresGuard { get; init; }
    }

    /// <summary>
    /// A semantic constraint that the generator could not turn into a table row.
    /// </summary>
    public class NotCoveredConstraint
    {
        /// <summary>
        /// Gets the name of the constraint.
        /// </summary>
        public string ConstraintName { get; init; }

        /// <summary>
        /// Gets the metaclass the constraint is declared on.
        /// </summary>
        public string MetaclassName { get; init; }

        /// <summary>
        /// Gets the §8.4.2 category of the constraint.
        /// </summary>
        public string Category { get; init; }

        /// <summary>
        /// Gets why the constraint could not be generated.
        /// </summary>
        public string Reason { get; init; }
    }
}
