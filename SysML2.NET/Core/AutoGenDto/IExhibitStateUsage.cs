// -------------------------------------------------------------------------------------------------
// <copyright file="IExhibitStateUsage.cs" company="Starion Group S.A.">
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
    /// An ExhibitStateUsage is a StateUsage that represents the exhibiting of a StateUsage. Unless it is
    /// the StateUsage itself, the StateUsage to be exhibited is related to the ExhibitStateUsage by a
    /// ReferenceSubsetting Relationship. An ExhibitStateUsage is also a PerformActionUsage, with its
    /// exhibitedState as the performedAction.
    /// </summary>
    [Class(xmiId: "Systems-States-ExhibitStateUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IExhibitStateUsage : IPerformActionUsage, IStateUsage
    {
        /// <summary>
        /// The StateUsage to be exhibited by the ExhibitStateUsage. It is the performedAction of the
        /// ExhibitStateUsage considered as a PerformActionUsage, which must be a StateUsage.
        /// </summary>
        [Property(xmiId: "Systems-States-ExhibitStateUsage-exhibitedState", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Actions-PerformActionUsage-performedAction")]
        Guid exhibitedState { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
