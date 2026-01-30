// -------------------------------------------------------------------------------------------------
// <copyright file="IEventOccurrenceUsage.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.DTO.Systems.Occurrences
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An EventOccurrenceUsage is an OccurrenceUsage that represents another OccurrenceUsage occurring as a
    /// suboccurrence of the containing occurrence of the EventOccurrenceUsage. Unless it is the
    /// EventOccurrenceUsage itself, the referenced OccurrenceUsage is related to the EventOccurrenceUsage
    /// by a ReferenceSubsetting Relationship.  If the EventOccurrenceUsage is owned by an
    /// OccurrenceDefinition or OccurrenceUsage, then it also subsets the timeEnclosedOccurrences property
    /// of the Class Occurrence from the Kernel Semantic Library model Occurrences.
    /// </summary>
    [Class(xmiId: "Systems-Occurrences-EventOccurrenceUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IEventOccurrenceUsage : IOccurrenceUsage
    {
        /// <summary>
        /// The OccurrenceUsage referenced as an event by this EventOccurrenceUsage. It is the referenceFeature
        /// of the ownedReferenceSubsetting for the EventOccurrenceUsage, if there is one, and, otherwise, the
        /// EventOccurrenceUsage itself.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-EventOccurrenceUsage-eventOccurrence", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid eventOccurrence { get; }

        /// <summary>
        /// Always true for an EventOccurrenceUsage.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-EventOccurrenceUsage-isReference", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [RedefinedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-isReference")]
        new bool isReference { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
