// -------------------------------------------------------------------------------------------------
// <copyright file="IConcernUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Requirements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConcernUsage is a Usage of a ConcernDefinition. The ownedStakeholder features of the ConcernUsage
    /// shall all subset the ConcernCheck::concernedStakeholders feature. If the ConcernUsage is an
    /// ownedFeature of a StakeholderDefinition or StakeholderUsage, then the ConcernUsage shall have an
    /// ownedStakeholder feature that is bound to the self feature of its owner.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1617051561652_163085_1288", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConcernUsage : IRequirementUsage
    {
        /// <summary>
        /// The ConcernDefinition that is the single type of this ConcernUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617052514912_780627_2256", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1583000408905_769743_1223")]
        Guid? concernDefinition { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
