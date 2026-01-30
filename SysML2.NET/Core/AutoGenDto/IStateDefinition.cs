// -------------------------------------------------------------------------------------------------
// <copyright file="IStateDefinition.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.DTO.Systems.Actions;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A StateDefinition is the Definition of the Behavior of a system or part of a system in a certain
    /// state condition.  A StateDefinition may be related to up to three of its ownedFeatures by
    /// StateBehaviorMembership Relationships, all of different kinds, corresponding to the entry, do and
    /// exit actions of the StateDefinition.
    /// </summary>
    [Class(xmiId: "Systems-States-StateDefinition", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IStateDefinition : IActionDefinition
    {
        /// <summary>
        /// The ActionUsage of this StateDefinition to be performed while in the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateDefinition by a
        /// StateSubactionMembership  with kind = do.
        /// </summary>
        [Property(xmiId: "Systems-States-StateDefinition-doAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? doAction { get; }

        /// <summary>
        /// The ActionUsage of this StateDefinition to be performed on entry to the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateDefinition by a
        /// StateSubactionMembership  with kind = entry.
        /// </summary>
        [Property(xmiId: "Systems-States-StateDefinition-entryAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? entryAction { get; }

        /// <summary>
        /// The ActionUsage of this StateDefinition to be performed on exit to the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateDefinition by a
        /// StateSubactionMembership  with kind = exit.
        /// </summary>
        [Property(xmiId: "Systems-States-StateDefinition-exitAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? exitAction { get; }

        /// <summary>
        /// Whether the ownedStates of this StateDefinition are to all be performed in parallel. If true, none
        /// of the ownedActions (which includes ownedStates) may have any incoming or outgoing Transitions. If
        /// false, only one ownedState may be performed at a time.
        /// </summary>
        [Property(xmiId: "Systems-States-StateDefinition-isParallel", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsParallel { get; set; }

        /// <summary>
        /// The StateUsages, which are actions in the StateDefinition, that specify the discrete states in the
        /// behavior defined by the StateDefinition.
        /// </summary>
        [Property(xmiId: "Systems-States-StateDefinition-state", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-Actions-ActionDefinition-action")]
        List<Guid> state { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
