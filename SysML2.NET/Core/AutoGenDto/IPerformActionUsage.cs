// -------------------------------------------------------------------------------------------------
// <copyright file="IPerformActionUsage.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO.Systems.Actions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A PerformActionUsage is an ActionUsage that represents the performance of an ActionUsage. Unless it
    /// is the PerformActionUsage itself, the ActionUsage to be performed is related to the
    /// PerformActionUsage by a ReferenceSubsetting relationship. A PerformActionUsage is also an
    /// EventOccurrenceUsage, with its performedAction as the eventOccurrence.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1565503273042_472885_33822", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IPerformActionUsage : IActionUsage, IEventOccurrenceUsage
    {
        /// <summary>
        /// The ActionUsage to be performed by this PerformedActionUsage. It is the eventOccurrence of the
        /// PerformActionUsage considered as an EventOccurrenceUsage, which must be an ActionUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1567740791820_867719_18017", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1622831790393_676695_195")]
        Guid performedAction { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
