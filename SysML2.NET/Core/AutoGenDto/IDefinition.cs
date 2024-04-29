// -------------------------------------------------------------------------------------------------
// <copyright file="IDefinition.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO
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
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsVariation { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
