// ------------------------------------------------------------------------------------------------
// <copyright file="Direction.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM
{
    /// <summary>
    /// Defines possible direction
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// At the same time in and out
        /// </summary>
        both,

        /// <summary>
        /// To be use for on an income direction
        /// </summary>
        @in,

        /// <summary>
        /// To be use for on an outcome direction
        /// </summary>
        @out
    }
}