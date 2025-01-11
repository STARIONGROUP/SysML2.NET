// -------------------------------------------------------------------------------------------------
// <copyright file="ExternalData.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Common;

    /// <summary>
    /// ExternalData is a realization of Data, and represents a resource external to a given tool or
    /// repository. ExternalData is defined only for the purpose of defining an ExternalRelationship
    /// </summary>
    public class ExternalData : IData
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the string representation of the IRI of the resource represented by the ExternalData
        /// </summary>
        public string ResourceIdentifier { get; set; }
    }
}
