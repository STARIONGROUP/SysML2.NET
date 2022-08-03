// -------------------------------------------------------------------------------------------------
// <copyright file="IStateUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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
    /// A StateUsage is an ActionUsage that is nominally the Usage of a StateDefinition. However, other
    /// kinds of kernel Behaviors are also allowed as types, to permit use of Behaviors from the Kernel
    /// Library.A StateUsage (other than an ExhibitStateUsage owned by a PartDefinition or PartUsage) must
    /// subset, directly or indirectly, either the base StateUsage stateActions from the Systems model
    /// library, if it is not a composite feature, or the StateUsage substates inherited from its owner, if
    /// it is a composite feature.A StateUsage may be related to up to three of its ownedFeatures by
    /// StateBehaviorMembership Relationships, all of different kinds, corresponding to the entry, do and
    /// exit actions of the StateUsage.let general : Sequence(Type) = ownedGeneralization.general ingeneral
    /// ->    selectByKind(StateDefinition).isParallel->    forAll(p | p = isParallel) andgeneral ->   
    /// selectByKind(StateUsage).isParallel->    forAll(p | p = isParallel)
    /// </summary>
    public partial interface IStateUsage : IActionUsage
    {
        /// <summary>
        /// Whether the nestedStates of this StateDefinition are to all be performed in parallel. If true, none
        /// of the nestedStates may have any incoming or outgoing transitions. If false, only one nestedState
        /// may be performed at a time.
        /// </summary>
        bool IsParallel { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
