// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="Starion Group S.A.">
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A static class that provides extension methods for the <see cref="Core.POCO.Root.Elements.IElement"/> class
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Updates the value properties of the <see cref="Core.POCO.Root.Elements.IElement"/> by setting the value equal to that of the dto.
        /// Removes deleted objects from the reference properties and returns the unique identifiers
        /// of the objects that have been removed from containment properties
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.Root.Elements.IElement"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.POCO.Root.Elements.IElement"/> with
        /// </param>
        /// <returns>
        /// The unique identifiers of the objects that have been removed from containment properties
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="poco"/> or <paramref name="dto"/> is null
        /// </exception>
        public static IEnumerable<Guid> UpdateValueAndRemoveDeletedReferenceProperties(this Core.POCO.Root.Elements.IElement poco, Core.DTO.Root.Elements.IElement dto)
        {
            if (poco == null)
            {
                throw new ArgumentNullException(nameof(poco), $"the {nameof(poco)} may not be null");
            }

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), $"the {nameof(dto)} may not be null");
            }

            return poco switch
            {
                Core.POCO.Systems.Actions.AcceptActionUsage acceptActionUsagePoco => acceptActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.AcceptActionUsage)dto),
                Core.POCO.Systems.Actions.ActionDefinition actionDefinitionPoco => actionDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.ActionDefinition)dto),
                Core.POCO.Systems.Actions.ActionUsage actionUsagePoco => actionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.ActionUsage)dto),
                Core.POCO.Systems.Requirements.ActorMembership actorMembershipPoco => actorMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.ActorMembership)dto),
                Core.POCO.Systems.Allocations.AllocationDefinition allocationDefinitionPoco => allocationDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Allocations.AllocationDefinition)dto),
                Core.POCO.Systems.Allocations.AllocationUsage allocationUsagePoco => allocationUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Allocations.AllocationUsage)dto),
                Core.POCO.Systems.AnalysisCases.AnalysisCaseDefinition analysisCaseDefinitionPoco => analysisCaseDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition)dto),
                Core.POCO.Systems.AnalysisCases.AnalysisCaseUsage analysisCaseUsagePoco => analysisCaseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage)dto),
                Core.POCO.Root.Annotations.AnnotatingElement annotatingElementPoco => annotatingElementPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Annotations.AnnotatingElement)dto),
                Core.POCO.Root.Annotations.Annotation annotationPoco => annotationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Annotations.Annotation)dto),
                Core.POCO.Systems.Constraints.AssertConstraintUsage assertConstraintUsagePoco => assertConstraintUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Constraints.AssertConstraintUsage)dto),
                Core.POCO.Systems.Actions.AssignmentActionUsage assignmentActionUsagePoco => assignmentActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.AssignmentActionUsage)dto),
                Core.POCO.Kernel.Associations.Association associationPoco => associationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Associations.Association)dto),
                Core.POCO.Kernel.Associations.AssociationStructure associationStructurePoco => associationStructurePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Associations.AssociationStructure)dto),
                Core.POCO.Systems.Attributes.AttributeDefinition attributeDefinitionPoco => attributeDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Attributes.AttributeDefinition)dto),
                Core.POCO.Systems.Attributes.AttributeUsage attributeUsagePoco => attributeUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Attributes.AttributeUsage)dto),
                Core.POCO.Kernel.Behaviors.Behavior behaviorPoco => behaviorPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Behaviors.Behavior)dto),
                Core.POCO.Kernel.Connectors.BindingConnector bindingConnectorPoco => bindingConnectorPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Connectors.BindingConnector)dto),
                Core.POCO.Systems.Connections.BindingConnectorAsUsage bindingConnectorAsUsagePoco => bindingConnectorAsUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Connections.BindingConnectorAsUsage)dto),
                Core.POCO.Kernel.Functions.BooleanExpression booleanExpressionPoco => booleanExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Functions.BooleanExpression)dto),
                Core.POCO.Systems.Calculations.CalculationDefinition calculationDefinitionPoco => calculationDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Calculations.CalculationDefinition)dto),
                Core.POCO.Systems.Calculations.CalculationUsage calculationUsagePoco => calculationUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Calculations.CalculationUsage)dto),
                Core.POCO.Systems.Cases.CaseDefinition caseDefinitionPoco => caseDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Cases.CaseDefinition)dto),
                Core.POCO.Systems.Cases.CaseUsage caseUsagePoco => caseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Cases.CaseUsage)dto),
                Core.POCO.Kernel.Classes.Class classPoco => classPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Classes.Class)dto),
                Core.POCO.Core.Classifiers.Classifier classifierPoco => classifierPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Classifiers.Classifier)dto),
                Core.POCO.Kernel.Expressions.CollectExpression collectExpressionPoco => collectExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.CollectExpression)dto),
                Core.POCO.Root.Annotations.Comment commentPoco => commentPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Annotations.Comment)dto),
                Core.POCO.Systems.Requirements.ConcernDefinition concernDefinitionPoco => concernDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.ConcernDefinition)dto),
                Core.POCO.Systems.Requirements.ConcernUsage concernUsagePoco => concernUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.ConcernUsage)dto),
                Core.POCO.Systems.Ports.ConjugatedPortDefinition conjugatedPortDefinitionPoco => conjugatedPortDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Ports.ConjugatedPortDefinition)dto),
                Core.POCO.Systems.Ports.ConjugatedPortTyping conjugatedPortTypingPoco => conjugatedPortTypingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Ports.ConjugatedPortTyping)dto),
                Core.POCO.Core.Types.Conjugation conjugationPoco => conjugationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.Conjugation)dto),
                Core.POCO.Systems.Connections.ConnectionDefinition connectionDefinitionPoco => connectionDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Connections.ConnectionDefinition)dto),
                Core.POCO.Systems.Connections.ConnectionUsage connectionUsagePoco => connectionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Connections.ConnectionUsage)dto),
                Core.POCO.Kernel.Connectors.Connector connectorPoco => connectorPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Connectors.Connector)dto),
                Core.POCO.Systems.Constraints.ConstraintDefinition constraintDefinitionPoco => constraintDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Constraints.ConstraintDefinition)dto),
                Core.POCO.Systems.Constraints.ConstraintUsage constraintUsagePoco => constraintUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Constraints.ConstraintUsage)dto),
                Core.POCO.Kernel.Expressions.ConstructorExpression constructorExpressionPoco => constructorExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.ConstructorExpression)dto),
                Core.POCO.Core.Features.CrossSubsetting crossSubsettingPoco => crossSubsettingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.CrossSubsetting)dto),
                Core.POCO.Kernel.DataTypes.DataType dataTypePoco => dataTypePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.DataTypes.DataType)dto),
                Core.POCO.Systems.Actions.DecisionNode decisionNodePoco => decisionNodePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.DecisionNode)dto),
                Core.POCO.Systems.DefinitionAndUsage.Definition definitionPoco => definitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.DefinitionAndUsage.Definition)dto),
                Core.POCO.Root.Dependencies.Dependency dependencyPoco => dependencyPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Dependencies.Dependency)dto),
                Core.POCO.Core.Types.Differencing differencingPoco => differencingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.Differencing)dto),
                Core.POCO.Core.Types.Disjoining disjoiningPoco => disjoiningPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.Disjoining)dto),
                Core.POCO.Root.Annotations.Documentation documentationPoco => documentationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Annotations.Documentation)dto),
                Core.POCO.Kernel.Packages.ElementFilterMembership elementFilterMembershipPoco => elementFilterMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Packages.ElementFilterMembership)dto),
                Core.POCO.Core.Features.EndFeatureMembership endFeatureMembershipPoco => endFeatureMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.EndFeatureMembership)dto),
                Core.POCO.Systems.Enumerations.EnumerationDefinition enumerationDefinitionPoco => enumerationDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Enumerations.EnumerationDefinition)dto),
                Core.POCO.Systems.Enumerations.EnumerationUsage enumerationUsagePoco => enumerationUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Enumerations.EnumerationUsage)dto),
                Core.POCO.Systems.Occurrences.EventOccurrenceUsage eventOccurrenceUsagePoco => eventOccurrenceUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Occurrences.EventOccurrenceUsage)dto),
                Core.POCO.Systems.States.ExhibitStateUsage exhibitStateUsagePoco => exhibitStateUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.States.ExhibitStateUsage)dto),
                Core.POCO.Kernel.Functions.Expression expressionPoco => expressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Functions.Expression)dto),
                Core.POCO.Core.Features.Feature featurePoco => featurePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.Feature)dto),
                Core.POCO.Kernel.Expressions.FeatureChainExpression featureChainExpressionPoco => featureChainExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.FeatureChainExpression)dto),
                Core.POCO.Core.Features.FeatureChaining featureChainingPoco => featureChainingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.FeatureChaining)dto),
                Core.POCO.Core.Features.FeatureInverting featureInvertingPoco => featureInvertingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.FeatureInverting)dto),
                Core.POCO.Core.Types.FeatureMembership featureMembershipPoco => featureMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.FeatureMembership)dto),
                Core.POCO.Kernel.Expressions.FeatureReferenceExpression featureReferenceExpressionPoco => featureReferenceExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.FeatureReferenceExpression)dto),
                Core.POCO.Core.Features.FeatureTyping featureTypingPoco => featureTypingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.FeatureTyping)dto),
                Core.POCO.Kernel.FeatureValues.FeatureValue featureValuePoco => featureValuePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.FeatureValues.FeatureValue)dto),
                Core.POCO.Kernel.Interactions.Flow flowPoco => flowPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Interactions.Flow)dto),
                Core.POCO.Systems.Flows.FlowDefinition flowDefinitionPoco => flowDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Flows.FlowDefinition)dto),
                Core.POCO.Kernel.Interactions.FlowEnd flowEndPoco => flowEndPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Interactions.FlowEnd)dto),
                Core.POCO.Systems.Flows.FlowUsage flowUsagePoco => flowUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Flows.FlowUsage)dto),
                Core.POCO.Systems.Actions.ForkNode forkNodePoco => forkNodePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.ForkNode)dto),
                Core.POCO.Systems.Actions.ForLoopActionUsage forLoopActionUsagePoco => forLoopActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.ForLoopActionUsage)dto),
                Core.POCO.Systems.Requirements.FramedConcernMembership framedConcernMembershipPoco => framedConcernMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.FramedConcernMembership)dto),
                Core.POCO.Kernel.Functions.Function functionPoco => functionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Functions.Function)dto),
                Core.POCO.Systems.Actions.IfActionUsage ifActionUsagePoco => ifActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.IfActionUsage)dto),
                Core.POCO.Systems.UseCases.IncludeUseCaseUsage includeUseCaseUsagePoco => includeUseCaseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.UseCases.IncludeUseCaseUsage)dto),
                Core.POCO.Kernel.Expressions.IndexExpression indexExpressionPoco => indexExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.IndexExpression)dto),
                Core.POCO.Kernel.Interactions.Interaction interactionPoco => interactionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Interactions.Interaction)dto),
                Core.POCO.Systems.Interfaces.InterfaceDefinition interfaceDefinitionPoco => interfaceDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Interfaces.InterfaceDefinition)dto),
                Core.POCO.Systems.Interfaces.InterfaceUsage interfaceUsagePoco => interfaceUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Interfaces.InterfaceUsage)dto),
                Core.POCO.Core.Types.Intersecting intersectingPoco => intersectingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.Intersecting)dto),
                Core.POCO.Kernel.Functions.Invariant invariantPoco => invariantPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Functions.Invariant)dto),
                Core.POCO.Kernel.Expressions.InvocationExpression invocationExpressionPoco => invocationExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.InvocationExpression)dto),
                Core.POCO.Systems.Items.ItemDefinition itemDefinitionPoco => itemDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Items.ItemDefinition)dto),
                Core.POCO.Systems.Items.ItemUsage itemUsagePoco => itemUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Items.ItemUsage)dto),
                Core.POCO.Systems.Actions.JoinNode joinNodePoco => joinNodePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.JoinNode)dto),
                Core.POCO.Kernel.Packages.LibraryPackage libraryPackagePoco => libraryPackagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Packages.LibraryPackage)dto),
                Core.POCO.Kernel.Expressions.LiteralBoolean literalBooleanPoco => literalBooleanPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.LiteralBoolean)dto),
                Core.POCO.Kernel.Expressions.LiteralExpression literalExpressionPoco => literalExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.LiteralExpression)dto),
                Core.POCO.Kernel.Expressions.LiteralInfinity literalInfinityPoco => literalInfinityPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.LiteralInfinity)dto),
                Core.POCO.Kernel.Expressions.LiteralInteger literalIntegerPoco => literalIntegerPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.LiteralInteger)dto),
                Core.POCO.Kernel.Expressions.LiteralRational literalRationalPoco => literalRationalPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.LiteralRational)dto),
                Core.POCO.Kernel.Expressions.LiteralString literalStringPoco => literalStringPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.LiteralString)dto),
                Core.POCO.Root.Namespaces.Membership membershipPoco => membershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Namespaces.Membership)dto),
                Core.POCO.Systems.Views.MembershipExpose membershipExposePoco => membershipExposePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.MembershipExpose)dto),
                Core.POCO.Root.Namespaces.MembershipImport membershipImportPoco => membershipImportPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Namespaces.MembershipImport)dto),
                Core.POCO.Systems.Actions.MergeNode mergeNodePoco => mergeNodePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.MergeNode)dto),
                Core.POCO.Kernel.Metadata.Metaclass metaclassPoco => metaclassPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Metadata.Metaclass)dto),
                Core.POCO.Kernel.Expressions.MetadataAccessExpression metadataAccessExpressionPoco => metadataAccessExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.MetadataAccessExpression)dto),
                Core.POCO.Systems.Metadata.MetadataDefinition metadataDefinitionPoco => metadataDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Metadata.MetadataDefinition)dto),
                Core.POCO.Kernel.Metadata.MetadataFeature metadataFeaturePoco => metadataFeaturePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Metadata.MetadataFeature)dto),
                Core.POCO.Systems.Metadata.MetadataUsage metadataUsagePoco => metadataUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Metadata.MetadataUsage)dto),
                Core.POCO.Core.Types.Multiplicity multiplicityPoco => multiplicityPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.Multiplicity)dto),
                Core.POCO.Kernel.Multiplicities.MultiplicityRange multiplicityRangePoco => multiplicityRangePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Multiplicities.MultiplicityRange)dto),
                Core.POCO.Root.Namespaces.Namespace namespacePoco => namespacePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Namespaces.Namespace)dto),
                Core.POCO.Systems.Views.NamespaceExpose namespaceExposePoco => namespaceExposePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.NamespaceExpose)dto),
                Core.POCO.Root.Namespaces.NamespaceImport namespaceImportPoco => namespaceImportPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Namespaces.NamespaceImport)dto),
                Core.POCO.Kernel.Expressions.NullExpression nullExpressionPoco => nullExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.NullExpression)dto),
                Core.POCO.Systems.Cases.ObjectiveMembership objectiveMembershipPoco => objectiveMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Cases.ObjectiveMembership)dto),
                Core.POCO.Systems.Occurrences.OccurrenceDefinition occurrenceDefinitionPoco => occurrenceDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Occurrences.OccurrenceDefinition)dto),
                Core.POCO.Systems.Occurrences.OccurrenceUsage occurrenceUsagePoco => occurrenceUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Occurrences.OccurrenceUsage)dto),
                Core.POCO.Kernel.Expressions.OperatorExpression operatorExpressionPoco => operatorExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.OperatorExpression)dto),
                Core.POCO.Root.Namespaces.OwningMembership owningMembershipPoco => owningMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Namespaces.OwningMembership)dto),
                Core.POCO.Kernel.Packages.Package packagePoco => packagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Packages.Package)dto),
                Core.POCO.Kernel.Behaviors.ParameterMembership parameterMembershipPoco => parameterMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Behaviors.ParameterMembership)dto),
                Core.POCO.Systems.Parts.PartDefinition partDefinitionPoco => partDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Parts.PartDefinition)dto),
                Core.POCO.Systems.Parts.PartUsage partUsagePoco => partUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Parts.PartUsage)dto),
                Core.POCO.Kernel.Interactions.PayloadFeature payloadFeaturePoco => payloadFeaturePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Interactions.PayloadFeature)dto),
                Core.POCO.Systems.Actions.PerformActionUsage performActionUsagePoco => performActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.PerformActionUsage)dto),
                Core.POCO.Systems.Ports.PortConjugation portConjugationPoco => portConjugationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Ports.PortConjugation)dto),
                Core.POCO.Systems.Ports.PortDefinition portDefinitionPoco => portDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Ports.PortDefinition)dto),
                Core.POCO.Systems.Ports.PortUsage portUsagePoco => portUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Ports.PortUsage)dto),
                Core.POCO.Kernel.Functions.Predicate predicatePoco => predicatePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Functions.Predicate)dto),
                Core.POCO.Core.Features.Redefinition redefinitionPoco => redefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.Redefinition)dto),
                Core.POCO.Core.Features.ReferenceSubsetting referenceSubsettingPoco => referenceSubsettingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.ReferenceSubsetting)dto),
                Core.POCO.Systems.DefinitionAndUsage.ReferenceUsage referenceUsagePoco => referenceUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage)dto),
                Core.POCO.Systems.Views.RenderingDefinition renderingDefinitionPoco => renderingDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.RenderingDefinition)dto),
                Core.POCO.Systems.Views.RenderingUsage renderingUsagePoco => renderingUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.RenderingUsage)dto),
                Core.POCO.Systems.Requirements.RequirementConstraintMembership requirementConstraintMembershipPoco => requirementConstraintMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.RequirementConstraintMembership)dto),
                Core.POCO.Systems.Requirements.RequirementDefinition requirementDefinitionPoco => requirementDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.RequirementDefinition)dto),
                Core.POCO.Systems.Requirements.RequirementUsage requirementUsagePoco => requirementUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.RequirementUsage)dto),
                Core.POCO.Systems.VerificationCases.RequirementVerificationMembership requirementVerificationMembershipPoco => requirementVerificationMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.VerificationCases.RequirementVerificationMembership)dto),
                Core.POCO.Kernel.Functions.ResultExpressionMembership resultExpressionMembershipPoco => resultExpressionMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Functions.ResultExpressionMembership)dto),
                Core.POCO.Kernel.Functions.ReturnParameterMembership returnParameterMembershipPoco => returnParameterMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Functions.ReturnParameterMembership)dto),
                Core.POCO.Systems.Requirements.SatisfyRequirementUsage satisfyRequirementUsagePoco => satisfyRequirementUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.SatisfyRequirementUsage)dto),
                Core.POCO.Kernel.Expressions.SelectExpression selectExpressionPoco => selectExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Expressions.SelectExpression)dto),
                Core.POCO.Systems.Actions.SendActionUsage sendActionUsagePoco => sendActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.SendActionUsage)dto),
                Core.POCO.Core.Types.Specialization specializationPoco => specializationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.Specialization)dto),
                Core.POCO.Systems.Requirements.StakeholderMembership stakeholderMembershipPoco => stakeholderMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.StakeholderMembership)dto),
                Core.POCO.Systems.States.StateDefinition stateDefinitionPoco => stateDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.States.StateDefinition)dto),
                Core.POCO.Systems.States.StateSubactionMembership stateSubactionMembershipPoco => stateSubactionMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.States.StateSubactionMembership)dto),
                Core.POCO.Systems.States.StateUsage stateUsagePoco => stateUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.States.StateUsage)dto),
                Core.POCO.Kernel.Behaviors.Step stepPoco => stepPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Behaviors.Step)dto),
                Core.POCO.Kernel.Structures.Structure structurePoco => structurePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Structures.Structure)dto),
                Core.POCO.Core.Classifiers.Subclassification subclassificationPoco => subclassificationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Classifiers.Subclassification)dto),
                Core.POCO.Systems.Requirements.SubjectMembership subjectMembershipPoco => subjectMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Requirements.SubjectMembership)dto),
                Core.POCO.Core.Features.Subsetting subsettingPoco => subsettingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.Subsetting)dto),
                Core.POCO.Kernel.Connectors.Succession successionPoco => successionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Connectors.Succession)dto),
                Core.POCO.Systems.Connections.SuccessionAsUsage successionAsUsagePoco => successionAsUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Connections.SuccessionAsUsage)dto),
                Core.POCO.Kernel.Interactions.SuccessionFlow successionFlowPoco => successionFlowPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Kernel.Interactions.SuccessionFlow)dto),
                Core.POCO.Systems.Flows.SuccessionFlowUsage successionFlowUsagePoco => successionFlowUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Flows.SuccessionFlowUsage)dto),
                Core.POCO.Systems.Actions.TerminateActionUsage terminateActionUsagePoco => terminateActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.TerminateActionUsage)dto),
                Core.POCO.Root.Annotations.TextualRepresentation textualRepresentationPoco => textualRepresentationPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Root.Annotations.TextualRepresentation)dto),
                Core.POCO.Systems.States.TransitionFeatureMembership transitionFeatureMembershipPoco => transitionFeatureMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.States.TransitionFeatureMembership)dto),
                Core.POCO.Systems.States.TransitionUsage transitionUsagePoco => transitionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.States.TransitionUsage)dto),
                Core.POCO.Systems.Actions.TriggerInvocationExpression triggerInvocationExpressionPoco => triggerInvocationExpressionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.TriggerInvocationExpression)dto),
                Core.POCO.Core.Types.Type typePoco => typePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.Type)dto),
                Core.POCO.Core.Features.TypeFeaturing typeFeaturingPoco => typeFeaturingPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Features.TypeFeaturing)dto),
                Core.POCO.Core.Types.Unioning unioningPoco => unioningPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Core.Types.Unioning)dto),
                Core.POCO.Systems.DefinitionAndUsage.Usage usagePoco => usagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.DefinitionAndUsage.Usage)dto),
                Core.POCO.Systems.UseCases.UseCaseDefinition useCaseDefinitionPoco => useCaseDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.UseCases.UseCaseDefinition)dto),
                Core.POCO.Systems.UseCases.UseCaseUsage useCaseUsagePoco => useCaseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.UseCases.UseCaseUsage)dto),
                Core.POCO.Systems.DefinitionAndUsage.VariantMembership variantMembershipPoco => variantMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.DefinitionAndUsage.VariantMembership)dto),
                Core.POCO.Systems.VerificationCases.VerificationCaseDefinition verificationCaseDefinitionPoco => verificationCaseDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.VerificationCases.VerificationCaseDefinition)dto),
                Core.POCO.Systems.VerificationCases.VerificationCaseUsage verificationCaseUsagePoco => verificationCaseUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.VerificationCases.VerificationCaseUsage)dto),
                Core.POCO.Systems.Views.ViewDefinition viewDefinitionPoco => viewDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.ViewDefinition)dto),
                Core.POCO.Systems.Views.ViewpointDefinition viewpointDefinitionPoco => viewpointDefinitionPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.ViewpointDefinition)dto),
                Core.POCO.Systems.Views.ViewpointUsage viewpointUsagePoco => viewpointUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.ViewpointUsage)dto),
                Core.POCO.Systems.Views.ViewRenderingMembership viewRenderingMembershipPoco => viewRenderingMembershipPoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.ViewRenderingMembership)dto),
                Core.POCO.Systems.Views.ViewUsage viewUsagePoco => viewUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Views.ViewUsage)dto),
                Core.POCO.Systems.Actions.WhileLoopActionUsage whileLoopActionUsagePoco => whileLoopActionUsagePoco.UpdateValueAndRemoveDeletedReferenceProperties((Core.DTO.Systems.Actions.WhileLoopActionUsage)dto),
                _ => throw new NotSupportedException($"{poco.GetType().Name} not yet supported")
            };
        }

        /// <summary>
        /// Updates the Reference properties of the <see cref="Core.POCO.Root.Elements.IElement"/> using the data (identifiers) encapsulated in the DTO
        /// and the provided cache to find the referenced object.
        /// </summary>
        /// <param name="poco">
        /// The <see cref="Core.POCO.Root.Elements.IElement"/> that is to be updated
        /// </param>
        /// <param name="dto">
        /// The DTO that is used to update the <see cref="Core.DTO.Root.Elements.IElement"/> with
        /// </param>
        /// <param name="cache">
        /// The <see cref="ConcurrentDictionary{String, Lazy}"/> that contains the
        /// ModelThings that are know and cached.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateReferenceProperties(this Core.POCO.Root.Elements.IElement poco, Core.DTO.Root.Elements.IElement dto, ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>> cache)
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
                case Core.POCO.Systems.Actions.AcceptActionUsage acceptActionUsagePoco:
                    acceptActionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.AcceptActionUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.ActionDefinition actionDefinitionPoco:
                    actionDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.ActionDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.ActionUsage actionUsagePoco:
                    actionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.ActionUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.ActorMembership actorMembershipPoco:
                    actorMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.ActorMembership)dto, cache);
                    break;
                case Core.POCO.Systems.Allocations.AllocationDefinition allocationDefinitionPoco:
                    allocationDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Allocations.AllocationDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Allocations.AllocationUsage allocationUsagePoco:
                    allocationUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Allocations.AllocationUsage)dto, cache);
                    break;
                case Core.POCO.Systems.AnalysisCases.AnalysisCaseDefinition analysisCaseDefinitionPoco:
                    analysisCaseDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.AnalysisCases.AnalysisCaseUsage analysisCaseUsagePoco:
                    analysisCaseUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage)dto, cache);
                    break;
                case Core.POCO.Root.Annotations.AnnotatingElement annotatingElementPoco:
                    annotatingElementPoco.UpdateReferenceProperties((Core.DTO.Root.Annotations.AnnotatingElement)dto, cache);
                    break;
                case Core.POCO.Root.Annotations.Annotation annotationPoco:
                    annotationPoco.UpdateReferenceProperties((Core.DTO.Root.Annotations.Annotation)dto, cache);
                    break;
                case Core.POCO.Systems.Constraints.AssertConstraintUsage assertConstraintUsagePoco:
                    assertConstraintUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Constraints.AssertConstraintUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.AssignmentActionUsage assignmentActionUsagePoco:
                    assignmentActionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.AssignmentActionUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Associations.Association associationPoco:
                    associationPoco.UpdateReferenceProperties((Core.DTO.Kernel.Associations.Association)dto, cache);
                    break;
                case Core.POCO.Kernel.Associations.AssociationStructure associationStructurePoco:
                    associationStructurePoco.UpdateReferenceProperties((Core.DTO.Kernel.Associations.AssociationStructure)dto, cache);
                    break;
                case Core.POCO.Systems.Attributes.AttributeDefinition attributeDefinitionPoco:
                    attributeDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Attributes.AttributeDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Attributes.AttributeUsage attributeUsagePoco:
                    attributeUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Attributes.AttributeUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Behaviors.Behavior behaviorPoco:
                    behaviorPoco.UpdateReferenceProperties((Core.DTO.Kernel.Behaviors.Behavior)dto, cache);
                    break;
                case Core.POCO.Kernel.Connectors.BindingConnector bindingConnectorPoco:
                    bindingConnectorPoco.UpdateReferenceProperties((Core.DTO.Kernel.Connectors.BindingConnector)dto, cache);
                    break;
                case Core.POCO.Systems.Connections.BindingConnectorAsUsage bindingConnectorAsUsagePoco:
                    bindingConnectorAsUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Connections.BindingConnectorAsUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Functions.BooleanExpression booleanExpressionPoco:
                    booleanExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Functions.BooleanExpression)dto, cache);
                    break;
                case Core.POCO.Systems.Calculations.CalculationDefinition calculationDefinitionPoco:
                    calculationDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Calculations.CalculationDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Calculations.CalculationUsage calculationUsagePoco:
                    calculationUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Calculations.CalculationUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Cases.CaseDefinition caseDefinitionPoco:
                    caseDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Cases.CaseDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Cases.CaseUsage caseUsagePoco:
                    caseUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Cases.CaseUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Classes.Class classPoco:
                    classPoco.UpdateReferenceProperties((Core.DTO.Kernel.Classes.Class)dto, cache);
                    break;
                case Core.POCO.Core.Classifiers.Classifier classifierPoco:
                    classifierPoco.UpdateReferenceProperties((Core.DTO.Core.Classifiers.Classifier)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.CollectExpression collectExpressionPoco:
                    collectExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.CollectExpression)dto, cache);
                    break;
                case Core.POCO.Root.Annotations.Comment commentPoco:
                    commentPoco.UpdateReferenceProperties((Core.DTO.Root.Annotations.Comment)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.ConcernDefinition concernDefinitionPoco:
                    concernDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.ConcernDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.ConcernUsage concernUsagePoco:
                    concernUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.ConcernUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Ports.ConjugatedPortDefinition conjugatedPortDefinitionPoco:
                    conjugatedPortDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Ports.ConjugatedPortDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Ports.ConjugatedPortTyping conjugatedPortTypingPoco:
                    conjugatedPortTypingPoco.UpdateReferenceProperties((Core.DTO.Systems.Ports.ConjugatedPortTyping)dto, cache);
                    break;
                case Core.POCO.Core.Types.Conjugation conjugationPoco:
                    conjugationPoco.UpdateReferenceProperties((Core.DTO.Core.Types.Conjugation)dto, cache);
                    break;
                case Core.POCO.Systems.Connections.ConnectionDefinition connectionDefinitionPoco:
                    connectionDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Connections.ConnectionDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Connections.ConnectionUsage connectionUsagePoco:
                    connectionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Connections.ConnectionUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Connectors.Connector connectorPoco:
                    connectorPoco.UpdateReferenceProperties((Core.DTO.Kernel.Connectors.Connector)dto, cache);
                    break;
                case Core.POCO.Systems.Constraints.ConstraintDefinition constraintDefinitionPoco:
                    constraintDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Constraints.ConstraintDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Constraints.ConstraintUsage constraintUsagePoco:
                    constraintUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Constraints.ConstraintUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.ConstructorExpression constructorExpressionPoco:
                    constructorExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.ConstructorExpression)dto, cache);
                    break;
                case Core.POCO.Core.Features.CrossSubsetting crossSubsettingPoco:
                    crossSubsettingPoco.UpdateReferenceProperties((Core.DTO.Core.Features.CrossSubsetting)dto, cache);
                    break;
                case Core.POCO.Kernel.DataTypes.DataType dataTypePoco:
                    dataTypePoco.UpdateReferenceProperties((Core.DTO.Kernel.DataTypes.DataType)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.DecisionNode decisionNodePoco:
                    decisionNodePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.DecisionNode)dto, cache);
                    break;
                case Core.POCO.Systems.DefinitionAndUsage.Definition definitionPoco:
                    definitionPoco.UpdateReferenceProperties((Core.DTO.Systems.DefinitionAndUsage.Definition)dto, cache);
                    break;
                case Core.POCO.Root.Dependencies.Dependency dependencyPoco:
                    dependencyPoco.UpdateReferenceProperties((Core.DTO.Root.Dependencies.Dependency)dto, cache);
                    break;
                case Core.POCO.Core.Types.Differencing differencingPoco:
                    differencingPoco.UpdateReferenceProperties((Core.DTO.Core.Types.Differencing)dto, cache);
                    break;
                case Core.POCO.Core.Types.Disjoining disjoiningPoco:
                    disjoiningPoco.UpdateReferenceProperties((Core.DTO.Core.Types.Disjoining)dto, cache);
                    break;
                case Core.POCO.Root.Annotations.Documentation documentationPoco:
                    documentationPoco.UpdateReferenceProperties((Core.DTO.Root.Annotations.Documentation)dto, cache);
                    break;
                case Core.POCO.Kernel.Packages.ElementFilterMembership elementFilterMembershipPoco:
                    elementFilterMembershipPoco.UpdateReferenceProperties((Core.DTO.Kernel.Packages.ElementFilterMembership)dto, cache);
                    break;
                case Core.POCO.Core.Features.EndFeatureMembership endFeatureMembershipPoco:
                    endFeatureMembershipPoco.UpdateReferenceProperties((Core.DTO.Core.Features.EndFeatureMembership)dto, cache);
                    break;
                case Core.POCO.Systems.Enumerations.EnumerationDefinition enumerationDefinitionPoco:
                    enumerationDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Enumerations.EnumerationDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Enumerations.EnumerationUsage enumerationUsagePoco:
                    enumerationUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Enumerations.EnumerationUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Occurrences.EventOccurrenceUsage eventOccurrenceUsagePoco:
                    eventOccurrenceUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Occurrences.EventOccurrenceUsage)dto, cache);
                    break;
                case Core.POCO.Systems.States.ExhibitStateUsage exhibitStateUsagePoco:
                    exhibitStateUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.States.ExhibitStateUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Functions.Expression expressionPoco:
                    expressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Functions.Expression)dto, cache);
                    break;
                case Core.POCO.Core.Features.Feature featurePoco:
                    featurePoco.UpdateReferenceProperties((Core.DTO.Core.Features.Feature)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.FeatureChainExpression featureChainExpressionPoco:
                    featureChainExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.FeatureChainExpression)dto, cache);
                    break;
                case Core.POCO.Core.Features.FeatureChaining featureChainingPoco:
                    featureChainingPoco.UpdateReferenceProperties((Core.DTO.Core.Features.FeatureChaining)dto, cache);
                    break;
                case Core.POCO.Core.Features.FeatureInverting featureInvertingPoco:
                    featureInvertingPoco.UpdateReferenceProperties((Core.DTO.Core.Features.FeatureInverting)dto, cache);
                    break;
                case Core.POCO.Core.Types.FeatureMembership featureMembershipPoco:
                    featureMembershipPoco.UpdateReferenceProperties((Core.DTO.Core.Types.FeatureMembership)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.FeatureReferenceExpression featureReferenceExpressionPoco:
                    featureReferenceExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.FeatureReferenceExpression)dto, cache);
                    break;
                case Core.POCO.Core.Features.FeatureTyping featureTypingPoco:
                    featureTypingPoco.UpdateReferenceProperties((Core.DTO.Core.Features.FeatureTyping)dto, cache);
                    break;
                case Core.POCO.Kernel.FeatureValues.FeatureValue featureValuePoco:
                    featureValuePoco.UpdateReferenceProperties((Core.DTO.Kernel.FeatureValues.FeatureValue)dto, cache);
                    break;
                case Core.POCO.Kernel.Interactions.Flow flowPoco:
                    flowPoco.UpdateReferenceProperties((Core.DTO.Kernel.Interactions.Flow)dto, cache);
                    break;
                case Core.POCO.Systems.Flows.FlowDefinition flowDefinitionPoco:
                    flowDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Flows.FlowDefinition)dto, cache);
                    break;
                case Core.POCO.Kernel.Interactions.FlowEnd flowEndPoco:
                    flowEndPoco.UpdateReferenceProperties((Core.DTO.Kernel.Interactions.FlowEnd)dto, cache);
                    break;
                case Core.POCO.Systems.Flows.FlowUsage flowUsagePoco:
                    flowUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Flows.FlowUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.ForkNode forkNodePoco:
                    forkNodePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.ForkNode)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.ForLoopActionUsage forLoopActionUsagePoco:
                    forLoopActionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.ForLoopActionUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.FramedConcernMembership framedConcernMembershipPoco:
                    framedConcernMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.FramedConcernMembership)dto, cache);
                    break;
                case Core.POCO.Kernel.Functions.Function functionPoco:
                    functionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Functions.Function)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.IfActionUsage ifActionUsagePoco:
                    ifActionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.IfActionUsage)dto, cache);
                    break;
                case Core.POCO.Systems.UseCases.IncludeUseCaseUsage includeUseCaseUsagePoco:
                    includeUseCaseUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.UseCases.IncludeUseCaseUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.IndexExpression indexExpressionPoco:
                    indexExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.IndexExpression)dto, cache);
                    break;
                case Core.POCO.Kernel.Interactions.Interaction interactionPoco:
                    interactionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Interactions.Interaction)dto, cache);
                    break;
                case Core.POCO.Systems.Interfaces.InterfaceDefinition interfaceDefinitionPoco:
                    interfaceDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Interfaces.InterfaceDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Interfaces.InterfaceUsage interfaceUsagePoco:
                    interfaceUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Interfaces.InterfaceUsage)dto, cache);
                    break;
                case Core.POCO.Core.Types.Intersecting intersectingPoco:
                    intersectingPoco.UpdateReferenceProperties((Core.DTO.Core.Types.Intersecting)dto, cache);
                    break;
                case Core.POCO.Kernel.Functions.Invariant invariantPoco:
                    invariantPoco.UpdateReferenceProperties((Core.DTO.Kernel.Functions.Invariant)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.InvocationExpression invocationExpressionPoco:
                    invocationExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.InvocationExpression)dto, cache);
                    break;
                case Core.POCO.Systems.Items.ItemDefinition itemDefinitionPoco:
                    itemDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Items.ItemDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Items.ItemUsage itemUsagePoco:
                    itemUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Items.ItemUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.JoinNode joinNodePoco:
                    joinNodePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.JoinNode)dto, cache);
                    break;
                case Core.POCO.Kernel.Packages.LibraryPackage libraryPackagePoco:
                    libraryPackagePoco.UpdateReferenceProperties((Core.DTO.Kernel.Packages.LibraryPackage)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.LiteralBoolean literalBooleanPoco:
                    literalBooleanPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.LiteralBoolean)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.LiteralExpression literalExpressionPoco:
                    literalExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.LiteralExpression)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.LiteralInfinity literalInfinityPoco:
                    literalInfinityPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.LiteralInfinity)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.LiteralInteger literalIntegerPoco:
                    literalIntegerPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.LiteralInteger)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.LiteralRational literalRationalPoco:
                    literalRationalPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.LiteralRational)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.LiteralString literalStringPoco:
                    literalStringPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.LiteralString)dto, cache);
                    break;
                case Core.POCO.Root.Namespaces.Membership membershipPoco:
                    membershipPoco.UpdateReferenceProperties((Core.DTO.Root.Namespaces.Membership)dto, cache);
                    break;
                case Core.POCO.Systems.Views.MembershipExpose membershipExposePoco:
                    membershipExposePoco.UpdateReferenceProperties((Core.DTO.Systems.Views.MembershipExpose)dto, cache);
                    break;
                case Core.POCO.Root.Namespaces.MembershipImport membershipImportPoco:
                    membershipImportPoco.UpdateReferenceProperties((Core.DTO.Root.Namespaces.MembershipImport)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.MergeNode mergeNodePoco:
                    mergeNodePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.MergeNode)dto, cache);
                    break;
                case Core.POCO.Kernel.Metadata.Metaclass metaclassPoco:
                    metaclassPoco.UpdateReferenceProperties((Core.DTO.Kernel.Metadata.Metaclass)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.MetadataAccessExpression metadataAccessExpressionPoco:
                    metadataAccessExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.MetadataAccessExpression)dto, cache);
                    break;
                case Core.POCO.Systems.Metadata.MetadataDefinition metadataDefinitionPoco:
                    metadataDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Metadata.MetadataDefinition)dto, cache);
                    break;
                case Core.POCO.Kernel.Metadata.MetadataFeature metadataFeaturePoco:
                    metadataFeaturePoco.UpdateReferenceProperties((Core.DTO.Kernel.Metadata.MetadataFeature)dto, cache);
                    break;
                case Core.POCO.Systems.Metadata.MetadataUsage metadataUsagePoco:
                    metadataUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Metadata.MetadataUsage)dto, cache);
                    break;
                case Core.POCO.Core.Types.Multiplicity multiplicityPoco:
                    multiplicityPoco.UpdateReferenceProperties((Core.DTO.Core.Types.Multiplicity)dto, cache);
                    break;
                case Core.POCO.Kernel.Multiplicities.MultiplicityRange multiplicityRangePoco:
                    multiplicityRangePoco.UpdateReferenceProperties((Core.DTO.Kernel.Multiplicities.MultiplicityRange)dto, cache);
                    break;
                case Core.POCO.Root.Namespaces.Namespace namespacePoco:
                    namespacePoco.UpdateReferenceProperties((Core.DTO.Root.Namespaces.Namespace)dto, cache);
                    break;
                case Core.POCO.Systems.Views.NamespaceExpose namespaceExposePoco:
                    namespaceExposePoco.UpdateReferenceProperties((Core.DTO.Systems.Views.NamespaceExpose)dto, cache);
                    break;
                case Core.POCO.Root.Namespaces.NamespaceImport namespaceImportPoco:
                    namespaceImportPoco.UpdateReferenceProperties((Core.DTO.Root.Namespaces.NamespaceImport)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.NullExpression nullExpressionPoco:
                    nullExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.NullExpression)dto, cache);
                    break;
                case Core.POCO.Systems.Cases.ObjectiveMembership objectiveMembershipPoco:
                    objectiveMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.Cases.ObjectiveMembership)dto, cache);
                    break;
                case Core.POCO.Systems.Occurrences.OccurrenceDefinition occurrenceDefinitionPoco:
                    occurrenceDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Occurrences.OccurrenceDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Occurrences.OccurrenceUsage occurrenceUsagePoco:
                    occurrenceUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Occurrences.OccurrenceUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.OperatorExpression operatorExpressionPoco:
                    operatorExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.OperatorExpression)dto, cache);
                    break;
                case Core.POCO.Root.Namespaces.OwningMembership owningMembershipPoco:
                    owningMembershipPoco.UpdateReferenceProperties((Core.DTO.Root.Namespaces.OwningMembership)dto, cache);
                    break;
                case Core.POCO.Kernel.Packages.Package packagePoco:
                    packagePoco.UpdateReferenceProperties((Core.DTO.Kernel.Packages.Package)dto, cache);
                    break;
                case Core.POCO.Kernel.Behaviors.ParameterMembership parameterMembershipPoco:
                    parameterMembershipPoco.UpdateReferenceProperties((Core.DTO.Kernel.Behaviors.ParameterMembership)dto, cache);
                    break;
                case Core.POCO.Systems.Parts.PartDefinition partDefinitionPoco:
                    partDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Parts.PartDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Parts.PartUsage partUsagePoco:
                    partUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Parts.PartUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Interactions.PayloadFeature payloadFeaturePoco:
                    payloadFeaturePoco.UpdateReferenceProperties((Core.DTO.Kernel.Interactions.PayloadFeature)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.PerformActionUsage performActionUsagePoco:
                    performActionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.PerformActionUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Ports.PortConjugation portConjugationPoco:
                    portConjugationPoco.UpdateReferenceProperties((Core.DTO.Systems.Ports.PortConjugation)dto, cache);
                    break;
                case Core.POCO.Systems.Ports.PortDefinition portDefinitionPoco:
                    portDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Ports.PortDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Ports.PortUsage portUsagePoco:
                    portUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Ports.PortUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Functions.Predicate predicatePoco:
                    predicatePoco.UpdateReferenceProperties((Core.DTO.Kernel.Functions.Predicate)dto, cache);
                    break;
                case Core.POCO.Core.Features.Redefinition redefinitionPoco:
                    redefinitionPoco.UpdateReferenceProperties((Core.DTO.Core.Features.Redefinition)dto, cache);
                    break;
                case Core.POCO.Core.Features.ReferenceSubsetting referenceSubsettingPoco:
                    referenceSubsettingPoco.UpdateReferenceProperties((Core.DTO.Core.Features.ReferenceSubsetting)dto, cache);
                    break;
                case Core.POCO.Systems.DefinitionAndUsage.ReferenceUsage referenceUsagePoco:
                    referenceUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Views.RenderingDefinition renderingDefinitionPoco:
                    renderingDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Views.RenderingDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Views.RenderingUsage renderingUsagePoco:
                    renderingUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Views.RenderingUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.RequirementConstraintMembership requirementConstraintMembershipPoco:
                    requirementConstraintMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.RequirementConstraintMembership)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.RequirementDefinition requirementDefinitionPoco:
                    requirementDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.RequirementDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.RequirementUsage requirementUsagePoco:
                    requirementUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.RequirementUsage)dto, cache);
                    break;
                case Core.POCO.Systems.VerificationCases.RequirementVerificationMembership requirementVerificationMembershipPoco:
                    requirementVerificationMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.VerificationCases.RequirementVerificationMembership)dto, cache);
                    break;
                case Core.POCO.Kernel.Functions.ResultExpressionMembership resultExpressionMembershipPoco:
                    resultExpressionMembershipPoco.UpdateReferenceProperties((Core.DTO.Kernel.Functions.ResultExpressionMembership)dto, cache);
                    break;
                case Core.POCO.Kernel.Functions.ReturnParameterMembership returnParameterMembershipPoco:
                    returnParameterMembershipPoco.UpdateReferenceProperties((Core.DTO.Kernel.Functions.ReturnParameterMembership)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.SatisfyRequirementUsage satisfyRequirementUsagePoco:
                    satisfyRequirementUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.SatisfyRequirementUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Expressions.SelectExpression selectExpressionPoco:
                    selectExpressionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Expressions.SelectExpression)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.SendActionUsage sendActionUsagePoco:
                    sendActionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.SendActionUsage)dto, cache);
                    break;
                case Core.POCO.Core.Types.Specialization specializationPoco:
                    specializationPoco.UpdateReferenceProperties((Core.DTO.Core.Types.Specialization)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.StakeholderMembership stakeholderMembershipPoco:
                    stakeholderMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.StakeholderMembership)dto, cache);
                    break;
                case Core.POCO.Systems.States.StateDefinition stateDefinitionPoco:
                    stateDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.States.StateDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.States.StateSubactionMembership stateSubactionMembershipPoco:
                    stateSubactionMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.States.StateSubactionMembership)dto, cache);
                    break;
                case Core.POCO.Systems.States.StateUsage stateUsagePoco:
                    stateUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.States.StateUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Behaviors.Step stepPoco:
                    stepPoco.UpdateReferenceProperties((Core.DTO.Kernel.Behaviors.Step)dto, cache);
                    break;
                case Core.POCO.Kernel.Structures.Structure structurePoco:
                    structurePoco.UpdateReferenceProperties((Core.DTO.Kernel.Structures.Structure)dto, cache);
                    break;
                case Core.POCO.Core.Classifiers.Subclassification subclassificationPoco:
                    subclassificationPoco.UpdateReferenceProperties((Core.DTO.Core.Classifiers.Subclassification)dto, cache);
                    break;
                case Core.POCO.Systems.Requirements.SubjectMembership subjectMembershipPoco:
                    subjectMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.Requirements.SubjectMembership)dto, cache);
                    break;
                case Core.POCO.Core.Features.Subsetting subsettingPoco:
                    subsettingPoco.UpdateReferenceProperties((Core.DTO.Core.Features.Subsetting)dto, cache);
                    break;
                case Core.POCO.Kernel.Connectors.Succession successionPoco:
                    successionPoco.UpdateReferenceProperties((Core.DTO.Kernel.Connectors.Succession)dto, cache);
                    break;
                case Core.POCO.Systems.Connections.SuccessionAsUsage successionAsUsagePoco:
                    successionAsUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Connections.SuccessionAsUsage)dto, cache);
                    break;
                case Core.POCO.Kernel.Interactions.SuccessionFlow successionFlowPoco:
                    successionFlowPoco.UpdateReferenceProperties((Core.DTO.Kernel.Interactions.SuccessionFlow)dto, cache);
                    break;
                case Core.POCO.Systems.Flows.SuccessionFlowUsage successionFlowUsagePoco:
                    successionFlowUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Flows.SuccessionFlowUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.TerminateActionUsage terminateActionUsagePoco:
                    terminateActionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.TerminateActionUsage)dto, cache);
                    break;
                case Core.POCO.Root.Annotations.TextualRepresentation textualRepresentationPoco:
                    textualRepresentationPoco.UpdateReferenceProperties((Core.DTO.Root.Annotations.TextualRepresentation)dto, cache);
                    break;
                case Core.POCO.Systems.States.TransitionFeatureMembership transitionFeatureMembershipPoco:
                    transitionFeatureMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.States.TransitionFeatureMembership)dto, cache);
                    break;
                case Core.POCO.Systems.States.TransitionUsage transitionUsagePoco:
                    transitionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.States.TransitionUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.TriggerInvocationExpression triggerInvocationExpressionPoco:
                    triggerInvocationExpressionPoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.TriggerInvocationExpression)dto, cache);
                    break;
                case Core.POCO.Core.Types.Type typePoco:
                    typePoco.UpdateReferenceProperties((Core.DTO.Core.Types.Type)dto, cache);
                    break;
                case Core.POCO.Core.Features.TypeFeaturing typeFeaturingPoco:
                    typeFeaturingPoco.UpdateReferenceProperties((Core.DTO.Core.Features.TypeFeaturing)dto, cache);
                    break;
                case Core.POCO.Core.Types.Unioning unioningPoco:
                    unioningPoco.UpdateReferenceProperties((Core.DTO.Core.Types.Unioning)dto, cache);
                    break;
                case Core.POCO.Systems.DefinitionAndUsage.Usage usagePoco:
                    usagePoco.UpdateReferenceProperties((Core.DTO.Systems.DefinitionAndUsage.Usage)dto, cache);
                    break;
                case Core.POCO.Systems.UseCases.UseCaseDefinition useCaseDefinitionPoco:
                    useCaseDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.UseCases.UseCaseDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.UseCases.UseCaseUsage useCaseUsagePoco:
                    useCaseUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.UseCases.UseCaseUsage)dto, cache);
                    break;
                case Core.POCO.Systems.DefinitionAndUsage.VariantMembership variantMembershipPoco:
                    variantMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.DefinitionAndUsage.VariantMembership)dto, cache);
                    break;
                case Core.POCO.Systems.VerificationCases.VerificationCaseDefinition verificationCaseDefinitionPoco:
                    verificationCaseDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.VerificationCases.VerificationCaseDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.VerificationCases.VerificationCaseUsage verificationCaseUsagePoco:
                    verificationCaseUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.VerificationCases.VerificationCaseUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Views.ViewDefinition viewDefinitionPoco:
                    viewDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Views.ViewDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Views.ViewpointDefinition viewpointDefinitionPoco:
                    viewpointDefinitionPoco.UpdateReferenceProperties((Core.DTO.Systems.Views.ViewpointDefinition)dto, cache);
                    break;
                case Core.POCO.Systems.Views.ViewpointUsage viewpointUsagePoco:
                    viewpointUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Views.ViewpointUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Views.ViewRenderingMembership viewRenderingMembershipPoco:
                    viewRenderingMembershipPoco.UpdateReferenceProperties((Core.DTO.Systems.Views.ViewRenderingMembership)dto, cache);
                    break;
                case Core.POCO.Systems.Views.ViewUsage viewUsagePoco:
                    viewUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Views.ViewUsage)dto, cache);
                    break;
                case Core.POCO.Systems.Actions.WhileLoopActionUsage whileLoopActionUsagePoco:
                    whileLoopActionUsagePoco.UpdateReferenceProperties((Core.DTO.Systems.Actions.WhileLoopActionUsage)dto, cache);
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
