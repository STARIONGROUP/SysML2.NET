// -------------------------------------------------------------------------------------------------
// <copyright file="IActionUsage.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Kernel.Behaviors;
    using SysML2.NET.Core.DTO.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An ActionUsage is a Usage that is also a Step, and, so, is typed by a Behavior. Nominally, if the
    /// type is an ActionDefinition, an ActionUsage is a Usage of that ActionDefinition within a system.
    /// However, other kinds of kernel Behaviors are also allowed, to permit use of Behaviors from the
    /// Kernel Model Libraries.
    /// </summary>
    [Class(xmiId: "Systems-Actions-ActionUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IActionUsage : IStep, IOccurrenceUsage
    {
        /// <summary>
        /// The Behaviors that are the types of this ActionUsage. Nominally, these would be ActionDefinitions,
        /// but other kinds of Kernel Behaviors are also allowed, to permit use of Behaviors from the Kernel
        /// Model Libraries.
        /// </summary>
        [Property(xmiId: "Systems-Actions-ActionUsage-actionDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Kernel-Behaviors-Step-behavior")]
        [RedefinedProperty(propertyName: "Systems-Occurrences-OccurrenceUsage-occurrenceDefinition")]
        List<Guid> actionDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
