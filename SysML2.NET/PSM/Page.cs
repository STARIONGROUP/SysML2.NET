// -------------------------------------------------------------------------------------------------
// <copyright file="Page.cs" company="Starion Group S.A.">
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
