// -------------------------------------------------------------------------------------------------
// <copyright file="PayloadMessagePackFormatter.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2026 Starion Group S.A.
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

/* -------------------------------------------------------------------------- |
 | index | property name                                                      |
 | -------------------------------------------------------------------------- |
 | -------------------------------------------------------------------------- |
 |  0    | Created                                                            |
 | -------------------------------------------------------------------------- |
 | 1    | Systems.Actions.AcceptActionUsage
 | -------------------------------------------------------------------------- |
 | 2    | Systems.Actions.ActionDefinition
 | -------------------------------------------------------------------------- |
 | 3    | Systems.Actions.ActionUsage
 | -------------------------------------------------------------------------- |
 | 4    | Systems.Requirements.ActorMembership
 | -------------------------------------------------------------------------- |
 | 5    | Systems.Allocations.AllocationDefinition
 | -------------------------------------------------------------------------- |
 | 6    | Systems.Allocations.AllocationUsage
 | -------------------------------------------------------------------------- |
 | 7    | Systems.AnalysisCases.AnalysisCaseDefinition
 | -------------------------------------------------------------------------- |
 | 8    | Systems.AnalysisCases.AnalysisCaseUsage
 | -------------------------------------------------------------------------- |
 | 9    | Root.Annotations.AnnotatingElement
 | -------------------------------------------------------------------------- |
 | 10    | Root.Annotations.Annotation
 | -------------------------------------------------------------------------- |
 | 11    | Systems.Constraints.AssertConstraintUsage
 | -------------------------------------------------------------------------- |
 | 12    | Systems.Actions.AssignmentActionUsage
 | -------------------------------------------------------------------------- |
 | 13    | Kernel.Associations.Association
 | -------------------------------------------------------------------------- |
 | 14    | Kernel.Associations.AssociationStructure
 | -------------------------------------------------------------------------- |
 | 15    | Systems.Attributes.AttributeDefinition
 | -------------------------------------------------------------------------- |
 | 16    | Systems.Attributes.AttributeUsage
 | -------------------------------------------------------------------------- |
 | 17    | Kernel.Behaviors.Behavior
 | -------------------------------------------------------------------------- |
 | 18    | Kernel.Connectors.BindingConnector
 | -------------------------------------------------------------------------- |
 | 19    | Systems.Connections.BindingConnectorAsUsage
 | -------------------------------------------------------------------------- |
 | 20    | Kernel.Functions.BooleanExpression
 | -------------------------------------------------------------------------- |
 | 21    | Systems.Calculations.CalculationDefinition
 | -------------------------------------------------------------------------- |
 | 22    | Systems.Calculations.CalculationUsage
 | -------------------------------------------------------------------------- |
 | 23    | Systems.Cases.CaseDefinition
 | -------------------------------------------------------------------------- |
 | 24    | Systems.Cases.CaseUsage
 | -------------------------------------------------------------------------- |
 | 25    | Kernel.Classes.Class
 | -------------------------------------------------------------------------- |
 | 26    | Core.Classifiers.Classifier
 | -------------------------------------------------------------------------- |
 | 27    | Kernel.Expressions.CollectExpression
 | -------------------------------------------------------------------------- |
 | 28    | Root.Annotations.Comment
 | -------------------------------------------------------------------------- |
 | 29    | Systems.Requirements.ConcernDefinition
 | -------------------------------------------------------------------------- |
 | 30    | Systems.Requirements.ConcernUsage
 | -------------------------------------------------------------------------- |
 | 31    | Systems.Ports.ConjugatedPortDefinition
 | -------------------------------------------------------------------------- |
 | 32    | Systems.Ports.ConjugatedPortTyping
 | -------------------------------------------------------------------------- |
 | 33    | Core.Types.Conjugation
 | -------------------------------------------------------------------------- |
 | 34    | Systems.Connections.ConnectionDefinition
 | -------------------------------------------------------------------------- |
 | 35    | Systems.Connections.ConnectionUsage
 | -------------------------------------------------------------------------- |
 | 36    | Kernel.Connectors.Connector
 | -------------------------------------------------------------------------- |
 | 37    | Systems.Constraints.ConstraintDefinition
 | -------------------------------------------------------------------------- |
 | 38    | Systems.Constraints.ConstraintUsage
 | -------------------------------------------------------------------------- |
 | 39    | Kernel.Expressions.ConstructorExpression
 | -------------------------------------------------------------------------- |
 | 40    | Core.Features.CrossSubsetting
 | -------------------------------------------------------------------------- |
 | 41    | Kernel.DataTypes.DataType
 | -------------------------------------------------------------------------- |
 | 42    | Systems.Actions.DecisionNode
 | -------------------------------------------------------------------------- |
 | 43    | Systems.DefinitionAndUsage.Definition
 | -------------------------------------------------------------------------- |
 | 44    | Root.Dependencies.Dependency
 | -------------------------------------------------------------------------- |
 | 45    | Core.Types.Differencing
 | -------------------------------------------------------------------------- |
 | 46    | Core.Types.Disjoining
 | -------------------------------------------------------------------------- |
 | 47    | Root.Annotations.Documentation
 | -------------------------------------------------------------------------- |
 | 48    | Kernel.Packages.ElementFilterMembership
 | -------------------------------------------------------------------------- |
 | 49    | Core.Features.EndFeatureMembership
 | -------------------------------------------------------------------------- |
 | 50    | Systems.Enumerations.EnumerationDefinition
 | -------------------------------------------------------------------------- |
 | 51    | Systems.Enumerations.EnumerationUsage
 | -------------------------------------------------------------------------- |
 | 52    | Systems.Occurrences.EventOccurrenceUsage
 | -------------------------------------------------------------------------- |
 | 53    | Systems.States.ExhibitStateUsage
 | -------------------------------------------------------------------------- |
 | 54    | Kernel.Functions.Expression
 | -------------------------------------------------------------------------- |
 | 55    | Core.Features.Feature
 | -------------------------------------------------------------------------- |
 | 56    | Kernel.Expressions.FeatureChainExpression
 | -------------------------------------------------------------------------- |
 | 57    | Core.Features.FeatureChaining
 | -------------------------------------------------------------------------- |
 | 58    | Core.Features.FeatureInverting
 | -------------------------------------------------------------------------- |
 | 59    | Core.Types.FeatureMembership
 | -------------------------------------------------------------------------- |
 | 60    | Kernel.Expressions.FeatureReferenceExpression
 | -------------------------------------------------------------------------- |
 | 61    | Core.Features.FeatureTyping
 | -------------------------------------------------------------------------- |
 | 62    | Kernel.FeatureValues.FeatureValue
 | -------------------------------------------------------------------------- |
 | 63    | Kernel.Interactions.Flow
 | -------------------------------------------------------------------------- |
 | 64    | Systems.Flows.FlowDefinition
 | -------------------------------------------------------------------------- |
 | 65    | Kernel.Interactions.FlowEnd
 | -------------------------------------------------------------------------- |
 | 66    | Systems.Flows.FlowUsage
 | -------------------------------------------------------------------------- |
 | 67    | Systems.Actions.ForkNode
 | -------------------------------------------------------------------------- |
 | 68    | Systems.Actions.ForLoopActionUsage
 | -------------------------------------------------------------------------- |
 | 69    | Systems.Requirements.FramedConcernMembership
 | -------------------------------------------------------------------------- |
 | 70    | Kernel.Functions.Function
 | -------------------------------------------------------------------------- |
 | 71    | Systems.Actions.IfActionUsage
 | -------------------------------------------------------------------------- |
 | 72    | Systems.UseCases.IncludeUseCaseUsage
 | -------------------------------------------------------------------------- |
 | 73    | Kernel.Expressions.IndexExpression
 | -------------------------------------------------------------------------- |
 | 74    | Kernel.Interactions.Interaction
 | -------------------------------------------------------------------------- |
 | 75    | Systems.Interfaces.InterfaceDefinition
 | -------------------------------------------------------------------------- |
 | 76    | Systems.Interfaces.InterfaceUsage
 | -------------------------------------------------------------------------- |
 | 77    | Core.Types.Intersecting
 | -------------------------------------------------------------------------- |
 | 78    | Kernel.Functions.Invariant
 | -------------------------------------------------------------------------- |
 | 79    | Kernel.Expressions.InvocationExpression
 | -------------------------------------------------------------------------- |
 | 80    | Systems.Items.ItemDefinition
 | -------------------------------------------------------------------------- |
 | 81    | Systems.Items.ItemUsage
 | -------------------------------------------------------------------------- |
 | 82    | Systems.Actions.JoinNode
 | -------------------------------------------------------------------------- |
 | 83    | Kernel.Packages.LibraryPackage
 | -------------------------------------------------------------------------- |
 | 84    | Kernel.Expressions.LiteralBoolean
 | -------------------------------------------------------------------------- |
 | 85    | Kernel.Expressions.LiteralExpression
 | -------------------------------------------------------------------------- |
 | 86    | Kernel.Expressions.LiteralInfinity
 | -------------------------------------------------------------------------- |
 | 87    | Kernel.Expressions.LiteralInteger
 | -------------------------------------------------------------------------- |
 | 88    | Kernel.Expressions.LiteralRational
 | -------------------------------------------------------------------------- |
 | 89    | Kernel.Expressions.LiteralString
 | -------------------------------------------------------------------------- |
 | 90    | Root.Namespaces.Membership
 | -------------------------------------------------------------------------- |
 | 91    | Systems.Views.MembershipExpose
 | -------------------------------------------------------------------------- |
 | 92    | Root.Namespaces.MembershipImport
 | -------------------------------------------------------------------------- |
 | 93    | Systems.Actions.MergeNode
 | -------------------------------------------------------------------------- |
 | 94    | Kernel.Metadata.Metaclass
 | -------------------------------------------------------------------------- |
 | 95    | Kernel.Expressions.MetadataAccessExpression
 | -------------------------------------------------------------------------- |
 | 96    | Systems.Metadata.MetadataDefinition
 | -------------------------------------------------------------------------- |
 | 97    | Kernel.Metadata.MetadataFeature
 | -------------------------------------------------------------------------- |
 | 98    | Systems.Metadata.MetadataUsage
 | -------------------------------------------------------------------------- |
 | 99    | Core.Types.Multiplicity
 | -------------------------------------------------------------------------- |
 | 100    | Kernel.Multiplicities.MultiplicityRange
 | -------------------------------------------------------------------------- |
 | 101    | Root.Namespaces.Namespace
 | -------------------------------------------------------------------------- |
 | 102    | Systems.Views.NamespaceExpose
 | -------------------------------------------------------------------------- |
 | 103    | Root.Namespaces.NamespaceImport
 | -------------------------------------------------------------------------- |
 | 104    | Kernel.Expressions.NullExpression
 | -------------------------------------------------------------------------- |
 | 105    | Systems.Cases.ObjectiveMembership
 | -------------------------------------------------------------------------- |
 | 106    | Systems.Occurrences.OccurrenceDefinition
 | -------------------------------------------------------------------------- |
 | 107    | Systems.Occurrences.OccurrenceUsage
 | -------------------------------------------------------------------------- |
 | 108    | Kernel.Expressions.OperatorExpression
 | -------------------------------------------------------------------------- |
 | 109    | Root.Namespaces.OwningMembership
 | -------------------------------------------------------------------------- |
 | 110    | Kernel.Packages.Package
 | -------------------------------------------------------------------------- |
 | 111    | Kernel.Behaviors.ParameterMembership
 | -------------------------------------------------------------------------- |
 | 112    | Systems.Parts.PartDefinition
 | -------------------------------------------------------------------------- |
 | 113    | Systems.Parts.PartUsage
 | -------------------------------------------------------------------------- |
 | 114    | Kernel.Interactions.PayloadFeature
 | -------------------------------------------------------------------------- |
 | 115    | Systems.Actions.PerformActionUsage
 | -------------------------------------------------------------------------- |
 | 116    | Systems.Ports.PortConjugation
 | -------------------------------------------------------------------------- |
 | 117    | Systems.Ports.PortDefinition
 | -------------------------------------------------------------------------- |
 | 118    | Systems.Ports.PortUsage
 | -------------------------------------------------------------------------- |
 | 119    | Kernel.Functions.Predicate
 | -------------------------------------------------------------------------- |
 | 120    | Core.Features.Redefinition
 | -------------------------------------------------------------------------- |
 | 121    | Core.Features.ReferenceSubsetting
 | -------------------------------------------------------------------------- |
 | 122    | Systems.DefinitionAndUsage.ReferenceUsage
 | -------------------------------------------------------------------------- |
 | 123    | Systems.Views.RenderingDefinition
 | -------------------------------------------------------------------------- |
 | 124    | Systems.Views.RenderingUsage
 | -------------------------------------------------------------------------- |
 | 125    | Systems.Requirements.RequirementConstraintMembership
 | -------------------------------------------------------------------------- |
 | 126    | Systems.Requirements.RequirementDefinition
 | -------------------------------------------------------------------------- |
 | 127    | Systems.Requirements.RequirementUsage
 | -------------------------------------------------------------------------- |
 | 128    | Systems.VerificationCases.RequirementVerificationMembership
 | -------------------------------------------------------------------------- |
 | 129    | Kernel.Functions.ResultExpressionMembership
 | -------------------------------------------------------------------------- |
 | 130    | Kernel.Functions.ReturnParameterMembership
 | -------------------------------------------------------------------------- |
 | 131    | Systems.Requirements.SatisfyRequirementUsage
 | -------------------------------------------------------------------------- |
 | 132    | Kernel.Expressions.SelectExpression
 | -------------------------------------------------------------------------- |
 | 133    | Systems.Actions.SendActionUsage
 | -------------------------------------------------------------------------- |
 | 134    | Core.Types.Specialization
 | -------------------------------------------------------------------------- |
 | 135    | Systems.Requirements.StakeholderMembership
 | -------------------------------------------------------------------------- |
 | 136    | Systems.States.StateDefinition
 | -------------------------------------------------------------------------- |
 | 137    | Systems.States.StateSubactionMembership
 | -------------------------------------------------------------------------- |
 | 138    | Systems.States.StateUsage
 | -------------------------------------------------------------------------- |
 | 139    | Kernel.Behaviors.Step
 | -------------------------------------------------------------------------- |
 | 140    | Kernel.Structures.Structure
 | -------------------------------------------------------------------------- |
 | 141    | Core.Classifiers.Subclassification
 | -------------------------------------------------------------------------- |
 | 142    | Systems.Requirements.SubjectMembership
 | -------------------------------------------------------------------------- |
 | 143    | Core.Features.Subsetting
 | -------------------------------------------------------------------------- |
 | 144    | Kernel.Connectors.Succession
 | -------------------------------------------------------------------------- |
 | 145    | Systems.Connections.SuccessionAsUsage
 | -------------------------------------------------------------------------- |
 | 146    | Kernel.Interactions.SuccessionFlow
 | -------------------------------------------------------------------------- |
 | 147    | Systems.Flows.SuccessionFlowUsage
 | -------------------------------------------------------------------------- |
 | 148    | Systems.Actions.TerminateActionUsage
 | -------------------------------------------------------------------------- |
 | 149    | Root.Annotations.TextualRepresentation
 | -------------------------------------------------------------------------- |
 | 150    | Systems.States.TransitionFeatureMembership
 | -------------------------------------------------------------------------- |
 | 151    | Systems.States.TransitionUsage
 | -------------------------------------------------------------------------- |
 | 152    | Systems.Actions.TriggerInvocationExpression
 | -------------------------------------------------------------------------- |
 | 153    | Core.Types.Type
 | -------------------------------------------------------------------------- |
 | 154    | Core.Features.TypeFeaturing
 | -------------------------------------------------------------------------- |
 | 155    | Core.Types.Unioning
 | -------------------------------------------------------------------------- |
 | 156    | Systems.DefinitionAndUsage.Usage
 | -------------------------------------------------------------------------- |
 | 157    | Systems.UseCases.UseCaseDefinition
 | -------------------------------------------------------------------------- |
 | 158    | Systems.UseCases.UseCaseUsage
 | -------------------------------------------------------------------------- |
 | 159    | Systems.DefinitionAndUsage.VariantMembership
 | -------------------------------------------------------------------------- |
 | 160    | Systems.VerificationCases.VerificationCaseDefinition
 | -------------------------------------------------------------------------- |
 | 161    | Systems.VerificationCases.VerificationCaseUsage
 | -------------------------------------------------------------------------- |
 | 162    | Systems.Views.ViewDefinition
 | -------------------------------------------------------------------------- |
 | 163    | Systems.Views.ViewpointDefinition
 | -------------------------------------------------------------------------- |
 | 164    | Systems.Views.ViewpointUsage
 | -------------------------------------------------------------------------- |
 | 165    | Systems.Views.ViewRenderingMembership
 | -------------------------------------------------------------------------- |
 | 166    | Systems.Views.ViewUsage
 | -------------------------------------------------------------------------- |
 | 167    | Systems.Actions.WhileLoopActionUsage
 | -------------------------------------------------------------------------- |
 * -------------------------------------------------------------------------- | */

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;

    using global::MessagePack;
    using global::MessagePack.Formatters;

    /// <summary>
    /// The purpose of the <see cref="PayloadMessagePackFormatter"/> is to provide
    /// the contract for serialization of the <see cref="Payload"/> type
    /// </summary>
    internal class PayloadMessagePackFormatter : IMessagePackFormatter<Payload>
    {
        /// <summary>
        /// Serializes an <see cref="Payload"/>.
        /// </summary>
        /// <param name="writer">
        /// The writer to use when serializing the <see cref="Payload"/>.
        /// </param>
        /// <param name="payload">
        /// The <see cref="Payload"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        public void Serialize(ref MessagePackWriter writer, Payload payload, MessagePackSerializerOptions options)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload), "The Payload may not be null");
            }

            var formatterResolver = options.Resolver;

            writer.WriteArrayHeader(167);

            writer.Write(payload.Created);

            writer.WriteArrayHeader(payload.AcceptActionUsage.Count);
            foreach (var acceptActionUsageDto in payload.AcceptActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage>().Serialize(ref writer, acceptActionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.ActionDefinition.Count);
            foreach (var actionDefinitionDto in payload.ActionDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition>().Serialize(ref writer, actionDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ActionUsage.Count);
            foreach (var actionUsageDto in payload.ActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.ActionUsage>().Serialize(ref writer, actionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.ActorMembership.Count);
            foreach (var actorMembershipDto in payload.ActorMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership>().Serialize(ref writer, actorMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.AllocationDefinition.Count);
            foreach (var allocationDefinitionDto in payload.AllocationDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition>().Serialize(ref writer, allocationDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.AllocationUsage.Count);
            foreach (var allocationUsageDto in payload.AllocationUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage>().Serialize(ref writer, allocationUsageDto, options);
            }

            writer.WriteArrayHeader(payload.AnalysisCaseDefinition.Count);
            foreach (var analysisCaseDefinitionDto in payload.AnalysisCaseDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition>().Serialize(ref writer, analysisCaseDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.AnalysisCaseUsage.Count);
            foreach (var analysisCaseUsageDto in payload.AnalysisCaseUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage>().Serialize(ref writer, analysisCaseUsageDto, options);
            }

            writer.WriteArrayHeader(payload.AnnotatingElement.Count);
            foreach (var annotatingElementDto in payload.AnnotatingElement)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement>().Serialize(ref writer, annotatingElementDto, options);
            }

            writer.WriteArrayHeader(payload.Annotation.Count);
            foreach (var annotationDto in payload.Annotation)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.Annotation>().Serialize(ref writer, annotationDto, options);
            }

            writer.WriteArrayHeader(payload.AssertConstraintUsage.Count);
            foreach (var assertConstraintUsageDto in payload.AssertConstraintUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage>().Serialize(ref writer, assertConstraintUsageDto, options);
            }

            writer.WriteArrayHeader(payload.AssignmentActionUsage.Count);
            foreach (var assignmentActionUsageDto in payload.AssignmentActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage>().Serialize(ref writer, assignmentActionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Association.Count);
            foreach (var associationDto in payload.Association)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Associations.Association>().Serialize(ref writer, associationDto, options);
            }

            writer.WriteArrayHeader(payload.AssociationStructure.Count);
            foreach (var associationStructureDto in payload.AssociationStructure)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure>().Serialize(ref writer, associationStructureDto, options);
            }

            writer.WriteArrayHeader(payload.AttributeDefinition.Count);
            foreach (var attributeDefinitionDto in payload.AttributeDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition>().Serialize(ref writer, attributeDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.AttributeUsage.Count);
            foreach (var attributeUsageDto in payload.AttributeUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage>().Serialize(ref writer, attributeUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Behavior.Count);
            foreach (var behaviorDto in payload.Behavior)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior>().Serialize(ref writer, behaviorDto, options);
            }

            writer.WriteArrayHeader(payload.BindingConnector.Count);
            foreach (var bindingConnectorDto in payload.BindingConnector)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector>().Serialize(ref writer, bindingConnectorDto, options);
            }

            writer.WriteArrayHeader(payload.BindingConnectorAsUsage.Count);
            foreach (var bindingConnectorAsUsageDto in payload.BindingConnectorAsUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage>().Serialize(ref writer, bindingConnectorAsUsageDto, options);
            }

            writer.WriteArrayHeader(payload.BooleanExpression.Count);
            foreach (var booleanExpressionDto in payload.BooleanExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression>().Serialize(ref writer, booleanExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.CalculationDefinition.Count);
            foreach (var calculationDefinitionDto in payload.CalculationDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition>().Serialize(ref writer, calculationDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.CalculationUsage.Count);
            foreach (var calculationUsageDto in payload.CalculationUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage>().Serialize(ref writer, calculationUsageDto, options);
            }

            writer.WriteArrayHeader(payload.CaseDefinition.Count);
            foreach (var caseDefinitionDto in payload.CaseDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition>().Serialize(ref writer, caseDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.CaseUsage.Count);
            foreach (var caseUsageDto in payload.CaseUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Cases.CaseUsage>().Serialize(ref writer, caseUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Class.Count);
            foreach (var classDto in payload.Class)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Classes.Class>().Serialize(ref writer, classDto, options);
            }

            writer.WriteArrayHeader(payload.Classifier.Count);
            foreach (var classifierDto in payload.Classifier)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Classifiers.Classifier>().Serialize(ref writer, classifierDto, options);
            }

            writer.WriteArrayHeader(payload.CollectExpression.Count);
            foreach (var collectExpressionDto in payload.CollectExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression>().Serialize(ref writer, collectExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.Comment.Count);
            foreach (var commentDto in payload.Comment)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.Comment>().Serialize(ref writer, commentDto, options);
            }

            writer.WriteArrayHeader(payload.ConcernDefinition.Count);
            foreach (var concernDefinitionDto in payload.ConcernDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition>().Serialize(ref writer, concernDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ConcernUsage.Count);
            foreach (var concernUsageDto in payload.ConcernUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage>().Serialize(ref writer, concernUsageDto, options);
            }

            writer.WriteArrayHeader(payload.ConjugatedPortDefinition.Count);
            foreach (var conjugatedPortDefinitionDto in payload.ConjugatedPortDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition>().Serialize(ref writer, conjugatedPortDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ConjugatedPortTyping.Count);
            foreach (var conjugatedPortTypingDto in payload.ConjugatedPortTyping)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping>().Serialize(ref writer, conjugatedPortTypingDto, options);
            }

            writer.WriteArrayHeader(payload.Conjugation.Count);
            foreach (var conjugationDto in payload.Conjugation)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Conjugation>().Serialize(ref writer, conjugationDto, options);
            }

            writer.WriteArrayHeader(payload.ConnectionDefinition.Count);
            foreach (var connectionDefinitionDto in payload.ConnectionDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition>().Serialize(ref writer, connectionDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ConnectionUsage.Count);
            foreach (var connectionUsageDto in payload.ConnectionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage>().Serialize(ref writer, connectionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Connector.Count);
            foreach (var connectorDto in payload.Connector)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Connectors.Connector>().Serialize(ref writer, connectorDto, options);
            }

            writer.WriteArrayHeader(payload.ConstraintDefinition.Count);
            foreach (var constraintDefinitionDto in payload.ConstraintDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition>().Serialize(ref writer, constraintDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ConstraintUsage.Count);
            foreach (var constraintUsageDto in payload.ConstraintUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage>().Serialize(ref writer, constraintUsageDto, options);
            }

            writer.WriteArrayHeader(payload.ConstructorExpression.Count);
            foreach (var constructorExpressionDto in payload.ConstructorExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression>().Serialize(ref writer, constructorExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.CrossSubsetting.Count);
            foreach (var crossSubsettingDto in payload.CrossSubsetting)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.CrossSubsetting>().Serialize(ref writer, crossSubsettingDto, options);
            }

            writer.WriteArrayHeader(payload.DataType.Count);
            foreach (var dataTypeDto in payload.DataType)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.DataTypes.DataType>().Serialize(ref writer, dataTypeDto, options);
            }

            writer.WriteArrayHeader(payload.DecisionNode.Count);
            foreach (var decisionNodeDto in payload.DecisionNode)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.DecisionNode>().Serialize(ref writer, decisionNodeDto, options);
            }

            writer.WriteArrayHeader(payload.Definition.Count);
            foreach (var definitionDto in payload.Definition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition>().Serialize(ref writer, definitionDto, options);
            }

            writer.WriteArrayHeader(payload.Dependency.Count);
            foreach (var dependencyDto in payload.Dependency)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Dependencies.Dependency>().Serialize(ref writer, dependencyDto, options);
            }

            writer.WriteArrayHeader(payload.Differencing.Count);
            foreach (var differencingDto in payload.Differencing)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Differencing>().Serialize(ref writer, differencingDto, options);
            }

            writer.WriteArrayHeader(payload.Disjoining.Count);
            foreach (var disjoiningDto in payload.Disjoining)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Disjoining>().Serialize(ref writer, disjoiningDto, options);
            }

            writer.WriteArrayHeader(payload.Documentation.Count);
            foreach (var documentationDto in payload.Documentation)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.Documentation>().Serialize(ref writer, documentationDto, options);
            }

            writer.WriteArrayHeader(payload.ElementFilterMembership.Count);
            foreach (var elementFilterMembershipDto in payload.ElementFilterMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership>().Serialize(ref writer, elementFilterMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.EndFeatureMembership.Count);
            foreach (var endFeatureMembershipDto in payload.EndFeatureMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership>().Serialize(ref writer, endFeatureMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.EnumerationDefinition.Count);
            foreach (var enumerationDefinitionDto in payload.EnumerationDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition>().Serialize(ref writer, enumerationDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.EnumerationUsage.Count);
            foreach (var enumerationUsageDto in payload.EnumerationUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage>().Serialize(ref writer, enumerationUsageDto, options);
            }

            writer.WriteArrayHeader(payload.EventOccurrenceUsage.Count);
            foreach (var eventOccurrenceUsageDto in payload.EventOccurrenceUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage>().Serialize(ref writer, eventOccurrenceUsageDto, options);
            }

            writer.WriteArrayHeader(payload.ExhibitStateUsage.Count);
            foreach (var exhibitStateUsageDto in payload.ExhibitStateUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage>().Serialize(ref writer, exhibitStateUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Expression.Count);
            foreach (var expressionDto in payload.Expression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.Expression>().Serialize(ref writer, expressionDto, options);
            }

            writer.WriteArrayHeader(payload.Feature.Count);
            foreach (var featureDto in payload.Feature)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.Feature>().Serialize(ref writer, featureDto, options);
            }

            writer.WriteArrayHeader(payload.FeatureChainExpression.Count);
            foreach (var featureChainExpressionDto in payload.FeatureChainExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression>().Serialize(ref writer, featureChainExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.FeatureChaining.Count);
            foreach (var featureChainingDto in payload.FeatureChaining)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.FeatureChaining>().Serialize(ref writer, featureChainingDto, options);
            }

            writer.WriteArrayHeader(payload.FeatureInverting.Count);
            foreach (var featureInvertingDto in payload.FeatureInverting)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.FeatureInverting>().Serialize(ref writer, featureInvertingDto, options);
            }

            writer.WriteArrayHeader(payload.FeatureMembership.Count);
            foreach (var featureMembershipDto in payload.FeatureMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.FeatureMembership>().Serialize(ref writer, featureMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.FeatureReferenceExpression.Count);
            foreach (var featureReferenceExpressionDto in payload.FeatureReferenceExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression>().Serialize(ref writer, featureReferenceExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.FeatureTyping.Count);
            foreach (var featureTypingDto in payload.FeatureTyping)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.FeatureTyping>().Serialize(ref writer, featureTypingDto, options);
            }

            writer.WriteArrayHeader(payload.FeatureValue.Count);
            foreach (var featureValueDto in payload.FeatureValue)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue>().Serialize(ref writer, featureValueDto, options);
            }

            writer.WriteArrayHeader(payload.Flow.Count);
            foreach (var flowDto in payload.Flow)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.Flow>().Serialize(ref writer, flowDto, options);
            }

            writer.WriteArrayHeader(payload.FlowDefinition.Count);
            foreach (var flowDefinitionDto in payload.FlowDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition>().Serialize(ref writer, flowDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.FlowEnd.Count);
            foreach (var flowEndDto in payload.FlowEnd)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd>().Serialize(ref writer, flowEndDto, options);
            }

            writer.WriteArrayHeader(payload.FlowUsage.Count);
            foreach (var flowUsageDto in payload.FlowUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Flows.FlowUsage>().Serialize(ref writer, flowUsageDto, options);
            }

            writer.WriteArrayHeader(payload.ForkNode.Count);
            foreach (var forkNodeDto in payload.ForkNode)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.ForkNode>().Serialize(ref writer, forkNodeDto, options);
            }

            writer.WriteArrayHeader(payload.ForLoopActionUsage.Count);
            foreach (var forLoopActionUsageDto in payload.ForLoopActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage>().Serialize(ref writer, forLoopActionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.FramedConcernMembership.Count);
            foreach (var framedConcernMembershipDto in payload.FramedConcernMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership>().Serialize(ref writer, framedConcernMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.Function.Count);
            foreach (var functionDto in payload.Function)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.Function>().Serialize(ref writer, functionDto, options);
            }

            writer.WriteArrayHeader(payload.IfActionUsage.Count);
            foreach (var ifActionUsageDto in payload.IfActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage>().Serialize(ref writer, ifActionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.IncludeUseCaseUsage.Count);
            foreach (var includeUseCaseUsageDto in payload.IncludeUseCaseUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage>().Serialize(ref writer, includeUseCaseUsageDto, options);
            }

            writer.WriteArrayHeader(payload.IndexExpression.Count);
            foreach (var indexExpressionDto in payload.IndexExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression>().Serialize(ref writer, indexExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.Interaction.Count);
            foreach (var interactionDto in payload.Interaction)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.Interaction>().Serialize(ref writer, interactionDto, options);
            }

            writer.WriteArrayHeader(payload.InterfaceDefinition.Count);
            foreach (var interfaceDefinitionDto in payload.InterfaceDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition>().Serialize(ref writer, interfaceDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.InterfaceUsage.Count);
            foreach (var interfaceUsageDto in payload.InterfaceUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage>().Serialize(ref writer, interfaceUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Intersecting.Count);
            foreach (var intersectingDto in payload.Intersecting)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Intersecting>().Serialize(ref writer, intersectingDto, options);
            }

            writer.WriteArrayHeader(payload.Invariant.Count);
            foreach (var invariantDto in payload.Invariant)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.Invariant>().Serialize(ref writer, invariantDto, options);
            }

            writer.WriteArrayHeader(payload.InvocationExpression.Count);
            foreach (var invocationExpressionDto in payload.InvocationExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression>().Serialize(ref writer, invocationExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.ItemDefinition.Count);
            foreach (var itemDefinitionDto in payload.ItemDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Items.ItemDefinition>().Serialize(ref writer, itemDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ItemUsage.Count);
            foreach (var itemUsageDto in payload.ItemUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Items.ItemUsage>().Serialize(ref writer, itemUsageDto, options);
            }

            writer.WriteArrayHeader(payload.JoinNode.Count);
            foreach (var joinNodeDto in payload.JoinNode)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.JoinNode>().Serialize(ref writer, joinNodeDto, options);
            }

            writer.WriteArrayHeader(payload.LibraryPackage.Count);
            foreach (var libraryPackageDto in payload.LibraryPackage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage>().Serialize(ref writer, libraryPackageDto, options);
            }

            writer.WriteArrayHeader(payload.LiteralBoolean.Count);
            foreach (var literalBooleanDto in payload.LiteralBoolean)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean>().Serialize(ref writer, literalBooleanDto, options);
            }

            writer.WriteArrayHeader(payload.LiteralExpression.Count);
            foreach (var literalExpressionDto in payload.LiteralExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression>().Serialize(ref writer, literalExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.LiteralInfinity.Count);
            foreach (var literalInfinityDto in payload.LiteralInfinity)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity>().Serialize(ref writer, literalInfinityDto, options);
            }

            writer.WriteArrayHeader(payload.LiteralInteger.Count);
            foreach (var literalIntegerDto in payload.LiteralInteger)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger>().Serialize(ref writer, literalIntegerDto, options);
            }

            writer.WriteArrayHeader(payload.LiteralRational.Count);
            foreach (var literalRationalDto in payload.LiteralRational)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational>().Serialize(ref writer, literalRationalDto, options);
            }

            writer.WriteArrayHeader(payload.LiteralString.Count);
            foreach (var literalStringDto in payload.LiteralString)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString>().Serialize(ref writer, literalStringDto, options);
            }

            writer.WriteArrayHeader(payload.Membership.Count);
            foreach (var membershipDto in payload.Membership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.Membership>().Serialize(ref writer, membershipDto, options);
            }

            writer.WriteArrayHeader(payload.MembershipExpose.Count);
            foreach (var membershipExposeDto in payload.MembershipExpose)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.MembershipExpose>().Serialize(ref writer, membershipExposeDto, options);
            }

            writer.WriteArrayHeader(payload.MembershipImport.Count);
            foreach (var membershipImportDto in payload.MembershipImport)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport>().Serialize(ref writer, membershipImportDto, options);
            }

            writer.WriteArrayHeader(payload.MergeNode.Count);
            foreach (var mergeNodeDto in payload.MergeNode)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.MergeNode>().Serialize(ref writer, mergeNodeDto, options);
            }

            writer.WriteArrayHeader(payload.Metaclass.Count);
            foreach (var metaclassDto in payload.Metaclass)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass>().Serialize(ref writer, metaclassDto, options);
            }

            writer.WriteArrayHeader(payload.MetadataAccessExpression.Count);
            foreach (var metadataAccessExpressionDto in payload.MetadataAccessExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression>().Serialize(ref writer, metadataAccessExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.MetadataDefinition.Count);
            foreach (var metadataDefinitionDto in payload.MetadataDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition>().Serialize(ref writer, metadataDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.MetadataFeature.Count);
            foreach (var metadataFeatureDto in payload.MetadataFeature)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature>().Serialize(ref writer, metadataFeatureDto, options);
            }

            writer.WriteArrayHeader(payload.MetadataUsage.Count);
            foreach (var metadataUsageDto in payload.MetadataUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage>().Serialize(ref writer, metadataUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Multiplicity.Count);
            foreach (var multiplicityDto in payload.Multiplicity)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Multiplicity>().Serialize(ref writer, multiplicityDto, options);
            }

            writer.WriteArrayHeader(payload.MultiplicityRange.Count);
            foreach (var multiplicityRangeDto in payload.MultiplicityRange)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange>().Serialize(ref writer, multiplicityRangeDto, options);
            }

            writer.WriteArrayHeader(payload.Namespace.Count);
            foreach (var namespaceDto in payload.Namespace)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.Namespace>().Serialize(ref writer, namespaceDto, options);
            }

            writer.WriteArrayHeader(payload.NamespaceExpose.Count);
            foreach (var namespaceExposeDto in payload.NamespaceExpose)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose>().Serialize(ref writer, namespaceExposeDto, options);
            }

            writer.WriteArrayHeader(payload.NamespaceImport.Count);
            foreach (var namespaceImportDto in payload.NamespaceImport)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport>().Serialize(ref writer, namespaceImportDto, options);
            }

            writer.WriteArrayHeader(payload.NullExpression.Count);
            foreach (var nullExpressionDto in payload.NullExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression>().Serialize(ref writer, nullExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.ObjectiveMembership.Count);
            foreach (var objectiveMembershipDto in payload.ObjectiveMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership>().Serialize(ref writer, objectiveMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.OccurrenceDefinition.Count);
            foreach (var occurrenceDefinitionDto in payload.OccurrenceDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition>().Serialize(ref writer, occurrenceDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.OccurrenceUsage.Count);
            foreach (var occurrenceUsageDto in payload.OccurrenceUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage>().Serialize(ref writer, occurrenceUsageDto, options);
            }

            writer.WriteArrayHeader(payload.OperatorExpression.Count);
            foreach (var operatorExpressionDto in payload.OperatorExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression>().Serialize(ref writer, operatorExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.OwningMembership.Count);
            foreach (var owningMembershipDto in payload.OwningMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership>().Serialize(ref writer, owningMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.Package.Count);
            foreach (var packageDto in payload.Package)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Packages.Package>().Serialize(ref writer, packageDto, options);
            }

            writer.WriteArrayHeader(payload.ParameterMembership.Count);
            foreach (var parameterMembershipDto in payload.ParameterMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership>().Serialize(ref writer, parameterMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.PartDefinition.Count);
            foreach (var partDefinitionDto in payload.PartDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Parts.PartDefinition>().Serialize(ref writer, partDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.PartUsage.Count);
            foreach (var partUsageDto in payload.PartUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Parts.PartUsage>().Serialize(ref writer, partUsageDto, options);
            }

            writer.WriteArrayHeader(payload.PayloadFeature.Count);
            foreach (var payloadFeatureDto in payload.PayloadFeature)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature>().Serialize(ref writer, payloadFeatureDto, options);
            }

            writer.WriteArrayHeader(payload.PerformActionUsage.Count);
            foreach (var performActionUsageDto in payload.PerformActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage>().Serialize(ref writer, performActionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.PortConjugation.Count);
            foreach (var portConjugationDto in payload.PortConjugation)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.PortConjugation>().Serialize(ref writer, portConjugationDto, options);
            }

            writer.WriteArrayHeader(payload.PortDefinition.Count);
            foreach (var portDefinitionDto in payload.PortDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.PortDefinition>().Serialize(ref writer, portDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.PortUsage.Count);
            foreach (var portUsageDto in payload.PortUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.PortUsage>().Serialize(ref writer, portUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Predicate.Count);
            foreach (var predicateDto in payload.Predicate)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.Predicate>().Serialize(ref writer, predicateDto, options);
            }

            writer.WriteArrayHeader(payload.Redefinition.Count);
            foreach (var redefinitionDto in payload.Redefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.Redefinition>().Serialize(ref writer, redefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ReferenceSubsetting.Count);
            foreach (var referenceSubsettingDto in payload.ReferenceSubsetting)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting>().Serialize(ref writer, referenceSubsettingDto, options);
            }

            writer.WriteArrayHeader(payload.ReferenceUsage.Count);
            foreach (var referenceUsageDto in payload.ReferenceUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage>().Serialize(ref writer, referenceUsageDto, options);
            }

            writer.WriteArrayHeader(payload.RenderingDefinition.Count);
            foreach (var renderingDefinitionDto in payload.RenderingDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition>().Serialize(ref writer, renderingDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.RenderingUsage.Count);
            foreach (var renderingUsageDto in payload.RenderingUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.RenderingUsage>().Serialize(ref writer, renderingUsageDto, options);
            }

            writer.WriteArrayHeader(payload.RequirementConstraintMembership.Count);
            foreach (var requirementConstraintMembershipDto in payload.RequirementConstraintMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership>().Serialize(ref writer, requirementConstraintMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.RequirementDefinition.Count);
            foreach (var requirementDefinitionDto in payload.RequirementDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition>().Serialize(ref writer, requirementDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.RequirementUsage.Count);
            foreach (var requirementUsageDto in payload.RequirementUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage>().Serialize(ref writer, requirementUsageDto, options);
            }

            writer.WriteArrayHeader(payload.RequirementVerificationMembership.Count);
            foreach (var requirementVerificationMembershipDto in payload.RequirementVerificationMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership>().Serialize(ref writer, requirementVerificationMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.ResultExpressionMembership.Count);
            foreach (var resultExpressionMembershipDto in payload.ResultExpressionMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership>().Serialize(ref writer, resultExpressionMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.ReturnParameterMembership.Count);
            foreach (var returnParameterMembershipDto in payload.ReturnParameterMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership>().Serialize(ref writer, returnParameterMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.SatisfyRequirementUsage.Count);
            foreach (var satisfyRequirementUsageDto in payload.SatisfyRequirementUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage>().Serialize(ref writer, satisfyRequirementUsageDto, options);
            }

            writer.WriteArrayHeader(payload.SelectExpression.Count);
            foreach (var selectExpressionDto in payload.SelectExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression>().Serialize(ref writer, selectExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.SendActionUsage.Count);
            foreach (var sendActionUsageDto in payload.SendActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage>().Serialize(ref writer, sendActionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Specialization.Count);
            foreach (var specializationDto in payload.Specialization)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Specialization>().Serialize(ref writer, specializationDto, options);
            }

            writer.WriteArrayHeader(payload.StakeholderMembership.Count);
            foreach (var stakeholderMembershipDto in payload.StakeholderMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership>().Serialize(ref writer, stakeholderMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.StateDefinition.Count);
            foreach (var stateDefinitionDto in payload.StateDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.StateDefinition>().Serialize(ref writer, stateDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.StateSubactionMembership.Count);
            foreach (var stateSubactionMembershipDto in payload.StateSubactionMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership>().Serialize(ref writer, stateSubactionMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.StateUsage.Count);
            foreach (var stateUsageDto in payload.StateUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.StateUsage>().Serialize(ref writer, stateUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Step.Count);
            foreach (var stepDto in payload.Step)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Behaviors.Step>().Serialize(ref writer, stepDto, options);
            }

            writer.WriteArrayHeader(payload.Structure.Count);
            foreach (var structureDto in payload.Structure)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Structures.Structure>().Serialize(ref writer, structureDto, options);
            }

            writer.WriteArrayHeader(payload.Subclassification.Count);
            foreach (var subclassificationDto in payload.Subclassification)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Classifiers.Subclassification>().Serialize(ref writer, subclassificationDto, options);
            }

            writer.WriteArrayHeader(payload.SubjectMembership.Count);
            foreach (var subjectMembershipDto in payload.SubjectMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership>().Serialize(ref writer, subjectMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.Subsetting.Count);
            foreach (var subsettingDto in payload.Subsetting)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.Subsetting>().Serialize(ref writer, subsettingDto, options);
            }

            writer.WriteArrayHeader(payload.Succession.Count);
            foreach (var successionDto in payload.Succession)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Connectors.Succession>().Serialize(ref writer, successionDto, options);
            }

            writer.WriteArrayHeader(payload.SuccessionAsUsage.Count);
            foreach (var successionAsUsageDto in payload.SuccessionAsUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage>().Serialize(ref writer, successionAsUsageDto, options);
            }

            writer.WriteArrayHeader(payload.SuccessionFlow.Count);
            foreach (var successionFlowDto in payload.SuccessionFlow)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow>().Serialize(ref writer, successionFlowDto, options);
            }

            writer.WriteArrayHeader(payload.SuccessionFlowUsage.Count);
            foreach (var successionFlowUsageDto in payload.SuccessionFlowUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage>().Serialize(ref writer, successionFlowUsageDto, options);
            }

            writer.WriteArrayHeader(payload.TerminateActionUsage.Count);
            foreach (var terminateActionUsageDto in payload.TerminateActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage>().Serialize(ref writer, terminateActionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.TextualRepresentation.Count);
            foreach (var textualRepresentationDto in payload.TextualRepresentation)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation>().Serialize(ref writer, textualRepresentationDto, options);
            }

            writer.WriteArrayHeader(payload.TransitionFeatureMembership.Count);
            foreach (var transitionFeatureMembershipDto in payload.TransitionFeatureMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership>().Serialize(ref writer, transitionFeatureMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.TransitionUsage.Count);
            foreach (var transitionUsageDto in payload.TransitionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.TransitionUsage>().Serialize(ref writer, transitionUsageDto, options);
            }

            writer.WriteArrayHeader(payload.TriggerInvocationExpression.Count);
            foreach (var triggerInvocationExpressionDto in payload.TriggerInvocationExpression)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression>().Serialize(ref writer, triggerInvocationExpressionDto, options);
            }

            writer.WriteArrayHeader(payload.Type.Count);
            foreach (var typeDto in payload.Type)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Type>().Serialize(ref writer, typeDto, options);
            }

            writer.WriteArrayHeader(payload.TypeFeaturing.Count);
            foreach (var typeFeaturingDto in payload.TypeFeaturing)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.TypeFeaturing>().Serialize(ref writer, typeFeaturingDto, options);
            }

            writer.WriteArrayHeader(payload.Unioning.Count);
            foreach (var unioningDto in payload.Unioning)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Unioning>().Serialize(ref writer, unioningDto, options);
            }

            writer.WriteArrayHeader(payload.Usage.Count);
            foreach (var usageDto in payload.Usage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage>().Serialize(ref writer, usageDto, options);
            }

            writer.WriteArrayHeader(payload.UseCaseDefinition.Count);
            foreach (var useCaseDefinitionDto in payload.UseCaseDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition>().Serialize(ref writer, useCaseDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.UseCaseUsage.Count);
            foreach (var useCaseUsageDto in payload.UseCaseUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage>().Serialize(ref writer, useCaseUsageDto, options);
            }

            writer.WriteArrayHeader(payload.VariantMembership.Count);
            foreach (var variantMembershipDto in payload.VariantMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership>().Serialize(ref writer, variantMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.VerificationCaseDefinition.Count);
            foreach (var verificationCaseDefinitionDto in payload.VerificationCaseDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition>().Serialize(ref writer, verificationCaseDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.VerificationCaseUsage.Count);
            foreach (var verificationCaseUsageDto in payload.VerificationCaseUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage>().Serialize(ref writer, verificationCaseUsageDto, options);
            }

            writer.WriteArrayHeader(payload.ViewDefinition.Count);
            foreach (var viewDefinitionDto in payload.ViewDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewDefinition>().Serialize(ref writer, viewDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ViewpointDefinition.Count);
            foreach (var viewpointDefinitionDto in payload.ViewpointDefinition)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition>().Serialize(ref writer, viewpointDefinitionDto, options);
            }

            writer.WriteArrayHeader(payload.ViewpointUsage.Count);
            foreach (var viewpointUsageDto in payload.ViewpointUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage>().Serialize(ref writer, viewpointUsageDto, options);
            }

            writer.WriteArrayHeader(payload.ViewRenderingMembership.Count);
            foreach (var viewRenderingMembershipDto in payload.ViewRenderingMembership)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership>().Serialize(ref writer, viewRenderingMembershipDto, options);
            }

            writer.WriteArrayHeader(payload.ViewUsage.Count);
            foreach (var viewUsageDto in payload.ViewUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewUsage>().Serialize(ref writer, viewUsageDto, options);
            }

            writer.WriteArrayHeader(payload.WhileLoopActionUsage.Count);
            foreach (var whileLoopActionUsageDto in payload.WhileLoopActionUsage)
            {
                formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage>().Serialize(ref writer, whileLoopActionUsageDto, options);
            }


            writer.Flush();
        }

        /// <summary>
        /// Deserializes an <see cref="Payload"/>.
        /// </summary>
        /// <param name="reader">
        /// The reader to deserialize from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized value.
        /// </returns>
        public Payload Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            var formatterResolver = options.Resolver;
            options.Security.DepthStep(ref reader);

            var payload = new Payload();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                int valueLength;
                int valueCounter;

                switch (i)
                {
                    case 0:
                        payload.Created = reader.ReadDateTime();
                        break;
                    case 1:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var acceptActionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage>().Deserialize(ref reader, options);
                            payload.AcceptActionUsage.Add(acceptActionUsageDto);
                        }

                        break;
                    case 2:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var actionDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition>().Deserialize(ref reader, options);
                            payload.ActionDefinition.Add(actionDefinitionDto);
                        }

                        break;
                    case 3:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var actionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.ActionUsage>().Deserialize(ref reader, options);
                            payload.ActionUsage.Add(actionUsageDto);
                        }

                        break;
                    case 4:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var actorMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership>().Deserialize(ref reader, options);
                            payload.ActorMembership.Add(actorMembershipDto);
                        }

                        break;
                    case 5:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var allocationDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition>().Deserialize(ref reader, options);
                            payload.AllocationDefinition.Add(allocationDefinitionDto);
                        }

                        break;
                    case 6:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var allocationUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage>().Deserialize(ref reader, options);
                            payload.AllocationUsage.Add(allocationUsageDto);
                        }

                        break;
                    case 7:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var analysisCaseDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition>().Deserialize(ref reader, options);
                            payload.AnalysisCaseDefinition.Add(analysisCaseDefinitionDto);
                        }

                        break;
                    case 8:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var analysisCaseUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage>().Deserialize(ref reader, options);
                            payload.AnalysisCaseUsage.Add(analysisCaseUsageDto);
                        }

                        break;
                    case 9:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var annotatingElementDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement>().Deserialize(ref reader, options);
                            payload.AnnotatingElement.Add(annotatingElementDto);
                        }

                        break;
                    case 10:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var annotationDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.Annotation>().Deserialize(ref reader, options);
                            payload.Annotation.Add(annotationDto);
                        }

                        break;
                    case 11:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var assertConstraintUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage>().Deserialize(ref reader, options);
                            payload.AssertConstraintUsage.Add(assertConstraintUsageDto);
                        }

                        break;
                    case 12:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var assignmentActionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage>().Deserialize(ref reader, options);
                            payload.AssignmentActionUsage.Add(assignmentActionUsageDto);
                        }

                        break;
                    case 13:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var associationDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Associations.Association>().Deserialize(ref reader, options);
                            payload.Association.Add(associationDto);
                        }

                        break;
                    case 14:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var associationStructureDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure>().Deserialize(ref reader, options);
                            payload.AssociationStructure.Add(associationStructureDto);
                        }

                        break;
                    case 15:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var attributeDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition>().Deserialize(ref reader, options);
                            payload.AttributeDefinition.Add(attributeDefinitionDto);
                        }

                        break;
                    case 16:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var attributeUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage>().Deserialize(ref reader, options);
                            payload.AttributeUsage.Add(attributeUsageDto);
                        }

                        break;
                    case 17:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var behaviorDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior>().Deserialize(ref reader, options);
                            payload.Behavior.Add(behaviorDto);
                        }

                        break;
                    case 18:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var bindingConnectorDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector>().Deserialize(ref reader, options);
                            payload.BindingConnector.Add(bindingConnectorDto);
                        }

                        break;
                    case 19:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var bindingConnectorAsUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage>().Deserialize(ref reader, options);
                            payload.BindingConnectorAsUsage.Add(bindingConnectorAsUsageDto);
                        }

                        break;
                    case 20:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var booleanExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression>().Deserialize(ref reader, options);
                            payload.BooleanExpression.Add(booleanExpressionDto);
                        }

                        break;
                    case 21:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var calculationDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition>().Deserialize(ref reader, options);
                            payload.CalculationDefinition.Add(calculationDefinitionDto);
                        }

                        break;
                    case 22:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var calculationUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage>().Deserialize(ref reader, options);
                            payload.CalculationUsage.Add(calculationUsageDto);
                        }

                        break;
                    case 23:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var caseDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition>().Deserialize(ref reader, options);
                            payload.CaseDefinition.Add(caseDefinitionDto);
                        }

                        break;
                    case 24:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var caseUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Cases.CaseUsage>().Deserialize(ref reader, options);
                            payload.CaseUsage.Add(caseUsageDto);
                        }

                        break;
                    case 25:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var classDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Classes.Class>().Deserialize(ref reader, options);
                            payload.Class.Add(classDto);
                        }

                        break;
                    case 26:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var classifierDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Classifiers.Classifier>().Deserialize(ref reader, options);
                            payload.Classifier.Add(classifierDto);
                        }

                        break;
                    case 27:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var collectExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression>().Deserialize(ref reader, options);
                            payload.CollectExpression.Add(collectExpressionDto);
                        }

                        break;
                    case 28:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var commentDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.Comment>().Deserialize(ref reader, options);
                            payload.Comment.Add(commentDto);
                        }

                        break;
                    case 29:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var concernDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition>().Deserialize(ref reader, options);
                            payload.ConcernDefinition.Add(concernDefinitionDto);
                        }

                        break;
                    case 30:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var concernUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage>().Deserialize(ref reader, options);
                            payload.ConcernUsage.Add(concernUsageDto);
                        }

                        break;
                    case 31:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var conjugatedPortDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition>().Deserialize(ref reader, options);
                            payload.ConjugatedPortDefinition.Add(conjugatedPortDefinitionDto);
                        }

                        break;
                    case 32:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var conjugatedPortTypingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping>().Deserialize(ref reader, options);
                            payload.ConjugatedPortTyping.Add(conjugatedPortTypingDto);
                        }

                        break;
                    case 33:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var conjugationDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Conjugation>().Deserialize(ref reader, options);
                            payload.Conjugation.Add(conjugationDto);
                        }

                        break;
                    case 34:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var connectionDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition>().Deserialize(ref reader, options);
                            payload.ConnectionDefinition.Add(connectionDefinitionDto);
                        }

                        break;
                    case 35:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var connectionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage>().Deserialize(ref reader, options);
                            payload.ConnectionUsage.Add(connectionUsageDto);
                        }

                        break;
                    case 36:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var connectorDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Connectors.Connector>().Deserialize(ref reader, options);
                            payload.Connector.Add(connectorDto);
                        }

                        break;
                    case 37:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var constraintDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition>().Deserialize(ref reader, options);
                            payload.ConstraintDefinition.Add(constraintDefinitionDto);
                        }

                        break;
                    case 38:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var constraintUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage>().Deserialize(ref reader, options);
                            payload.ConstraintUsage.Add(constraintUsageDto);
                        }

                        break;
                    case 39:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var constructorExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression>().Deserialize(ref reader, options);
                            payload.ConstructorExpression.Add(constructorExpressionDto);
                        }

                        break;
                    case 40:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var crossSubsettingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.CrossSubsetting>().Deserialize(ref reader, options);
                            payload.CrossSubsetting.Add(crossSubsettingDto);
                        }

                        break;
                    case 41:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var dataTypeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.DataTypes.DataType>().Deserialize(ref reader, options);
                            payload.DataType.Add(dataTypeDto);
                        }

                        break;
                    case 42:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var decisionNodeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.DecisionNode>().Deserialize(ref reader, options);
                            payload.DecisionNode.Add(decisionNodeDto);
                        }

                        break;
                    case 43:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var definitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition>().Deserialize(ref reader, options);
                            payload.Definition.Add(definitionDto);
                        }

                        break;
                    case 44:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var dependencyDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Dependencies.Dependency>().Deserialize(ref reader, options);
                            payload.Dependency.Add(dependencyDto);
                        }

                        break;
                    case 45:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var differencingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Differencing>().Deserialize(ref reader, options);
                            payload.Differencing.Add(differencingDto);
                        }

                        break;
                    case 46:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var disjoiningDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Disjoining>().Deserialize(ref reader, options);
                            payload.Disjoining.Add(disjoiningDto);
                        }

                        break;
                    case 47:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var documentationDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.Documentation>().Deserialize(ref reader, options);
                            payload.Documentation.Add(documentationDto);
                        }

                        break;
                    case 48:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var elementFilterMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership>().Deserialize(ref reader, options);
                            payload.ElementFilterMembership.Add(elementFilterMembershipDto);
                        }

                        break;
                    case 49:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var endFeatureMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership>().Deserialize(ref reader, options);
                            payload.EndFeatureMembership.Add(endFeatureMembershipDto);
                        }

                        break;
                    case 50:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var enumerationDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition>().Deserialize(ref reader, options);
                            payload.EnumerationDefinition.Add(enumerationDefinitionDto);
                        }

                        break;
                    case 51:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var enumerationUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage>().Deserialize(ref reader, options);
                            payload.EnumerationUsage.Add(enumerationUsageDto);
                        }

                        break;
                    case 52:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var eventOccurrenceUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage>().Deserialize(ref reader, options);
                            payload.EventOccurrenceUsage.Add(eventOccurrenceUsageDto);
                        }

                        break;
                    case 53:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var exhibitStateUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage>().Deserialize(ref reader, options);
                            payload.ExhibitStateUsage.Add(exhibitStateUsageDto);
                        }

                        break;
                    case 54:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var expressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.Expression>().Deserialize(ref reader, options);
                            payload.Expression.Add(expressionDto);
                        }

                        break;
                    case 55:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var featureDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.Feature>().Deserialize(ref reader, options);
                            payload.Feature.Add(featureDto);
                        }

                        break;
                    case 56:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var featureChainExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression>().Deserialize(ref reader, options);
                            payload.FeatureChainExpression.Add(featureChainExpressionDto);
                        }

                        break;
                    case 57:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var featureChainingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.FeatureChaining>().Deserialize(ref reader, options);
                            payload.FeatureChaining.Add(featureChainingDto);
                        }

                        break;
                    case 58:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var featureInvertingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.FeatureInverting>().Deserialize(ref reader, options);
                            payload.FeatureInverting.Add(featureInvertingDto);
                        }

                        break;
                    case 59:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var featureMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.FeatureMembership>().Deserialize(ref reader, options);
                            payload.FeatureMembership.Add(featureMembershipDto);
                        }

                        break;
                    case 60:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var featureReferenceExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression>().Deserialize(ref reader, options);
                            payload.FeatureReferenceExpression.Add(featureReferenceExpressionDto);
                        }

                        break;
                    case 61:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var featureTypingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.FeatureTyping>().Deserialize(ref reader, options);
                            payload.FeatureTyping.Add(featureTypingDto);
                        }

                        break;
                    case 62:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var featureValueDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue>().Deserialize(ref reader, options);
                            payload.FeatureValue.Add(featureValueDto);
                        }

                        break;
                    case 63:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var flowDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.Flow>().Deserialize(ref reader, options);
                            payload.Flow.Add(flowDto);
                        }

                        break;
                    case 64:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var flowDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition>().Deserialize(ref reader, options);
                            payload.FlowDefinition.Add(flowDefinitionDto);
                        }

                        break;
                    case 65:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var flowEndDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd>().Deserialize(ref reader, options);
                            payload.FlowEnd.Add(flowEndDto);
                        }

                        break;
                    case 66:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var flowUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Flows.FlowUsage>().Deserialize(ref reader, options);
                            payload.FlowUsage.Add(flowUsageDto);
                        }

                        break;
                    case 67:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var forkNodeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.ForkNode>().Deserialize(ref reader, options);
                            payload.ForkNode.Add(forkNodeDto);
                        }

                        break;
                    case 68:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var forLoopActionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage>().Deserialize(ref reader, options);
                            payload.ForLoopActionUsage.Add(forLoopActionUsageDto);
                        }

                        break;
                    case 69:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var framedConcernMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership>().Deserialize(ref reader, options);
                            payload.FramedConcernMembership.Add(framedConcernMembershipDto);
                        }

                        break;
                    case 70:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var functionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.Function>().Deserialize(ref reader, options);
                            payload.Function.Add(functionDto);
                        }

                        break;
                    case 71:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var ifActionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage>().Deserialize(ref reader, options);
                            payload.IfActionUsage.Add(ifActionUsageDto);
                        }

                        break;
                    case 72:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var includeUseCaseUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage>().Deserialize(ref reader, options);
                            payload.IncludeUseCaseUsage.Add(includeUseCaseUsageDto);
                        }

                        break;
                    case 73:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var indexExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression>().Deserialize(ref reader, options);
                            payload.IndexExpression.Add(indexExpressionDto);
                        }

                        break;
                    case 74:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var interactionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.Interaction>().Deserialize(ref reader, options);
                            payload.Interaction.Add(interactionDto);
                        }

                        break;
                    case 75:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var interfaceDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition>().Deserialize(ref reader, options);
                            payload.InterfaceDefinition.Add(interfaceDefinitionDto);
                        }

                        break;
                    case 76:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var interfaceUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage>().Deserialize(ref reader, options);
                            payload.InterfaceUsage.Add(interfaceUsageDto);
                        }

                        break;
                    case 77:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var intersectingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Intersecting>().Deserialize(ref reader, options);
                            payload.Intersecting.Add(intersectingDto);
                        }

                        break;
                    case 78:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var invariantDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.Invariant>().Deserialize(ref reader, options);
                            payload.Invariant.Add(invariantDto);
                        }

                        break;
                    case 79:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var invocationExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression>().Deserialize(ref reader, options);
                            payload.InvocationExpression.Add(invocationExpressionDto);
                        }

                        break;
                    case 80:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var itemDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Items.ItemDefinition>().Deserialize(ref reader, options);
                            payload.ItemDefinition.Add(itemDefinitionDto);
                        }

                        break;
                    case 81:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var itemUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Items.ItemUsage>().Deserialize(ref reader, options);
                            payload.ItemUsage.Add(itemUsageDto);
                        }

                        break;
                    case 82:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var joinNodeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.JoinNode>().Deserialize(ref reader, options);
                            payload.JoinNode.Add(joinNodeDto);
                        }

                        break;
                    case 83:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var libraryPackageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage>().Deserialize(ref reader, options);
                            payload.LibraryPackage.Add(libraryPackageDto);
                        }

                        break;
                    case 84:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var literalBooleanDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean>().Deserialize(ref reader, options);
                            payload.LiteralBoolean.Add(literalBooleanDto);
                        }

                        break;
                    case 85:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var literalExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression>().Deserialize(ref reader, options);
                            payload.LiteralExpression.Add(literalExpressionDto);
                        }

                        break;
                    case 86:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var literalInfinityDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity>().Deserialize(ref reader, options);
                            payload.LiteralInfinity.Add(literalInfinityDto);
                        }

                        break;
                    case 87:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var literalIntegerDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger>().Deserialize(ref reader, options);
                            payload.LiteralInteger.Add(literalIntegerDto);
                        }

                        break;
                    case 88:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var literalRationalDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational>().Deserialize(ref reader, options);
                            payload.LiteralRational.Add(literalRationalDto);
                        }

                        break;
                    case 89:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var literalStringDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString>().Deserialize(ref reader, options);
                            payload.LiteralString.Add(literalStringDto);
                        }

                        break;
                    case 90:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var membershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.Membership>().Deserialize(ref reader, options);
                            payload.Membership.Add(membershipDto);
                        }

                        break;
                    case 91:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var membershipExposeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.MembershipExpose>().Deserialize(ref reader, options);
                            payload.MembershipExpose.Add(membershipExposeDto);
                        }

                        break;
                    case 92:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var membershipImportDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport>().Deserialize(ref reader, options);
                            payload.MembershipImport.Add(membershipImportDto);
                        }

                        break;
                    case 93:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var mergeNodeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.MergeNode>().Deserialize(ref reader, options);
                            payload.MergeNode.Add(mergeNodeDto);
                        }

                        break;
                    case 94:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var metaclassDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass>().Deserialize(ref reader, options);
                            payload.Metaclass.Add(metaclassDto);
                        }

                        break;
                    case 95:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var metadataAccessExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression>().Deserialize(ref reader, options);
                            payload.MetadataAccessExpression.Add(metadataAccessExpressionDto);
                        }

                        break;
                    case 96:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var metadataDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition>().Deserialize(ref reader, options);
                            payload.MetadataDefinition.Add(metadataDefinitionDto);
                        }

                        break;
                    case 97:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var metadataFeatureDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature>().Deserialize(ref reader, options);
                            payload.MetadataFeature.Add(metadataFeatureDto);
                        }

                        break;
                    case 98:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var metadataUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage>().Deserialize(ref reader, options);
                            payload.MetadataUsage.Add(metadataUsageDto);
                        }

                        break;
                    case 99:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var multiplicityDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Multiplicity>().Deserialize(ref reader, options);
                            payload.Multiplicity.Add(multiplicityDto);
                        }

                        break;
                    case 100:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var multiplicityRangeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange>().Deserialize(ref reader, options);
                            payload.MultiplicityRange.Add(multiplicityRangeDto);
                        }

                        break;
                    case 101:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var namespaceDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.Namespace>().Deserialize(ref reader, options);
                            payload.Namespace.Add(namespaceDto);
                        }

                        break;
                    case 102:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var namespaceExposeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose>().Deserialize(ref reader, options);
                            payload.NamespaceExpose.Add(namespaceExposeDto);
                        }

                        break;
                    case 103:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var namespaceImportDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport>().Deserialize(ref reader, options);
                            payload.NamespaceImport.Add(namespaceImportDto);
                        }

                        break;
                    case 104:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var nullExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression>().Deserialize(ref reader, options);
                            payload.NullExpression.Add(nullExpressionDto);
                        }

                        break;
                    case 105:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var objectiveMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership>().Deserialize(ref reader, options);
                            payload.ObjectiveMembership.Add(objectiveMembershipDto);
                        }

                        break;
                    case 106:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var occurrenceDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition>().Deserialize(ref reader, options);
                            payload.OccurrenceDefinition.Add(occurrenceDefinitionDto);
                        }

                        break;
                    case 107:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var occurrenceUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage>().Deserialize(ref reader, options);
                            payload.OccurrenceUsage.Add(occurrenceUsageDto);
                        }

                        break;
                    case 108:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var operatorExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression>().Deserialize(ref reader, options);
                            payload.OperatorExpression.Add(operatorExpressionDto);
                        }

                        break;
                    case 109:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var owningMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership>().Deserialize(ref reader, options);
                            payload.OwningMembership.Add(owningMembershipDto);
                        }

                        break;
                    case 110:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var packageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Packages.Package>().Deserialize(ref reader, options);
                            payload.Package.Add(packageDto);
                        }

                        break;
                    case 111:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var parameterMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership>().Deserialize(ref reader, options);
                            payload.ParameterMembership.Add(parameterMembershipDto);
                        }

                        break;
                    case 112:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var partDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Parts.PartDefinition>().Deserialize(ref reader, options);
                            payload.PartDefinition.Add(partDefinitionDto);
                        }

                        break;
                    case 113:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var partUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Parts.PartUsage>().Deserialize(ref reader, options);
                            payload.PartUsage.Add(partUsageDto);
                        }

                        break;
                    case 114:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var payloadFeatureDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature>().Deserialize(ref reader, options);
                            payload.PayloadFeature.Add(payloadFeatureDto);
                        }

                        break;
                    case 115:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var performActionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage>().Deserialize(ref reader, options);
                            payload.PerformActionUsage.Add(performActionUsageDto);
                        }

                        break;
                    case 116:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var portConjugationDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.PortConjugation>().Deserialize(ref reader, options);
                            payload.PortConjugation.Add(portConjugationDto);
                        }

                        break;
                    case 117:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var portDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.PortDefinition>().Deserialize(ref reader, options);
                            payload.PortDefinition.Add(portDefinitionDto);
                        }

                        break;
                    case 118:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var portUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Ports.PortUsage>().Deserialize(ref reader, options);
                            payload.PortUsage.Add(portUsageDto);
                        }

                        break;
                    case 119:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var predicateDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.Predicate>().Deserialize(ref reader, options);
                            payload.Predicate.Add(predicateDto);
                        }

                        break;
                    case 120:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var redefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.Redefinition>().Deserialize(ref reader, options);
                            payload.Redefinition.Add(redefinitionDto);
                        }

                        break;
                    case 121:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var referenceSubsettingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting>().Deserialize(ref reader, options);
                            payload.ReferenceSubsetting.Add(referenceSubsettingDto);
                        }

                        break;
                    case 122:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var referenceUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage>().Deserialize(ref reader, options);
                            payload.ReferenceUsage.Add(referenceUsageDto);
                        }

                        break;
                    case 123:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var renderingDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition>().Deserialize(ref reader, options);
                            payload.RenderingDefinition.Add(renderingDefinitionDto);
                        }

                        break;
                    case 124:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var renderingUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.RenderingUsage>().Deserialize(ref reader, options);
                            payload.RenderingUsage.Add(renderingUsageDto);
                        }

                        break;
                    case 125:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var requirementConstraintMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership>().Deserialize(ref reader, options);
                            payload.RequirementConstraintMembership.Add(requirementConstraintMembershipDto);
                        }

                        break;
                    case 126:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var requirementDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition>().Deserialize(ref reader, options);
                            payload.RequirementDefinition.Add(requirementDefinitionDto);
                        }

                        break;
                    case 127:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var requirementUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage>().Deserialize(ref reader, options);
                            payload.RequirementUsage.Add(requirementUsageDto);
                        }

                        break;
                    case 128:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var requirementVerificationMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership>().Deserialize(ref reader, options);
                            payload.RequirementVerificationMembership.Add(requirementVerificationMembershipDto);
                        }

                        break;
                    case 129:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var resultExpressionMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership>().Deserialize(ref reader, options);
                            payload.ResultExpressionMembership.Add(resultExpressionMembershipDto);
                        }

                        break;
                    case 130:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var returnParameterMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership>().Deserialize(ref reader, options);
                            payload.ReturnParameterMembership.Add(returnParameterMembershipDto);
                        }

                        break;
                    case 131:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var satisfyRequirementUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage>().Deserialize(ref reader, options);
                            payload.SatisfyRequirementUsage.Add(satisfyRequirementUsageDto);
                        }

                        break;
                    case 132:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var selectExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression>().Deserialize(ref reader, options);
                            payload.SelectExpression.Add(selectExpressionDto);
                        }

                        break;
                    case 133:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var sendActionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage>().Deserialize(ref reader, options);
                            payload.SendActionUsage.Add(sendActionUsageDto);
                        }

                        break;
                    case 134:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var specializationDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Specialization>().Deserialize(ref reader, options);
                            payload.Specialization.Add(specializationDto);
                        }

                        break;
                    case 135:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var stakeholderMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership>().Deserialize(ref reader, options);
                            payload.StakeholderMembership.Add(stakeholderMembershipDto);
                        }

                        break;
                    case 136:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var stateDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.StateDefinition>().Deserialize(ref reader, options);
                            payload.StateDefinition.Add(stateDefinitionDto);
                        }

                        break;
                    case 137:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var stateSubactionMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership>().Deserialize(ref reader, options);
                            payload.StateSubactionMembership.Add(stateSubactionMembershipDto);
                        }

                        break;
                    case 138:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var stateUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.StateUsage>().Deserialize(ref reader, options);
                            payload.StateUsage.Add(stateUsageDto);
                        }

                        break;
                    case 139:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var stepDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Behaviors.Step>().Deserialize(ref reader, options);
                            payload.Step.Add(stepDto);
                        }

                        break;
                    case 140:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var structureDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Structures.Structure>().Deserialize(ref reader, options);
                            payload.Structure.Add(structureDto);
                        }

                        break;
                    case 141:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var subclassificationDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Classifiers.Subclassification>().Deserialize(ref reader, options);
                            payload.Subclassification.Add(subclassificationDto);
                        }

                        break;
                    case 142:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var subjectMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership>().Deserialize(ref reader, options);
                            payload.SubjectMembership.Add(subjectMembershipDto);
                        }

                        break;
                    case 143:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var subsettingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.Subsetting>().Deserialize(ref reader, options);
                            payload.Subsetting.Add(subsettingDto);
                        }

                        break;
                    case 144:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var successionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Connectors.Succession>().Deserialize(ref reader, options);
                            payload.Succession.Add(successionDto);
                        }

                        break;
                    case 145:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var successionAsUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage>().Deserialize(ref reader, options);
                            payload.SuccessionAsUsage.Add(successionAsUsageDto);
                        }

                        break;
                    case 146:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var successionFlowDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow>().Deserialize(ref reader, options);
                            payload.SuccessionFlow.Add(successionFlowDto);
                        }

                        break;
                    case 147:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var successionFlowUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage>().Deserialize(ref reader, options);
                            payload.SuccessionFlowUsage.Add(successionFlowUsageDto);
                        }

                        break;
                    case 148:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var terminateActionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage>().Deserialize(ref reader, options);
                            payload.TerminateActionUsage.Add(terminateActionUsageDto);
                        }

                        break;
                    case 149:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var textualRepresentationDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation>().Deserialize(ref reader, options);
                            payload.TextualRepresentation.Add(textualRepresentationDto);
                        }

                        break;
                    case 150:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var transitionFeatureMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership>().Deserialize(ref reader, options);
                            payload.TransitionFeatureMembership.Add(transitionFeatureMembershipDto);
                        }

                        break;
                    case 151:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var transitionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.States.TransitionUsage>().Deserialize(ref reader, options);
                            payload.TransitionUsage.Add(transitionUsageDto);
                        }

                        break;
                    case 152:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var triggerInvocationExpressionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression>().Deserialize(ref reader, options);
                            payload.TriggerInvocationExpression.Add(triggerInvocationExpressionDto);
                        }

                        break;
                    case 153:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var typeDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Type>().Deserialize(ref reader, options);
                            payload.Type.Add(typeDto);
                        }

                        break;
                    case 154:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var typeFeaturingDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Features.TypeFeaturing>().Deserialize(ref reader, options);
                            payload.TypeFeaturing.Add(typeFeaturingDto);
                        }

                        break;
                    case 155:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var unioningDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Core.Types.Unioning>().Deserialize(ref reader, options);
                            payload.Unioning.Add(unioningDto);
                        }

                        break;
                    case 156:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var usageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage>().Deserialize(ref reader, options);
                            payload.Usage.Add(usageDto);
                        }

                        break;
                    case 157:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var useCaseDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition>().Deserialize(ref reader, options);
                            payload.UseCaseDefinition.Add(useCaseDefinitionDto);
                        }

                        break;
                    case 158:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var useCaseUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage>().Deserialize(ref reader, options);
                            payload.UseCaseUsage.Add(useCaseUsageDto);
                        }

                        break;
                    case 159:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var variantMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership>().Deserialize(ref reader, options);
                            payload.VariantMembership.Add(variantMembershipDto);
                        }

                        break;
                    case 160:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var verificationCaseDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition>().Deserialize(ref reader, options);
                            payload.VerificationCaseDefinition.Add(verificationCaseDefinitionDto);
                        }

                        break;
                    case 161:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var verificationCaseUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage>().Deserialize(ref reader, options);
                            payload.VerificationCaseUsage.Add(verificationCaseUsageDto);
                        }

                        break;
                    case 162:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var viewDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewDefinition>().Deserialize(ref reader, options);
                            payload.ViewDefinition.Add(viewDefinitionDto);
                        }

                        break;
                    case 163:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var viewpointDefinitionDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition>().Deserialize(ref reader, options);
                            payload.ViewpointDefinition.Add(viewpointDefinitionDto);
                        }

                        break;
                    case 164:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var viewpointUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage>().Deserialize(ref reader, options);
                            payload.ViewpointUsage.Add(viewpointUsageDto);
                        }

                        break;
                    case 165:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var viewRenderingMembershipDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership>().Deserialize(ref reader, options);
                            payload.ViewRenderingMembership.Add(viewRenderingMembershipDto);
                        }

                        break;
                    case 166:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var viewUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Views.ViewUsage>().Deserialize(ref reader, options);
                            payload.ViewUsage.Add(viewUsageDto);
                        }

                        break;
                    case 167:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var whileLoopActionUsageDto = formatterResolver.GetFormatterWithVerify<SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage>().Deserialize(ref reader, options);
                            payload.WhileLoopActionUsage.Add(whileLoopActionUsageDto);
                        }

                        break;
                }
            }

            return payload;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
