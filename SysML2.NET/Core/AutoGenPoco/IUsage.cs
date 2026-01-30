// -------------------------------------------------------------------------------------------------
// <copyright file="IUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.DefinitionAndUsage
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
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
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Usage is a usage of a Definition.  A Usage may have nestedUsages that model features that apply in
    /// the context of the owningUsage. A Usage may also have Definitions nested in it, but this has no
    /// semantic significance, other than the nested scoping resulting from the Usage being considered as a
    /// Namespace for any nested Definitions.  However, if a Usage has isVariation = true, then it
    /// represents a variation point Usage. In this case, all of its members must be variant Usages, related
    /// to the Usage by VariantMembership Relationships. Rather than being features of the Usage, variant
    /// Usages model different concrete alternatives that can be chosen to fill in for the variation point
    /// Usage.
    /// </summary>
    [Class(xmiId: "Systems-DefinitionAndUsage-Usage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IUsage : IFeature
    {
        /// <summary>
        /// The Classifiers that are the types of this Usage. Nominally, these are Definitions, but other kinds
        /// of Kernel Classifiers are also allowed, to permit use of Classifiers from the Kernel Model
        /// Libraries.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-definition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-Feature-type")]
        List<IClassifier> definition { get; }

        /// <summary>
        /// The usages of this Usage that are directedFeatures.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-directedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        List<IUsage> directedUsage { get; }

        /// <summary>
        /// Whether this Usage is a referential Usage, that is, it has isComposite = false.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-isReference", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool isReference { get; }

        /// <summary>
        /// Whether this Usage is for a variation point or not. If true, then all the memberships of the Usage
        /// must be VariantMemberships.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-isVariation", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool IsVariation { get; set; }

        /// <summary>
        /// Whether this Usage may be time varying (that is, whether it is featured by the snapshots of its
        /// owningType, rather than being featured by the owningType itself). However, if isConstant is also
        /// true, then the value of the Usage is nevertheless constant over the entire duration of an instance
        /// of its owningType (that is, it has the same value on all snapshots).  The property mayTimeVary
        /// redefines the KerML property Feature::isVariable, making it derived. The property isConstant is
        /// inherited from Feature.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-mayTimeVary", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-Feature-isVariable")]
        bool mayTimeVary { get; }

        /// <summary>
        /// The ActionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedOccurrence")]
        List<IActionUsage> nestedAction { get; }

        /// <summary>
        /// The AllocationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAllocation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        List<IAllocationUsage> nestedAllocation { get; }

        /// <summary>
        /// The AnalysisCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAnalysisCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        List<IAnalysisCaseUsage> nestedAnalysisCase { get; }

        /// <summary>
        /// The code>AttributeUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAttribute", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<IAttributeUsage> nestedAttribute { get; }

        /// <summary>
        /// The CalculationUsage that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedCalculation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAction")]
        List<ICalculationUsage> nestedCalculation { get; }

        /// <summary>
        /// The CaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCalculation")]
        List<ICaseUsage> nestedCase { get; }

        /// <summary>
        /// The ConcernUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConcern", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedRequirement")]
        List<IConcernUsage> nestedConcern { get; }

        /// <summary>
        /// The ConnectorAsUsages that are nestedUsages of this Usage. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowConnectionUsages because these are
        /// ConnectorAsUsages even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConnection", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<IConnectorAsUsage> nestedConnection { get; }

        /// <summary>
        /// The ConstraintUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedOccurrence")]
        List<IConstraintUsage> nestedConstraint { get; }

        /// <summary>
        /// The code>EnumerationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedEnumeration", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAttribute")]
        List<IEnumerationUsage> nestedEnumeration { get; }

        /// <summary>
        /// The code>FlowUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedFlow", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        List<IFlowUsage> nestedFlow { get; }

        /// <summary>
        /// The InterfaceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedInterface", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        List<IInterfaceUsage> nestedInterface { get; }

        /// <summary>
        /// The ItemUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedItem", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedOccurrence")]
        List<IItemUsage> nestedItem { get; }

        /// <summary>
        /// The MetadataUsages that are nestedUsages of this of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedMetadata", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedItem")]
        List<IMetadataUsage> nestedMetadata { get; }

        /// <summary>
        /// The OccurrenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedOccurrence", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<IOccurrenceUsage> nestedOccurrence { get; }

        /// <summary>
        /// The PartUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedPart", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedItem")]
        List<IPartUsage> nestedPart { get; }

        /// <summary>
        /// The PortUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedPort", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<IPortUsage> nestedPort { get; }

        /// <summary>
        /// The ReferenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedReference", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<IReferenceUsage> nestedReference { get; }

        /// <summary>
        /// The RenderingUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedRendering", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedPart")]
        List<IRenderingUsage> nestedRendering { get; }

        /// <summary>
        /// The RequirementUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedRequirement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConstraint")]
        List<IRequirementUsage> nestedRequirement { get; }

        /// <summary>
        /// The StateUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedState", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAction")]
        List<IStateUsage> nestedState { get; }

        /// <summary>
        /// The TransitionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedTransition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<ITransitionUsage> nestedTransition { get; }

        /// <summary>
        /// The Usages that are ownedFeatures of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        List<IUsage> nestedUsage { get; }

        /// <summary>
        /// The UseCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedUseCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        List<IUseCaseUsage> nestedUseCase { get; }

        /// <summary>
        /// The VerificationCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedVerificationCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        List<IVerificationCaseUsage> nestedVerificationCase { get; }

        /// <summary>
        /// The ViewUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedView", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedPart")]
        List<IViewUsage> nestedView { get; }

        /// <summary>
        /// The ViewpointUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedViewpoint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedRequirement")]
        List<IViewpointUsage> nestedViewpoint { get; }

        /// <summary>
        /// The Definition that owns this Usage (if any).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-owningDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-owningType")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-A_usage_featuringDefinition-featuringDefinition")]
        IDefinition owningDefinition { get; }

        /// <summary>
        /// The Usage in which this Usage is nested (if any).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-owningUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-owningType")]
        IUsage owningUsage { get; }

        /// <summary>
        /// The Usages that are features of this Usage (not necessarily owned).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-usage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        List<IUsage> usage { get; }

        /// <summary>
        /// The Usages which represent the variants of this Usage as a variation point Usage, if isVariation =
        /// true. If isVariation = false, then there must be no variants.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-variant", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        List<IUsage> variant { get; }

        /// <summary>
        /// The ownedMemberships of this Usage that are VariantMemberships. If isVariation = true, then this
        /// must be all memberships of the Usage. If isVariation = false, then variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-variantMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMembership")]
        List<IVariantMembership> variantMembership { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
