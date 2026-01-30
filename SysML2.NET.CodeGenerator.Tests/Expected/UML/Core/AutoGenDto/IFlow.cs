// -------------------------------------------------------------------------------------------------
// <copyright file="IFlow.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Kernel.Interactions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Kernel.Behaviors;
    using SysML2.NET.Core.DTO.Kernel.Connectors;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An Flow is a Step that represents the transfer of values from one Feature to another. Flows can take
    /// non-zero time to complete.
    /// </summary>
    [Class(xmiId: "Kernel-Interactions-Flow", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFlow : IConnector, IStep
    {
        /// <summary>
        /// The connectorEnds of this Flow that are FlowEnds.
        /// </summary>
        [Property(xmiId: "Kernel-Interactions-Flow-flowEnd", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 2, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Connectors-Connector-connectorEnd")]
        List<Guid> flowEnd { get; }

        /// <summary>
        /// The Interactions that type this Flow. Interactions are both Associations and Behaviors, which can
        /// type Connectors and Steps, respectively.
        /// </summary>
        [Property(xmiId: "Kernel-Interactions-Flow-interaction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Kernel-Connectors-Connector-association")]
        [RedefinedProperty(propertyName: "Kernel-Behaviors-Step-behavior")]
        List<Guid> interaction { get; }

        /// <summary>
        /// The ownedFeature of the Flow that is a PayloadFeature (if any).
        /// </summary>
        [Property(xmiId: "Kernel-Interactions-Flow-payloadFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        Guid? payloadFeature { get; }

        /// <summary>
        /// The type of values transferred, which is the type of the payloadFeature of the Flow.
        /// </summary>
        [Property(xmiId: "Kernel-Interactions-Flow-payloadType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        List<Guid> payloadType { get; }

        /// <summary>
        /// The Feature that provides the items carried by the Flow. It must be a feature of the source of the
        /// Flow.
        /// </summary>
        [Property(xmiId: "Kernel-Interactions-Flow-sourceOutputFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        Guid? sourceOutputFeature { get; }

        /// <summary>
        /// The Feature that receives the values carried by the Flow. It must be a feature of the target of the
        /// Flow.
        /// </summary>
        [Property(xmiId: "Kernel-Interactions-Flow-targetInputFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        Guid? targetInputFeature { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
