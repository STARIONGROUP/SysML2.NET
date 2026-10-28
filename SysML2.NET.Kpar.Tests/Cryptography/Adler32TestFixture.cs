// ------------------------------------------------------------------------------------------------
// <copyright file="Adler32TestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
