// ------------------------------------------------------------------------------------------------
// <copyright file="FlexoJwt.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;

    /// <summary>
    /// Creates the HMAC-SHA256 JSON Web Tokens that the Flexo layer1 service accepts, signed with a secret that is
    /// generated per test run so that no secret is stored in source
    /// </summary>
    public sealed class FlexoJwt
    {
        /// <summary>
        /// The issuer that layer1 is configured to require (its <c>JWT_DOMAIN</c>)
        /// </summary>
        public const string Issuer = "http://flexo-mms-services";

        /// <summary>
        /// The audience that layer1 is configured to require (its <c>JWT_AUDIENCE</c>)
        /// </summary>
        public const string Audience = "flexo-mms-audience";

        /// <summary>
        /// The realm that layer1 is configured with (its <c>JWT_REALM</c>)
        /// </summary>
        public const string Realm = "flexo-mms";

        /// <summary>
        /// The user that <c>cluster.trig</c> grants the super-administrator policy to
        /// </summary>
        public const string UserName = "ldap/user/user01";

        /// <summary>
        /// The group that <c>cluster.trig</c> grants the super-administrator policy to
        /// </summary>
        public const string Group = "super_admins";

        /// <summary>
        /// Initializes a new instance of the <see cref="FlexoJwt"/> class with a random secret
        /// </summary>
        public FlexoJwt()
        {
            var secretBytes = RandomNumberGenerator.GetBytes(48);
            this.Secret = ToBase64Url(secretBytes);
        }

        /// <summary>
        /// Gets the secret that layer1 must be configured with (its <c>JWT_SECRET</c>)
        /// </summary>
        public string Secret { get; }

        /// <summary>
        /// Creates a signed token for the super-administrator user
        /// </summary>
        /// <param name="lifetime">
        /// The time after which the token expires
        /// </param>
        /// <returns>
        /// The compact serialization of the token
        /// </returns>
        public string CreateToken(TimeSpan lifetime)
        {
            var now = DateTimeOffset.UtcNow;
            string[] groups = [Group];

            var header = JsonSerializer.SerializeToUtf8Bytes(new { alg = "HS256", typ = "JWT" });

            var payload = JsonSerializer.SerializeToUtf8Bytes(new
            {
                iss = Issuer,
                aud = Audience,
                username = UserName,
                groups,
                iat = now.ToUnixTimeSeconds(),
                exp = now.Add(lifetime).ToUnixTimeSeconds()
            });

            var signingInput = $"{ToBase64Url(header)}.{ToBase64Url(payload)}";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(this.Secret));
            var signature = hmac.ComputeHash(Encoding.ASCII.GetBytes(signingInput));

            return $"{signingInput}.{ToBase64Url(signature)}";
        }

        /// <summary>
        /// Encodes bytes as base64url without padding (RFC 7515, section 2)
        /// </summary>
        /// <param name="bytes">
        /// The bytes to encode
        /// </param>
        /// <returns>
        /// The encoded text
        /// </returns>
        private static string ToBase64Url(byte[] bytes)
        {
            return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
    }
}
