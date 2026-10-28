// ------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Utilities
{
    using System;

    /// <summary>
    /// The <see cref="StringExtensions"/> class provides extensions methods for <see cref="String"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Normalizes a ZIP entry path by converting directory separators to forward
        /// slashes (<c>'/'</c>) and removing any leading <c>"./"</c> segment.
        /// </summary>
        /// <param name="path">
        /// The ZIP entry path to normalize.
        /// </param>
        /// <returns>
        /// A normalized path using forward slashes as directory separators and
        /// without a leading <c>"./"</c> segment, if present.
        /// </returns>
        public static string NormalizeZipPath(this string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.Replace('\\', '/');

            while (path.StartsWith("./", StringComparison.Ordinal))
            {
                path = path.Substring(2);
            }

            return path;
        }
    }
}
