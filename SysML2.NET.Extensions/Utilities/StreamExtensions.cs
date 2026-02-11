// -------------------------------------------------------------------------------------------------
// <copyright file="StreamExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions.Utilities
{
    using System;
    using System.Buffers;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides high-performance extension methods for reading
    /// <see cref="Stream"/> instances into byte arrays.
    /// </summary>
    /// <remarks>
    /// These methods:
    /// <list type="bullet">
    /// <item><description>Read from the current stream position to end-of-stream.</description></item>
    /// <item><description>Use pooled buffers to minimize intermediate allocations.</description></item>
    /// <item><description>Return a newly allocated, right-sized byte array.</description></item>
    /// </list>
    /// The input stream is not disposed.
    /// </remarks>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads all remaining bytes from the stream into a new byte array.
        /// </summary>
        /// <param name="stream">The readable stream.</param>
        /// <returns>
        /// A byte array whose length exactly matches the number of bytes
        /// read from the stream.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="stream"/> is <see langword="null"/>.
        /// </exception>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            const int initialSize = 32 * 1024;

            var rented = ArrayPool<byte>.Shared.Rent(initialSize);

            try
            {
                var total = 0;

                while (true)
                {
                    var read = stream.Read(rented, total, rented.Length - total);
                    if (read == 0) break;

                    total += read;

                    if (total == rented.Length)
                    {
                        var newBuf = ArrayPool<byte>.Shared.Rent(rented.Length * 2);
                        Buffer.BlockCopy(rented, 0, newBuf, 0, total);

                        ArrayPool<byte>.Shared.Return(rented, clearArray: true);
                        rented = newBuf;
                    }
                }

                var result = new byte[total];
                Buffer.BlockCopy(rented, 0, result, 0, total);

                return result;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(rented, clearArray: true);
            }
        }

        /// <summary>
        /// Asynchronously reads all remaining bytes from the stream into a new byte array.
        /// </summary>
        /// <param name="stream">The readable stream.</param>
        /// <param name="cancellationToken">A token used to cancel the operation.</param>
        /// <returns>
        /// A task producing a byte array whose length exactly matches
        /// the number of bytes read.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="stream"/> is <see langword="null"/>.
        /// </exception>
        public static async Task<byte[]> ReadAllBytesAsync(this Stream stream, CancellationToken cancellationToken = default)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            const int initialSize = 32 * 1024;

            var rented = ArrayPool<byte>.Shared.Rent(initialSize);

            try
            {
                var total = 0;

                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var read = await stream
                        .ReadAsync(rented.AsMemory(total, rented.Length - total), cancellationToken)
                        .ConfigureAwait(false);

                    if (read == 0) break;

                    total += read;

                    if (total == rented.Length)
                    {
                        var newBuf = ArrayPool<byte>.Shared.Rent(rented.Length * 2);
                        Buffer.BlockCopy(rented, 0, newBuf, 0, total);

                        ArrayPool<byte>.Shared.Return(rented, clearArray: true);
                        rented = newBuf;
                    }
                }

                var result = new byte[total];
                Buffer.BlockCopy(rented, 0, result, 0, total);

                return result;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(rented, clearArray: true);
            }
        }
    }
}
