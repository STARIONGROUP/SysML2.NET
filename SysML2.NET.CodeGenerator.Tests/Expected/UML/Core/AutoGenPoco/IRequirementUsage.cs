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

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
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
        List<IPartUsage> QueryActorParameter();

        /// <summary>
        /// The owned ConstraintUsages that represent assumptions of this RequirementUsage, derived as the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementUsage with kind =
        /// assumption.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583377612865_991722_535", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        List<IConstraintUsage> QueryAssumedConstraint();

        /// <summary>
        /// The ConcernUsages framed by this RequirementUsage, which are the ownedConcerns of all
        /// FramedConcernMemberships of the RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617116922864_514612_3264", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583377448339_252740_390")]
        List<IConcernUsage> QueryFramedConcern();

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementUsage (used, e.g., to link it to an
        /// original requirement text in some source document), which is the declaredShortName for the
        /// RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376474630_975784_96", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1594160442439_915308_4153")]
        string ReqId { get; set; }

        /// <summary>
        /// The owned ConstraintUsages that represent requirements of this RequirementUsage, which are the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementUsage with kind =
        /// requirement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583377448339_252740_390", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        List<IConstraintUsage> QueryRequiredConstraint();

        /// <summary>
        /// The RequirementDefinition that is the single definition of this RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583000408905_769743_1223", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1578067546711_751168_1745")]
        IRequirementDefinition QueryRequirementDefinition();

        /// <summary>
        /// The parameters of this RequirementUsage that represent stakeholders for the requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624032823963_328647_40107", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        List<IPartUsage> QueryStakeholderParameter();

        /// <summary>
        /// The parameter of this RequirementUsage that represents its subject.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189397261_941898_844", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        IUsage QuerySubjectParameter();

        /// <summary>
        /// An optional textual statement of the requirement represented by this RequirementUsage, derived from
        /// the bodies of the documentation of the RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376480942_745679_99", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<string> QueryText();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
