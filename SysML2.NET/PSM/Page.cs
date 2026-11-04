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
    /// Defines the structure of the page for the pagination process
    /// </summary>
    public struct Page
    {
        /// <summary>
        /// The time, in milliseconds since the Unix epoch, that positions the page
        /// </summary>
        public long Epoch { get; set; }

        /// <summary>
        /// The page identifier
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Converts a page token into a <see cref="Page"/>
        /// </summary>
        /// <param name="token">
        /// The page token: the unpadded Base64 encoding of an epoch and an identifier separated by a vertical bar (Systems Modeling API and Services, Clause 8.1.3)
        /// </param>
        /// <param name="page">
        /// The <see cref="Page"/> when the conversion succeeds, or the default <see cref="Page"/> otherwise
        /// </param>
        /// <returns>
        /// <c>true</c> when <paramref name="token"/> was converted, <c>false</c> otherwise
        /// </returns>
        public static bool TryParse(string token, out Page page)
        {
            page = default;

            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var base64 = token.Replace('-', '+').Replace('_', '/');
            base64 = base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');

            var bytes = new byte[base64.Length * 3 / 4];

            if (!Convert.TryFromBase64String(base64, bytes, out var byteCount))
            {
                return false;
            }

            var parts = Encoding.UTF8.GetString(bytes, 0, byteCount).Split('|');

            if (parts.Length != 2 || !long.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out var epoch) || !Guid.TryParse(parts[1], out var identifier))
            {
                return false;
            }

            page = new Page
            {
                Epoch = epoch,
                Identifier = identifier
            };

            return true;
        }

        /// <summary>
        /// Returns the page token that represents the <see cref="Page"/>
        /// </summary>
        /// <returns>
        /// The unpadded Base64 encoding of the epoch and the identifier separated by a vertical bar
        /// </returns>
        public override string ToString()
        {
            var text = $"{this.Epoch.ToString(CultureInfo.InvariantCulture)}|{this.Identifier}";

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text)).TrimEnd('=');
        }
    }
}
