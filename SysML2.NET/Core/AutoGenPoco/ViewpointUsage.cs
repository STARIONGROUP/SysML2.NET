// -------------------------------------------------------------------------------------------------
// <copyright file="ViewpointUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Views
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
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
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ViewpointUsage is a Usage of a ViewpointDefinition.
    /// </summary>
    [Class(xmiId: "_19_0_2_59601fc_1583087291401_74297_590", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class ViewpointUsage : IViewpointUsage
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IData.Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The parameters of this RequirementUsage that represent actors involved in the requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621564075474_350859_2735", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        [Implements(implementation: "IRequirementUsage.ActorParameter")]
        public List<IPartUsage> actorParameter => this.ComputeActorParameter();

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594312532679_496267_4310", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.AliasIds")]
        public List<string> AliasIds { get; set; } = [];

        /// <summary>
        /// The owned ConstraintUsages that represent assumptions of this RequirementUsage, derived as the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementUsage with kind =
        /// assumption.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583377612865_991722_535", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        [Implements(implementation: "IRequirementUsage.AssumedConstraint")]
        public List<IConstraintUsage> assumedConstraint => this.ComputeAssumedConstraint();

        /// <summary>
        /// The Behaviors that type this Step.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1536346315176_954314_17388", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674969_376003_43216")]
        [RedefinedByProperty("IExpression.Function")]
        [Implements(implementation: "IStep.Behavior")]
        List<IBehavior> Kernel.Behaviors.IStep.behavior => ((SysML2.NET.Core.POCO.Kernel.Functions.IExpression)this).function != null ? [((SysML2.NET.Core.POCO.Kernel.Functions.IExpression)this).function] : [];

        /// <summary>
        /// The Feature that are chained together to determine the values of this Feature, derived from the
        /// chainingFeatures of the ownedFeatureChainings of this Feature, in the same order. The values of a
        /// Feature with chainingFeatures are the same as values of the last Feature in the chain, which can be
        /// found by starting with the values of the first Feature (for each instance of the domain of the
        /// original Feature), then using each of those as domain instances to find the values of the second
        /// Feature in chainingFeatures, and so on, to values of the last Feature.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1619792219511_543311_445", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IFeature.ChainingFeature")]
        public List<IFeature> chainingFeature => this.ComputeChainingFeature();

        /// <summary>
        /// The (single) Predicate that is the type of this ConstraintUsage. Nominally, this will be a
        /// ConstraintDefinition, but other kinds of Predicates are also allowed, to permit use of Predicates
        /// from the Kernel Model Libraries.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578067546711_751168_1745", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1578025035149_386_969")]
        [RedefinedByProperty("IRequirementUsage.RequirementDefinition")]
        [Implements(implementation: "IConstraintUsage.ConstraintDefinition")]
        IPredicate Systems.Constraints.IConstraintUsage.constraintDefinition => ((SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage)this).requirementDefinition;

        /// <summary>
        /// The second chainingFeature of the crossedFeature of the ownedCrossSubsetting of this Feature, if it
        /// has one. Semantically, the values of the crossFeature of an end Feature must include all values of
        /// the end Feature obtained when navigating from values of the other end Features of the same
        /// owningType.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1689616227528_355910_218", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IFeature.CrossFeature")]
        public IFeature crossFeature => this.ComputeCrossFeature();

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
        [RedefinedByProperty("IRequirementUsage.ReqId")]
        [Implements(implementation: "IElement.DeclaredShortName")]
        string Root.Elements.IElement.DeclaredShortName
        {
            get => this.ReqId;
            set
            {
                this.ReqId = value;
            }
        }

        /// <summary>
        /// The Classifiers that are the types of this Usage. Nominally, these are Definitions, but other kinds
        /// of Kernel Classifiers are also allowed, to permit use of Classifiers from the Kernel Model
        /// Libraries.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591477641252_179221_958", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674969_376003_43216")]
        [RedefinedByProperty("IOccurrenceUsage.OccurrenceDefinition")]
        [Implements(implementation: "IUsage.Definition")]
        List<IClassifier> Systems.DefinitionAndUsage.IUsage.definition => [.. this.occurrenceDefinition];

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
        public List<IType> differencingType => this.ComputeDifferencingType();

        /// <summary>
        /// The features of this Type that have a non-null direction.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1623952188842_882068_37169", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        [RedefinedByProperty("IStep.Parameter")]
        [Implements(implementation: "IType.DirectedFeature")]
        List<IFeature> Core.Types.IType.directedFeature => [.. this.parameter];

        /// <summary>
        /// The usages of this Usage that are directedFeatures.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591217699198_66279_508", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        [Implements(implementation: "IUsage.DirectedUsage")]
        public List<IUsage> directedUsage => this.ComputeDirectedUsage();

        /// <summary>
        /// Indicates how values of this Feature are determined or used (as specified for the
        /// FeatureDirectionKind).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674994_447677_43347", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IFeature.Direction")]
        public FeatureDirectionKind? Direction { get; set; }

        /// <summary>
        /// The Documentation owned by this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594150061166_345630_1621", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        [Implements(implementation: "IElement.Documentation")]
        public List<IDocumentation> documentation => this.ComputeDocumentation();

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
        public List<IFeature> endFeature => this.ComputeEndFeature();

        /// <summary>
        /// The Type that is related to this Feature by an EndFeatureMembership in which the Feature is an
        /// ownedMemberFeature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1563834516279_920295_20653", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1562476168386_366266_22107")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_592215_43200")]
        [Implements(implementation: "IFeature.EndOwningType")]
        public IType endOwningType => this.ComputeEndOwningType();

        /// <summary>
        /// The ownedMemberFeatures of the featureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_326391_43166", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        [Implements(implementation: "IType.Feature")]
        public List<IFeature> feature => this.ComputeFeature();

        /// <summary>
        /// The FeatureMemberships for features of this Type, which include all ownedFeatureMemberships and
        /// those inheritedMemberships that are FeatureMemberships (but does not include any
        /// importedMemberships).
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1651076866512_962346_485", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.FeatureMembership")]
        public List<IFeatureMembership> featureMembership => this.ComputeFeatureMembership();

        /// <summary>
        /// The last of the chainingFeatures of this Feature, if it has any. Otherwise, this Feature itself.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1715790852907_110671_19", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IFeature.FeatureTarget")]
        public IFeature featureTarget => this.ComputeFeatureTarget();

        /// <summary>
        /// Types that feature this Feature, such that any instance in the domain of the Feature must be
        /// classified by all of these Types, including at least all the featuringTypes of its typeFeaturings. 
        /// If the Feature is chained, then the featuringTypes of the first Feature in the chain are also
        /// featuringTypes of the chained Feature.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1603905619975_304385_743", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IFeature.FeaturingType")]
        public List<IType> featuringType => this.ComputeFeaturingType();

        /// <summary>
        /// The ConcernUsages framed by this RequirementUsage, which are the ownedConcerns of all
        /// FramedConcernMemberships of the RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617116922864_514612_3264", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583377448339_252740_390")]
        [Implements(implementation: "IRequirementUsage.FramedConcern")]
        public List<IConcernUsage> framedConcern => this.ComputeFramedConcern();

        /// <summary>
        /// The Function that types this Expression.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543948477241_299049_20934", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_b9102da_1536346315176_954314_17388")]
        [RedefinedByProperty("IBooleanExpression.Predicate")]
        [Implements(implementation: "IExpression.Function")]
        IFunction Kernel.Functions.IExpression.function => ((SysML2.NET.Core.POCO.Kernel.Functions.IBooleanExpression)this).predicate;

        /// <summary>
        /// The Memberships in this Namespace that result from the ownedImports of this Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_207869_43270", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [Implements(implementation: "INamespace.ImportedMembership")]
        public List<IMembership> importedMembership => this.ComputeImportedMembership();

        /// <summary>
        /// The at most one occurrenceDefinition that has isIndividual = true.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618958878775_52798_7090", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943843466_158863_236")]
        [Implements(implementation: "IOccurrenceUsage.IndividualDefinition")]
        public IOccurrenceDefinition individualDefinition => this.ComputeIndividualDefinition();

        /// <summary>
        /// All the memberFeatures of the inheritedMemberships of this Type that are FeatureMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575499020770_15576_2334", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        [Implements(implementation: "IType.InheritedFeature")]
        public List<IFeature> inheritedFeature => this.ComputeInheritedFeature();

        /// <summary>
        /// All Memberships inherited by this Type via Specialization or Conjugation. These are included in the
        /// derived union for the memberships of the Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1551972927538_787976_19004", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [Implements(implementation: "IType.InheritedMembership")]
        public List<IMembership> inheritedMembership => this.ComputeInheritedMembership();

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction in or inout.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_37384_43169", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [Implements(implementation: "IType.Input")]
        public List<IFeature> input => this.ComputeInput();

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
        public List<IType> intersectingType => this.ComputeIntersectingType();

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_741353_43165", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IType.IsAbstract")]
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after its featuring instance no longer does and cannot be values of another composite
        /// feature that is not on the same featuring instance.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674970_331870_43224", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsComposite")]
        public bool IsComposite { get; set; }

        /// <summary>
        /// Indicates whether this Type has an ownedConjugator.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575485930816_796088_1933", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.IsConjugated")]
        public bool isConjugated => this.ComputeIsConjugated();

        /// <summary>
        /// If isVariable is true, then whether the value of this Feature nevertheless does not change over all
        /// snapshots of its owningType.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674993_300560_43342", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsConstant")]
        public bool IsConstant { get; set; }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Features.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674992_500504_43341", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsDerived")]
        public bool IsDerived { get; set; }

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
        [Implements(implementation: "IFeature.IsEnd")]
        public bool IsEnd { get; set; }

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
        /// Whether this OccurrenceUsage represents the usage of the specific individual represented by its
        /// individualDefinition.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618959865886_548379_7149", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IOccurrenceUsage.IsIndividual")]
        public bool IsIndividual { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1665443500960_5561_723", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.IsLibraryElement")]
        public bool isLibraryElement => this.ComputeIsLibraryElement();

        /// <summary>
        /// Whether this Expression meets the constraints necessary to be evaluated at model level, that is,
        /// using metadata within the model.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1609957047704_424471_48", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IExpression.IsModelLevelEvaluable")]
        public bool isModelLevelEvaluable => this.ComputeIsModelLevelEvaluable();

        /// <summary>
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674969_728225_43215", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsOrdered")]
        public bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the domain of
        /// the Feature and represent the same thing as those instances.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1559231981638_234817_22063", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsPortion")]
        public bool IsPortion { get; set; }

        /// <summary>
        /// Whether this Usage is a referential Usage, that is, it has isComposite = false.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624035114787_488767_41423", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IUsage.IsReference")]
        public bool isReference => this.ComputeIsReference();

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
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674968_321342_43214", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [Implements(implementation: "IFeature.IsUnique")]
        public bool IsUnique { get; set; } = true;

        /// <summary>
        /// Whether the value of this Feature might vary over time. That is, whether the Feature may have a
        /// different value for each snapshot of an owningType that is an Occurrence.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1725998273002_23711_212", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [RedefinedByProperty("IUsage.MayTimeVary")]
        [Implements(implementation: "IFeature.IsVariable")]
        bool Core.Features.IFeature.IsVariable
        {
            get => this.mayTimeVary;
            set { }
        }

        /// <summary>
        /// Whether this Usage is for a variation point or not. If true, then all the memberships of the Usage
        /// must be VariantMemberships.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590978312364_290951_421", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IUsage.IsVariation")]
        public bool IsVariation { get; set; }

        /// <summary>
        /// Whether this Usage may be time varying (that is, whether it is featured by the snapshots of its
        /// owningType, rather than being featured by the owningType itself). However, if isConstant is also
        /// true, then the value of the Usage is nevertheless constant over the entire duration of an instance
        /// of its owningType (that is, it has the same value on all snapshots).The property mayTimeVary
        /// redefines the KerML property Feature::isVariable, making it derived. The property isConstant is
        /// inherited from Feature.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1737227200362_771035_69", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_2022x_2_12e503d9_1725998273002_23711_212")]
        [Implements(implementation: "IUsage.MayTimeVary")]
        public bool mayTimeVary => this.ComputeMayTimeVary();

        /// <summary>
        /// The set of all member Elements of this Namespace, which are the memberElements of all memberships of
        /// the Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_644335_43267", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "INamespace.Member")]
        public List<IElement> member => this.ComputeMember();

        /// <summary>
        /// All Memberships in this Namespace, including (at least) the union of ownedMemberships and
        /// importedMemberships.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674962_198288_43183", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: true, isUnique: true, defaultValue: null)]
        [Implements(implementation: "INamespace.Membership")]
        public List<IMembership> membership => this.ComputeMembership();

        /// <summary>
        /// An ownedMember of this Type that is a Multiplicity, which constraints the cardinality of the Type.
        /// If there is no such ownedMember, then the cardinality of this Type is constrained by all the
        /// Multiplicity constraints applicable to any direct supertypes.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1573095852093_324833_5396", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        [Implements(implementation: "IType.Multiplicity")]
        public IMultiplicity multiplicity => this.ComputeMultiplicity();

        /// <summary>
        /// The name to be used for this Element during name resolution within its owningNamespace. This is
        /// derived using the effectiveName() operation. By default, it is the same as the declaredName, but
        /// this is overridden for certain kinds of Elements to compute a name even when the declaredName is
        /// null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617485009541_709355_27528", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Name")]
        public string name => this.ComputeName();

        /// <summary>
        /// The ActionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565501745142_70952_31609", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943853976_48759_278")]
        [Implements(implementation: "IUsage.NestedAction")]
        public List<IActionUsage> nestedAction => this.ComputeNestedAction();

        /// <summary>
        /// The AllocationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1611430983774_648557_1053", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479754895_422988_1242")]
        [Implements(implementation: "IUsage.NestedAllocation")]
        public List<IAllocationUsage> nestedAllocation => this.ComputeNestedAllocation();

        /// <summary>
        /// The AnalysisCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591152666850_226358_3749", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591135021853_494751_737")]
        [Implements(implementation: "IUsage.NestedAnalysisCase")]
        public List<IAnalysisCaseUsage> nestedAnalysisCase => this.ComputeNestedAnalysisCase();

        /// <summary>
        /// The code>AttributeUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591500785349_111324_4486", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        [Implements(implementation: "IUsage.NestedAttribute")]
        public List<IAttributeUsage> nestedAttribute => this.ComputeNestedAttribute();

        /// <summary>
        /// The CalculationUsage that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1588215112283_215964_632", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565501745142_70952_31609")]
        [Implements(implementation: "IUsage.NestedCalculation")]
        public List<ICalculationUsage> nestedCalculation => this.ComputeNestedCalculation();

        /// <summary>
        /// The CaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591135021853_494751_737", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1588215112283_215964_632")]
        [Implements(implementation: "IUsage.NestedCase")]
        public List<ICaseUsage> nestedCase => this.ComputeNestedCase();

        /// <summary>
        /// The ConcernUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617051711833_106553_1460", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000447195_878123_1244")]
        [Implements(implementation: "IUsage.NestedConcern")]
        public List<IConcernUsage> nestedConcern => this.ComputeNestedConcern();

        /// <summary>
        /// The ConnectorAsUsages that are nestedUsages of this Usage. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowConnectionUsages because these are
        /// ConnectorAsUsages even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591479754895_422988_1242", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        [Implements(implementation: "IUsage.NestedConnection")]
        public List<IConnectorAsUsage> nestedConnection => this.ComputeNestedConnection();

        /// <summary>
        /// The ConstraintUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578067664051_434365_1774", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943853976_48759_278")]
        [Implements(implementation: "IUsage.NestedConstraint")]
        public List<IConstraintUsage> nestedConstraint => this.ComputeNestedConstraint();

        /// <summary>
        /// The code>EnumerationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1606946589000_158124_239", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591500785349_111324_4486")]
        [Implements(implementation: "IUsage.NestedEnumeration")]
        public List<IEnumerationUsage> nestedEnumeration => this.ComputeNestedEnumeration();

        /// <summary>
        /// The code>FlowUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624054938718_124518_1464", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479754895_422988_1242")]
        [Implements(implementation: "IUsage.NestedFlow")]
        public List<IFlowUsage> nestedFlow => this.ComputeNestedFlow();

        /// <summary>
        /// The InterfaceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591498454569_383419_3839", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591479754895_422988_1242")]
        [Implements(implementation: "IUsage.NestedInterface")]
        public List<IInterfaceUsage> nestedInterface => this.ComputeNestedInterface();

        /// <summary>
        /// The ItemUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591482421103_284620_2978", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1618943853976_48759_278")]
        [Implements(implementation: "IUsage.NestedItem")]
        public List<IItemUsage> nestedItem => this.ComputeNestedItem();

        /// <summary>
        /// The MetadataUsages that are nestedUsages of this of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1661488589862_120785_2970", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591482421103_284620_2978")]
        [Implements(implementation: "IUsage.NestedMetadata")]
        public List<IMetadataUsage> nestedMetadata => this.ComputeNestedMetadata();

        /// <summary>
        /// The OccurrenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618943853976_48759_278", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        [Implements(implementation: "IUsage.NestedOccurrence")]
        public List<IOccurrenceUsage> nestedOccurrence => this.ComputeNestedOccurrence();

        /// <summary>
        /// The PartUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591496406876_479979_3188", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591482421103_284620_2978")]
        [Implements(implementation: "IUsage.NestedPart")]
        public List<IPartUsage> nestedPart => this.ComputeNestedPart();

        /// <summary>
        /// The PortUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565494459494_859367_26042", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        [Implements(implementation: "IUsage.NestedPort")]
        public List<IPortUsage> nestedPort => this.ComputeNestedPort();

        /// <summary>
        /// The ReferenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591477541360_47573_933", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        [Implements(implementation: "IUsage.NestedReference")]
        public List<IReferenceUsage> nestedReference => this.ComputeNestedReference();

        /// <summary>
        /// The RenderingUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596741501454_147708_6545", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591496406876_479979_3188")]
        [Implements(implementation: "IUsage.NestedRendering")]
        public List<IRenderingUsage> nestedRendering => this.ComputeNestedRendering();

        /// <summary>
        /// The RequirementUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583000447195_878123_1244", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1578067664051_434365_1774")]
        [Implements(implementation: "IUsage.NestedRequirement")]
        public List<IRequirementUsage> nestedRequirement => this.ComputeNestedRequirement();

        /// <summary>
        /// The StateUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575587743891_973819_756", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565501745142_70952_31609")]
        [Implements(implementation: "IUsage.NestedState")]
        public List<IStateUsage> nestedState => this.ComputeNestedState();

        /// <summary>
        /// The TransitionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578597913303_768272_3894", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565472757327_162097_21259")]
        [Implements(implementation: "IUsage.NestedTransition")]
        public List<ITransitionUsage> nestedTransition => this.ComputeNestedTransition();

        /// <summary>
        /// The Usages that are ownedFeatures of this Usage.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565472757327_162097_21259", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        [Implements(implementation: "IUsage.NestedUsage")]
        public List<IUsage> nestedUsage => this.ComputeNestedUsage();

        /// <summary>
        /// The UseCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1621463992900_247262_1080", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591135021853_494751_737")]
        [Implements(implementation: "IUsage.NestedUseCase")]
        public List<IUseCaseUsage> nestedUseCase => this.ComputeNestedUseCase();

        /// <summary>
        /// The VerificationCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596821592100_42801_10499", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591135021853_494751_737")]
        [Implements(implementation: "IUsage.NestedVerificationCase")]
        public List<IVerificationCaseUsage> nestedVerificationCase => this.ComputeNestedVerificationCase();

        /// <summary>
        /// The ViewUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596644669126_858176_809", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591496406876_479979_3188")]
        [Implements(implementation: "IUsage.NestedView")]
        public List<IViewUsage> nestedView => this.ComputeNestedView();

        /// <summary>
        /// The ViewpointUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596649930212_443356_3818", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1583000447195_878123_1244")]
        [Implements(implementation: "IUsage.NestedViewpoint")]
        public List<IViewpointUsage> nestedViewpoint => this.ComputeNestedViewpoint();

        /// <summary>
        /// The Classes that are the types of this OccurrenceUsage. Nominally, these are OccurrenceDefinitions,
        /// but other kinds of kernel Classes are also allowed, to permit use of Classes from the Kernel Model
        /// Libraries.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618943843466_158863_236", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1591477641252_179221_958")]
        [Implements(implementation: "IOccurrenceUsage.OccurrenceDefinition")]
        public List<IClass> occurrenceDefinition => this.ComputeOccurrenceDefinition();

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction out or inout.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_365618_43170", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [Implements(implementation: "IType.Output")]
        public List<IFeature> output => this.ComputeOutput();

        /// <summary>
        /// The ownedRelationships of this Element that are Annotations, for which this Element is the
        /// annotatedElement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594152527165_702130_2500", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543094430277_599480_18543")]
        [Implements(implementation: "IElement.OwnedAnnotation")]
        public List<IAnnotation> ownedAnnotation => this.ComputeOwnedAnnotation();

        /// <summary>
        /// A Conjugation owned by this Type for which the Type is the originalType.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1575482646809_280165_440", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1575482490144_309557_300")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "IType.OwnedConjugator")]
        public IConjugation ownedConjugator => this.ComputeOwnedConjugator();

        /// <summary>
        /// The one ownedSubsetting of this Feature, if any, that is a CrossSubsetting}, for which the Feature
        /// is the crossingFeature.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1689616916594_145818_277", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674970_472382_43221")]
        [Implements(implementation: "IFeature.OwnedCrossSubsetting")]
        public ICrossSubsetting ownedCrossSubsetting => this.ComputeOwnedCrossSubsetting();

        /// <summary>
        /// The ownedRelationships of this Type that are Differencings, having this Type as their
        /// typeDifferenced.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661871168454_98082_797", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "IType.OwnedDifferencing")]
        public List<IDifferencing> ownedDifferencing => this.ComputeOwnedDifferencing();

        /// <summary>
        /// The ownedRelationships of this Type that are Disjoinings, for which the Type is the typeDisjoined
        /// Type.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1627447519613_145554_370", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623183194914_502526_616")]
        [Implements(implementation: "IType.OwnedDisjoining")]
        public List<IDisjoining> ownedDisjoining => this.ComputeOwnedDisjoining();

        /// <summary>
        /// The Elements owned by this Element, derived as the ownedRelatedElements of the ownedRelationships of
        /// this Element.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_112608_17278", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.OwnedElement")]
        public List<IElement> ownedElement => this.ComputeOwnedElement();

        /// <summary>
        /// All endFeatures of this Type that are ownedFeatures.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1563834516278_687758_20652", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1562476168385_824569_22106")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        [Implements(implementation: "IType.OwnedEndFeature")]
        public List<IFeature> ownedEndFeature => this.ComputeOwnedEndFeature();

        /// <summary>
        /// The ownedMemberFeatures of the ownedFeatureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_226999_43167", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        [Implements(implementation: "IType.OwnedFeature")]
        public List<IFeature> ownedFeature => this.ComputeOwnedFeature();

        /// <summary>
        /// The ownedRelationships of this Feature that are FeatureChainings, for which the Feature will be the
        /// featureChained.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1622125589880_791465_72", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [Implements(implementation: "IFeature.OwnedFeatureChaining")]
        public List<IFeatureChaining> ownedFeatureChaining => this.ComputeOwnedFeatureChaining();

        /// <summary>
        /// The ownedRelationships of this Feature that are FeatureInvertings and for which the Feature is the
        /// featureInverted.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1653567738671_359235_43", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_b9102da_1623178838861_768019_145")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "IFeature.OwnedFeatureInverting")]
        public List<IFeatureInverting> ownedFeatureInverting => this.ComputeOwnedFeatureInverting();

        /// <summary>
        /// The ownedMemberships of this Type that are FeatureMemberships, for which the Type is the owningType.
        /// Each such FeatureMembership identifies an ownedFeature of the Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674960_868417_43171", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_190614_43269")]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1651076866512_962346_485")]
        [Implements(implementation: "IType.OwnedFeatureMembership")]
        public List<IFeatureMembership> ownedFeatureMembership => this.ComputeOwnedFeatureMembership();

        /// <summary>
        /// The ownedRelationships of this Namespace that are Imports, for which the Namespace is the
        /// importOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674974_746786_43247", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [Implements(implementation: "INamespace.OwnedImport")]
        public List<IImport> ownedImport => this.ComputeOwnedImport();

        /// <summary>
        /// The ownedRelationships of this Type that are Intersectings, have the Type as their typeIntersected.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1623242552144_910757_524", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "IType.OwnedIntersecting")]
        public List<IIntersecting> ownedIntersecting => this.ComputeOwnedIntersecting();

        /// <summary>
        /// The owned members of this Namespace, which are the <cpde>ownedMemberElements of the ownedMemberships
        /// of the Namespace.</cpde>
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_259543_43268", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        [Implements(implementation: "INamespace.OwnedMember")]
        public List<IElement> ownedMember => this.ComputeOwnedMember();

        /// <summary>
        /// The ownedRelationships of this Namespace that are Memberships, for which the Namespace is the
        /// membershipOwningNamespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_190614_43269", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "INamespace.OwnedMembership")]
        public List<IMembership> ownedMembership => this.ComputeOwnedMembership();

        /// <summary>
        /// The ownedSubsettings of this Feature that are Redefinitions, for which the Feature is the
        /// redefiningFeature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674970_161813_43220", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674970_472382_43221")]
        [Implements(implementation: "IFeature.OwnedRedefinition")]
        public List<IRedefinition> ownedRedefinition => this.ComputeOwnedRedefinition();

        /// <summary>
        /// The one ownedSubsetting of this Feature, if any, that is a ReferenceSubsetting, for which the
        /// Feature is the referencingFeature.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1661555161564_247405_255", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674970_472382_43221")]
        [Implements(implementation: "IFeature.OwnedReferenceSubsetting")]
        public IReferenceSubsetting ownedReferenceSubsetting => this.ComputeOwnedReferenceSubsetting();

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092026091_217766_16748", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        [Implements(implementation: "IElement.OwnedRelationship")]
        public List<IRelationship> OwnedRelationship { get; set; } = [];

        /// <summary>
        /// The ownedRelationships of this Type that are Specializations, for which the Type is the specific
        /// Type.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674959_579676_43168", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674984_558067_43292")]
        [Implements(implementation: "IType.OwnedSpecialization")]
        public List<ISpecialization> ownedSpecialization => this.ComputeOwnedSpecialization();

        /// <summary>
        /// The ownedSpecializations of this Feature that are Subsettings, for which the Feature is the
        /// subsettingFeature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674970_472382_43221", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_579676_43168")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674966_718145_43205")]
        [Implements(implementation: "IFeature.OwnedSubsetting")]
        public List<ISubsetting> ownedSubsetting => this.ComputeOwnedSubsetting();

        /// <summary>
        /// The ownedRelationships of this Feature that are TypeFeaturings and for which the Feature is the
        /// featureOfType.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1603905673975_310948_762", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1603904928950_196800_580")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [Implements(implementation: "IFeature.OwnedTypeFeaturing")]
        public List<ITypeFeaturing> ownedTypeFeaturing => this.ComputeOwnedTypeFeaturing();

        /// <summary>
        /// The ownedSpecializations of this Feature that are FeatureTypings, for which the Feature is the
        /// typedFeature.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596597427751_965862_42", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_579676_43168")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543180501615_804591_21100")]
        [Implements(implementation: "IFeature.OwnedTyping")]
        public List<IFeatureTyping> ownedTyping => this.ComputeOwnedTyping();

        /// <summary>
        /// The ownedRelationships of this Type that are Unionings, having the Type as their typeUnioned.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661869978505_968809_460", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092026091_217766_16748")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_80547_43227")]
        [Implements(implementation: "IType.OwnedUnioning")]
        public List<IUnioning> ownedUnioning => this.ComputeOwnedUnioning();

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543092869879_744477_17277", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Owner")]
        public IElement owner => this.ComputeOwner();

        /// <summary>
        /// The Definition that owns this Usage (if any).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565479686638_420576_23237", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_592215_43200")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1565498571495_18876_27787")]
        [Implements(implementation: "IUsage.OwningDefinition")]
        public IDefinition owningDefinition => this.ComputeOwningDefinition();

        /// <summary>
        /// The FeatureMembership that owns this Feature as an ownedMemberFeature, determining its owningType.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674970_68441_43223", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674972_622493_43236")]
        [Implements(implementation: "IFeature.OwningFeatureMembership")]
        public IFeatureMembership owningFeatureMembership => this.ComputeOwningFeatureMembership();

        /// <summary>
        /// The owningRelationship of this Element, if that Relationship is a Membership.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674972_622493_43236", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674973_469277_43243")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_482273_43303")]
        [Implements(implementation: "IElement.OwningMembership")]
        public IOwningMembership owningMembership => this.ComputeOwningMembership();

        /// <summary>
        /// The Namespace that owns this Element, which is the membershipOwningNamespace of the owningMembership
        /// of this Element, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_474739_43306", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674980_717955_43271")]
        [Implements(implementation: "IElement.OwningNamespace")]
        public INamespace owningNamespace => this.ComputeOwningNamespace();

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674986_482273_43303", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_585972_43176")]
        [Implements(implementation: "IElement.OwningRelationship")]
        public IRelationship OwningRelationship { get; set; }

        /// <summary>
        /// The Type that is the owningType of the owningFeatureMembership of this Feature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674965_592215_43200", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674987_297074_43308")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674986_474739_43306")]
        [SubsettedProperty(propertyName: "_19_0_4_12e503d9_1603905619975_304385_743")]
        [Implements(implementation: "IFeature.OwningType")]
        public IType owningType => this.ComputeOwningType();

        /// <summary>
        /// The Usage in which this Usage is nested (if any).
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1565472757327_504924_21260", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674965_592215_43200")]
        [Implements(implementation: "IUsage.OwningUsage")]
        public IUsage owningUsage => this.ComputeOwningUsage();

        /// <summary>
        /// The parameters of this Step, which are defined as its directedFeatures, whose values are passed into
        /// and/or out of a performance of the Step.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189174990_213826_657", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [Implements(implementation: "IStep.Parameter")]
        public List<IFeature> parameter => this.ComputeParameter();

        /// <summary>
        /// The kind of temporal portion (time slice or snapshot) is represented by this OccurrenceUsage. If
        /// portionKind is not null, then the owningType of the OccurrenceUsage must be non-null, and the
        /// OccurrenceUsage represents portions of the featuring instance of the owningType.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1618959362712_182798_7138", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IOccurrenceUsage.PortionKind")]
        public PortionKind? PortionKind { get; set; }

        /// <summary>
        /// The Predicate that types the Expression.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1578025035149_386_969", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1543948477241_299049_20934")]
        [RedefinedByProperty("IConstraintUsage.ConstraintDefinition")]
        [Implements(implementation: "IBooleanExpression.Predicate")]
        IPredicate Kernel.Functions.IBooleanExpression.predicate => ((SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage)this).constraintDefinition;

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
        public string qualifiedName => this.ComputeQualifiedName();

        /// <summary>
        /// An optional modeler-specified identifier for this RequirementUsage (used, e.g., to link it to an
        /// original requirement text in some source document), which is the declaredShortName for the
        /// RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376474630_975784_96", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1594160442439_915308_4153")]
        [Implements(implementation: "IRequirementUsage.ReqId")]
        public string ReqId { get; set; }

        /// <summary>
        /// The owned ConstraintUsages that represent requirements of this RequirementUsage, which are the
        /// ownedConstraints of the RequirementConstraintMemberships of the RequirementUsage with kind =
        /// requirement.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583377448339_252740_390", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_226999_43167")]
        [Implements(implementation: "IRequirementUsage.RequiredConstraint")]
        public List<IConstraintUsage> requiredConstraint => this.ComputeRequiredConstraint();

        /// <summary>
        /// The RequirementDefinition that is the single definition of this RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583000408905_769743_1223", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1578067546711_751168_1745")]
        [RedefinedByProperty("IViewpointUsage.ViewpointDefinition")]
        [Implements(implementation: "IRequirementUsage.RequirementDefinition")]
        IRequirementDefinition Systems.Requirements.IRequirementUsage.requirementDefinition => this.viewpointDefinition;

        /// <summary>
        /// An output parameter of the Expression whose value is the result of the Expression. The result of an
        /// Expression is either inherited from its function or it is related to the Expression via a
        /// ReturnParameterMembership, in which case it redefines the result parameter of its function.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595188071574_902060_363", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674960_365618_43170")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [Implements(implementation: "IExpression.Result")]
        public IFeature result => this.ComputeResult();

        /// <summary>
        /// The short name to be used for this Element during name resolution within its owningNamespace. This
        /// is derived using the effectiveShortName() operation. By default, it is the same as the
        /// declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even
        /// when the declaredName is null.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1673496405504_544235_24", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ShortName")]
        public string shortName => this.ComputeShortName();

        /// <summary>
        /// The parameters of this RequirementUsage that represent stakeholders for the requirement.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1624032823963_328647_40107", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        [Implements(implementation: "IRequirementUsage.StakeholderParameter")]
        public List<IPartUsage> stakeholderParameter => this.ComputeStakeholderParameter();

        /// <summary>
        /// The parameter of this RequirementUsage that represents its subject.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189397261_941898_844", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1595189174990_213826_657")]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1591217543254_26688_475")]
        [Implements(implementation: "IRequirementUsage.SubjectParameter")]
        public IUsage subjectParameter => this.ComputeSubjectParameter();

        /// <summary>
        /// An optional textual statement of the requirement represented by this RequirementUsage, derived from
        /// the bodies of the documentation of the RequirementUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1583376480942_745679_99", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IRequirementUsage.Text")]
        public List<string> text => this.ComputeText();

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594154758493_640290_3388", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_19_0_2_12e503d9_1594145755059_76214_87")]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_112608_17278")]
        [Implements(implementation: "IElement.TextualRepresentation")]
        public List<ITextualRepresentation> textualRepresentation => this.ComputeTextualRepresentation();

        /// <summary>
        /// Types that restrict the values of this Feature, such that the values must be instances of all the
        /// types. The types of a Feature are derived from its typings and the types of its subsettings. If the
        /// Feature is chained, then the types of the last Feature in the chain are also types of the chained
        /// Feature.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674969_376003_43216", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedByProperty("IUsage.Definition")]
        [Implements(implementation: "IFeature.Type")]
        List<IType> Core.Features.IFeature.type => [.. ((SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage)this).definition];

        /// <summary>
        /// The interpretations of a Type with unioningTypes are asserted to be the same as those of all the
        /// unioningTypes together, which are the Types derived from the unioningType of the ownedUnionings of
        /// this Type. For example, a Classifier for people might be the union of Classifiers for all the sexes.
        /// Similarly, a feature for people&#39;s children might be the union of features dividing them in the
        /// same ways as people in general.
        /// </summary>
        [Property(xmiId: "_19_0_4_b9102da_1661974896766_783268_1231", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.UnioningType")]
        public List<IType> unioningType => this.ComputeUnioningType();

        /// <summary>
        /// The Usages that are features of this Usage (not necessarily owned).
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1591217543254_26688_475", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674959_326391_43166")]
        [Implements(implementation: "IUsage.Usage")]
        public List<IUsage> usage => this.ComputeUsage();

        /// <summary>
        /// The Usages which represent the variants of this Usage as a variation point Usage, if isVariation =
        /// true. If isVariation = false, then there must be no variants.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590979649160_380466_999", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_259543_43268")]
        [Implements(implementation: "IUsage.Variant")]
        public List<IUsage> variant => this.ComputeVariant();

        /// <summary>
        /// The ownedMemberships of this Usage that are VariantMemberships. If isVariation = true, then this
        /// must be all memberships of the Usage. If isVariation = false, then variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1590979136735_982171_914", aggregation: AggregationKind.Composite, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_190614_43269")]
        [Implements(implementation: "IUsage.VariantMembership")]
        public List<IVariantMembership> variantMembership => this.ComputeVariantMembership();

        /// <summary>
        /// The ViewpointDefinition that is the definition of this ViewpointUsage.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596649684798_569222_3524", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1583000408905_769743_1223")]
        [Implements(implementation: "IViewpointUsage.ViewpointDefinition")]
        public IViewpointDefinition viewpointDefinition => this.ComputeViewpointDefinition();

        /// <summary>
        /// The PartUsages that identify the stakeholders with concerns framed by this ViewpointUsage, which are
        /// the owned and inherited stakeholderParameters of the framedConcerns of this ViewpointUsage.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1617117200628_940407_3323", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IViewpointUsage.ViewpointStakeholder")]
        public List<IPartUsage> viewpointStakeholder => this.ComputeViewpointStakeholder();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
