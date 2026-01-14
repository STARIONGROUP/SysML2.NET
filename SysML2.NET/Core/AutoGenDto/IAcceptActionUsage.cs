// -------------------------------------------------------------------------------------------------
// <copyright file="IAcceptActionUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Actions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An AcceptActionUsage is an ActionUsage that specifies the acceptance of an incomingTransfer from the
    /// Occurrence given by the result of its receiverArgument Expression. (If no receiverArgument is
    /// provided, the default is the this context of the AcceptActionUsage.) The payload of the accepted
    /// Transfer is output on its payloadParameter. Which Transfers may be accepted is determined by
    /// conformance to the typing and (potentially) binding of the payloadParameter.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1565503089035_106795_33475", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAcceptActionUsage : IActionUsage
    {
        /// <summary>
        /// An Expression whose result is bound to the payload parameter of this AcceptActionUsage. If provided,
        /// the AcceptActionUsage will only accept a Transfer with exactly this payload.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1642710978429_81558_4948", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? payloadArgument { get; }

        /// <summary>
        /// The nestedReference of this AcceptActionUsage that redefines the payload output parameter of the
        /// base AcceptActionUsage AcceptAction from the Systems Model Library.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1642701018287_478584_4462", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591477541360_47573_933")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        Guid payloadParameter { get; }

        /// <summary>
        /// An Expression whose result is bound to the receiver input parameter of this AcceptActionUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1612814670555_311543_168", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? receiverArgument { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
