// ------------------------------------------------------------------------------------------------
// <copyright file="ChangeType.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM
{
    /// <summary>
    /// Defines changes type
    /// </summary>
    public enum ChangeType
    {
        /// <summary>
        /// To be used in case of creation
        /// </summary>
        CREATED,

        /// <summary>
        /// To be used in case of deletion
        /// </summary>
        DELETED,

        /// <summary>
        /// To be used in case of update
        /// </summary>
        UPDATED,
    }
}