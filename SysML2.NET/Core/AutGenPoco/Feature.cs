// -------------------------------------------------------------------------------------------------
// <copyright file="Feature.cs" company="RHEA System S.A.">
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
    /// A Feature is a Type that classifies sequences of multiple things (in the universe). These must
    /// concatenate a sequence drawn from the intersection of the Feature&#39;s featuringTypes (domain) with
    /// a sequence drawn from the intersection of its types (co-domain), treating (co)domains as sets of
    /// sequences. The domain of Features that do not have any featuringTypes is the same as if it were the
    /// library Type Anything. A Feature&#39;s types include at least Anything, which can be narrowed to
    /// other Classifiers by Redefinition.In the simplest cases, a Feature&#39;s featuringTypes and types
    /// are Classifiers, its sequences being pairs (length = 2), with the first element drawn from the
    /// Feature&#39;s domain and the second element from its co-domain (the Feature &quot;value&quot;).
    /// Examples include cars paired with wheels, people paired with other people, and cars paired with
    /// numbers representing the car length.Since Features are Types, their featuringTypes and types
    /// can be Features. When both are, Features classify sequences of at least four elements (length &gt;
    /// 3), otherwise at least three (length &gt; 2). The featuringTypes of nested Features are Features.The
    /// values of a Feature with chainingFeatures are the same as values of the last Feature in the chain,
    /// which can be found by starting with values of the first Feature, then from those values to values of
    /// the second feature, and so on, to values of the last feature.ownedRedefinition =
    /// ownedSubsetting->selectByKind(Redefinition)ownedTypeFeaturing =
    /// ownedRelationship->selectByKind(TypeFeaturing)->    select(tf | tf.featureOfType =
    /// self)ownedSubsetting = ownedGeneralization->selectByKind(Subsetting)isComposite =
    /// owningFeatureMembership <> null and owningFeatureMembership.isCompositeownedTyping =
    /// ownedGeneralization->selectByKind(FeatureTyping)isEnd = owningFeatureMembership <> null and
    /// owningFeatureMembership.oclIsKindOf(EndFeatureMembership)multiplicity <> null implies
    /// multiplicity.featuringType = featuringType
    /// allSupertypes()->includes(resolve("Base::things"))chainingFeatures->excludes(self)ownedFeatureChaining
    /// = ownedRelationship->selectByKind(FeatureChaining)chainingFeature =
    /// ownedFeatureChaining.chainingFeaturechainingFeatures->size() <> 1inverseFeature =
    /// invertingFeatureInverting.featureInverseinvertedFeature =
    /// invertedFeatureInverting.featureInvertedownedTyping.type->exists(selectByKind(Class)) implies   
    /// allSupertypes()->includes(resolve("Occurrences::occurrences"))ownedTyping.type->exists(selectByKind(DataType))
    /// implies    allSupertypes()->includes(resolve("Base::dataValues"))
    /// </summary>
    public partial class Feature : IFeature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feature"/> class.
        /// </summary>
        public Feature()
        {
            this.AliasIds = new List<string>();
            this.IsAbstract = false;
            this.IsComposite = false;
            this.IsDerived = false;
            this.IsEnd = false;
            this.IsImpliedIncluded = false;
            this.IsOrdered = false;
            this.IsPortion = false;
            this.IsReadOnly = false;
            this.IsSufficient = false;
            this.IsUnique = true;
            this.OwnedRelationship = new List<Relationship>();
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
        /// Queries the derived property ChainingFeature
        /// </summary>
        public List<Feature> QueryChainingFeature()
        {
            throw new NotImplementedException("Derived property ChainingFeature not yet supported");
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
        /// Determines how values of this Feature are determined or used (see FeatureDirectionKind).
        /// </summary>
        public FeatureDirectionKind? Direction { get; set; }

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
        /// Queries the derived property EndOwningType
        /// </summary>
        public Type QueryEndOwningType()
        {
            throw new NotImplementedException("Derived property EndOwningType not yet supported");
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
        /// Queries the derived property FeaturingType
        /// </summary>
        public List<Type> QueryFeaturingType()
        {
            throw new NotImplementedException("Derived property FeaturingType not yet supported");
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
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after the instance of the featuringType no longer does..
        /// </summary>
        public bool IsComposite { get; set; }

        /// <summary>
        /// Queries the derived property IsConjugated
        /// </summary>
        public bool QueryIsConjugated()
        {
            throw new NotImplementedException("Derived property IsConjugated not yet supported");
        }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Features.
        /// </summary>
        public bool IsDerived { get; set; }

        /// <summary>
        /// Whether or not the this Feature is an end Feature, requiring a different interpretation of the
        /// multiplicity of the Feature.An end Feature is always considered to map each domain entity to a
        /// single co-domain entity, whether or not a Multiplicity is given for it. If a Multiplicity is given
        /// for an end Feature, rather than giving the co-domain cardinality for the Feature as usual, it
        /// specifies a cardinality constraint for navigating across the endFeatures of the featuringType of the
        /// end Feature. That is, if a Type has n endFeatures, then the Multiplicity of any one of those end
        /// Features constrains the cardinality of the set of values of that Feature when the values of the
        /// other n-1 end Features are held fixed.
        /// </summary>
        public bool IsEnd { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Queries the derived property IsLibraryElement
        /// </summary>
        public bool QueryIsLibraryElement()
        {
            throw new NotImplementedException("Derived property IsLibraryElement not yet supported");
        }

        /// <summary>
        /// Queries the derived property IsNonunique
        /// </summary>
        public bool QueryIsNonunique()
        {
            throw new NotImplementedException("Derived property IsNonunique not yet supported");
        }

        /// <summary>
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        public bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the
        /// Feature&#39;s domain.
        /// </summary>
        public bool IsPortion { get; set; }

        /// <summary>
        /// Whether the values of this Feature can change over the lifetime of an instance of the domain.
        /// </summary>
        public bool IsReadOnly { get; set; }

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
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        public bool IsUnique { get; set; }

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
        /// Queries the derived property OwnedAnnotation
        /// </summary>
        public List<Annotation> QueryOwnedAnnotation()
        {
            throw new NotImplementedException("Derived property OwnedAnnotation not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedConjugator
        /// </summary>
        public Conjugation QueryOwnedConjugator()
        {
            throw new NotImplementedException("Derived property OwnedConjugator not yet supported");
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
        /// Queries the derived property OwnedFeature
        /// </summary>
        public List<Feature> QueryOwnedFeature()
        {
            throw new NotImplementedException("Derived property OwnedFeature not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedFeatureChaining
        /// </summary>
        public List<FeatureChaining> QueryOwnedFeatureChaining()
        {
            throw new NotImplementedException("Derived property OwnedFeatureChaining not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedFeatureInverting
        /// </summary>
        public List<FeatureInverting> QueryOwnedFeatureInverting()
        {
            throw new NotImplementedException("Derived property OwnedFeatureInverting not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedFeatureMembership
        /// </summary>
        public List<FeatureMembership> QueryOwnedFeatureMembership()
        {
            throw new NotImplementedException("Derived property OwnedFeatureMembership not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedImport
        /// </summary>
        public List<Import> QueryOwnedImport()
        {
            throw new NotImplementedException("Derived property OwnedImport not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedIntersecting
        /// </summary>
        public List<Intersecting> QueryOwnedIntersecting()
        {
            throw new NotImplementedException("Derived property OwnedIntersecting not yet supported");
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
        /// Queries the derived property OwnedRedefinition
        /// </summary>
        public List<Redefinition> QueryOwnedRedefinition()
        {
            throw new NotImplementedException("Derived property OwnedRedefinition not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedReferenceSubsetting
        /// </summary>
        public ReferenceSubsetting QueryOwnedReferenceSubsetting()
        {
            throw new NotImplementedException("Derived property OwnedReferenceSubsetting not yet supported");
        }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        public List<Relationship> OwnedRelationship { get; set; }

        /// <summary>
        /// Queries the derived property OwnedSpecialization
        /// </summary>
        public List<Specialization> QueryOwnedSpecialization()
        {
            throw new NotImplementedException("Derived property OwnedSpecialization not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedSubsetting
        /// </summary>
        public List<Subsetting> QueryOwnedSubsetting()
        {
            throw new NotImplementedException("Derived property OwnedSubsetting not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedTypeFeaturing
        /// </summary>
        public List<TypeFeaturing> QueryOwnedTypeFeaturing()
        {
            throw new NotImplementedException("Derived property OwnedTypeFeaturing not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedTyping
        /// </summary>
        public List<FeatureTyping> QueryOwnedTyping()
        {
            throw new NotImplementedException("Derived property OwnedTyping not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwnedUnioning
        /// </summary>
        public List<Unioning> QueryOwnedUnioning()
        {
            throw new NotImplementedException("Derived property OwnedUnioning not yet supported");
        }

        /// <summary>
        /// Queries the derived property Owner
        /// </summary>
        public Element QueryOwner()
        {
            throw new NotImplementedException("Derived property Owner not yet supported");
        }

        /// <summary>
        /// Queries the derived property OwningFeatureMembership
        /// </summary>
        public FeatureMembership QueryOwningFeatureMembership()
        {
            throw new NotImplementedException("Derived property OwningFeatureMembership not yet supported");
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
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        public Relationship OwningRelationship { get; set; }

        /// <summary>
        /// Queries the derived property OwningType
        /// </summary>
        public Type QueryOwningType()
        {
            throw new NotImplementedException("Derived property OwningType not yet supported");
        }

        /// <summary>
        /// Queries the derived property QualifiedName
        /// </summary>
        public string QueryQualifiedName()
        {
            throw new NotImplementedException("Derived property QualifiedName not yet supported");
        }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Queries the derived property TextualRepresentation
        /// </summary>
        public List<TextualRepresentation> QueryTextualRepresentation()
        {
            throw new NotImplementedException("Derived property TextualRepresentation not yet supported");
        }

        /// <summary>
        /// Queries the derived property Type
        /// </summary>
        public List<Type> QueryType()
        {
            throw new NotImplementedException("Derived property Type not yet supported");
        }

        /// <summary>
        /// Queries the derived property UnioningType
        /// </summary>
        public List<Type> QueryUnioningType()
        {
            throw new NotImplementedException("Derived property UnioningType not yet supported");
        }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
