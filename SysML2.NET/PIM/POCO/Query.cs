// -------------------------------------------------------------------------------------------------
// <copyright file="Query.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM.POCO
{
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// <see cref="Query"/> is a subclass of <see cref="Record"/> that represents a precise and language-independent
    /// request for information  retrieval using the Systems Modeling API and Services.
    /// <see cref="Query"/> can be mapped to commonly used query languages, such as SQL, Gremlin, GraphQL, and SPARQL
    /// </summary>
    public class Query : Record
    {
        /// <summary>
        /// Gets or sets the owning <see cref="Project"/> of this <see cref="Query"/>
        /// </summary>
        public Project OwningProject { get; set; }

        /// <summary>
        /// Gets or sets the name of the Query
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// list of properties of Element or its subtypes that will be included for each Element object in the
        /// query response. Element is the root-metaclass in KerML. If no properties are specified, then all the
        /// properties will be included for each Element in the query response
        /// </summary>
        public List<string> Select { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the list of Element objects that define the scope for query execution. The default scope of a <see cref="Query"/> is
        /// the owning <see cref="Project"/>
        /// </summary>
        public List<IData> Scope { get; set; } = new List<IData>();

        /// <summary>
        /// represents the conditions that Elements in the query response must satisfy
        /// </summary>
        public Constraint Where { get; set; }

        /// <summary>
        /// Gets or sets a list of properties of Data (or its realizations) that are used for sorting the Data objects in the
        /// query response.The order of properties in the list governs the sorting order
        /// </summary>
        public List<string> OrderBy { get; set; } = new List<string>();
    }
}
