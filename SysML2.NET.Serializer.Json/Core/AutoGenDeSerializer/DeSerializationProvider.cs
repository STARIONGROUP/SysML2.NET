// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializationProvider.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Delegate provider for the appropriate deserialization method to deserialize a <see cref="System.Type"/>
    /// </summary>
    internal static class DeSerializationProvider
    {
        /// <summary>
        /// a dictionary that provides delegates for deserialization
        /// </summary>
        private static readonly Dictionary<string, Func<JsonElement, SerializationModeKind, ILoggerFactory, IData>> DeSerializerActionMap = new Dictionary<string, Func<JsonElement, SerializationModeKind, ILoggerFactory, IData>>
        {
            { "AcceptActionUsage", AcceptActionUsageDeSerializer.DeSerialize },
            { "ActionDefinition", ActionDefinitionDeSerializer.DeSerialize },
            { "ActionUsage", ActionUsageDeSerializer.DeSerialize },
            { "ActorMembership", ActorMembershipDeSerializer.DeSerialize },
            { "AllocationDefinition", AllocationDefinitionDeSerializer.DeSerialize },
            { "AllocationUsage", AllocationUsageDeSerializer.DeSerialize },
            { "AnalysisCaseDefinition", AnalysisCaseDefinitionDeSerializer.DeSerialize },
            { "AnalysisCaseUsage", AnalysisCaseUsageDeSerializer.DeSerialize },
            { "AnnotatingElement", AnnotatingElementDeSerializer.DeSerialize },
            { "Annotation", AnnotationDeSerializer.DeSerialize },
            { "AssertConstraintUsage", AssertConstraintUsageDeSerializer.DeSerialize },
            { "AssignmentActionUsage", AssignmentActionUsageDeSerializer.DeSerialize },
            { "Association", AssociationDeSerializer.DeSerialize },
            { "AssociationStructure", AssociationStructureDeSerializer.DeSerialize },
            { "AttributeDefinition", AttributeDefinitionDeSerializer.DeSerialize },
            { "AttributeUsage", AttributeUsageDeSerializer.DeSerialize },
            { "Behavior", BehaviorDeSerializer.DeSerialize },
            { "BindingConnector", BindingConnectorDeSerializer.DeSerialize },
            { "BindingConnectorAsUsage", BindingConnectorAsUsageDeSerializer.DeSerialize },
            { "BooleanExpression", BooleanExpressionDeSerializer.DeSerialize },
            { "CalculationDefinition", CalculationDefinitionDeSerializer.DeSerialize },
            { "CalculationUsage", CalculationUsageDeSerializer.DeSerialize },
            { "CaseDefinition", CaseDefinitionDeSerializer.DeSerialize },
            { "CaseUsage", CaseUsageDeSerializer.DeSerialize },
            { "Class", ClassDeSerializer.DeSerialize },
            { "Classifier", ClassifierDeSerializer.DeSerialize },
            { "CollectExpression", CollectExpressionDeSerializer.DeSerialize },
            { "Comment", CommentDeSerializer.DeSerialize },
            { "ConcernDefinition", ConcernDefinitionDeSerializer.DeSerialize },
            { "ConcernUsage", ConcernUsageDeSerializer.DeSerialize },
            { "ConjugatedPortDefinition", ConjugatedPortDefinitionDeSerializer.DeSerialize },
            { "ConjugatedPortTyping", ConjugatedPortTypingDeSerializer.DeSerialize },
            { "Conjugation", ConjugationDeSerializer.DeSerialize },
            { "ConnectionDefinition", ConnectionDefinitionDeSerializer.DeSerialize },
            { "ConnectionUsage", ConnectionUsageDeSerializer.DeSerialize },
            { "Connector", ConnectorDeSerializer.DeSerialize },
            { "ConstraintDefinition", ConstraintDefinitionDeSerializer.DeSerialize },
            { "ConstraintUsage", ConstraintUsageDeSerializer.DeSerialize },
            { "DataType", DataTypeDeSerializer.DeSerialize },
            { "DecisionNode", DecisionNodeDeSerializer.DeSerialize },
            { "Definition", DefinitionDeSerializer.DeSerialize },
            { "Dependency", DependencyDeSerializer.DeSerialize },
            { "Differencing", DifferencingDeSerializer.DeSerialize },
            { "Disjoining", DisjoiningDeSerializer.DeSerialize },
            { "Documentation", DocumentationDeSerializer.DeSerialize },
            { "ElementFilterMembership", ElementFilterMembershipDeSerializer.DeSerialize },
            { "EndFeatureMembership", EndFeatureMembershipDeSerializer.DeSerialize },
            { "EnumerationDefinition", EnumerationDefinitionDeSerializer.DeSerialize },
            { "EnumerationUsage", EnumerationUsageDeSerializer.DeSerialize },
            { "EventOccurrenceUsage", EventOccurrenceUsageDeSerializer.DeSerialize },
            { "ExhibitStateUsage", ExhibitStateUsageDeSerializer.DeSerialize },
            { "Expression", ExpressionDeSerializer.DeSerialize },
            { "Feature", FeatureDeSerializer.DeSerialize },
            { "FeatureChainExpression", FeatureChainExpressionDeSerializer.DeSerialize },
            { "FeatureChaining", FeatureChainingDeSerializer.DeSerialize },
            { "FeatureInverting", FeatureInvertingDeSerializer.DeSerialize },
            { "FeatureMembership", FeatureMembershipDeSerializer.DeSerialize },
            { "FeatureReferenceExpression", FeatureReferenceExpressionDeSerializer.DeSerialize },
            { "FeatureTyping", FeatureTypingDeSerializer.DeSerialize },
            { "FeatureValue", FeatureValueDeSerializer.DeSerialize },
            { "FlowConnectionDefinition", FlowConnectionDefinitionDeSerializer.DeSerialize },
            { "FlowConnectionUsage", FlowConnectionUsageDeSerializer.DeSerialize },
            { "ForkNode", ForkNodeDeSerializer.DeSerialize },
            { "ForLoopActionUsage", ForLoopActionUsageDeSerializer.DeSerialize },
            { "FramedConcernMembership", FramedConcernMembershipDeSerializer.DeSerialize },
            { "Function", FunctionDeSerializer.DeSerialize },
            { "IfActionUsage", IfActionUsageDeSerializer.DeSerialize },
            { "IncludeUseCaseUsage", IncludeUseCaseUsageDeSerializer.DeSerialize },
            { "Interaction", InteractionDeSerializer.DeSerialize },
            { "InterfaceDefinition", InterfaceDefinitionDeSerializer.DeSerialize },
            { "InterfaceUsage", InterfaceUsageDeSerializer.DeSerialize },
            { "Intersecting", IntersectingDeSerializer.DeSerialize },
            { "Invariant", InvariantDeSerializer.DeSerialize },
            { "InvocationExpression", InvocationExpressionDeSerializer.DeSerialize },
            { "ItemDefinition", ItemDefinitionDeSerializer.DeSerialize },
            { "ItemFeature", ItemFeatureDeSerializer.DeSerialize },
            { "ItemFlow", ItemFlowDeSerializer.DeSerialize },
            { "ItemFlowEnd", ItemFlowEndDeSerializer.DeSerialize },
            { "ItemUsage", ItemUsageDeSerializer.DeSerialize },
            { "JoinNode", JoinNodeDeSerializer.DeSerialize },
            { "LibraryPackage", LibraryPackageDeSerializer.DeSerialize },
            { "LifeClass", LifeClassDeSerializer.DeSerialize },
            { "LiteralBoolean", LiteralBooleanDeSerializer.DeSerialize },
            { "LiteralExpression", LiteralExpressionDeSerializer.DeSerialize },
            { "LiteralInfinity", LiteralInfinityDeSerializer.DeSerialize },
            { "LiteralInteger", LiteralIntegerDeSerializer.DeSerialize },
            { "LiteralRational", LiteralRationalDeSerializer.DeSerialize },
            { "LiteralString", LiteralStringDeSerializer.DeSerialize },
            { "Membership", MembershipDeSerializer.DeSerialize },
            { "MembershipExpose", MembershipExposeDeSerializer.DeSerialize },
            { "MembershipImport", MembershipImportDeSerializer.DeSerialize },
            { "MergeNode", MergeNodeDeSerializer.DeSerialize },
            { "Metaclass", MetaclassDeSerializer.DeSerialize },
            { "MetadataAccessExpression", MetadataAccessExpressionDeSerializer.DeSerialize },
            { "MetadataDefinition", MetadataDefinitionDeSerializer.DeSerialize },
            { "MetadataFeature", MetadataFeatureDeSerializer.DeSerialize },
            { "MetadataUsage", MetadataUsageDeSerializer.DeSerialize },
            { "Multiplicity", MultiplicityDeSerializer.DeSerialize },
            { "MultiplicityRange", MultiplicityRangeDeSerializer.DeSerialize },
            { "Namespace", NamespaceDeSerializer.DeSerialize },
            { "NamespaceExpose", NamespaceExposeDeSerializer.DeSerialize },
            { "NamespaceImport", NamespaceImportDeSerializer.DeSerialize },
            { "NullExpression", NullExpressionDeSerializer.DeSerialize },
            { "ObjectiveMembership", ObjectiveMembershipDeSerializer.DeSerialize },
            { "OccurrenceDefinition", OccurrenceDefinitionDeSerializer.DeSerialize },
            { "OccurrenceUsage", OccurrenceUsageDeSerializer.DeSerialize },
            { "OperatorExpression", OperatorExpressionDeSerializer.DeSerialize },
            { "OwningMembership", OwningMembershipDeSerializer.DeSerialize },
            { "Package", PackageDeSerializer.DeSerialize },
            { "ParameterMembership", ParameterMembershipDeSerializer.DeSerialize },
            { "PartDefinition", PartDefinitionDeSerializer.DeSerialize },
            { "PartUsage", PartUsageDeSerializer.DeSerialize },
            { "PerformActionUsage", PerformActionUsageDeSerializer.DeSerialize },
            { "PortConjugation", PortConjugationDeSerializer.DeSerialize },
            { "PortDefinition", PortDefinitionDeSerializer.DeSerialize },
            { "PortUsage", PortUsageDeSerializer.DeSerialize },
            { "Predicate", PredicateDeSerializer.DeSerialize },
            { "Redefinition", RedefinitionDeSerializer.DeSerialize },
            { "ReferenceSubsetting", ReferenceSubsettingDeSerializer.DeSerialize },
            { "ReferenceUsage", ReferenceUsageDeSerializer.DeSerialize },
            { "RenderingDefinition", RenderingDefinitionDeSerializer.DeSerialize },
            { "RenderingUsage", RenderingUsageDeSerializer.DeSerialize },
            { "RequirementConstraintMembership", RequirementConstraintMembershipDeSerializer.DeSerialize },
            { "RequirementDefinition", RequirementDefinitionDeSerializer.DeSerialize },
            { "RequirementUsage", RequirementUsageDeSerializer.DeSerialize },
            { "RequirementVerificationMembership", RequirementVerificationMembershipDeSerializer.DeSerialize },
            { "ResultExpressionMembership", ResultExpressionMembershipDeSerializer.DeSerialize },
            { "ReturnParameterMembership", ReturnParameterMembershipDeSerializer.DeSerialize },
            { "SatisfyRequirementUsage", SatisfyRequirementUsageDeSerializer.DeSerialize },
            { "SelectExpression", SelectExpressionDeSerializer.DeSerialize },
            { "SendActionUsage", SendActionUsageDeSerializer.DeSerialize },
            { "Specialization", SpecializationDeSerializer.DeSerialize },
            { "StakeholderMembership", StakeholderMembershipDeSerializer.DeSerialize },
            { "StateDefinition", StateDefinitionDeSerializer.DeSerialize },
            { "StateSubactionMembership", StateSubactionMembershipDeSerializer.DeSerialize },
            { "StateUsage", StateUsageDeSerializer.DeSerialize },
            { "Step", StepDeSerializer.DeSerialize },
            { "Structure", StructureDeSerializer.DeSerialize },
            { "Subclassification", SubclassificationDeSerializer.DeSerialize },
            { "SubjectMembership", SubjectMembershipDeSerializer.DeSerialize },
            { "Subsetting", SubsettingDeSerializer.DeSerialize },
            { "Succession", SuccessionDeSerializer.DeSerialize },
            { "SuccessionAsUsage", SuccessionAsUsageDeSerializer.DeSerialize },
            { "SuccessionFlowConnectionUsage", SuccessionFlowConnectionUsageDeSerializer.DeSerialize },
            { "SuccessionItemFlow", SuccessionItemFlowDeSerializer.DeSerialize },
            { "TextualRepresentation", TextualRepresentationDeSerializer.DeSerialize },
            { "TransitionFeatureMembership", TransitionFeatureMembershipDeSerializer.DeSerialize },
            { "TransitionUsage", TransitionUsageDeSerializer.DeSerialize },
            { "TriggerInvocationExpression", TriggerInvocationExpressionDeSerializer.DeSerialize },
            { "Type", TypeDeSerializer.DeSerialize },
            { "TypeFeaturing", TypeFeaturingDeSerializer.DeSerialize },
            { "Unioning", UnioningDeSerializer.DeSerialize },
            { "Usage", UsageDeSerializer.DeSerialize },
            { "UseCaseDefinition", UseCaseDefinitionDeSerializer.DeSerialize },
            { "UseCaseUsage", UseCaseUsageDeSerializer.DeSerialize },
            { "VariantMembership", VariantMembershipDeSerializer.DeSerialize },
            { "VerificationCaseDefinition", VerificationCaseDefinitionDeSerializer.DeSerialize },
            { "VerificationCaseUsage", VerificationCaseUsageDeSerializer.DeSerialize },
            { "ViewDefinition", ViewDefinitionDeSerializer.DeSerialize },
            { "ViewpointDefinition", ViewpointDefinitionDeSerializer.DeSerialize },
            { "ViewpointUsage", ViewpointUsageDeSerializer.DeSerialize },
            { "ViewRenderingMembership", ViewRenderingMembershipDeSerializer.DeSerialize },
            { "ViewUsage", ViewUsageDeSerializer.DeSerialize },
            { "WhileLoopActionUsage", WhileLoopActionUsageDeSerializer.DeSerialize },
        };

        /// <summary>
        /// Provides the delegate <see cref="Func{JsonElement, SerializationModeKind, ILoggerFactory, IData}"/> for the
        /// <see cref="System.Type"/> that is to be deserialized
        /// </summary>
        /// <param name="typeName">
        /// The name of the subject <see cref="System.Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// A Delegate of <see cref="Func{JsonElement, SerializationModeKind, ILoggerFactory, IData}"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="System.Type"/> is not supported.
        /// </exception>
        internal static Func<JsonElement, SerializationModeKind, ILoggerFactory, IData> Provide(string typeName)
        {
            if (!DeSerializerActionMap.TryGetValue(typeName, out var func))
            {
                throw new NotSupportedException($"The {typeName} is not supported by the DeSerializationProvider.");
            }

            return func;
        }

        /// <summary>
        /// Asserts whether the <paramref name="typeName"/> is supported by the provider
        /// </summary>
        /// <param name="typeName">
        /// The name of the subject <see cref="System.Type"/> for which support is asserted
        /// </param>
        /// <returns></returns>
        internal static bool IsTypeSupported(string typeName)
        {
            return DeSerializerActionMap.ContainsKey(typeName);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
