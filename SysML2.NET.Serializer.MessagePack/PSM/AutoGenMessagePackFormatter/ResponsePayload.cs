// -------------------------------------------------------------------------------------------------
// <copyright file="ResponsePayload.cs" company="Starion Group S.A.">
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
    /// The <see cref="ResponsePayload"/> acts as envelope around the Systems Modeling API and Services
    /// response data transfer objects and is used as construct to transport them using MessagePack
    /// </summary>
    internal class ResponsePayload
    {
        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> at which the <see cref="ResponsePayload"/> was created
        /// </summary>
        internal DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the list of <see cref="Branch"/>.
        /// </summary>
        internal List<Branch> Branch { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="Commit"/>.
        /// </summary>
        internal List<Commit> Commit { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="CompositeConstraint"/>.
        /// </summary>
        internal List<CompositeConstraint> CompositeConstraint { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="DataDifference"/>.
        /// </summary>
        internal List<DataDifference> DataDifference { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="DataIdentity"/>.
        /// </summary>
        internal List<DataIdentity> DataIdentity { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="DataVersion"/>.
        /// </summary>
        internal List<DataVersion> DataVersion { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="Error"/>.
        /// </summary>
        internal List<Error> Error { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="ExternalData"/>.
        /// </summary>
        internal List<ExternalData> ExternalData { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="ExternalRelationship"/>.
        /// </summary>
        internal List<ExternalRelationship> ExternalRelationship { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="PrimitiveConstraint"/>.
        /// </summary>
        internal List<PrimitiveConstraint> PrimitiveConstraint { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="Project"/>.
        /// </summary>
        internal List<Project> Project { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="ProjectUsage"/>.
        /// </summary>
        internal List<ProjectUsage> ProjectUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="Query"/>.
        /// </summary>
        internal List<Query> Query { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="Tag"/>.
        /// </summary>
        internal List<Tag> Tag { get; set; } = [];

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
