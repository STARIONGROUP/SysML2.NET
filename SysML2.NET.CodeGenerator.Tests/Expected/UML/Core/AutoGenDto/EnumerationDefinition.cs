// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationDefinition.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.DTO.Systems.Enumerations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.DTO.Systems.Attributes;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An EnumerationDefinition is an AttributeDefinition all of whose instances are given by an explicit
    /// list of enumeratedValues. This is realized by requiring that the EnumerationDefinition have
    /// isVariation = true, with the enumeratedValues being its variants.
    /// </summary>
    [Class(xmiId: "_19_0_4_12e503d9_1606946467364_179493_153", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class EnumerationDefinition : IEnumerationDefinition
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IData.Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594312532679_496267_4310", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.AliasIds")]
        public List<string> AliasIds { get; set; } = [];

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674987_737648_43307", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.DeclaredName")]
        public string DeclaredName { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594160442439_915308_4153", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.DeclaredShortName")]
        public string DeclaredShortName { get; set; }

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
        [Implements(implementation: "IType.DifferencingType")]
        public List<Guid> DifferencingType { get; internal set; }

        /// <summary>
        /// The features of this Type that have a non-null direction.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1623952188842_882068_37169", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        [Implements(implementation: "IType.DirectedFeature")]
        public List<Guid> DirectedFeature { get; internal set; }

        /// <summary>
        /// The usages of this Definition that are directedFeatures.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565495064714_974634_26150", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        [Implements(implementation: "IDefinition.DirectedUsage")]
        public List<Guid> DirectedUsage { get; internal set; }

        /// <summary>
        /// The Documentation owned by this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594150061166_345630_1621", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        [Implements(implementation: "IElement.Documentation")]
        public List<Guid> Documentation { get; internal set; }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_844338_43305", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ElementId")]
        public string ElementId { get; set; }

        /// <summary>
        /// All features of this Type with isEnd = true.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1562476168385_824569_22106", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        [Implements(implementation: "IType.EndFeature")]
        public List<Guid> EndFeature { get; internal set; }

        /// <summary>
        /// EnumerationUsages of this EnumerationDefinitionthat have distinct, fixed values. Each
        /// enumeratedValue specifies one of the allowed instances of the EnumerationDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606946634788_959145_265", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1590979457191_746167_951")]
        [Implements(implementation: "IEnumerationDefinition.EnumeratedValue")]
        public List<Guid> EnumeratedValue { get; internal set; }

        /// <summary>
        /// The ownedMemberFeatures of the featureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_326391_43166", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        [Implements(implementation: "IType.Feature")]
        public List<Guid> Feature { get; internal set; }

        /// <summary>
        /// The FeatureMemberships for features of this Type, which include all ownedFeatureMemberships and
        /// those inheritedMemberships that are FeatureMemberships (but does not include any
        /// importedMemberships).
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1651076866512_962346_485", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.FeatureMembership")]
        public List<Guid> FeatureMembership { get; internal set; }

        /// <summary>
        /// The Memberships in this Namespace that result from the ownedImports of this Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_207869_43270", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [Implements(implementation: "INamespace.ImportedMembership")]
        public List<Guid> ImportedMembership { get; internal set; }

        /// <summary>
        /// All the memberFeatures of the inheritedMemberships of this Type that are FeatureMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575499020770_15576_2334", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        [Implements(implementation: "IType.InheritedFeature")]
        public List<Guid> InheritedFeature { get; internal set; }

        /// <summary>
        /// All Memberships inherited by this Type via Specialization or Conjugation. These are included in the
        /// derived union for the memberships of the Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1551972927538_787976_19004", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [Implements(implementation: "IType.InheritedMembership")]
        public List<Guid> InheritedMembership { get; internal set; }

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction in or inout.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_37384_43169", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [Implements(implementation: "IType.Input")]
        public List<Guid> Input { get; internal set; }

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
        [Implements(implementation: "IType.IntersectingType")]
        public List<Guid> IntersectingType { get; internal set; }

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_741353_43165", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IType.IsAbstract")]
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Indicates whether this Type has an ownedConjugator.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575485930816_796088_1933", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.IsConjugated")]
        public bool IsConjugated { get; internal set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1662070949317_79713_3658", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IElement.IsImpliedIncluded")]
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1665443500960_5561_723", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.IsLibraryElement")]
        public bool IsLibraryElement { get; internal set; }

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.(A Type gives conditions that must be met by whatever it classifies, but when isSufficient
        /// is false, things may meet those conditions but still not be classified by the Type. For example, a
        /// Type Car that is not sufficient could require everything it classifies to have four wheels, but not
        /// all four wheeled things would classify as cars. However, if the Type Car were sufficient, it would
        /// classify all four-wheeled things.)
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1564072709069_937523_30797", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IType.IsSufficient")]
        public bool IsSufficient { get; set; }

        /// <summary>
        /// An EnumerationDefinition is considered semantically to be a variation whose allowed variants are its
        /// enumerationValues.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606946783667_895456_287", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1590978283180_265362_419")]
        [Implements(implementation: "IEnumerationDefinition.IsVariation")]
        bool IEnumerationDefinition.IsVariation { get; set; } = true;

        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590978283180_265362_419", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedByProperty("IEnumerationDefinition.IsVariation")]
        [Implements(implementation: "IDefinition.IsVariation")]
        bool Systems.DefinitionAndUsage.IDefinition.IsVariation { get; set; }

        /// <summary>
        /// The set of all member Elements of this Namespace, which are the memberElements of all memberships of
        /// the Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_644335_43267", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "INamespace.Member")]
        public List<Guid> Member { get; internal set; }

        /// <summary>
        /// All Memberships in this Namespace, including (at least) the union of ownedMemberships and
        /// importedMemberships.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674962_198288_43183", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: true, isUnique: true, defaultValue: null)]
        [Implements(implementation: "INamespace.Membership")]
        public List<Guid> Membership { get; internal set; }

        /// <summary>
        /// An ownedMember of this Type that is a Multiplicity, which constraints the cardinality of the Type.
        /// If there is no such ownedMember, then the cardinality of this Type is constrained by all the
        /// Multiplicity constraints applicable to any direct supertypes.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1573095852093_324833_5396", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        [Implements(implementation: "IType.Multiplicity")]
        public Guid? Multiplicity { get; internal set; }

        /// <summary>
        /// The name to be used for this Element during name resolution within its owningNamespace. This is
        /// derived using the effectiveName() operation. By default, it is the same as the declaredName, but
        /// this is overridden for certain kinds of Elements to compute a name even when the declaredName is
        /// null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617485009541_709355_27528", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Name")]
        public string Name { get; internal set; }

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction out or inout.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_365618_43170", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [Implements(implementation: "IType.Output")]
        public List<Guid> Output { get; internal set; }

        /// <summary>
        /// The ActionUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591479011613_547927_1091", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943849505_989631_257")]
        [Implements(implementation: "IDefinition.OwnedAction")]
        public List<Guid> OwnedAction { get; internal set; }

        /// <summary>
        /// The AllocationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611430819239_430196_1024", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591480607506_951212_2333")]
        [Implements(implementation: "IDefinition.OwnedAllocation")]
        public List<Guid> OwnedAllocation { get; internal set; }

        /// <summary>
        /// The AnalysisCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591152747086_367030_3846", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_59601fc_1590257108055_7496_483")]
        [Implements(implementation: "IDefinition.OwnedAnalysisCase")]
        public List<Guid> OwnedAnalysisCase { get; internal set; }

        /// <summary>
        /// The ownedRelationships of this Element that are Annotations, for which this Element is the
        /// annotatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594152527165_702130_2500", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094430277_599480_18543")]
        [Implements(implementation: "IElement.OwnedAnnotation")]
        public List<Guid> OwnedAnnotation { get; internal set; }

        /// <summary>
        /// The AttributeUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591500614097_490259_4413", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        [Implements(implementation: "IDefinition.OwnedAttribute")]
        public List<Guid> OwnedAttribute { get; internal set; }

        /// <summary>
        /// The CalculationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1588215335104_898924_667", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479011613_547927_1091")]
        [Implements(implementation: "IDefinition.OwnedCalculation")]
        public List<Guid> OwnedCalculation { get; internal set; }

        /// <summary>
        /// The code>CaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_59601fc_1590257108055_7496_483", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1588215335104_898924_667")]
        [Implements(implementation: "IDefinition.OwnedCase")]
        public List<Guid> OwnedCase { get; internal set; }

        /// <summary>
        /// The ConcernUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617051597354_928367_1357", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000559760_444344_1273")]
        [Implements(implementation: "IDefinition.OwnedConcern")]
        public List<Guid> OwnedConcern { get; internal set; }

        /// <summary>
        /// A Conjugation owned by this Type for which the Type is the originalType.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575482646809_280165_440", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1575482490144_309557_300")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "IType.OwnedConjugator")]
        public Guid? OwnedConjugator { get; internal set; }

        /// <summary>
        /// The ConnectorAsUsages that are ownedUsages of this Definition. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowUsages because these are ConnectorAsUsages
        /// even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591480607506_951212_2333", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        [Implements(implementation: "IDefinition.OwnedConnection")]
        public List<Guid> OwnedConnection { get; internal set; }

        /// <summary>
        /// The ConstraintUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578068081992_244000_1803", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943849505_989631_257")]
        [Implements(implementation: "IDefinition.OwnedConstraint")]
        public List<Guid> OwnedConstraint { get; internal set; }

        /// <summary>
        /// The ownedRelationships of this Type that are Differencings, having this Type as their
        /// typeDifferenced.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661871168454_98082_797", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "IType.OwnedDifferencing")]
        public List<Guid> OwnedDifferencing { get; internal set; }

        /// <summary>
        /// The ownedRelationships of this Type that are Disjoinings, for which the Type is the typeDisjoined
        /// Type.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1627447519613_145554_370", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623183194914_502526_616")]
        [Implements(implementation: "IType.OwnedDisjoining")]
        public List<Guid> OwnedDisjoining { get; internal set; }

        /// <summary>
        /// The Elements owned by this Element, derived as the ownedRelatedElements of the ownedRelationships of
        /// this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_112608_17278", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.OwnedElement")]
        public List<Guid> OwnedElement { get; internal set; }

        /// <summary>
        /// All endFeatures of this Type that are ownedFeatures.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1563834516278_687758_20652", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1562476168385_824569_22106")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        [Implements(implementation: "IType.OwnedEndFeature")]
        public List<Guid> OwnedEndFeature { get; internal set; }

        /// <summary>
        /// The EnumerationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606946600508_763872_252", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591500614097_490259_4413")]
        [Implements(implementation: "IDefinition.OwnedEnumeration")]
        public List<Guid> OwnedEnumeration { get; internal set; }

        /// <summary>
        /// The ownedMemberFeatures of the ownedFeatureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_226999_43167", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        [Implements(implementation: "IType.OwnedFeature")]
        public List<Guid> OwnedFeature { get; internal set; }

        /// <summary>
        /// The ownedMemberships of this Type that are FeatureMemberships, for which the Type is the owningType.
        /// Each such FeatureMembership identifies an ownedFeature of the Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_868417_43171", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_190614_43269")]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1651076866512_962346_485")]
        [Implements(implementation: "IType.OwnedFeatureMembership")]
        public List<Guid> OwnedFeatureMembership { get; internal set; }

        /// <summary>
        /// The FlowUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624055201422_104863_1697", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591480607506_951212_2333")]
        [Implements(implementation: "IDefinition.OwnedFlow")]
        public List<Guid> OwnedFlow { get; internal set; }

        /// <summary>
        /// The ownedRelationships of this Namespace that are Imports, for which the Namespace is the
        /// importOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674974_746786_43247", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [Implements(implementation: "INamespace.OwnedImport")]
        public List<Guid> OwnedImport { get; internal set; }

        /// <summary>
        /// The InterfaceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591498709150_220812_4128", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591480607506_951212_2333")]
        [Implements(implementation: "IDefinition.OwnedInterface")]
        public List<Guid> OwnedInterface { get; internal set; }

        /// <summary>
        /// The ownedRelationships of this Type that are Intersectings, have the Type as their typeIntersected.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623242552144_910757_524", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "IType.OwnedIntersecting")]
        public List<Guid> OwnedIntersecting { get; internal set; }

        /// <summary>
        /// The ItemUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591482567975_649284_3005", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943849505_989631_257")]
        [Implements(implementation: "IDefinition.OwnedItem")]
        public List<Guid> OwnedItem { get; internal set; }

        /// <summary>
        /// The owned members of this Namespace, which are the <cpde>ownedMemberElements of the ownedMemberships
        /// of the Namespace.</cpde>
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_259543_43268", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        [Implements(implementation: "INamespace.OwnedMember")]
        public List<Guid> OwnedMember { get; internal set; }

        /// <summary>
        /// The ownedRelationships of this Namespace that are Memberships, for which the Namespace is the
        /// membershipOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_190614_43269", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "INamespace.OwnedMembership")]
        public List<Guid> OwnedMembership { get; internal set; }

        /// <summary>
        /// The MetadataUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1661488358064_457109_2881", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591482567975_649284_3005")]
        [Implements(implementation: "IDefinition.OwnedMetadata")]
        public List<Guid> OwnedMetadata { get; internal set; }

        /// <summary>
        /// The OccurrenceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618943849505_989631_257", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        [Implements(implementation: "IDefinition.OwnedOccurrence")]
        public List<Guid> OwnedOccurrence { get; internal set; }

        /// <summary>
        /// The PartUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591496643392_630316_3279", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591482567975_649284_3005")]
        [Implements(implementation: "IDefinition.OwnedPart")]
        public List<Guid> OwnedPart { get; internal set; }

        /// <summary>
        /// The PortUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565494319970_455996_25799", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        [Implements(implementation: "IDefinition.OwnedPort")]
        public List<Guid> OwnedPort { get; internal set; }

        /// <summary>
        /// The ReferenceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591477471991_39731_908", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        [Implements(implementation: "IDefinition.OwnedReference")]
        public List<Guid> OwnedReference { get; internal set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092026091_217766_16748", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        [Implements(implementation: "IElement.OwnedRelationship")]
        public List<Guid> OwnedRelationship { get; set; } = [];

        /// <summary>
        /// The RenderingUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596741437225_963350_6474", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591496643392_630316_3279")]
        [Implements(implementation: "IDefinition.OwnedRendering")]
        public List<Guid> OwnedRendering { get; internal set; }

        /// <summary>
        /// The RequirementUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583000559760_444344_1273", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1578068081992_244000_1803")]
        [Implements(implementation: "IDefinition.OwnedRequirement")]
        public List<Guid> OwnedRequirement { get; internal set; }

        /// <summary>
        /// The ownedRelationships of this Type that are Specializations, for which the Type is the specific
        /// Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_579676_43168", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674984_558067_43292")]
        [Implements(implementation: "IType.OwnedSpecialization")]
        public List<Guid> OwnedSpecialization { get; internal set; }

        /// <summary>
        /// The StateUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575587977045_745776_941", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479011613_547927_1091")]
        [Implements(implementation: "IDefinition.OwnedState")]
        public List<Guid> OwnedState { get; internal set; }

        /// <summary>
        /// The ownedSpecializations of this Classifier that are Subclassifications, for which this Classifier
        /// is the subclassifier.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543189170643_419862_25507", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_579676_43168")]
        [Implements(implementation: "IClassifier.OwnedSubclassification")]
        public List<Guid> OwnedSubclassification { get; internal set; }

        /// <summary>
        /// The TransitionUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578598061680_350995_3923", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        [Implements(implementation: "IDefinition.OwnedTransition")]
        public List<Guid> OwnedTransition { get; internal set; }

        /// <summary>
        /// The ownedRelationships of this Type that are Unionings, having the Type as their typeUnioned.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661869978505_968809_460", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [Implements(implementation: "IType.OwnedUnioning")]
        public List<Guid> OwnedUnioning { get; internal set; }

        /// <summary>
        /// The Usages that are ownedFeatures of this Definition.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565479686637_967933_23236", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        [Implements(implementation: "IDefinition.OwnedUsage")]
        public List<Guid> OwnedUsage { get; internal set; }

        /// <summary>
        /// The UseCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621461106608_978605_945", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_59601fc_1590257108055_7496_483")]
        [Implements(implementation: "IDefinition.OwnedUseCase")]
        public List<Guid> OwnedUseCase { get; internal set; }

        /// <summary>
        /// The VerificationCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596821523387_872104_10416", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_59601fc_1590257108055_7496_483")]
        [Implements(implementation: "IDefinition.OwnedVerificationCase")]
        public List<Guid> OwnedVerificationCase { get; internal set; }

        /// <summary>
        /// The ViewUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596644570381_840567_784", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591496643392_630316_3279")]
        [Implements(implementation: "IDefinition.OwnedView")]
        public List<Guid> OwnedView { get; internal set; }

        /// <summary>
        /// The ViewpointUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596649828408_673531_3683", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000559760_444344_1273")]
        [Implements(implementation: "IDefinition.OwnedViewpoint")]
        public List<Guid> OwnedViewpoint { get; internal set; }

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_744477_17277", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Owner")]
        public Guid? Owner { get; internal set; }

        /// <summary>
        /// The owningRelationship of this Element, if that Relationship is a Membership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674972_622493_43236", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674973_469277_43243")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_482273_43303")]
        [Implements(implementation: "IElement.OwningMembership")]
        public Guid? OwningMembership { get; internal set; }

        /// <summary>
        /// The Namespace that owns this Element, which is the membershipOwningNamespace of the owningMembership
        /// of this Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_474739_43306", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674980_717955_43271")]
        [Implements(implementation: "IElement.OwningNamespace")]
        public Guid? OwningNamespace { get; internal set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_482273_43303", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        [Implements(implementation: "IElement.OwningRelationship")]
        public Guid? OwningRelationship { get; set; }

        /// <summary>
        /// The full ownership-qualified name of this Element, represented in a form that is valid according to
        /// the KerML textual concrete syntax for qualified names (including use of unrestricted name notation
        /// and escaped characters, as necessary). The qualifiedName is null if this Element has no
        /// owningNamespace or if there is not a complete ownership chain of named Namespaces from a root
        /// Namespace to this Element. If the owningNamespace has other Elements with the same name as this one,
        /// then the qualifiedName is null for all such Elements other than the first.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611356604987_900871_594", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.QualifiedName")]
        public string QualifiedName { get; internal set; }

        /// <summary>
        /// The short name to be used for this Element during name resolution within its owningNamespace. This
        /// is derived using the effectiveShortName() operation. By default, it is the same as the
        /// declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even
        /// when the declaredName is null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1673496405504_544235_24", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ShortName")]
        public string ShortName { get; internal set; }

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594154758493_640290_3388", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        [Implements(implementation: "IElement.TextualRepresentation")]
        public List<Guid> TextualRepresentation { get; internal set; }

        /// <summary>
        /// The interpretations of a Type with unioningTypes are asserted to be the same as those of all the
        /// unioningTypes together, which are the Types derived from the unioningType of the ownedUnionings of
        /// this Type. For example, a Classifier for people might be the union of Classifiers for all the sexes.
        /// Similarly, a feature for people&#39;s children might be the union of features dividing them in the
        /// same ways as people in general.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661974896766_783268_1231", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.UnioningType")]
        public List<Guid> UnioningType { get; internal set; }

        /// <summary>
        /// The Usages that are features of this Definition (not necessarily owned).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565498571495_48981_27786", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        [Implements(implementation: "IDefinition.Usage")]
        public List<Guid> Usage { get; internal set; }

        /// <summary>
        /// The Usages which represent the variants of this Definition as a variation point Definition, if
        /// isVariation = true. If isVariation = false, the there must be no variants.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590979457191_746167_951", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        [RedefinedByProperty("IEnumerationDefinition.EnumeratedValue")]
        [Implements(implementation: "IDefinition.Variant")]
        public List<Guid> Variant { get; internal set; }

        /// <summary>
        /// The ownedMemberships of this Definition that are VariantMemberships. If isVariation = true, then
        /// this must be all ownedMemberships of the Definition. If isVariation = false, then
        /// variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590979005861_503124_894", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_190614_43269")]
        [Implements(implementation: "IDefinition.VariantMembership")]
        public List<Guid> VariantMembership { get; internal set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
