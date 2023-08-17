// -------------------------------------------------------------------------------------------------
// <copyright file="Query.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="Query"/> is a subclass of <see cref="Record"/> that represents a precise and language-independent
    /// request for information  retrieval using the Systems Modeling API and Services.
    /// <see cref="Query"/> can be mapped to commonly used query languages, such as SQL, Gremlin, GraphQL, and SPARQL
    /// </summary>
    public class Query : Record
    {
        /// <summary>
        /// Gets or sets the unique identifier of the owning <see cref="Project"/>
        /// </summary>
        public Guid OwningProject { get; set; }

        /// <summary>
        /// list of properties of Element or its subtypes that will be included for each Element object in the
        /// query response. Element is the root-metaclass in KerML. If no properties are specified, then all the
        /// properties will be included for each Element in the query response
        /// </summary>
        public List<string> Select { get; set; }

        /// <summary>
        /// represents the conditions that Elements in the query response must satisfy
        /// </summary>
        public Constraint Where { get; set; }
    }
}
