﻿// -------------------------------------------------------------------------------------------------
// <copyright file="Redefinition.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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
    /// Redefinition is a kind of Subsetting that requires the redefinedFeature and the redefiningFeature to
    /// have the same values (on each instance of the domain of the redefiningFeature). This means any
    /// restrictions on the redefiningFeature, such as type or multiplicity, also apply to the
    /// redefinedFeature (on each instance of the domain of the redefiningFeature), and vice versa. The
    /// redefinedFeature might have values for instances of the domain of the redefiningFeature, but only as
    /// instances of the domain of the redefinedFeature that happen to also be instances of the domain of
    /// the redefiningFeature. This is supported by the constraints inherited from Subsetting on the domains
    /// of the redefiningFeature and redefinedFeature. However, these constraints are narrowed for
    /// Redefinition to require the owningTypes of the redefiningFeature and redefinedFeature to be
    /// different and the redefinedFeature to not be inherited into the owningNamespace of the
    /// redefiningFeature.This enables the redefiningFeature to have the same name as the redefinedFeature,
    /// if desired.let anythingType: Type =   
    /// redefiningFeature.resolveGlobal('Base::Anything').modelElement.oclAsType(Type) in -- Including
    /// "Anything" accounts for implicit featuringType of Features-- with no explicit featuringType.let
    /// redefiningFeaturingTypes: Set(Type) =    if redefiningFeature.isVariable then
    /// Set{redefiningFeature.owningType}    else
    /// redefiningFeature.featuringTypes->asSet()->including(anythingType)     endif inlet
    /// redefinedFeaturingTypes: Set(Type) =    if redefinedFeature.isVariable then
    /// Set{redefinedFeature.owningType}    else
    /// redefinedFeature.featuringTypes->asSet()->including(anythingType)    endif
    /// inredefiningFeaturingTypes <> redefinedFeaturingTypelet featuringTypes : Sequence(Type) =    if
    /// redefiningFeature.isVariable then Sequence{redefiningFeature.owningType}    else
    /// redefiningFeature.featuringType    endif infeaturingTypes->forAll(t |    let direction :
    /// FeatureDirectionKind = t.directionOf(redefinedFeature) in    ((direction =
    /// FeatureDirectionKind::_'in' or       direction = FeatureDirectionKind::out) implies        
    /// redefiningFeature.direction = direction)    and     (direction = FeatureDirectionKind::inout implies
    /// redefiningFeature.direction <> null))redefinedFeature.isEnd implies redefiningFeature.isEnd
    /// </summary>
    public partial class Redefinition : IRedefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Redefinition"/> class.
        /// </summary>
        public Redefinition()
        {
            this.AliasIds = new List<string>();
            this.IsImplied = false;
            this.IsImpliedIncluded = false;
            this.OwnedRelatedElement = new List<Guid>();
            this.OwnedRelationship = new List<Guid>();
            this.Source = new List<Guid>();
            this.Target = new List<Guid>();
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
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public string ElementId { get; set; }

        /// <summary>
        /// A Type with a superset of all instances of the specific Type, which might be the same set.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid General { get; set; }

        /// <summary>
        /// Whether this Relationship was generated by tooling to meet semantic rules, rather than being
        /// directly created by a modeler.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public bool IsImplied { get; set; }

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
        /// The relatedElements of this Relationship that are owned by the Relationship.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: true)]
        public List<Guid> OwnedRelatedElement { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: true)]
        public List<Guid> OwnedRelationship { get; set; }

        /// <summary>
        /// The relatedElement of this Relationship that owns the Relationship, if any.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid? OwningRelatedElement { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid? OwningRelationship { get; set; }

        /// <summary>
        /// The Feature that is redefined by the redefiningFeature of this Redefinition.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid RedefinedFeature { get; set; }

        /// <summary>
        /// The Feature that is redefining the redefinedFeature of this Redefinition.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid RedefiningFeature { get; set; }

        /// <summary>
        /// The relatedElements from which this Relationship is considered to be directed.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        public List<Guid> Source { get; set; }

        /// <summary>
        /// A Type with a subset of all instances of the general Type, which might be the same set.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid Specific { get; set; }

        /// <summary>
        /// The Feature that is subsetted by the subsettingFeature of this Subsetting.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid SubsettedFeature { get; set; }

        /// <summary>
        /// The Feature that is a subset of the subsettedFeature of this Subsetting.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        public Guid SubsettingFeature { get; set; }

        /// <summary>
        /// The relatedElements to which this Relationship is considered to be directed.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        public List<Guid> Target { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
