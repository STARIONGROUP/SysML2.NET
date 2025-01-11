// -------------------------------------------------------------------------------------------------
// <copyright file="SerializationProvider.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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
        private static readonly Dictionary<System.Type, Action<object, Utf8JsonWriter, SerializationModeKind>> SerializerActionMap = new Dictionary<System.Type, Action<object, Utf8JsonWriter, SerializationModeKind>>
        {
            { typeof(SysML2.NET.Core.DTO.AcceptActionUsage), AcceptActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ActionDefinition), ActionDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ActionUsage), ActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ActorMembership), ActorMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AllocationDefinition), AllocationDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AllocationUsage), AllocationUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AnalysisCaseDefinition), AnalysisCaseDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AnalysisCaseUsage), AnalysisCaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AnnotatingElement), AnnotatingElementSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Annotation), AnnotationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AssertConstraintUsage), AssertConstraintUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AssignmentActionUsage), AssignmentActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Association), AssociationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AssociationStructure), AssociationStructureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AttributeDefinition), AttributeDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.AttributeUsage), AttributeUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Behavior), BehaviorSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.BindingConnector), BindingConnectorSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.BindingConnectorAsUsage), BindingConnectorAsUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.BooleanExpression), BooleanExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.CalculationDefinition), CalculationDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.CalculationUsage), CalculationUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.CaseDefinition), CaseDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.CaseUsage), CaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Class), ClassSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Classifier), ClassifierSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.CollectExpression), CollectExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Comment), CommentSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ConcernDefinition), ConcernDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ConcernUsage), ConcernUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ConjugatedPortDefinition), ConjugatedPortDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ConjugatedPortTyping), ConjugatedPortTypingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Conjugation), ConjugationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ConnectionDefinition), ConnectionDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ConnectionUsage), ConnectionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Connector), ConnectorSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ConstraintDefinition), ConstraintDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ConstraintUsage), ConstraintUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.DataType), DataTypeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.DecisionNode), DecisionNodeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Definition), DefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Dependency), DependencySerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Differencing), DifferencingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Disjoining), DisjoiningSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Documentation), DocumentationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ElementFilterMembership), ElementFilterMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.EndFeatureMembership), EndFeatureMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.EnumerationDefinition), EnumerationDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.EnumerationUsage), EnumerationUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.EventOccurrenceUsage), EventOccurrenceUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ExhibitStateUsage), ExhibitStateUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Expression), ExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Feature), FeatureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FeatureChainExpression), FeatureChainExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FeatureChaining), FeatureChainingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FeatureInverting), FeatureInvertingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FeatureMembership), FeatureMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FeatureReferenceExpression), FeatureReferenceExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FeatureTyping), FeatureTypingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FeatureValue), FeatureValueSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FlowConnectionDefinition), FlowConnectionDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FlowConnectionUsage), FlowConnectionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ForkNode), ForkNodeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ForLoopActionUsage), ForLoopActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.FramedConcernMembership), FramedConcernMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Function), FunctionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.IfActionUsage), IfActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.IncludeUseCaseUsage), IncludeUseCaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Interaction), InteractionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.InterfaceDefinition), InterfaceDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.InterfaceUsage), InterfaceUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Intersecting), IntersectingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Invariant), InvariantSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.InvocationExpression), InvocationExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ItemDefinition), ItemDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ItemFeature), ItemFeatureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ItemFlow), ItemFlowSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ItemFlowEnd), ItemFlowEndSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ItemUsage), ItemUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.JoinNode), JoinNodeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.LibraryPackage), LibraryPackageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.LifeClass), LifeClassSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.LiteralBoolean), LiteralBooleanSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.LiteralExpression), LiteralExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.LiteralInfinity), LiteralInfinitySerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.LiteralInteger), LiteralIntegerSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.LiteralRational), LiteralRationalSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.LiteralString), LiteralStringSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Membership), MembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.MembershipExpose), MembershipExposeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.MembershipImport), MembershipImportSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.MergeNode), MergeNodeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Metaclass), MetaclassSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.MetadataAccessExpression), MetadataAccessExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.MetadataDefinition), MetadataDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.MetadataFeature), MetadataFeatureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.MetadataUsage), MetadataUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Multiplicity), MultiplicitySerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.MultiplicityRange), MultiplicityRangeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Namespace), NamespaceSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.NamespaceExpose), NamespaceExposeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.NamespaceImport), NamespaceImportSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.NullExpression), NullExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ObjectiveMembership), ObjectiveMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.OccurrenceDefinition), OccurrenceDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.OccurrenceUsage), OccurrenceUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.OperatorExpression), OperatorExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.OwningMembership), OwningMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Package), PackageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ParameterMembership), ParameterMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.PartDefinition), PartDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.PartUsage), PartUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.PerformActionUsage), PerformActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.PortConjugation), PortConjugationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.PortDefinition), PortDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.PortUsage), PortUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Predicate), PredicateSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Redefinition), RedefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ReferenceSubsetting), ReferenceSubsettingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ReferenceUsage), ReferenceUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.RenderingDefinition), RenderingDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.RenderingUsage), RenderingUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.RequirementConstraintMembership), RequirementConstraintMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.RequirementDefinition), RequirementDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.RequirementUsage), RequirementUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.RequirementVerificationMembership), RequirementVerificationMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ResultExpressionMembership), ResultExpressionMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ReturnParameterMembership), ReturnParameterMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.SatisfyRequirementUsage), SatisfyRequirementUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.SelectExpression), SelectExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.SendActionUsage), SendActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Specialization), SpecializationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.StakeholderMembership), StakeholderMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.StateDefinition), StateDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.StateSubactionMembership), StateSubactionMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.StateUsage), StateUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Step), StepSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Structure), StructureSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Subclassification), SubclassificationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.SubjectMembership), SubjectMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Subsetting), SubsettingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Succession), SuccessionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.SuccessionAsUsage), SuccessionAsUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.SuccessionFlowConnectionUsage), SuccessionFlowConnectionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.SuccessionItemFlow), SuccessionItemFlowSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.TerminateActionUsage), TerminateActionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.TextualRepresentation), TextualRepresentationSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.TransitionFeatureMembership), TransitionFeatureMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.TransitionUsage), TransitionUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.TriggerInvocationExpression), TriggerInvocationExpressionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Type), TypeSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.TypeFeaturing), TypeFeaturingSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Unioning), UnioningSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.Usage), UsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.UseCaseDefinition), UseCaseDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.UseCaseUsage), UseCaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.VariantMembership), VariantMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.VerificationCaseDefinition), VerificationCaseDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.VerificationCaseUsage), VerificationCaseUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ViewDefinition), ViewDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ViewpointDefinition), ViewpointDefinitionSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ViewpointUsage), ViewpointUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ViewRenderingMembership), ViewRenderingMembershipSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.ViewUsage), ViewUsageSerializer.Serialize },
            { typeof(SysML2.NET.Core.DTO.WhileLoopActionUsage), WhileLoopActionUsageSerializer.Serialize },
        };

        /// <summary>
        /// Provides the delegate <see cref="Action{object, Utf8JsonWriter, SerializationModeKind}"/> for the
        /// <see cref="System.Type"/> that is to be serialized
        /// </summary>
        /// <param name="type">
        /// The subject <see cref="System.Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// A Delegate of <see cref="Action{object, Utf8JsonWriter, SerializationModeKind}"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="System.Type"/> is not supported.
        /// </exception>
        internal static Action<object, Utf8JsonWriter, SerializationModeKind> Provide(System.Type type)
        {
            if (!SerializerActionMap.TryGetValue(type, out var action))
            {
                throw new NotSupportedException($"The {type.Name} is not supported by the SerializationProvider.");
            }

            return action;
        }

        /// <summary>
        /// Asserts whether the <paramref name="type"/> is supported by the provider
        /// </summary>
        /// <param name="typeName">
        /// The name of the subject <see cref="System.Type"/> for which support is asserted
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
