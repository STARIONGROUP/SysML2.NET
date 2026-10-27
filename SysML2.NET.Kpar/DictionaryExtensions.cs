// ------------------------------------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Kpar
{
    using System;
    using System.Collections.Generic;
    using System.IO.Compression;
    using System.Linq;

    /// <summary>
    /// Extension methods for <see cref="Dictionary{TKey,TValue}"/> used by the <c>.kpar</c> ZIP reader.
    /// </summary>
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Removes all entries whose key ends with the specified suffix, using an ordinal case-insensitive comparison.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary from which entries will be removed.
        /// </param>
        /// <param name="suffix">
        /// The suffix to match against dictionary keys (for example <c>.project.json</c> or <c>.meta.json</c>).
        /// </param>
        /// <remarks>
        /// This method enumerates the dictionary keys to compute the removal set first, then removes matching entries.
        /// This avoids modifying the dictionary while iterating it.
        /// </remarks>
        internal static void RemoveWhereKeyEndsWith(this Dictionary<string, ZipArchiveEntry> dictionary, string suffix)
        {
            var keys = dictionary.Keys.Where(k => k.EndsWith(suffix, StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var k in keys) dictionary.Remove(k);
        }
    }
}
