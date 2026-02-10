// -------------------------------------------------------------------------------------------------
// <copyright file="PocoReferenceResolveExtensionsFacade.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi.Extensions
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;

    /// <summary>
    /// The <see cref="PocoReferenceResolveExtensionsFacade"/> provides access to extensions method for POCO <see cref="IData"/> to resolve reference
    /// </summary>
    public class PocoReferenceResolveExtensionsFacade : IPocoReferenceResolveExtensionsFacade
    {
        /// <summary>
        /// Resolve and assign single reference value for a specific property
        /// </summary>
        /// <param name="data">The <see cref="IData"/> that should have the value of a property to be set</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="reference">The identifier of the <see cref="IData"/> value to set</param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/></param>
        /// <param name="logger">The <see cref="ILogger" /> used to produce log statement</param>
        public void ResolveAndAssignSingleValueReference(IData data, string propertyName, Guid reference, IXmiDataCache xmiDataCache, ILogger logger)
        {
            switch (data)
            {
                case SysML2.NET.Core.POCO.Systems.Actions.AcceptActionUsage acceptActionUsagePoco:
                    acceptActionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ActionDefinition actionDefinitionPoco:
                    actionDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ActionUsage actionUsagePoco:
                    actionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.ActorMembership actorMembershipPoco:
                    actorMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Allocations.AllocationDefinition allocationDefinitionPoco:
                    allocationDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Allocations.AllocationUsage allocationUsagePoco:
                    allocationUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.AnalysisCases.AnalysisCaseDefinition analysisCaseDefinitionPoco:
                    analysisCaseDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.AnalysisCases.AnalysisCaseUsage analysisCaseUsagePoco:
                    analysisCaseUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement annotatingElementPoco:
                    annotatingElementPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.Annotation annotationPoco:
                    annotationPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage assertConstraintUsagePoco:
                    assertConstraintUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.AssignmentActionUsage assignmentActionUsagePoco:
                    assignmentActionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Associations.Association associationPoco:
                    associationPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Associations.AssociationStructure associationStructurePoco:
                    associationStructurePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Attributes.AttributeDefinition attributeDefinitionPoco:
                    attributeDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Attributes.AttributeUsage attributeUsagePoco:
                    attributeUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.Behavior behaviorPoco:
                    behaviorPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.BindingConnector bindingConnectorPoco:
                    bindingConnectorPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.BindingConnectorAsUsage bindingConnectorAsUsagePoco:
                    bindingConnectorAsUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.BooleanExpression booleanExpressionPoco:
                    booleanExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Calculations.CalculationDefinition calculationDefinitionPoco:
                    calculationDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Calculations.CalculationUsage calculationUsagePoco:
                    calculationUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Cases.CaseDefinition caseDefinitionPoco:
                    caseDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Cases.CaseUsage caseUsagePoco:
                    caseUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Classes.Class classPoco:
                    classPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Classifiers.Classifier classifierPoco:
                    classifierPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.CollectExpression collectExpressionPoco:
                    collectExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.Comment commentPoco:
                    commentPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.ConcernDefinition concernDefinitionPoco:
                    concernDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.ConcernUsage concernUsagePoco:
                    concernUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.ConjugatedPortDefinition conjugatedPortDefinitionPoco:
                    conjugatedPortDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.ConjugatedPortTyping conjugatedPortTypingPoco:
                    conjugatedPortTypingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Conjugation conjugationPoco:
                    conjugationPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.ConnectionDefinition connectionDefinitionPoco:
                    connectionDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.ConnectionUsage connectionUsagePoco:
                    connectionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.Connector connectorPoco:
                    connectorPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.ConstraintDefinition constraintDefinitionPoco:
                    constraintDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.ConstraintUsage constraintUsagePoco:
                    constraintUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ConstructorExpression constructorExpressionPoco:
                    constructorExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.CrossSubsetting crossSubsettingPoco:
                    crossSubsettingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.DataTypes.DataType dataTypePoco:
                    dataTypePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.DecisionNode decisionNodePoco:
                    decisionNodePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.Definition definitionPoco:
                    definitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Dependencies.Dependency dependencyPoco:
                    dependencyPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Differencing differencingPoco:
                    differencingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Disjoining disjoiningPoco:
                    disjoiningPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.Documentation documentationPoco:
                    documentationPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.ElementFilterMembership elementFilterMembershipPoco:
                    elementFilterMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.EndFeatureMembership endFeatureMembershipPoco:
                    endFeatureMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Enumerations.EnumerationDefinition enumerationDefinitionPoco:
                    enumerationDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Enumerations.EnumerationUsage enumerationUsagePoco:
                    enumerationUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.EventOccurrenceUsage eventOccurrenceUsagePoco:
                    eventOccurrenceUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.ExhibitStateUsage exhibitStateUsagePoco:
                    exhibitStateUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Expression expressionPoco:
                    expressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.Feature featurePoco:
                    featurePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.FeatureChainExpression featureChainExpressionPoco:
                    featureChainExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.FeatureChaining featureChainingPoco:
                    featureChainingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.FeatureInverting featureInvertingPoco:
                    featureInvertingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.FeatureMembership featureMembershipPoco:
                    featureMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression featureReferenceExpressionPoco:
                    featureReferenceExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.FeatureTyping featureTypingPoco:
                    featureTypingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue featureValuePoco:
                    featureValuePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.Flow flowPoco:
                    flowPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.FlowDefinition flowDefinitionPoco:
                    flowDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.FlowEnd flowEndPoco:
                    flowEndPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.FlowUsage flowUsagePoco:
                    flowUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ForkNode forkNodePoco:
                    forkNodePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ForLoopActionUsage forLoopActionUsagePoco:
                    forLoopActionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.FramedConcernMembership framedConcernMembershipPoco:
                    framedConcernMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Function functionPoco:
                    functionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IfActionUsage ifActionUsagePoco:
                    ifActionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.IncludeUseCaseUsage includeUseCaseUsagePoco:
                    includeUseCaseUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IndexExpression indexExpressionPoco:
                    indexExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.Interaction interactionPoco:
                    interactionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceDefinition interfaceDefinitionPoco:
                    interfaceDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceUsage interfaceUsagePoco:
                    interfaceUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Intersecting intersectingPoco:
                    intersectingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Invariant invariantPoco:
                    invariantPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.InvocationExpression invocationExpressionPoco:
                    invocationExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Items.ItemDefinition itemDefinitionPoco:
                    itemDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Items.ItemUsage itemUsagePoco:
                    itemUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.JoinNode joinNodePoco:
                    joinNodePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.LibraryPackage libraryPackagePoco:
                    libraryPackagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralBoolean literalBooleanPoco:
                    literalBooleanPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralExpression literalExpressionPoco:
                    literalExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralInfinity literalInfinityPoco:
                    literalInfinityPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralInteger literalIntegerPoco:
                    literalIntegerPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralRational literalRationalPoco:
                    literalRationalPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralString literalStringPoco:
                    literalStringPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.Membership membershipPoco:
                    membershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.MembershipExpose membershipExposePoco:
                    membershipExposePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.MembershipImport membershipImportPoco:
                    membershipImportPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.MergeNode mergeNodePoco:
                    mergeNodePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Metadata.Metaclass metaclassPoco:
                    metaclassPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.MetadataAccessExpression metadataAccessExpressionPoco:
                    metadataAccessExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Metadata.MetadataDefinition metadataDefinitionPoco:
                    metadataDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Metadata.MetadataFeature metadataFeaturePoco:
                    metadataFeaturePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Metadata.MetadataUsage metadataUsagePoco:
                    metadataUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Multiplicity multiplicityPoco:
                    multiplicityPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Multiplicities.MultiplicityRange multiplicityRangePoco:
                    multiplicityRangePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.Namespace namespacePoco:
                    namespacePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.NamespaceExpose namespaceExposePoco:
                    namespaceExposePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.NamespaceImport namespaceImportPoco:
                    namespaceImportPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.NullExpression nullExpressionPoco:
                    nullExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Cases.ObjectiveMembership objectiveMembershipPoco:
                    objectiveMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.OccurrenceDefinition occurrenceDefinitionPoco:
                    occurrenceDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.OccurrenceUsage occurrenceUsagePoco:
                    occurrenceUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.OperatorExpression operatorExpressionPoco:
                    operatorExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership owningMembershipPoco:
                    owningMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.Package packagePoco:
                    packagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.ParameterMembership parameterMembershipPoco:
                    parameterMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Parts.PartDefinition partDefinitionPoco:
                    partDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Parts.PartUsage partUsagePoco:
                    partUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.PayloadFeature payloadFeaturePoco:
                    payloadFeaturePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.PerformActionUsage performActionUsagePoco:
                    performActionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.PortConjugation portConjugationPoco:
                    portConjugationPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.PortDefinition portDefinitionPoco:
                    portDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.PortUsage portUsagePoco:
                    portUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Predicate predicatePoco:
                    predicatePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.Redefinition redefinitionPoco:
                    redefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.ReferenceSubsetting referenceSubsettingPoco:
                    referenceSubsettingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.ReferenceUsage referenceUsagePoco:
                    referenceUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.RenderingDefinition renderingDefinitionPoco:
                    renderingDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.RenderingUsage renderingUsagePoco:
                    renderingUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementConstraintMembership requirementConstraintMembershipPoco:
                    requirementConstraintMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementDefinition requirementDefinitionPoco:
                    requirementDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementUsage requirementUsagePoco:
                    requirementUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.VerificationCases.RequirementVerificationMembership requirementVerificationMembershipPoco:
                    requirementVerificationMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.ResultExpressionMembership resultExpressionMembershipPoco:
                    resultExpressionMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.ReturnParameterMembership returnParameterMembershipPoco:
                    returnParameterMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage satisfyRequirementUsagePoco:
                    satisfyRequirementUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.SelectExpression selectExpressionPoco:
                    selectExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.SendActionUsage sendActionUsagePoco:
                    sendActionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Specialization specializationPoco:
                    specializationPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.StakeholderMembership stakeholderMembershipPoco:
                    stakeholderMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.StateDefinition stateDefinitionPoco:
                    stateDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.StateSubactionMembership stateSubactionMembershipPoco:
                    stateSubactionMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.StateUsage stateUsagePoco:
                    stateUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.Step stepPoco:
                    stepPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Structures.Structure structurePoco:
                    structurePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Classifiers.Subclassification subclassificationPoco:
                    subclassificationPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.SubjectMembership subjectMembershipPoco:
                    subjectMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.Subsetting subsettingPoco:
                    subsettingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.Succession successionPoco:
                    successionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.SuccessionAsUsage successionAsUsagePoco:
                    successionAsUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.SuccessionFlow successionFlowPoco:
                    successionFlowPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.SuccessionFlowUsage successionFlowUsagePoco:
                    successionFlowUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.TerminateActionUsage terminateActionUsagePoco:
                    terminateActionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.TextualRepresentation textualRepresentationPoco:
                    textualRepresentationPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.TransitionFeatureMembership transitionFeatureMembershipPoco:
                    transitionFeatureMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.TransitionUsage transitionUsagePoco:
                    transitionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.TriggerInvocationExpression triggerInvocationExpressionPoco:
                    triggerInvocationExpressionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Type typePoco:
                    typePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.TypeFeaturing typeFeaturingPoco:
                    typeFeaturingPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Unioning unioningPoco:
                    unioningPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.Usage usagePoco:
                    usagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.UseCaseDefinition useCaseDefinitionPoco:
                    useCaseDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.UseCaseUsage useCaseUsagePoco:
                    useCaseUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.VariantMembership variantMembershipPoco:
                    variantMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.VerificationCases.VerificationCaseDefinition verificationCaseDefinitionPoco:
                    verificationCaseDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.VerificationCases.VerificationCaseUsage verificationCaseUsagePoco:
                    verificationCaseUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewDefinition viewDefinitionPoco:
                    viewDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewpointDefinition viewpointDefinitionPoco:
                    viewpointDefinitionPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewpointUsage viewpointUsagePoco:
                    viewpointUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewRenderingMembership viewRenderingMembershipPoco:
                    viewRenderingMembershipPoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewUsage viewUsagePoco:
                    viewUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.WhileLoopActionUsage whileLoopActionUsagePoco:
                    whileLoopActionUsagePoco.ResolveAndAssignSingleValueReference(propertyName, reference, xmiDataCache, logger);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(data), data, "Unsupported type");
            }
        }

        /// <summary>
        /// Resolve and assign multiple references value for a specific property
        /// </summary>
        /// <param name="data">The <see cref="IData"/> that should have the value of a property to be set</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="references">The collection of identifier values to set</param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/></param>
        /// <param name="logger">The <see cref="ILogger" /> used to produce log statement</param>
        public void ResolveAndAssignMultipleValueReferences(IData data, string propertyName, IReadOnlyCollection<Guid> references, IXmiDataCache xmiDataCache, ILogger logger)
        {
            switch (data)
            {
                case SysML2.NET.Core.POCO.Systems.Actions.AcceptActionUsage acceptActionUsagePoco:
                    acceptActionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ActionDefinition actionDefinitionPoco:
                    actionDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ActionUsage actionUsagePoco:
                    actionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.ActorMembership actorMembershipPoco:
                    actorMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Allocations.AllocationDefinition allocationDefinitionPoco:
                    allocationDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Allocations.AllocationUsage allocationUsagePoco:
                    allocationUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.AnalysisCases.AnalysisCaseDefinition analysisCaseDefinitionPoco:
                    analysisCaseDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.AnalysisCases.AnalysisCaseUsage analysisCaseUsagePoco:
                    analysisCaseUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement annotatingElementPoco:
                    annotatingElementPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.Annotation annotationPoco:
                    annotationPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage assertConstraintUsagePoco:
                    assertConstraintUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.AssignmentActionUsage assignmentActionUsagePoco:
                    assignmentActionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Associations.Association associationPoco:
                    associationPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Associations.AssociationStructure associationStructurePoco:
                    associationStructurePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Attributes.AttributeDefinition attributeDefinitionPoco:
                    attributeDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Attributes.AttributeUsage attributeUsagePoco:
                    attributeUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.Behavior behaviorPoco:
                    behaviorPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.BindingConnector bindingConnectorPoco:
                    bindingConnectorPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.BindingConnectorAsUsage bindingConnectorAsUsagePoco:
                    bindingConnectorAsUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.BooleanExpression booleanExpressionPoco:
                    booleanExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Calculations.CalculationDefinition calculationDefinitionPoco:
                    calculationDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Calculations.CalculationUsage calculationUsagePoco:
                    calculationUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Cases.CaseDefinition caseDefinitionPoco:
                    caseDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Cases.CaseUsage caseUsagePoco:
                    caseUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Classes.Class classPoco:
                    classPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Classifiers.Classifier classifierPoco:
                    classifierPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.CollectExpression collectExpressionPoco:
                    collectExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.Comment commentPoco:
                    commentPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.ConcernDefinition concernDefinitionPoco:
                    concernDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.ConcernUsage concernUsagePoco:
                    concernUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.ConjugatedPortDefinition conjugatedPortDefinitionPoco:
                    conjugatedPortDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.ConjugatedPortTyping conjugatedPortTypingPoco:
                    conjugatedPortTypingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Conjugation conjugationPoco:
                    conjugationPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.ConnectionDefinition connectionDefinitionPoco:
                    connectionDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.ConnectionUsage connectionUsagePoco:
                    connectionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.Connector connectorPoco:
                    connectorPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.ConstraintDefinition constraintDefinitionPoco:
                    constraintDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.ConstraintUsage constraintUsagePoco:
                    constraintUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ConstructorExpression constructorExpressionPoco:
                    constructorExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.CrossSubsetting crossSubsettingPoco:
                    crossSubsettingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.DataTypes.DataType dataTypePoco:
                    dataTypePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.DecisionNode decisionNodePoco:
                    decisionNodePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.Definition definitionPoco:
                    definitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Dependencies.Dependency dependencyPoco:
                    dependencyPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Differencing differencingPoco:
                    differencingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Disjoining disjoiningPoco:
                    disjoiningPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.Documentation documentationPoco:
                    documentationPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.ElementFilterMembership elementFilterMembershipPoco:
                    elementFilterMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.EndFeatureMembership endFeatureMembershipPoco:
                    endFeatureMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Enumerations.EnumerationDefinition enumerationDefinitionPoco:
                    enumerationDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Enumerations.EnumerationUsage enumerationUsagePoco:
                    enumerationUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.EventOccurrenceUsage eventOccurrenceUsagePoco:
                    eventOccurrenceUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.ExhibitStateUsage exhibitStateUsagePoco:
                    exhibitStateUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Expression expressionPoco:
                    expressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.Feature featurePoco:
                    featurePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.FeatureChainExpression featureChainExpressionPoco:
                    featureChainExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.FeatureChaining featureChainingPoco:
                    featureChainingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.FeatureInverting featureInvertingPoco:
                    featureInvertingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.FeatureMembership featureMembershipPoco:
                    featureMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression featureReferenceExpressionPoco:
                    featureReferenceExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.FeatureTyping featureTypingPoco:
                    featureTypingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue featureValuePoco:
                    featureValuePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.Flow flowPoco:
                    flowPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.FlowDefinition flowDefinitionPoco:
                    flowDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.FlowEnd flowEndPoco:
                    flowEndPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.FlowUsage flowUsagePoco:
                    flowUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ForkNode forkNodePoco:
                    forkNodePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ForLoopActionUsage forLoopActionUsagePoco:
                    forLoopActionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.FramedConcernMembership framedConcernMembershipPoco:
                    framedConcernMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Function functionPoco:
                    functionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.IfActionUsage ifActionUsagePoco:
                    ifActionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.IncludeUseCaseUsage includeUseCaseUsagePoco:
                    includeUseCaseUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.IndexExpression indexExpressionPoco:
                    indexExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.Interaction interactionPoco:
                    interactionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceDefinition interfaceDefinitionPoco:
                    interfaceDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceUsage interfaceUsagePoco:
                    interfaceUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Intersecting intersectingPoco:
                    intersectingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Invariant invariantPoco:
                    invariantPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.InvocationExpression invocationExpressionPoco:
                    invocationExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Items.ItemDefinition itemDefinitionPoco:
                    itemDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Items.ItemUsage itemUsagePoco:
                    itemUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.JoinNode joinNodePoco:
                    joinNodePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.LibraryPackage libraryPackagePoco:
                    libraryPackagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralBoolean literalBooleanPoco:
                    literalBooleanPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralExpression literalExpressionPoco:
                    literalExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralInfinity literalInfinityPoco:
                    literalInfinityPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralInteger literalIntegerPoco:
                    literalIntegerPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralRational literalRationalPoco:
                    literalRationalPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralString literalStringPoco:
                    literalStringPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.Membership membershipPoco:
                    membershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.MembershipExpose membershipExposePoco:
                    membershipExposePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.MembershipImport membershipImportPoco:
                    membershipImportPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.MergeNode mergeNodePoco:
                    mergeNodePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Metadata.Metaclass metaclassPoco:
                    metaclassPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.MetadataAccessExpression metadataAccessExpressionPoco:
                    metadataAccessExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Metadata.MetadataDefinition metadataDefinitionPoco:
                    metadataDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Metadata.MetadataFeature metadataFeaturePoco:
                    metadataFeaturePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Metadata.MetadataUsage metadataUsagePoco:
                    metadataUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Multiplicity multiplicityPoco:
                    multiplicityPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Multiplicities.MultiplicityRange multiplicityRangePoco:
                    multiplicityRangePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.Namespace namespacePoco:
                    namespacePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.NamespaceExpose namespaceExposePoco:
                    namespaceExposePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.NamespaceImport namespaceImportPoco:
                    namespaceImportPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.NullExpression nullExpressionPoco:
                    nullExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Cases.ObjectiveMembership objectiveMembershipPoco:
                    objectiveMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.OccurrenceDefinition occurrenceDefinitionPoco:
                    occurrenceDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Occurrences.OccurrenceUsage occurrenceUsagePoco:
                    occurrenceUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.OperatorExpression operatorExpressionPoco:
                    operatorExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership owningMembershipPoco:
                    owningMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.Package packagePoco:
                    packagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.ParameterMembership parameterMembershipPoco:
                    parameterMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Parts.PartDefinition partDefinitionPoco:
                    partDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Parts.PartUsage partUsagePoco:
                    partUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.PayloadFeature payloadFeaturePoco:
                    payloadFeaturePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.PerformActionUsage performActionUsagePoco:
                    performActionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.PortConjugation portConjugationPoco:
                    portConjugationPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.PortDefinition portDefinitionPoco:
                    portDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Ports.PortUsage portUsagePoco:
                    portUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Predicate predicatePoco:
                    predicatePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.Redefinition redefinitionPoco:
                    redefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.ReferenceSubsetting referenceSubsettingPoco:
                    referenceSubsettingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.ReferenceUsage referenceUsagePoco:
                    referenceUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.RenderingDefinition renderingDefinitionPoco:
                    renderingDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.RenderingUsage renderingUsagePoco:
                    renderingUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementConstraintMembership requirementConstraintMembershipPoco:
                    requirementConstraintMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementDefinition requirementDefinitionPoco:
                    requirementDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementUsage requirementUsagePoco:
                    requirementUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.VerificationCases.RequirementVerificationMembership requirementVerificationMembershipPoco:
                    requirementVerificationMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.ResultExpressionMembership resultExpressionMembershipPoco:
                    resultExpressionMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.ReturnParameterMembership returnParameterMembershipPoco:
                    returnParameterMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage satisfyRequirementUsagePoco:
                    satisfyRequirementUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.SelectExpression selectExpressionPoco:
                    selectExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.SendActionUsage sendActionUsagePoco:
                    sendActionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Specialization specializationPoco:
                    specializationPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.StakeholderMembership stakeholderMembershipPoco:
                    stakeholderMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.StateDefinition stateDefinitionPoco:
                    stateDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.StateSubactionMembership stateSubactionMembershipPoco:
                    stateSubactionMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.StateUsage stateUsagePoco:
                    stateUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.Step stepPoco:
                    stepPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Structures.Structure structurePoco:
                    structurePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Classifiers.Subclassification subclassificationPoco:
                    subclassificationPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.SubjectMembership subjectMembershipPoco:
                    subjectMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.Subsetting subsettingPoco:
                    subsettingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.Succession successionPoco:
                    successionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.SuccessionAsUsage successionAsUsagePoco:
                    successionAsUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.SuccessionFlow successionFlowPoco:
                    successionFlowPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Flows.SuccessionFlowUsage successionFlowUsagePoco:
                    successionFlowUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.TerminateActionUsage terminateActionUsagePoco:
                    terminateActionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Root.Annotations.TextualRepresentation textualRepresentationPoco:
                    textualRepresentationPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.TransitionFeatureMembership transitionFeatureMembershipPoco:
                    transitionFeatureMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.TransitionUsage transitionUsagePoco:
                    transitionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.TriggerInvocationExpression triggerInvocationExpressionPoco:
                    triggerInvocationExpressionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Type typePoco:
                    typePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.TypeFeaturing typeFeaturingPoco:
                    typeFeaturingPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Unioning unioningPoco:
                    unioningPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.Usage usagePoco:
                    usagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.UseCaseDefinition useCaseDefinitionPoco:
                    useCaseDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.UseCaseUsage useCaseUsagePoco:
                    useCaseUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.VariantMembership variantMembershipPoco:
                    variantMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.VerificationCases.VerificationCaseDefinition verificationCaseDefinitionPoco:
                    verificationCaseDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.VerificationCases.VerificationCaseUsage verificationCaseUsagePoco:
                    verificationCaseUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewDefinition viewDefinitionPoco:
                    viewDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewpointDefinition viewpointDefinitionPoco:
                    viewpointDefinitionPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewpointUsage viewpointUsagePoco:
                    viewpointUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewRenderingMembership viewRenderingMembershipPoco:
                    viewRenderingMembershipPoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewUsage viewUsagePoco:
                    viewUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.WhileLoopActionUsage whileLoopActionUsagePoco:
                    whileLoopActionUsagePoco.ResolveAndAssignMultipleValueReferences(propertyName, references, xmiDataCache, logger);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(data), data, "Unsupported type");
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
