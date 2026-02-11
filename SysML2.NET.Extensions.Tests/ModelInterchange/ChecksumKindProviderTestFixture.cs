// -------------------------------------------------------------------------------------------------
// <copyright file="ChecksumKindProviderTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions.Tests.Core.ModelInterchange
{
    using System;
    using System.Buffers;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;
    
    using SysML2.NET.ModelInterchange;

    [TestFixture]
    public class ChecksumKindProviderTestFixture
    {
        [Test]
        public void Verify_that_on_Parse_when_value_is_null_then_ArgumentNullException_is_thrown()
        {
            Assert.That(() =>
            {
                ReadOnlySpan<char> value = null;
                ChecksumKindProvider.Parse(value);
            }, Throws.ArgumentNullException);
            
            Assert.That(() =>
            {
                ReadOnlySpan<byte> value = null;
                ChecksumKindProvider.Parse(value);
            }, Throws.ArgumentNullException);
        }
        
        [TestCase(ChecksumKind.SHA1, "SHA1")]
        [TestCase(ChecksumKind.SHA224, "SHA224")]
        [TestCase(ChecksumKind.SHA256, "SHA256")]
        [TestCase(ChecksumKind.SHA384, "SHA-384")]
        [TestCase(ChecksumKind.SHA3256, "SHA3-256")]
        [TestCase(ChecksumKind.SHA3384, "SHA3-384")]
        [TestCase(ChecksumKind.SHA3512, "SHA3-512")]
        [TestCase(ChecksumKind.BLAKE2b256, "BLAKE2b-256")]
        [TestCase(ChecksumKind.BLAKE2b384, "BLAKE2b-384")]
        [TestCase(ChecksumKind.BLAKE2b512, "BLAKE2b-512")]
        [TestCase(ChecksumKind.BLAKE3, "BLAKE3")]
        [TestCase(ChecksumKind.MD2, "MD2")]
        [TestCase(ChecksumKind.MD4, "MD4")]
        [TestCase(ChecksumKind.MD5, "MD5")]
        [TestCase(ChecksumKind.MD6, "MD6")]
        [TestCase(ChecksumKind.ADLER32, "ADLER32")]
        public void Verify_that_Parse_charSpan_parses_known_values_case_insensitively(ChecksumKind expected, string token)
        {
            // Upper
            Assert.That(ChecksumKindProvider.Parse(token.AsSpan()), Is.EqualTo(expected));

            // Lower
            Assert.That(ChecksumKindProvider.Parse(token.ToLowerInvariant().AsSpan()), Is.EqualTo(expected));

            // Mixed (simple mixed variant)
            var mixed = token.Length > 1
                ? char.ToLowerInvariant(token[0]) + token.Substring(1).ToUpperInvariant()
                : token;
            Assert.That(ChecksumKindProvider.Parse(mixed.AsSpan()), Is.EqualTo(expected));
        }

        [TestCase(ChecksumKind.SHA1, "SHA1")]
        [TestCase(ChecksumKind.SHA224, "SHA224")]
        [TestCase(ChecksumKind.SHA256, "SHA256")]
        [TestCase(ChecksumKind.SHA384, "SHA-384")]
        [TestCase(ChecksumKind.SHA3256, "SHA3-256")]
        [TestCase(ChecksumKind.SHA3384, "SHA3-384")]
        [TestCase(ChecksumKind.SHA3512, "SHA3-512")]
        [TestCase(ChecksumKind.BLAKE2b256, "BLAKE2b-256")]
        [TestCase(ChecksumKind.BLAKE2b384, "BLAKE2b-384")]
        [TestCase(ChecksumKind.BLAKE2b512, "BLAKE2b-512")]
        [TestCase(ChecksumKind.BLAKE3, "BLAKE3")]
        [TestCase(ChecksumKind.MD2, "MD2")]
        [TestCase(ChecksumKind.MD4, "MD4")]
        [TestCase(ChecksumKind.MD5, "MD5")]
        [TestCase(ChecksumKind.MD6, "MD6")]
        [TestCase(ChecksumKind.ADLER32, "ADLER32")]
        public void Verify_that_Parse_utf8Span_parses_known_values_case_sensitively(ChecksumKind expected, string token)
        {
            // Note: this overload uses SequenceEqual against uppercase literals.
            var utf8 = Encoding.UTF8.GetBytes(token);
            Assert.That(ChecksumKindProvider.Parse(utf8.AsSpan()), Is.EqualTo(expected));
        }

        [TestCase(ChecksumKind.SHA1, "SHA1")]
        [TestCase(ChecksumKind.SHA224, "SHA224")]
        [TestCase(ChecksumKind.SHA256, "SHA256")]
        [TestCase(ChecksumKind.SHA384, "SHA-384")]
        [TestCase(ChecksumKind.SHA3256, "SHA3-256")]
        [TestCase(ChecksumKind.SHA3384, "SHA3-384")]
        [TestCase(ChecksumKind.SHA3512, "SHA3-512")]
        [TestCase(ChecksumKind.BLAKE2b256, "BLAKE2b-256")]
        [TestCase(ChecksumKind.BLAKE2b384, "BLAKE2b-384")]
        [TestCase(ChecksumKind.BLAKE2b512, "BLAKE2b-512")]
        [TestCase(ChecksumKind.BLAKE3, "BLAKE3")]
        [TestCase(ChecksumKind.MD2, "MD2")]
        [TestCase(ChecksumKind.MD4, "MD4")]
        [TestCase(ChecksumKind.MD5, "MD5")]
        [TestCase(ChecksumKind.MD6, "MD6")]
        [TestCase(ChecksumKind.ADLER32, "ADLER32")]
        public void Verify_that_Parse_readonlySequence_parses_single_segment(ChecksumKind expected, string token)
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            var sequence = new ReadOnlySequence<byte>(bytes);
            Assert.That(ChecksumKindProvider.Parse(sequence), Is.EqualTo(expected));
        }

        [Test]
        public void Verify_that_Parse_readonlySequence_parses_multi_segment_using_stackalloc_copy()
        {
            // Multi-segment "SHA256" -> "SHA" + "256"
            var seg1 = Encoding.UTF8.GetBytes("SHA");
            var seg2 = Encoding.UTF8.GetBytes("256");

            var first = new BufferSegment(seg1);
            var last = first.Append(seg2);

            var sequence = new ReadOnlySequence<byte>(first, 0, last, last.Memory.Length);

            Assert.That(sequence.IsSingleSegment, Is.False);
            Assert.That(ChecksumKindProvider.Parse(sequence), Is.EqualTo(ChecksumKind.SHA256));
        }

        [Test]
        public void Verify_that_Parse_readonlySequence_throws_when_length_exceeds_16()
        {
            var bytes = Enumerable.Repeat((byte)'A', 17).ToArray();
            var sequence = new ReadOnlySequence<byte>(bytes);

            var ex = Assert.Throws<ArgumentException>(() => ChecksumKindProvider.Parse(sequence));
            Assert.That(ex!.ParamName, Is.EqualTo("value"));
            Assert.That(ex.Message, Does.Contain("'AAAAAAAAAAAAAAAAA' is not a valid ChecksumKind (Parameter 'value')"));
        }

        [Test]
        public void Verify_that_Parse_charSpan_throws_for_unknown_value()
        {
            var ex = Assert.Throws<ArgumentException>(() => ChecksumKindProvider.Parse("NOPE".AsSpan()));
            Assert.That(ex!.ParamName, Is.EqualTo("value"));
            Assert.That(ex.Message, Does.Contain("is not a valid ChecksumKind"));
        }

        [Test]
        public void Verify_that_Parse_utf8Span_throws_for_unknown_value_and_message_contains_decoded_value()
        {
            var bytes = Encoding.UTF8.GetBytes("NOPE");
            var ex = Assert.Throws<ArgumentException>(() => ChecksumKindProvider.Parse(bytes.AsSpan()));
            Assert.That(ex!.ParamName, Is.EqualTo("value"));
            Assert.That(ex.Message, Does.Contain("NOPE"));
            Assert.That(ex.Message, Does.Contain("is not a valid ChecksumKind"));
        }

        [TestCase(ChecksumKind.SHA1, "SHA1")]
        [TestCase(ChecksumKind.SHA224, "SHA224")]
        [TestCase(ChecksumKind.SHA256, "SHA256")]
        [TestCase(ChecksumKind.SHA384, "SHA-384")]
        [TestCase(ChecksumKind.SHA3256, "SHA3-256")]
        [TestCase(ChecksumKind.SHA3384, "SHA3-384")]
        [TestCase(ChecksumKind.SHA3512, "SHA3-512")]
        [TestCase(ChecksumKind.BLAKE2b256, "BLAKE2b-256")]
        [TestCase(ChecksumKind.BLAKE2b384, "BLAKE2b-384")]
        [TestCase(ChecksumKind.BLAKE2b512, "BLAKE2b-512")]
        [TestCase(ChecksumKind.BLAKE3, "BLAKE3")]
        [TestCase(ChecksumKind.MD2, "MD2")]
        [TestCase(ChecksumKind.MD4, "MD4")]
        [TestCase(ChecksumKind.MD5, "MD5")]
        [TestCase(ChecksumKind.MD6, "MD6")]
        [TestCase(ChecksumKind.ADLER32, "ADLER32")]
        public void Verify_that_ToUtf8LowerBytes_returns_expected_token_and_roundtrips_via_Parse_utf8Span(
            ChecksumKind kind,
            string expectedToken)
        {
            var bytes = ChecksumKindProvider.ToUtf8LowerBytes(kind);

            // Verify exact bytes content
            Assert.That(Encoding.UTF8.GetString(bytes), Is.EqualTo(expectedToken));

            // Round-trip via Parse(ReadOnlySpan<byte>)
            Assert.That(ChecksumKindProvider.Parse(bytes), Is.EqualTo(kind));
        }

        [Test]
        public void Verify_that_ToUtf8LowerBytes_throws_for_undefined_enum_value()
        {
            // Pick a clearly undefined value
            var invalid = (ChecksumKind)123456;
            Assert.Throws<ArgumentOutOfRangeException>(() => ChecksumKindProvider.ToUtf8LowerBytes(invalid));
        }

        /// <summary>
        /// Minimal ReadOnlySequence segment helper to build multi-segment sequences.
        /// </summary>
        private sealed class BufferSegment : ReadOnlySequenceSegment<byte>
        {
            public BufferSegment(ReadOnlyMemory<byte> memory)
            {
                Memory = memory;
            }

            public BufferSegment Append(ReadOnlyMemory<byte> memory)
            {
                var segment = new BufferSegment(memory)
                {
                    RunningIndex = RunningIndex + Memory.Length
                };

                Next = segment;
                return segment;
            }
        }
    }
}
