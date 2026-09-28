// -------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraintOperatorProviderTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions.Tests.PSM.EnumProvider
{
    using System;
    using System.Buffers;
    using System.Text;

    using NUnit.Framework;

    using SysML2.NET.Extensions.PSM;
    using SysML2.NET.PSM.Enumerations;

    [TestFixture]
    public class PrimitiveConstraintOperatorProviderTestFixture
    {
        [Test]
        public void VerifyParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintOperatorProvider.Parse("<".AsSpan()), Is.EqualTo(PrimitiveConstraintOperator.lessthan));
                Assert.That(PrimitiveConstraintOperatorProvider.Parse("<=".AsSpan()), Is.EqualTo(PrimitiveConstraintOperator.lessthanorequalto));
                Assert.That(PrimitiveConstraintOperatorProvider.Parse("=".AsSpan()), Is.EqualTo(PrimitiveConstraintOperator.equalto));
                Assert.That(PrimitiveConstraintOperatorProvider.Parse(">".AsSpan()), Is.EqualTo(PrimitiveConstraintOperator.greaterthan));
                Assert.That(PrimitiveConstraintOperatorProvider.Parse(">=".AsSpan()), Is.EqualTo(PrimitiveConstraintOperator.greaterthanorequalto));
                Assert.That(PrimitiveConstraintOperatorProvider.Parse("in".AsSpan()), Is.EqualTo(PrimitiveConstraintOperator.@in));
                Assert.That(PrimitiveConstraintOperatorProvider.Parse("instanceOf".AsSpan()), Is.EqualTo(PrimitiveConstraintOperator.instanceOf));
                Assert.That(PrimitiveConstraintOperatorProvider.Parse("INSTANCEOF".AsSpan()), Is.EqualTo(PrimitiveConstraintOperator.instanceOf));

                Assert.That(PrimitiveConstraintOperatorProvider.Parse("instanceOf"u8), Is.EqualTo(PrimitiveConstraintOperator.instanceOf));
                Assert.That(PrimitiveConstraintOperatorProvider.Parse("<="u8), Is.EqualTo(PrimitiveConstraintOperator.lessthanorequalto));

                Assert.That(() => PrimitiveConstraintOperatorProvider.Parse("Starion".AsSpan()), Throws.ArgumentException);
                Assert.That(() => PrimitiveConstraintOperatorProvider.Parse(string.Empty.AsSpan()), Throws.ArgumentException);
                Assert.That(() => PrimitiveConstraintOperatorProvider.Parse("Starion"u8), Throws.ArgumentException);
                Assert.That(() => PrimitiveConstraintOperatorProvider.Parse("aRatherLongerValue"u8), Throws.ArgumentException);
            }

            var singleSegment = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("instanceOf"));
            var unknownSegment = new ReadOnlySequence<byte>(Encoding.UTF8.GetBytes("Starion"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintOperatorProvider.Parse(in singleSegment), Is.EqualTo(PrimitiveConstraintOperator.instanceOf));
                Assert.That(() => PrimitiveConstraintOperatorProvider.Parse(in unknownSegment), Throws.ArgumentException);
            }
        }

        [Test]
        public void VerifyTryParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintOperatorProvider.TryParse(">=".AsSpan(), out var greaterThanOrEqualTo), Is.True);
                Assert.That(greaterThanOrEqualTo, Is.EqualTo(PrimitiveConstraintOperator.greaterthanorequalto));
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintOperatorProvider.TryParse("Starion".AsSpan(), out var unknown), Is.False);
                Assert.That(unknown, Is.EqualTo(default(PrimitiveConstraintOperator)));
            }
        }

        [Test]
        public void VerifyToUtf8Bytes()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintOperatorProvider.ToUtf8Bytes(PrimitiveConstraintOperator.lessthan).SequenceEqual("<"u8), Is.True);
                Assert.That(PrimitiveConstraintOperatorProvider.ToUtf8Bytes(PrimitiveConstraintOperator.lessthanorequalto).SequenceEqual("<="u8), Is.True);
                Assert.That(PrimitiveConstraintOperatorProvider.ToUtf8Bytes(PrimitiveConstraintOperator.equalto).SequenceEqual("="u8), Is.True);
                Assert.That(PrimitiveConstraintOperatorProvider.ToUtf8Bytes(PrimitiveConstraintOperator.greaterthan).SequenceEqual(">"u8), Is.True);
                Assert.That(PrimitiveConstraintOperatorProvider.ToUtf8Bytes(PrimitiveConstraintOperator.greaterthanorequalto).SequenceEqual(">="u8), Is.True);
                Assert.That(PrimitiveConstraintOperatorProvider.ToUtf8Bytes(PrimitiveConstraintOperator.@in).SequenceEqual("in"u8), Is.True);
                Assert.That(PrimitiveConstraintOperatorProvider.ToUtf8Bytes(PrimitiveConstraintOperator.instanceOf).SequenceEqual("instanceOf"u8), Is.True);
                Assert.That(() => PrimitiveConstraintOperatorProvider.ToUtf8Bytes((PrimitiveConstraintOperator)123456), Throws.TypeOf<ArgumentOutOfRangeException>());
            }
        }
    }
}
