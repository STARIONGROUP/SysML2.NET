// -------------------------------------------------------------------------------------------------
// <copyright file="GuidComparer.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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