// -------------------------------------------------------------------------------------------------
// <copyright file="GuidSequenceEquality.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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
    /// Provides utility methods for comparing sequences of <see cref="Guid"/> values
    /// according to ordered and unordered equality semantics.
    /// </summary>
    public static class GuidSequenceEquality
    {
        /// <summary>
        /// Determines whether two sequences of <see cref="Guid"/> values are equal
        /// using an ordered (positional) comparison.
        /// </summary>
        /// <param name="a">
        /// The first sequence to compare. The order of elements is significant.
        /// </param>
        /// <param name="b">
        /// The second sequence to compare. The order of elements is significant.
        /// </param>
        /// <returns>
        /// <c>true</c> if both sequences contain the same number of elements and each
        /// element in <paramref name="a"/> is equal to the corresponding element in
        /// <paramref name="b"/> at the same position; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs a fast, allocation-free comparison and is suitable
        /// for properties with <c>isOrdered = true</c> semantics in the metamodel.
        /// </remarks>
        public static bool OrderedEqual(IReadOnlyList<Guid> a, IReadOnlyList<Guid> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;

            var count = a.Count;
            if (count != b.Count) return false;

            for (var i = 0; i < count; i++)
            {
                if (a[i] != b[i]) return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether two sequences of <see cref="Guid"/> values are equal
        /// regardless of element ordering.
        /// </summary>
        /// <param name="a">
        /// The first sequence to compare. Element order is ignored.
        /// </param>
        /// <param name="b">
        /// The second sequence to compare. Element order is ignored.
        /// </param>
        /// <returns>
        /// <c>true</c> if both sequences contain the same elements with the same
        /// multiplicities, independent of order; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method uses a hash-based comparison and correctly handles duplicate
        /// values. It is suitable for properties with <c>isOrdered = false</c>
        /// semantics in the metamodel.
        /// </remarks>
        public static bool UnorderedEqual(IReadOnlyList<Guid> a, IReadOnlyList<Guid> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;

            var count = a.Count;
            if (count != b.Count) return false;

            var counts = new Dictionary<Guid, int>(count);

            for (var i = 0; i < count; i++)
            {
                var key = a[i];
                if (counts.TryGetValue(key, out var existing))
                {
                    counts[key] = existing + 1;
                }
                else
                {
                    counts.Add(key, 1);
                }
            }

            for (var i = 0; i < count; i++)
            {
                var key = b[i];
                if (!counts.TryGetValue(key, out var existing))
                {
                    return false;
                }

                if (existing == 1)
                {
                    counts.Remove(key);
                }
                else
                {
                    counts[key] = existing - 1;
                }
            }

            return counts.Count == 0;
        }
    }
}
