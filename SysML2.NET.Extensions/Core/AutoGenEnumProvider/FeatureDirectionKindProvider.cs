// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureDirectionKindProvider.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;

    /// <summary>
    /// The purpose of the <see cref="FeatureDirectionKindProvider"/> is to provide conversion methods
    /// to the <see cref="FeatureDirectionKind"/> enum
    /// </summary>
    public static class FeatureDirectionKindProvider
    {
        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="FeatureDirectionKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Char}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="FeatureDirectionKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Char}"/> cannot be
        /// parsed into a valid <see cref="FeatureDirectionKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for  string parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly
        /// </remarks>
        public static FeatureDirectionKind Parse(ReadOnlySpan<char> value)
        {
            if (value.Length == 2 && value.Equals("in".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return FeatureDirectionKind.In;
            }

            if (value.Length == 5 && value.Equals("inout".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return FeatureDirectionKind.Inout;
            }

            if (value.Length == 3 && value.Equals("out".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return FeatureDirectionKind.Out;
            }

            throw new ArgumentException($"'{new string(value)}' is not a valid FeatureDirectionKind", nameof(value));
        }

        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="FeatureDirectionKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Byte}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="FeatureDirectionKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Byte}"/> cannot be
        /// parsed into a valid <see cref="FeatureDirectionKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for streaming parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly
        /// </remarks>
        public static FeatureDirectionKind Parse(ReadOnlySpan<byte> value)
        {
            if (value.Length == 2 && value.SequenceEqual("in"u8))
            {
                return FeatureDirectionKind.In;
            }

            if (value.Length == 5 && value.SequenceEqual("inout"u8))
            {
                return FeatureDirectionKind.Inout;
            }

            if (value.Length == 3 && value.SequenceEqual("out"u8))
            {
                return FeatureDirectionKind.Out;
            }

            throw new ArgumentException($"'{System.Text.Encoding.UTF8.GetString(value)}' is not a valid FeatureDirectionKind", nameof(value));
        }

        /// <summary>
        /// Parses a UTF-8 encoded <see cref="ReadOnlySequence{Byte}"/> into a
        /// <see cref="FeatureDirectionKind"/> enumeration literal.
        /// </summary>
        /// <param name="value">
        /// A UTF-8 encoded sequence representing a <see cref="FeatureDirectionKind"/> literal.
        /// Valid values are
        /// <list type="bullet">
        /// <item><c>in (2 bytes) </c></item>
        /// <item><c>inout (5 bytes) </c></item>
        /// <item><c>out (3 bytes) </c></item>
        /// </list>
        /// </param>
        /// <returns>
        /// The corresponding <see cref="FeatureDirectionKind"/> enumeration value.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the supplied sequence does not represent a valid
        /// <see cref="FeatureDirectionKind"/> literal.
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
        /// stack-allocated buffer (maximum 5 bytes).
        /// </para>
        /// <para>
        /// No allocations, no boxing, and JIT-friendly control flow.
        /// </para>
        /// </remarks>
        public static FeatureDirectionKind Parse(in ReadOnlySequence<byte> value)
        {
            if (value.IsSingleSegment)
            {
                return Parse(value.FirstSpan);
            }

            if (value.Length > 5)
            {
                throw new ArgumentException("Invalid FeatureDirectionKind length", nameof(value));
            }

            Span<byte> tmp = stackalloc byte[5];
            value.CopyTo(tmp);
            return Parse(tmp[..(int)value.Length]);
        }

        /// <summary>
        /// Converts a <see cref="FeatureDirectionKind"/> value to its
        /// lowercase UTF-8 encoded byte representation.
        /// </summary>
        /// <param name="value">
        /// The <see cref="FeatureDirectionKind"/> value to convert.
        /// </param>
        /// <returns>
        /// A <see cref="ReadOnlySpan{Byte}"/> containing the lowercase UTF-8
        /// encoding of the specified <see cref="FeatureDirectionKind"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not a defined
        /// <see cref="FeatureDirectionKind"/> enumeration value.
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
        /// <item><c>in</c></item>
        /// <item><c>inout</c></item>
        /// <item><c>out</c></item>
        /// </list>
        /// </para>
        /// <para>
        /// No allocations, no boxing, branch-predictable switch,
        /// and JIT-friendly control flow.
        /// </para>
        /// </remarks>
        public static ReadOnlySpan<byte> ToUtf8LowerBytes(FeatureDirectionKind value)
        {
            return value switch
            {
                FeatureDirectionKind.In => "in"u8,
                FeatureDirectionKind.Inout => "inout"u8,
                FeatureDirectionKind.Out => "out"u8,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
