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
        public void VerifyConstructor()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new Page(null), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new Page(string.Empty), Throws.TypeOf<ArgumentException>());
                Assert.That(() => new Page(" "), Throws.TypeOf<ArgumentException>());
                Assert.That(new Page("not a token!").Token, Is.EqualTo("not a token!"));
                Assert.That(new Page(SpecificationToken).Token, Is.EqualTo(SpecificationToken));
            }

            Assert.That(default(Page).Token, Is.Null);
        }

        [Test]
        public void VerifyFromCursor()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(Page.FromCursor(1618861496632, SpecificationIdentifier).Token, Is.EqualTo(SpecificationToken));
                Assert.That(Page.FromCursor(0, Guid.Empty).Token, Is.EqualTo(Encode($"0|{Guid.Empty}").TrimEnd('=')));
                Assert.That(Page.FromCursor(1618861496632, Guid.Empty), Is.Not.EqualTo(Page.FromCursor(1618861496632, SpecificationIdentifier)));
            }
        }

        [Test]
        public void VerifyToString()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(new Page("not a token!").ToString(), Is.EqualTo("not a token!"));
                Assert.That(Page.FromCursor(1618861496632, SpecificationIdentifier).ToString(), Is.EqualTo(SpecificationToken));
                Assert.That(default(Page).ToString(), Is.Null);
            }
        }

        [Test]
        public void VerifyTryDecode()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(default(Page).TryDecode(out _, out _), Is.False);
                Assert.That(new Page("not a token!").TryDecode(out _, out _), Is.False);
                Assert.That(new Page(Encode($"1618861496632{SpecificationIdentifier}")).TryDecode(out _, out _), Is.False);
                Assert.That(new Page(Encode($"epoch|{SpecificationIdentifier}")).TryDecode(out _, out _), Is.False);
                Assert.That(new Page(Encode($"-1|{SpecificationIdentifier}")).TryDecode(out _, out _), Is.False);
                Assert.That(new Page(Encode($"1618861496632|{SpecificationIdentifier}?").Replace('/', '_').Replace('+', '-')).TryDecode(out _, out _), Is.False);
                Assert.That(new Page(Encode("1618861496632|not-a-guid")).TryDecode(out var rejectedEpoch, out var rejectedIdentifier), Is.False);
                Assert.That(rejectedEpoch, Is.Zero);
                Assert.That(rejectedIdentifier, Is.EqualTo(Guid.Empty));
            }

            Assert.That(new Page(SpecificationToken).TryDecode(out var epoch, out var identifier), Is.True);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(epoch, Is.EqualTo(1618861496632));
                Assert.That(identifier, Is.EqualTo(SpecificationIdentifier));
                Assert.That(new Page(Encode($"1618861496632|{SpecificationIdentifier}")).TryDecode(out var paddedEpoch, out var paddedIdentifier), Is.True);
                Assert.That(paddedEpoch, Is.EqualTo(epoch));
                Assert.That(paddedIdentifier, Is.EqualTo(identifier));
            }

            var roundTripIdentifier = Guid.Parse("08f433db-b6d4-41b8-9020-e820ebc417be");

            Assert.That(Page.FromCursor(1618862165315, roundTripIdentifier).TryDecode(out var roundTripEpoch, out var decodedIdentifier) && roundTripEpoch == 1618862165315 && decodedIdentifier == roundTripIdentifier, Is.True);
        }
    }
}
