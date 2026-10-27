// ------------------------------------------------------------------------------------------------
// <copyright file="JoinOperator.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM
{
    using SysML2.NET.PIM.DTO;

    /// <summary>
    /// Enumeration whose literals are logical operator for composing <see cref="Constraint"/>
    /// </summary>
    public enum JoinOperator
    {
        /// <summary>
        /// The logical AND join operator
        /// </summary>
        AND,

        /// <summary>
        /// The logical OR join operator
        /// </summary>
        OR
    }
}
