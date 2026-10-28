// ------------------------------------------------------------------------------------------------
// <copyright file="PagedResultTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.PSM
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.PSM;

    [TestFixture]
    public class PagedResultTestFixture
    {
        private static readonly Guid FirstIdentifier = Guid.Parse("2f1a0e1c-6b2a-4d5e-9c3f-8a7b6d5e4c3b");

        private static readonly Guid LastIdentifier = Guid.Parse("7c4b2a19-3e5d-4f6a-8b9c-1d2e3f4a5b6c");

        [Test]
        public void VerifyPagedResult()
        {
            var pagedResult = new PagedResult<string>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(pagedResult.Records, Has.Count.EqualTo(0));
                Assert.That(pagedResult.Next, Is.Null);
                Assert.That(pagedResult.Previous, Is.Null);
            }

            pagedResult.Records = ["the first record", "the last record"];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(pagedResult.Records, Has.Count.EqualTo(2));
                Assert.That(pagedResult.Records[0], Is.EqualTo("the first record"));
                Assert.That(pagedResult.Records[^1], Is.EqualTo("the last record"));
            }

            pagedResult.Previous = new Page { Epoch = 1700000000, Identifier = FirstIdentifier };
            pagedResult.Next = new Page { Epoch = 1700000600, Identifier = LastIdentifier };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(pagedResult.Previous.Value.ToString(), Is.EqualTo($"1700000000|{FirstIdentifier}"));
                Assert.That(pagedResult.Next.Value.ToString(), Is.EqualTo($"1700000600|{LastIdentifier}"));
            }
        }
    }
}
