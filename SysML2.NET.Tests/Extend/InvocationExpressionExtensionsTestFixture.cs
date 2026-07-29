// -------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Expressions;

    [TestFixture]
    public class InvocationExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            Assert.That(
                () => ((IInvocationExpression)null).ComputeRedefinedModelLevelEvaluableOperation([]),
                Throws.TypeOf<ArgumentNullException>());

            var subject = new InvocationExpression();

            // For later: populated case depends on ComputeArgument / Function.ComputeIsModelLevelEvaluable, still a stub (out of batch).
            Assert.That(
                () => subject.ComputeRedefinedModelLevelEvaluableOperation([]),
                Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeRedefinedEvaluateOperation()
        {
            var subject = new InvocationExpression();

            // For later: deferred — needs Function-application engine (no OCL).
            Assert.That(
                () => subject.ComputeRedefinedEvaluateOperation(null),
                Throws.TypeOf<NotSupportedException>());
        }
    }
}
