// -------------------------------------------------------------------------------------------------
// <copyright file="IFlow.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Types;

    using SysML2.NET.Decorators;

    /// <summary>
    /// An Flow is a Step that represents the transfer of values from one Feature to another. Flows can take
    /// non-zero time to complete.
    /// </summary>
    [Class(xmiId: "_18_5_3_b9102da_1536869417406_861526_17744", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFlow : IConnector, IStep
    {
        /// <summary>
        /// The connectorEnds of this Flow that are FlowEnds.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1563219311176_506548_20966", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 2, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1556735067666_827798_21922")]
        List<Guid> FlowEnd { get; }

        /// <summary>
        /// The Interactions that type this Flow. Interactions are both Associations and Behaviors, which can
        /// type Connectors and Steps, respectively.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1661900477937_518125_727", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674983_471497_43284")]
        [RedefinedProperty(propertyName: "_18_5_3_b9102da_1536346315176_954314_17388")]
        List<Guid> Interaction { get; }

        /// <summary>
        /// The ownedFeature of the Flow that is a PayloadFeature (if any).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1563219424870_347345_21142", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        Guid? PayloadFeature { get; }

        /// <summary>
        /// The type of values transferred, which is the type of the payloadFeature of the Flow.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1536870569046_1672_18020", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        List<Guid> PayloadType { get; }

        /// <summary>
        /// The Feature that provides the items carried by the Flow. It must be a feature of the source of the
        /// Flow.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1536870707078_57525_18088", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        Guid? SourceOutputFeature { get; }

        /// <summary>
        /// The Feature that receives the values carried by the Flow. It must be a feature of the target of the
        /// Flow.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1536870573474_966268_18041", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        Guid? TargetInputFeature { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
