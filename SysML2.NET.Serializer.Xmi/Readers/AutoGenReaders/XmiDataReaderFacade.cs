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
        /// <returns>An instance of the read <see cref="IData"/></returns>
        /// <exception cref="InvalidOperationException">If the xsi:type is not supported</exception>
        public IData QueryXmiData(XmlReader xmiReader, IXmiDataCache xmiDataCache, Uri currentLocation, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory)
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

            var xsiType = xmiReader.GetAttribute("xsi:type");

            if (xsiType == null)
            {
                throw new InvalidOperationException($"The xsi:type is not specified");
            }

            if (this.readerCache.TryGetValue(xsiType, out var readerFactory))
            {
                return readerFactory(xmiDataCache, externalReferenceService, loggerFactory, xmiReader, currentLocation);
            }

            throw new InvalidOperationException($"No reader found for xsi:type - {xsiType}");
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
