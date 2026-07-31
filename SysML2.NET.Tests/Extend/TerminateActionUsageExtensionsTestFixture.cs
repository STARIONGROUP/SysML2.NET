// -------------------------------------------------------------------------------------------------
// <copyright file="TerminateActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class TerminateActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTerminatedOccurrenceArgument()
        {
            Assert.That(() => ((ITerminateActionUsage)null).ComputeTerminatedOccurrenceArgument(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> argument(1) is null -> null.
            Assert.That(new TerminateActionUsage().ComputeTerminatedOccurrenceArgument(), Is.Null);

            // A single owned input parameter carrying a FeatureValue -> argument(1) resolves to its value.
            var terminateActionUsage = new TerminateActionUsage();
            var inputParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            terminateActionUsage.AssignOwnership(new FeatureMembership(), inputParameter);

            var terminatedOccurrenceExpression = new LiteralInteger();
            inputParameter.AssignOwnership(new FeatureValue(), terminatedOccurrenceExpression);

            // terminatedOccurrenceArgument = argument(1)
            Assert.That(terminateActionUsage.ComputeTerminatedOccurrenceArgument(), Is.SameAs(terminatedOccurrenceExpression));
        }
    }
}
