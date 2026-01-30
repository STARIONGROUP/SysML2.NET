// -------------------------------------------------------------------------------------------------
// <copyright file="IOccurrenceUsage.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Systems.DefinitionAndUsage;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An OccurrenceUsage is a Usage whose types are all Classes. Nominally, if a type is an
    /// OccurrenceDefinition, an OccurrenceUsage is a Usage of that OccurrenceDefinition within a system.
    /// However, other types of Kernel Classes are also allowed, to permit use of Classes from the Kernel
    /// Model Libraries.
    /// </summary>
    [Class(xmiId: "Systems-Occurrences-OccurrenceUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IOccurrenceUsage : IUsage
    {
        /// <summary>
        /// The at most one occurrenceDefinition that has isIndividual = true.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceUsage-individualDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-Occurrences-OccurrenceUsage-occurrenceDefinition")]
        Guid? individualDefinition { get; }

        /// <summary>
        /// Whether this OccurrenceUsage represents the usage of the specific individual represented by its
        /// individualDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceUsage-isIndividual", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsIndividual { get; set; }

        /// <summary>
        /// The Classes that are the types of this OccurrenceUsage. Nominally, these are OccurrenceDefinitions,
        /// but other kinds of kernel Classes are also allowed, to permit use of Classes from the Kernel Model
        /// Libraries.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceUsage-occurrenceDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-definition")]
        List<Guid> occurrenceDefinition { get; }

        /// <summary>
        /// The kind of temporal portion (time slice or snapshot) is represented by this OccurrenceUsage. If
        /// portionKind is not null, then the owningType of the OccurrenceUsage must be non-null, and the
        /// OccurrenceUsage represents portions of the featuring instance of the owningType.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceUsage-portionKind", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        PortionKind? PortionKind { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
