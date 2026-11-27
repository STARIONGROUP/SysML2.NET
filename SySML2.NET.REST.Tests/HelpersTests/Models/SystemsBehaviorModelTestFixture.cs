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
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Core.DTO.Kernel.FeatureValues;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Systems.States;
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

            var level = dtos.OfType<Feature>().Single(feature => feature.GetType() == typeof(Feature) && feature.DeclaredName == "level");
            var limit = dtos.OfType<Feature>().Single(feature => feature.GetType() == typeof(Feature) && feature.DeclaredName == "limit");
            var initialValue = dtos.OfType<FeatureValue>().Single(value => value.OwningRelatedElement == limit.Id);
            var parallel = dtos.OfType<StateDefinition>().Single(state => state.DeclaredName == "Par");
            var parallelUsage = dtos.OfType<StateUsage>().Single(state => state.DeclaredName == "pu");
            var ownedByParallel = dtos.OfType<IRelationship>().Where(relationship => relationship.OwningRelatedElement == parallel.Id).SelectMany(relationship => relationship.OwnedRelatedElement).Select(id => dtos.Single(dto => dto.Id == id)).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(level.IsVariable, Is.True);
                Assert.That(level.IsConstant, Is.False, "the referent of an assignment changes over time");
                Assert.That(dtos.OfType<FeatureValue>().Where(value => value.OwningRelatedElement == level.Id), Is.Empty, "the referent of an assignment has no initial value");
                Assert.That(limit.IsVariable, Is.True);
                Assert.That(limit.IsConstant, Is.True, "a constant feature must be variable");
                Assert.That(initialValue.IsInitial, Is.True);
                Assert.That(dtos.Single(dto => dto.Id == initialValue.OwnedRelatedElement[0]), Is.TypeOf<LiteralInteger>().With.Property(nameof(LiteralInteger.Value)).EqualTo(0));
                Assert.That(parallel.IsParallel, Is.True);
                Assert.That(ownedByParallel.Select(element => element.DeclaredName), Is.EquivalentTo((string[])["pa1", "pa2"]));
                Assert.That(ownedByParallel, Has.All.TypeOf<StateUsage>(), "the substates of a parallel state have no transitions");
                Assert.That(parallelUsage.IsParallel, Is.True);
                Assert.That(dtos.OfType<FeatureTyping>().Where(typing => typing.OwningRelatedElement == parallelUsage.Id).Select(typing => typing.Type), Is.EqualTo((Guid[])[parallel.Id]));
                Assert.That(dtos.OfType<StateDefinition>().Single(state => state.DeclaredName == "St").IsParallel, Is.False, "the state definition with a transition is not parallel");
            }
        }
    }
}
