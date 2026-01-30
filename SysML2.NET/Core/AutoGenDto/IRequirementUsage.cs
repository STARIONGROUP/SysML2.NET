// -------------------------------------------------------------------------------------------------
// <copyright file="IRequirementUsage.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.DTO.Systems.Constraints;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A RequirementUsage is a Usage of a RequirementDefinition.
    /// </summary>
    [Class(xmiId: "Systems-Requirements-RequirementUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IRequirementUsage : IConstraintUsage
    {
        /// <summary>
        /// The parameters of this RequirementUsage that represent actors involved in the requirement.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-actorParameter", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Step-parameter")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        List<Guid> actorParameter { get; }

        /// <summary>
        /// The owned ConstraintUsages that represent assumptions of this RequirementUsage, derived as the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementUsage with kind =
        /// assumption.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-assumedConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        List<Guid> assumedConstraint { get; }

        /// <summary>
        /// The ConcernUsages framed by this RequirementUsage, which are the ownedConcerns of all
        /// FramedConcernMemberships of the RequirementUsage.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-framedConcern", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-Requirements-RequirementUsage-requiredConstraint")]
        List<Guid> framedConcern { get; }

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementUsage (used, e.g., to link it to an
        /// original requirement text in some source document), which is the declaredShortName for the
        /// RequirementUsage.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-reqId", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Element-declaredShortName")]
        string ReqId { get; set; }

        /// <summary>
        /// The owned ConstraintUsages that represent requirements of this RequirementUsage, which are the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementUsage with kind =
        /// requirement.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-requiredConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        List<Guid> requiredConstraint { get; }

        /// <summary>
        /// The RequirementDefinition that is the single definition of this RequirementUsage.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-requirementDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Constraints-ConstraintUsage-constraintDefinition")]
        Guid? requirementDefinition { get; }

        /// <summary>
        /// The parameters of this RequirementUsage that represent stakeholders for the requirement.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-stakeholderParameter", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Step-parameter")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        List<Guid> stakeholderParameter { get; }

        /// <summary>
        /// The parameter of this RequirementUsage that represents its subject.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-subjectParameter", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Step-parameter")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        Guid subjectParameter { get; }

        /// <summary>
        /// An optional textual statement of the requirement represented by this RequirementUsage, derived from
        /// the bodies of the documentation of the RequirementUsage.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementUsage-text", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<string> text { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
