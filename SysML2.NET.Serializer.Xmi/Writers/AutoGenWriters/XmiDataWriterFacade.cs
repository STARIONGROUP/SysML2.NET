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
        /// The <see cref="ILoggerFactory"/> used to set up logging for writer instances
        /// </summary>
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// A dictionary that contains actions that write <see cref="IData"/> based on a key that represents the POCO type name
        /// </summary>
        private readonly Dictionary<string, Action<XmlWriter, IData, string, bool, bool, IXmiElementOriginMap, Uri>> writerCache;

        /// <summary>
        /// A dictionary that contains functions that asynchronously write <see cref="IData"/> based on a key that represents the POCO type name
        /// </summary>
        private readonly Dictionary<string, Func<XmlWriter, IData, string, bool, bool, IXmiElementOriginMap, Uri, Task>> writerAsyncCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmiDataWriterFacade"/>
        /// </summary>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> used to set up logging</param>
        public XmiDataWriterFacade(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;

            this.writerCache = new Dictionary<string, Action<XmlWriter, IData, string, bool, bool, IXmiElementOriginMap, Uri>>
            {
                ["AcceptActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AcceptActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IAcceptActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ActionDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ActionDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IActionDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ActorMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ActorMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.IActorMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AllocationDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AllocationDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AllocationUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AllocationUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AnalysisCaseDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AnalysisCaseDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AnalysisCaseUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AnalysisCaseUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AnnotatingElement"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AnnotatingElementWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Annotations.IAnnotatingElement)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Annotation"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AnnotationWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Annotations.IAnnotation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AssertConstraintUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AssertConstraintUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Constraints.IAssertConstraintUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AssignmentActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AssignmentActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IAssignmentActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Association"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AssociationWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Associations.IAssociation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AssociationStructure"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AssociationStructureWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Associations.IAssociationStructure)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AttributeDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AttributeDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AttributeUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AttributeUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Behavior"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new BehaviorWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IBehavior)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["BindingConnector"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new BindingConnectorWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Connectors.IBindingConnector)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["BindingConnectorAsUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new BindingConnectorAsUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Connections.IBindingConnectorAsUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["BooleanExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new BooleanExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Functions.IBooleanExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CalculationDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CalculationDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CalculationUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CalculationUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CaseDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CaseDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Cases.ICaseDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CaseUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CaseUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Cases.ICaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Class"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ClassWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Classes.IClass)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Classifier"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ClassifierWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Classifiers.IClassifier)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CollectExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CollectExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ICollectExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Comment"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CommentWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Annotations.IComment)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConcernDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConcernDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.IConcernDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConcernUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConcernUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.IConcernUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConjugatedPortDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConjugatedPortDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConjugatedPortTyping"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConjugatedPortTypingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortTyping)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Conjugation"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConjugationWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.IConjugation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConnectionDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConnectionDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Connections.IConnectionDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConnectionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConnectionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Connections.IConnectionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Connector"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConnectorWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Connectors.IConnector)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConstraintDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConstraintDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConstraintUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConstraintUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConstructorExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConstructorExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IConstructorExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CrossSubsetting"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CrossSubsettingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.ICrossSubsetting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["DataType"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DataTypeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.DataTypes.IDataType)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["DecisionNode"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DecisionNodeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IDecisionNode)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Definition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Dependency"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DependencyWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Dependencies.IDependency)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Differencing"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DifferencingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.IDifferencing)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Disjoining"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DisjoiningWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.IDisjoining)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Documentation"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DocumentationWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Annotations.IDocumentation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ElementFilterMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ElementFilterMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Packages.IElementFilterMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["EndFeatureMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new EndFeatureMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.IEndFeatureMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["EnumerationDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new EnumerationDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["EnumerationUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new EnumerationUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["EventOccurrenceUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new EventOccurrenceUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IEventOccurrenceUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ExhibitStateUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ExhibitStateUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.States.IExhibitStateUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Expression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Functions.IExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Feature"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.IFeature)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureChainExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureChainExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureChainExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureChaining"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureChainingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureChaining)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureInverting"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureInvertingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureInverting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.IFeatureMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureReferenceExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureReferenceExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureReferenceExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureTyping"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureTypingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureTyping)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureValue"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureValueWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.FeatureValues.IFeatureValue)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Flow"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FlowWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IFlow)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FlowDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FlowDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Flows.IFlowDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FlowEnd"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FlowEndWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IFlowEnd)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FlowUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FlowUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Flows.IFlowUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ForkNode"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ForkNodeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IForkNode)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ForLoopActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ForLoopActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IForLoopActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FramedConcernMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FramedConcernMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.IFramedConcernMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Function"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FunctionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Functions.IFunction)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["IfActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new IfActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IIfActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["IncludeUseCaseUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new IncludeUseCaseUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.UseCases.IIncludeUseCaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["IndexExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new IndexExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IIndexExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Interaction"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InteractionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IInteraction)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["InterfaceDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InterfaceDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["InterfaceUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InterfaceUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Intersecting"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new IntersectingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.IIntersecting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Invariant"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InvariantWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Functions.IInvariant)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["InvocationExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InvocationExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IInvocationExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ItemDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ItemDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Items.IItemDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ItemUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ItemUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Items.IItemUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["JoinNode"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new JoinNodeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IJoinNode)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LibraryPackage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LibraryPackageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Packages.ILibraryPackage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralBoolean"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralBooleanWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralBoolean)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralInfinity"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralInfinityWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInfinity)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralInteger"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralIntegerWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInteger)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralRational"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralRationalWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralRational)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralString"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralStringWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralString)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Membership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Namespaces.IMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MembershipExpose"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MembershipExposeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.IMembershipExpose)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MembershipImport"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MembershipImportWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Namespaces.IMembershipImport)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MergeNode"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MergeNodeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IMergeNode)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Metaclass"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetaclassWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Metadata.IMetaclass)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MetadataAccessExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetadataAccessExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IMetadataAccessExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MetadataDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetadataDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MetadataFeature"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetadataFeatureWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Metadata.IMetadataFeature)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MetadataUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetadataUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Multiplicity"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MultiplicityWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.IMultiplicity)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MultiplicityRange"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MultiplicityRangeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Multiplicities.IMultiplicityRange)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Namespace"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new NamespaceWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Namespaces.INamespace)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["NamespaceExpose"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new NamespaceExposeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.INamespaceExpose)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["NamespaceImport"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new NamespaceImportWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Namespaces.INamespaceImport)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["NullExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new NullExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.INullExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ObjectiveMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ObjectiveMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Cases.IObjectiveMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["OccurrenceDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new OccurrenceDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["OccurrenceUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new OccurrenceUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["OperatorExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new OperatorExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IOperatorExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["OwningMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new OwningMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Namespaces.IOwningMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Package"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PackageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Packages.IPackage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ParameterMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ParameterMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IParameterMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PartDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PartDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Parts.IPartDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PartUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PartUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Parts.IPartUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PayloadFeature"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PayloadFeatureWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IPayloadFeature)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PerformActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PerformActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IPerformActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PortConjugation"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PortConjugationWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortConjugation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PortDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PortDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PortUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PortUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Predicate"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PredicateWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Functions.IPredicate)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Redefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RedefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.IRedefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ReferenceSubsetting"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ReferenceSubsettingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.IReferenceSubsetting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ReferenceUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ReferenceUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RenderingDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RenderingDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.IRenderingDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RenderingUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RenderingUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.IRenderingUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RequirementConstraintMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RequirementConstraintMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementConstraintMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RequirementDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RequirementDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RequirementUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RequirementUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RequirementVerificationMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RequirementVerificationMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ResultExpressionMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ResultExpressionMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Functions.IResultExpressionMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ReturnParameterMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ReturnParameterMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Functions.IReturnParameterMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SatisfyRequirementUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SatisfyRequirementUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.ISatisfyRequirementUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SelectExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SelectExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ISelectExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SendActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SendActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.ISendActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Specialization"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SpecializationWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.ISpecialization)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["StakeholderMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StakeholderMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.IStakeholderMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["StateDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StateDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.States.IStateDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["StateSubactionMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StateSubactionMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.States.IStateSubactionMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["StateUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StateUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.States.IStateUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Step"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StepWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IStep)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Structure"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StructureWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Structures.IStructure)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Subclassification"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SubclassificationWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Classifiers.ISubclassification)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SubjectMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SubjectMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Requirements.ISubjectMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Subsetting"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SubsettingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.ISubsetting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Succession"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SuccessionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Connectors.ISuccession)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SuccessionAsUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SuccessionAsUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Connections.ISuccessionAsUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SuccessionFlow"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SuccessionFlowWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Kernel.Interactions.ISuccessionFlow)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SuccessionFlowUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SuccessionFlowUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Flows.ISuccessionFlowUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TerminateActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TerminateActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.ITerminateActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TextualRepresentation"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TextualRepresentationWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Root.Annotations.ITextualRepresentation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TransitionFeatureMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TransitionFeatureMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.States.ITransitionFeatureMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TransitionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TransitionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.States.ITransitionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TriggerInvocationExpression"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TriggerInvocationExpressionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.ITriggerInvocationExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Type"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TypeWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.IType)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TypeFeaturing"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TypeFeaturingWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Features.ITypeFeaturing)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Unioning"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new UnioningWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Core.Types.IUnioning)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Usage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new UsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["UseCaseDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new UseCaseDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["UseCaseUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new UseCaseUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["VariantMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new VariantMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IVariantMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["VerificationCaseDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new VerificationCaseDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["VerificationCaseUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new VerificationCaseUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.IViewDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewpointDefinition"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewpointDefinitionWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.IViewpointDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewpointUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewpointUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.IViewpointUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewRenderingMembership"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewRenderingMembershipWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.IViewRenderingMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Views.IViewUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["WhileLoopActionUsage"] = (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new WhileLoopActionUsageWriter(this, this.loggerFactory);
                    writer.Write(xmlWriter, (Core.POCO.Systems.Actions.IWhileLoopActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
            };

            this.writerAsyncCache = new Dictionary<string, Func<XmlWriter, IData, string, bool, bool, IXmiElementOriginMap, Uri, Task>>
            {
                ["AcceptActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AcceptActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IAcceptActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ActionDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ActionDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IActionDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ActorMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ActorMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IActorMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AllocationDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AllocationDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AllocationUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AllocationUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AnalysisCaseDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AnalysisCaseDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AnalysisCaseUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AnalysisCaseUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AnnotatingElement"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AnnotatingElementWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.IAnnotatingElement)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Annotation"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AnnotationWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.IAnnotation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AssertConstraintUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AssertConstraintUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Constraints.IAssertConstraintUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AssignmentActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AssignmentActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IAssignmentActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Association"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AssociationWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Associations.IAssociation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AssociationStructure"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AssociationStructureWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Associations.IAssociationStructure)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AttributeDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AttributeDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["AttributeUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new AttributeUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Behavior"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new BehaviorWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Behaviors.IBehavior)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["BindingConnector"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new BindingConnectorWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Connectors.IBindingConnector)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["BindingConnectorAsUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new BindingConnectorAsUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Connections.IBindingConnectorAsUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["BooleanExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new BooleanExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IBooleanExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CalculationDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CalculationDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CalculationUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CalculationUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CaseDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CaseDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Cases.ICaseDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CaseUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CaseUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Cases.ICaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Class"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ClassWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Classes.IClass)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Classifier"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ClassifierWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Classifiers.IClassifier)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CollectExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CollectExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ICollectExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Comment"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CommentWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.IComment)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConcernDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConcernDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IConcernDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConcernUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConcernUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IConcernUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConjugatedPortDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConjugatedPortDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConjugatedPortTyping"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConjugatedPortTypingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortTyping)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Conjugation"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConjugationWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IConjugation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConnectionDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConnectionDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Connections.IConnectionDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConnectionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConnectionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Connections.IConnectionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Connector"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConnectorWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Connectors.IConnector)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConstraintDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConstraintDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConstraintUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConstraintUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ConstructorExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ConstructorExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IConstructorExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["CrossSubsetting"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new CrossSubsettingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.ICrossSubsetting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["DataType"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DataTypeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.DataTypes.IDataType)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["DecisionNode"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DecisionNodeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IDecisionNode)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Definition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Dependency"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DependencyWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Dependencies.IDependency)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Differencing"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DifferencingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IDifferencing)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Disjoining"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DisjoiningWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IDisjoining)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Documentation"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new DocumentationWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.IDocumentation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ElementFilterMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ElementFilterMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Packages.IElementFilterMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["EndFeatureMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new EndFeatureMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IEndFeatureMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["EnumerationDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new EnumerationDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["EnumerationUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new EnumerationUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["EventOccurrenceUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new EventOccurrenceUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Occurrences.IEventOccurrenceUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ExhibitStateUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ExhibitStateUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.States.IExhibitStateUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Expression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Feature"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IFeature)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureChainExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureChainExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureChainExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureChaining"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureChainingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IFeatureChaining)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureInverting"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureInvertingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IFeatureInverting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IFeatureMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureReferenceExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureReferenceExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureReferenceExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureTyping"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureTypingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IFeatureTyping)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FeatureValue"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FeatureValueWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.FeatureValues.IFeatureValue)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Flow"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FlowWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.IFlow)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FlowDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FlowDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Flows.IFlowDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FlowEnd"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FlowEndWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.IFlowEnd)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FlowUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FlowUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Flows.IFlowUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ForkNode"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ForkNodeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IForkNode)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ForLoopActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ForLoopActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IForLoopActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["FramedConcernMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FramedConcernMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IFramedConcernMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Function"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new FunctionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IFunction)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["IfActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new IfActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IIfActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["IncludeUseCaseUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new IncludeUseCaseUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.UseCases.IIncludeUseCaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["IndexExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new IndexExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IIndexExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Interaction"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InteractionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.IInteraction)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["InterfaceDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InterfaceDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["InterfaceUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InterfaceUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Intersecting"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new IntersectingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IIntersecting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Invariant"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InvariantWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IInvariant)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["InvocationExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new InvocationExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IInvocationExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ItemDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ItemDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Items.IItemDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ItemUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ItemUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Items.IItemUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["JoinNode"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new JoinNodeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IJoinNode)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LibraryPackage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LibraryPackageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Packages.ILibraryPackage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralBoolean"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralBooleanWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralBoolean)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralInfinity"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralInfinityWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInfinity)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralInteger"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralIntegerWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInteger)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralRational"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralRationalWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralRational)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["LiteralString"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new LiteralStringWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralString)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Membership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.IMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MembershipExpose"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MembershipExposeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IMembershipExpose)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MembershipImport"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MembershipImportWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.IMembershipImport)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MergeNode"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MergeNodeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IMergeNode)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Metaclass"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetaclassWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Metadata.IMetaclass)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MetadataAccessExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetadataAccessExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IMetadataAccessExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MetadataDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetadataDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MetadataFeature"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetadataFeatureWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Metadata.IMetadataFeature)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MetadataUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MetadataUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Multiplicity"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MultiplicityWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IMultiplicity)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["MultiplicityRange"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new MultiplicityRangeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Multiplicities.IMultiplicityRange)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Namespace"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new NamespaceWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.INamespace)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["NamespaceExpose"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new NamespaceExposeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.INamespaceExpose)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["NamespaceImport"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new NamespaceImportWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.INamespaceImport)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["NullExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new NullExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.INullExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ObjectiveMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ObjectiveMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Cases.IObjectiveMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["OccurrenceDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new OccurrenceDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["OccurrenceUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new OccurrenceUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["OperatorExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new OperatorExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.IOperatorExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["OwningMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new OwningMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Namespaces.IOwningMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Package"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PackageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Packages.IPackage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ParameterMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ParameterMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Behaviors.IParameterMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PartDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PartDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Parts.IPartDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PartUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PartUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Parts.IPartUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PayloadFeature"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PayloadFeatureWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.IPayloadFeature)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PerformActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PerformActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IPerformActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PortConjugation"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PortConjugationWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IPortConjugation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PortDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PortDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IPortDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["PortUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PortUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Ports.IPortUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Predicate"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new PredicateWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IPredicate)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Redefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RedefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IRedefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ReferenceSubsetting"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ReferenceSubsettingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.IReferenceSubsetting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ReferenceUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ReferenceUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RenderingDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RenderingDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IRenderingDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RenderingUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RenderingUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IRenderingUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RequirementConstraintMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RequirementConstraintMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementConstraintMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RequirementDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RequirementDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RequirementUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RequirementUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["RequirementVerificationMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new RequirementVerificationMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ResultExpressionMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ResultExpressionMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IResultExpressionMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ReturnParameterMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ReturnParameterMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Functions.IReturnParameterMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SatisfyRequirementUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SatisfyRequirementUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.ISatisfyRequirementUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SelectExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SelectExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Expressions.ISelectExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SendActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SendActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.ISendActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Specialization"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SpecializationWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.ISpecialization)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["StakeholderMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StakeholderMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.IStakeholderMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["StateDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StateDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.States.IStateDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["StateSubactionMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StateSubactionMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.States.IStateSubactionMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["StateUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StateUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.States.IStateUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Step"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StepWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Behaviors.IStep)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Structure"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new StructureWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Structures.IStructure)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Subclassification"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SubclassificationWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Classifiers.ISubclassification)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SubjectMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SubjectMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Requirements.ISubjectMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Subsetting"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SubsettingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.ISubsetting)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Succession"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SuccessionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Connectors.ISuccession)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SuccessionAsUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SuccessionAsUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Connections.ISuccessionAsUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SuccessionFlow"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SuccessionFlowWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Kernel.Interactions.ISuccessionFlow)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["SuccessionFlowUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new SuccessionFlowUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Flows.ISuccessionFlowUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TerminateActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TerminateActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.ITerminateActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TextualRepresentation"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TextualRepresentationWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Root.Annotations.ITextualRepresentation)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TransitionFeatureMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TransitionFeatureMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.States.ITransitionFeatureMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TransitionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TransitionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.States.ITransitionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TriggerInvocationExpression"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TriggerInvocationExpressionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.ITriggerInvocationExpression)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Type"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TypeWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IType)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["TypeFeaturing"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new TypeFeaturingWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Features.ITypeFeaturing)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Unioning"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new UnioningWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Core.Types.IUnioning)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["Usage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new UsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["UseCaseDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new UseCaseDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["UseCaseUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new UseCaseUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["VariantMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new VariantMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IVariantMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["VerificationCaseDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new VerificationCaseDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["VerificationCaseUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new VerificationCaseUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewpointDefinition"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewpointDefinitionWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewpointDefinition)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewpointUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewpointUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewpointUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewRenderingMembership"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewRenderingMembershipWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewRenderingMembership)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["ViewUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new ViewUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Views.IViewUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
                ["WhileLoopActionUsage"] = async (xmlWriter, data, elementName, includeDerived, includesImplied, originMap, uri) =>
                {
                    var writer = new WhileLoopActionUsageWriter(this, this.loggerFactory);
                    await writer.WriteAsync(xmlWriter, (Core.POCO.Systems.Actions.IWhileLoopActionUsage)data, elementName, includeDerived, includesImplied, originMap, uri);
                },
            };
        }

        /// <summary>
        /// Writes the specified <see cref="IData"/> as an XMI element by dispatching to the appropriate per-type writer
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="data">The <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        public void Write(XmlWriter xmlWriter, IData data, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            var typeName = data.GetType().Name;

            if (this.writerCache.TryGetValue(typeName, out var writer))
            {
                writer(xmlWriter, data, elementName, includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        public void WriteContainedElement(XmlWriter xmlWriter, IData childData, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (!includesImplied && childData is IRelationship rel && rel.IsImplied)
            {
                return;
            }

            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    WriteHrefElement(xmlWriter, childData, elementName, childSourceFile, currentFileUri);
                    return;
                }
            }

            this.Write(xmlWriter, childData, elementName, includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
        }

        /// <summary>
        /// Writes a reference child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        public void WriteReferenceElement(XmlWriter xmlWriter, IData childData, string elementName, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    WriteHrefElement(xmlWriter, childData, elementName, childSourceFile, currentFileUri);
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
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        public async Task WriteAsync(XmlWriter xmlWriter, IData data, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            var typeName = data.GetType().Name;

            if (this.writerAsyncCache.TryGetValue(typeName, out var writer))
            {
                await writer(xmlWriter, data, elementName, includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        public async Task WriteContainedElementAsync(XmlWriter xmlWriter, IData childData, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (!includesImplied && childData is IRelationship rel && rel.IsImplied)
            {
                return;
            }

            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    await WriteHrefElementAsync(xmlWriter, childData, elementName, childSourceFile, currentFileUri);
                    return;
                }
            }

            await this.WriteAsync(xmlWriter, childData, elementName, includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
        }

        /// <summary>
        /// Asynchronously writes a reference child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        public async Task WriteReferenceElementAsync(XmlWriter xmlWriter, IData childData, string elementName, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (elementOriginMap != null && currentFileUri != null)
            {
                var childSourceFile = elementOriginMap.GetSourceFile(childData.Id);

                if (childSourceFile != null && childSourceFile != currentFileUri)
                {
                    await WriteHrefElementAsync(xmlWriter, childData, elementName, childSourceFile, currentFileUri);
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
        private static void WriteHrefElement(XmlWriter xmlWriter, IData childData, string elementName, Uri targetFile, Uri currentFile)
        {
            var relativePath = currentFile.MakeRelativeUri(targetFile);
            var href = $"{Uri.UnescapeDataString(relativePath.ToString())}#{childData.Id}";

            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("href", href);
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes an href element for cross-file references
        /// </summary>
        private static async Task WriteHrefElementAsync(XmlWriter xmlWriter, IData childData, string elementName, Uri targetFile, Uri currentFile)
        {
            var relativePath = currentFile.MakeRelativeUri(targetFile);
            var href = $"{Uri.UnescapeDataString(relativePath.ToString())}#{childData.Id}";

            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync(null, "href", null, href);
            await xmlWriter.WriteEndElementAsync();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
