// ------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraintOperatorProviderTestFixture.cs" company="Starion Group S.A.">
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
    public class CompositeConstraintOperatorProviderTestFixture
    {
        [Test]
        public void VerifyParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintOperatorProvider.Parse("and".AsSpan()), Is.EqualTo(CompositeConstraintOperator.and));
                Assert.That(CompositeConstraintOperatorProvider.Parse("or".AsSpan()), Is.EqualTo(CompositeConstraintOperator.or));
                Assert.That(CompositeConstraintOperatorProvider.Parse("AND".AsSpan()), Is.EqualTo(CompositeConstraintOperator.and));

                Assert.That(CompositeConstraintOperatorProvider.Parse("and"u8), Is.EqualTo(CompositeConstraintOperator.and));
                Assert.That(CompositeConstraintOperatorProvider.Parse("or"u8), Is.EqualTo(CompositeConstraintOperator.or));

                Assert.That(() => CompositeConstraintOperatorProvider.Parse("xor".AsSpan()), Throws.ArgumentException);
                Assert.That(() => CompositeConstraintOperatorProvider.Parse(string.Empty.AsSpan()), Throws.ArgumentException);
                Assert.That(() => CompositeConstraintOperatorProvider.Parse("xor"u8), Throws.ArgumentException);
                Assert.That(() => CompositeConstraintOperatorProvider.Parse("Starion"u8), Throws.ArgumentException);
            }

            var singleSegment = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("and"));
            var unknownSegment = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("Starion"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintOperatorProvider.Parse(in singleSegment), Is.EqualTo(CompositeConstraintOperator.and));
                Assert.That(() => CompositeConstraintOperatorProvider.Parse(in unknownSegment), Throws.ArgumentException);
            }
        }

        [Test]
        public void VerifyTryParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintOperatorProvider.TryParse("or".AsSpan(), out var or), Is.True);
                Assert.That(or, Is.EqualTo(CompositeConstraintOperator.or));
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintOperatorProvider.TryParse("xor".AsSpan(), out var unknown), Is.False);
                Assert.That(unknown, Is.EqualTo(default(CompositeConstraintOperator)));
            }
        }

        [Test]
        public void VerifyToUtf8Bytes()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(CompositeConstraintOperatorProvider.ToUtf8Bytes(CompositeConstraintOperator.and).SequenceEqual("and"u8), Is.True);
                Assert.That(CompositeConstraintOperatorProvider.ToUtf8Bytes(CompositeConstraintOperator.or).SequenceEqual("or"u8), Is.True);
                Assert.That(() => CompositeConstraintOperatorProvider.ToUtf8Bytes((CompositeConstraintOperator)123456), Throws.TypeOf<ArgumentOutOfRangeException>());
            }
        }
    }
}
