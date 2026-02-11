// -------------------------------------------------------------------------------------------------
// <copyright file="StreamExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extensions.Utilities
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Extensions.Utilities;

    /// <summary>
    /// Unit tests for <see cref="StreamExtensions"/>.
    /// </summary>
    [TestFixture]
    public sealed class StreamExtensionsTestFixture
    {
        [Test]
        public void Verify_that_ReadAllBytes_throws_on_null_stream()
        {
            Stream stream = null;

            Assert.That(() => stream.ReadAllBytes(),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("stream"));
        }

        [Test]
        public async Task Verify_that_ReadAllBytesAsync_throws_on_null_stream()
        {
            Stream stream = null;

            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await stream.ReadAllBytesAsync(CancellationToken.None));

            Assert.That(ex!.ParamName, Is.EqualTo("stream"));
        }

        [Test]
        public void Verify_that_ReadAllBytes_returns_empty_array_for_empty_stream()
        {
            using var ms = new MemoryStream(Array.Empty<byte>());

            var bytes = ms.ReadAllBytes();

            Assert.That(bytes, Is.Not.Null);
            Assert.That(bytes, Is.Empty);
            Assert.That(ms.Position, Is.EqualTo(ms.Length));
        }

        [Test]
        public async Task Verify_that_ReadAllBytesAsync_returns_empty_array_for_empty_stream()
        {
            using var ms = new MemoryStream(Array.Empty<byte>());

            var bytes = await ms.ReadAllBytesAsync();

            Assert.That(bytes, Is.Not.Null);
            Assert.That(bytes, Is.Empty);
            Assert.That(ms.Position, Is.EqualTo(ms.Length));
        }

        [Test]
        public void Verify_that_ReadAllBytes_reads_from_current_position_to_end()
        {
            var payload = CreateBytes(1024);
            using var ms = new MemoryStream(payload);

            // advance stream
            ms.Position = 100;

            var bytes = ms.ReadAllBytes();

            Assert.That(bytes, Is.EqualTo(payload.AsSpan(100).ToArray()));
            Assert.That(ms.Position, Is.EqualTo(ms.Length));
        }

        [Test]
        public async Task Verify_that_ReadAllBytesAsync_reads_from_current_position_to_end()
        {
            var payload = CreateBytes(1024);
            await using var ms = new MemoryStream(payload);

            ms.Position = 100;

            var bytes = await ms.ReadAllBytesAsync();

            Assert.That(bytes, Is.EqualTo(payload.AsSpan(100).ToArray()));
            Assert.That(ms.Position, Is.EqualTo(ms.Length));
        }

        [Test]
        public void Verify_that_ReadAllBytes_handles_buffer_growth()
        {
            // Larger than initialSize (32 KiB) to force at least one growth.
            var payload = CreateBytes(100 * 1024);
            using var ms = new MemoryStream(payload);

            var bytes = ms.ReadAllBytes();

            Assert.That(bytes, Is.EqualTo(payload));
            Assert.That(ms.Position, Is.EqualTo(ms.Length));
        }

        [Test]
        public async Task Verify_that_ReadAllBytesAsync_handles_buffer_growth()
        {
            var payload = CreateBytes(100 * 1024);
            await using var ms = new MemoryStream(payload);

            var bytes = await ms.ReadAllBytesAsync();

            Assert.That(bytes, Is.EqualTo(payload));
            Assert.That(ms.Position, Is.EqualTo(ms.Length));
        }

        [Test]
        public void Verify_that_ReadAllBytes_does_not_dispose_stream()
        {
            var payload = CreateBytes(1024);
            var ms = new MemoryStream(payload);

            _ = ms.ReadAllBytes();

            Assert.That(ms.CanRead, Is.True);
            Assert.That(ms.CanSeek, Is.True);

            ms.Dispose();
        }

        [Test]
        public async Task Verify_that_ReadAllBytesAsync_does_not_dispose_stream()
        {
            var payload = CreateBytes(1024);
            var ms = new MemoryStream(payload);

            _ = await ms.ReadAllBytesAsync();

            Assert.That(ms.CanRead, Is.True);
            Assert.That(ms.CanSeek, Is.True);

            ms.Dispose();
        }

        [Test]
        public void Verify_that_ReadAllBytesAsync_honors_cancellation()
        {
            // Use a stream that produces infinite data to ensure we hit cancellation deterministically.
            using var stream = new InfiniteReadStream();

            using var cts = new CancellationTokenSource();
            cts.Cancel();

            Assert.That(async () => await stream.ReadAllBytesAsync(cts.Token),
                Throws.InstanceOf<OperationCanceledException>());
        }

        private static byte[] CreateBytes(int length)
        {
            var bytes = new byte[length];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)(i & 0xFF);
            }

            return bytes;
        }

        /// <summary>
        /// A stream that always returns data on reads (until canceled).
        /// </summary>
        private sealed class InfiniteReadStream : Stream
        {
            public override bool CanRead => true;
            public override bool CanSeek => false;
            public override bool CanWrite => false;
            public override long Length => throw new NotSupportedException();
            public override long Position { get => 0; set => throw new NotSupportedException(); }
            public override void Flush() => throw new NotSupportedException();
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
            public override void SetLength(long value) => throw new NotSupportedException();
            public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

            public override int Read(byte[] buffer, int offset, int count)
            {
                // Produce some bytes without ever reaching end-of-stream.
                var n = Math.Min(count, 4096);
                Array.Clear(buffer, offset, n);
                return n;
            }

            public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var n = Math.Min(buffer.Length, 4096);
                buffer.Span.Slice(0, n).Clear();
                return new ValueTask<int>(n);
            }
        }
    }
}
