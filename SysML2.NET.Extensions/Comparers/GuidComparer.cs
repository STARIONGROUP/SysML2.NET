// ------------------------------------------------------------------------------------------------
// <copyright file="GuidComparer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Comparers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// an IComparer to compare <see cref="Guid"/>s
    /// </summary>
    public class GuidComparer : IComparer<Guid>
    {
        /// <summary>
        /// Compares this instance to a specified object or Guid and returns an indication of their relative values
        /// </summary>
        /// <param name="x">the first <see cref="Guid"/> to compare</param>
        /// <param name="y">the second <see cref="Guid"/> to compare</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and value.
        /// </returns>
        public int Compare(Guid x, Guid y)
        {
            return x.CompareTo(y);
        }
    }
}