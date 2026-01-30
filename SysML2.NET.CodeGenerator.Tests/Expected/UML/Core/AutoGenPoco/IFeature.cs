// -------------------------------------------------------------------------------------------------
// <copyright file="IFeature.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Feature is a Type that classifies relations between multiple things (in the universe). The domain
    /// of the relation is the intersection of the featuringTypes of the Feature. (The domain of a Feature
    /// with no featuringTyps is implicitly the most general Type Base::Anything from the Kernel Semantic
    /// Library.) The co-domain of the relation is the intersection of the types of the Feature.  In the
    /// simplest cases, the featuringTypes and types are Classifiers and the Feature relates two things, one
    /// from the domain and one from the range. Examples include cars paired with wheels, people paired with
    /// other people, and cars paired with numbers representing the car length.  Since Features are Types,
    /// their featuringTypes and types can be Features. In this case, the Feature effectively classifies
    /// relations between relations, which can be interpreted as the sequence of things related by the
    /// domain Feature concatenated with the sequence of things related by the co-domain Feature.  The
    /// values of a Feature for a given instance of its domain are all the instances of its co-domain that
    /// are related to that domain instance by the Feature. The values of a Feature with chainingFeatures
    /// are the same as values of the last Feature in the chain, which can be found by starting with values
    /// of the first Feature, then using those values as domain instances to obtain valus of the second
    /// Feature, and so on, to values of the last Feature.
    /// </summary>
    [Class(xmiId: "Core-Features-Feature", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeature : IType
    {
        /// <summary>
        /// The Feature that are chained together to determine the values of this Feature, derived from the
        /// chainingFeatures of the ownedFeatureChainings of this Feature, in the same order. The values of a
        /// Feature with chainingFeatures are the same as values of the last Feature in the chain, which can be
        /// found by starting with the values of the first Feature (for each instance of the domain of the
        /// original Feature), then using each of those as domain instances to find the values of the second
        /// Feature in chainingFeatures, and so on, to values of the last Feature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-chainingFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        List<IFeature> chainingFeature { get; }

        /// <summary>
        /// The second chainingFeature of the crossedFeature of the ownedCrossSubsetting of this Feature, if it
        /// has one. Semantically, the values of the crossFeature of an end Feature must include all values of
        /// the end Feature obtained when navigating from values of the other end Features of the same
        /// owningType.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-crossFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IFeature crossFeature { get; }

        /// <summary>
        /// Indicates how values of this Feature are determined or used (as specified for the
        /// FeatureDirectionKind).
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-direction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        FeatureDirectionKind? Direction { get; set; }

        /// <summary>
        /// The Type that is related to this Feature by an EndFeatureMembership in which the Feature is an
        /// ownedMemberFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-endOwningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-A_endFeature_typeWithEndFeature-typeWithEndFeature")]
        [SubsettedProperty(propertyName: "Core-Features-Feature-owningType")]
        IType endOwningType { get; }

        /// <summary>
        /// The last of the chainingFeatures of this Feature, if it has any. Otherwise, this Feature itself.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-featureTarget", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IFeature featureTarget { get; }

        /// <summary>
        /// Types that feature this Feature, such that any instance in the domain of the Feature must be
        /// classified by all of these Types, including at least all the featuringTypes of its typeFeaturings. 
        /// If the Feature is chained, then the featuringTypes of the first Feature in the chain are also
        /// featuringTypes of the chained Feature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-featuringType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IType> featuringType { get; }

        /// <summary>
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after its featuring instance no longer does and cannot be values of another composite
        /// feature that is not on the same featuring instance.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isComposite", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsComposite { get; set; }

        /// <summary>
        /// If isVariable is true, then whether the value of this Feature nevertheless does not change over all
        /// snapshots of its owningType.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isConstant", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsConstant { get; set; }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Features.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isDerived", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsDerived { get; set; }

        /// <summary>
        /// Whether or not this Feature is an end Feature. An end Feature always has multiplicity 1, mapping
        /// each of its domain instances to a single co-domain instance. However, it may have a crossFeature, in
        /// which case values of the crossFeature must be the same as those found by navigation across instances
        /// of the owningType from values of other end Features to values of this Feature. If the owningType has
        /// n end Features, then the multiplicity, ordering, and uniqueness declared for the crossFeature of any
        /// one of these end Features constrains the cardinality, ordering, and uniqueness of the collection of
        /// values of that Feature reached by navigation when the values of the other n-1 end Features are held
        /// fixed.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isEnd", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsEnd { get; set; }

        /// <summary>
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isOrdered", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the domain of
        /// the Feature and represent the same thing as those instances.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isPortion", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsPortion { get; set; }

        /// <summary>
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isUnique", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        bool IsUnique { get; set; }

        /// <summary>
        /// Whether the value of this Feature might vary over time. That is, whether the Feature may have a
        /// different value for each snapshot of an owningType that is an Occurrence.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isVariable", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsVariable { get; set; }

        /// <summary>
        /// The one ownedSubsetting of this Feature, if any, that is a CrossSubsetting}, for which the Feature
        /// is the crossingFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedCrossSubsetting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-ownedSubsetting")]
        ICrossSubsetting ownedCrossSubsetting { get; }

        /// <summary>
        /// The ownedRelationships of this Feature that are FeatureChainings, for which the Feature will be the
        /// featureChained.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedFeatureChaining", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        List<IFeatureChaining> ownedFeatureChaining { get; }

        /// <summary>
        /// The ownedRelationships of this Feature that are FeatureInvertings and for which the Feature is the
        /// featureInverted.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedFeatureInverting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-A_invertingFeatureInverting_featureInverted-invertingFeatureInverting")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        List<IFeatureInverting> ownedFeatureInverting { get; }

        /// <summary>
        /// The ownedSubsettings of this Feature that are Redefinitions, for which the Feature is the
        /// redefiningFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedRedefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-ownedSubsetting")]
        List<IRedefinition> ownedRedefinition { get; }

        /// <summary>
        /// The one ownedSubsetting of this Feature, if any, that is a ReferenceSubsetting, for which the
        /// Feature is the referencingFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedReferenceSubsetting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-ownedSubsetting")]
        IReferenceSubsetting ownedReferenceSubsetting { get; }

        /// <summary>
        /// The ownedSpecializations of this Feature that are Subsettings, for which the Feature is the
        /// subsettingFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedSubsetting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedSpecialization")]
        [SubsettedProperty(propertyName: "Core-Features-A_subsettingFeature_subsetting-subsetting")]
        List<ISubsetting> ownedSubsetting { get; }

        /// <summary>
        /// The ownedRelationships of this Feature that are TypeFeaturings and for which the Feature is the
        /// featureOfType.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedTypeFeaturing", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-A_featureOfType_typeFeaturing-typeFeaturing")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        List<ITypeFeaturing> ownedTypeFeaturing { get; }

        /// <summary>
        /// The ownedSpecializations of this Feature that are FeatureTypings, for which the Feature is the
        /// typedFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedTyping", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedSpecialization")]
        [SubsettedProperty(propertyName: "Core-Features-A_typing_typedFeature-typing")]
        List<IFeatureTyping> ownedTyping { get; }

        /// <summary>
        /// The FeatureMembership that owns this Feature as an ownedMemberFeature, determining its owningType.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-owningFeatureMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningMembership")]
        IFeatureMembership owningFeatureMembership { get; }

        /// <summary>
        /// The Type that is the owningType of the owningFeatureMembership of this Feature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-owningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-featuringType")]
        [SubsettedProperty(propertyName: "Core-Types-A_typeWithFeature_feature-typeWithFeature")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningNamespace")]
        IType owningType { get; }

        /// <summary>
        /// Types that restrict the values of this Feature, such that the values must be instances of all the
        /// types. The types of a Feature are derived from its typings and the types of its subsettings. If the
        /// Feature is chained, then the types of the last Feature in the chain are also types of the chained
        /// Feature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-type", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IType> type { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
