// -------------------------------------------------------------------------------------------------
// <copyright file="ICaseDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Cases
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Systems.Calculations;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A CaseDefinition is a CalculationDefinition for a process, often involving collecting evidence or
    /// data, relative to a subject, possibly involving the collaboration of one or more other actors,
    /// producing a result that meets an objective.
    /// </summary>
    [Class(xmiId: "_19_0_2_59601fc_1590256070522_658678_81", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ICaseDefinition : ICalculationDefinition
    {
        /// <summary>
        /// The parameters of this CaseDefinition that represent actors involved in the case.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621464609772_382176_1612", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        List<Guid> actorParameter { get; }

        /// <summary>
        /// The RequirementUsage representing the objective of this CaseDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_59601fc_1590259317710_27529_910", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        Guid? objectiveRequirement { get; }

        /// <summary>
        /// The parameter of this CaseDefinition that represents its subject.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189932946_106647_973", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        Guid subjectParameter { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
