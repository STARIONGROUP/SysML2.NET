// -------------------------------------------------------------------------------------------------
// <copyright file="IDependency.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A Dependency is a Relationship that indicates that one or more client Elements require one more
    /// supplier Elements for their complete specification. In general, this means that a change to one of
    /// the supplier Elements may necessitate a change to, or re-specification of, the client Elements.Note
    /// that a Dependency is entirely a model-level Relationship, without instance-level semantics.
    /// </summary>
    public partial interface IDependency : IRelationship
    {
        /// <summary>
        /// </summary>
        List<Guid> Client { get; set; }

        /// <summary>
        /// </summary>
        List<Guid> Supplier { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
