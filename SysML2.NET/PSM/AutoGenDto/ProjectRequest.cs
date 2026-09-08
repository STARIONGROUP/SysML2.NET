// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectRequest.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.PSM.Enumerations;

    /// <summary>
    /// Data transfer object that represents the ProjectRequest schema of the Systems Modeling API and Services
    /// </summary>
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class ProjectRequest : IRequest
    {
        /// <summary>
        /// The value of the type discriminator of the ProjectRequest schema
        /// </summary>
        public const string Type = "Project";

        /// <summary>
        /// Gets or sets the alias of the ProjectRequest
        /// </summary>
        public List<string> Alias { get; set; } = [];

        /// <summary>
        /// Gets or sets the unique identifier of the referenced Branch
        /// </summary>
        public Guid? DefaultBranch { get; set; }

        /// <summary>
        /// Gets or sets the description of the ProjectRequest
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the ProjectRequest
        /// </summary>
        public string Name { get; set; }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
