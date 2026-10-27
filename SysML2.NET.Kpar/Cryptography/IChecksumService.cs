// ------------------------------------------------------------------------------------------------
// <copyright file="IChecksumService.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Kpar.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.IO.Compression;
    using System.Threading;
    using System.Threading.Tasks;
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
    public interface IChecksumService
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
        IReadOnlyList<ChecksumMismatch> Validate(ZipArchive zip, InterchangeProjectMetadata metadata, ChecksumFailureBehavior behavior = ChecksumFailureBehavior.Throw);
        
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
        Task<IReadOnlyList<ChecksumMismatch>> ValidateAsync(ZipArchive zip, InterchangeProjectMetadata metadata, ChecksumFailureBehavior behavior = ChecksumFailureBehavior.Throw, CancellationToken cancellationToken = default);
    }
}
