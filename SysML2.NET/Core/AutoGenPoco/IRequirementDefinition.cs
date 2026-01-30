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

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
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
    /// A RequirementDefinition is a ConstraintDefinition that defines a requirement used in the context of
    /// a specification as a constraint that a valid solution must satisfy. The specification is relative to
    /// a specified subject, possibly in collaboration with one or more external actors.
    /// </summary>
    [Class(xmiId: "Systems-Requirements-RequirementDefinition", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IRequirementDefinition : IConstraintDefinition
    {
        /// <summary>
        /// The parameters of this RequirementDefinition that represent actors involved in the requirement.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementDefinition-actorParameter", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Behavior-parameter")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-usage")]
        List<IPartUsage> actorParameter { get; }

        /// <summary>
        /// The owned ConstraintUsages that represent assumptions of this RequirementDefinition, which are the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementDefinition with kind =
        /// assumption.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementDefinition-assumedConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        List<IConstraintUsage> assumedConstraint { get; }

        /// <summary>
        /// The ConcernUsages framed by this RequirementDefinition, which are the ownedConcerns of all
        /// FramedConcernMemberships of the RequirementDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementDefinition-framedConcern", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-Requirements-RequirementDefinition-requiredConstraint")]
        List<IConcernUsage> framedConcern { get; }

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementDefinition (used, e.g., to link it to
        /// an original requirement text in some source document), which is the declaredShortName for the
        /// RequirementDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementDefinition-reqId", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Element-declaredShortName")]
        string ReqId { get; set; }

        /// <summary>
        /// The owned ConstraintUsages that represent requirements of this RequirementDefinition, derived as the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementDefinition with kind =
        /// requirement.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementDefinition-requiredConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        List<IConstraintUsage> requiredConstraint { get; }

        /// <summary>
        /// The parameters of this RequirementDefinition that represent stakeholders for th requirement.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementDefinition-stakeholderParameter", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Behavior-parameter")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-usage")]
        List<IPartUsage> stakeholderParameter { get; }

        /// <summary>
        /// The parameter of this RequirementDefinition that represents its subject.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementDefinition-subjectParameter", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Kernel-Behaviors-Behavior-parameter")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-usage")]
        IUsage subjectParameter { get; }

        /// <summary>
        /// An optional textual statement of the requirement represented by this RequirementDefinition, derived
        /// from the bodies of the documentation of the RequirementDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Requirements-RequirementDefinition-text", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<string> text { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
