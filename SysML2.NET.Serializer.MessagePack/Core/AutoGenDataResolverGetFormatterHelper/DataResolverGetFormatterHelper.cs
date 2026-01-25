// -------------------------------------------------------------------------------------------------
// <copyright file="DataResolverGetFormatterHelper.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;
    using System.Collections.Generic;

    using global::MessagePack.Formatters;

    /// <summary>
    /// Helper class that resolves a <see cref="IMessagePackFormatter"/> based on a <see cref="Type"/>
    /// </summary>
    internal static class DataResolverGetFormatterHelper
    {
        private static readonly Dictionary<Type, object> FormatterMap = new()
        {
            { typeof(Payload), new PayloadMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage), new AcceptActionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition), new ActionDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.ActionUsage), new ActionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership), new ActorMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition), new AllocationDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage), new AllocationUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition), new AnalysisCaseDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage), new AnalysisCaseUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement), new AnnotatingElementMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.Annotation), new AnnotationMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage), new AssertConstraintUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage), new AssignmentActionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Associations.Association), new AssociationMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure), new AssociationStructureMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition), new AttributeDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage), new AttributeUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior), new BehaviorMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector), new BindingConnectorMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage), new BindingConnectorAsUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression), new BooleanExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition), new CalculationDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage), new CalculationUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition), new CaseDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Cases.CaseUsage), new CaseUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Classes.Class), new ClassMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Classifiers.Classifier), new ClassifierMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression), new CollectExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.Comment), new CommentMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition), new ConcernDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage), new ConcernUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition), new ConjugatedPortDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping), new ConjugatedPortTypingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Conjugation), new ConjugationMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition), new ConnectionDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage), new ConnectionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.Connector), new ConnectorMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition), new ConstraintDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage), new ConstraintUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression), new ConstructorExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.CrossSubsetting), new CrossSubsettingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.DataTypes.DataType), new DataTypeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.DecisionNode), new DecisionNodeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition), new DefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Dependencies.Dependency), new DependencyMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Differencing), new DifferencingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Disjoining), new DisjoiningMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.Documentation), new DocumentationMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership), new ElementFilterMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership), new EndFeatureMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition), new EnumerationDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage), new EnumerationUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage), new EventOccurrenceUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage), new ExhibitStateUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Expression), new ExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.Feature), new FeatureMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression), new FeatureChainExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureChaining), new FeatureChainingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureInverting), new FeatureInvertingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.FeatureMembership), new FeatureMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression), new FeatureReferenceExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureTyping), new FeatureTypingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue), new FeatureValueMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.Flow), new FlowMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition), new FlowDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd), new FlowEndMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Flows.FlowUsage), new FlowUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.ForkNode), new ForkNodeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage), new ForLoopActionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership), new FramedConcernMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Function), new FunctionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage), new IfActionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage), new IncludeUseCaseUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression), new IndexExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.Interaction), new InteractionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition), new InterfaceDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage), new InterfaceUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Intersecting), new IntersectingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Invariant), new InvariantMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression), new InvocationExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Items.ItemDefinition), new ItemDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Items.ItemUsage), new ItemUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.JoinNode), new JoinNodeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage), new LibraryPackageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean), new LiteralBooleanMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression), new LiteralExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity), new LiteralInfinityMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger), new LiteralIntegerMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational), new LiteralRationalMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString), new LiteralStringMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.Membership), new MembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.MembershipExpose), new MembershipExposeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport), new MembershipImportMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.MergeNode), new MergeNodeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass), new MetaclassMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression), new MetadataAccessExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition), new MetadataDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature), new MetadataFeatureMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage), new MetadataUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Multiplicity), new MultiplicityMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange), new MultiplicityRangeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.Namespace), new NamespaceMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose), new NamespaceExposeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport), new NamespaceImportMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression), new NullExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership), new ObjectiveMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition), new OccurrenceDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage), new OccurrenceUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression), new OperatorExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership), new OwningMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Packages.Package), new PackageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership), new ParameterMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Parts.PartDefinition), new PartDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Parts.PartUsage), new PartUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature), new PayloadFeatureMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage), new PerformActionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortConjugation), new PortConjugationMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortDefinition), new PortDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortUsage), new PortUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Predicate), new PredicateMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.Redefinition), new RedefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting), new ReferenceSubsettingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage), new ReferenceUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition), new RenderingDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.RenderingUsage), new RenderingUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership), new RequirementConstraintMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition), new RequirementDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage), new RequirementUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership), new RequirementVerificationMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership), new ResultExpressionMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership), new ReturnParameterMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage), new SatisfyRequirementUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression), new SelectExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage), new SendActionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Specialization), new SpecializationMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership), new StakeholderMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.States.StateDefinition), new StateDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership), new StateSubactionMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.States.StateUsage), new StateUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.Step), new StepMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Structures.Structure), new StructureMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Classifiers.Subclassification), new SubclassificationMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership), new SubjectMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.Subsetting), new SubsettingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.Succession), new SuccessionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage), new SuccessionAsUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow), new SuccessionFlowMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage), new SuccessionFlowUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage), new TerminateActionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation), new TextualRepresentationMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership), new TransitionFeatureMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.States.TransitionUsage), new TransitionUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression), new TriggerInvocationExpressionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Type), new TypeMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Features.TypeFeaturing), new TypeFeaturingMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Unioning), new UnioningMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage), new UsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition), new UseCaseDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage), new UseCaseUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership), new VariantMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition), new VerificationCaseDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage), new VerificationCaseUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewDefinition), new ViewDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition), new ViewpointDefinitionMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage), new ViewpointUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership), new ViewRenderingMembershipMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewUsage), new ViewUsageMessagePackFormatter() },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage), new WhileLoopActionUsageMessagePackFormatter() },
        };

        /// <summary>
        /// Gets a <see cref="IMessagePackFormatter"/> for the specific <see cref="Type"/>
        /// </summary>
        /// <param name="t">
        /// The subject <see cref="Type"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="IMessagePackFormatter"/>
        /// </returns>
        internal static object GetFormatter(Type t)
        {
            object formatter;
            if (FormatterMap.TryGetValue(t, out formatter))
            {
                return formatter;
            }

            // If type can not get, must return null for fallback mechanism.
            return null;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
