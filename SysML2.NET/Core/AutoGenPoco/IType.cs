// -------------------------------------------------------------------------------------------------
// <copyright file="IType.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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
    /// A Type is a Namespace that is the most general kind of Element supporting the semantics of
    /// classification. A Type may be a Classifier or a Feature, defining conditions on what is classified
    /// by the Type (see also the description of isSufficient).ownedSpecialization =
    /// ownedRelationship->selectByKind(Specialization)->    select(s | s.special = self)    multiplicity = 
    ///    let ownedMultiplicities: Sequence(Multiplicity) =        ownedMember->selectByKind(Multiplicity)
    /// in    if ownedMultiplicities->isEmpty() then null    else ownedMultiplicities->first()   
    /// endifownedFeatureMembership = ownedRelationship->selectByKind(FeatureMembership)ownedConjugator =   
    /// let ownedConjugators: Sequence(Conjugator) =         ownedRelationship->selectByKind(Conjugation) in
    ///    if ownedConjugators->isEmpty() then null     else ownedConjugators->at(1) endifoutput =
    /// feature->select(f |     let direction: FeatureDirectionKind = directionOf(f) in    direction =
    /// FeatureDirectionKind::out or    direction = FeatureDirectionKind::inout)input = feature->select(f | 
    ///    let direction: FeatureDirectionKind = directionOf(f) in    direction =
    /// FeatureDirectionKind::_'in' or    direction = FeatureDirectionKind::inout)inheritedMembership =
    /// inheritedMemberships(Set{})specializesFromLibrary('Base::Anything')directedFeature =
    /// feature->select(f | directionOf(f) <> null)feature =
    /// featureMembership.ownedMemberFeaturefeatureMembership = ownedFeatureMembership->union(   
    /// inheritedMembership->selectByKind(FeatureMembership))ownedFeature =
    /// ownedFeatureMembership.ownedMemberFeaturedifferencingType =
    /// ownedDifferencing.differencingTypeintersectingType->excludes(self)differencingType->excludes(self)unioningType
    /// = ownedUnioning.unioningTypeunioningType->excludes(self)intersectingType =
    /// ownedIntersecting.intersectingTypeownedRelationship->selectByKind(Conjugation)->size() <=
    /// 1ownedMember->selectByKind(Multiplicity)->size() <= 1endFeature =
    /// feature->select(isEnd)ownedDisjoining =    ownedRelationship->selectByKind(Disjoining)ownedUnioning
    /// =   
    /// ownedRelationship->selectByKind(Unioning)ownedRelationship->selectByKind(Intersecting)ownedDifferencing
    /// =    ownedRelationship->selectByKind(Differencing)ownedEndFeature =
    /// ownedFeature->select(isEnd)inheritedFeature = inheritedMemberships->   
    /// selectByKind(FeatureMembership).memberFeatureownedUnioning->size() <> 1ownedIntersecting->size() <>
    /// 1ownedDifferencing->size() <> 1
    /// </summary>
    public partial interface IType : INamespace
    {
        /// <summary>
        /// Queries the derived property DifferencingType
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Type> QueryDifferencingType();

        /// <summary>
        /// Queries the derived property DirectedFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryDirectedFeature();

        /// <summary>
        /// Queries the derived property EndFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryEndFeature();

        /// <summary>
        /// Queries the derived property Feature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryFeature();

        /// <summary>
        /// Queries the derived property FeatureMembership
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<FeatureMembership> QueryFeatureMembership();

        /// <summary>
        /// Queries the derived property InheritedFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryInheritedFeature();

        /// <summary>
        /// Queries the derived property InheritedMembership
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Membership> QueryInheritedMembership();

        /// <summary>
        /// Queries the derived property Input
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryInput();

        /// <summary>
        /// Queries the derived property IntersectingType
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Type> QueryIntersectingType();

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsAbstract { get; set; }

        /// <summary>
        /// Queries the derived property IsConjugated
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool QueryIsConjugated();

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.(A Type gives conditions that must be met by whatever it classifies, but when isSufficient
        /// is false, things may meet those conditions but still not be classified by the Type. For example, a
        /// Type Car that is not sufficient could require everything it classifies to have four wheels, but not
        /// all four wheeled things would classify as cars. However, if the Type Car were sufficient, it would
        /// classify all four-wheeled things.)
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsSufficient { get; set; }

        /// <summary>
        /// Queries the derived property Multiplicity
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Multiplicity QueryMultiplicity();

        /// <summary>
        /// Queries the derived property Output
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryOutput();

        /// <summary>
        /// Queries the derived property OwnedConjugator
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Conjugation QueryOwnedConjugator();

        /// <summary>
        /// Queries the derived property OwnedDifferencing
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Differencing> QueryOwnedDifferencing();

        /// <summary>
        /// Queries the derived property OwnedDisjoining
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Disjoining> QueryOwnedDisjoining();

        /// <summary>
        /// Queries the derived property OwnedEndFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryOwnedEndFeature();

        /// <summary>
        /// Queries the derived property OwnedFeature
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Feature> QueryOwnedFeature();

        /// <summary>
        /// Queries the derived property OwnedFeatureMembership
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<FeatureMembership> QueryOwnedFeatureMembership();

        /// <summary>
        /// Queries the derived property OwnedIntersecting
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Intersecting> QueryOwnedIntersecting();

        /// <summary>
        /// Queries the derived property OwnedSpecialization
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Specialization> QueryOwnedSpecialization();

        /// <summary>
        /// Queries the derived property OwnedUnioning
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Unioning> QueryOwnedUnioning();

        /// <summary>
        /// Queries the derived property UnioningType
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Type> QueryUnioningType();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
