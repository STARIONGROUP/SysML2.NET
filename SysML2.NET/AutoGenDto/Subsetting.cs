// -------------------------------------------------------------------------------------------------
// <copyright file="Subsetting.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Subsetting is Generalization in which the specific and general Types that are Features. This means
    /// all values of the subsettingFeature (on instances of its domain, i.e., the intersection of its
    /// featuringTypes) are values of the subsettedFeature on instances of its domain.  To support
    /// this, the domain of the subsettingFeature must be the same or specialize (at least
    /// indirectly) the domain of the subsettedFeature (via Generalization), and the range
    /// (intersection of a Feature&#39;s types) of the subsettingFeature must specialize the range of the
    /// subsettedFeature. The subsettedFeature is imported into the owningNamespace of the
    /// subsettingFeature (if it is not already in that namespace), requiring the names of the
    /// subsettingFeature and subsettedFeature to be different.
    /// </summary>
    public class Subsetting : ISpecialization
    {
        /// <summary>
        /// </summary>
        public Guid SubsettedFeature { get; set; }

        /// <summary>
        /// </summary>
        public Guid SubsettingFeature { get; set; }

        /// <summary>
        /// </summary>
        public Guid General { get; set; }

        /// <summary>
        /// </summary>
        public Guid Specific { get; set; }

        /// <summary>
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        public List<Guid> OwnedRelatedElement { get; set; }

        /// <summary>
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        public Guid? OwningRelatedElement { get; set; }

        /// <summary>
        /// </summary>
        public List<Guid> Source { get; set; }

        /// <summary>
        /// </summary>
        public List<Guid> Target { get; set; }

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