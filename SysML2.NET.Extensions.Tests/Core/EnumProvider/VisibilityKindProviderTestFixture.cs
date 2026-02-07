// -------------------------------------------------------------------------------------------------
// <copyright file="VisibilityKindProviderTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions.Tests.Core.EnumProvider
{
    using System;
    using System.Buffers;

    using SysML2.NET.Extensions.Core;

    using NUnit.Framework;

    using SysML2.NET.Core.Root.Namespaces;

    [TestFixture]
    public class VisibilityKindProviderTestFixture
    {
        [Test]
        [TestCase("Private", VisibilityKind.Private)]
        [TestCase("Protected", VisibilityKind.Protected)]
        [TestCase("Public", VisibilityKind.Public)]
        public void Verify_that_enumvalue_can_be_parsed_from_string(string input, VisibilityKind expected)
        {
            Assert.That(VisibilityKindProvider.Parse(input), 
                Is.EqualTo(expected));
        }

        [Test]
        public void Verify_that_enumvalue_throws_exception_from_unknown_string()
        {
            Assert.That(() => VisibilityKindProvider.Parse("Starion"), Throws.ArgumentException);
        }

        [Test]
        [TestCase("private", VisibilityKind.Private)]
        [TestCase("protected", VisibilityKind.Protected)]
        [TestCase("public", VisibilityKind.Public)]
        public void Verify_that_enumvalue_can_be_parsed_from_bytes(string input, VisibilityKind expected)
        {
            ReadOnlySpan<byte> value = System.Text.Encoding.UTF8.GetBytes(input);

            Assert.That(VisibilityKindProvider.Parse(value),
                Is.EqualTo(expected));
        }

        [Test]
        public void Verify_that_enumvalue_throws_exception_from_unknown_bytes()
        {
            Assert.That(() => VisibilityKindProvider.Parse("Starion"u8), Throws.ArgumentException);
        }

        [TestCase(VisibilityKind.Private, "private")]
        [TestCase(VisibilityKind.Protected, "protected")]
        [TestCase(VisibilityKind.Public, "public")]
        public void Verify_that_ToUtf8LowerBytes_returns_expected_bytes(VisibilityKind kind, string expected)
        {
            ReadOnlySpan<byte> bytes = VisibilityKindProvider.ToUtf8LowerBytes(kind);

            Assert.That(bytes.SequenceEqual(System.Text.Encoding.UTF8.GetBytes(expected)), Is.True);
        }

        [Test]
        public void Verify_that_ToUtf8LowerBytes_throws_for_undefined_value()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = VisibilityKindProvider.ToUtf8LowerBytes((VisibilityKind)123456);
            });
        }

        [TestCase("private", VisibilityKind.Private)]
        [TestCase("protected", VisibilityKind.Protected)]
        [TestCase("public", VisibilityKind.Public)]
        public void Verify_that_Parse_ReadOnlySequence_single_segment_parses(string text, VisibilityKind expected)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            var seq = new ReadOnlySequence<byte>(bytes);

            var actual = VisibilityKindProvider.Parse(in seq);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("private", VisibilityKind.Private)]
        [TestCase("protected", VisibilityKind.Protected)]
        [TestCase("public", VisibilityKind.Public)]
        public void Verify_that_Parse_ReadOnlySequence_multi_segment_parses(string text, VisibilityKind expected)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);

            // Force a multi-segment ReadOnlySequence by chaining two segments.
            var split = Math.Max(1, bytes.Length / 2);
            var first = new BufferSegment(bytes, 0, split);
            var last = first.Append(bytes, split, bytes.Length - split);

            var seq = new ReadOnlySequence<byte>(first, 0, last, last.Memory.Length);

            var actual = VisibilityKindProvider.Parse(in seq);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("")]
        [TestCase("x")]
        [TestCase("privat")]     // off by 1
        [TestCase("PrivateX")]   // invalid
        [TestCase("internal")]   // unsupported
        [TestCase("protectedx")] // invalid (10 bytes)
        public void Verify_that_Parse_ReadOnlySequence_throws_for_invalid(string text)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            var seq = new ReadOnlySequence<byte>(bytes);

            Assert.Throws<ArgumentException>(() => VisibilityKindProvider.Parse(in seq));
        }

        /// <summary>
        /// Minimal helper to create a multi-segment ReadOnlySequence{byte} for tests.
        /// </summary>
        private sealed class BufferSegment : ReadOnlySequenceSegment<byte>
        {
            public BufferSegment(byte[] buffer, int offset, int count)
            {
                Memory = new ReadOnlyMemory<byte>(buffer, offset, count);
            }

            public BufferSegment Append(byte[] buffer, int offset, int count)
            {
                var next = new BufferSegment(buffer, offset, count)
                {
                    RunningIndex = RunningIndex + Memory.Length
                };

                Next = next;
                return next;
            }
        }
    }
}
