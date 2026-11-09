// ------------------------------------------------------------------------------------------------
// <copyright file="LiteralRationalComparerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Tests.Core.DTO.Comparers
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Extensions.Core.DTO.Comparers;

    [TestFixture]
    public class LiteralRationalComparerTestFixture
    {
        [Test]
        public void VerifyEquals()
        {
            var comparer = new LiteralRationalComparer();
            var id = Guid.NewGuid();
            var literal = new LiteralRational { Id = id, Value = 2.5 };
            var copy = new LiteralRational { Id = id, Value = 2.5 };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(comparer.Equals(literal, null), Is.False);
                Assert.That(comparer.Equals(literal, literal), Is.True);
                Assert.That(comparer.Equals(literal, copy), Is.True);
                Assert.That(ComparerProvider.Resolve(literal).Equals(literal, copy), Is.True);
            }

            copy.Value = 3.0;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(comparer.Equals(literal, copy), Is.False);
                Assert.That(ComparerProvider.Resolve(literal).Equals(literal, copy), Is.False);
            }

            literal.Value = 0.1 + 0.2;
            copy.Value = 0.3;

            Assert.That(comparer.Equals(literal, copy), Is.True);

            copy.Id = Guid.NewGuid();

            Assert.That(comparer.Equals(literal, copy), Is.False);
        }
    }
}
