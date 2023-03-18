// -------------------------------------------------------------------------------------------------
// <copyright file="IViewpointDefinition.cs" company="RHEA System S.A.">
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
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ViewpointDefinition is a RequirementDefinition that specifies one or more stakeholder concerns
    /// that are to be satisfied by creating a view of a model.viewpointStakeholder =
    /// framedConcern.featureMemberhsip->    selectByKind(StakeholderMembership).   
    /// ownedStakeholderParameterspecializesFromLibrary('Views::Viewpoint')
    /// </summary>
    public partial interface IViewpointDefinition : IRequirementDefinition
    {
        /// <summary>
        /// Queries the derived property ViewpointStakeholder
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PartUsage> QueryViewpointStakeholder();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
