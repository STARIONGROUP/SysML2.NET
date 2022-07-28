// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureValue.cs" company="RHEA System S.A.">
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
    /// A FeatureValue is a Membership that identifies a particular member Expression that provides the
    /// value of the Feature that owns the FeatureValue. The value is specified as either a bound value or
    /// an initial value, and as either a concrete or default value. A Feature can have at most one
    /// FeatureValue.If isInitial = false, then the result of the value expression is bound to the
    /// featureWithValue using a BindingConnector. Otherwise, the featureWithValue is initialized using a
    /// FeatureWritePeformance.If isDefault = false, then the above semantics of the FeatureValue are
    /// realized for the given featureWithValue. Otherwise, the semantics are realized for any individual of
    /// the featuringType of the featureWithValue, unless another value is explicitly given for the
    /// featureWithValue for that individual.value.featuringType =
    /// featureWithValue.featuringTypevalueConnector.owningNamespace = featureWithValue
    /// andvalueConnector.relatedFeature->includes(featureWithValue)
    /// andvalueConnector.relatedFeature->includes(value.result) andvalueConnector.featuringType =
    /// featureWithValue.featuringType
    /// </summary>
    public partial class FeatureValue : IFeatureValue
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        public Guid FeatureWithValue { get; set; }

        /// <summary>
        /// Whether this FeatureValue is a concrete specification of the bound of initial value of the
        /// featureWithValue, or just a default value that may be overridden.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Whether this FeatureValue specifies a bound value or an initial value for the featureWithValue.
        /// </summary>
        public bool IsInitial { get; set; }

        /// <summary>
        /// </summary>
        public Guid MemberElement { get; set; }

        /// <summary>
        /// The name of the memberElement relative to the membershipOwningNamespace.
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// The short name of the memberElement relative to the membershipOwningNamespace.
        /// </summary>
        public string MemberShortName { get; set; }

        /// <summary>
        /// Whether or not the Membership of the memberElement in the membershipOwningNamespace is publicly
        /// visible outside that Namespace.
        /// </summary>
        public VisibilityKind Visibility { get; set; }

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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
