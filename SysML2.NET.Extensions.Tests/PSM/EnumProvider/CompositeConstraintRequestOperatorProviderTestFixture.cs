// ------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraintRequestOperatorProviderTestFixture.cs" company="Starion Group S.A.">
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
    public class CompositeConstraintRequestOperatorProviderTestFixture
    {
        [Test]
        public void VerifyParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintRequestOperatorProvider.Parse("and".AsSpan()), Is.EqualTo(CompositeConstraintRequestOperator.and));
                Assert.That(CompositeConstraintRequestOperatorProvider.Parse("or".AsSpan()), Is.EqualTo(CompositeConstraintRequestOperator.or));
                Assert.That(CompositeConstraintRequestOperatorProvider.Parse("AND".AsSpan()), Is.EqualTo(CompositeConstraintRequestOperator.and));

                Assert.That(CompositeConstraintRequestOperatorProvider.Parse("and"u8), Is.EqualTo(CompositeConstraintRequestOperator.and));
                Assert.That(CompositeConstraintRequestOperatorProvider.Parse("or"u8), Is.EqualTo(CompositeConstraintRequestOperator.or));

                Assert.That(() => CompositeConstraintRequestOperatorProvider.Parse("xor".AsSpan()), Throws.ArgumentException);
                Assert.That(() => CompositeConstraintRequestOperatorProvider.Parse(string.Empty.AsSpan()), Throws.ArgumentException);
                Assert.That(() => CompositeConstraintRequestOperatorProvider.Parse("xor"u8), Throws.ArgumentException);
                Assert.That(() => CompositeConstraintRequestOperatorProvider.Parse("Starion"u8), Throws.ArgumentException);
            }

            var singleSegment = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("and"));
            var unknownSegment = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("Starion"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintRequestOperatorProvider.Parse(in singleSegment), Is.EqualTo(CompositeConstraintRequestOperator.and));
                Assert.That(() => CompositeConstraintRequestOperatorProvider.Parse(in unknownSegment), Throws.ArgumentException);
            }
        }

        [Test]
        public void VerifyTryParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintRequestOperatorProvider.TryParse("or".AsSpan(), out var or), Is.True);
                Assert.That(or, Is.EqualTo(CompositeConstraintRequestOperator.or));
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintRequestOperatorProvider.TryParse("xor".AsSpan(), out var unknown), Is.False);
                Assert.That(unknown, Is.EqualTo(default(CompositeConstraintRequestOperator)));
            }
        }

        [Test]
        public void VerifyToUtf8Bytes()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintRequestOperatorProvider.ToUtf8Bytes(CompositeConstraintRequestOperator.and).SequenceEqual("and"u8), Is.True);
                Assert.That(CompositeConstraintRequestOperatorProvider.ToUtf8Bytes(CompositeConstraintRequestOperator.or).SequenceEqual("or"u8), Is.True);
                Assert.That(() => CompositeConstraintRequestOperatorProvider.ToUtf8Bytes((CompositeConstraintRequestOperator)123456), Throws.TypeOf<ArgumentOutOfRangeException>());
            }
        }
    }
}
