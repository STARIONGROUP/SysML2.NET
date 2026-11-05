// ------------------------------------------------------------------------------------------------
// <copyright file="Page.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Defines the opaque page token of the cursor-based pagination (Systems Modeling API and Services, Clause 8.1.3)
    /// </summary>
    public readonly struct Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> struct
        /// </summary>
        /// <param name="token">
        /// The page token, kept exactly as the service returned it
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="token"/> is <c>null</c>, empty or white space
        /// </exception>
        public Page(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("The page token is required", nameof(token));
            }

            this.Token = token;
        }

        /// <summary>
        /// The page token, sent back to the service unchanged
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Creates a <see cref="Page"/> whose token is the unpadded Base64 encoding of an epoch and an identifier separated by a vertical bar
        /// </summary>
        /// <param name="epoch">
        /// The time, in milliseconds since the Unix epoch, that positions the page
        /// </param>
        /// <param name="identifier">
        /// The identifier that positions the page
        /// </param>
        /// <returns>
        /// The <see cref="Page"/> in the token format shown by the examples of Clause 8.1.3
        /// </returns>
        public static Page FromCursor(long epoch, Guid identifier)
        {
            var text = $"{epoch.ToString(CultureInfo.InvariantCulture)}|{identifier}";

            return new Page(Convert.ToBase64String(Encoding.UTF8.GetBytes(text)).TrimEnd('='));
        }

        /// <summary>
        /// Returns the page token
        /// </summary>
        /// <returns>
        /// The <see cref="Token"/>
        /// </returns>
        public override string ToString()
        {
            return this.Token;
        }

        /// <summary>
        /// Decodes the token as the unpadded Base64 encoding of an epoch and an identifier separated by a vertical bar
        /// </summary>
        /// <param name="epoch">
        /// The epoch, in milliseconds since the Unix epoch, when the decoding succeeds, or 0 otherwise
        /// </param>
        /// <param name="identifier">
        /// The identifier when the decoding succeeds, or <see cref="Guid.Empty"/> otherwise
        /// </param>
        /// <returns>
        /// <c>true</c> when the token has the format shown by the examples of Clause 8.1.3, <c>false</c> otherwise
        /// </returns>
        public bool TryDecode(out long epoch, out Guid identifier)
        {
            epoch = 0;
            identifier = Guid.Empty;

            if (string.IsNullOrWhiteSpace(this.Token))
            {
                return false;
            }

            var base64 = this.Token.Replace('-', '+').Replace('_', '/');
            base64 = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');

            var bytes = new byte[base64.Length * 3 / 4];

            if (!Convert.TryFromBase64String(base64, bytes, out var byteCount))
            {
                return false;
            }

            var parts = Encoding.UTF8.GetString(bytes, 0, byteCount).Split('|');

            if (parts.Length != 2 || !long.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out var decodedEpoch) || !Guid.TryParse(parts[1], out var decodedIdentifier))
            {
                return false;
            }

            epoch = decodedEpoch;
            identifier = decodedIdentifier;

            return true;
        }
    }
}
