// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementVerificationMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class RequirementVerificationMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedRequirement()
        {
            Assert.That(() => ((IRequirementVerificationMembership)null).ComputeOwnedRequirement(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var emptyMembership = new RequirementVerificationMembership();

            Assert.That(() => emptyMembership.ComputeOwnedRequirement(), Throws.TypeOf<IncompleteModelException>());

            // Single non-IRequirementUsage in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var nonRequirementMembership = new RequirementVerificationMembership();
            var nonRequirementElement = new Namespace();
            ((IContainedRelationship)nonRequirementMembership).OwnedRelatedElement.Add(nonRequirementElement);

            Assert.That(() => nonRequirementMembership.ComputeOwnedRequirement(), Throws.TypeOf<IncompleteModelException>());

            // Single IRequirementUsage wired via the public API → returned.
            // RequirementVerificationMembership is a FeatureMembership (via RequirementConstraintMembership)
            // and requires an IType source per AssignOwnership; VerificationCaseDefinition is the natural
            // IType context for verifying a requirement.
            var owningNamespace = new VerificationCaseDefinition();
            var membership = new RequirementVerificationMembership();
            var requirementUsage = new RequirementUsage();
            owningNamespace.AssignOwnership(membership, requirementUsage);

            Assert.That(membership.ComputeOwnedRequirement(), Is.SameAs(requirementUsage));

            // Two IRequirementUsage in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var twoRequirementMembership = new RequirementVerificationMembership();
            var firstReq = new RequirementUsage();
            var secondReq = new RequirementUsage();
            ((IContainedRelationship)twoRequirementMembership).OwnedRelatedElement.Add(firstReq);
            ((IContainedRelationship)twoRequirementMembership).OwnedRelatedElement.Add(secondReq);

            Assert.That(() => twoRequirementMembership.ComputeOwnedRequirement(), Throws.TypeOf<IncompleteModelException>());

            // Mixed: non-IRequirementUsage (Namespace) alongside a single IRequirementUsage —
            // the type filter picks out the RequirementUsage regardless of its position.
            var mixedMembership = new RequirementVerificationMembership();
            var siblingNamespace = new Namespace();
            var mixedReq = new RequirementUsage();
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNamespace);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedReq);

            Assert.That(mixedMembership.ComputeOwnedRequirement(), Is.SameAs(mixedReq));
        }

        [Test]
        public void VerifyComputeVerifiedRequirement()
        {
            Assert.That(() => ((IRequirementVerificationMembership)null).ComputeVerifiedRequirement(), Throws.TypeOf<ArgumentNullException>());

            // Empty ownedRequirement → propagates IncompleteModelException from ownedRequirement's [1..1] guard.
            var emptyMembership = new RequirementVerificationMembership();

            Assert.That(() => emptyMembership.ComputeVerifiedRequirement(), Throws.TypeOf<IncompleteModelException>());

            // Populated, no ownedReferenceSubsetting on the ownedRequirement → referencedFeatureTarget is null
            // → returns ownedRequirement itself.
            // RequirementVerificationMembership is a FeatureMembership (via RequirementConstraintMembership)
            // and requires an IType source per AssignOwnership; VerificationCaseDefinition is the natural
            // IType context for verifying a requirement.
            var owningNamespace = new VerificationCaseDefinition();
            var membership = new RequirementVerificationMembership();
            var ownedRequirement = new RequirementUsage();
            owningNamespace.AssignOwnership(membership, ownedRequirement);

            Assert.That(membership.ComputeVerifiedRequirement(), Is.SameAs(ownedRequirement));

            // Populated, with ownedReferenceSubsetting whose ReferencedFeature is itself a RequirementUsage
            // → returns that RequirementUsage.
            var owningNamespace2 = new VerificationCaseDefinition();
            var membership2 = new RequirementVerificationMembership();
            var ownedRequirement2 = new RequirementUsage();
            owningNamespace2.AssignOwnership(membership2, ownedRequirement2);

            var referencedRequirement = new RequirementUsage();
            var refSubsetting = new ReferenceSubsetting { ReferencedFeature = referencedRequirement };
            ownedRequirement2.AssignOwnership(refSubsetting);

            Assert.That(membership2.ComputeVerifiedRequirement(), Is.SameAs(referencedRequirement));

            // Populated, with ownedReferenceSubsetting whose ReferencedFeature is a non-RequirementUsage Feature
            // → returns null (the OCL "else null" branch).
            var owningNamespace3 = new VerificationCaseDefinition();
            var membership3 = new RequirementVerificationMembership();
            var ownedRequirement3 = new RequirementUsage();
            owningNamespace3.AssignOwnership(membership3, ownedRequirement3);

            var nonRequirementTarget = new PartUsage();
            var refSubsetting3 = new ReferenceSubsetting { ReferencedFeature = nonRequirementTarget };
            ownedRequirement3.AssignOwnership(refSubsetting3);

            Assert.That(membership3.ComputeVerifiedRequirement(), Is.Null);
        }
    }
}
