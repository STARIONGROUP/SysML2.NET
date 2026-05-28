// -------------------------------------------------------------------------------------------------
// <copyright file="AssociationExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AssociationExtensionsTestFixture
    {
        [Test]
        public void Verify_ComputeAssociationEnd()
        {
            Assert.That(
                () => ((IAssociation)null).ComputeAssociationEnd(),
                Throws.TypeOf<ArgumentNullException>());

            var emptyAssociation = new Association();

            Assert.That(emptyAssociation.ComputeAssociationEnd(), Is.Empty);

            var nonEndOnlyAssociation = new Association();
            var nonEndFeature = new Feature { IsEnd = false };
            nonEndOnlyAssociation.AssignOwnership(new FeatureMembership(), nonEndFeature);

            Assert.That(nonEndOnlyAssociation.ComputeAssociationEnd(), Is.Empty);

            var mixedAssociation = new Association();
            var mixedNonEnd = new Feature { IsEnd = false };
            var mixedEnd = new Feature { IsEnd = true };
            mixedAssociation.AssignOwnership(new FeatureMembership(), mixedNonEnd);
            mixedAssociation.AssignOwnership(new FeatureMembership(), mixedEnd);

            Assert.That(mixedAssociation.ComputeAssociationEnd(), Is.EqualTo(new[] { mixedEnd }));

            var binaryAssociation = new Association();
            var firstEnd = new Feature { IsEnd = true };
            var secondEnd = new Feature { IsEnd = true };
            binaryAssociation.AssignOwnership(new FeatureMembership(), firstEnd);
            binaryAssociation.AssignOwnership(new FeatureMembership(), secondEnd);

            Assert.That(binaryAssociation.ComputeAssociationEnd(), Is.EqualTo(new[] { firstEnd, secondEnd }));
        }

        [Test]
        public void Verify_ComputeRelatedType()
        {
            Assert.That(
                () => ((IAssociation)null).ComputeRelatedType(),
                Throws.TypeOf<ArgumentNullException>());

            var emptyAssociation = new Association();

            Assert.That(emptyAssociation.ComputeRelatedType(), Is.Empty);

            var oneEndAssociation = new Association();
            var soleType = new Classifier();
            oneEndAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(soleType));

            Assert.That(oneEndAssociation.ComputeRelatedType(), Is.EqualTo(new[] { soleType }));

            var twoEndAssociation = new Association();
            var firstType = new Classifier();
            var secondType = new Classifier();
            twoEndAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(firstType));
            twoEndAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(secondType));

            Assert.That(twoEndAssociation.ComputeRelatedType(), Is.EqualTo(new[] { firstType, secondType }));

            var sharedTypeAssociation = new Association();
            var sharedType = new Classifier();
            sharedTypeAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(sharedType));
            sharedTypeAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(sharedType));

            // isUnique=false on relatedType — duplicates are retained.
            Assert.That(sharedTypeAssociation.ComputeRelatedType(), Is.EqualTo(new[] { sharedType, sharedType }));
        }

        [Test]
        public void Verify_ComputeSourceType()
        {
            Assert.That(
                () => ((IAssociation)null).ComputeSourceType(),
                Throws.TypeOf<ArgumentNullException>());

            var emptyAssociation = new Association();

            Assert.That(emptyAssociation.ComputeSourceType(), Is.Null);

            var oneRelatedAssociation = new Association();
            var oneRelatedType = new Classifier();
            oneRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(oneRelatedType));

            Assert.That(oneRelatedAssociation.ComputeSourceType(), Is.SameAs(oneRelatedType));

            var twoRelatedAssociation = new Association();
            var sourceType = new Classifier();
            var targetType = new Classifier();
            twoRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(sourceType));
            twoRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(targetType));

            Assert.That(twoRelatedAssociation.ComputeSourceType(), Is.SameAs(sourceType));
        }

        [Test]
        public void Verify_ComputeTargetType()
        {
            Assert.That(
                () => ((IAssociation)null).ComputeTargetType(),
                Throws.TypeOf<ArgumentNullException>());

            var emptyAssociation = new Association();

            Assert.That(emptyAssociation.ComputeTargetType(), Is.Empty);

            var oneRelatedAssociation = new Association();
            oneRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(new Classifier()));

            Assert.That(oneRelatedAssociation.ComputeTargetType(), Is.Empty);

            var twoRelatedAssociation = new Association();
            var twoRelatedTargetType = new Classifier();
            twoRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(new Classifier()));
            twoRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(twoRelatedTargetType));

            Assert.That(twoRelatedAssociation.ComputeTargetType(), Is.EqualTo(new[] { twoRelatedTargetType }));

            var threeRelatedAssociation = new Association();
            var threeMiddleType = new Classifier();
            var threeTargetType = new Classifier();
            threeRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(new Classifier()));
            threeRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(threeMiddleType));
            threeRelatedAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(threeTargetType));

            Assert.That(threeRelatedAssociation.ComputeTargetType(), Is.EqualTo(new[] { threeMiddleType, threeTargetType }));

            var dupTailAssociation = new Association();
            var dupSharedType = new Classifier();
            dupTailAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(new Classifier()));
            dupTailAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(dupSharedType));
            dupTailAssociation.AssignOwnership(new FeatureMembership(), CreateEndFeature(dupSharedType));

            // asOrderedSet() in OCL → Distinct() in C#.
            Assert.That(dupTailAssociation.ComputeTargetType(), Is.EqualTo(new[] { dupSharedType }));
        }

        /// <summary>
        /// Build an end Feature with a FeatureTyping pointing to <paramref name="featureType" />.
        /// </summary>
        private static Feature CreateEndFeature(IType featureType)
        {
            var feature = new Feature { IsEnd = true };
            feature.AssignOwnership(new FeatureTyping { Type = featureType });

            return feature;
        }
    }
}
