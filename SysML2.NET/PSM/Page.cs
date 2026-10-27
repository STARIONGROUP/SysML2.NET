// ------------------------------------------------------------------------------------------------
// <copyright file="Page.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM
{
    using System;

    /// <summary>
    /// Defines the structure of the page for the pagination process
    /// </summary>
    public struct Page
    {
        /// <summary>
        /// the epoch time
        /// </summary>
        public int Epoch { get; set; }

        /// <summary>
        /// The page identifier
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Returns a string that represents the <see cref="Page"/>
        /// </summary>
        /// <returns>
        /// a string representation
        /// </returns>
        public override string ToString()
        {
            return $"{this.Epoch}|{this.Identifier}";
        }
    }
}
