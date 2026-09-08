// -------------------------------------------------------------------------------------------------
// <copyright file="Branch.cs" company="Starion Group S.A.">
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
    /// Data transfer object that represents the Branch schema of the Systems Modeling API and Services
    /// </summary>
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class Branch : IResponse, SysML2.NET.Common.IIdentified
    {
        /// <summary>
        /// The value of the type discriminator of the Branch schema
        /// </summary>
        public const string Type = "Branch";

        /// <summary>
        /// Gets or sets the unique identifier of the Branch
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the alias of the Branch
        /// </summary>
        public List<string> Alias { get; set; } = [];

        /// <summary>
        /// Gets or sets the created of the Branch
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the deleted of the Branch
        /// </summary>
        public DateTime? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the description of the Branch
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the referenced Commit
        /// </summary>
        public Guid? Head { get; set; }

        /// <summary>
        /// Gets or sets the name of the Branch
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the referenced Project
        /// </summary>
        public Guid OwningProject { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the referenced Commit
        /// </summary>
        public Guid ReferencedCommit { get; set; }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
