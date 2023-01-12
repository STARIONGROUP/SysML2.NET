// -------------------------------------------------------------------------------------------------
// <copyright file="IStateDefinition.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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

    /// <summary>
    /// A StateDefinition is the Definition of the Behavior of a system or part of a system in a certain
    /// state condition.A State Definition must subclass, directly or indirectly, the base StateDefinition
    /// StateAction from the Systems model library.A StateDefinition may be related to up to three of its
    /// ownedFeatures by StateBehaviorMembership Relationships, all of different kinds, corresponding to the
    /// entry, do and exit actions of the StateDefinition.ownedGeneralization.general->   
    /// selectByKind(StateDefinition).isParallel->    forAll(p | p = isParallel)
    /// </summary>
    public partial interface IStateDefinition : IActionDefinition
    {
        /// <summary>
        /// Whether the ownedStates of this StateDefinition are to all be performed in parallel. If true, none
        /// of the ownedStates may have any incoming or outgoing transitions. If false, only one ownedState may
        /// be performed at a time.
        /// </summary>
        bool IsParallel { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
