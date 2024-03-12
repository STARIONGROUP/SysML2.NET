// -------------------------------------------------------------------------------------------------
// <copyright file="DictionaryWriterProvider.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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
    /// Delegate provider for the appropriate Write method to convert a <see cref="Type" /> to
    /// a Dictionary
    /// </summary>
    internal static class DictionaryWriterProvider
    {
        private static readonly Dictionary<System.Type, Func<IData, DictionaryKind, Dictionary<string, object>>> DictionaryWriterFuncMap = new Dictionary<System.Type, Func<IData, DictionaryKind, Dictionary<string, object>>>
        {
            { typeof(SysML2.NET.Core.DTO.AcceptActionUsage), AcceptActionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ActionDefinition), ActionDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ActionUsage), ActionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ActorMembership), ActorMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AllocationDefinition), AllocationDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AllocationUsage), AllocationUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AnalysisCaseDefinition), AnalysisCaseDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AnalysisCaseUsage), AnalysisCaseUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AnnotatingElement), AnnotatingElementDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Annotation), AnnotationDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AssertConstraintUsage), AssertConstraintUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AssignmentActionUsage), AssignmentActionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Association), AssociationDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AssociationStructure), AssociationStructureDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AttributeDefinition), AttributeDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.AttributeUsage), AttributeUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Behavior), BehaviorDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.BindingConnector), BindingConnectorDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.BindingConnectorAsUsage), BindingConnectorAsUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.BooleanExpression), BooleanExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.CalculationDefinition), CalculationDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.CalculationUsage), CalculationUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.CaseDefinition), CaseDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.CaseUsage), CaseUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Class), ClassDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Classifier), ClassifierDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.CollectExpression), CollectExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Comment), CommentDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ConcernDefinition), ConcernDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ConcernUsage), ConcernUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ConjugatedPortDefinition), ConjugatedPortDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ConjugatedPortTyping), ConjugatedPortTypingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Conjugation), ConjugationDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ConnectionDefinition), ConnectionDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ConnectionUsage), ConnectionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Connector), ConnectorDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ConstraintDefinition), ConstraintDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ConstraintUsage), ConstraintUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.DataType), DataTypeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.DecisionNode), DecisionNodeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Definition), DefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Dependency), DependencyDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Differencing), DifferencingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Disjoining), DisjoiningDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Documentation), DocumentationDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ElementFilterMembership), ElementFilterMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.EndFeatureMembership), EndFeatureMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.EnumerationDefinition), EnumerationDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.EnumerationUsage), EnumerationUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.EventOccurrenceUsage), EventOccurrenceUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ExhibitStateUsage), ExhibitStateUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Expression), ExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Feature), FeatureDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FeatureChainExpression), FeatureChainExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FeatureChaining), FeatureChainingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FeatureInverting), FeatureInvertingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FeatureMembership), FeatureMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FeatureReferenceExpression), FeatureReferenceExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FeatureTyping), FeatureTypingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FeatureValue), FeatureValueDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FlowConnectionDefinition), FlowConnectionDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FlowConnectionUsage), FlowConnectionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ForkNode), ForkNodeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ForLoopActionUsage), ForLoopActionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.FramedConcernMembership), FramedConcernMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Function), FunctionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.IfActionUsage), IfActionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.IncludeUseCaseUsage), IncludeUseCaseUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Interaction), InteractionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.InterfaceDefinition), InterfaceDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.InterfaceUsage), InterfaceUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Intersecting), IntersectingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Invariant), InvariantDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.InvocationExpression), InvocationExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ItemDefinition), ItemDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ItemFeature), ItemFeatureDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ItemFlow), ItemFlowDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ItemFlowEnd), ItemFlowEndDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ItemUsage), ItemUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.JoinNode), JoinNodeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.LibraryPackage), LibraryPackageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.LifeClass), LifeClassDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.LiteralBoolean), LiteralBooleanDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.LiteralExpression), LiteralExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.LiteralInfinity), LiteralInfinityDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.LiteralInteger), LiteralIntegerDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.LiteralRational), LiteralRationalDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.LiteralString), LiteralStringDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Membership), MembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.MembershipExpose), MembershipExposeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.MembershipImport), MembershipImportDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.MergeNode), MergeNodeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Metaclass), MetaclassDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.MetadataAccessExpression), MetadataAccessExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.MetadataDefinition), MetadataDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.MetadataFeature), MetadataFeatureDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.MetadataUsage), MetadataUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Multiplicity), MultiplicityDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.MultiplicityRange), MultiplicityRangeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Namespace), NamespaceDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.NamespaceExpose), NamespaceExposeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.NamespaceImport), NamespaceImportDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.NullExpression), NullExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ObjectiveMembership), ObjectiveMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.OccurrenceDefinition), OccurrenceDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.OccurrenceUsage), OccurrenceUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.OperatorExpression), OperatorExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.OwningMembership), OwningMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Package), PackageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ParameterMembership), ParameterMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.PartDefinition), PartDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.PartUsage), PartUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.PerformActionUsage), PerformActionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.PortConjugation), PortConjugationDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.PortDefinition), PortDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.PortUsage), PortUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Predicate), PredicateDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Redefinition), RedefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ReferenceSubsetting), ReferenceSubsettingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ReferenceUsage), ReferenceUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.RenderingDefinition), RenderingDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.RenderingUsage), RenderingUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.RequirementConstraintMembership), RequirementConstraintMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.RequirementDefinition), RequirementDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.RequirementUsage), RequirementUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.RequirementVerificationMembership), RequirementVerificationMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ResultExpressionMembership), ResultExpressionMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ReturnParameterMembership), ReturnParameterMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.SatisfyRequirementUsage), SatisfyRequirementUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.SelectExpression), SelectExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.SendActionUsage), SendActionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Specialization), SpecializationDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.StakeholderMembership), StakeholderMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.StateDefinition), StateDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.StateSubactionMembership), StateSubactionMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.StateUsage), StateUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Step), StepDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Structure), StructureDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Subclassification), SubclassificationDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.SubjectMembership), SubjectMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Subsetting), SubsettingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Succession), SuccessionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.SuccessionAsUsage), SuccessionAsUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.SuccessionFlowConnectionUsage), SuccessionFlowConnectionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.SuccessionItemFlow), SuccessionItemFlowDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.TextualRepresentation), TextualRepresentationDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.TransitionFeatureMembership), TransitionFeatureMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.TransitionUsage), TransitionUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.TriggerInvocationExpression), TriggerInvocationExpressionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Type), TypeDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.TypeFeaturing), TypeFeaturingDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Unioning), UnioningDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.Usage), UsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.UseCaseDefinition), UseCaseDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.UseCaseUsage), UseCaseUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.VariantMembership), VariantMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.VerificationCaseDefinition), VerificationCaseDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.VerificationCaseUsage), VerificationCaseUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ViewDefinition), ViewDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ViewpointDefinition), ViewpointDefinitionDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ViewpointUsage), ViewpointUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ViewRenderingMembership), ViewRenderingMembershipDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.ViewUsage), ViewUsageDictionaryWriter.Write },
            { typeof(SysML2.NET.Core.DTO.WhileLoopActionUsage), WhileLoopActionUsageDictionaryWriter.Write },
        };

        /// <summary>
        /// Provides the delegate <see cref="Func{IData, DictionaryKind, Dictionary{String, Object}}"/> for the
        /// <see cref="System.Type"/> that is to be serialized
        /// </summary>
        /// <param name="type">
        /// The subject <see cref="System.Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// A Delegate of <see cref="Func{IData, DictionaryKind, Dictionary{String, Object}}"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="System.Type"/> is not supported.
        /// </exception>
        internal static Func<IData, DictionaryKind, Dictionary<string, object>> Provide(System.Type type)
        {
            if (!DictionaryWriterFuncMap.TryGetValue(type, out var func))
            {
                throw new NotSupportedException($"The {type.Name} is not supported by the DictionaryWriterProvider.");
            }

            return func;
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
            return DictionaryWriterFuncMap.ContainsKey(type);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
