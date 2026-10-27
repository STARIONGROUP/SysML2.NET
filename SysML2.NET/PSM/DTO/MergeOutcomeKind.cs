// ------------------------------------------------------------------------------------------------
// <copyright file="MergeOutcomeKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Enumeration of the alternatives that a <see cref="MergeOutcome"/> may hold
    /// </summary>
    public enum MergeOutcomeKind
    {
        /// <summary>
        /// The merge succeeded and the outcome holds the resulting commit
        /// </summary>
        Merged,

        /// <summary>
        /// The merge failed and the outcome holds the data identities that conflict
        /// </summary>
        Conflicted
    }
}
