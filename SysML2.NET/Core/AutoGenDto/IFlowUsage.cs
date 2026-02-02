// -------------------------------------------------------------------------------------------------
// <copyright file="IFlowUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Flows
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Kernel.Interactions;
    using SysML2.NET.Core.DTO.Systems.Actions;
    using SysML2.NET.Core.DTO.Systems.Connections;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FlowUsage is an ActionUsage that is also a ConnectorAsUsage and a KerML Flow.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1624054663096_771284_1274", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFlowUsage : IConnectorAsUsage, IFlow, IActionUsage
    {
        /// <summary>
        /// The Interactions that are the types of this FlowUsage. Nominally, these are FlowDefinitions, but
        /// other kinds of Kernel Interactions are also allowed, to permit use of Interactions from the Kernel
        /// Model Libraries.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1661892878973_977062_185", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1661900477937_518125_727")]
        List<Guid> flowDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
