// -------------------------------------------------------------------------------------------------
// <copyright file="DictionaryReaderProvider.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Dictionary.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// Delegate provider for the appropriate Read method to convert a Dictionary to
    /// an instance of <see cref="IData"/>
    /// </summary>
    internal static class DictionaryReaderProvider
    {
        private static readonly Dictionary<string, Func<Dictionary<string, object>, DictionaryKind, IData>> DictionaryReaderFuncMap = new Dictionary<string, Func<Dictionary<string, object>, DictionaryKind, IData>>
        {
            { "AcceptActionUsage", AcceptActionUsageDictionaryReader.Read },
            { "ActionDefinition", ActionDefinitionDictionaryReader.Read },
            { "ActionUsage", ActionUsageDictionaryReader.Read },
            { "ActorMembership", ActorMembershipDictionaryReader.Read },
            { "AllocationDefinition", AllocationDefinitionDictionaryReader.Read },
            { "AllocationUsage", AllocationUsageDictionaryReader.Read },
            { "AnalysisCaseDefinition", AnalysisCaseDefinitionDictionaryReader.Read },
            { "AnalysisCaseUsage", AnalysisCaseUsageDictionaryReader.Read },
            { "AnnotatingElement", AnnotatingElementDictionaryReader.Read },
            { "Annotation", AnnotationDictionaryReader.Read },
            { "AssertConstraintUsage", AssertConstraintUsageDictionaryReader.Read },
            { "AssignmentActionUsage", AssignmentActionUsageDictionaryReader.Read },
            { "Association", AssociationDictionaryReader.Read },
            { "AssociationStructure", AssociationStructureDictionaryReader.Read },
            { "AttributeDefinition", AttributeDefinitionDictionaryReader.Read },
            { "AttributeUsage", AttributeUsageDictionaryReader.Read },
            { "Behavior", BehaviorDictionaryReader.Read },
            { "BindingConnector", BindingConnectorDictionaryReader.Read },
            { "BindingConnectorAsUsage", BindingConnectorAsUsageDictionaryReader.Read },
            { "BooleanExpression", BooleanExpressionDictionaryReader.Read },
            { "CalculationDefinition", CalculationDefinitionDictionaryReader.Read },
            { "CalculationUsage", CalculationUsageDictionaryReader.Read },
            { "CaseDefinition", CaseDefinitionDictionaryReader.Read },
            { "CaseUsage", CaseUsageDictionaryReader.Read },
            { "Class", ClassDictionaryReader.Read },
            { "Classifier", ClassifierDictionaryReader.Read },
            { "CollectExpression", CollectExpressionDictionaryReader.Read },
            { "Comment", CommentDictionaryReader.Read },
            { "ConcernDefinition", ConcernDefinitionDictionaryReader.Read },
            { "ConcernUsage", ConcernUsageDictionaryReader.Read },
            { "ConjugatedPortDefinition", ConjugatedPortDefinitionDictionaryReader.Read },
            { "ConjugatedPortTyping", ConjugatedPortTypingDictionaryReader.Read },
            { "Conjugation", ConjugationDictionaryReader.Read },
            { "ConnectionDefinition", ConnectionDefinitionDictionaryReader.Read },
            { "ConnectionUsage", ConnectionUsageDictionaryReader.Read },
            { "Connector", ConnectorDictionaryReader.Read },
            { "ConstraintDefinition", ConstraintDefinitionDictionaryReader.Read },
            { "ConstraintUsage", ConstraintUsageDictionaryReader.Read },
            { "DataType", DataTypeDictionaryReader.Read },
            { "DecisionNode", DecisionNodeDictionaryReader.Read },
            { "Definition", DefinitionDictionaryReader.Read },
            { "Dependency", DependencyDictionaryReader.Read },
            { "Differencing", DifferencingDictionaryReader.Read },
            { "Disjoining", DisjoiningDictionaryReader.Read },
            { "Documentation", DocumentationDictionaryReader.Read },
            { "ElementFilterMembership", ElementFilterMembershipDictionaryReader.Read },
            { "EndFeatureMembership", EndFeatureMembershipDictionaryReader.Read },
            { "EnumerationDefinition", EnumerationDefinitionDictionaryReader.Read },
            { "EnumerationUsage", EnumerationUsageDictionaryReader.Read },
            { "EventOccurrenceUsage", EventOccurrenceUsageDictionaryReader.Read },
            { "ExhibitStateUsage", ExhibitStateUsageDictionaryReader.Read },
            { "Expression", ExpressionDictionaryReader.Read },
            { "Feature", FeatureDictionaryReader.Read },
            { "FeatureChainExpression", FeatureChainExpressionDictionaryReader.Read },
            { "FeatureChaining", FeatureChainingDictionaryReader.Read },
            { "FeatureInverting", FeatureInvertingDictionaryReader.Read },
            { "FeatureMembership", FeatureMembershipDictionaryReader.Read },
            { "FeatureReferenceExpression", FeatureReferenceExpressionDictionaryReader.Read },
            { "FeatureTyping", FeatureTypingDictionaryReader.Read },
            { "FeatureValue", FeatureValueDictionaryReader.Read },
            { "FlowConnectionDefinition", FlowConnectionDefinitionDictionaryReader.Read },
            { "FlowConnectionUsage", FlowConnectionUsageDictionaryReader.Read },
            { "ForkNode", ForkNodeDictionaryReader.Read },
            { "ForLoopActionUsage", ForLoopActionUsageDictionaryReader.Read },
            { "FramedConcernMembership", FramedConcernMembershipDictionaryReader.Read },
            { "Function", FunctionDictionaryReader.Read },
            { "IfActionUsage", IfActionUsageDictionaryReader.Read },
            { "IncludeUseCaseUsage", IncludeUseCaseUsageDictionaryReader.Read },
            { "Interaction", InteractionDictionaryReader.Read },
            { "InterfaceDefinition", InterfaceDefinitionDictionaryReader.Read },
            { "InterfaceUsage", InterfaceUsageDictionaryReader.Read },
            { "Intersecting", IntersectingDictionaryReader.Read },
            { "Invariant", InvariantDictionaryReader.Read },
            { "InvocationExpression", InvocationExpressionDictionaryReader.Read },
            { "ItemDefinition", ItemDefinitionDictionaryReader.Read },
            { "ItemFeature", ItemFeatureDictionaryReader.Read },
            { "ItemFlow", ItemFlowDictionaryReader.Read },
            { "ItemFlowEnd", ItemFlowEndDictionaryReader.Read },
            { "ItemUsage", ItemUsageDictionaryReader.Read },
            { "JoinNode", JoinNodeDictionaryReader.Read },
            { "LibraryPackage", LibraryPackageDictionaryReader.Read },
            { "LifeClass", LifeClassDictionaryReader.Read },
            { "LiteralBoolean", LiteralBooleanDictionaryReader.Read },
            { "LiteralExpression", LiteralExpressionDictionaryReader.Read },
            { "LiteralInfinity", LiteralInfinityDictionaryReader.Read },
            { "LiteralInteger", LiteralIntegerDictionaryReader.Read },
            { "LiteralRational", LiteralRationalDictionaryReader.Read },
            { "LiteralString", LiteralStringDictionaryReader.Read },
            { "Membership", MembershipDictionaryReader.Read },
            { "MembershipExpose", MembershipExposeDictionaryReader.Read },
            { "MembershipImport", MembershipImportDictionaryReader.Read },
            { "MergeNode", MergeNodeDictionaryReader.Read },
            { "Metaclass", MetaclassDictionaryReader.Read },
            { "MetadataAccessExpression", MetadataAccessExpressionDictionaryReader.Read },
            { "MetadataDefinition", MetadataDefinitionDictionaryReader.Read },
            { "MetadataFeature", MetadataFeatureDictionaryReader.Read },
            { "MetadataUsage", MetadataUsageDictionaryReader.Read },
            { "Multiplicity", MultiplicityDictionaryReader.Read },
            { "MultiplicityRange", MultiplicityRangeDictionaryReader.Read },
            { "Namespace", NamespaceDictionaryReader.Read },
            { "NamespaceExpose", NamespaceExposeDictionaryReader.Read },
            { "NamespaceImport", NamespaceImportDictionaryReader.Read },
            { "NullExpression", NullExpressionDictionaryReader.Read },
            { "ObjectiveMembership", ObjectiveMembershipDictionaryReader.Read },
            { "OccurrenceDefinition", OccurrenceDefinitionDictionaryReader.Read },
            { "OccurrenceUsage", OccurrenceUsageDictionaryReader.Read },
            { "OperatorExpression", OperatorExpressionDictionaryReader.Read },
            { "OwningMembership", OwningMembershipDictionaryReader.Read },
            { "Package", PackageDictionaryReader.Read },
            { "ParameterMembership", ParameterMembershipDictionaryReader.Read },
            { "PartDefinition", PartDefinitionDictionaryReader.Read },
            { "PartUsage", PartUsageDictionaryReader.Read },
            { "PerformActionUsage", PerformActionUsageDictionaryReader.Read },
            { "PortConjugation", PortConjugationDictionaryReader.Read },
            { "PortDefinition", PortDefinitionDictionaryReader.Read },
            { "PortUsage", PortUsageDictionaryReader.Read },
            { "Predicate", PredicateDictionaryReader.Read },
            { "Redefinition", RedefinitionDictionaryReader.Read },
            { "ReferenceSubsetting", ReferenceSubsettingDictionaryReader.Read },
            { "ReferenceUsage", ReferenceUsageDictionaryReader.Read },
            { "RenderingDefinition", RenderingDefinitionDictionaryReader.Read },
            { "RenderingUsage", RenderingUsageDictionaryReader.Read },
            { "RequirementConstraintMembership", RequirementConstraintMembershipDictionaryReader.Read },
            { "RequirementDefinition", RequirementDefinitionDictionaryReader.Read },
            { "RequirementUsage", RequirementUsageDictionaryReader.Read },
            { "RequirementVerificationMembership", RequirementVerificationMembershipDictionaryReader.Read },
            { "ResultExpressionMembership", ResultExpressionMembershipDictionaryReader.Read },
            { "ReturnParameterMembership", ReturnParameterMembershipDictionaryReader.Read },
            { "SatisfyRequirementUsage", SatisfyRequirementUsageDictionaryReader.Read },
            { "SelectExpression", SelectExpressionDictionaryReader.Read },
            { "SendActionUsage", SendActionUsageDictionaryReader.Read },
            { "Specialization", SpecializationDictionaryReader.Read },
            { "StakeholderMembership", StakeholderMembershipDictionaryReader.Read },
            { "StateDefinition", StateDefinitionDictionaryReader.Read },
            { "StateSubactionMembership", StateSubactionMembershipDictionaryReader.Read },
            { "StateUsage", StateUsageDictionaryReader.Read },
            { "Step", StepDictionaryReader.Read },
            { "Structure", StructureDictionaryReader.Read },
            { "Subclassification", SubclassificationDictionaryReader.Read },
            { "SubjectMembership", SubjectMembershipDictionaryReader.Read },
            { "Subsetting", SubsettingDictionaryReader.Read },
            { "Succession", SuccessionDictionaryReader.Read },
            { "SuccessionAsUsage", SuccessionAsUsageDictionaryReader.Read },
            { "SuccessionFlowConnectionUsage", SuccessionFlowConnectionUsageDictionaryReader.Read },
            { "SuccessionItemFlow", SuccessionItemFlowDictionaryReader.Read },
            { "TerminateActionUsage", TerminateActionUsageDictionaryReader.Read },
            { "TextualRepresentation", TextualRepresentationDictionaryReader.Read },
            { "TransitionFeatureMembership", TransitionFeatureMembershipDictionaryReader.Read },
            { "TransitionUsage", TransitionUsageDictionaryReader.Read },
            { "TriggerInvocationExpression", TriggerInvocationExpressionDictionaryReader.Read },
            { "Type", TypeDictionaryReader.Read },
            { "TypeFeaturing", TypeFeaturingDictionaryReader.Read },
            { "Unioning", UnioningDictionaryReader.Read },
            { "Usage", UsageDictionaryReader.Read },
            { "UseCaseDefinition", UseCaseDefinitionDictionaryReader.Read },
            { "UseCaseUsage", UseCaseUsageDictionaryReader.Read },
            { "VariantMembership", VariantMembershipDictionaryReader.Read },
            { "VerificationCaseDefinition", VerificationCaseDefinitionDictionaryReader.Read },
            { "VerificationCaseUsage", VerificationCaseUsageDictionaryReader.Read },
            { "ViewDefinition", ViewDefinitionDictionaryReader.Read },
            { "ViewpointDefinition", ViewpointDefinitionDictionaryReader.Read },
            { "ViewpointUsage", ViewpointUsageDictionaryReader.Read },
            { "ViewRenderingMembership", ViewRenderingMembershipDictionaryReader.Read },
            { "ViewUsage", ViewUsageDictionaryReader.Read },
            { "WhileLoopActionUsage", WhileLoopActionUsageDictionaryReader.Read },
        };

        /// <summary>
        /// Provides the delegate <see cref="Func{Dictionary{String, Object}, DictionaryKind, IData}"/> for the
        /// <see cref="System.Type"/> that is to be read
        /// </summary>
        /// <param name="typeName">
        /// The subject <see cref="System.Type"/> that is to be read
        /// </param>
        /// <returns>
        /// A Delegate of <see cref="Func{Dictionary{String, Object}, DictionaryKind, IData}"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="System.Type"/> is not supported.
        /// </exception>
        internal static Func<Dictionary<string, object>, DictionaryKind, IData> Provide(string typeName)
        {
            if (!DictionaryReaderFuncMap.TryGetValue(typeName, out var func))
            {
                throw new NotSupportedException($"The {typeName} is not supported by the DictionaryWriterProvider.");
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
            return DictionaryReaderFuncMap.ContainsKey(typeName);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
