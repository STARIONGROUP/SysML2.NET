// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class FeatureMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedMemberFeature()
        {
            Assert.That(() => ((IFeatureMembership)null).ComputeOwnedMemberFeature(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var featureMembership = new FeatureMembership();

            Assert.That(() => featureMembership.ComputeOwnedMemberFeature(), Throws.TypeOf<IncompleteModelException>());

            // Single IFeature wired via the public API → returned.
            var owningType = new Type();
            var feature = new Feature();

            owningType.AssignOwnership(featureMembership, feature);

            Assert.That(featureMembership.ComputeOwnedMemberFeature(), Is.SameAs(feature));

            // Two IFeatures in OwnedRelatedElement → upper-bound violation: throws MultiplicityViolationException.
            var twoFeatureMembership = new FeatureMembership();
            var firstFeature = new Feature();
            var secondFeature = new Feature();

            ((IContainedRelationship)twoFeatureMembership).OwnedRelatedElement.Add(firstFeature);
            ((IContainedRelationship)twoFeatureMembership).OwnedRelatedElement.Add(secondFeature);

            Assert.That(() => twoFeatureMembership.ComputeOwnedMemberFeature(), Throws.TypeOf<MultiplicityViolationException>());

            // Mixed-type owned related elements: exactly one IFeature alongside a non-IFeature (Namespace).
            // The OfType<IFeature>() projection MUST pick out the IFeature regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new FeatureMembership();
            var siblingNonFeature = new Namespace();
            var mixedFeature = new Feature();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonFeature);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedFeature);

            Assert.That(mixedMembership.ComputeOwnedMemberFeature(), Is.SameAs(mixedFeature));

            // OwnedRelatedElement populated with non-IFeature element(s) only → no IFeature match:
            // [1..1] violation, throws IncompleteModelException.
            var nonFeatureMembership = new FeatureMembership();
            var nonFeatureElement = new Namespace();

            ((IContainedRelationship)nonFeatureMembership).OwnedRelatedElement.Add(nonFeatureElement);

            Assert.That(() => nonFeatureMembership.ComputeOwnedMemberFeature(), Throws.TypeOf<IncompleteModelException>());
        }

        [Test]
        public void VerifyComputeOwningType()
        {
            Assert.That(() => ((IFeatureMembership)null).ComputeOwningType(), Throws.TypeOf<ArgumentNullException>());

            var featureMembership = new FeatureMembership();

            Assert.That(featureMembership.ComputeOwningType(), Is.Null);

            var owningType = new Type();
            var feature = new Feature();

            owningType.AssignOwnership(featureMembership, feature);

            Assert.That(featureMembership.ComputeOwningType(), Is.SameAs(owningType));

            // NOTE: assigning a non-IType as OwningRelatedElement is rejected by the public AssignOwnership
            // guard (IFeatureMembership requires an IType source). We therefore directly set the backing
            // field via the IContainedRelationship explicit interface to exercise the as-cast-returns-null
            // path, which proves ComputeOwningType returns null when the owner is not an IType.
            var nonTypeMembership = new FeatureMembership();
            var nonTypeOwner = new Namespace();

            ((IContainedRelationship)nonTypeMembership).OwningRelatedElement = nonTypeOwner;

            Assert.That(nonTypeMembership.ComputeOwningType(), Is.Null);
        }
    }
}
