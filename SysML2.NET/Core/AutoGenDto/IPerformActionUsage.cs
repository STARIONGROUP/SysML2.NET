// -------------------------------------------------------------------------------------------------
// <copyright file="IPerformActionUsage.cs" company="RHEA System S.A.">
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
    using SysML2.NET.Decorators;

    /// <summary>
    /// A PerformActionUsage is an ActionUsage that represents the performance of an ActionUsage. Unless it
    /// is the PerformActionUsage itself, the ActionUsage to be performed is related to the
    /// PerformActionUsage by a ReferenceSubsetting relationship. A PerformActionUsage is also an
    /// EventOccurrenceUsage, with its performedAction as the eventOccurrence.ownedReferenceSubsetting <>
    /// null implies    ownedReferenceSubsetting.referencedFeature.oclIsKindOf(ActionUsage)owningType <>
    /// null and(owningType.oclIsKindOf(PartDefinition) or owningType.oclIsKindOf(PartUsage)) implies   
    /// specializesFromLibrary('Parts::Part::performedActions')
    /// </summary>
    public partial interface IPerformActionUsage : IActionUsage, IEventOccurrenceUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
