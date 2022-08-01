// -------------------------------------------------------------------------------------------------
// <copyright file="SerializationProvider.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    /// <summary>
    /// Delegate provider for the appropriate serialization method to serialize a <see cref="Type" />
    /// </summary>
    internal static class SerializationProvider
    {
        private static readonly Dictionary<Type, Action<object, Utf8JsonWriter, SerializationModeKind>> SerializerActionMap = new Dictionary<Type, Action<object, Utf8JsonWriter, SerializationModeKind>>
        {
            { typeof(DTO.AcceptActionUsage), AcceptActionUsageSerializer.Serialize },
            { typeof(DTO.ActionDefinition), ActionDefinitionSerializer.Serialize },
            { typeof(DTO.ActionUsage), ActionUsageSerializer.Serialize },
            { typeof(DTO.ActorMembership), ActorMembershipSerializer.Serialize },
            { typeof(DTO.AllocationDefinition), AllocationDefinitionSerializer.Serialize },
            { typeof(DTO.AllocationUsage), AllocationUsageSerializer.Serialize },
            { typeof(DTO.AnalysisCaseDefinition), AnalysisCaseDefinitionSerializer.Serialize },
            { typeof(DTO.AnalysisCaseUsage), AnalysisCaseUsageSerializer.Serialize },
            { typeof(DTO.AnnotatingElement), AnnotatingElementSerializer.Serialize },
            { typeof(DTO.Annotation), AnnotationSerializer.Serialize },
            { typeof(DTO.AssertConstraintUsage), AssertConstraintUsageSerializer.Serialize },
            { typeof(DTO.AssignmentActionUsage), AssignmentActionUsageSerializer.Serialize },
            { typeof(DTO.Association), AssociationSerializer.Serialize },
            { typeof(DTO.AssociationStructure), AssociationStructureSerializer.Serialize },
            { typeof(DTO.AttributeDefinition), AttributeDefinitionSerializer.Serialize },
            { typeof(DTO.AttributeUsage), AttributeUsageSerializer.Serialize },
            { typeof(DTO.Behavior), BehaviorSerializer.Serialize },
            { typeof(DTO.BindingConnector), BindingConnectorSerializer.Serialize },
            { typeof(DTO.BindingConnectorAsUsage), BindingConnectorAsUsageSerializer.Serialize },
            { typeof(DTO.BooleanExpression), BooleanExpressionSerializer.Serialize },
            { typeof(DTO.CalculationDefinition), CalculationDefinitionSerializer.Serialize },
            { typeof(DTO.CalculationUsage), CalculationUsageSerializer.Serialize },
            { typeof(DTO.CaseDefinition), CaseDefinitionSerializer.Serialize },
            { typeof(DTO.CaseUsage), CaseUsageSerializer.Serialize },
            { typeof(DTO.Class), ClassSerializer.Serialize },
            { typeof(DTO.Classifier), ClassifierSerializer.Serialize },
            { typeof(DTO.CollectExpression), CollectExpressionSerializer.Serialize },
            { typeof(DTO.Comment), CommentSerializer.Serialize },
            { typeof(DTO.ConcernDefinition), ConcernDefinitionSerializer.Serialize },
            { typeof(DTO.ConcernUsage), ConcernUsageSerializer.Serialize },
            { typeof(DTO.ConjugatedPortDefinition), ConjugatedPortDefinitionSerializer.Serialize },
            { typeof(DTO.ConjugatedPortTyping), ConjugatedPortTypingSerializer.Serialize },
            { typeof(DTO.Conjugation), ConjugationSerializer.Serialize },
            { typeof(DTO.ConnectionDefinition), ConnectionDefinitionSerializer.Serialize },
            { typeof(DTO.ConnectionUsage), ConnectionUsageSerializer.Serialize },
            { typeof(DTO.Connector), ConnectorSerializer.Serialize },
            { typeof(DTO.ConstraintDefinition), ConstraintDefinitionSerializer.Serialize },
            { typeof(DTO.ConstraintUsage), ConstraintUsageSerializer.Serialize },
            { typeof(DTO.DataType), DataTypeSerializer.Serialize },
            { typeof(DTO.DecisionNode), DecisionNodeSerializer.Serialize },
            { typeof(DTO.Definition), DefinitionSerializer.Serialize },
            { typeof(DTO.Dependency), DependencySerializer.Serialize },
            { typeof(DTO.Disjoining), DisjoiningSerializer.Serialize },
            { typeof(DTO.Documentation), DocumentationSerializer.Serialize },
            { typeof(DTO.Element), ElementSerializer.Serialize },
            { typeof(DTO.ElementFilterMembership), ElementFilterMembershipSerializer.Serialize },
            { typeof(DTO.EndFeatureMembership), EndFeatureMembershipSerializer.Serialize },
            { typeof(DTO.EnumerationDefinition), EnumerationDefinitionSerializer.Serialize },
            { typeof(DTO.EnumerationUsage), EnumerationUsageSerializer.Serialize },
            { typeof(DTO.EventOccurrenceUsage), EventOccurrenceUsageSerializer.Serialize },
            { typeof(DTO.ExhibitStateUsage), ExhibitStateUsageSerializer.Serialize },
            { typeof(DTO.Expose), ExposeSerializer.Serialize },
            { typeof(DTO.Expression), ExpressionSerializer.Serialize },
            { typeof(DTO.Feature), FeatureSerializer.Serialize },
            { typeof(DTO.FeatureChainExpression), FeatureChainExpressionSerializer.Serialize },
            { typeof(DTO.FeatureChaining), FeatureChainingSerializer.Serialize },
            { typeof(DTO.FeatureInverting), FeatureInvertingSerializer.Serialize },
            { typeof(DTO.FeatureMembership), FeatureMembershipSerializer.Serialize },
            { typeof(DTO.FeatureReferenceExpression), FeatureReferenceExpressionSerializer.Serialize },
            { typeof(DTO.FeatureTyping), FeatureTypingSerializer.Serialize },
            { typeof(DTO.FeatureValue), FeatureValueSerializer.Serialize },
            { typeof(DTO.FlowConnectionUsage), FlowConnectionUsageSerializer.Serialize },
            { typeof(DTO.ForkNode), ForkNodeSerializer.Serialize },
            { typeof(DTO.ForLoopActionUsage), ForLoopActionUsageSerializer.Serialize },
            { typeof(DTO.FramedConcernMembership), FramedConcernMembershipSerializer.Serialize },
            { typeof(DTO.Function), FunctionSerializer.Serialize },
            { typeof(DTO.IfActionUsage), IfActionUsageSerializer.Serialize },
            { typeof(DTO.Import), ImportSerializer.Serialize },
            { typeof(DTO.IncludeUseCaseUsage), IncludeUseCaseUsageSerializer.Serialize },
            { typeof(DTO.Interaction), InteractionSerializer.Serialize },
            { typeof(DTO.InterfaceDefinition), InterfaceDefinitionSerializer.Serialize },
            { typeof(DTO.InterfaceUsage), InterfaceUsageSerializer.Serialize },
            { typeof(DTO.Invariant), InvariantSerializer.Serialize },
            { typeof(DTO.InvocationExpression), InvocationExpressionSerializer.Serialize },
            { typeof(DTO.ItemDefinition), ItemDefinitionSerializer.Serialize },
            { typeof(DTO.ItemFeature), ItemFeatureSerializer.Serialize },
            { typeof(DTO.ItemFlow), ItemFlowSerializer.Serialize },
            { typeof(DTO.ItemFlowEnd), ItemFlowEndSerializer.Serialize },
            { typeof(DTO.ItemFlowFeature), ItemFlowFeatureSerializer.Serialize },
            { typeof(DTO.ItemUsage), ItemUsageSerializer.Serialize },
            { typeof(DTO.JoinNode), JoinNodeSerializer.Serialize },
            { typeof(DTO.LifeClass), LifeClassSerializer.Serialize },
            { typeof(DTO.LiteralBoolean), LiteralBooleanSerializer.Serialize },
            { typeof(DTO.LiteralExpression), LiteralExpressionSerializer.Serialize },
            { typeof(DTO.LiteralInfinity), LiteralInfinitySerializer.Serialize },
            { typeof(DTO.LiteralInteger), LiteralIntegerSerializer.Serialize },
            { typeof(DTO.LiteralRational), LiteralRationalSerializer.Serialize },
            { typeof(DTO.LiteralString), LiteralStringSerializer.Serialize },
            { typeof(DTO.Membership), MembershipSerializer.Serialize },
            { typeof(DTO.MergeNode), MergeNodeSerializer.Serialize },
            { typeof(DTO.Metaclass), MetaclassSerializer.Serialize },
            { typeof(DTO.MetadataDefinition), MetadataDefinitionSerializer.Serialize },
            { typeof(DTO.MetadataFeature), MetadataFeatureSerializer.Serialize },
            { typeof(DTO.MetadataUsage), MetadataUsageSerializer.Serialize },
            { typeof(DTO.Multiplicity), MultiplicitySerializer.Serialize },
            { typeof(DTO.MultiplicityRange), MultiplicityRangeSerializer.Serialize },
            { typeof(DTO.Namespace), NamespaceSerializer.Serialize },
            { typeof(DTO.NullExpression), NullExpressionSerializer.Serialize },
            { typeof(DTO.ObjectiveMembership), ObjectiveMembershipSerializer.Serialize },
            { typeof(DTO.OccurrenceDefinition), OccurrenceDefinitionSerializer.Serialize },
            { typeof(DTO.OccurrenceUsage), OccurrenceUsageSerializer.Serialize },
            { typeof(DTO.OperatorExpression), OperatorExpressionSerializer.Serialize },
            { typeof(DTO.OwningMembership), OwningMembershipSerializer.Serialize },
            { typeof(DTO.Package), PackageSerializer.Serialize },
            { typeof(DTO.ParameterMembership), ParameterMembershipSerializer.Serialize },
            { typeof(DTO.PartDefinition), PartDefinitionSerializer.Serialize },
            { typeof(DTO.PartUsage), PartUsageSerializer.Serialize },
            { typeof(DTO.PerformActionUsage), PerformActionUsageSerializer.Serialize },
            { typeof(DTO.PortConjugation), PortConjugationSerializer.Serialize },
            { typeof(DTO.PortDefinition), PortDefinitionSerializer.Serialize },
            { typeof(DTO.PortioningFeature), PortioningFeatureSerializer.Serialize },
            { typeof(DTO.PortUsage), PortUsageSerializer.Serialize },
            { typeof(DTO.Predicate), PredicateSerializer.Serialize },
            { typeof(DTO.Redefinition), RedefinitionSerializer.Serialize },
            { typeof(DTO.ReferenceUsage), ReferenceUsageSerializer.Serialize },
            { typeof(DTO.Relationship), RelationshipSerializer.Serialize },
            { typeof(DTO.RenderingDefinition), RenderingDefinitionSerializer.Serialize },
            { typeof(DTO.RenderingUsage), RenderingUsageSerializer.Serialize },
            { typeof(DTO.RequirementConstraintMembership), RequirementConstraintMembershipSerializer.Serialize },
            { typeof(DTO.RequirementDefinition), RequirementDefinitionSerializer.Serialize },
            { typeof(DTO.RequirementUsage), RequirementUsageSerializer.Serialize },
            { typeof(DTO.RequirementVerificationMembership), RequirementVerificationMembershipSerializer.Serialize },
            { typeof(DTO.ResultExpressionMembership), ResultExpressionMembershipSerializer.Serialize },
            { typeof(DTO.ReturnParameterMembership), ReturnParameterMembershipSerializer.Serialize },
            { typeof(DTO.SatisfyRequirementUsage), SatisfyRequirementUsageSerializer.Serialize },
            { typeof(DTO.SelectExpression), SelectExpressionSerializer.Serialize },
            { typeof(DTO.SendActionUsage), SendActionUsageSerializer.Serialize },
            { typeof(DTO.SourceEnd), SourceEndSerializer.Serialize },
            { typeof(DTO.Specialization), SpecializationSerializer.Serialize },
            { typeof(DTO.StakeholderMembership), StakeholderMembershipSerializer.Serialize },
            { typeof(DTO.StateDefinition), StateDefinitionSerializer.Serialize },
            { typeof(DTO.StateSubactionMembership), StateSubactionMembershipSerializer.Serialize },
            { typeof(DTO.StateUsage), StateUsageSerializer.Serialize },
            { typeof(DTO.Step), StepSerializer.Serialize },
            { typeof(DTO.Structure), StructureSerializer.Serialize },
            { typeof(DTO.Subclassification), SubclassificationSerializer.Serialize },
            { typeof(DTO.SubjectMembership), SubjectMembershipSerializer.Serialize },
            { typeof(DTO.Subsetting), SubsettingSerializer.Serialize },
            { typeof(DTO.Succession), SuccessionSerializer.Serialize },
            { typeof(DTO.SuccessionAsUsage), SuccessionAsUsageSerializer.Serialize },
            { typeof(DTO.SuccessionFlowConnectionUsage), SuccessionFlowConnectionUsageSerializer.Serialize },
            { typeof(DTO.SuccessionItemFlow), SuccessionItemFlowSerializer.Serialize },
            { typeof(DTO.TargetEnd), TargetEndSerializer.Serialize },
            { typeof(DTO.TextualRepresentation), TextualRepresentationSerializer.Serialize },
            { typeof(DTO.TransitionFeatureMembership), TransitionFeatureMembershipSerializer.Serialize },
            { typeof(DTO.TransitionUsage), TransitionUsageSerializer.Serialize },
            { typeof(DTO.TriggerInvocationExpression), TriggerInvocationExpressionSerializer.Serialize },
            { typeof(DTO.Type), TypeSerializer.Serialize },
            { typeof(DTO.TypeFeaturing), TypeFeaturingSerializer.Serialize },
            { typeof(DTO.Usage), UsageSerializer.Serialize },
            { typeof(DTO.UseCaseDefinition), UseCaseDefinitionSerializer.Serialize },
            { typeof(DTO.UseCaseUsage), UseCaseUsageSerializer.Serialize },
            { typeof(DTO.VariantMembership), VariantMembershipSerializer.Serialize },
            { typeof(DTO.VerificationCaseDefinition), VerificationCaseDefinitionSerializer.Serialize },
            { typeof(DTO.VerificationCaseUsage), VerificationCaseUsageSerializer.Serialize },
            { typeof(DTO.ViewDefinition), ViewDefinitionSerializer.Serialize },
            { typeof(DTO.ViewpointDefinition), ViewpointDefinitionSerializer.Serialize },
            { typeof(DTO.ViewpointUsage), ViewpointUsageSerializer.Serialize },
            { typeof(DTO.ViewRenderingMembership), ViewRenderingMembershipSerializer.Serialize },
            { typeof(DTO.ViewUsage), ViewUsageSerializer.Serialize },
            { typeof(DTO.WhileLoopActionUsage), WhileLoopActionUsageSerializer.Serialize },
        };

        /// <summary>
        /// Provides the delegate <see cref="Action{object, Utf8JsonWriter, SerializationModeKind}"/> for the
        /// <see cref="Type"/> that is to be serialized
        /// </summary>
        /// <param name="type">
        /// The subject <see cref="Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// A Delegate of <see cref="Action{object, Utf8JsonWriter, SerializationModeKind}"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="Type"/> is not supported.
        /// </exception>
        internal static Action<object, Utf8JsonWriter, SerializationModeKind> Provide(Type type)
        {
            if (!SerializerActionMap.TryGetValue(type, out var action))
            {
                throw new NotSupportedException($"The {type.Name} is not supported by the SerializationProvider.");
            }

            return action;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
