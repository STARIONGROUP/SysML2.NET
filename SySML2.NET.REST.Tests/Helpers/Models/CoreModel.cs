// ------------------------------------------------------------------------------------------------
// <copyright file="CoreModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    /// <summary>
    /// The model of scenario step 3: the KerML Core metaclasses, in a package <c>Core</c> of the root package
    /// </summary>
    /// <remarks>
    /// <code>
    /// package Core {
    ///     abstract classifier A { feature f : B :&gt; g [m]; feature g { in feature h; } inout feature k ::&gt; g inverse of f; }
    ///     classifier B specializes A { feature r :&gt;&gt; g; }
    ///     type T ~ A;
    ///     type U specializes A unions A, B intersects A, B differences A, B;
    ///     classifier C disjoint from A { end feature e1 crosses cf; end feature e2 { feature x; } }
    /// }
    /// </code>
    /// where <c>cf</c> is the feature chain <c>e2.x</c>, a member of <c>Core</c> because the SDK lets only an owning
    /// membership or an annotation own a related element, and every owned feature is also featured by its owning type. The end features <c>e1</c> and <c>e2</c> get the multiplicity <c>[1]</c> that
    /// KerML requires of an end feature (<c>validateFeatureEndMultiplicity</c>) in step 4, because
    /// <c>MultiplicityRange</c> is a Kernel metaclass. Rules that need the model library (<c>checkTypeSpecialization</c>
    /// and the like) cannot hold without one. The type featuring of <c>f</c> by <c>A</c> is marked implied, so <c>f</c>
    /// includes its implied relationships (<c>validateElementIsImpliedIncluded</c>).
    /// </remarks>
    public sealed class CoreModel : StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreModel"/> class and builds the model on that of step 2
        /// </summary>
        /// <param name="root">
        /// The model of step 2
        /// </param>
        public CoreModel(RootModel root) : base((root ?? throw new ArgumentNullException(nameof(root))).Builder, "core")
        {
            this.Package = this.CreateNamed<Package>("Core", "core", PackageExtensions.ToDto);
            this.OwnMember(root.Root, this.Package);

            this.A = this.CreateNamed<Classifier>("A", "a", ClassifierExtensions.ToDto);
            this.A.IsAbstract = true;
            this.OwnMember(this.Package, this.A);

            var b = this.CreateNamed<Classifier>("B", "b", ClassifierExtensions.ToDto);
            this.OwnMember(this.Package, b);

            var f = this.CreateNamed<Feature>("f", "f", FeatureExtensions.ToDto);
            this.G = this.CreateNamed<Feature>("g", "g", FeatureExtensions.ToDto);
            this.G.IsDerived = true;
            var k = this.CreateNamed<Feature>("k", "k", FeatureExtensions.ToDto);
            k.Direction = FeatureDirectionKind.Inout;
            this.OwnFeature(this.A, f);
            this.OwnFeature(this.A, this.G);
            this.OwnFeature(this.A, k);

            f.OwnedRelationship.OfType<TypeFeaturing>().Single().IsImplied = true;
            f.IsImpliedIncluded = true;

            var h = this.CreateNamed<Feature>("h", "h", FeatureExtensions.ToDto);
            h.Direction = FeatureDirectionKind.In;
            h.IsUnique = false;
            h.IsOrdered = true;
            h.IsComposite = true;
            h.IsPortion = true;
            this.OwnFeature(this.G, h);

            var typing = this.Builder.Create<FeatureTyping>("f/typing", FeatureTypingExtensions.ToDto);
            typing.TypedFeature = f;
            typing.Type = b;
            f.AssignOwnership(typing);

            var subsetting = this.Builder.Create<Subsetting>("f/subsetting", SubsettingExtensions.ToDto);
            subsetting.SubsettingFeature = f;
            subsetting.SubsettedFeature = this.G;
            f.AssignOwnership(subsetting);

            var multiplicity = this.CreateNamed<Multiplicity>("m", "m", MultiplicityExtensions.ToDto);
            this.OwnMember(f, multiplicity);

            var multiplicityFeaturing = this.Builder.Create<TypeFeaturing>("m/featuring", TypeFeaturingExtensions.ToDto);
            multiplicityFeaturing.FeatureOfType = multiplicity;
            multiplicityFeaturing.FeaturingType = this.A;
            multiplicity.AssignOwnership(multiplicityFeaturing);

            var referenceSubsetting = this.Builder.Create<ReferenceSubsetting>("k/reference", ReferenceSubsettingExtensions.ToDto);
            referenceSubsetting.ReferencedFeature = this.G;
            k.AssignOwnership(referenceSubsetting);

            var inverting = this.Builder.Create<FeatureInverting>("k/inverting", FeatureInvertingExtensions.ToDto);
            inverting.FeatureInverted = k;
            inverting.InvertingFeature = f;
            k.AssignOwnership(inverting);

            var subclassification = this.Builder.Create<Subclassification>("B/subclassification", SubclassificationExtensions.ToDto);
            subclassification.Subclassifier = b;
            subclassification.Superclassifier = this.A;
            b.AssignOwnership(subclassification);

            var r = this.CreateNamed<Feature>("r", "r", FeatureExtensions.ToDto);
            this.OwnFeature(b, r);

            var redefinition = this.Builder.Create<Redefinition>("r/redefinition", RedefinitionExtensions.ToDto);
            redefinition.RedefiningFeature = r;
            redefinition.RedefinedFeature = this.G;
            r.AssignOwnership(redefinition);

            var t = this.CreateNamed<Type>("T", "t", TypeExtensions.ToDto);
            t.IsSufficient = true;
            this.OwnMember(this.Package, t);

            var conjugation = this.Builder.Create<Conjugation>("T/conjugation", ConjugationExtensions.ToDto);
            conjugation.ConjugatedType = t;
            conjugation.OriginalType = this.A;
            t.AssignOwnership(conjugation);

            var u = this.CreateNamed<Type>("U", "u", TypeExtensions.ToDto);
            this.OwnMember(this.Package, u);

            var specialization = this.Builder.Create<Specialization>("U/specialization", SpecializationExtensions.ToDto);
            specialization.Specific = u;
            specialization.General = this.A;
            u.AssignOwnership(specialization);

            foreach (var (operand, index) in new[] { (IType)this.A, b }.Select((operand, index) => (operand, index)))
            {
                var unioning = this.Builder.Create<Unioning>($"U/unioning {index}", UnioningExtensions.ToDto);
                unioning.UnioningType = operand;
                u.AssignOwnership(unioning);

                var intersecting = this.Builder.Create<Intersecting>($"U/intersecting {index}", IntersectingExtensions.ToDto);
                intersecting.IntersectingType = operand;
                u.AssignOwnership(intersecting);

                var differencing = this.Builder.Create<Differencing>($"U/differencing {index}", DifferencingExtensions.ToDto);
                differencing.DifferencingType = operand;
                u.AssignOwnership(differencing);
            }

            this.C = this.CreateNamed<Classifier>("C", "c", ClassifierExtensions.ToDto);
            this.OwnMember(this.Package, this.C);

            var disjoining = this.Builder.Create<Disjoining>("C/disjoining", DisjoiningExtensions.ToDto);
            disjoining.TypeDisjoined = this.C;
            disjoining.DisjoiningType = this.A;
            this.C.AssignOwnership(disjoining);

            this.FirstEnd = this.CreateNamed<Feature>("e1", "e1", FeatureExtensions.ToDto);
            this.FirstEnd.IsEnd = true;
            this.SecondEnd = this.CreateNamed<Feature>("e2", "e2", FeatureExtensions.ToDto);
            this.SecondEnd.IsEnd = true;
            this.OwnFeature(this.C, this.FirstEnd);
            this.OwnFeature(this.C, this.SecondEnd);

            var x = this.CreateNamed<Feature>("x", "x", FeatureExtensions.ToDto);
            this.OwnFeature(this.SecondEnd, x);

            var chain = this.CreateNamed<Feature>("cf", "cf", FeatureExtensions.ToDto);
            this.OwnMember(this.Package, chain);

            var crossSubsetting = this.Builder.Create<CrossSubsetting>("e1/cross", CrossSubsettingExtensions.ToDto);
            crossSubsetting.CrossedFeature = chain;
            this.FirstEnd.AssignOwnership(crossSubsetting);

            foreach (var (link, index) in new[] { this.SecondEnd, x }.Select((link, index) => (link, index)))
            {
                var chaining = this.Builder.Create<FeatureChaining>($"cf/chaining {index}", FeatureChainingExtensions.ToDto);
                chaining.ChainingFeature = link;
                chain.AssignOwnership(chaining);
            }
        }

        /// <summary>
        /// Gets the package that holds the step
        /// </summary>
        public Package Package { get; }

        /// <summary>
        /// Gets the abstract classifier <c>A</c>, which most relationships of the step target
        /// </summary>
        public Classifier A { get; }

        /// <summary>
        /// Gets the classifier <c>C</c>, which owns the end features
        /// </summary>
        public Classifier C { get; }

        /// <summary>
        /// Gets the feature <c>g</c> of <c>A</c>, which is subsetted and redefined
        /// </summary>
        public Feature G { get; }

        /// <summary>
        /// Gets the end feature <c>e1</c> of <c>C</c>
        /// </summary>
        public Feature FirstEnd { get; }

        /// <summary>
        /// Gets the end feature <c>e2</c> of <c>C</c>
        /// </summary>
        public Feature SecondEnd { get; }
    }
}
