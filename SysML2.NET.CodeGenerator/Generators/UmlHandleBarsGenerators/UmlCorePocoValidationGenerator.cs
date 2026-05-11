// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCorePocoValidationGenerator.cs" company="Starion Group S.A.">
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
    using uml4net.HandleBars;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    using ClassHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.ClassHelper;
    using NamedElementHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.NamedElementHelper;
    using PropertyHelper = SysML2.NET.CodeGenerator.HandleBarHelpers.PropertyHelper;

    /// <summary>
    /// The <see cref="UmlCorePocoValidationGenerator" /> generates the auto-generated companion to the
    /// hand-coded <c>SysML2.NET.Extensions.ElementExtensions</c> partial class. It emits a switch expression
    /// (<c>QueryIsValidForContainment</c>) and a parallel name lookup (<c>ExpectedContainmentTypeName</c>)
    /// that map each Relationship metaclass to the runtime-type check for the element it may carry as an
    /// <c>OwnedRelatedElement</c>, derived from every typed composite property that subsets
    /// <c>IRelationship.OwnedRelatedElement</c>.
    /// </summary>
    public class UmlCorePocoValidationGenerator : UmlHandleBarsGenerator
    {
        /// <summary>
        /// The name of the Handlebars template that emits the partial-class file.
        /// </summary>
        private const string CorePocoValidationTemplateName = "core-poco-validation-template";

        /// <summary>
        /// The name of the file to write into the output directory.
        /// </summary>
        private const string OutputFileName = "ElementExtensions.cs";

        /// <summary>
        /// Generates the <see cref="OutputFileName" /> file in the supplied <paramref name="outputDirectory" />.
        /// </summary>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult" /> with the loaded UML model.</param>
        /// <param name="outputDirectory">The target directory.</param>
        /// <returns>An awaitable <see cref="Task" />.</returns>
        /// <exception cref="ArgumentNullException">If either argument is null.</exception>
        public override async Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            await this.GenerateElementExtensions(xmiReaderResult, outputDirectory);
        }

        /// <summary>
        /// Renders the <see cref="OutputFileName" /> file, writes it to <paramref name="outputDirectory" />,
        /// and returns the generated source for assertion in expected-output tests (the "ExpectedResult"
        /// principle used by sibling generators such as <c>UmlPocoReferenceResolveExtensionGenerator</c>).
        /// </summary>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult" /> with the loaded UML model.</param>
        /// <param name="outputDirectory">The target directory.</param>
        /// <returns>The generated C# source, after <c>CodeCleanup</c>.</returns>
        /// <exception cref="ArgumentNullException">If either argument is null.</exception>
        public async Task<string> GenerateElementExtensions(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);
            ArgumentNullException.ThrowIfNull(outputDirectory);

            var payload = QueryContainmentRulePayload(xmiReaderResult);

            var template = this.Templates[CorePocoValidationTemplateName];
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
            this.RegisterTemplate(CorePocoValidationTemplateName);
        }

        /// <summary>
        /// Walks the merged UML model and produces two parallel lists of <see cref="ContainmentRule" />:
        /// one for the target-side narrowing (typed composite properties subsetting
        /// <c>OwnedRelatedElement</c>) and one for the owner-side narrowing (typed properties subsetting
        /// <c>OwningRelatedElement</c> with a type strictly narrower than <c>Element</c>). Each list is
        /// returned ordered from most-specific to least-specific (deepest inheritance depth first), so
        /// the emitted switch expressions match a class against its narrowest applicable interface first.
        /// </summary>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult" /> with the loaded UML model.</param>
        /// <returns>The payload with the two ordered rule lists.</returns>
        private static ContainmentRulePayload QueryContainmentRulePayload(XmiReaderResult xmiReaderResult)
        {
            var (_, ownedRelatedElementXmiId, owningRelatedElementXmiId, elementXmiId) =
                QueryRootContainmentMeta(xmiReaderResult);

            var seenClassXmiIds = new HashSet<string>();
            var targetRows = new List<(IClass UmlClass, IProperty TypedProperty, int InheritanceDepth)>();
            var ownerRows = new List<(IClass UmlClass, IProperty TypedProperty, int InheritanceDepth)>();

            foreach (var package in xmiReaderResult.QueryContainedAndImported("SysML"))
            {
                foreach (var umlClass in package.PackagedElement.OfType<IClass>())
                {
                    if (!seenClassXmiIds.Add(umlClass.XmiId))
                    {
                        continue;
                    }

                    var inheritanceDepth = QueryInheritanceDepth(umlClass);

                    var typedTarget = QueryTypedSubsettingProperty(umlClass, ownedRelatedElementXmiId, requireComposite: true);
                    
                    if (typedTarget != null)
                    {
                        targetRows.Add((umlClass, typedTarget, inheritanceDepth));
                    }

                    // Owner-side narrowing is meaningful only when the typed property's type is strictly
                    // narrower than Element — otherwise the arm would be equivalent to 'source is not null'
                    // which is exactly what the '_ => true' default already returns.
                    var typedOwner = QueryTypedSubsettingProperty(umlClass, owningRelatedElementXmiId, requireComposite: false);
                    
                    if (typedOwner != null && typedOwner.Type.XmiId != elementXmiId)
                    {
                        ownerRows.Add((umlClass, typedOwner, inheritanceDepth));
                    }
                }
            }

            return new ContainmentRulePayload
            {
                TargetRules = targetRows
                    .OrderByDescending(x => x.InheritanceDepth)
                    .ThenBy(x => x.UmlClass.Name, StringComparer.Ordinal)
                    .Select(x =>
                    {
                        var targetFqn = x.TypedProperty.Type.QueryFullyQualifiedTypeName();
                        var targetSimpleName = "I" + x.TypedProperty.Type.Name;

                        // When the expected target type is the root Element type, 'target is IElement' is
                        // equivalent to 'target is not null' (since the parameter is already typed IElement).
                        // Emit the latter to avoid the CA1508 / IDE0078 "type check succeeds on any
                        // not-null value" analyzer warning.
                        var targetCheck = x.TypedProperty.Type.XmiId == elementXmiId
                            ? "target is not null"
                            : $"target is {targetFqn}";

                        return new ContainmentRule
                        {
                            BridgeFqn = x.UmlClass.QueryFullyQualifiedTypeName(),
                            BridgeName = "I" + x.UmlClass.Name,
                            TargetFqn = targetFqn,
                            TargetName = targetSimpleName,
                            TargetCheck = targetCheck
                        };
                    })
                    .ToList(),

                OwnerRules = ownerRows
                    .OrderByDescending(x => x.InheritanceDepth)
                    .ThenBy(x => x.UmlClass.Name, StringComparer.Ordinal)
                    .Select(x =>
                    {
                        var ownerFqn = x.TypedProperty.Type.QueryFullyQualifiedTypeName();
                        var ownerSimpleName = "I" + x.TypedProperty.Type.Name;

                        return new ContainmentRule
                        {
                            BridgeFqn = x.UmlClass.QueryFullyQualifiedTypeName(),
                            BridgeName = "I" + x.UmlClass.Name,
                            OwnerFqn = ownerFqn,
                            OwnerName = ownerSimpleName,
                            OwnerCheck = $"source is {ownerFqn}"
                        };
                    })
                    .ToList()
            };
        }

        /// <summary>
        /// Discovers the root containment metaclass and its two root containment properties purely from
        /// the structure of the loaded UML model — no metaclass or property names are used.
        /// </summary>
        /// <remarks>
        /// The structural fingerprint of the root containment metaclass (KerML <c>Relationship</c>) is the
        /// "self-referential containment" pattern: it declares two owned attributes whose declared Type is
        /// a strict supertype of the metaclass itself. One attribute is composite (the contained-elements
        /// side, <c>ownedRelatedElement</c>); the other is non-composite (the owner back-reference,
        /// <c>owningRelatedElement</c>). The Type of those two attributes is the root <c>Element</c>
        /// metaclass — i.e. one level above in the inheritance tree.
        /// </remarks>
        /// <param name="xmiReaderResult">The <see cref="XmiReaderResult" /> with the loaded UML model.</param>
        /// <returns>The relationship class together with the xmi ids of the two root containment
        /// properties and the xmi id of the root element type they point to.</returns>
        /// <exception cref="InvalidOperationException">If no class in the loaded model matches the
        /// self-referential containment fingerprint, or if more than one does.</exception>
        private static (IClass RelationshipClass, string OwnedRelatedElementXmiId, string OwningRelatedElementXmiId, string ElementXmiId)
            QueryRootContainmentMeta(XmiReaderResult xmiReaderResult)
        {
            var matches = new List<(IClass Class, IProperty Composite, IProperty BackRef)>();

            foreach (var package in xmiReaderResult.QueryContainedAndImported("SysML"))
            {
                foreach (var candidateClass in package.PackagedElement.OfType<IClass>())
                {
                    IProperty composite = null;
                    IProperty backRef = null;

                    foreach (var attribute in candidateClass.OwnedAttribute)
                    {
                        // Derived attributes (e.g. the 'relatedElement' derived union on Relationship,
                        // which also has Type = Element) are not part of the structural fingerprint —
                        // we want the concrete association ends that subclasses subset, not the
                        // computed-union roots above them.
                        if (attribute.IsDerived)
                        {
                            continue;
                        }

                        if (attribute.Type is not IClass attributeType)
                        {
                            continue;
                        }

                        // The structural fingerprint: the owned attribute's Type is a strict supertype of
                        // the class that declares it. Only the root containment metaclass (Relationship)
                        // has this self-referential containment pattern.
                        if (!IsStrictDescendantOf(candidateClass, attributeType))
                        {
                            continue;
                        }

                        if (attribute.IsComposite && composite == null)
                        {
                            composite = attribute;
                        }
                        else if (!attribute.IsComposite && backRef == null)
                        {
                            backRef = attribute;
                        }
                    }

                    if (composite != null && backRef != null)
                    {
                        matches.Add((candidateClass, composite, backRef));
                    }
                }
            }

            if (matches.Count == 0)
            {
                throw new InvalidOperationException(
                    "Could not structurally identify the root containment metaclass in the loaded UML " +
                    "model: no class declares both a composite and a non-composite owned attribute whose " +
                    "Type is a strict supertype of itself.");
            }

            if (matches.Count > 1)
            {
                throw new InvalidOperationException(
                    $"The structural fingerprint for the root containment metaclass is ambiguous in the " +
                    $"loaded UML model: {matches.Count} classes match. Expected exactly one.");
            }

            var match = matches[0];
            return (match.Class, match.Composite.XmiId, match.BackRef.XmiId, ((IClass)match.Composite.Type).XmiId);
        }

        /// <summary>
        /// Returns true when <paramref name="candidateDescendant" /> is a strict (non-reflexive) descendant
        /// of <paramref name="candidateAncestor" /> via transitive <see cref="IClass.SuperClass" /> walks.
        /// </summary>
        /// <param name="candidateDescendant">The class whose ancestor chain is walked.</param>
        /// <param name="candidateAncestor">The class looked for as an ancestor.</param>
        /// <returns><c>true</c> if <paramref name="candidateAncestor" /> is a strict ancestor of
        /// <paramref name="candidateDescendant" />; otherwise <c>false</c>.</returns>
        private static bool IsStrictDescendantOf(IClass candidateDescendant, IClass candidateAncestor)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<IClass>();
            queue.Enqueue(candidateDescendant);
            visited.Add(candidateDescendant.XmiId);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var super in current.SuperClass)
                {
                    if (super == null)
                    {
                        continue;
                    }

                    if (super.XmiId == candidateAncestor.XmiId)
                    {
                        return true;
                    }

                    if (visited.Add(super.XmiId))
                    {
                        queue.Enqueue(super);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the most-specific owned attribute of <paramref name="umlClass" /> whose
        /// subsetting/redefinition chain transitively reaches the property identified by
        /// <paramref name="rootPropertyXmiId" />. Returns null if no such attribute exists on the class
        /// itself (inherited-only properties are intentionally ignored — the C# pattern-matching switch
        /// already handles inheritance via interface-pattern fallthrough).
        /// </summary>
        /// <param name="umlClass">The candidate class.</param>
        /// <param name="rootPropertyXmiId">The xmi id of the root property in the subsetting chain.</param>
        /// <param name="requireComposite">When true, only composite-aggregation attributes are considered
        /// (target-side <c>OwnedRelatedElement</c> chain). When false, any aggregation is accepted
        /// (owner-side <c>OwningRelatedElement</c> chain, which is not composite).</param>
        /// <returns>The typed property, or null.</returns>
        private static IProperty QueryTypedSubsettingProperty(IClass umlClass, string rootPropertyXmiId, bool requireComposite)
        {
            IProperty bestMatch = null;
            var bestDepth = int.MinValue;

            foreach (var attribute in umlClass.OwnedAttribute)
            {
                if (requireComposite && !attribute.IsComposite)
                {
                    continue;
                }

                var depth = QueryDepthToRootProperty(attribute, rootPropertyXmiId);

                if (depth < 0)
                {
                    continue;
                }

                if (depth > bestDepth)
                {
                    bestMatch = attribute;
                    bestDepth = depth;
                }
            }

            return bestMatch;
        }

        /// <summary>
        /// Computes the shortest distance from <paramref name="property" /> to the property identified by
        /// <paramref name="rootPropertyXmiId" /> via subsets and redefinitions. Returns -1 if the chain
        /// does not reach the root.
        /// </summary>
        /// <param name="property">The starting property.</param>
        /// <param name="rootPropertyXmiId">The xmi id of the root property in the subsetting chain.</param>
        /// <returns>The chain depth, or -1.</returns>
        private static int QueryDepthToRootProperty(IProperty property, string rootPropertyXmiId)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<(IProperty Property, int Depth)>();
            queue.Enqueue((property, 0));
            visited.Add(property.XmiId);

            while (queue.Count > 0)
            {
                var (current, depth) = queue.Dequeue();

                if (current.XmiId == rootPropertyXmiId)
                {
                    return depth;
                }

                foreach (var redefined in current.RedefinedProperty)
                {
                    if (visited.Add(redefined.XmiId))
                    {
                        queue.Enqueue((redefined, depth + 1));
                    }
                }

                foreach (var subsetted in current.SubsettedProperty)
                {
                    if (visited.Add(subsetted.XmiId))
                    {
                        queue.Enqueue((subsetted, depth + 1));
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Counts the number of distinct ancestor classes reachable from <paramref name="umlClass" /> via
        /// generalisation. Used as the sort key for the emitted switch expression so that more-specific
        /// metaclasses appear first.
        /// </summary>
        /// <param name="umlClass">The class to measure.</param>
        /// <returns>The transitive ancestor count.</returns>
        private static int QueryInheritanceDepth(IClass umlClass)
        {
            var visited = new HashSet<string>();
            var queue = new Queue<IClass>();
            queue.Enqueue(umlClass);
            visited.Add(umlClass.XmiId);

            var depth = 0;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var super in current.SuperClass.Where(super => super != null && visited.Add(super.XmiId)))
                {
                    queue.Enqueue(super);
                    depth++;
                }
            }

            return depth;
        }
    }

    /// <summary>
    /// Payload passed to the <c>core-poco-validation-template</c> Handlebars template. Holds two ordered
    /// lists of <see cref="ContainmentRule" />: one for the target-side switch (<c>QueryIsValidForContainment</c>
    /// and <c>ExpectedContainmentTypeName</c>) and one for the owner-side switch
    /// (<c>QueryIsValidContainmentOwner</c> and <c>ExpectedContainmentOwnerTypeName</c>).
    /// </summary>
    public class ContainmentRulePayload
    {
        /// <summary>
        /// Gets or sets the rules for the target-side switch (each bridge metaclass that declares a typed
        /// composite subset of <c>OwnedRelatedElement</c>).
        /// </summary>
        public IReadOnlyList<ContainmentRule> TargetRules { get; set; }

        /// <summary>
        /// Gets or sets the rules for the owner-side switch (each bridge metaclass that declares a typed
        /// subset of <c>OwningRelatedElement</c> with a type strictly narrower than <c>Element</c>).
        /// </summary>
        public IReadOnlyList<ContainmentRule> OwnerRules { get; set; }
    }

    /// <summary>
    /// One arm of a generated switch expression: the bridge metaclass and either its expected target type
    /// (when emitted into the target-side switch) or its expected owner type (when emitted into the
    /// owner-side switch). A given <see cref="ContainmentRule" /> instance carries only one side's fields.
    /// </summary>
    public class ContainmentRule
    {
        /// <summary>
        /// Gets or sets the fully-qualified C# interface name of the bridge metaclass
        /// (e.g. <c>SysML2.NET.Core.POCO.Root.Namespaces.IFeatureMembership</c>).
        /// </summary>
        public string BridgeFqn { get; set; }

        /// <summary>
        /// Gets or sets the simple C# interface name of the bridge metaclass (e.g. <c>IFeatureMembership</c>).
        /// </summary>
        public string BridgeName { get; set; }

        /// <summary>
        /// Gets or sets the fully-qualified C# interface name of the expected target type
        /// (e.g. <c>SysML2.NET.Core.POCO.Core.Features.IFeature</c>). Set only on target-side rules.
        /// </summary>
        public string TargetFqn { get; set; }

        /// <summary>
        /// Gets or sets the simple C# interface name of the expected target type (e.g. <c>IFeature</c>).
        /// Set only on target-side rules.
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Gets or sets the pre-composed C# expression rendered on the right-hand side of the
        /// <c>QueryIsValidForContainment</c> switch arm. Equals <c>target is {TargetFqn}</c> for narrowed
        /// target types, or <c>target is not null</c> when the expected target type is <c>IElement</c>
        /// (the parameter type), to avoid the CA1508 / IDE0078 analyzer warning.
        /// </summary>
        public string TargetCheck { get; set; }

        /// <summary>
        /// Gets or sets the fully-qualified C# interface name of the expected owner type
        /// (e.g. <c>SysML2.NET.Core.POCO.Core.Types.IType</c>). Set only on owner-side rules.
        /// </summary>
        public string OwnerFqn { get; set; }

        /// <summary>
        /// Gets or sets the simple C# interface name of the expected owner type (e.g. <c>IType</c>).
        /// Set only on owner-side rules.
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// Gets or sets the pre-composed C# expression rendered on the right-hand side of the
        /// <c>QueryIsValidContainmentOwner</c> switch arm. Equals <c>source is {OwnerFqn}</c>. The
        /// <c>source is not null</c> short-circuit is unnecessary on the owner side because rules whose
        /// owner type is <c>IElement</c> are filtered out at generation time (they would be equivalent
        /// to the <c>_ =&gt; true</c> default arm).
        /// </summary>
        public string OwnerCheck { get; set; }
    }
}
