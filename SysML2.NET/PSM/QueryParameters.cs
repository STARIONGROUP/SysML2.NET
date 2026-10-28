// ------------------------------------------------------------------------------------------------
// <copyright file="QueryParameters.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provide object structure for parameters than could be defined inside a queyr
    /// </summary>
    public class QueryParameters
    {
        /// <summary>
        /// specifies the maximum number of records that will be returned per page in the response
        /// </summary>
        public int PageSize { get; set; } = 0;

        /// <summary>
        /// specifies the URL of the page succeeding the page being requested
        /// </summary>
        /// <remarks>
        /// format: epoch|GUID, where epoch is Unix epoch: DateTime(1970, 1, 1, 0, 0, 0, 0)
        /// </remarks>
        public Page? PageAfter { get; set; } = null;

        /// <summary>
        /// specifies the URL of a page preceding the page being requested
        /// </summary>
        /// <remarks>
        /// format: epoch|GUID, where epoch is Unix epoch: DateTime(1970, 1, 1, 0, 0, 0, 0)
        /// </remarks>
        public Page? PageBefore { get; set; } = null;

        /// <summary>
        /// Returns a string that represents the <see cref="QueryParameters"/>
        /// </summary>
        /// <returns>
        /// a string representation
        /// </returns>
        public override string ToString()
        {
            var queryParameters = new List<string>();

            if (this.PageSize > 0)
            {
                queryParameters.Add($"page[size]={this.PageSize}");
            }

            if (this.PageAfter != null)
            {
                queryParameters.Add($"page[after]={this.PageAfter}");
            }

            if (this.PageBefore != null)
            {
                queryParameters.Add($"page[before]={this.PageBefore}");
            }

            if (!queryParameters.Any())
            {
                return string.Empty;
            }

            return "?" + string.Join("&", queryParameters);
        }

        /// <summary>
        /// Creates an instance
        /// </summary>
        /// <returns>A <see cref="QueryParameters"/></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static QueryParameters FromString()
        {
            throw new NotImplementedException();
        }
    }
}
