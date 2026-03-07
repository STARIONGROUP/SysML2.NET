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
    using System.Xml;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="XmiDataWriterFacade"/> is to dispatch writing of <see cref="IData"/> instances
    /// to the appropriate per-type static writer class
    /// </summary>
    public class XmiDataWriterFacade : IXmiDataWriterFacade
    {
        /// <summary>
        /// A dictionary that contains actions that write <see cref="IData"/> based on a key that represents the POCO type name
        /// </summary>
        private readonly Dictionary<string, Action<XmlWriter, IData, string, bool, IXmiDataWriterFacade, IXmiElementOriginMap, Uri>> writerCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmiDataWriterFacade"/>
        /// </summary>
        public XmiDataWriterFacade()
        {
            this.writerCache = new Dictionary<string, Action<XmlWriter, IData, string, bool, IXmiDataWriterFacade, IXmiElementOriginMap, Uri>>
            {
                ["AcceptActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AcceptActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IAcceptActionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["ActionDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ActionDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IActionDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IActionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["ActorMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ActorMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IActorMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["AllocationDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AllocationDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["AllocationUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AllocationUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Allocations.IAllocationUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["AnalysisCaseDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AnalysisCaseDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["AnalysisCaseUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AnalysisCaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["AnnotatingElement"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AnnotatingElementWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.IAnnotatingElement)data, elementName, includeDerived, facade, originMap, uri),
                ["Annotation"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AnnotationWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.IAnnotation)data, elementName, includeDerived, facade, originMap, uri),
                ["AssertConstraintUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AssertConstraintUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Constraints.IAssertConstraintUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["AssignmentActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AssignmentActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IAssignmentActionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Association"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AssociationWriter.Write(xmlWriter, (Core.POCO.Kernel.Associations.IAssociation)data, elementName, includeDerived, facade, originMap, uri),
                ["AssociationStructure"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AssociationStructureWriter.Write(xmlWriter, (Core.POCO.Kernel.Associations.IAssociationStructure)data, elementName, includeDerived, facade, originMap, uri),
                ["AttributeDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AttributeDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["AttributeUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    AttributeUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Attributes.IAttributeUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Behavior"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    BehaviorWriter.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IBehavior)data, elementName, includeDerived, facade, originMap, uri),
                ["BindingConnector"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    BindingConnectorWriter.Write(xmlWriter, (Core.POCO.Kernel.Connectors.IBindingConnector)data, elementName, includeDerived, facade, originMap, uri),
                ["BindingConnectorAsUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    BindingConnectorAsUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Connections.IBindingConnectorAsUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["BooleanExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    BooleanExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IBooleanExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["CalculationDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    CalculationDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["CalculationUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    CalculationUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Calculations.ICalculationUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["CaseDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    CaseDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Cases.ICaseDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["CaseUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    CaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Cases.ICaseUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Class"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ClassWriter.Write(xmlWriter, (Core.POCO.Kernel.Classes.IClass)data, elementName, includeDerived, facade, originMap, uri),
                ["Classifier"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ClassifierWriter.Write(xmlWriter, (Core.POCO.Core.Classifiers.IClassifier)data, elementName, includeDerived, facade, originMap, uri),
                ["CollectExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    CollectExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ICollectExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["Comment"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    CommentWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.IComment)data, elementName, includeDerived, facade, originMap, uri),
                ["ConcernDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConcernDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IConcernDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ConcernUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConcernUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IConcernUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["ConjugatedPortDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConjugatedPortDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ConjugatedPortTyping"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConjugatedPortTypingWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IConjugatedPortTyping)data, elementName, includeDerived, facade, originMap, uri),
                ["Conjugation"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConjugationWriter.Write(xmlWriter, (Core.POCO.Core.Types.IConjugation)data, elementName, includeDerived, facade, originMap, uri),
                ["ConnectionDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConnectionDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Connections.IConnectionDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ConnectionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConnectionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Connections.IConnectionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Connector"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConnectorWriter.Write(xmlWriter, (Core.POCO.Kernel.Connectors.IConnector)data, elementName, includeDerived, facade, originMap, uri),
                ["ConstraintDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConstraintDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ConstraintUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConstraintUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Constraints.IConstraintUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["ConstructorExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ConstructorExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IConstructorExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["CrossSubsetting"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    CrossSubsettingWriter.Write(xmlWriter, (Core.POCO.Core.Features.ICrossSubsetting)data, elementName, includeDerived, facade, originMap, uri),
                ["DataType"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    DataTypeWriter.Write(xmlWriter, (Core.POCO.Kernel.DataTypes.IDataType)data, elementName, includeDerived, facade, originMap, uri),
                ["DecisionNode"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    DecisionNodeWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IDecisionNode)data, elementName, includeDerived, facade, originMap, uri),
                ["Definition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    DefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["Dependency"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    DependencyWriter.Write(xmlWriter, (Core.POCO.Root.Dependencies.IDependency)data, elementName, includeDerived, facade, originMap, uri),
                ["Differencing"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    DifferencingWriter.Write(xmlWriter, (Core.POCO.Core.Types.IDifferencing)data, elementName, includeDerived, facade, originMap, uri),
                ["Disjoining"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    DisjoiningWriter.Write(xmlWriter, (Core.POCO.Core.Types.IDisjoining)data, elementName, includeDerived, facade, originMap, uri),
                ["Documentation"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    DocumentationWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.IDocumentation)data, elementName, includeDerived, facade, originMap, uri),
                ["ElementFilterMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ElementFilterMembershipWriter.Write(xmlWriter, (Core.POCO.Kernel.Packages.IElementFilterMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["EndFeatureMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    EndFeatureMembershipWriter.Write(xmlWriter, (Core.POCO.Core.Features.IEndFeatureMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["EnumerationDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    EnumerationDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["EnumerationUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    EnumerationUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Enumerations.IEnumerationUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["EventOccurrenceUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    EventOccurrenceUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IEventOccurrenceUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["ExhibitStateUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ExhibitStateUsageWriter.Write(xmlWriter, (Core.POCO.Systems.States.IExhibitStateUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Expression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["Feature"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FeatureWriter.Write(xmlWriter, (Core.POCO.Core.Features.IFeature)data, elementName, includeDerived, facade, originMap, uri),
                ["FeatureChainExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FeatureChainExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureChainExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["FeatureChaining"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FeatureChainingWriter.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureChaining)data, elementName, includeDerived, facade, originMap, uri),
                ["FeatureInverting"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FeatureInvertingWriter.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureInverting)data, elementName, includeDerived, facade, originMap, uri),
                ["FeatureMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FeatureMembershipWriter.Write(xmlWriter, (Core.POCO.Core.Types.IFeatureMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["FeatureReferenceExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FeatureReferenceExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IFeatureReferenceExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["FeatureTyping"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FeatureTypingWriter.Write(xmlWriter, (Core.POCO.Core.Features.IFeatureTyping)data, elementName, includeDerived, facade, originMap, uri),
                ["FeatureValue"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FeatureValueWriter.Write(xmlWriter, (Core.POCO.Kernel.FeatureValues.IFeatureValue)data, elementName, includeDerived, facade, originMap, uri),
                ["Flow"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FlowWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IFlow)data, elementName, includeDerived, facade, originMap, uri),
                ["FlowDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FlowDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Flows.IFlowDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["FlowEnd"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FlowEndWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IFlowEnd)data, elementName, includeDerived, facade, originMap, uri),
                ["FlowUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FlowUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Flows.IFlowUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["ForkNode"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ForkNodeWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IForkNode)data, elementName, includeDerived, facade, originMap, uri),
                ["ForLoopActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ForLoopActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IForLoopActionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["FramedConcernMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FramedConcernMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IFramedConcernMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["Function"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    FunctionWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IFunction)data, elementName, includeDerived, facade, originMap, uri),
                ["IfActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    IfActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IIfActionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["IncludeUseCaseUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    IncludeUseCaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.UseCases.IIncludeUseCaseUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["IndexExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    IndexExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IIndexExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["Interaction"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    InteractionWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IInteraction)data, elementName, includeDerived, facade, originMap, uri),
                ["InterfaceDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    InterfaceDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["InterfaceUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    InterfaceUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Interfaces.IInterfaceUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Intersecting"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    IntersectingWriter.Write(xmlWriter, (Core.POCO.Core.Types.IIntersecting)data, elementName, includeDerived, facade, originMap, uri),
                ["Invariant"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    InvariantWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IInvariant)data, elementName, includeDerived, facade, originMap, uri),
                ["InvocationExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    InvocationExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IInvocationExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["ItemDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ItemDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Items.IItemDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ItemUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ItemUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Items.IItemUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["JoinNode"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    JoinNodeWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IJoinNode)data, elementName, includeDerived, facade, originMap, uri),
                ["LibraryPackage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    LibraryPackageWriter.Write(xmlWriter, (Core.POCO.Kernel.Packages.ILibraryPackage)data, elementName, includeDerived, facade, originMap, uri),
                ["LiteralBoolean"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    LiteralBooleanWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralBoolean)data, elementName, includeDerived, facade, originMap, uri),
                ["LiteralExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    LiteralExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["LiteralInfinity"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    LiteralInfinityWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInfinity)data, elementName, includeDerived, facade, originMap, uri),
                ["LiteralInteger"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    LiteralIntegerWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralInteger)data, elementName, includeDerived, facade, originMap, uri),
                ["LiteralRational"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    LiteralRationalWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralRational)data, elementName, includeDerived, facade, originMap, uri),
                ["LiteralString"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    LiteralStringWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ILiteralString)data, elementName, includeDerived, facade, originMap, uri),
                ["Membership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MembershipWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.IMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["MembershipExpose"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MembershipExposeWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IMembershipExpose)data, elementName, includeDerived, facade, originMap, uri),
                ["MembershipImport"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MembershipImportWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.IMembershipImport)data, elementName, includeDerived, facade, originMap, uri),
                ["MergeNode"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MergeNodeWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IMergeNode)data, elementName, includeDerived, facade, originMap, uri),
                ["Metaclass"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MetaclassWriter.Write(xmlWriter, (Core.POCO.Kernel.Metadata.IMetaclass)data, elementName, includeDerived, facade, originMap, uri),
                ["MetadataAccessExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MetadataAccessExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IMetadataAccessExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["MetadataDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MetadataDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["MetadataFeature"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MetadataFeatureWriter.Write(xmlWriter, (Core.POCO.Kernel.Metadata.IMetadataFeature)data, elementName, includeDerived, facade, originMap, uri),
                ["MetadataUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MetadataUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Metadata.IMetadataUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Multiplicity"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MultiplicityWriter.Write(xmlWriter, (Core.POCO.Core.Types.IMultiplicity)data, elementName, includeDerived, facade, originMap, uri),
                ["MultiplicityRange"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    MultiplicityRangeWriter.Write(xmlWriter, (Core.POCO.Kernel.Multiplicities.IMultiplicityRange)data, elementName, includeDerived, facade, originMap, uri),
                ["Namespace"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    NamespaceWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.INamespace)data, elementName, includeDerived, facade, originMap, uri),
                ["NamespaceExpose"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    NamespaceExposeWriter.Write(xmlWriter, (Core.POCO.Systems.Views.INamespaceExpose)data, elementName, includeDerived, facade, originMap, uri),
                ["NamespaceImport"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    NamespaceImportWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.INamespaceImport)data, elementName, includeDerived, facade, originMap, uri),
                ["NullExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    NullExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.INullExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["ObjectiveMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ObjectiveMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Cases.IObjectiveMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["OccurrenceDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    OccurrenceDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["OccurrenceUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    OccurrenceUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Occurrences.IOccurrenceUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["OperatorExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    OperatorExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.IOperatorExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["OwningMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    OwningMembershipWriter.Write(xmlWriter, (Core.POCO.Root.Namespaces.IOwningMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["Package"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PackageWriter.Write(xmlWriter, (Core.POCO.Kernel.Packages.IPackage)data, elementName, includeDerived, facade, originMap, uri),
                ["ParameterMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ParameterMembershipWriter.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IParameterMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["PartDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PartDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Parts.IPartDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["PartUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PartUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Parts.IPartUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["PayloadFeature"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PayloadFeatureWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.IPayloadFeature)data, elementName, includeDerived, facade, originMap, uri),
                ["PerformActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PerformActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IPerformActionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["PortConjugation"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PortConjugationWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortConjugation)data, elementName, includeDerived, facade, originMap, uri),
                ["PortDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PortDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["PortUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PortUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Ports.IPortUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Predicate"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    PredicateWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IPredicate)data, elementName, includeDerived, facade, originMap, uri),
                ["Redefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    RedefinitionWriter.Write(xmlWriter, (Core.POCO.Core.Features.IRedefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ReferenceSubsetting"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ReferenceSubsettingWriter.Write(xmlWriter, (Core.POCO.Core.Features.IReferenceSubsetting)data, elementName, includeDerived, facade, originMap, uri),
                ["ReferenceUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ReferenceUsageWriter.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["RenderingDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    RenderingDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IRenderingDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["RenderingUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    RenderingUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IRenderingUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["RequirementConstraintMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    RequirementConstraintMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementConstraintMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["RequirementDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    RequirementDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["RequirementUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    RequirementUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IRequirementUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["RequirementVerificationMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    RequirementVerificationMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["ResultExpressionMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ResultExpressionMembershipWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IResultExpressionMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["ReturnParameterMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ReturnParameterMembershipWriter.Write(xmlWriter, (Core.POCO.Kernel.Functions.IReturnParameterMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["SatisfyRequirementUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SatisfyRequirementUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.ISatisfyRequirementUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["SelectExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SelectExpressionWriter.Write(xmlWriter, (Core.POCO.Kernel.Expressions.ISelectExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["SendActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SendActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.ISendActionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Specialization"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SpecializationWriter.Write(xmlWriter, (Core.POCO.Core.Types.ISpecialization)data, elementName, includeDerived, facade, originMap, uri),
                ["StakeholderMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    StakeholderMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.IStakeholderMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["StateDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    StateDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.States.IStateDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["StateSubactionMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    StateSubactionMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.States.IStateSubactionMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["StateUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    StateUsageWriter.Write(xmlWriter, (Core.POCO.Systems.States.IStateUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["Step"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    StepWriter.Write(xmlWriter, (Core.POCO.Kernel.Behaviors.IStep)data, elementName, includeDerived, facade, originMap, uri),
                ["Structure"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    StructureWriter.Write(xmlWriter, (Core.POCO.Kernel.Structures.IStructure)data, elementName, includeDerived, facade, originMap, uri),
                ["Subclassification"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SubclassificationWriter.Write(xmlWriter, (Core.POCO.Core.Classifiers.ISubclassification)data, elementName, includeDerived, facade, originMap, uri),
                ["SubjectMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SubjectMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Requirements.ISubjectMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["Subsetting"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SubsettingWriter.Write(xmlWriter, (Core.POCO.Core.Features.ISubsetting)data, elementName, includeDerived, facade, originMap, uri),
                ["Succession"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SuccessionWriter.Write(xmlWriter, (Core.POCO.Kernel.Connectors.ISuccession)data, elementName, includeDerived, facade, originMap, uri),
                ["SuccessionAsUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SuccessionAsUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Connections.ISuccessionAsUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["SuccessionFlow"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SuccessionFlowWriter.Write(xmlWriter, (Core.POCO.Kernel.Interactions.ISuccessionFlow)data, elementName, includeDerived, facade, originMap, uri),
                ["SuccessionFlowUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    SuccessionFlowUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Flows.ISuccessionFlowUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["TerminateActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    TerminateActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.ITerminateActionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["TextualRepresentation"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    TextualRepresentationWriter.Write(xmlWriter, (Core.POCO.Root.Annotations.ITextualRepresentation)data, elementName, includeDerived, facade, originMap, uri),
                ["TransitionFeatureMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    TransitionFeatureMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.States.ITransitionFeatureMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["TransitionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    TransitionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.States.ITransitionUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["TriggerInvocationExpression"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    TriggerInvocationExpressionWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.ITriggerInvocationExpression)data, elementName, includeDerived, facade, originMap, uri),
                ["Type"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    TypeWriter.Write(xmlWriter, (Core.POCO.Core.Types.IType)data, elementName, includeDerived, facade, originMap, uri),
                ["TypeFeaturing"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    TypeFeaturingWriter.Write(xmlWriter, (Core.POCO.Core.Features.ITypeFeaturing)data, elementName, includeDerived, facade, originMap, uri),
                ["Unioning"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    UnioningWriter.Write(xmlWriter, (Core.POCO.Core.Types.IUnioning)data, elementName, includeDerived, facade, originMap, uri),
                ["Usage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    UsageWriter.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["UseCaseDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    UseCaseDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["UseCaseUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    UseCaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.UseCases.IUseCaseUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["VariantMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    VariantMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.DefinitionAndUsage.IVariantMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["VerificationCaseDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    VerificationCaseDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["VerificationCaseUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    VerificationCaseUsageWriter.Write(xmlWriter, (Core.POCO.Systems.VerificationCases.IVerificationCaseUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["ViewDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ViewDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ViewpointDefinition"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ViewpointDefinitionWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewpointDefinition)data, elementName, includeDerived, facade, originMap, uri),
                ["ViewpointUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ViewpointUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewpointUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["ViewRenderingMembership"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ViewRenderingMembershipWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewRenderingMembership)data, elementName, includeDerived, facade, originMap, uri),
                ["ViewUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    ViewUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Views.IViewUsage)data, elementName, includeDerived, facade, originMap, uri),
                ["WhileLoopActionUsage"] = (xmlWriter, data, elementName, includeDerived, facade, originMap, uri) =>
                    WhileLoopActionUsageWriter.Write(xmlWriter, (Core.POCO.Systems.Actions.IWhileLoopActionUsage)data, elementName, includeDerived, facade, originMap, uri),
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
        public void Write(XmlWriter xmlWriter, IData data, string elementName, bool includeDerivedProperties, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            var typeName = data.GetType().Name;

            if (this.writerCache.TryGetValue(typeName, out var writer))
            {
                writer(xmlWriter, data, elementName, includeDerivedProperties, this, elementOriginMap, currentFileUri);
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
        public void WriteContainedElement(XmlWriter xmlWriter, IData childData, string elementName, bool includeDerivedProperties, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
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

            this.Write(xmlWriter, childData, elementName, includeDerivedProperties, elementOriginMap, currentFileUri);
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
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
