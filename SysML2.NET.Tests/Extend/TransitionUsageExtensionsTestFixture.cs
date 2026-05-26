// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Extensions;

    using PocoFeature = SysML2.NET.Core.POCO.Core.Features.Feature;

    [TestFixture]
    public class TransitionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeEffectAction()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeEffectAction(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // Empty ownedFeatureMembership → empty list.
            Assert.That(transitionUsage.ComputeEffectAction(), Has.Count.EqualTo(0));

            // Only a Trigger-kind TFM present → Effect filter excludes it → still empty.
            var triggerTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Trigger };
            var triggerAction = new AcceptActionUsage();
            transitionUsage.AssignOwnership(triggerTfm, triggerAction);

            Assert.That(transitionUsage.ComputeEffectAction(), Has.Count.EqualTo(0));

            // Effect-kind TFM wired with an ActionUsage as transitionFeature → positive case.
            // The Trigger TFM already wired above proves Kind-filter discrimination (Trigger excluded by Effect filter).
            var effectTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Effect };
            var effectAction = new ActionUsage();
            transitionUsage.AssignOwnership(effectTfm, effectAction);

            Assert.That(transitionUsage.ComputeEffectAction(), Is.EqualTo([effectAction]));

            // Kind-filter discrimination: add a Guard TFM whose transitionFeature is an IActionUsage → excluded
            // because its Kind is Guard, not Effect.
            var guardTfmForEffectTest = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Guard };
            var guardActionUsageStep = new ActionUsage();
            transitionUsage.AssignOwnership(guardTfmForEffectTest, guardActionUsageStep);

            Assert.That(transitionUsage.ComputeEffectAction(), Is.EqualTo([effectAction]));

            // Type-discrimination: a second Effect-kind TFM whose transitionFeature is NOT an IActionUsage
            // (a LiteralBoolean / IExpression) → excluded by the trailing OfType<IActionUsage>().
            var effectTfmWrongType = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Effect };
            var expressionStep = new LiteralBoolean();
            transitionUsage.AssignOwnership(effectTfmWrongType, expressionStep);

            Assert.That(transitionUsage.ComputeEffectAction(), Is.EqualTo([effectAction]));
        }

        [Test]
        public void VerifyComputeGuardExpression()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeGuardExpression(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // Empty ownedFeatureMembership → empty list.
            Assert.That(transitionUsage.ComputeGuardExpression(), Has.Count.EqualTo(0));

            // Only Trigger-kind TFMs → Guard filter excludes them → empty.
            var triggerTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Trigger };
            var triggerAction = new AcceptActionUsage();
            transitionUsage.AssignOwnership(triggerTfm, triggerAction);

            Assert.That(transitionUsage.ComputeGuardExpression(), Has.Count.EqualTo(0));

            // Guard-kind TFM wired with a LiteralBoolean (IExpression) as transitionFeature → positive case.
            // The Trigger TFM already wired above proves Kind-filter discrimination (Trigger excluded by Guard filter).
            var guardTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Guard };
            var guardExpression = new LiteralBoolean();
            transitionUsage.AssignOwnership(guardTfm, guardExpression);

            Assert.That(transitionUsage.ComputeGuardExpression(), Is.EqualTo([guardExpression]));

            // Kind-filter discrimination: add an Effect TFM whose transitionFeature is an IExpression → excluded
            // because its Kind is Effect, not Guard.
            var effectTfmForGuardTest = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Effect };
            var effectExpressionStep = new LiteralBoolean();
            transitionUsage.AssignOwnership(effectTfmForGuardTest, effectExpressionStep);

            Assert.That(transitionUsage.ComputeGuardExpression(), Is.EqualTo([guardExpression]));

            // Type-discrimination: a second Guard-kind TFM whose transitionFeature is NOT an IExpression
            // (an ActionUsage) → excluded by the trailing OfType<IExpression>().
            var guardTfmWrongType = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Guard };
            var actionUsageStep = new ActionUsage();
            transitionUsage.AssignOwnership(guardTfmWrongType, actionUsageStep);

            Assert.That(transitionUsage.ComputeGuardExpression(), Is.EqualTo([guardExpression]));
        }

        [Test]
        public void VerifyComputeSource()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeSource(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // No qualifying non-FeatureMembership Feature with ActionUsage featureTarget → SourceFeature() returns null → null.
            Assert.That(transitionUsage.ComputeSource(), Is.Null);

            // Non-FeatureMembership (OwningMembership) owning an ActionUsage: featureTarget = the ActionUsage itself
            // (no chainingFeature) which IS IActionUsage → returned.
            var sourceAction = new ActionUsage();
            transitionUsage.AssignOwnership(new OwningMembership(), sourceAction);

            Assert.That(transitionUsage.ComputeSource(), Is.SameAs(sourceAction));
        }

        [Test]
        public void VerifyComputeSuccession()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeSuccession(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // Empty ownedMember → null.
            Assert.That(transitionUsage.ComputeSuccession(), Is.Null);

            // Non-Succession member only → still null.
            var nonSuccessionMember = new ActionUsage();
            transitionUsage.AssignOwnership(new OwningMembership(), nonSuccessionMember);

            Assert.That(transitionUsage.ComputeSuccession(), Is.Null);

            // First Succession added → returned.
            var firstSuccession = new Succession();
            transitionUsage.AssignOwnership(new OwningMembership(), firstSuccession);

            Assert.That(transitionUsage.ComputeSuccession(), Is.SameAs(firstSuccession));

            // Second Succession added → first is still returned (OCL ->at(1)).
            var secondSuccession = new Succession();
            transitionUsage.AssignOwnership(new OwningMembership(), secondSuccession);

            Assert.That(transitionUsage.ComputeSuccession(), Is.SameAs(firstSuccession));
        }

        [Test]
        public void VerifyComputeTarget()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeTarget(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // No Succession in ownedMember → succession is null → null.
            Assert.That(transitionUsage.ComputeTarget(), Is.Null);

            // Helper: wire a connector end onto the given succession.
            // Creates a Feature(IsEnd=true) with a ReferenceSubsetting pointing at relatedTarget,
            // wrapped in a FeatureMembership owned by the succession.
            static void AddSuccessionEnd(Succession owningSuccession, IFeature relatedTarget)
            {
                var endFeature = new PocoFeature { IsEnd = true };
                var referenceSubsetting = new ReferenceSubsetting { ReferencedFeature = relatedTarget };
                endFeature.AssignOwnership(referenceSubsetting);

                var featureMembership = new FeatureMembership();
                owningSuccession.AssignOwnership(featureMembership, endFeature);
            }

            // Succession present, one connector end → relatedFeature has 1 element → targetFeature is empty
            // (GetRange(1, 0)) → firstTargetFeature is null → null.
            var successionOneEnd = new Succession();
            transitionUsage.AssignOwnership(new OwningMembership(), successionOneEnd);
            AddSuccessionEnd(successionOneEnd, new PocoFeature());

            Assert.That(transitionUsage.ComputeTarget(), Is.Null);

            // Succession present, two connector ends, second end's relatedTarget is a plain Feature.
            // featureTarget of Feature (no chaining) = Feature itself → not IActionUsage → null.
            var transitionUsage2 = new TransitionUsage();
            var successionNonAction = new Succession();
            transitionUsage2.AssignOwnership(new OwningMembership(), successionNonAction);

            var nonActionTarget = new PocoFeature();
            AddSuccessionEnd(successionNonAction, new PocoFeature());
            AddSuccessionEnd(successionNonAction, nonActionTarget);

            Assert.That(transitionUsage2.ComputeTarget(), Is.Null);

            // firstTargetFeature.featureTarget == null → null.
            // Wire the second end's relatedTarget with a FeatureChaining whose ChainingFeature = null,
            // which makes featureTarget derive to null.
            var transitionUsage3 = new TransitionUsage();
            var successionNullFeatureTarget = new Succession();
            transitionUsage3.AssignOwnership(new OwningMembership(), successionNullFeatureTarget);

            var relatedTargetWithNullChain = new PocoFeature();
            relatedTargetWithNullChain.AssignOwnership(new FeatureChaining { ChainingFeature = null });

            AddSuccessionEnd(successionNullFeatureTarget, new PocoFeature());
            AddSuccessionEnd(successionNullFeatureTarget, relatedTargetWithNullChain);

            // relatedTargetWithNullChain.featureTarget = null (last ChainingFeature is null) → null.
            Assert.That(transitionUsage3.ComputeTarget(), Is.Null);

            // POSITIVE case: two connector ends, the second end's relatedTarget is an ActionUsage.
            // ActionUsage has no chaining → featureTarget = itself = IActionUsage → returned.
            var transitionUsage4 = new TransitionUsage();
            var successionPositive = new Succession();
            transitionUsage4.AssignOwnership(new OwningMembership(), successionPositive);

            var actionUsageTarget = new ActionUsage();
            AddSuccessionEnd(successionPositive, new PocoFeature());
            AddSuccessionEnd(successionPositive, actionUsageTarget);

            Assert.That(transitionUsage4.ComputeTarget(), Is.SameAs(actionUsageTarget));

            // Three connector ends: source, targetEnd2 (ActionUsage), targetEnd3 (plain Feature).
            // targetFeature = [targetEnd2, targetEnd3]; firstTargetFeature = targetEnd2 (ActionUsage)
            // → featureTarget = itself → returned.
            var transitionUsage5 = new TransitionUsage();
            var successionThreeEnds = new Succession();
            transitionUsage5.AssignOwnership(new OwningMembership(), successionThreeEnds);

            var actionUsageTarget5 = new ActionUsage();
            AddSuccessionEnd(successionThreeEnds, new PocoFeature());
            AddSuccessionEnd(successionThreeEnds, actionUsageTarget5);
            AddSuccessionEnd(successionThreeEnds, new PocoFeature());

            Assert.That(transitionUsage5.ComputeTarget(), Is.SameAs(actionUsageTarget5));
        }

        [Test]
        public void VerifyComputeTriggerAction()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeTriggerAction(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // Empty ownedFeatureMembership → empty list.
            Assert.That(transitionUsage.ComputeTriggerAction(), Has.Count.EqualTo(0));

            // Only Effect-kind and Guard-kind TFMs → Trigger filter excludes them → empty.
            var effectTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Effect };
            var effectAction = new ActionUsage();
            transitionUsage.AssignOwnership(effectTfm, effectAction);

            var guardTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Guard };
            var guardExpression = new LiteralBoolean();
            transitionUsage.AssignOwnership(guardTfm, guardExpression);

            Assert.That(transitionUsage.ComputeTriggerAction(), Has.Count.EqualTo(0));

            // Trigger-kind TFM wired with an AcceptActionUsage as transitionFeature → positive case.
            // The Effect and Guard TFMs already wired above prove Kind-filter discrimination.
            var triggerTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Trigger };
            var acceptAction = new AcceptActionUsage();
            transitionUsage.AssignOwnership(triggerTfm, acceptAction);

            Assert.That(transitionUsage.ComputeTriggerAction(), Is.EqualTo([acceptAction]));

            // Kind-filter discrimination: add a Guard TFM whose transitionFeature is an AcceptActionUsage → excluded
            // because its Kind is Guard, not Trigger.
            var guardTfmForTriggerTest = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Guard };
            var guardAcceptAction = new AcceptActionUsage();
            transitionUsage.AssignOwnership(guardTfmForTriggerTest, guardAcceptAction);

            Assert.That(transitionUsage.ComputeTriggerAction(), Is.EqualTo([acceptAction]));

            // Type-discrimination: a second Trigger-kind TFM whose transitionFeature is NOT an IAcceptActionUsage
            // (a plain ActionUsage) → excluded by the trailing OfType<IAcceptActionUsage>().
            var triggerTfmWrongType = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Trigger };
            var actionUsageStep = new ActionUsage();
            transitionUsage.AssignOwnership(triggerTfmWrongType, actionUsageStep);

            Assert.That(transitionUsage.ComputeTriggerAction(), Is.EqualTo([acceptAction]));
        }

        [Test]
        public void VerifyComputeTriggerPayloadParameterOperation()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeTriggerPayloadParameterOperation(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // No Trigger TFMs → triggerAction is empty → null.
            Assert.That(transitionUsage.ComputeTriggerPayloadParameterOperation(), Is.Null);

            // Trigger TFM wired; triggerAction now resolves (ComputeTransitionFeature is implemented).
            // The NotSupportedException is now thrown by AcceptActionUsage.payloadParameter → StepExtensions.ComputeParameter,
            // which is still a stub. Expand this test when StepExtensions.ComputeParameter is implemented.
            var triggerTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Trigger };
            var acceptAction = new AcceptActionUsage();
            transitionUsage.AssignOwnership(triggerTfm, acceptAction);

            Assert.That(() => transitionUsage.ComputeTriggerPayloadParameterOperation(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeSourceFeatureOperation()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeSourceFeatureOperation(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // Empty ownedMembership → null.
            Assert.That(transitionUsage.ComputeSourceFeatureOperation(), Is.Null);

            // All entries are IFeatureMembership → rejected by the non-FeatureMembership filter → null.
            var featureMembership = new FeatureMembership();
            var featureViaFm = new ActionUsage();
            transitionUsage.AssignOwnership(featureMembership, featureViaFm);

            Assert.That(transitionUsage.ComputeSourceFeatureOperation(), Is.Null);

            // Non-FeatureMembership (OwningMembership) owning a plain Feature (featureTarget = itself = not IActionUsage) → filtered out → null.
            var plainFeature = new Feature();
            transitionUsage.AssignOwnership(new OwningMembership(), plainFeature);

            Assert.That(transitionUsage.ComputeSourceFeatureOperation(), Is.Null);

            // Non-FeatureMembership owning an ActionUsage (featureTarget = itself = IActionUsage) → returned.
            var sourceAction = new ActionUsage();
            transitionUsage.AssignOwnership(new OwningMembership(), sourceAction);

            Assert.That(transitionUsage.ComputeSourceFeatureOperation(), Is.SameAs(sourceAction));
        }
    }
}
