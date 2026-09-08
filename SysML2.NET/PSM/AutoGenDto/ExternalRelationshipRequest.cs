// -------------------------------------------------------------------------------------------------
// <copyright file="ExternalRelationshipRequest.cs" company="Starion Group S.A.">
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
    /// Data transfer object that represents the ExternalRelationshipRequest schema of the Systems Modeling API and Services
    /// </summary>
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class ExternalRelationshipRequest : IDataRequest, IRequest
    {
        /// <summary>
        /// The value of the type discriminator of the ExternalRelationshipRequest schema
        /// </summary>
        public const string Type = "ExternalRelationship";

        /// <summary>
        /// Gets or sets the unique identifier of the ExternalRelationshipRequest
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the referenced Element
        /// </summary>
        public Guid? ElementEnd { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the referenced ExternalData
        /// </summary>
        public Guid? ExternalDataEnd { get; set; }

        /// <summary>
        /// Gets or sets the language of the ExternalRelationshipRequest
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the specification of the ExternalRelationshipRequest
        /// </summary>
        public string Specification { get; set; }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
