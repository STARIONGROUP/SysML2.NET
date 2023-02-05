// -------------------------------------------------------------------------------------------------
// <copyright file="ICaseDefinition.cs" company="RHEA System S.A.">
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
    /// A CaseDefinition is a CalculationDefinition for a process, often involving collecting evidence or
    /// data, relative to a subject, possibly involving the collaboration of one or more other actors,
    /// producing a result that meets an objective.A CaseDefinition must subclass, directly or indirectly,
    /// the base CaseDefinition Case from the Systems model library.
    /// </summary>
    public partial interface ICaseDefinition : ICalculationDefinition
    {
        /// <summary>
        /// Queries the derived property ActorParameter
        /// </summary>
        List<PartUsage> QueryActorParameter();

        /// <summary>
        /// Queries the derived property ObjectiveRequirement
        /// </summary>
        RequirementUsage QueryObjectiveRequirement();

        /// <summary>
        /// Queries the derived property SubjectParameter
        /// </summary>
        Usage QuerySubjectParameter();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
