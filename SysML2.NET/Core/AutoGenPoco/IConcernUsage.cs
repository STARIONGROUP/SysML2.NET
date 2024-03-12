﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IConcernUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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
    /// A ConcernUsage is a Usage of a ConcernDefinition. The ownedStakeholder features of the ConcernUsage
    /// shall all subset the ConcernCheck::concernedStakeholders feature. If the ConcernUsage is an
    /// ownedFeature of a StakeholderDefinition or StakeholderUsage, then the ConcernUsage shall have an
    /// ownedStakeholder feature that is bound to the self feature of its
    /// owner.specializesFromLibrary('Requirements::concernChecks')owningFeatureMembership <> null
    /// andowningFeatureMembership.oclIsKindOf(FramedConcernMembership) implies   
    /// specializesFromLibrary('Requirements::RequirementCheck::concerns')
    /// </summary>
    public partial interface IConcernUsage : IRequirementUsage
    {
        /// <summary>
        /// Queries the derived property ConcernDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ConcernDefinition QueryConcernDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
