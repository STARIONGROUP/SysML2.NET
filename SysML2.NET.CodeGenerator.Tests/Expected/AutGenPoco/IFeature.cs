// -------------------------------------------------------------------------------------------------
// <copyright file="IFeature.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO
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
    /// ownedGeneralization->selectByKind(FeatureTyping)type =     let types : OrderedSet(Types) =
    /// OrderedSet{self}->        -- Note: The closure operation automatically handles circular
    /// relationships.        closure(typingFeatures()).typing.type->asOrderedSet() in    types->reject(t1 |
    /// types->exist(t2 | t2 <> t1 and t2.specializes(t1)))multiplicity <> null implies
    /// multiplicity.featuringType = featuringType
    /// specializesFromLibrary('Base::things')chainingFeature->excludes(self)ownedFeatureChaining =
    /// ownedRelationship->selectByKind(FeatureChaining)chainingFeature =
    /// ownedFeatureChaining.chainingFeaturechainingFeature->size() <> 1isEnd and owningType <> null implies
    ///    let i : Integer =         owningType.ownedEndFeature->indexOf(self) in   
    /// owningType.ownedSpecialization.general->        forAll(supertype |            
    /// supertype.endFeature->size() >= i implies               
    /// redefines(supertype.endFeature->at(i))direction = null andownedSpecializations->forAll(isImplied)
    /// implies    ownedMembership->        selectByKind(FeatureValue)->        forAll(fv |
    /// specializes(fv.value.result))isEnd and owningType <> null and(owningType.oclIsKindOf(Association) or
    /// owningType.oclIsKindOf(Connector)) implies   
    /// specializesFromLibrary('Links::Link::participant')isComposite
    /// andownedTyping.type->includes(oclIsKindOf(Structure)) andowningType <> null
    /// and(owningType.oclIsKindOf(Structure) or owningType.type->includes(oclIsKindOf(Structure))) implies 
    ///   specializesFromLibrary('Occurrence::Occurrence::suboccurrences')owningType <> null
    /// andowningType.oclIsKindOf(FeatureReferenceExpression) andself =
    /// owningType.oclAsType(FeatureReferenceExpression).result implies   
    /// specializes(owningType.oclAsType(FeatureReferenceExpression).referent)ownedTyping.type->exists(selectByKind(Class))
    /// implies    specializesFromLibrary('Occurrences::occurrences')isComposite
    /// andownedTyping.type->includes(oclIsKindOf(Class)) andowningType <> null
    /// and(owningType.oclIsKindOf(Class) or owningType.oclIsKindOf(Feature) and   
    /// owningType.oclAsType(Feature).type->        exists(oclIsKindOf(Class))) implies   
    /// specializesFromLibrary('Occurrence::Occurrence::suboccurrences')ownedTyping.type->exists(selectByKind(DataType))
    /// implies    specializesFromLibrary('Base::dataValues')owningType <> null
    /// andowningType.oclIsKindOf(ItemFlowEnd) andowningType.ownedFeature->at(1) = self implies    let
    /// flowType : Type = owningType.owningType in    flowType <> null implies        let i : Integer =     
    ///        flowType.ownedFeature.indexOf(owningType) in        (i = 1 implies            
    /// redefinesFromLibrary('Transfers::Transfer::source::sourceOutput')) and        (i = 2 implies        
    ///    redefinesFromLibrary('Transfers::Transfer::source::targetInput'))                 owningType <>
    /// null and(owningType.oclIsKindOf(Behavior) or owningType.oclIsKindOf(Step)) implies    let i :
    /// Integer =         owningType.ownedFeature->select(direction <> null) in   
    /// owningType.ownedSpecialization.general->        forAll(supertype |            let ownedParameters :
    /// Sequence(Feature) =                 supertype.ownedFeature->select(direction <> null) in           
    /// ownedParameters->size() >= i implies               
    /// redefines(ownedParameters->at(i))ownedTyping.type->exists(selectByKind(Structure)) implies   
    /// specializesFromLibary('Objects::objects')owningType <> null and(owningType.oclIsKindOf(Function) and
    ///    self = owningType.oclAsType(Function).result or owningType.oclIsKindOf(Expression) and    self =
    /// owningType.oclAsType(Expression).result) implies    owningType.ownedSpecialization.general->       
    /// select(oclIsKindOf(Function) or oclIsKindOf(Expression))->        forAll(supertype |           
    /// redefines(                if superType.oclIsKindOf(Function) then                   
    /// superType.oclAsType(Function).result                else                   
    /// superType.oclAsType(Expression).result                endif)ownedFeatureInverting =
    /// ownedRelationship->selectByKind(FeatureInverting)->    select(fi | fi.featureInverted =
    /// self)featuringType =    let featuringTypes : OrderedSet(Type) =        
    /// featuring.type->asOrderedSet() in    if chainingFeature->isEmpty() then featuringTypes    else      
    ///  featuringTypes->            union(chainingFeature->first().featuringType)->           
    /// asOrderedSet()    endifownedReferenceSubsetting =    let referenceSubsettings :
    /// OrderedSet(ReferenceSubsetting) =        ownedSubsetting->selectByKind(ReferenceSubsetting) in    if
    /// referenceSubsettings->isEmpty() then null    else referenceSubsettings->first()
    /// endifownedSubsetting->selectByKind(ReferenceSubsetting)->size() <=
    /// 1Sequence{1..chainingFeature->size() - 1}->forAll(i |    chainingFeature->at(i +
    /// 1).featuringType->forAll(t |         chainingFeature->at(i).specializes(t)))isPortion
    /// andownedTyping.type->includes(oclIsKindOf(Class)) andowningType <> null
    /// and(owningType.oclIsKindOf(Class) or owningType.oclIsKindOf(Feature) and   
    /// owningType.oclAsType(Feature).type->        exists(oclIsKindOf(Class))) implies   
    /// specializesFromLibrary('Occurrence::Occurrence::portions')
    /// </summary>
    public partial interface IFeature : IType
    {
        /// <summary>
        /// Queries the derived property ChainingFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: false, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryChainingFeature();

        /// <summary>
        /// Indicates how values of this Feature are determined or used (as specified for the
        /// FeatureDirectionKind).
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        FeatureDirectionKind? Direction { get; set; }

        /// <summary>
        /// Queries the derived property EndOwningType
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Type QueryEndOwningType();

        /// <summary>
        /// Queries the derived property FeaturingType
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Type> QueryFeaturingType();

        /// <summary>
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after its featuring instance no longer does.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsComposite { get; set; }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Features.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsDerived { get; set; }

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
        bool IsEnd { get; set; }

        /// <summary>
        /// Queries the derived property IsNonunique
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool QueryIsNonunique();

        /// <summary>
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the domain of
        /// the Feature and represent the same thing as those instances.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsPortion { get; set; }

        /// <summary>
        /// Whether the values of this Feature can change over the lifetime of an instance of the domain.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsUnique { get; set; }

        /// <summary>
        /// Queries the derived property OwnedFeatureChaining
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<FeatureChaining> QueryOwnedFeatureChaining();

        /// <summary>
        /// Queries the derived property OwnedFeatureInverting
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<FeatureInverting> QueryOwnedFeatureInverting();

        /// <summary>
        /// Queries the derived property OwnedRedefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Redefinition> QueryOwnedRedefinition();

        /// <summary>
        /// Queries the derived property OwnedReferenceSubsetting
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ReferenceSubsetting QueryOwnedReferenceSubsetting();

        /// <summary>
        /// Queries the derived property OwnedSubsetting
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Subsetting> QueryOwnedSubsetting();

        /// <summary>
        /// Queries the derived property OwnedTypeFeaturing
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<TypeFeaturing> QueryOwnedTypeFeaturing();

        /// <summary>
        /// Queries the derived property OwnedTyping
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<FeatureTyping> QueryOwnedTyping();

        /// <summary>
        /// Queries the derived property OwningFeatureMembership
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        FeatureMembership QueryOwningFeatureMembership();

        /// <summary>
        /// Queries the derived property OwningType
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Type QueryOwningType();

        /// <summary>
        /// Queries the derived property Type
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Type> QueryType();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
