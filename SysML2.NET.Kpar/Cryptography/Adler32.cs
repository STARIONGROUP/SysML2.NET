// -------------------------------------------------------------------------------------------------
// <copyright file="Adler32.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Kpar.Cryptography
{
    using System;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// Provides an implementation of the Adler-32 checksum algorithm as defined in RFC 1950.
    /// </summary>
    /// <remarks>
    /// Adler-32 is a non-cryptographic checksum algorithm primarily used in zlib.
    /// It is fast but not collision-resistant and MUST NOT be used for security-sensitive integrity validation.
    /// </remarks>
    public static class Adler32
    {
        private const uint ModAdler = 65521;

        /// <summary>
        /// Computes the Adler-32 checksum for the specified byte span.
        /// </summary>
        /// <param name="data">The input data.</param>
        /// <returns>The computed Adler-32 checksum.</returns>
        public static uint Compute(ReadOnlySpan<byte> data)
        {
            uint a = 1;
            uint b = 0;

            foreach (var t in data)
            {
                a = (a + t) % ModAdler;
                b = (b + a) % ModAdler;
            }

            return (b << 16) | a;
        }

        /// <summary>
        /// Computes the Adler-32 checksum for the specified stream.
        /// </summary>
        /// <param name="stream">The input stream. Must be readable.</param>
        /// <returns>The computed Adler-32 checksum.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        public static uint Compute(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            uint a = 1;
            uint b = 0;

            Span<byte> buffer = stackalloc byte[8192];

            int read;
            while ((read = stream.Read(buffer)) > 0)
            {
                for (int i = 0; i < read; i++)
                {
                    a = (a + buffer[i]) % ModAdler;
                    b = (b + a) % ModAdler;
                }
            }

            return (b << 16) | a;
        }

        /// <summary>
        /// Computes the Adler-32 checksum for the specified stream asynchronously.
        /// </summary>
        /// <param name="stream">The input stream. Must be readable.</param>
        /// <param name="cancellationToken">A token used to cancel the operation.</param>
        /// <returns>The computed Adler-32 checksum.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        public static async System.Threading.Tasks.Task<uint> ComputeAsync(Stream stream, CancellationToken cancellationToken )
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            uint a = 1;
            uint b = 0;

            byte[] buffer = new byte[8192];

            int read;
            while ((read = await stream.ReadAsync(buffer).ConfigureAwait(false)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                for (int i = 0; i < read; i++)
                {
                    a = (a + buffer[i]) % ModAdler;
                    b = (b + a) % ModAdler;
                }
            }

            return (b << 16) | a;
        }

        /// <summary>
        /// Converts the checksum value to a lowercase hexadecimal string (8 characters).
        /// </summary>
        /// <param name="checksum">The checksum value.</param>
        /// <returns>Lowercase hexadecimal string representation.</returns>
        public static string ToHexString(uint checksum)
        {
            return checksum.ToString("x8");
        }
    }
}
