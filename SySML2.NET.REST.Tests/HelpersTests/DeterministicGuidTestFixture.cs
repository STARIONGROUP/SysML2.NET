// ------------------------------------------------------------------------------------------------
// <copyright file="DeterministicGuidTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class DeterministicGuidTestFixture
    {
        [Test]
        public void VerifyCreate()
        {
            var guid = DeterministicGuid.Create("root/CoverageModel");
            var bytes = guid.ToByteArray(true);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guid, Is.EqualTo(new Guid("9ca3add6-05b7-5b0f-9012-32f191129875")), "the version 5 UUID that RFC 9562 gives for the name in the scenario namespace");
                Assert.That(bytes[6] >> 4, Is.EqualTo(5), "version");
                Assert.That(bytes[8] >> 6, Is.EqualTo(0b10), "variant");
                Assert.That(DeterministicGuid.Create("root/CoverageModel"), Is.EqualTo(guid));
                Assert.That(DeterministicGuid.Create("root/Nested"), Is.Not.EqualTo(guid));
                Assert.That(() => DeterministicGuid.Create(null), Throws.ArgumentException);
                Assert.That(() => DeterministicGuid.Create(string.Empty), Throws.ArgumentException);
                Assert.That(() => DeterministicGuid.Create(" "), Throws.ArgumentException);
            }
        }
    }
}
