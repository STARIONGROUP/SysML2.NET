// -------------------------------------------------------------------------------------------------
// <copyright file="IEventOccurrenceUsage.cs" company="RHEA System S.A.">
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
    /// An EventOccurrenceUsage is an OccurrenceUsage that represents another OccurrenceUsage occurring as a
    /// suboccurrence of the containing occurrence of the EventOccurrenceUsage. Unless it is the
    /// EventOccurrenceUsage itself, the referenced OccurrenceUsage is related to the EventOccurrenceUsage
    /// by a ReferenceSubsetting Relationship.If the EventOccurrenceUsage is owned by an
    /// OccurrenceDefinition or OccurrenceUsage, then it also subsets the timeEnclosedOccurrences property
    /// of the Class Occurrence from the Kernel Semantic Library model Occurrences.eventOccurrence =    if
    /// ownedReferenceSubsetting = null then self    else if
    /// ownedReferenceSubsetting.referencedFeature.oclIsKindOf(OccurrenceUsage) then        
    /// ownedReferenceSubsetting.referencedFeature.oclAsType(OccurrenceUsage)    else null    endif
    /// endifownedReferenceSubsetting <> null implies   
    /// ownedReferenceSubsetting.referencedFeature.oclIsKindOf(OccurrenceUsage)owningType <> null
    /// and(owningType.oclIsKindOf(OccurrenceDefinition) or owningType.oclIsKindOf(OccurrenceUsage)) implies
    /// specializesFromLibrary('Occurrences::Occurrence::timeEnclosedOccurrences')isReference
    /// </summary>
    public partial interface IEventOccurrenceUsage : IOccurrenceUsage
    {
        /// <summary>
        /// Queries the derived property EventOccurrence
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        OccurrenceUsage QueryEventOccurrence();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
