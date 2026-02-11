// -------------------------------------------------------------------------------------------------
// <copyright file="ChecksumService.cs" company="Starion Group S.A.">
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
    using System.Buffers;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Threading;
    using System.Threading.Tasks;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Digests;

    using SysML2.NET.Extensions.Utilities;
    using SysML2.NET.Kpar;
    using SysML2.NET.ModelInterchange;

    /// <summary>
    /// Provides checksum computation and validation utilities for <c>.kpar</c> archives.
    /// </summary>
    /// <remarks>
    /// This service:
    /// <list type="bullet">
    /// <item><description>Computes checksums for supported <see cref="ChecksumKind"/> values.</description></item>
    /// <item><description>Validates the <see cref="InterchangeProjectMetadata.Checksum"/> map against the actual ZIP entry contents.</description></item>
    /// <item><description>Uses BouncyCastle for most digest algorithms and a built-in implementation for <see cref="ChecksumKind.ADLER32"/>.</description></item>
    /// </list>
    /// <para>
    /// Algorithms that are not available in the current dependency set will raise <see cref="NotSupportedException"/>.
    /// </para>
    /// </remarks>
    public sealed class ChecksumService : IChecksumService
    {
        /// <summary>
        /// Validates all checksum entries in <paramref name="metadata"/> against the corresponding ZIP entries.
        /// </summary>
        /// <param name="zip">The open ZIP archive containing the <c>.kpar</c> entries.</param>
        /// <param name="metadata">The parsed <c>.meta.json</c> metadata.</param>
        /// <param name="behavior">
        /// Controls what happens when mismatches are detected. The default is <see cref="ChecksumFailureBehavior.Throw"/>.
        /// </param>
        /// <returns>
        /// A read-only list of detected mismatches. If <paramref name="behavior"/> is <see cref="ChecksumFailureBehavior.Throw"/>,
        /// this method throws instead of returning a non-empty list.
        /// </returns>
        public IReadOnlyList<ChecksumMismatch> Validate(ZipArchive zip, InterchangeProjectMetadata metadata, ChecksumFailureBehavior behavior = ChecksumFailureBehavior.Throw)
        {
            if (zip is null) throw new ArgumentNullException(nameof(zip));
            
            if (metadata is null) throw new ArgumentNullException(nameof(metadata));

            var checksums = metadata.Checksum;
            if (checksums is null || checksums.Count == 0)
            {
                return Array.Empty<ChecksumMismatch>();
            }

            var mismatches = new List<ChecksumMismatch>();

            foreach (var kvp in checksums)
            {
                var archivePath = kvp.Key?.NormalizeZipPath();
                var expected = kvp.Value;

                if (string.IsNullOrWhiteSpace(archivePath) || expected is null)
                {
                    continue;
                }

                var entry = zip.GetEntry(archivePath);
                if (entry is null)
                {
                    throw new FileNotFoundException($"ZIP entry '{archivePath}' not found for checksum validation.",
                        archivePath);
                }

                using var stream = entry.Open();
                var actualHex = ComputeHex(stream, expected.Algorithm);

                if (!HexEquals(actualHex, expected.Value))
                {
                    mismatches.Add(new ChecksumMismatch
                    {
                        IndexKey = kvp.Key,
                        Path = archivePath,
                        Algorithm = expected.Algorithm,
                        Expected =expected.Value,
                        Actual =actualHex 
                    });
                }
            }

            if (mismatches.Count > 0 && behavior == ChecksumFailureBehavior.Throw)
            {
                throw new ChecksumValidationException(mismatches);
            }

            return mismatches;
        }

        /// <summary>
        /// Asynchronously validates all checksum entries in <paramref name="metadata"/> against the corresponding ZIP entries.
        /// </summary>
        /// <param name="zip">The open ZIP archive containing the <c>.kpar</c> entries.</param>
        /// <param name="metadata">The parsed <c>.meta.json</c> metadata.</param>
        /// <param name="behavior">
        /// Controls what happens when mismatches are detected. The default is <see cref="ChecksumFailureBehavior.Throw"/>.
        /// </param>
        /// <param name="cancellationToken">A token used to cancel the operation.</param>
        /// <returns>
        /// A task that returns a read-only list of detected mismatches. If <paramref name="behavior"/> is <see cref="ChecksumFailureBehavior.Throw"/>,
        /// this method throws instead of returning a non-empty list.
        /// </returns>
        public async Task<IReadOnlyList<ChecksumMismatch>> ValidateAsync(ZipArchive zip, InterchangeProjectMetadata metadata, ChecksumFailureBehavior behavior = ChecksumFailureBehavior.Throw, CancellationToken cancellationToken = default)
        {
            if (zip is null) throw new ArgumentNullException(nameof(zip));
            
            if (metadata is null) throw new ArgumentNullException(nameof(metadata));

            var checksums = metadata.Checksum;
            if (checksums is null || checksums.Count == 0)
            {
                return Array.Empty<ChecksumMismatch>();
            }

            var mismatches = new List<ChecksumMismatch>();

            foreach (var kvp in checksums)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var archivePath = kvp.Key?.NormalizeZipPath();
                var expected = kvp.Value;

                if (string.IsNullOrWhiteSpace(archivePath) || expected is null)
                {
                    continue;
                }

                var entry = zip.GetEntry(archivePath);
                if (entry is null)
                {
                    throw new FileNotFoundException($"ZIP entry '{archivePath}' not found for checksum validation.",
                        archivePath);
                }

                await using var stream = entry.Open();
                var actualHex = await ComputeHexAsync(stream, expected.Algorithm, cancellationToken)
                    .ConfigureAwait(false);

                if (!HexEquals(actualHex, expected.Value))
                {
                    mismatches.Add(new ChecksumMismatch
                    {
                        IndexKey = kvp.Key,
                        Path = archivePath,
                        Algorithm = expected.Algorithm,
                        Expected =expected.Value,
                        Actual =actualHex 
                    });
                }
            }

            if (mismatches.Count > 0 && behavior == ChecksumFailureBehavior.Throw)
            {
                throw new ChecksumValidationException(mismatches);
            }

            return mismatches;
        }

        /// <summary>
        /// Computes the checksum over the provided <paramref name="stream"/> and returns a lowercase hex string.
        /// </summary>
        /// <param name="stream">The input stream to hash.</param>
        /// <param name="kind">The checksum algorithm to apply.</param>
        /// <returns>A lowercase hex string representing the computed checksum.</returns>
        private static string ComputeHex(Stream stream, ChecksumKind kind)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            if (kind == ChecksumKind.ADLER32)
            {
                var adler = Adler32.Compute(stream);
                return adler.ToString("x8", CultureInfo.InvariantCulture);
            }

            var digest = CreateDigest(kind);
            ComputeDigest(stream, digest);
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            return ToLowerHex(output);
        }

        /// <summary>
        /// Asynchronously computes the checksum over the provided <paramref name="stream"/> and returns a lowercase hex string.
        /// </summary>
        /// <param name="stream">The input stream to hash.</param>
        /// <param name="kind">The checksum algorithm to apply.</param>
        /// <param name="cancellationToken">A token used to cancel the operation.</param>
        /// <returns>A task returning a lowercase hex string representing the computed checksum.</returns>
        private static async Task<string> ComputeHexAsync(Stream stream, ChecksumKind kind, CancellationToken cancellationToken = default)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            if (kind == ChecksumKind.ADLER32)
            {
                var adler = await Adler32.ComputeAsync(stream, cancellationToken).ConfigureAwait(false);
                return adler.ToString("x8", CultureInfo.InvariantCulture);
            }

            var digest = CreateDigest(kind);
            await ComputeDigestAsync(stream, digest, cancellationToken).ConfigureAwait(false);
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            return ToLowerHex(output);
        }

        /// <summary>
        /// Creates a BouncyCastle <see cref="IDigest"/> instance for the specified <see cref="ChecksumKind"/>.
        /// </summary>
        /// <param name="kind">The checksum algorithm to instantiate.</param>
        /// <returns>
        /// A concrete <see cref="IDigest"/> implementation that can be fed with input bytes via
        /// <see cref="IDigest.BlockUpdate(byte[], int, int)"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This factory covers digest algorithms that are available in the referenced BouncyCastle package,
        /// and intentionally throws for algorithms that require an additional dependency (for example BLAKE3, MD6).
        /// </para>
        /// <para>
        /// Note that <see cref="ChecksumKind.ADLER32"/> is not a cryptographic digest and is typically handled
        /// outside BouncyCastle (for example with a dedicated Adler-32 implementation).
        /// </para>
        /// </remarks>
        private static IDigest CreateDigest(ChecksumKind kind)
        {
            return kind switch
            {
                ChecksumKind.SHA1 => new Sha1Digest(),
                ChecksumKind.SHA224 => new Sha224Digest(),
                ChecksumKind.SHA256 => new Sha256Digest(),
                ChecksumKind.SHA384 => new Sha384Digest(),

                ChecksumKind.SHA3256 => new Sha3Digest(256),
                ChecksumKind.SHA3384 => new Sha3Digest(384),
                ChecksumKind.SHA3512 => new Sha3Digest(512),

                // Blake2bDigest takes output size in bits.
                ChecksumKind.BLAKE2b256 => new Blake2bDigest(256),
                ChecksumKind.BLAKE2b384 => new Blake2bDigest(384),
                ChecksumKind.BLAKE2b512 => new Blake2bDigest(512),

                ChecksumKind.MD2 => new MD2Digest(),
                ChecksumKind.MD4 => new MD4Digest(),
                ChecksumKind.MD5 => new MD5Digest(),

                // Not available in BouncyCastle by default (and not provided here).
                ChecksumKind.BLAKE3 => throw new NotSupportedException(
                    "BLAKE3 is not supported by the current implementation. Add a BLAKE3 implementation and extend CreateDigest/ComputeHex accordingly."),
                ChecksumKind.MD6 => throw new NotSupportedException(
                    "MD6 is not supported by the current implementation. Add an MD6 implementation and extend CreateDigest/ComputeHex accordingly."),

                _ => throw new NotSupportedException($"Checksum algorithm '{kind}' is not supported.")
            };
        }

        /// <summary>
        /// Reads all bytes from <paramref name="stream"/> and feeds them into the provided <paramref name="digest"/>.
        /// </summary>
        /// <param name="stream">The readable input stream to hash from its current position to end-of-stream.</param>
        /// <param name="digest">The digest instance that will receive the input bytes.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="stream"/> or <paramref name="digest"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method does not reset the stream position. After completion, the stream is positioned at end-of-stream.
        /// </para>
        /// <para>
        /// A pooled buffer is used to reduce allocations. The buffer is returned to the pool even if an exception occurs.
        /// </para>
        /// </remarks>
        private static void ComputeDigest(Stream stream, IDigest digest)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(64 * 1024);
            try
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    digest.BlockUpdate(buffer, 0, read);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Asynchronously reads all bytes from <paramref name="stream"/> and feeds them into the provided <paramref name="digest"/>.
        /// </summary>
        /// <param name="stream">The readable input stream to hash from its current position to end-of-stream.</param>
        /// <param name="digest">The digest instance that will receive the input bytes.</param>
        /// <param name="cancellationToken">A token used to cancel the operation.</param>
        /// <returns>A task that completes when all bytes have been read and fed to the digest.</returns>
        /// <remarks>
        /// <para>
        /// This method does not reset the stream position. After completion, the stream is positioned at end-of-stream.
        /// </para>
        /// <para>
        /// A pooled buffer is used to reduce allocations. The buffer is returned to the pool even if an exception occurs.
        /// </para>
        /// </remarks>
        private static async Task ComputeDigestAsync(Stream stream, IDigest digest, CancellationToken cancellationToken)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(64 * 1024);
            try
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var read = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken).ConfigureAwait(false);
                    if (read <= 0) break;

                    digest.BlockUpdate(buffer, 0, read);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Compares two hexadecimal strings for equality, ignoring case and surrounding whitespace.
        /// </summary>
        /// <param name="a">The first hexadecimal string.</param>
        /// <param name="b">The second hexadecimal string.</param>
        /// <returns>
        /// <see langword="true"/> if both strings are non-null and represent the same sequence of hex characters
        /// (case-insensitive, trimmed); otherwise <see langword="false"/>.
        /// </returns>
        /// <remarks>
        /// This helper is intentionally permissive to tolerate differences in casing and incidental whitespace
        /// in descriptor files.
        /// </remarks>
        private static bool HexEquals(string a, string b)
        {
            if (a is null || b is null) return false;
            
            return string.Equals(a.Trim(), b.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Converts the provided bytes to a lowercase hexadecimal string without separators.
        /// </summary>
        /// <param name="bytes">The bytes to encode as hexadecimal.</param>
        /// <returns>
        /// A lowercase hexadecimal string whose length is <c>bytes.Length * 2</c>.
        /// </returns>
        /// <remarks>
        /// This method performs no allocations besides the resulting string and does not use culture-sensitive formatting.
        /// </remarks>
        private static string ToLowerHex(ReadOnlySpan<byte> bytes)
        {
            // 2 chars per byte.
            var chars = new char[bytes.Length * 2];
            var c = 0;

            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                chars[c++] = GetLowerHexNibble(b >> 4);
                chars[c++] = GetLowerHexNibble(b & 0xF);
            }

            return new string(chars);

            static char GetLowerHexNibble(int value)
                => (char)(value < 10 ? ('0' + value) : ('a' + (value - 10)));
        }
    }
}
