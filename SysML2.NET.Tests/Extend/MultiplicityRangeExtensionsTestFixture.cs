// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityRangeExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    using Moq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Multiplicities;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class MultiplicityRangeExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeBound()
        {
            Assert.That(() => ((IMultiplicityRange)null).ComputeBound(), Throws.TypeOf<ArgumentNullException>());

            var multiplicityRange = new MultiplicityRange();

            Assert.That(multiplicityRange.ComputeBound(), Has.Count.EqualTo(0));

            var firstExpression = new Expression();
            multiplicityRange.AssignOwnership(new OwningMembership(), firstExpression);

            using (Assert.EnterMultipleScope())
            {
                // Exactly one Expression: bound == [upperBound] (lowerBound is null).
                Assert.That(multiplicityRange.ComputeBound(), Is.EquivalentTo([firstExpression]));
                Assert.That(multiplicityRange.ComputeBound()[0], Is.SameAs(multiplicityRange.upperBound));
            }

            var secondExpression = new Expression();
            multiplicityRange.AssignOwnership(new OwningMembership(), secondExpression);

            using (Assert.EnterMultipleScope())
            {
                // Two Expressions: bound == [lowerBound, upperBound] in order.
                Assert.That(multiplicityRange.ComputeBound(), Is.EqualTo([firstExpression, secondExpression]));
                Assert.That(multiplicityRange.ComputeBound()[0], Is.SameAs(multiplicityRange.lowerBound));
                Assert.That(multiplicityRange.ComputeBound()[1], Is.SameAs(multiplicityRange.upperBound));
            }

            // Mixed: a non-Expression owned member preceding two Expressions; OfType<IExpression>() filters it out
            // so the bound list is still [firstExpression, secondExpression].
            var mixedRange = new MultiplicityRange();
            var nonExpressionElement = new Definition();
            mixedRange.AssignOwnership(new OwningMembership(), nonExpressionElement);

            var mixedLower = new Expression();
            var mixedUpper = new Expression();
            mixedRange.AssignOwnership(new OwningMembership(), mixedLower);
            mixedRange.AssignOwnership(new OwningMembership(), mixedUpper);

            Assert.That(mixedRange.ComputeBound(), Is.EqualTo([mixedLower, mixedUpper]));
        }

        [Test]
        public void VerifyComputeLowerBound()
        {
            Assert.That(() => ((IMultiplicityRange)null).ComputeLowerBound(), Throws.TypeOf<ArgumentNullException>());

            var multiplicityRange = new MultiplicityRange();

            Assert.That(multiplicityRange.ComputeLowerBound(), Is.Null);

            var firstExpression = new Expression();
            multiplicityRange.AssignOwnership(new OwningMembership(), firstExpression);

            // Exactly one Expression -> lowerBound is null (the single expression is the upperBound).
            Assert.That(multiplicityRange.ComputeLowerBound(), Is.Null);

            var secondExpression = new Expression();
            multiplicityRange.AssignOwnership(new OwningMembership(), secondExpression);

            // Two Expressions -> lowerBound is the first one.
            Assert.That(multiplicityRange.ComputeLowerBound(), Is.SameAs(firstExpression));

            // Mixed: a non-Expression ownedMember preceding two Expressions; OfType<IExpression>() filters it out.
            var mixedRange = new MultiplicityRange();
            var nonExpressionElement = new Definition();
            mixedRange.AssignOwnership(new OwningMembership(), nonExpressionElement);

            var mixedLower = new Expression();
            var mixedUpper = new Expression();
            mixedRange.AssignOwnership(new OwningMembership(), mixedLower);
            mixedRange.AssignOwnership(new OwningMembership(), mixedUpper);

            Assert.That(mixedRange.ComputeLowerBound(), Is.SameAs(mixedLower));
        }

        [Test]
        public void VerifyComputeUpperBound()
        {
            Assert.That(() => ((IMultiplicityRange)null).ComputeUpperBound(), Throws.TypeOf<ArgumentNullException>());

            var multiplicityRange = new MultiplicityRange();

            Assert.That(multiplicityRange.ComputeUpperBound(), Is.Null);

            var firstExpression = new Expression();
            multiplicityRange.AssignOwnership(new OwningMembership(), firstExpression);

            // Exactly one Expression -> upperBound IS that expression.
            Assert.That(multiplicityRange.ComputeUpperBound(), Is.SameAs(firstExpression));

            var secondExpression = new Expression();
            multiplicityRange.AssignOwnership(new OwningMembership(), secondExpression);

            using (Assert.EnterMultipleScope())
            {
                // Two Expressions -> upperBound is the second one (OCL at(2), C# index 1).
                Assert.That(multiplicityRange.ComputeUpperBound(), Is.SameAs(secondExpression));
                Assert.That(multiplicityRange.ComputeUpperBound(), Is.Not.SameAs(firstExpression));
            }

            // Mixed: a non-Expression ownedMember preceding two Expressions; the second Expression is the upperBound.
            var mixedRange = new MultiplicityRange();
            var nonExpressionElement = new Definition();
            mixedRange.AssignOwnership(new OwningMembership(), nonExpressionElement);

            var mixedLower = new Expression();
            var mixedUpper = new Expression();
            mixedRange.AssignOwnership(new OwningMembership(), mixedLower);
            mixedRange.AssignOwnership(new OwningMembership(), mixedUpper);

            Assert.That(mixedRange.ComputeUpperBound(), Is.SameAs(mixedUpper));
        }

        [Test]
        public void VerifyComputeHasBoundsOperation()
        {
            Assert.That(() => ((IMultiplicityRange)null).ComputeHasBoundsOperation(0, "*"), Throws.TypeOf<ArgumentNullException>());

            // Case A: lowerBound evaluates to "1", upperBound evaluates to "5".
            // HasBounds(1, "5") -> true (both match exactly).
            // HasBounds(0, "5") -> false (upper matches but lower does not, and lowerValue is not null).
            // HasBounds(1, "6") -> false (upper does not match).
            var rangeOneFive = BuildMockedRange(lowerValueOf: "1", upperValueOf: "5");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rangeOneFive.ComputeHasBoundsOperation(1, "5"), Is.True);
                Assert.That(rangeOneFive.ComputeHasBoundsOperation(0, "5"), Is.False);
                Assert.That(rangeOneFive.ComputeHasBoundsOperation(1, "6"), Is.False);
            }

            // Case B: no lowerBound (lowerValue == null), upperBound evaluates to "*".
            // HasBounds(0, "*") -> true via the 0..* implied branch.
            // HasBounds(1, "*") -> false (lower != upper and not the 0..* shape).
            var rangeZeroStar = BuildMockedRange(lowerValueOf: null, upperValueOf: "*");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rangeZeroStar.ComputeHasBoundsOperation(0, "*"), Is.True);
                Assert.That(rangeZeroStar.ComputeHasBoundsOperation(1, "*"), Is.False);
            }

            // Case C: no lowerBound (lowerValue == null), upperBound evaluates to "3".
            // HasBounds(3, "3") -> true via the single-multiplicity implied branch (lower == upper).
            // HasBounds(0, "3") -> false (neither branch satisfied).
            var rangeThree = BuildMockedRange(lowerValueOf: null, upperValueOf: "3");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rangeThree.ComputeHasBoundsOperation(3, "3"), Is.True);
                Assert.That(rangeThree.ComputeHasBoundsOperation(0, "3"), Is.False);
            }
        }

        [Test]
        public void VerifyComputeValueOfOperation()
        {
            var multiplicityRange = new MultiplicityRange();

            Assert.That(() => ((IMultiplicityRange)null).ComputeValueOfOperation(null), Throws.TypeOf<ArgumentNullException>());

            // bound == null -> null (explicit OCL contract, NOT a throw).
            Assert.That(multiplicityRange.ComputeValueOfOperation(null), Is.Null);

            // bound.isModelLevelEvaluable == false -> null.
            var notEvaluable = new Mock<IExpression>();
            notEvaluable.Setup(x => x.isModelLevelEvaluable).Returns(false);

            Assert.That(multiplicityRange.ComputeValueOfOperation(notEvaluable.Object), Is.Null);

            // Evaluate returns [] -> null.
            Assert.That(multiplicityRange.ComputeValueOfOperation(BuildEvaluableExpression([])), Is.Null);

            // Evaluate returns two elements -> null.
            var twoElementResult = BuildEvaluableExpression(
                [new LiteralInteger { Value = 1 }, new LiteralInteger { Value = 2 }]);
            Assert.That(multiplicityRange.ComputeValueOfOperation(twoElementResult), Is.Null);

            // Evaluate returns [LiteralInfinity] -> "*".
            var infinity = BuildEvaluableExpression([new LiteralInfinity()]);
            Assert.That(multiplicityRange.ComputeValueOfOperation(infinity), Is.EqualTo("*"));

            // Evaluate returns [LiteralInteger { Value = 0 }] -> "0".
            var zero = BuildEvaluableExpression([new LiteralInteger { Value = 0 }]);
            Assert.That(multiplicityRange.ComputeValueOfOperation(zero), Is.EqualTo("0"));

            // Evaluate returns [LiteralInteger { Value = 5 }] -> "5".
            var five = BuildEvaluableExpression([new LiteralInteger { Value = 5 }]);
            Assert.That(multiplicityRange.ComputeValueOfOperation(five), Is.EqualTo("5"));

            // Evaluate returns [LiteralInteger { Value = -1 }] -> null (negative not representable as UnlimitedNatural).
            var negative = BuildEvaluableExpression([new LiteralInteger { Value = -1 }]);
            Assert.That(multiplicityRange.ComputeValueOfOperation(negative), Is.Null);

            // Evaluate returns [LiteralBoolean] -> null (not a LiteralInfinity / LiteralInteger).
            var boolean = BuildEvaluableExpression([new LiteralBoolean { Value = true }]);
            Assert.That(multiplicityRange.ComputeValueOfOperation(boolean), Is.Null);
        }

        /// <summary>
        /// Builds a Moq-backed <see cref="IExpression"/> whose <c>isModelLevelEvaluable</c> is <c>true</c>
        /// and whose <c>Evaluate</c> returns the supplied result list for any target.
        /// </summary>
        /// <param name="result">The list of <see cref="IElement"/> that <c>Evaluate</c> should yield.</param>
        /// <returns>The configured mock <see cref="IExpression"/>.</returns>
        private static IExpression BuildEvaluableExpression(List<IElement> result)
        {
            var mockExpression = new Mock<IExpression>();
            mockExpression.Setup(x => x.isModelLevelEvaluable).Returns(true);
            mockExpression.Setup(x => x.Evaluate(It.IsAny<IElement>())).Returns(result);

            return mockExpression.Object;
        }

        /// <summary>
        /// Builds a Moq-backed <see cref="IMultiplicityRange"/> whose <c>ValueOf(upperBound)</c> returns
        /// <paramref name="upperValueOf"/> and whose <c>ValueOf(lowerBound)</c> returns <paramref name="lowerValueOf"/>.
        /// When <paramref name="lowerValueOf"/> is null the mock's <c>lowerBound</c> property returns null, exercising
        /// the "no lower bound" branches of <c>ComputeHasBoundsOperation</c>.
        /// </summary>
        /// <param name="lowerValueOf">
        /// The string the mock's <c>ValueOf(lowerBound)</c> should return; or null if the range has no lowerBound.
        /// </param>
        /// <param name="upperValueOf">
        /// The string the mock's <c>ValueOf(upperBound)</c> should return.
        /// </param>
        /// <returns>The configured mock <see cref="IMultiplicityRange"/>.</returns>
        private static IMultiplicityRange BuildMockedRange(string lowerValueOf, string upperValueOf)
        {
            var upperBoundExpression = new Mock<IExpression>().Object;
            var lowerBoundExpression = lowerValueOf is null ? null : new Mock<IExpression>().Object;

            var mockRange = new Mock<IMultiplicityRange>();
            mockRange.Setup(x => x.upperBound).Returns(upperBoundExpression);
            mockRange.Setup(x => x.lowerBound).Returns(lowerBoundExpression);
            mockRange.Setup(x => x.ValueOf(upperBoundExpression)).Returns(upperValueOf);
            mockRange.Setup(x => x.ValueOf(lowerBoundExpression)).Returns(lowerValueOf);

            return mockRange.Object;
        }
    }
}
