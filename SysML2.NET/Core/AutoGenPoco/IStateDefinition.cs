// -------------------------------------------------------------------------------------------------
// <copyright file="IStateDefinition.cs" company="Starion Group S.A.">
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
    /// A StateDefinition is the Definition of the Behavior of a system or part of a system in a certain
    /// state condition.A StateDefinition may be related to up to three of its ownedFeatures by
    /// StateBehaviorMembership Relationships, all of different kinds, corresponding to the entry, do and
    /// exit actions of the StateDefinition.specializesFromLibrary('States::StateAction')ownedMembership->  
    ///  selectByKind(StateSubactionMembership)->    isUnique(kind)state =
    /// action->selectByKind(StateUsage)doAction =    let doMemberships : Sequence(StateSubactionMembership)
    /// =        ownedMembership->            selectByKind(StateSubactionMembership)->           
    /// select(kind = StateSubactionKind::do) in    if doMemberships->isEmpty() then null    else
    /// doMemberships->at(1)    endifentryAction =    let entryMemberships :
    /// Sequence(StateSubactionMembership) =        ownedMembership->           
    /// selectByKind(StateSubactionMembership)->            select(kind = StateSubactionKind::entry) in   
    /// if entryMemberships->isEmpty() then null    else entryMemberships->at(1)    endifisParallel implies 
    ///   ownedAction.incomingTransition->isEmpty() and   
    /// ownedAction.outgoingTransition->isEmpty()exitAction =     let exitMemberships :
    /// Sequence(StateSubactionMembership) =        ownedMembership->           
    /// selectByKind(StateSubactionMembership)->            select(kind = StateSubactionKind::exit) in    if
    /// exitMemberships->isEmpty() then null    else exitMemberships->at(1)    endif
    /// </summary>
    public partial interface IStateDefinition : IActionDefinition
    {
        /// <summary>
        /// Queries the derived property DoAction
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ActionUsage QueryDoAction();

        /// <summary>
        /// Queries the derived property EntryAction
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ActionUsage QueryEntryAction();

        /// <summary>
        /// Queries the derived property ExitAction
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ActionUsage QueryExitAction();

        /// <summary>
        /// Whether the ownedStates of this StateDefinition are to all be performed in parallel. If true, none
        /// of the ownedActions (which includes ownedStates) may have any incoming or outgoing Transitions. If
        /// false, only one ownedState may be performed at a time.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsParallel { get; set; }

        /// <summary>
        /// Queries the derived property State
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<StateUsage> QueryState();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
