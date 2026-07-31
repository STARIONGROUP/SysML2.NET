// -------------------------------------------------------------------------------------------------
// <copyright file="SendActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class SendActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePayloadArgument()
        {
            Assert.That(() => ((ISendActionUsage)null).ComputePayloadArgument(), Throws.TypeOf<ArgumentNullException>());

            Assert.That(new SendActionUsage().ComputePayloadArgument(), Is.Null);

            var payloadExpression = new LiteralInteger();
            var senderExpression = new LiteralInteger();
            var receiverExpression = new LiteralInteger();
            var sendActionUsage = CreateSendActionUsageWithArguments(payloadExpression, senderExpression, receiverExpression);

            using (Assert.EnterMultipleScope())
            {
                // payloadArgument = argument(1)
                Assert.That(sendActionUsage.ComputePayloadArgument(), Is.SameAs(payloadExpression));
                Assert.That(sendActionUsage.ComputePayloadArgument(), Is.Not.SameAs(senderExpression));
            }
        }

        [Test]
        public void VerifyComputeReceiverArgument()
        {
            Assert.That(() => ((ISendActionUsage)null).ComputeReceiverArgument(), Throws.TypeOf<ArgumentNullException>());

            Assert.That(new SendActionUsage().ComputeReceiverArgument(), Is.Null);

            var payloadExpression = new LiteralInteger();
            var senderExpression = new LiteralInteger();
            var receiverExpression = new LiteralInteger();
            var sendActionUsage = CreateSendActionUsageWithArguments(payloadExpression, senderExpression, receiverExpression);

            // Only two input parameters present -> argument(3) is out of range -> null.
            var twoArgumentSendActionUsage = CreateSendActionUsageWithArguments(new LiteralInteger(), new LiteralInteger());

            using (Assert.EnterMultipleScope())
            {
                // receiverArgument = argument(3)
                Assert.That(sendActionUsage.ComputeReceiverArgument(), Is.SameAs(receiverExpression));
                Assert.That(sendActionUsage.ComputeReceiverArgument(), Is.Not.SameAs(senderExpression));
                Assert.That(twoArgumentSendActionUsage.ComputeReceiverArgument(), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeSenderArgument()
        {
            Assert.That(() => ((ISendActionUsage)null).ComputeSenderArgument(), Throws.TypeOf<ArgumentNullException>());

            Assert.That(new SendActionUsage().ComputeSenderArgument(), Is.Null);

            var payloadExpression = new LiteralInteger();
            var senderExpression = new LiteralInteger();
            var receiverExpression = new LiteralInteger();
            var sendActionUsage = CreateSendActionUsageWithArguments(payloadExpression, senderExpression, receiverExpression);

            // Only a single input parameter present -> argument(2) is out of range -> null.
            var singleArgumentSendActionUsage = CreateSendActionUsageWithArguments(new LiteralInteger());

            using (Assert.EnterMultipleScope())
            {
                // senderArgument = argument(2)
                Assert.That(sendActionUsage.ComputeSenderArgument(), Is.SameAs(senderExpression));
                Assert.That(sendActionUsage.ComputeSenderArgument(), Is.Not.SameAs(payloadExpression));
                Assert.That(singleArgumentSendActionUsage.ComputeSenderArgument(), Is.Null);
            }
        }

        /// <summary>
        /// Builds a <see cref="SendActionUsage" /> whose i-th owned input parameter carries a
        /// <see cref="FeatureValue" /> whose value is the i-th supplied argument expression, so that
        /// <c>Argument(i)</c> (and therefore each Compute* under test) resolves to that expression.
        /// </summary>
        private static SendActionUsage CreateSendActionUsageWithArguments(params LiteralInteger[] argumentExpressions)
        {
            var sendActionUsage = new SendActionUsage();

            foreach (var argumentExpression in argumentExpressions)
            {
                var inputParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
                sendActionUsage.AssignOwnership(new FeatureMembership(), inputParameter);
                inputParameter.AssignOwnership(new FeatureValue(), argumentExpression);
            }

            return sendActionUsage;
        }
    }
}
