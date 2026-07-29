// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class LiteralExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            Assert.That(
                () => ((ILiteralExpression)null).ComputeRedefinedModelLevelEvaluableOperation([]),
                Throws.TypeOf<ArgumentNullException>());

            var subject = new LiteralExpression();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeRedefinedModelLevelEvaluableOperation([]), Is.True);

                // visited is unused by the OCL body (`true`); a null visited must still return true.
                Assert.That(subject.ComputeRedefinedModelLevelEvaluableOperation(null), Is.True);
            }
        }

        [Test]
        public void VerifyComputeRedefinedEvaluateOperation()
        {
            Assert.That(
                () => ((ILiteralExpression)null).ComputeRedefinedEvaluateOperation(null),
                Throws.TypeOf<ArgumentNullException>());

            var subject = new LiteralExpression();

            // target is unused by the OCL body (`Sequence{self}`); pass null.
            var result = subject.ComputeRedefinedEvaluateOperation(null);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0], Is.SameAs(subject));
            }
        }
    }
}
