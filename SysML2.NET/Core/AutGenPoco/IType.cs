// -------------------------------------------------------------------------------------------------
// <copyright file="IType.cs" company="RHEA System S.A.">
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
    /// A Type is a Namespace that is the most general kind of Element supporting the semantics of
    /// classification. A Type may be a Classifier or a Feature, defining conditions on what is classified
    /// by the Type (see also the description of isSufficient).ownedSpecialization =
    /// ownedRelationship->selectByKind(Specialization)->    select(g | g.special = self)    multiplicity =
    /// feature->select(oclIsKindOf(Multiplicity))ownedFeatureMembership =
    /// ownedRelationship->selectByKind(FeatureMembership)let ownedConjugators: Sequence(Conjugator) =    
    /// ownedRelationship->selectByKind(Conjugation) in    ownedConjugators->size() = 1 and   
    /// ownedConjugator = ownedConjugators->at(1)output =    if isConjugated then        
    /// conjugator.originalType.input    else         feature->select(direction = out or direction = inout) 
    ///   endifinput =     if isConjugated then         conjugator.originalType.output    else        
    /// feature->select(direction = _'in' or direction = inout)    endifinheritedMembership =
    /// inheritedMemberships(Set{})ownedFeature =
    /// ownedFeatureMembership.ownedMemberFeatureintersectingType->excludes(self)differencingType =
    /// ownedDifferencing.differencingTypeallSupertypes()->includes(Kernel Library::Anything)disjointType =
    /// disjoiningTypeDisjoining.disjoiningTypeintersectingType =
    /// ownedIntersecting.intersectingTypeunioningType = ownedUnioning.unioningTypedirectedFeature =
    /// feature->select(direction <> null)differencingType->excludes(self)featureMembership =
    /// ownedMembership->union(    inheritedMembership->selectByKind(FeatureMembership))feature =
    /// featureMembership.ownedMemberFeatureunioningType->excludes(self)
    /// </summary>
    public partial interface IType : INamespace
    {
        /// <summary>
        /// Queries the derived property DifferencingType
        /// </summary>
        List<Type> QueryDifferencingType();

        /// <summary>
        /// Queries the derived property DirectedFeature
        /// </summary>
        List<Feature> QueryDirectedFeature();

        /// <summary>
        /// Queries the derived property EndFeature
        /// </summary>
        List<Feature> QueryEndFeature();

        /// <summary>
        /// Queries the derived property Feature
        /// </summary>
        List<Feature> QueryFeature();

        /// <summary>
        /// Queries the derived property FeatureMembership
        /// </summary>
        List<FeatureMembership> QueryFeatureMembership();

        /// <summary>
        /// Queries the derived property InheritedFeature
        /// </summary>
        List<Feature> QueryInheritedFeature();

        /// <summary>
        /// Queries the derived property InheritedMembership
        /// </summary>
        List<Membership> QueryInheritedMembership();

        /// <summary>
        /// Queries the derived property Input
        /// </summary>
        List<Feature> QueryInput();

        /// <summary>
        /// Queries the derived property IntersectingType
        /// </summary>
        List<Type> QueryIntersectingType();

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        bool IsAbstract { get; set; }

        /// <summary>
        /// Queries the derived property IsConjugated
        /// </summary>
        bool QueryIsConjugated();

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.(A Type gives conditions that must be met by whatever it classifies, but when isSufficient
        /// is false, things may meet those conditions but still not be classified by the Type. For example, a
        /// Type Car that is not sufficient could require everything it classifies to have four wheels, but not
        /// all four wheeled things would need to be cars. However, if the type Car were sufficient, it would
        /// classify all four-wheeled things.)
        /// </summary>
        bool IsSufficient { get; set; }

        /// <summary>
        /// Queries the derived property Multiplicity
        /// </summary>
        Multiplicity QueryMultiplicity();

        /// <summary>
        /// Queries the derived property Output
        /// </summary>
        List<Feature> QueryOutput();

        /// <summary>
        /// Queries the derived property OwnedConjugator
        /// </summary>
        Conjugation QueryOwnedConjugator();

        /// <summary>
        /// Queries the derived property OwnedDifferencing
        /// </summary>
        List<Differencing> QueryOwnedDifferencing();

        /// <summary>
        /// Queries the derived property OwnedDisjoining
        /// </summary>
        List<Disjoining> QueryOwnedDisjoining();

        /// <summary>
        /// Queries the derived property OwnedEndFeature
        /// </summary>
        List<Feature> QueryOwnedEndFeature();

        /// <summary>
        /// Queries the derived property OwnedFeature
        /// </summary>
        List<Feature> QueryOwnedFeature();

        /// <summary>
        /// Queries the derived property OwnedFeatureMembership
        /// </summary>
        List<FeatureMembership> QueryOwnedFeatureMembership();

        /// <summary>
        /// Queries the derived property OwnedIntersecting
        /// </summary>
        List<Intersecting> QueryOwnedIntersecting();

        /// <summary>
        /// Queries the derived property OwnedSpecialization
        /// </summary>
        List<Specialization> QueryOwnedSpecialization();

        /// <summary>
        /// Queries the derived property OwnedUnioning
        /// </summary>
        List<Unioning> QueryOwnedUnioning();

        /// <summary>
        /// Queries the derived property UnioningType
        /// </summary>
        List<Type> QueryUnioningType();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
