// -------------------------------------------------------------------------------------------------
// <copyright file="IStateUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.States
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Actions;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A StateUsage is an ActionUsage that is nominally the Usage of a StateDefinition. However, other
    /// kinds of kernel Behaviors are also allowed as types, to permit use of Behaviors                     
    ///   A StateUsage may be related to up to three of its ownedFeatures by StateSubactionMembership
    /// Relationships, all of different kinds, corresponding to the entry, do and exit actions of the
    /// StateUsage.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1575587557729_586912_651", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IStateUsage : IActionUsage
    {
        /// <summary>
        /// The ActionUsage of this StateUsage to be performed while in the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateUsage by a StateSubactionMembership
        /// with kind = do.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1582976255473_203238_644", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? doAction { get; }

        /// <summary>
        /// The ActionUsage of this StateUsage to be performed on entry to the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateUsage by a StateSubactionMembership
        /// with kind = entry.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1582976239200_979652_605", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? entryAction { get; }

        /// <summary>
        /// The ActionUsage of this StateUsage to be performed on exit to the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateUsage by a StateSubactionMembership
        /// with kind = exit.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1582976283940_998741_691", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? exitAction { get; }

        /// <summary>
        /// Whether the nestedStates of this StateUsage are to all be performed in parallel. If true, none of
        /// the nestedActions (which include nestedStates) may have any incoming or outgoing Transitions. If
        /// false, only one nestedState may be performed at a time.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624025713025_548712_37708", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsParallel { get; set; }

        /// <summary>
        /// The Behaviors that are the types of this StateUsage. Nominally, these would be StateDefinitions, but
        /// kernel Behaviors are also allowed, to permit use of Behaviors from the Kernel Model Libraries.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575588456737_49200_1438", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1565500905804_589845_30779")]
        List<Guid> stateDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
