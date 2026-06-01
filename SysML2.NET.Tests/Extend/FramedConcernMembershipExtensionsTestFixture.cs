// -------------------------------------------------------------------------------------------------
// <copyright file="FramedConcernMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class FramedConcernMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedConcern()
        {
            Assert.That(() => ((IFramedConcernMembership)null).ComputeOwnedConcern(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var emptyMembership = new FramedConcernMembership();

            Assert.That(() => emptyMembership.ComputeOwnedConcern(), Throws.TypeOf<IncompleteModelException>());

            // Single non-IConcernUsage in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var nonConcernMembership = new FramedConcernMembership();
            var nonConcernElement = new Namespace();
            ((IContainedRelationship)nonConcernMembership).OwnedRelatedElement.Add(nonConcernElement);

            Assert.That(() => nonConcernMembership.ComputeOwnedConcern(), Throws.TypeOf<IncompleteModelException>());

            // Single IConcernUsage wired via the public API → returned.
            // FramedConcernMembership is a FeatureMembership and requires an IType source per
            // AssignOwnership; RequirementDefinition is the natural Requirements-namespace IType
            // for a framed concern.
            var owningNamespace = new RequirementDefinition();
            var framedMembership = new FramedConcernMembership();
            var concernUsage = new ConcernUsage();
            owningNamespace.AssignOwnership(framedMembership, concernUsage);

            Assert.That(framedMembership.ComputeOwnedConcern(), Is.SameAs(concernUsage));

            // Two IConcernUsage in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var twoConcernMembership = new FramedConcernMembership();
            var firstConcern = new ConcernUsage();
            var secondConcern = new ConcernUsage();
            ((IContainedRelationship)twoConcernMembership).OwnedRelatedElement.Add(firstConcern);
            ((IContainedRelationship)twoConcernMembership).OwnedRelatedElement.Add(secondConcern);

            Assert.That(() => twoConcernMembership.ComputeOwnedConcern(), Throws.TypeOf<IncompleteModelException>());

            // Mixed: non-IConcernUsage (Namespace) alongside a single IConcernUsage — the type filter
            // picks out the ConcernUsage regardless of its position.
            var mixedMembership = new FramedConcernMembership();
            var siblingNamespace = new Namespace();
            var mixedConcern = new ConcernUsage();
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNamespace);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedConcern);

            Assert.That(mixedMembership.ComputeOwnedConcern(), Is.SameAs(mixedConcern));
        }

        [Test]
        public void VerifyComputeReferencedConcern()
        {
            Assert.That(() => ((IFramedConcernMembership)null).ComputeReferencedConcern(), Throws.TypeOf<ArgumentNullException>());

            // Empty ownedConcern → propagates IncompleteModelException from ownedConcern's [1..1] guard.
            var emptyMembership = new FramedConcernMembership();

            Assert.That(() => emptyMembership.ComputeReferencedConcern(), Throws.TypeOf<IncompleteModelException>());

            // Populated, no ownedReferenceSubsetting on the ownedConcern → referencedFeatureTarget is null
            // → returns ownedConcern itself.
            // FramedConcernMembership is a FeatureMembership and requires an IType source per
            // AssignOwnership; RequirementDefinition is the natural Requirements-namespace IType
            // for a framed concern.
            var owningNamespace = new RequirementDefinition();
            var membership = new FramedConcernMembership();
            var ownedConcern = new ConcernUsage();
            owningNamespace.AssignOwnership(membership, ownedConcern);

            Assert.That(membership.ComputeReferencedConcern(), Is.SameAs(ownedConcern));

            // Populated, with ownedReferenceSubsetting whose ReferencedFeature is itself a ConcernUsage
            // → returns that ConcernUsage.
            var owningNamespace2 = new RequirementDefinition();
            var membership2 = new FramedConcernMembership();
            var ownedConcern2 = new ConcernUsage();
            owningNamespace2.AssignOwnership(membership2, ownedConcern2);

            var referencedConcern = new ConcernUsage();
            var refSubsetting = new ReferenceSubsetting { ReferencedFeature = referencedConcern };
            ownedConcern2.AssignOwnership(refSubsetting);

            Assert.That(membership2.ComputeReferencedConcern(), Is.SameAs(referencedConcern));

            // Populated, with ownedReferenceSubsetting whose ReferencedFeature is a non-ConcernUsage Feature
            // → returns null (the OCL "else null" branch).
            var owningNamespace3 = new RequirementDefinition();
            var membership3 = new FramedConcernMembership();
            var ownedConcern3 = new ConcernUsage();
            owningNamespace3.AssignOwnership(membership3, ownedConcern3);

            var nonConcernTarget = new PartUsage();
            var refSubsetting3 = new ReferenceSubsetting { ReferencedFeature = nonConcernTarget };
            ownedConcern3.AssignOwnership(refSubsetting3);

            Assert.That(membership3.ComputeReferencedConcern(), Is.Null);
        }
    }
}
