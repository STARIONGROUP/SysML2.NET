// -------------------------------------------------------------------------------------------------
// <copyright file="ChecksumKindProvider.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.ModelInterchange
{
    using System;
    using System.Buffers;

    /// <summary>
    /// Provides high-performance parsing and serialization helpers
    /// for the <see cref="ChecksumKind"/> enumeration.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This provider enables zero-allocation parsing from
    /// <see cref="ReadOnlySpan{Char}"/>, <see cref="ReadOnlySpan{Byte}"/>,
    /// and <see cref="ReadOnlySequence{Byte}"/> values.
    /// </para>
    /// <para>
    /// It is optimized for streaming deserialization scenarios
    /// such as System.Text.Json, MessagePack, and custom UTF-8 parsers.
    /// </para>
    /// <para>
    /// All parsing operations use length-based short-circuit evaluation
    /// to enable branch prediction and JIT-friendly control flow.
    /// </para>
    /// </remarks>
    public static class ChecksumKindProvider
    {
        /// <summary>
        /// Parses a textual checksum algorithm representation
        /// into a <see cref="ChecksumKind"/> enumeration value.
        /// </summary>
        /// <param name="value">
        /// The character span representing the checksum algorithm.
        /// </param>
        /// <returns>
        /// The corresponding <see cref="ChecksumKind"/> value.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> does not represent
        /// a valid checksum algorithm.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This overload is suited for string-based parsing.
        /// It performs no heap allocations and avoids boxing.
        /// </para>
        /// <para>
        /// Parsing is case-insensitive and uses
        /// <see cref="StringComparison.OrdinalIgnoreCase"/>.
        /// </para>
        /// </remarks>
        public static ChecksumKind Parse(ReadOnlySpan<char> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            switch (value.Length)
            {
                case 3:
                    if (value.Equals("MD2".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.MD2;
                    if (value.Equals("MD4".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.MD4;
                    if (value.Equals("MD5".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.MD5;
                    if (value.Equals("MD6".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.MD6;
                    break;
                case 4:
                    if (value.Equals("SHA1".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.SHA1;
                    break;
                case 6:
                    if (value.Equals("SHA224".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.SHA224;
                    if (value.Equals("SHA256".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.SHA256;
                    if (value.Equals("BLAKE3".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.BLAKE3;
                    break;
                case 7:
                    if (value.Equals("ADLER32".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.ADLER32;
                    if (value.Equals("SHA-384".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.SHA384;
                    break;
                case 8:
                    if (value.Equals("SHA3-256".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.SHA3256;
                    if (value.Equals("SHA3-384".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.SHA3384;
                    if (value.Equals("SHA3-512".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.SHA3512;
                    break;
                case 11:
                    if (value.Equals("BLAKE2b-256".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.BLAKE2b256;
                    if (value.Equals("BLAKE2b-384".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.BLAKE2b384;
                    if (value.Equals("BLAKE2b-512".AsSpan(), StringComparison.OrdinalIgnoreCase)) return ChecksumKind.BLAKE2b512;
                    break;
            }

            throw new ArgumentException($"'{new string(value)}' is not a valid ChecksumKind", nameof(value));
        }

        /// <summary>
        /// Parses a UTF-8 encoded byte span into a
        /// <see cref="ChecksumKind"/> enumeration value.
        /// </summary>
        /// <param name="value">
        /// The UTF-8 encoded byte span representing the checksum algorithm.
        /// </param>
        /// <returns>
        /// The corresponding <see cref="ChecksumKind"/> value.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> does not represent
        /// a valid checksum algorithm.
        /// </exception>
        public static ChecksumKind Parse(ReadOnlySpan<byte> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            switch (value.Length)
            {
                case 3:
                    if (value.SequenceEqual("MD2"u8)) return ChecksumKind.MD2;
                    if (value.SequenceEqual("MD4"u8)) return ChecksumKind.MD4;
                    if (value.SequenceEqual("MD5"u8)) return ChecksumKind.MD5;
                    if (value.SequenceEqual("MD6"u8)) return ChecksumKind.MD6;
                    break;
                case 4:
                     if (value.SequenceEqual("SHA1"u8)) return ChecksumKind.SHA1;
                    break;
                case 6:
                    if (value.SequenceEqual("SHA224"u8)) return ChecksumKind.SHA224;
                    if (value.SequenceEqual("SHA256"u8)) return ChecksumKind.SHA256;
                    if (value.SequenceEqual("BLAKE3"u8)) return ChecksumKind.BLAKE3;
                    break;
                case 7:
                    if (value.SequenceEqual("SHA-384"u8)) return ChecksumKind.SHA384;
                    if (value.SequenceEqual("ADLER32"u8)) return ChecksumKind.ADLER32;
                    break;
                case 8:
                    if (value.SequenceEqual("SHA3-256"u8)) return ChecksumKind.SHA3256;
                    if (value.SequenceEqual("SHA3-384"u8)) return ChecksumKind.SHA3384;
                    if (value.SequenceEqual("SHA3-512"u8)) return ChecksumKind.SHA3512;
                    break;
                case 11:
                    if (value.SequenceEqual("BLAKE2b-256"u8)) return ChecksumKind.BLAKE2b256;
                    if (value.SequenceEqual("BLAKE2b-384"u8)) return ChecksumKind.BLAKE2b384;
                    if (value.SequenceEqual("BLAKE2b-512"u8)) return ChecksumKind.BLAKE2b512;
                    break;
            }

            throw new ArgumentException($"'{System.Text.Encoding.UTF8.GetString(value)}' is not a valid ChecksumKind", nameof(value));
        }

        /// <summary>
        /// Parses a UTF-8 encoded <see cref="ReadOnlySequence{Byte}"/> into
        /// a <see cref="ChecksumKind"/> enumeration value.
        /// </summary>
        /// <param name="value">
        /// A UTF-8 encoded sequence representing a checksum algorithm.
        /// </param>
        /// <returns>
        /// The corresponding <see cref="ChecksumKind"/> value.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the supplied sequence does not represent
        /// a valid checksum algorithm.
        /// </exception>
        /// <remarks>
        /// <para>
        /// If the sequence is contiguous, parsing is delegated to the
        /// <see cref="Parse(ReadOnlySpan{Byte})"/> overload.
        /// </para>
        /// <para>
        /// For multi-segment sequences, the content is copied into
        /// a stack-allocated buffer before parsing.
        /// </para>
        /// <para>
        /// No heap allocations are performed.
        /// </para>
        /// </remarks>
        public static ChecksumKind Parse(in ReadOnlySequence<byte> value)
        {
            if (value.IsSingleSegment)
            {
                return Parse(value.FirstSpan);
            }

            if (value.Length > 16)
            {
                throw new ArgumentException("Invalid ChecksumKind length", nameof(value));
            }

            Span<byte> tmp = stackalloc byte[(int)value.Length];
            value.CopyTo(tmp);
            return Parse(tmp);
        }

        /// <summary>
        /// Converts a <see cref="ChecksumKind"/> value into its
        /// UTF-8 encoded byte representation.
        /// </summary>
        /// <param name="value">
        /// The <see cref="ChecksumKind"/> to convert.
        /// </param>
        /// <returns>
        /// A UTF-8 encoded byte span representing the checksum algorithm.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="value"/> is not a defined
        /// <see cref="ChecksumKind"/> enumeration literal.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method is optimized for serialization scenarios,
        /// such as MessagePack or JSON writers.
        /// </para>
        /// <para>
        /// The returned span is backed by static UTF-8 data
        /// and remains valid for the lifetime of the process.
        /// </para>
        /// <para>
        /// No heap allocations, no boxing, branch-predictable switch.
        /// </para>
        /// </remarks>
        public static ReadOnlySpan<byte> ToUtf8LowerBytes(ChecksumKind value)
        {
            return value switch
            {
                ChecksumKind.SHA1 => "SHA1"u8,
                ChecksumKind.SHA224 => "SHA224"u8,
                ChecksumKind.SHA256 => "SHA256"u8,
                ChecksumKind.SHA384 => "SHA-384"u8,
                ChecksumKind.SHA3256 => "SHA3-256"u8,
                ChecksumKind.SHA3384 => "SHA3-384"u8,
                ChecksumKind.SHA3512 => "SHA3-512"u8,
                ChecksumKind.BLAKE2b256 => "BLAKE2b-256"u8,
                ChecksumKind.BLAKE2b384 => "BLAKE2b-384"u8,
                ChecksumKind.BLAKE2b512 => "BLAKE2b-512"u8,
                ChecksumKind.BLAKE3 => "BLAKE3"u8,
                ChecksumKind.MD2 => "MD2"u8,
                ChecksumKind.MD4 => "MD4"u8,
                ChecksumKind.MD5 => "MD5"u8,
                ChecksumKind.MD6 => "MD6"u8,
                ChecksumKind.ADLER32 => "ADLER32"u8,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}
