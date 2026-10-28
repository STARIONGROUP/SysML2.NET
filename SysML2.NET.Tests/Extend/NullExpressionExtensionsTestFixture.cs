// ------------------------------------------------------------------------------------------------
// <copyright file="NullExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class NullExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            Assert.That(
                () => ((INullExpression)null).ComputeRedefinedModelLevelEvaluableOperation([]),
                Throws.TypeOf<ArgumentNullException>());

            var subject = new NullExpression();

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
                () => ((INullExpression)null).ComputeRedefinedEvaluateOperation(null),
                Throws.TypeOf<ArgumentNullException>());

            var subject = new NullExpression();

            // target is unused by the OCL body (`Sequence{}`); pass null.
            Assert.That(subject.ComputeRedefinedEvaluateOperation(null), Is.Empty);
        }
    }
}
