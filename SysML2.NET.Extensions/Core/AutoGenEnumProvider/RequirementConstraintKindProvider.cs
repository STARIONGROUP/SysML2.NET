// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementConstraintKindProvider.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Core
{
    using System;
    using System.Buffers;

    using SysML2.NET.Core.Systems.Requirements;

    /// <summary>
    /// The purpose of the <see cref="RequirementConstraintKindProvider"/> is to provide conversion methods
    /// to the <see cref="RequirementConstraintKind"/> enum
    /// </summary>
    public static class RequirementConstraintKindProvider
    {
        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="RequirementConstraintKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Char}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="RequirementConstraintKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Char}"/> cannot be
        /// parsed into a valid <see cref="RequirementConstraintKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for  string parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly
        /// </remarks>
        public static RequirementConstraintKind Parse(ReadOnlySpan<char> value)
        {
            if (value.Length == 10 && value.Equals("assumption".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return RequirementConstraintKind.Assumption;
            }

            if (value.Length == 11 && value.Equals("requirement".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return RequirementConstraintKind.Requirement;
            }

            throw new ArgumentException($"'{new string(value)}' is not a valid RequirementConstraintKind", nameof(value));
        }

        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="RequirementConstraintKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Byte}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="RequirementConstraintKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Byte}"/> cannot be
        /// parsed into a valid <see cref="RequirementConstraintKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for streaming parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly
        /// </remarks>
        public static RequirementConstraintKind Parse(ReadOnlySpan<byte> value)
        {
            if (value.Length == 10 && value.SequenceEqual("assumption"u8))
            {
                return RequirementConstraintKind.Assumption;
            }

            if (value.Length == 11 && value.SequenceEqual("requirement"u8))
            {
                return RequirementConstraintKind.Requirement;
            }

            throw new ArgumentException($"'{System.Text.Encoding.UTF8.GetString(value)}' is not a valid RequirementConstraintKind", nameof(value));
        }

        /// <summary>
        /// Parses a UTF-8 encoded <see cref="ReadOnlySequence{Byte}"/> into a
        /// <see cref="RequirementConstraintKind"/> enumeration literal.
        /// </summary>
        /// <param name="value">
        /// A UTF-8 encoded sequence representing a <see cref="RequirementConstraintKind"/> literal.
        /// Valid values are
        /// <list type="bullet">
        /// <item><c>assumption (10 bytes) </c></item>
        /// <item><c>requirement (11 bytes) </c></item>
        /// </list>
        /// </param>
        /// <returns>
        /// The corresponding <see cref="RequirementConstraintKind"/> enumeration value.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the supplied sequence does not represent a valid
        /// <see cref="RequirementConstraintKind"/> literal.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method is optimized for streaming scenarios.
        /// It avoids heap allocations and performs fast short-circuit evaluation.
        /// </para>
        /// <para>
        /// If the sequence consists of a single contiguous segment, parsing is
        /// delegated directly to the <see cref="Parse(ReadOnlySpan{Byte})"/> overload.
        /// For multi-segment sequences, the data is copied into a small
        /// stack-allocated buffer (maximum 11 bytes).
        /// </para>
        /// <para>
        /// No allocations, no boxing, and JIT-friendly control flow.
        /// </para>
        /// </remarks>
        public static RequirementConstraintKind Parse(in ReadOnlySequence<byte> value)
        {
            if (value.IsSingleSegment)
            {
                return Parse(value.FirstSpan);
            }

            if (value.Length > 11)
            {
                throw new ArgumentException("Invalid FeatureDirectionKind length", nameof(value));
            }

            Span<byte> tmp = stackalloc byte[11];
            value.CopyTo(tmp);
            return Parse(tmp[..(int)value.Length]);
        }

        /// <summary>
        /// Converts a <see cref="RequirementConstraintKind"/> value to its
        /// lowercase UTF-8 encoded byte representation.
        /// </summary>
        /// <param name="value">
        /// The <see cref="RequirementConstraintKind"/> value to convert.
        /// </param>
        /// <returns>
        /// A <see cref="ReadOnlySpan{Byte}"/> containing the lowercase UTF-8
        /// encoding of the specified <see cref="RequirementConstraintKind"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not a defined
        /// <see cref="RequirementConstraintKind"/> enumeration value.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method is optimized for serialization scenarios (e.g. MessagePack).
        /// It returns a span backed by a static UTF-8 literal and performs
        /// no heap allocations.
        /// </para>
        /// <para>
        /// The returned span is backed by static UTF-8 data and remains valid for
        /// the lifetime of the current process.The span must be consumed
        /// immediately and must not be stored beyond the calling scope.
        /// </para>
        /// <para>
        /// Valid encodings are:
        /// <list type="bullet">
        /// <item><c>assumption</c></item>
        /// <item><c>requirement</c></item>
        /// </list>
        /// </para>
        /// <para>
        /// No allocations, no boxing, branch-predictable switch,
        /// and JIT-friendly control flow.
        /// </para>
        /// </remarks>
        public static ReadOnlySpan<byte> ToUtf8LowerBytes(RequirementConstraintKind value)
        {
            return value switch
            {
                RequirementConstraintKind.Assumption => "assumption"u8,
                RequirementConstraintKind.Requirement => "requirement"u8,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
