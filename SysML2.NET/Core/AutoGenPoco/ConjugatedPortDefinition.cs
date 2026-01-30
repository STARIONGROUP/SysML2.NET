// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
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
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConjugatedPortDefinition is a PortDefinition that is a PortDefinition of its original
    /// PortDefinition. That is, a ConjugatedPortDefinition inherits all the features of the original
    /// PortDefinition, but input flows of the original PortDefinition become outputs on the
    /// ConjugatedPortDefinition and output flows of the original PortDefinition become inputs on the
    /// ConjugatedPortDefinition. Every PortDefinition (that is not itself a ConjugatedPortDefinition) has
    /// exactly one corresponding ConjugatedPortDefinition, whose effective name is the name of the
    /// originalPortDefinition, with the character ~ prepended.
    /// </summary>
    [Class(xmiId: "Systems-Ports-ConjugatedPortDefinition", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class ConjugatedPortDefinition : IConjugatedPortDefinition
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
        [Property(xmiId: "Root-Elements-Element-aliasIds", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.AliasIds")]
        public List<string> AliasIds { get; set; } = [];

        /// <summary>
        /// The <> that is conjugate to this PortDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Ports-PortDefinition-conjugatedPortDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        [Implements(implementation: "IPortDefinition.ConjugatedPortDefinition")]
        public IConjugatedPortDefinition conjugatedPortDefinition => this.ComputeConjugatedPortDefinition();

        /// <summary>
        /// The declared name of this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-declaredName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.DeclaredName")]
        public string DeclaredName { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-declaredShortName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
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
        [Property(xmiId: "Core-Types-Type-differencingType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.DifferencingType")]
        public List<IType> differencingType => this.ComputeDifferencingType();

        /// <summary>
        /// The features of this Type that have a non-null direction.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-directedFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        [Implements(implementation: "IType.DirectedFeature")]
        public List<IFeature> directedFeature => this.ComputeDirectedFeature();

        /// <summary>
        /// The usages of this Definition that are directedFeatures.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-directedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-usage")]
        [Implements(implementation: "IDefinition.DirectedUsage")]
        public List<IUsage> directedUsage => this.ComputeDirectedUsage();

        /// <summary>
        /// The Documentation owned by this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-documentation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotatingElement-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedElement")]
        [Implements(implementation: "IElement.Documentation")]
        public List<IDocumentation> documentation => this.ComputeDocumentation();

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-elementId", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ElementId")]
        public string ElementId { get; set; }

        /// <summary>
        /// All features of this Type with isEnd = true.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-endFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        [Implements(implementation: "IType.EndFeature")]
        public List<IFeature> endFeature => this.ComputeEndFeature();

        /// <summary>
        /// The ownedMemberFeatures of the featureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-feature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-member")]
        [Implements(implementation: "IType.Feature")]
        public List<IFeature> feature => this.ComputeFeature();

        /// <summary>
        /// The FeatureMemberships for features of this Type, which include all ownedFeatureMemberships and
        /// those inheritedMemberships that are FeatureMemberships (but does not include any
        /// importedMemberships).
        /// </summary>
        [Property(xmiId: "Core-Types-Type-featureMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.FeatureMembership")]
        public List<IFeatureMembership> featureMembership => this.ComputeFeatureMembership();

        /// <summary>
        /// The Memberships in this Namespace that result from the ownedImports of this Namespace.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Namespace-importedMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-membership")]
        [Implements(implementation: "INamespace.ImportedMembership")]
        public List<IMembership> importedMembership => this.ComputeImportedMembership();

        /// <summary>
        /// All the memberFeatures of the inheritedMemberships of this Type that are FeatureMemberships.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-inheritedFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        [Implements(implementation: "IType.InheritedFeature")]
        public List<IFeature> inheritedFeature => this.ComputeInheritedFeature();

        /// <summary>
        /// All Memberships inherited by this Type via Specialization or Conjugation. These are included in the
        /// derived union for the memberships of the Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-inheritedMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-membership")]
        [Implements(implementation: "IType.InheritedMembership")]
        public List<IMembership> inheritedMembership => this.ComputeInheritedMembership();

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction in or inout.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-input", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
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
        [Property(xmiId: "Core-Types-Type-intersectingType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.IntersectingType")]
        public List<IType> intersectingType => this.ComputeIntersectingType();

        /// <summary>
        /// Indicates whether instances of this Type must also be instances of at least one of its specialized
        /// Types.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-isAbstract", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IType.IsAbstract")]
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Indicates whether this Type has an ownedConjugator.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-isConjugated", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.IsConjugated")]
        public bool isConjugated => this.ComputeIsConjugated();

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-isImpliedIncluded", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IElement.IsImpliedIncluded")]
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// Whether this OccurrenceDefinition is constrained to represent at most one thing.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceDefinition-isIndividual", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IOccurrenceDefinition.IsIndividual")]
        public bool IsIndividual { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-isLibraryElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.IsLibraryElement")]
        public bool isLibraryElement => this.ComputeIsLibraryElement();

        /// <summary>
        /// Whether all things that meet the classification conditions of this Type must be classified by the
        /// Type.  (A Type gives conditions that must be met by whatever it classifies, but when
        /// isSufficient is false, things may meet those conditions but still not be classified by the Type. For
        /// example, a Type Car that is not sufficient could require everything it classifies to have four
        /// wheels, but not all four wheeled things would classify as cars. However, if the Type Car were
        /// sufficient, it would classify all four-wheeled things.)
        /// </summary>
        [Property(xmiId: "Core-Types-Type-isSufficient", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IType.IsSufficient")]
        public bool IsSufficient { get; set; }

        /// <summary>
        /// Whether this Definition is for a variation point or not. If true, then all the memberships of the
        /// Definition must be VariantMemberships.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-isVariation", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IDefinition.IsVariation")]
        public bool IsVariation { get; set; }

        /// <summary>
        /// The set of all member Elements of this Namespace, which are the memberElements of all memberships of
        /// the Namespace.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Namespace-member", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "INamespace.Member")]
        public List<IElement> member => this.ComputeMember();

        /// <summary>
        /// All Memberships in this Namespace, including (at least) the union of ownedMemberships and
        /// importedMemberships.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Namespace-membership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: true, isUnique: true, defaultValue: null)]
        [Implements(implementation: "INamespace.Membership")]
        public List<IMembership> membership => this.ComputeMembership();

        /// <summary>
        /// An ownedMember of this Type that is a Multiplicity, which constraints the cardinality of the Type.
        /// If there is no such ownedMember, then the cardinality of this Type is constrained by all the
        /// Multiplicity constraints applicable to any direct supertypes.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-multiplicity", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        [Implements(implementation: "IType.Multiplicity")]
        public IMultiplicity multiplicity => this.ComputeMultiplicity();

        /// <summary>
        /// The name to be used for this Element during name resolution within its owningNamespace. This is
        /// derived using the effectiveName() operation. By default, it is the same as the declaredName, but
        /// this is overridden for certain kinds of Elements to compute a name even when the declaredName is
        /// null.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-name", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Name")]
        public string name => this.ComputeName();

        /// <summary>
        /// The original PortDefinition for this ConjugatedPortDefinition, which is the owningNamespace of the
        /// ConjugatedPortDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Ports-ConjugatedPortDefinition-originalPortDefinition", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Element-owningNamespace")]
        [Implements(implementation: "IConjugatedPortDefinition.OriginalPortDefinition")]
        public IPortDefinition originalPortDefinition => this.ComputeOriginalPortDefinition();

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction out or inout.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-output", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        [Implements(implementation: "IType.Output")]
        public List<IFeature> output => this.ComputeOutput();

        /// <summary>
        /// The ActionUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedOccurrence")]
        [Implements(implementation: "IDefinition.OwnedAction")]
        public List<IActionUsage> ownedAction => this.ComputeOwnedAction();

        /// <summary>
        /// The AllocationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedAllocation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedConnection")]
        [Implements(implementation: "IDefinition.OwnedAllocation")]
        public List<IAllocationUsage> ownedAllocation => this.ComputeOwnedAllocation();

        /// <summary>
        /// The AnalysisCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedAnalysisCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedCase")]
        [Implements(implementation: "IDefinition.OwnedAnalysisCase")]
        public List<IAnalysisCaseUsage> ownedAnalysisCase => this.ComputeOwnedAnalysisCase();

        /// <summary>
        /// The ownedRelationships of this Element that are Annotations, for which this Element is the
        /// annotatedElement.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedAnnotation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotation-annotation")]
        [Implements(implementation: "IElement.OwnedAnnotation")]
        public List<IAnnotation> ownedAnnotation => this.ComputeOwnedAnnotation();

        /// <summary>
        /// The AttributeUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedAttribute", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        [Implements(implementation: "IDefinition.OwnedAttribute")]
        public List<IAttributeUsage> ownedAttribute => this.ComputeOwnedAttribute();

        /// <summary>
        /// The CalculationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedCalculation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedAction")]
        [Implements(implementation: "IDefinition.OwnedCalculation")]
        public List<ICalculationUsage> ownedCalculation => this.ComputeOwnedCalculation();

        /// <summary>
        /// The code>CaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedCalculation")]
        [Implements(implementation: "IDefinition.OwnedCase")]
        public List<ICaseUsage> ownedCase => this.ComputeOwnedCase();

        /// <summary>
        /// The ConcernUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedConcern", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedRequirement")]
        [Implements(implementation: "IDefinition.OwnedConcern")]
        public List<IConcernUsage> ownedConcern => this.ComputeOwnedConcern();

        /// <summary>
        /// A Conjugation owned by this Type for which the Type is the originalType.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedConjugator", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-A_conjugatedType_conjugator-conjugator")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [RedefinedByProperty("IConjugatedPortDefinition.OwnedPortConjugator")]
        [Implements(implementation: "IType.OwnedConjugator")]
        IConjugation Core.Types.IType.ownedConjugator => this.ownedPortConjugator;

        /// <summary>
        /// The ConnectorAsUsages that are ownedUsages of this Definition. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowUsages because these are ConnectorAsUsages
        /// even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedConnection", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        [Implements(implementation: "IDefinition.OwnedConnection")]
        public List<IConnectorAsUsage> ownedConnection => this.ComputeOwnedConnection();

        /// <summary>
        /// The ConstraintUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedOccurrence")]
        [Implements(implementation: "IDefinition.OwnedConstraint")]
        public List<IConstraintUsage> ownedConstraint => this.ComputeOwnedConstraint();

        /// <summary>
        /// The ownedRelationships of this Type that are Differencings, having this Type as their
        /// typeDifferenced.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedDifferencing", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [Implements(implementation: "IType.OwnedDifferencing")]
        public List<IDifferencing> ownedDifferencing => this.ComputeOwnedDifferencing();

        /// <summary>
        /// The ownedRelationships of this Type that are Disjoinings, for which the Type is the typeDisjoined
        /// Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedDisjoining", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Core-Types-A_disjoiningTypeDisjoining_typeDisjoined-disjoiningTypeDisjoining")]
        [Implements(implementation: "IType.OwnedDisjoining")]
        public List<IDisjoining> ownedDisjoining => this.ComputeOwnedDisjoining();

        /// <summary>
        /// The Elements owned by this Element, derived as the ownedRelatedElements of the ownedRelationships of
        /// this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedElement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.OwnedElement")]
        public List<IElement> ownedElement => this.ComputeOwnedElement();

        /// <summary>
        /// All endFeatures of this Type that are ownedFeatures.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedEndFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-endFeature")]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        [Implements(implementation: "IType.OwnedEndFeature")]
        public List<IFeature> ownedEndFeature => this.ComputeOwnedEndFeature();

        /// <summary>
        /// The EnumerationUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedEnumeration", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedAttribute")]
        [Implements(implementation: "IDefinition.OwnedEnumeration")]
        public List<IEnumerationUsage> ownedEnumeration => this.ComputeOwnedEnumeration();

        /// <summary>
        /// The ownedMemberFeatures of the ownedFeatureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        [Implements(implementation: "IType.OwnedFeature")]
        public List<IFeature> ownedFeature => this.ComputeOwnedFeature();

        /// <summary>
        /// The ownedMemberships of this Type that are FeatureMemberships, for which the Type is the owningType.
        /// Each such FeatureMembership identifies an ownedFeature of the Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedFeatureMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMembership")]
        [SubsettedProperty(propertyName: "Core-Types-Type-featureMembership")]
        [Implements(implementation: "IType.OwnedFeatureMembership")]
        public List<IFeatureMembership> ownedFeatureMembership => this.ComputeOwnedFeatureMembership();

        /// <summary>
        /// The FlowUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedFlow", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedConnection")]
        [Implements(implementation: "IDefinition.OwnedFlow")]
        public List<IFlowUsage> ownedFlow => this.ComputeOwnedFlow();

        /// <summary>
        /// The ownedRelationships of this Namespace that are Imports, for which the Namespace is the
        /// importOwningNamespace.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Namespace-ownedImport", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [Implements(implementation: "INamespace.OwnedImport")]
        public List<IImport> ownedImport => this.ComputeOwnedImport();

        /// <summary>
        /// The InterfaceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedInterface", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedConnection")]
        [Implements(implementation: "IDefinition.OwnedInterface")]
        public List<IInterfaceUsage> ownedInterface => this.ComputeOwnedInterface();

        /// <summary>
        /// The ownedRelationships of this Type that are Intersectings, have the Type as their typeIntersected.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedIntersecting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [Implements(implementation: "IType.OwnedIntersecting")]
        public List<IIntersecting> ownedIntersecting => this.ComputeOwnedIntersecting();

        /// <summary>
        /// The ItemUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedItem", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedOccurrence")]
        [Implements(implementation: "IDefinition.OwnedItem")]
        public List<IItemUsage> ownedItem => this.ComputeOwnedItem();

        /// <summary>
        /// The owned members of this Namespace, which are the <cpde>ownedMemberElements of the ownedMemberships
        /// of the Namespace.</cpde>
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Namespace-ownedMember", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-member")]
        [Implements(implementation: "INamespace.OwnedMember")]
        public List<IElement> ownedMember => this.ComputeOwnedMember();

        /// <summary>
        /// The ownedRelationships of this Namespace that are Memberships, for which the Namespace is the
        /// membershipOwningNamespace.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Namespace-ownedMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-membership")]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [Implements(implementation: "INamespace.OwnedMembership")]
        public List<IMembership> ownedMembership => this.ComputeOwnedMembership();

        /// <summary>
        /// The MetadataUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedMetadata", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedItem")]
        [Implements(implementation: "IDefinition.OwnedMetadata")]
        public List<IMetadataUsage> ownedMetadata => this.ComputeOwnedMetadata();

        /// <summary>
        /// The OccurrenceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedOccurrence", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        [Implements(implementation: "IDefinition.OwnedOccurrence")]
        public List<IOccurrenceUsage> ownedOccurrence => this.ComputeOwnedOccurrence();

        /// <summary>
        /// The PartUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedPart", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedItem")]
        [Implements(implementation: "IDefinition.OwnedPart")]
        public List<IPartUsage> ownedPart => this.ComputeOwnedPart();

        /// <summary>
        /// The PortUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedPort", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        [Implements(implementation: "IDefinition.OwnedPort")]
        public List<IPortUsage> ownedPort => this.ComputeOwnedPort();

        /// <summary>
        /// The PortConjugation that is the ownedConjugator of this ConjugatedPortDefinition, linking it to its
        /// originalPortDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Ports-ConjugatedPortDefinition-ownedPortConjugator", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Type-ownedConjugator")]
        [Implements(implementation: "IConjugatedPortDefinition.OwnedPortConjugator")]
        public IPortConjugation ownedPortConjugator => this.ComputeOwnedPortConjugator();

        /// <summary>
        /// The ReferenceUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedReference", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        [Implements(implementation: "IDefinition.OwnedReference")]
        public List<IReferenceUsage> ownedReference => this.ComputeOwnedReference();

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        [Implements(implementation: "IElement.OwnedRelationship")]
        public List<IRelationship> OwnedRelationship { get; set; } = [];

        /// <summary>
        /// The RenderingUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedRendering", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedPart")]
        [Implements(implementation: "IDefinition.OwnedRendering")]
        public List<IRenderingUsage> ownedRendering => this.ComputeOwnedRendering();

        /// <summary>
        /// The RequirementUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedRequirement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedConstraint")]
        [Implements(implementation: "IDefinition.OwnedRequirement")]
        public List<IRequirementUsage> ownedRequirement => this.ComputeOwnedRequirement();

        /// <summary>
        /// The ownedRelationships of this Type that are Specializations, for which the Type is the specific
        /// Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedSpecialization", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Core-Types-A_specific_specialization-specialization")]
        [Implements(implementation: "IType.OwnedSpecialization")]
        public List<ISpecialization> ownedSpecialization => this.ComputeOwnedSpecialization();

        /// <summary>
        /// The StateUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedState", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedAction")]
        [Implements(implementation: "IDefinition.OwnedState")]
        public List<IStateUsage> ownedState => this.ComputeOwnedState();

        /// <summary>
        /// The ownedSpecializations of this Classifier that are Subclassifications, for which this Classifier
        /// is the subclassifier.
        /// </summary>
        [Property(xmiId: "Core-Classifiers-Classifier-ownedSubclassification", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedSpecialization")]
        [Implements(implementation: "IClassifier.OwnedSubclassification")]
        public List<ISubclassification> ownedSubclassification => this.ComputeOwnedSubclassification();

        /// <summary>
        /// The TransitionUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedTransition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedUsage")]
        [Implements(implementation: "IDefinition.OwnedTransition")]
        public List<ITransitionUsage> ownedTransition => this.ComputeOwnedTransition();

        /// <summary>
        /// The ownedRelationships of this Type that are Unionings, having the Type as their typeUnioned.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedUnioning", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [Implements(implementation: "IType.OwnedUnioning")]
        public List<IUnioning> ownedUnioning => this.ComputeOwnedUnioning();

        /// <summary>
        /// The Usages that are ownedFeatures of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-usage")]
        [Implements(implementation: "IDefinition.OwnedUsage")]
        public List<IUsage> ownedUsage => this.ComputeOwnedUsage();

        /// <summary>
        /// The UseCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedUseCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedCase")]
        [Implements(implementation: "IDefinition.OwnedUseCase")]
        public List<IUseCaseUsage> ownedUseCase => this.ComputeOwnedUseCase();

        /// <summary>
        /// The VerificationCaseUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedVerificationCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedCase")]
        [Implements(implementation: "IDefinition.OwnedVerificationCase")]
        public List<IVerificationCaseUsage> ownedVerificationCase => this.ComputeOwnedVerificationCase();

        /// <summary>
        /// The ViewUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedView", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedPart")]
        [Implements(implementation: "IDefinition.OwnedView")]
        public List<IViewUsage> ownedView => this.ComputeOwnedView();

        /// <summary>
        /// The ViewpointUsages that are ownedUsages of this Definition.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-ownedViewpoint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Definition-ownedRequirement")]
        [Implements(implementation: "IDefinition.OwnedViewpoint")]
        public List<IViewpointUsage> ownedViewpoint => this.ComputeOwnedViewpoint();

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owner", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Owner")]
        public IElement owner => this.ComputeOwner();

        /// <summary>
        /// The owningRelationship of this Element, if that Relationship is a Membership.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-A_memberElement_membership-membership")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningRelationship")]
        [Implements(implementation: "IElement.OwningMembership")]
        public IOwningMembership owningMembership => this.ComputeOwningMembership();

        /// <summary>
        /// The Namespace that owns this Element, which is the membershipOwningNamespace of the owningMembership
        /// of this Element, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningNamespace", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-A_member_namespace-namespace")]
        [RedefinedByProperty("IConjugatedPortDefinition.OriginalPortDefinition")]
        [Implements(implementation: "IElement.OwningNamespace")]
        INamespace Root.Elements.IElement.owningNamespace => this.originalPortDefinition;

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        [Implements(implementation: "IElement.OwningRelationship")]
        public IRelationship OwningRelationship { get; set; }

        /// <summary>
        /// The full ownership-qualified name of this Element, represented in a form that is valid according to
        /// the KerML textual concrete syntax for qualified names (including use of unrestricted name notation
        /// and escaped characters, as necessary). The qualifiedName is null if this Element has no
        /// owningNamespace or if there is not a complete ownership chain of named Namespaces from a root
        /// Namespace to this Element. If the owningNamespace has other Elements with the same name as this one,
        /// then the qualifiedName is null for all such Elements other than the first.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-qualifiedName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.QualifiedName")]
        public string qualifiedName => this.ComputeQualifiedName();

        /// <summary>
        /// The short name to be used for this Element during name resolution within its owningNamespace. This
        /// is derived using the effectiveShortName() operation. By default, it is the same as the
        /// declaredShortName, but this is overridden for certain kinds of Elements to compute a shortName even
        /// when the declaredName is null.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-shortName", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.ShortName")]
        public string shortName => this.ComputeShortName();

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-textualRepresentation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotatingElement-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedElement")]
        [Implements(implementation: "IElement.TextualRepresentation")]
        public List<ITextualRepresentation> textualRepresentation => this.ComputeTextualRepresentation();

        /// <summary>
        /// The interpretations of a Type with unioningTypes are asserted to be the same as those of all the
        /// unioningTypes together, which are the Types derived from the unioningType of the ownedUnionings of
        /// this Type. For example, a Classifier for people might be the union of Classifiers for all the sexes.
        /// Similarly, a feature for people&#39;s children might be the union of features dividing them in the
        /// same ways as people in general.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-unioningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.UnioningType")]
        public List<IType> unioningType => this.ComputeUnioningType();

        /// <summary>
        /// The Usages that are features of this Definition (not necessarily owned).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-usage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        [Implements(implementation: "IDefinition.Usage")]
        public List<IUsage> usage => this.ComputeUsage();

        /// <summary>
        /// The Usages which represent the variants of this Definition as a variation point Definition, if
        /// isVariation = true. If isVariation = false, the there must be no variants.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-variant", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        [Implements(implementation: "IDefinition.Variant")]
        public List<IUsage> variant => this.ComputeVariant();

        /// <summary>
        /// The ownedMemberships of this Definition that are VariantMemberships. If isVariation = true, then
        /// this must be all ownedMemberships of the Definition. If isVariation = false, then
        /// variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Definition-variantMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMembership")]
        [Implements(implementation: "IDefinition.VariantMembership")]
        public List<IVariantMembership> variantMembership => this.ComputeVariantMembership();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
