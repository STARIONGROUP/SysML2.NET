// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralExpression.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Collections;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A LiteralExpression is an Expression that provides a basic DataValue as a result.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651688_624289_42165", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class LiteralExpression : ILiteralExpression, IContainedElement
    {
        /// <summary>
        /// Initialize a new instance of <see cref="LiteralExpression" />
        /// </summary>
        public LiteralExpression()
        {
            ((IContainedElement)this).OwnedRelationship = new ContainerList<IElement, IRelationship>(this,
            (relationship, element) => ((IContainedRelationship)relationship).OwningRelatedElement = element,
            relationship => relationship.OwningRelatedElement);

        }

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
        /// The Behaviors that type this Step.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1536346315176_954314_17388", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674969_376003_43216")]
        [RedefinedByProperty("IExpression.Function")]
        [Implements(implementation: "IStep.Behavior")]
        List<IBehavior> Kernel.Behaviors.IStep.behavior => this.function != null ? [this.function] : [];

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
        /// The Function that types this Expression.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543948477241_299049_20934", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_b9102da_1536346315176_954314_17388")]
        [Implements(implementation: "IExpression.Function")]
        public IFunction function => this.ComputeFunction();

        /// <summary>
        /// The Memberships in this Namespace that result from the ownedImports of this Namespace.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674979_207869_43270", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674962_198288_43183")]
        [Implements(implementation: "INamespace.ImportedMembership")]
        public List<IMembership> importedMembership => this.ComputeImportedMembership();

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
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.                            (A Type gives conditions that must be met by whatever it
        /// classifies, but when isSufficient is false, things may meet those conditions but still not be
        /// classified by the Type. For example, a Type Car that is not sufficient could require everything it
        /// classifies to have four wheels, but not all four wheeled things would classify as cars. However, if
        /// the Type Car were sufficient, it would classify all four-wheeled things.)
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
        [Implements(implementation: "IFeature.IsVariable")]
        public bool IsVariable { get; set; }

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
        public IReadOnlyList<IRelationship> OwnedRelationship => ((IContainedElement)this).OwnedRelationship;

        /// <summary>Backing field for IElement.OwnedRelationship</summary>
        ContainerList<IElement, IRelationship> IContainedElement.OwnedRelationship { get; set; }

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
        public IRelationship OwningRelationship => ((IContainedElement)this).OwningRelationship;

        /// <summary>Backing field for IElement.OwningRelationship</summary>
        IRelationship IContainedElement.OwningRelationship { get; set; }

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
        /// The parameters of this Step, which are defined as its directedFeatures, whose values are passed into
        /// and/or out of a performance of the Step.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189174990_213826_657", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        [Implements(implementation: "IStep.Parameter")]
        public List<IFeature> parameter => this.ComputeParameter();

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
        [Implements(implementation: "IFeature.Type")]
        public List<IType> type => this.ComputeType();

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
        /// Return this Feature and all the Features that are directly or indirectly Redefined by this Feature.
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        public List<IFeature> AllRedefinedFeatures() => this.ComputeAllRedefinedFeaturesOperation();

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
        public List<IFeature> AllRedefinedFeaturesOf(IMembership membership) => this.ComputeAllRedefinedFeaturesOfOperation(membership);

        /// <summary>
        /// Return this Type and all Types that are directly or transitively supertypes of this Type (as
        /// determined by the supertypes operation with excludeImplied = false).
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        public List<IType> AllSupertypes() => this.ComputeAllSupertypesOperation();

        /// <summary>
        /// If isCartesianProduct is true, then return the list of Types whose Cartesian product can be
        /// represented by this Feature. (If isCartesianProduct is not true, the operation will still return a
        /// valid value, it will just not represent anything useful.)
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        public List<IType> AsCartesianProduct() => this.ComputeAsCartesianProductOperation();

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
        public bool CanAccess(IFeature feature) => this.ComputeCanAccessOperation(feature);

        /// <summary>
        /// Model-level evaluate this Expression with the given target. If the result is a LiteralBoolean,
        /// return its value. Otherwise return false.
        /// </summary>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        public bool CheckCondition(IElement target) => this.ComputeCheckConditionOperation(target);

        /// <summary>
        /// Return the directionOf this Feature relative to the given type.
        /// </summary>
        /// <param name="type">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        public FeatureDirectionKind? DirectionFor(IType type) => this.ComputeDirectionForOperation(type);

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
        public FeatureDirectionKind? DirectionOf(IFeature feature) => this.ComputeDirectionOfOperation(feature);

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
        public FeatureDirectionKind? DirectionOfExcluding(IFeature feature, List<IType> excluded) => this.ComputeDirectionOfExcludingOperation(feature, excluded);

        /// <summary>
        /// If a Feature has no declaredName or declaredShortName                            , then its
        /// effective name is given by the effective name of the Feature returned by the namingFeature()
        /// operation, if any.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string EffectiveName() => this.ComputeRedefinedEffectiveNameOperation();

        /// <summary>
        /// Return an effective name for this Element. By default this is the same as its declaredName.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        string IElement.EffectiveName() => this.EffectiveName();

        /// <summary>
        /// If a Feature has no declaredShortName or declaredName, then its effective shortName is given by the
        /// effective shortName of the Feature returned by the namingFeature() operation, if any.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string EffectiveShortName() => this.ComputeRedefinedEffectiveShortNameOperation();

        /// <summary>
        /// Return an effective shortName for this Element. By default this is the same as its
        /// declaredShortName.
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        string IElement.EffectiveShortName() => this.EffectiveShortName();

        /// <summary>
        /// Return name, if that is not null, otherwise the shortName, if that is not null, otherwise null. If
        /// the returned value is non-null, it is returned as-is if it has the form of a basic name, or,
        /// otherwise, represented as a restricted name according to the lexical structure of the KerML textual
        /// notation (i.e., surrounded by single quote characters and with special characters escaped).
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string EscapedName() => this.ComputeEscapedNameOperation();

        /// <summary>
        /// The model-level value of a LiteralExpression is itself.
        /// </summary>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        public List<IElement> Evaluate(IElement target) => this.ComputeRedefinedEvaluateOperation(target);

        /// <summary>
        /// If this Expression isModelLevelEvaluable, then evaluate it using the target as the context Element
        /// for resolving Feature names and testing classification. The result is a collection of Elements,
        /// which, for a fully evaluable Expression, will be a LiteralExpression or a Feature that is not an
        /// Expression.
        /// </summary>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        List<IElement> IExpression.Evaluate(IElement target) => this.Evaluate(target);

        /// <summary>
        /// Derive the imported Memberships of this Namespace as the importedMembership of all ownedImports,
        /// excluding those Imports whose importOwningNamespace is in the excluded set, and excluding
        /// Memberships that have distinguisibility collisions with each other or with any ownedMembership.
        /// </summary>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        public List<IMembership> ImportedMemberships(List<INamespace> excluded) => this.ComputeImportedMembershipsOperation(excluded);

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
        public List<IMembership> InheritableMemberships(List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied) => this.ComputeInheritableMembershipsOperation(excludedNamespaces, excludedTypes, excludeImplied);

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
        public List<IMembership> InheritedMemberships(List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied) => this.ComputeInheritedMembershipsOperation(excludedNamespaces, excludedTypes, excludeImplied);

        /// <summary>
        /// Check whether this Feature can be used to represent a Cartesian product of Types.
        /// </summary>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        public bool IsCartesianProduct() => this.ComputeIsCartesianProductOperation();

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
        public bool IsCompatibleWith(IType otherType) => this.ComputeRedefinedIsCompatibleWithOperation(otherType);

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
        bool IType.IsCompatibleWith(IType otherType) => this.IsCompatibleWith(otherType);

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
        public bool IsFeaturedWithin(IType type) => this.ComputeIsFeaturedWithinOperation(type);

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
        public bool IsFeaturingType(IType type) => this.ComputeIsFeaturingTypeOperation(type);

        /// <summary>
        /// Return whether this Feature is an owned cross Feature of an end Feature.
        /// </summary>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        public bool IsOwnedCrossFeature() => this.ComputeIsOwnedCrossFeatureOperation();

        /// <summary>
        /// By default, return the library Namespace of the owningRelationship of this Element, if it has one.
        /// </summary>
        /// <returns>
        /// The expected <see cref="INamespace" />
        /// </returns>
        public INamespace LibraryNamespace() => this.ComputeLibraryNamespaceOperation();

        /// <summary>
        /// If visibility is not null, return the Memberships of this Namespace with the given visibility,
        /// including ownedMemberships with the given visibility and Memberships imported with the given
        /// visibility. If visibility is null, return all ownedMemberships and imported Memberships regardless
        /// of visibility. When computing imported Memberships, ignore this Namespace and any Namespaces in the
        /// given excluded set.
        /// </summary>
        /// <param name="visibility">
        /// No documentation provided
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IMembership" />
        /// </returns>
        public List<IMembership> MembershipsOfVisibility(VisibilityKind? visibility, List<INamespace> excluded) => this.ComputeMembershipsOfVisibilityOperation(visibility, excluded);

        /// <summary>
        /// A LiteralExpression is always model-level evaluable.
        /// </summary>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        public bool ModelLevelEvaluable(List<IFeature> visited) => this.ComputeRedefinedModelLevelEvaluableOperation(visited);

        /// <summary>
        /// Return whether this Expression is model-level evaluable. The visited parameter is used to track
        /// possible circular Feature references made from FeatureReferenceExpressions (see the redefinition of
        /// this operation for FeatureReferenceExpression). Such circular references are not allowed in
        /// model-level evaluable expressions.                            An Expression that is not otherwise
        /// specialized is model-level evaluable if it has no (non-implied) ownedSpecializations and all its
        /// ownedFeatures are either in parameters, the result parameter or a result Expression owned via a
        /// ResultExpressionMembership. The parameters  must not have any ownedFeatures or a FeatureValue, and
        /// the result Expression must be model-level evaluable.
        /// </summary>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        bool IExpression.ModelLevelEvaluable(List<IFeature> visited) => this.ModelLevelEvaluable(visited);

        /// <summary>
        /// Return the owned or inherited Multiplicities for this Type<./code>.
        /// </summary>
        /// <returns>
        /// The expected collection of <see cref="IMultiplicity" />
        /// </returns>
        public List<IMultiplicity> Multiplicities() => this.ComputeMultiplicitiesOperation();

        /// <summary>
        /// Return the names of the given element as it is known in this Namespace.
        /// </summary>
        /// <param name="element">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="string" />
        /// </returns>
        public List<string> NamesOf(IElement element) => this.ComputeNamesOfOperation(element);

        /// <summary>
        /// By default, the naming Feature of a Feature is given by its first redefinedFeature of its first
        /// ownedRedefinition, if any.
        /// </summary>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        public IFeature NamingFeature() => this.ComputeNamingFeatureOperation();

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
        public List<IMembership> NonPrivateMemberships(List<INamespace> excludedNamespaces, List<IType> excludedTypes, bool excludeImplied) => this.ComputeNonPrivateMembershipsOperation(excludedNamespaces, excludedTypes, excludeImplied);

        /// <summary>
        /// If this Feature is an end Feature of its owningType, then return the first ownedMember of the
        /// Feature that is a Feature, but not a Multiplicity or a MetadataFeature, and whose owningMembership
        /// is not a FeatureMembership. If this exists, it is the crossFeature of the end Feature.
        /// </summary>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        public IFeature OwnedCrossFeature() => this.ComputeOwnedCrossFeatureOperation();

        /// <summary>
        /// Return a unique description of the location of this Element in the containment structure rooted in a
        /// root Namespace. If the Element has a non-null qualifiedName, then return that. Otherwise, if it has
        /// an owningRelationship, then return the string constructed by appending to the path of it's
        /// owningRelationship the character / followed by the string representation of its position in the list
        /// of ownedRelatedElements of the owningRelationship (indexed starting at 1). Otherwise, return the
        /// empty string.                            (Note that this operation is overridden for Relationships
        /// to use owningRelatedElement when appropriate.)
        /// </summary>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string Path() => this.ComputePathOperation();

        /// <summary>
        /// Return a string with valid KerML syntax representing the qualification part of a given
        /// qualifiedName, that is, a qualified name with all the segment names of the given name except the
        /// last. If the given qualifiedName has only one segment, then return null.
        /// </summary>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string QualificationOf(string qualifiedName) => this.ComputeQualificationOfOperation(qualifiedName);

        /// <summary>
        /// Check whether this Feature directly redefines the given redefinedFeature.
        /// </summary>
        /// <param name="redefinedFeature">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        public bool Redefines(IFeature redefinedFeature) => this.ComputeRedefinesOperation(redefinedFeature);

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
        public bool RedefinesFromLibrary(string libraryFeatureName) => this.ComputeRedefinesFromLibraryOperation(libraryFeatureName);

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
        public List<IMembership> RemoveRedefinedFeatures(List<IMembership> memberships) => this.ComputeRemoveRedefinedFeaturesOperation(memberships);

        /// <summary>
        /// Resolve the given qualified name to the named Membership (if any), starting with this Namespace as
        /// the local scope. The qualified name string must conform to the concrete syntax of the KerML textual
        /// notation. According to the KerML name resolution rules every qualified name will resolve to either a
        /// single Membership, or to none.
        /// </summary>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        public IMembership Resolve(string qualifiedName) => this.ComputeResolveOperation(qualifiedName);

        /// <summary>
        /// Resolve the given qualified name to the named Membership (if any) in the effective global Namespace
        /// that is the outermost naming scope. The qualified name string must conform to the concrete syntax of
        /// the KerML textual notation.
        /// </summary>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        public IMembership ResolveGlobal(string qualifiedName) => this.ComputeResolveGlobalOperation(qualifiedName);

        /// <summary>
        /// Resolve a simple name starting with this Namespace as the local scope, and continuing with
        /// containing outer scopes as necessary. However, if this Namespace is a root Namespace, then the
        /// resolution is done directly in global scope.
        /// </summary>
        /// <param name="name">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        public IMembership ResolveLocal(string name) => this.ComputeResolveLocalOperation(name);

        /// <summary>
        /// Resolve a simple name from the visible Memberships of this Namespace.
        /// </summary>
        /// <param name="name">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IMembership" />
        /// </returns>
        public IMembership ResolveVisible(string name) => this.ComputeResolveVisibleOperation(name);

        /// <summary>
        /// Check whether this Type is a direct or indirect specialization of the given supertype.
        /// </summary>
        /// <param name="supertype">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        public bool Specializes(IType supertype) => this.ComputeSpecializesOperation(supertype);

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
        public bool SpecializesFromLibrary(string libraryTypeName) => this.ComputeSpecializesFromLibraryOperation(libraryTypeName);

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
        public bool SubsetsChain(IFeature first, IFeature second) => this.ComputeSubsetsChainOperation(first, second);

        /// <summary>
        /// </summary>
        /// <param name="excludeImplied">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IType" />
        /// </returns>
        public List<IType> Supertypes(bool excludeImplied) => this.ComputeRedefinedSupertypesOperation(excludeImplied);

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
        List<IType> IType.Supertypes(bool excludeImplied) => this.Supertypes(excludeImplied);

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
        public List<IFeature> TypingFeatures() => this.ComputeTypingFeaturesOperation();

        /// <summary>
        /// Return the simple name that is the last segment name of the given qualifiedName. If this segment
        /// name has the form of a KerML unrestricted name, then "unescape" it by removing the surrounding
        /// single quotes and replacing all escape sequences with the specified character.
        /// </summary>
        /// <param name="qualifiedName">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        public string UnqualifiedNameOf(string qualifiedName) => this.ComputeUnqualifiedNameOfOperation(qualifiedName);

        /// <summary>
        /// Returns this visibility of mem relative to this Namespace. If mem is an importedMembership, this is
        /// the visibility of its Import. Otherwise it is the visibility of the Membership itself.
        /// </summary>
        /// <param name="mem">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="VisibilityKind" />
        /// </returns>
        public VisibilityKind VisibilityOf(IMembership mem) => this.ComputeVisibilityOfOperation(mem);

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
        public List<IMembership> VisibleMemberships(List<INamespace> excluded, bool isRecursive, bool includeAll) => this.ComputeRedefinedVisibleMembershipsOperation(excluded, isRecursive, includeAll);

        /// <summary>
        /// If includeAll = true, then return all the Memberships of this Namespace. Otherwise, return only the
        /// publicly visible Memberships of this Namespace, including ownedMemberships that have a visibility of
        /// public and Memberships imported with a visibility of public. If isRecursive = true, also recursively
        /// include all visible Memberships of any public owned Namespaces, or, if IncludeAll = true, all
        /// Memberships of all owned Namespaces. When computing imported Memberships, ignore this Namespace and
        /// any Namespaces in the given excluded set.
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
        List<IMembership> INamespace.VisibleMemberships(List<INamespace> excluded, bool isRecursive, bool includeAll) => this.VisibleMemberships(excluded, isRecursive, includeAll);
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
