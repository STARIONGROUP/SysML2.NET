// -------------------------------------------------------------------------------------------------
// <copyright file="IPartUsage.cs" company="RHEA System S.A.">
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
    /// A PartUsage is a usage of a PartDefinition to represent a system or a part of a system. At least one
    /// of the itemDefinitions of the PartUsage must be a PartDefinition.A PartUsage must subset, directly
    /// or indirectly, the base PartUsage parts from the Systems Model
    /// Library.itemDefinition->selectByKind(PartDefinition)partDefinition->notEmpty()specializesFromLibrary('Parts::parts')isComposite
    /// and owningType <> null and(owningType.oclIsKindOf(ItemDefinition) or
    /// owningType.oclIsKindOf(ItemUsage)) implies   
    /// specializesFromLibrary('Items::Item::subparts')owningFeatureMembership <> null
    /// andowningFeatureMembership.oclIsKindOf(ActorMembership) implies    if
    /// owningType.oclIsKindOf(RequirementDefinition) or        owningType.oclIsKindOf(RequirementUsage)   
    /// then specializesFromLibrary('Requirements::RequirementCheck::actors')    else
    /// specializesFromLibrary('Cases::Case::actors')owningFeatureMembership <> null
    /// andowningFeatureMembership.oclIsKindOf(StakeholderMembership) implies   
    /// specializesFromLibrary('Requirements::RequirementCheck::stakeholders')
    /// </summary>
    public partial interface IPartUsage : IItemUsage
    {
        /// <summary>
        /// Queries the derived property PartDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PartDefinition> QueryPartDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
