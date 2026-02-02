// -------------------------------------------------------------------------------------------------
// <copyright file="IAllocationDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Allocations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Systems.Connections;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An AllocationDefinition is a ConnectionDefinition that specifies that some or all of the
    /// responsibility to realize the intent of the source is allocated to the target instances. Such
    /// allocations define mappings across the various structures and hierarchies of a system model, perhaps
    /// as a precursor to more rigorous specifications and implementations. An AllocationDefinition can
    /// itself be refined using nested allocations that give a finer-grained decomposition of the containing
    /// allocation mapping.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1611430566467_608282_906", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAllocationDefinition : IConnectionDefinition
    {
        /// <summary>
        /// The AllocationUsages that refine the allocation mapping defined by this AllocationDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611430644481_402036_964", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        List<Guid> allocation { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
