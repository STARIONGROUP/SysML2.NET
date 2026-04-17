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
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Feature is a Type that classifies relations between multiple things (in the universe). The domain
    /// of the relation is the intersection of the featuringTypes of the Feature. (The domain of a Feature
    /// with no featuringTyps is implicitly the most general Type Base::Anything from the Kernel Semantic
    /// Library.) The co-domain of the relation is the intersection of the types of the Feature.            
    ///            In the simplest cases, the featuringTypes and types are Classifiers and the Feature
    /// relates two things, one from the domain and one from the range. Examples include cars paired with
    /// wheels, people paired with other people, and cars paired with numbers representing the car length.  
    ///                      Since Features are Types, their featuringTypes and types can be Features. In
    /// this case, the Feature effectively classifies relations between relations, which can be interpreted
    /// as the sequence of things related by the domain Feature concatenated with the sequence of things
    /// related by the co-domain Feature.                        The values of a Feature for a given
    /// instance of its domain are all the instances of its co-domain that are related to that domain
    /// instance by the Feature. The values of a Feature with chainingFeatures are the same as values of the
    /// last Feature in the chain, which can be found by starting with values of the first Feature, then
    /// using those values as domain instances to obtain valus of the second Feature, and so on, to values
    /// of the last Feature.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651684_893483_42160", isAbstract: false, isFinalSpecialization: false, isActive: false)]
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
        [Property(xmiId: "_19_0_4_b9102da_1619792219511_543311_445", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        List<IFeature> chainingFeature { get; }

        /// <summary>
        /// The second chainingFeature of the crossedFeature of the ownedCrossSubsetting of this Feature, if it
        /// has one. Semantically, the values of the crossFeature of an end Feature must include all values of
        /// the end Feature obtained when navigating from values of the other end Features of the same
        /// owningType.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1689616227528_355910_218", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IFeature crossFeature { get; }

        /// <summary>
        /// Indicates how values of this Feature are determined or used (as specified for the
        /// FeatureDirectionKind).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674994_447677_43347", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        FeatureDirectionKind? Direction { get; set; }

        /// <summary>
        /// The Type that is related to this Feature by an EndFeatureMembership in which the Feature is an
        /// ownedMemberFeature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1563834516279_920295_20653", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1562476168386_366266_22107")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_592215_43200")]
        IType endOwningType { get; }

        /// <summary>
        /// The last of the chainingFeatures of this Feature, if it has any. Otherwise, this Feature itself.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1715790852907_110671_19", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IFeature featureTarget { get; }

        /// <summary>
        /// Types that feature this Feature, such that any instance in the domain of the Feature must be
        /// classified by all of these Types, including at least all the featuringTypes of its typeFeaturings. 
        /// If the Feature is chained, then the featuringTypes of the first Feature in the chain are also
        /// featuringTypes of the chained Feature.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1603905619975_304385_743", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IType> featuringType { get; }

        /// <summary>
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after its featuring instance no longer does and cannot be values of another composite
        /// feature that is not on the same featuring instance.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674970_331870_43224", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsComposite { get; set; }

        /// <summary>
        /// If isVariable is true, then whether the value of this Feature nevertheless does not change over all
        /// snapshots of its owningType.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674993_300560_43342", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsConstant { get; set; }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Features.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674992_500504_43341", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
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
        [Property(xmiId: "_18_5_3_12e503d9_1562475749426_705395_21984", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsEnd { get; set; }

        /// <summary>
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674969_728225_43215", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the domain of
        /// the Feature and represent the same thing as those instances.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1559231981638_234817_22063", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsPortion { get; set; }

        /// <summary>
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674968_321342_43214", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        bool IsUnique { get; set; }

        /// <summary>
        /// Whether the value of this Feature might vary over time. That is, whether the Feature may have a
        /// different value for each snapshot of an owningType that is an Occurrence.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1725998273002_23711_212", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsVariable { get; set; }

        /// <summary>
        /// The one ownedSubsetting of this Feature, if any, that is a CrossSubsetting}, for which the Feature
        /// is the crossingFeature.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1689616916594_145818_277", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674970_472382_43221")]
        ICrossSubsetting ownedCrossSubsetting { get; }

        /// <summary>
        /// The ownedRelationships of this Feature that are FeatureChainings, for which the Feature will be the
        /// featureChained.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1622125589880_791465_72", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        List<IFeatureChaining> ownedFeatureChaining { get; }

        /// <summary>
        /// The ownedRelationships of this Feature that are FeatureInvertings and for which the Feature is the
        /// featureInverted.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1653567738671_359235_43", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623178838861_768019_145")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        List<IFeatureInverting> ownedFeatureInverting { get; }

        /// <summary>
        /// The ownedSubsettings of this Feature that are Redefinitions, for which the Feature is the
        /// redefiningFeature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674970_161813_43220", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674970_472382_43221")]
        List<IRedefinition> ownedRedefinition { get; }

        /// <summary>
        /// The one ownedSubsetting of this Feature, if any, that is a ReferenceSubsetting, for which the
        /// Feature is the referencingFeature.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1661555161564_247405_255", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674970_472382_43221")]
        IReferenceSubsetting ownedReferenceSubsetting { get; }

        /// <summary>
        /// The ownedSpecializations of this Feature that are Subsettings, for which the Feature is the
        /// subsettingFeature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674970_472382_43221", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_579676_43168")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674966_718145_43205")]
        List<ISubsetting> ownedSubsetting { get; }

        /// <summary>
        /// The ownedRelationships of this Feature that are TypeFeaturings and for which the Feature is the
        /// featureOfType.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1603905673975_310948_762", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1603904928950_196800_580")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        List<ITypeFeaturing> ownedTypeFeaturing { get; }

        /// <summary>
        /// The ownedSpecializations of this Feature that are FeatureTypings, for which the Feature is the
        /// typedFeature.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596597427751_965862_42", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_579676_43168")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543180501615_804591_21100")]
        List<IFeatureTyping> ownedTyping { get; }

        /// <summary>
        /// The FeatureMembership that owns this Feature as an ownedMemberFeature, determining its owningType.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674970_68441_43223", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674972_622493_43236")]
        IFeatureMembership owningFeatureMembership { get; }

        /// <summary>
        /// The Type that is the owningType of the owningFeatureMembership of this Feature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674965_592215_43200", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674987_297074_43308")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_474739_43306")]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1603905619975_304385_743")]
        IType owningType { get; }

        /// <summary>
        /// Types that restrict the values of this Feature, such that the values must be instances of all the
        /// types. The types of a Feature are derived from its typings and the types of its subsettings. If the
        /// Feature is chained, then the types of the last Feature in the chain are also types of the chained
        /// Feature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674969_376003_43216", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IType> type { get; }

        /// <summary>
        /// Return the directionOf this Feature relative to the given type.
        /// </summary>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        FeatureDirectionKind? DirectionFor(IType type);

        /// <summary>
        /// If a Feature has no declaredShortName or declaredName, then its effective shortName is given by the
        /// effective shortName of the Feature returned by the namingFeature() operation, if any.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        new string EffectiveShortName();

        /// <summary>
        /// If a Feature has no declaredName or declaredShortName                            , then its
        /// effective name is given by the effective name of the Feature returned by the namingFeature()
        /// operation, if any.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        new string EffectiveName();

        /// <summary>
        /// By default, the naming Feature of a Feature is given by its first redefinedFeature of its first
        /// ownedRedefinition, if any.
        /// </summary>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        IFeature NamingFeature();

        /// <summary>
        /// </summary>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        new List<IType> Supertypes(bool excludeImplied);

        /// <summary>
        /// Check whether this Feature directly redefines the given redefinedFeature.
        /// </summary>
        /// <param name="redefinedFeature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool Redefines(IFeature redefinedFeature);

        /// <summary>
        /// Check whether this Feature directly redefines the named library Feature. libraryFeatureName must
        /// conform to the syntax of a KerML qualified name and must resolve to a Feature in global scope.
        /// </summary>
        /// <param name="libraryFeatureName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool RedefinesFromLibrary(string libraryFeatureName);

        /// <summary>
        /// Check whether this Feature directly or indirectly specializes a Feature whose last two
        /// chainingFeatures are the given Features first and second.
        /// </summary>
        /// <param name="first">
        /// No documentation provided
        /// </param>
        /// <param name="second">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool SubsetsChain(IFeature first, IFeature second);

        /// <summary>
        /// A Feature is compatible with an otherType if it either directly or indirectly specializes the
        /// otherType or if the otherType is also a Feature and all of the following are true.                  
        ///          <ol>                            <li>Neither this Feature or the otherType have any
        /// ownedFeatures.</li>                            <li>This Feature directly or indirectly redefines a
        /// Feature that is also directly or indirectly redefined by the otherType.</li>                        
        /// <li>This Feature can access the otherType.                            </li></ol>
        /// </summary>
        /// <param name="otherType">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        new bool IsCompatibleWith(IType otherType);

        /// <summary>
        /// Return the Features used to determine the types of this Feature (other than this Feature itself). If
        /// this Feature is not conjugated, then the typingFeatures consist of all subsetted Features, except
        /// from CrossSubsetting, and the last chainingFeature (if any). If this Feature is conjugated, then the
        /// typingFeatures are only its originalType (if the originalType is a Feature).                        
        ///    <strong>Note.</strong> CrossSubsetting is excluded from the determination of the type of a
        /// Feature in order to avoid circularity in the construction of implied CrossSubsetting relationships.
        /// The validateFeatureCrossFeatureType requires that the crossFeature of a Feature have the same type
        /// as the Feature.
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        List<IFeature> TypingFeatures();

        /// <summary>
        /// If isCartesianProduct is true, then return the list of Types whose Cartesian product can be
        /// represented by this Feature. (If isCartesianProduct is not true, the operation will still return a
        /// valid value, it will just not represent anything useful.)
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        List<IType> AsCartesianProduct();

        /// <summary>
        /// Check whether this Feature can be used to represent a Cartesian product of Types.
        /// </summary>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool IsCartesianProduct();

        /// <summary>
        /// Return whether this Feature is an owned cross Feature of an end Feature.
        /// </summary>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool IsOwnedCrossFeature();

        /// <summary>
        /// If this Feature is an end Feature of its owningType, then return the first ownedMember of the
        /// Feature that is a Feature, but not a Multiplicity or a MetadataFeature, and whose owningMembership
        /// is not a FeatureMembership. If this exists, it is the crossFeature of the end Feature.
        /// </summary>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        IFeature OwnedCrossFeature();

        /// <summary>
        /// Return this Feature and all the Features that are directly or indirectly Redefined by this Feature.
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        List<IFeature> AllRedefinedFeatures();

        /// <summary>
        /// Return if the featuringTypes of this Feature are compatible with the given type. If type is null,
        /// then check if this Feature is explicitly or implicitly featured by Base::Anything. If this Feature
        /// has isVariable = true, then also consider it to be featured within its owningType. If this Feature
        /// is a feature chain whose first chainingFeature has isVariable = true, then also consider it to be
        /// featured within the owningType of its first chainingFeature.
        /// </summary>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool IsFeaturedWithin(IType type);

        /// <summary>
        /// A Feature can access another feature if the other feature is featured within one of the direct or
        /// indirect featuringTypes of this Feature.
        /// </summary>
        /// <param name="feature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool CanAccess(IFeature feature);

        /// <summary>
        /// Return whether the given type must be a featuringType of this Feature. If this Feature has
        /// isVariable = false, then return true if the type is the owningType of the Feature. If isVariable =
        /// true, then return true if the type is a Feature representing the snapshots of the owningType of this
        /// Feature.
        /// </summary>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool IsFeaturingType(IType type);
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
