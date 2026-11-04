// ------------------------------------------------------------------------------------------------
// <copyright file="PageTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.PSM
{
    using System;
    using System.Text;

    using NUnit.Framework;

    using SysML2.NET.PSM;

    [TestFixture]
    public class PageTestFixture
    {
        private const string SpecificationToken = "MTYxODg2MTQ5NjYzMnwyMDEwOWY0MC00ODI1LTQxNmEtODZmNi03NTA4YWM0MmEwMjE";

        private static readonly Guid SpecificationIdentifier = Guid.Parse("20109f40-4825-416a-86f6-7508ac42a021");

        private static string Encode(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        [Test]
        public void VerifyToString()
        {
            Assert.That(new Page { Epoch = 1618861496632, Identifier = SpecificationIdentifier }.ToString(), Is.EqualTo(SpecificationToken));

            Assert.That(new Page().ToString(), Is.EqualTo(Encode($"0|{Guid.Empty}").TrimEnd('=')));
        }

        [Test]
        public void VerifyTryParse()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(Page.TryParse(null, out _), Is.False);
                Assert.That(Page.TryParse(" ", out _), Is.False);
                Assert.That(Page.TryParse("not a token!", out _), Is.False);
                Assert.That(Page.TryParse(Encode($"1618861496632{SpecificationIdentifier}"), out _), Is.False);
                Assert.That(Page.TryParse(Encode($"epoch|{SpecificationIdentifier}"), out _), Is.False);
                Assert.That(Page.TryParse(Encode($"-1|{SpecificationIdentifier}"), out _), Is.False);
                Assert.That(Page.TryParse(Encode("1618861496632|not-a-guid"), out var rejected), Is.False);
                Assert.That(rejected, Is.EqualTo(default(Page)));
            }

            Assert.That(Page.TryParse(SpecificationToken, out var page), Is.True);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(page.Epoch, Is.EqualTo(1618861496632));
                Assert.That(page.Identifier, Is.EqualTo(SpecificationIdentifier));
                Assert.That(Page.TryParse(Encode($"1618861496632|{SpecificationIdentifier}"), out var padded), Is.True);
                Assert.That(padded, Is.EqualTo(page));
                Assert.That(Page.TryParse(Encode($"1618861496632|{SpecificationIdentifier}?").Replace('/', '_').Replace('+', '-'), out _), Is.False);
            }

            var roundTripped = new Page { Epoch = 1618862165315, Identifier = Guid.Parse("08f433db-b6d4-41b8-9020-e820ebc417be") };

            Assert.That(Page.TryParse(roundTripped.ToString(), out var parsed) && parsed.Equals(roundTripped), Is.True);
        }
    }
}
