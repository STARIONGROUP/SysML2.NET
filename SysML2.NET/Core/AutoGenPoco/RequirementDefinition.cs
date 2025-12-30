// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A RequirementDefinition is a ConstraintDefinition that defines a requirement used in the context of
    /// a specification as a constraint that a valid solution must satisfy. The specification is relative to
    /// a specified subject, possibly in collaboration with one or more external actors.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1582990729262_130404_898", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class RequirementDefinition : IRequirementDefinition
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IData.Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The parameters of this RequirementDefinition that represent actors involved in the requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621564041941_652319_2722", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        public List<IPartUsage> QueryActorParameter()
        {
            throw new NotImplementedException("Derived property ActorParameter not yet supported");
        }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594312532679_496267_4310", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<string> AliasIds { get; set; }

        /// <summary>
        /// The owned ConstraintUsages that represent assumptions of this RequirementDefinition, which are the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementDefinition with kind =
        /// assumption.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376806647_629021_133", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        public List<IConstraintUsage> QueryAssumedConstraint()
        {
            throw new NotImplementedException("Derived property AssumedConstraint not yet supported");
        }

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674987_737648_43307", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public string DeclaredName { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594160442439_915308_4153", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedByProperty("IRequirementDefinition.ReqId")] public string DeclaredShortName { get; set; }

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
        public List<IType> QueryDifferencingType()
        {
            throw new NotImplementedException("Derived property DifferencingType not yet supported");
        }

        /// <summary>
        /// The features of this Type that have a non-null direction.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1623952188842_882068_37169", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        [RedefinedByProperty("IBehavior.Parameter")]
        public List<IFeature> QueryDirectedFeature()
        {
            throw new NotImplementedException("Derived property DirectedFeature not yet supported");
        }

        /// <summary>
        /// The usages of this Definition that are directedFeatures.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565495064714_974634_26150", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        public List<IUsage> QueryDirectedUsage()
        {
            throw new NotImplementedException("Derived property DirectedUsage not yet supported");
        }

        /// <summary>
        /// The Documentation owned by this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594150061166_345630_1621", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        public List<IDocumentation> QueryDocumentation()
        {
            throw new NotImplementedException("Derived property Documentation not yet supported");
        }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_844338_43305", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public string ElementId { get; set; }

        /// <summary>
        /// All features of this Type with isEnd = true.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1562476168385_824569_22106", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        public List<IFeature> QueryEndFeature()
        {
            throw new NotImplementedException("Derived property EndFeature not yet supported");
        }

        /// <summary>
        /// The Expressions that are steps in the calculation of the result of this Function.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543948400639_301251_20841", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_b9102da_1536346067212_587255_17343")]
        public List<IExpression> QueryExpression()
        {
            throw new NotImplementedException("Derived property Expression not yet supported");
        }

        /// <summary>
        /// The ownedMemberFeatures of the featureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_326391_43166", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        public List<IFeature> QueryFeature()
        {
            throw new NotImplementedException("Derived property Feature not yet supported");
        }

        /// <summary>
        /// The FeatureMemberships for features of this Type, which include all ownedFeatureMemberships and
        /// those inheritedMemberships that are FeatureMemberships (but does not include any
        /// importedMemberships).
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1651076866512_962346_485", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<IFeatureMembership> QueryFeatureMembership()
        {
            throw new NotImplementedException("Derived property FeatureMembership not yet supported");
        }

        /// <summary>
        /// The ConcernUsages framed by this RequirementDefinition, which are the ownedConcerns of all
        /// FramedConcernMemberships of the RequirementDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617116733499_587735_3242", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583376932997_792124_158")]
        public List<IConcernUsage> QueryFramedConcern()
        {
            throw new NotImplementedException("Derived property FramedConcern not yet supported");
        }

        /// <summary>
        /// The Memberships in this Namespace that result from the ownedImports of this Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_207869_43270", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        public List<IMembership> QueryImportedMembership()
        {
            throw new NotImplementedException("Derived property ImportedMembership not yet supported");
        }

        /// <summary>
        /// All the memberFeatures of the inheritedMemberships of this Type that are FeatureMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575499020770_15576_2334", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        public List<IFeature> QueryInheritedFeature()
        {
            throw new NotImplementedException("Derived property InheritedFeature not yet supported");
        }

        /// <summary>
        /// All Memberships inherited by this Type via Specialization or Conjugation. These are included in the
        /// derived union for the memberships of the Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1551972927538_787976_19004", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        public List<IMembership> QueryInheritedMembership()
        {
            throw new NotImplementedException("Derived property InheritedMembership not yet supported");
        }

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction in or inout.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_37384_43169", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        public List<IFeature> QueryInput()
        {
            throw new NotImplementedException("Derived property Input not yet supported");
        }

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
        public List<IType> QueryIntersectingType()
        {
            throw new NotImplementedException("Derived property IntersectingType not yet supported");
        }

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_741353_43165", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Indicates whether this Type has an ownedConjugator.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575485930816_796088_1933", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public bool QueryIsConjugated()
        {
            throw new NotImplementedException("Derived property IsConjugated not yet supported");
        }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1662070949317_79713_3658", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether this OccurrenceDefinition is constrained to represent at most one thing.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618955405499_394357_6740", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        public bool IsIndividual { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1665443500960_5561_723", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public bool QueryIsLibraryElement()
        {
            throw new NotImplementedException("Derived property IsLibraryElement not yet supported");
        }

        /// <summary>
        /// Whether this Function can be used as the function of a model-level evaluable InvocationExpression.
        /// Certain Functions from the Kernel Functions Library are considered to have isModelLevelEvaluable =
        /// true. For all other Functions it is false.<strong>Note:</strong> See the specification of the KerML
        /// concrete syntax notation for Expressions for an identification of which library Functions are
        /// model-level evaluable.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617395221463_139517_26381", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public bool QueryIsModelLevelEvaluable()
        {
            throw new NotImplementedException("Derived property IsModelLevelEvaluable not yet supported");
        }

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.(A Type gives conditions that must be met by whatever it classifies, but when isSufficient
        /// is false, things may meet those conditions but still not be classified by the Type. For example, a
        /// Type Car that is not sufficient could require everything it classifies to have four wheels, but not
        /// all four wheeled things would classify as cars. However, if the Type Car were sufficient, it would
        /// classify all four-wheeled things.)
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1564072709069_937523_30797", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        public bool IsSufficient { get; set; }

        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590978283180_265362_419", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public bool IsVariation { get; set; }

        /// <summary>
        /// The set of all member Elements of this Namespace, which are the memberElements of all memberships of
        /// the Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_644335_43267", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<IElement> QueryMember()
        {
            throw new NotImplementedException("Derived property Member not yet supported");
        }

        /// <summary>
        /// All Memberships in this Namespace, including (at least) the union of ownedMemberships and
        /// importedMemberships.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674962_198288_43183", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: true, isUnique: true, defaultValue: null)]
        public List<IMembership> QueryMembership()
        {
            throw new NotImplementedException("Derived property Membership not yet supported");
        }

        /// <summary>
        /// An ownedMember of this Type that is a Multiplicity, which constraints the cardinality of the Type.
        /// If there is no such ownedMember, then the cardinality of this Type is constrained by all the
        /// Multiplicity constraints applicable to any direct supertypes.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1573095852093_324833_5396", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        public IMultiplicity QueryMultiplicity()
        {
            throw new NotImplementedException("Derived property Multiplicity not yet supported");
        }

        /// <summary>
        /// The name to be used for this Element during name resolution within its owningNamespace. This is
        /// derived using the effectiveName() operation. By default, it is the same as the declaredName, but
        /// this is overridden for certain kinds of Elements to compute a name even when the declaredName is
        /// null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617485009541_709355_27528", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public string QueryName()
        {
            throw new NotImplementedException("Derived property Name not yet supported");
        }

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction out or inout.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_365618_43170", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        public List<IFeature> QueryOutput()
        {
            throw new NotImplementedException("Derived property Output not yet supported");
        }

        /// <summary>
        /// The ActionUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591479011613_547927_1091", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943849505_989631_257")]
        public List<IActionUsage> QueryOwnedAction()
        {
            throw new NotImplementedException("Derived property OwnedAction not yet supported");
        }

        /// <summary>
        /// The AllocationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611430819239_430196_1024", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591480607506_951212_2333")]
        public List<IAllocationUsage> QueryOwnedAllocation()
        {
            throw new NotImplementedException("Derived property OwnedAllocation not yet supported");
        }

        /// <summary>
        /// The AnalysisCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591152747086_367030_3846", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_59601fc_1590257108055_7496_483")]
        public List<IAnalysisCaseUsage> QueryOwnedAnalysisCase()
        {
            throw new NotImplementedException("Derived property OwnedAnalysisCase not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Element that are Annotations, for which this Element is the
        /// annotatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594152527165_702130_2500", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094430277_599480_18543")]
        public List<IAnnotation> QueryOwnedAnnotation()
        {
            throw new NotImplementedException("Derived property OwnedAnnotation not yet supported");
        }

        /// <summary>
        /// The AttributeUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591500614097_490259_4413", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        public List<IAttributeUsage> QueryOwnedAttribute()
        {
            throw new NotImplementedException("Derived property OwnedAttribute not yet supported");
        }

        /// <summary>
        /// The CalculationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1588215335104_898924_667", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479011613_547927_1091")]
        public List<ICalculationUsage> QueryOwnedCalculation()
        {
            throw new NotImplementedException("Derived property OwnedCalculation not yet supported");
        }

        /// <summary>
        /// The code>CaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_59601fc_1590257108055_7496_483", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1588215335104_898924_667")]
        public List<ICaseUsage> QueryOwnedCase()
        {
            throw new NotImplementedException("Derived property OwnedCase not yet supported");
        }

        /// <summary>
        /// The ConcernUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617051597354_928367_1357", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000559760_444344_1273")]
        public List<IConcernUsage> QueryOwnedConcern()
        {
            throw new NotImplementedException("Derived property OwnedConcern not yet supported");
        }

        /// <summary>
        /// A Conjugation owned by this Type for which the Type is the originalType.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575482646809_280165_440", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1575482490144_309557_300")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        public IConjugation QueryOwnedConjugator()
        {
            throw new NotImplementedException("Derived property OwnedConjugator not yet supported");
        }

        /// <summary>
        /// The ConnectorAsUsages that are ownedUsages of this Definition. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowUsages because these are ConnectorAsUsages
        /// even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591480607506_951212_2333", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        public List<IConnectorAsUsage> QueryOwnedConnection()
        {
            throw new NotImplementedException("Derived property OwnedConnection not yet supported");
        }

        /// <summary>
        /// The ConstraintUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578068081992_244000_1803", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943849505_989631_257")]
        public List<IConstraintUsage> QueryOwnedConstraint()
        {
            throw new NotImplementedException("Derived property OwnedConstraint not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Type that are Differencings, having this Type as their
        /// typeDifferenced.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661871168454_98082_797", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        public List<IDifferencing> QueryOwnedDifferencing()
        {
            throw new NotImplementedException("Derived property OwnedDifferencing not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Type that are Disjoinings, for which the Type is the typeDisjoined
        /// Type.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1627447519613_145554_370", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623183194914_502526_616")]
        public List<IDisjoining> QueryOwnedDisjoining()
        {
            throw new NotImplementedException("Derived property OwnedDisjoining not yet supported");
        }

        /// <summary>
        /// The Elements owned by this Element, derived as the ownedRelatedElements of the ownedRelationships of
        /// this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_112608_17278", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<IElement> QueryOwnedElement()
        {
            throw new NotImplementedException("Derived property OwnedElement not yet supported");
        }

        /// <summary>
        /// All endFeatures of this Type that are ownedFeatures.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1563834516278_687758_20652", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1562476168385_824569_22106")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        public List<IFeature> QueryOwnedEndFeature()
        {
            throw new NotImplementedException("Derived property OwnedEndFeature not yet supported");
        }

        /// <summary>
        /// The EnumerationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606946600508_763872_252", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591500614097_490259_4413")]
        public List<IEnumerationUsage> QueryOwnedEnumeration()
        {
            throw new NotImplementedException("Derived property OwnedEnumeration not yet supported");
        }

        /// <summary>
        /// The ownedMemberFeatures of the ownedFeatureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_226999_43167", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        public List<IFeature> QueryOwnedFeature()
        {
            throw new NotImplementedException("Derived property OwnedFeature not yet supported");
        }

        /// <summary>
        /// The ownedMemberships of this Type that are FeatureMemberships, for which the Type is the owningType.
        /// Each such FeatureMembership identifies an ownedFeature of the Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_868417_43171", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_190614_43269")]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1651076866512_962346_485")]
        public List<IFeatureMembership> QueryOwnedFeatureMembership()
        {
            throw new NotImplementedException("Derived property OwnedFeatureMembership not yet supported");
        }

        /// <summary>
        /// The FlowUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624055201422_104863_1697", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591480607506_951212_2333")]
        public List<IFlowUsage> QueryOwnedFlow()
        {
            throw new NotImplementedException("Derived property OwnedFlow not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Namespace that are Imports, for which the Namespace is the
        /// importOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674974_746786_43247", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        public List<IImport> QueryOwnedImport()
        {
            throw new NotImplementedException("Derived property OwnedImport not yet supported");
        }

        /// <summary>
        /// The InterfaceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591498709150_220812_4128", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591480607506_951212_2333")]
        public List<IInterfaceUsage> QueryOwnedInterface()
        {
            throw new NotImplementedException("Derived property OwnedInterface not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Type that are Intersectings, have the Type as their typeIntersected.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623242552144_910757_524", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        public List<IIntersecting> QueryOwnedIntersecting()
        {
            throw new NotImplementedException("Derived property OwnedIntersecting not yet supported");
        }

        /// <summary>
        /// The ItemUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591482567975_649284_3005", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943849505_989631_257")]
        public List<IItemUsage> QueryOwnedItem()
        {
            throw new NotImplementedException("Derived property OwnedItem not yet supported");
        }

        /// <summary>
        /// The owned members of this Namespace, which are the <cpde>ownedMemberElements of the ownedMemberships
        /// of the Namespace.</cpde>
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_259543_43268", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        public List<IElement> QueryOwnedMember()
        {
            throw new NotImplementedException("Derived property OwnedMember not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Namespace that are Memberships, for which the Namespace is the
        /// membershipOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_190614_43269", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        public List<IMembership> QueryOwnedMembership()
        {
            throw new NotImplementedException("Derived property OwnedMembership not yet supported");
        }

        /// <summary>
        /// The MetadataUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1661488358064_457109_2881", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591482567975_649284_3005")]
        public List<IMetadataUsage> QueryOwnedMetadata()
        {
            throw new NotImplementedException("Derived property OwnedMetadata not yet supported");
        }

        /// <summary>
        /// The OccurrenceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618943849505_989631_257", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        public List<IOccurrenceUsage> QueryOwnedOccurrence()
        {
            throw new NotImplementedException("Derived property OwnedOccurrence not yet supported");
        }

        /// <summary>
        /// The PartUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591496643392_630316_3279", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591482567975_649284_3005")]
        public List<IPartUsage> QueryOwnedPart()
        {
            throw new NotImplementedException("Derived property OwnedPart not yet supported");
        }

        /// <summary>
        /// The PortUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565494319970_455996_25799", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        public List<IPortUsage> QueryOwnedPort()
        {
            throw new NotImplementedException("Derived property OwnedPort not yet supported");
        }

        /// <summary>
        /// The ReferenceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591477471991_39731_908", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        public List<IReferenceUsage> QueryOwnedReference()
        {
            throw new NotImplementedException("Derived property OwnedReference not yet supported");
        }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092026091_217766_16748", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        public List<IRelationship> OwnedRelationship { get; set; }

        /// <summary>
        /// The RenderingUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596741437225_963350_6474", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591496643392_630316_3279")]
        public List<IRenderingUsage> QueryOwnedRendering()
        {
            throw new NotImplementedException("Derived property OwnedRendering not yet supported");
        }

        /// <summary>
        /// The RequirementUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583000559760_444344_1273", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1578068081992_244000_1803")]
        public List<IRequirementUsage> QueryOwnedRequirement()
        {
            throw new NotImplementedException("Derived property OwnedRequirement not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Type that are Specializations, for which the Type is the specific
        /// Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_579676_43168", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674984_558067_43292")]
        public List<ISpecialization> QueryOwnedSpecialization()
        {
            throw new NotImplementedException("Derived property OwnedSpecialization not yet supported");
        }

        /// <summary>
        /// The StateUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575587977045_745776_941", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479011613_547927_1091")]
        public List<IStateUsage> QueryOwnedState()
        {
            throw new NotImplementedException("Derived property OwnedState not yet supported");
        }

        /// <summary>
        /// The ownedSpecializations of this Classifier that are Subclassifications, for which this Classifier
        /// is the subclassifier.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543189170643_419862_25507", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_579676_43168")]
        public List<ISubclassification> QueryOwnedSubclassification()
        {
            throw new NotImplementedException("Derived property OwnedSubclassification not yet supported");
        }

        /// <summary>
        /// The TransitionUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578598061680_350995_3923", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565479686637_967933_23236")]
        public List<ITransitionUsage> QueryOwnedTransition()
        {
            throw new NotImplementedException("Derived property OwnedTransition not yet supported");
        }

        /// <summary>
        /// The ownedRelationships of this Type that are Unionings, having the Type as their typeUnioned.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661869978505_968809_460", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        public List<IUnioning> QueryOwnedUnioning()
        {
            throw new NotImplementedException("Derived property OwnedUnioning not yet supported");
        }

        /// <summary>
        /// The Usages that are ownedFeatures of this Definition.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565479686637_967933_23236", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        public List<IUsage> QueryOwnedUsage()
        {
            throw new NotImplementedException("Derived property OwnedUsage not yet supported");
        }

        /// <summary>
        /// The UseCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621461106608_978605_945", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_59601fc_1590257108055_7496_483")]
        public List<IUseCaseUsage> QueryOwnedUseCase()
        {
            throw new NotImplementedException("Derived property OwnedUseCase not yet supported");
        }

        /// <summary>
        /// The VerificationCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596821523387_872104_10416", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_59601fc_1590257108055_7496_483")]
        public List<IVerificationCaseUsage> QueryOwnedVerificationCase()
        {
            throw new NotImplementedException("Derived property OwnedVerificationCase not yet supported");
        }

        /// <summary>
        /// The ViewUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596644570381_840567_784", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591496643392_630316_3279")]
        public List<IViewUsage> QueryOwnedView()
        {
            throw new NotImplementedException("Derived property OwnedView not yet supported");
        }

        /// <summary>
        /// The ViewpointUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596649828408_673531_3683", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000559760_444344_1273")]
        public List<IViewpointUsage> QueryOwnedViewpoint()
        {
            throw new NotImplementedException("Derived property OwnedViewpoint not yet supported");
        }

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_744477_17277", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public IElement QueryOwner()
        {
            throw new NotImplementedException("Derived property Owner not yet supported");
        }

        /// <summary>
        /// The owningRelationship of this Element, if that Relationship is a Membership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674972_622493_43236", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674973_469277_43243")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_482273_43303")]
        public IOwningMembership QueryOwningMembership()
        {
            throw new NotImplementedException("Derived property OwningMembership not yet supported");
        }

        /// <summary>
        /// The Namespace that owns this Element, which is the membershipOwningNamespace of the owningMembership
        /// of this Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_474739_43306", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674980_717955_43271")]
        public INamespace QueryOwningNamespace()
        {
            throw new NotImplementedException("Derived property OwningNamespace not yet supported");
        }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_482273_43303", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        public IRelationship OwningRelationship { get; set; }

        /// <summary>
        /// The parameters of this Behavior, which are defined as its directedFeatures, whose values are passed
        /// into and/or out of a performance of the Behavior.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543948010065_362066_20413", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        public List<IFeature> QueryParameter()
        {
            throw new NotImplementedException("Derived property Parameter not yet supported");
        }

        /// <summary>
        /// The full ownership-qualified name of this Element, represented in a form that is valid according to
        /// the KerML textual concrete syntax for qualified names (including use of unrestricted name notation
        /// and escaped characters, as necessary). The qualifiedName is null if this Element has no
        /// owningNamespace or if there is not a complete ownership chain of named Namespaces from a root
        /// Namespace to this Element. If the owningNamespace has other Elements with the same name as this one,
        /// then the qualifiedName is null for all such Elements other than the first.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611356604987_900871_594", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public string QueryQualifiedName()
        {
            throw new NotImplementedException("Derived property QualifiedName not yet supported");
        }

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementDefinition (used, e.g., to link it to
        /// an original requirement text in some source document), which is the declaredShortName for the
        /// RequirementDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376411386_270321_92", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1594160442439_915308_4153")]
        public string ReqId { get; set; }

        /// <summary>
        /// The owned ConstraintUsages that represent requirements of this RequirementDefinition, derived as the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementDefinition with kind =
        /// requirement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376932997_792124_158", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        public List<IConstraintUsage> QueryRequiredConstraint()
        {
            throw new NotImplementedException("Derived property RequiredConstraint not yet supported");
        }

        /// <summary>
        /// The object or value that is the result of evaluating the Function.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543948912268_88159_21323", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674960_365618_43170")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        public IFeature QueryResult()
        {
            throw new NotImplementedException("Derived property Result not yet supported");
        }

        /// <summary>
        /// The short name to be used for this Element during name resolution within its owningNamespace. This
        /// is derived using the effectiveShortName() operation. By default, it is the same as the
        /// declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even
        /// when the declaredName is null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1673496405504_544235_24", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public string QueryShortName()
        {
            throw new NotImplementedException("Derived property ShortName not yet supported");
        }

        /// <summary>
        /// The parameters of this RequirementDefinition that represent stakeholders for th requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624033010374_29375_40166", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        public List<IPartUsage> QueryStakeholderParameter()
        {
            throw new NotImplementedException("Derived property StakeholderParameter not yet supported");
        }

        /// <summary>
        /// The Steps that make up this Behavior.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1536346067212_587255_17343", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        public List<IStep> QueryStep()
        {
            throw new NotImplementedException("Derived property Step not yet supported");
        }

        /// <summary>
        /// The parameter of this RequirementDefinition that represents its subject.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189007408_784255_586", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543948010065_362066_20413")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_48981_27786")]
        public IUsage QuerySubjectParameter()
        {
            throw new NotImplementedException("Derived property SubjectParameter not yet supported");
        }

        /// <summary>
        /// An optional textual statement of the requirement represented by this RequirementDefinition, derived
        /// from the bodies of the documentation of the RequirementDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376433122_189839_94", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<string> QueryText()
        {
            throw new NotImplementedException("Derived property Text not yet supported");
        }

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594154758493_640290_3388", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        public List<ITextualRepresentation> QueryTextualRepresentation()
        {
            throw new NotImplementedException("Derived property TextualRepresentation not yet supported");
        }

        /// <summary>
        /// The interpretations of a Type with unioningTypes are asserted to be the same as those of all the
        /// unioningTypes together, which are the Types derived from the unioningType of the ownedUnionings of
        /// this Type. For example, a Classifier for people might be the union of Classifiers for all the sexes.
        /// Similarly, a feature for people&#39;s children might be the union of features dividing them in the
        /// same ways as people in general.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661974896766_783268_1231", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<IType> QueryUnioningType()
        {
            throw new NotImplementedException("Derived property UnioningType not yet supported");
        }

        /// <summary>
        /// The Usages that are features of this Definition (not necessarily owned).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565498571495_48981_27786", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        public List<IUsage> QueryUsage()
        {
            throw new NotImplementedException("Derived property Usage not yet supported");
        }

        /// <summary>
        /// The Usages which represent the variants of this Definition as a variation point Definition, if
        /// isVariation = true. If isVariation = false, the there must be no variants.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590979457191_746167_951", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        public List<IUsage> QueryVariant()
        {
            throw new NotImplementedException("Derived property Variant not yet supported");
        }

        /// <summary>
        /// The ownedMemberships of this Definition that are VariantMemberships. If isVariation = true, then
        /// this must be all ownedMemberships of the Definition. If isVariation = false, then
        /// variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590979005861_503124_894", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_190614_43269")]
        public List<IVariantMembership> QueryVariantMembership()
        {
            throw new NotImplementedException("Derived property VariantMembership not yet supported");
        }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
