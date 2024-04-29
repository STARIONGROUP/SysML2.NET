// -------------------------------------------------------------------------------------------------
// <copyright file="Definition.cs" company="Starion Group S.A.">
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
    public partial class Definition : IDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Definition"/> class.
        /// </summary>
        public Definition()
        {
            this.AliasIds = new List<string>();
            this.IsAbstract = false;
            this.IsImpliedIncluded = false;
            this.IsSufficient = false;
            this.OwnedRelationship = new List<Guid>();
        }

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        public List<string> AliasIds { get; set; }

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public string DeclaredName { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public string DeclaredShortName { get; set; }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public string ElementId { get; set; }

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.(A Type gives conditions that must be met by whatever it classifies, but when isSufficient
        /// is false, things may meet those conditions but still not be classified by the Type. For example, a
        /// Type Car that is not sufficient could require everything it classifies to have four wheels, but not
        /// all four wheeled things would classify as cars. However, if the Type Car were sufficient, it would
        /// classify all four-wheeled things.)
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsSufficient { get; set; }

        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsVariation { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: true)]
        public List<Guid> OwnedRelationship { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid? OwningRelationship { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
