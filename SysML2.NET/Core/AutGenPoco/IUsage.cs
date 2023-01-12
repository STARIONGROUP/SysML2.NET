// -------------------------------------------------------------------------------------------------
// <copyright file="IUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// A Usage is a usage of a Definition. A Usage may only be an ownedFeature of a Definition or another
    /// Usage.A Usage may have nestedUsages that model features that apply in the context of the
    /// owningUsage. A Usage may also have Definitions nested in it, but this has no semantic significance,
    /// other than the nested scoping resulting from the Usage being considered as a Namespace for any
    /// nested Definitions.However, if a Usage has isVariation = true, then it represents a variation point
    /// Usage. In this case, all of its members must be variant Usages, related to the Usage by
    /// VariantMembership Relationships. Rather than being features of the Usage, variant Usages model
    /// different concrete alternatives that can be chosen to fill in for the variation point Usage.variant
    /// = variantMembership.ownedVariantUsagevariantMembership =
    /// ownedMembership->selectByKind(VariantMembership)not isVariation implies
    /// variantMembership->isEmpty()isVariation implies variantMembership = ownedMembershipisReference = not
    /// isComposite
    /// </summary>
    public partial interface IUsage : IFeature
    {
        /// <summary>
        /// Queries the derived property Definition
        /// </summary>
        List<Classifier> QueryDefinition();

        /// <summary>
        /// Queries the derived property DirectedUsage
        /// </summary>
        List<Usage> QueryDirectedUsage();

        /// <summary>
        /// Queries the derived property IsReference
        /// </summary>
        bool QueryIsReference();

        /// <summary>
        /// Whether this Usage is for a variation point or not. If true, then all the memberships of the Usage
        /// must be VariantMemberships.
        /// </summary>
        bool IsVariation { get; set; }

        /// <summary>
        /// Queries the derived property NestedAction
        /// </summary>
        List<ActionUsage> QueryNestedAction();

        /// <summary>
        /// Queries the derived property NestedAllocation
        /// </summary>
        List<AllocationUsage> QueryNestedAllocation();

        /// <summary>
        /// Queries the derived property NestedAnalysisCase
        /// </summary>
        List<AnalysisCaseUsage> QueryNestedAnalysisCase();

        /// <summary>
        /// Queries the derived property NestedAttribute
        /// </summary>
        List<AttributeUsage> QueryNestedAttribute();

        /// <summary>
        /// Queries the derived property NestedCalculation
        /// </summary>
        List<CalculationUsage> QueryNestedCalculation();

        /// <summary>
        /// Queries the derived property NestedCase
        /// </summary>
        List<CaseUsage> QueryNestedCase();

        /// <summary>
        /// Queries the derived property NestedConcern
        /// </summary>
        List<ConcernUsage> QueryNestedConcern();

        /// <summary>
        /// Queries the derived property NestedConnection
        /// </summary>
        List<ConnectorAsUsage> QueryNestedConnection();

        /// <summary>
        /// Queries the derived property NestedConstraint
        /// </summary>
        List<ConstraintUsage> QueryNestedConstraint();

        /// <summary>
        /// Queries the derived property NestedEnumeration
        /// </summary>
        List<EnumerationUsage> QueryNestedEnumeration();

        /// <summary>
        /// Queries the derived property NestedFlow
        /// </summary>
        List<FlowConnectionUsage> QueryNestedFlow();

        /// <summary>
        /// Queries the derived property NestedInterface
        /// </summary>
        List<InterfaceUsage> QueryNestedInterface();

        /// <summary>
        /// Queries the derived property NestedItem
        /// </summary>
        List<ItemUsage> QueryNestedItem();

        /// <summary>
        /// Queries the derived property NestedMetadata
        /// </summary>
        List<MetadataUsage> QueryNestedMetadata();

        /// <summary>
        /// Queries the derived property NestedOccurrence
        /// </summary>
        List<OccurrenceUsage> QueryNestedOccurrence();

        /// <summary>
        /// Queries the derived property NestedPart
        /// </summary>
        List<PartUsage> QueryNestedPart();

        /// <summary>
        /// Queries the derived property NestedPort
        /// </summary>
        List<PortUsage> QueryNestedPort();

        /// <summary>
        /// Queries the derived property NestedReference
        /// </summary>
        List<ReferenceUsage> QueryNestedReference();

        /// <summary>
        /// Queries the derived property NestedRendering
        /// </summary>
        List<RenderingUsage> QueryNestedRendering();

        /// <summary>
        /// Queries the derived property NestedRequirement
        /// </summary>
        List<RequirementUsage> QueryNestedRequirement();

        /// <summary>
        /// Queries the derived property NestedState
        /// </summary>
        List<StateUsage> QueryNestedState();

        /// <summary>
        /// Queries the derived property NestedTransition
        /// </summary>
        List<TransitionUsage> QueryNestedTransition();

        /// <summary>
        /// Queries the derived property NestedUsage
        /// </summary>
        List<Usage> QueryNestedUsage();

        /// <summary>
        /// Queries the derived property NestedUseCase
        /// </summary>
        List<UseCaseUsage> QueryNestedUseCase();

        /// <summary>
        /// Queries the derived property NestedVerificationCase
        /// </summary>
        List<VerificationCaseUsage> QueryNestedVerificationCase();

        /// <summary>
        /// Queries the derived property NestedView
        /// </summary>
        List<ViewUsage> QueryNestedView();

        /// <summary>
        /// Queries the derived property NestedViewpoint
        /// </summary>
        List<ViewpointUsage> QueryNestedViewpoint();

        /// <summary>
        /// Queries the derived property OwningDefinition
        /// </summary>
        Definition QueryOwningDefinition();

        /// <summary>
        /// Queries the derived property OwningUsage
        /// </summary>
        Usage QueryOwningUsage();

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
