// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using ElementExtensions = SysML2.NET.Extensions.ElementExtensions;
    using IContainedRelationship = SysML2.NET.Core.POCO.Root.Elements.IContainedRelationship;

    [TestFixture]
    public class ElementExtensionsTestFixture
    {
        private static readonly string[] OneElementSequence = ["only"];
        private static readonly string[] TwoElementSequence = ["first", "second"];
        private static readonly string[] ThreeElementSequence = ["a", "b", "c"];

        private PartDefinition source;
        private FeatureMembership bridgeRelationship;
        private Specialization referenceBridgeRelationship;
        private Feature target;

        [SetUp]
        public void SetUp()
        {
            this.source = new PartDefinition();
            this.bridgeRelationship = new FeatureMembership();

            // Specialization's owner-side narrowing is IType — PartDefinition IS-A IType, so the fixture
            // source can validly own the reference bridge under the stricter owner-type guard.
            this.referenceBridgeRelationship = new Specialization();
            this.target = new Feature();
        }

        [Test]
        public void AssignOwnership_OwnerOnly_WithContainmentCycle_ThrowsInvalidOperationException()
        {
            // Arrange: bridge already directly contains source so that source.OwningRelationship == bridge.
            ((IContainedRelationship)this.referenceBridgeRelationship).OwnedRelatedElement.Add(this.source);

            Assert.That(
                () => this.source.AssignOwnership(this.referenceBridgeRelationship),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("Containment cycle"));
        }

        [Test]
        public void AssignOwnership_OwnerOnly_WithInvalidArguments_Throws()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => ElementExtensions.AssignOwnership(null, this.referenceBridgeRelationship), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.source.AssignOwnership(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.referenceBridgeRelationship.AssignOwnership(this.referenceBridgeRelationship), Throws.TypeOf<InvalidOperationException>());
                Assert.That(() => this.source.AssignOwnership(this.bridgeRelationship), Throws.TypeOf<InvalidOperationException>());
            }
        }

        [Test]
        public void AssignOwnership_OwnerOnly_WithMembershipAndNonNamespaceSource_ThrowsInvalidOperationException()
        {
            var nonNamespaceSource = new Comment();
            var membership = new Membership();

            Assert.That(
                () => nonNamespaceSource.AssignOwnership(membership),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("INamespace"));
        }

        [Test]
        public void AssignOwnership_OwnerOnly_WithValidParameters_AssignsOwnershipCorrectly()
        {
            this.source.AssignOwnership(this.referenceBridgeRelationship);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.referenceBridgeRelationship.OwningRelatedElement, Is.EqualTo(this.source));
                Assert.That(this.source.OwnedRelationship, Does.Contain(this.referenceBridgeRelationship));
                Assert.That(this.referenceBridgeRelationship.OwnedRelatedElement, Is.Empty);
            }
        }

        [Test]
        public void AssignOwnership_WithAnnotation_AssignsOwnershipCorrectly()
        {
            var annotation = new Annotation();
            var comment = new Comment();

            this.source.AssignOwnership(annotation, comment);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(annotation.OwningRelatedElement, Is.EqualTo(this.source));
                Assert.That(this.source.OwnedRelationship, Does.Contain(annotation));
                Assert.That(annotation.OwnedRelatedElement, Does.Contain(comment));
            }
        }

        [Test]
        public void AssignOwnership_WithBridgeEqualsTarget_ThrowsInvalidOperationException()
        {
            Assert.That(
                () => this.source.AssignOwnership(this.bridgeRelationship, this.bridgeRelationship),
                Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void AssignOwnership_WithCompatibleTargetType_AssignsOwnershipCorrectly()
        {
            // FeatureValue's owner-side narrowing is IFeature, so the owner must be a Feature.
            var featureValueOwner = new Feature();
            var featureValue = new FeatureValue();
            var literal = new LiteralBoolean();

            var annotationOwner = new PartDefinition();
            var annotation = new Annotation();
            var comment = new Comment();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(
                    () => featureValueOwner.AssignOwnership(featureValue, literal),
                    Throws.Nothing);

                Assert.That(
                    () => annotationOwner.AssignOwnership(annotation, comment),
                    Throws.Nothing);
            }
        }

        [Test]
        public void AssignOwnership_WithContainmentCycle_ThrowsInvalidOperationException()
        {
            // Scenario 1: bridge directly contains source.
            var bridgeWithSourceInside = new FeatureMembership();
            var sourceInsideBridge = new PartDefinition();
            ((IContainedRelationship)bridgeWithSourceInside).OwnedRelatedElement.Add(sourceInsideBridge);

            // Scenario 2: target transitively contains source through a multi-level chain.
            // Features are used throughout because FeatureMembership requires its target to be an IFeature
            // (enforced by the QueryIsValidForContainment guard); Feature is also an INamespace, so it can
            // serve as the owner of further FeatureMemberships in the chain.
            var ancestorTarget = new Feature();
            var midMembership = new FeatureMembership();
            var midElement = new Feature();
            ancestorTarget.AssignOwnership(midMembership, midElement);

            var leafMembership = new FeatureMembership();
            var leafElement = new Feature();
            midElement.AssignOwnership(leafMembership, leafElement);

            var newBridge = new FeatureMembership();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(
                    () => sourceInsideBridge.AssignOwnership(bridgeWithSourceInside, new Feature()),
                    Throws.TypeOf<InvalidOperationException>().With.Message.Contains("Containment cycle"));

                Assert.That(
                    () => leafElement.AssignOwnership(newBridge, ancestorTarget),
                    Throws.TypeOf<InvalidOperationException>().With.Message.Contains("Containment cycle"));
            }
        }

        [Test]
        public void AssignOwnership_WithIncompatibleOwnerType_ThrowsInvalidOperationException()
        {
            // Package IS-A INamespace but NOT IType; FeatureMembership requires its owner to be IType
            // per the KerML specification. The coarse "is IMembership → source is INamespace" check
            // (the previous, lax behaviour) would have accepted this. The stricter generated guard rejects it.
            var packageSource = new Package();
            var featureMembership = new FeatureMembership();
            var memberFeature = new Feature();

            Assert.That(
                () => packageSource.AssignOwnership(featureMembership, memberFeature),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("not a valid containment owner"));
        }

        [Test]
        public void AssignOwnership_WithIncompatibleTargetType_ThrowsInvalidOperationException()
        {
            using (Assert.EnterMultipleScope())
            {
                // FeatureMembership requires an IFeature target; Comment is not an IFeature.
                Assert.That(
                    () => this.source.AssignOwnership(this.bridgeRelationship, new Comment()),
                    Throws.TypeOf<InvalidOperationException>().With.Message.Contains("not a valid containment target"));

                // FeatureValue requires an IExpression target; Feature is not an IExpression.
                // The owner (a Feature) is valid since FeatureValue requires an IFeature owner.
                var featureValueOwner = new Feature();
                var featureValue = new FeatureValue();

                Assert.That(
                    () => featureValueOwner.AssignOwnership(featureValue, new Feature()),
                    Throws.TypeOf<InvalidOperationException>().With.Message.Contains("not a valid containment target"));

                // Annotation requires an IAnnotatingElement target; Feature is not an IAnnotatingElement.
                var annotationOwner = new PartDefinition();
                var annotation = new Annotation();

                Assert.That(
                    () => annotationOwner.AssignOwnership(annotation, new Feature()),
                    Throws.TypeOf<InvalidOperationException>().With.Message.Contains("not a valid containment target"));
            }
        }

        [Test]
        public void AssignOwnership_WithMembershipAndNonNamespaceSource_ThrowsInvalidOperationException()
        {
            // Comment is neither an INamespace nor an IType, so a generic OwningMembership owner check
            // (requires INamespace) fires.
            var nonNamespaceSource = new Comment();
            var owningMembership = new OwningMembership();
            var memberElement = new Feature();

            Assert.That(
                () => nonNamespaceSource.AssignOwnership(owningMembership, memberElement),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("INamespace"));
        }

        [Test]
        public void AssignOwnership_WithNullBridge_ThrowsArgumentNullException()
        {
            Assert.That(
                () => this.source.AssignOwnership(null, this.target),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void AssignOwnership_WithNullSource_ThrowsArgumentNullException()
        {
            Assert.That(
                () => ElementExtensions.AssignOwnership(null, this.bridgeRelationship, this.target),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void AssignOwnership_WithNullTarget_ThrowsArgumentNullException()
        {
            Assert.That(
                () => this.source.AssignOwnership(this.bridgeRelationship, null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void AssignOwnership_WithReferenceOnlyRelationship_ThrowsInvalidOperationException()
        {
            Assert.That(
                () => this.source.AssignOwnership(this.referenceBridgeRelationship, this.target),
                Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void AssignOwnership_WithSourceEqualsTarget_ThrowsInvalidOperationException()
        {
            Assert.That(
                () => this.source.AssignOwnership(this.bridgeRelationship, this.source),
                Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void AssignOwnership_WithValidParameters_AssignsOwnershipCorrectly()
        {
            this.source.AssignOwnership(this.bridgeRelationship, this.target);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.bridgeRelationship.OwningRelatedElement, Is.EqualTo(this.source));
                Assert.That(this.source.OwnedRelationship, Does.Contain(this.bridgeRelationship));
                Assert.That(this.bridgeRelationship.OwnedRelatedElement, Does.Contain(this.target));
            }
        }

        [Test]
        public void VerifySingleStrictWithOfTypeFilter()
        {
            // IEnumerable overload (with implicit OfType<TResult>): used when the source is wider than
            // the desired result type — bundles OfType<TResult>() + SingleStrict in one call.

            // Empty input → IncompleteModelException (lower-bound violation; the [1..1] property is missing).
            Assert.That(
                () => new List<IElement>().SingleStrict<IFeature>("subject"),
                Throws.TypeOf<IncompleteModelException>());

            // Only non-matching elements → IncompleteModelException (no IFeature match).
            IReadOnlyList<IElement> nonMatchingOnly = new List<IElement> { new Comment(), new PartDefinition() };

            Assert.That(
                () => nonMatchingOnly.SingleStrict<IFeature>("subject"),
                Throws.TypeOf<IncompleteModelException>());

            // Exactly one match among non-matching siblings → returns the match (position-agnostic).
            var feature = new Feature();
            IReadOnlyList<IElement> singleMatch = new List<IElement> { new Comment(), feature, new PartDefinition() };
            Assert.That(singleMatch.SingleStrict<IFeature>("subject"), Is.SameAs(feature));

            // Two matches → MultiplicityViolationException (upper-bound violation).
            IReadOnlyList<IElement> twoMatches = new List<IElement> { new Feature(), new Feature() };

            Assert.That(
                () => twoMatches.SingleStrict<IFeature>("subject"),
                Throws.TypeOf<MultiplicityViolationException>());

            // Three matches → MultiplicityViolationException (early-exit not regressed).
            IReadOnlyList<IElement> threeMatches = new List<IElement> { new Feature(), new Feature(), new Feature() };

            Assert.That(
                () => threeMatches.SingleStrict<IFeature>("subject"),
                Throws.TypeOf<MultiplicityViolationException>());
        }

        [Test]
        public void VerifySingleOrDefaultStrictWithOfTypeFilter()
        {
            // IEnumerable overload (with implicit OfType<TResult>): used when the source is wider than
            // the desired result type — bundles OfType<TResult>() + SingleOrDefaultStrict in one call.

            // Empty input → null (lower bound is 0; this is the only difference vs SingleStrict<TResult>).
            Assert.That(
                new List<IElement>().SingleOrDefaultStrict<IFeature>("subject"),
                Is.Null);

            // Only non-matching elements → null.
            IReadOnlyList<IElement> nonMatchingOnly = new List<IElement> { new Comment(), new PartDefinition() };
            Assert.That(nonMatchingOnly.SingleOrDefaultStrict<IFeature>("subject"), Is.Null);

            // Exactly one match among non-matching siblings → returns the match (position-agnostic).
            var feature = new Feature();
            IReadOnlyList<IElement> singleMatch = new List<IElement> { new Comment(), feature, new PartDefinition() };
            Assert.That(singleMatch.SingleOrDefaultStrict<IFeature>("subject"), Is.SameAs(feature));

            // Two matches → MultiplicityViolationException (upper-bound violation against the [0..1] property).
            IReadOnlyList<IElement> twoMatches = new List<IElement> { new Feature(), new Feature() };

            Assert.That(
                () => twoMatches.SingleOrDefaultStrict<IFeature>("subject"),
                Throws.TypeOf<MultiplicityViolationException>());

            // Three matches → MultiplicityViolationException (early-exit not regressed).
            IReadOnlyList<IElement> threeMatches = new List<IElement> { new Feature(), new Feature(), new Feature() };

            Assert.That(
                () => threeMatches.SingleOrDefaultStrict<IFeature>("subject"),
                Throws.TypeOf<MultiplicityViolationException>());
        }

        [Test]
        public void VerifySingleStrict()
        {
            // Empty sequence → IncompleteModelException (lower-bound violation; the [1..1] property is missing).
            Assert.That(
                () => Enumerable.Empty<string>().SingleStrict("subject"),
                Throws.TypeOf<IncompleteModelException>());

            // Exactly one element → returns it.
            Assert.That(OneElementSequence.SingleStrict("subject"), Is.EqualTo("only"));

            // Two elements → MultiplicityViolationException (upper-bound violation).
            Assert.That(
                () => TwoElementSequence.SingleStrict("subject"),
                Throws.TypeOf<MultiplicityViolationException>());

            // Three elements → MultiplicityViolationException (early-exit not regressed).
            Assert.That(
                () => ThreeElementSequence.SingleStrict("subject"),
                Throws.TypeOf<MultiplicityViolationException>());

            // Explicit OfType<T>() chain — same contract as the IEnumerable overload SingleStrict<TResult>.
            IReadOnlyList<IElement> threeFeatures = new List<IElement> { new Feature(), new Feature(), new Feature() };

            Assert.That(
                () => threeFeatures.OfType<IFeature>().SingleStrict("subject"),
                Throws.TypeOf<MultiplicityViolationException>());
        }

        [Test]
        public void VerifySingleOrDefaultStrict()
        {
            // Empty sequence → null (lower bound is 0; the only difference vs SingleStrict).
            Assert.That(Enumerable.Empty<string>().SingleOrDefaultStrict("subject"), Is.Null);

            // Exactly one element → returns it.
            Assert.That(OneElementSequence.SingleOrDefaultStrict("subject"), Is.EqualTo("only"));

            // Two elements → MultiplicityViolationException (upper-bound violation against the [0..1] property).
            Assert.That(
                () => TwoElementSequence.SingleOrDefaultStrict("subject"),
                Throws.TypeOf<MultiplicityViolationException>());

            // Three elements → MultiplicityViolationException (early-exit not regressed).
            Assert.That(
                () => ThreeElementSequence.SingleOrDefaultStrict("subject"),
                Throws.TypeOf<MultiplicityViolationException>());

            // Explicit OfType<T>() chain — same contract as the IEnumerable overload SingleOrDefaultStrict<TResult>.
            IReadOnlyList<IElement> twoFeatures = new List<IElement> { new Feature(), new Feature() };

            Assert.That(
                () => twoFeatures.OfType<IFeature>().SingleOrDefaultStrict("subject"),
                Throws.TypeOf<MultiplicityViolationException>());
        }
    }
}
