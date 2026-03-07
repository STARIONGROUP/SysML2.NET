// -------------------------------------------------------------------------------------------------
// <copyright file="XmiDataWriterFacade.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi.Writers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="XmiDataWriterFacade"/> is to dispatch writing of <see cref="IData"/> instances
    /// to the appropriate per-type writer class
    /// </summary>
    public class XmiDataWriterFacade : IXmiDataWriterFacade
    {
        /// <summary>
        /// A dictionary that contains actions that write <see cref="IData"/> based on a key that represents the POCO type name
        /// </summary>
        private readonly Dictionary<string, Action<XmlWriter, IData, string, XmiWriterOptions, IXmiElementOriginMap, Uri>> writerCache;

        /// <summary>
        /// A dictionary that contains functions that asynchronously write <see cref="IData"/> based on a key that represents the POCO type name
        /// </summary>
        private readonly Dictionary<string, Func<XmlWriter, IData, string, XmiWriterOptions, IXmiElementOriginMap, Uri, Task>> writerAsyncCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmiDataWriterFacade"/>
        /// </summary>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> used to set up logging</param>
        public XmiDataWriterFacade(ILoggerFactory loggerFactory)
        {
            var acceptActionUsageWriter = new AcceptActionUsageWriter(this, loggerFactory);
            var actionDefinitionWriter = new ActionDefinitionWriter(this, loggerFactory);
            var actionUsageWriter = new ActionUsageWriter(this, loggerFactory);
            var actorMembershipWriter = new ActorMembershipWriter(this, loggerFactory);
            var allocationDefinitionWriter = new AllocationDefinitionWriter(this, loggerFactory);
            var allocationUsageWriter = new AllocationUsageWriter(this, loggerFactory);
            var analysisCaseDefinitionWriter = new AnalysisCaseDefinitionWriter(this, loggerFactory);
            var analysisCaseUsageWriter = new AnalysisCaseUsageWriter(this, loggerFactory);
            var annotatingElementWriter = new AnnotatingElementWriter(this, loggerFactory);
            var annotationWriter = new AnnotationWriter(this, loggerFactory);
            var assertConstraintUsageWriter = new AssertConstraintUsageWriter(this, loggerFactory);
            var assignmentActionUsageWriter = new AssignmentActionUsageWriter(this, loggerFactory);
            var associationWriter = new AssociationWriter(this, loggerFactory);
            var associationStructureWriter = new AssociationStructureWriter(this, loggerFactory);
            var attributeDefinitionWriter = new AttributeDefinitionWriter(this, loggerFactory);
            var attributeUsageWriter = new AttributeUsageWriter(this, loggerFactory);
            var behaviorWriter = new BehaviorWriter(this, loggerFactory);
            var bindingConnectorWriter = new BindingConnectorWriter(this, loggerFactory);
            var bindingConnectorAsUsageWriter = new BindingConnectorAsUsageWriter(this, loggerFactory);
            var booleanExpressionWriter = new BooleanExpressionWriter(this, loggerFactory);
            var calculationDefinitionWriter = new CalculationDefinitionWriter(this, loggerFactory);
            var calculationUsageWriter = new CalculationUsageWriter(this, loggerFactory);
            var caseDefinitionWriter = new CaseDefinitionWriter(this, loggerFactory);
            var caseUsageWriter = new CaseUsageWriter(this, loggerFactory);
            var classWriter = new ClassWriter(this, loggerFactory);
            var classifierWriter = new ClassifierWriter(this, loggerFactory);
            var collectExpressionWriter = new CollectExpressionWriter(this, loggerFactory);
            var commentWriter = new CommentWriter(this, loggerFactory);
            var concernDefinitionWriter = new ConcernDefinitionWriter(this, loggerFactory);
            var concernUsageWriter = new ConcernUsageWriter(this, loggerFactory);
            var conjugatedPortDefinitionWriter = new ConjugatedPortDefinitionWriter(this, loggerFactory);
            var conjugatedPortTypingWriter = new ConjugatedPortTypingWriter(this, loggerFactory);
            var conjugationWriter = new ConjugationWriter(this, loggerFactory);
            var connectionDefinitionWriter = new ConnectionDefinitionWriter(this, loggerFactory);
            var connectionUsageWriter = new ConnectionUsageWriter(this, loggerFactory);
            var connectorWriter = new ConnectorWriter(this, loggerFactory);
            var constraintDefinitionWriter = new ConstraintDefinitionWriter(this, loggerFactory);
            var constraintUsageWriter = new ConstraintUsageWriter(this, loggerFactory);
            var constructorExpressionWriter = new ConstructorExpressionWriter(this, loggerFactory);
            var crossSubsettingWriter = new CrossSubsettingWriter(this, loggerFactory);
            var dataTypeWriter = new DataTypeWriter(this, loggerFactory);
            var decisionNodeWriter = new DecisionNodeWriter(this, loggerFactory);
            var definitionWriter = new DefinitionWriter(this, loggerFactory);
            var dependencyWriter = new DependencyWriter(this, loggerFactory);
            var differencingWriter = new DifferencingWriter(this, loggerFactory);
            var disjoiningWriter = new DisjoiningWriter(this, loggerFactory);
            var documentationWriter = new DocumentationWriter(this, loggerFactory);
            var elementFilterMembershipWriter = new ElementFilterMembershipWriter(this, loggerFactory);
            var endFeatureMembershipWriter = new EndFeatureMembershipWriter(this, loggerFactory);
            var enumerationDefinitionWriter = new EnumerationDefinitionWriter(this, loggerFactory);
            var enumerationUsageWriter = new EnumerationUsageWriter(this, loggerFactory);
            var eventOccurrenceUsageWriter = new EventOccurrenceUsageWriter(this, loggerFactory);
            var exhibitStateUsageWriter = new ExhibitStateUsageWriter(this, loggerFactory);
            var expressionWriter = new ExpressionWriter(this, loggerFactory);
            var featureWriter = new FeatureWriter(this, loggerFactory);
            var featureChainExpressionWriter = new FeatureChainExpressionWriter(this, loggerFactory);
            var featureChainingWriter = new FeatureChainingWriter(this, loggerFactory);
            var featureInvertingWriter = new FeatureInvertingWriter(this, loggerFactory);
            var featureMembershipWriter = new FeatureMembershipWriter(this, loggerFactory);
            var featureReferenceExpressionWriter = new FeatureReferenceExpressionWriter(this, loggerFactory);
            var featureTypingWriter = new FeatureTypingWriter(this, loggerFactory);
            var featureValueWriter = new FeatureValueWriter(this, loggerFactory);
            var flowWriter = new FlowWriter(this, loggerFactory);
            var flowDefinitionWriter = new FlowDefinitionWriter(this, loggerFactory);
            var flowEndWriter = new FlowEndWriter(this, loggerFactory);
            var flowUsageWriter = new FlowUsageWriter(this, loggerFactory);
            var forkNodeWriter = new ForkNodeWriter(this, loggerFactory);
            var forLoopActionUsageWriter = new ForLoopActionUsageWriter(this, loggerFactory);
            var framedConcernMembershipWriter = new FramedConcernMembershipWriter(this, loggerFactory);
            var functionWriter = new FunctionWriter(this, loggerFactory);
            var ifActionUsageWriter = new IfActionUsageWriter(this, loggerFactory);
            var includeUseCaseUsageWriter = new IncludeUseCaseUsageWriter(this, loggerFactory);
            var indexExpressionWriter = new IndexExpressionWriter(this, loggerFactory);
            var interactionWriter = new InteractionWriter(this, loggerFactory);
            var interfaceDefinitionWriter = new InterfaceDefinitionWriter(this, loggerFactory);
            var interfaceUsageWriter = new InterfaceUsageWriter(this, loggerFactory);
            var intersectingWriter = new IntersectingWriter(this, loggerFactory);
            var invariantWriter = new InvariantWriter(this, loggerFactory);
            var invocationExpressionWriter = new InvocationExpressionWriter(this, loggerFactory);
            var itemDefinitionWriter = new ItemDefinitionWriter(this, loggerFactory);
            var itemUsageWriter = new ItemUsageWriter(this, loggerFactory);
            var joinNodeWriter = new JoinNodeWriter(this, loggerFactory);
            var libraryPackageWriter = new LibraryPackageWriter(this, loggerFactory);
            var literalBooleanWriter = new LiteralBooleanWriter(this, loggerFactory);
            var literalExpressionWriter = new LiteralExpressionWriter(this, loggerFactory);
            var literalInfinityWriter = new LiteralInfinityWriter(this, loggerFactory);
            var literalIntegerWriter = new LiteralIntegerWriter(this, loggerFactory);
            var literalRationalWriter = new LiteralRationalWriter(this, loggerFactory);
            var literalStringWriter = new LiteralStringWriter(this, loggerFactory);
            var membershipWriter = new MembershipWriter(this, loggerFactory);
            var membershipExposeWriter = new MembershipExposeWriter(this, loggerFactory);
            var membershipImportWriter = new MembershipImportWriter(this, loggerFactory);
            var mergeNodeWriter = new MergeNodeWriter(this, loggerFactory);
            var metaclassWriter = new MetaclassWriter(this, loggerFactory);
            var metadataAccessExpressionWriter = new MetadataAccessExpressionWriter(this, loggerFactory);
            var metadataDefinitionWriter = new MetadataDefinitionWriter(this, loggerFactory);
            var metadataFeatureWriter = new MetadataFeatureWriter(this, loggerFactory);
            var metadataUsageWriter = new MetadataUsageWriter(this, loggerFactory);
            var multiplicityWriter = new MultiplicityWriter(this, loggerFactory);
            var multiplicityRangeWriter = new MultiplicityRangeWriter(this, loggerFactory);
            var namespaceWriter = new NamespaceWriter(this, loggerFactory);
            var namespaceExposeWriter = new NamespaceExposeWriter(this, loggerFactory);
            var namespaceImportWriter = new NamespaceImportWriter(this, loggerFactory);
            var nullExpressionWriter = new NullExpressionWriter(this, loggerFactory);
            var objectiveMembershipWriter = new ObjectiveMembershipWriter(this, loggerFactory);
            var occurrenceDefinitionWriter = new OccurrenceDefinitionWriter(this, loggerFactory);
            var occurrenceUsageWriter = new OccurrenceUsageWriter(this, loggerFactory);
            var operatorExpressionWriter = new OperatorExpressionWriter(this, loggerFactory);
            var owningMembershipWriter = new OwningMembershipWriter(this, loggerFactory);
            var packageWriter = new PackageWriter(this, loggerFactory);
            var parameterMembershipWriter = new ParameterMembershipWriter(this, loggerFactory);
            var partDefinitionWriter = new PartDefinitionWriter(this, loggerFactory);
            var partUsageWriter = new PartUsageWriter(this, loggerFactory);
            var payloadFeatureWriter = new PayloadFeatureWriter(this, loggerFactory);
            var performActionUsageWriter = new PerformActionUsageWriter(this, loggerFactory);
            var portConjugationWriter = new PortConjugationWriter(this, loggerFactory);
            var portDefinitionWriter = new PortDefinitionWriter(this, loggerFactory);
            var portUsageWriter = new PortUsageWriter(this, loggerFactory);
            var predicateWriter = new PredicateWriter(this, loggerFactory);
            var redefinitionWriter = new RedefinitionWriter(this, loggerFactory);
            var referenceSubsettingWriter = new ReferenceSubsettingWriter(this, loggerFactory);
            var referenceUsageWriter = new ReferenceUsageWriter(this, loggerFactory);
            var renderingDefinitionWriter = new RenderingDefinitionWriter(this, loggerFactory);
            var renderingUsageWriter = new RenderingUsageWriter(this, loggerFactory);
            var requirementConstraintMembershipWriter = new RequirementConstraintMembershipWriter(this, loggerFactory);
            var requirementDefinitionWriter = new RequirementDefinitionWriter(this, loggerFactory);
            var requirementUsageWriter = new RequirementUsageWriter(this, loggerFactory);
            var requirementVerificationMembershipWriter = new RequirementVerificationMembershipWriter(this, loggerFactory);
            var resultExpressionMembershipWriter = new ResultExpressionMembershipWriter(this, loggerFactory);
            var returnParameterMembershipWriter = new ReturnParameterMembershipWriter(this, loggerFactory);
            var satisfyRequirementUsageWriter = new SatisfyRequirementUsageWriter(this, loggerFactory);
            var selectExpressionWriter = new SelectExpressionWriter(this, loggerFactory);
            var sendActionUsageWriter = new SendActionUsageWriter(this, loggerFactory);
            var specializationWriter = new SpecializationWriter(this, loggerFactory);
            var stakeholderMembershipWriter = new StakeholderMembershipWriter(this, loggerFactory);
            var stateDefinitionWriter = new StateDefinitionWriter(this, loggerFactory);
            var stateSubactionMembershipWriter = new StateSubactionMembershipWriter(this, loggerFactory);
            var stateUsageWriter = new StateUsageWriter(this, loggerFactory);
            var stepWriter = new StepWriter(this, loggerFactory);
            var structureWriter = new StructureWriter(this, loggerFactory);
            var subclassificationWriter = new SubclassificationWriter(this, loggerFactory);
            var subjectMembershipWriter = new SubjectMembershipWriter(this, loggerFactory);
            var subsettingWriter = new SubsettingWriter(this, loggerFactory);
            var successionWriter = new SuccessionWriter(this, loggerFactory);
            var successionAsUsageWriter = new SuccessionAsUsageWriter(this, loggerFactory);
            var successionFlowWriter = new SuccessionFlowWriter(this, loggerFactory);
            var successionFlowUsageWriter = new SuccessionFlowUsageWriter(this, loggerFactory);
            var terminateActionUsageWriter = new TerminateActionUsageWriter(this, loggerFactory);
            var textualRepresentationWriter = new TextualRepresentationWriter(this, loggerFactory);
            var transitionFeatureMembershipWriter = new TransitionFeatureMembershipWriter(this, loggerFactory);
            var transitionUsageWriter = new TransitionUsageWriter(this, loggerFactory);
            var triggerInvocationExpressionWriter = new TriggerInvocationExpressionWriter(this, loggerFactory);
            var typeWriter = new TypeWriter(this, loggerFactory);
            var typeFeaturingWriter = new TypeFeaturingWriter(this, loggerFactory);
            var unioningWriter = new UnioningWriter(this, loggerFactory);
            var usageWriter = new UsageWriter(this, loggerFactory);
            var useCaseDefinitionWriter = new UseCaseDefinitionWriter(this, loggerFactory);
            var useCaseUsageWriter = new UseCaseUsageWriter(this, loggerFactory);
            var variantMembershipWriter = new VariantMembershipWriter(this, loggerFactory);
            var verificationCaseDefinitionWriter = new VerificationCaseDefinitionWriter(this, loggerFactory);
            var verificationCaseUsageWriter = new VerificationCaseUsageWriter(this, loggerFactory);
            var viewDefinitionWriter = new ViewDefinitionWriter(this, loggerFactory);
            var viewpointDefinitionWriter = new ViewpointDefinitionWriter(this, loggerFactory);
            var viewpointUsageWriter = new ViewpointUsageWriter(this, loggerFactory);
            var viewRenderingMembershipWriter = new ViewRenderingMembershipWriter(this, loggerFactory);
            var viewUsageWriter = new ViewUsageWriter(this, loggerFactory);
            var whileLoopActionUsageWriter = new WhileLoopActionUsageWriter(this, loggerFactory);

            this.writerCache = new Dictionary<string, Action<XmlWriter, IData, string, XmiWriterOptions, IXmiElementOriginMap, Uri>>
            {
                ["AcceptActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    acceptActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IAcceptActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ActionDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    actionDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IActionDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    actionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ActorMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    actorMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IActorMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["AllocationDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    allocationDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["AllocationUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    allocationUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["AnalysisCaseDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    analysisCaseDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["AnalysisCaseUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    analysisCaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["AnnotatingElement"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    annotatingElementWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.IAnnotatingElement)data, elementName, writerOptions, originMap, uri);
                },
                ["Annotation"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    annotationWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.IAnnotation)data, elementName, writerOptions, originMap, uri);
                },
                ["AssertConstraintUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    assertConstraintUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Constraints.IAssertConstraintUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["AssignmentActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    assignmentActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IAssignmentActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Association"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    associationWriter.Write(xmlWriter, (Core.POCO.Kernel.Associations.IAssociation)data, elementName, writerOptions, originMap, uri);
                },
                ["AssociationStructure"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    associationStructureWriter.Write(xmlWriter, (Core.POCO.Kernel.Associations.IAssociationStructure)data, elementName, writerOptions, originMap, uri);
                },
                ["AttributeDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    attributeDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["AttributeUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    attributeUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Behavior"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    behaviorWriter.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IBehavior)data, elementName, writerOptions, originMap, uri);
                },
                ["BindingConnector"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    bindingConnectorWriter.Write(xmlWriter, (Core.POCO.Kernel.Connectors.IBindingConnector)data, elementName, writerOptions, originMap, uri);
                },
                ["BindingConnectorAsUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    bindingConnectorAsUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Connections.IBindingConnectorAsUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["BooleanExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    booleanExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IBooleanExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["CalculationDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    calculationDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["CalculationUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    calculationUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["CaseDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    caseDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Cases.ICaseDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["CaseUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    caseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Cases.ICaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Class"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    classWriter.Write(xmlWriter, (Core.POCO.Kernel.Classes.IClass)data, elementName, writerOptions, originMap, uri);
                },
                ["Classifier"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    classifierWriter.Write(xmlWriter, (Core.POCO.Core.Classifiers.IClassifier)data, elementName, writerOptions, originMap, uri);
                },
                ["CollectExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    collectExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ICollectExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["Comment"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    commentWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.IComment)data, elementName, writerOptions, originMap, uri);
                },
                ["ConcernDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    concernDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IConcernDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ConcernUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    concernUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IConcernUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ConjugatedPortDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    conjugatedPortDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ConjugatedPortTyping"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    conjugatedPortTypingWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortTyping)data, elementName, writerOptions, originMap, uri);
                },
                ["Conjugation"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    conjugationWriter.Write(xmlWriter, (Core.POCO.Core.Types.IConjugation)data, elementName, writerOptions, originMap, uri);
                },
                ["ConnectionDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    connectionDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Connections.IConnectionDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ConnectionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    connectionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Connections.IConnectionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Connector"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    connectorWriter.Write(xmlWriter, (Core.POCO.Kernel.Connectors.IConnector)data, elementName, writerOptions, originMap, uri);
                },
                ["ConstraintDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    constraintDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ConstraintUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    constraintUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ConstructorExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    constructorExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IConstructorExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["CrossSubsetting"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    crossSubsettingWriter.Write(xmlWriter, (Core.POCO.Core.Features.ICrossSubsetting)data, elementName, writerOptions, originMap, uri);
                },
                ["DataType"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    dataTypeWriter.Write(xmlWriter, (Core.POCO.Kernel.DataTypes.IDataType)data, elementName, writerOptions, originMap, uri);
                },
                ["DecisionNode"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    decisionNodeWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IDecisionNode)data, elementName, writerOptions, originMap, uri);
                },
                ["Definition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    definitionWriter.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["Dependency"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    dependencyWriter.Write(xmlWriter, (Core.POCO.Root.Dependencies.IDependency)data, elementName, writerOptions, originMap, uri);
                },
                ["Differencing"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    differencingWriter.Write(xmlWriter, (Core.POCO.Core.Types.IDifferencing)data, elementName, writerOptions, originMap, uri);
                },
                ["Disjoining"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    disjoiningWriter.Write(xmlWriter, (Core.POCO.Core.Types.IDisjoining)data, elementName, writerOptions, originMap, uri);
                },
                ["Documentation"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    documentationWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.IDocumentation)data, elementName, writerOptions, originMap, uri);
                },
                ["ElementFilterMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    elementFilterMembershipWriter.Write(xmlWriter, (Core.POCO.Kernel.Packages.IElementFilterMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["EndFeatureMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    endFeatureMembershipWriter.Write(xmlWriter, (Core.POCO.Core.Features.IEndFeatureMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["EnumerationDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    enumerationDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["EnumerationUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    enumerationUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["EventOccurrenceUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    eventOccurrenceUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IEventOccurrenceUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ExhibitStateUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    exhibitStateUsageWriter.Write(xmlWriter, (Core.POCO.Systems.States.IExhibitStateUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Expression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    expressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["Feature"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    featureWriter.Write(xmlWriter, (Core.POCO.Core.Features.IFeature)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureChainExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    featureChainExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureChainExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureChaining"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    featureChainingWriter.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureChaining)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureInverting"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    featureInvertingWriter.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureInverting)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    featureMembershipWriter.Write(xmlWriter, (Core.POCO.Core.Types.IFeatureMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureReferenceExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    featureReferenceExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureReferenceExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureTyping"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    featureTypingWriter.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureTyping)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureValue"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    featureValueWriter.Write(xmlWriter, (Core.POCO.Kernel.FeatureValues.IFeatureValue)data, elementName, writerOptions, originMap, uri);
                },
                ["Flow"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    flowWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IFlow)data, elementName, writerOptions, originMap, uri);
                },
                ["FlowDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    flowDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Flows.IFlowDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["FlowEnd"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    flowEndWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IFlowEnd)data, elementName, writerOptions, originMap, uri);
                },
                ["FlowUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    flowUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Flows.IFlowUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ForkNode"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    forkNodeWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IForkNode)data, elementName, writerOptions, originMap, uri);
                },
                ["ForLoopActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    forLoopActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IForLoopActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["FramedConcernMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    framedConcernMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IFramedConcernMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["Function"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    functionWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IFunction)data, elementName, writerOptions, originMap, uri);
                },
                ["IfActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    ifActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IIfActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["IncludeUseCaseUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    includeUseCaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.UseCases.IIncludeUseCaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["IndexExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    indexExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IIndexExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["Interaction"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    interactionWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IInteraction)data, elementName, writerOptions, originMap, uri);
                },
                ["InterfaceDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    interfaceDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["InterfaceUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    interfaceUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Intersecting"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    intersectingWriter.Write(xmlWriter, (Core.POCO.Core.Types.IIntersecting)data, elementName, writerOptions, originMap, uri);
                },
                ["Invariant"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    invariantWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IInvariant)data, elementName, writerOptions, originMap, uri);
                },
                ["InvocationExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    invocationExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IInvocationExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["ItemDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    itemDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Items.IItemDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ItemUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    itemUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Items.IItemUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["JoinNode"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    joinNodeWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IJoinNode)data, elementName, writerOptions, originMap, uri);
                },
                ["LibraryPackage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    libraryPackageWriter.Write(xmlWriter, (Core.POCO.Kernel.Packages.ILibraryPackage)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralBoolean"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    literalBooleanWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralBoolean)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    literalExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralInfinity"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    literalInfinityWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInfinity)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralInteger"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    literalIntegerWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInteger)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralRational"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    literalRationalWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralRational)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralString"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    literalStringWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralString)data, elementName, writerOptions, originMap, uri);
                },
                ["Membership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    membershipWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.IMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["MembershipExpose"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    membershipExposeWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IMembershipExpose)data, elementName, writerOptions, originMap, uri);
                },
                ["MembershipImport"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    membershipImportWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.IMembershipImport)data, elementName, writerOptions, originMap, uri);
                },
                ["MergeNode"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    mergeNodeWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IMergeNode)data, elementName, writerOptions, originMap, uri);
                },
                ["Metaclass"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    metaclassWriter.Write(xmlWriter, (Core.POCO.Kernel.Metadata.IMetaclass)data, elementName, writerOptions, originMap, uri);
                },
                ["MetadataAccessExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    metadataAccessExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IMetadataAccessExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["MetadataDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    metadataDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["MetadataFeature"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    metadataFeatureWriter.Write(xmlWriter, (Core.POCO.Kernel.Metadata.IMetadataFeature)data, elementName, writerOptions, originMap, uri);
                },
                ["MetadataUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    metadataUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Multiplicity"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    multiplicityWriter.Write(xmlWriter, (Core.POCO.Core.Types.IMultiplicity)data, elementName, writerOptions, originMap, uri);
                },
                ["MultiplicityRange"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    multiplicityRangeWriter.Write(xmlWriter, (Core.POCO.Kernel.Multiplicities.IMultiplicityRange)data, elementName, writerOptions, originMap, uri);
                },
                ["Namespace"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    namespaceWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.INamespace)data, elementName, writerOptions, originMap, uri);
                },
                ["NamespaceExpose"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    namespaceExposeWriter.Write(xmlWriter, (Core.POCO.Systems.Views.INamespaceExpose)data, elementName, writerOptions, originMap, uri);
                },
                ["NamespaceImport"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    namespaceImportWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.INamespaceImport)data, elementName, writerOptions, originMap, uri);
                },
                ["NullExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    nullExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.INullExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["ObjectiveMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    objectiveMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Cases.IObjectiveMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["OccurrenceDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    occurrenceDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["OccurrenceUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    occurrenceUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["OperatorExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    operatorExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IOperatorExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["OwningMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    owningMembershipWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.IOwningMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["Package"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    packageWriter.Write(xmlWriter, (Core.POCO.Kernel.Packages.IPackage)data, elementName, writerOptions, originMap, uri);
                },
                ["ParameterMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    parameterMembershipWriter.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IParameterMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["PartDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    partDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Parts.IPartDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["PartUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    partUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Parts.IPartUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["PayloadFeature"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    payloadFeatureWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IPayloadFeature)data, elementName, writerOptions, originMap, uri);
                },
                ["PerformActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    performActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IPerformActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["PortConjugation"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    portConjugationWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortConjugation)data, elementName, writerOptions, originMap, uri);
                },
                ["PortDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    portDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["PortUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    portUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Predicate"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    predicateWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IPredicate)data, elementName, writerOptions, originMap, uri);
                },
                ["Redefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    redefinitionWriter.Write(xmlWriter, (Core.POCO.Core.Features.IRedefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ReferenceSubsetting"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    referenceSubsettingWriter.Write(xmlWriter, (Core.POCO.Core.Features.IReferenceSubsetting)data, elementName, writerOptions, originMap, uri);
                },
                ["ReferenceUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    referenceUsageWriter.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["RenderingDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    renderingDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IRenderingDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["RenderingUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    renderingUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IRenderingUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["RequirementConstraintMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    requirementConstraintMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementConstraintMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["RequirementDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    requirementDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["RequirementUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    requirementUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["RequirementVerificationMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    requirementVerificationMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["ResultExpressionMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    resultExpressionMembershipWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IResultExpressionMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["ReturnParameterMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    returnParameterMembershipWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IReturnParameterMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["SatisfyRequirementUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    satisfyRequirementUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.ISatisfyRequirementUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["SelectExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    selectExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ISelectExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["SendActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    sendActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.ISendActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Specialization"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    specializationWriter.Write(xmlWriter, (Core.POCO.Core.Types.ISpecialization)data, elementName, writerOptions, originMap, uri);
                },
                ["StakeholderMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    stakeholderMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IStakeholderMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["StateDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    stateDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.States.IStateDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["StateSubactionMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    stateSubactionMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.States.IStateSubactionMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["StateUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    stateUsageWriter.Write(xmlWriter, (Core.POCO.Systems.States.IStateUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Step"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    stepWriter.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IStep)data, elementName, writerOptions, originMap, uri);
                },
                ["Structure"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    structureWriter.Write(xmlWriter, (Core.POCO.Kernel.Structures.IStructure)data, elementName, writerOptions, originMap, uri);
                },
                ["Subclassification"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    subclassificationWriter.Write(xmlWriter, (Core.POCO.Core.Classifiers.ISubclassification)data, elementName, writerOptions, originMap, uri);
                },
                ["SubjectMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    subjectMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.ISubjectMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["Subsetting"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    subsettingWriter.Write(xmlWriter, (Core.POCO.Core.Features.ISubsetting)data, elementName, writerOptions, originMap, uri);
                },
                ["Succession"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    successionWriter.Write(xmlWriter, (Core.POCO.Kernel.Connectors.ISuccession)data, elementName, writerOptions, originMap, uri);
                },
                ["SuccessionAsUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    successionAsUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Connections.ISuccessionAsUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["SuccessionFlow"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    successionFlowWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.ISuccessionFlow)data, elementName, writerOptions, originMap, uri);
                },
                ["SuccessionFlowUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    successionFlowUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Flows.ISuccessionFlowUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["TerminateActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    terminateActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.ITerminateActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["TextualRepresentation"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    textualRepresentationWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.ITextualRepresentation)data, elementName, writerOptions, originMap, uri);
                },
                ["TransitionFeatureMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    transitionFeatureMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.States.ITransitionFeatureMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["TransitionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    transitionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.States.ITransitionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["TriggerInvocationExpression"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    triggerInvocationExpressionWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.ITriggerInvocationExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["Type"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    typeWriter.Write(xmlWriter, (Core.POCO.Core.Types.IType)data, elementName, writerOptions, originMap, uri);
                },
                ["TypeFeaturing"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    typeFeaturingWriter.Write(xmlWriter, (Core.POCO.Core.Features.ITypeFeaturing)data, elementName, writerOptions, originMap, uri);
                },
                ["Unioning"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    unioningWriter.Write(xmlWriter, (Core.POCO.Core.Types.IUnioning)data, elementName, writerOptions, originMap, uri);
                },
                ["Usage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    usageWriter.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["UseCaseDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    useCaseDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["UseCaseUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    useCaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["VariantMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    variantMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IVariantMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["VerificationCaseDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    verificationCaseDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["VerificationCaseUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    verificationCaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    viewDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewpointDefinition"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    viewpointDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewpointDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewpointUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    viewpointUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewpointUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewRenderingMembership"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    viewRenderingMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewRenderingMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    viewUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["WhileLoopActionUsage"] = (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    whileLoopActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IWhileLoopActionUsage)data, elementName, writerOptions, originMap, uri);
                },
            };

            this.writerAsyncCache = new Dictionary<string, Func<XmlWriter, IData, string, XmiWriterOptions, IXmiElementOriginMap, Uri, Task>>
            {
                ["AcceptActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await acceptActionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IAcceptActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ActionDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await actionDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IActionDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await actionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ActorMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await actorMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IActorMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["AllocationDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await allocationDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["AllocationUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await allocationUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["AnalysisCaseDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await analysisCaseDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["AnalysisCaseUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await analysisCaseUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["AnnotatingElement"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await annotatingElementWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.IAnnotatingElement)data, elementName, writerOptions, originMap, uri);
                },
                ["Annotation"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await annotationWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.IAnnotation)data, elementName, writerOptions, originMap, uri);
                },
                ["AssertConstraintUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await assertConstraintUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Constraints.IAssertConstraintUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["AssignmentActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await assignmentActionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IAssignmentActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Association"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await associationWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Associations.IAssociation)data, elementName, writerOptions, originMap, uri);
                },
                ["AssociationStructure"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await associationStructureWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Associations.IAssociationStructure)data, elementName, writerOptions, originMap, uri);
                },
                ["AttributeDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await attributeDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["AttributeUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await attributeUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Behavior"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await behaviorWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Behaviors.IBehavior)data, elementName, writerOptions, originMap, uri);
                },
                ["BindingConnector"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await bindingConnectorWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Connectors.IBindingConnector)data, elementName, writerOptions, originMap, uri);
                },
                ["BindingConnectorAsUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await bindingConnectorAsUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Connections.IBindingConnectorAsUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["BooleanExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await booleanExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IBooleanExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["CalculationDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await calculationDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["CalculationUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await calculationUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["CaseDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await caseDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Cases.ICaseDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["CaseUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await caseUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Cases.ICaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Class"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await classWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Classes.IClass)data, elementName, writerOptions, originMap, uri);
                },
                ["Classifier"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await classifierWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Classifiers.IClassifier)data, elementName, writerOptions, originMap, uri);
                },
                ["CollectExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await collectExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ICollectExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["Comment"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await commentWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.IComment)data, elementName, writerOptions, originMap, uri);
                },
                ["ConcernDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await concernDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IConcernDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ConcernUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await concernUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IConcernUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ConjugatedPortDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await conjugatedPortDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ConjugatedPortTyping"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await conjugatedPortTypingWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortTyping)data, elementName, writerOptions, originMap, uri);
                },
                ["Conjugation"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await conjugationWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IConjugation)data, elementName, writerOptions, originMap, uri);
                },
                ["ConnectionDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await connectionDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Connections.IConnectionDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ConnectionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await connectionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Connections.IConnectionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Connector"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await connectorWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Connectors.IConnector)data, elementName, writerOptions, originMap, uri);
                },
                ["ConstraintDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await constraintDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ConstraintUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await constraintUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ConstructorExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await constructorExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IConstructorExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["CrossSubsetting"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await crossSubsettingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.ICrossSubsetting)data, elementName, writerOptions, originMap, uri);
                },
                ["DataType"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await dataTypeWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.DataTypes.IDataType)data, elementName, writerOptions, originMap, uri);
                },
                ["DecisionNode"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await decisionNodeWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IDecisionNode)data, elementName, writerOptions, originMap, uri);
                },
                ["Definition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await definitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["Dependency"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await dependencyWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Dependencies.IDependency)data, elementName, writerOptions, originMap, uri);
                },
                ["Differencing"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await differencingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IDifferencing)data, elementName, writerOptions, originMap, uri);
                },
                ["Disjoining"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await disjoiningWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IDisjoining)data, elementName, writerOptions, originMap, uri);
                },
                ["Documentation"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await documentationWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.IDocumentation)data, elementName, writerOptions, originMap, uri);
                },
                ["ElementFilterMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await elementFilterMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Packages.IElementFilterMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["EndFeatureMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await endFeatureMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IEndFeatureMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["EnumerationDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await enumerationDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["EnumerationUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await enumerationUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["EventOccurrenceUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await eventOccurrenceUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Occurrences.IEventOccurrenceUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ExhibitStateUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await exhibitStateUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.States.IExhibitStateUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Expression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await expressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["Feature"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await featureWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IFeature)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureChainExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await featureChainExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureChainExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureChaining"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await featureChainingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IFeatureChaining)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureInverting"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await featureInvertingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IFeatureInverting)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await featureMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IFeatureMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureReferenceExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await featureReferenceExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureReferenceExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureTyping"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await featureTypingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IFeatureTyping)data, elementName, writerOptions, originMap, uri);
                },
                ["FeatureValue"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await featureValueWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.FeatureValues.IFeatureValue)data, elementName, writerOptions, originMap, uri);
                },
                ["Flow"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await flowWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.IFlow)data, elementName, writerOptions, originMap, uri);
                },
                ["FlowDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await flowDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Flows.IFlowDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["FlowEnd"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await flowEndWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.IFlowEnd)data, elementName, writerOptions, originMap, uri);
                },
                ["FlowUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await flowUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Flows.IFlowUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ForkNode"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await forkNodeWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IForkNode)data, elementName, writerOptions, originMap, uri);
                },
                ["ForLoopActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await forLoopActionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IForLoopActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["FramedConcernMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await framedConcernMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IFramedConcernMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["Function"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await functionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IFunction)data, elementName, writerOptions, originMap, uri);
                },
                ["IfActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await ifActionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IIfActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["IncludeUseCaseUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await includeUseCaseUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.UseCases.IIncludeUseCaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["IndexExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await indexExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IIndexExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["Interaction"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await interactionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.IInteraction)data, elementName, writerOptions, originMap, uri);
                },
                ["InterfaceDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await interfaceDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["InterfaceUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await interfaceUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Intersecting"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await intersectingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IIntersecting)data, elementName, writerOptions, originMap, uri);
                },
                ["Invariant"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await invariantWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IInvariant)data, elementName, writerOptions, originMap, uri);
                },
                ["InvocationExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await invocationExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IInvocationExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["ItemDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await itemDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Items.IItemDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ItemUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await itemUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Items.IItemUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["JoinNode"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await joinNodeWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IJoinNode)data, elementName, writerOptions, originMap, uri);
                },
                ["LibraryPackage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await libraryPackageWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Packages.ILibraryPackage)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralBoolean"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await literalBooleanWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralBoolean)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await literalExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralInfinity"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await literalInfinityWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInfinity)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralInteger"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await literalIntegerWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInteger)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralRational"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await literalRationalWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralRational)data, elementName, writerOptions, originMap, uri);
                },
                ["LiteralString"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await literalStringWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralString)data, elementName, writerOptions, originMap, uri);
                },
                ["Membership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await membershipWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.IMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["MembershipExpose"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await membershipExposeWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IMembershipExpose)data, elementName, writerOptions, originMap, uri);
                },
                ["MembershipImport"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await membershipImportWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.IMembershipImport)data, elementName, writerOptions, originMap, uri);
                },
                ["MergeNode"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await mergeNodeWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IMergeNode)data, elementName, writerOptions, originMap, uri);
                },
                ["Metaclass"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await metaclassWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Metadata.IMetaclass)data, elementName, writerOptions, originMap, uri);
                },
                ["MetadataAccessExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await metadataAccessExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IMetadataAccessExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["MetadataDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await metadataDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["MetadataFeature"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await metadataFeatureWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Metadata.IMetadataFeature)data, elementName, writerOptions, originMap, uri);
                },
                ["MetadataUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await metadataUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Multiplicity"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await multiplicityWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IMultiplicity)data, elementName, writerOptions, originMap, uri);
                },
                ["MultiplicityRange"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await multiplicityRangeWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Multiplicities.IMultiplicityRange)data, elementName, writerOptions, originMap, uri);
                },
                ["Namespace"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await namespaceWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.INamespace)data, elementName, writerOptions, originMap, uri);
                },
                ["NamespaceExpose"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await namespaceExposeWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.INamespaceExpose)data, elementName, writerOptions, originMap, uri);
                },
                ["NamespaceImport"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await namespaceImportWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.INamespaceImport)data, elementName, writerOptions, originMap, uri);
                },
                ["NullExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await nullExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.INullExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["ObjectiveMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await objectiveMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Cases.IObjectiveMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["OccurrenceDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await occurrenceDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["OccurrenceUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await occurrenceUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["OperatorExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await operatorExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IOperatorExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["OwningMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await owningMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.IOwningMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["Package"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await packageWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Packages.IPackage)data, elementName, writerOptions, originMap, uri);
                },
                ["ParameterMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await parameterMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Behaviors.IParameterMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["PartDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await partDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Parts.IPartDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["PartUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await partUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Parts.IPartUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["PayloadFeature"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await payloadFeatureWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.IPayloadFeature)data, elementName, writerOptions, originMap, uri);
                },
                ["PerformActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await performActionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IPerformActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["PortConjugation"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await portConjugationWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IPortConjugation)data, elementName, writerOptions, originMap, uri);
                },
                ["PortDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await portDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IPortDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["PortUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await portUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IPortUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Predicate"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await predicateWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IPredicate)data, elementName, writerOptions, originMap, uri);
                },
                ["Redefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await redefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IRedefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ReferenceSubsetting"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await referenceSubsettingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IReferenceSubsetting)data, elementName, writerOptions, originMap, uri);
                },
                ["ReferenceUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await referenceUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["RenderingDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await renderingDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IRenderingDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["RenderingUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await renderingUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IRenderingUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["RequirementConstraintMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await requirementConstraintMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementConstraintMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["RequirementDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await requirementDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["RequirementUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await requirementUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["RequirementVerificationMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await requirementVerificationMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["ResultExpressionMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await resultExpressionMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IResultExpressionMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["ReturnParameterMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await returnParameterMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IReturnParameterMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["SatisfyRequirementUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await satisfyRequirementUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.ISatisfyRequirementUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["SelectExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await selectExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ISelectExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["SendActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await sendActionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.ISendActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Specialization"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await specializationWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.ISpecialization)data, elementName, writerOptions, originMap, uri);
                },
                ["StakeholderMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await stakeholderMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IStakeholderMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["StateDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await stateDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.States.IStateDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["StateSubactionMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await stateSubactionMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.States.IStateSubactionMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["StateUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await stateUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.States.IStateUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["Step"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await stepWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Behaviors.IStep)data, elementName, writerOptions, originMap, uri);
                },
                ["Structure"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await structureWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Structures.IStructure)data, elementName, writerOptions, originMap, uri);
                },
                ["Subclassification"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await subclassificationWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Classifiers.ISubclassification)data, elementName, writerOptions, originMap, uri);
                },
                ["SubjectMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await subjectMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.ISubjectMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["Subsetting"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await subsettingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.ISubsetting)data, elementName, writerOptions, originMap, uri);
                },
                ["Succession"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await successionWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Connectors.ISuccession)data, elementName, writerOptions, originMap, uri);
                },
                ["SuccessionAsUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await successionAsUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Connections.ISuccessionAsUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["SuccessionFlow"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await successionFlowWriter.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.ISuccessionFlow)data, elementName, writerOptions, originMap, uri);
                },
                ["SuccessionFlowUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await successionFlowUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Flows.ISuccessionFlowUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["TerminateActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await terminateActionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.ITerminateActionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["TextualRepresentation"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await textualRepresentationWriter.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.ITextualRepresentation)data, elementName, writerOptions, originMap, uri);
                },
                ["TransitionFeatureMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await transitionFeatureMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.States.ITransitionFeatureMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["TransitionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await transitionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.States.ITransitionUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["TriggerInvocationExpression"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await triggerInvocationExpressionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.ITriggerInvocationExpression)data, elementName, writerOptions, originMap, uri);
                },
                ["Type"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await typeWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IType)data, elementName, writerOptions, originMap, uri);
                },
                ["TypeFeaturing"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await typeFeaturingWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Features.ITypeFeaturing)data, elementName, writerOptions, originMap, uri);
                },
                ["Unioning"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await unioningWriter.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IUnioning)data, elementName, writerOptions, originMap, uri);
                },
                ["Usage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await usageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["UseCaseDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await useCaseDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["UseCaseUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await useCaseUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["VariantMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await variantMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IVariantMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["VerificationCaseDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await verificationCaseDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["VerificationCaseUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await verificationCaseUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await viewDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewpointDefinition"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await viewpointDefinitionWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewpointDefinition)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewpointUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await viewpointUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewpointUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewRenderingMembership"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await viewRenderingMembershipWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewRenderingMembership)data, elementName, writerOptions, originMap, uri);
                },
                ["ViewUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await viewUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewUsage)data, elementName, writerOptions, originMap, uri);
                },
                ["WhileLoopActionUsage"] = async (xmlWriter, data, elementName, writerOptions, originMap, uri) =>
                {
                    await whileLoopActionUsageWriter.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IWhileLoopActionUsage)data, elementName, writerOptions, originMap, uri);
                },
            };
        }

        /// <summary>
        /// Writes the specified <see cref="IData"/> as an XMI element by dispatching to the appropriate per-type writer
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="data">The <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        public void Write(XmlWriter xmlWriter, IData data, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            var typeName = data.GetType().Name;

            if (this.writerCache.TryGetValue(typeName, out var writer))
            {
                writer(xmlWriter, data, elementName, writerOptions, elementOriginMap, currentFileUri);
            }
            else
            {
                throw new InvalidOperationException($"No writer found for type {typeName}");
            }
        }

        /// <summary>
        /// Writes a contained child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        /// <param name="shouldSpecifyType">Asserts that the XSI type should be specified in case of HREF</param>
        public void WriteContainedElement(XmlWriter xmlWriter, IData childData, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool shouldSpecifyType = false)
        {
            if (!writerOptions.IncludeImplied && childData is IRelationship rel && rel.IsImplied)
            {
                return;
            }

            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    WriteHrefElement(xmlWriter, childData, elementName, childSourceFile, currentFileUri, shouldSpecifyType);
                    return;
                }
            }

            this.Write(xmlWriter, childData, elementName, writerOptions, elementOriginMap, currentFileUri);
        }

        /// <summary>
        /// Writes a reference child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        /// <param name="shouldSpecifyType">Asserts that the XSI type should be specified in case of HREF</param>
        public void WriteReferenceElement(XmlWriter xmlWriter, IData childData, string elementName, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool shouldSpecifyType = false)
        {
            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    WriteHrefElement(xmlWriter, childData, elementName, childSourceFile, currentFileUri, shouldSpecifyType);
                    return;
                }
            }

            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xmi", "idref", null, childData.Id.ToString());
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes the specified <see cref="IData"/> as an XMI element by dispatching to the appropriate per-type writer
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="data">The <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        public async Task WriteAsync(XmlWriter xmlWriter, IData data, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            var typeName = data.GetType().Name;

            if (this.writerAsyncCache.TryGetValue(typeName, out var writer))
            {
                await writer(xmlWriter, data, elementName, writerOptions, elementOriginMap, currentFileUri);
            }
            else
            {
                throw new InvalidOperationException($"No writer found for type {typeName}");
            }
        }

        /// <summary>
        /// Asynchronously writes a contained child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        /// <param name="shouldSpecifyType">Asserts that the XSI type should be specified in case of HREF</param>
        public async Task WriteContainedElementAsync(XmlWriter xmlWriter, IData childData, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool shouldSpecifyType = false)
        {
            if (!writerOptions.IncludeImplied && childData is IRelationship rel && rel.IsImplied)
            {
                return;
            }

            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    await WriteHrefElementAsync(xmlWriter, childData, elementName, childSourceFile, currentFileUri, shouldSpecifyType);
                    return;
                }
            }

            await this.WriteAsync(xmlWriter, childData, elementName, writerOptions, elementOriginMap, currentFileUri);
        }

        /// <summary>
        /// Asynchronously writes a reference child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        /// <param name="shouldSpecifyType">Asserts that the XSI type should be specified in case of HREF</param>
        public async Task WriteReferenceElementAsync(XmlWriter xmlWriter, IData childData, string elementName, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool shouldSpecifyType = false)
        {
            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    await WriteHrefElementAsync(xmlWriter, childData, elementName, childSourceFile, currentFileUri, shouldSpecifyType);
                    return;
                }
            }

            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xmi", "idref", null, childData.Id.ToString());
            await xmlWriter.WriteEndElementAsync();
        }

        /// <summary>
        /// Writes an href element for cross-file references
        /// </summary>
        private static void WriteHrefElement(XmlWriter xmlWriter, IData childData, string elementName, Uri targetFile, Uri currentFile, bool shouldSpecifyType)
        {
            var relativePath = currentFile.MakeRelativeUri(targetFile);
            var href = $"{Uri.UnescapeDataString(relativePath.ToString())}#{childData.Id}";

            xmlWriter.WriteStartElement(elementName);

            if (shouldSpecifyType)
            {
                xmlWriter.WriteAttributeString("xsi", "type", null, $"sysml:{childData.GetType().Name}");
            }

            xmlWriter.WriteAttributeString("href", href);
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes an href element for cross-file references
        /// </summary>
        private static async Task WriteHrefElementAsync(XmlWriter xmlWriter, IData childData, string elementName, Uri targetFile, Uri currentFile, bool shouldSpecifyType)
        {
            var relativePath = currentFile.MakeRelativeUri(targetFile);
            var href = $"{Uri.UnescapeDataString(relativePath.ToString())}#{childData.Id}";

            await xmlWriter.WriteStartElementAsync(null, elementName, null);

            if (shouldSpecifyType)
            {
                await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, $"sysml:{childData.GetType().Name}");
            }

            await xmlWriter.WriteAttributeStringAsync(null, "href", null, href);
            await xmlWriter.WriteEndElementAsync();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
