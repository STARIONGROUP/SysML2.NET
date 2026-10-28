// ------------------------------------------------------------------------------------------------
// <copyright file="LiteralExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
