// ------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraintRequestOperatorProviderTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Tests.PSM.EnumProvider
{
    using System;
    using System.Buffers;
    using System.Text;

    using NUnit.Framework;

    using SysML2.NET.Extensions.PSM;
    using SysML2.NET.PSM.Enumerations;

    [TestFixture]
    public class PrimitiveConstraintRequestOperatorProviderTestFixture
    {
        [Test]
        public void VerifyParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse("<".AsSpan()), Is.EqualTo(PrimitiveConstraintRequestOperator.lessthan));
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse("<=".AsSpan()), Is.EqualTo(PrimitiveConstraintRequestOperator.lessthanorequalto));
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse("=".AsSpan()), Is.EqualTo(PrimitiveConstraintRequestOperator.equalto));
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse(">".AsSpan()), Is.EqualTo(PrimitiveConstraintRequestOperator.greaterthan));
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse(">=".AsSpan()), Is.EqualTo(PrimitiveConstraintRequestOperator.greaterthanorequalto));
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse("in".AsSpan()), Is.EqualTo(PrimitiveConstraintRequestOperator.@in));
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse("instanceOf".AsSpan()), Is.EqualTo(PrimitiveConstraintRequestOperator.instanceOf));
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse("INSTANCEOF".AsSpan()), Is.EqualTo(PrimitiveConstraintRequestOperator.instanceOf));

                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse("instanceOf"u8), Is.EqualTo(PrimitiveConstraintRequestOperator.instanceOf));
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse("<="u8), Is.EqualTo(PrimitiveConstraintRequestOperator.lessthanorequalto));

                Assert.That(() => PrimitiveConstraintRequestOperatorProvider.Parse("Starion".AsSpan()), Throws.ArgumentException);
                Assert.That(() => PrimitiveConstraintRequestOperatorProvider.Parse(string.Empty.AsSpan()), Throws.ArgumentException);
                Assert.That(() => PrimitiveConstraintRequestOperatorProvider.Parse("Starion"u8), Throws.ArgumentException);
                Assert.That(() => PrimitiveConstraintRequestOperatorProvider.Parse("aRatherLongerValue"u8), Throws.ArgumentException);
            }

            var singleSegment = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("instanceOf"));
            var unknownSegment = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("Starion"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintRequestOperatorProvider.Parse(in singleSegment), Is.EqualTo(PrimitiveConstraintRequestOperator.instanceOf));
                Assert.That(() => PrimitiveConstraintRequestOperatorProvider.Parse(in unknownSegment), Throws.ArgumentException);
            }
        }

        [Test]
        public void VerifyTryParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintRequestOperatorProvider.TryParse(">=".AsSpan(), out var greaterThanOrEqualTo), Is.True);
                Assert.That(greaterThanOrEqualTo, Is.EqualTo(PrimitiveConstraintRequestOperator.greaterthanorequalto));
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintRequestOperatorProvider.TryParse("Starion".AsSpan(), out var unknown), Is.False);
                Assert.That(unknown, Is.EqualTo(default(PrimitiveConstraintRequestOperator)));
            }
        }

        [Test]
        public void VerifyToUtf8Bytes()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes(PrimitiveConstraintRequestOperator.lessthan).SequenceEqual("<"u8), Is.True);
                Assert.That(PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes(PrimitiveConstraintRequestOperator.lessthanorequalto).SequenceEqual("<="u8), Is.True);
                Assert.That(PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes(PrimitiveConstraintRequestOperator.equalto).SequenceEqual("="u8), Is.True);
                Assert.That(PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes(PrimitiveConstraintRequestOperator.greaterthan).SequenceEqual(">"u8), Is.True);
                Assert.That(PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes(PrimitiveConstraintRequestOperator.greaterthanorequalto).SequenceEqual(">="u8), Is.True);
                Assert.That(PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes(PrimitiveConstraintRequestOperator.@in).SequenceEqual("in"u8), Is.True);
                Assert.That(PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes(PrimitiveConstraintRequestOperator.instanceOf).SequenceEqual("instanceOf"u8), Is.True);
                Assert.That(() => PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes((PrimitiveConstraintRequestOperator)123456), Throws.TypeOf<ArgumentOutOfRangeException>());
            }
        }
    }
}
