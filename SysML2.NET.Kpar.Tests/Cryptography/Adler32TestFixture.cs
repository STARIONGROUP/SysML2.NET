// -------------------------------------------------------------------------------------------------
// <copyright file="Adler32TestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Kpar.Tests.Cryptography
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Kpar.Cryptography;

    [TestFixture]
    public class Adler32TestFixture
    {
        [Test]
        public void Verify_that_adler32_for_empty_input_is_00000001()
        {
            var checksum = Adler32.Compute(ReadOnlySpan<byte>.Empty);

            Assert.That(checksum, Is.EqualTo(0x00000001u));
            Assert.That(Adler32.ToHexString(checksum), Is.EqualTo("00000001"));
        }

        [Test]
        public void Verify_that_adler32_for_hello_is_known_value()
        {
            var data = Encoding.ASCII.GetBytes("Hello");

            var checksum = Adler32.Compute(data);

            Assert.That(Adler32.ToHexString(checksum), Is.EqualTo("058c01f5"));
        }

        [Test]
        public void Verify_that_adler32_for_wikipedia_test_vector_is_known_value()
        {
            var data = Encoding.ASCII.GetBytes("Wikipedia");

            var checksum = Adler32.Compute(data);

            Assert.That(checksum, Is.EqualTo(0x11E60398u));
            Assert.That(Adler32.ToHexString(checksum), Is.EqualTo("11e60398"));
        }

        [Test]
        public void Verify_that_stream_and_span_computations_match()
        {
            var data = Encoding.UTF8.GetBytes("The quick brown fox jumps over the lazy dog");
            var spanChecksum = Adler32.Compute(data);

            using var ms = new MemoryStream(data);
            var streamChecksum = Adler32.Compute(ms);

            Assert.That(streamChecksum, Is.EqualTo(spanChecksum));
        }

        [Test]
        public async Task Verify_that_stream_async_and_span_computations_match()
        {
            var cts = new CancellationTokenSource();
            
            var data = Encoding.UTF8.GetBytes("The quick brown fox jumps over the lazy dog");
            var spanChecksum = Adler32.Compute(data);

            await using var ms = new MemoryStream(data);
            var asyncChecksum = await Adler32.ComputeAsync(ms, cts.Token).ConfigureAwait(false);

            Assert.That(asyncChecksum, Is.EqualTo(spanChecksum));
        }

        [Test]
        public void Verify_that_adler32_is_deterministic()
        {
            var data = Encoding.UTF8.GetBytes("Determinism matters.");

            var c1 = Adler32.Compute(data);
            var c2 = Adler32.Compute(data);

            Assert.That(c2, Is.EqualTo(c1));
        }
    }
}
