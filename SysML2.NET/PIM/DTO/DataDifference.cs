// -------------------------------------------------------------------------------------------------
// <copyright file="DataDifference.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.PIM.DTO
{
    using System;

    /// <summary>
    /// Represents a single difference between two compared versions of the same <see cref="DataIdentity"/>
    /// </summary>
    public class DataDifference
    {
        /// <summary>
        /// Gets or sets the <see cref="DataVersion"/> taken as the baseline of the comparison, or <c>null</c> when the data was added
        /// </summary>
        public Guid? BaseData { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DataVersion"/> compared against the baseline, or <c>null</c> when the data was deleted
        /// </summary>
        public Guid? CompareData { get; set; }
    }
}
