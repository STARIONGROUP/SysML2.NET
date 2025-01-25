// -------------------------------------------------------------------------------------------------
// <copyright file="IUsage.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Usage is a usage of a Definition. A Usage may only be an ownedFeature of a Definition or another
    /// Usage.A Usage may have nestedUsages that model features that apply in the context of the
    /// owningUsage. A Usage may also have Definitions nested in it, but this has no semantic significance,
    /// other than the nested scoping resulting from the Usage being considered as a Namespace for any
    /// nested Definitions.However, if a Usage has isVariation = true, then it represents a variation point
    /// Usage. In this case, all of its members must be variant Usages, related to the Usage by
    /// VariantMembership Relationships. Rather than being features of the Usage, variant Usages model
    /// different concrete alternatives that can be chosen to fill in for the variation point Usage.variant
    /// = variantMembership.ownedVariantUsagevariantMembership =
    /// ownedMembership->selectByKind(VariantMembership)isVariation implies
    /// ownedFeatureMembership->isEmpty()isReference = not isCompositeowningVariationUsage <> null implies  
    ///  specializes(owningVariationUsage)isVariation implies    not ownedSpecialization.specific->exists(  
    ///      oclIsKindOf(Definition) and        oclAsType(Definition).isVariation or       
    /// oclIsKindOf(Usage) and        oclAsType(Usage).isVariation)owningVariationDefinition <> null implies
    ///    specializes(owningVariationDefinition)directedUsage =
    /// directedFeature->selectByKind(Usage)nestedAction =
    /// nestedUsage->selectByKind(ActionUsage)nestedAllocation =
    /// nestedUsage->selectByKind(AllocationUsage)nestedAnalysisCase =
    /// nestedUsage->selectByKind(AnalysisCaseUsage)nestedAttribute =
    /// nestedUsage->selectByKind(AttributeUsage)nestedCalculation =
    /// nestedUsage->selectByKind(CalculationUsage)nestedCase =
    /// nestedUsage->selectByKind(CaseUsage)nestedConcern =
    /// nestedUsage->selectByKind(ConcernUsage)nestedConnection =
    /// nestedUsage->selectByKind(ConnectorAsUsage)nestedConstraint =
    /// nestedUsage->selectByKind(ConstraintUsage)ownedNested =
    /// nestedUsage->selectByKind(EnumerationUsage)nestedFlow =
    /// nestedUsage->selectByKind(FlowConnectionUsage)nestedInterface =
    /// nestedUsage->selectByKind(ReferenceUsage)nestedItem =
    /// nestedUsage->selectByKind(ItemUsage)nestedMetadata =
    /// nestedUsage->selectByKind(MetadataUsage)nestedOccurrence =
    /// nestedUsage->selectByKind(OccurrenceUsage)nestedPart =
    /// nestedUsage->selectByKind(PartUsage)nestedPort = nestedUsage->selectByKind(PortUsage)nestedReference
    /// = nestedUsage->selectByKind(ReferenceUsage)nestedRendering =
    /// nestedUsage->selectByKind(RenderingUsage)nestedRequirement =
    /// nestedUsage->selectByKind(RequirementUsage)nestedState =
    /// nestedUsage->selectByKind(StateUsage)nestedTransition =
    /// nestedUsage->selectByKind(TransitionUsage)nestedUsage =
    /// ownedFeature->selectByKind(Usage)nestedUseCase =
    /// nestedUsage->selectByKind(UseCaseUsage)nestedVerificationCase =
    /// nestedUsage->selectByKind(VerificationCaseUsage)nestedView =
    /// nestedUsage->selectByKind(ViewUsage)nestedViewpoint = nestedUsage->selectByKind(ViewpointUsage)usage
    /// = feature->selectByKind(Usage)direction <> null or isEnd or featuringType->isEmpty() implies   
    /// isReferenceisVariation implies isAbstract
    /// </summary>
    public partial interface IUsage : IFeature
    {
        /// <summary>
        /// Queries the derived property Definition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Classifier> QueryDefinition();

        /// <summary>
        /// Queries the derived property DirectedUsage
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Usage> QueryDirectedUsage();

        /// <summary>
        /// Queries the derived property IsReference
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool QueryIsReference();

        /// <summary>
        /// Whether this Usage is for a variation point or not. If true, then all the memberships of the Usage
        /// must be VariantMemberships.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsVariation { get; set; }

        /// <summary>
        /// Queries the derived property NestedAction
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ActionUsage> QueryNestedAction();

        /// <summary>
        /// Queries the derived property NestedAllocation
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<AllocationUsage> QueryNestedAllocation();

        /// <summary>
        /// Queries the derived property NestedAnalysisCase
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<AnalysisCaseUsage> QueryNestedAnalysisCase();

        /// <summary>
        /// Queries the derived property NestedAttribute
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<AttributeUsage> QueryNestedAttribute();

        /// <summary>
        /// Queries the derived property NestedCalculation
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<CalculationUsage> QueryNestedCalculation();

        /// <summary>
        /// Queries the derived property NestedCase
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<CaseUsage> QueryNestedCase();

        /// <summary>
        /// Queries the derived property NestedConcern
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ConcernUsage> QueryNestedConcern();

        /// <summary>
        /// Queries the derived property NestedConnection
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<IConnectorAsUsage> QueryNestedConnection();

        /// <summary>
        /// Queries the derived property NestedConstraint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ConstraintUsage> QueryNestedConstraint();

        /// <summary>
        /// Queries the derived property NestedEnumeration
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<EnumerationUsage> QueryNestedEnumeration();

        /// <summary>
        /// Queries the derived property NestedFlow
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<FlowConnectionUsage> QueryNestedFlow();

        /// <summary>
        /// Queries the derived property NestedInterface
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<InterfaceUsage> QueryNestedInterface();

        /// <summary>
        /// Queries the derived property NestedItem
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ItemUsage> QueryNestedItem();

        /// <summary>
        /// Queries the derived property NestedMetadata
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<MetadataUsage> QueryNestedMetadata();

        /// <summary>
        /// Queries the derived property NestedOccurrence
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<OccurrenceUsage> QueryNestedOccurrence();

        /// <summary>
        /// Queries the derived property NestedPart
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PartUsage> QueryNestedPart();

        /// <summary>
        /// Queries the derived property NestedPort
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PortUsage> QueryNestedPort();

        /// <summary>
        /// Queries the derived property NestedReference
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ReferenceUsage> QueryNestedReference();

        /// <summary>
        /// Queries the derived property NestedRendering
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<RenderingUsage> QueryNestedRendering();

        /// <summary>
        /// Queries the derived property NestedRequirement
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<RequirementUsage> QueryNestedRequirement();

        /// <summary>
        /// Queries the derived property NestedState
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<StateUsage> QueryNestedState();

        /// <summary>
        /// Queries the derived property NestedTransition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<TransitionUsage> QueryNestedTransition();

        /// <summary>
        /// Queries the derived property NestedUsage
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Usage> QueryNestedUsage();

        /// <summary>
        /// Queries the derived property NestedUseCase
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<UseCaseUsage> QueryNestedUseCase();

        /// <summary>
        /// Queries the derived property NestedVerificationCase
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<VerificationCaseUsage> QueryNestedVerificationCase();

        /// <summary>
        /// Queries the derived property NestedView
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ViewUsage> QueryNestedView();

        /// <summary>
        /// Queries the derived property NestedViewpoint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ViewpointUsage> QueryNestedViewpoint();

        /// <summary>
        /// Queries the derived property OwningDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Definition QueryOwningDefinition();

        /// <summary>
        /// Queries the derived property OwningUsage
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Usage QueryOwningUsage();

        /// <summary>
        /// Queries the derived property Usage
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Usage> QueryUsage();

        /// <summary>
        /// Queries the derived property Variant
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Usage> QueryVariant();

        /// <summary>
        /// Queries the derived property VariantMembership
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<VariantMembership> QueryVariantMembership();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
