// -------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class VerificationCaseUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeVerificationCaseDefinition()
        {
            Assert.That(() => ((IVerificationCaseUsage)null).ComputeVerificationCaseDefinition(), Throws.TypeOf<ArgumentNullException>());

            var verificationCaseUsage = new VerificationCaseUsage();

            // Empty case: no FeatureTyping whose Type is an IVerificationCaseDefinition → null.
            Assert.That(verificationCaseUsage.ComputeVerificationCaseDefinition(), Is.Null);

            // Negative case: FeatureTyping whose Type is a Usage (not IVerificationCaseDefinition) — no match → null.
            var nonDefinitionTyping = new FeatureTyping { Type = new Usage() };
            verificationCaseUsage.AssignOwnership(nonDefinitionTyping);

            Assert.That(verificationCaseUsage.ComputeVerificationCaseDefinition(), Is.Null);

            // Populated case: FeatureTyping whose Type is a VerificationCaseDefinition → returns the VerificationCaseDefinition.
            var verificationCaseDefinition = new VerificationCaseDefinition();
            var verificationCaseDefinitionTyping = new FeatureTyping { Type = verificationCaseDefinition };
            verificationCaseUsage.AssignOwnership(verificationCaseDefinitionTyping);

            Assert.That(verificationCaseUsage.ComputeVerificationCaseDefinition(), Is.SameAs(verificationCaseDefinition));
        }

        [Test]
        public void VerifyComputeVerifiedRequirement()
        {
            Assert.That(() => ((IVerificationCaseUsage)null).ComputeVerifiedRequirement(), Throws.TypeOf<ArgumentNullException>());

            var verificationCaseUsage = new VerificationCaseUsage();

            // Empty case A: objectiveRequirement is null (no ObjectiveMembership) → empty list.
            Assert.That(verificationCaseUsage.ComputeVerifiedRequirement(), Is.Empty);

            // Empty case B: objectiveRequirement is set but its featureMembership has no
            // RequirementVerificationMembership → empty list.
            var objectiveMembership = new ObjectiveMembership();
            var requirementUsage = new RequirementUsage();
            verificationCaseUsage.AssignOwnership(objectiveMembership, requirementUsage);

            Assert.That(verificationCaseUsage.ComputeVerifiedRequirement(), Is.Empty);

            // For Later: populated case depends on IRequirementVerificationMembership.ComputeVerifiedRequirement, which is still a stub.
            var requirementVerificationMembership = new RequirementVerificationMembership();
            var verifiedRequirement = new RequirementUsage();
            requirementUsage.AssignOwnership(requirementVerificationMembership, verifiedRequirement);

            Assert.That(() => verificationCaseUsage.ComputeVerifiedRequirement(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
