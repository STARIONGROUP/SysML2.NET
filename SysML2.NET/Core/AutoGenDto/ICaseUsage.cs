// -------------------------------------------------------------------------------------------------
// <copyright file="ICaseUsage.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Calculations;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A CaseUsage is a Usage of a CaseDefinition.
    /// </summary>
    [Class(xmiId: "Systems-Cases-CaseUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ICaseUsage : ICalculationUsage
    {
        /// <summary>
        /// The parameters of this CaseUsage that represent actors involved in the case.
        /// </summary>
        [Property(xmiId: "Systems-Cases-CaseUsage-actorParameter", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Step-parameter")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        List<Guid> actorParameter { get; }

        /// <summary>
        /// The CaseDefinition that is the type of this CaseUsage.
        /// </summary>
        [Property(xmiId: "Systems-Cases-CaseUsage-caseDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Calculations-CalculationUsage-calculationDefinition")]
        Guid? caseDefinition { get; }

        /// <summary>
        /// The RequirementUsage representing the objective of this CaseUsage.
        /// </summary>
        [Property(xmiId: "Systems-Cases-CaseUsage-objectiveRequirement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        Guid? objectiveRequirement { get; }

        /// <summary>
        /// The parameter of this CaseUsage that represents its subject.
        /// </summary>
        [Property(xmiId: "Systems-Cases-CaseUsage-subjectParameter", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Step-parameter")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        Guid subjectParameter { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
