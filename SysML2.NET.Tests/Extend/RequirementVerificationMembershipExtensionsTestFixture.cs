// ------------------------------------------------------------------------------------------------
// <copyright file="RequirementVerificationMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
    using SysML2.NET.Extensions;

    [TestFixture]
    public class RequirementVerificationMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedRequirement()
        {
            Assert.That(() => ((IRequirementVerificationMembership)null).ComputeOwnedRequirement(), Throws.TypeOf<ArgumentNullException>());

            var emptyMembership = new RequirementVerificationMembership();

            Assert.That(emptyMembership.ComputeOwnedRequirement(), Is.Null);

            var nonRequirementMembership = new RequirementVerificationMembership();
            var nonRequirementElement = new Namespace();
            ((IContainedRelationship)nonRequirementMembership).OwnedRelatedElement.Add(nonRequirementElement);

            Assert.That(nonRequirementMembership.ComputeOwnedRequirement(), Is.Null);

            // Single IRequirementUsage wired via the public API → returned.
            // RequirementVerificationMembership is a FeatureMembership (via RequirementConstraintMembership)
            // and requires an IType source per AssignOwnership; VerificationCaseDefinition is the natural
            // IType context for verifying a requirement.
            var owningNamespace = new VerificationCaseDefinition();
            var membership = new RequirementVerificationMembership();
            var requirementUsage = new RequirementUsage();
            owningNamespace.AssignOwnership(membership, requirementUsage);

            Assert.That(membership.ComputeOwnedRequirement(), Is.SameAs(requirementUsage));

            var twoRequirementMembership = new RequirementVerificationMembership();
            var firstReq = new RequirementUsage();
            var secondReq = new RequirementUsage();
            ((IContainedRelationship)twoRequirementMembership).OwnedRelatedElement.Add(firstReq);
            ((IContainedRelationship)twoRequirementMembership).OwnedRelatedElement.Add(secondReq);

            Assert.That(twoRequirementMembership.ComputeOwnedRequirement(), Is.SameAs(firstReq));

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

            var emptyMembership = new RequirementVerificationMembership();

            Assert.That(emptyMembership.ComputeVerifiedRequirement(), Is.Null);

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
