// -------------------------------------------------------------------------------------------------
// <copyright file="ElementFactory.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Dal
{
    using System;

    /// <summary>
    /// The purpose of the <see cref="ElementFactory"/> is to create a <see cref="Core.POCO.IElement"/> POCO
    /// based on a <see cref="Core.DTO.IElement"/> DTO
    /// </summary>
    public class ElementFactory : IElementFactory
    {
        /// <summary>
        /// Creates a <see cref="Core.POCO.IElement"/> POCO based on a <see cref="Core.DTO.IElement"/>
        /// </summary>
        /// <param name="dto">
        /// the source <see cref="Core.DTO.IElement"/> DTO
        /// </param>
        /// <returns>
        /// a <see cref="Core.POCO.IElement"/> POCO
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// thrown when <paramref name="dto"/> is null
        /// </exception>
        public Core.POCO.IElement Create(Core.DTO.IElement dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            switch (dto)
            {
                case Core.DTO.AcceptActionUsage acceptActionUsageDto:
                    var acceptActionUsageFactory = new AcceptActionUsageFactory();
                    return acceptActionUsageFactory.Create(acceptActionUsageDto);
                case Core.DTO.ActionDefinition actionDefinitionDto:
                    var actionDefinitionFactory = new ActionDefinitionFactory();
                    return actionDefinitionFactory.Create(actionDefinitionDto);
                case Core.DTO.ActionUsage actionUsageDto:
                    var actionUsageFactory = new ActionUsageFactory();
                    return actionUsageFactory.Create(actionUsageDto);
                case Core.DTO.ActorMembership actorMembershipDto:
                    var actorMembershipFactory = new ActorMembershipFactory();
                    return actorMembershipFactory.Create(actorMembershipDto);
                case Core.DTO.AllocationDefinition allocationDefinitionDto:
                    var allocationDefinitionFactory = new AllocationDefinitionFactory();
                    return allocationDefinitionFactory.Create(allocationDefinitionDto);
                case Core.DTO.AllocationUsage allocationUsageDto:
                    var allocationUsageFactory = new AllocationUsageFactory();
                    return allocationUsageFactory.Create(allocationUsageDto);
                case Core.DTO.AnalysisCaseDefinition analysisCaseDefinitionDto:
                    var analysisCaseDefinitionFactory = new AnalysisCaseDefinitionFactory();
                    return analysisCaseDefinitionFactory.Create(analysisCaseDefinitionDto);
                case Core.DTO.AnalysisCaseUsage analysisCaseUsageDto:
                    var analysisCaseUsageFactory = new AnalysisCaseUsageFactory();
                    return analysisCaseUsageFactory.Create(analysisCaseUsageDto);
                case Core.DTO.AnnotatingElement annotatingElementDto:
                    var annotatingElementFactory = new AnnotatingElementFactory();
                    return annotatingElementFactory.Create(annotatingElementDto);
                case Core.DTO.Annotation annotationDto:
                    var annotationFactory = new AnnotationFactory();
                    return annotationFactory.Create(annotationDto);
                case Core.DTO.AssertConstraintUsage assertConstraintUsageDto:
                    var assertConstraintUsageFactory = new AssertConstraintUsageFactory();
                    return assertConstraintUsageFactory.Create(assertConstraintUsageDto);
                case Core.DTO.AssignmentActionUsage assignmentActionUsageDto:
                    var assignmentActionUsageFactory = new AssignmentActionUsageFactory();
                    return assignmentActionUsageFactory.Create(assignmentActionUsageDto);
                case Core.DTO.Association associationDto:
                    var associationFactory = new AssociationFactory();
                    return associationFactory.Create(associationDto);
                case Core.DTO.AssociationStructure associationStructureDto:
                    var associationStructureFactory = new AssociationStructureFactory();
                    return associationStructureFactory.Create(associationStructureDto);
                case Core.DTO.AttributeDefinition attributeDefinitionDto:
                    var attributeDefinitionFactory = new AttributeDefinitionFactory();
                    return attributeDefinitionFactory.Create(attributeDefinitionDto);
                case Core.DTO.AttributeUsage attributeUsageDto:
                    var attributeUsageFactory = new AttributeUsageFactory();
                    return attributeUsageFactory.Create(attributeUsageDto);
                case Core.DTO.Behavior behaviorDto:
                    var behaviorFactory = new BehaviorFactory();
                    return behaviorFactory.Create(behaviorDto);
                case Core.DTO.BindingConnector bindingConnectorDto:
                    var bindingConnectorFactory = new BindingConnectorFactory();
                    return bindingConnectorFactory.Create(bindingConnectorDto);
                case Core.DTO.BindingConnectorAsUsage bindingConnectorAsUsageDto:
                    var bindingConnectorAsUsageFactory = new BindingConnectorAsUsageFactory();
                    return bindingConnectorAsUsageFactory.Create(bindingConnectorAsUsageDto);
                case Core.DTO.BooleanExpression booleanExpressionDto:
                    var booleanExpressionFactory = new BooleanExpressionFactory();
                    return booleanExpressionFactory.Create(booleanExpressionDto);
                case Core.DTO.CalculationDefinition calculationDefinitionDto:
                    var calculationDefinitionFactory = new CalculationDefinitionFactory();
                    return calculationDefinitionFactory.Create(calculationDefinitionDto);
                case Core.DTO.CalculationUsage calculationUsageDto:
                    var calculationUsageFactory = new CalculationUsageFactory();
                    return calculationUsageFactory.Create(calculationUsageDto);
                case Core.DTO.CaseDefinition caseDefinitionDto:
                    var caseDefinitionFactory = new CaseDefinitionFactory();
                    return caseDefinitionFactory.Create(caseDefinitionDto);
                case Core.DTO.CaseUsage caseUsageDto:
                    var caseUsageFactory = new CaseUsageFactory();
                    return caseUsageFactory.Create(caseUsageDto);
                case Core.DTO.Class classDto:
                    var classFactory = new ClassFactory();
                    return classFactory.Create(classDto);
                case Core.DTO.Classifier classifierDto:
                    var classifierFactory = new ClassifierFactory();
                    return classifierFactory.Create(classifierDto);
                case Core.DTO.CollectExpression collectExpressionDto:
                    var collectExpressionFactory = new CollectExpressionFactory();
                    return collectExpressionFactory.Create(collectExpressionDto);
                case Core.DTO.Comment commentDto:
                    var commentFactory = new CommentFactory();
                    return commentFactory.Create(commentDto);
                case Core.DTO.ConcernDefinition concernDefinitionDto:
                    var concernDefinitionFactory = new ConcernDefinitionFactory();
                    return concernDefinitionFactory.Create(concernDefinitionDto);
                case Core.DTO.ConcernUsage concernUsageDto:
                    var concernUsageFactory = new ConcernUsageFactory();
                    return concernUsageFactory.Create(concernUsageDto);
                case Core.DTO.ConjugatedPortDefinition conjugatedPortDefinitionDto:
                    var conjugatedPortDefinitionFactory = new ConjugatedPortDefinitionFactory();
                    return conjugatedPortDefinitionFactory.Create(conjugatedPortDefinitionDto);
                case Core.DTO.ConjugatedPortTyping conjugatedPortTypingDto:
                    var conjugatedPortTypingFactory = new ConjugatedPortTypingFactory();
                    return conjugatedPortTypingFactory.Create(conjugatedPortTypingDto);
                case Core.DTO.Conjugation conjugationDto:
                    var conjugationFactory = new ConjugationFactory();
                    return conjugationFactory.Create(conjugationDto);
                case Core.DTO.ConnectionDefinition connectionDefinitionDto:
                    var connectionDefinitionFactory = new ConnectionDefinitionFactory();
                    return connectionDefinitionFactory.Create(connectionDefinitionDto);
                case Core.DTO.ConnectionUsage connectionUsageDto:
                    var connectionUsageFactory = new ConnectionUsageFactory();
                    return connectionUsageFactory.Create(connectionUsageDto);
                case Core.DTO.Connector connectorDto:
                    var connectorFactory = new ConnectorFactory();
                    return connectorFactory.Create(connectorDto);
                case Core.DTO.ConstraintDefinition constraintDefinitionDto:
                    var constraintDefinitionFactory = new ConstraintDefinitionFactory();
                    return constraintDefinitionFactory.Create(constraintDefinitionDto);
                case Core.DTO.ConstraintUsage constraintUsageDto:
                    var constraintUsageFactory = new ConstraintUsageFactory();
                    return constraintUsageFactory.Create(constraintUsageDto);
                case Core.DTO.ConstructorExpression constructorExpressionDto:
                    var constructorExpressionFactory = new ConstructorExpressionFactory();
                    return constructorExpressionFactory.Create(constructorExpressionDto);
                case Core.DTO.CrossSubsetting crossSubsettingDto:
                    var crossSubsettingFactory = new CrossSubsettingFactory();
                    return crossSubsettingFactory.Create(crossSubsettingDto);
                case Core.DTO.DataType dataTypeDto:
                    var dataTypeFactory = new DataTypeFactory();
                    return dataTypeFactory.Create(dataTypeDto);
                case Core.DTO.DecisionNode decisionNodeDto:
                    var decisionNodeFactory = new DecisionNodeFactory();
                    return decisionNodeFactory.Create(decisionNodeDto);
                case Core.DTO.Definition definitionDto:
                    var definitionFactory = new DefinitionFactory();
                    return definitionFactory.Create(definitionDto);
                case Core.DTO.Dependency dependencyDto:
                    var dependencyFactory = new DependencyFactory();
                    return dependencyFactory.Create(dependencyDto);
                case Core.DTO.Differencing differencingDto:
                    var differencingFactory = new DifferencingFactory();
                    return differencingFactory.Create(differencingDto);
                case Core.DTO.Disjoining disjoiningDto:
                    var disjoiningFactory = new DisjoiningFactory();
                    return disjoiningFactory.Create(disjoiningDto);
                case Core.DTO.Documentation documentationDto:
                    var documentationFactory = new DocumentationFactory();
                    return documentationFactory.Create(documentationDto);
                case Core.DTO.ElementFilterMembership elementFilterMembershipDto:
                    var elementFilterMembershipFactory = new ElementFilterMembershipFactory();
                    return elementFilterMembershipFactory.Create(elementFilterMembershipDto);
                case Core.DTO.EndFeatureMembership endFeatureMembershipDto:
                    var endFeatureMembershipFactory = new EndFeatureMembershipFactory();
                    return endFeatureMembershipFactory.Create(endFeatureMembershipDto);
                case Core.DTO.EnumerationDefinition enumerationDefinitionDto:
                    var enumerationDefinitionFactory = new EnumerationDefinitionFactory();
                    return enumerationDefinitionFactory.Create(enumerationDefinitionDto);
                case Core.DTO.EnumerationUsage enumerationUsageDto:
                    var enumerationUsageFactory = new EnumerationUsageFactory();
                    return enumerationUsageFactory.Create(enumerationUsageDto);
                case Core.DTO.EventOccurrenceUsage eventOccurrenceUsageDto:
                    var eventOccurrenceUsageFactory = new EventOccurrenceUsageFactory();
                    return eventOccurrenceUsageFactory.Create(eventOccurrenceUsageDto);
                case Core.DTO.ExhibitStateUsage exhibitStateUsageDto:
                    var exhibitStateUsageFactory = new ExhibitStateUsageFactory();
                    return exhibitStateUsageFactory.Create(exhibitStateUsageDto);
                case Core.DTO.Expression expressionDto:
                    var expressionFactory = new ExpressionFactory();
                    return expressionFactory.Create(expressionDto);
                case Core.DTO.Feature featureDto:
                    var featureFactory = new FeatureFactory();
                    return featureFactory.Create(featureDto);
                case Core.DTO.FeatureChainExpression featureChainExpressionDto:
                    var featureChainExpressionFactory = new FeatureChainExpressionFactory();
                    return featureChainExpressionFactory.Create(featureChainExpressionDto);
                case Core.DTO.FeatureChaining featureChainingDto:
                    var featureChainingFactory = new FeatureChainingFactory();
                    return featureChainingFactory.Create(featureChainingDto);
                case Core.DTO.FeatureInverting featureInvertingDto:
                    var featureInvertingFactory = new FeatureInvertingFactory();
                    return featureInvertingFactory.Create(featureInvertingDto);
                case Core.DTO.FeatureMembership featureMembershipDto:
                    var featureMembershipFactory = new FeatureMembershipFactory();
                    return featureMembershipFactory.Create(featureMembershipDto);
                case Core.DTO.FeatureReferenceExpression featureReferenceExpressionDto:
                    var featureReferenceExpressionFactory = new FeatureReferenceExpressionFactory();
                    return featureReferenceExpressionFactory.Create(featureReferenceExpressionDto);
                case Core.DTO.FeatureTyping featureTypingDto:
                    var featureTypingFactory = new FeatureTypingFactory();
                    return featureTypingFactory.Create(featureTypingDto);
                case Core.DTO.FeatureValue featureValueDto:
                    var featureValueFactory = new FeatureValueFactory();
                    return featureValueFactory.Create(featureValueDto);
                case Core.DTO.Flow flowDto:
                    var flowFactory = new FlowFactory();
                    return flowFactory.Create(flowDto);
                case Core.DTO.FlowDefinition flowDefinitionDto:
                    var flowDefinitionFactory = new FlowDefinitionFactory();
                    return flowDefinitionFactory.Create(flowDefinitionDto);
                case Core.DTO.FlowEnd flowEndDto:
                    var flowEndFactory = new FlowEndFactory();
                    return flowEndFactory.Create(flowEndDto);
                case Core.DTO.FlowUsage flowUsageDto:
                    var flowUsageFactory = new FlowUsageFactory();
                    return flowUsageFactory.Create(flowUsageDto);
                case Core.DTO.ForkNode forkNodeDto:
                    var forkNodeFactory = new ForkNodeFactory();
                    return forkNodeFactory.Create(forkNodeDto);
                case Core.DTO.ForLoopActionUsage forLoopActionUsageDto:
                    var forLoopActionUsageFactory = new ForLoopActionUsageFactory();
                    return forLoopActionUsageFactory.Create(forLoopActionUsageDto);
                case Core.DTO.FramedConcernMembership framedConcernMembershipDto:
                    var framedConcernMembershipFactory = new FramedConcernMembershipFactory();
                    return framedConcernMembershipFactory.Create(framedConcernMembershipDto);
                case Core.DTO.Function functionDto:
                    var functionFactory = new FunctionFactory();
                    return functionFactory.Create(functionDto);
                case Core.DTO.IfActionUsage ifActionUsageDto:
                    var ifActionUsageFactory = new IfActionUsageFactory();
                    return ifActionUsageFactory.Create(ifActionUsageDto);
                case Core.DTO.IncludeUseCaseUsage includeUseCaseUsageDto:
                    var includeUseCaseUsageFactory = new IncludeUseCaseUsageFactory();
                    return includeUseCaseUsageFactory.Create(includeUseCaseUsageDto);
                case Core.DTO.IndexExpression indexExpressionDto:
                    var indexExpressionFactory = new IndexExpressionFactory();
                    return indexExpressionFactory.Create(indexExpressionDto);
                case Core.DTO.Interaction interactionDto:
                    var interactionFactory = new InteractionFactory();
                    return interactionFactory.Create(interactionDto);
                case Core.DTO.InterfaceDefinition interfaceDefinitionDto:
                    var interfaceDefinitionFactory = new InterfaceDefinitionFactory();
                    return interfaceDefinitionFactory.Create(interfaceDefinitionDto);
                case Core.DTO.InterfaceUsage interfaceUsageDto:
                    var interfaceUsageFactory = new InterfaceUsageFactory();
                    return interfaceUsageFactory.Create(interfaceUsageDto);
                case Core.DTO.Intersecting intersectingDto:
                    var intersectingFactory = new IntersectingFactory();
                    return intersectingFactory.Create(intersectingDto);
                case Core.DTO.Invariant invariantDto:
                    var invariantFactory = new InvariantFactory();
                    return invariantFactory.Create(invariantDto);
                case Core.DTO.InvocationExpression invocationExpressionDto:
                    var invocationExpressionFactory = new InvocationExpressionFactory();
                    return invocationExpressionFactory.Create(invocationExpressionDto);
                case Core.DTO.ItemDefinition itemDefinitionDto:
                    var itemDefinitionFactory = new ItemDefinitionFactory();
                    return itemDefinitionFactory.Create(itemDefinitionDto);
                case Core.DTO.ItemUsage itemUsageDto:
                    var itemUsageFactory = new ItemUsageFactory();
                    return itemUsageFactory.Create(itemUsageDto);
                case Core.DTO.JoinNode joinNodeDto:
                    var joinNodeFactory = new JoinNodeFactory();
                    return joinNodeFactory.Create(joinNodeDto);
                case Core.DTO.LibraryPackage libraryPackageDto:
                    var libraryPackageFactory = new LibraryPackageFactory();
                    return libraryPackageFactory.Create(libraryPackageDto);
                case Core.DTO.LiteralBoolean literalBooleanDto:
                    var literalBooleanFactory = new LiteralBooleanFactory();
                    return literalBooleanFactory.Create(literalBooleanDto);
                case Core.DTO.LiteralExpression literalExpressionDto:
                    var literalExpressionFactory = new LiteralExpressionFactory();
                    return literalExpressionFactory.Create(literalExpressionDto);
                case Core.DTO.LiteralInfinity literalInfinityDto:
                    var literalInfinityFactory = new LiteralInfinityFactory();
                    return literalInfinityFactory.Create(literalInfinityDto);
                case Core.DTO.LiteralInteger literalIntegerDto:
                    var literalIntegerFactory = new LiteralIntegerFactory();
                    return literalIntegerFactory.Create(literalIntegerDto);
                case Core.DTO.LiteralRational literalRationalDto:
                    var literalRationalFactory = new LiteralRationalFactory();
                    return literalRationalFactory.Create(literalRationalDto);
                case Core.DTO.LiteralString literalStringDto:
                    var literalStringFactory = new LiteralStringFactory();
                    return literalStringFactory.Create(literalStringDto);
                case Core.DTO.Membership membershipDto:
                    var membershipFactory = new MembershipFactory();
                    return membershipFactory.Create(membershipDto);
                case Core.DTO.MembershipExpose membershipExposeDto:
                    var membershipExposeFactory = new MembershipExposeFactory();
                    return membershipExposeFactory.Create(membershipExposeDto);
                case Core.DTO.MembershipImport membershipImportDto:
                    var membershipImportFactory = new MembershipImportFactory();
                    return membershipImportFactory.Create(membershipImportDto);
                case Core.DTO.MergeNode mergeNodeDto:
                    var mergeNodeFactory = new MergeNodeFactory();
                    return mergeNodeFactory.Create(mergeNodeDto);
                case Core.DTO.Metaclass metaclassDto:
                    var metaclassFactory = new MetaclassFactory();
                    return metaclassFactory.Create(metaclassDto);
                case Core.DTO.MetadataAccessExpression metadataAccessExpressionDto:
                    var metadataAccessExpressionFactory = new MetadataAccessExpressionFactory();
                    return metadataAccessExpressionFactory.Create(metadataAccessExpressionDto);
                case Core.DTO.MetadataDefinition metadataDefinitionDto:
                    var metadataDefinitionFactory = new MetadataDefinitionFactory();
                    return metadataDefinitionFactory.Create(metadataDefinitionDto);
                case Core.DTO.MetadataFeature metadataFeatureDto:
                    var metadataFeatureFactory = new MetadataFeatureFactory();
                    return metadataFeatureFactory.Create(metadataFeatureDto);
                case Core.DTO.MetadataUsage metadataUsageDto:
                    var metadataUsageFactory = new MetadataUsageFactory();
                    return metadataUsageFactory.Create(metadataUsageDto);
                case Core.DTO.Multiplicity multiplicityDto:
                    var multiplicityFactory = new MultiplicityFactory();
                    return multiplicityFactory.Create(multiplicityDto);
                case Core.DTO.MultiplicityRange multiplicityRangeDto:
                    var multiplicityRangeFactory = new MultiplicityRangeFactory();
                    return multiplicityRangeFactory.Create(multiplicityRangeDto);
                case Core.DTO.Namespace namespaceDto:
                    var namespaceFactory = new NamespaceFactory();
                    return namespaceFactory.Create(namespaceDto);
                case Core.DTO.NamespaceExpose namespaceExposeDto:
                    var namespaceExposeFactory = new NamespaceExposeFactory();
                    return namespaceExposeFactory.Create(namespaceExposeDto);
                case Core.DTO.NamespaceImport namespaceImportDto:
                    var namespaceImportFactory = new NamespaceImportFactory();
                    return namespaceImportFactory.Create(namespaceImportDto);
                case Core.DTO.NullExpression nullExpressionDto:
                    var nullExpressionFactory = new NullExpressionFactory();
                    return nullExpressionFactory.Create(nullExpressionDto);
                case Core.DTO.ObjectiveMembership objectiveMembershipDto:
                    var objectiveMembershipFactory = new ObjectiveMembershipFactory();
                    return objectiveMembershipFactory.Create(objectiveMembershipDto);
                case Core.DTO.OccurrenceDefinition occurrenceDefinitionDto:
                    var occurrenceDefinitionFactory = new OccurrenceDefinitionFactory();
                    return occurrenceDefinitionFactory.Create(occurrenceDefinitionDto);
                case Core.DTO.OccurrenceUsage occurrenceUsageDto:
                    var occurrenceUsageFactory = new OccurrenceUsageFactory();
                    return occurrenceUsageFactory.Create(occurrenceUsageDto);
                case Core.DTO.OperatorExpression operatorExpressionDto:
                    var operatorExpressionFactory = new OperatorExpressionFactory();
                    return operatorExpressionFactory.Create(operatorExpressionDto);
                case Core.DTO.OwningMembership owningMembershipDto:
                    var owningMembershipFactory = new OwningMembershipFactory();
                    return owningMembershipFactory.Create(owningMembershipDto);
                case Core.DTO.Package packageDto:
                    var packageFactory = new PackageFactory();
                    return packageFactory.Create(packageDto);
                case Core.DTO.ParameterMembership parameterMembershipDto:
                    var parameterMembershipFactory = new ParameterMembershipFactory();
                    return parameterMembershipFactory.Create(parameterMembershipDto);
                case Core.DTO.PartDefinition partDefinitionDto:
                    var partDefinitionFactory = new PartDefinitionFactory();
                    return partDefinitionFactory.Create(partDefinitionDto);
                case Core.DTO.PartUsage partUsageDto:
                    var partUsageFactory = new PartUsageFactory();
                    return partUsageFactory.Create(partUsageDto);
                case Core.DTO.PayloadFeature payloadFeatureDto:
                    var payloadFeatureFactory = new PayloadFeatureFactory();
                    return payloadFeatureFactory.Create(payloadFeatureDto);
                case Core.DTO.PerformActionUsage performActionUsageDto:
                    var performActionUsageFactory = new PerformActionUsageFactory();
                    return performActionUsageFactory.Create(performActionUsageDto);
                case Core.DTO.PortConjugation portConjugationDto:
                    var portConjugationFactory = new PortConjugationFactory();
                    return portConjugationFactory.Create(portConjugationDto);
                case Core.DTO.PortDefinition portDefinitionDto:
                    var portDefinitionFactory = new PortDefinitionFactory();
                    return portDefinitionFactory.Create(portDefinitionDto);
                case Core.DTO.PortUsage portUsageDto:
                    var portUsageFactory = new PortUsageFactory();
                    return portUsageFactory.Create(portUsageDto);
                case Core.DTO.Predicate predicateDto:
                    var predicateFactory = new PredicateFactory();
                    return predicateFactory.Create(predicateDto);
                case Core.DTO.Redefinition redefinitionDto:
                    var redefinitionFactory = new RedefinitionFactory();
                    return redefinitionFactory.Create(redefinitionDto);
                case Core.DTO.ReferenceSubsetting referenceSubsettingDto:
                    var referenceSubsettingFactory = new ReferenceSubsettingFactory();
                    return referenceSubsettingFactory.Create(referenceSubsettingDto);
                case Core.DTO.ReferenceUsage referenceUsageDto:
                    var referenceUsageFactory = new ReferenceUsageFactory();
                    return referenceUsageFactory.Create(referenceUsageDto);
                case Core.DTO.RenderingDefinition renderingDefinitionDto:
                    var renderingDefinitionFactory = new RenderingDefinitionFactory();
                    return renderingDefinitionFactory.Create(renderingDefinitionDto);
                case Core.DTO.RenderingUsage renderingUsageDto:
                    var renderingUsageFactory = new RenderingUsageFactory();
                    return renderingUsageFactory.Create(renderingUsageDto);
                case Core.DTO.RequirementConstraintMembership requirementConstraintMembershipDto:
                    var requirementConstraintMembershipFactory = new RequirementConstraintMembershipFactory();
                    return requirementConstraintMembershipFactory.Create(requirementConstraintMembershipDto);
                case Core.DTO.RequirementDefinition requirementDefinitionDto:
                    var requirementDefinitionFactory = new RequirementDefinitionFactory();
                    return requirementDefinitionFactory.Create(requirementDefinitionDto);
                case Core.DTO.RequirementUsage requirementUsageDto:
                    var requirementUsageFactory = new RequirementUsageFactory();
                    return requirementUsageFactory.Create(requirementUsageDto);
                case Core.DTO.RequirementVerificationMembership requirementVerificationMembershipDto:
                    var requirementVerificationMembershipFactory = new RequirementVerificationMembershipFactory();
                    return requirementVerificationMembershipFactory.Create(requirementVerificationMembershipDto);
                case Core.DTO.ResultExpressionMembership resultExpressionMembershipDto:
                    var resultExpressionMembershipFactory = new ResultExpressionMembershipFactory();
                    return resultExpressionMembershipFactory.Create(resultExpressionMembershipDto);
                case Core.DTO.ReturnParameterMembership returnParameterMembershipDto:
                    var returnParameterMembershipFactory = new ReturnParameterMembershipFactory();
                    return returnParameterMembershipFactory.Create(returnParameterMembershipDto);
                case Core.DTO.SatisfyRequirementUsage satisfyRequirementUsageDto:
                    var satisfyRequirementUsageFactory = new SatisfyRequirementUsageFactory();
                    return satisfyRequirementUsageFactory.Create(satisfyRequirementUsageDto);
                case Core.DTO.SelectExpression selectExpressionDto:
                    var selectExpressionFactory = new SelectExpressionFactory();
                    return selectExpressionFactory.Create(selectExpressionDto);
                case Core.DTO.SendActionUsage sendActionUsageDto:
                    var sendActionUsageFactory = new SendActionUsageFactory();
                    return sendActionUsageFactory.Create(sendActionUsageDto);
                case Core.DTO.Specialization specializationDto:
                    var specializationFactory = new SpecializationFactory();
                    return specializationFactory.Create(specializationDto);
                case Core.DTO.StakeholderMembership stakeholderMembershipDto:
                    var stakeholderMembershipFactory = new StakeholderMembershipFactory();
                    return stakeholderMembershipFactory.Create(stakeholderMembershipDto);
                case Core.DTO.StateDefinition stateDefinitionDto:
                    var stateDefinitionFactory = new StateDefinitionFactory();
                    return stateDefinitionFactory.Create(stateDefinitionDto);
                case Core.DTO.StateSubactionMembership stateSubactionMembershipDto:
                    var stateSubactionMembershipFactory = new StateSubactionMembershipFactory();
                    return stateSubactionMembershipFactory.Create(stateSubactionMembershipDto);
                case Core.DTO.StateUsage stateUsageDto:
                    var stateUsageFactory = new StateUsageFactory();
                    return stateUsageFactory.Create(stateUsageDto);
                case Core.DTO.Step stepDto:
                    var stepFactory = new StepFactory();
                    return stepFactory.Create(stepDto);
                case Core.DTO.Structure structureDto:
                    var structureFactory = new StructureFactory();
                    return structureFactory.Create(structureDto);
                case Core.DTO.Subclassification subclassificationDto:
                    var subclassificationFactory = new SubclassificationFactory();
                    return subclassificationFactory.Create(subclassificationDto);
                case Core.DTO.SubjectMembership subjectMembershipDto:
                    var subjectMembershipFactory = new SubjectMembershipFactory();
                    return subjectMembershipFactory.Create(subjectMembershipDto);
                case Core.DTO.Subsetting subsettingDto:
                    var subsettingFactory = new SubsettingFactory();
                    return subsettingFactory.Create(subsettingDto);
                case Core.DTO.Succession successionDto:
                    var successionFactory = new SuccessionFactory();
                    return successionFactory.Create(successionDto);
                case Core.DTO.SuccessionAsUsage successionAsUsageDto:
                    var successionAsUsageFactory = new SuccessionAsUsageFactory();
                    return successionAsUsageFactory.Create(successionAsUsageDto);
                case Core.DTO.SuccessionFlow successionFlowDto:
                    var successionFlowFactory = new SuccessionFlowFactory();
                    return successionFlowFactory.Create(successionFlowDto);
                case Core.DTO.SuccessionFlowUsage successionFlowUsageDto:
                    var successionFlowUsageFactory = new SuccessionFlowUsageFactory();
                    return successionFlowUsageFactory.Create(successionFlowUsageDto);
                case Core.DTO.TerminateActionUsage terminateActionUsageDto:
                    var terminateActionUsageFactory = new TerminateActionUsageFactory();
                    return terminateActionUsageFactory.Create(terminateActionUsageDto);
                case Core.DTO.TextualRepresentation textualRepresentationDto:
                    var textualRepresentationFactory = new TextualRepresentationFactory();
                    return textualRepresentationFactory.Create(textualRepresentationDto);
                case Core.DTO.TransitionFeatureMembership transitionFeatureMembershipDto:
                    var transitionFeatureMembershipFactory = new TransitionFeatureMembershipFactory();
                    return transitionFeatureMembershipFactory.Create(transitionFeatureMembershipDto);
                case Core.DTO.TransitionUsage transitionUsageDto:
                    var transitionUsageFactory = new TransitionUsageFactory();
                    return transitionUsageFactory.Create(transitionUsageDto);
                case Core.DTO.TriggerInvocationExpression triggerInvocationExpressionDto:
                    var triggerInvocationExpressionFactory = new TriggerInvocationExpressionFactory();
                    return triggerInvocationExpressionFactory.Create(triggerInvocationExpressionDto);
                case Core.DTO.Type typeDto:
                    var typeFactory = new TypeFactory();
                    return typeFactory.Create(typeDto);
                case Core.DTO.TypeFeaturing typeFeaturingDto:
                    var typeFeaturingFactory = new TypeFeaturingFactory();
                    return typeFeaturingFactory.Create(typeFeaturingDto);
                case Core.DTO.Unioning unioningDto:
                    var unioningFactory = new UnioningFactory();
                    return unioningFactory.Create(unioningDto);
                case Core.DTO.Usage usageDto:
                    var usageFactory = new UsageFactory();
                    return usageFactory.Create(usageDto);
                case Core.DTO.UseCaseDefinition useCaseDefinitionDto:
                    var useCaseDefinitionFactory = new UseCaseDefinitionFactory();
                    return useCaseDefinitionFactory.Create(useCaseDefinitionDto);
                case Core.DTO.UseCaseUsage useCaseUsageDto:
                    var useCaseUsageFactory = new UseCaseUsageFactory();
                    return useCaseUsageFactory.Create(useCaseUsageDto);
                case Core.DTO.VariantMembership variantMembershipDto:
                    var variantMembershipFactory = new VariantMembershipFactory();
                    return variantMembershipFactory.Create(variantMembershipDto);
                case Core.DTO.VerificationCaseDefinition verificationCaseDefinitionDto:
                    var verificationCaseDefinitionFactory = new VerificationCaseDefinitionFactory();
                    return verificationCaseDefinitionFactory.Create(verificationCaseDefinitionDto);
                case Core.DTO.VerificationCaseUsage verificationCaseUsageDto:
                    var verificationCaseUsageFactory = new VerificationCaseUsageFactory();
                    return verificationCaseUsageFactory.Create(verificationCaseUsageDto);
                case Core.DTO.ViewDefinition viewDefinitionDto:
                    var viewDefinitionFactory = new ViewDefinitionFactory();
                    return viewDefinitionFactory.Create(viewDefinitionDto);
                case Core.DTO.ViewpointDefinition viewpointDefinitionDto:
                    var viewpointDefinitionFactory = new ViewpointDefinitionFactory();
                    return viewpointDefinitionFactory.Create(viewpointDefinitionDto);
                case Core.DTO.ViewpointUsage viewpointUsageDto:
                    var viewpointUsageFactory = new ViewpointUsageFactory();
                    return viewpointUsageFactory.Create(viewpointUsageDto);
                case Core.DTO.ViewRenderingMembership viewRenderingMembershipDto:
                    var viewRenderingMembershipFactory = new ViewRenderingMembershipFactory();
                    return viewRenderingMembershipFactory.Create(viewRenderingMembershipDto);
                case Core.DTO.ViewUsage viewUsageDto:
                    var viewUsageFactory = new ViewUsageFactory();
                    return viewUsageFactory.Create(viewUsageDto);
                case Core.DTO.WhileLoopActionUsage whileLoopActionUsageDto:
                    var whileLoopActionUsageFactory = new WhileLoopActionUsageFactory();
                    return whileLoopActionUsageFactory.Create(whileLoopActionUsageDto);
                default:
                    throw new NotSupportedException($"{dto.GetType().Name} not yet supported");
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
