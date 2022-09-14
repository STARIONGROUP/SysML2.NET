// -------------------------------------------------------------------------------------------------
// <copyright file="Type.cs" company="RHEA System S.A.">
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
    public partial class Type : IType
    {
        public Type()
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
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        public List<string> AliasIds { get; set; }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        public bool IsImpliedIncluded { get; set; }

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
        /// The primary name of this Element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        public List<Guid> OwnedRelationship { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        public Guid? OwningRelationship { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        public string ShortName { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
