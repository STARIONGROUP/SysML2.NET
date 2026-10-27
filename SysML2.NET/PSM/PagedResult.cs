// ------------------------------------------------------------------------------------------------
// <copyright file="PagedResult.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
