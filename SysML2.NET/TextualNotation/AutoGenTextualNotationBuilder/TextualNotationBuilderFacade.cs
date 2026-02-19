// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationBuilderFacade.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="TextualNotationBuilderFacade"/> provides access to built textual notation for <see cref="IElement" /> via <see cref="TextualNotationBuilder{TElement}" />
    /// </summary>
    public class TextualNotationBuilderFacade : ITextualNotationBuilderFacade
    {
        /// <summary>
        /// Queries the Textual Notation of an <see cref="IElement"/>
        /// </summary>
        /// <param name="element">The <see cref="IElement"/> to built textual notation from</param>
        /// <returns>The built textual notation string</returns>
        public string QueryTextualNotationOfElement(IElement element)
        {
            switch (element)
            {
                case SysML2.NET.Core.POCO.Systems.Ports.PortUsage pocoPortUsage:
                    var portUsageTextualNotationBuilder = new PortUsageTextualNotationBuilder(this);
                    return portUsageTextualNotationBuilder.BuildTextualNotation(pocoPortUsage);

                case SysML2.NET.Core.POCO.Systems.Ports.PortDefinition pocoPortDefinition:
                    var portDefinitionTextualNotationBuilder = new PortDefinitionTextualNotationBuilder(this);
                    return portDefinitionTextualNotationBuilder.BuildTextualNotation(pocoPortDefinition);

                case SysML2.NET.Core.POCO.Systems.Ports.ConjugatedPortDefinition pocoConjugatedPortDefinition:
                    var conjugatedPortDefinitionTextualNotationBuilder = new ConjugatedPortDefinitionTextualNotationBuilder(this);
                    return conjugatedPortDefinitionTextualNotationBuilder.BuildTextualNotation(pocoConjugatedPortDefinition);

                case SysML2.NET.Core.POCO.Systems.Ports.PortConjugation pocoPortConjugation:
                    var portConjugationTextualNotationBuilder = new PortConjugationTextualNotationBuilder(this);
                    return portConjugationTextualNotationBuilder.BuildTextualNotation(pocoPortConjugation);

                case SysML2.NET.Core.POCO.Systems.Ports.ConjugatedPortTyping pocoConjugatedPortTyping:
                    var conjugatedPortTypingTextualNotationBuilder = new ConjugatedPortTypingTextualNotationBuilder(this);
                    return conjugatedPortTypingTextualNotationBuilder.BuildTextualNotation(pocoConjugatedPortTyping);

                case SysML2.NET.Core.POCO.Systems.Attributes.AttributeDefinition pocoAttributeDefinition:
                    var attributeDefinitionTextualNotationBuilder = new AttributeDefinitionTextualNotationBuilder(this);
                    return attributeDefinitionTextualNotationBuilder.BuildTextualNotation(pocoAttributeDefinition);

                case SysML2.NET.Core.POCO.Systems.Attributes.AttributeUsage pocoAttributeUsage:
                    var attributeUsageTextualNotationBuilder = new AttributeUsageTextualNotationBuilder(this);
                    return attributeUsageTextualNotationBuilder.BuildTextualNotation(pocoAttributeUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.AcceptActionUsage pocoAcceptActionUsage:
                    var acceptActionUsageTextualNotationBuilder = new AcceptActionUsageTextualNotationBuilder(this);
                    return acceptActionUsageTextualNotationBuilder.BuildTextualNotation(pocoAcceptActionUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.SendActionUsage pocoSendActionUsage:
                    var sendActionUsageTextualNotationBuilder = new SendActionUsageTextualNotationBuilder(this);
                    return sendActionUsageTextualNotationBuilder.BuildTextualNotation(pocoSendActionUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.PerformActionUsage pocoPerformActionUsage:
                    var performActionUsageTextualNotationBuilder = new PerformActionUsageTextualNotationBuilder(this);
                    return performActionUsageTextualNotationBuilder.BuildTextualNotation(pocoPerformActionUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.ForkNode pocoForkNode:
                    var forkNodeTextualNotationBuilder = new ForkNodeTextualNotationBuilder(this);
                    return forkNodeTextualNotationBuilder.BuildTextualNotation(pocoForkNode);

                case SysML2.NET.Core.POCO.Systems.Actions.JoinNode pocoJoinNode:
                    var joinNodeTextualNotationBuilder = new JoinNodeTextualNotationBuilder(this);
                    return joinNodeTextualNotationBuilder.BuildTextualNotation(pocoJoinNode);

                case SysML2.NET.Core.POCO.Systems.Actions.ActionUsage pocoActionUsage:
                    var actionUsageTextualNotationBuilder = new ActionUsageTextualNotationBuilder(this);
                    return actionUsageTextualNotationBuilder.BuildTextualNotation(pocoActionUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.DecisionNode pocoDecisionNode:
                    var decisionNodeTextualNotationBuilder = new DecisionNodeTextualNotationBuilder(this);
                    return decisionNodeTextualNotationBuilder.BuildTextualNotation(pocoDecisionNode);

                case SysML2.NET.Core.POCO.Systems.Actions.MergeNode pocoMergeNode:
                    var mergeNodeTextualNotationBuilder = new MergeNodeTextualNotationBuilder(this);
                    return mergeNodeTextualNotationBuilder.BuildTextualNotation(pocoMergeNode);

                case SysML2.NET.Core.POCO.Systems.Actions.ActionDefinition pocoActionDefinition:
                    var actionDefinitionTextualNotationBuilder = new ActionDefinitionTextualNotationBuilder(this);
                    return actionDefinitionTextualNotationBuilder.BuildTextualNotation(pocoActionDefinition);

                case SysML2.NET.Core.POCO.Systems.Actions.IfActionUsage pocoIfActionUsage:
                    var ifActionUsageTextualNotationBuilder = new IfActionUsageTextualNotationBuilder(this);
                    return ifActionUsageTextualNotationBuilder.BuildTextualNotation(pocoIfActionUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.ForLoopActionUsage pocoForLoopActionUsage:
                    var forLoopActionUsageTextualNotationBuilder = new ForLoopActionUsageTextualNotationBuilder(this);
                    return forLoopActionUsageTextualNotationBuilder.BuildTextualNotation(pocoForLoopActionUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.AssignmentActionUsage pocoAssignmentActionUsage:
                    var assignmentActionUsageTextualNotationBuilder = new AssignmentActionUsageTextualNotationBuilder(this);
                    return assignmentActionUsageTextualNotationBuilder.BuildTextualNotation(pocoAssignmentActionUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.WhileLoopActionUsage pocoWhileLoopActionUsage:
                    var whileLoopActionUsageTextualNotationBuilder = new WhileLoopActionUsageTextualNotationBuilder(this);
                    return whileLoopActionUsageTextualNotationBuilder.BuildTextualNotation(pocoWhileLoopActionUsage);

                case SysML2.NET.Core.POCO.Systems.Actions.TriggerInvocationExpression pocoTriggerInvocationExpression:
                    var triggerInvocationExpressionTextualNotationBuilder = new TriggerInvocationExpressionTextualNotationBuilder(this);
                    return triggerInvocationExpressionTextualNotationBuilder.BuildTextualNotation(pocoTriggerInvocationExpression);

                case SysML2.NET.Core.POCO.Systems.Actions.TerminateActionUsage pocoTerminateActionUsage:
                    var terminateActionUsageTextualNotationBuilder = new TerminateActionUsageTextualNotationBuilder(this);
                    return terminateActionUsageTextualNotationBuilder.BuildTextualNotation(pocoTerminateActionUsage);

                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.Definition pocoDefinition:
                    var definitionTextualNotationBuilder = new DefinitionTextualNotationBuilder(this);
                    return definitionTextualNotationBuilder.BuildTextualNotation(pocoDefinition);

                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.Usage pocoUsage:
                    var usageTextualNotationBuilder = new UsageTextualNotationBuilder(this);
                    return usageTextualNotationBuilder.BuildTextualNotation(pocoUsage);

                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.ReferenceUsage pocoReferenceUsage:
                    var referenceUsageTextualNotationBuilder = new ReferenceUsageTextualNotationBuilder(this);
                    return referenceUsageTextualNotationBuilder.BuildTextualNotation(pocoReferenceUsage);

                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.VariantMembership pocoVariantMembership:
                    var variantMembershipTextualNotationBuilder = new VariantMembershipTextualNotationBuilder(this);
                    return variantMembershipTextualNotationBuilder.BuildTextualNotation(pocoVariantMembership);

                case SysML2.NET.Core.POCO.Systems.Parts.PartDefinition pocoPartDefinition:
                    var partDefinitionTextualNotationBuilder = new PartDefinitionTextualNotationBuilder(this);
                    return partDefinitionTextualNotationBuilder.BuildTextualNotation(pocoPartDefinition);

                case SysML2.NET.Core.POCO.Systems.Parts.PartUsage pocoPartUsage:
                    var partUsageTextualNotationBuilder = new PartUsageTextualNotationBuilder(this);
                    return partUsageTextualNotationBuilder.BuildTextualNotation(pocoPartUsage);

                case SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceUsage pocoInterfaceUsage:
                    var interfaceUsageTextualNotationBuilder = new InterfaceUsageTextualNotationBuilder(this);
                    return interfaceUsageTextualNotationBuilder.BuildTextualNotation(pocoInterfaceUsage);

                case SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceDefinition pocoInterfaceDefinition:
                    var interfaceDefinitionTextualNotationBuilder = new InterfaceDefinitionTextualNotationBuilder(this);
                    return interfaceDefinitionTextualNotationBuilder.BuildTextualNotation(pocoInterfaceDefinition);

                case SysML2.NET.Core.POCO.Systems.States.StateUsage pocoStateUsage:
                    var stateUsageTextualNotationBuilder = new StateUsageTextualNotationBuilder(this);
                    return stateUsageTextualNotationBuilder.BuildTextualNotation(pocoStateUsage);

                case SysML2.NET.Core.POCO.Systems.States.StateSubactionMembership pocoStateSubactionMembership:
                    var stateSubactionMembershipTextualNotationBuilder = new StateSubactionMembershipTextualNotationBuilder(this);
                    return stateSubactionMembershipTextualNotationBuilder.BuildTextualNotation(pocoStateSubactionMembership);

                case SysML2.NET.Core.POCO.Systems.States.StateDefinition pocoStateDefinition:
                    var stateDefinitionTextualNotationBuilder = new StateDefinitionTextualNotationBuilder(this);
                    return stateDefinitionTextualNotationBuilder.BuildTextualNotation(pocoStateDefinition);

                case SysML2.NET.Core.POCO.Systems.States.TransitionUsage pocoTransitionUsage:
                    var transitionUsageTextualNotationBuilder = new TransitionUsageTextualNotationBuilder(this);
                    return transitionUsageTextualNotationBuilder.BuildTextualNotation(pocoTransitionUsage);

                case SysML2.NET.Core.POCO.Systems.States.TransitionFeatureMembership pocoTransitionFeatureMembership:
                    var transitionFeatureMembershipTextualNotationBuilder = new TransitionFeatureMembershipTextualNotationBuilder(this);
                    return transitionFeatureMembershipTextualNotationBuilder.BuildTextualNotation(pocoTransitionFeatureMembership);

                case SysML2.NET.Core.POCO.Systems.States.ExhibitStateUsage pocoExhibitStateUsage:
                    var exhibitStateUsageTextualNotationBuilder = new ExhibitStateUsageTextualNotationBuilder(this);
                    return exhibitStateUsageTextualNotationBuilder.BuildTextualNotation(pocoExhibitStateUsage);

                case SysML2.NET.Core.POCO.Systems.Constraints.ConstraintUsage pocoConstraintUsage:
                    var constraintUsageTextualNotationBuilder = new ConstraintUsageTextualNotationBuilder(this);
                    return constraintUsageTextualNotationBuilder.BuildTextualNotation(pocoConstraintUsage);

                case SysML2.NET.Core.POCO.Systems.Constraints.ConstraintDefinition pocoConstraintDefinition:
                    var constraintDefinitionTextualNotationBuilder = new ConstraintDefinitionTextualNotationBuilder(this);
                    return constraintDefinitionTextualNotationBuilder.BuildTextualNotation(pocoConstraintDefinition);

                case SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage pocoAssertConstraintUsage:
                    var assertConstraintUsageTextualNotationBuilder = new AssertConstraintUsageTextualNotationBuilder(this);
                    return assertConstraintUsageTextualNotationBuilder.BuildTextualNotation(pocoAssertConstraintUsage);

                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementDefinition pocoRequirementDefinition:
                    var requirementDefinitionTextualNotationBuilder = new RequirementDefinitionTextualNotationBuilder(this);
                    return requirementDefinitionTextualNotationBuilder.BuildTextualNotation(pocoRequirementDefinition);

                case SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage pocoSatisfyRequirementUsage:
                    var satisfyRequirementUsageTextualNotationBuilder = new SatisfyRequirementUsageTextualNotationBuilder(this);
                    return satisfyRequirementUsageTextualNotationBuilder.BuildTextualNotation(pocoSatisfyRequirementUsage);

                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementUsage pocoRequirementUsage:
                    var requirementUsageTextualNotationBuilder = new RequirementUsageTextualNotationBuilder(this);
                    return requirementUsageTextualNotationBuilder.BuildTextualNotation(pocoRequirementUsage);

                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementConstraintMembership pocoRequirementConstraintMembership:
                    var requirementConstraintMembershipTextualNotationBuilder = new RequirementConstraintMembershipTextualNotationBuilder(this);
                    return requirementConstraintMembershipTextualNotationBuilder.BuildTextualNotation(pocoRequirementConstraintMembership);

                case SysML2.NET.Core.POCO.Systems.Requirements.SubjectMembership pocoSubjectMembership:
                    var subjectMembershipTextualNotationBuilder = new SubjectMembershipTextualNotationBuilder(this);
                    return subjectMembershipTextualNotationBuilder.BuildTextualNotation(pocoSubjectMembership);

                case SysML2.NET.Core.POCO.Systems.Requirements.FramedConcernMembership pocoFramedConcernMembership:
                    var framedConcernMembershipTextualNotationBuilder = new FramedConcernMembershipTextualNotationBuilder(this);
                    return framedConcernMembershipTextualNotationBuilder.BuildTextualNotation(pocoFramedConcernMembership);

                case SysML2.NET.Core.POCO.Systems.Requirements.ConcernDefinition pocoConcernDefinition:
                    var concernDefinitionTextualNotationBuilder = new ConcernDefinitionTextualNotationBuilder(this);
                    return concernDefinitionTextualNotationBuilder.BuildTextualNotation(pocoConcernDefinition);

                case SysML2.NET.Core.POCO.Systems.Requirements.ConcernUsage pocoConcernUsage:
                    var concernUsageTextualNotationBuilder = new ConcernUsageTextualNotationBuilder(this);
                    return concernUsageTextualNotationBuilder.BuildTextualNotation(pocoConcernUsage);

                case SysML2.NET.Core.POCO.Systems.Requirements.StakeholderMembership pocoStakeholderMembership:
                    var stakeholderMembershipTextualNotationBuilder = new StakeholderMembershipTextualNotationBuilder(this);
                    return stakeholderMembershipTextualNotationBuilder.BuildTextualNotation(pocoStakeholderMembership);

                case SysML2.NET.Core.POCO.Systems.Requirements.ActorMembership pocoActorMembership:
                    var actorMembershipTextualNotationBuilder = new ActorMembershipTextualNotationBuilder(this);
                    return actorMembershipTextualNotationBuilder.BuildTextualNotation(pocoActorMembership);

                case SysML2.NET.Core.POCO.Systems.Calculations.CalculationDefinition pocoCalculationDefinition:
                    var calculationDefinitionTextualNotationBuilder = new CalculationDefinitionTextualNotationBuilder(this);
                    return calculationDefinitionTextualNotationBuilder.BuildTextualNotation(pocoCalculationDefinition);

                case SysML2.NET.Core.POCO.Systems.Calculations.CalculationUsage pocoCalculationUsage:
                    var calculationUsageTextualNotationBuilder = new CalculationUsageTextualNotationBuilder(this);
                    return calculationUsageTextualNotationBuilder.BuildTextualNotation(pocoCalculationUsage);

                case SysML2.NET.Core.POCO.Systems.Connections.ConnectionDefinition pocoConnectionDefinition:
                    var connectionDefinitionTextualNotationBuilder = new ConnectionDefinitionTextualNotationBuilder(this);
                    return connectionDefinitionTextualNotationBuilder.BuildTextualNotation(pocoConnectionDefinition);

                case SysML2.NET.Core.POCO.Systems.Connections.ConnectionUsage pocoConnectionUsage:
                    var connectionUsageTextualNotationBuilder = new ConnectionUsageTextualNotationBuilder(this);
                    return connectionUsageTextualNotationBuilder.BuildTextualNotation(pocoConnectionUsage);

                case SysML2.NET.Core.POCO.Systems.Connections.SuccessionAsUsage pocoSuccessionAsUsage:
                    var successionAsUsageTextualNotationBuilder = new SuccessionAsUsageTextualNotationBuilder(this);
                    return successionAsUsageTextualNotationBuilder.BuildTextualNotation(pocoSuccessionAsUsage);

                case SysML2.NET.Core.POCO.Systems.Connections.BindingConnectorAsUsage pocoBindingConnectorAsUsage:
                    var bindingConnectorAsUsageTextualNotationBuilder = new BindingConnectorAsUsageTextualNotationBuilder(this);
                    return bindingConnectorAsUsageTextualNotationBuilder.BuildTextualNotation(pocoBindingConnectorAsUsage);

                case SysML2.NET.Core.POCO.Systems.Cases.CaseUsage pocoCaseUsage:
                    var caseUsageTextualNotationBuilder = new CaseUsageTextualNotationBuilder(this);
                    return caseUsageTextualNotationBuilder.BuildTextualNotation(pocoCaseUsage);

                case SysML2.NET.Core.POCO.Systems.Cases.CaseDefinition pocoCaseDefinition:
                    var caseDefinitionTextualNotationBuilder = new CaseDefinitionTextualNotationBuilder(this);
                    return caseDefinitionTextualNotationBuilder.BuildTextualNotation(pocoCaseDefinition);

                case SysML2.NET.Core.POCO.Systems.Cases.ObjectiveMembership pocoObjectiveMembership:
                    var objectiveMembershipTextualNotationBuilder = new ObjectiveMembershipTextualNotationBuilder(this);
                    return objectiveMembershipTextualNotationBuilder.BuildTextualNotation(pocoObjectiveMembership);

                case SysML2.NET.Core.POCO.Systems.AnalysisCases.AnalysisCaseUsage pocoAnalysisCaseUsage:
                    var analysisCaseUsageTextualNotationBuilder = new AnalysisCaseUsageTextualNotationBuilder(this);
                    return analysisCaseUsageTextualNotationBuilder.BuildTextualNotation(pocoAnalysisCaseUsage);

                case SysML2.NET.Core.POCO.Systems.AnalysisCases.AnalysisCaseDefinition pocoAnalysisCaseDefinition:
                    var analysisCaseDefinitionTextualNotationBuilder = new AnalysisCaseDefinitionTextualNotationBuilder(this);
                    return analysisCaseDefinitionTextualNotationBuilder.BuildTextualNotation(pocoAnalysisCaseDefinition);

                case SysML2.NET.Core.POCO.Systems.Items.ItemUsage pocoItemUsage:
                    var itemUsageTextualNotationBuilder = new ItemUsageTextualNotationBuilder(this);
                    return itemUsageTextualNotationBuilder.BuildTextualNotation(pocoItemUsage);

                case SysML2.NET.Core.POCO.Systems.Items.ItemDefinition pocoItemDefinition:
                    var itemDefinitionTextualNotationBuilder = new ItemDefinitionTextualNotationBuilder(this);
                    return itemDefinitionTextualNotationBuilder.BuildTextualNotation(pocoItemDefinition);

                case SysML2.NET.Core.POCO.Systems.Views.ViewpointDefinition pocoViewpointDefinition:
                    var viewpointDefinitionTextualNotationBuilder = new ViewpointDefinitionTextualNotationBuilder(this);
                    return viewpointDefinitionTextualNotationBuilder.BuildTextualNotation(pocoViewpointDefinition);

                case SysML2.NET.Core.POCO.Systems.Views.ViewUsage pocoViewUsage:
                    var viewUsageTextualNotationBuilder = new ViewUsageTextualNotationBuilder(this);
                    return viewUsageTextualNotationBuilder.BuildTextualNotation(pocoViewUsage);

                case SysML2.NET.Core.POCO.Systems.Views.RenderingDefinition pocoRenderingDefinition:
                    var renderingDefinitionTextualNotationBuilder = new RenderingDefinitionTextualNotationBuilder(this);
                    return renderingDefinitionTextualNotationBuilder.BuildTextualNotation(pocoRenderingDefinition);

                case SysML2.NET.Core.POCO.Systems.Views.ViewpointUsage pocoViewpointUsage:
                    var viewpointUsageTextualNotationBuilder = new ViewpointUsageTextualNotationBuilder(this);
                    return viewpointUsageTextualNotationBuilder.BuildTextualNotation(pocoViewpointUsage);

                case SysML2.NET.Core.POCO.Systems.Views.ViewDefinition pocoViewDefinition:
                    var viewDefinitionTextualNotationBuilder = new ViewDefinitionTextualNotationBuilder(this);
                    return viewDefinitionTextualNotationBuilder.BuildTextualNotation(pocoViewDefinition);

                case SysML2.NET.Core.POCO.Systems.Views.RenderingUsage pocoRenderingUsage:
                    var renderingUsageTextualNotationBuilder = new RenderingUsageTextualNotationBuilder(this);
                    return renderingUsageTextualNotationBuilder.BuildTextualNotation(pocoRenderingUsage);

                case SysML2.NET.Core.POCO.Systems.Views.ViewRenderingMembership pocoViewRenderingMembership:
                    var viewRenderingMembershipTextualNotationBuilder = new ViewRenderingMembershipTextualNotationBuilder(this);
                    return viewRenderingMembershipTextualNotationBuilder.BuildTextualNotation(pocoViewRenderingMembership);

                case SysML2.NET.Core.POCO.Systems.Views.NamespaceExpose pocoNamespaceExpose:
                    var namespaceExposeTextualNotationBuilder = new NamespaceExposeTextualNotationBuilder(this);
                    return namespaceExposeTextualNotationBuilder.BuildTextualNotation(pocoNamespaceExpose);

                case SysML2.NET.Core.POCO.Systems.Views.MembershipExpose pocoMembershipExpose:
                    var membershipExposeTextualNotationBuilder = new MembershipExposeTextualNotationBuilder(this);
                    return membershipExposeTextualNotationBuilder.BuildTextualNotation(pocoMembershipExpose);

                case SysML2.NET.Core.POCO.Systems.VerificationCases.VerificationCaseDefinition pocoVerificationCaseDefinition:
                    var verificationCaseDefinitionTextualNotationBuilder = new VerificationCaseDefinitionTextualNotationBuilder(this);
                    return verificationCaseDefinitionTextualNotationBuilder.BuildTextualNotation(pocoVerificationCaseDefinition);

                case SysML2.NET.Core.POCO.Systems.VerificationCases.VerificationCaseUsage pocoVerificationCaseUsage:
                    var verificationCaseUsageTextualNotationBuilder = new VerificationCaseUsageTextualNotationBuilder(this);
                    return verificationCaseUsageTextualNotationBuilder.BuildTextualNotation(pocoVerificationCaseUsage);

                case SysML2.NET.Core.POCO.Systems.VerificationCases.RequirementVerificationMembership pocoRequirementVerificationMembership:
                    var requirementVerificationMembershipTextualNotationBuilder = new RequirementVerificationMembershipTextualNotationBuilder(this);
                    return requirementVerificationMembershipTextualNotationBuilder.BuildTextualNotation(pocoRequirementVerificationMembership);

                case SysML2.NET.Core.POCO.Systems.Enumerations.EnumerationDefinition pocoEnumerationDefinition:
                    var enumerationDefinitionTextualNotationBuilder = new EnumerationDefinitionTextualNotationBuilder(this);
                    return enumerationDefinitionTextualNotationBuilder.BuildTextualNotation(pocoEnumerationDefinition);

                case SysML2.NET.Core.POCO.Systems.Enumerations.EnumerationUsage pocoEnumerationUsage:
                    var enumerationUsageTextualNotationBuilder = new EnumerationUsageTextualNotationBuilder(this);
                    return enumerationUsageTextualNotationBuilder.BuildTextualNotation(pocoEnumerationUsage);

                case SysML2.NET.Core.POCO.Systems.Allocations.AllocationDefinition pocoAllocationDefinition:
                    var allocationDefinitionTextualNotationBuilder = new AllocationDefinitionTextualNotationBuilder(this);
                    return allocationDefinitionTextualNotationBuilder.BuildTextualNotation(pocoAllocationDefinition);

                case SysML2.NET.Core.POCO.Systems.Allocations.AllocationUsage pocoAllocationUsage:
                    var allocationUsageTextualNotationBuilder = new AllocationUsageTextualNotationBuilder(this);
                    return allocationUsageTextualNotationBuilder.BuildTextualNotation(pocoAllocationUsage);

                case SysML2.NET.Core.POCO.Systems.Occurrences.OccurrenceUsage pocoOccurrenceUsage:
                    var occurrenceUsageTextualNotationBuilder = new OccurrenceUsageTextualNotationBuilder(this);
                    return occurrenceUsageTextualNotationBuilder.BuildTextualNotation(pocoOccurrenceUsage);

                case SysML2.NET.Core.POCO.Systems.Occurrences.OccurrenceDefinition pocoOccurrenceDefinition:
                    var occurrenceDefinitionTextualNotationBuilder = new OccurrenceDefinitionTextualNotationBuilder(this);
                    return occurrenceDefinitionTextualNotationBuilder.BuildTextualNotation(pocoOccurrenceDefinition);

                case SysML2.NET.Core.POCO.Systems.Occurrences.EventOccurrenceUsage pocoEventOccurrenceUsage:
                    var eventOccurrenceUsageTextualNotationBuilder = new EventOccurrenceUsageTextualNotationBuilder(this);
                    return eventOccurrenceUsageTextualNotationBuilder.BuildTextualNotation(pocoEventOccurrenceUsage);

                case SysML2.NET.Core.POCO.Systems.UseCases.IncludeUseCaseUsage pocoIncludeUseCaseUsage:
                    var includeUseCaseUsageTextualNotationBuilder = new IncludeUseCaseUsageTextualNotationBuilder(this);
                    return includeUseCaseUsageTextualNotationBuilder.BuildTextualNotation(pocoIncludeUseCaseUsage);

                case SysML2.NET.Core.POCO.Systems.UseCases.UseCaseUsage pocoUseCaseUsage:
                    var useCaseUsageTextualNotationBuilder = new UseCaseUsageTextualNotationBuilder(this);
                    return useCaseUsageTextualNotationBuilder.BuildTextualNotation(pocoUseCaseUsage);

                case SysML2.NET.Core.POCO.Systems.UseCases.UseCaseDefinition pocoUseCaseDefinition:
                    var useCaseDefinitionTextualNotationBuilder = new UseCaseDefinitionTextualNotationBuilder(this);
                    return useCaseDefinitionTextualNotationBuilder.BuildTextualNotation(pocoUseCaseDefinition);

                case SysML2.NET.Core.POCO.Systems.Metadata.MetadataDefinition pocoMetadataDefinition:
                    var metadataDefinitionTextualNotationBuilder = new MetadataDefinitionTextualNotationBuilder(this);
                    return metadataDefinitionTextualNotationBuilder.BuildTextualNotation(pocoMetadataDefinition);

                case SysML2.NET.Core.POCO.Systems.Metadata.MetadataUsage pocoMetadataUsage:
                    var metadataUsageTextualNotationBuilder = new MetadataUsageTextualNotationBuilder(this);
                    return metadataUsageTextualNotationBuilder.BuildTextualNotation(pocoMetadataUsage);

                case SysML2.NET.Core.POCO.Systems.Flows.FlowUsage pocoFlowUsage:
                    var flowUsageTextualNotationBuilder = new FlowUsageTextualNotationBuilder(this);
                    return flowUsageTextualNotationBuilder.BuildTextualNotation(pocoFlowUsage);

                case SysML2.NET.Core.POCO.Systems.Flows.FlowDefinition pocoFlowDefinition:
                    var flowDefinitionTextualNotationBuilder = new FlowDefinitionTextualNotationBuilder(this);
                    return flowDefinitionTextualNotationBuilder.BuildTextualNotation(pocoFlowDefinition);

                case SysML2.NET.Core.POCO.Systems.Flows.SuccessionFlowUsage pocoSuccessionFlowUsage:
                    var successionFlowUsageTextualNotationBuilder = new SuccessionFlowUsageTextualNotationBuilder(this);
                    return successionFlowUsageTextualNotationBuilder.BuildTextualNotation(pocoSuccessionFlowUsage);

                case SysML2.NET.Core.POCO.Root.Dependencies.Dependency pocoDependency:
                    var dependencyTextualNotationBuilder = new DependencyTextualNotationBuilder(this);
                    return dependencyTextualNotationBuilder.BuildTextualNotation(pocoDependency);

                case SysML2.NET.Core.POCO.Root.Annotations.Comment pocoComment:
                    var commentTextualNotationBuilder = new CommentTextualNotationBuilder(this);
                    return commentTextualNotationBuilder.BuildTextualNotation(pocoComment);

                case SysML2.NET.Core.POCO.Root.Annotations.Annotation pocoAnnotation:
                    var annotationTextualNotationBuilder = new AnnotationTextualNotationBuilder(this);
                    return annotationTextualNotationBuilder.BuildTextualNotation(pocoAnnotation);

                case SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement pocoAnnotatingElement:
                    var annotatingElementTextualNotationBuilder = new AnnotatingElementTextualNotationBuilder(this);
                    return annotatingElementTextualNotationBuilder.BuildTextualNotation(pocoAnnotatingElement);

                case SysML2.NET.Core.POCO.Root.Annotations.TextualRepresentation pocoTextualRepresentation:
                    var textualRepresentationTextualNotationBuilder = new TextualRepresentationTextualNotationBuilder(this);
                    return textualRepresentationTextualNotationBuilder.BuildTextualNotation(pocoTextualRepresentation);

                case SysML2.NET.Core.POCO.Root.Annotations.Documentation pocoDocumentation:
                    var documentationTextualNotationBuilder = new DocumentationTextualNotationBuilder(this);
                    return documentationTextualNotationBuilder.BuildTextualNotation(pocoDocumentation);

                case SysML2.NET.Core.POCO.Root.Namespaces.Namespace pocoNamespace:
                    var namespaceTextualNotationBuilder = new NamespaceTextualNotationBuilder(this);
                    return namespaceTextualNotationBuilder.BuildTextualNotation(pocoNamespace);

                case SysML2.NET.Core.POCO.Root.Namespaces.MembershipImport pocoMembershipImport:
                    var membershipImportTextualNotationBuilder = new MembershipImportTextualNotationBuilder(this);
                    return membershipImportTextualNotationBuilder.BuildTextualNotation(pocoMembershipImport);

                case SysML2.NET.Core.POCO.Root.Namespaces.NamespaceImport pocoNamespaceImport:
                    var namespaceImportTextualNotationBuilder = new NamespaceImportTextualNotationBuilder(this);
                    return namespaceImportTextualNotationBuilder.BuildTextualNotation(pocoNamespaceImport);

                case SysML2.NET.Core.POCO.Root.Namespaces.Membership pocoMembership:
                    var membershipTextualNotationBuilder = new MembershipTextualNotationBuilder(this);
                    return membershipTextualNotationBuilder.BuildTextualNotation(pocoMembership);

                case SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership pocoOwningMembership:
                    var owningMembershipTextualNotationBuilder = new OwningMembershipTextualNotationBuilder(this);
                    return owningMembershipTextualNotationBuilder.BuildTextualNotation(pocoOwningMembership);

                case SysML2.NET.Core.POCO.Core.Types.Specialization pocoSpecialization:
                    var specializationTextualNotationBuilder = new SpecializationTextualNotationBuilder(this);
                    return specializationTextualNotationBuilder.BuildTextualNotation(pocoSpecialization);

                case SysML2.NET.Core.POCO.Core.Types.Type pocoType:
                    var typeTextualNotationBuilder = new TypeTextualNotationBuilder(this);
                    return typeTextualNotationBuilder.BuildTextualNotation(pocoType);

                case SysML2.NET.Core.POCO.Core.Types.FeatureMembership pocoFeatureMembership:
                    var featureMembershipTextualNotationBuilder = new FeatureMembershipTextualNotationBuilder(this);
                    return featureMembershipTextualNotationBuilder.BuildTextualNotation(pocoFeatureMembership);

                case SysML2.NET.Core.POCO.Core.Types.Conjugation pocoConjugation:
                    var conjugationTextualNotationBuilder = new ConjugationTextualNotationBuilder(this);
                    return conjugationTextualNotationBuilder.BuildTextualNotation(pocoConjugation);

                case SysML2.NET.Core.POCO.Core.Types.Multiplicity pocoMultiplicity:
                    var multiplicityTextualNotationBuilder = new MultiplicityTextualNotationBuilder(this);
                    return multiplicityTextualNotationBuilder.BuildTextualNotation(pocoMultiplicity);

                case SysML2.NET.Core.POCO.Core.Types.Disjoining pocoDisjoining:
                    var disjoiningTextualNotationBuilder = new DisjoiningTextualNotationBuilder(this);
                    return disjoiningTextualNotationBuilder.BuildTextualNotation(pocoDisjoining);

                case SysML2.NET.Core.POCO.Core.Types.Differencing pocoDifferencing:
                    var differencingTextualNotationBuilder = new DifferencingTextualNotationBuilder(this);
                    return differencingTextualNotationBuilder.BuildTextualNotation(pocoDifferencing);

                case SysML2.NET.Core.POCO.Core.Types.Unioning pocoUnioning:
                    var unioningTextualNotationBuilder = new UnioningTextualNotationBuilder(this);
                    return unioningTextualNotationBuilder.BuildTextualNotation(pocoUnioning);

                case SysML2.NET.Core.POCO.Core.Types.Intersecting pocoIntersecting:
                    var intersectingTextualNotationBuilder = new IntersectingTextualNotationBuilder(this);
                    return intersectingTextualNotationBuilder.BuildTextualNotation(pocoIntersecting);

                case SysML2.NET.Core.POCO.Core.Classifiers.Subclassification pocoSubclassification:
                    var subclassificationTextualNotationBuilder = new SubclassificationTextualNotationBuilder(this);
                    return subclassificationTextualNotationBuilder.BuildTextualNotation(pocoSubclassification);

                case SysML2.NET.Core.POCO.Core.Classifiers.Classifier pocoClassifier:
                    var classifierTextualNotationBuilder = new ClassifierTextualNotationBuilder(this);
                    return classifierTextualNotationBuilder.BuildTextualNotation(pocoClassifier);

                case SysML2.NET.Core.POCO.Core.Features.Redefinition pocoRedefinition:
                    var redefinitionTextualNotationBuilder = new RedefinitionTextualNotationBuilder(this);
                    return redefinitionTextualNotationBuilder.BuildTextualNotation(pocoRedefinition);

                case SysML2.NET.Core.POCO.Core.Features.Feature pocoFeature:
                    var featureTextualNotationBuilder = new FeatureTextualNotationBuilder(this);
                    return featureTextualNotationBuilder.BuildTextualNotation(pocoFeature);

                case SysML2.NET.Core.POCO.Core.Features.FeatureTyping pocoFeatureTyping:
                    var featureTypingTextualNotationBuilder = new FeatureTypingTextualNotationBuilder(this);
                    return featureTypingTextualNotationBuilder.BuildTextualNotation(pocoFeatureTyping);

                case SysML2.NET.Core.POCO.Core.Features.Subsetting pocoSubsetting:
                    var subsettingTextualNotationBuilder = new SubsettingTextualNotationBuilder(this);
                    return subsettingTextualNotationBuilder.BuildTextualNotation(pocoSubsetting);

                case SysML2.NET.Core.POCO.Core.Features.TypeFeaturing pocoTypeFeaturing:
                    var typeFeaturingTextualNotationBuilder = new TypeFeaturingTextualNotationBuilder(this);
                    return typeFeaturingTextualNotationBuilder.BuildTextualNotation(pocoTypeFeaturing);

                case SysML2.NET.Core.POCO.Core.Features.EndFeatureMembership pocoEndFeatureMembership:
                    var endFeatureMembershipTextualNotationBuilder = new EndFeatureMembershipTextualNotationBuilder(this);
                    return endFeatureMembershipTextualNotationBuilder.BuildTextualNotation(pocoEndFeatureMembership);

                case SysML2.NET.Core.POCO.Core.Features.FeatureChaining pocoFeatureChaining:
                    var featureChainingTextualNotationBuilder = new FeatureChainingTextualNotationBuilder(this);
                    return featureChainingTextualNotationBuilder.BuildTextualNotation(pocoFeatureChaining);

                case SysML2.NET.Core.POCO.Core.Features.FeatureInverting pocoFeatureInverting:
                    var featureInvertingTextualNotationBuilder = new FeatureInvertingTextualNotationBuilder(this);
                    return featureInvertingTextualNotationBuilder.BuildTextualNotation(pocoFeatureInverting);

                case SysML2.NET.Core.POCO.Core.Features.ReferenceSubsetting pocoReferenceSubsetting:
                    var referenceSubsettingTextualNotationBuilder = new ReferenceSubsettingTextualNotationBuilder(this);
                    return referenceSubsettingTextualNotationBuilder.BuildTextualNotation(pocoReferenceSubsetting);

                case SysML2.NET.Core.POCO.Core.Features.CrossSubsetting pocoCrossSubsetting:
                    var crossSubsettingTextualNotationBuilder = new CrossSubsettingTextualNotationBuilder(this);
                    return crossSubsettingTextualNotationBuilder.BuildTextualNotation(pocoCrossSubsetting);

                case SysML2.NET.Core.POCO.Kernel.Interactions.PayloadFeature pocoPayloadFeature:
                    var payloadFeatureTextualNotationBuilder = new PayloadFeatureTextualNotationBuilder(this);
                    return payloadFeatureTextualNotationBuilder.BuildTextualNotation(pocoPayloadFeature);

                case SysML2.NET.Core.POCO.Kernel.Interactions.Interaction pocoInteraction:
                    var interactionTextualNotationBuilder = new InteractionTextualNotationBuilder(this);
                    return interactionTextualNotationBuilder.BuildTextualNotation(pocoInteraction);

                case SysML2.NET.Core.POCO.Kernel.Interactions.SuccessionFlow pocoSuccessionFlow:
                    var successionFlowTextualNotationBuilder = new SuccessionFlowTextualNotationBuilder(this);
                    return successionFlowTextualNotationBuilder.BuildTextualNotation(pocoSuccessionFlow);

                case SysML2.NET.Core.POCO.Kernel.Interactions.Flow pocoFlow:
                    var flowTextualNotationBuilder = new FlowTextualNotationBuilder(this);
                    return flowTextualNotationBuilder.BuildTextualNotation(pocoFlow);

                case SysML2.NET.Core.POCO.Kernel.Interactions.FlowEnd pocoFlowEnd:
                    var flowEndTextualNotationBuilder = new FlowEndTextualNotationBuilder(this);
                    return flowEndTextualNotationBuilder.BuildTextualNotation(pocoFlowEnd);

                case SysML2.NET.Core.POCO.Kernel.Packages.LibraryPackage pocoLibraryPackage:
                    var libraryPackageTextualNotationBuilder = new LibraryPackageTextualNotationBuilder(this);
                    return libraryPackageTextualNotationBuilder.BuildTextualNotation(pocoLibraryPackage);

                case SysML2.NET.Core.POCO.Kernel.Packages.ElementFilterMembership pocoElementFilterMembership:
                    var elementFilterMembershipTextualNotationBuilder = new ElementFilterMembershipTextualNotationBuilder(this);
                    return elementFilterMembershipTextualNotationBuilder.BuildTextualNotation(pocoElementFilterMembership);

                case SysML2.NET.Core.POCO.Kernel.Packages.Package pocoPackage:
                    var packageTextualNotationBuilder = new PackageTextualNotationBuilder(this);
                    return packageTextualNotationBuilder.BuildTextualNotation(pocoPackage);

                case SysML2.NET.Core.POCO.Kernel.Classes.Class pocoClass:
                    var classTextualNotationBuilder = new ClassTextualNotationBuilder(this);
                    return classTextualNotationBuilder.BuildTextualNotation(pocoClass);

                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralBoolean pocoLiteralBoolean:
                    var literalBooleanTextualNotationBuilder = new LiteralBooleanTextualNotationBuilder(this);
                    return literalBooleanTextualNotationBuilder.BuildTextualNotation(pocoLiteralBoolean);

                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralExpression pocoLiteralExpression:
                    var literalExpressionTextualNotationBuilder = new LiteralExpressionTextualNotationBuilder(this);
                    return literalExpressionTextualNotationBuilder.BuildTextualNotation(pocoLiteralExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralRational pocoLiteralRational:
                    var literalRationalTextualNotationBuilder = new LiteralRationalTextualNotationBuilder(this);
                    return literalRationalTextualNotationBuilder.BuildTextualNotation(pocoLiteralRational);

                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralInfinity pocoLiteralInfinity:
                    var literalInfinityTextualNotationBuilder = new LiteralInfinityTextualNotationBuilder(this);
                    return literalInfinityTextualNotationBuilder.BuildTextualNotation(pocoLiteralInfinity);

                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralInteger pocoLiteralInteger:
                    var literalIntegerTextualNotationBuilder = new LiteralIntegerTextualNotationBuilder(this);
                    return literalIntegerTextualNotationBuilder.BuildTextualNotation(pocoLiteralInteger);

                case SysML2.NET.Core.POCO.Kernel.Expressions.NullExpression pocoNullExpression:
                    var nullExpressionTextualNotationBuilder = new NullExpressionTextualNotationBuilder(this);
                    return nullExpressionTextualNotationBuilder.BuildTextualNotation(pocoNullExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralString pocoLiteralString:
                    var literalStringTextualNotationBuilder = new LiteralStringTextualNotationBuilder(this);
                    return literalStringTextualNotationBuilder.BuildTextualNotation(pocoLiteralString);

                case SysML2.NET.Core.POCO.Kernel.Expressions.InvocationExpression pocoInvocationExpression:
                    var invocationExpressionTextualNotationBuilder = new InvocationExpressionTextualNotationBuilder(this);
                    return invocationExpressionTextualNotationBuilder.BuildTextualNotation(pocoInvocationExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression pocoFeatureReferenceExpression:
                    var featureReferenceExpressionTextualNotationBuilder = new FeatureReferenceExpressionTextualNotationBuilder(this);
                    return featureReferenceExpressionTextualNotationBuilder.BuildTextualNotation(pocoFeatureReferenceExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.SelectExpression pocoSelectExpression:
                    var selectExpressionTextualNotationBuilder = new SelectExpressionTextualNotationBuilder(this);
                    return selectExpressionTextualNotationBuilder.BuildTextualNotation(pocoSelectExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.OperatorExpression pocoOperatorExpression:
                    var operatorExpressionTextualNotationBuilder = new OperatorExpressionTextualNotationBuilder(this);
                    return operatorExpressionTextualNotationBuilder.BuildTextualNotation(pocoOperatorExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.CollectExpression pocoCollectExpression:
                    var collectExpressionTextualNotationBuilder = new CollectExpressionTextualNotationBuilder(this);
                    return collectExpressionTextualNotationBuilder.BuildTextualNotation(pocoCollectExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.FeatureChainExpression pocoFeatureChainExpression:
                    var featureChainExpressionTextualNotationBuilder = new FeatureChainExpressionTextualNotationBuilder(this);
                    return featureChainExpressionTextualNotationBuilder.BuildTextualNotation(pocoFeatureChainExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.MetadataAccessExpression pocoMetadataAccessExpression:
                    var metadataAccessExpressionTextualNotationBuilder = new MetadataAccessExpressionTextualNotationBuilder(this);
                    return metadataAccessExpressionTextualNotationBuilder.BuildTextualNotation(pocoMetadataAccessExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.IndexExpression pocoIndexExpression:
                    var indexExpressionTextualNotationBuilder = new IndexExpressionTextualNotationBuilder(this);
                    return indexExpressionTextualNotationBuilder.BuildTextualNotation(pocoIndexExpression);

                case SysML2.NET.Core.POCO.Kernel.Expressions.ConstructorExpression pocoConstructorExpression:
                    var constructorExpressionTextualNotationBuilder = new ConstructorExpressionTextualNotationBuilder(this);
                    return constructorExpressionTextualNotationBuilder.BuildTextualNotation(pocoConstructorExpression);

                case SysML2.NET.Core.POCO.Kernel.Structures.Structure pocoStructure:
                    var structureTextualNotationBuilder = new StructureTextualNotationBuilder(this);
                    return structureTextualNotationBuilder.BuildTextualNotation(pocoStructure);

                case SysML2.NET.Core.POCO.Kernel.Functions.Predicate pocoPredicate:
                    var predicateTextualNotationBuilder = new PredicateTextualNotationBuilder(this);
                    return predicateTextualNotationBuilder.BuildTextualNotation(pocoPredicate);

                case SysML2.NET.Core.POCO.Kernel.Functions.ReturnParameterMembership pocoReturnParameterMembership:
                    var returnParameterMembershipTextualNotationBuilder = new ReturnParameterMembershipTextualNotationBuilder(this);
                    return returnParameterMembershipTextualNotationBuilder.BuildTextualNotation(pocoReturnParameterMembership);

                case SysML2.NET.Core.POCO.Kernel.Functions.Invariant pocoInvariant:
                    var invariantTextualNotationBuilder = new InvariantTextualNotationBuilder(this);
                    return invariantTextualNotationBuilder.BuildTextualNotation(pocoInvariant);

                case SysML2.NET.Core.POCO.Kernel.Functions.BooleanExpression pocoBooleanExpression:
                    var booleanExpressionTextualNotationBuilder = new BooleanExpressionTextualNotationBuilder(this);
                    return booleanExpressionTextualNotationBuilder.BuildTextualNotation(pocoBooleanExpression);

                case SysML2.NET.Core.POCO.Kernel.Functions.Expression pocoExpression:
                    var expressionTextualNotationBuilder = new ExpressionTextualNotationBuilder(this);
                    return expressionTextualNotationBuilder.BuildTextualNotation(pocoExpression);

                case SysML2.NET.Core.POCO.Kernel.Functions.Function pocoFunction:
                    var functionTextualNotationBuilder = new FunctionTextualNotationBuilder(this);
                    return functionTextualNotationBuilder.BuildTextualNotation(pocoFunction);

                case SysML2.NET.Core.POCO.Kernel.Functions.ResultExpressionMembership pocoResultExpressionMembership:
                    var resultExpressionMembershipTextualNotationBuilder = new ResultExpressionMembershipTextualNotationBuilder(this);
                    return resultExpressionMembershipTextualNotationBuilder.BuildTextualNotation(pocoResultExpressionMembership);

                case SysML2.NET.Core.POCO.Kernel.Multiplicities.MultiplicityRange pocoMultiplicityRange:
                    var multiplicityRangeTextualNotationBuilder = new MultiplicityRangeTextualNotationBuilder(this);
                    return multiplicityRangeTextualNotationBuilder.BuildTextualNotation(pocoMultiplicityRange);

                case SysML2.NET.Core.POCO.Kernel.Behaviors.Step pocoStep:
                    var stepTextualNotationBuilder = new StepTextualNotationBuilder(this);
                    return stepTextualNotationBuilder.BuildTextualNotation(pocoStep);

                case SysML2.NET.Core.POCO.Kernel.Behaviors.Behavior pocoBehavior:
                    var behaviorTextualNotationBuilder = new BehaviorTextualNotationBuilder(this);
                    return behaviorTextualNotationBuilder.BuildTextualNotation(pocoBehavior);

                case SysML2.NET.Core.POCO.Kernel.Behaviors.ParameterMembership pocoParameterMembership:
                    var parameterMembershipTextualNotationBuilder = new ParameterMembershipTextualNotationBuilder(this);
                    return parameterMembershipTextualNotationBuilder.BuildTextualNotation(pocoParameterMembership);

                case SysML2.NET.Core.POCO.Kernel.Metadata.Metaclass pocoMetaclass:
                    var metaclassTextualNotationBuilder = new MetaclassTextualNotationBuilder(this);
                    return metaclassTextualNotationBuilder.BuildTextualNotation(pocoMetaclass);

                case SysML2.NET.Core.POCO.Kernel.Metadata.MetadataFeature pocoMetadataFeature:
                    var metadataFeatureTextualNotationBuilder = new MetadataFeatureTextualNotationBuilder(this);
                    return metadataFeatureTextualNotationBuilder.BuildTextualNotation(pocoMetadataFeature);

                case SysML2.NET.Core.POCO.Kernel.DataTypes.DataType pocoDataType:
                    var dataTypeTextualNotationBuilder = new DataTypeTextualNotationBuilder(this);
                    return dataTypeTextualNotationBuilder.BuildTextualNotation(pocoDataType);

                case SysML2.NET.Core.POCO.Kernel.Associations.AssociationStructure pocoAssociationStructure:
                    var associationStructureTextualNotationBuilder = new AssociationStructureTextualNotationBuilder(this);
                    return associationStructureTextualNotationBuilder.BuildTextualNotation(pocoAssociationStructure);

                case SysML2.NET.Core.POCO.Kernel.Associations.Association pocoAssociation:
                    var associationTextualNotationBuilder = new AssociationTextualNotationBuilder(this);
                    return associationTextualNotationBuilder.BuildTextualNotation(pocoAssociation);

                case SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue pocoFeatureValue:
                    var featureValueTextualNotationBuilder = new FeatureValueTextualNotationBuilder(this);
                    return featureValueTextualNotationBuilder.BuildTextualNotation(pocoFeatureValue);

                case SysML2.NET.Core.POCO.Kernel.Connectors.Connector pocoConnector:
                    var connectorTextualNotationBuilder = new ConnectorTextualNotationBuilder(this);
                    return connectorTextualNotationBuilder.BuildTextualNotation(pocoConnector);

                case SysML2.NET.Core.POCO.Kernel.Connectors.BindingConnector pocoBindingConnector:
                    var bindingConnectorTextualNotationBuilder = new BindingConnectorTextualNotationBuilder(this);
                    return bindingConnectorTextualNotationBuilder.BuildTextualNotation(pocoBindingConnector);

                case SysML2.NET.Core.POCO.Kernel.Connectors.Succession pocoSuccession:
                    var successionTextualNotationBuilder = new SuccessionTextualNotationBuilder(this);
                    return successionTextualNotationBuilder.BuildTextualNotation(pocoSuccession);

                default:
                    throw new ArgumentOutOfRangeException(nameof(element), "Provided element is not supported");
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
