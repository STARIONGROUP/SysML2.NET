// -------------------------------------------------------------------------------------------------
// <copyright file="IType.cs" company="RHEA System S.A.">
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

    /// <summary>
    /// A Type is a Namespace that is the most general kind of Element supporting the semantics of
    /// classification. A Type may be a Classifier or a Feature, defining conditions on what is classified
    /// by the Type (see also the description of isSufficient).ownedSpecialization =
    /// ownedRelationship->selectByKind(Specialization)->    select(g | g.special = self)    multiplicity =
    /// feature->select(oclIsKindOf(Multiplicity))ownedFeatureMembership =
    /// ownedRelationship->selectByKind(FeatureMembership)let ownedConjugators: Sequence(Conjugator) =    
    /// ownedRelationship->selectByKind(Conjugation) in    ownedConjugator =         if
    /// ownedConjugators->isEmpty() then null         else ownedConjugators->at(1) endifoutput =    if
    /// isConjugated then         conjugator.originalType.input    else         feature->select(direction =
    /// out or direction = inout)    endifinput =     if isConjugated then        
    /// conjugator.originalType.output    else         feature->select(direction = _'in' or direction =
    /// inout)    endifinheritedMembership = inheritedMemberships(Set{})disjointType =
    /// disjoiningTypeDisjoining.disjoiningTypeallSupertypes()->includes(resolve("Base::Anything"))directedFeature
    /// = feature->select(direction <> null)feature = featureMembership.ownedMemberFeaturefeatureMembership
    /// = ownedMembership->union(    inheritedMembership->selectByKind(FeatureMembership))ownedFeature =
    /// ownedFeatureMembership.ownedMemberFeatureintersectingType->excludes(self)unioningType->excludes(self)differencingType->excludes(self)differencingType
    /// = ownedDifferencing.differencingTypeunioningType = ownedUnioning.unioningTypeintersectingType =
    /// ownedIntersecting.intersectingTypeownedRelationship->selectByKind(Conjugator)->size() <= 1
    /// </summary>
    public partial interface IType : INamespace
    {
        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        bool IsAbstract { get; set; }

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.(A Type gives conditions that must be met by whatever it classifies, but when isSufficient
        /// is false, things may meet those conditions but still not be classified by the Type. For example, a
        /// Type Car that is not sufficient could require everything it classifies to have four wheels, but not
        /// all four wheeled things would need to be cars. However, if the type Car were sufficient, it would
        /// classify all four-wheeled things.)
        /// </summary>
        bool IsSufficient { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
