// -------------------------------------------------------------------------------------------------
// <copyright file="RequestPayload.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack.PSM
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// The <see cref="RequestPayload"/> acts as envelope around the Systems Modeling API and Services
    /// request data transfer objects and is used as construct to transport them using MessagePack
    /// </summary>
    internal class RequestPayload
    {
        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> at which the <see cref="RequestPayload"/> was created
        /// </summary>
        internal DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the list of <see cref="BranchRequest"/>.
        /// </summary>
        internal List<BranchRequest> BranchRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="CommitRequest"/>.
        /// </summary>
        internal List<CommitRequest> CommitRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="CompositeConstraintRequest"/>.
        /// </summary>
        internal List<CompositeConstraintRequest> CompositeConstraintRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="DataIdentityRequest"/>.
        /// </summary>
        internal List<DataIdentityRequest> DataIdentityRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="DataVersionRequest"/>.
        /// </summary>
        internal List<DataVersionRequest> DataVersionRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="ExternalDataRequest"/>.
        /// </summary>
        internal List<ExternalDataRequest> ExternalDataRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="ExternalRelationshipRequest"/>.
        /// </summary>
        internal List<ExternalRelationshipRequest> ExternalRelationshipRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="PrimitiveConstraintRequest"/>.
        /// </summary>
        internal List<PrimitiveConstraintRequest> PrimitiveConstraintRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="ProjectRequest"/>.
        /// </summary>
        internal List<ProjectRequest> ProjectRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="ProjectUsageRequest"/>.
        /// </summary>
        internal List<ProjectUsageRequest> ProjectUsageRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="QueryRequest"/>.
        /// </summary>
        internal List<QueryRequest> QueryRequest { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="TagRequest"/>.
        /// </summary>
        internal List<TagRequest> TagRequest { get; set; } = [];

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
