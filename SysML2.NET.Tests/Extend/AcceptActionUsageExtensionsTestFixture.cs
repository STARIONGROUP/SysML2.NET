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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AcceptActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePayloadArgument()
        {
            Assert.That(() => ((IAcceptActionUsage)null).ComputePayloadArgument(), Throws.TypeOf<ArgumentNullException>());

            // Branch 1 — bare AcceptActionUsage with no input parameters: argument(1) is null.
            var emptyAccept = new AcceptActionUsage();

            Assert.That(emptyAccept.ComputePayloadArgument(), Is.Null);

            // Branch 2 — one input parameter, no FeatureValue membership: argument(1) is null.
            var bareParameterAccept = new AcceptActionUsage();
            var bareParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            bareParameterAccept.AssignOwnership(new FeatureMembership(), bareParameter);

            Assert.That(bareParameterAccept.ComputePayloadArgument(), Is.Null);

            // Branch 3 — one input parameter owning a FeatureValue whose value is a LiteralInteger: argument(1) returns that expression.
            var populatedAccept = new AcceptActionUsage();
            var populatedParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            populatedAccept.AssignOwnership(new FeatureMembership(), populatedParameter);

            var payloadExpression = new LiteralInteger();
            populatedParameter.AssignOwnership(new FeatureValue(), payloadExpression);

            Assert.That(populatedAccept.ComputePayloadArgument(), Is.SameAs(payloadExpression));

            // Branch 4 — two input parameters, only the second has a FeatureValue: argument(1) must still be null.
            var twoParameterAccept = new AcceptActionUsage();
            var firstParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            twoParameterAccept.AssignOwnership(new FeatureMembership(), firstParameter);

            var secondParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            twoParameterAccept.AssignOwnership(new FeatureMembership(), secondParameter);

            var secondExpression = new LiteralInteger();
            secondParameter.AssignOwnership(new FeatureValue(), secondExpression);

            Assert.That(twoParameterAccept.ComputePayloadArgument(), Is.Null);
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

            // Branch 1 — bare AcceptActionUsage with no input parameters: argument(2) is null.
            var emptyAccept = new AcceptActionUsage();

            Assert.That(emptyAccept.ComputeReceiverArgument(), Is.Null);

            // Branch 2 — one input parameter only: argument(2) is out-of-range and returns null.
            var singleParameterAccept = new AcceptActionUsage();
            var loneParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            singleParameterAccept.AssignOwnership(new FeatureMembership(), loneParameter);

            Assert.That(singleParameterAccept.ComputeReceiverArgument(), Is.Null);

            // Branch 3 — two input parameters, neither has a FeatureValue: argument(2) is null.
            var bareTwoParameterAccept = new AcceptActionUsage();
            var bareFirstParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            bareTwoParameterAccept.AssignOwnership(new FeatureMembership(), bareFirstParameter);

            var bareSecondParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            bareTwoParameterAccept.AssignOwnership(new FeatureMembership(), bareSecondParameter);

            Assert.That(bareTwoParameterAccept.ComputeReceiverArgument(), Is.Null);

            // Branch 4 — two input parameters, only the SECOND has a FeatureValue with a non-null IExpression: argument(2) returns that expression.
            var populatedAccept = new AcceptActionUsage();
            var populatedFirstParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            populatedAccept.AssignOwnership(new FeatureMembership(), populatedFirstParameter);

            var populatedSecondParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            populatedAccept.AssignOwnership(new FeatureMembership(), populatedSecondParameter);

            var receiverExpression = new LiteralInteger();
            populatedSecondParameter.AssignOwnership(new FeatureValue(), receiverExpression);

            Assert.That(populatedAccept.ComputeReceiverArgument(), Is.SameAs(receiverExpression));

            // Branch 5 — two input parameters, only the FIRST has a FeatureValue: argument(2) must still be null.
            var firstOnlyAccept = new AcceptActionUsage();
            var firstOnlyFirstParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            firstOnlyAccept.AssignOwnership(new FeatureMembership(), firstOnlyFirstParameter);

            var firstOnlySecondParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            firstOnlyAccept.AssignOwnership(new FeatureMembership(), firstOnlySecondParameter);

            var firstExpression = new LiteralInteger();
            firstOnlyFirstParameter.AssignOwnership(new FeatureValue(), firstExpression);

            Assert.That(firstOnlyAccept.ComputeReceiverArgument(), Is.Null);
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
