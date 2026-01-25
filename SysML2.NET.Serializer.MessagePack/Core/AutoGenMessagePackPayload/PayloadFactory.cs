// -------------------------------------------------------------------------------------------------
// <copyright file="PayloadFactory.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    using SysML2.NET.Core.DTO.Root.Annotations;

    /// <summary>
    /// The purpose of the <see cref="PayloadFactory"/> class is to create an
    /// instance of <see cref="Payload"/>
    /// </summary>
    internal static class PayloadFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="Payload"/> from the provided <see cref="IEnumerable{IData}"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> on the bases of which the <see cref="Payload"/> will
        /// be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="Payload"/>
        /// </returns>
        internal static Payload ToPayload(this IEnumerable<IData> dataItems)
        {
            if (dataItems == null)
            {
                throw new ArgumentNullException(nameof(dataItems));
            }

            var payload = new Payload
            {
                Created = DateTime.UtcNow,
            };

            // compute the capacity of each DTO type to optimize the size of the Payload properties
            var capacity = new Dictionary<Type, int>(capacity: 167);

            foreach (var item in dataItems)
            {
                var type = item.GetType();
                capacity.TryGetValue(type, out var current);
                capacity[type] = current + 1;
            }

            // based on the computed capacity, set the size of each Payload property
            SetCapacity(payload.AcceptActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage));
            SetCapacity(payload.ActionDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition));
            SetCapacity(payload.ActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.ActionUsage));
            SetCapacity(payload.ActorMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership));
            SetCapacity(payload.AllocationDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition));
            SetCapacity(payload.AllocationUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage));
            SetCapacity(payload.AnalysisCaseDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition));
            SetCapacity(payload.AnalysisCaseUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage));
            SetCapacity(payload.AnnotatingElement, capacity, typeof(SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement));
            SetCapacity(payload.Annotation, capacity, typeof(SysML2.NET.Core.DTO.Root.Annotations.Annotation));
            SetCapacity(payload.AssertConstraintUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage));
            SetCapacity(payload.AssignmentActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage));
            SetCapacity(payload.Association, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Associations.Association));
            SetCapacity(payload.AssociationStructure, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure));
            SetCapacity(payload.AttributeDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition));
            SetCapacity(payload.AttributeUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage));
            SetCapacity(payload.Behavior, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior));
            SetCapacity(payload.BindingConnector, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector));
            SetCapacity(payload.BindingConnectorAsUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage));
            SetCapacity(payload.BooleanExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression));
            SetCapacity(payload.CalculationDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition));
            SetCapacity(payload.CalculationUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage));
            SetCapacity(payload.CaseDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition));
            SetCapacity(payload.CaseUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Cases.CaseUsage));
            SetCapacity(payload.Class, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Classes.Class));
            SetCapacity(payload.Classifier, capacity, typeof(SysML2.NET.Core.DTO.Core.Classifiers.Classifier));
            SetCapacity(payload.CollectExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression));
            SetCapacity(payload.Comment, capacity, typeof(SysML2.NET.Core.DTO.Root.Annotations.Comment));
            SetCapacity(payload.ConcernDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition));
            SetCapacity(payload.ConcernUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage));
            SetCapacity(payload.ConjugatedPortDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition));
            SetCapacity(payload.ConjugatedPortTyping, capacity, typeof(SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping));
            SetCapacity(payload.Conjugation, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.Conjugation));
            SetCapacity(payload.ConnectionDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition));
            SetCapacity(payload.ConnectionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage));
            SetCapacity(payload.Connector, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Connectors.Connector));
            SetCapacity(payload.ConstraintDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition));
            SetCapacity(payload.ConstraintUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage));
            SetCapacity(payload.ConstructorExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression));
            SetCapacity(payload.CrossSubsetting, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.CrossSubsetting));
            SetCapacity(payload.DataType, capacity, typeof(SysML2.NET.Core.DTO.Kernel.DataTypes.DataType));
            SetCapacity(payload.DecisionNode, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.DecisionNode));
            SetCapacity(payload.Definition, capacity, typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition));
            SetCapacity(payload.Dependency, capacity, typeof(SysML2.NET.Core.DTO.Root.Dependencies.Dependency));
            SetCapacity(payload.Differencing, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.Differencing));
            SetCapacity(payload.Disjoining, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.Disjoining));
            SetCapacity(payload.Documentation, capacity, typeof(SysML2.NET.Core.DTO.Root.Annotations.Documentation));
            SetCapacity(payload.ElementFilterMembership, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership));
            SetCapacity(payload.EndFeatureMembership, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership));
            SetCapacity(payload.EnumerationDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition));
            SetCapacity(payload.EnumerationUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage));
            SetCapacity(payload.EventOccurrenceUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage));
            SetCapacity(payload.ExhibitStateUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage));
            SetCapacity(payload.Expression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Functions.Expression));
            SetCapacity(payload.Feature, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.Feature));
            SetCapacity(payload.FeatureChainExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression));
            SetCapacity(payload.FeatureChaining, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.FeatureChaining));
            SetCapacity(payload.FeatureInverting, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.FeatureInverting));
            SetCapacity(payload.FeatureMembership, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.FeatureMembership));
            SetCapacity(payload.FeatureReferenceExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression));
            SetCapacity(payload.FeatureTyping, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.FeatureTyping));
            SetCapacity(payload.FeatureValue, capacity, typeof(SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue));
            SetCapacity(payload.Flow, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Interactions.Flow));
            SetCapacity(payload.FlowDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition));
            SetCapacity(payload.FlowEnd, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd));
            SetCapacity(payload.FlowUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Flows.FlowUsage));
            SetCapacity(payload.ForkNode, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.ForkNode));
            SetCapacity(payload.ForLoopActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage));
            SetCapacity(payload.FramedConcernMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership));
            SetCapacity(payload.Function, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Functions.Function));
            SetCapacity(payload.IfActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage));
            SetCapacity(payload.IncludeUseCaseUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage));
            SetCapacity(payload.IndexExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression));
            SetCapacity(payload.Interaction, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Interactions.Interaction));
            SetCapacity(payload.InterfaceDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition));
            SetCapacity(payload.InterfaceUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage));
            SetCapacity(payload.Intersecting, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.Intersecting));
            SetCapacity(payload.Invariant, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Functions.Invariant));
            SetCapacity(payload.InvocationExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression));
            SetCapacity(payload.ItemDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Items.ItemDefinition));
            SetCapacity(payload.ItemUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Items.ItemUsage));
            SetCapacity(payload.JoinNode, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.JoinNode));
            SetCapacity(payload.LibraryPackage, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage));
            SetCapacity(payload.LiteralBoolean, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean));
            SetCapacity(payload.LiteralExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression));
            SetCapacity(payload.LiteralInfinity, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity));
            SetCapacity(payload.LiteralInteger, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger));
            SetCapacity(payload.LiteralRational, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational));
            SetCapacity(payload.LiteralString, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString));
            SetCapacity(payload.Membership, capacity, typeof(SysML2.NET.Core.DTO.Root.Namespaces.Membership));
            SetCapacity(payload.MembershipExpose, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.MembershipExpose));
            SetCapacity(payload.MembershipImport, capacity, typeof(SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport));
            SetCapacity(payload.MergeNode, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.MergeNode));
            SetCapacity(payload.Metaclass, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass));
            SetCapacity(payload.MetadataAccessExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression));
            SetCapacity(payload.MetadataDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition));
            SetCapacity(payload.MetadataFeature, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature));
            SetCapacity(payload.MetadataUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage));
            SetCapacity(payload.Multiplicity, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.Multiplicity));
            SetCapacity(payload.MultiplicityRange, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange));
            SetCapacity(payload.Namespace, capacity, typeof(SysML2.NET.Core.DTO.Root.Namespaces.Namespace));
            SetCapacity(payload.NamespaceExpose, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose));
            SetCapacity(payload.NamespaceImport, capacity, typeof(SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport));
            SetCapacity(payload.NullExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression));
            SetCapacity(payload.ObjectiveMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership));
            SetCapacity(payload.OccurrenceDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition));
            SetCapacity(payload.OccurrenceUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage));
            SetCapacity(payload.OperatorExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression));
            SetCapacity(payload.OwningMembership, capacity, typeof(SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership));
            SetCapacity(payload.Package, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Packages.Package));
            SetCapacity(payload.ParameterMembership, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership));
            SetCapacity(payload.PartDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Parts.PartDefinition));
            SetCapacity(payload.PartUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Parts.PartUsage));
            SetCapacity(payload.PayloadFeature, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature));
            SetCapacity(payload.PerformActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage));
            SetCapacity(payload.PortConjugation, capacity, typeof(SysML2.NET.Core.DTO.Systems.Ports.PortConjugation));
            SetCapacity(payload.PortDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Ports.PortDefinition));
            SetCapacity(payload.PortUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Ports.PortUsage));
            SetCapacity(payload.Predicate, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Functions.Predicate));
            SetCapacity(payload.Redefinition, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.Redefinition));
            SetCapacity(payload.ReferenceSubsetting, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting));
            SetCapacity(payload.ReferenceUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage));
            SetCapacity(payload.RenderingDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition));
            SetCapacity(payload.RenderingUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.RenderingUsage));
            SetCapacity(payload.RequirementConstraintMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership));
            SetCapacity(payload.RequirementDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition));
            SetCapacity(payload.RequirementUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage));
            SetCapacity(payload.RequirementVerificationMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership));
            SetCapacity(payload.ResultExpressionMembership, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership));
            SetCapacity(payload.ReturnParameterMembership, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership));
            SetCapacity(payload.SatisfyRequirementUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage));
            SetCapacity(payload.SelectExpression, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression));
            SetCapacity(payload.SendActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage));
            SetCapacity(payload.Specialization, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.Specialization));
            SetCapacity(payload.StakeholderMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership));
            SetCapacity(payload.StateDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.States.StateDefinition));
            SetCapacity(payload.StateSubactionMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership));
            SetCapacity(payload.StateUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.States.StateUsage));
            SetCapacity(payload.Step, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Behaviors.Step));
            SetCapacity(payload.Structure, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Structures.Structure));
            SetCapacity(payload.Subclassification, capacity, typeof(SysML2.NET.Core.DTO.Core.Classifiers.Subclassification));
            SetCapacity(payload.SubjectMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership));
            SetCapacity(payload.Subsetting, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.Subsetting));
            SetCapacity(payload.Succession, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Connectors.Succession));
            SetCapacity(payload.SuccessionAsUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage));
            SetCapacity(payload.SuccessionFlow, capacity, typeof(SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow));
            SetCapacity(payload.SuccessionFlowUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage));
            SetCapacity(payload.TerminateActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage));
            SetCapacity(payload.TextualRepresentation, capacity, typeof(SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation));
            SetCapacity(payload.TransitionFeatureMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership));
            SetCapacity(payload.TransitionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.States.TransitionUsage));
            SetCapacity(payload.TriggerInvocationExpression, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression));
            SetCapacity(payload.Type, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.Type));
            SetCapacity(payload.TypeFeaturing, capacity, typeof(SysML2.NET.Core.DTO.Core.Features.TypeFeaturing));
            SetCapacity(payload.Unioning, capacity, typeof(SysML2.NET.Core.DTO.Core.Types.Unioning));
            SetCapacity(payload.Usage, capacity, typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage));
            SetCapacity(payload.UseCaseDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition));
            SetCapacity(payload.UseCaseUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage));
            SetCapacity(payload.VariantMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership));
            SetCapacity(payload.VerificationCaseDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition));
            SetCapacity(payload.VerificationCaseUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage));
            SetCapacity(payload.ViewDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.ViewDefinition));
            SetCapacity(payload.ViewpointDefinition, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition));
            SetCapacity(payload.ViewpointUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage));
            SetCapacity(payload.ViewRenderingMembership, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership));
            SetCapacity(payload.ViewUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Views.ViewUsage));
            SetCapacity(payload.WhileLoopActionUsage, capacity, typeof(SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage));

            // iterate through the dataItems and allocate to the appropriate Payload property
            foreach (var dataItem in dataItems)
            {
                switch (dataItem)
                {
                    case SysML2.NET.Core.DTO.Systems.Actions.AcceptActionUsage acceptActionUsageDto:
                        payload.AcceptActionUsage.Add(acceptActionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.ActionDefinition actionDefinitionDto:
                        payload.ActionDefinition.Add(actionDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.ActionUsage actionUsageDto:
                        payload.ActionUsage.Add(actionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.ActorMembership actorMembershipDto:
                        payload.ActorMembership.Add(actorMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Allocations.AllocationDefinition allocationDefinitionDto:
                        payload.AllocationDefinition.Add(allocationDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Allocations.AllocationUsage allocationUsageDto:
                        payload.AllocationUsage.Add(allocationUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseDefinition analysisCaseDefinitionDto:
                        payload.AnalysisCaseDefinition.Add(analysisCaseDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.AnalysisCases.AnalysisCaseUsage analysisCaseUsageDto:
                        payload.AnalysisCaseUsage.Add(analysisCaseUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Annotations.AnnotatingElement annotatingElementDto:
                        payload.AnnotatingElement.Add(annotatingElementDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Annotations.Annotation annotationDto:
                        payload.Annotation.Add(annotationDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Constraints.AssertConstraintUsage assertConstraintUsageDto:
                        payload.AssertConstraintUsage.Add(assertConstraintUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.AssignmentActionUsage assignmentActionUsageDto:
                        payload.AssignmentActionUsage.Add(assignmentActionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Associations.Association associationDto:
                        payload.Association.Add(associationDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Associations.AssociationStructure associationStructureDto:
                        payload.AssociationStructure.Add(associationStructureDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Attributes.AttributeDefinition attributeDefinitionDto:
                        payload.AttributeDefinition.Add(attributeDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Attributes.AttributeUsage attributeUsageDto:
                        payload.AttributeUsage.Add(attributeUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Behaviors.Behavior behaviorDto:
                        payload.Behavior.Add(behaviorDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Connectors.BindingConnector bindingConnectorDto:
                        payload.BindingConnector.Add(bindingConnectorDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Connections.BindingConnectorAsUsage bindingConnectorAsUsageDto:
                        payload.BindingConnectorAsUsage.Add(bindingConnectorAsUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Functions.BooleanExpression booleanExpressionDto:
                        payload.BooleanExpression.Add(booleanExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Calculations.CalculationDefinition calculationDefinitionDto:
                        payload.CalculationDefinition.Add(calculationDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Calculations.CalculationUsage calculationUsageDto:
                        payload.CalculationUsage.Add(calculationUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Cases.CaseDefinition caseDefinitionDto:
                        payload.CaseDefinition.Add(caseDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Cases.CaseUsage caseUsageDto:
                        payload.CaseUsage.Add(caseUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Classes.Class classDto:
                        payload.Class.Add(classDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Classifiers.Classifier classifierDto:
                        payload.Classifier.Add(classifierDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.CollectExpression collectExpressionDto:
                        payload.CollectExpression.Add(collectExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Annotations.Comment commentDto:
                        payload.Comment.Add(commentDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.ConcernDefinition concernDefinitionDto:
                        payload.ConcernDefinition.Add(concernDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.ConcernUsage concernUsageDto:
                        payload.ConcernUsage.Add(concernUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortDefinition conjugatedPortDefinitionDto:
                        payload.ConjugatedPortDefinition.Add(conjugatedPortDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Ports.ConjugatedPortTyping conjugatedPortTypingDto:
                        payload.ConjugatedPortTyping.Add(conjugatedPortTypingDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.Conjugation conjugationDto:
                        payload.Conjugation.Add(conjugationDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Connections.ConnectionDefinition connectionDefinitionDto:
                        payload.ConnectionDefinition.Add(connectionDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Connections.ConnectionUsage connectionUsageDto:
                        payload.ConnectionUsage.Add(connectionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Connectors.Connector connectorDto:
                        payload.Connector.Add(connectorDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Constraints.ConstraintDefinition constraintDefinitionDto:
                        payload.ConstraintDefinition.Add(constraintDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Constraints.ConstraintUsage constraintUsageDto:
                        payload.ConstraintUsage.Add(constraintUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.ConstructorExpression constructorExpressionDto:
                        payload.ConstructorExpression.Add(constructorExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.CrossSubsetting crossSubsettingDto:
                        payload.CrossSubsetting.Add(crossSubsettingDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.DataTypes.DataType dataTypeDto:
                        payload.DataType.Add(dataTypeDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.DecisionNode decisionNodeDto:
                        payload.DecisionNode.Add(decisionNodeDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Definition definitionDto:
                        payload.Definition.Add(definitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Dependencies.Dependency dependencyDto:
                        payload.Dependency.Add(dependencyDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.Differencing differencingDto:
                        payload.Differencing.Add(differencingDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.Disjoining disjoiningDto:
                        payload.Disjoining.Add(disjoiningDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Annotations.Documentation documentationDto:
                        payload.Documentation.Add(documentationDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Packages.ElementFilterMembership elementFilterMembershipDto:
                        payload.ElementFilterMembership.Add(elementFilterMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.EndFeatureMembership endFeatureMembershipDto:
                        payload.EndFeatureMembership.Add(endFeatureMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationDefinition enumerationDefinitionDto:
                        payload.EnumerationDefinition.Add(enumerationDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Enumerations.EnumerationUsage enumerationUsageDto:
                        payload.EnumerationUsage.Add(enumerationUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Occurrences.EventOccurrenceUsage eventOccurrenceUsageDto:
                        payload.EventOccurrenceUsage.Add(eventOccurrenceUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.States.ExhibitStateUsage exhibitStateUsageDto:
                        payload.ExhibitStateUsage.Add(exhibitStateUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Functions.Expression expressionDto:
                        payload.Expression.Add(expressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.Feature featureDto:
                        payload.Feature.Add(featureDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.FeatureChainExpression featureChainExpressionDto:
                        payload.FeatureChainExpression.Add(featureChainExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.FeatureChaining featureChainingDto:
                        payload.FeatureChaining.Add(featureChainingDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.FeatureInverting featureInvertingDto:
                        payload.FeatureInverting.Add(featureInvertingDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.FeatureMembership featureMembershipDto:
                        payload.FeatureMembership.Add(featureMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.FeatureReferenceExpression featureReferenceExpressionDto:
                        payload.FeatureReferenceExpression.Add(featureReferenceExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.FeatureTyping featureTypingDto:
                        payload.FeatureTyping.Add(featureTypingDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.FeatureValues.FeatureValue featureValueDto:
                        payload.FeatureValue.Add(featureValueDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Interactions.Flow flowDto:
                        payload.Flow.Add(flowDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Flows.FlowDefinition flowDefinitionDto:
                        payload.FlowDefinition.Add(flowDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Interactions.FlowEnd flowEndDto:
                        payload.FlowEnd.Add(flowEndDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Flows.FlowUsage flowUsageDto:
                        payload.FlowUsage.Add(flowUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.ForkNode forkNodeDto:
                        payload.ForkNode.Add(forkNodeDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.ForLoopActionUsage forLoopActionUsageDto:
                        payload.ForLoopActionUsage.Add(forLoopActionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.FramedConcernMembership framedConcernMembershipDto:
                        payload.FramedConcernMembership.Add(framedConcernMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Functions.Function functionDto:
                        payload.Function.Add(functionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.IfActionUsage ifActionUsageDto:
                        payload.IfActionUsage.Add(ifActionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.UseCases.IncludeUseCaseUsage includeUseCaseUsageDto:
                        payload.IncludeUseCaseUsage.Add(includeUseCaseUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.IndexExpression indexExpressionDto:
                        payload.IndexExpression.Add(indexExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Interactions.Interaction interactionDto:
                        payload.Interaction.Add(interactionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceDefinition interfaceDefinitionDto:
                        payload.InterfaceDefinition.Add(interfaceDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Interfaces.InterfaceUsage interfaceUsageDto:
                        payload.InterfaceUsage.Add(interfaceUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.Intersecting intersectingDto:
                        payload.Intersecting.Add(intersectingDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Functions.Invariant invariantDto:
                        payload.Invariant.Add(invariantDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.InvocationExpression invocationExpressionDto:
                        payload.InvocationExpression.Add(invocationExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Items.ItemDefinition itemDefinitionDto:
                        payload.ItemDefinition.Add(itemDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Items.ItemUsage itemUsageDto:
                        payload.ItemUsage.Add(itemUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.JoinNode joinNodeDto:
                        payload.JoinNode.Add(joinNodeDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Packages.LibraryPackage libraryPackageDto:
                        payload.LibraryPackage.Add(libraryPackageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.LiteralBoolean literalBooleanDto:
                        payload.LiteralBoolean.Add(literalBooleanDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.LiteralExpression literalExpressionDto:
                        payload.LiteralExpression.Add(literalExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInfinity literalInfinityDto:
                        payload.LiteralInfinity.Add(literalInfinityDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.LiteralInteger literalIntegerDto:
                        payload.LiteralInteger.Add(literalIntegerDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.LiteralRational literalRationalDto:
                        payload.LiteralRational.Add(literalRationalDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.LiteralString literalStringDto:
                        payload.LiteralString.Add(literalStringDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Namespaces.Membership membershipDto:
                        payload.Membership.Add(membershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.MembershipExpose membershipExposeDto:
                        payload.MembershipExpose.Add(membershipExposeDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Namespaces.MembershipImport membershipImportDto:
                        payload.MembershipImport.Add(membershipImportDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.MergeNode mergeNodeDto:
                        payload.MergeNode.Add(mergeNodeDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Metadata.Metaclass metaclassDto:
                        payload.Metaclass.Add(metaclassDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.MetadataAccessExpression metadataAccessExpressionDto:
                        payload.MetadataAccessExpression.Add(metadataAccessExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Metadata.MetadataDefinition metadataDefinitionDto:
                        payload.MetadataDefinition.Add(metadataDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature metadataFeatureDto:
                        payload.MetadataFeature.Add(metadataFeatureDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Metadata.MetadataUsage metadataUsageDto:
                        payload.MetadataUsage.Add(metadataUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.Multiplicity multiplicityDto:
                        payload.Multiplicity.Add(multiplicityDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Multiplicities.MultiplicityRange multiplicityRangeDto:
                        payload.MultiplicityRange.Add(multiplicityRangeDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Namespaces.Namespace namespaceDto:
                        payload.Namespace.Add(namespaceDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.NamespaceExpose namespaceExposeDto:
                        payload.NamespaceExpose.Add(namespaceExposeDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Namespaces.NamespaceImport namespaceImportDto:
                        payload.NamespaceImport.Add(namespaceImportDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.NullExpression nullExpressionDto:
                        payload.NullExpression.Add(nullExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Cases.ObjectiveMembership objectiveMembershipDto:
                        payload.ObjectiveMembership.Add(objectiveMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceDefinition occurrenceDefinitionDto:
                        payload.OccurrenceDefinition.Add(occurrenceDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Occurrences.OccurrenceUsage occurrenceUsageDto:
                        payload.OccurrenceUsage.Add(occurrenceUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.OperatorExpression operatorExpressionDto:
                        payload.OperatorExpression.Add(operatorExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Namespaces.OwningMembership owningMembershipDto:
                        payload.OwningMembership.Add(owningMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Packages.Package packageDto:
                        payload.Package.Add(packageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Behaviors.ParameterMembership parameterMembershipDto:
                        payload.ParameterMembership.Add(parameterMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Parts.PartDefinition partDefinitionDto:
                        payload.PartDefinition.Add(partDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Parts.PartUsage partUsageDto:
                        payload.PartUsage.Add(partUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Interactions.PayloadFeature payloadFeatureDto:
                        payload.PayloadFeature.Add(payloadFeatureDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.PerformActionUsage performActionUsageDto:
                        payload.PerformActionUsage.Add(performActionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Ports.PortConjugation portConjugationDto:
                        payload.PortConjugation.Add(portConjugationDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Ports.PortDefinition portDefinitionDto:
                        payload.PortDefinition.Add(portDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Ports.PortUsage portUsageDto:
                        payload.PortUsage.Add(portUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Functions.Predicate predicateDto:
                        payload.Predicate.Add(predicateDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.Redefinition redefinitionDto:
                        payload.Redefinition.Add(redefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting referenceSubsettingDto:
                        payload.ReferenceSubsetting.Add(referenceSubsettingDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.ReferenceUsage referenceUsageDto:
                        payload.ReferenceUsage.Add(referenceUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.RenderingDefinition renderingDefinitionDto:
                        payload.RenderingDefinition.Add(renderingDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.RenderingUsage renderingUsageDto:
                        payload.RenderingUsage.Add(renderingUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.RequirementConstraintMembership requirementConstraintMembershipDto:
                        payload.RequirementConstraintMembership.Add(requirementConstraintMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.RequirementDefinition requirementDefinitionDto:
                        payload.RequirementDefinition.Add(requirementDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.RequirementUsage requirementUsageDto:
                        payload.RequirementUsage.Add(requirementUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.VerificationCases.RequirementVerificationMembership requirementVerificationMembershipDto:
                        payload.RequirementVerificationMembership.Add(requirementVerificationMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Functions.ResultExpressionMembership resultExpressionMembershipDto:
                        payload.ResultExpressionMembership.Add(resultExpressionMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Functions.ReturnParameterMembership returnParameterMembershipDto:
                        payload.ReturnParameterMembership.Add(returnParameterMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.SatisfyRequirementUsage satisfyRequirementUsageDto:
                        payload.SatisfyRequirementUsage.Add(satisfyRequirementUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Expressions.SelectExpression selectExpressionDto:
                        payload.SelectExpression.Add(selectExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.SendActionUsage sendActionUsageDto:
                        payload.SendActionUsage.Add(sendActionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.Specialization specializationDto:
                        payload.Specialization.Add(specializationDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.StakeholderMembership stakeholderMembershipDto:
                        payload.StakeholderMembership.Add(stakeholderMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.States.StateDefinition stateDefinitionDto:
                        payload.StateDefinition.Add(stateDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.States.StateSubactionMembership stateSubactionMembershipDto:
                        payload.StateSubactionMembership.Add(stateSubactionMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.States.StateUsage stateUsageDto:
                        payload.StateUsage.Add(stateUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Behaviors.Step stepDto:
                        payload.Step.Add(stepDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Structures.Structure structureDto:
                        payload.Structure.Add(structureDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Classifiers.Subclassification subclassificationDto:
                        payload.Subclassification.Add(subclassificationDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Requirements.SubjectMembership subjectMembershipDto:
                        payload.SubjectMembership.Add(subjectMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.Subsetting subsettingDto:
                        payload.Subsetting.Add(subsettingDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Connectors.Succession successionDto:
                        payload.Succession.Add(successionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Connections.SuccessionAsUsage successionAsUsageDto:
                        payload.SuccessionAsUsage.Add(successionAsUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Kernel.Interactions.SuccessionFlow successionFlowDto:
                        payload.SuccessionFlow.Add(successionFlowDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Flows.SuccessionFlowUsage successionFlowUsageDto:
                        payload.SuccessionFlowUsage.Add(successionFlowUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.TerminateActionUsage terminateActionUsageDto:
                        payload.TerminateActionUsage.Add(terminateActionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation textualRepresentationDto:
                        payload.TextualRepresentation.Add(textualRepresentationDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership transitionFeatureMembershipDto:
                        payload.TransitionFeatureMembership.Add(transitionFeatureMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.States.TransitionUsage transitionUsageDto:
                        payload.TransitionUsage.Add(transitionUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.TriggerInvocationExpression triggerInvocationExpressionDto:
                        payload.TriggerInvocationExpression.Add(triggerInvocationExpressionDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.Type typeDto:
                        payload.Type.Add(typeDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Features.TypeFeaturing typeFeaturingDto:
                        payload.TypeFeaturing.Add(typeFeaturingDto);
                        break;
                    case SysML2.NET.Core.DTO.Core.Types.Unioning unioningDto:
                        payload.Unioning.Add(unioningDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.Usage usageDto:
                        payload.Usage.Add(usageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.UseCases.UseCaseDefinition useCaseDefinitionDto:
                        payload.UseCaseDefinition.Add(useCaseDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.UseCases.UseCaseUsage useCaseUsageDto:
                        payload.UseCaseUsage.Add(useCaseUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.DefinitionAndUsage.VariantMembership variantMembershipDto:
                        payload.VariantMembership.Add(variantMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseDefinition verificationCaseDefinitionDto:
                        payload.VerificationCaseDefinition.Add(verificationCaseDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.VerificationCases.VerificationCaseUsage verificationCaseUsageDto:
                        payload.VerificationCaseUsage.Add(verificationCaseUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.ViewDefinition viewDefinitionDto:
                        payload.ViewDefinition.Add(viewDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.ViewpointDefinition viewpointDefinitionDto:
                        payload.ViewpointDefinition.Add(viewpointDefinitionDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.ViewpointUsage viewpointUsageDto:
                        payload.ViewpointUsage.Add(viewpointUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.ViewRenderingMembership viewRenderingMembershipDto:
                        payload.ViewRenderingMembership.Add(viewRenderingMembershipDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Views.ViewUsage viewUsageDto:
                        payload.ViewUsage.Add(viewUsageDto);
                        break;
                    case SysML2.NET.Core.DTO.Systems.Actions.WhileLoopActionUsage whileLoopActionUsageDto:
                        payload.WhileLoopActionUsage.Add(whileLoopActionUsageDto);
                        break;
                }
            }

            return payload;
        }

        /// <summary>
        /// Creates an <see cref="IEnumerable{IData}"/> from the provided <see cref="Payload"/>.
        /// </summary>
        /// <param name="payload">
        /// The <see cref="Payload"/> that carries the <see cref="IData"/>s.
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>.
        /// </returns>
        internal static IEnumerable<IData> ToDataItems(this Payload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var capacity = 0
                + payload.AcceptActionUsage.Count
                + payload.ActionDefinition.Count
                + payload.ActionUsage.Count
                + payload.ActorMembership.Count
                + payload.AllocationDefinition.Count
                + payload.AllocationUsage.Count
                + payload.AnalysisCaseDefinition.Count
                + payload.AnalysisCaseUsage.Count
                + payload.AnnotatingElement.Count
                + payload.Annotation.Count
                + payload.AssertConstraintUsage.Count
                + payload.AssignmentActionUsage.Count
                + payload.Association.Count
                + payload.AssociationStructure.Count
                + payload.AttributeDefinition.Count
                + payload.AttributeUsage.Count
                + payload.Behavior.Count
                + payload.BindingConnector.Count
                + payload.BindingConnectorAsUsage.Count
                + payload.BooleanExpression.Count
                + payload.CalculationDefinition.Count
                + payload.CalculationUsage.Count
                + payload.CaseDefinition.Count
                + payload.CaseUsage.Count
                + payload.Class.Count
                + payload.Classifier.Count
                + payload.CollectExpression.Count
                + payload.Comment.Count
                + payload.ConcernDefinition.Count
                + payload.ConcernUsage.Count
                + payload.ConjugatedPortDefinition.Count
                + payload.ConjugatedPortTyping.Count
                + payload.Conjugation.Count
                + payload.ConnectionDefinition.Count
                + payload.ConnectionUsage.Count
                + payload.Connector.Count
                + payload.ConstraintDefinition.Count
                + payload.ConstraintUsage.Count
                + payload.ConstructorExpression.Count
                + payload.CrossSubsetting.Count
                + payload.DataType.Count
                + payload.DecisionNode.Count
                + payload.Definition.Count
                + payload.Dependency.Count
                + payload.Differencing.Count
                + payload.Disjoining.Count
                + payload.Documentation.Count
                + payload.ElementFilterMembership.Count
                + payload.EndFeatureMembership.Count
                + payload.EnumerationDefinition.Count
                + payload.EnumerationUsage.Count
                + payload.EventOccurrenceUsage.Count
                + payload.ExhibitStateUsage.Count
                + payload.Expression.Count
                + payload.Feature.Count
                + payload.FeatureChainExpression.Count
                + payload.FeatureChaining.Count
                + payload.FeatureInverting.Count
                + payload.FeatureMembership.Count
                + payload.FeatureReferenceExpression.Count
                + payload.FeatureTyping.Count
                + payload.FeatureValue.Count
                + payload.Flow.Count
                + payload.FlowDefinition.Count
                + payload.FlowEnd.Count
                + payload.FlowUsage.Count
                + payload.ForkNode.Count
                + payload.ForLoopActionUsage.Count
                + payload.FramedConcernMembership.Count
                + payload.Function.Count
                + payload.IfActionUsage.Count
                + payload.IncludeUseCaseUsage.Count
                + payload.IndexExpression.Count
                + payload.Interaction.Count
                + payload.InterfaceDefinition.Count
                + payload.InterfaceUsage.Count
                + payload.Intersecting.Count
                + payload.Invariant.Count
                + payload.InvocationExpression.Count
                + payload.ItemDefinition.Count
                + payload.ItemUsage.Count
                + payload.JoinNode.Count
                + payload.LibraryPackage.Count
                + payload.LiteralBoolean.Count
                + payload.LiteralExpression.Count
                + payload.LiteralInfinity.Count
                + payload.LiteralInteger.Count
                + payload.LiteralRational.Count
                + payload.LiteralString.Count
                + payload.Membership.Count
                + payload.MembershipExpose.Count
                + payload.MembershipImport.Count
                + payload.MergeNode.Count
                + payload.Metaclass.Count
                + payload.MetadataAccessExpression.Count
                + payload.MetadataDefinition.Count
                + payload.MetadataFeature.Count
                + payload.MetadataUsage.Count
                + payload.Multiplicity.Count
                + payload.MultiplicityRange.Count
                + payload.Namespace.Count
                + payload.NamespaceExpose.Count
                + payload.NamespaceImport.Count
                + payload.NullExpression.Count
                + payload.ObjectiveMembership.Count
                + payload.OccurrenceDefinition.Count
                + payload.OccurrenceUsage.Count
                + payload.OperatorExpression.Count
                + payload.OwningMembership.Count
                + payload.Package.Count
                + payload.ParameterMembership.Count
                + payload.PartDefinition.Count
                + payload.PartUsage.Count
                + payload.PayloadFeature.Count
                + payload.PerformActionUsage.Count
                + payload.PortConjugation.Count
                + payload.PortDefinition.Count
                + payload.PortUsage.Count
                + payload.Predicate.Count
                + payload.Redefinition.Count
                + payload.ReferenceSubsetting.Count
                + payload.ReferenceUsage.Count
                + payload.RenderingDefinition.Count
                + payload.RenderingUsage.Count
                + payload.RequirementConstraintMembership.Count
                + payload.RequirementDefinition.Count
                + payload.RequirementUsage.Count
                + payload.RequirementVerificationMembership.Count
                + payload.ResultExpressionMembership.Count
                + payload.ReturnParameterMembership.Count
                + payload.SatisfyRequirementUsage.Count
                + payload.SelectExpression.Count
                + payload.SendActionUsage.Count
                + payload.Specialization.Count
                + payload.StakeholderMembership.Count
                + payload.StateDefinition.Count
                + payload.StateSubactionMembership.Count
                + payload.StateUsage.Count
                + payload.Step.Count
                + payload.Structure.Count
                + payload.Subclassification.Count
                + payload.SubjectMembership.Count
                + payload.Subsetting.Count
                + payload.Succession.Count
                + payload.SuccessionAsUsage.Count
                + payload.SuccessionFlow.Count
                + payload.SuccessionFlowUsage.Count
                + payload.TerminateActionUsage.Count
                + payload.TextualRepresentation.Count
                + payload.TransitionFeatureMembership.Count
                + payload.TransitionUsage.Count
                + payload.TriggerInvocationExpression.Count
                + payload.Type.Count
                + payload.TypeFeaturing.Count
                + payload.Unioning.Count
                + payload.Usage.Count
                + payload.UseCaseDefinition.Count
                + payload.UseCaseUsage.Count
                + payload.VariantMembership.Count
                + payload.VerificationCaseDefinition.Count
                + payload.VerificationCaseUsage.Count
                + payload.ViewDefinition.Count
                + payload.ViewpointDefinition.Count
                + payload.ViewpointUsage.Count
                + payload.ViewRenderingMembership.Count
                + payload.ViewUsage.Count
                + payload.WhileLoopActionUsage.Count
            ;

            var result = new List<IData>(capacity);

            AddRange(result, payload.AcceptActionUsage);
            AddRange(result, payload.ActionDefinition);
            AddRange(result, payload.ActionUsage);
            AddRange(result, payload.ActorMembership);
            AddRange(result, payload.AllocationDefinition);
            AddRange(result, payload.AllocationUsage);
            AddRange(result, payload.AnalysisCaseDefinition);
            AddRange(result, payload.AnalysisCaseUsage);
            AddRange(result, payload.AnnotatingElement);
            AddRange(result, payload.Annotation);
            AddRange(result, payload.AssertConstraintUsage);
            AddRange(result, payload.AssignmentActionUsage);
            AddRange(result, payload.Association);
            AddRange(result, payload.AssociationStructure);
            AddRange(result, payload.AttributeDefinition);
            AddRange(result, payload.AttributeUsage);
            AddRange(result, payload.Behavior);
            AddRange(result, payload.BindingConnector);
            AddRange(result, payload.BindingConnectorAsUsage);
            AddRange(result, payload.BooleanExpression);
            AddRange(result, payload.CalculationDefinition);
            AddRange(result, payload.CalculationUsage);
            AddRange(result, payload.CaseDefinition);
            AddRange(result, payload.CaseUsage);
            AddRange(result, payload.Class);
            AddRange(result, payload.Classifier);
            AddRange(result, payload.CollectExpression);
            AddRange(result, payload.Comment);
            AddRange(result, payload.ConcernDefinition);
            AddRange(result, payload.ConcernUsage);
            AddRange(result, payload.ConjugatedPortDefinition);
            AddRange(result, payload.ConjugatedPortTyping);
            AddRange(result, payload.Conjugation);
            AddRange(result, payload.ConnectionDefinition);
            AddRange(result, payload.ConnectionUsage);
            AddRange(result, payload.Connector);
            AddRange(result, payload.ConstraintDefinition);
            AddRange(result, payload.ConstraintUsage);
            AddRange(result, payload.ConstructorExpression);
            AddRange(result, payload.CrossSubsetting);
            AddRange(result, payload.DataType);
            AddRange(result, payload.DecisionNode);
            AddRange(result, payload.Definition);
            AddRange(result, payload.Dependency);
            AddRange(result, payload.Differencing);
            AddRange(result, payload.Disjoining);
            AddRange(result, payload.Documentation);
            AddRange(result, payload.ElementFilterMembership);
            AddRange(result, payload.EndFeatureMembership);
            AddRange(result, payload.EnumerationDefinition);
            AddRange(result, payload.EnumerationUsage);
            AddRange(result, payload.EventOccurrenceUsage);
            AddRange(result, payload.ExhibitStateUsage);
            AddRange(result, payload.Expression);
            AddRange(result, payload.Feature);
            AddRange(result, payload.FeatureChainExpression);
            AddRange(result, payload.FeatureChaining);
            AddRange(result, payload.FeatureInverting);
            AddRange(result, payload.FeatureMembership);
            AddRange(result, payload.FeatureReferenceExpression);
            AddRange(result, payload.FeatureTyping);
            AddRange(result, payload.FeatureValue);
            AddRange(result, payload.Flow);
            AddRange(result, payload.FlowDefinition);
            AddRange(result, payload.FlowEnd);
            AddRange(result, payload.FlowUsage);
            AddRange(result, payload.ForkNode);
            AddRange(result, payload.ForLoopActionUsage);
            AddRange(result, payload.FramedConcernMembership);
            AddRange(result, payload.Function);
            AddRange(result, payload.IfActionUsage);
            AddRange(result, payload.IncludeUseCaseUsage);
            AddRange(result, payload.IndexExpression);
            AddRange(result, payload.Interaction);
            AddRange(result, payload.InterfaceDefinition);
            AddRange(result, payload.InterfaceUsage);
            AddRange(result, payload.Intersecting);
            AddRange(result, payload.Invariant);
            AddRange(result, payload.InvocationExpression);
            AddRange(result, payload.ItemDefinition);
            AddRange(result, payload.ItemUsage);
            AddRange(result, payload.JoinNode);
            AddRange(result, payload.LibraryPackage);
            AddRange(result, payload.LiteralBoolean);
            AddRange(result, payload.LiteralExpression);
            AddRange(result, payload.LiteralInfinity);
            AddRange(result, payload.LiteralInteger);
            AddRange(result, payload.LiteralRational);
            AddRange(result, payload.LiteralString);
            AddRange(result, payload.Membership);
            AddRange(result, payload.MembershipExpose);
            AddRange(result, payload.MembershipImport);
            AddRange(result, payload.MergeNode);
            AddRange(result, payload.Metaclass);
            AddRange(result, payload.MetadataAccessExpression);
            AddRange(result, payload.MetadataDefinition);
            AddRange(result, payload.MetadataFeature);
            AddRange(result, payload.MetadataUsage);
            AddRange(result, payload.Multiplicity);
            AddRange(result, payload.MultiplicityRange);
            AddRange(result, payload.Namespace);
            AddRange(result, payload.NamespaceExpose);
            AddRange(result, payload.NamespaceImport);
            AddRange(result, payload.NullExpression);
            AddRange(result, payload.ObjectiveMembership);
            AddRange(result, payload.OccurrenceDefinition);
            AddRange(result, payload.OccurrenceUsage);
            AddRange(result, payload.OperatorExpression);
            AddRange(result, payload.OwningMembership);
            AddRange(result, payload.Package);
            AddRange(result, payload.ParameterMembership);
            AddRange(result, payload.PartDefinition);
            AddRange(result, payload.PartUsage);
            AddRange(result, payload.PayloadFeature);
            AddRange(result, payload.PerformActionUsage);
            AddRange(result, payload.PortConjugation);
            AddRange(result, payload.PortDefinition);
            AddRange(result, payload.PortUsage);
            AddRange(result, payload.Predicate);
            AddRange(result, payload.Redefinition);
            AddRange(result, payload.ReferenceSubsetting);
            AddRange(result, payload.ReferenceUsage);
            AddRange(result, payload.RenderingDefinition);
            AddRange(result, payload.RenderingUsage);
            AddRange(result, payload.RequirementConstraintMembership);
            AddRange(result, payload.RequirementDefinition);
            AddRange(result, payload.RequirementUsage);
            AddRange(result, payload.RequirementVerificationMembership);
            AddRange(result, payload.ResultExpressionMembership);
            AddRange(result, payload.ReturnParameterMembership);
            AddRange(result, payload.SatisfyRequirementUsage);
            AddRange(result, payload.SelectExpression);
            AddRange(result, payload.SendActionUsage);
            AddRange(result, payload.Specialization);
            AddRange(result, payload.StakeholderMembership);
            AddRange(result, payload.StateDefinition);
            AddRange(result, payload.StateSubactionMembership);
            AddRange(result, payload.StateUsage);
            AddRange(result, payload.Step);
            AddRange(result, payload.Structure);
            AddRange(result, payload.Subclassification);
            AddRange(result, payload.SubjectMembership);
            AddRange(result, payload.Subsetting);
            AddRange(result, payload.Succession);
            AddRange(result, payload.SuccessionAsUsage);
            AddRange(result, payload.SuccessionFlow);
            AddRange(result, payload.SuccessionFlowUsage);
            AddRange(result, payload.TerminateActionUsage);
            AddRange(result, payload.TextualRepresentation);
            AddRange(result, payload.TransitionFeatureMembership);
            AddRange(result, payload.TransitionUsage);
            AddRange(result, payload.TriggerInvocationExpression);
            AddRange(result, payload.Type);
            AddRange(result, payload.TypeFeaturing);
            AddRange(result, payload.Unioning);
            AddRange(result, payload.Usage);
            AddRange(result, payload.UseCaseDefinition);
            AddRange(result, payload.UseCaseUsage);
            AddRange(result, payload.VariantMembership);
            AddRange(result, payload.VerificationCaseDefinition);
            AddRange(result, payload.VerificationCaseUsage);
            AddRange(result, payload.ViewDefinition);
            AddRange(result, payload.ViewpointDefinition);
            AddRange(result, payload.ViewpointUsage);
            AddRange(result, payload.ViewRenderingMembership);
            AddRange(result, payload.ViewUsage);
            AddRange(result, payload.WhileLoopActionUsage);

            return result;
        }

        /// <summary>
        /// Adds all elements from the specified source list to the target list
        /// without incurring additional allocations or intermediate collections.
        /// </summary>
        /// <typeparam name="T">
        /// The concrete data type of the elements being added. The type must
        /// implement <see cref="IData"/>.
        /// </typeparam>
        /// <param name="target">
        /// The destination list that receives the elements.
        /// </param>
        /// <param name="source">
        /// The source list whose elements are appended to the <paramref name="target"/> list.
        /// </param>
        /// <remarks>
        /// This method is intentionally implemented using a <c>foreach</c> loop over
        /// <see cref="List{T}"/> rather than calling <see cref="List{T}.AddRange(System.Collections.Generic.IEnumerable{T})"/>
        /// in order to:
        /// <list type="bullet">
        /// <item>
        /// Avoid unnecessary interface-based enumeration when aggregating heterogeneous
        /// collections into a <see cref="List{IData}"/>.
        /// </item>
        /// <item>
        /// Benefit from the struct-based enumerator used by <see cref="List{T}"/>,
        /// which minimizes overhead in tight loops.
        /// </item>
        /// <item>
        /// Centralize the <see cref="IData"/> constraint and casting logic in a single,
        /// well-defined location.
        /// </item>
        /// </list>
        /// This method is intended for high-throughput aggregation of DTO payloads
        /// where predictable performance and minimal allocations are required.
        /// </remarks>
        private static void AddRange<T>(List<IData> target, List<T> source) where T : IData
        {
            foreach (var item in source)
            {
                target.Add(item);
            }
        }

        /// <summary>
        /// Ensures that the specified list has sufficient capacity to hold all
        /// elements of the given runtime type without triggering internal resizing.
        /// </summary>
        /// <typeparam name="T">
        /// The element type of the <paramref name="list"/>.
        /// </typeparam>
        /// <param name="list">
        /// The list whose <see cref="List{T}.Capacity"/> is adjusted, if required.
        /// </param>
        /// <param name="counts">
        /// A lookup table mapping concrete runtime types to the number of elements
        /// of that type present in the source collection.
        /// </param>
        /// <param name="key">
        /// The concrete runtime <see cref="Type"/> corresponding to the elements
        /// stored in the <paramref name="list"/>.
        /// </param>
        /// <remarks>
        /// This method is used during payload construction to pre-size collections
        /// based on a prior counting pass. Pre-sizing avoids repeated internal
        /// reallocations and array copying that would otherwise occur when elements
        /// are added incrementally.
        /// <para/>
        /// The capacity is only increased when the required size exceeds the current
        /// capacity; existing capacity is never reduced.
        /// </remarks>
        private static void SetCapacity<T>(List<T> list, Dictionary<Type, int> counts, Type key)
        {
            if (counts.TryGetValue(key, out var required) && required > list.Capacity)
            {
                list.Capacity = required;
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
