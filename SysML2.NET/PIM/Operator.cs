// ------------------------------------------------------------------------------------------------
// <copyright file="Operator.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM
{
    /// <summary>
    /// Enumeration whose literals are mathematical operators
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// To assert that it is an instance of a specific type
        /// </summary>
        instanceOf,

        /// <summary>
        /// To assert that it is equal to 
        /// </summary>
        equalto,

        /// <summary>
        /// To assert that it is less than
        /// </summary>
        lessthan,

        /// <summary>
        /// To assert that it is less than or equal to 
        /// </summary>
        lessthanorequalto,

        /// <summary>
        /// To assert that it is greater than 
        /// </summary>
        greaterthan,

        /// <summary>
        /// To assert that it is greater than or equal to 
        /// </summary>
        greaterthanorequalto,

        /// <summary>
        /// To assert that it is in
        /// </summary>
        @in
    }
}
