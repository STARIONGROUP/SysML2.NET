﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IViewpointUsage.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ViewpointUsage is a Usage of a ViewpointDefinition.viewpointStakeholder =
    /// framedConcern.featureMemberhsip->    selectByKind(StakeholderMembership).   
    /// ownedStakeholderParameterspecializesFromLibrary('Views::viewpoints')isComposite and owningType <>
    /// null and(owningType.oclIsKindOf(ViewDefinition) or owningType.oclIsKindOf(ViewUsage)) implies   
    /// specializesFromLibrary('Views::View::viewpointSatisfactions')
    /// </summary>
    public partial interface IViewpointUsage : IRequirementUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
