// -------------------------------------------------------------------------------------------------
// <copyright file="IActionDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Actions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Kernel.Behaviors;
    using SysML2.NET.Core.DTO.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An ActionDefinition is a Definition that is also a Behavior that defines an Action performed by a
    /// system or part of a system.
    /// </summary>
    [Class(xmiId: "Systems-Actions-ActionDefinition", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IActionDefinition : IOccurrenceDefinition, IBehavior
    {
        /// <summary>
        /// The ActionUsages that are steps in this ActionDefinition, which define the actions that specify the
        /// behavior of the ActionDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Actions-ActionDefinition-action", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Behavior-step")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-usage")]
        List<Guid> action { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
