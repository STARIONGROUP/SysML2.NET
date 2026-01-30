// -------------------------------------------------------------------------------------------------
// <copyright file="IType.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.DTO.Core.Types
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Type is a Namespace that is the most general kind of Element supporting the semantics of
    /// classification. A Type may be a Classifier or a Feature, defining conditions on what is classified
    /// by the Type (see also the description of isSufficient).
    /// </summary>
    [Class(xmiId: "Core-Types-Type", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IType : INamespace
    {
        /// <summary>
        /// The interpretations of a Type with differencingTypes are asserted to be those of the first of those
        /// Types, but not including those of the remaining Types. For example, a Classifier might be the
        /// difference of a Classifier for people and another for people of a particular nationality, leaving
        /// people who are not of that nationality. Similarly, a feature of people might be the difference
        /// between a feature for their children and a Classifier for people of a particular sex, identifying
        /// their children not of that sex (because the interpretations of the children Feature that identify
        /// those of that sex are also interpretations of the Classifier for that sex).
        /// </summary>
        [Property(xmiId: "Core-Types-Type-differencingType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> differencingType { get; }

        /// <summary>
        /// The features of this Type that have a non-null direction.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-directedFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        List<Guid> directedFeature { get; }

        /// <summary>
        /// All features of this Type with isEnd = true.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-endFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        List<Guid> endFeature { get; }

        /// <summary>
        /// The ownedMemberFeatures of the featureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-feature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-member")]
        List<Guid> feature { get; }

        /// <summary>
        /// The FeatureMemberships for features of this Type, which include all ownedFeatureMemberships and
        /// those inheritedMemberships that are FeatureMemberships (but does not include any
        /// importedMemberships).
        /// </summary>
        [Property(xmiId: "Core-Types-Type-featureMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> featureMembership { get; }

        /// <summary>
        /// All the memberFeatures of the inheritedMemberships of this Type that are FeatureMemberships.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-inheritedFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        List<Guid> inheritedFeature { get; }

        /// <summary>
        /// All Memberships inherited by this Type via Specialization or Conjugation. These are included in the
        /// derived union for the memberships of the Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-inheritedMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-membership")]
        List<Guid> inheritedMembership { get; }

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction in or inout.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-input", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        List<Guid> input { get; }

        /// <summary>
        /// The interpretations of a Type with intersectingTypes are asserted to be those in common among the
        /// intersectingTypes, which are the Types derived from the intersectingType of the ownedIntersectings
        /// of this Type. For example, a Classifier might be an intersection of Classifiers for people of a
        /// particular sex and of a particular nationality. Similarly, a feature for people&#39;s children of a
        /// particular sex might be the intersection of a Feature for their children and a Classifier for people
        /// of that sex (because the interpretations of the children Feature that identify those of that sex are
        /// also interpretations of the Classifier for that sex).
        /// </summary>
        [Property(xmiId: "Core-Types-Type-intersectingType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> intersectingType { get; }

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-isAbstract", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsAbstract { get; set; }

        /// <summary>
        /// Indicates whether this Type has an ownedConjugator.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-isConjugated", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool isConjugated { get; }

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.  (A Type gives conditions that must be met by whatever it classifies, but when
        /// isSufficient is false, things may meet those conditions but still not be classified by the Type. For
        /// example, a Type Car that is not sufficient could require everything it classifies to have four
        /// wheels, but not all four wheeled things would classify as cars. However, if the Type Car were
        /// sufficient, it would classify all four-wheeled things.)
        /// </summary>
        [Property(xmiId: "Core-Types-Type-isSufficient", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsSufficient { get; set; }

        /// <summary>
        /// An ownedMember of this Type that is a Multiplicity, which constraints the cardinality of the Type.
        /// If there is no such ownedMember, then the cardinality of this Type is constrained by all the
        /// Multiplicity constraints applicable to any direct supertypes.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-multiplicity", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        Guid? multiplicity { get; }

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction out or inout.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-output", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        List<Guid> output { get; }

        /// <summary>
        /// A Conjugation owned by this Type for which the Type is the originalType.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedConjugator", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-A_conjugatedType_conjugator-conjugator")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        Guid? ownedConjugator { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Differencings, having this Type as their
        /// typeDifferenced.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedDifferencing", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        List<Guid> ownedDifferencing { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Disjoinings, for which the Type is the typeDisjoined
        /// Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedDisjoining", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Core-Types-A_disjoiningTypeDisjoining_typeDisjoined-disjoiningTypeDisjoining")]
        List<Guid> ownedDisjoining { get; }

        /// <summary>
        /// All endFeatures of this Type that are ownedFeatures.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedEndFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-endFeature")]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        List<Guid> ownedEndFeature { get; }

        /// <summary>
        /// The ownedMemberFeatures of the ownedFeatureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        List<Guid> ownedFeature { get; }

        /// <summary>
        /// The ownedMemberships of this Type that are FeatureMemberships, for which the Type is the owningType.
        /// Each such FeatureMembership identifies an ownedFeature of the Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedFeatureMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMembership")]
        [SubsettedProperty(propertyName: "Core-Types-Type-featureMembership")]
        List<Guid> ownedFeatureMembership { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Intersectings, have the Type as their typeIntersected.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedIntersecting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        List<Guid> ownedIntersecting { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Specializations, for which the Type is the specific
        /// Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedSpecialization", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Core-Types-A_specific_specialization-specialization")]
        List<Guid> ownedSpecialization { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Unionings, having the Type as their typeUnioned.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedUnioning", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        List<Guid> ownedUnioning { get; }

        /// <summary>
        /// The interpretations of a Type with unioningTypes are asserted to be the same as those of all the
        /// unioningTypes together, which are the Types derived from the unioningType of the ownedUnionings of
        /// this Type. For example, a Classifier for people might be the union of Classifiers for all the sexes.
        /// Similarly, a feature for people&#39;s children might be the union of features dividing them in the
        /// same ways as people in general.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-unioningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<Guid> unioningType { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
