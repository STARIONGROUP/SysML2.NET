// ------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class VerificationCaseDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeVerifiedRequirement()
        {
            Assert.That(() => ((IVerificationCaseDefinition)null).ComputeVerifiedRequirement(), Throws.TypeOf<ArgumentNullException>());

            var verificationCaseDefinition = new VerificationCaseDefinition();

            // Empty case A: objectiveRequirement is null (no ObjectiveMembership) → empty list.
            Assert.That(verificationCaseDefinition.ComputeVerifiedRequirement(), Is.Empty);

            // Empty case B: objectiveRequirement is set but its featureMembership has no
            // RequirementVerificationMembership → empty list.
            var objectiveMembership = new ObjectiveMembership();
            var requirementUsage = new RequirementUsage();
            verificationCaseDefinition.AssignOwnership(objectiveMembership, requirementUsage);

            Assert.That(verificationCaseDefinition.ComputeVerifiedRequirement(), Is.Empty);

            // Populated: a RequirementVerificationMembership inside objectiveRequirement.featureMembership,
            // carrying a verifiedRequirement RequirementUsage with no ownedReferenceSubsetting →
            // ComputeVerifiedRequirement falls back to ownedRequirement (the verifiedRequirement itself).
            var requirementVerificationMembership = new RequirementVerificationMembership();
            var verifiedRequirement = new RequirementUsage();
            requirementUsage.AssignOwnership(requirementVerificationMembership, verifiedRequirement);

            Assert.That(verificationCaseDefinition.ComputeVerifiedRequirement(), Is.EqualTo([verifiedRequirement]));
        }
    }
}
