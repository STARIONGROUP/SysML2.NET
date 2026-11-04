// ------------------------------------------------------------------------------------------------
// <copyright file="DirectionProvider.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.PSM
{
    using System;
    using System.Buffers;
    using System.Text;

    using SysML2.NET.PSM.Enumerations;

    /// <summary>
    /// The purpose of the <see cref="DirectionProvider"/> is to convert a <see cref="Direction"/>
    /// to and from its representation in the Systems Modeling API and Services
    /// </summary>
    public static class DirectionProvider
    {
        /// <summary>
        /// The length in bytes of the longest Direction representation
        /// </summary>
        private const int LongestValueByteLength = 4;

        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Char}"/> to a <see cref="Direction"/>
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySpan{Char}"/> that is to be parsed.</param>
        /// <returns>A <see cref="Direction"/> enumeration literal.</returns>
        /// <exception cref="ArgumentException">Thrown when the value denotes no literal.</exception>
        public static Direction Parse(ReadOnlySpan<char> value)
        {
            if (TryParse(value, out var result))
            {
                return result;
            }

            throw new ArgumentException($"'{new string(value)}' is not a valid Direction", nameof(value));
        }

        /// <summary>
        /// Parses the UTF-8 encoded <see cref="ReadOnlySpan{Byte}"/> to a <see cref="Direction"/>
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySpan{Byte}"/> that is to be parsed.</param>
        /// <returns>A <see cref="Direction"/> enumeration literal.</returns>
        /// <exception cref="ArgumentException">Thrown when the value denotes no literal.</exception>
        public static Direction Parse(ReadOnlySpan<byte> value)
        {
            if (value.Length <= LongestValueByteLength)
            {
                Span<char> characters = stackalloc char[LongestValueByteLength];

                var characterCount = Encoding.UTF8.GetChars(value, characters);

                if (TryParse(characters[..characterCount], out var result))
                {
                    return result;
                }
            }

            throw new ArgumentException($"'{Encoding.UTF8.GetString(value)}' is not a valid Direction", nameof(value));
        }

        /// <summary>
        /// Parses the UTF-8 encoded <see cref="ReadOnlySequence{Byte}"/> to a <see cref="Direction"/>
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySequence{Byte}"/> that is to be parsed.</param>
        /// <returns>A <see cref="Direction"/> enumeration literal.</returns>
        /// <exception cref="ArgumentException">Thrown when the value denotes no literal.</exception>
        public static Direction Parse(in ReadOnlySequence<byte> value)
        {
            if (value.IsSingleSegment)
            {
                return Parse(value.FirstSpan);
            }

            if (value.Length > LongestValueByteLength)
            {
                throw new ArgumentException($"a Direction is at most {LongestValueByteLength} bytes long", nameof(value));
            }

            Span<byte> buffer = stackalloc byte[LongestValueByteLength];
            value.CopyTo(buffer);

            return Parse(buffer[..(int)value.Length]);
        }

        /// <summary>
        /// Tries to parse the <see cref="ReadOnlySpan{Char}"/> to a <see cref="Direction"/>
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySpan{Char}"/> that is to be parsed.</param>
        /// <param name="result">The parsed literal, or <c>default</c> when the value denotes none.</param>
        /// <returns>True when the value was parsed.</returns>
        public static bool TryParse(ReadOnlySpan<char> value, out Direction result)
        {
            if (value.Length == 2 && value.Equals("in".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                result = Direction.@in;
                return true;
            }

            if (value.Length == 3 && value.Equals("out".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                result = Direction.@out;
                return true;
            }

            if (value.Length == 4 && value.Equals("both".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                result = Direction.both;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Converts a <see cref="Direction"/> to its UTF-8 encoded representation on the wire
        /// </summary>
        /// <param name="value">The <see cref="Direction"/> that is to be converted.</param>
        /// <returns>A <see cref="ReadOnlySpan{Byte}"/> backed by static UTF-8 data.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the literal is not known.</exception>
        public static ReadOnlySpan<byte> ToUtf8Bytes(Direction value)
        {
            return value switch
            {
                Direction.@in => "in"u8,
                Direction.@out => "out"u8,
                Direction.both => "both"u8,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
