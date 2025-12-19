// -------------------------------------------------------------------------------------------------
// <copyright file="IAssignmentActionUsage.cs" company="Starion Group S.A.">
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
    /// An AssignmentActionUsage is an ActionUsage that is defined, directly or indirectly, by the
    /// ActionDefinition AssignmentAction from the Systems Model Library. It specifies that the value of the
    /// referent Feature, relative to the target given by the result of the targetArgument Expression,
    /// should be set to the result of the valueExpression.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1624201606942_142574_2658", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IAssignmentActionUsage : IActionUsage
    {
        /// <summary>
        /// The Feature whose value is to be set.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624202269076_561550_3109", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        Guid Referent { get; }

        /// <summary>
        /// The Expression whose value is an occurrence in the domain of the referent Feature, for which the
        /// value of the referent will be set to the result of the valueExpression by this
        /// AssignmentActionUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624201786354_844501_2835", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? TargetArgument { get; }

        /// <summary>
        /// The Expression whose result is to be assigned to the referent Feature.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624201792996_104394_2856", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        Guid? ValueExpression { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
