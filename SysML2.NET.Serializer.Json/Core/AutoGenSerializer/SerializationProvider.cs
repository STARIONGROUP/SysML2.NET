// -------------------------------------------------------------------------------------------------
// <copyright file="SerializationProvider.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Delegate provider for the appropriate serialization method to serialize a <see cref="Type" />
    /// </summary>
    internal static class SerializationProvider
    {
        /// <summary>
        /// Caches the delegate <see cref="Action{object, Utf8JsonWriter, SerializationModeKind, bool}"/> for the
        /// <see cref="System.Type"/> that is to be serialized
        /// </summary>
        private static readonly Dictionary<System.Type, Action<object, Utf8JsonWriter, SerializationModeKind, bool>> SerializerActionMap = new Dictionary<System.Type, Action<object, Utf8JsonWriter, SerializationModeKind, bool>>
        {
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage), AcceptActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition), ActionDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.ActionUsage), ActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership), ActorMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition), AllocationDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage), AllocationUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition), AnalysisCaseDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage), AnalysisCaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement), AnnotatingElementSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.Annotation), AnnotationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage), AssertConstraintUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage), AssignmentActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Associations.Association), AssociationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure), AssociationStructureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition), AttributeDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage), AttributeUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior), BehaviorSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector), BindingConnectorSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage), BindingConnectorAsUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression), BooleanExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition), CalculationDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage), CalculationUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition), CaseDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Cases.CaseUsage), CaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Classes.Class), ClassSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Classifiers.Classifier), ClassifierSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression), CollectExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.Comment), CommentSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition), ConcernDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage), ConcernUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition), ConjugatedPortDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping), ConjugatedPortTypingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Conjugation), ConjugationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition), ConnectionDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage), ConnectionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.Connector), ConnectorSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition), ConstraintDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage), ConstraintUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression), ConstructorExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.CrossSubsetting), CrossSubsettingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.DataTypes.DataType), DataTypeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.DecisionNode), DecisionNodeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition), DefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Dependencies.Dependency), DependencySerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Differencing), DifferencingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Disjoining), DisjoiningSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.Documentation), DocumentationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership), ElementFilterMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership), EndFeatureMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition), EnumerationDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage), EnumerationUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage), EventOccurrenceUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage), ExhibitStateUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Expression), ExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.Feature), FeatureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression), FeatureChainExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureChaining), FeatureChainingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureInverting), FeatureInvertingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.FeatureMembership), FeatureMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression), FeatureReferenceExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.FeatureTyping), FeatureTypingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue), FeatureValueSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.Flow), FlowSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition), FlowDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd), FlowEndSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Flows.FlowUsage), FlowUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.ForkNode), ForkNodeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage), ForLoopActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership), FramedConcernMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Function), FunctionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage), IfActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage), IncludeUseCaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression), IndexExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.Interaction), InteractionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition), InterfaceDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage), InterfaceUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Intersecting), IntersectingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Invariant), InvariantSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression), InvocationExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Items.ItemDefinition), ItemDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Items.ItemUsage), ItemUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.JoinNode), JoinNodeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage), LibraryPackageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean), LiteralBooleanSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression), LiteralExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity), LiteralInfinitySerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger), LiteralIntegerSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational), LiteralRationalSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString), LiteralStringSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.Membership), MembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.MembershipExpose), MembershipExposeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport), MembershipImportSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.MergeNode), MergeNodeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass), MetaclassSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression), MetadataAccessExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition), MetadataDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature), MetadataFeatureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage), MetadataUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Multiplicity), MultiplicitySerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange), MultiplicityRangeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.Namespace), NamespaceSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose), NamespaceExposeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport), NamespaceImportSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression), NullExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership), ObjectiveMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition), OccurrenceDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage), OccurrenceUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression), OperatorExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership), OwningMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Packages.Package), PackageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership), ParameterMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Parts.PartDefinition), PartDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Parts.PartUsage), PartUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature), PayloadFeatureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage), PerformActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortConjugation), PortConjugationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortDefinition), PortDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Ports.PortUsage), PortUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.Predicate), PredicateSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.Redefinition), RedefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting), ReferenceSubsettingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage), ReferenceUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition), RenderingDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.RenderingUsage), RenderingUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership), RequirementConstraintMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition), RequirementDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage), RequirementUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership), RequirementVerificationMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership), ResultExpressionMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership), ReturnParameterMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage), SatisfyRequirementUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression), SelectExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage), SendActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Specialization), SpecializationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership), StakeholderMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.States.StateDefinition), StateDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership), StateSubactionMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.States.StateUsage), StateUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.Step), StepSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Structures.Structure), StructureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Classifiers.Subclassification), SubclassificationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership), SubjectMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.Subsetting), SubsettingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Connectors.Succession), SuccessionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage), SuccessionAsUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow), SuccessionFlowSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage), SuccessionFlowUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage), TerminateActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation), TextualRepresentationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership), TransitionFeatureMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.States.TransitionUsage), TransitionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression), TriggerInvocationExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Type), TypeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Features.TypeFeaturing), TypeFeaturingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Core.Types.Unioning), UnioningSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage), UsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition), UseCaseDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage), UseCaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership), VariantMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition), VerificationCaseDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage), VerificationCaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewDefinition), ViewDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition), ViewpointDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage), ViewpointUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership), ViewRenderingMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Views.ViewUsage), ViewUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage), WhileLoopActionUsageSerializer.Serialize },
        };

        /// <summary>
        /// Provides the delegate <see cref="Action{object, Utf8JsonWriter, SerializationModeKind, bool}"/> for the
        /// <see cref="System.Type"/> that is to be serialized
        /// </summary>
        /// <param name="type">
        /// The subject <see cref="System.Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// A Delegate of <see cref="Action{object, Utf8JsonWriter, SerializationModeKind, bool}"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="System.Type"/> is not supported.
        /// </exception>
        internal static Action<object, Utf8JsonWriter, SerializationModeKind, bool> Provide(System.Type type)
        {
            return !SerializerActionMap.TryGetValue(type, out var action) ? throw new NotSupportedException($"The {type.Name} is not supported by the SerializationProvider.") : action;
        }

        /// <summary>
        /// Asserts whether the <paramref name="type"/> is supported by the provider
        /// </summary>
        /// <param name="type">
        /// The <see cref="System.Type"/> for which support is asserted
        /// </param>
        /// <returns></returns>
        internal static bool IsTypeSupported(System.Type type)
        {
            return SerializerActionMap.ContainsKey(type);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
