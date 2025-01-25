// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="Starion Group S.A.">
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A static class that provides extension methods for the <see cref="Core.POCO.IElement"/> class
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Updates the value properties of the <see cref="Core.POCO.IElement"/> by setting the value equal to that of the dto.
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from containment properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.IElement"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.POCO.IElement"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from containment properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.IElement poco, Core.DTO.IElement dto)
        {
            if (poco == null)
            {
                throw new ArgumentNullException(nameof(poco), $"the {nameof(poco)} may not be null");
            }

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            switch (poco)
            {
                case Core.POCO.AcceptActionUsage acceptActionUsagePoco:
                    return acceptActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AcceptActionUsage)dto);
                case Core.POCO.ActionDefinition actionDefinitionPoco:
                    return actionDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ActionDefinition)dto);
                case Core.POCO.ActionUsage actionUsagePoco:
                    return actionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ActionUsage)dto);
                case Core.POCO.ActorMembership actorMembershipPoco:
                    return actorMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ActorMembership)dto);
                case Core.POCO.AllocationDefinition allocationDefinitionPoco:
                    return allocationDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AllocationDefinition)dto);
                case Core.POCO.AllocationUsage allocationUsagePoco:
                    return allocationUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AllocationUsage)dto);
                case Core.POCO.AnalysisCaseDefinition analysisCaseDefinitionPoco:
                    return analysisCaseDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AnalysisCaseDefinition)dto);
                case Core.POCO.AnalysisCaseUsage analysisCaseUsagePoco:
                    return analysisCaseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AnalysisCaseUsage)dto);
                case Core.POCO.AnnotatingElement annotatingElementPoco:
                    return annotatingElementPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AnnotatingElement)dto);
                case Core.POCO.Annotation annotationPoco:
                    return annotationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Annotation)dto);
                case Core.POCO.AssertConstraintUsage assertConstraintUsagePoco:
                    return assertConstraintUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AssertConstraintUsage)dto);
                case Core.POCO.AssignmentActionUsage assignmentActionUsagePoco:
                    return assignmentActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AssignmentActionUsage)dto);
                case Core.POCO.Association associationPoco:
                    return associationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Association)dto);
                case Core.POCO.AssociationStructure associationStructurePoco:
                    return associationStructurePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AssociationStructure)dto);
                case Core.POCO.AttributeDefinition attributeDefinitionPoco:
                    return attributeDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AttributeDefinition)dto);
                case Core.POCO.AttributeUsage attributeUsagePoco:
                    return attributeUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.AttributeUsage)dto);
                case Core.POCO.Behavior behaviorPoco:
                    return behaviorPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Behavior)dto);
                case Core.POCO.BindingConnector bindingConnectorPoco:
                    return bindingConnectorPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.BindingConnector)dto);
                case Core.POCO.BindingConnectorAsUsage bindingConnectorAsUsagePoco:
                    return bindingConnectorAsUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.BindingConnectorAsUsage)dto);
                case Core.POCO.BooleanExpression booleanExpressionPoco:
                    return booleanExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.BooleanExpression)dto);
                case Core.POCO.CalculationDefinition calculationDefinitionPoco:
                    return calculationDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.CalculationDefinition)dto);
                case Core.POCO.CalculationUsage calculationUsagePoco:
                    return calculationUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.CalculationUsage)dto);
                case Core.POCO.CaseDefinition caseDefinitionPoco:
                    return caseDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.CaseDefinition)dto);
                case Core.POCO.CaseUsage caseUsagePoco:
                    return caseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.CaseUsage)dto);
                case Core.POCO.Class classPoco:
                    return classPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Class)dto);
                case Core.POCO.Classifier classifierPoco:
                    return classifierPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Classifier)dto);
                case Core.POCO.CollectExpression collectExpressionPoco:
                    return collectExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.CollectExpression)dto);
                case Core.POCO.Comment commentPoco:
                    return commentPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Comment)dto);
                case Core.POCO.ConcernDefinition concernDefinitionPoco:
                    return concernDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ConcernDefinition)dto);
                case Core.POCO.ConcernUsage concernUsagePoco:
                    return concernUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ConcernUsage)dto);
                case Core.POCO.ConjugatedPortDefinition conjugatedPortDefinitionPoco:
                    return conjugatedPortDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ConjugatedPortDefinition)dto);
                case Core.POCO.ConjugatedPortTyping conjugatedPortTypingPoco:
                    return conjugatedPortTypingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ConjugatedPortTyping)dto);
                case Core.POCO.Conjugation conjugationPoco:
                    return conjugationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Conjugation)dto);
                case Core.POCO.ConnectionDefinition connectionDefinitionPoco:
                    return connectionDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ConnectionDefinition)dto);
                case Core.POCO.ConnectionUsage connectionUsagePoco:
                    return connectionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ConnectionUsage)dto);
                case Core.POCO.Connector connectorPoco:
                    return connectorPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Connector)dto);
                case Core.POCO.ConstraintDefinition constraintDefinitionPoco:
                    return constraintDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ConstraintDefinition)dto);
                case Core.POCO.ConstraintUsage constraintUsagePoco:
                    return constraintUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ConstraintUsage)dto);
                case Core.POCO.CrossSubsetting crossSubsettingPoco:
                    return crossSubsettingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.CrossSubsetting)dto);
                case Core.POCO.DataType dataTypePoco:
                    return dataTypePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.DataType)dto);
                case Core.POCO.DecisionNode decisionNodePoco:
                    return decisionNodePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.DecisionNode)dto);
                case Core.POCO.Definition definitionPoco:
                    return definitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Definition)dto);
                case Core.POCO.Dependency dependencyPoco:
                    return dependencyPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Dependency)dto);
                case Core.POCO.Differencing differencingPoco:
                    return differencingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Differencing)dto);
                case Core.POCO.Disjoining disjoiningPoco:
                    return disjoiningPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Disjoining)dto);
                case Core.POCO.Documentation documentationPoco:
                    return documentationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Documentation)dto);
                case Core.POCO.ElementFilterMembership elementFilterMembershipPoco:
                    return elementFilterMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ElementFilterMembership)dto);
                case Core.POCO.EndFeatureMembership endFeatureMembershipPoco:
                    return endFeatureMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.EndFeatureMembership)dto);
                case Core.POCO.EnumerationDefinition enumerationDefinitionPoco:
                    return enumerationDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.EnumerationDefinition)dto);
                case Core.POCO.EnumerationUsage enumerationUsagePoco:
                    return enumerationUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.EnumerationUsage)dto);
                case Core.POCO.EventOccurrenceUsage eventOccurrenceUsagePoco:
                    return eventOccurrenceUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.EventOccurrenceUsage)dto);
                case Core.POCO.ExhibitStateUsage exhibitStateUsagePoco:
                    return exhibitStateUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ExhibitStateUsage)dto);
                case Core.POCO.Expression expressionPoco:
                    return expressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Expression)dto);
                case Core.POCO.Feature featurePoco:
                    return featurePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Feature)dto);
                case Core.POCO.FeatureChainExpression featureChainExpressionPoco:
                    return featureChainExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FeatureChainExpression)dto);
                case Core.POCO.FeatureChaining featureChainingPoco:
                    return featureChainingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FeatureChaining)dto);
                case Core.POCO.FeatureInverting featureInvertingPoco:
                    return featureInvertingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FeatureInverting)dto);
                case Core.POCO.FeatureMembership featureMembershipPoco:
                    return featureMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FeatureMembership)dto);
                case Core.POCO.FeatureReferenceExpression featureReferenceExpressionPoco:
                    return featureReferenceExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FeatureReferenceExpression)dto);
                case Core.POCO.FeatureTyping featureTypingPoco:
                    return featureTypingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FeatureTyping)dto);
                case Core.POCO.FeatureValue featureValuePoco:
                    return featureValuePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FeatureValue)dto);
                case Core.POCO.FlowConnectionDefinition flowConnectionDefinitionPoco:
                    return flowConnectionDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FlowConnectionDefinition)dto);
                case Core.POCO.FlowConnectionUsage flowConnectionUsagePoco:
                    return flowConnectionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FlowConnectionUsage)dto);
                case Core.POCO.ForkNode forkNodePoco:
                    return forkNodePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ForkNode)dto);
                case Core.POCO.ForLoopActionUsage forLoopActionUsagePoco:
                    return forLoopActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ForLoopActionUsage)dto);
                case Core.POCO.FramedConcernMembership framedConcernMembershipPoco:
                    return framedConcernMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.FramedConcernMembership)dto);
                case Core.POCO.Function functionPoco:
                    return functionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Function)dto);
                case Core.POCO.IfActionUsage ifActionUsagePoco:
                    return ifActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.IfActionUsage)dto);
                case Core.POCO.IncludeUseCaseUsage includeUseCaseUsagePoco:
                    return includeUseCaseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.IncludeUseCaseUsage)dto);
                case Core.POCO.IndexExpression indexExpressionPoco:
                    return indexExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.IndexExpression)dto);
                case Core.POCO.Interaction interactionPoco:
                    return interactionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Interaction)dto);
                case Core.POCO.InterfaceDefinition interfaceDefinitionPoco:
                    return interfaceDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.InterfaceDefinition)dto);
                case Core.POCO.InterfaceUsage interfaceUsagePoco:
                    return interfaceUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.InterfaceUsage)dto);
                case Core.POCO.Intersecting intersectingPoco:
                    return intersectingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Intersecting)dto);
                case Core.POCO.Invariant invariantPoco:
                    return invariantPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Invariant)dto);
                case Core.POCO.InvocationExpression invocationExpressionPoco:
                    return invocationExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.InvocationExpression)dto);
                case Core.POCO.ItemDefinition itemDefinitionPoco:
                    return itemDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ItemDefinition)dto);
                case Core.POCO.ItemFeature itemFeaturePoco:
                    return itemFeaturePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ItemFeature)dto);
                case Core.POCO.ItemFlow itemFlowPoco:
                    return itemFlowPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ItemFlow)dto);
                case Core.POCO.ItemFlowEnd itemFlowEndPoco:
                    return itemFlowEndPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ItemFlowEnd)dto);
                case Core.POCO.ItemUsage itemUsagePoco:
                    return itemUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ItemUsage)dto);
                case Core.POCO.JoinNode joinNodePoco:
                    return joinNodePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.JoinNode)dto);
                case Core.POCO.LibraryPackage libraryPackagePoco:
                    return libraryPackagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.LibraryPackage)dto);
                case Core.POCO.LifeClass lifeClassPoco:
                    return lifeClassPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.LifeClass)dto);
                case Core.POCO.LiteralBoolean literalBooleanPoco:
                    return literalBooleanPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.LiteralBoolean)dto);
                case Core.POCO.LiteralExpression literalExpressionPoco:
                    return literalExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.LiteralExpression)dto);
                case Core.POCO.LiteralInfinity literalInfinityPoco:
                    return literalInfinityPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.LiteralInfinity)dto);
                case Core.POCO.LiteralInteger literalIntegerPoco:
                    return literalIntegerPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.LiteralInteger)dto);
                case Core.POCO.LiteralRational literalRationalPoco:
                    return literalRationalPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.LiteralRational)dto);
                case Core.POCO.LiteralString literalStringPoco:
                    return literalStringPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.LiteralString)dto);
                case Core.POCO.Membership membershipPoco:
                    return membershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Membership)dto);
                case Core.POCO.MembershipExpose membershipExposePoco:
                    return membershipExposePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.MembershipExpose)dto);
                case Core.POCO.MembershipImport membershipImportPoco:
                    return membershipImportPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.MembershipImport)dto);
                case Core.POCO.MergeNode mergeNodePoco:
                    return mergeNodePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.MergeNode)dto);
                case Core.POCO.Metaclass metaclassPoco:
                    return metaclassPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Metaclass)dto);
                case Core.POCO.MetadataAccessExpression metadataAccessExpressionPoco:
                    return metadataAccessExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.MetadataAccessExpression)dto);
                case Core.POCO.MetadataDefinition metadataDefinitionPoco:
                    return metadataDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.MetadataDefinition)dto);
                case Core.POCO.MetadataFeature metadataFeaturePoco:
                    return metadataFeaturePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.MetadataFeature)dto);
                case Core.POCO.MetadataUsage metadataUsagePoco:
                    return metadataUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.MetadataUsage)dto);
                case Core.POCO.Multiplicity multiplicityPoco:
                    return multiplicityPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Multiplicity)dto);
                case Core.POCO.MultiplicityRange multiplicityRangePoco:
                    return multiplicityRangePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.MultiplicityRange)dto);
                case Core.POCO.Namespace namespacePoco:
                    return namespacePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Namespace)dto);
                case Core.POCO.NamespaceExpose namespaceExposePoco:
                    return namespaceExposePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.NamespaceExpose)dto);
                case Core.POCO.NamespaceImport namespaceImportPoco:
                    return namespaceImportPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.NamespaceImport)dto);
                case Core.POCO.NullExpression nullExpressionPoco:
                    return nullExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.NullExpression)dto);
                case Core.POCO.ObjectiveMembership objectiveMembershipPoco:
                    return objectiveMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ObjectiveMembership)dto);
                case Core.POCO.OccurrenceDefinition occurrenceDefinitionPoco:
                    return occurrenceDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.OccurrenceDefinition)dto);
                case Core.POCO.OccurrenceUsage occurrenceUsagePoco:
                    return occurrenceUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.OccurrenceUsage)dto);
                case Core.POCO.OperatorExpression operatorExpressionPoco:
                    return operatorExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.OperatorExpression)dto);
                case Core.POCO.OwningMembership owningMembershipPoco:
                    return owningMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.OwningMembership)dto);
                case Core.POCO.Package packagePoco:
                    return packagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Package)dto);
                case Core.POCO.ParameterMembership parameterMembershipPoco:
                    return parameterMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ParameterMembership)dto);
                case Core.POCO.PartDefinition partDefinitionPoco:
                    return partDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.PartDefinition)dto);
                case Core.POCO.PartUsage partUsagePoco:
                    return partUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.PartUsage)dto);
                case Core.POCO.PerformActionUsage performActionUsagePoco:
                    return performActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.PerformActionUsage)dto);
                case Core.POCO.PortConjugation portConjugationPoco:
                    return portConjugationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.PortConjugation)dto);
                case Core.POCO.PortDefinition portDefinitionPoco:
                    return portDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.PortDefinition)dto);
                case Core.POCO.PortUsage portUsagePoco:
                    return portUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.PortUsage)dto);
                case Core.POCO.Predicate predicatePoco:
                    return predicatePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Predicate)dto);
                case Core.POCO.Redefinition redefinitionPoco:
                    return redefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Redefinition)dto);
                case Core.POCO.ReferenceSubsetting referenceSubsettingPoco:
                    return referenceSubsettingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ReferenceSubsetting)dto);
                case Core.POCO.ReferenceUsage referenceUsagePoco:
                    return referenceUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ReferenceUsage)dto);
                case Core.POCO.RenderingDefinition renderingDefinitionPoco:
                    return renderingDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.RenderingDefinition)dto);
                case Core.POCO.RenderingUsage renderingUsagePoco:
                    return renderingUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.RenderingUsage)dto);
                case Core.POCO.RequirementConstraintMembership requirementConstraintMembershipPoco:
                    return requirementConstraintMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.RequirementConstraintMembership)dto);
                case Core.POCO.RequirementDefinition requirementDefinitionPoco:
                    return requirementDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.RequirementDefinition)dto);
                case Core.POCO.RequirementUsage requirementUsagePoco:
                    return requirementUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.RequirementUsage)dto);
                case Core.POCO.RequirementVerificationMembership requirementVerificationMembershipPoco:
                    return requirementVerificationMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.RequirementVerificationMembership)dto);
                case Core.POCO.ResultExpressionMembership resultExpressionMembershipPoco:
                    return resultExpressionMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ResultExpressionMembership)dto);
                case Core.POCO.ReturnParameterMembership returnParameterMembershipPoco:
                    return returnParameterMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ReturnParameterMembership)dto);
                case Core.POCO.SatisfyRequirementUsage satisfyRequirementUsagePoco:
                    return satisfyRequirementUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.SatisfyRequirementUsage)dto);
                case Core.POCO.SelectExpression selectExpressionPoco:
                    return selectExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.SelectExpression)dto);
                case Core.POCO.SendActionUsage sendActionUsagePoco:
                    return sendActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.SendActionUsage)dto);
                case Core.POCO.Specialization specializationPoco:
                    return specializationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Specialization)dto);
                case Core.POCO.StakeholderMembership stakeholderMembershipPoco:
                    return stakeholderMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.StakeholderMembership)dto);
                case Core.POCO.StateDefinition stateDefinitionPoco:
                    return stateDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.StateDefinition)dto);
                case Core.POCO.StateSubactionMembership stateSubactionMembershipPoco:
                    return stateSubactionMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.StateSubactionMembership)dto);
                case Core.POCO.StateUsage stateUsagePoco:
                    return stateUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.StateUsage)dto);
                case Core.POCO.Step stepPoco:
                    return stepPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Step)dto);
                case Core.POCO.Structure structurePoco:
                    return structurePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Structure)dto);
                case Core.POCO.Subclassification subclassificationPoco:
                    return subclassificationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Subclassification)dto);
                case Core.POCO.SubjectMembership subjectMembershipPoco:
                    return subjectMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.SubjectMembership)dto);
                case Core.POCO.Subsetting subsettingPoco:
                    return subsettingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Subsetting)dto);
                case Core.POCO.Succession successionPoco:
                    return successionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Succession)dto);
                case Core.POCO.SuccessionAsUsage successionAsUsagePoco:
                    return successionAsUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.SuccessionAsUsage)dto);
                case Core.POCO.SuccessionFlowConnectionUsage successionFlowConnectionUsagePoco:
                    return successionFlowConnectionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.SuccessionFlowConnectionUsage)dto);
                case Core.POCO.SuccessionItemFlow successionItemFlowPoco:
                    return successionItemFlowPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.SuccessionItemFlow)dto);
                case Core.POCO.TerminateActionUsage terminateActionUsagePoco:
                    return terminateActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.TerminateActionUsage)dto);
                case Core.POCO.TextualRepresentation textualRepresentationPoco:
                    return textualRepresentationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.TextualRepresentation)dto);
                case Core.POCO.TransitionFeatureMembership transitionFeatureMembershipPoco:
                    return transitionFeatureMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.TransitionFeatureMembership)dto);
                case Core.POCO.TransitionUsage transitionUsagePoco:
                    return transitionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.TransitionUsage)dto);
                case Core.POCO.TriggerInvocationExpression triggerInvocationExpressionPoco:
                    return triggerInvocationExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.TriggerInvocationExpression)dto);
                case Core.POCO.Type typePoco:
                    return typePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Type)dto);
                case Core.POCO.TypeFeaturing typeFeaturingPoco:
                    return typeFeaturingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.TypeFeaturing)dto);
                case Core.POCO.Unioning unioningPoco:
                    return unioningPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Unioning)dto);
                case Core.POCO.Usage usagePoco:
                    return usagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Usage)dto);
                case Core.POCO.UseCaseDefinition useCaseDefinitionPoco:
                    return useCaseDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.UseCaseDefinition)dto);
                case Core.POCO.UseCaseUsage useCaseUsagePoco:
                    return useCaseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.UseCaseUsage)dto);
                case Core.POCO.VariantMembership variantMembershipPoco:
                    return variantMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.VariantMembership)dto);
                case Core.POCO.VerificationCaseDefinition verificationCaseDefinitionPoco:
                    return verificationCaseDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.VerificationCaseDefinition)dto);
                case Core.POCO.VerificationCaseUsage verificationCaseUsagePoco:
                    return verificationCaseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.VerificationCaseUsage)dto);
                case Core.POCO.ViewDefinition viewDefinitionPoco:
                    return viewDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ViewDefinition)dto);
                case Core.POCO.ViewpointDefinition viewpointDefinitionPoco:
                    return viewpointDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ViewpointDefinition)dto);
                case Core.POCO.ViewpointUsage viewpointUsagePoco:
                    return viewpointUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ViewpointUsage)dto);
                case Core.POCO.ViewRenderingMembership viewRenderingMembershipPoco:
                    return viewRenderingMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ViewRenderingMembership)dto);
                case Core.POCO.ViewUsage viewUsagePoco:
                    return viewUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.ViewUsage)dto);
                case Core.POCO.WhileLoopActionUsage whileLoopActionUsagePoco:
                    return whileLoopActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.WhileLoopActionUsage)dto);
                default:
                    throw new NotSupportedException($"{poco.GetType().Name} not yet supported");
            }
        }

        /// <summary>
        /// Updates the Reference properties of the <see cref="Core.POCO.IElement"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref=""/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref=""/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{String, Lazy{Core.POCO.IElement}}"/> that contains the
        /// <see cref="ModelThing"/>s that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.IElement poco, Core.DTO.IElement dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.IElement>> cache)
        {
            if (poco == null)
            {
                throw new ArgumentNullException(nameof(poco), $"the {nameof(poco)} may not be null");
            }

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache), $"the {nameof(cache)} may not be null");
            }

            switch (poco)
            {
                case Core.POCO.AcceptActionUsage acceptActionUsagePoco:
                    acceptActionUsagePoco.UpdateReferenceProperties((Core.DTO.AcceptActionUsage)dto, cache);
                    break;
                case Core.POCO.ActionDefinition actionDefinitionPoco:
                    actionDefinitionPoco.UpdateReferenceProperties((Core.DTO.ActionDefinition)dto, cache);
                    break;
                case Core.POCO.ActionUsage actionUsagePoco:
                    actionUsagePoco.UpdateReferenceProperties((Core.DTO.ActionUsage)dto, cache);
                    break;
                case Core.POCO.ActorMembership actorMembershipPoco:
                    actorMembershipPoco.UpdateReferenceProperties((Core.DTO.ActorMembership)dto, cache);
                    break;
                case Core.POCO.AllocationDefinition allocationDefinitionPoco:
                    allocationDefinitionPoco.UpdateReferenceProperties((Core.DTO.AllocationDefinition)dto, cache);
                    break;
                case Core.POCO.AllocationUsage allocationUsagePoco:
                    allocationUsagePoco.UpdateReferenceProperties((Core.DTO.AllocationUsage)dto, cache);
                    break;
                case Core.POCO.AnalysisCaseDefinition analysisCaseDefinitionPoco:
                    analysisCaseDefinitionPoco.UpdateReferenceProperties((Core.DTO.AnalysisCaseDefinition)dto, cache);
                    break;
                case Core.POCO.AnalysisCaseUsage analysisCaseUsagePoco:
                    analysisCaseUsagePoco.UpdateReferenceProperties((Core.DTO.AnalysisCaseUsage)dto, cache);
                    break;
                case Core.POCO.AnnotatingElement annotatingElementPoco:
                    annotatingElementPoco.UpdateReferenceProperties((Core.DTO.AnnotatingElement)dto, cache);
                    break;
                case Core.POCO.Annotation annotationPoco:
                    annotationPoco.UpdateReferenceProperties((Core.DTO.Annotation)dto, cache);
                    break;
                case Core.POCO.AssertConstraintUsage assertConstraintUsagePoco:
                    assertConstraintUsagePoco.UpdateReferenceProperties((Core.DTO.AssertConstraintUsage)dto, cache);
                    break;
                case Core.POCO.AssignmentActionUsage assignmentActionUsagePoco:
                    assignmentActionUsagePoco.UpdateReferenceProperties((Core.DTO.AssignmentActionUsage)dto, cache);
                    break;
                case Core.POCO.Association associationPoco:
                    associationPoco.UpdateReferenceProperties((Core.DTO.Association)dto, cache);
                    break;
                case Core.POCO.AssociationStructure associationStructurePoco:
                    associationStructurePoco.UpdateReferenceProperties((Core.DTO.AssociationStructure)dto, cache);
                    break;
                case Core.POCO.AttributeDefinition attributeDefinitionPoco:
                    attributeDefinitionPoco.UpdateReferenceProperties((Core.DTO.AttributeDefinition)dto, cache);
                    break;
                case Core.POCO.AttributeUsage attributeUsagePoco:
                    attributeUsagePoco.UpdateReferenceProperties((Core.DTO.AttributeUsage)dto, cache);
                    break;
                case Core.POCO.Behavior behaviorPoco:
                    behaviorPoco.UpdateReferenceProperties((Core.DTO.Behavior)dto, cache);
                    break;
                case Core.POCO.BindingConnector bindingConnectorPoco:
                    bindingConnectorPoco.UpdateReferenceProperties((Core.DTO.BindingConnector)dto, cache);
                    break;
                case Core.POCO.BindingConnectorAsUsage bindingConnectorAsUsagePoco:
                    bindingConnectorAsUsagePoco.UpdateReferenceProperties((Core.DTO.BindingConnectorAsUsage)dto, cache);
                    break;
                case Core.POCO.BooleanExpression booleanExpressionPoco:
                    booleanExpressionPoco.UpdateReferenceProperties((Core.DTO.BooleanExpression)dto, cache);
                    break;
                case Core.POCO.CalculationDefinition calculationDefinitionPoco:
                    calculationDefinitionPoco.UpdateReferenceProperties((Core.DTO.CalculationDefinition)dto, cache);
                    break;
                case Core.POCO.CalculationUsage calculationUsagePoco:
                    calculationUsagePoco.UpdateReferenceProperties((Core.DTO.CalculationUsage)dto, cache);
                    break;
                case Core.POCO.CaseDefinition caseDefinitionPoco:
                    caseDefinitionPoco.UpdateReferenceProperties((Core.DTO.CaseDefinition)dto, cache);
                    break;
                case Core.POCO.CaseUsage caseUsagePoco:
                    caseUsagePoco.UpdateReferenceProperties((Core.DTO.CaseUsage)dto, cache);
                    break;
                case Core.POCO.Class classPoco:
                    classPoco.UpdateReferenceProperties((Core.DTO.Class)dto, cache);
                    break;
                case Core.POCO.Classifier classifierPoco:
                    classifierPoco.UpdateReferenceProperties((Core.DTO.Classifier)dto, cache);
                    break;
                case Core.POCO.CollectExpression collectExpressionPoco:
                    collectExpressionPoco.UpdateReferenceProperties((Core.DTO.CollectExpression)dto, cache);
                    break;
                case Core.POCO.Comment commentPoco:
                    commentPoco.UpdateReferenceProperties((Core.DTO.Comment)dto, cache);
                    break;
                case Core.POCO.ConcernDefinition concernDefinitionPoco:
                    concernDefinitionPoco.UpdateReferenceProperties((Core.DTO.ConcernDefinition)dto, cache);
                    break;
                case Core.POCO.ConcernUsage concernUsagePoco:
                    concernUsagePoco.UpdateReferenceProperties((Core.DTO.ConcernUsage)dto, cache);
                    break;
                case Core.POCO.ConjugatedPortDefinition conjugatedPortDefinitionPoco:
                    conjugatedPortDefinitionPoco.UpdateReferenceProperties((Core.DTO.ConjugatedPortDefinition)dto, cache);
                    break;
                case Core.POCO.ConjugatedPortTyping conjugatedPortTypingPoco:
                    conjugatedPortTypingPoco.UpdateReferenceProperties((Core.DTO.ConjugatedPortTyping)dto, cache);
                    break;
                case Core.POCO.Conjugation conjugationPoco:
                    conjugationPoco.UpdateReferenceProperties((Core.DTO.Conjugation)dto, cache);
                    break;
                case Core.POCO.ConnectionDefinition connectionDefinitionPoco:
                    connectionDefinitionPoco.UpdateReferenceProperties((Core.DTO.ConnectionDefinition)dto, cache);
                    break;
                case Core.POCO.ConnectionUsage connectionUsagePoco:
                    connectionUsagePoco.UpdateReferenceProperties((Core.DTO.ConnectionUsage)dto, cache);
                    break;
                case Core.POCO.Connector connectorPoco:
                    connectorPoco.UpdateReferenceProperties((Core.DTO.Connector)dto, cache);
                    break;
                case Core.POCO.ConstraintDefinition constraintDefinitionPoco:
                    constraintDefinitionPoco.UpdateReferenceProperties((Core.DTO.ConstraintDefinition)dto, cache);
                    break;
                case Core.POCO.ConstraintUsage constraintUsagePoco:
                    constraintUsagePoco.UpdateReferenceProperties((Core.DTO.ConstraintUsage)dto, cache);
                    break;
                case Core.POCO.CrossSubsetting crossSubsettingPoco:
                    crossSubsettingPoco.UpdateReferenceProperties((Core.DTO.CrossSubsetting)dto, cache);
                    break;
                case Core.POCO.DataType dataTypePoco:
                    dataTypePoco.UpdateReferenceProperties((Core.DTO.DataType)dto, cache);
                    break;
                case Core.POCO.DecisionNode decisionNodePoco:
                    decisionNodePoco.UpdateReferenceProperties((Core.DTO.DecisionNode)dto, cache);
                    break;
                case Core.POCO.Definition definitionPoco:
                    definitionPoco.UpdateReferenceProperties((Core.DTO.Definition)dto, cache);
                    break;
                case Core.POCO.Dependency dependencyPoco:
                    dependencyPoco.UpdateReferenceProperties((Core.DTO.Dependency)dto, cache);
                    break;
                case Core.POCO.Differencing differencingPoco:
                    differencingPoco.UpdateReferenceProperties((Core.DTO.Differencing)dto, cache);
                    break;
                case Core.POCO.Disjoining disjoiningPoco:
                    disjoiningPoco.UpdateReferenceProperties((Core.DTO.Disjoining)dto, cache);
                    break;
                case Core.POCO.Documentation documentationPoco:
                    documentationPoco.UpdateReferenceProperties((Core.DTO.Documentation)dto, cache);
                    break;
                case Core.POCO.ElementFilterMembership elementFilterMembershipPoco:
                    elementFilterMembershipPoco.UpdateReferenceProperties((Core.DTO.ElementFilterMembership)dto, cache);
                    break;
                case Core.POCO.EndFeatureMembership endFeatureMembershipPoco:
                    endFeatureMembershipPoco.UpdateReferenceProperties((Core.DTO.EndFeatureMembership)dto, cache);
                    break;
                case Core.POCO.EnumerationDefinition enumerationDefinitionPoco:
                    enumerationDefinitionPoco.UpdateReferenceProperties((Core.DTO.EnumerationDefinition)dto, cache);
                    break;
                case Core.POCO.EnumerationUsage enumerationUsagePoco:
                    enumerationUsagePoco.UpdateReferenceProperties((Core.DTO.EnumerationUsage)dto, cache);
                    break;
                case Core.POCO.EventOccurrenceUsage eventOccurrenceUsagePoco:
                    eventOccurrenceUsagePoco.UpdateReferenceProperties((Core.DTO.EventOccurrenceUsage)dto, cache);
                    break;
                case Core.POCO.ExhibitStateUsage exhibitStateUsagePoco:
                    exhibitStateUsagePoco.UpdateReferenceProperties((Core.DTO.ExhibitStateUsage)dto, cache);
                    break;
                case Core.POCO.Expression expressionPoco:
                    expressionPoco.UpdateReferenceProperties((Core.DTO.Expression)dto, cache);
                    break;
                case Core.POCO.Feature featurePoco:
                    featurePoco.UpdateReferenceProperties((Core.DTO.Feature)dto, cache);
                    break;
                case Core.POCO.FeatureChainExpression featureChainExpressionPoco:
                    featureChainExpressionPoco.UpdateReferenceProperties((Core.DTO.FeatureChainExpression)dto, cache);
                    break;
                case Core.POCO.FeatureChaining featureChainingPoco:
                    featureChainingPoco.UpdateReferenceProperties((Core.DTO.FeatureChaining)dto, cache);
                    break;
                case Core.POCO.FeatureInverting featureInvertingPoco:
                    featureInvertingPoco.UpdateReferenceProperties((Core.DTO.FeatureInverting)dto, cache);
                    break;
                case Core.POCO.FeatureMembership featureMembershipPoco:
                    featureMembershipPoco.UpdateReferenceProperties((Core.DTO.FeatureMembership)dto, cache);
                    break;
                case Core.POCO.FeatureReferenceExpression featureReferenceExpressionPoco:
                    featureReferenceExpressionPoco.UpdateReferenceProperties((Core.DTO.FeatureReferenceExpression)dto, cache);
                    break;
                case Core.POCO.FeatureTyping featureTypingPoco:
                    featureTypingPoco.UpdateReferenceProperties((Core.DTO.FeatureTyping)dto, cache);
                    break;
                case Core.POCO.FeatureValue featureValuePoco:
                    featureValuePoco.UpdateReferenceProperties((Core.DTO.FeatureValue)dto, cache);
                    break;
                case Core.POCO.FlowConnectionDefinition flowConnectionDefinitionPoco:
                    flowConnectionDefinitionPoco.UpdateReferenceProperties((Core.DTO.FlowConnectionDefinition)dto, cache);
                    break;
                case Core.POCO.FlowConnectionUsage flowConnectionUsagePoco:
                    flowConnectionUsagePoco.UpdateReferenceProperties((Core.DTO.FlowConnectionUsage)dto, cache);
                    break;
                case Core.POCO.ForkNode forkNodePoco:
                    forkNodePoco.UpdateReferenceProperties((Core.DTO.ForkNode)dto, cache);
                    break;
                case Core.POCO.ForLoopActionUsage forLoopActionUsagePoco:
                    forLoopActionUsagePoco.UpdateReferenceProperties((Core.DTO.ForLoopActionUsage)dto, cache);
                    break;
                case Core.POCO.FramedConcernMembership framedConcernMembershipPoco:
                    framedConcernMembershipPoco.UpdateReferenceProperties((Core.DTO.FramedConcernMembership)dto, cache);
                    break;
                case Core.POCO.Function functionPoco:
                    functionPoco.UpdateReferenceProperties((Core.DTO.Function)dto, cache);
                    break;
                case Core.POCO.IfActionUsage ifActionUsagePoco:
                    ifActionUsagePoco.UpdateReferenceProperties((Core.DTO.IfActionUsage)dto, cache);
                    break;
                case Core.POCO.IncludeUseCaseUsage includeUseCaseUsagePoco:
                    includeUseCaseUsagePoco.UpdateReferenceProperties((Core.DTO.IncludeUseCaseUsage)dto, cache);
                    break;
                case Core.POCO.IndexExpression indexExpressionPoco:
                    indexExpressionPoco.UpdateReferenceProperties((Core.DTO.IndexExpression)dto, cache);
                    break;
                case Core.POCO.Interaction interactionPoco:
                    interactionPoco.UpdateReferenceProperties((Core.DTO.Interaction)dto, cache);
                    break;
                case Core.POCO.InterfaceDefinition interfaceDefinitionPoco:
                    interfaceDefinitionPoco.UpdateReferenceProperties((Core.DTO.InterfaceDefinition)dto, cache);
                    break;
                case Core.POCO.InterfaceUsage interfaceUsagePoco:
                    interfaceUsagePoco.UpdateReferenceProperties((Core.DTO.InterfaceUsage)dto, cache);
                    break;
                case Core.POCO.Intersecting intersectingPoco:
                    intersectingPoco.UpdateReferenceProperties((Core.DTO.Intersecting)dto, cache);
                    break;
                case Core.POCO.Invariant invariantPoco:
                    invariantPoco.UpdateReferenceProperties((Core.DTO.Invariant)dto, cache);
                    break;
                case Core.POCO.InvocationExpression invocationExpressionPoco:
                    invocationExpressionPoco.UpdateReferenceProperties((Core.DTO.InvocationExpression)dto, cache);
                    break;
                case Core.POCO.ItemDefinition itemDefinitionPoco:
                    itemDefinitionPoco.UpdateReferenceProperties((Core.DTO.ItemDefinition)dto, cache);
                    break;
                case Core.POCO.ItemFeature itemFeaturePoco:
                    itemFeaturePoco.UpdateReferenceProperties((Core.DTO.ItemFeature)dto, cache);
                    break;
                case Core.POCO.ItemFlow itemFlowPoco:
                    itemFlowPoco.UpdateReferenceProperties((Core.DTO.ItemFlow)dto, cache);
                    break;
                case Core.POCO.ItemFlowEnd itemFlowEndPoco:
                    itemFlowEndPoco.UpdateReferenceProperties((Core.DTO.ItemFlowEnd)dto, cache);
                    break;
                case Core.POCO.ItemUsage itemUsagePoco:
                    itemUsagePoco.UpdateReferenceProperties((Core.DTO.ItemUsage)dto, cache);
                    break;
                case Core.POCO.JoinNode joinNodePoco:
                    joinNodePoco.UpdateReferenceProperties((Core.DTO.JoinNode)dto, cache);
                    break;
                case Core.POCO.LibraryPackage libraryPackagePoco:
                    libraryPackagePoco.UpdateReferenceProperties((Core.DTO.LibraryPackage)dto, cache);
                    break;
                case Core.POCO.LifeClass lifeClassPoco:
                    lifeClassPoco.UpdateReferenceProperties((Core.DTO.LifeClass)dto, cache);
                    break;
                case Core.POCO.LiteralBoolean literalBooleanPoco:
                    literalBooleanPoco.UpdateReferenceProperties((Core.DTO.LiteralBoolean)dto, cache);
                    break;
                case Core.POCO.LiteralExpression literalExpressionPoco:
                    literalExpressionPoco.UpdateReferenceProperties((Core.DTO.LiteralExpression)dto, cache);
                    break;
                case Core.POCO.LiteralInfinity literalInfinityPoco:
                    literalInfinityPoco.UpdateReferenceProperties((Core.DTO.LiteralInfinity)dto, cache);
                    break;
                case Core.POCO.LiteralInteger literalIntegerPoco:
                    literalIntegerPoco.UpdateReferenceProperties((Core.DTO.LiteralInteger)dto, cache);
                    break;
                case Core.POCO.LiteralRational literalRationalPoco:
                    literalRationalPoco.UpdateReferenceProperties((Core.DTO.LiteralRational)dto, cache);
                    break;
                case Core.POCO.LiteralString literalStringPoco:
                    literalStringPoco.UpdateReferenceProperties((Core.DTO.LiteralString)dto, cache);
                    break;
                case Core.POCO.Membership membershipPoco:
                    membershipPoco.UpdateReferenceProperties((Core.DTO.Membership)dto, cache);
                    break;
                case Core.POCO.MembershipExpose membershipExposePoco:
                    membershipExposePoco.UpdateReferenceProperties((Core.DTO.MembershipExpose)dto, cache);
                    break;
                case Core.POCO.MembershipImport membershipImportPoco:
                    membershipImportPoco.UpdateReferenceProperties((Core.DTO.MembershipImport)dto, cache);
                    break;
                case Core.POCO.MergeNode mergeNodePoco:
                    mergeNodePoco.UpdateReferenceProperties((Core.DTO.MergeNode)dto, cache);
                    break;
                case Core.POCO.Metaclass metaclassPoco:
                    metaclassPoco.UpdateReferenceProperties((Core.DTO.Metaclass)dto, cache);
                    break;
                case Core.POCO.MetadataAccessExpression metadataAccessExpressionPoco:
                    metadataAccessExpressionPoco.UpdateReferenceProperties((Core.DTO.MetadataAccessExpression)dto, cache);
                    break;
                case Core.POCO.MetadataDefinition metadataDefinitionPoco:
                    metadataDefinitionPoco.UpdateReferenceProperties((Core.DTO.MetadataDefinition)dto, cache);
                    break;
                case Core.POCO.MetadataFeature metadataFeaturePoco:
                    metadataFeaturePoco.UpdateReferenceProperties((Core.DTO.MetadataFeature)dto, cache);
                    break;
                case Core.POCO.MetadataUsage metadataUsagePoco:
                    metadataUsagePoco.UpdateReferenceProperties((Core.DTO.MetadataUsage)dto, cache);
                    break;
                case Core.POCO.Multiplicity multiplicityPoco:
                    multiplicityPoco.UpdateReferenceProperties((Core.DTO.Multiplicity)dto, cache);
                    break;
                case Core.POCO.MultiplicityRange multiplicityRangePoco:
                    multiplicityRangePoco.UpdateReferenceProperties((Core.DTO.MultiplicityRange)dto, cache);
                    break;
                case Core.POCO.Namespace namespacePoco:
                    namespacePoco.UpdateReferenceProperties((Core.DTO.Namespace)dto, cache);
                    break;
                case Core.POCO.NamespaceExpose namespaceExposePoco:
                    namespaceExposePoco.UpdateReferenceProperties((Core.DTO.NamespaceExpose)dto, cache);
                    break;
                case Core.POCO.NamespaceImport namespaceImportPoco:
                    namespaceImportPoco.UpdateReferenceProperties((Core.DTO.NamespaceImport)dto, cache);
                    break;
                case Core.POCO.NullExpression nullExpressionPoco:
                    nullExpressionPoco.UpdateReferenceProperties((Core.DTO.NullExpression)dto, cache);
                    break;
                case Core.POCO.ObjectiveMembership objectiveMembershipPoco:
                    objectiveMembershipPoco.UpdateReferenceProperties((Core.DTO.ObjectiveMembership)dto, cache);
                    break;
                case Core.POCO.OccurrenceDefinition occurrenceDefinitionPoco:
                    occurrenceDefinitionPoco.UpdateReferenceProperties((Core.DTO.OccurrenceDefinition)dto, cache);
                    break;
                case Core.POCO.OccurrenceUsage occurrenceUsagePoco:
                    occurrenceUsagePoco.UpdateReferenceProperties((Core.DTO.OccurrenceUsage)dto, cache);
                    break;
                case Core.POCO.OperatorExpression operatorExpressionPoco:
                    operatorExpressionPoco.UpdateReferenceProperties((Core.DTO.OperatorExpression)dto, cache);
                    break;
                case Core.POCO.OwningMembership owningMembershipPoco:
                    owningMembershipPoco.UpdateReferenceProperties((Core.DTO.OwningMembership)dto, cache);
                    break;
                case Core.POCO.Package packagePoco:
                    packagePoco.UpdateReferenceProperties((Core.DTO.Package)dto, cache);
                    break;
                case Core.POCO.ParameterMembership parameterMembershipPoco:
                    parameterMembershipPoco.UpdateReferenceProperties((Core.DTO.ParameterMembership)dto, cache);
                    break;
                case Core.POCO.PartDefinition partDefinitionPoco:
                    partDefinitionPoco.UpdateReferenceProperties((Core.DTO.PartDefinition)dto, cache);
                    break;
                case Core.POCO.PartUsage partUsagePoco:
                    partUsagePoco.UpdateReferenceProperties((Core.DTO.PartUsage)dto, cache);
                    break;
                case Core.POCO.PerformActionUsage performActionUsagePoco:
                    performActionUsagePoco.UpdateReferenceProperties((Core.DTO.PerformActionUsage)dto, cache);
                    break;
                case Core.POCO.PortConjugation portConjugationPoco:
                    portConjugationPoco.UpdateReferenceProperties((Core.DTO.PortConjugation)dto, cache);
                    break;
                case Core.POCO.PortDefinition portDefinitionPoco:
                    portDefinitionPoco.UpdateReferenceProperties((Core.DTO.PortDefinition)dto, cache);
                    break;
                case Core.POCO.PortUsage portUsagePoco:
                    portUsagePoco.UpdateReferenceProperties((Core.DTO.PortUsage)dto, cache);
                    break;
                case Core.POCO.Predicate predicatePoco:
                    predicatePoco.UpdateReferenceProperties((Core.DTO.Predicate)dto, cache);
                    break;
                case Core.POCO.Redefinition redefinitionPoco:
                    redefinitionPoco.UpdateReferenceProperties((Core.DTO.Redefinition)dto, cache);
                    break;
                case Core.POCO.ReferenceSubsetting referenceSubsettingPoco:
                    referenceSubsettingPoco.UpdateReferenceProperties((Core.DTO.ReferenceSubsetting)dto, cache);
                    break;
                case Core.POCO.ReferenceUsage referenceUsagePoco:
                    referenceUsagePoco.UpdateReferenceProperties((Core.DTO.ReferenceUsage)dto, cache);
                    break;
                case Core.POCO.RenderingDefinition renderingDefinitionPoco:
                    renderingDefinitionPoco.UpdateReferenceProperties((Core.DTO.RenderingDefinition)dto, cache);
                    break;
                case Core.POCO.RenderingUsage renderingUsagePoco:
                    renderingUsagePoco.UpdateReferenceProperties((Core.DTO.RenderingUsage)dto, cache);
                    break;
                case Core.POCO.RequirementConstraintMembership requirementConstraintMembershipPoco:
                    requirementConstraintMembershipPoco.UpdateReferenceProperties((Core.DTO.RequirementConstraintMembership)dto, cache);
                    break;
                case Core.POCO.RequirementDefinition requirementDefinitionPoco:
                    requirementDefinitionPoco.UpdateReferenceProperties((Core.DTO.RequirementDefinition)dto, cache);
                    break;
                case Core.POCO.RequirementUsage requirementUsagePoco:
                    requirementUsagePoco.UpdateReferenceProperties((Core.DTO.RequirementUsage)dto, cache);
                    break;
                case Core.POCO.RequirementVerificationMembership requirementVerificationMembershipPoco:
                    requirementVerificationMembershipPoco.UpdateReferenceProperties((Core.DTO.RequirementVerificationMembership)dto, cache);
                    break;
                case Core.POCO.ResultExpressionMembership resultExpressionMembershipPoco:
                    resultExpressionMembershipPoco.UpdateReferenceProperties((Core.DTO.ResultExpressionMembership)dto, cache);
                    break;
                case Core.POCO.ReturnParameterMembership returnParameterMembershipPoco:
                    returnParameterMembershipPoco.UpdateReferenceProperties((Core.DTO.ReturnParameterMembership)dto, cache);
                    break;
                case Core.POCO.SatisfyRequirementUsage satisfyRequirementUsagePoco:
                    satisfyRequirementUsagePoco.UpdateReferenceProperties((Core.DTO.SatisfyRequirementUsage)dto, cache);
                    break;
                case Core.POCO.SelectExpression selectExpressionPoco:
                    selectExpressionPoco.UpdateReferenceProperties((Core.DTO.SelectExpression)dto, cache);
                    break;
                case Core.POCO.SendActionUsage sendActionUsagePoco:
                    sendActionUsagePoco.UpdateReferenceProperties((Core.DTO.SendActionUsage)dto, cache);
                    break;
                case Core.POCO.Specialization specializationPoco:
                    specializationPoco.UpdateReferenceProperties((Core.DTO.Specialization)dto, cache);
                    break;
                case Core.POCO.StakeholderMembership stakeholderMembershipPoco:
                    stakeholderMembershipPoco.UpdateReferenceProperties((Core.DTO.StakeholderMembership)dto, cache);
                    break;
                case Core.POCO.StateDefinition stateDefinitionPoco:
                    stateDefinitionPoco.UpdateReferenceProperties((Core.DTO.StateDefinition)dto, cache);
                    break;
                case Core.POCO.StateSubactionMembership stateSubactionMembershipPoco:
                    stateSubactionMembershipPoco.UpdateReferenceProperties((Core.DTO.StateSubactionMembership)dto, cache);
                    break;
                case Core.POCO.StateUsage stateUsagePoco:
                    stateUsagePoco.UpdateReferenceProperties((Core.DTO.StateUsage)dto, cache);
                    break;
                case Core.POCO.Step stepPoco:
                    stepPoco.UpdateReferenceProperties((Core.DTO.Step)dto, cache);
                    break;
                case Core.POCO.Structure structurePoco:
                    structurePoco.UpdateReferenceProperties((Core.DTO.Structure)dto, cache);
                    break;
                case Core.POCO.Subclassification subclassificationPoco:
                    subclassificationPoco.UpdateReferenceProperties((Core.DTO.Subclassification)dto, cache);
                    break;
                case Core.POCO.SubjectMembership subjectMembershipPoco:
                    subjectMembershipPoco.UpdateReferenceProperties((Core.DTO.SubjectMembership)dto, cache);
                    break;
                case Core.POCO.Subsetting subsettingPoco:
                    subsettingPoco.UpdateReferenceProperties((Core.DTO.Subsetting)dto, cache);
                    break;
                case Core.POCO.Succession successionPoco:
                    successionPoco.UpdateReferenceProperties((Core.DTO.Succession)dto, cache);
                    break;
                case Core.POCO.SuccessionAsUsage successionAsUsagePoco:
                    successionAsUsagePoco.UpdateReferenceProperties((Core.DTO.SuccessionAsUsage)dto, cache);
                    break;
                case Core.POCO.SuccessionFlowConnectionUsage successionFlowConnectionUsagePoco:
                    successionFlowConnectionUsagePoco.UpdateReferenceProperties((Core.DTO.SuccessionFlowConnectionUsage)dto, cache);
                    break;
                case Core.POCO.SuccessionItemFlow successionItemFlowPoco:
                    successionItemFlowPoco.UpdateReferenceProperties((Core.DTO.SuccessionItemFlow)dto, cache);
                    break;
                case Core.POCO.TerminateActionUsage terminateActionUsagePoco:
                    terminateActionUsagePoco.UpdateReferenceProperties((Core.DTO.TerminateActionUsage)dto, cache);
                    break;
                case Core.POCO.TextualRepresentation textualRepresentationPoco:
                    textualRepresentationPoco.UpdateReferenceProperties((Core.DTO.TextualRepresentation)dto, cache);
                    break;
                case Core.POCO.TransitionFeatureMembership transitionFeatureMembershipPoco:
                    transitionFeatureMembershipPoco.UpdateReferenceProperties((Core.DTO.TransitionFeatureMembership)dto, cache);
                    break;
                case Core.POCO.TransitionUsage transitionUsagePoco:
                    transitionUsagePoco.UpdateReferenceProperties((Core.DTO.TransitionUsage)dto, cache);
                    break;
                case Core.POCO.TriggerInvocationExpression triggerInvocationExpressionPoco:
                    triggerInvocationExpressionPoco.UpdateReferenceProperties((Core.DTO.TriggerInvocationExpression)dto, cache);
                    break;
                case Core.POCO.Type typePoco:
                    typePoco.UpdateReferenceProperties((Core.DTO.Type)dto, cache);
                    break;
                case Core.POCO.TypeFeaturing typeFeaturingPoco:
                    typeFeaturingPoco.UpdateReferenceProperties((Core.DTO.TypeFeaturing)dto, cache);
                    break;
                case Core.POCO.Unioning unioningPoco:
                    unioningPoco.UpdateReferenceProperties((Core.DTO.Unioning)dto, cache);
                    break;
                case Core.POCO.Usage usagePoco:
                    usagePoco.UpdateReferenceProperties((Core.DTO.Usage)dto, cache);
                    break;
                case Core.POCO.UseCaseDefinition useCaseDefinitionPoco:
                    useCaseDefinitionPoco.UpdateReferenceProperties((Core.DTO.UseCaseDefinition)dto, cache);
                    break;
                case Core.POCO.UseCaseUsage useCaseUsagePoco:
                    useCaseUsagePoco.UpdateReferenceProperties((Core.DTO.UseCaseUsage)dto, cache);
                    break;
                case Core.POCO.VariantMembership variantMembershipPoco:
                    variantMembershipPoco.UpdateReferenceProperties((Core.DTO.VariantMembership)dto, cache);
                    break;
                case Core.POCO.VerificationCaseDefinition verificationCaseDefinitionPoco:
                    verificationCaseDefinitionPoco.UpdateReferenceProperties((Core.DTO.VerificationCaseDefinition)dto, cache);
                    break;
                case Core.POCO.VerificationCaseUsage verificationCaseUsagePoco:
                    verificationCaseUsagePoco.UpdateReferenceProperties((Core.DTO.VerificationCaseUsage)dto, cache);
                    break;
                case Core.POCO.ViewDefinition viewDefinitionPoco:
                    viewDefinitionPoco.UpdateReferenceProperties((Core.DTO.ViewDefinition)dto, cache);
                    break;
                case Core.POCO.ViewpointDefinition viewpointDefinitionPoco:
                    viewpointDefinitionPoco.UpdateReferenceProperties((Core.DTO.ViewpointDefinition)dto, cache);
                    break;
                case Core.POCO.ViewpointUsage viewpointUsagePoco:
                    viewpointUsagePoco.UpdateReferenceProperties((Core.DTO.ViewpointUsage)dto, cache);
                    break;
                case Core.POCO.ViewRenderingMembership viewRenderingMembershipPoco:
                    viewRenderingMembershipPoco.UpdateReferenceProperties((Core.DTO.ViewRenderingMembership)dto, cache);
                    break;
                case Core.POCO.ViewUsage viewUsagePoco:
                    viewUsagePoco.UpdateReferenceProperties((Core.DTO.ViewUsage)dto, cache);
                    break;
                case Core.POCO.WhileLoopActionUsage whileLoopActionUsagePoco:
                    whileLoopActionUsagePoco.UpdateReferenceProperties((Core.DTO.WhileLoopActionUsage)dto, cache);
                    break;
                default:
                    throw new NotSupportedException($"{poco.GetType().Name} not yet supported");
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
