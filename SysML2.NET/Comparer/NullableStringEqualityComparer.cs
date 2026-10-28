// ------------------------------------------------------------------------------------------------
// <copyright file="NullableStringEqualityComparer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Comparer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides equality comparison for string, handling null case
    /// </summary>
    public sealed class NullSafeStringComparer : IEqualityComparer<string>
    {
        /// <summary>Determines whether the specified objects are equal.</summary>
        /// <param name="x">The first <see cref="string"/> to compare.</param>
        /// <param name="y">The second <see cref="string"/> to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
        public bool Equals(string x, string y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return string.Equals(x, y, StringComparison.Ordinal);
        }

        /// <summary>Returns a hash code for the specified object.</summary>
        /// <param name="obj">The nullable <see cref="string"/> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        public int GetHashCode(string obj)
        {
            return obj?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Gets the instance of the <see cref="NullSafeStringComparer"/>
        /// </summary>
        public static readonly NullSafeStringComparer Instance = new();
    }
}
