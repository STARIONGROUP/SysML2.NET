// -------------------------------------------------------------------------------------------------
// <copyright file="IDefinition.cs" company="Starion Group S.A.">
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
    /// A Definition is a Classifier of Usages. The actual kinds of Definition that may appear in a model
    /// are given by the subclasses of Definition (possibly as extended with user-defined
    /// SemanticMetadata).Normally, a Definition has owned Usages that model features of the thing being
    /// defined. A Definition may also have other Definitions nested in it, but this has no semantic
    /// significance, other than the nested scoping resulting from the Definition being considered as a
    /// Namespace for any nested Definitions.However, if a Definition has isVariation = true, then it
    /// represents a variation point Definition. In this case, all of its members must be variant Usages,
    /// related to the Definition by VariantMembership Relationships. Rather than being features of the
    /// Definition, variant Usages model different concrete alternatives that can be chosen to fill in for
    /// an abstract Usage of the variation point Definition.isVariation implies
    /// ownedFeatureMembership->isEmpty()variant = variantMembership.ownedVariantUsagevariantMembership =
    /// ownedMembership->selectByKind(VariantMembership)isVariation implies    not
    /// ownedSpecialization.specific->exists(        oclIsKindOf(Definition) and       
    /// oclAsType(Definition).isVariation)usage = feature->selectByKind(Usage)directedUsage =
    /// directedFeature->selectByKind(Usage)ownedUsage = ownedFeature->selectByKind(Usage)ownedAttribute =
    /// ownedUsage->selectByKind(AttributeUsage)ownedReference =
    /// ownedUsage->selectByKind(ReferenceUsage)ownedEnumeration =
    /// ownedUsage->selectByKind(EnumerationUsage)ownedOccurrence =
    /// ownedUsage->selectByKind(OccurrenceUsage)ownedItem = ownedUsage->selectByKind(ItemUsage)ownedPart =
    /// ownedUsage->selectByKind(PartUsage)ownedPort = ownedUsage->selectByKind(PortUsage)ownedConnection =
    /// ownedUsage->selectByKind(ConnectorAsUsage)ownedFlow =
    /// ownedUsage->selectByKind(FlowConnectionUsage)ownedInterface =
    /// ownedUsage->selectByKind(ReferenceUsage)ownedAllocation =
    /// ownedUsage->selectByKind(AllocationUsage)ownedAction =
    /// ownedUsage->selectByKind(ActionUsage)ownedState =
    /// ownedUsage->selectByKind(StateUsage)ownedTransition =
    /// ownedUsage->selectByKind(TransitionUsage)ownedCalculation =
    /// ownedUsage->selectByKind(CalculationUsage)ownedConstraint =
    /// ownedUsage->selectByKind(ConstraintUsage)ownedRequirement =
    /// ownedUsage->selectByKind(RequirementUsage)ownedConcern =
    /// ownedUsage->selectByKind(ConcernUsage)ownedCase =
    /// ownedUsage->selectByKind(CaseUsage)ownedAnalysisCase =
    /// ownedUsage->selectByKind(AnalysisCaseUsage)ownedVerificationCase =
    /// ownedUsage->selectByKind(VerificationCaseUsage)ownedUseCase =
    /// ownedUsage->selectByKind(UseCaseUsage)ownedView = ownedUsage->selectByKind(ViewUsage)ownedViewpoint
    /// = ownedUsage->selectByKind(ViewpointUsage)ownedRendering =
    /// ownedUsage->selectByKind(RenderingUsage)ownedMetadata =
    /// ownedUsage->selectByKind(MetadataUsage)isVariation implies isAbstract
    /// </summary>
    public partial interface IDefinition : IClassifier
    {
        /// <summary>
        /// Queries the derived property DirectedUsage
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Usage> QueryDirectedUsage();

        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsVariation { get; set; }

        /// <summary>
        /// Queries the derived property OwnedAction
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ActionUsage> QueryOwnedAction();

        /// <summary>
        /// Queries the derived property OwnedAllocation
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<AllocationUsage> QueryOwnedAllocation();

        /// <summary>
        /// Queries the derived property OwnedAnalysisCase
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<AnalysisCaseUsage> QueryOwnedAnalysisCase();

        /// <summary>
        /// Queries the derived property OwnedAttribute
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<AttributeUsage> QueryOwnedAttribute();

        /// <summary>
        /// Queries the derived property OwnedCalculation
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<CalculationUsage> QueryOwnedCalculation();

        /// <summary>
        /// Queries the derived property OwnedCase
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<CaseUsage> QueryOwnedCase();

        /// <summary>
        /// Queries the derived property OwnedConcern
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ConcernUsage> QueryOwnedConcern();

        /// <summary>
        /// Queries the derived property OwnedConnection
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<IConnectorAsUsage> QueryOwnedConnection();

        /// <summary>
        /// Queries the derived property OwnedConstraint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ConstraintUsage> QueryOwnedConstraint();

        /// <summary>
        /// Queries the derived property OwnedEnumeration
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<EnumerationUsage> QueryOwnedEnumeration();

        /// <summary>
        /// Queries the derived property OwnedFlow
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<FlowConnectionUsage> QueryOwnedFlow();

        /// <summary>
        /// Queries the derived property OwnedInterface
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<InterfaceUsage> QueryOwnedInterface();

        /// <summary>
        /// Queries the derived property OwnedItem
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ItemUsage> QueryOwnedItem();

        /// <summary>
        /// Queries the derived property OwnedMetadata
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<MetadataUsage> QueryOwnedMetadata();

        /// <summary>
        /// Queries the derived property OwnedOccurrence
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<OccurrenceUsage> QueryOwnedOccurrence();

        /// <summary>
        /// Queries the derived property OwnedPart
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PartUsage> QueryOwnedPart();

        /// <summary>
        /// Queries the derived property OwnedPort
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<PortUsage> QueryOwnedPort();

        /// <summary>
        /// Queries the derived property OwnedReference
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ReferenceUsage> QueryOwnedReference();

        /// <summary>
        /// Queries the derived property OwnedRendering
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<RenderingUsage> QueryOwnedRendering();

        /// <summary>
        /// Queries the derived property OwnedRequirement
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<RequirementUsage> QueryOwnedRequirement();

        /// <summary>
        /// Queries the derived property OwnedState
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<StateUsage> QueryOwnedState();

        /// <summary>
        /// Queries the derived property OwnedTransition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<TransitionUsage> QueryOwnedTransition();

        /// <summary>
        /// Queries the derived property OwnedUsage
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Usage> QueryOwnedUsage();

        /// <summary>
        /// Queries the derived property OwnedUseCase
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<UseCaseUsage> QueryOwnedUseCase();

        /// <summary>
        /// Queries the derived property OwnedVerificationCase
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<VerificationCaseUsage> QueryOwnedVerificationCase();

        /// <summary>
        /// Queries the derived property OwnedView
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ViewUsage> QueryOwnedView();

        /// <summary>
        /// Queries the derived property OwnedViewpoint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ViewpointUsage> QueryOwnedViewpoint();

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
