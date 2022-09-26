// -------------------------------------------------------------------------------------------------
// <copyright file="IExhibitStateUsage.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// An ExhibitStateUsage is a StateUsage that represents the exhibiting of a StateUsage. Unless it is
    /// the StateUsage itself, the StateUsage to be exhibited is related to the ExhibitStateUsage by a
    /// ReferenceSubsetting Relationship. An ExhibitStateUsage is also a PerformActionUsage, with its
    /// exhibitedState as the performedAction.If the ExhibitStateUsage is owned by a PartDefinition or
    /// PartUsage, then it also subsets the StateUsage Part::exhibitedStates from the Systems model library.
    /// </summary>
    public partial interface IExhibitStateUsage : IStateUsage, IPerformActionUsage
    {
        /// <summary>
        /// Queries the derived property ExhibitedState
        /// </summary>
        StateUsage QueryExhibitedState();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
