// -------------------------------------------------------------------------------------------------
// <copyright file="IOccurrenceUsage.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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
    /// An OccurrenceUsage is a Usage whose types are all Classes. Nominally, if a type is an
    /// OccurrenceDefinition, an OccurrenceUsage is a Usage of that OccurrenceDefinition within a system.
    /// However, other types of Kernel Classes are also allowed, to permit use of Classes from the Kernel
    /// Model Libraries.individualDefinition =    let individualDefinitions :
    /// OrderedSet(OccurrenceDefinition) =         occurrenceDefinition->           
    /// selectByKind(OccurrenceDefinition)->            select(isIndividual) in    if
    /// individualDefinitions->isEmpty() then null    else individualDefinitions->first() endifisIndividual
    /// implies individualDefinition <> nullspecializesFromLibrary('Occurrences::occurrences')isComposite
    /// andowningType <> null and(owningType.oclIsKindOf(Class) or owningType.oclIsKindOf(OccurrenceUsage)
    /// or owningType.oclIsKindOf(Feature) and    owningType.oclAsType(Feature).type->       
    /// exists(oclIsKind(Class))) implies   
    /// specializesFromLibrary('Occurrences::Occurrence::suboccurrences')occurrenceDefinition->   
    /// selectByKind(OccurrenceDefinition)->    select(isIndividual).size() <= 1portionKind <> null implies 
    /// occurrenceDefinition->forAll(occ |         featuringType->exists(specializes(occ)))
    /// </summary>
    public partial interface IOccurrenceUsage : IUsage
    {
        /// <summary>
        /// Whether this OccurrenceUsage represents the usage of the specific individual (or portion of it)
        /// represented by its individualDefinition.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsIndividual { get; set; }

        /// <summary>
        /// The kind of (temporal) portion of the life of the occurrenceDefinition represented by this
        /// OccurrenceUsage, if it is so restricted.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        PortionKind? PortionKind { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
