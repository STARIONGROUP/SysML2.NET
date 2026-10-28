// ------------------------------------------------------------------------------------------------
// <copyright file="InterchangeChecksum.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.ModelInterchange
{
    /// <summary>
    /// Represents a checksum entry for a model interchange file.
    /// </summary>
    public sealed class InterchangeChecksum
    {
        /// <summary>
        /// Gets or sets the checksum value computed for the file.
        /// </summary>
        /// <remarks>
        /// Mandatory. Hexadecimal-encoded string.
        /// </remarks>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the algorithm used to compute the checksum.
        /// </summary>
        /// <remarks>
        /// Mandatory. Must match one of the supported algorithms defined by <see cref="ChecksumKind"/>.
        /// </remarks>
        public ChecksumKind Algorithm { get; set; }
    }
}
