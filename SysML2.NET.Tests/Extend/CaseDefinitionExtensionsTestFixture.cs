// ------------------------------------------------------------------------------------------------
// <copyright file="CaseDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class CaseDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeActorParameter()
        {
            Assert.That(() => ((ICaseDefinition)null).ComputeActorParameter(), Throws.TypeOf<ArgumentNullException>());

            var caseDefinition = new CaseDefinition();

            Assert.That(caseDefinition.ComputeActorParameter(), Is.Empty);

            // Discrimination: add a ParameterMembership (not ActorMembership) — must be excluded from result.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            caseDefinition.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(caseDefinition.ComputeActorParameter(), Is.Empty);

            // Populated: an ActorMembership carrying a PartUsage → that PartUsage appears in the result.
            var actorMembership = new ActorMembership();
            var actorPartUsage = new PartUsage();
            caseDefinition.AssignOwnership(actorMembership, actorPartUsage);

            Assert.That(caseDefinition.ComputeActorParameter(), Is.EqualTo([actorPartUsage]));
        }

        [Test]
        public void VerifyComputeObjectiveRequirement()
        {
            Assert.That(() => ((ICaseDefinition)null).ComputeObjectiveRequirement(), Throws.TypeOf<ArgumentNullException>());

            var caseDefinition = new CaseDefinition();

            // Empty case: no ObjectiveMembership in featureMembership → null.
            Assert.That(caseDefinition.ComputeObjectiveRequirement(), Is.Null);

            // Discrimination: add a ParameterMembership (not ObjectiveMembership) — still null.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            caseDefinition.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(caseDefinition.ComputeObjectiveRequirement(), Is.Null);

            // Populated case: ObjectiveMembership owns a RequirementUsage → returns the RequirementUsage.
            var objectiveMembership = new ObjectiveMembership();
            var requirementUsage = new RequirementUsage();
            caseDefinition.AssignOwnership(objectiveMembership, requirementUsage);

            Assert.That(caseDefinition.ComputeObjectiveRequirement(), Is.SameAs(requirementUsage));
        }

        [Test]
        public void VerifyComputeSubjectParameter()
        {
            Assert.That(() => ((ICaseDefinition)null).ComputeSubjectParameter(), Throws.TypeOf<ArgumentNullException>());

            var caseDefinition = new CaseDefinition();

            // Empty case: no SubjectMembership in featureMembership → null.
            Assert.That(caseDefinition.ComputeSubjectParameter(), Is.Null);

            // Discrimination: add a ParameterMembership (not SubjectMembership) → still null.
            var parameterMembership = new ParameterMembership();
            var parameterUsage = new Usage();
            caseDefinition.AssignOwnership(parameterMembership, parameterUsage);

            Assert.That(caseDefinition.ComputeSubjectParameter(), Is.Null);

            // Populated case: SubjectMembership is present alongside the earlier ParameterMembership.
            // OfType<ISubjectMembership> must discriminate — only the subject's ownedSubjectParameter surfaces.
            var subjectMembership = new SubjectMembership();
            var subjectUsage = new Usage();
            caseDefinition.AssignOwnership(subjectMembership, subjectUsage);

            Assert.That(caseDefinition.ComputeSubjectParameter(), Is.SameAs(subjectUsage));
        }
    }
}
