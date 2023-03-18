// -------------------------------------------------------------------------------------------------
// <copyright file="Feature.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Feature is a Type that classifies relations between multiple things (in the universe). The domain
    /// of the relation is the intersection of the featuringTypes of the Feature. (The domain of a Feature
    /// with no featuringTyps is implicitly the most general Type Base::Anything from the Kernel Semantic
    /// Library.) The co-domain of the relation is the intersection of the types of the Feature.In the
    /// simplest cases, the featuringTypes and types are Classifiers and the Feature relates two things, one
    /// from the domain and one from the range. Examples include cars paired with wheels, people paired with
    /// other people, and cars paired with numbers representing the car length.Since Features are Types,
    /// their featuringTypes and types can be Features. In this case, the Feature effectively classifies
    /// relations between relations, which can be interpreted as the sequence of things related by the
    /// domain Feature concatenated with the sequence of things related by the co-domain Feature.The values
    /// of a Feature for a given instance of its domain are all the instances of its co-domain that are
    /// related to that domain instance by the Feature. The values of a Feature with chainingFeatures are
    /// the same as values of the last Feature in the chain, which can be found by starting with values of
    /// the first Feature, then using those values as domain instances to obtain valus of the second
    /// Feature, and so on, to values of the last Feature.ownedRedefinition =
    /// ownedSubsetting->selectByKind(Redefinition)ownedTypeFeaturing =
    /// ownedRelationship->selectByKind(TypeFeaturing)->    select(tf | tf.featureOfType =
    /// self)ownedSubsetting = ownedSpecialization->selectByKind(Subsetting)ownedTyping =
    /// ownedGeneralization->selectByKind(FeatureTyping)type =    let types : OrderedSet(Type) =
    /// typing.type->        union(subsetting.subsettedFeature.type)->        asOrderedSet() in    if
    /// chainingFeature->isEmpty() then types    else         types->union(chainingFeature->last().type)->  
    ///      asOrderedSet()    endifmultiplicity <> null implies multiplicity.featuringType = featuringType
    /// specializesFromLibrary("Base::things")chainingFeatures->excludes(self)ownedFeatureChaining =
    /// ownedRelationship->selectByKind(FeatureChaining)chainingFeature =
    /// ownedFeatureChaining.chainingFeaturechainingFeatures->size() <> 1isEnd and owningType <> null
    /// implies    let i : Integer =         owningType.ownedFeature->select(isEnd) in   
    /// owningType.ownedSpecialization.general->        forAll(supertype |            let ownedEndFeatures :
    /// Sequence(Feature) =                 supertype.ownedFeature->select(isEnd) in           
    /// ownedEndFeatures->size() >= i implies               
    /// redefines(ownedEndFeatures->at(i))ownedMembership->    selectByKind(FeatureValue)->    forAll(fv |
    /// specializes(fv.value.result))isEnd and owningType <> null andowningType.oclIsKindOf(Association)
    /// implies    specializesFromLibrary("Links::Link::participants")isComposite
    /// andownedTyping.type->includes(oclIsKindOf(Structure)) andowningType <> null
    /// and(owningType.oclIsKindOf(Structure) or owningType.type->includes(oclIsKindOf(Structure))) implies 
    ///   specializesFromLibrary("Occurrence::Occurrence::suboccurrences")owningType <> null
    /// and(owningType.oclIsKindOf(LiteralExpression) or owningType.oclIsKindOf(FeatureReferenceExpression))
    /// implies    if owningType.oclIsKindOf(LiteralString) then       
    /// specializesFromLibrary("ScalarValues::String")    else if owningType.oclIsKindOf(LiteralBoolean)
    /// then        specializesFromLibrary("ScalarValues::Boolean")    else if
    /// owningType.oclIsKindOf(LiteralInteger) then        specializesFromLibrary("ScalarValues::Rational") 
    ///   else if owningType.oclIsKindOf(LiteralBoolean) then       
    /// specializesFromLibrary("ScalarValues::Rational")    else if owningType.oclIsKindOf(LiteralBoolean)
    /// then        specializesFromLibrary("ScalarValues::Real")    else specializes(       
    /// owningType.oclAsType(FeatureReferenceExpression).referent)    endif endif endif endif
    /// endifownedTyping.type->exists(selectByKind(Class)) implies   
    /// specializesFromLibrary("Occurrences::occurrences")isComposite
    /// andownedTyping.type->includes(oclIsKindOf(Class)) andowningType <> null
    /// and(owningType.oclIsKindOf(Class) or owningType.oclIsKindOf(Feature) and   
    /// owningType.oclAsType(Feature).type->        exists(oclIsKindOf(Class))) implies   
    /// specializesFromLibrary("Occurrence::Occurrence::suboccurrences")ownedTyping.type->exists(selectByKind(DataType))
    /// implies    specializesFromLibary("Base::dataValues")owningType <> null
    /// andowningType.oclIsKindOf(ItemFlowEnd) andowningType.ownedFeature->at(1) = self implies    let
    /// flowType : Type = owningType.owningType in    flowType <> null implies        let i : Integer =     
    ///        flowType.ownedFeature.indexOf(owningType) in        (i = 1 implies            
    /// redefinesFromLibrary("Transfers::Transfer::source::sourceOutput")) and        (i = 2 implies        
    ///    redefinesFromLibrary("Transfers::Transfer::source::targetInput"))                 owningType <>
    /// null and(owningType.oclIsKindOf(Behavior) or owningType.oclIsKindOf(Step)) implies    let i :
    /// Integer =         owningType.ownedFeature->select(direction <> null) in   
    /// owningType.ownedSpecialization.general->        forAll(supertype |            let ownedParameters :
    /// Sequence(Feature) =                 supertype.ownedFeature->select(direction <> null) in           
    /// ownedParameters->size() >= i implies               
    /// redefines(ownedParameters->at(i))ownedTyping.type->exists(selectByKind(Structure)) implies   
    /// specializesFromLibary("Objects::objects")owningType <> null and(owningType.oclIsKindOf(Function) and
    ///    self = owningType.oclAsType(Function).result or owningType.oclIsKindOf(Expression) and    self =
    /// owningType.oclAsType(Expression).result) implies    owningType.ownedSpecialization.general->       
    /// select(oclIsKindOf(Function) or oclIsKindOf(Expression))->        forAll(supertype |           
    /// redefines(                if superType.oclIsKindOf(Function) then                   
    /// superType.oclAsType(Function).result                else                   
    /// superType.oclAsType(Expression).result                endif)ownedFeatureInverting =
    /// ownedRelationship->selectByKind(FeatureInverting)->    select(fi | fi.featureInverted =
    /// self)featuringType =    let featuringTypes : OrderedSet(Type) =        
    /// typeFeaturing.featuringType->asOrderedSet() in    if chainingFeature->isEmpty() then featuringTypes 
    ///   else        featuringTypes->            union(chainingFeature->first().featuringType)->           
    /// asOrderedSet()    endifownedReferenceSubsetting =    let referenceSubsettings :
    /// OrderedSet(ReferenceSubsetting) =        ownedSubsetting->selectByKind(ReferenceSubsetting) in    if
    /// referenceSubsettings->isEmpty() then null    else referenceSubsettings->first()
    /// endifownedSubsetting->selectByKind(ReferenceSubsetting)->size() <= 1
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
        /// Indicates how values of this Feature are determined or used (as specified for the
        /// FeatureDirectionKind).
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public FeatureDirectionKind? Direction { get; set; }

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
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after its featuring instance no longer does.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsComposite { get; set; }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Feature.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsDerived { get; set; }

        /// <summary>
        /// Whether or not the this Feature is an end Feature, requiring a different interpretation of the
        /// multiplicity of the Feature.An end Feature is always considered to map each domain instance to a
        /// single co-domain instance, whether or not a Multiplicity is given for it. If a Multiplicity is given
        /// for an end Feature, rather than giving the co-domain cardinality for the Feature as usual, it
        /// specifies a cardinality constraint for navigating across the endFeatures of the featuringType of the
        /// end Feature. That is, if a Type has n endFeatures, then the Multiplicity of any one of those end
        /// Features constrains the cardinality of the set of values of that Feature when the values of the
        /// other n-1 end Features are held fixed.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsEnd { get; set; }

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
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the domain of
        /// the Feature and represent the same thing as those instances.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsPortion { get; set; }

        /// <summary>
        /// Whether the values of this Feature can change over the lifetime of an instance of the domain.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsReadOnly { get; set; }

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
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsUnique { get; set; }

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
