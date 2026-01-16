// -------------------------------------------------------------------------------------------------
// <copyright file="IViewpointUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Views
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Requirements;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ViewpointUsage is a Usage of a ViewpointDefinition.
    /// </summary>
    [Class(xmiId: "_19_0_2_59601fc_1583087291401_74297_590", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IViewpointUsage : IRequirementUsage
    {
        /// <summary>
        /// The ViewpointDefinition that is the definition of this ViewpointUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596649684798_569222_3524", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1583000408905_769743_1223")]
        Guid? viewpointDefinition { get; }

        /// <summary>
        /// The PartUsages that identify the stakeholders with concerns framed by this ViewpointUsage, which are
        /// the owned and inherited stakeholderParameters of the framedConcerns of this ViewpointUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617117200628_940407_3323", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> viewpointStakeholder { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
