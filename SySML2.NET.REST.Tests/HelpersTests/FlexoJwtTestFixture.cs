// ------------------------------------------------------------------------------------------------
// <copyright file="FlexoJwtTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class FlexoJwtTestFixture
    {
        [Test]
        public void VerifyCreateToken()
        {
            var jwt = new FlexoJwt();
            var token = jwt.CreateToken(TimeSpan.FromHours(1));
            var parts = token.Split('.');

            Assert.That(parts, Has.Length.EqualTo(3));

            using var header = JsonDocument.Parse(FromBase64Url(parts[0]));
            using var payload = JsonDocument.Parse(FromBase64Url(parts[1]));

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(jwt.Secret));
            var signature = hmac.ComputeHash(Encoding.ASCII.GetBytes($"{parts[0]}.{parts[1]}"));
            var tamperedSignature = hmac.ComputeHash(Encoding.ASCII.GetBytes($"{parts[0]}.{parts[1]}x"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(token, Does.Not.Contain("=").And.Not.Contain("+").And.Not.Contain("/"));
                Assert.That(header.RootElement.GetProperty("alg").GetString(), Is.EqualTo("HS256"));
                Assert.That(header.RootElement.GetProperty("typ").GetString(), Is.EqualTo("JWT"));
                Assert.That(payload.RootElement.GetProperty("iss").GetString(), Is.EqualTo(FlexoJwt.Issuer));
                Assert.That(payload.RootElement.GetProperty("aud").GetString(), Is.EqualTo(FlexoJwt.Audience));
                Assert.That(payload.RootElement.GetProperty("username").GetString(), Is.EqualTo(FlexoJwt.UserName));
                Assert.That(payload.RootElement.GetProperty("groups").EnumerateArray().Select(group => group.GetString()), Is.EqualTo((string[])[FlexoJwt.Group]));
                Assert.That(payload.RootElement.GetProperty("exp").GetInt64() - payload.RootElement.GetProperty("iat").GetInt64(), Is.EqualTo(3600));
                Assert.That(FromBase64Url(parts[2]), Is.EqualTo(signature));
                Assert.That(FromBase64Url(parts[2]), Is.Not.EqualTo(tamperedSignature));
                Assert.That(jwt.Secret, Has.Length.GreaterThanOrEqualTo(32));
                Assert.That(new FlexoJwt().Secret, Is.Not.EqualTo(jwt.Secret));
            }
        }

        private static byte[] FromBase64Url(string text)
        {
            var base64 = text.Replace('-', '+').Replace('_', '/');
            return Convert.FromBase64String(base64.PadRight(base64.Length + ((4 - (base64.Length % 4)) % 4), '='));
        }
    }
}
