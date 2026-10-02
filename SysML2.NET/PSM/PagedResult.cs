// -------------------------------------------------------------------------------------------------
// <copyright file="PagedResult.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    /// <summary>
    /// One page of records together with the cursors that address the pages either side of it.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the records that the page carries.
    /// </typeparam>
    /// <remarks>
    /// A service applies the <see cref="QueryParameters"/> of a request when it queries its store, so the
    /// cursors are known there and not by the caller. Several record types carry no timestamp of their own,
    /// which makes a <see cref="Page"/> underivable from the records alone.
    /// </remarks>
    public class PagedResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        public PagedResult()
        {
            this.Records = [];
        }

        /// <summary>
        /// The records of this page, in the order the service returned them.
        /// </summary>
        public IReadOnlyList<T> Records { get; set; }

        /// <summary>
        /// The cursor of the page succeeding this one, or <c>null</c> when this is the last page.
        /// </summary>
        public Page? Next { get; set; }

        /// <summary>
        /// The cursor of the page preceding this one, or <c>null</c> when this is the first page.
        /// </summary>
        public Page? Previous { get; set; }
    }
}
