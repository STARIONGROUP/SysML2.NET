// ------------------------------------------------------------------------------------------------
// <copyright file="ChecksumFailureBehavior.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Kpar
{
    /// <summary>
    /// Defines how checksum mismatches are handled when checksum validation is enabled.
    /// </summary>
    public enum ChecksumFailureBehavior
    {
        /// <summary>
        /// Throws a <see cref="ChecksumValidationException"/> if one or more mismatches are detected.
        /// </summary>
        Throw = 0,

        /// <summary>
        /// Collects mismatches and exposes them to the caller without throwing.
        /// </summary>
        Collect = 1
    }
}
