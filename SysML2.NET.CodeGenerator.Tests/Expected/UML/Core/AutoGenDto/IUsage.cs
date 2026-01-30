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

namespace SysML2.NET.Core.DTO.Systems.DefinitionAndUsage
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Core.Features;
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
        List<Guid> definition { get; }

        /// <summary>
        /// The usages of this Usage that are directedFeatures.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-directedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        List<Guid> directedUsage { get; }

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
        List<Guid> nestedAction { get; }

        /// <summary>
        /// The AllocationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAllocation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        List<Guid> nestedAllocation { get; }

        /// <summary>
        /// The AnalysisCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAnalysisCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        List<Guid> nestedAnalysisCase { get; }

        /// <summary>
        /// The code>AttributeUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAttribute", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<Guid> nestedAttribute { get; }

        /// <summary>
        /// The CalculationUsage that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedCalculation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAction")]
        List<Guid> nestedCalculation { get; }

        /// <summary>
        /// The CaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCalculation")]
        List<Guid> nestedCase { get; }

        /// <summary>
        /// The ConcernUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConcern", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedRequirement")]
        List<Guid> nestedConcern { get; }

        /// <summary>
        /// The ConnectorAsUsages that are nestedUsages of this Usage. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowConnectionUsages because these are
        /// ConnectorAsUsages even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConnection", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<Guid> nestedConnection { get; }

        /// <summary>
        /// The ConstraintUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedOccurrence")]
        List<Guid> nestedConstraint { get; }

        /// <summary>
        /// The code>EnumerationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedEnumeration", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAttribute")]
        List<Guid> nestedEnumeration { get; }

        /// <summary>
        /// The code>FlowUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedFlow", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        List<Guid> nestedFlow { get; }

        /// <summary>
        /// The InterfaceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedInterface", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        List<Guid> nestedInterface { get; }

        /// <summary>
        /// The ItemUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedItem", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedOccurrence")]
        List<Guid> nestedItem { get; }

        /// <summary>
        /// The MetadataUsages that are nestedUsages of this of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedMetadata", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedItem")]
        List<Guid> nestedMetadata { get; }

        /// <summary>
        /// The OccurrenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedOccurrence", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<Guid> nestedOccurrence { get; }

        /// <summary>
        /// The PartUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedPart", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedItem")]
        List<Guid> nestedPart { get; }

        /// <summary>
        /// The PortUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedPort", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<Guid> nestedPort { get; }

        /// <summary>
        /// The ReferenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedReference", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<Guid> nestedReference { get; }

        /// <summary>
        /// The RenderingUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedRendering", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedPart")]
        List<Guid> nestedRendering { get; }

        /// <summary>
        /// The RequirementUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedRequirement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConstraint")]
        List<Guid> nestedRequirement { get; }

        /// <summary>
        /// The StateUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedState", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAction")]
        List<Guid> nestedState { get; }

        /// <summary>
        /// The TransitionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedTransition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        List<Guid> nestedTransition { get; }

        /// <summary>
        /// The Usages that are ownedFeatures of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        List<Guid> nestedUsage { get; }

        /// <summary>
        /// The UseCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedUseCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        List<Guid> nestedUseCase { get; }

        /// <summary>
        /// The VerificationCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedVerificationCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        List<Guid> nestedVerificationCase { get; }

        /// <summary>
        /// The ViewUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedView", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedPart")]
        List<Guid> nestedView { get; }

        /// <summary>
        /// The ViewpointUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedViewpoint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedRequirement")]
        List<Guid> nestedViewpoint { get; }

        /// <summary>
        /// The Definition that owns this Usage (if any).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-owningDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-owningType")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-A_usage_featuringDefinition-featuringDefinition")]
        Guid? owningDefinition { get; }

        /// <summary>
        /// The Usage in which this Usage is nested (if any).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-owningUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-owningType")]
        Guid? owningUsage { get; }

        /// <summary>
        /// The Usages that are features of this Usage (not necessarily owned).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-usage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        List<Guid> usage { get; }

        /// <summary>
        /// The Usages which represent the variants of this Usage as a variation point Usage, if isVariation =
        /// true. If isVariation = false, then there must be no variants.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-variant", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        List<Guid> variant { get; }

        /// <summary>
        /// The ownedMemberships of this Usage that are VariantMemberships. If isVariation = true, then this
        /// must be all memberships of the Usage. If isVariation = false, then variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-variantMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMembership")]
        List<Guid> variantMembership { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
