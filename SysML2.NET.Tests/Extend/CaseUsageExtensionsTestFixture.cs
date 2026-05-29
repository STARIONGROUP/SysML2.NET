// -------------------------------------------------------------------------------------------------
// <copyright file="CaseUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class CaseUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeActorParameter()
        {
            Assert.That(() => ((ICaseUsage)null).ComputeActorParameter(), Throws.TypeOf<ArgumentNullException>());

            var caseUsage = new CaseUsage();

            Assert.That(caseUsage.ComputeActorParameter(), Is.Empty);

            // Discrimination: add a ParameterMembership (not ActorMembership) — must be excluded from result.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            caseUsage.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(caseUsage.ComputeActorParameter(), Is.Empty);

            // For Later: populated case depends on IActorMembership.ComputeOwnedActorParameter, which is still a stub.
            var actorMembership = new ActorMembership();
            var actorPartUsage = new PartUsage();
            caseUsage.AssignOwnership(actorMembership, actorPartUsage);

            Assert.That(() => caseUsage.ComputeActorParameter(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeCaseDefinition()
        {
            Assert.That(() => ((ICaseUsage)null).ComputeCaseDefinition(), Throws.TypeOf<ArgumentNullException>());

            var caseUsage = new CaseUsage();

            // Empty case: no FeatureTyping whose Type is an ICaseDefinition → null.
            Assert.That(caseUsage.ComputeCaseDefinition(), Is.Null);

            // Negative case: FeatureTyping whose Type is a Usage (not ICaseDefinition) — no match → null.
            var nonDefinitionTyping = new FeatureTyping { Type = new Usage() };
            caseUsage.AssignOwnership(nonDefinitionTyping);

            Assert.That(caseUsage.ComputeCaseDefinition(), Is.Null);

            // Populated case: FeatureTyping whose Type is a CaseDefinition → returns the CaseDefinition.
            var caseDefinition = new CaseDefinition();
            var caseDefinitionTyping = new FeatureTyping { Type = caseDefinition };
            caseUsage.AssignOwnership(caseDefinitionTyping);

            Assert.That(caseUsage.ComputeCaseDefinition(), Is.SameAs(caseDefinition));
        }

        [Test]
        public void VerifyComputeObjectiveRequirement()
        {
            Assert.That(() => ((ICaseUsage)null).ComputeObjectiveRequirement(), Throws.TypeOf<ArgumentNullException>());

            var caseUsage = new CaseUsage();

            // Empty case: no ObjectiveMembership in featureMembership → null.
            Assert.That(caseUsage.ComputeObjectiveRequirement(), Is.Null);

            // Discrimination: add a ParameterMembership (not ObjectiveMembership) — still null.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            caseUsage.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(caseUsage.ComputeObjectiveRequirement(), Is.Null);

            // Populated case: ObjectiveMembership owns a RequirementUsage → returns the RequirementUsage.
            var objectiveMembership = new ObjectiveMembership();
            var requirementUsage = new RequirementUsage();
            caseUsage.AssignOwnership(objectiveMembership, requirementUsage);

            Assert.That(caseUsage.ComputeObjectiveRequirement(), Is.SameAs(requirementUsage));
        }

        [Test]
        public void VerifyComputeSubjectParameter()
        {
            Assert.That(() => ((ICaseUsage)null).ComputeSubjectParameter(), Throws.TypeOf<ArgumentNullException>());

            var caseUsage = new CaseUsage();

            // Empty case: no SubjectMembership in featureMembership → null.
            Assert.That(caseUsage.ComputeSubjectParameter(), Is.Null);

            // Discrimination: add a ParameterMembership (not SubjectMembership) → still null.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            caseUsage.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(caseUsage.ComputeSubjectParameter(), Is.Null);

            // Populated case: SubjectMembership is present alongside the earlier ParameterMembership.
            // OfType<ISubjectMembership> must discriminate — only the subject's ownedSubjectParameter surfaces.
            var subjectMembership = new SubjectMembership();
            var subjectUsage = new Usage();
            caseUsage.AssignOwnership(subjectMembership, subjectUsage);

            Assert.That(caseUsage.ComputeSubjectParameter(), Is.SameAs(subjectUsage));
        }
    }
}
