﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementConstraintMembership.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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
    /// A RequirementConstraintMembership is a FeatureMembership for an assumed or required ConstraintUsage
    /// of a RequirementDefinition or RequirementUsage.referencedConstraint =    let referencedFeature :
    /// Feature =         ownedConstraint.referencedFeatureTarget() in    if referencedFeature = null then
    /// ownedConstraint    else if referencedFeature.oclIsKindOf(ConstraintUsage) then       
    /// refrencedFeature.oclAsType(ConstraintUsage)    else null    endif
    /// endifowningType.oclIsKindOf(RequirementDefinition)
    /// orowningType.oclIsKindOf(RequirementUsage)ownedConstraint.isComposite
    /// </summary>
    public partial interface IRequirementConstraintMembership : IFeatureMembership
    {
        /// <summary>
        /// Whether the RequirementConstraintMembership is for an assumed or required ConstraintUsage.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        RequirementConstraintKind Kind { get; set; }

        /// <summary>
        /// Queries the derived property OwnedConstraint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ConstraintUsage QueryOwnedConstraint();

        /// <summary>
        /// Queries the derived property ReferencedConstraint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ConstraintUsage QueryReferencedConstraint();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
