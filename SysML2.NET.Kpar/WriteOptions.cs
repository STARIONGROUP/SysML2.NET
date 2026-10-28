// ------------------------------------------------------------------------------------------------
// <copyright file="WriteOptions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Kpar
{
    /// <summary>
    /// Options controlling <c>.kpar</c> writing behavior.
    /// </summary>
    public sealed class WriteOptions
    {
        /// <summary>
        /// If true, the writer normalizes paths to use forward slashes and strips leading slashes.
        /// </summary>
        public bool NormalizePaths { get; set; } = true;

        /// <summary>
        /// If true, the writer fails if any descriptor would be written outside the archive root.
        /// </summary>
        public bool EnforceDescriptorsAtRoot { get; set; } = true;

        /// <summary>
        /// Optional: compute and populate metadata checksums for some or all model files.
        /// </summary>
        public bool ComputeChecksums { get; set; } = false;

        /// <summary>
        /// Optional checksum algorithm name (e.g., <c>SHA256</c>) if <see cref="ComputeChecksums"/> is enabled.
        /// </summary>
        public string ChecksumAlgorithm { get; set; }
    }
}
