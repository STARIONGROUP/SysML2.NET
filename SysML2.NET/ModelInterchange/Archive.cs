// ------------------------------------------------------------------------------------------------
// <copyright file="Archive.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.ModelInterchange
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the logical contents of a <c>.kpar</c> archive.
    /// </summary>
    public class Archive
    {
        /// <summary>
        /// Gets or sets the interchange project descriptor (serialized to <c>.project.json</c>).
        /// </summary>
        public InterchangeProject Project { get; set; }

        /// <summary>
        /// Gets or sets the interchange metadata (serialized to <c>.meta.json</c>).
        /// </summary>
        public InterchangeProjectMetadata Metadata { get; set; }
        
        /// <summary>
        /// Gets the Path of the kpar that has been read
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets the model interchange files contained in the archive.
        /// </summary>
        /// <remarks>
        /// Each entry uses an archive-relative path (forward slashes).
        /// </remarks>
        public IReadOnlyList<ModelEntry> Models { get; set; } = Array.Empty<ModelEntry>();
        
        /// <summary>
        /// Gets the checksum validation mismatches detected while reading a <c>.kpar</c> archive.
        /// </summary>
        public IReadOnlyList<ChecksumMismatch> ChecksumMismatches { get; set; } = Array.Empty<ChecksumMismatch>();
    }
}
