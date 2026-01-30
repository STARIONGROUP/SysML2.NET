// -------------------------------------------------------------------------------------------------
// <copyright file="IDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO.Systems.DefinitionAndUsage
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Core.Classifiers;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Definition is a Classifier of Usages. The actual kinds of Definition that may appear in a model
    /// are given by the subclasses of Definition (possibly as extended with user-defined SemanticMetadata).
    ///  Normally, a Definition has owned Usages that model features of the thing being defined. A
    /// Definition may also have other Definitions nested in it, but this has no semantic significance,
    /// other than the nested scoping resulting from the Definition being considered as a Namespace for any
    /// nested Definitions.  However, if a Definition has isVariation = true, then it represents a variation
    /// point Definition. In this case, all of its members must be variant Usages, related to the Definition
    /// by VariantMembership Relationships. Rather than being features of the Definition, variant Usages
    /// model different concrete alternatives that can be chosen to fill in for an abstract Usage of the
    /// variation point Definition.
    /// </summary>
    [Class(xmiId: "Systems-DefinitionAndUsage-Definition", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IDefinition : IClassifier
    {
        /// <summary>
        /// The usages of this Definition that are directedFeatures.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-directedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-usage")]
        List<Guid> directedUsage { get; }

        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-isVariation", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool IsVariation { get; set; }

        /// <summary>
        /// The ActionUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedOccurrence")]
        List<Guid> ownedAction { get; }

        /// <summary>
        /// The AllocationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedAllocation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedConnection")]
        List<Guid> ownedAllocation { get; }

        /// <summary>
        /// The AnalysisCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedAnalysisCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedCase")]
        List<Guid> ownedAnalysisCase { get; }

        /// <summary>
        /// The AttributeUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedAttribute", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        List<Guid> ownedAttribute { get; }

        /// <summary>
        /// The CalculationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedCalculation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedAction")]
        List<Guid> ownedCalculation { get; }

        /// <summary>
        /// The code>CaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedCalculation")]
        List<Guid> ownedCase { get; }

        /// <summary>
        /// The ConcernUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedConcern", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedRequirement")]
        List<Guid> ownedConcern { get; }

        /// <summary>
        /// The ConnectorAsUsages that are ownedUsages of this Definition. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowUsages because these are ConnectorAsUsages
        /// even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedConnection", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        List<Guid> ownedConnection { get; }

        /// <summary>
        /// The ConstraintUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedOccurrence")]
        List<Guid> ownedConstraint { get; }

        /// <summary>
        /// The EnumerationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedEnumeration", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedAttribute")]
        List<Guid> ownedEnumeration { get; }

        /// <summary>
        /// The FlowUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedFlow", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedConnection")]
        List<Guid> ownedFlow { get; }

        /// <summary>
        /// The InterfaceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedInterface", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedConnection")]
        List<Guid> ownedInterface { get; }

        /// <summary>
        /// The ItemUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedItem", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedOccurrence")]
        List<Guid> ownedItem { get; }

        /// <summary>
        /// The MetadataUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedMetadata", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedItem")]
        List<Guid> ownedMetadata { get; }

        /// <summary>
        /// The OccurrenceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedOccurrence", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        List<Guid> ownedOccurrence { get; }

        /// <summary>
        /// The PartUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedPart", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedItem")]
        List<Guid> ownedPart { get; }

        /// <summary>
        /// The PortUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedPort", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        List<Guid> ownedPort { get; }

        /// <summary>
        /// The ReferenceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedReference", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        List<Guid> ownedReference { get; }

        /// <summary>
        /// The RenderingUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedRendering", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedPart")]
        List<Guid> ownedRendering { get; }

        /// <summary>
        /// The RequirementUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedRequirement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedConstraint")]
        List<Guid> ownedRequirement { get; }

        /// <summary>
        /// The StateUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedState", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedAction")]
        List<Guid> ownedState { get; }

        /// <summary>
        /// The TransitionUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedTransition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        List<Guid> ownedTransition { get; }

        /// <summary>
        /// The Usages that are ownedFeatures of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-usage")]
        List<Guid> ownedUsage { get; }

        /// <summary>
        /// The UseCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedUseCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedCase")]
        List<Guid> ownedUseCase { get; }

        /// <summary>
        /// The VerificationCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedVerificationCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedCase")]
        List<Guid> ownedVerificationCase { get; }

        /// <summary>
        /// The ViewUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedView", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedPart")]
        List<Guid> ownedView { get; }

        /// <summary>
        /// The ViewpointUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedViewpoint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedRequirement")]
        List<Guid> ownedViewpoint { get; }

        /// <summary>
        /// The Usages that are features of this Definition (not necessarily owned).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-usage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        List<Guid> usage { get; }

        /// <summary>
        /// The Usages which represent the variants of this Definition as a variation point Definition, if
        /// isVariation = true. If isVariation = false, the there must be no variants.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-variant", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        List<Guid> variant { get; }

        /// <summary>
        /// The ownedMemberships of this Definition that are VariantMemberships. If isVariation = true, then
        /// this must be all ownedMemberships of the Definition. If isVariation = false, then
        /// variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-variantMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMembership")]
        List<Guid> variantMembership { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
