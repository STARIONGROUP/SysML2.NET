// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectionDefinition.cs" company="RHEA System S.A.">
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
    /// A ConnectionDefinition is a PartDefinition that is also an AssociationStructure, with two or more
    /// end features. The associationEnds of a ConnectionDefinition must be Usages.A ConnectionDefinition
    /// must subclass, directly or indirectly, the base ConnectionDefinition Connection from the Systems
    /// model library.
    /// </summary>
    public partial class ConnectionDefinition : IConnectionDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionDefinition"/> class.
        /// </summary>
        public ConnectionDefinition()
        {
            this.AliasIds = new List<string>();
            this.IsAbstract = false;
            this.IsImplied = false;
            this.IsImpliedIncluded = false;
            this.IsIndividual = false;
            this.IsSufficient = false;
            this.OwnedRelatedElement = new List<Element>();
            this.OwnedRelationship = new List<Relationship>();
            this.Source = new List<Element>();
            this.Target = new List<Element>();
        }

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        public List<string> AliasIds { get; set; }

        /// <summary>
        /// Queries the derived property AssociationEnd
        /// </summary>
        public List<Feature> QueryAssociationEnd()
        {
            throw new NotImplementedException("Derived property AssociationEnd not yet supported");
        }

        /// <summary>
        /// Queries the derived property ConnectionEnd
        /// </summary>
        public List<Usage> QueryConnectionEnd()
        {
            throw new NotImplementedException("Derived property ConnectionEnd not yet supported");
        }

        /// <summary>
        /// Queries the derived property DifferencingType
        /// </summary>
        public List<Type> QueryDifferencingType()
        {
            throw new NotImplementedException("Derived property DifferencingType not yet supported");
        }

        /// <summary>
        /// Queries the derived property DirectedFeature
        /// </summary>
        public List<Feature> QueryDirectedFeature()
        {
            throw new NotImplementedException("Derived property DirectedFeature not yet supported");
        }

        /// <summary>
        /// Queries the derived property DirectedUsage
        /// </summary>
        public List<Usage> QueryDirectedUsage()
        {
            throw new NotImplementedException("Derived property DirectedUsage not yet supported");
        }

        /// <summary>
        /// Queries the derived property Documentation
        /// </summary>
        public List<Documentation> QueryDocumentation()
        {
            throw new NotImplementedException("Derived property Documentation not yet supported");
        }

        /// <summary>
        /// Queries the derived property EffectiveName
        /// </summary>
        public string QueryEffectiveName()
        {
            throw new NotImplementedException("Derived property EffectiveName not yet supported");
        }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// Queries the derived property EndFeature
        /// </summary>
        public List<Feature> QueryEndFeature()
        {
            throw new NotImplementedException("Derived property EndFeature not yet supported");
        }

        /// <summary>
        /// Queries the derived property Feature
        /// </summary>
        public List<Feature> QueryFeature()
        {
            throw new NotImplementedException("Derived property Feature not yet supported");
        }

        /// <summary>
        /// Queries the derived property FeatureMembership
        /// </summary>
        public List<FeatureMembership> QueryFeatureMembership()
        {
            throw new NotImplementedException("Derived property FeatureMembership not yet supported");
        }

        /// <summary>
        /// Queries the derived property ImportedMembership
        /// </summary>
        public List<Membership> QueryImportedMembership()
        {
            throw new NotImplementedException("Derived property ImportedMembership not yet supported");
        }

        /// <summary>
        /// Queries the derived property InheritedFeature
        /// </summary>
        public List<Feature> QueryInheritedFeature()
        {
            throw new NotImplementedException("Derived property InheritedFeature not yet supported");
        }

        /// <summary>
        /// Queries the derived property InheritedMembership
        /// </summary>
        public List<Membership> QueryInheritedMembership()
        {
            throw new NotImplementedException("Derived property InheritedMembership not yet supported");
        }

        /// <summary>
        /// Queries the derived property Input
        /// </summary>
        public List<Feature> QueryInput()
        {
            throw new NotImplementedException("Derived property Input not yet supported");
        }

        /// <summary>
        /// Queries the derived property IntersectingType
        /// </summary>
        public List<Type> QueryIntersectingType()
        {
            throw new NotImplementedException("Derived property IntersectingType not yet supported");
        }

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Queries the derived property IsConjugated
        /// </summary>
        public bool QueryIsConjugated()
        {
            throw new NotImplementedException("Derived property IsConjugated not yet supported");
        }

        /// <summary>
        /// Whether this Relationship was generated by tooling to meet semantic rules, rather than being
        /// directly created by a modeler.
        /// </summary>
        public bool IsImplied { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether this OccurrenceDefinition is constrained to represent single individual.
        /// </summary>
        public bool IsIndividual { get; set; }

        /// <summary>
        /// Queries the derived property IsLibraryElement
        /// </summary>
        public bool QueryIsLibraryElement()
        {
            throw new NotImplementedException("Derived property IsLibraryElement not yet supported");
        }

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.(A Type gives conditions that must be met by whatever it classifies, but when isSufficient
        /// is false, things may meet those conditions but still not be classified by the Type. For example, a
        /// Type Car that is not sufficient could require everything it classifies to have four wheels, but not
        /// all four wheeled things would need to be cars. However, if the type Car were sufficient, it would
        /// classify all four-wheeled things.)
        /// </summary>
        public bool IsSufficient { get; set; }

        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        public bool IsVariation { get; set; }

        /// <summary>
        /// Queries the derived property LifeClass
        /// </summary>
        public LifeClass QueryLifeClass()
        {
            throw new NotImplementedException("Derived property LifeClass not yet supported");
        }

        /// <summary>
        /// Queries the derived property Member
        /// </summary>
        public List<Element> QueryMember()
        {
            throw new NotImplementedException("Derived property Member not yet supported");
        }

        /// <summary>
        /// Queries the derived property Membership
        /// </summary>
        public List<Membership> QueryMembership()
        {
            throw new NotImplementedException("Derived property Membership not yet supported");
        }

        /// <summary>
        /// Queries the derived property Multiplicity
        /// </summary>
        public Multiplicity QueryMultiplicity()
        {
            throw new NotImplementedException("Derived property Multiplicity not yet supported");
        }

        /// <summary>
        /// The primary name of this Element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Queries the derived property Output
        /// </summary>
        public List<Feature> QueryOutput()
        {
            throw new NotImplementedException("Derived property Output not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedAction
        /// </summary>
        public List<ActionUsage> QueryOwnedAction()
        {
            throw new NotImplementedException("Derived property OwnedAction not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedAllocation
        /// </summary>
        public List<AllocationUsage> QueryOwnedAllocation()
        {
            throw new NotImplementedException("Derived property OwnedAllocation not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedAnalysisCase
        /// </summary>
        public List<AnalysisCaseUsage> QueryOwnedAnalysisCase()
        {
            throw new NotImplementedException("Derived property OwnedAnalysisCase not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedAnnotation
        /// </summary>
        public List<Annotation> QueryOwnedAnnotation()
        {
            throw new NotImplementedException("Derived property OwnedAnnotation not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedAttribute
        /// </summary>
        public List<AttributeUsage> QueryOwnedAttribute()
        {
            throw new NotImplementedException("Derived property OwnedAttribute not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedCalculation
        /// </summary>
        public List<CalculationUsage> QueryOwnedCalculation()
        {
            throw new NotImplementedException("Derived property OwnedCalculation not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedCase
        /// </summary>
        public List<CaseUsage> QueryOwnedCase()
        {
            throw new NotImplementedException("Derived property OwnedCase not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedConcern
        /// </summary>
        public List<ConcernUsage> QueryOwnedConcern()
        {
            throw new NotImplementedException("Derived property OwnedConcern not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedConjugator
        /// </summary>
        public Conjugation QueryOwnedConjugator()
        {
            throw new NotImplementedException("Derived property OwnedConjugator not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedConnection
        /// </summary>
        public List<IConnectorAsUsage> QueryOwnedConnection()
        {
            throw new NotImplementedException("Derived property OwnedConnection not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedConstraint
        /// </summary>
        public List<ConstraintUsage> QueryOwnedConstraint()
        {
            throw new NotImplementedException("Derived property OwnedConstraint not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedDifferencing
        /// </summary>
        public List<Differencing> QueryOwnedDifferencing()
        {
            throw new NotImplementedException("Derived property OwnedDifferencing not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedDisjoining
        /// </summary>
        public List<Disjoining> QueryOwnedDisjoining()
        {
            throw new NotImplementedException("Derived property OwnedDisjoining not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedElement
        /// </summary>
        public List<Element> QueryOwnedElement()
        {
            throw new NotImplementedException("Derived property OwnedElement not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedEndFeature
        /// </summary>
        public List<Feature> QueryOwnedEndFeature()
        {
            throw new NotImplementedException("Derived property OwnedEndFeature not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedEnumeration
        /// </summary>
        public List<EnumerationUsage> QueryOwnedEnumeration()
        {
            throw new NotImplementedException("Derived property OwnedEnumeration not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedFeature
        /// </summary>
        public List<Feature> QueryOwnedFeature()
        {
            throw new NotImplementedException("Derived property OwnedFeature not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedFeatureMembership
        /// </summary>
        public List<FeatureMembership> QueryOwnedFeatureMembership()
        {
            throw new NotImplementedException("Derived property OwnedFeatureMembership not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedFlow
        /// </summary>
        public List<FlowConnectionUsage> QueryOwnedFlow()
        {
            throw new NotImplementedException("Derived property OwnedFlow not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedImport
        /// </summary>
        public List<IImport> QueryOwnedImport()
        {
            throw new NotImplementedException("Derived property OwnedImport not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedInterface
        /// </summary>
        public List<InterfaceUsage> QueryOwnedInterface()
        {
            throw new NotImplementedException("Derived property OwnedInterface not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedIntersecting
        /// </summary>
        public List<Intersecting> QueryOwnedIntersecting()
        {
            throw new NotImplementedException("Derived property OwnedIntersecting not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedItem
        /// </summary>
        public List<ItemUsage> QueryOwnedItem()
        {
            throw new NotImplementedException("Derived property OwnedItem not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedMember
        /// </summary>
        public List<Element> QueryOwnedMember()
        {
            throw new NotImplementedException("Derived property OwnedMember not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedMembership
        /// </summary>
        public List<Membership> QueryOwnedMembership()
        {
            throw new NotImplementedException("Derived property OwnedMembership not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedMetadata
        /// </summary>
        public List<MetadataUsage> QueryOwnedMetadata()
        {
            throw new NotImplementedException("Derived property OwnedMetadata not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedOccurrence
        /// </summary>
        public List<OccurrenceUsage> QueryOwnedOccurrence()
        {
            throw new NotImplementedException("Derived property OwnedOccurrence not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedPart
        /// </summary>
        public List<PartUsage> QueryOwnedPart()
        {
            throw new NotImplementedException("Derived property OwnedPart not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedPort
        /// </summary>
        public List<PortUsage> QueryOwnedPort()
        {
            throw new NotImplementedException("Derived property OwnedPort not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedReference
        /// </summary>
        public List<ReferenceUsage> QueryOwnedReference()
        {
            throw new NotImplementedException("Derived property OwnedReference not yet supported");
        }

        /// <summary>
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        public List<Element> OwnedRelatedElement { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        public List<Relationship> OwnedRelationship { get; set; }

        /// <summary>
        /// Queries the derived property OwnedRendering
        /// </summary>
        public List<RenderingUsage> QueryOwnedRendering()
        {
            throw new NotImplementedException("Derived property OwnedRendering not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedRequirement
        /// </summary>
        public List<RequirementUsage> QueryOwnedRequirement()
        {
            throw new NotImplementedException("Derived property OwnedRequirement not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedSpecialization
        /// </summary>
        public List<Specialization> QueryOwnedSpecialization()
        {
            throw new NotImplementedException("Derived property OwnedSpecialization not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedState
        /// </summary>
        public List<StateUsage> QueryOwnedState()
        {
            throw new NotImplementedException("Derived property OwnedState not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedSubclassification
        /// </summary>
        public List<Subclassification> QueryOwnedSubclassification()
        {
            throw new NotImplementedException("Derived property OwnedSubclassification not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedTransition
        /// </summary>
        public List<TransitionUsage> QueryOwnedTransition()
        {
            throw new NotImplementedException("Derived property OwnedTransition not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedUnioning
        /// </summary>
        public List<Unioning> QueryOwnedUnioning()
        {
            throw new NotImplementedException("Derived property OwnedUnioning not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedUsage
        /// </summary>
        public List<Usage> QueryOwnedUsage()
        {
            throw new NotImplementedException("Derived property OwnedUsage not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedUseCase
        /// </summary>
        public List<UseCaseUsage> QueryOwnedUseCase()
        {
            throw new NotImplementedException("Derived property OwnedUseCase not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedVerificationCase
        /// </summary>
        public List<VerificationCaseUsage> QueryOwnedVerificationCase()
        {
            throw new NotImplementedException("Derived property OwnedVerificationCase not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedView
        /// </summary>
        public List<ViewUsage> QueryOwnedView()
        {
            throw new NotImplementedException("Derived property OwnedView not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedViewpoint
        /// </summary>
        public List<ViewpointUsage> QueryOwnedViewpoint()
        {
            throw new NotImplementedException("Derived property OwnedViewpoint not yet supported");
        }

        /// <summary>
        /// Queries the derived property Owner
        /// </summary>
        public Element QueryOwner()
        {
            throw new NotImplementedException("Derived property Owner not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwningMembership
        /// </summary>
        public OwningMembership QueryOwningMembership()
        {
            throw new NotImplementedException("Derived property OwningMembership not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwningNamespace
        /// </summary>
        public Namespace QueryOwningNamespace()
        {
            throw new NotImplementedException("Derived property OwningNamespace not yet supported");
        }

        /// <summary>
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        public Element OwningRelatedElement { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        public Relationship OwningRelationship { get; set; }

        /// <summary>
        /// Queries the derived property QualifiedName
        /// </summary>
        public string QueryQualifiedName()
        {
            throw new NotImplementedException("Derived property QualifiedName not yet supported");
        }

        /// <summary>
        /// Queries the derived property RelatedElement
        /// </summary>
        public List<Element> QueryRelatedElement()
        {
            throw new NotImplementedException("Derived property RelatedElement not yet supported");
        }

        /// <summary>
        /// Queries the derived property RelatedType
        /// </summary>
        public List<Type> QueryRelatedType()
        {
            throw new NotImplementedException("Derived property RelatedType not yet supported");
        }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// </summary>
        public List<Element> Source { get; set; }

        /// <summary>
        /// Queries the derived property SourceType
        /// </summary>
        public Type QuerySourceType()
        {
            throw new NotImplementedException("Derived property SourceType not yet supported");
        }

        /// <summary>
        /// </summary>
        public List<Element> Target { get; set; }

        /// <summary>
        /// Queries the derived property TargetType
        /// </summary>
        public List<Type> QueryTargetType()
        {
            throw new NotImplementedException("Derived property TargetType not yet supported");
        }

        /// <summary>
        /// Queries the derived property TextualRepresentation
        /// </summary>
        public List<TextualRepresentation> QueryTextualRepresentation()
        {
            throw new NotImplementedException("Derived property TextualRepresentation not yet supported");
        }

        /// <summary>
        /// Queries the derived property UnioningType
        /// </summary>
        public List<Type> QueryUnioningType()
        {
            throw new NotImplementedException("Derived property UnioningType not yet supported");
        }

        /// <summary>
        /// Queries the derived property Usage
        /// </summary>
        public List<Usage> QueryUsage()
        {
            throw new NotImplementedException("Derived property Usage not yet supported");
        }

        /// <summary>
        /// Queries the derived property Variant
        /// </summary>
        public List<Usage> QueryVariant()
        {
            throw new NotImplementedException("Derived property Variant not yet supported");
        }

        /// <summary>
        /// Queries the derived property VariantMembership
        /// </summary>
        public List<VariantMembership> QueryVariantMembership()
        {
            throw new NotImplementedException("Derived property VariantMembership not yet supported");
        }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
