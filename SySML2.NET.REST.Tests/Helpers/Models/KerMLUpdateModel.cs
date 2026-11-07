// ------------------------------------------------------------------------------------------------
// <copyright file="KerMLUpdateModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.DataTypes;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Dependencies;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;
    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    /// <summary>
    /// The changes of scenario step 5 to the KerML model of steps 2 to 4, after plan §6.1: U1 renames one instance of
    /// every metaclass, U2 changes the non-derived attributes, U3 retargets relationship ends, and U4 extends owned
    /// relationships, so that every metaclass gets U1 and at least one of U2 to U4
    /// </summary>
    /// <remarks>
    /// Retargeting an owning membership moves its member, and U4 also asks for an owned relationship list reordered and
    /// shrunk; the public SDK can express none of these (#427), so an owning membership gets U2 (its visibility). A
    /// <c>TypeFeaturing</c> keeps its ends, because its featuring type is the type that owns its feature.
    /// </remarks>
    public sealed class KerMLUpdateModel : StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KerMLUpdateModel"/> class and changes the model of steps 2 to 4
        /// </summary>
        /// <param name="root">
        /// The model of step 2
        /// </param>
        /// <param name="core">
        /// The model of step 3
        /// </param>
        /// <param name="kernel">
        /// The model of step 4
        /// </param>
        public KerMLUpdateModel(RootModel root, CoreModel core, KernelModel kernel) : base((kernel ?? throw new ArgumentNullException(nameof(kernel))).Builder, "kerml update")
        {
            ArgumentNullException.ThrowIfNull(root);
            ArgumentNullException.ThrowIfNull(core);

            var firstInstances = this.Builder.Elements.GroupBy(entry => entry.Element.GetType()).Select(group => group.First().Element).ToList();

            this.Rename(firstInstances);
            this.ChangeAttributes(root);
            this.Retarget(core, kernel);
            this.Complete(firstInstances);
            this.Extend(core);
        }

        /// <summary>
        /// Gets the classifier that step 5 adds to the package <c>Core</c>
        /// </summary>
        public Classifier Added { get; private set; }

        /// <summary>
        /// U2: changes every non-derived attribute that steps 2 to 4 set, to another legal value
        /// </summary>
        /// <param name="root">
        /// The model of step 2
        /// </param>
        private void ChangeAttributes(RootModel root)
        {
            root.Root.AliasIds.Add("urn:coverage-model:CoverageModel:2");

            var comment = this.Builder.Find<Comment>("root/Note");
            comment.Body = "A comment about the library.";
            comment.Locale = "nl-NL";

            this.Builder.Find<Documentation>("root/doc").Body = "The coverage model, updated.";

            var representation = this.Builder.Find<TextualRepresentation>("root/Representation");
            representation.Language = "plain";
            representation.Body = "The updated coverage model, in words.";

            this.Builder.Find<LibraryPackage>("root/Library").IsStandard = false;

            var namespaceImport = this.Builder.Find<NamespaceImport>("root/Nested/import Library");
            namespaceImport.IsRecursive = false;
            namespaceImport.IsImportAll = false;
            namespaceImport.Visibility = VisibilityKind.Public;

            var membershipImport = this.Builder.Find<MembershipImport>("root/Nested/import LibAlias");
            membershipImport.IsRecursive = true;
            membershipImport.Visibility = VisibilityKind.Private;

            var alias = this.Builder.Find<Membership>("root/Space/LibAlias");
            alias.MemberName = "LibraryAlias";
            alias.MemberShortName = "lia";
            alias.Visibility = VisibilityKind.Private;

            this.Builder.Find<LiteralBoolean>("root/Library/filter/condition").Value = false;

            this.Builder.Find<Classifier>("core/A").IsAbstract = false;
            this.Builder.Find<Type>("core/T").IsSufficient = false;
            this.Builder.Find<Feature>("core/g").IsDerived = false;

            var h = this.Builder.Find<Feature>("core/h");
            h.IsUnique = true;
            h.IsOrdered = false;
            h.IsComposite = false;
            h.IsPortion = false;
            h.Direction = FeatureDirectionKind.Out;

            this.Builder.Find<Feature>("core/k").Direction = FeatureDirectionKind.In;

            this.Builder.Find<DataType>("kernel/D").IsSufficient = false;

            var c = this.Builder.Find<Feature>("kernel/c");
            c.IsOrdered = false;
            c.IsUnique = true;

            this.Builder.Find<FeatureValue>("kernel/a/value").IsDefault = false;
            this.Builder.Find<LiteralInteger>("kernel/a/value/42").Value = 43;
            this.Builder.Find<LiteralRational>("kernel/rational").Value = 3.5;
            this.Builder.Find<LiteralString>("kernel/text").Value = "t";
            this.Builder.Find<Invariant>("kernel/inv").IsNegated = false;
            this.Builder.Find<LiteralBoolean>("kernel/P/body").Value = false;
        }

        /// <summary>
        /// U4: extends the owned relationships of the package <c>Core</c> with a new classifier
        /// </summary>
        /// <param name="core">
        /// The model of step 3
        /// </param>
        private void Extend(CoreModel core)
        {
            this.Added = this.CreateNamed<Classifier>("Added", "add", ClassifierExtensions.ToDto);
            this.OwnMember(core.Package, this.Added);
        }

        /// <summary>
        /// Gives the first instance of every metaclass that has no attribute or end of its own to change a change of
        /// U2 or U4: a membership changes its visibility, a namespace gets an owned comment, and an annotating element
        /// annotates one more element
        /// </summary>
        /// <param name="firstInstances">
        /// The first instance of every metaclass that steps 2 to 4 created
        /// </param>
        private void Complete(IReadOnlyList<PocoElement> firstInstances)
        {
            var space = this.Builder.Find<Namespace>("root/Space");

            foreach (var element in firstInstances)
            {
                switch (element)
                {
                    case IMembership membership:
                        membership.Visibility = membership.Visibility == VisibilityKind.Private ? VisibilityKind.Public : VisibilityKind.Private;
                        break;
                    case INamespace owner:
                        var comment = this.Builder.Create<Comment>($"{element.ElementId}/update comment", CommentExtensions.ToDto);
                        comment.Body = $"Updated in step 5: {element.GetType().Name}.";
                        this.OwnMember(owner, comment);
                        break;
                    case AnnotatingElement annotating:
                        var annotation = this.Builder.Create<Annotation>($"{element.ElementId}/update annotation", AnnotationExtensions.ToDto);
                        annotation.AnnotatedElement = space;
                        annotating.AssignOwnership(annotation);
                        break;
                }
            }
        }

        /// <summary>
        /// U1: renames the first instance of every metaclass that steps 2 to 4 created
        /// </summary>
        /// <param name="firstInstances">
        /// The first instance of every metaclass that steps 2 to 4 created
        /// </param>
        private void Rename(IReadOnlyList<PocoElement> firstInstances)
        {
            foreach (var element in firstInstances)
            {
                element.DeclaredName = $"{element.DeclaredName ?? element.GetType().Name} renamed";
                element.DeclaredShortName = $"{element.DeclaredShortName ?? element.GetType().Name.ToLowerInvariant()}2";
            }
        }

        /// <summary>
        /// U3: retargets an end of every kind of KerML relationship that steps 2 to 4 created, keeping the model
        /// well-formed; a connector, flow or association is retargeted through the reference or typing of an end
        /// </summary>
        /// <param name="core">
        /// The model of step 3
        /// </param>
        /// <param name="kernel">
        /// The model of step 4
        /// </param>
        private void Retarget(CoreModel core, KernelModel kernel)
        {
            var library = this.Builder.Find<LibraryPackage>("root/Library");
            var space = this.Builder.Find<Namespace>("root/Space");
            var b = this.Builder.Find<Classifier>("core/B");
            var f = this.Builder.Find<Feature>("core/f");
            var k = this.Builder.Find<Feature>("core/k");

            this.Builder.Find<Annotation>("root/Note/about Nested").AnnotatedElement = library;

            var dependency = this.Builder.Find<Dependency>("root/Uses");
            dependency.Supplier.Clear();
            dependency.Supplier.Add(space);

            this.Builder.Find<NamespaceImport>("root/Nested/import Library").ImportedNamespace = space;
            this.Builder.Find<Membership>("root/Space/LibAlias").MemberElement = this.Builder.Find<Package>("root/Nested");

            this.Builder.Find<FeatureTyping>("core/f/typing").Type = core.A;
            this.Builder.Find<Subsetting>("core/f/subsetting").SubsettedFeature = k;
            this.Builder.Find<Redefinition>("core/r/redefinition").RedefinedFeature = f;
            this.Builder.Find<ReferenceSubsetting>("core/k/reference").ReferencedFeature = f;
            this.Builder.Find<Specialization>("core/U/specialization").General = b;
            this.Builder.Find<Conjugation>("core/T/conjugation").OriginalType = b;
            this.Builder.Find<Disjoining>("core/C/disjoining").DisjoiningType = b;
            this.Builder.Find<Unioning>("core/U/unioning 0").UnioningType = core.C;
            this.Builder.Find<Intersecting>("core/U/intersecting 0").IntersectingType = core.C;
            this.Builder.Find<Differencing>("core/U/differencing 0").DifferencingType = core.C;
            this.Builder.Find<FeatureInverting>("core/k/inverting").InvertingFeature = core.G;

            this.Builder.Find<MembershipImport>("root/Nested/import LibAlias").ImportedMembership = (IMembership)core.A.OwningRelationship;
            this.Builder.Find<Subclassification>("kernel/M/subclassification").Superclassifier = kernel.Class;

            var x2 = this.CreateNamed<Feature>("x2", "x2", FeatureExtensions.ToDto);
            this.OwnFeature(core.SecondEnd, x2);
            this.Builder.Find<FeatureChaining>("core/cf/chaining 1").ChainingFeature = x2;

            var chain = this.CreateNamed<Feature>("cf2", "cf2", FeatureExtensions.ToDto);
            this.OwnMember(core.Package, chain);

            foreach (var (link, index) in new[] { core.SecondEnd, x2 }.Select((link, index) => (link, index)))
            {
                var chaining = this.Builder.Create<FeatureChaining>($"cf2/chaining {index}", FeatureChainingExtensions.ToDto);
                chaining.ChainingFeature = link;
                chain.AssignOwnership(chaining);
            }

            this.Builder.Find<CrossSubsetting>("core/e1/cross").CrossedFeature = chain;

            var dataType = this.Builder.Find<DataType>("kernel/D");
            var first = this.Builder.Find<Step>("kernel/s1");
            var second = this.Builder.Find<Step>("kernel/s2");

            this.Builder.Find<ReferenceSubsetting>("kernel/k/target/reference").ReferencedFeature = this.Builder.Find<Feature>("kernel/c");
            this.Builder.Find<ReferenceSubsetting>("kernel/bc/target/reference").ReferencedFeature = this.Builder.Find<Feature>("kernel/c");
            this.Builder.Find<ReferenceSubsetting>("kernel/su/first/reference").ReferencedFeature = second;
            this.Builder.Find<ReferenceSubsetting>("kernel/su/then/reference").ReferencedFeature = first;

            foreach (var flow in new[] { "fl", "sf" })
            {
                this.Builder.Find<ReferenceSubsetting>($"kernel/{flow}/source/reference").ReferencedFeature = second;
                this.Builder.Find<ReferenceSubsetting>($"kernel/{flow}/target/reference").ReferencedFeature = first;
            }

            foreach (var (association, endType) in new[] { ("As", (IType)dataType), ("AsS", this.Builder.Find<SysML2.NET.Core.POCO.Kernel.Structures.Structure>("kernel/S")), ("I", dataType) })
            {
                this.Builder.Find<Feature>($"kernel/{association}/e2").OwnedRelationship.OfType<FeatureTyping>().Single().Type = endType;
            }

            this.Builder.Find<Annotation>("kernel/md/about Cl").AnnotatedElement = dataType;
        }
    }
}
