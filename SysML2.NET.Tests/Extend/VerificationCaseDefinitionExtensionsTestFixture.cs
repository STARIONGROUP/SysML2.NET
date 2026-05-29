// -------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

            // For Later: populated case depends on IRequirementVerificationMembership.ComputeVerifiedRequirement, which is still a stub.
            var requirementVerificationMembership = new RequirementVerificationMembership();
            var verifiedRequirement = new RequirementUsage();
            requirementUsage.AssignOwnership(requirementVerificationMembership, verifiedRequirement);

            Assert.That(() => verificationCaseDefinition.ComputeVerifiedRequirement(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
