// ------------------------------------------------------------------------------------------------
// <copyright file="ReadOptions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Kpar
{
    using System;

    /// <summary>
    /// Options controlling <c>.kpar</c> reading behavior.
    /// </summary>
    public sealed class ReadOptions
    {
        /// <summary>
        /// If true, the reader validates that <c>.project.json</c> and <c>.meta.json</c> exist at archive root and are unique.
        /// </summary>
        public bool ValidateRequiredDescriptors { get; set; } = true;

        /// <summary>
        /// If true, the reader ensures the <c>index</c> entries in metadata resolve to existing model files.
        /// </summary>
        public bool ValidateIndexPaths { get; set; } = true;
        
        /// <summary>
        /// If true, model file checksums declared in <c>.meta.json</c>
        /// are computed and validated.
        /// </summary>
        public bool ValidateChecksums { get; set; } = false;
        
        /// <summary>
        /// Controls how checksum mismatches are handled when
        /// <see cref="ValidateChecksums"/> is enabled.
        /// </summary>
        /// <remarks>
        /// Default behavior is <see cref="ChecksumFailureBehavior.Throw"/>,
        /// causing archive opening to fail immediately on integrity violation.
        /// </remarks>
        public ChecksumFailureBehavior ChecksumFailureBehavior { get; set; } = ChecksumFailureBehavior.Throw;
    }
}
