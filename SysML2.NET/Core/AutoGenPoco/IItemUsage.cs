// -------------------------------------------------------------------------------------------------
// <copyright file="IItemUsage.cs" company="Starion Group S.A.">
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
    /// An ItemUsage is a ItemUsage whose definition is a Structure. Nominally, if the definition is an
    /// ItemDefinition, an ItemUsage is a ItemUsage of that ItemDefinition within a system. However, other
    /// kinds of Kernel Structures are also allowed, to permit use of Structures from the Kernel Model
    /// Libraries.itemDefinition =
    /// occurrenceDefinition->selectByKind(ItemDefinition)specializesFromLibrary('Items::items')isComposite
    /// and owningType <> null and(owningType.oclIsKindOf(ItemDefinition) or
    /// owningType.oclIsKindOf(ItemUsage)) implies    specializesFromLibrary('Items::Item::subitem')
    /// </summary>
    public partial interface IItemUsage : IOccurrenceUsage
    {
        /// <summary>
        /// Queries the derived property ItemDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Structure> QueryItemDefinition();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
