// -------------------------------------------------------------------------------------------------
// <copyright file="ElementFactory.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Dal
{
    using System;

    /// <summary>
    /// The purpose of the <see cref="ElementFactory"/> is to create a <see cref="Core.POCO.Root.Elements.IElement"/> POCO
    /// based on a <see cref="Core.DTO.Root.Elements.IElement"/> DTO
    /// </summary>
    public class ElementFactory : IElementFactory
    {
        /// <summary>
        /// Creates a <see cref="Core.POCO.Root.Elements.IElement"/> POCO based on a <see cref="Core.DTO.Root.Elements.IElement"/>
        /// </summary>
        /// <param name="dto">
        /// the source <see cref="Core.DTO.Root.Elements.IElement"/> DTO
        /// </param>
        /// <returns>
        /// a <see cref="Core.POCO.Root.Elements.IElement"/> POCO
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// thrown when <paramref name="dto"/> is null
        /// </exception>
        public Core.POCO.Root.Elements.IElement Create(Core.DTO.Root.Elements.IElement dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            switch (dto)
            {
                case Core.DTO.Systems.Actions.AcceptActionUsage acceptActionUsageDto:
                    return AcceptActionUsageFactory.Create(acceptActionUsageDto);
                case Core.DTO.Systems.Actions.ActionDefinition actionDefinitionDto:
                    return ActionDefinitionFactory.Create(actionDefinitionDto);
                case Core.DTO.Systems.Actions.ActionUsage actionUsageDto:
                    return ActionUsageFactory.Create(actionUsageDto);
                case Core.DTO.Systems.Requirements.ActorMembership actorMembershipDto:
                    return ActorMembershipFactory.Create(actorMembershipDto);
                case Core.DTO.Systems.Allocations.AllocationDefinition allocationDefinitionDto:
                    return AllocationDefinitionFactory.Create(allocationDefinitionDto);
                case Core.DTO.Systems.Allocations.AllocationUsage allocationUsageDto:
                    return AllocationUsageFactory.Create(allocationUsageDto);
                case Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition analysisCaseDefinitionDto:
                    return AnalysisCaseDefinitionFactory.Create(analysisCaseDefinitionDto);
                case Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage analysisCaseUsageDto:
                    return AnalysisCaseUsageFactory.Create(analysisCaseUsageDto);
                case Core.DTO.Root.Annotations.AnnotatingElement annotatingElementDto:
                    return AnnotatingElementFactory.Create(annotatingElementDto);
                case Core.DTO.Root.Annotations.Annotation annotationDto:
                    return AnnotationFactory.Create(annotationDto);
                case Core.DTO.Systems.Constraints.AssertConstraintUsage assertConstraintUsageDto:
                    return AssertConstraintUsageFactory.Create(assertConstraintUsageDto);
                case Core.DTO.Systems.Actions.AssignmentActionUsage assignmentActionUsageDto:
                    return AssignmentActionUsageFactory.Create(assignmentActionUsageDto);
                case Core.DTO.Kernel.Associations.Association associationDto:
                    return AssociationFactory.Create(associationDto);
                case Core.DTO.Kernel.Associations.AssociationStructure associationStructureDto:
                    return AssociationStructureFactory.Create(associationStructureDto);
                case Core.DTO.Systems.Attributes.AttributeDefinition attributeDefinitionDto:
                    return AttributeDefinitionFactory.Create(attributeDefinitionDto);
                case Core.DTO.Systems.Attributes.AttributeUsage attributeUsageDto:
                    return AttributeUsageFactory.Create(attributeUsageDto);
                case Core.DTO.Kernel.Behaviors.Behavior behaviorDto:
                    return BehaviorFactory.Create(behaviorDto);
                case Core.DTO.Kernel.Connectors.BindingConnector bindingConnectorDto:
                    return BindingConnectorFactory.Create(bindingConnectorDto);
                case Core.DTO.Systems.Connections.BindingConnectorAsUsage bindingConnectorAsUsageDto:
                    return BindingConnectorAsUsageFactory.Create(bindingConnectorAsUsageDto);
                case Core.DTO.Kernel.Functions.BooleanExpression booleanExpressionDto:
                    return BooleanExpressionFactory.Create(booleanExpressionDto);
                case Core.DTO.Systems.Calculations.CalculationDefinition calculationDefinitionDto:
                    return CalculationDefinitionFactory.Create(calculationDefinitionDto);
                case Core.DTO.Systems.Calculations.CalculationUsage calculationUsageDto:
                    return CalculationUsageFactory.Create(calculationUsageDto);
                case Core.DTO.Systems.Cases.CaseDefinition caseDefinitionDto:
                    return CaseDefinitionFactory.Create(caseDefinitionDto);
                case Core.DTO.Systems.Cases.CaseUsage caseUsageDto:
                    return CaseUsageFactory.Create(caseUsageDto);
                case Core.DTO.Kernel.Classes.Class classDto:
                    return ClassFactory.Create(classDto);
                case Core.DTO.Core.Classifiers.Classifier classifierDto:
                    return ClassifierFactory.Create(classifierDto);
                case Core.DTO.Kernel.Expressions.CollectExpression collectExpressionDto:
                    return CollectExpressionFactory.Create(collectExpressionDto);
                case Core.DTO.Root.Annotations.Comment commentDto:
                    return CommentFactory.Create(commentDto);
                case Core.DTO.Systems.Requirements.ConcernDefinition concernDefinitionDto:
                    return ConcernDefinitionFactory.Create(concernDefinitionDto);
                case Core.DTO.Systems.Requirements.ConcernUsage concernUsageDto:
                    return ConcernUsageFactory.Create(concernUsageDto);
                case Core.DTO.Systems.Ports.ConjugatedPortDefinition conjugatedPortDefinitionDto:
                    return ConjugatedPortDefinitionFactory.Create(conjugatedPortDefinitionDto);
                case Core.DTO.Systems.Ports.ConjugatedPortTyping conjugatedPortTypingDto:
                    return ConjugatedPortTypingFactory.Create(conjugatedPortTypingDto);
                case Core.DTO.Core.Types.Conjugation conjugationDto:
                    return ConjugationFactory.Create(conjugationDto);
                case Core.DTO.Systems.Connections.ConnectionDefinition connectionDefinitionDto:
                    return ConnectionDefinitionFactory.Create(connectionDefinitionDto);
                case Core.DTO.Systems.Connections.ConnectionUsage connectionUsageDto:
                    return ConnectionUsageFactory.Create(connectionUsageDto);
                case Core.DTO.Kernel.Connectors.Connector connectorDto:
                    return ConnectorFactory.Create(connectorDto);
                case Core.DTO.Systems.Constraints.ConstraintDefinition constraintDefinitionDto:
                    return ConstraintDefinitionFactory.Create(constraintDefinitionDto);
                case Core.DTO.Systems.Constraints.ConstraintUsage constraintUsageDto:
                    return ConstraintUsageFactory.Create(constraintUsageDto);
                case Core.DTO.Kernel.Expressions.ConstructorExpression constructorExpressionDto:
                    return ConstructorExpressionFactory.Create(constructorExpressionDto);
                case Core.DTO.Core.Features.CrossSubsetting crossSubsettingDto:
                    return CrossSubsettingFactory.Create(crossSubsettingDto);
                case Core.DTO.Kernel.DataTypes.DataType dataTypeDto:
                    return DataTypeFactory.Create(dataTypeDto);
                case Core.DTO.Systems.Actions.DecisionNode decisionNodeDto:
                    return DecisionNodeFactory.Create(decisionNodeDto);
                case Core.DTO.Systems.DefinitionAndUsage.Definition definitionDto:
                    return DefinitionFactory.Create(definitionDto);
                case Core.DTO.Root.Dependencies.Dependency dependencyDto:
                    return DependencyFactory.Create(dependencyDto);
                case Core.DTO.Core.Types.Differencing differencingDto:
                    return DifferencingFactory.Create(differencingDto);
                case Core.DTO.Core.Types.Disjoining disjoiningDto:
                    return DisjoiningFactory.Create(disjoiningDto);
                case Core.DTO.Root.Annotations.Documentation documentationDto:
                    return DocumentationFactory.Create(documentationDto);
                case Core.DTO.Kernel.Packages.ElementFilterMembership elementFilterMembershipDto:
                    return ElementFilterMembershipFactory.Create(elementFilterMembershipDto);
                case Core.DTO.Core.Features.EndFeatureMembership endFeatureMembershipDto:
                    return EndFeatureMembershipFactory.Create(endFeatureMembershipDto);
                case Core.DTO.Systems.Enumerations.EnumerationDefinition enumerationDefinitionDto:
                    return EnumerationDefinitionFactory.Create(enumerationDefinitionDto);
                case Core.DTO.Systems.Enumerations.EnumerationUsage enumerationUsageDto:
                    return EnumerationUsageFactory.Create(enumerationUsageDto);
                case Core.DTO.Systems.Occurrences.EventOccurrenceUsage eventOccurrenceUsageDto:
                    return EventOccurrenceUsageFactory.Create(eventOccurrenceUsageDto);
                case Core.DTO.Systems.States.ExhibitStateUsage exhibitStateUsageDto:
                    return ExhibitStateUsageFactory.Create(exhibitStateUsageDto);
                case Core.DTO.Kernel.Functions.Expression expressionDto:
                    return ExpressionFactory.Create(expressionDto);
                case Core.DTO.Core.Features.Feature featureDto:
                    return FeatureFactory.Create(featureDto);
                case Core.DTO.Kernel.Expressions.FeatureChainExpression featureChainExpressionDto:
                    return FeatureChainExpressionFactory.Create(featureChainExpressionDto);
                case Core.DTO.Core.Features.FeatureChaining featureChainingDto:
                    return FeatureChainingFactory.Create(featureChainingDto);
                case Core.DTO.Core.Features.FeatureInverting featureInvertingDto:
                    return FeatureInvertingFactory.Create(featureInvertingDto);
                case Core.DTO.Core.Types.FeatureMembership featureMembershipDto:
                    return FeatureMembershipFactory.Create(featureMembershipDto);
                case Core.DTO.Kernel.Expressions.FeatureReferenceExpression featureReferenceExpressionDto:
                    return FeatureReferenceExpressionFactory.Create(featureReferenceExpressionDto);
                case Core.DTO.Core.Features.FeatureTyping featureTypingDto:
                    return FeatureTypingFactory.Create(featureTypingDto);
                case Core.DTO.Kernel.FeatureValues.FeatureValue featureValueDto:
                    return FeatureValueFactory.Create(featureValueDto);
                case Core.DTO.Kernel.Interactions.Flow flowDto:
                    return FlowFactory.Create(flowDto);
                case Core.DTO.Systems.Flows.FlowDefinition flowDefinitionDto:
                    return FlowDefinitionFactory.Create(flowDefinitionDto);
                case Core.DTO.Kernel.Interactions.FlowEnd flowEndDto:
                    return FlowEndFactory.Create(flowEndDto);
                case Core.DTO.Systems.Flows.FlowUsage flowUsageDto:
                    return FlowUsageFactory.Create(flowUsageDto);
                case Core.DTO.Systems.Actions.ForkNode forkNodeDto:
                    return ForkNodeFactory.Create(forkNodeDto);
                case Core.DTO.Systems.Actions.ForLoopActionUsage forLoopActionUsageDto:
                    return ForLoopActionUsageFactory.Create(forLoopActionUsageDto);
                case Core.DTO.Systems.Requirements.FramedConcernMembership framedConcernMembershipDto:
                    return FramedConcernMembershipFactory.Create(framedConcernMembershipDto);
                case Core.DTO.Kernel.Functions.Function functionDto:
                    return FunctionFactory.Create(functionDto);
                case Core.DTO.Systems.Actions.IfActionUsage ifActionUsageDto:
                    return IfActionUsageFactory.Create(ifActionUsageDto);
                case Core.DTO.Systems.UseCases.IncludeUseCaseUsage includeUseCaseUsageDto:
                    return IncludeUseCaseUsageFactory.Create(includeUseCaseUsageDto);
                case Core.DTO.Kernel.Expressions.IndexExpression indexExpressionDto:
                    return IndexExpressionFactory.Create(indexExpressionDto);
                case Core.DTO.Kernel.Interactions.Interaction interactionDto:
                    return InteractionFactory.Create(interactionDto);
                case Core.DTO.Systems.Interfaces.InterfaceDefinition interfaceDefinitionDto:
                    return InterfaceDefinitionFactory.Create(interfaceDefinitionDto);
                case Core.DTO.Systems.Interfaces.InterfaceUsage interfaceUsageDto:
                    return InterfaceUsageFactory.Create(interfaceUsageDto);
                case Core.DTO.Core.Types.Intersecting intersectingDto:
                    return IntersectingFactory.Create(intersectingDto);
                case Core.DTO.Kernel.Functions.Invariant invariantDto:
                    return InvariantFactory.Create(invariantDto);
                case Core.DTO.Kernel.Expressions.InvocationExpression invocationExpressionDto:
                    return InvocationExpressionFactory.Create(invocationExpressionDto);
                case Core.DTO.Systems.Items.ItemDefinition itemDefinitionDto:
                    return ItemDefinitionFactory.Create(itemDefinitionDto);
                case Core.DTO.Systems.Items.ItemUsage itemUsageDto:
                    return ItemUsageFactory.Create(itemUsageDto);
                case Core.DTO.Systems.Actions.JoinNode joinNodeDto:
                    return JoinNodeFactory.Create(joinNodeDto);
                case Core.DTO.Kernel.Packages.LibraryPackage libraryPackageDto:
                    return LibraryPackageFactory.Create(libraryPackageDto);
                case Core.DTO.Kernel.Expressions.LiteralBoolean literalBooleanDto:
                    return LiteralBooleanFactory.Create(literalBooleanDto);
                case Core.DTO.Kernel.Expressions.LiteralExpression literalExpressionDto:
                    return LiteralExpressionFactory.Create(literalExpressionDto);
                case Core.DTO.Kernel.Expressions.LiteralInfinity literalInfinityDto:
                    return LiteralInfinityFactory.Create(literalInfinityDto);
                case Core.DTO.Kernel.Expressions.LiteralInteger literalIntegerDto:
                    return LiteralIntegerFactory.Create(literalIntegerDto);
                case Core.DTO.Kernel.Expressions.LiteralRational literalRationalDto:
                    return LiteralRationalFactory.Create(literalRationalDto);
                case Core.DTO.Kernel.Expressions.LiteralString literalStringDto:
                    return LiteralStringFactory.Create(literalStringDto);
                case Core.DTO.Root.Namespaces.Membership membershipDto:
                    return MembershipFactory.Create(membershipDto);
                case Core.DTO.Systems.Views.MembershipExpose membershipExposeDto:
                    return MembershipExposeFactory.Create(membershipExposeDto);
                case Core.DTO.Root.Namespaces.MembershipImport membershipImportDto:
                    return MembershipImportFactory.Create(membershipImportDto);
                case Core.DTO.Systems.Actions.MergeNode mergeNodeDto:
                    return MergeNodeFactory.Create(mergeNodeDto);
                case Core.DTO.Kernel.Metadata.Metaclass metaclassDto:
                    return MetaclassFactory.Create(metaclassDto);
                case Core.DTO.Kernel.Expressions.MetadataAccessExpression metadataAccessExpressionDto:
                    return MetadataAccessExpressionFactory.Create(metadataAccessExpressionDto);
                case Core.DTO.Systems.Metadata.MetadataDefinition metadataDefinitionDto:
                    return MetadataDefinitionFactory.Create(metadataDefinitionDto);
                case Core.DTO.Kernel.Metadata.MetadataFeature metadataFeatureDto:
                    return MetadataFeatureFactory.Create(metadataFeatureDto);
                case Core.DTO.Systems.Metadata.MetadataUsage metadataUsageDto:
                    return MetadataUsageFactory.Create(metadataUsageDto);
                case Core.DTO.Core.Types.Multiplicity multiplicityDto:
                    return MultiplicityFactory.Create(multiplicityDto);
                case Core.DTO.Kernel.Multiplicities.MultiplicityRange multiplicityRangeDto:
                    return MultiplicityRangeFactory.Create(multiplicityRangeDto);
                case Core.DTO.Root.Namespaces.Namespace namespaceDto:
                    return NamespaceFactory.Create(namespaceDto);
                case Core.DTO.Systems.Views.NamespaceExpose namespaceExposeDto:
                    return NamespaceExposeFactory.Create(namespaceExposeDto);
                case Core.DTO.Root.Namespaces.NamespaceImport namespaceImportDto:
                    return NamespaceImportFactory.Create(namespaceImportDto);
                case Core.DTO.Kernel.Expressions.NullExpression nullExpressionDto:
                    return NullExpressionFactory.Create(nullExpressionDto);
                case Core.DTO.Systems.Cases.ObjectiveMembership objectiveMembershipDto:
                    return ObjectiveMembershipFactory.Create(objectiveMembershipDto);
                case Core.DTO.Systems.Occurrences.OccurrenceDefinition occurrenceDefinitionDto:
                    return OccurrenceDefinitionFactory.Create(occurrenceDefinitionDto);
                case Core.DTO.Systems.Occurrences.OccurrenceUsage occurrenceUsageDto:
                    return OccurrenceUsageFactory.Create(occurrenceUsageDto);
                case Core.DTO.Kernel.Expressions.OperatorExpression operatorExpressionDto:
                    return OperatorExpressionFactory.Create(operatorExpressionDto);
                case Core.DTO.Root.Namespaces.OwningMembership owningMembershipDto:
                    return OwningMembershipFactory.Create(owningMembershipDto);
                case Core.DTO.Kernel.Packages.Package packageDto:
                    return PackageFactory.Create(packageDto);
                case Core.DTO.Kernel.Behaviors.ParameterMembership parameterMembershipDto:
                    return ParameterMembershipFactory.Create(parameterMembershipDto);
                case Core.DTO.Systems.Parts.PartDefinition partDefinitionDto:
                    return PartDefinitionFactory.Create(partDefinitionDto);
                case Core.DTO.Systems.Parts.PartUsage partUsageDto:
                    return PartUsageFactory.Create(partUsageDto);
                case Core.DTO.Kernel.Interactions.PayloadFeature payloadFeatureDto:
                    return PayloadFeatureFactory.Create(payloadFeatureDto);
                case Core.DTO.Systems.Actions.PerformActionUsage performActionUsageDto:
                    return PerformActionUsageFactory.Create(performActionUsageDto);
                case Core.DTO.Systems.Ports.PortConjugation portConjugationDto:
                    return PortConjugationFactory.Create(portConjugationDto);
                case Core.DTO.Systems.Ports.PortDefinition portDefinitionDto:
                    return PortDefinitionFactory.Create(portDefinitionDto);
                case Core.DTO.Systems.Ports.PortUsage portUsageDto:
                    return PortUsageFactory.Create(portUsageDto);
                case Core.DTO.Kernel.Functions.Predicate predicateDto:
                    return PredicateFactory.Create(predicateDto);
                case Core.DTO.Core.Features.Redefinition redefinitionDto:
                    return RedefinitionFactory.Create(redefinitionDto);
                case Core.DTO.Core.Features.ReferenceSubsetting referenceSubsettingDto:
                    return ReferenceSubsettingFactory.Create(referenceSubsettingDto);
                case Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage referenceUsageDto:
                    return ReferenceUsageFactory.Create(referenceUsageDto);
                case Core.DTO.Systems.Views.RenderingDefinition renderingDefinitionDto:
                    return RenderingDefinitionFactory.Create(renderingDefinitionDto);
                case Core.DTO.Systems.Views.RenderingUsage renderingUsageDto:
                    return RenderingUsageFactory.Create(renderingUsageDto);
                case Core.DTO.Systems.Requirements.RequirementConstraintMembership requirementConstraintMembershipDto:
                    return RequirementConstraintMembershipFactory.Create(requirementConstraintMembershipDto);
                case Core.DTO.Systems.Requirements.RequirementDefinition requirementDefinitionDto:
                    return RequirementDefinitionFactory.Create(requirementDefinitionDto);
                case Core.DTO.Systems.Requirements.RequirementUsage requirementUsageDto:
                    return RequirementUsageFactory.Create(requirementUsageDto);
                case Core.DTO.Systems.VerificationCases.RequirementVerificationMembership requirementVerificationMembershipDto:
                    return RequirementVerificationMembershipFactory.Create(requirementVerificationMembershipDto);
                case Core.DTO.Kernel.Functions.ResultExpressionMembership resultExpressionMembershipDto:
                    return ResultExpressionMembershipFactory.Create(resultExpressionMembershipDto);
                case Core.DTO.Kernel.Functions.ReturnParameterMembership returnParameterMembershipDto:
                    return ReturnParameterMembershipFactory.Create(returnParameterMembershipDto);
                case Core.DTO.Systems.Requirements.SatisfyRequirementUsage satisfyRequirementUsageDto:
                    return SatisfyRequirementUsageFactory.Create(satisfyRequirementUsageDto);
                case Core.DTO.Kernel.Expressions.SelectExpression selectExpressionDto:
                    return SelectExpressionFactory.Create(selectExpressionDto);
                case Core.DTO.Systems.Actions.SendActionUsage sendActionUsageDto:
                    return SendActionUsageFactory.Create(sendActionUsageDto);
                case Core.DTO.Core.Types.Specialization specializationDto:
                    return SpecializationFactory.Create(specializationDto);
                case Core.DTO.Systems.Requirements.StakeholderMembership stakeholderMembershipDto:
                    return StakeholderMembershipFactory.Create(stakeholderMembershipDto);
                case Core.DTO.Systems.States.StateDefinition stateDefinitionDto:
                    return StateDefinitionFactory.Create(stateDefinitionDto);
                case Core.DTO.Systems.States.StateSubactionMembership stateSubactionMembershipDto:
                    return StateSubactionMembershipFactory.Create(stateSubactionMembershipDto);
                case Core.DTO.Systems.States.StateUsage stateUsageDto:
                    return StateUsageFactory.Create(stateUsageDto);
                case Core.DTO.Kernel.Behaviors.Step stepDto:
                    return StepFactory.Create(stepDto);
                case Core.DTO.Kernel.Structures.Structure structureDto:
                    return StructureFactory.Create(structureDto);
                case Core.DTO.Core.Classifiers.Subclassification subclassificationDto:
                    return SubclassificationFactory.Create(subclassificationDto);
                case Core.DTO.Systems.Requirements.SubjectMembership subjectMembershipDto:
                    return SubjectMembershipFactory.Create(subjectMembershipDto);
                case Core.DTO.Core.Features.Subsetting subsettingDto:
                    return SubsettingFactory.Create(subsettingDto);
                case Core.DTO.Kernel.Connectors.Succession successionDto:
                    return SuccessionFactory.Create(successionDto);
                case Core.DTO.Systems.Connections.SuccessionAsUsage successionAsUsageDto:
                    return SuccessionAsUsageFactory.Create(successionAsUsageDto);
                case Core.DTO.Kernel.Interactions.SuccessionFlow successionFlowDto:
                    return SuccessionFlowFactory.Create(successionFlowDto);
                case Core.DTO.Systems.Flows.SuccessionFlowUsage successionFlowUsageDto:
                    return SuccessionFlowUsageFactory.Create(successionFlowUsageDto);
                case Core.DTO.Systems.Actions.TerminateActionUsage terminateActionUsageDto:
                    return TerminateActionUsageFactory.Create(terminateActionUsageDto);
                case Core.DTO.Root.Annotations.TextualRepresentation textualRepresentationDto:
                    return TextualRepresentationFactory.Create(textualRepresentationDto);
                case Core.DTO.Systems.States.TransitionFeatureMembership transitionFeatureMembershipDto:
                    return TransitionFeatureMembershipFactory.Create(transitionFeatureMembershipDto);
                case Core.DTO.Systems.States.TransitionUsage transitionUsageDto:
                    return TransitionUsageFactory.Create(transitionUsageDto);
                case Core.DTO.Systems.Actions.TriggerInvocationExpression triggerInvocationExpressionDto:
                    return TriggerInvocationExpressionFactory.Create(triggerInvocationExpressionDto);
                case Core.DTO.Core.Types.Type typeDto:
                    return TypeFactory.Create(typeDto);
                case Core.DTO.Core.Features.TypeFeaturing typeFeaturingDto:
                    return TypeFeaturingFactory.Create(typeFeaturingDto);
                case Core.DTO.Core.Types.Unioning unioningDto:
                    return UnioningFactory.Create(unioningDto);
                case Core.DTO.Systems.DefinitionAndUsage.Usage usageDto:
                    return UsageFactory.Create(usageDto);
                case Core.DTO.Systems.UseCases.UseCaseDefinition useCaseDefinitionDto:
                    return UseCaseDefinitionFactory.Create(useCaseDefinitionDto);
                case Core.DTO.Systems.UseCases.UseCaseUsage useCaseUsageDto:
                    return UseCaseUsageFactory.Create(useCaseUsageDto);
                case Core.DTO.Systems.DefinitionAndUsage.VariantMembership variantMembershipDto:
                    return VariantMembershipFactory.Create(variantMembershipDto);
                case Core.DTO.Systems.VerificationCases.VerificationCaseDefinition verificationCaseDefinitionDto:
                    return VerificationCaseDefinitionFactory.Create(verificationCaseDefinitionDto);
                case Core.DTO.Systems.VerificationCases.VerificationCaseUsage verificationCaseUsageDto:
                    return VerificationCaseUsageFactory.Create(verificationCaseUsageDto);
                case Core.DTO.Systems.Views.ViewDefinition viewDefinitionDto:
                    return ViewDefinitionFactory.Create(viewDefinitionDto);
                case Core.DTO.Systems.Views.ViewpointDefinition viewpointDefinitionDto:
                    return ViewpointDefinitionFactory.Create(viewpointDefinitionDto);
                case Core.DTO.Systems.Views.ViewpointUsage viewpointUsageDto:
                    return ViewpointUsageFactory.Create(viewpointUsageDto);
                case Core.DTO.Systems.Views.ViewRenderingMembership viewRenderingMembershipDto:
                    return ViewRenderingMembershipFactory.Create(viewRenderingMembershipDto);
                case Core.DTO.Systems.Views.ViewUsage viewUsageDto:
                    return ViewUsageFactory.Create(viewUsageDto);
                case Core.DTO.Systems.Actions.WhileLoopActionUsage whileLoopActionUsageDto:
                    return WhileLoopActionUsageFactory.Create(whileLoopActionUsageDto);
                default:
                    throw new NotSupportedException($"{dto.GetType().Name} not yet supported");
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
