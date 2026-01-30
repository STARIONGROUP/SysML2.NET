// -------------------------------------------------------------------------------------------------
// <copyright file="ITerminateActionUsage.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Decorators;

    /// <summary>
    /// A TerminateActionUsage is an ActionUsage that directly or indirectly specializes the
    /// ActionDefinition TerminateAction from the Systems Model Library, which causes a given
    /// terminatedOccurrence to end during its performance. By default, the terminatedOccurrence is the
    /// featuring instance (that) of the performance of the TerminateActionUsage, generally the performance
    /// of its immediately containing ActionDefinition or ActionUsage.
    /// </summary>
    [Class(xmiId: "Systems-Actions-TerminateActionUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ITerminateActionUsage : IActionUsage
    {
        /// <summary>
        /// The Expression that is the featureValue of the terminateOccurrence parameter of this
        /// TerminateActionUsage.
        /// </summary>
        [Property(xmiId: "Systems-Actions-TerminateActionUsage-terminatedOccurrenceArgument", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? terminatedOccurrenceArgument { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
