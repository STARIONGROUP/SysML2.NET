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

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Type is a Namespace that is the most general kind of Element supporting the semantics of
    /// classification. A Type may be a Classifier or a Feature, defining conditions on what is classified
    /// by the Type (see also the description of isSufficient).
    /// </summary>
    [Class(xmiId: "_18_5_3_71301a1_1537895141427_270492_15579", isAbstract: false, isFinalSpecialization: false, isActive: false)]
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
        [Property(xmiId: "_19_0_4_b9102da_1661975883472_645501_1372", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IType> differencingType { get; }

        /// <summary>
        /// The features of this Type that have a non-null direction.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1623952188842_882068_37169", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        List<IFeature> directedFeature { get; }

        /// <summary>
        /// All features of this Type with isEnd = true.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1562476168385_824569_22106", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        List<IFeature> endFeature { get; }

        /// <summary>
        /// The ownedMemberFeatures of the featureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_326391_43166", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        List<IFeature> feature { get; }

        /// <summary>
        /// The FeatureMemberships for features of this Type, which include all ownedFeatureMemberships and
        /// those inheritedMemberships that are FeatureMemberships (but does not include any
        /// importedMemberships).
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1651076866512_962346_485", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IFeatureMembership> featureMembership { get; }

        /// <summary>
        /// All the memberFeatures of the inheritedMemberships of this Type that are FeatureMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575499020770_15576_2334", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        List<IFeature> inheritedFeature { get; }

        /// <summary>
        /// All Memberships inherited by this Type via Specialization or Conjugation. These are included in the
        /// derived union for the memberships of the Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1551972927538_787976_19004", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        List<IMembership> inheritedMembership { get; }

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction in or inout.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_37384_43169", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        List<IFeature> input { get; }

        /// <summary>
        /// The interpretations of a Type with intersectingTypes are asserted to be those in common among the
        /// intersectingTypes, which are the Types derived from the intersectingType of the ownedIntersectings
        /// of this Type. For example, a Classifier might be an intersection of Classifiers for people of a
        /// particular sex and of a particular nationality. Similarly, a feature for people&#39;s children of a
        /// particular sex might be the intersection of a Feature for their children and a Classifier for people
        /// of that sex (because the interpretations of the children Feature that identify those of that sex are
        /// also interpretations of the Classifier for that sex).
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661973922199_584242_1045", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IType> intersectingType { get; }

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_741353_43165", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsAbstract { get; set; }

        /// <summary>
        /// Indicates whether this Type has an ownedConjugator.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575485930816_796088_1933", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        bool isConjugated { get; }

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.                            (A Type gives conditions that must be met by whatever it
        /// classifies, but when isSufficient is false, things may meet those conditions but still not be
        /// classified by the Type. For example, a Type Car that is not sufficient could require everything it
        /// classifies to have four wheels, but not all four wheeled things would classify as cars. However, if
        /// the Type Car were sufficient, it would classify all four-wheeled things.)
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1564072709069_937523_30797", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        bool IsSufficient { get; set; }

        /// <summary>
        /// An ownedMember of this Type that is a Multiplicity, which constraints the cardinality of the Type.
        /// If there is no such ownedMember, then the cardinality of this Type is constrained by all the
        /// Multiplicity constraints applicable to any direct supertypes.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1573095852093_324833_5396", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        IMultiplicity multiplicity { get; }

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction out or inout.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_365618_43170", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        List<IFeature> output { get; }

        /// <summary>
        /// A Conjugation owned by this Type for which the Type is the originalType.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575482646809_280165_440", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1575482490144_309557_300")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        IConjugation ownedConjugator { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Differencings, having this Type as their
        /// typeDifferenced.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661871168454_98082_797", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        List<IDifferencing> ownedDifferencing { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Disjoinings, for which the Type is the typeDisjoined
        /// Type.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1627447519613_145554_370", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623183194914_502526_616")]
        List<IDisjoining> ownedDisjoining { get; }

        /// <summary>
        /// All endFeatures of this Type that are ownedFeatures.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1563834516278_687758_20652", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1562476168385_824569_22106")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        List<IFeature> ownedEndFeature { get; }

        /// <summary>
        /// The ownedMemberFeatures of the ownedFeatureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_226999_43167", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        List<IFeature> ownedFeature { get; }

        /// <summary>
        /// The ownedMemberships of this Type that are FeatureMemberships, for which the Type is the owningType.
        /// Each such FeatureMembership identifies an ownedFeature of the Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_868417_43171", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_190614_43269")]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1651076866512_962346_485")]
        List<IFeatureMembership> ownedFeatureMembership { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Intersectings, have the Type as their typeIntersected.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623242552144_910757_524", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        List<IIntersecting> ownedIntersecting { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Specializations, for which the Type is the specific
        /// Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_579676_43168", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674984_558067_43292")]
        List<ISpecialization> ownedSpecialization { get; }

        /// <summary>
        /// The ownedRelationships of this Type that are Unionings, having the Type as their typeUnioned.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661869978505_968809_460", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        List<IUnioning> ownedUnioning { get; }

        /// <summary>
        /// The interpretations of a Type with unioningTypes are asserted to be the same as those of all the
        /// unioningTypes together, which are the Types derived from the unioningType of the ownedUnionings of
        /// this Type. For example, a Classifier for people might be the union of Classifiers for all the sexes.
        /// Similarly, a feature for people&#39;s children might be the union of features dividing them in the
        /// same ways as people in general.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661974896766_783268_1231", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        List<IType> unioningType { get; }

        /// <summary>
        /// The visible Memberships of a Type include inheritedMemberships.
        /// </summary>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <param name="isRecursive">
        /// No documentation provided
        /// </param>
        /// <param name="includeAll">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        new List<IMembership> VisibleMemberships(List<INamespace> excluded, bool isRecursive, bool includeAll) => this.ComputeRedefinedVisibleMembershipsOperation(excluded, isRecursive, includeAll);

        /// <summary>
        /// Return the Memberships inheritable from supertypes of this Type with redefined Features removed.
        /// When computing inheritable Memberships, exclude Imports of excludedNamespaces, Specializations of
        /// excludedTypes, and, if excludeImplied = true, all implied Specializations.
        /// </summary>
        /// <param name="excludedNamespaces">
        /// No documentation provided
        /// </param>
        /// <param name="excludedTypes">
        /// No documentation provided
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        List<IMembership> InheritedMemberships(List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied) => this.ComputeInheritedMembershipsOperation(excludedNamespaces, excludedTypes, excludeImplied);

        /// <summary>
        /// Return all the non-private Memberships of all the supertypes of this Type, excluding any supertypes
        /// that are this Type or are in the given set of excludedTypes. If excludeImplied = true, then also
        /// transitively exclude any supertypes from implied Specializations.
        /// </summary>
        /// <param name="excludedNamespaces">
        /// No documentation provided
        /// </param>
        /// <param name="excludedTypes">
        /// No documentation provided
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        List<IMembership> InheritableMemberships(List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied) => this.ComputeInheritableMembershipsOperation(excludedNamespaces, excludedTypes, excludeImplied);

        /// <summary>
        /// Return the public, protected and inherited Memberships of this Type. When computing imported
        /// Memberships, exclude the given set of excludedNamespaces. When computing inherited Memberships,
        /// exclude Types in the given set of excludedTypes. If excludeImplied = true, then also exclude any
        /// supertypes from implied Specializations.
        /// </summary>
        /// <param name="excludedNamespaces">
        /// No documentation provided
        /// </param>
        /// <param name="excludedTypes">
        /// No documentation provided
        /// </param>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        List<IMembership> NonPrivateMemberships(List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied) => this.ComputeNonPrivateMembershipsOperation(excludedNamespaces, excludedTypes, excludeImplied);

        /// <summary>
        /// Return a subset of memberships, removing those Memberships whose memberElements are Features and for
        /// which either of the following two conditions holds:                            <ol>                 
        ///           <li>The memberElement of the Membership is included in redefined Features of another
        /// Membership in memberships.</li>                            <li>One of the redefined Features of the
        /// Membership is a directly redefinedFeature of an ownedFeature of this Type.</li>                     
        ///       </ol>                            For this purpose, the redefined Features of a Membership
        /// whose memberElement is a Feature includes the memberElement and all Features directly or indirectly
        /// redefined by the memberElement.
        /// </summary>
        /// <param name="memberships">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        List<IMembership> RemoveRedefinedFeatures(List<IMembership> memberships) => this.ComputeRemoveRedefinedFeaturesOperation(memberships);

        /// <summary>
        /// If the memberElement of the given membership is a Feature, then return all Features directly or
        /// indirectly redefined by the memberElement.
        /// </summary>
        /// <param name="membership">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        List<IFeature> AllRedefinedFeaturesOf(IMembership membership) => this.ComputeAllRedefinedFeaturesOfOperation(membership);

        /// <summary>
        /// If the given feature is a feature of this Type, then return its direction relative to this Type,
        /// taking conjugation into account.
        /// </summary>
        /// <param name="feature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        FeatureDirectionKind DirectionOf(IFeature feature) => this.ComputeDirectionOfOperation(feature);

        /// <summary>
        /// Return the direction of the given feature relative to this Type, excluding a given set of Types from
        /// the search of supertypes of this Type.
        /// </summary>
        /// <param name="feature">
        /// No documentation provided
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        FeatureDirectionKind DirectionOfExcluding(IFeature feature, List<IType> excluded) => this.ComputeDirectionOfExcludingOperation(feature, excluded);

        /// <summary>
        /// If this Type is conjugated, then return just the originalType of the Conjugation. Otherwise, return
        /// the general Types from all ownedSpecializations of this type, if excludeImplied = false, or all
        /// non-implied ownedSpecializations, if excludeImplied = true.
        /// </summary>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        List<IType> Supertypes(bool excludeImplied) => this.ComputeSupertypesOperation(excludeImplied);

        /// <summary>
        /// Return this Type and all Types that are directly or transitively supertypes of this Type (as
        /// determined by the supertypes operation with excludeImplied = false).
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        List<IType> AllSupertypes() => this.ComputeAllSupertypesOperation();

        /// <summary>
        /// Check whether this Type is a direct or indirect specialization of the given supertype.
        /// </summary>
        /// <param name="supertype">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool Specializes(IType supertype) => this.ComputeSpecializesOperation(supertype);

        /// <summary>
        /// Check whether this Type is a direct or indirect specialization of the named library Type.
        /// libraryTypeName must conform to the syntax of a KerML qualified name and must resolve to a Type in
        /// global scope.
        /// </summary>
        /// <param name="libraryTypeName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool SpecializesFromLibrary(string libraryTypeName) => this.ComputeSpecializesFromLibraryOperation(libraryTypeName);

        /// <summary>
        /// By default, this Type is compatible with an otherType if it directly or indirectly specializes the
        /// otherType.
        /// </summary>
        /// <param name="otherType">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool IsCompatibleWith(IType otherType) => this.ComputeIsCompatibleWithOperation(otherType);

        /// <summary>
        /// Return the owned or inherited Multiplicities for this Type<./code>.
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IMultiplicity" />
        /// </returns>
        List<IMultiplicity> Multiplicities() => this.ComputeMultiplicitiesOperation();
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
