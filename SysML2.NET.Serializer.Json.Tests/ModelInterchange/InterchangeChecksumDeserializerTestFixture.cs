// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeChecksumDeserializerTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.Tests.ModelInterchange
{
    using System;
    using System.Buffers;
    using System.Text;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using NUnit.Framework;

    using SysML2.NET.ModelInterchange;
    using SysML2.NET.Serializer.Json.ModelInterchange;

    [TestFixture]
    public class InterchangeChecksumDeserializerTestFixture
    {
        [Test]
        public void Verify_that_DeSerialize_reads_value_and_algorithm_from_valid_object()
        {
            var json = @"{""value"":""ABCDEF0123456789"",""algorithm"":""SHA256""}";

            var checksum = DeserializeFromJson(json);

            Assert.That(checksum, Is.Not.Null);
            Assert.That(checksum.Value, Is.EqualTo("ABCDEF0123456789"));
            Assert.That(checksum.Algorithm, Is.EqualTo(ChecksumKind.SHA256));
        }

        [Test]
        public void Verify_that_DeSerialize_tolerates_null_value_and_sets_empty_string()
        {
            var json = @"{""value"":null,""algorithm"":""SHA1""}";

            var checksum = DeserializeFromJson(json);

            Assert.That(checksum.Value, Is.EqualTo(string.Empty));
            Assert.That(checksum.Algorithm, Is.EqualTo(ChecksumKind.SHA1));
        }

        [Test]
        public void Verify_that_DeSerialize_tolerates_null_algorithm_and_leaves_default()
        {
            var json = @"{""value"":""deadbeef"",""algorithm"":null}";

            var checksum = DeserializeFromJson(json);

            Assert.That(checksum.Value, Is.EqualTo("deadbeef"));
            Assert.That(checksum.Algorithm, Is.EqualTo(default(ChecksumKind)));
        }

        [Test]
        public void Verify_that_DeSerialize_skips_unknown_properties_and_continues()
        {
            var json = @"{
                ""value"":""1234"",
                ""unknownString"":""x"",
                ""unknownObj"":{""a"":1,""b"":[true,false]},
                ""unknownArr"":[1,2,3],
                ""algorithm"":""MD5""
            }";

            var checksum = DeserializeFromJson(json, NullLoggerFactory.Instance);

            Assert.That(checksum.Value, Is.EqualTo("1234"));
            Assert.That(checksum.Algorithm, Is.EqualTo(ChecksumKind.MD5));
        }

        [Test]
        public void Verify_that_DeSerialize_skips_non_string_algorithm_and_leaves_default()
        {
            var json = @"{""value"":""1234"",""algorithm"":123}";

            var checksum = DeserializeFromJson(json);

            Assert.That(checksum.Value, Is.EqualTo("1234"));
            Assert.That(checksum.Algorithm, Is.EqualTo(default(ChecksumKind)));
        }
        
        [Test]
        public void Verify_that_DeSerialize_reads_algorithm_from_multi_segment_value_sequence()
        {
            // Force HasValueSequence = true by using a segmented ReadOnlySequence<byte>.
            // We craft JSON so that the algorithm token ("SHA256") spans segments.

            var utf8 = Encoding.UTF8.GetBytes(@"{""value"":""v"",""algorithm"":""SHA256""}");

            // Split *inside* the algorithm string bytes so the ValueSequence is multi-segment.
            // Find the "SHA256" bytes and split after "SHA".
            var needle = Encoding.UTF8.GetBytes(@"""SHA256""");
            var idx = IndexOf(utf8, needle);
            Assert.That(idx, Is.GreaterThanOrEqualTo(0), "Test setup failed: could not find algorithm token.");

            // Position of actual letters starts after the first quote
            var startLetters = idx + 1;
            var splitAt = startLetters + 3; // "SHA" | "256"

            var sequence = CreateMultiSegmentSequence(utf8, splitAt);

            var reader = new Utf8JsonReader(sequence, isFinalBlock: true, state: default);

            // Move to StartObject
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject));

            var checksum = InterchangeChecksumDeserializer.DeSerialize(ref reader);

            Assert.That(checksum.Value, Is.EqualTo("v"));
            Assert.That(checksum.Algorithm, Is.EqualTo(ChecksumKind.SHA256));
        }

        private static InterchangeChecksum DeserializeFromJson(string json, ILoggerFactory loggerFactory = null)
        {
            var bytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(bytes, isFinalBlock: true, state: default);

            // Position the reader on StartObject (as required by the deserializer)
            Assert.That(reader.Read(), Is.True, "JSON did not yield any tokens.");
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject), "Reader not positioned on StartObject.");

            return InterchangeChecksumDeserializer.DeSerialize(ref reader, loggerFactory);
        }

        private static int IndexOf(byte[] haystack, byte[] needle)
        {
            if (needle.Length == 0) return 0;
            for (var i = 0; i <= haystack.Length - needle.Length; i++)
            {
                var match = true;
                for (var j = 0; j < needle.Length; j++)
                {
                    if (haystack[i + j] != needle[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return i;
            }
            return -1;
        }

        private static ReadOnlySequence<byte> CreateMultiSegmentSequence(byte[] data, int splitIndex)
        {
            if (splitIndex <= 0 || splitIndex >= data.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(splitIndex));
            }

            var firstMem = new ReadOnlyMemory<byte>(data, 0, splitIndex);
            var secondMem = new ReadOnlyMemory<byte>(data, splitIndex, data.Length - splitIndex);

            var first = new BufferSegment(firstMem);
            var last = first.Append(secondMem);

            return new ReadOnlySequence<byte>(first, 0, last, last.Memory.Length);
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
