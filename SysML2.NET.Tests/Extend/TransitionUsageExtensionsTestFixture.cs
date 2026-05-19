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

            // Effect-kind TFM wired with an ActionUsage as transitionFeature.
            // For Later: populated path depends on TransitionFeatureMembershipExtensions.ComputeTransitionFeature
            // at SysML2.NET/Extend/TransitionFeatureMembershipExtensions.cs:51, which is still a stub.
            var effectTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Effect };
            var effectAction = new ActionUsage();
            transitionUsage.AssignOwnership(effectTfm, effectAction);

            Assert.That(() => transitionUsage.ComputeEffectAction(), Throws.TypeOf<NotSupportedException>());
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

            // Guard-kind TFM wired with a LiteralBoolean (IExpression) as transitionFeature.
            // For Later: populated path depends on TransitionFeatureMembershipExtensions.ComputeTransitionFeature
            // at SysML2.NET/Extend/TransitionFeatureMembershipExtensions.cs:51, which is still a stub.
            var guardTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Guard };
            var guardExpression = new LiteralBoolean();
            transitionUsage.AssignOwnership(guardTfm, guardExpression);

            Assert.That(() => transitionUsage.ComputeGuardExpression(), Throws.TypeOf<NotSupportedException>());
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

            // Succession present but targetFeature access hits ConnectorExtensions.ComputeTargetFeature stub.
            // For Later: populated path depends on ConnectorExtensions.ComputeTargetFeature
            // at SysML2.NET/Extend/ConnectorExtensions.cs:171, which is still a stub.
            var succession = new Succession();
            transitionUsage.AssignOwnership(new OwningMembership(), succession);

            Assert.That(() => transitionUsage.ComputeTarget(), Throws.TypeOf<NotSupportedException>());
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

            // Trigger-kind TFM wired with an AcceptActionUsage as transitionFeature.
            // For Later: populated path depends on TransitionFeatureMembershipExtensions.ComputeTransitionFeature
            // at SysML2.NET/Extend/TransitionFeatureMembershipExtensions.cs:51, which is still a stub.
            var triggerTfm = new TransitionFeatureMembership { Kind = TransitionFeatureKind.Trigger };
            var acceptAction = new AcceptActionUsage();
            transitionUsage.AssignOwnership(triggerTfm, acceptAction);

            Assert.That(() => transitionUsage.ComputeTriggerAction(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeTriggerPayloadParameterOperation()
        {
            Assert.That(() => ((ITransitionUsage)null).ComputeTriggerPayloadParameterOperation(), Throws.TypeOf<ArgumentNullException>());

            var transitionUsage = new TransitionUsage();

            // No Trigger TFMs → triggerAction is empty → null.
            Assert.That(transitionUsage.ComputeTriggerPayloadParameterOperation(), Is.Null);

            // Trigger TFM wired but access to transitionFeature hits ComputeTransitionFeature stub.
            // For Later: populated path depends on TransitionFeatureMembershipExtensions.ComputeTransitionFeature
            // at SysML2.NET/Extend/TransitionFeatureMembershipExtensions.cs:51, which is still a stub.
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
