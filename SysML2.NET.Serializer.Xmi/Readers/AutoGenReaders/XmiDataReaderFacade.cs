// -------------------------------------------------------------------------------------------------
// <copyright file="XmiDataReaderFacade.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi.Readers
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;

    /// <summary>
    /// The purpose of the <see cref="XmiDataReaderFacade"/> is to read an <see cref="IData"/> from an
    /// <see cref="XmlReader"/>
    /// </summary>
    public class XmiDataReaderFacade : IXmiDataReaderFacade
    {
        /// <summary>
        /// A dictionary that contains functions that return <see cref="IData"/> based a key that represents the xsi Type
        /// and a provided <see cref="IXmiDataCache"/>, <see cref="IExternalReferenceService" />, <see cref="ILoggerFactory"/> and <see cref="XmlReader"/>
        /// </summary>
        private readonly Dictionary<string, Func<IXmiDataCache, IExternalReferenceService, ILoggerFactory, XmlReader, Uri, IData>> readerCache;

        /// <summary>
        /// A dictionary that contains functions that return an awaitable <see cref="Task" /> with the <see cref="IData"/> based a key that represents the xsi Type
        /// and a provided <see cref="IXmiDataCache"/>, <see cref="IExternalReferenceService" />, <see cref="ILoggerFactory"/> and <see cref="XmlReader"/>
        /// </summary>
        private readonly Dictionary<string, Func<IXmiDataCache, IExternalReferenceService, ILoggerFactory, XmlReader, Uri, Task<IData>>> readerAsyncCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmiDataReaderFacade"/>
        /// </summary>
        public XmiDataReaderFacade()
        {
            this.readerCache = new Dictionary<string, Func<IXmiDataCache, IExternalReferenceService, ILoggerFactory, XmlReader, Uri, IData>>
            {
                ["sysml:AcceptActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var acceptActionUsageReader = new AcceptActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return acceptActionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ActionDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var actionDefinitionReader = new ActionDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return actionDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var actionUsageReader = new ActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return actionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ActorMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var actorMembershipReader = new ActorMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return actorMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AllocationDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var allocationDefinitionReader = new AllocationDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return allocationDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AllocationUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var allocationUsageReader = new AllocationUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return allocationUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AnalysisCaseDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var analysisCaseDefinitionReader = new AnalysisCaseDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return analysisCaseDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AnalysisCaseUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var analysisCaseUsageReader = new AnalysisCaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return analysisCaseUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AnnotatingElement"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var annotatingElementReader = new AnnotatingElementReader(cache, this, externalReferenceService, loggerFactory);
                    return annotatingElementReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Annotation"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var annotationReader = new AnnotationReader(cache, this, externalReferenceService, loggerFactory);
                    return annotationReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AssertConstraintUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var assertConstraintUsageReader = new AssertConstraintUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return assertConstraintUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AssignmentActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var assignmentActionUsageReader = new AssignmentActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return assignmentActionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Association"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var associationReader = new AssociationReader(cache, this, externalReferenceService, loggerFactory);
                    return associationReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AssociationStructure"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var associationStructureReader = new AssociationStructureReader(cache, this, externalReferenceService, loggerFactory);
                    return associationStructureReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AttributeDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var attributeDefinitionReader = new AttributeDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return attributeDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:AttributeUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var attributeUsageReader = new AttributeUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return attributeUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Behavior"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var behaviorReader = new BehaviorReader(cache, this, externalReferenceService, loggerFactory);
                    return behaviorReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:BindingConnector"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var bindingConnectorReader = new BindingConnectorReader(cache, this, externalReferenceService, loggerFactory);
                    return bindingConnectorReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:BindingConnectorAsUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var bindingConnectorAsUsageReader = new BindingConnectorAsUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return bindingConnectorAsUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:BooleanExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var booleanExpressionReader = new BooleanExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return booleanExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:CalculationDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var calculationDefinitionReader = new CalculationDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return calculationDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:CalculationUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var calculationUsageReader = new CalculationUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return calculationUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:CaseDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var caseDefinitionReader = new CaseDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return caseDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:CaseUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var caseUsageReader = new CaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return caseUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Class"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var classReader = new ClassReader(cache, this, externalReferenceService, loggerFactory);
                    return classReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Classifier"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var classifierReader = new ClassifierReader(cache, this, externalReferenceService, loggerFactory);
                    return classifierReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:CollectExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var collectExpressionReader = new CollectExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return collectExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Comment"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var commentReader = new CommentReader(cache, this, externalReferenceService, loggerFactory);
                    return commentReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConcernDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var concernDefinitionReader = new ConcernDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return concernDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConcernUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var concernUsageReader = new ConcernUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return concernUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConjugatedPortDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var conjugatedPortDefinitionReader = new ConjugatedPortDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return conjugatedPortDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConjugatedPortTyping"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var conjugatedPortTypingReader = new ConjugatedPortTypingReader(cache, this, externalReferenceService, loggerFactory);
                    return conjugatedPortTypingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Conjugation"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var conjugationReader = new ConjugationReader(cache, this, externalReferenceService, loggerFactory);
                    return conjugationReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConnectionDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var connectionDefinitionReader = new ConnectionDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return connectionDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConnectionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var connectionUsageReader = new ConnectionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return connectionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Connector"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var connectorReader = new ConnectorReader(cache, this, externalReferenceService, loggerFactory);
                    return connectorReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConstraintDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var constraintDefinitionReader = new ConstraintDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return constraintDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConstraintUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var constraintUsageReader = new ConstraintUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return constraintUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ConstructorExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var constructorExpressionReader = new ConstructorExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return constructorExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:CrossSubsetting"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var crossSubsettingReader = new CrossSubsettingReader(cache, this, externalReferenceService, loggerFactory);
                    return crossSubsettingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:DataType"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var dataTypeReader = new DataTypeReader(cache, this, externalReferenceService, loggerFactory);
                    return dataTypeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:DecisionNode"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var decisionNodeReader = new DecisionNodeReader(cache, this, externalReferenceService, loggerFactory);
                    return decisionNodeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Definition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var definitionReader = new DefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return definitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Dependency"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var dependencyReader = new DependencyReader(cache, this, externalReferenceService, loggerFactory);
                    return dependencyReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Differencing"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var differencingReader = new DifferencingReader(cache, this, externalReferenceService, loggerFactory);
                    return differencingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Disjoining"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var disjoiningReader = new DisjoiningReader(cache, this, externalReferenceService, loggerFactory);
                    return disjoiningReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Documentation"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var documentationReader = new DocumentationReader(cache, this, externalReferenceService, loggerFactory);
                    return documentationReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ElementFilterMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var elementFilterMembershipReader = new ElementFilterMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return elementFilterMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:EndFeatureMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var endFeatureMembershipReader = new EndFeatureMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return endFeatureMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:EnumerationDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var enumerationDefinitionReader = new EnumerationDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return enumerationDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:EnumerationUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var enumerationUsageReader = new EnumerationUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return enumerationUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:EventOccurrenceUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var eventOccurrenceUsageReader = new EventOccurrenceUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return eventOccurrenceUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ExhibitStateUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var exhibitStateUsageReader = new ExhibitStateUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return exhibitStateUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Expression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var expressionReader = new ExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return expressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Feature"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureReader = new FeatureReader(cache, this, externalReferenceService, loggerFactory);
                    return featureReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FeatureChainExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureChainExpressionReader = new FeatureChainExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return featureChainExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FeatureChaining"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureChainingReader = new FeatureChainingReader(cache, this, externalReferenceService, loggerFactory);
                    return featureChainingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FeatureInverting"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureInvertingReader = new FeatureInvertingReader(cache, this, externalReferenceService, loggerFactory);
                    return featureInvertingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FeatureMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureMembershipReader = new FeatureMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return featureMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FeatureReferenceExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureReferenceExpressionReader = new FeatureReferenceExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return featureReferenceExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FeatureTyping"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureTypingReader = new FeatureTypingReader(cache, this, externalReferenceService, loggerFactory);
                    return featureTypingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FeatureValue"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureValueReader = new FeatureValueReader(cache, this, externalReferenceService, loggerFactory);
                    return featureValueReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Flow"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var flowReader = new FlowReader(cache, this, externalReferenceService, loggerFactory);
                    return flowReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FlowDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var flowDefinitionReader = new FlowDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return flowDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FlowEnd"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var flowEndReader = new FlowEndReader(cache, this, externalReferenceService, loggerFactory);
                    return flowEndReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FlowUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var flowUsageReader = new FlowUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return flowUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ForkNode"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var forkNodeReader = new ForkNodeReader(cache, this, externalReferenceService, loggerFactory);
                    return forkNodeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ForLoopActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var forLoopActionUsageReader = new ForLoopActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return forLoopActionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:FramedConcernMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var framedConcernMembershipReader = new FramedConcernMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return framedConcernMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Function"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var functionReader = new FunctionReader(cache, this, externalReferenceService, loggerFactory);
                    return functionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:IfActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var ifActionUsageReader = new IfActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return ifActionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:IncludeUseCaseUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var includeUseCaseUsageReader = new IncludeUseCaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return includeUseCaseUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:IndexExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var indexExpressionReader = new IndexExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return indexExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Interaction"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var interactionReader = new InteractionReader(cache, this, externalReferenceService, loggerFactory);
                    return interactionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:InterfaceDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var interfaceDefinitionReader = new InterfaceDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return interfaceDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:InterfaceUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var interfaceUsageReader = new InterfaceUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return interfaceUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Intersecting"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var intersectingReader = new IntersectingReader(cache, this, externalReferenceService, loggerFactory);
                    return intersectingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Invariant"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var invariantReader = new InvariantReader(cache, this, externalReferenceService, loggerFactory);
                    return invariantReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:InvocationExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var invocationExpressionReader = new InvocationExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return invocationExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ItemDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var itemDefinitionReader = new ItemDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return itemDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ItemUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var itemUsageReader = new ItemUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return itemUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:JoinNode"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var joinNodeReader = new JoinNodeReader(cache, this, externalReferenceService, loggerFactory);
                    return joinNodeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:LibraryPackage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var libraryPackageReader = new LibraryPackageReader(cache, this, externalReferenceService, loggerFactory);
                    return libraryPackageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:LiteralBoolean"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalBooleanReader = new LiteralBooleanReader(cache, this, externalReferenceService, loggerFactory);
                    return literalBooleanReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:LiteralExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalExpressionReader = new LiteralExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return literalExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:LiteralInfinity"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalInfinityReader = new LiteralInfinityReader(cache, this, externalReferenceService, loggerFactory);
                    return literalInfinityReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:LiteralInteger"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalIntegerReader = new LiteralIntegerReader(cache, this, externalReferenceService, loggerFactory);
                    return literalIntegerReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:LiteralRational"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalRationalReader = new LiteralRationalReader(cache, this, externalReferenceService, loggerFactory);
                    return literalRationalReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:LiteralString"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalStringReader = new LiteralStringReader(cache, this, externalReferenceService, loggerFactory);
                    return literalStringReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Membership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var membershipReader = new MembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return membershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:MembershipExpose"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var membershipExposeReader = new MembershipExposeReader(cache, this, externalReferenceService, loggerFactory);
                    return membershipExposeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:MembershipImport"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var membershipImportReader = new MembershipImportReader(cache, this, externalReferenceService, loggerFactory);
                    return membershipImportReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:MergeNode"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var mergeNodeReader = new MergeNodeReader(cache, this, externalReferenceService, loggerFactory);
                    return mergeNodeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Metaclass"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metaclassReader = new MetaclassReader(cache, this, externalReferenceService, loggerFactory);
                    return metaclassReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:MetadataAccessExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metadataAccessExpressionReader = new MetadataAccessExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return metadataAccessExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:MetadataDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metadataDefinitionReader = new MetadataDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return metadataDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:MetadataFeature"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metadataFeatureReader = new MetadataFeatureReader(cache, this, externalReferenceService, loggerFactory);
                    return metadataFeatureReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:MetadataUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metadataUsageReader = new MetadataUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return metadataUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Multiplicity"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var multiplicityReader = new MultiplicityReader(cache, this, externalReferenceService, loggerFactory);
                    return multiplicityReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:MultiplicityRange"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var multiplicityRangeReader = new MultiplicityRangeReader(cache, this, externalReferenceService, loggerFactory);
                    return multiplicityRangeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Namespace"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var namespaceReader = new NamespaceReader(cache, this, externalReferenceService, loggerFactory);
                    return namespaceReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:NamespaceExpose"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var namespaceExposeReader = new NamespaceExposeReader(cache, this, externalReferenceService, loggerFactory);
                    return namespaceExposeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:NamespaceImport"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var namespaceImportReader = new NamespaceImportReader(cache, this, externalReferenceService, loggerFactory);
                    return namespaceImportReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:NullExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var nullExpressionReader = new NullExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return nullExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ObjectiveMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var objectiveMembershipReader = new ObjectiveMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return objectiveMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:OccurrenceDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var occurrenceDefinitionReader = new OccurrenceDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return occurrenceDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:OccurrenceUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var occurrenceUsageReader = new OccurrenceUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return occurrenceUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:OperatorExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var operatorExpressionReader = new OperatorExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return operatorExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:OwningMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var owningMembershipReader = new OwningMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return owningMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Package"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var packageReader = new PackageReader(cache, this, externalReferenceService, loggerFactory);
                    return packageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ParameterMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var parameterMembershipReader = new ParameterMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return parameterMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:PartDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var partDefinitionReader = new PartDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return partDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:PartUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var partUsageReader = new PartUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return partUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:PayloadFeature"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var payloadFeatureReader = new PayloadFeatureReader(cache, this, externalReferenceService, loggerFactory);
                    return payloadFeatureReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:PerformActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var performActionUsageReader = new PerformActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return performActionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:PortConjugation"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var portConjugationReader = new PortConjugationReader(cache, this, externalReferenceService, loggerFactory);
                    return portConjugationReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:PortDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var portDefinitionReader = new PortDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return portDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:PortUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var portUsageReader = new PortUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return portUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Predicate"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var predicateReader = new PredicateReader(cache, this, externalReferenceService, loggerFactory);
                    return predicateReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Redefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var redefinitionReader = new RedefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return redefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ReferenceSubsetting"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var referenceSubsettingReader = new ReferenceSubsettingReader(cache, this, externalReferenceService, loggerFactory);
                    return referenceSubsettingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ReferenceUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var referenceUsageReader = new ReferenceUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return referenceUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:RenderingDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var renderingDefinitionReader = new RenderingDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return renderingDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:RenderingUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var renderingUsageReader = new RenderingUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return renderingUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:RequirementConstraintMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var requirementConstraintMembershipReader = new RequirementConstraintMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return requirementConstraintMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:RequirementDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var requirementDefinitionReader = new RequirementDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return requirementDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:RequirementUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var requirementUsageReader = new RequirementUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return requirementUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:RequirementVerificationMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var requirementVerificationMembershipReader = new RequirementVerificationMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return requirementVerificationMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ResultExpressionMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var resultExpressionMembershipReader = new ResultExpressionMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return resultExpressionMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ReturnParameterMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var returnParameterMembershipReader = new ReturnParameterMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return returnParameterMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:SatisfyRequirementUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var satisfyRequirementUsageReader = new SatisfyRequirementUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return satisfyRequirementUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:SelectExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var selectExpressionReader = new SelectExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return selectExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:SendActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var sendActionUsageReader = new SendActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return sendActionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Specialization"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var specializationReader = new SpecializationReader(cache, this, externalReferenceService, loggerFactory);
                    return specializationReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:StakeholderMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stakeholderMembershipReader = new StakeholderMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return stakeholderMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:StateDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stateDefinitionReader = new StateDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return stateDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:StateSubactionMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stateSubactionMembershipReader = new StateSubactionMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return stateSubactionMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:StateUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stateUsageReader = new StateUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return stateUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Step"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stepReader = new StepReader(cache, this, externalReferenceService, loggerFactory);
                    return stepReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Structure"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var structureReader = new StructureReader(cache, this, externalReferenceService, loggerFactory);
                    return structureReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Subclassification"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var subclassificationReader = new SubclassificationReader(cache, this, externalReferenceService, loggerFactory);
                    return subclassificationReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:SubjectMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var subjectMembershipReader = new SubjectMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return subjectMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Subsetting"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var subsettingReader = new SubsettingReader(cache, this, externalReferenceService, loggerFactory);
                    return subsettingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Succession"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var successionReader = new SuccessionReader(cache, this, externalReferenceService, loggerFactory);
                    return successionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:SuccessionAsUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var successionAsUsageReader = new SuccessionAsUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return successionAsUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:SuccessionFlow"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var successionFlowReader = new SuccessionFlowReader(cache, this, externalReferenceService, loggerFactory);
                    return successionFlowReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:SuccessionFlowUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var successionFlowUsageReader = new SuccessionFlowUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return successionFlowUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:TerminateActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var terminateActionUsageReader = new TerminateActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return terminateActionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:TextualRepresentation"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var textualRepresentationReader = new TextualRepresentationReader(cache, this, externalReferenceService, loggerFactory);
                    return textualRepresentationReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:TransitionFeatureMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var transitionFeatureMembershipReader = new TransitionFeatureMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return transitionFeatureMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:TransitionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var transitionUsageReader = new TransitionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return transitionUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:TriggerInvocationExpression"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var triggerInvocationExpressionReader = new TriggerInvocationExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return triggerInvocationExpressionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Type"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var typeReader = new TypeReader(cache, this, externalReferenceService, loggerFactory);
                    return typeReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:TypeFeaturing"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var typeFeaturingReader = new TypeFeaturingReader(cache, this, externalReferenceService, loggerFactory);
                    return typeFeaturingReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Unioning"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var unioningReader = new UnioningReader(cache, this, externalReferenceService, loggerFactory);
                    return unioningReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:Usage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var usageReader = new UsageReader(cache, this, externalReferenceService, loggerFactory);
                    return usageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:UseCaseDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var useCaseDefinitionReader = new UseCaseDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return useCaseDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:UseCaseUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var useCaseUsageReader = new UseCaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return useCaseUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:VariantMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var variantMembershipReader = new VariantMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return variantMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:VerificationCaseDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var verificationCaseDefinitionReader = new VerificationCaseDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return verificationCaseDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:VerificationCaseUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var verificationCaseUsageReader = new VerificationCaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return verificationCaseUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ViewDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewDefinitionReader = new ViewDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return viewDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ViewpointDefinition"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewpointDefinitionReader = new ViewpointDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return viewpointDefinitionReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ViewpointUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewpointUsageReader = new ViewpointUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return viewpointUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ViewRenderingMembership"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewRenderingMembershipReader = new ViewRenderingMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return viewRenderingMembershipReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:ViewUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewUsageReader = new ViewUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return viewUsageReader.Read(subXmlReader, currentLocation);
                },
                ["sysml:WhileLoopActionUsage"] = (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var whileLoopActionUsageReader = new WhileLoopActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return whileLoopActionUsageReader.Read(subXmlReader, currentLocation);
                },
            };

            this.readerAsyncCache = new Dictionary<string, Func<IXmiDataCache, IExternalReferenceService, ILoggerFactory, XmlReader, Uri, Task<IData>>>
            {
                ["sysml:AcceptActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var acceptActionUsageReader = new AcceptActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await acceptActionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ActionDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var actionDefinitionReader = new ActionDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await actionDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var actionUsageReader = new ActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await actionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ActorMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var actorMembershipReader = new ActorMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await actorMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AllocationDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var allocationDefinitionReader = new AllocationDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await allocationDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AllocationUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var allocationUsageReader = new AllocationUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await allocationUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AnalysisCaseDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var analysisCaseDefinitionReader = new AnalysisCaseDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await analysisCaseDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AnalysisCaseUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var analysisCaseUsageReader = new AnalysisCaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await analysisCaseUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AnnotatingElement"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var annotatingElementReader = new AnnotatingElementReader(cache, this, externalReferenceService, loggerFactory);
                    return await annotatingElementReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Annotation"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var annotationReader = new AnnotationReader(cache, this, externalReferenceService, loggerFactory);
                    return await annotationReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AssertConstraintUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var assertConstraintUsageReader = new AssertConstraintUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await assertConstraintUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AssignmentActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var assignmentActionUsageReader = new AssignmentActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await assignmentActionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Association"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var associationReader = new AssociationReader(cache, this, externalReferenceService, loggerFactory);
                    return await associationReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AssociationStructure"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var associationStructureReader = new AssociationStructureReader(cache, this, externalReferenceService, loggerFactory);
                    return await associationStructureReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AttributeDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var attributeDefinitionReader = new AttributeDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await attributeDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:AttributeUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var attributeUsageReader = new AttributeUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await attributeUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Behavior"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var behaviorReader = new BehaviorReader(cache, this, externalReferenceService, loggerFactory);
                    return await behaviorReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:BindingConnector"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var bindingConnectorReader = new BindingConnectorReader(cache, this, externalReferenceService, loggerFactory);
                    return await bindingConnectorReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:BindingConnectorAsUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var bindingConnectorAsUsageReader = new BindingConnectorAsUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await bindingConnectorAsUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:BooleanExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var booleanExpressionReader = new BooleanExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await booleanExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:CalculationDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var calculationDefinitionReader = new CalculationDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await calculationDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:CalculationUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var calculationUsageReader = new CalculationUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await calculationUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:CaseDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var caseDefinitionReader = new CaseDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await caseDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:CaseUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var caseUsageReader = new CaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await caseUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Class"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var classReader = new ClassReader(cache, this, externalReferenceService, loggerFactory);
                    return await classReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Classifier"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var classifierReader = new ClassifierReader(cache, this, externalReferenceService, loggerFactory);
                    return await classifierReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:CollectExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var collectExpressionReader = new CollectExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await collectExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Comment"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var commentReader = new CommentReader(cache, this, externalReferenceService, loggerFactory);
                    return await commentReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConcernDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var concernDefinitionReader = new ConcernDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await concernDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConcernUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var concernUsageReader = new ConcernUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await concernUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConjugatedPortDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var conjugatedPortDefinitionReader = new ConjugatedPortDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await conjugatedPortDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConjugatedPortTyping"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var conjugatedPortTypingReader = new ConjugatedPortTypingReader(cache, this, externalReferenceService, loggerFactory);
                    return await conjugatedPortTypingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Conjugation"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var conjugationReader = new ConjugationReader(cache, this, externalReferenceService, loggerFactory);
                    return await conjugationReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConnectionDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var connectionDefinitionReader = new ConnectionDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await connectionDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConnectionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var connectionUsageReader = new ConnectionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await connectionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Connector"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var connectorReader = new ConnectorReader(cache, this, externalReferenceService, loggerFactory);
                    return await connectorReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConstraintDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var constraintDefinitionReader = new ConstraintDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await constraintDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConstraintUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var constraintUsageReader = new ConstraintUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await constraintUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ConstructorExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var constructorExpressionReader = new ConstructorExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await constructorExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:CrossSubsetting"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var crossSubsettingReader = new CrossSubsettingReader(cache, this, externalReferenceService, loggerFactory);
                    return await crossSubsettingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:DataType"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var dataTypeReader = new DataTypeReader(cache, this, externalReferenceService, loggerFactory);
                    return await dataTypeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:DecisionNode"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var decisionNodeReader = new DecisionNodeReader(cache, this, externalReferenceService, loggerFactory);
                    return await decisionNodeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Definition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var definitionReader = new DefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await definitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Dependency"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var dependencyReader = new DependencyReader(cache, this, externalReferenceService, loggerFactory);
                    return await dependencyReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Differencing"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var differencingReader = new DifferencingReader(cache, this, externalReferenceService, loggerFactory);
                    return await differencingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Disjoining"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var disjoiningReader = new DisjoiningReader(cache, this, externalReferenceService, loggerFactory);
                    return await disjoiningReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Documentation"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var documentationReader = new DocumentationReader(cache, this, externalReferenceService, loggerFactory);
                    return await documentationReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ElementFilterMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var elementFilterMembershipReader = new ElementFilterMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await elementFilterMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:EndFeatureMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var endFeatureMembershipReader = new EndFeatureMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await endFeatureMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:EnumerationDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var enumerationDefinitionReader = new EnumerationDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await enumerationDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:EnumerationUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var enumerationUsageReader = new EnumerationUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await enumerationUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:EventOccurrenceUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var eventOccurrenceUsageReader = new EventOccurrenceUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await eventOccurrenceUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ExhibitStateUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var exhibitStateUsageReader = new ExhibitStateUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await exhibitStateUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Expression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var expressionReader = new ExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await expressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Feature"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureReader = new FeatureReader(cache, this, externalReferenceService, loggerFactory);
                    return await featureReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FeatureChainExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureChainExpressionReader = new FeatureChainExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await featureChainExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FeatureChaining"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureChainingReader = new FeatureChainingReader(cache, this, externalReferenceService, loggerFactory);
                    return await featureChainingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FeatureInverting"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureInvertingReader = new FeatureInvertingReader(cache, this, externalReferenceService, loggerFactory);
                    return await featureInvertingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FeatureMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureMembershipReader = new FeatureMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await featureMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FeatureReferenceExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureReferenceExpressionReader = new FeatureReferenceExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await featureReferenceExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FeatureTyping"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureTypingReader = new FeatureTypingReader(cache, this, externalReferenceService, loggerFactory);
                    return await featureTypingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FeatureValue"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var featureValueReader = new FeatureValueReader(cache, this, externalReferenceService, loggerFactory);
                    return await featureValueReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Flow"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var flowReader = new FlowReader(cache, this, externalReferenceService, loggerFactory);
                    return await flowReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FlowDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var flowDefinitionReader = new FlowDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await flowDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FlowEnd"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var flowEndReader = new FlowEndReader(cache, this, externalReferenceService, loggerFactory);
                    return await flowEndReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FlowUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var flowUsageReader = new FlowUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await flowUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ForkNode"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var forkNodeReader = new ForkNodeReader(cache, this, externalReferenceService, loggerFactory);
                    return await forkNodeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ForLoopActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var forLoopActionUsageReader = new ForLoopActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await forLoopActionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:FramedConcernMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var framedConcernMembershipReader = new FramedConcernMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await framedConcernMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Function"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var functionReader = new FunctionReader(cache, this, externalReferenceService, loggerFactory);
                    return await functionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:IfActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var ifActionUsageReader = new IfActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await ifActionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:IncludeUseCaseUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var includeUseCaseUsageReader = new IncludeUseCaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await includeUseCaseUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:IndexExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var indexExpressionReader = new IndexExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await indexExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Interaction"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var interactionReader = new InteractionReader(cache, this, externalReferenceService, loggerFactory);
                    return await interactionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:InterfaceDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var interfaceDefinitionReader = new InterfaceDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await interfaceDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:InterfaceUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var interfaceUsageReader = new InterfaceUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await interfaceUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Intersecting"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var intersectingReader = new IntersectingReader(cache, this, externalReferenceService, loggerFactory);
                    return await intersectingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Invariant"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var invariantReader = new InvariantReader(cache, this, externalReferenceService, loggerFactory);
                    return await invariantReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:InvocationExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var invocationExpressionReader = new InvocationExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await invocationExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ItemDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var itemDefinitionReader = new ItemDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await itemDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ItemUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var itemUsageReader = new ItemUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await itemUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:JoinNode"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var joinNodeReader = new JoinNodeReader(cache, this, externalReferenceService, loggerFactory);
                    return await joinNodeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:LibraryPackage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var libraryPackageReader = new LibraryPackageReader(cache, this, externalReferenceService, loggerFactory);
                    return await libraryPackageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:LiteralBoolean"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalBooleanReader = new LiteralBooleanReader(cache, this, externalReferenceService, loggerFactory);
                    return await literalBooleanReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:LiteralExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalExpressionReader = new LiteralExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await literalExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:LiteralInfinity"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalInfinityReader = new LiteralInfinityReader(cache, this, externalReferenceService, loggerFactory);
                    return await literalInfinityReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:LiteralInteger"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalIntegerReader = new LiteralIntegerReader(cache, this, externalReferenceService, loggerFactory);
                    return await literalIntegerReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:LiteralRational"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalRationalReader = new LiteralRationalReader(cache, this, externalReferenceService, loggerFactory);
                    return await literalRationalReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:LiteralString"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var literalStringReader = new LiteralStringReader(cache, this, externalReferenceService, loggerFactory);
                    return await literalStringReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Membership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var membershipReader = new MembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await membershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:MembershipExpose"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var membershipExposeReader = new MembershipExposeReader(cache, this, externalReferenceService, loggerFactory);
                    return await membershipExposeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:MembershipImport"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var membershipImportReader = new MembershipImportReader(cache, this, externalReferenceService, loggerFactory);
                    return await membershipImportReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:MergeNode"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var mergeNodeReader = new MergeNodeReader(cache, this, externalReferenceService, loggerFactory);
                    return await mergeNodeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Metaclass"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metaclassReader = new MetaclassReader(cache, this, externalReferenceService, loggerFactory);
                    return await metaclassReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:MetadataAccessExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metadataAccessExpressionReader = new MetadataAccessExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await metadataAccessExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:MetadataDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metadataDefinitionReader = new MetadataDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await metadataDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:MetadataFeature"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metadataFeatureReader = new MetadataFeatureReader(cache, this, externalReferenceService, loggerFactory);
                    return await metadataFeatureReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:MetadataUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var metadataUsageReader = new MetadataUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await metadataUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Multiplicity"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var multiplicityReader = new MultiplicityReader(cache, this, externalReferenceService, loggerFactory);
                    return await multiplicityReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:MultiplicityRange"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var multiplicityRangeReader = new MultiplicityRangeReader(cache, this, externalReferenceService, loggerFactory);
                    return await multiplicityRangeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Namespace"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var namespaceReader = new NamespaceReader(cache, this, externalReferenceService, loggerFactory);
                    return await namespaceReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:NamespaceExpose"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var namespaceExposeReader = new NamespaceExposeReader(cache, this, externalReferenceService, loggerFactory);
                    return await namespaceExposeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:NamespaceImport"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var namespaceImportReader = new NamespaceImportReader(cache, this, externalReferenceService, loggerFactory);
                    return await namespaceImportReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:NullExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var nullExpressionReader = new NullExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await nullExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ObjectiveMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var objectiveMembershipReader = new ObjectiveMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await objectiveMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:OccurrenceDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var occurrenceDefinitionReader = new OccurrenceDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await occurrenceDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:OccurrenceUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var occurrenceUsageReader = new OccurrenceUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await occurrenceUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:OperatorExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var operatorExpressionReader = new OperatorExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await operatorExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:OwningMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var owningMembershipReader = new OwningMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await owningMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Package"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var packageReader = new PackageReader(cache, this, externalReferenceService, loggerFactory);
                    return await packageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ParameterMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var parameterMembershipReader = new ParameterMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await parameterMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:PartDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var partDefinitionReader = new PartDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await partDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:PartUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var partUsageReader = new PartUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await partUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:PayloadFeature"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var payloadFeatureReader = new PayloadFeatureReader(cache, this, externalReferenceService, loggerFactory);
                    return await payloadFeatureReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:PerformActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var performActionUsageReader = new PerformActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await performActionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:PortConjugation"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var portConjugationReader = new PortConjugationReader(cache, this, externalReferenceService, loggerFactory);
                    return await portConjugationReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:PortDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var portDefinitionReader = new PortDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await portDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:PortUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var portUsageReader = new PortUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await portUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Predicate"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var predicateReader = new PredicateReader(cache, this, externalReferenceService, loggerFactory);
                    return await predicateReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Redefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var redefinitionReader = new RedefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await redefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ReferenceSubsetting"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var referenceSubsettingReader = new ReferenceSubsettingReader(cache, this, externalReferenceService, loggerFactory);
                    return await referenceSubsettingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ReferenceUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var referenceUsageReader = new ReferenceUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await referenceUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:RenderingDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var renderingDefinitionReader = new RenderingDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await renderingDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:RenderingUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var renderingUsageReader = new RenderingUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await renderingUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:RequirementConstraintMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var requirementConstraintMembershipReader = new RequirementConstraintMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await requirementConstraintMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:RequirementDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var requirementDefinitionReader = new RequirementDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await requirementDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:RequirementUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var requirementUsageReader = new RequirementUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await requirementUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:RequirementVerificationMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var requirementVerificationMembershipReader = new RequirementVerificationMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await requirementVerificationMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ResultExpressionMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var resultExpressionMembershipReader = new ResultExpressionMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await resultExpressionMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ReturnParameterMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var returnParameterMembershipReader = new ReturnParameterMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await returnParameterMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:SatisfyRequirementUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var satisfyRequirementUsageReader = new SatisfyRequirementUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await satisfyRequirementUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:SelectExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var selectExpressionReader = new SelectExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await selectExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:SendActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var sendActionUsageReader = new SendActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await sendActionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Specialization"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var specializationReader = new SpecializationReader(cache, this, externalReferenceService, loggerFactory);
                    return await specializationReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:StakeholderMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stakeholderMembershipReader = new StakeholderMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await stakeholderMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:StateDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stateDefinitionReader = new StateDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await stateDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:StateSubactionMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stateSubactionMembershipReader = new StateSubactionMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await stateSubactionMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:StateUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stateUsageReader = new StateUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await stateUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Step"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var stepReader = new StepReader(cache, this, externalReferenceService, loggerFactory);
                    return await stepReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Structure"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var structureReader = new StructureReader(cache, this, externalReferenceService, loggerFactory);
                    return await structureReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Subclassification"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var subclassificationReader = new SubclassificationReader(cache, this, externalReferenceService, loggerFactory);
                    return await subclassificationReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:SubjectMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var subjectMembershipReader = new SubjectMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await subjectMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Subsetting"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var subsettingReader = new SubsettingReader(cache, this, externalReferenceService, loggerFactory);
                    return await subsettingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Succession"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var successionReader = new SuccessionReader(cache, this, externalReferenceService, loggerFactory);
                    return await successionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:SuccessionAsUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var successionAsUsageReader = new SuccessionAsUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await successionAsUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:SuccessionFlow"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var successionFlowReader = new SuccessionFlowReader(cache, this, externalReferenceService, loggerFactory);
                    return await successionFlowReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:SuccessionFlowUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var successionFlowUsageReader = new SuccessionFlowUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await successionFlowUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:TerminateActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var terminateActionUsageReader = new TerminateActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await terminateActionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:TextualRepresentation"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var textualRepresentationReader = new TextualRepresentationReader(cache, this, externalReferenceService, loggerFactory);
                    return await textualRepresentationReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:TransitionFeatureMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var transitionFeatureMembershipReader = new TransitionFeatureMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await transitionFeatureMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:TransitionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var transitionUsageReader = new TransitionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await transitionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:TriggerInvocationExpression"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var triggerInvocationExpressionReader = new TriggerInvocationExpressionReader(cache, this, externalReferenceService, loggerFactory);
                    return await triggerInvocationExpressionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Type"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var typeReader = new TypeReader(cache, this, externalReferenceService, loggerFactory);
                    return await typeReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:TypeFeaturing"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var typeFeaturingReader = new TypeFeaturingReader(cache, this, externalReferenceService, loggerFactory);
                    return await typeFeaturingReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Unioning"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var unioningReader = new UnioningReader(cache, this, externalReferenceService, loggerFactory);
                    return await unioningReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:Usage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var usageReader = new UsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await usageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:UseCaseDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var useCaseDefinitionReader = new UseCaseDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await useCaseDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:UseCaseUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var useCaseUsageReader = new UseCaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await useCaseUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:VariantMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var variantMembershipReader = new VariantMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await variantMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:VerificationCaseDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var verificationCaseDefinitionReader = new VerificationCaseDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await verificationCaseDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:VerificationCaseUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var verificationCaseUsageReader = new VerificationCaseUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await verificationCaseUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ViewDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewDefinitionReader = new ViewDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await viewDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ViewpointDefinition"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewpointDefinitionReader = new ViewpointDefinitionReader(cache, this, externalReferenceService, loggerFactory);
                    return await viewpointDefinitionReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ViewpointUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewpointUsageReader = new ViewpointUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await viewpointUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ViewRenderingMembership"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewRenderingMembershipReader = new ViewRenderingMembershipReader(cache, this, externalReferenceService, loggerFactory);
                    return await viewRenderingMembershipReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:ViewUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var viewUsageReader = new ViewUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await viewUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
                ["sysml:WhileLoopActionUsage"] = async (cache, externalReferenceService, loggerFactory, xmlReader, currentLocation) =>
                {
                    using var subXmlReader = xmlReader.ReadSubtree();
                    var whileLoopActionUsageReader = new WhileLoopActionUsageReader(cache, this, externalReferenceService, loggerFactory);
                    return await whileLoopActionUsageReader.ReadAsync(subXmlReader, currentLocation);
                },
            };
        }

        /// <summary>
        /// Queries an instance of an <see cref="IData" /> based on the position of the <see cref="XmlReader"/>.
        /// When the reader is at an XML Element and has an attribute of xsi:type, the value of that attribute is used to select the appropriate
        /// XmiElementReader from which the <see cref="IData"/> is returned.
        /// </summary>
        /// <param name="xmiReader">The current <see cref="XmlReader"/></param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/> to register and query read <see cref="IData"/></param>
        /// <param name="currentLocation">The <see cref="Uri"/> that keep tracks of the current location</param>
        /// <param name="externalReferenceService">The <see cref="IExternalReferenceService"/> used to register and process external references</param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> used to set up logging</param>
        /// <param name="explicitTypeName">The explicit type name to resolve, in case of un-specified xsi:type</param>
        /// <returns>An instance of the read <see cref="IData"/></returns>
        /// <exception cref="InvalidOperationException">If the xsi:type is not supported</exception>
        public IData QueryXmiData(XmlReader xmiReader, IXmiDataCache xmiDataCache, Uri currentLocation, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory, string explicitTypeName = "")
        {
            AssertValidQueryXmiDataParameters(xmiReader, xmiDataCache, currentLocation);

            var xsiType = xmiReader.GetAttribute("xsi:type");

            if (xsiType == null && string.IsNullOrEmpty(explicitTypeName))
            {
                throw new InvalidOperationException($"The xsi:type is not specified");
            }

            xsiType ??= explicitTypeName;

            if (this.readerCache.TryGetValue(xsiType, out var readerFactory))
            {
                return readerFactory(xmiDataCache, externalReferenceService, loggerFactory, xmiReader, currentLocation);
            }

            throw new InvalidOperationException($"No reader found for xsi:type - {xsiType}");
        }

        /// <summary>
        /// Queries asynchronously an instance of an <see cref="IData" /> based on the position of the <see cref="XmlReader"/>.
        /// When the reader is at an XML Element and has an attribute of xsi:type, the value of that attribute is used to select the appropriate
        /// XmiElementReader from which the <see cref="IData"/> is returned.
        /// </summary>
        /// <param name="xmiReader">The current <see cref="XmlReader"/></param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/> to register and query read <see cref="IData"/></param>
        /// <param name="currentLocation">The <see cref="Uri"/> that keep tracks of the current location</param>
        /// <param name="externalReferenceService">The <see cref="IExternalReferenceService"/> used to register and process external references</param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> used to set up logging</param>
        /// <param name="explicitTypeName">The explicit type name to resolve, in case of un-specified xsi:type</param>
        /// <returns>An awaitable <see cref="Task" /> with the instance of the read <see cref="IData"/></returns>
        /// <exception cref="InvalidOperationException">If the xsi:type is not supported</exception>
        public Task<IData> QueryXmiDataAsync(XmlReader xmiReader, IXmiDataCache xmiDataCache, Uri currentLocation, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory, string explicitTypeName = "")
        {
            AssertValidQueryXmiDataParameters(xmiReader, xmiDataCache, currentLocation);

            var xsiType = xmiReader.GetAttribute("xsi:type");

            if (xsiType == null && string.IsNullOrEmpty(explicitTypeName))
            {
                throw new InvalidOperationException($"The xsi:type is not specified");
            }

            xsiType ??= explicitTypeName;

            if (this.readerAsyncCache.TryGetValue(xsiType, out var readerFactory))
            {
                return readerFactory(xmiDataCache, externalReferenceService, loggerFactory, xmiReader, currentLocation);
            }

            throw new InvalidOperationException($"No reader found for xsi:type - {xsiType}");
        }

        /// <summary>
        /// Asserts that parameters provided to QueryXmiData and QueryXmiDataAsync methods are valid
        /// </summary>
        /// <param name="xmiReader">The current <see cref="XmlReader"/></param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/> to register and query read <see cref="IData"/></param>
        /// <param name="currentLocation">The <see cref="Uri"/> that keep tracks of the current location</param>
        /// <exception cref="ArgumentNullException">If one of the parameter is null</exception>
        private static void AssertValidQueryXmiDataParameters(XmlReader xmiReader, IXmiDataCache xmiDataCache, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            if (xmiDataCache == null)
            {
                throw new ArgumentNullException(nameof(xmiDataCache));
            }

            if (currentLocation == null)
            {
                throw new ArgumentNullException(nameof(currentLocation));
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
