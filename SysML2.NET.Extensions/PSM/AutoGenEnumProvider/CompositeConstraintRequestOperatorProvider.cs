// -------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraintRequestOperatorProvider.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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
    /// The purpose of the <see cref="CompositeConstraintRequestOperatorProvider"/> is to convert a <see cref="CompositeConstraintRequestOperator"/>
    /// to and from its representation in the Systems Modeling API and Services
    /// </summary>
    public static class CompositeConstraintRequestOperatorProvider
    {
        /// <summary>
        /// The length in bytes of the longest CompositeConstraintRequestOperator representation
        /// </summary>
        private const int LongestValueByteLength = 3;

        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Char}"/> to a <see cref="CompositeConstraintRequestOperator"/>
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySpan{Char}"/> that is to be parsed.</param>
        /// <returns>A <see cref="CompositeConstraintRequestOperator"/> enumeration literal.</returns>
        /// <exception cref="ArgumentException">Thrown when the value denotes no literal.</exception>
        public static CompositeConstraintRequestOperator Parse(ReadOnlySpan<char> value)
        {
            if (TryParse(value, out var result))
            {
                return result;
            }

            throw new ArgumentException($"'{new string(value)}' is not a valid CompositeConstraintRequestOperator", nameof(value));
        }

        /// <summary>
        /// Tries to parse the <see cref="ReadOnlySpan{Char}"/> to a <see cref="CompositeConstraintRequestOperator"/>
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySpan{Char}"/> that is to be parsed.</param>
        /// <param name="result">The parsed literal, or <c>default</c> when the value denotes none.</param>
        /// <returns>True when the value was parsed.</returns>
        public static bool TryParse(ReadOnlySpan<char> value, out CompositeConstraintRequestOperator result)
        {
            if (value.Length == 3 && value.Equals("and".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                result = CompositeConstraintRequestOperator.and;
                return true;
            }

            if (value.Length == 2 && value.Equals("or".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                result = CompositeConstraintRequestOperator.or;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Parses the UTF-8 encoded <see cref="ReadOnlySpan{Byte}"/> to a <see cref="CompositeConstraintRequestOperator"/>
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySpan{Byte}"/> that is to be parsed.</param>
        /// <returns>A <see cref="CompositeConstraintRequestOperator"/> enumeration literal.</returns>
        /// <exception cref="ArgumentException">Thrown when the value denotes no literal.</exception>
        public static CompositeConstraintRequestOperator Parse(ReadOnlySpan<byte> value)
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

            throw new ArgumentException($"'{Encoding.UTF8.GetString(value)}' is not a valid CompositeConstraintRequestOperator", nameof(value));
        }

        /// <summary>
        /// Parses the UTF-8 encoded <see cref="ReadOnlySequence{Byte}"/> to a <see cref="CompositeConstraintRequestOperator"/>
        /// </summary>
        /// <param name="value">The <see cref="ReadOnlySequence{Byte}"/> that is to be parsed.</param>
        /// <returns>A <see cref="CompositeConstraintRequestOperator"/> enumeration literal.</returns>
        /// <exception cref="ArgumentException">Thrown when the value denotes no literal.</exception>
        public static CompositeConstraintRequestOperator Parse(in ReadOnlySequence<byte> value)
        {
            if (value.IsSingleSegment)
            {
                return Parse(value.FirstSpan);
            }

            if (value.Length > LongestValueByteLength)
            {
                throw new ArgumentException($"a CompositeConstraintRequestOperator is at most {LongestValueByteLength} bytes long", nameof(value));
            }

            Span<byte> buffer = stackalloc byte[LongestValueByteLength];
            value.CopyTo(buffer);

            return Parse(buffer[..(int)value.Length]);
        }

        /// <summary>
        /// Converts a <see cref="CompositeConstraintRequestOperator"/> to its UTF-8 encoded representation on the wire
        /// </summary>
        /// <param name="value">The <see cref="CompositeConstraintRequestOperator"/> that is to be converted.</param>
        /// <returns>A <see cref="ReadOnlySpan{Byte}"/> backed by static UTF-8 data.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the literal is not known.</exception>
        public static ReadOnlySpan<byte> ToUtf8Bytes(CompositeConstraintRequestOperator value)
        {
            return value switch
            {
                CompositeConstraintRequestOperator.and => "and"u8,
                CompositeConstraintRequestOperator.or => "or"u8,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
