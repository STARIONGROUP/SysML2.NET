// -------------------------------------------------------------------------------------------------
// <copyright file="AcceptActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AcceptActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePayloadArgument()
        {
            Assert.That(() => ((IAcceptActionUsage)null).ComputePayloadArgument(), Throws.TypeOf<ArgumentNullException>());

            var accept = new AcceptActionUsage();

            // For Later: populated case depends on ActionUsageExtensions.ComputeArgumentOperation at SysML2.NET/Extend/ActionUsageExtensions.cs:157, which is still a stub.
            Assert.That(() => accept.ComputePayloadArgument(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputePayloadParameter()
        {
            Assert.That(() => ((IAcceptActionUsage)null).ComputePayloadParameter(), Throws.TypeOf<ArgumentNullException>());

            var accept = new AcceptActionUsage();

            // For Later: populated case depends on StepExtensions.ComputeParameter at SysML2.NET/Extend/StepExtensions.cs:71, which is still a stub.
            Assert.That(() => accept.ComputePayloadParameter(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeReceiverArgument()
        {
            Assert.That(() => ((IAcceptActionUsage)null).ComputeReceiverArgument(), Throws.TypeOf<ArgumentNullException>());

            var accept = new AcceptActionUsage();

            // For Later: populated case depends on ActionUsageExtensions.ComputeArgumentOperation at SysML2.NET/Extend/ActionUsageExtensions.cs:157, which is still a stub.
            Assert.That(() => accept.ComputeReceiverArgument(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeIsTriggerActionOperation()
        {
            Assert.That(() => ((IAcceptActionUsage)null).ComputeIsTriggerActionOperation(), Throws.TypeOf<ArgumentNullException>());

            // Branch A — null owningType: no OwningRelationship set, so owningType is null → false.
            var acceptNoOwner = new AcceptActionUsage();

            Assert.That(acceptNoOwner.ComputeIsTriggerActionOperation(), Is.False);

            // Branch B — non-null owningType, not ITransitionUsage: owner is ActionDefinition → false.
            var actionDefinitionOwner = new ActionDefinition();
            var featureMembershipB = new FeatureMembership();
            actionDefinitionOwner.AssignOwnership(featureMembershipB, new AcceptActionUsage());
            var acceptNotTransition = new AcceptActionUsage();
            actionDefinitionOwner.AssignOwnership(new FeatureMembership(), acceptNotTransition);

            Assert.That(acceptNotTransition.ComputeIsTriggerActionOperation(), Is.False);

            // Branch C — owningType IS ITransitionUsage: triggerAction dispatch hits the ComputeTriggerAction stub.
            var transitionOwner = new TransitionUsage();
            var featureMembershipC = new FeatureMembership();
            var acceptInTransition = new AcceptActionUsage();
            transitionOwner.AssignOwnership(featureMembershipC, acceptInTransition);

            // For Later: populated case depends on TransitionUsageExtensions.ComputeTriggerAction at SysML2.NET/Extend/TransitionUsageExtensions.cs:208, which is still a stub.
            Assert.That(() => acceptInTransition.ComputeIsTriggerActionOperation(), Throws.TypeOf<NotSupportedException>());
        }
    }
}
