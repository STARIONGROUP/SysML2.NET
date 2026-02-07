// -------------------------------------------------------------------------------------------------
// <copyright file="StringSequenceEquality.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// 
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Comparers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides utility methods for comparing sequences of <see cref="string"/> values
    /// using ordered and unordered equality semantics.
    /// </summary>
    /// <remarks>
    /// All comparisons performed by this class use <em>ordinal</em> string semantics
    /// by default (<see cref="StringComparer.Ordinal"/>), ensuring deterministic,
    /// culture-independent behavior suitable for identifiers, names, and other
    /// model-level string values.
    /// </remarks>
    public static class StringSequenceEquality
    {
        /// <summary>
        /// Determines whether two string sequences are equal using an ordered
        /// (positional) comparison with ordinal semantics.
        /// </summary>
        public static bool OrderedEqual(IReadOnlyList<string> a, IReadOnlyList<string> b, StringComparer comparer = null)
        {
            comparer ??= StringComparer.Ordinal;

            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;

            var count = a.Count;
            if (count != b.Count) return false;

            for (var i = 0; i < count; i++)
            {
                if (!comparer.Equals(a[i], b[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether two string sequences are equal regardless of order,
        /// using ordinal semantics.
        /// </summary>
        public static bool UnorderedEqual(IReadOnlyList<string> a, IReadOnlyList<string> b, StringComparer comparer = null)
        {
            comparer ??= StringComparer.Ordinal;

            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;

            var count = a.Count;
            if (count != b.Count) return false;

            var set = new Dictionary<string, int>(count, comparer);

            for (var i = 0; i < count; i++)
            {
                var value = a[i];
                if (set.TryGetValue(value, out var current))
                {
                    set[value] = current + 1;
                }
                else
                {
                    set[value] = 1;
                }
            }

            for (var i = 0; i < count; i++)
            {
                var value = b[i];
                if (!set.TryGetValue(value, out var current))
                {
                    return false;
                }

                if (current == 1)
                {
                    set.Remove(value);
                }
                else
                {
                    set[value] = current - 1;
                }
            }

            return set.Count == 0;
        }
    }
}
