// -------------------------------------------------------------------------------------------------
// <copyright file="IfActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class IfActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeElseAction()
        {
            Assert.That(() => ((IIfActionUsage)null).ComputeElseAction(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> inputParameter(3) is null -> null.
            Assert.That(new IfActionUsage().ComputeElseAction(), Is.Null);

            // Only two input parameters -> inputParameter(3) is out of range -> null.
            var twoParameterIfActionUsage = CreateIfActionUsageWithInputParameters(new LiteralInteger(), new ActionUsage());

            // inputParameter(3) present and IS an IActionUsage -> returned.
            var elseAction = new ActionUsage();
            var ifActionUsage = CreateIfActionUsageWithInputParameters(new LiteralInteger(), new ActionUsage(), elseAction);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(twoParameterIfActionUsage.ComputeElseAction(), Is.Null);

                // elseAction = inputParameter(3) as ActionUsage
                Assert.That(ifActionUsage.ComputeElseAction(), Is.SameAs(elseAction));
            }
        }

        [Test]
        public void VerifyComputeIfArgument()
        {
            Assert.That(() => ((IIfActionUsage)null).ComputeIfArgument(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> inputParameter(1) is null -> null.
            Assert.That(new IfActionUsage().ComputeIfArgument(), Is.Null);

            // inputParameter(1) present but NOT an IExpression -> the 'as IExpression' filter yields null.
            var wrongTypeIfActionUsage = CreateIfActionUsageWithInputParameters(new ReferenceUsage());

            // inputParameter(1) present and IS an IExpression -> returned.
            var ifExpression = new LiteralInteger();
            var ifActionUsage = CreateIfActionUsageWithInputParameters(ifExpression, new ActionUsage(), new ActionUsage());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(wrongTypeIfActionUsage.ComputeIfArgument(), Is.Null);

                // ifArgument = inputParameter(1) as Expression
                Assert.That(ifActionUsage.ComputeIfArgument(), Is.SameAs(ifExpression));
            }
        }

        [Test]
        public void VerifyComputeThenAction()
        {
            Assert.That(() => ((IIfActionUsage)null).ComputeThenAction(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> inputParameter(2) is null -> null.
            Assert.That(new IfActionUsage().ComputeThenAction(), Is.Null);

            // inputParameter(2) present but NOT an IActionUsage -> the 'as IActionUsage' filter yields null.
            var wrongTypeIfActionUsage = CreateIfActionUsageWithInputParameters(new LiteralInteger(), new ReferenceUsage());

            // inputParameter(2) present and IS an IActionUsage -> returned.
            var thenAction = new ActionUsage();
            var ifActionUsage = CreateIfActionUsageWithInputParameters(new LiteralInteger(), thenAction, new ActionUsage());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(wrongTypeIfActionUsage.ComputeThenAction(), Is.Null);

                // thenAction = inputParameter(2) as ActionUsage
                Assert.That(ifActionUsage.ComputeThenAction(), Is.SameAs(thenAction));
            }
        }

        /// <summary>
        /// Builds an <see cref="IfActionUsage" /> owning the supplied features as ordered input parameters
        /// (Direction = In), so that <c>InputParameter(i)</c> resolves to the i-th supplied feature.
        /// </summary>
        private static IfActionUsage CreateIfActionUsageWithInputParameters(params IFeature[] inputParameters)
        {
            var ifActionUsage = new IfActionUsage();

            foreach (var inputParameter in inputParameters)
            {
                inputParameter.Direction = FeatureDirectionKind.In;
                ifActionUsage.AssignOwnership(new FeatureMembership(), inputParameter);
            }

            return ifActionUsage;
        }
    }
}
