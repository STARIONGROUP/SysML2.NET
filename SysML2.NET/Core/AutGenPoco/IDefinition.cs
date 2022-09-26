// -------------------------------------------------------------------------------------------------
// <copyright file="IDefinition.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

    /// <summary>
    /// A Definition is a Classifier of Usages. The actual kinds of Definitions that may appear in a model
    /// are given by the subclasses of Definition (possibly as extended with user-defined
    /// SemanticMetadata).Normally, a Definition has owned Usages that model features of the thing being
    /// defined. A Definition may also have other Definitions nested in it, but this has no semantic
    /// significance, other than the nested scoping resulting from the Definition being considered as a
    /// Namespace for any nested Definitions.However, if a Definition has isVariation = true, then it
    /// represents a variation point Definition. In this case, all of its members must be variant Usages,
    /// related to the Definition by VariantMembership Relationships. Rather than being features of the
    /// Definition, variant Usages model different concrete alternatives that can be chosen to fill in for
    /// an abstract Usage of the variation point Definition.isVariation implies variantMembership =
    /// ownedMembershipvariant = variantMembership.ownedVariantUsagevariantMembership =
    /// ownedMembership->selectByKind(VariantMembership)not isVariation implies variantMembership->isEmpty()
    /// </summary>
    public partial interface IDefinition : IClassifier
    {
        /// <summary>
        /// Queries the derived property DirectedUsage
        /// </summary>
        List<Usage> QueryDirectedUsage();

        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        bool IsVariation { get; set; }

        /// <summary>
        /// Queries the derived property OwnedAction
        /// </summary>
        List<ActionUsage> QueryOwnedAction();

        /// <summary>
        /// Queries the derived property OwnedAllocation
        /// </summary>
        List<AllocationUsage> QueryOwnedAllocation();

        /// <summary>
        /// Queries the derived property OwnedAnalysisCase
        /// </summary>
        List<AnalysisCaseUsage> QueryOwnedAnalysisCase();

        /// <summary>
        /// Queries the derived property OwnedAttribute
        /// </summary>
        List<AttributeUsage> QueryOwnedAttribute();

        /// <summary>
        /// Queries the derived property OwnedCalculation
        /// </summary>
        List<CalculationUsage> QueryOwnedCalculation();

        /// <summary>
        /// Queries the derived property OwnedCase
        /// </summary>
        List<CaseUsage> QueryOwnedCase();

        /// <summary>
        /// Queries the derived property OwnedConcern
        /// </summary>
        List<ConcernUsage> QueryOwnedConcern();

        /// <summary>
        /// Queries the derived property OwnedConnection
        /// </summary>
        List<ConnectorAsUsage> QueryOwnedConnection();

        /// <summary>
        /// Queries the derived property OwnedConstraint
        /// </summary>
        List<ConstraintUsage> QueryOwnedConstraint();

        /// <summary>
        /// Queries the derived property OwnedEnumeration
        /// </summary>
        List<EnumerationUsage> QueryOwnedEnumeration();

        /// <summary>
        /// Queries the derived property OwnedFlow
        /// </summary>
        List<FlowConnectionUsage> QueryOwnedFlow();

        /// <summary>
        /// Queries the derived property OwnedInterface
        /// </summary>
        List<InterfaceUsage> QueryOwnedInterface();

        /// <summary>
        /// Queries the derived property OwnedItem
        /// </summary>
        List<ItemUsage> QueryOwnedItem();

        /// <summary>
        /// Queries the derived property OwnedMetadata
        /// </summary>
        List<MetadataUsage> QueryOwnedMetadata();

        /// <summary>
        /// Queries the derived property OwnedOccurrence
        /// </summary>
        List<OccurrenceUsage> QueryOwnedOccurrence();

        /// <summary>
        /// Queries the derived property OwnedPart
        /// </summary>
        List<PartUsage> QueryOwnedPart();

        /// <summary>
        /// Queries the derived property OwnedPort
        /// </summary>
        List<PortUsage> QueryOwnedPort();

        /// <summary>
        /// Queries the derived property OwnedReference
        /// </summary>
        List<ReferenceUsage> QueryOwnedReference();

        /// <summary>
        /// Queries the derived property OwnedRendering
        /// </summary>
        List<RenderingUsage> QueryOwnedRendering();

        /// <summary>
        /// Queries the derived property OwnedRequirement
        /// </summary>
        List<RequirementUsage> QueryOwnedRequirement();

        /// <summary>
        /// Queries the derived property OwnedState
        /// </summary>
        List<StateUsage> QueryOwnedState();

        /// <summary>
        /// Queries the derived property OwnedTransition
        /// </summary>
        List<TransitionUsage> QueryOwnedTransition();

        /// <summary>
        /// Queries the derived property OwnedUsage
        /// </summary>
        List<Usage> QueryOwnedUsage();

        /// <summary>
        /// Queries the derived property OwnedUseCase
        /// </summary>
        List<UseCaseUsage> QueryOwnedUseCase();

        /// <summary>
        /// Queries the derived property OwnedVerificationCase
        /// </summary>
        List<VerificationCaseUsage> QueryOwnedVerificationCase();

        /// <summary>
        /// Queries the derived property OwnedView
        /// </summary>
        List<ViewUsage> QueryOwnedView();

        /// <summary>
        /// Queries the derived property OwnedViewpoint
        /// </summary>
        List<ViewpointUsage> QueryOwnedViewpoint();

        /// <summary>
        /// Queries the derived property Usage
        /// </summary>
        List<Usage> QueryUsage();

        /// <summary>
        /// Queries the derived property Variant
        /// </summary>
        List<Usage> QueryVariant();

        /// <summary>
        /// Queries the derived property VariantMembership
        /// </summary>
        List<VariantMembership> QueryVariantMembership();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
