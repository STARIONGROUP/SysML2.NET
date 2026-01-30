// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureKindProvider.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Systems.States;

    /// <summary>
    /// The purpose of the <see cref="TransitionFeatureKindProvider"/> is to provide conversion methods
    /// to the <see cref="TransitionFeatureKind"/> enum
    /// </summary>
    public static class TransitionFeatureKindProvider
    {
        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="TransitionFeatureKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Char}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="TransitionFeatureKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Char}"/> cannot be
        /// parsed into a valid <see cref="TransitionFeatureKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for  string parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly
        /// </remarks>
        public static TransitionFeatureKind Parse(ReadOnlySpan<char> value)
        {
            if (value.Length == 7 && value.Equals("trigger".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TransitionFeatureKind.Trigger;
            }

            if (value.Length == 5 && value.Equals("guard".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TransitionFeatureKind.Guard;
            }

            if (value.Length == 6 && value.Equals("effect".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TransitionFeatureKind.Effect;
            }

            throw new ArgumentException($"'{new string(value)}' is not a valid TransitionFeatureKind", nameof(value));
        }

        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="TransitionFeatureKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Byte}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="TransitionFeatureKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Byte}"/> cannot be
        /// parsed into a valid <see cref="TransitionFeatureKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for streaming parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly
        /// </remarks>
        public static TransitionFeatureKind Parse(ReadOnlySpan<byte> value)
        {
            if (value.Length == 7 && value.SequenceEqual("trigger"u8))
            {
                return TransitionFeatureKind.Trigger;
            }

            if (value.Length == 5 && value.SequenceEqual("guard"u8))
            {
                return TransitionFeatureKind.Guard;
            }

            if (value.Length == 6 && value.SequenceEqual("effect"u8))
            {
                return TransitionFeatureKind.Effect;
            }

            throw new ArgumentException($"'{System.Text.Encoding.UTF8.GetString(value)}' is not a valid TransitionFeatureKind", nameof(value));
        }

        /// <summary>
        /// Parses a UTF-8 encoded <see cref="ReadOnlySequence{Byte}"/> into a
        /// <see cref="TransitionFeatureKind"/> enumeration literal.
        /// </summary>
        /// <param name="value">
        /// A UTF-8 encoded sequence representing a <see cref="TransitionFeatureKind"/> literal.
        /// Valid values are
        /// <list type="bullet">
        /// <item><c>trigger (7 bytes) </c></item>
        /// <item><c>guard (5 bytes) </c></item>
        /// <item><c>effect (6 bytes) </c></item>
        /// </list>
        /// </param>
        /// <returns>
        /// The corresponding <see cref="TransitionFeatureKind"/> enumeration value.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the supplied sequence does not represent a valid
        /// <see cref="TransitionFeatureKind"/> literal.
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
        /// stack-allocated buffer (maximum 7 bytes).
        /// </para>
        /// <para>
        /// No allocations, no boxing, and JIT-friendly control flow.
        /// </para>
        /// </remarks>
        public static TransitionFeatureKind Parse(in ReadOnlySequence<byte> value)
        {
            if (value.IsSingleSegment)
            {
                return Parse(value.FirstSpan);
            }

            if (value.Length > 7)
            {
                throw new ArgumentException("Invalid FeatureDirectionKind length", nameof(value));
            }

            Span<byte> tmp = stackalloc byte[7];
            value.CopyTo(tmp);
            return Parse(tmp[..(int)value.Length]);
        }

        /// <summary>
        /// Converts a <see cref="TransitionFeatureKind"/> value to its
        /// lowercase UTF-8 encoded byte representation.
        /// </summary>
        /// <param name="value">
        /// The <see cref="TransitionFeatureKind"/> value to convert.
        /// </param>
        /// <returns>
        /// A <see cref="ReadOnlySpan{Byte}"/> containing the lowercase UTF-8
        /// encoding of the specified <see cref="TransitionFeatureKind"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not a defined
        /// <see cref="TransitionFeatureKind"/> enumeration value.
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
        /// <item><c>trigger</c></item>
        /// <item><c>guard</c></item>
        /// <item><c>effect</c></item>
        /// </list>
        /// </para>
        /// <para>
        /// No allocations, no boxing, branch-predictable switch,
        /// and JIT-friendly control flow.
        /// </para>
        /// </remarks>
        public static ReadOnlySpan<byte> ToUtf8LowerBytes(TransitionFeatureKind value)
        {
            return value switch
            {
                TransitionFeatureKind.Trigger => "trigger"u8,
                TransitionFeatureKind.Guard => "guard"u8,
                TransitionFeatureKind.Effect => "effect"u8,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
