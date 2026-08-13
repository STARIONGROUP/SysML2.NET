// -------------------------------------------------------------------------------------------------
// <copyright file="GeneratedImpliedRuleGuards.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Semantics.Implied
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The guards whose OCL was mechanically translated from the abstract syntax.
    /// </summary>
    /// <remarks>
    /// A conditional semantic constraint absent from this set has a guard expression outside the
    /// translatable shapes and must be supplied by a hand-written <see cref="IImpliedRuleGuard" />.
    /// </remarks>
    public static class GeneratedImpliedRuleGuards
    {
        /// <summary>
        /// The generated guards, ordered by constraint name.
        /// </summary>
        public static IReadOnlyList<IImpliedRuleGuard> All { get; } =
        [
            // not isTriggerAction()
            new GeneratedRuleGuard("checkAcceptActionUsageSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage guardSubject && !guardSubject.IsTriggerAction()),
            // isTriggerAction()
            new GeneratedRuleGuard("checkAcceptActionUsageTriggerActionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage guardSubject && guardSubject.IsTriggerAction()),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(PartDefinition) or owningType.oclIsKindOf(PartUsage))
            new GeneratedRuleGuard("checkActionUsageOwnedActionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IActionUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.Parts.IPartDefinition or SysML2.NET.Core.POCO.Systems.Parts.IPartUsage }),
            // isSubactionUsage()
            new GeneratedRuleGuard("checkActionUsageSubactionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IActionUsage guardSubject && guardSubject.IsSubactionUsage()),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(AnalysisCaseDefinition) or owningType.oclIsKindOf(AnalysisCaseUsage))
            new GeneratedRuleGuard("checkAnalysisCaseUsageSubAnalysisCaseSpecialization", element => element is SysML2.NET.Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.AnalysisCases.IAnalysisCaseDefinition or SysML2.NET.Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage }),
            // isSubactionUsage()
            new GeneratedRuleGuard("checkAssignmentActionUsageSubactionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage guardSubject && guardSubject.IsSubactionUsage()),
            // owningType <> null and (owningType.oclIsKindOf(CalculationDefinition) or owningType.oclIsKindOf(CalculationUsage))
            new GeneratedRuleGuard("checkCalculationUsageSubcalculationSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Calculations.ICalculationUsage { owningType: SysML2.NET.Core.POCO.Systems.Calculations.ICalculationDefinition or SysML2.NET.Core.POCO.Systems.Calculations.ICalculationUsage }),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(CaseDefinition) or owningType.oclIsKindOf(CaseUsage))
            new GeneratedRuleGuard("checkCaseUsageSubcaseSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Cases.ICaseUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.Cases.ICaseDefinition or SysML2.NET.Core.POCO.Systems.Cases.ICaseUsage }),
            // owningFeatureMembership <> null and owningFeatureMembership.oclIsKindOf(FramedConcernMembership)
            new GeneratedRuleGuard("checkConcernUsageFramedConcernSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Requirements.IConcernUsage { owningFeatureMembership: SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership }),
            // ownedEndFeature->size() = 2
            new GeneratedRuleGuard("checkConnectionDefinitionBinarySpecialization", element => element is SysML2.NET.Core.POCO.Systems.Connections.IConnectionDefinition guardSubject && ((SysML2.NET.Core.POCO.Core.Types.IType)guardSubject).ownedEndFeature.Count == 2),
            // ownedEndFeature->size() = 2
            new GeneratedRuleGuard("checkConnectionUsageBinarySpecialization", element => element is SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage guardSubject && ((SysML2.NET.Core.POCO.Core.Types.IType)guardSubject).ownedEndFeature.Count == 2),
            // owningType <> null and (owningType.oclIsKindOf(ItemDefinition) or owningType.oclIsKindOf(ItemUsage))
            new GeneratedRuleGuard("checkConstraintUsageCheckedConstraintSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage { owningType: SysML2.NET.Core.POCO.Systems.Items.IItemDefinition or SysML2.NET.Core.POCO.Systems.Items.IItemUsage }),
            // owningType <> null and (owningType.oclIsKindOf(OccurrenceDefinition) or owningType.oclIsKindOf(OccurrenceUsage))
            new GeneratedRuleGuard("checkEventOccurrenceUsageSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Occurrences.IEventOccurrenceUsage { owningType: SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition or SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage }),
            // owningType <> null and (owningType.oclIsKindOf(PartDefinition) or owningType.oclIsKindOf(PartUsage))
            new GeneratedRuleGuard("checkExhibitStateUsageSpecialization", element => element is SysML2.NET.Core.POCO.Systems.States.IExhibitStateUsage { owningType: SysML2.NET.Core.POCO.Systems.Parts.IPartDefinition or SysML2.NET.Core.POCO.Systems.Parts.IPartUsage }),
            // ownedTyping.type->exists(selectByKind(DataType))
            new GeneratedRuleGuard("checkFeatureDataValueSpecialization", element => element is SysML2.NET.Core.POCO.Core.Features.IFeature guardSubject && guardSubject.ownedTyping.Any(featureTyping => featureTyping.Type is SysML2.NET.Core.POCO.Kernel.DataTypes.IDataType)),
            // ownedTyping.type->exists(selectByKind(Structure))
            new GeneratedRuleGuard("checkFeatureObjectSpecialization", element => element is SysML2.NET.Core.POCO.Core.Features.IFeature guardSubject && guardSubject.ownedTyping.Any(featureTyping => featureTyping.Type is SysML2.NET.Core.POCO.Kernel.Structures.IStructure)),
            // ownedTyping.type->exists(selectByKind(Class))
            new GeneratedRuleGuard("checkFeatureOccurrenceSpecialization", element => element is SysML2.NET.Core.POCO.Core.Features.IFeature guardSubject && guardSubject.ownedTyping.Any(featureTyping => featureTyping.Type is SysML2.NET.Core.POCO.Kernel.Classes.IClass)),
            // ownedEndFeatures->notEmpty()
            new GeneratedRuleGuard("checkFlowUsageFlowSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage guardSubject && ((SysML2.NET.Core.POCO.Core.Types.IType)guardSubject).ownedEndFeature.Count > 0),
            // ownedEndFeatures->notEmpty()
            new GeneratedRuleGuard("checkFlowWithEndsSpecialization", element => element is SysML2.NET.Core.POCO.Kernel.Interactions.IFlow guardSubject && ((SysML2.NET.Core.POCO.Core.Types.IType)guardSubject).ownedEndFeature.Count > 0),
            // isSubactionUsage()
            new GeneratedRuleGuard("checkForLoopActionUsageSubactionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IForLoopActionUsage guardSubject && guardSubject.IsSubactionUsage()),
            // isSubactionUsage()
            new GeneratedRuleGuard("checkIfActionUsageSubactionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IIfActionUsage guardSubject && guardSubject.IsSubactionUsage()),
            // ownedEndFeature->size() = 2
            new GeneratedRuleGuard("checkInterfaceDefinitionBinarySpecialization", element => element is SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceDefinition guardSubject && ((SysML2.NET.Core.POCO.Core.Types.IType)guardSubject).ownedEndFeature.Count == 2),
            // ownedEndFeature->size() = 2
            new GeneratedRuleGuard("checkInterfaceUsageBinarySpecialization", element => element is SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage guardSubject && ((SysML2.NET.Core.POCO.Core.Types.IType)guardSubject).ownedEndFeature.Count == 2),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(ItemDefinition) or owningType.oclIsKindOf(ItemUsage))
            new GeneratedRuleGuard("checkItemUsageSubitemSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Items.IItemUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.Items.IItemDefinition or SysML2.NET.Core.POCO.Systems.Items.IItemUsage }),
            // isIndividual
            new GeneratedRuleGuard("checkOccurrenceDefinitionIndividualSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition { IsIndividual: true }),
            // portionKind = PortionKind::snapshot
            new GeneratedRuleGuard("checkOccurrenceUsageSnapshotSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage { PortionKind: SysML2.NET.Core.Systems.Occurrences.PortionKind.Snapshot }),
            // portionKind = PortionKind::timeslice
            new GeneratedRuleGuard("checkOccurrenceUsageTimeSliceSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage { PortionKind: SysML2.NET.Core.Systems.Occurrences.PortionKind.Timeslice }),
            // owningFeatureMembership <> null and owningFeatureMembership.oclIsKindOf(StakeholderMembership)
            new GeneratedRuleGuard("checkPartUsageStakeholderSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Parts.IPartUsage { owningFeatureMembership: SysML2.NET.Core.POCO.Systems.Requirements.IStakeholderMembership }),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(ItemDefinition) or owningType.oclIsKindOf(ItemUsage))
            new GeneratedRuleGuard("checkPartUsageSubpartSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Parts.IPartUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.Items.IItemDefinition or SysML2.NET.Core.POCO.Systems.Items.IItemUsage }),
            // owningType <> null and (owningType.oclIsKindOf(PartDefinition) or owningType.oclIsKindOf(PartUsage))
            new GeneratedRuleGuard("checkPerformActionUsageSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage { owningType: SysML2.NET.Core.POCO.Systems.Parts.IPartDefinition or SysML2.NET.Core.POCO.Systems.Parts.IPartUsage }),
            // owningType <> null and (owningType.oclIsKindOf(PartDefinition) or owningType.oclIsKindOf(PartUsage))
            new GeneratedRuleGuard("checkPortUsageOwnedPortSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Ports.IPortUsage { owningType: SysML2.NET.Core.POCO.Systems.Parts.IPartDefinition or SysML2.NET.Core.POCO.Systems.Parts.IPartUsage }),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(PortDefinition) or owningType.oclIsKindOf(PortUsage))
            new GeneratedRuleGuard("checkPortUsageSubportSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Ports.IPortUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.Ports.IPortDefinition or SysML2.NET.Core.POCO.Systems.Ports.IPortUsage }),
            // owningType <> null and (owningType.oclIsKindOf(RenderingDefinition) or owningType.oclIsKindOf(RenderingUsage))
            new GeneratedRuleGuard("checkRenderingUsageSubrenderingSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage { owningType: SysML2.NET.Core.POCO.Systems.Views.IRenderingDefinition or SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage }),
            // owningFeatureMembership <> null and owningFeatureMembership.oclIsKindOf(RequirementVerificationMembership)
            new GeneratedRuleGuard("checkRequirementUsageRequirementVerificationSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage { owningFeatureMembership: SysML2.NET.Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership }),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(RequirementDefinition) or owningType.oclIsKindOf(RequirementUsage))
            new GeneratedRuleGuard("checkRequirementUsageSubrequirementSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.Requirements.IRequirementDefinition or SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage }),
            // isSubactionUsage()
            new GeneratedRuleGuard("checkSendActionUsageSubactionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage guardSubject && guardSubject.IsSubactionUsage()),
            // isSubstateUsage(false)
            new GeneratedRuleGuard("checkStateUsageExclusiveStateSpecialization", element => element is SysML2.NET.Core.POCO.Systems.States.IStateUsage guardSubject && guardSubject.IsSubstateUsage(false)),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(PartDefinition) or owningType.oclIsKindOf(PartUsage))
            new GeneratedRuleGuard("checkStateUsageOwnedStateSpecialization", element => element is SysML2.NET.Core.POCO.Systems.States.IStateUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.Parts.IPartDefinition or SysML2.NET.Core.POCO.Systems.Parts.IPartUsage }),
            // isSubstateUsage(true)
            new GeneratedRuleGuard("checkStateUsageSubstateSpecialization", element => element is SysML2.NET.Core.POCO.Systems.States.IStateUsage guardSubject && guardSubject.IsSubstateUsage(true)),
            // owningType <> null and (owningType.oclIsKindOf(Behavior) or owningType.oclIsKindOf(Step))
            new GeneratedRuleGuard("checkStepEnclosedPerformanceSpecialization", element => element is SysML2.NET.Core.POCO.Kernel.Behaviors.IStep { owningType: SysML2.NET.Core.POCO.Kernel.Behaviors.IBehavior or SysML2.NET.Core.POCO.Kernel.Behaviors.IStep }),
            // isSubactionUsage()
            new GeneratedRuleGuard("checkTerminateActionUsageSubactionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.ITerminateActionUsage guardSubject && guardSubject.IsSubactionUsage()),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(UseCaseDefinition) or owningType.oclIsKindOf(UseCaseUsage))
            new GeneratedRuleGuard("checkUseCaseUsageSubUseCaseSpecialization", element => element is SysML2.NET.Core.POCO.Systems.UseCases.IUseCaseUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.UseCases.IUseCaseDefinition or SysML2.NET.Core.POCO.Systems.UseCases.IUseCaseUsage }),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(VerificationCaseDefinition) or owningType.oclIsKindOf(VerificationCaseUsage))
            new GeneratedRuleGuard("checkVerificationCaseUsageSubVerificationCaseSpecialization", element => element is SysML2.NET.Core.POCO.Systems.VerificationCases.IVerificationCaseUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.VerificationCases.IVerificationCaseDefinition or SysML2.NET.Core.POCO.Systems.VerificationCases.IVerificationCaseUsage }),
            // owningType <> null and (owningType.oclIsKindOf(ViewDefinition) or owningType.oclIsKindOf(ViewUsage))
            new GeneratedRuleGuard("checkViewUsageSubviewSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Views.IViewUsage { owningType: SysML2.NET.Core.POCO.Systems.Views.IViewDefinition or SysML2.NET.Core.POCO.Systems.Views.IViewUsage }),
            // isComposite and owningType <> null and (owningType.oclIsKindOf(ViewDefinition) or owningType.oclIsKindOf(ViewUsage))
            new GeneratedRuleGuard("checkViewpointUsageViewpointSatisfactionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Views.IViewpointUsage { IsComposite: true, owningType: SysML2.NET.Core.POCO.Systems.Views.IViewDefinition or SysML2.NET.Core.POCO.Systems.Views.IViewUsage }),
            // isSubactionUsage()
            new GeneratedRuleGuard("checkWhileLoopActionUsageSubactionSpecialization", element => element is SysML2.NET.Core.POCO.Systems.Actions.IWhileLoopActionUsage guardSubject && guardSubject.IsSubactionUsage()),
        ];
    }
}
