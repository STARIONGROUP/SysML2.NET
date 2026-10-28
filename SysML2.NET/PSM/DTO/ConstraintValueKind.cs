// ------------------------------------------------------------------------------------------------
// <copyright file="ConstraintValueKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Enumeration of the alternatives that a <see cref="ConstraintValue"/> may hold
    /// </summary>
    public enum ConstraintValueKind
    {
        /// <summary>
        /// The value is null
        /// </summary>
        Null = 0,

        /// <summary>
        /// The value is a boolean
        /// </summary>
        Boolean,

        /// <summary>
        /// The value is a number
        /// </summary>
        Number,

        /// <summary>
        /// The value is a string
        /// </summary>
        String,

        /// <summary>
        /// The value is the unique identifier of a referenced element
        /// </summary>
        Guid
    }
}
