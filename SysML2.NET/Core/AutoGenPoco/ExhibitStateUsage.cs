// -------------------------------------------------------------------------------------------------
// <copyright file="ExhibitStateUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.States
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
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Decorators;

    /// <summary>
    /// An ExhibitStateUsage is a StateUsage that represents the exhibiting of a StateUsage. Unless it is
    /// the StateUsage itself, the StateUsage to be exhibited is related to the ExhibitStateUsage by a
    /// ReferenceSubsetting Relationship. An ExhibitStateUsage is also a PerformActionUsage, with its
    /// exhibitedState as the performedAction.
    /// </summary>
    [Class(xmiId: "Systems-States-ExhibitStateUsage", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial class ExhibitStateUsage : IExhibitStateUsage
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IData.Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The Behaviors that are the types of this ActionUsage. Nominally, these would be ActionDefinitions,
        /// but other kinds of Kernel Behaviors are also allowed, to permit use of Behaviors from the Kernel
        /// Model Libraries.
        /// </summary>
        [Property(xmiId: "Systems-Actions-ActionUsage-actionDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Kernel-Behaviors-Step-behavior")]
        [RedefinedProperty(propertyName: "Systems-Occurrences-OccurrenceUsage-occurrenceDefinition")]
        [RedefinedByProperty("IStateUsage.StateDefinition")]
        [Implements(implementation: "IActionUsage.ActionDefinition")]
        List<IBehavior> Systems.Actions.IActionUsage.actionDefinition => [.. this.stateDefinition];

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-aliasIds", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.AliasIds")]
        public List<string> AliasIds { get; set; } = [];

        /// <summary>
        /// The Behaviors that type this Step.
        /// </summary>
        [Property(xmiId: "Kernel-Behaviors-Step-behavior", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-type")]
        [RedefinedByProperty("IActionUsage.ActionDefinition")]
        [Implements(implementation: "IStep.Behavior")]
        List<IBehavior> Kernel.Behaviors.IStep.behavior => [.. ((SysML2.NET.Core.POCO.Systems.Actions.IActionUsage)this).actionDefinition];

        /// <summary>
        /// The Feature that are chained together to determine the values of this Feature, derived from the
        /// chainingFeatures of the ownedFeatureChainings of this Feature, in the same order. The values of a
        /// Feature with chainingFeatures are the same as values of the last Feature in the chain, which can be
        /// found by starting with the values of the first Feature (for each instance of the domain of the
        /// original Feature), then using each of those as domain instances to find the values of the second
        /// Feature in chainingFeatures, and so on, to values of the last Feature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-chainingFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [Implements(implementation: "IFeature.ChainingFeature")]
        public List<IFeature> chainingFeature => this.ComputeChainingFeature();

        /// <summary>
        /// The second chainingFeature of the crossedFeature of the ownedCrossSubsetting of this Feature, if it
        /// has one. Semantically, the values of the crossFeature of an end Feature must include all values of
        /// the end Feature obtained when navigating from values of the other end Features of the same
        /// owningType.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-crossFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IFeature.CrossFeature")]
        public IFeature crossFeature => this.ComputeCrossFeature();

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
        /// The Classifiers that are the types of this Usage. Nominally, these are Definitions, but other kinds
        /// of Kernel Classifiers are also allowed, to permit use of Classifiers from the Kernel Model
        /// Libraries.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-definition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-Feature-type")]
        [RedefinedByProperty("IOccurrenceUsage.OccurrenceDefinition")]
        [Implements(implementation: "IUsage.Definition")]
        List<IClassifier> Systems.DefinitionAndUsage.IUsage.definition => [.. ((SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage)this).occurrenceDefinition];

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
        [RedefinedByProperty("IStep.Parameter")]
        [Implements(implementation: "IType.DirectedFeature")]
        List<IFeature> Core.Types.IType.directedFeature => [.. this.parameter];

        /// <summary>
        /// The usages of this Usage that are directedFeatures.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-directedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        [Implements(implementation: "IUsage.DirectedUsage")]
        public List<IUsage> directedUsage => this.ComputeDirectedUsage();

        /// <summary>
        /// Indicates how values of this Feature are determined or used (as specified for the
        /// FeatureDirectionKind).
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-direction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IFeature.Direction")]
        public FeatureDirectionKind? Direction { get; set; }

        /// <summary>
        /// The ActionUsage of this StateUsage to be performed while in the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateUsage by a StateSubactionMembership
        /// with kind = do.
        /// </summary>
        [Property(xmiId: "Systems-States-StateUsage-doAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IStateUsage.DoAction")]
        public IActionUsage doAction => this.ComputeDoAction();

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
        /// The Type that is related to this Feature by an EndFeatureMembership in which the Feature is an
        /// ownedMemberFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-endOwningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-A_endFeature_typeWithEndFeature-typeWithEndFeature")]
        [SubsettedProperty(propertyName: "Core-Features-Feature-owningType")]
        [Implements(implementation: "IFeature.EndOwningType")]
        public IType endOwningType => this.ComputeEndOwningType();

        /// <summary>
        /// The ActionUsage of this StateUsage to be performed on entry to the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateUsage by a StateSubactionMembership
        /// with kind = entry.
        /// </summary>
        [Property(xmiId: "Systems-States-StateUsage-entryAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IStateUsage.EntryAction")]
        public IActionUsage entryAction => this.ComputeEntryAction();

        /// <summary>
        /// The OccurrenceUsage referenced as an event by this EventOccurrenceUsage. It is the referenceFeature
        /// of the ownedReferenceSubsetting for the EventOccurrenceUsage, if there is one, and, otherwise, the
        /// EventOccurrenceUsage itself.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-EventOccurrenceUsage-eventOccurrence", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedByProperty("IPerformActionUsage.PerformedAction")]
        [Implements(implementation: "IEventOccurrenceUsage.EventOccurrence")]
        IOccurrenceUsage Systems.Occurrences.IEventOccurrenceUsage.eventOccurrence => ((SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage)this).performedAction;

        /// <summary>
        /// The StateUsage to be exhibited by the ExhibitStateUsage. It is the performedAction of the
        /// ExhibitStateUsage considered as a PerformActionUsage, which must be a StateUsage.
        /// </summary>
        [Property(xmiId: "Systems-States-ExhibitStateUsage-exhibitedState", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Actions-PerformActionUsage-performedAction")]
        [Implements(implementation: "IExhibitStateUsage.ExhibitedState")]
        public IStateUsage exhibitedState => this.ComputeExhibitedState();

        /// <summary>
        /// The ActionUsage of this StateUsage to be performed on exit to the state defined by the
        /// StateDefinition. It is the owned ActionUsage related to the StateUsage by a StateSubactionMembership
        /// with kind = exit.
        /// </summary>
        [Property(xmiId: "Systems-States-StateUsage-exitAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IStateUsage.ExitAction")]
        public IActionUsage exitAction => this.ComputeExitAction();

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
        /// The last of the chainingFeatures of this Feature, if it has any. Otherwise, this Feature itself.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-featureTarget", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IFeature.FeatureTarget")]
        public IFeature featureTarget => this.ComputeFeatureTarget();

        /// <summary>
        /// Types that feature this Feature, such that any instance in the domain of the Feature must be
        /// classified by all of these Types, including at least all the featuringTypes of its typeFeaturings. 
        /// If the Feature is chained, then the featuringTypes of the first Feature in the chain are also
        /// featuringTypes of the chained Feature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-featuringType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IFeature.FeaturingType")]
        public List<IType> featuringType => this.ComputeFeaturingType();

        /// <summary>
        /// The Memberships in this Namespace that result from the ownedImports of this Namespace.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Namespace-importedMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-membership")]
        [Implements(implementation: "INamespace.ImportedMembership")]
        public List<IMembership> importedMembership => this.ComputeImportedMembership();

        /// <summary>
        /// The at most one occurrenceDefinition that has isIndividual = true.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceUsage-individualDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-Occurrences-OccurrenceUsage-occurrenceDefinition")]
        [Implements(implementation: "IOccurrenceUsage.IndividualDefinition")]
        public IOccurrenceDefinition individualDefinition => this.ComputeIndividualDefinition();

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
        /// Whether the Feature is a composite feature of its featuringType. If so, the values of the Feature
        /// cannot exist after its featuring instance no longer does and cannot be values of another composite
        /// feature that is not on the same featuring instance.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isComposite", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsComposite")]
        public bool IsComposite { get; set; }

        /// <summary>
        /// Indicates whether this Type has an ownedConjugator.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-isConjugated", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.IsConjugated")]
        public bool isConjugated => this.ComputeIsConjugated();

        /// <summary>
        /// If isVariable is true, then whether the value of this Feature nevertheless does not change over all
        /// snapshots of its owningType.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isConstant", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsConstant")]
        public bool IsConstant { get; set; }

        /// <summary>
        /// Whether the values of this Feature can always be computed from the values of other Features.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isDerived", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
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
        [Property(xmiId: "Core-Features-Feature-isEnd", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsEnd")]
        public bool IsEnd { get; set; }

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
        /// Whether this OccurrenceUsage represents the usage of the specific individual represented by its
        /// individualDefinition.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceUsage-isIndividual", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IOccurrenceUsage.IsIndividual")]
        public bool IsIndividual { get; set; }

        /// <summary>
        /// Whether this Element is contained in the ownership tree of a library model.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-isLibraryElement", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.IsLibraryElement")]
        public bool isLibraryElement => this.ComputeIsLibraryElement();

        /// <summary>
        /// Whether an order exists for the values of this Feature or not.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isOrdered", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsOrdered")]
        public bool IsOrdered { get; set; }

        /// <summary>
        /// Whether the nestedStates of this StateUsage are to all be performed in parallel. If true, none of
        /// the nestedActions (which include nestedStates) may have any incoming or outgoing Transitions. If
        /// false, only one nestedState may be performed at a time.
        /// </summary>
        [Property(xmiId: "Systems-States-StateUsage-isParallel", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IStateUsage.IsParallel")]
        public bool IsParallel { get; set; }

        /// <summary>
        /// Whether the values of this Feature are contained in the space and time of instances of the domain of
        /// the Feature and represent the same thing as those instances.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isPortion", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
        [Implements(implementation: "IFeature.IsPortion")]
        public bool IsPortion { get; set; }

        /// <summary>
        /// Always true for an EventOccurrenceUsage.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-EventOccurrenceUsage-isReference", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [RedefinedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-isReference")]
        [Implements(implementation: "IEventOccurrenceUsage.IsReference")]
        public bool isReference => this.ComputeIsReference();

        /// <summary>
        /// Whether this Usage is a referential Usage, that is, it has isComposite = false.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-isReference", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedByProperty("IEventOccurrenceUsage.IsReference")]
        [Implements(implementation: "IUsage.IsReference")]
        bool Systems.DefinitionAndUsage.IUsage.isReference => this.isReference;

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
        /// Whether or not values for this Feature must have no duplicates or not.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isUnique", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "true")]
        [Implements(implementation: "IFeature.IsUnique")]
        public bool IsUnique { get; set; } = true;

        /// <summary>
        /// Whether the value of this Feature might vary over time. That is, whether the Feature may have a
        /// different value for each snapshot of an owningType that is an Occurrence.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-isVariable", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: "false")]
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
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-isVariation", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IUsage.IsVariation")]
        public bool IsVariation { get; set; }

        /// <summary>
        /// Whether this Usage may be time varying (that is, whether it is featured by the snapshots of its
        /// owningType, rather than being featured by the owningType itself). However, if isConstant is also
        /// true, then the value of the Usage is nevertheless constant over the entire duration of an instance
        /// of its owningType (that is, it has the same value on all snapshots).  The property mayTimeVary
        /// redefines the KerML property Feature::isVariable, making it derived. The property isConstant is
        /// inherited from Feature.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-mayTimeVary", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Features-Feature-isVariable")]
        [Implements(implementation: "IUsage.MayTimeVary")]
        public bool mayTimeVary => this.ComputeMayTimeVary();

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
        /// The ActionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAction", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedOccurrence")]
        [Implements(implementation: "IUsage.NestedAction")]
        public List<IActionUsage> nestedAction => this.ComputeNestedAction();

        /// <summary>
        /// The AllocationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAllocation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        [Implements(implementation: "IUsage.NestedAllocation")]
        public List<IAllocationUsage> nestedAllocation => this.ComputeNestedAllocation();

        /// <summary>
        /// The AnalysisCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAnalysisCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        [Implements(implementation: "IUsage.NestedAnalysisCase")]
        public List<IAnalysisCaseUsage> nestedAnalysisCase => this.ComputeNestedAnalysisCase();

        /// <summary>
        /// The code>AttributeUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedAttribute", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        [Implements(implementation: "IUsage.NestedAttribute")]
        public List<IAttributeUsage> nestedAttribute => this.ComputeNestedAttribute();

        /// <summary>
        /// The CalculationUsage that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedCalculation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAction")]
        [Implements(implementation: "IUsage.NestedCalculation")]
        public List<ICalculationUsage> nestedCalculation => this.ComputeNestedCalculation();

        /// <summary>
        /// The CaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCalculation")]
        [Implements(implementation: "IUsage.NestedCase")]
        public List<ICaseUsage> nestedCase => this.ComputeNestedCase();

        /// <summary>
        /// The ConcernUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConcern", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedRequirement")]
        [Implements(implementation: "IUsage.NestedConcern")]
        public List<IConcernUsage> nestedConcern => this.ComputeNestedConcern();

        /// <summary>
        /// The ConnectorAsUsages that are nestedUsages of this Usage. Note that this list includes
        /// BindingConnectorAsUsages, SuccessionAsUsages, and FlowConnectionUsages because these are
        /// ConnectorAsUsages even though they are not ConnectionUsages.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConnection", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        [Implements(implementation: "IUsage.NestedConnection")]
        public List<IConnectorAsUsage> nestedConnection => this.ComputeNestedConnection();

        /// <summary>
        /// The ConstraintUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedConstraint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedOccurrence")]
        [Implements(implementation: "IUsage.NestedConstraint")]
        public List<IConstraintUsage> nestedConstraint => this.ComputeNestedConstraint();

        /// <summary>
        /// The code>EnumerationUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedEnumeration", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAttribute")]
        [Implements(implementation: "IUsage.NestedEnumeration")]
        public List<IEnumerationUsage> nestedEnumeration => this.ComputeNestedEnumeration();

        /// <summary>
        /// The code>FlowUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedFlow", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        [Implements(implementation: "IUsage.NestedFlow")]
        public List<IFlowUsage> nestedFlow => this.ComputeNestedFlow();

        /// <summary>
        /// The InterfaceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedInterface", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConnection")]
        [Implements(implementation: "IUsage.NestedInterface")]
        public List<IInterfaceUsage> nestedInterface => this.ComputeNestedInterface();

        /// <summary>
        /// The ItemUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedItem", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedOccurrence")]
        [Implements(implementation: "IUsage.NestedItem")]
        public List<IItemUsage> nestedItem => this.ComputeNestedItem();

        /// <summary>
        /// The MetadataUsages that are nestedUsages of this of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedMetadata", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedItem")]
        [Implements(implementation: "IUsage.NestedMetadata")]
        public List<IMetadataUsage> nestedMetadata => this.ComputeNestedMetadata();

        /// <summary>
        /// The OccurrenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedOccurrence", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        [Implements(implementation: "IUsage.NestedOccurrence")]
        public List<IOccurrenceUsage> nestedOccurrence => this.ComputeNestedOccurrence();

        /// <summary>
        /// The PartUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedPart", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedItem")]
        [Implements(implementation: "IUsage.NestedPart")]
        public List<IPartUsage> nestedPart => this.ComputeNestedPart();

        /// <summary>
        /// The PortUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedPort", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        [Implements(implementation: "IUsage.NestedPort")]
        public List<IPortUsage> nestedPort => this.ComputeNestedPort();

        /// <summary>
        /// The ReferenceUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedReference", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        [Implements(implementation: "IUsage.NestedReference")]
        public List<IReferenceUsage> nestedReference => this.ComputeNestedReference();

        /// <summary>
        /// The RenderingUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedRendering", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedPart")]
        [Implements(implementation: "IUsage.NestedRendering")]
        public List<IRenderingUsage> nestedRendering => this.ComputeNestedRendering();

        /// <summary>
        /// The RequirementUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedRequirement", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedConstraint")]
        [Implements(implementation: "IUsage.NestedRequirement")]
        public List<IRequirementUsage> nestedRequirement => this.ComputeNestedRequirement();

        /// <summary>
        /// The StateUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedState", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedAction")]
        [Implements(implementation: "IUsage.NestedState")]
        public List<IStateUsage> nestedState => this.ComputeNestedState();

        /// <summary>
        /// The TransitionUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedTransition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedUsage")]
        [Implements(implementation: "IUsage.NestedTransition")]
        public List<ITransitionUsage> nestedTransition => this.ComputeNestedTransition();

        /// <summary>
        /// The Usages that are ownedFeatures of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedFeature")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-usage")]
        [Implements(implementation: "IUsage.NestedUsage")]
        public List<IUsage> nestedUsage => this.ComputeNestedUsage();

        /// <summary>
        /// The UseCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedUseCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        [Implements(implementation: "IUsage.NestedUseCase")]
        public List<IUseCaseUsage> nestedUseCase => this.ComputeNestedUseCase();

        /// <summary>
        /// The VerificationCaseUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedVerificationCase", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedCase")]
        [Implements(implementation: "IUsage.NestedVerificationCase")]
        public List<IVerificationCaseUsage> nestedVerificationCase => this.ComputeNestedVerificationCase();

        /// <summary>
        /// The ViewUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedView", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedPart")]
        [Implements(implementation: "IUsage.NestedView")]
        public List<IViewUsage> nestedView => this.ComputeNestedView();

        /// <summary>
        /// The ViewpointUsages that are nestedUsages of this Usage.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-nestedViewpoint", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-nestedRequirement")]
        [Implements(implementation: "IUsage.NestedViewpoint")]
        public List<IViewpointUsage> nestedViewpoint => this.ComputeNestedViewpoint();

        /// <summary>
        /// The Classes that are the types of this OccurrenceUsage. Nominally, these are OccurrenceDefinitions,
        /// but other kinds of kernel Classes are also allowed, to permit use of Classes from the Kernel Model
        /// Libraries.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceUsage-occurrenceDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-DefinitionAndUsage-Usage-definition")]
        [RedefinedByProperty("IActionUsage.ActionDefinition")]
        [Implements(implementation: "IOccurrenceUsage.OccurrenceDefinition")]
        List<IClass> Systems.Occurrences.IOccurrenceUsage.occurrenceDefinition => [.. ((SysML2.NET.Core.POCO.Systems.Actions.IActionUsage)this).actionDefinition];

        /// <summary>
        /// All features related to this Type by FeatureMemberships that have direction out or inout.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-output", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-directedFeature")]
        [Implements(implementation: "IType.Output")]
        public List<IFeature> output => this.ComputeOutput();

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
        /// A Conjugation owned by this Type for which the Type is the originalType.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedConjugator", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-A_conjugatedType_conjugator-conjugator")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [Implements(implementation: "IType.OwnedConjugator")]
        public IConjugation ownedConjugator => this.ComputeOwnedConjugator();

        /// <summary>
        /// The one ownedSubsetting of this Feature, if any, that is a CrossSubsetting}, for which the Feature
        /// is the crossingFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedCrossSubsetting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-ownedSubsetting")]
        [Implements(implementation: "IFeature.OwnedCrossSubsetting")]
        public ICrossSubsetting ownedCrossSubsetting => this.ComputeOwnedCrossSubsetting();

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
        /// The ownedMemberFeatures of the ownedFeatureMemberships of this Type.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedFeature", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        [Implements(implementation: "IType.OwnedFeature")]
        public List<IFeature> ownedFeature => this.ComputeOwnedFeature();

        /// <summary>
        /// The ownedRelationships of this Feature that are FeatureChainings, for which the Feature will be the
        /// featureChained.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedFeatureChaining", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [Implements(implementation: "IFeature.OwnedFeatureChaining")]
        public List<IFeatureChaining> ownedFeatureChaining => this.ComputeOwnedFeatureChaining();

        /// <summary>
        /// The ownedRelationships of this Feature that are FeatureInvertings and for which the Feature is the
        /// featureInverted.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedFeatureInverting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-A_invertingFeatureInverting_featureInverted-invertingFeatureInverting")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [Implements(implementation: "IFeature.OwnedFeatureInverting")]
        public List<IFeatureInverting> ownedFeatureInverting => this.ComputeOwnedFeatureInverting();

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
        /// The ownedRelationships of this Namespace that are Imports, for which the Namespace is the
        /// importOwningNamespace.
        /// </summary>
        [Property(xmiId: "Root-Namespaces-Namespace-ownedImport", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [Implements(implementation: "INamespace.OwnedImport")]
        public List<IImport> ownedImport => this.ComputeOwnedImport();

        /// <summary>
        /// The ownedRelationships of this Type that are Intersectings, have the Type as their typeIntersected.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedIntersecting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [Implements(implementation: "IType.OwnedIntersecting")]
        public List<IIntersecting> ownedIntersecting => this.ComputeOwnedIntersecting();

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
        /// The ownedSubsettings of this Feature that are Redefinitions, for which the Feature is the
        /// redefiningFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedRedefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-ownedSubsetting")]
        [Implements(implementation: "IFeature.OwnedRedefinition")]
        public List<IRedefinition> ownedRedefinition => this.ComputeOwnedRedefinition();

        /// <summary>
        /// The one ownedSubsetting of this Feature, if any, that is a ReferenceSubsetting, for which the
        /// Feature is the referencingFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedReferenceSubsetting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-ownedSubsetting")]
        [Implements(implementation: "IFeature.OwnedReferenceSubsetting")]
        public IReferenceSubsetting ownedReferenceSubsetting => this.ComputeOwnedReferenceSubsetting();

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-ownedRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        [Implements(implementation: "IElement.OwnedRelationship")]
        public List<IRelationship> OwnedRelationship { get; set; } = [];

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
        /// The ownedSpecializations of this Feature that are Subsettings, for which the Feature is the
        /// subsettingFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedSubsetting", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedSpecialization")]
        [SubsettedProperty(propertyName: "Core-Features-A_subsettingFeature_subsetting-subsetting")]
        [Implements(implementation: "IFeature.OwnedSubsetting")]
        public List<ISubsetting> ownedSubsetting => this.ComputeOwnedSubsetting();

        /// <summary>
        /// The ownedRelationships of this Feature that are TypeFeaturings and for which the Feature is the
        /// featureOfType.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedTypeFeaturing", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-A_featureOfType_typeFeaturing-typeFeaturing")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [Implements(implementation: "IFeature.OwnedTypeFeaturing")]
        public List<ITypeFeaturing> ownedTypeFeaturing => this.ComputeOwnedTypeFeaturing();

        /// <summary>
        /// The ownedSpecializations of this Feature that are FeatureTypings, for which the Feature is the
        /// typedFeature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-ownedTyping", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-ownedSpecialization")]
        [SubsettedProperty(propertyName: "Core-Features-A_typing_typedFeature-typing")]
        [Implements(implementation: "IFeature.OwnedTyping")]
        public List<IFeatureTyping> ownedTyping => this.ComputeOwnedTyping();

        /// <summary>
        /// The ownedRelationships of this Type that are Unionings, having the Type as their typeUnioned.
        /// </summary>
        [Property(xmiId: "Core-Types-Type-ownedUnioning", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedRelationship")]
        [SubsettedProperty(propertyName: "Root-Elements-A_source_sourceRelationship-sourceRelationship")]
        [Implements(implementation: "IType.OwnedUnioning")]
        public List<IUnioning> ownedUnioning => this.ComputeOwnedUnioning();

        /// <summary>
        /// The owner of this Element, derived as the owningRelatedElement of the owningRelationship of this
        /// Element, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owner", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IElement.Owner")]
        public IElement owner => this.ComputeOwner();

        /// <summary>
        /// The Definition that owns this Usage (if any).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-owningDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-owningType")]
        [SubsettedProperty(propertyName: "Systems-DefinitionAndUsage-A_usage_featuringDefinition-featuringDefinition")]
        [Implements(implementation: "IUsage.OwningDefinition")]
        public IDefinition owningDefinition => this.ComputeOwningDefinition();

        /// <summary>
        /// The FeatureMembership that owns this Feature as an ownedMemberFeature, determining its owningType.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-owningFeatureMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningMembership")]
        [Implements(implementation: "IFeature.OwningFeatureMembership")]
        public IFeatureMembership owningFeatureMembership => this.ComputeOwningFeatureMembership();

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
        [Implements(implementation: "IElement.OwningNamespace")]
        public INamespace owningNamespace => this.ComputeOwningNamespace();

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-owningRelationship", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Elements-A_relatedElement_relationship-relationship")]
        [Implements(implementation: "IElement.OwningRelationship")]
        public IRelationship OwningRelationship { get; set; }

        /// <summary>
        /// The Type that is the owningType of the owningFeatureMembership of this Feature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-owningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-featuringType")]
        [SubsettedProperty(propertyName: "Core-Types-A_typeWithFeature_feature-typeWithFeature")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-owningNamespace")]
        [Implements(implementation: "IFeature.OwningType")]
        public IType owningType => this.ComputeOwningType();

        /// <summary>
        /// The Usage in which this Usage is nested (if any).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-owningUsage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Features-Feature-owningType")]
        [Implements(implementation: "IUsage.OwningUsage")]
        public IUsage owningUsage => this.ComputeOwningUsage();

        /// <summary>
        /// The parameters of this Step, which are defined as its directedFeatures, whose values are passed into
        /// and/or out of a performance of the Step.
        /// </summary>
        [Property(xmiId: "Kernel-Behaviors-Step-parameter", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Core-Types-Type-directedFeature")]
        [Implements(implementation: "IStep.Parameter")]
        public List<IFeature> parameter => this.ComputeParameter();

        /// <summary>
        /// The ActionUsage to be performed by this PerformedActionUsage. It is the eventOccurrence of the
        /// PerformActionUsage considered as an EventOccurrenceUsage, which must be an ActionUsage.
        /// </summary>
        [Property(xmiId: "Systems-Actions-PerformActionUsage-performedAction", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Occurrences-EventOccurrenceUsage-eventOccurrence")]
        [RedefinedByProperty("IExhibitStateUsage.ExhibitedState")]
        [Implements(implementation: "IPerformActionUsage.PerformedAction")]
        IActionUsage Systems.Actions.IPerformActionUsage.performedAction => this.exhibitedState;

        /// <summary>
        /// The kind of temporal portion (time slice or snapshot) is represented by this OccurrenceUsage. If
        /// portionKind is not null, then the owningType of the OccurrenceUsage must be non-null, and the
        /// OccurrenceUsage represents portions of the featuring instance of the owningType.
        /// </summary>
        [Property(xmiId: "Systems-Occurrences-OccurrenceUsage-portionKind", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IOccurrenceUsage.PortionKind")]
        public PortionKind? PortionKind { get; set; }

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
        /// The Behaviors that are the types of this StateUsage. Nominally, these would be StateDefinitions, but
        /// kernel Behaviors are also allowed, to permit use of Behaviors from the Kernel Model Libraries.
        /// </summary>
        [Property(xmiId: "Systems-States-StateUsage-stateDefinition", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Systems-Actions-ActionUsage-actionDefinition")]
        [Implements(implementation: "IStateUsage.StateDefinition")]
        public List<IBehavior> stateDefinition => this.ComputeStateDefinition();

        /// <summary>
        /// The TextualRepresentations that annotate this Element.
        /// </summary>
        [Property(xmiId: "Root-Elements-Element-textualRepresentation", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Annotations-A_annotatedElement_annotatingElement-annotatingElement")]
        [SubsettedProperty(propertyName: "Root-Elements-Element-ownedElement")]
        [Implements(implementation: "IElement.TextualRepresentation")]
        public List<ITextualRepresentation> textualRepresentation => this.ComputeTextualRepresentation();

        /// <summary>
        /// Types that restrict the values of this Feature, such that the values must be instances of all the
        /// types. The types of a Feature are derived from its typings and the types of its subsettings. If the
        /// Feature is chained, then the types of the last Feature in the chain are also types of the chained
        /// Feature.
        /// </summary>
        [Property(xmiId: "Core-Features-Feature-type", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
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
        [Property(xmiId: "Core-Types-Type-unioningType", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [Implements(implementation: "IType.UnioningType")]
        public List<IType> unioningType => this.ComputeUnioningType();

        /// <summary>
        /// The Usages that are features of this Usage (not necessarily owned).
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-usage", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Core-Types-Type-feature")]
        [Implements(implementation: "IUsage.Usage")]
        public List<IUsage> usage => this.ComputeUsage();

        /// <summary>
        /// The Usages which represent the variants of this Usage as a variation point Usage, if isVariation =
        /// true. If isVariation = false, then there must be no variants.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-variant", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMember")]
        [Implements(implementation: "IUsage.Variant")]
        public List<IUsage> variant => this.ComputeVariant();

        /// <summary>
        /// The ownedMemberships of this Usage that are VariantMemberships. If isVariation = true, then this
        /// must be all memberships of the Usage. If isVariation = false, then variantMembershipmust be empty.
        /// </summary>
        [Property(xmiId: "Systems-DefinitionAndUsage-Usage-variantMembership", aggregation: AggregationKind.None, lowerValue: 0, upperValue: -1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "Root-Namespaces-Namespace-ownedMembership")]
        [Implements(implementation: "IUsage.VariantMembership")]
        public List<IVariantMembership> variantMembership => this.ComputeVariantMembership();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
