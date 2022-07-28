// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementDefinition.cs" company="RHEA System S.A.">
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
    /// A RequirementDefinition is a ConstraintDefinition that defines a requirement as a constraint that is
    /// used in the context of a specification of a that a valid solution must satisfy. The specification is
    /// relative to a specified subject, possibly in collaboration with one or more external actors.A
    /// RequirementDefinition must subclass, directly or indirectly, the base RequirementDefinition
    /// RequirementCheck from the Systems model library.
    /// </summary>
    public partial interface IRequirementDefinition : IConstraintDefinition
    {
        /// <summary>
        /// An optional modeler-specified identifier for this RequirementDefinition (used, e.g., to link it to
        /// an original requirement text in some source document), derived as the modeledId for the
        /// RequirementDefinition.
        /// </summary>
        string ReqId { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
