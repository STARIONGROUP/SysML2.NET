// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.Requirements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Systems.Constraints;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A RequirementDefinition is a ConstraintDefinition that defines a requirement used in the context of
    /// a specification as a constraint that a valid solution must satisfy. The specification is relative to
    /// a specified subject, possibly in collaboration with one or more external actors.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1582990729262_130404_898", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IRequirementDefinition : IConstraintDefinition
    {
        /// <summary>
        /// The parameters of this RequirementDefinition that represent actors involved in the requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621564041941_652319_2722", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        List<Guid> actorParameter { get; }

        /// <summary>
        /// The owned ConstraintUsages that represent assumptions of this RequirementDefinition, which are the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementDefinition with kind =
        /// assumption.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376806647_629021_133", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        List<Guid> assumedConstraint { get; }

        /// <summary>
        /// The ConcernUsages framed by this RequirementDefinition, which are the ownedConcerns of all
        /// FramedConcernMemberships of the RequirementDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617116733499_587735_3242", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583376932997_792124_158")]
        List<Guid> framedConcern { get; }

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementDefinition (used, e.g., to link it to
        /// an original requirement text in some source document), which is the declaredShortName for the
        /// RequirementDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376411386_270321_92", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1594160442439_915308_4153")]
        string ReqId { get; set; }

        /// <summary>
        /// The owned ConstraintUsages that represent requirements of this RequirementDefinition, derived as the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementDefinition with kind =
        /// requirement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376932997_792124_158", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        List<Guid> requiredConstraint { get; }

        /// <summary>
        /// The parameters of this RequirementDefinition that represent stakeholders for th requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624033010374_29375_40166", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        List<Guid> stakeholderParameter { get; }

        /// <summary>
        /// The parameter of this RequirementDefinition that represents its subject.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189007408_784255_586", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        Guid subjectParameter { get; }

        /// <summary>
        /// An optional textual statement of the requirement represented by this RequirementDefinition, derived
        /// from the bodies of the documentation of the RequirementDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376433122_189839_94", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<string> text { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
