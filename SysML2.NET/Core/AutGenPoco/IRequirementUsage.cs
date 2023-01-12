// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementUsage.cs" company="RHEA System S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// A RequirementUsage is a Usage of a RequirementDefinition.A RequirementUsage must subset, directly or
    /// indirectly, the base RequirementUsage requirementChecks from the Systems model library.
    /// </summary>
    public partial interface IRequirementUsage : IConstraintUsage
    {
        /// <summary>
        /// Queries the derived property ActorParameter
        /// </summary>
        List<PartUsage> QueryActorParameter();

        /// <summary>
        /// Queries the derived property AssumedConstraint
        /// </summary>
        List<ConstraintUsage> QueryAssumedConstraint();

        /// <summary>
        /// Queries the derived property FramedConcern
        /// </summary>
        List<ConcernUsage> QueryFramedConcern();

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementUsage (used, e.g., to link it to an
        /// original requirement text in some source document), derived as the modeledId for the
        /// RequirementUsage.
        /// </summary>
        string ReqId { get; set; }

        /// <summary>
        /// Queries the derived property RequiredConstraint
        /// </summary>
        List<ConstraintUsage> QueryRequiredConstraint();

        /// <summary>
        /// Queries the derived property RequirementDefinition
        /// </summary>
        RequirementDefinition QueryRequirementDefinition();

        /// <summary>
        /// Queries the derived property StakeholderParameter
        /// </summary>
        List<PartUsage> QueryStakeholderParameter();

        /// <summary>
        /// Queries the derived property SubjectParameter
        /// </summary>
        Usage QuerySubjectParameter();

        /// <summary>
        /// Queries the derived property Text
        /// </summary>
        List<string> QueryText();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
