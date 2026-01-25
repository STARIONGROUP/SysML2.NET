// -------------------------------------------------------------------------------------------------
// <copyright file="Payload.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

/* -------------------------------------------------------------------------- |
 | index | property name                                                      |
 | -------------------------------------------------------------------------- |
 | -------------------------------------------------------------------------- |
 |  0    | Created                                                            |
 | -------------------------------------------------------------------------- |
 | 1    | Systems.Actions.AcceptActionUsage
 | -------------------------------------------------------------------------- |
 | 2    | Systems.Actions.ActionDefinition
 | -------------------------------------------------------------------------- |
 | 3    | Systems.Actions.ActionUsage
 | -------------------------------------------------------------------------- |
 | 4    | Systems.Requirements.ActorMembership
 | -------------------------------------------------------------------------- |
 | 5    | Systems.Allocations.AllocationDefinition
 | -------------------------------------------------------------------------- |
 | 6    | Systems.Allocations.AllocationUsage
 | -------------------------------------------------------------------------- |
 | 7    | Systems.AnalysisCases.AnalysisCaseDefinition
 | -------------------------------------------------------------------------- |
 | 8    | Systems.AnalysisCases.AnalysisCaseUsage
 | -------------------------------------------------------------------------- |
 | 9    | Root.Annotations.AnnotatingElement
 | -------------------------------------------------------------------------- |
 | 10    | Root.Annotations.Annotation
 | -------------------------------------------------------------------------- |
 | 11    | Systems.Constraints.AssertConstraintUsage
 | -------------------------------------------------------------------------- |
 | 12    | Systems.Actions.AssignmentActionUsage
 | -------------------------------------------------------------------------- |
 | 13    | Kernel.Associations.Association
 | -------------------------------------------------------------------------- |
 | 14    | Kernel.Associations.AssociationStructure
 | -------------------------------------------------------------------------- |
 | 15    | Systems.Attributes.AttributeDefinition
 | -------------------------------------------------------------------------- |
 | 16    | Systems.Attributes.AttributeUsage
 | -------------------------------------------------------------------------- |
 | 17    | Kernel.Behaviors.Behavior
 | -------------------------------------------------------------------------- |
 | 18    | Kernel.Connectors.BindingConnector
 | -------------------------------------------------------------------------- |
 | 19    | Systems.Connections.BindingConnectorAsUsage
 | -------------------------------------------------------------------------- |
 | 20    | Kernel.Functions.BooleanExpression
 | -------------------------------------------------------------------------- |
 | 21    | Systems.Calculations.CalculationDefinition
 | -------------------------------------------------------------------------- |
 | 22    | Systems.Calculations.CalculationUsage
 | -------------------------------------------------------------------------- |
 | 23    | Systems.Cases.CaseDefinition
 | -------------------------------------------------------------------------- |
 | 24    | Systems.Cases.CaseUsage
 | -------------------------------------------------------------------------- |
 | 25    | Kernel.Classes.Class
 | -------------------------------------------------------------------------- |
 | 26    | Core.Classifiers.Classifier
 | -------------------------------------------------------------------------- |
 | 27    | Kernel.Expressions.CollectExpression
 | -------------------------------------------------------------------------- |
 | 28    | Root.Annotations.Comment
 | -------------------------------------------------------------------------- |
 | 29    | Systems.Requirements.ConcernDefinition
 | -------------------------------------------------------------------------- |
 | 30    | Systems.Requirements.ConcernUsage
 | -------------------------------------------------------------------------- |
 | 31    | Systems.Ports.ConjugatedPortDefinition
 | -------------------------------------------------------------------------- |
 | 32    | Systems.Ports.ConjugatedPortTyping
 | -------------------------------------------------------------------------- |
 | 33    | Core.Types.Conjugation
 | -------------------------------------------------------------------------- |
 | 34    | Systems.Connections.ConnectionDefinition
 | -------------------------------------------------------------------------- |
 | 35    | Systems.Connections.ConnectionUsage
 | -------------------------------------------------------------------------- |
 | 36    | Kernel.Connectors.Connector
 | -------------------------------------------------------------------------- |
 | 37    | Systems.Constraints.ConstraintDefinition
 | -------------------------------------------------------------------------- |
 | 38    | Systems.Constraints.ConstraintUsage
 | -------------------------------------------------------------------------- |
 | 39    | Kernel.Expressions.ConstructorExpression
 | -------------------------------------------------------------------------- |
 | 40    | Core.Features.CrossSubsetting
 | -------------------------------------------------------------------------- |
 | 41    | Kernel.DataTypes.DataType
 | -------------------------------------------------------------------------- |
 | 42    | Systems.Actions.DecisionNode
 | -------------------------------------------------------------------------- |
 | 43    | Systems.DefinitionAndUsage.Definition
 | -------------------------------------------------------------------------- |
 | 44    | Root.Dependencies.Dependency
 | -------------------------------------------------------------------------- |
 | 45    | Core.Types.Differencing
 | -------------------------------------------------------------------------- |
 | 46    | Core.Types.Disjoining
 | -------------------------------------------------------------------------- |
 | 47    | Root.Annotations.Documentation
 | -------------------------------------------------------------------------- |
 | 48    | Kernel.Packages.ElementFilterMembership
 | -------------------------------------------------------------------------- |
 | 49    | Core.Features.EndFeatureMembership
 | -------------------------------------------------------------------------- |
 | 50    | Systems.Enumerations.EnumerationDefinition
 | -------------------------------------------------------------------------- |
 | 51    | Systems.Enumerations.EnumerationUsage
 | -------------------------------------------------------------------------- |
 | 52    | Systems.Occurrences.EventOccurrenceUsage
 | -------------------------------------------------------------------------- |
 | 53    | Systems.States.ExhibitStateUsage
 | -------------------------------------------------------------------------- |
 | 54    | Kernel.Functions.Expression
 | -------------------------------------------------------------------------- |
 | 55    | Core.Features.Feature
 | -------------------------------------------------------------------------- |
 | 56    | Kernel.Expressions.FeatureChainExpression
 | -------------------------------------------------------------------------- |
 | 57    | Core.Features.FeatureChaining
 | -------------------------------------------------------------------------- |
 | 58    | Core.Features.FeatureInverting
 | -------------------------------------------------------------------------- |
 | 59    | Core.Types.FeatureMembership
 | -------------------------------------------------------------------------- |
 | 60    | Kernel.Expressions.FeatureReferenceExpression
 | -------------------------------------------------------------------------- |
 | 61    | Core.Features.FeatureTyping
 | -------------------------------------------------------------------------- |
 | 62    | Kernel.FeatureValues.FeatureValue
 | -------------------------------------------------------------------------- |
 | 63    | Kernel.Interactions.Flow
 | -------------------------------------------------------------------------- |
 | 64    | Systems.Flows.FlowDefinition
 | -------------------------------------------------------------------------- |
 | 65    | Kernel.Interactions.FlowEnd
 | -------------------------------------------------------------------------- |
 | 66    | Systems.Flows.FlowUsage
 | -------------------------------------------------------------------------- |
 | 67    | Systems.Actions.ForkNode
 | -------------------------------------------------------------------------- |
 | 68    | Systems.Actions.ForLoopActionUsage
 | -------------------------------------------------------------------------- |
 | 69    | Systems.Requirements.FramedConcernMembership
 | -------------------------------------------------------------------------- |
 | 70    | Kernel.Functions.Function
 | -------------------------------------------------------------------------- |
 | 71    | Systems.Actions.IfActionUsage
 | -------------------------------------------------------------------------- |
 | 72    | Systems.UseCases.IncludeUseCaseUsage
 | -------------------------------------------------------------------------- |
 | 73    | Kernel.Expressions.IndexExpression
 | -------------------------------------------------------------------------- |
 | 74    | Kernel.Interactions.Interaction
 | -------------------------------------------------------------------------- |
 | 75    | Systems.Interfaces.InterfaceDefinition
 | -------------------------------------------------------------------------- |
 | 76    | Systems.Interfaces.InterfaceUsage
 | -------------------------------------------------------------------------- |
 | 77    | Core.Types.Intersecting
 | -------------------------------------------------------------------------- |
 | 78    | Kernel.Functions.Invariant
 | -------------------------------------------------------------------------- |
 | 79    | Kernel.Expressions.InvocationExpression
 | -------------------------------------------------------------------------- |
 | 80    | Systems.Items.ItemDefinition
 | -------------------------------------------------------------------------- |
 | 81    | Systems.Items.ItemUsage
 | -------------------------------------------------------------------------- |
 | 82    | Systems.Actions.JoinNode
 | -------------------------------------------------------------------------- |
 | 83    | Kernel.Packages.LibraryPackage
 | -------------------------------------------------------------------------- |
 | 84    | Kernel.Expressions.LiteralBoolean
 | -------------------------------------------------------------------------- |
 | 85    | Kernel.Expressions.LiteralExpression
 | -------------------------------------------------------------------------- |
 | 86    | Kernel.Expressions.LiteralInfinity
 | -------------------------------------------------------------------------- |
 | 87    | Kernel.Expressions.LiteralInteger
 | -------------------------------------------------------------------------- |
 | 88    | Kernel.Expressions.LiteralRational
 | -------------------------------------------------------------------------- |
 | 89    | Kernel.Expressions.LiteralString
 | -------------------------------------------------------------------------- |
 | 90    | Root.Namespaces.Membership
 | -------------------------------------------------------------------------- |
 | 91    | Systems.Views.MembershipExpose
 | -------------------------------------------------------------------------- |
 | 92    | Root.Namespaces.MembershipImport
 | -------------------------------------------------------------------------- |
 | 93    | Systems.Actions.MergeNode
 | -------------------------------------------------------------------------- |
 | 94    | Kernel.Metadata.Metaclass
 | -------------------------------------------------------------------------- |
 | 95    | Kernel.Expressions.MetadataAccessExpression
 | -------------------------------------------------------------------------- |
 | 96    | Systems.Metadata.MetadataDefinition
 | -------------------------------------------------------------------------- |
 | 97    | Kernel.Metadata.MetadataFeature
 | -------------------------------------------------------------------------- |
 | 98    | Systems.Metadata.MetadataUsage
 | -------------------------------------------------------------------------- |
 | 99    | Core.Types.Multiplicity
 | -------------------------------------------------------------------------- |
 | 100    | Kernel.Multiplicities.MultiplicityRange
 | -------------------------------------------------------------------------- |
 | 101    | Root.Namespaces.Namespace
 | -------------------------------------------------------------------------- |
 | 102    | Systems.Views.NamespaceExpose
 | -------------------------------------------------------------------------- |
 | 103    | Root.Namespaces.NamespaceImport
 | -------------------------------------------------------------------------- |
 | 104    | Kernel.Expressions.NullExpression
 | -------------------------------------------------------------------------- |
 | 105    | Systems.Cases.ObjectiveMembership
 | -------------------------------------------------------------------------- |
 | 106    | Systems.Occurrences.OccurrenceDefinition
 | -------------------------------------------------------------------------- |
 | 107    | Systems.Occurrences.OccurrenceUsage
 | -------------------------------------------------------------------------- |
 | 108    | Kernel.Expressions.OperatorExpression
 | -------------------------------------------------------------------------- |
 | 109    | Root.Namespaces.OwningMembership
 | -------------------------------------------------------------------------- |
 | 110    | Kernel.Packages.Package
 | -------------------------------------------------------------------------- |
 | 111    | Kernel.Behaviors.ParameterMembership
 | -------------------------------------------------------------------------- |
 | 112    | Systems.Parts.PartDefinition
 | -------------------------------------------------------------------------- |
 | 113    | Systems.Parts.PartUsage
 | -------------------------------------------------------------------------- |
 | 114    | Kernel.Interactions.PayloadFeature
 | -------------------------------------------------------------------------- |
 | 115    | Systems.Actions.PerformActionUsage
 | -------------------------------------------------------------------------- |
 | 116    | Systems.Ports.PortConjugation
 | -------------------------------------------------------------------------- |
 | 117    | Systems.Ports.PortDefinition
 | -------------------------------------------------------------------------- |
 | 118    | Systems.Ports.PortUsage
 | -------------------------------------------------------------------------- |
 | 119    | Kernel.Functions.Predicate
 | -------------------------------------------------------------------------- |
 | 120    | Core.Features.Redefinition
 | -------------------------------------------------------------------------- |
 | 121    | Core.Features.ReferenceSubsetting
 | -------------------------------------------------------------------------- |
 | 122    | Systems.DefinitionAndUsage.ReferenceUsage
 | -------------------------------------------------------------------------- |
 | 123    | Systems.Views.RenderingDefinition
 | -------------------------------------------------------------------------- |
 | 124    | Systems.Views.RenderingUsage
 | -------------------------------------------------------------------------- |
 | 125    | Systems.Requirements.RequirementConstraintMembership
 | -------------------------------------------------------------------------- |
 | 126    | Systems.Requirements.RequirementDefinition
 | -------------------------------------------------------------------------- |
 | 127    | Systems.Requirements.RequirementUsage
 | -------------------------------------------------------------------------- |
 | 128    | Systems.VerificationCases.RequirementVerificationMembership
 | -------------------------------------------------------------------------- |
 | 129    | Kernel.Functions.ResultExpressionMembership
 | -------------------------------------------------------------------------- |
 | 130    | Kernel.Functions.ReturnParameterMembership
 | -------------------------------------------------------------------------- |
 | 131    | Systems.Requirements.SatisfyRequirementUsage
 | -------------------------------------------------------------------------- |
 | 132    | Kernel.Expressions.SelectExpression
 | -------------------------------------------------------------------------- |
 | 133    | Systems.Actions.SendActionUsage
 | -------------------------------------------------------------------------- |
 | 134    | Core.Types.Specialization
 | -------------------------------------------------------------------------- |
 | 135    | Systems.Requirements.StakeholderMembership
 | -------------------------------------------------------------------------- |
 | 136    | Systems.States.StateDefinition
 | -------------------------------------------------------------------------- |
 | 137    | Systems.States.StateSubactionMembership
 | -------------------------------------------------------------------------- |
 | 138    | Systems.States.StateUsage
 | -------------------------------------------------------------------------- |
 | 139    | Kernel.Behaviors.Step
 | -------------------------------------------------------------------------- |
 | 140    | Kernel.Structures.Structure
 | -------------------------------------------------------------------------- |
 | 141    | Core.Classifiers.Subclassification
 | -------------------------------------------------------------------------- |
 | 142    | Systems.Requirements.SubjectMembership
 | -------------------------------------------------------------------------- |
 | 143    | Core.Features.Subsetting
 | -------------------------------------------------------------------------- |
 | 144    | Kernel.Connectors.Succession
 | -------------------------------------------------------------------------- |
 | 145    | Systems.Connections.SuccessionAsUsage
 | -------------------------------------------------------------------------- |
 | 146    | Kernel.Interactions.SuccessionFlow
 | -------------------------------------------------------------------------- |
 | 147    | Systems.Flows.SuccessionFlowUsage
 | -------------------------------------------------------------------------- |
 | 148    | Systems.Actions.TerminateActionUsage
 | -------------------------------------------------------------------------- |
 | 149    | Root.Annotations.TextualRepresentation
 | -------------------------------------------------------------------------- |
 | 150    | Systems.States.TransitionFeatureMembership
 | -------------------------------------------------------------------------- |
 | 151    | Systems.States.TransitionUsage
 | -------------------------------------------------------------------------- |
 | 152    | Systems.Actions.TriggerInvocationExpression
 | -------------------------------------------------------------------------- |
 | 153    | Core.Types.Type
 | -------------------------------------------------------------------------- |
 | 154    | Core.Features.TypeFeaturing
 | -------------------------------------------------------------------------- |
 | 155    | Core.Types.Unioning
 | -------------------------------------------------------------------------- |
 | 156    | Systems.DefinitionAndUsage.Usage
 | -------------------------------------------------------------------------- |
 | 157    | Systems.UseCases.UseCaseDefinition
 | -------------------------------------------------------------------------- |
 | 158    | Systems.UseCases.UseCaseUsage
 | -------------------------------------------------------------------------- |
 | 159    | Systems.DefinitionAndUsage.VariantMembership
 | -------------------------------------------------------------------------- |
 | 160    | Systems.VerificationCases.VerificationCaseDefinition
 | -------------------------------------------------------------------------- |
 | 161    | Systems.VerificationCases.VerificationCaseUsage
 | -------------------------------------------------------------------------- |
 | 162    | Systems.Views.ViewDefinition
 | -------------------------------------------------------------------------- |
 | 163    | Systems.Views.ViewpointDefinition
 | -------------------------------------------------------------------------- |
 | 164    | Systems.Views.ViewpointUsage
 | -------------------------------------------------------------------------- |
 | 165    | Systems.Views.ViewRenderingMembership
 | -------------------------------------------------------------------------- |
 | 166    | Systems.Views.ViewUsage
 | -------------------------------------------------------------------------- |
 | 167    | Systems.Actions.WhileLoopActionUsage
 | -------------------------------------------------------------------------- |
 * -------------------------------------------------------------------------- | */

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Payload"/> acts as envelope around the Core DTO classes and is used as
    /// construct to transport the objects using MessagePack
    /// </summary>
    internal class Payload
    {
        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> at which the <see cref="Payload"/> was created
        /// </summary>
        internal DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage> AcceptActionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition> ActionDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.ActionUsage> ActionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership> ActorMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition> AllocationDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage> AllocationUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition> AnalysisCaseDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage> AnalysisCaseUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement> AnnotatingElement { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Annotations.Annotation> Annotation { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage> AssertConstraintUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage> AssignmentActionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Associations.Association> Association { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure> AssociationStructure { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition> AttributeDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage> AttributeUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior> Behavior { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector> BindingConnector { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage> BindingConnectorAsUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression> BooleanExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition> CalculationDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage> CalculationUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition> CaseDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Cases.CaseUsage> CaseUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Classes.Class> Class { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Classifiers.Classifier> Classifier { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression> CollectExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Annotations.Comment> Comment { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition> ConcernDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage> ConcernUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition> ConjugatedPortDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping> ConjugatedPortTyping { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.Conjugation> Conjugation { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition> ConnectionDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage> ConnectionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Connectors.Connector> Connector { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition> ConstraintDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage> ConstraintUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression> ConstructorExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.CrossSubsetting> CrossSubsetting { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.DataTypes.DataType> DataType { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.DecisionNode> DecisionNode { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition> Definition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Dependencies.Dependency> Dependency { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.Differencing> Differencing { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.Disjoining> Disjoining { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Annotations.Documentation> Documentation { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership> ElementFilterMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership> EndFeatureMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition> EnumerationDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage> EnumerationUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage> EventOccurrenceUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage> ExhibitStateUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Functions.Expression> Expression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.Feature> Feature { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression> FeatureChainExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.FeatureChaining> FeatureChaining { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.FeatureInverting> FeatureInverting { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.FeatureMembership> FeatureMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression> FeatureReferenceExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.FeatureTyping> FeatureTyping { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue> FeatureValue { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Interactions.Flow> Flow { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition> FlowDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd> FlowEnd { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Flows.FlowUsage> FlowUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.ForkNode> ForkNode { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage> ForLoopActionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership> FramedConcernMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Functions.Function> Function { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage> IfActionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage> IncludeUseCaseUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression> IndexExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Interactions.Interaction> Interaction { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition> InterfaceDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage> InterfaceUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.Intersecting> Intersecting { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Functions.Invariant> Invariant { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression> InvocationExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Items.ItemDefinition> ItemDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Items.ItemUsage> ItemUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.JoinNode> JoinNode { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage> LibraryPackage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean> LiteralBoolean { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression> LiteralExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity> LiteralInfinity { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger> LiteralInteger { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational> LiteralRational { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString> LiteralString { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Namespaces.Membership> Membership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.MembershipExpose> MembershipExpose { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport> MembershipImport { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.MergeNode> MergeNode { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass> Metaclass { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression> MetadataAccessExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition> MetadataDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature> MetadataFeature { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage> MetadataUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.Multiplicity> Multiplicity { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange> MultiplicityRange { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Namespaces.Namespace> Namespace { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose> NamespaceExpose { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport> NamespaceImport { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression> NullExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership> ObjectiveMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition> OccurrenceDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage> OccurrenceUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression> OperatorExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership> OwningMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Packages.Package> Package { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership> ParameterMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Parts.PartDefinition> PartDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Parts.PartUsage> PartUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature> PayloadFeature { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage> PerformActionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Ports.PortConjugation> PortConjugation { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Ports.PortDefinition> PortDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Ports.PortUsage> PortUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Functions.Predicate> Predicate { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.Redefinition> Redefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting> ReferenceSubsetting { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage> ReferenceUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition> RenderingDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.RenderingUsage> RenderingUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership> RequirementConstraintMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition> RequirementDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage> RequirementUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership> RequirementVerificationMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership> ResultExpressionMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership> ReturnParameterMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage> SatisfyRequirementUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression> SelectExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage> SendActionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.Specialization> Specialization { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership> StakeholderMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.States.StateDefinition> StateDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership> StateSubactionMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.States.StateUsage> StateUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Behaviors.Step> Step { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Structures.Structure> Structure { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Classifiers.Subclassification> Subclassification { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership> SubjectMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.Subsetting> Subsetting { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Connectors.Succession> Succession { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage> SuccessionAsUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow> SuccessionFlow { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage> SuccessionFlowUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage> TerminateActionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation> TextualRepresentation { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership> TransitionFeatureMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.States.TransitionUsage> TransitionUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression> TriggerInvocationExpression { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.Type> Type { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Features.TypeFeaturing> TypeFeaturing { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Core.Types.Unioning> Unioning { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage> Usage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition> UseCaseDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage> UseCaseUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership> VariantMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition> VerificationCaseDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage> VerificationCaseUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.ViewDefinition> ViewDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition> ViewpointDefinition { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage> ViewpointUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership> ViewRenderingMembership { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Views.ViewUsage> ViewUsage { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of <see cref="AnnotatingElement"/>.
        /// </summary>
        internal List<SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage> WhileLoopActionUsage { get; set; } = [];

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------