// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementUsage.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Systems.Occurrences;

    using SysML2.NET.Decorators;

    /// <summary>
    /// A RequirementUsage is a Usage of a RequirementDefinition.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1582991078230_41497_1143", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IRequirementUsage : IConstraintUsage
    {
        /// <summary>
        /// The parameters of this RequirementUsage that represent actors involved in the requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621564075474_350859_2735", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        List<Guid> ActorParameter { get; }

        /// <summary>
        /// The owned ConstraintUsages that represent assumptions of this RequirementUsage, derived as the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementUsage with kind =
        /// assumption.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583377612865_991722_535", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        List<Guid> AssumedConstraint { get; }

        /// <summary>
        /// The ConcernUsages framed by this RequirementUsage, which are the ownedConcerns of all
        /// FramedConcernMemberships of the RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617116922864_514612_3264", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583377448339_252740_390")]
        List<Guid> FramedConcern { get; }

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementUsage (used, e.g., to link it to an
        /// original requirement text in some source document), which is the declaredShortName for the
        /// RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376474630_975784_96", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1594160442439_915308_4153")]
        string? ReqId { get; set; }

        /// <summary>
        /// The owned ConstraintUsages that represent requirements of this RequirementUsage, which are the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementUsage with kind =
        /// requirement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583377448339_252740_390", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        List<Guid> RequiredConstraint { get; }

        /// <summary>
        /// The RequirementDefinition that is the single definition of this RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583000408905_769743_1223", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1578067546711_751168_1745")]
        Guid? RequirementDefinition { get; }

        /// <summary>
        /// The parameters of this RequirementUsage that represent stakeholders for the requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624032823963_328647_40107", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        List<Guid> StakeholderParameter { get; }

        /// <summary>
        /// The parameter of this RequirementUsage that represents its subject.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189397261_941898_844", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        Guid SubjectParameter { get; }

        /// <summary>
        /// An optional textual statement of the requirement represented by this RequirementUsage, derived from
        /// the bodies of the documentation of the RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376480942_745679_99", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<string> Text { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
