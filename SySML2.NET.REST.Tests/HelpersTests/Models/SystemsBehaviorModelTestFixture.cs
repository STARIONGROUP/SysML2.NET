// ------------------------------------------------------------------------------------------------
// <copyright file="SystemsBehaviorModelTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests.Models
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Serializer.Json;

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    [TestFixture]
    public class SystemsBehaviorModelTestFixture
    {
        private static readonly string[] BehaviorMetaclasses =
        [
            "AcceptActionUsage", "ActionDefinition", "ActionUsage", "AssignmentActionUsage", "DecisionNode", "ForLoopActionUsage", "ForkNode",
            "IfActionUsage", "JoinNode", "MergeNode", "PerformActionUsage", "SendActionUsage", "TerminateActionUsage", "TriggerInvocationExpression",
            "WhileLoopActionUsage", "StateDefinition", "StateUsage", "ExhibitStateUsage", "StateSubactionMembership", "TransitionUsage",
            "TransitionFeatureMembership", "CalculationDefinition", "CalculationUsage", "ConstraintDefinition", "ConstraintUsage",
            "AssertConstraintUsage", "RequirementDefinition", "RequirementUsage", "SatisfyRequirementUsage", "ConcernDefinition", "ConcernUsage",
            "ActorMembership", "StakeholderMembership", "SubjectMembership", "FramedConcernMembership", "RequirementConstraintMembership",
            "CaseDefinition", "CaseUsage", "ObjectiveMembership", "AnalysisCaseDefinition", "AnalysisCaseUsage", "VerificationCaseDefinition",
            "VerificationCaseUsage", "RequirementVerificationMembership", "UseCaseDefinition", "UseCaseUsage", "IncludeUseCaseUsage",
            "ViewDefinition", "ViewUsage", "ViewpointDefinition", "ViewpointUsage", "RenderingDefinition", "RenderingUsage",
            "ViewRenderingMembership", "MembershipExpose", "NamespaceExpose", "MetadataDefinition", "MetadataUsage"
        ];

        [Test]
        public void VerifySystemsBehaviorModel()
        {
            var root = new RootModel();
            var structure = new SystemsStructureModel(root);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new SystemsBehaviorModel(null, structure), Throws.ArgumentNullException);
                Assert.That(() => new SystemsBehaviorModel(root, null), Throws.ArgumentNullException);
            }

            root = new RootModel();
            structure = new SystemsStructureModel(root);

            var atStep6 = root.Builder.ToDtos();
            var model = new SystemsBehaviorModel(root, structure);

            var dtos = model.Builder.ToDtos();
            var changed = ModelBuilder.Changed(atStep6, dtos);

            using var stream = new MemoryStream();
            new Serializer().SerializeRequest(ModelBuilder.ToCommitRequest(changed, "systems behaviour"), SerializationModeKind.JSON, false, stream, new JsonWriterOptions());
            var json = Encoding.UTF8.GetString(stream.ToArray());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos.Select(dto => dto.Id), Is.Unique);
                Assert.That(dtos.Select(dto => dto.GetType().Name).Distinct(), Is.SupersetOf(BehaviorMetaclasses));
                Assert.That(changed.Select(dto => dto.Id), Does.Contain(root.Root.Id).And.Contain(structure.OccurrenceDefinition.Id), "the root package and the occurrence definition of step 6 change");
                Assert.That(json, Does.Not.Contain("00000000-0000-0000-0000-000000000000"));
                Assert.That(model.Builder.WithoutDerivedValues, Is.EquivalentTo((string[])["systems behaviour/accept when/trigger", "systems behaviour/accept at/trigger", "systems behaviour/accept after/trigger"]), "only the trigger invocations, whose instantiated type needs the model library");
            }
        }
    }
}
