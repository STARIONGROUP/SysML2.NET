// -------------------------------------------------------------------------------------------------
// <copyright file="ComparerProvider.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

namespace SysML2.NET.Extensions.Core.DTO.Comparers
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// Provides runtime access to equality comparers for DTO types.
    /// </summary>
    public static class ComparerProvider
    {
        private static readonly IReadOnlyDictionary<Type, IEqualityComparer<IData>> Comparers =
            new Dictionary<Type, IEqualityComparer<IData>>
            {
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage>(new AcceptActionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition>(new ActionDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.ActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.ActionUsage>(new ActionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership>(new ActorMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition>(new AllocationDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage>(new AllocationUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition>(new AnalysisCaseDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage), new Adapter<SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage>(new AnalysisCaseUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement), new Adapter<SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement>(new AnnotatingElementComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Annotations.Annotation), new Adapter<SysML2.NET.Core.DTO.Root.Annotations.Annotation>(new AnnotationComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage>(new AssertConstraintUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage>(new AssignmentActionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Associations.Association), new Adapter<SysML2.NET.Core.DTO.Kernel.Associations.Association>(new AssociationComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure), new Adapter<SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure>(new AssociationStructureComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition>(new AttributeDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage>(new AttributeUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior), new Adapter<SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior>(new BehaviorComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector), new Adapter<SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector>(new BindingConnectorComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage>(new BindingConnectorAsUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression>(new BooleanExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition>(new CalculationDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage>(new CalculationUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition>(new CaseDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Cases.CaseUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Cases.CaseUsage>(new CaseUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Classes.Class), new Adapter<SysML2.NET.Core.DTO.Kernel.Classes.Class>(new ClassComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Classifiers.Classifier), new Adapter<SysML2.NET.Core.DTO.Core.Classifiers.Classifier>(new ClassifierComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression>(new CollectExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Annotations.Comment), new Adapter<SysML2.NET.Core.DTO.Root.Annotations.Comment>(new CommentComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition>(new ConcernDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage>(new ConcernUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition>(new ConjugatedPortDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping), new Adapter<SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping>(new ConjugatedPortTypingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.Conjugation), new Adapter<SysML2.NET.Core.DTO.Core.Types.Conjugation>(new ConjugationComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition>(new ConnectionDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage>(new ConnectionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.Connector), new Adapter<SysML2.NET.Core.DTO.Kernel.Connectors.Connector>(new ConnectorComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition>(new ConstraintDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage>(new ConstraintUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression>(new ConstructorExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.CrossSubsetting), new Adapter<SysML2.NET.Core.DTO.Core.Features.CrossSubsetting>(new CrossSubsettingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.DataTypes.DataType), new Adapter<SysML2.NET.Core.DTO.Kernel.DataTypes.DataType>(new DataTypeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.DecisionNode), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.DecisionNode>(new DecisionNodeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition), new Adapter<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition>(new DefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Dependencies.Dependency), new Adapter<SysML2.NET.Core.DTO.Root.Dependencies.Dependency>(new DependencyComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.Differencing), new Adapter<SysML2.NET.Core.DTO.Core.Types.Differencing>(new DifferencingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.Disjoining), new Adapter<SysML2.NET.Core.DTO.Core.Types.Disjoining>(new DisjoiningComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Annotations.Documentation), new Adapter<SysML2.NET.Core.DTO.Root.Annotations.Documentation>(new DocumentationComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership), new Adapter<SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership>(new ElementFilterMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership), new Adapter<SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership>(new EndFeatureMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition>(new EnumerationDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage>(new EnumerationUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage>(new EventOccurrenceUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage), new Adapter<SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage>(new ExhibitStateUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Expression), new Adapter<SysML2.NET.Core.DTO.Kernel.Functions.Expression>(new ExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.Feature), new Adapter<SysML2.NET.Core.DTO.Core.Features.Feature>(new FeatureComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression>(new FeatureChainExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureChaining), new Adapter<SysML2.NET.Core.DTO.Core.Features.FeatureChaining>(new FeatureChainingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureInverting), new Adapter<SysML2.NET.Core.DTO.Core.Features.FeatureInverting>(new FeatureInvertingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.FeatureMembership), new Adapter<SysML2.NET.Core.DTO.Core.Types.FeatureMembership>(new FeatureMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression>(new FeatureReferenceExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureTyping), new Adapter<SysML2.NET.Core.DTO.Core.Features.FeatureTyping>(new FeatureTypingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue), new Adapter<SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue>(new FeatureValueComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.Flow), new Adapter<SysML2.NET.Core.DTO.Kernel.Interactions.Flow>(new FlowComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition>(new FlowDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd), new Adapter<SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd>(new FlowEndComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Flows.FlowUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Flows.FlowUsage>(new FlowUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.ForkNode), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.ForkNode>(new ForkNodeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage>(new ForLoopActionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership>(new FramedConcernMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Function), new Adapter<SysML2.NET.Core.DTO.Kernel.Functions.Function>(new FunctionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage>(new IfActionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage), new Adapter<SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage>(new IncludeUseCaseUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression>(new IndexExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.Interaction), new Adapter<SysML2.NET.Core.DTO.Kernel.Interactions.Interaction>(new InteractionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition>(new InterfaceDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage>(new InterfaceUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.Intersecting), new Adapter<SysML2.NET.Core.DTO.Core.Types.Intersecting>(new IntersectingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Invariant), new Adapter<SysML2.NET.Core.DTO.Kernel.Functions.Invariant>(new InvariantComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression>(new InvocationExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Items.ItemDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Items.ItemDefinition>(new ItemDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Items.ItemUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Items.ItemUsage>(new ItemUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.JoinNode), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.JoinNode>(new JoinNodeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage), new Adapter<SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage>(new LibraryPackageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean>(new LiteralBooleanComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression>(new LiteralExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity>(new LiteralInfinityComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger>(new LiteralIntegerComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational>(new LiteralRationalComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString>(new LiteralStringComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Namespaces.Membership), new Adapter<SysML2.NET.Core.DTO.Root.Namespaces.Membership>(new MembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.MembershipExpose), new Adapter<SysML2.NET.Core.DTO.Systems.Views.MembershipExpose>(new MembershipExposeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport), new Adapter<SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport>(new MembershipImportComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.MergeNode), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.MergeNode>(new MergeNodeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass), new Adapter<SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass>(new MetaclassComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression>(new MetadataAccessExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition>(new MetadataDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature), new Adapter<SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature>(new MetadataFeatureComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage>(new MetadataUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.Multiplicity), new Adapter<SysML2.NET.Core.DTO.Core.Types.Multiplicity>(new MultiplicityComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange), new Adapter<SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange>(new MultiplicityRangeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Namespaces.Namespace), new Adapter<SysML2.NET.Core.DTO.Root.Namespaces.Namespace>(new NamespaceComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose), new Adapter<SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose>(new NamespaceExposeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport), new Adapter<SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport>(new NamespaceImportComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression>(new NullExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership), new Adapter<SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership>(new ObjectiveMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition>(new OccurrenceDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage>(new OccurrenceUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression>(new OperatorExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership), new Adapter<SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership>(new OwningMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Packages.Package), new Adapter<SysML2.NET.Core.DTO.Kernel.Packages.Package>(new PackageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership), new Adapter<SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership>(new ParameterMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Parts.PartDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Parts.PartDefinition>(new PartDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Parts.PartUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Parts.PartUsage>(new PartUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature), new Adapter<SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature>(new PayloadFeatureComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage>(new PerformActionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortConjugation), new Adapter<SysML2.NET.Core.DTO.Systems.Ports.PortConjugation>(new PortConjugationComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Ports.PortDefinition>(new PortDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Ports.PortUsage>(new PortUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Predicate), new Adapter<SysML2.NET.Core.DTO.Kernel.Functions.Predicate>(new PredicateComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.Redefinition), new Adapter<SysML2.NET.Core.DTO.Core.Features.Redefinition>(new RedefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting), new Adapter<SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting>(new ReferenceSubsettingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage), new Adapter<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage>(new ReferenceUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition>(new RenderingDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.RenderingUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Views.RenderingUsage>(new RenderingUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership>(new RequirementConstraintMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition>(new RequirementDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage>(new RequirementUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership), new Adapter<SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership>(new RequirementVerificationMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership), new Adapter<SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership>(new ResultExpressionMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership), new Adapter<SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership>(new ReturnParameterMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage>(new SatisfyRequirementUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression), new Adapter<SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression>(new SelectExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage>(new SendActionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.Specialization), new Adapter<SysML2.NET.Core.DTO.Core.Types.Specialization>(new SpecializationComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership>(new StakeholderMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.States.StateDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.States.StateDefinition>(new StateDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership), new Adapter<SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership>(new StateSubactionMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.States.StateUsage), new Adapter<SysML2.NET.Core.DTO.Systems.States.StateUsage>(new StateUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.Step), new Adapter<SysML2.NET.Core.DTO.Kernel.Behaviors.Step>(new StepComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Structures.Structure), new Adapter<SysML2.NET.Core.DTO.Kernel.Structures.Structure>(new StructureComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Classifiers.Subclassification), new Adapter<SysML2.NET.Core.DTO.Core.Classifiers.Subclassification>(new SubclassificationComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership), new Adapter<SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership>(new SubjectMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.Subsetting), new Adapter<SysML2.NET.Core.DTO.Core.Features.Subsetting>(new SubsettingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.Succession), new Adapter<SysML2.NET.Core.DTO.Kernel.Connectors.Succession>(new SuccessionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage>(new SuccessionAsUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow), new Adapter<SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow>(new SuccessionFlowComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage>(new SuccessionFlowUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage>(new TerminateActionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation), new Adapter<SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation>(new TextualRepresentationComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership), new Adapter<SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership>(new TransitionFeatureMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.States.TransitionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.States.TransitionUsage>(new TransitionUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression>(new TriggerInvocationExpressionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.Type), new Adapter<SysML2.NET.Core.DTO.Core.Types.Type>(new TypeComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Features.TypeFeaturing), new Adapter<SysML2.NET.Core.DTO.Core.Features.TypeFeaturing>(new TypeFeaturingComparer()) },
                { typeof(SysML2.NET.Core.DTO.Core.Types.Unioning), new Adapter<SysML2.NET.Core.DTO.Core.Types.Unioning>(new UnioningComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage), new Adapter<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage>(new UsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition>(new UseCaseDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage), new Adapter<SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage>(new UseCaseUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership), new Adapter<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership>(new VariantMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition>(new VerificationCaseDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage), new Adapter<SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage>(new VerificationCaseUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Views.ViewDefinition>(new ViewDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition), new Adapter<SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition>(new ViewpointDefinitionComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage>(new ViewpointUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership), new Adapter<SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership>(new ViewRenderingMembershipComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Views.ViewUsage>(new ViewUsageComparer()) },
                { typeof(SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage), new Adapter<SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage>(new WhileLoopActionUsageComparer()) },
            };

        /// <summary>
        /// Resolves the equality comparer for the specified DTO runtime type.
        /// </summary>
        public static IEqualityComparer<IData> Resolve(Type dtoType)
        {
            if (dtoType == null)
            {
                throw new ArgumentNullException(nameof(dtoType));
            }

            if (!Comparers.TryGetValue(dtoType, out var comparer))
            {
                throw new NotSupportedException($"No equality comparer generated for DTO type '{dtoType.FullName}'.");
            }

            return comparer;
        }

        /// <summary>
        /// Resolves the equality comparer for the runtime type of the given DTO.
        /// </summary>
        public static IEqualityComparer<IData> Resolve(IData dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return Resolve(dto.GetType());
        }

        /// <summary>
        /// Adapts a strongly-typed comparer to <see cref="IEqualityComparer{IData}"/>.
        /// This adapter exists solely to bridge generic invariance in C#.
        /// </summary>
        private sealed class Adapter<T> : IEqualityComparer<IData>
            where T : class, IData
        {
            private readonly IEqualityComparer<T> inner;

            /// <summary>
            /// Initializes a new instance of the <see cref="Adapter{T}"/> class
            /// wrapping the specified strongly-typed comparer.
            /// </summary>
            /// <param name="inner">
            /// The generated equality comparer responsible for comparing instances
            /// of <typeparamref name="T"/>.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// Thrown when <paramref name="inner"/> is <c>null</c>.
            /// </exception>
            public Adapter(IEqualityComparer<T> inner)
            {
                this.inner = inner;
            }

            /// <summary>
            /// Determines whether the specified <see cref="IData"/> instances are
            /// equal by delegating to the wrapped <see cref="IEqualityComparer{T}"/>.
            /// </summary>
            /// <param name="x">
            /// The first object to compare.
            /// </param>
            /// <param name="y">
            /// The second object to compare.
            /// </param>
            /// <returns>
            /// <c>true</c> if <paramref name="x"/> and <paramref name="y"/> are
            /// considered equal according to the wrapped comparer; otherwise,
            /// <c>false</c>.
            /// </returns>
            /// <remarks>
            /// <para>
            /// If both references are identical, the method returns <c>true</c>
            /// immediately. If either reference is <c>null</c>, the method returns
            /// <c>false</c>.
            /// </para>
            /// <para>
            /// Both arguments are expected to be instances of
            /// <typeparamref name="T"/>. This invariant is guaranteed by
            /// <see cref="ComparerProvider"/>, which resolves adapters strictly
            /// by the runtime type of the DTO.
            /// </para>
            /// </remarks>
            public bool Equals(IData x, IData y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x is null || y is null) return false;

                return this.inner.Equals((T)x, (T)y);
            }

            /// <summary>
            /// Returns a hash code for the specified <see cref="IData"/> instance
            /// by delegating to the wrapped <see cref="IEqualityComparer{T}"/>.
            /// </summary>
            /// <param name="obj">
            /// The object for which a hash code is to be returned.
            /// </param>
            /// <returns>
            /// A hash code for <paramref name="obj"/>, suitable for use in hash-based
            /// collections when combined with the corresponding equality semantics.
            /// </returns>
            /// <remarks>
            /// <para>
            /// The hash code implementation is fully delegated to the wrapped
            /// comparer and therefore remains consistent with its
            /// <see cref="Equals(IData, IData)"/> semantics.
            /// </para>
            /// <para>
            /// The adapter does not attempt to normalize or reinterpret the hash
            /// code in any way.
            /// </para>
            /// </remarks>
            public int GetHashCode(IData obj)
            {
                return this.inner.GetHashCode((T)obj);
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
