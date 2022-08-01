// -------------------------------------------------------------------------------------------------
// <copyright file="IConjugatedPortTyping.cs" company="RHEA System S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A ConjugatedPortTyping is a FeatureTyping in which the type is derived as the
    /// conjugatedPortDefinition of a given PortDefinition. A ConjugatedPortTyping allows a PortUsage to by
    /// related directly to a PortDefinition, but to be effectively typed by the conjugation of the
    /// referenced PortDefinition.Note that ConjugatedPortTyping is a ternary Relationship, with
    /// portDefinition being a third relatedElement, in addition to type and typedFeature from
    /// FeatureTyping.conjugatedPortDefinition = portDefinition.conjugatedPortDefinition
    /// </summary>
    public partial interface IConjugatedPortTyping : IFeatureTyping
    {
        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        List<string> AliasIds { get; set; }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        string ElementId { get; set; }

        /// <summary>
        /// </summary>
        Guid General { get; set; }

        /// <summary>
        /// The primary name of this Element.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        List<Guid> OwnedRelatedElement { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        List<Guid> OwnedRelationship { get; set; }

        /// <summary>
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        Guid? OwningRelatedElement { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        Guid? OwningRelationship { get; set; }

        /// <summary>
        /// </summary>
        Guid PortDefinition { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context. 
        /// </summary>
        string ShortName { get; set; }

        /// <summary>
        /// </summary>
        List<Guid> Source { get; set; }

        /// <summary>
        /// </summary>
        Guid Specific { get; set; }

        /// <summary>
        /// </summary>
        List<Guid> Target { get; set; }

        /// <summary>
        /// </summary>
        Guid Type { get; set; }

        /// <summary>
        /// </summary>
        Guid TypedFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
