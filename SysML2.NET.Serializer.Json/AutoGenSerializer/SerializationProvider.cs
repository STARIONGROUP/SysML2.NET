// -------------------------------------------------------------------------------------------------
// <copyright file="SerializationProvider.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
        private static readonly Dictionary<System.Type, Action<object, Utf8JsonWriter, SerializationModeKind>> SerializerActionMap = new Dictionary<System.Type, Action<object, Utf8JsonWriter, SerializationModeKind>>
        {
            { typeof(Core.DTO.AcceptActionUsage), AcceptActionUsageSerializer.Serialize },
            { typeof(Core.DTO.ActionDefinition), ActionDefinitionSerializer.Serialize },
            { typeof(Core.DTO.ActionUsage), ActionUsageSerializer.Serialize },
            { typeof(Core.DTO.ActorMembership), ActorMembershipSerializer.Serialize },
            { typeof(Core.DTO.AllocationDefinition), AllocationDefinitionSerializer.Serialize },
            { typeof(Core.DTO.AllocationUsage), AllocationUsageSerializer.Serialize },
            { typeof(Core.DTO.AnalysisCaseDefinition), AnalysisCaseDefinitionSerializer.Serialize },
            { typeof(Core.DTO.AnalysisCaseUsage), AnalysisCaseUsageSerializer.Serialize },
            { typeof(Core.DTO.AnnotatingElement), AnnotatingElementSerializer.Serialize },
            { typeof(Core.DTO.Annotation), AnnotationSerializer.Serialize },
            { typeof(Core.DTO.AssertConstraintUsage), AssertConstraintUsageSerializer.Serialize },
            { typeof(Core.DTO.AssignmentActionUsage), AssignmentActionUsageSerializer.Serialize },
            { typeof(Core.DTO.Association), AssociationSerializer.Serialize },
            { typeof(Core.DTO.AssociationStructure), AssociationStructureSerializer.Serialize },
            { typeof(Core.DTO.AttributeDefinition), AttributeDefinitionSerializer.Serialize },
            { typeof(Core.DTO.AttributeUsage), AttributeUsageSerializer.Serialize },
            { typeof(Core.DTO.Behavior), BehaviorSerializer.Serialize },
            { typeof(Core.DTO.BindingConnector), BindingConnectorSerializer.Serialize },
            { typeof(Core.DTO.BindingConnectorAsUsage), BindingConnectorAsUsageSerializer.Serialize },
            { typeof(Core.DTO.BooleanExpression), BooleanExpressionSerializer.Serialize },
            { typeof(Core.DTO.CalculationDefinition), CalculationDefinitionSerializer.Serialize },
            { typeof(Core.DTO.CalculationUsage), CalculationUsageSerializer.Serialize },
            { typeof(Core.DTO.CaseDefinition), CaseDefinitionSerializer.Serialize },
            { typeof(Core.DTO.CaseUsage), CaseUsageSerializer.Serialize },
            { typeof(Core.DTO.Class), ClassSerializer.Serialize },
            { typeof(Core.DTO.Classifier), ClassifierSerializer.Serialize },
            { typeof(Core.DTO.CollectExpression), CollectExpressionSerializer.Serialize },
            { typeof(Core.DTO.Comment), CommentSerializer.Serialize },
            { typeof(Core.DTO.ConcernDefinition), ConcernDefinitionSerializer.Serialize },
            { typeof(Core.DTO.ConcernUsage), ConcernUsageSerializer.Serialize },
            { typeof(Core.DTO.ConjugatedPortDefinition), ConjugatedPortDefinitionSerializer.Serialize },
            { typeof(Core.DTO.ConjugatedPortTyping), ConjugatedPortTypingSerializer.Serialize },
            { typeof(Core.DTO.Conjugation), ConjugationSerializer.Serialize },
            { typeof(Core.DTO.ConnectionDefinition), ConnectionDefinitionSerializer.Serialize },
            { typeof(Core.DTO.ConnectionUsage), ConnectionUsageSerializer.Serialize },
            { typeof(Core.DTO.Connector), ConnectorSerializer.Serialize },
            { typeof(Core.DTO.ConstraintDefinition), ConstraintDefinitionSerializer.Serialize },
            { typeof(Core.DTO.ConstraintUsage), ConstraintUsageSerializer.Serialize },
            { typeof(Core.DTO.DataType), DataTypeSerializer.Serialize },
            { typeof(Core.DTO.DecisionNode), DecisionNodeSerializer.Serialize },
            { typeof(Core.DTO.Definition), DefinitionSerializer.Serialize },
            { typeof(Core.DTO.Dependency), DependencySerializer.Serialize },
            { typeof(Core.DTO.Differencing), DifferencingSerializer.Serialize },
            { typeof(Core.DTO.Disjoining), DisjoiningSerializer.Serialize },
            { typeof(Core.DTO.Documentation), DocumentationSerializer.Serialize },
            { typeof(Core.DTO.Element), ElementSerializer.Serialize },
            { typeof(Core.DTO.ElementFilterMembership), ElementFilterMembershipSerializer.Serialize },
            { typeof(Core.DTO.EndFeatureMembership), EndFeatureMembershipSerializer.Serialize },
            { typeof(Core.DTO.EnumerationDefinition), EnumerationDefinitionSerializer.Serialize },
            { typeof(Core.DTO.EnumerationUsage), EnumerationUsageSerializer.Serialize },
            { typeof(Core.DTO.EventOccurrenceUsage), EventOccurrenceUsageSerializer.Serialize },
            { typeof(Core.DTO.ExhibitStateUsage), ExhibitStateUsageSerializer.Serialize },
            { typeof(Core.DTO.Expose), ExposeSerializer.Serialize },
            { typeof(Core.DTO.Expression), ExpressionSerializer.Serialize },
            { typeof(Core.DTO.Feature), FeatureSerializer.Serialize },
            { typeof(Core.DTO.FeatureChainExpression), FeatureChainExpressionSerializer.Serialize },
            { typeof(Core.DTO.FeatureChaining), FeatureChainingSerializer.Serialize },
            { typeof(Core.DTO.FeatureInverting), FeatureInvertingSerializer.Serialize },
            { typeof(Core.DTO.FeatureMembership), FeatureMembershipSerializer.Serialize },
            { typeof(Core.DTO.FeatureReferenceExpression), FeatureReferenceExpressionSerializer.Serialize },
            { typeof(Core.DTO.FeatureTyping), FeatureTypingSerializer.Serialize },
            { typeof(Core.DTO.FeatureValue), FeatureValueSerializer.Serialize },
            { typeof(Core.DTO.FlowConnectionDefinition), FlowConnectionDefinitionSerializer.Serialize },
            { typeof(Core.DTO.FlowConnectionUsage), FlowConnectionUsageSerializer.Serialize },
            { typeof(Core.DTO.ForkNode), ForkNodeSerializer.Serialize },
            { typeof(Core.DTO.ForLoopActionUsage), ForLoopActionUsageSerializer.Serialize },
            { typeof(Core.DTO.FramedConcernMembership), FramedConcernMembershipSerializer.Serialize },
            { typeof(Core.DTO.Function), FunctionSerializer.Serialize },
            { typeof(Core.DTO.IfActionUsage), IfActionUsageSerializer.Serialize },
            { typeof(Core.DTO.Import), ImportSerializer.Serialize },
            { typeof(Core.DTO.IncludeUseCaseUsage), IncludeUseCaseUsageSerializer.Serialize },
            { typeof(Core.DTO.Interaction), InteractionSerializer.Serialize },
            { typeof(Core.DTO.InterfaceDefinition), InterfaceDefinitionSerializer.Serialize },
            { typeof(Core.DTO.InterfaceUsage), InterfaceUsageSerializer.Serialize },
            { typeof(Core.DTO.Intersecting), IntersectingSerializer.Serialize },
            { typeof(Core.DTO.Invariant), InvariantSerializer.Serialize },
            { typeof(Core.DTO.InvocationExpression), InvocationExpressionSerializer.Serialize },
            { typeof(Core.DTO.ItemDefinition), ItemDefinitionSerializer.Serialize },
            { typeof(Core.DTO.ItemFeature), ItemFeatureSerializer.Serialize },
            { typeof(Core.DTO.ItemFlow), ItemFlowSerializer.Serialize },
            { typeof(Core.DTO.ItemFlowEnd), ItemFlowEndSerializer.Serialize },
            { typeof(Core.DTO.ItemFlowFeature), ItemFlowFeatureSerializer.Serialize },
            { typeof(Core.DTO.ItemUsage), ItemUsageSerializer.Serialize },
            { typeof(Core.DTO.JoinNode), JoinNodeSerializer.Serialize },
            { typeof(Core.DTO.LibraryPackage), LibraryPackageSerializer.Serialize },
            { typeof(Core.DTO.LifeClass), LifeClassSerializer.Serialize },
            { typeof(Core.DTO.LiteralBoolean), LiteralBooleanSerializer.Serialize },
            { typeof(Core.DTO.LiteralExpression), LiteralExpressionSerializer.Serialize },
            { typeof(Core.DTO.LiteralInfinity), LiteralInfinitySerializer.Serialize },
            { typeof(Core.DTO.LiteralInteger), LiteralIntegerSerializer.Serialize },
            { typeof(Core.DTO.LiteralRational), LiteralRationalSerializer.Serialize },
            { typeof(Core.DTO.LiteralString), LiteralStringSerializer.Serialize },
            { typeof(Core.DTO.Membership), MembershipSerializer.Serialize },
            { typeof(Core.DTO.MergeNode), MergeNodeSerializer.Serialize },
            { typeof(Core.DTO.Metaclass), MetaclassSerializer.Serialize },
            { typeof(Core.DTO.MetadataAccessExpression), MetadataAccessExpressionSerializer.Serialize },
            { typeof(Core.DTO.MetadataDefinition), MetadataDefinitionSerializer.Serialize },
            { typeof(Core.DTO.MetadataFeature), MetadataFeatureSerializer.Serialize },
            { typeof(Core.DTO.MetadataUsage), MetadataUsageSerializer.Serialize },
            { typeof(Core.DTO.Multiplicity), MultiplicitySerializer.Serialize },
            { typeof(Core.DTO.MultiplicityRange), MultiplicityRangeSerializer.Serialize },
            { typeof(Core.DTO.Namespace), NamespaceSerializer.Serialize },
            { typeof(Core.DTO.NullExpression), NullExpressionSerializer.Serialize },
            { typeof(Core.DTO.ObjectiveMembership), ObjectiveMembershipSerializer.Serialize },
            { typeof(Core.DTO.OccurrenceDefinition), OccurrenceDefinitionSerializer.Serialize },
            { typeof(Core.DTO.OccurrenceUsage), OccurrenceUsageSerializer.Serialize },
            { typeof(Core.DTO.OperatorExpression), OperatorExpressionSerializer.Serialize },
            { typeof(Core.DTO.OwningMembership), OwningMembershipSerializer.Serialize },
            { typeof(Core.DTO.Package), PackageSerializer.Serialize },
            { typeof(Core.DTO.ParameterMembership), ParameterMembershipSerializer.Serialize },
            { typeof(Core.DTO.PartDefinition), PartDefinitionSerializer.Serialize },
            { typeof(Core.DTO.PartUsage), PartUsageSerializer.Serialize },
            { typeof(Core.DTO.PerformActionUsage), PerformActionUsageSerializer.Serialize },
            { typeof(Core.DTO.PortConjugation), PortConjugationSerializer.Serialize },
            { typeof(Core.DTO.PortDefinition), PortDefinitionSerializer.Serialize },
            { typeof(Core.DTO.PortioningFeature), PortioningFeatureSerializer.Serialize },
            { typeof(Core.DTO.PortUsage), PortUsageSerializer.Serialize },
            { typeof(Core.DTO.Predicate), PredicateSerializer.Serialize },
            { typeof(Core.DTO.Redefinition), RedefinitionSerializer.Serialize },
            { typeof(Core.DTO.ReferenceSubsetting), ReferenceSubsettingSerializer.Serialize },
            { typeof(Core.DTO.ReferenceUsage), ReferenceUsageSerializer.Serialize },
            { typeof(Core.DTO.Relationship), RelationshipSerializer.Serialize },
            { typeof(Core.DTO.RenderingDefinition), RenderingDefinitionSerializer.Serialize },
            { typeof(Core.DTO.RenderingUsage), RenderingUsageSerializer.Serialize },
            { typeof(Core.DTO.RequirementConstraintMembership), RequirementConstraintMembershipSerializer.Serialize },
            { typeof(Core.DTO.RequirementDefinition), RequirementDefinitionSerializer.Serialize },
            { typeof(Core.DTO.RequirementUsage), RequirementUsageSerializer.Serialize },
            { typeof(Core.DTO.RequirementVerificationMembership), RequirementVerificationMembershipSerializer.Serialize },
            { typeof(Core.DTO.ResultExpressionMembership), ResultExpressionMembershipSerializer.Serialize },
            { typeof(Core.DTO.ReturnParameterMembership), ReturnParameterMembershipSerializer.Serialize },
            { typeof(Core.DTO.SatisfyRequirementUsage), SatisfyRequirementUsageSerializer.Serialize },
            { typeof(Core.DTO.SelectExpression), SelectExpressionSerializer.Serialize },
            { typeof(Core.DTO.SendActionUsage), SendActionUsageSerializer.Serialize },
            { typeof(Core.DTO.SourceEnd), SourceEndSerializer.Serialize },
            { typeof(Core.DTO.Specialization), SpecializationSerializer.Serialize },
            { typeof(Core.DTO.StakeholderMembership), StakeholderMembershipSerializer.Serialize },
            { typeof(Core.DTO.StateDefinition), StateDefinitionSerializer.Serialize },
            { typeof(Core.DTO.StateSubactionMembership), StateSubactionMembershipSerializer.Serialize },
            { typeof(Core.DTO.StateUsage), StateUsageSerializer.Serialize },
            { typeof(Core.DTO.Step), StepSerializer.Serialize },
            { typeof(Core.DTO.Structure), StructureSerializer.Serialize },
            { typeof(Core.DTO.Subclassification), SubclassificationSerializer.Serialize },
            { typeof(Core.DTO.SubjectMembership), SubjectMembershipSerializer.Serialize },
            { typeof(Core.DTO.Subsetting), SubsettingSerializer.Serialize },
            { typeof(Core.DTO.Succession), SuccessionSerializer.Serialize },
            { typeof(Core.DTO.SuccessionAsUsage), SuccessionAsUsageSerializer.Serialize },
            { typeof(Core.DTO.SuccessionFlowConnectionUsage), SuccessionFlowConnectionUsageSerializer.Serialize },
            { typeof(Core.DTO.SuccessionItemFlow), SuccessionItemFlowSerializer.Serialize },
            { typeof(Core.DTO.TargetEnd), TargetEndSerializer.Serialize },
            { typeof(Core.DTO.TextualRepresentation), TextualRepresentationSerializer.Serialize },
            { typeof(Core.DTO.TransitionFeatureMembership), TransitionFeatureMembershipSerializer.Serialize },
            { typeof(Core.DTO.TransitionUsage), TransitionUsageSerializer.Serialize },
            { typeof(Core.DTO.TriggerInvocationExpression), TriggerInvocationExpressionSerializer.Serialize },
            { typeof(Core.DTO.Type), TypeSerializer.Serialize },
            { typeof(Core.DTO.TypeFeaturing), TypeFeaturingSerializer.Serialize },
            { typeof(Core.DTO.Unioning), UnioningSerializer.Serialize },
            { typeof(Core.DTO.Usage), UsageSerializer.Serialize },
            { typeof(Core.DTO.UseCaseDefinition), UseCaseDefinitionSerializer.Serialize },
            { typeof(Core.DTO.UseCaseUsage), UseCaseUsageSerializer.Serialize },
            { typeof(Core.DTO.VariantMembership), VariantMembershipSerializer.Serialize },
            { typeof(Core.DTO.VerificationCaseDefinition), VerificationCaseDefinitionSerializer.Serialize },
            { typeof(Core.DTO.VerificationCaseUsage), VerificationCaseUsageSerializer.Serialize },
            { typeof(Core.DTO.ViewDefinition), ViewDefinitionSerializer.Serialize },
            { typeof(Core.DTO.ViewpointDefinition), ViewpointDefinitionSerializer.Serialize },
            { typeof(Core.DTO.ViewpointUsage), ViewpointUsageSerializer.Serialize },
            { typeof(Core.DTO.ViewRenderingMembership), ViewRenderingMembershipSerializer.Serialize },
            { typeof(Core.DTO.ViewUsage), ViewUsageSerializer.Serialize },
            { typeof(Core.DTO.WhileLoopActionUsage), WhileLoopActionUsageSerializer.Serialize },
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
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
