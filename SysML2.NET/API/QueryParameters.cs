// -------------------------------------------------------------------------------------------------
// <copyright file="QueryParameters.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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

namespace SysML2.NET.API
{
	using System;
	using System.Collections.Generic;
    using System.Linq;

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
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static QueryParameters FromString()
        {


            throw new NotImplementedException();
        }
    }

    public struct Page
    {
        public int Epoch { get; set; }

        public Guid Identifier { get; set; }

        public override string ToString()
        {
            return $"{this.Epoch}|{this.Identifier}";
        }
    }

}
