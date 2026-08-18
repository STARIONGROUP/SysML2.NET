// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipTable.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The kind of <c>Relationship</c> that is implied to satisfy a semantic constraint.
    /// </summary>
    public enum ImpliedRelationshipKind
    {
        /// <summary>
        /// A <c>Subclassification</c>, implied for a Classifier.
        /// </summary>
        Subclassification,

        /// <summary>
        /// A <c>Subsetting</c>, implied for a Feature.
        /// </summary>
        Subsetting
    }

    /// <summary>
    /// A single implied library <c>Specialization</c>, as required by one semantic constraint of the
    /// KerML/SysML abstract syntax.
    /// </summary>
    public readonly struct ImpliedLibrarySpecialization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImpliedLibrarySpecialization"/> struct.
        /// </summary>
        /// <param name="constraintName">
        /// The name of the semantic constraint the rule was extracted from
        /// </param>
        /// <param name="targetLibraryName">
        /// The qualified name of the library Type that must be specialized
        /// </param>
        /// <param name="declaringMetaclassName">
        /// The metaclass the constraint is declared on, which decides <see cref="Kind"/>
        /// </param>
        /// <param name="requiresGuard">
        /// Whether the constraint's OCL guards the specialization
        /// </param>
        public ImpliedLibrarySpecialization(string constraintName, string targetLibraryName, string declaringMetaclassName, bool requiresGuard)
        {
            this.ConstraintName = constraintName;
            this.TargetLibraryName = targetLibraryName;
            this.DeclaringMetaclassName = declaringMetaclassName;
            this.RequiresGuard = requiresGuard;
        }

        /// <summary>
        /// Gets the name of the semantic constraint the rule was extracted from.
        /// </summary>
        public string ConstraintName { get; }

        /// <summary>
        /// Gets the qualified name of the library Type that must be specialized.
        /// </summary>
        public string TargetLibraryName { get; }

        /// <summary>
        /// Gets the metaclass the constraint is declared on. The Relationship kind is decided from this
        /// name, not from the metaclass that inherits the constraint.
        /// </summary>
        public string DeclaringMetaclassName { get; }

        /// <summary>
        /// Gets a value indicating whether the constraint's OCL guards the specialization, in which case the
        /// rule applies only when the hand-written predicate for <see cref="ConstraintName"/> says so.
        /// </summary>
        public bool RequiresGuard { get; }

        /// <summary>
        /// Gets the kind of Relationship to imply, decided by whether the declaring metaclass is a
        /// Classifier or a Feature.
        /// </summary>
        public ImpliedRelationshipKind Kind =>
            ImpliedRelationshipTable.SubclassificationMetaclasses.Contains(this.DeclaringMetaclassName)
                ? ImpliedRelationshipKind.Subclassification
                : ImpliedRelationshipKind.Subsetting;
    }

    /// <summary>
    /// The table of implied library <c>Specializations</c> that KerML 1.0 §8.4.2 allows a tool to insert to
    /// satisfy the specialization constraints of the abstract syntax.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The metaclass and the library target of each row come from the constraint's OCL body in the UML XMI.
    /// The <see cref="ImpliedRelationshipKind"/> does NOT: the OCL says only what to specialize, never
    /// whether the implied Relationship is a Subclassification or a Subsetting. That is stated only in the
    /// specification tables — KerML 1.0 Table 8 (§8.4.3.1.1) and Table 10 (§8.4.4.1), and SysML 2.0
    /// Tables 31-33 — so <see cref="SubclassificationMetaclasses"/> below is transcribed by hand from those
    /// tables and lives in the Handlebars template, not in the generator.
    /// </para>
    /// <para>
    /// Rows are NOT reduced against the §8.4.2 redundancy rules. Rule 1 suppresses an implied Specialization
    /// whose general Type is a supertype of another applicable one, and deciding that needs the library
    /// Types resolved — they are not present in the metamodel XMI. The reduction therefore belongs to the
    /// caller, which must also apply rule 2 (de-duplicate identical targets). Neither rule applies to
    /// Redefinitions.
    /// </para>
    /// </remarks>
    public static class ImpliedRelationshipTable
    {
        /// <summary>
        /// The metaclasses whose implied Specialization is a <c>Subclassification</c> rather than a
        /// <c>Subsetting</c> — that is, the Classifiers.
        /// </summary>
        /// <remarks>
        /// HAND-MAINTAINED. This is the one part of the table that cannot be derived from the OCL; it is
        /// transcribed from KerML Table 8 / Table 10 and SysML Tables 31-33. KerML Table 8 note 1 is the
        /// reason <c>Type</c> is absent: <c>checkTypeSpecialization</c> applies to every Type, but the
        /// Subclassification is only implied for Classifiers. Anything not listed here is a Feature and
        /// implies a Subsetting.
        /// </remarks>
        internal static readonly HashSet<string> SubclassificationMetaclasses =
        [
            "ActionDefinition",
            "AllocationDefinition",
            "AnalysisCaseDefinition",
            "Association",
            "AssociationStructure",
            "Behavior",
            "CalculationDefinition",
            "CaseDefinition",
            "Class",
            "ConcernDefinition",
            "ConnectionDefinition",
            "ConstraintDefinition",
            "DataType",
            "FlowDefinition",
            "Function",
            "InterfaceDefinition",
            "ItemDefinition",
            "Metaclass",
            "MetadataDefinition",
            "OccurrenceDefinition",
            "PartDefinition",
            "PortDefinition",
            "Predicate",
            "RenderingDefinition",
            "RequirementDefinition",
            "StateDefinition",
            "Structure",
            "UseCaseDefinition",
            "VerificationCaseDefinition",
            "ViewDefinition",
            "ViewpointDefinition"
        ];

        /// <summary>
        /// The semantic constraints that are NOT represented in this table, with the reason. Emitted so that
        /// no constraint of KerML §8.4.2 is silently dropped while its hand-coded arm is outstanding.
        /// </summary>
        public static IReadOnlyList<string> NotCovered { get; } =
        [
            "AcceptActionUsage.checkAcceptActionUsageReceiverBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "ActionUsage.checkActionUsageStateActionRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "AssertConstraintUsage.checkAssertConstraintUsageSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "AssignmentActionUsage.checkAssignmentActionUsageAccessedFeatureRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "AssignmentActionUsage.checkAssignmentActionUsageReferentRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "AssignmentActionUsage.checkAssignmentActionUsageStartingAtRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "Connector.checkConnectorTypeFeaturing (TypeFeaturing) - OCL is not a specializesFromLibrary call",
            "ConstraintUsage.checkConstraintUsageRequirementConstraintSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "ConstructorExpression.checkConstructorExpressionResultDefaultValueBindingConnector (BindingConnector) - specification body is TBD",
            "ConstructorExpression.checkConstructorExpressionResultFeatureRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "ConstructorExpression.checkConstructorExpressionResultSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "ConstructorExpression.checkConstructorExpressionSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "DecisionNode.checkDecisionNodeOutgoingSuccessionSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Expression.checkExpressionResultBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "Expression.checkExpressionTypeFeaturing (TypeFeaturing) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureCrossingSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureEndRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureFeatureMembershipTypeFeaturing (TypeFeaturing) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureFlowFeatureRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureOwnedCrossFeatureRedefinitionSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureOwnedCrossFeatureSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureOwnedCrossFeatureTypeFeaturing (TypeFeaturing) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureParameterRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureResultRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "Feature.checkFeatureValuationSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "FeatureChainExpression.checkFeatureChainExpressionResultSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "FeatureChainExpression.checkFeatureChainExpressionSourceTargetRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "FeatureChainExpression.checkFeatureChainExpressionTargetRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "FeatureReferenceExpression.checkFeatureReferenceExpressionBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "FeatureReferenceExpression.checkFeatureReferenceExpressionResultSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "FeatureValue.checkFeatureValueBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "ForLoopActionUsage.checkForLoopActionUsageVarRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "Function.checkFunctionResultBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "IfActionUsage.checkIfActionUsageSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "IndexExpression.checkIndexExpressionResultSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Invariant.checkInvariantSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "InvocationExpression.checkInvocationExpressionBehaviorBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "InvocationExpression.checkInvocationExpressionBehaviorResultSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "InvocationExpression.checkInvocationExpressionDefaultValueBindingConnector (BindingConnector) - specification body is TBD",
            "InvocationExpression.checkInvocationExpressionSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "MergeNode.checkMergeNodeIncomingSuccessionSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "MetadataFeature.checkMetadataFeatureSemanticSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Multiplicity.checkMultiplicityTypeFeaturing (TypeFeaturing) - OCL is not a specializesFromLibrary call",
            "MultiplicityRange.checkMultiplicityRangeExpressionTypeFeaturing (TypeFeaturing) - OCL is not a specializesFromLibrary call",
            "OccurrenceDefinition.checkOccurrenceDefinitionMultiplicitySpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "PartUsage.checkPartUsageActorSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "PayloadFeature.checkPayloadFeatureRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "RenderingUsage.checkRenderingUsageRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "RequirementUsage.checkRequirementUsageObjectiveRedefinition (Redefinition) - OCL is not a specializesFromLibrary call",
            "SatisfyRequirementUsage.checkSatisfyRequirementUsageBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "SatisfyRequirementUsage.checkSatisfyRequirementUsageSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "SelectExpression.checkSelectExpressionResultSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "TransitionUsage.checkTransitionUsagePayloadSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "TransitionUsage.checkTransitionUsageSourceBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "TransitionUsage.checkTransitionUsageSuccessionBindingConnector (BindingConnector) - OCL is not a specializesFromLibrary call",
            "TransitionUsage.checkTransitionUsageSuccessionSourceSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "TransitionUsage.checkTransitionUsageTransitionFeatureSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Usage.checkUsageVariationDefinitionSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Usage.checkUsageVariationUsageSpecialization (Specialization) - OCL is not a specializesFromLibrary call",
            "Usage.checkUsageVariationUsageTypeFeaturing (TypeFeaturing) - OCL is not a specializesFromLibrary call",
        ];

        /// <summary>
        /// The name of every semantic constraint declared in the abstract syntax, covered or not.
        /// </summary>
        public static IReadOnlyList<string> AllConstraintNames { get; } =
        [
            "checkAcceptActionUsageReceiverBindingConnector",
            "checkAcceptActionUsageSpecialization",
            "checkAcceptActionUsageSubactionSpecialization",
            "checkAcceptActionUsageTriggerActionSpecialization",
            "checkActionDefinitionSpecialization",
            "checkActionUsageOwnedActionSpecialization",
            "checkActionUsageSpecialization",
            "checkActionUsageStateActionRedefinition",
            "checkActionUsageSubactionSpecialization",
            "checkAllocationDefinitionSpecialization",
            "checkAllocationUsageSpecialization",
            "checkAnalysisCaseDefinitionSpecialization",
            "checkAnalysisCaseUsageSpecialization",
            "checkAnalysisCaseUsageSubAnalysisCaseSpecialization",
            "checkAssertConstraintUsageSpecialization",
            "checkAssignmentActionUsageAccessedFeatureRedefinition",
            "checkAssignmentActionUsageReferentRedefinition",
            "checkAssignmentActionUsageSpecialization",
            "checkAssignmentActionUsageStartingAtRedefinition",
            "checkAssignmentActionUsageSubactionSpecialization",
            "checkAssociationBinarySpecialization",
            "checkAssociationSpecialization",
            "checkAssociationStructureBinarySpecialization",
            "checkAssociationStructureSpecialization",
            "checkAttributeUsageSpecialization",
            "checkBehaviorSpecialization",
            "checkBindingConnectorSpecialization",
            "checkBooleanExpressionSpecialization",
            "checkCalculationDefinitionSpecialization",
            "checkCalculationUsageSpecialization",
            "checkCalculationUsageSubcalculationSpecialization",
            "checkCaseDefinitionSpecialization",
            "checkCaseUsageSpecialization",
            "checkCaseUsageSubcaseSpecialization",
            "checkClassSpecialization",
            "checkConcernDefinitionSpecialization",
            "checkConcernUsageFramedConcernSpecialization",
            "checkConcernUsageSpecialization",
            "checkConnectionDefinitionBinarySpecialization",
            "checkConnectionDefinitionSpecializations",
            "checkConnectionUsageBinarySpecialization",
            "checkConnectionUsageSpecialization",
            "checkConnectorBinaryObjectSpecialization",
            "checkConnectorBinarySpecialization",
            "checkConnectorObjectSpecialization",
            "checkConnectorSpecialization",
            "checkConnectorTypeFeaturing",
            "checkConstraintDefinitionSpecialization",
            "checkConstraintUsageCheckedConstraintSpecialization",
            "checkConstraintUsageRequirementConstraintSpecialization",
            "checkConstraintUsageSpecialization",
            "checkConstructorExpressionResultDefaultValueBindingConnector",
            "checkConstructorExpressionResultFeatureRedefinition",
            "checkConstructorExpressionResultSpecialization",
            "checkConstructorExpressionSpecialization",
            "checkControlNodeSpecialization",
            "checkDataTypeSpecialization",
            "checkDecisionNodeOutgoingSuccessionSpecialization",
            "checkDecisionNodeSpecialization",
            "checkEventOccurrenceUsageSpecialization",
            "checkExhibitStateUsageSpecialization",
            "checkExpressionResultBindingConnector",
            "checkExpressionSpecialization",
            "checkExpressionTypeFeaturing",
            "checkFeatureChainExpressionResultSpecialization",
            "checkFeatureChainExpressionSourceTargetRedefinition",
            "checkFeatureChainExpressionTargetRedefinition",
            "checkFeatureCrossingSpecialization",
            "checkFeatureDataValueSpecialization",
            "checkFeatureEndRedefinition",
            "checkFeatureEndSpecialization",
            "checkFeatureFeatureMembershipTypeFeaturing",
            "checkFeatureFlowFeatureRedefinition",
            "checkFeatureObjectSpecialization",
            "checkFeatureOccurrenceSpecialization",
            "checkFeatureOwnedCrossFeatureRedefinitionSpecialization",
            "checkFeatureOwnedCrossFeatureSpecialization",
            "checkFeatureOwnedCrossFeatureTypeFeaturing",
            "checkFeatureParameterRedefinition",
            "checkFeaturePortionSpecialization",
            "checkFeatureReferenceExpressionBindingConnector",
            "checkFeatureReferenceExpressionResultSpecialization",
            "checkFeatureResultRedefinition",
            "checkFeatureSpecialization",
            "checkFeatureSubobjectSpecialization",
            "checkFeatureSuboccurrenceSpecialization",
            "checkFeatureValuationSpecialization",
            "checkFeatureValueBindingConnector",
            "checkFlowDefinitionBinarySpecialization",
            "checkFlowDefinitionSpecialization",
            "checkFlowSpecialization",
            "checkFlowUsageFlowSpecialization",
            "checkFlowUsageSpecialization",
            "checkFlowWithEndsSpecialization",
            "checkForLoopActionUsageSpecialization",
            "checkForLoopActionUsageSubactionSpecialization",
            "checkForLoopActionUsageVarRedefinition",
            "checkForkNodeSpecialization",
            "checkFunctionResultBindingConnector",
            "checkFunctionSpecialization",
            "checkIfActionUsageSpecialization",
            "checkIfActionUsageSubactionSpecialization",
            "checkIncludeUseCaseUsageSpecialization",
            "checkIndexExpressionResultSpecialization",
            "checkInterfaceDefinitionBinarySpecialization",
            "checkInterfaceDefinitionSpecialization",
            "checkInterfaceUsageBinarySpecialization",
            "checkInterfaceUsageSpecialization",
            "checkInvariantSpecialization",
            "checkInvocationExpressionBehaviorBindingConnector",
            "checkInvocationExpressionBehaviorResultSpecialization",
            "checkInvocationExpressionDefaultValueBindingConnector",
            "checkInvocationExpressionSpecialization",
            "checkItemDefinitionSpecialization",
            "checkItemUsageSpecialization",
            "checkItemUsageSubitemSpecialization",
            "checkJoinNodeSpecialization",
            "checkLiteralBooleanSpecialization",
            "checkLiteralExpressionSpecialization",
            "checkLiteralInfinitySpecialization",
            "checkLiteralIntegerSpecialization",
            "checkLiteralRationalSpecialization",
            "checkLiteralStringSpecialization",
            "checkMergeNodeIncomingSuccessionSpecialization",
            "checkMergeNodeSpecialization",
            "checkMetaclassSpecialization",
            "checkMetadataAccessExpressionSpecialization",
            "checkMetadataDefinitionSpecialization",
            "checkMetadataFeatureSemanticSpecialization",
            "checkMetadataFeatureSpecialization",
            "checkMetadataUsageSpecialization",
            "checkMultiplicityRangeExpressionTypeFeaturing",
            "checkMultiplicitySpecialization",
            "checkMultiplicityTypeFeaturing",
            "checkNullExpressionSpecialization",
            "checkOccurrenceDefinitionIndividualSpecialization",
            "checkOccurrenceDefinitionMultiplicitySpecialization",
            "checkOccurrenceUsageSnapshotSpecialization",
            "checkOccurrenceUsageSpecialization",
            "checkOccurrenceUsageSuboccurrenceSpecialization",
            "checkOccurrenceUsageTimeSliceSpecialization",
            "checkPartDefinitionSpecialization",
            "checkPartUsageActorSpecialization",
            "checkPartUsageSpecialization",
            "checkPartUsageStakeholderSpecialization",
            "checkPartUsageSubpartSpecialization",
            "checkPayloadFeatureRedefinition",
            "checkPerformActionUsageSpecialization",
            "checkPortDefinitionSpecialization",
            "checkPortUsageOwnedPortSpecialization",
            "checkPortUsageSpecialization",
            "checkPortUsageSubportSpecialization",
            "checkPredicateSpecialization",
            "checkRenderingDefinitionSpecialization",
            "checkRenderingUsageRedefinition",
            "checkRenderingUsageSpecialization",
            "checkRenderingUsageSubrenderingSpecialization",
            "checkRequirementDefinitionSpecialization",
            "checkRequirementUsageObjectiveRedefinition",
            "checkRequirementUsageRequirementVerificationSpecialization",
            "checkRequirementUsageSpecialization",
            "checkRequirementUsageSubrequirementSpecialization",
            "checkSatisfyRequirementUsageBindingConnector",
            "checkSatisfyRequirementUsageSpecialization",
            "checkSelectExpressionResultSpecialization",
            "checkSendActionUsageSpecialization",
            "checkSendActionUsageSubactionSpecialization",
            "checkStateDefinitionSpecialization",
            "checkStateUsageExclusiveStateSpecialization",
            "checkStateUsageOwnedStateSpecialization",
            "checkStateUsageSpecialization",
            "checkStateUsageSubstateSpecialization",
            "checkStepEnclosedPerformanceSpecialization",
            "checkStepOwnedPerformanceSpecialization",
            "checkStepSpecialization",
            "checkStepSubperformanceSpecialization",
            "checkStructureSpecialization",
            "checkSuccessionFlowSpecialization",
            "checkSuccessionFlowUsageSpecialization",
            "checkSuccessionSpecialization",
            "checkTerminateActionUsageSpecialization",
            "checkTerminateActionUsageSubactionSpecialization",
            "checkTransitionUsageActionSpecialization",
            "checkTransitionUsagePayloadSpecialization",
            "checkTransitionUsageSourceBindingConnector",
            "checkTransitionUsageSpecialization",
            "checkTransitionUsageStateSpecialization",
            "checkTransitionUsageSuccessionBindingConnector",
            "checkTransitionUsageSuccessionSourceSpecialization",
            "checkTransitionUsageTransitionFeatureSpecialization",
            "checkTypeSpecialization",
            "checkUsageVariationDefinitionSpecialization",
            "checkUsageVariationUsageSpecialization",
            "checkUsageVariationUsageTypeFeaturing",
            "checkUseCaseDefinitionSpecialization",
            "checkUseCaseUsageSpecialization",
            "checkUseCaseUsageSubUseCaseSpecialization",
            "checkVerificationCaseSpecialization",
            "checkVerificationCaseUsageSpecialization",
            "checkVerificationCaseUsageSubVerificationCaseSpecialization",
            "checkViewDefinitionSpecialization",
            "checkViewUsageSpecialization",
            "checkViewUsageSubviewSpecialization",
            "checkViewpointDefinitionSpecialization",
            "checkViewpointUsageSpecialization",
            "checkViewpointUsageViewpointSatisfactionSpecialization",
            "checkWhileLoopActionUsageSpecialization",
            "checkWhileLoopActionUsageSubactionSpecialization",
        ];

        /// <summary>
        /// The name of every constraint whose application is conditional, i.e. every row that requires a
        /// guard before its implied <c>Relationship</c> may be included.
        /// </summary>
        public static IReadOnlyList<string> AllConditionalConstraintNames { get; } =
        [
            "checkAcceptActionUsageSpecialization",
            "checkAcceptActionUsageSubactionSpecialization",
            "checkAcceptActionUsageTriggerActionSpecialization",
            "checkActionUsageOwnedActionSpecialization",
            "checkActionUsageSubactionSpecialization",
            "checkAnalysisCaseUsageSubAnalysisCaseSpecialization",
            "checkAssignmentActionUsageSubactionSpecialization",
            "checkAssociationBinarySpecialization",
            "checkAssociationStructureBinarySpecialization",
            "checkCalculationUsageSubcalculationSpecialization",
            "checkCaseUsageSubcaseSpecialization",
            "checkConcernUsageFramedConcernSpecialization",
            "checkConnectionDefinitionBinarySpecialization",
            "checkConnectionUsageBinarySpecialization",
            "checkConnectorBinaryObjectSpecialization",
            "checkConnectorBinarySpecialization",
            "checkConnectorObjectSpecialization",
            "checkConstraintUsageCheckedConstraintSpecialization",
            "checkEventOccurrenceUsageSpecialization",
            "checkExhibitStateUsageSpecialization",
            "checkFeatureDataValueSpecialization",
            "checkFeatureEndSpecialization",
            "checkFeatureObjectSpecialization",
            "checkFeatureOccurrenceSpecialization",
            "checkFeaturePortionSpecialization",
            "checkFeatureSubobjectSpecialization",
            "checkFeatureSuboccurrenceSpecialization",
            "checkFlowDefinitionBinarySpecialization",
            "checkFlowUsageFlowSpecialization",
            "checkFlowWithEndsSpecialization",
            "checkForLoopActionUsageSubactionSpecialization",
            "checkIfActionUsageSubactionSpecialization",
            "checkIncludeUseCaseUsageSpecialization",
            "checkInterfaceDefinitionBinarySpecialization",
            "checkInterfaceUsageBinarySpecialization",
            "checkItemUsageSubitemSpecialization",
            "checkOccurrenceDefinitionIndividualSpecialization",
            "checkOccurrenceUsageSnapshotSpecialization",
            "checkOccurrenceUsageSuboccurrenceSpecialization",
            "checkOccurrenceUsageTimeSliceSpecialization",
            "checkPartUsageStakeholderSpecialization",
            "checkPartUsageSubpartSpecialization",
            "checkPerformActionUsageSpecialization",
            "checkPortUsageOwnedPortSpecialization",
            "checkPortUsageSubportSpecialization",
            "checkRenderingUsageSubrenderingSpecialization",
            "checkRequirementUsageRequirementVerificationSpecialization",
            "checkRequirementUsageSubrequirementSpecialization",
            "checkSendActionUsageSubactionSpecialization",
            "checkStateUsageExclusiveStateSpecialization",
            "checkStateUsageOwnedStateSpecialization",
            "checkStateUsageSubstateSpecialization",
            "checkStepEnclosedPerformanceSpecialization",
            "checkStepOwnedPerformanceSpecialization",
            "checkStepSubperformanceSpecialization",
            "checkTerminateActionUsageSubactionSpecialization",
            "checkTransitionUsageActionSpecialization",
            "checkTransitionUsageStateSpecialization",
            "checkUseCaseUsageSubUseCaseSpecialization",
            "checkVerificationCaseUsageSubVerificationCaseSpecialization",
            "checkViewUsageSubviewSpecialization",
            "checkViewpointUsageViewpointSatisfactionSpecialization",
            "checkWhileLoopActionUsageSubactionSpecialization",
        ];

        /// <summary>
        /// The qualified name of every library Type targeted by a row of this table, without duplicates.
        /// </summary>
        /// <remarks>
        /// Every name here must resolve against a full model-library load, or the constraint that carries
        /// it can never be satisfied. Exposed so that resolution can be asserted for the whole table rather
        /// than only for the rows a given corpus happens to exercise.
        /// </remarks>
        public static IReadOnlyList<string> AllLibraryTargets { get; } =
        [
            "Actions::Action",
            "Actions::Action::acceptSubactions",
            "Actions::Action::assignments",
            "Actions::Action::controls",
            "Actions::Action::decisionTransitions",
            "Actions::Action::decisions",
            "Actions::Action::forLoops",
            "Actions::Action::forks",
            "Actions::Action::ifSubactions",
            "Actions::Action::joins",
            "Actions::Action::merges",
            "Actions::Action::subactions",
            "Actions::Action::terminateSubactions",
            "Actions::Action::whileLoops",
            "Actions::TransitionAction::accepter",
            "Actions::acceptActions",
            "Actions::actions",
            "Actions::assignmentActions",
            "Actions::forLoopActions",
            "Actions::sendActions",
            "Actions::terminateActions",
            "Actions::transitionActions",
            "Actions::whileLoopActions",
            "Allocations::Allocation",
            "Allocations::allocations",
            "AnalysisCases::AnalysisCase",
            "AnalysisCases::AnalysisCase::subAnalysisCases",
            "AnalysisCases::analysisCases",
            "Base::Anything",
            "Base::DataValue",
            "Base::dataValues",
            "Base::naturals",
            "Base::things",
            "Calculations::Calculation",
            "Calculations::Calculation::subcalculations",
            "Calculations::calculations",
            "Cases::Case",
            "Cases::Case::subcases",
            "Cases::cases",
            "Connections::BinaryConnection",
            "Connections::Connection",
            "Connections::binaryConnections",
            "Connections::connections",
            "Constraints::ConstraintCheck",
            "Constraints::constraintChecks",
            "Flows::Message",
            "Flows::MessageAction",
            "Flows::flows",
            "Flows::messages",
            "Flows::successionFlows",
            "Interfaces::BinaryInterface",
            "Interfaces::Interface",
            "Interfaces::binaryInterfaces",
            "Interfaces::interfaces",
            "Items::Item",
            "Items::Item::checkedConstraints",
            "Items::Item::subitems",
            "Items::Item::subparts",
            "Items::items",
            "Links::BinaryLink",
            "Links::Link",
            "Links::Link::participant",
            "Links::binaryLinks",
            "Links::links",
            "Links::selfLinks",
            "Metadata::MetadataItem",
            "Metadata::metadataItems",
            "Metaobjects::Metaobject",
            "Metaobjects::metaobjects",
            "Objects::BinaryLinkObject",
            "Objects::LinkObject",
            "Objects::Object",
            "Objects::Object::ownedPerformances",
            "Objects::binaryLinkObjects",
            "Objects::linkObjects",
            "Objects::objects",
            "Occurrences::Life",
            "Occurrences::Occurrence",
            "Occurrences::Occurrence::portions",
            "Occurrences::Occurrence::snapshots",
            "Occurrences::Occurrence::suboccurrences",
            "Occurrences::Occurrence::timeEnclosedOccurrences",
            "Occurrences::Occurrence::timeSlices",
            "Occurrences::happensBeforeLinks",
            "Occurrences::occurrences",
            "Parts::Part",
            "Parts::Part::exhibitedStates",
            "Parts::Part::ownedActions",
            "Parts::Part::ownedPorts",
            "Parts::Part::ownedStates",
            "Parts::Part::performedActions",
            "Parts::parts",
            "Performances::BooleanEvaluation",
            "Performances::Evaluation",
            "Performances::Performance",
            "Performances::Performance::enclosedPerformances",
            "Performances::Performance::subperformances",
            "Performances::booleanEvaluations",
            "Performances::evaluations",
            "Performances::literalBooleanEvaluations",
            "Performances::literalEvaluations",
            "Performances::literalIntegerEvaluations",
            "Performances::literalRationalEvaluations",
            "Performances::literalStringEvaluations",
            "Performances::metadataAccessEvaluations",
            "Performances::nullEvaluations",
            "Performances::performances",
            "Ports::Port",
            "Ports::Port::subports",
            "Ports::ports",
            "Requirements::ConcernCheck",
            "Requirements::RequirementCheck",
            "Requirements::RequirementCheck::concerns",
            "Requirements::RequirementCheck::stakeholders",
            "Requirements::RequirementCheck::subrequirements",
            "Requirements::concernChecks",
            "Requirements::requirementChecks",
            "States::StateAction",
            "States::StateAction::exclusiveStates",
            "States::StateAction::stateTransitions",
            "States::StateAction::substates",
            "States::stateActions",
            "Transfers::flowTransfers",
            "Transfers::flowTransfersBefore",
            "Transfers::transfers",
            "UseCases::UseCase",
            "UseCases::UseCase::includedUseCases",
            "UseCases::UseCase::subUseCases",
            "UseCases::useCases",
            "VerificationCases::VerificationCase",
            "VerificationCases::VerificationCase::obj::requirementVerifications",
            "VerificationCases::VerificationCase::subVerificationCases",
            "VerificationCases::verificationCases",
            "Views::Rendering",
            "Views::Rendering::subrenderings",
            "Views::View",
            "Views::View::subviews",
            "Views::View::viewpointSatisfactions",
            "Views::ViewpointCheck",
            "Views::renderings",
            "Views::viewpointChecks",
            "Views::views",
        ];

        /// <summary>
        /// Returns the implied library <c>Specializations</c> that apply to the supplied element, including
        /// those inherited from its supertypes in the metamodel.
        /// </summary>
        /// <param name="element">
        /// The <see cref="IElement"/> to query
        /// </param>
        /// <returns>
        /// The applicable rules, or an empty list when the metaclass carries none
        /// </returns>
        public static IReadOnlyList<ImpliedLibrarySpecialization> QueryImpliedLibrarySpecializations(IElement element)
        {
            return element switch
            {
                SysML2.NET.Core.POCO.Systems.Flows.ISuccessionFlowUsage => SuccessionFlowUsageRules,
                SysML2.NET.Core.POCO.Systems.Allocations.IAllocationDefinition => AllocationDefinitionRules,
                SysML2.NET.Core.POCO.Systems.UseCases.IIncludeUseCaseUsage => IncludeUseCaseUsageRules,
                SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceDefinition => InterfaceDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Connections.IConnectionDefinition => ConnectionDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Requirements.ISatisfyRequirementUsage => SatisfyRequirementUsageRules,
                SysML2.NET.Core.POCO.Systems.Allocations.IAllocationUsage => AllocationUsageRules,
                SysML2.NET.Core.POCO.Systems.AnalysisCases.IAnalysisCaseDefinition => AnalysisCaseDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Requirements.IConcernDefinition => ConcernDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Flows.IFlowDefinition => FlowDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage => FlowUsageRules,
                SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage => InterfaceUsageRules,
                SysML2.NET.Core.POCO.Systems.UseCases.IUseCaseDefinition => UseCaseDefinitionRules,
                SysML2.NET.Core.POCO.Systems.VerificationCases.IVerificationCaseDefinition => VerificationCaseDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Views.IViewpointDefinition => ViewpointDefinitionRules,
                SysML2.NET.Core.POCO.Systems.AnalysisCases.IAnalysisCaseUsage => AnalysisCaseUsageRules,
                SysML2.NET.Core.POCO.Systems.Constraints.IAssertConstraintUsage => AssertConstraintUsageRules,
                SysML2.NET.Core.POCO.Systems.Cases.ICaseDefinition => CaseDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Requirements.IConcernUsage => ConcernUsageRules,
                SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage => ConnectionUsageRules,
                SysML2.NET.Core.POCO.Systems.States.IExhibitStateUsage => ExhibitStateUsageRules,
                SysML2.NET.Core.POCO.Systems.Requirements.IRequirementDefinition => RequirementDefinitionRules,
                SysML2.NET.Core.POCO.Systems.UseCases.IUseCaseUsage => UseCaseUsageRules,
                SysML2.NET.Core.POCO.Systems.VerificationCases.IVerificationCaseUsage => VerificationCaseUsageRules,
                SysML2.NET.Core.POCO.Systems.Views.IViewpointUsage => ViewpointUsageRules,
                SysML2.NET.Core.POCO.Systems.Calculations.ICalculationDefinition => CalculationDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Cases.ICaseUsage => CaseUsageRules,
                SysML2.NET.Core.POCO.Systems.Constraints.IConstraintDefinition => ConstraintDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Metadata.IMetadataDefinition => MetadataDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Views.IRenderingDefinition => RenderingDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage => RequirementUsageRules,
                SysML2.NET.Core.POCO.Systems.Views.IViewDefinition => ViewDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Connections.IBindingConnectorAsUsage => BindingConnectorAsUsageRules,
                SysML2.NET.Core.POCO.Systems.Calculations.ICalculationUsage => CalculationUsageRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.ICollectExpression => CollectExpressionRules,
                SysML2.NET.Core.POCO.Systems.Ports.IConjugatedPortDefinition => ConjugatedPortDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage => ConstraintUsageRules,
                SysML2.NET.Core.POCO.Systems.Actions.IDecisionNode => DecisionNodeRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureChainExpression => FeatureChainExpressionRules,
                SysML2.NET.Core.POCO.Systems.Actions.IForLoopActionUsage => ForLoopActionUsageRules,
                SysML2.NET.Core.POCO.Systems.Actions.IForkNode => ForkNodeRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.IIndexExpression => IndexExpressionRules,
                SysML2.NET.Core.POCO.Systems.Actions.IJoinNode => JoinNodeRules,
                SysML2.NET.Core.POCO.Systems.Actions.IMergeNode => MergeNodeRules,
                SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage => MetadataUsageRules,
                SysML2.NET.Core.POCO.Systems.Parts.IPartDefinition => PartDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Actions.IPerformActionUsage => PerformActionUsageRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.ISelectExpression => SelectExpressionRules,
                SysML2.NET.Core.POCO.Systems.States.IStateDefinition => StateDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage => SuccessionAsUsageRules,
                SysML2.NET.Core.POCO.Kernel.Interactions.ISuccessionFlow => SuccessionFlowRules,
                SysML2.NET.Core.POCO.Systems.Actions.IWhileLoopActionUsage => WhileLoopActionUsageRules,
                SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage => AcceptActionUsageRules,
                SysML2.NET.Core.POCO.Systems.Actions.IActionDefinition => ActionDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage => AssignmentActionUsageRules,
                SysML2.NET.Core.POCO.Kernel.Associations.IAssociationStructure => AssociationStructureRules,
                SysML2.NET.Core.POCO.Systems.Actions.IControlNode => ControlNodeRules,
                SysML2.NET.Core.POCO.Systems.Actions.IIfActionUsage => IfActionUsageRules,
                SysML2.NET.Core.POCO.Kernel.Interactions.IInteraction => InteractionRules,
                SysML2.NET.Core.POCO.Systems.Items.IItemDefinition => ItemDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Actions.ILoopActionUsage => LoopActionUsageRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression => OperatorExpressionRules,
                SysML2.NET.Core.POCO.Systems.Ports.IPortDefinition => PortDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage => RenderingUsageRules,
                SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage => SendActionUsageRules,
                SysML2.NET.Core.POCO.Systems.States.IStateUsage => StateUsageRules,
                SysML2.NET.Core.POCO.Systems.Actions.ITerminateActionUsage => TerminateActionUsageRules,
                SysML2.NET.Core.POCO.Systems.States.ITransitionUsage => TransitionUsageRules,
                SysML2.NET.Core.POCO.Systems.Actions.ITriggerInvocationExpression => TriggerInvocationExpressionRules,
                SysML2.NET.Core.POCO.Systems.Views.IViewUsage => ViewUsageRules,
                SysML2.NET.Core.POCO.Systems.Actions.IActionUsage => ActionUsageRules,
                SysML2.NET.Core.POCO.Systems.Connections.IConnectorAsUsage => ConnectorAsUsageRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.IConstructorExpression => ConstructorExpressionRules,
                SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationDefinition => EnumerationDefinitionRules,
                SysML2.NET.Core.POCO.Kernel.Interactions.IFlow => FlowRules,
                SysML2.NET.Core.POCO.Kernel.Functions.IInvariant => InvariantRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.IInvocationExpression => InvocationExpressionRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralBoolean => LiteralBooleanRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralInfinity => LiteralInfinityRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralInteger => LiteralIntegerRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralRational => LiteralRationalRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralString => LiteralStringRules,
                SysML2.NET.Core.POCO.Systems.Parts.IPartUsage => PartUsageRules,
                SysML2.NET.Core.POCO.Kernel.Functions.IPredicate => PredicateRules,
                SysML2.NET.Core.POCO.Systems.Attributes.IAttributeDefinition => AttributeDefinitionRules,
                SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector => BindingConnectorRules,
                SysML2.NET.Core.POCO.Kernel.Functions.IBooleanExpression => BooleanExpressionRules,
                SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationUsage => EnumerationUsageRules,
                SysML2.NET.Core.POCO.Systems.Occurrences.IEventOccurrenceUsage => EventOccurrenceUsageRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression => FeatureReferenceExpressionRules,
                SysML2.NET.Core.POCO.Kernel.Functions.IFunction => FunctionRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.IInstantiationExpression => InstantiationExpressionRules,
                SysML2.NET.Core.POCO.Systems.Items.IItemUsage => ItemUsageRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralExpression => LiteralExpressionRules,
                SysML2.NET.Core.POCO.Kernel.Metadata.IMetaclass => MetaclassRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.IMetadataAccessExpression => MetadataAccessExpressionRules,
                SysML2.NET.Core.POCO.Kernel.Expressions.INullExpression => NullExpressionRules,
                SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition => OccurrenceDefinitionRules,
                SysML2.NET.Core.POCO.Systems.Ports.IPortUsage => PortUsageRules,
                SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession => SuccessionRules,
                SysML2.NET.Core.POCO.Kernel.Associations.IAssociation => AssociationRules,
                SysML2.NET.Core.POCO.Systems.Attributes.IAttributeUsage => AttributeUsageRules,
                SysML2.NET.Core.POCO.Kernel.Behaviors.IBehavior => BehaviorRules,
                SysML2.NET.Core.POCO.Kernel.Connectors.IConnector => ConnectorRules,
                SysML2.NET.Core.POCO.Kernel.Functions.IExpression => ExpressionRules,
                SysML2.NET.Core.POCO.Kernel.Metadata.IMetadataFeature => MetadataFeatureRules,
                SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange => MultiplicityRangeRules,
                SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage => OccurrenceUsageRules,
                SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage => ReferenceUsageRules,
                SysML2.NET.Core.POCO.Kernel.Structures.IStructure => StructureRules,
                SysML2.NET.Core.POCO.Kernel.Classes.IClass => ClassRules,
                SysML2.NET.Core.POCO.Kernel.DataTypes.IDataType => DataTypeRules,
                SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition => DefinitionRules,
                SysML2.NET.Core.POCO.Kernel.Interactions.IFlowEnd => FlowEndRules,
                SysML2.NET.Core.POCO.Core.Types.IMultiplicity => MultiplicityRules,
                SysML2.NET.Core.POCO.Kernel.Interactions.IPayloadFeature => PayloadFeatureRules,
                SysML2.NET.Core.POCO.Kernel.Behaviors.IStep => StepRules,
                SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage => UsageRules,
                SysML2.NET.Core.POCO.Core.Classifiers.IClassifier => ClassifierRules,
                SysML2.NET.Core.POCO.Core.Features.IFeature => FeatureRules,
                SysML2.NET.Core.POCO.Core.Types.IType => TypeRules,
                _ => []
            };
        }

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>SuccessionFlowUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] SuccessionFlowUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFlowSpecialization", "Transfers::transfers", "Flow", false),
            new("checkFlowUsageFlowSpecialization", "Flows::flows", "FlowUsage", true),
            new("checkFlowUsageSpecialization", "Flows::messages", "FlowUsage", false),
            new("checkFlowWithEndsSpecialization", "Transfers::flowTransfers", "Flow", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkSuccessionFlowSpecialization", "Transfers::flowTransfersBefore", "SuccessionFlow", false),
            new("checkSuccessionFlowUsageSpecialization", "Flows::successionFlows", "SuccessionFlowUsage", false),
            new("checkSuccessionSpecialization", "Occurrences::happensBeforeLinks", "Succession", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AllocationDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AllocationDefinitionRules =
        [
            new("checkAllocationDefinitionSpecialization", "Allocations::Allocation", "AllocationDefinition", false),
            new("checkAssociationBinarySpecialization", "Links::BinaryLink", "Association", true),
            new("checkAssociationSpecialization", "Links::Link", "Association", false),
            new("checkAssociationStructureBinarySpecialization", "Objects::BinaryLinkObject", "AssociationStructure", true),
            new("checkAssociationStructureSpecialization", "Objects::LinkObject", "AssociationStructure", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkConnectionDefinitionBinarySpecialization", "Connections::BinaryConnection", "ConnectionDefinition", true),
            new("checkConnectionDefinitionSpecializations", "Connections::Connection", "ConnectionDefinition", false),
            new("checkItemDefinitionSpecialization", "Items::Item", "ItemDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPartDefinitionSpecialization", "Parts::Part", "PartDefinition", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>IncludeUseCaseUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] IncludeUseCaseUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkCalculationUsageSpecialization", "Calculations::calculations", "CalculationUsage", false),
            new("checkCalculationUsageSubcalculationSpecialization", "Calculations::Calculation::subcalculations", "CalculationUsage", true),
            new("checkCaseUsageSpecialization", "Cases::cases", "CaseUsage", false),
            new("checkCaseUsageSubcaseSpecialization", "Cases::Case::subcases", "CaseUsage", true),
            new("checkEventOccurrenceUsageSpecialization", "Occurrences::Occurrence::timeEnclosedOccurrences", "EventOccurrenceUsage", true),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkIncludeUseCaseUsageSpecialization", "UseCases::UseCase::includedUseCases", "IncludeUseCaseUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPerformActionUsageSpecialization", "Parts::Part::performedActions", "PerformActionUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkUseCaseUsageSpecialization", "UseCases::useCases", "UseCaseUsage", false),
            new("checkUseCaseUsageSubUseCaseSpecialization", "UseCases::UseCase::subUseCases", "UseCaseUsage", true),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>InterfaceDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] InterfaceDefinitionRules =
        [
            new("checkAssociationBinarySpecialization", "Links::BinaryLink", "Association", true),
            new("checkAssociationSpecialization", "Links::Link", "Association", false),
            new("checkAssociationStructureBinarySpecialization", "Objects::BinaryLinkObject", "AssociationStructure", true),
            new("checkAssociationStructureSpecialization", "Objects::LinkObject", "AssociationStructure", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkConnectionDefinitionBinarySpecialization", "Connections::BinaryConnection", "ConnectionDefinition", true),
            new("checkConnectionDefinitionSpecializations", "Connections::Connection", "ConnectionDefinition", false),
            new("checkInterfaceDefinitionBinarySpecialization", "Interfaces::BinaryInterface", "InterfaceDefinition", true),
            new("checkInterfaceDefinitionSpecialization", "Interfaces::Interface", "InterfaceDefinition", false),
            new("checkItemDefinitionSpecialization", "Items::Item", "ItemDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPartDefinitionSpecialization", "Parts::Part", "PartDefinition", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConnectionDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConnectionDefinitionRules =
        [
            new("checkAssociationBinarySpecialization", "Links::BinaryLink", "Association", true),
            new("checkAssociationSpecialization", "Links::Link", "Association", false),
            new("checkAssociationStructureBinarySpecialization", "Objects::BinaryLinkObject", "AssociationStructure", true),
            new("checkAssociationStructureSpecialization", "Objects::LinkObject", "AssociationStructure", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkConnectionDefinitionBinarySpecialization", "Connections::BinaryConnection", "ConnectionDefinition", true),
            new("checkConnectionDefinitionSpecializations", "Connections::Connection", "ConnectionDefinition", false),
            new("checkItemDefinitionSpecialization", "Items::Item", "ItemDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPartDefinitionSpecialization", "Parts::Part", "PartDefinition", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>SatisfyRequirementUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] SatisfyRequirementUsageRules =
        [
            new("checkBooleanExpressionSpecialization", "Performances::booleanEvaluations", "BooleanExpression", false),
            new("checkConstraintUsageCheckedConstraintSpecialization", "Items::Item::checkedConstraints", "ConstraintUsage", true),
            new("checkConstraintUsageSpecialization", "Constraints::constraintChecks", "ConstraintUsage", false),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkRequirementUsageRequirementVerificationSpecialization", "VerificationCases::VerificationCase::obj::requirementVerifications", "RequirementUsage", true),
            new("checkRequirementUsageSpecialization", "Requirements::requirementChecks", "RequirementUsage", false),
            new("checkRequirementUsageSubrequirementSpecialization", "Requirements::RequirementCheck::subrequirements", "RequirementUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AllocationUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AllocationUsageRules =
        [
            new("checkAllocationUsageSpecialization", "Allocations::allocations", "AllocationUsage", false),
            new("checkConnectionUsageBinarySpecialization", "Connections::binaryConnections", "ConnectionUsage", true),
            new("checkConnectionUsageSpecialization", "Connections::connections", "ConnectionUsage", false),
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkItemUsageSpecialization", "Items::items", "ItemUsage", false),
            new("checkItemUsageSubitemSpecialization", "Items::Item::subitems", "ItemUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPartUsageSpecialization", "Parts::parts", "PartUsage", false),
            new("checkPartUsageStakeholderSpecialization", "Requirements::RequirementCheck::stakeholders", "PartUsage", true),
            new("checkPartUsageSubpartSpecialization", "Items::Item::subparts", "PartUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AnalysisCaseDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AnalysisCaseDefinitionRules =
        [
            new("checkActionDefinitionSpecialization", "Actions::Action", "ActionDefinition", false),
            new("checkAnalysisCaseDefinitionSpecialization", "AnalysisCases::AnalysisCase", "AnalysisCaseDefinition", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkCalculationDefinitionSpecialization", "Calculations::Calculation", "CalculationDefinition", false),
            new("checkCaseDefinitionSpecialization", "Cases::Case", "CaseDefinition", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConcernDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConcernDefinitionRules =
        [
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkConcernDefinitionSpecialization", "Requirements::ConcernCheck", "ConcernDefinition", false),
            new("checkConstraintDefinitionSpecialization", "Constraints::ConstraintCheck", "ConstraintDefinition", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPredicateSpecialization", "Performances::BooleanEvaluation", "Predicate", false),
            new("checkRequirementDefinitionSpecialization", "Requirements::RequirementCheck", "RequirementDefinition", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>FlowDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] FlowDefinitionRules =
        [
            new("checkActionDefinitionSpecialization", "Actions::Action", "ActionDefinition", false),
            new("checkAssociationBinarySpecialization", "Links::BinaryLink", "Association", true),
            new("checkAssociationSpecialization", "Links::Link", "Association", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkFlowDefinitionBinarySpecialization", "Flows::Message", "FlowDefinition", true),
            new("checkFlowDefinitionSpecialization", "Flows::MessageAction", "FlowDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>FlowUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] FlowUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFlowSpecialization", "Transfers::transfers", "Flow", false),
            new("checkFlowUsageFlowSpecialization", "Flows::flows", "FlowUsage", true),
            new("checkFlowUsageSpecialization", "Flows::messages", "FlowUsage", false),
            new("checkFlowWithEndsSpecialization", "Transfers::flowTransfers", "Flow", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>InterfaceUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] InterfaceUsageRules =
        [
            new("checkConnectionUsageBinarySpecialization", "Connections::binaryConnections", "ConnectionUsage", true),
            new("checkConnectionUsageSpecialization", "Connections::connections", "ConnectionUsage", false),
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkInterfaceUsageBinarySpecialization", "Interfaces::binaryInterfaces", "InterfaceUsage", true),
            new("checkInterfaceUsageSpecialization", "Interfaces::interfaces", "InterfaceUsage", false),
            new("checkItemUsageSpecialization", "Items::items", "ItemUsage", false),
            new("checkItemUsageSubitemSpecialization", "Items::Item::subitems", "ItemUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPartUsageSpecialization", "Parts::parts", "PartUsage", false),
            new("checkPartUsageStakeholderSpecialization", "Requirements::RequirementCheck::stakeholders", "PartUsage", true),
            new("checkPartUsageSubpartSpecialization", "Items::Item::subparts", "PartUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>UseCaseDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] UseCaseDefinitionRules =
        [
            new("checkActionDefinitionSpecialization", "Actions::Action", "ActionDefinition", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkCalculationDefinitionSpecialization", "Calculations::Calculation", "CalculationDefinition", false),
            new("checkCaseDefinitionSpecialization", "Cases::Case", "CaseDefinition", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkUseCaseDefinitionSpecialization", "UseCases::UseCase", "UseCaseDefinition", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>VerificationCaseDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] VerificationCaseDefinitionRules =
        [
            new("checkActionDefinitionSpecialization", "Actions::Action", "ActionDefinition", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkCalculationDefinitionSpecialization", "Calculations::Calculation", "CalculationDefinition", false),
            new("checkCaseDefinitionSpecialization", "Cases::Case", "CaseDefinition", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkVerificationCaseSpecialization", "VerificationCases::VerificationCase", "VerificationCaseDefinition", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ViewpointDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ViewpointDefinitionRules =
        [
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkConstraintDefinitionSpecialization", "Constraints::ConstraintCheck", "ConstraintDefinition", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPredicateSpecialization", "Performances::BooleanEvaluation", "Predicate", false),
            new("checkRequirementDefinitionSpecialization", "Requirements::RequirementCheck", "RequirementDefinition", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkViewpointDefinitionSpecialization", "Views::ViewpointCheck", "ViewpointDefinition", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AnalysisCaseUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AnalysisCaseUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkAnalysisCaseUsageSpecialization", "AnalysisCases::analysisCases", "AnalysisCaseUsage", false),
            new("checkAnalysisCaseUsageSubAnalysisCaseSpecialization", "AnalysisCases::AnalysisCase::subAnalysisCases", "AnalysisCaseUsage", true),
            new("checkCalculationUsageSpecialization", "Calculations::calculations", "CalculationUsage", false),
            new("checkCalculationUsageSubcalculationSpecialization", "Calculations::Calculation::subcalculations", "CalculationUsage", true),
            new("checkCaseUsageSpecialization", "Cases::cases", "CaseUsage", false),
            new("checkCaseUsageSubcaseSpecialization", "Cases::Case::subcases", "CaseUsage", true),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AssertConstraintUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AssertConstraintUsageRules =
        [
            new("checkBooleanExpressionSpecialization", "Performances::booleanEvaluations", "BooleanExpression", false),
            new("checkConstraintUsageCheckedConstraintSpecialization", "Items::Item::checkedConstraints", "ConstraintUsage", true),
            new("checkConstraintUsageSpecialization", "Constraints::constraintChecks", "ConstraintUsage", false),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>CaseDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] CaseDefinitionRules =
        [
            new("checkActionDefinitionSpecialization", "Actions::Action", "ActionDefinition", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkCalculationDefinitionSpecialization", "Calculations::Calculation", "CalculationDefinition", false),
            new("checkCaseDefinitionSpecialization", "Cases::Case", "CaseDefinition", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConcernUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConcernUsageRules =
        [
            new("checkBooleanExpressionSpecialization", "Performances::booleanEvaluations", "BooleanExpression", false),
            new("checkConcernUsageFramedConcernSpecialization", "Requirements::RequirementCheck::concerns", "ConcernUsage", true),
            new("checkConcernUsageSpecialization", "Requirements::concernChecks", "ConcernUsage", false),
            new("checkConstraintUsageCheckedConstraintSpecialization", "Items::Item::checkedConstraints", "ConstraintUsage", true),
            new("checkConstraintUsageSpecialization", "Constraints::constraintChecks", "ConstraintUsage", false),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkRequirementUsageRequirementVerificationSpecialization", "VerificationCases::VerificationCase::obj::requirementVerifications", "RequirementUsage", true),
            new("checkRequirementUsageSpecialization", "Requirements::requirementChecks", "RequirementUsage", false),
            new("checkRequirementUsageSubrequirementSpecialization", "Requirements::RequirementCheck::subrequirements", "RequirementUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConnectionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConnectionUsageRules =
        [
            new("checkConnectionUsageBinarySpecialization", "Connections::binaryConnections", "ConnectionUsage", true),
            new("checkConnectionUsageSpecialization", "Connections::connections", "ConnectionUsage", false),
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkItemUsageSpecialization", "Items::items", "ItemUsage", false),
            new("checkItemUsageSubitemSpecialization", "Items::Item::subitems", "ItemUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPartUsageSpecialization", "Parts::parts", "PartUsage", false),
            new("checkPartUsageStakeholderSpecialization", "Requirements::RequirementCheck::stakeholders", "PartUsage", true),
            new("checkPartUsageSubpartSpecialization", "Items::Item::subparts", "PartUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ExhibitStateUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ExhibitStateUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkEventOccurrenceUsageSpecialization", "Occurrences::Occurrence::timeEnclosedOccurrences", "EventOccurrenceUsage", true),
            new("checkExhibitStateUsageSpecialization", "Parts::Part::exhibitedStates", "ExhibitStateUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPerformActionUsageSpecialization", "Parts::Part::performedActions", "PerformActionUsage", true),
            new("checkStateUsageExclusiveStateSpecialization", "States::StateAction::exclusiveStates", "StateUsage", true),
            new("checkStateUsageOwnedStateSpecialization", "Parts::Part::ownedStates", "StateUsage", true),
            new("checkStateUsageSpecialization", "States::stateActions", "StateUsage", false),
            new("checkStateUsageSubstateSpecialization", "States::StateAction::substates", "StateUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>RequirementDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] RequirementDefinitionRules =
        [
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkConstraintDefinitionSpecialization", "Constraints::ConstraintCheck", "ConstraintDefinition", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPredicateSpecialization", "Performances::BooleanEvaluation", "Predicate", false),
            new("checkRequirementDefinitionSpecialization", "Requirements::RequirementCheck", "RequirementDefinition", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>UseCaseUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] UseCaseUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkCalculationUsageSpecialization", "Calculations::calculations", "CalculationUsage", false),
            new("checkCalculationUsageSubcalculationSpecialization", "Calculations::Calculation::subcalculations", "CalculationUsage", true),
            new("checkCaseUsageSpecialization", "Cases::cases", "CaseUsage", false),
            new("checkCaseUsageSubcaseSpecialization", "Cases::Case::subcases", "CaseUsage", true),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkUseCaseUsageSpecialization", "UseCases::useCases", "UseCaseUsage", false),
            new("checkUseCaseUsageSubUseCaseSpecialization", "UseCases::UseCase::subUseCases", "UseCaseUsage", true),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>VerificationCaseUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] VerificationCaseUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkCalculationUsageSpecialization", "Calculations::calculations", "CalculationUsage", false),
            new("checkCalculationUsageSubcalculationSpecialization", "Calculations::Calculation::subcalculations", "CalculationUsage", true),
            new("checkCaseUsageSpecialization", "Cases::cases", "CaseUsage", false),
            new("checkCaseUsageSubcaseSpecialization", "Cases::Case::subcases", "CaseUsage", true),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkVerificationCaseUsageSpecialization", "VerificationCases::verificationCases", "VerificationCaseUsage", false),
            new("checkVerificationCaseUsageSubVerificationCaseSpecialization", "VerificationCases::VerificationCase::subVerificationCases", "VerificationCaseUsage", true),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ViewpointUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ViewpointUsageRules =
        [
            new("checkBooleanExpressionSpecialization", "Performances::booleanEvaluations", "BooleanExpression", false),
            new("checkConstraintUsageCheckedConstraintSpecialization", "Items::Item::checkedConstraints", "ConstraintUsage", true),
            new("checkConstraintUsageSpecialization", "Constraints::constraintChecks", "ConstraintUsage", false),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkRequirementUsageRequirementVerificationSpecialization", "VerificationCases::VerificationCase::obj::requirementVerifications", "RequirementUsage", true),
            new("checkRequirementUsageSpecialization", "Requirements::requirementChecks", "RequirementUsage", false),
            new("checkRequirementUsageSubrequirementSpecialization", "Requirements::RequirementCheck::subrequirements", "RequirementUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkViewpointUsageSpecialization", "Views::viewpointChecks", "ViewpointUsage", false),
            new("checkViewpointUsageViewpointSatisfactionSpecialization", "Views::View::viewpointSatisfactions", "ViewpointUsage", true),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>CalculationDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] CalculationDefinitionRules =
        [
            new("checkActionDefinitionSpecialization", "Actions::Action", "ActionDefinition", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkCalculationDefinitionSpecialization", "Calculations::Calculation", "CalculationDefinition", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>CaseUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] CaseUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkCalculationUsageSpecialization", "Calculations::calculations", "CalculationUsage", false),
            new("checkCalculationUsageSubcalculationSpecialization", "Calculations::Calculation::subcalculations", "CalculationUsage", true),
            new("checkCaseUsageSpecialization", "Cases::cases", "CaseUsage", false),
            new("checkCaseUsageSubcaseSpecialization", "Cases::Case::subcases", "CaseUsage", true),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConstraintDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConstraintDefinitionRules =
        [
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkConstraintDefinitionSpecialization", "Constraints::ConstraintCheck", "ConstraintDefinition", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPredicateSpecialization", "Performances::BooleanEvaluation", "Predicate", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>MetadataDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] MetadataDefinitionRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkItemDefinitionSpecialization", "Items::Item", "ItemDefinition", false),
            new("checkMetaclassSpecialization", "Metaobjects::Metaobject", "Metaclass", false),
            new("checkMetadataDefinitionSpecialization", "Metadata::MetadataItem", "MetadataDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>RenderingDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] RenderingDefinitionRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkItemDefinitionSpecialization", "Items::Item", "ItemDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPartDefinitionSpecialization", "Parts::Part", "PartDefinition", false),
            new("checkRenderingDefinitionSpecialization", "Views::Rendering", "RenderingDefinition", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>RequirementUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] RequirementUsageRules =
        [
            new("checkBooleanExpressionSpecialization", "Performances::booleanEvaluations", "BooleanExpression", false),
            new("checkConstraintUsageCheckedConstraintSpecialization", "Items::Item::checkedConstraints", "ConstraintUsage", true),
            new("checkConstraintUsageSpecialization", "Constraints::constraintChecks", "ConstraintUsage", false),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkRequirementUsageRequirementVerificationSpecialization", "VerificationCases::VerificationCase::obj::requirementVerifications", "RequirementUsage", true),
            new("checkRequirementUsageSpecialization", "Requirements::requirementChecks", "RequirementUsage", false),
            new("checkRequirementUsageSubrequirementSpecialization", "Requirements::RequirementCheck::subrequirements", "RequirementUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ViewDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ViewDefinitionRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkItemDefinitionSpecialization", "Items::Item", "ItemDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPartDefinitionSpecialization", "Parts::Part", "PartDefinition", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkViewDefinitionSpecialization", "Views::View", "ViewDefinition", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>BindingConnectorAsUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] BindingConnectorAsUsageRules =
        [
            new("checkBindingConnectorSpecialization", "Links::selfLinks", "BindingConnector", false),
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>CalculationUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] CalculationUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkCalculationUsageSpecialization", "Calculations::calculations", "CalculationUsage", false),
            new("checkCalculationUsageSubcalculationSpecialization", "Calculations::Calculation::subcalculations", "CalculationUsage", true),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>CollectExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] CollectExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConjugatedPortDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConjugatedPortDefinitionRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPortDefinitionSpecialization", "Ports::Port", "PortDefinition", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConstraintUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConstraintUsageRules =
        [
            new("checkBooleanExpressionSpecialization", "Performances::booleanEvaluations", "BooleanExpression", false),
            new("checkConstraintUsageCheckedConstraintSpecialization", "Items::Item::checkedConstraints", "ConstraintUsage", true),
            new("checkConstraintUsageSpecialization", "Constraints::constraintChecks", "ConstraintUsage", false),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>DecisionNode</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] DecisionNodeRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkControlNodeSpecialization", "Actions::Action::controls", "ControlNode", false),
            new("checkDecisionNodeSpecialization", "Actions::Action::decisions", "DecisionNode", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>FeatureChainExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] FeatureChainExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ForLoopActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ForLoopActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkForLoopActionUsageSpecialization", "Actions::forLoopActions", "ForLoopActionUsage", false),
            new("checkForLoopActionUsageSubactionSpecialization", "Actions::Action::forLoops", "ForLoopActionUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ForkNode</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ForkNodeRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkControlNodeSpecialization", "Actions::Action::controls", "ControlNode", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkForkNodeSpecialization", "Actions::Action::forks", "ForkNode", false),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>IndexExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] IndexExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>JoinNode</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] JoinNodeRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkControlNodeSpecialization", "Actions::Action::controls", "ControlNode", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkJoinNodeSpecialization", "Actions::Action::joins", "JoinNode", false),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>MergeNode</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] MergeNodeRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkControlNodeSpecialization", "Actions::Action::controls", "ControlNode", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkMergeNodeSpecialization", "Actions::Action::merges", "MergeNode", false),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>MetadataUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] MetadataUsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkItemUsageSpecialization", "Items::items", "ItemUsage", false),
            new("checkItemUsageSubitemSpecialization", "Items::Item::subitems", "ItemUsage", true),
            new("checkMetadataFeatureSpecialization", "Metaobjects::metaobjects", "MetadataFeature", false),
            new("checkMetadataUsageSpecialization", "Metadata::metadataItems", "MetadataUsage", false),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>PartDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] PartDefinitionRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkItemDefinitionSpecialization", "Items::Item", "ItemDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPartDefinitionSpecialization", "Parts::Part", "PartDefinition", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>PerformActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] PerformActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkEventOccurrenceUsageSpecialization", "Occurrences::Occurrence::timeEnclosedOccurrences", "EventOccurrenceUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPerformActionUsageSpecialization", "Parts::Part::performedActions", "PerformActionUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>SelectExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] SelectExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>StateDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] StateDefinitionRules =
        [
            new("checkActionDefinitionSpecialization", "Actions::Action", "ActionDefinition", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkStateDefinitionSpecialization", "States::StateAction", "StateDefinition", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>SuccessionAsUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] SuccessionAsUsageRules =
        [
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkSuccessionSpecialization", "Occurrences::happensBeforeLinks", "Succession", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>SuccessionFlow</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] SuccessionFlowRules =
        [
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFlowSpecialization", "Transfers::transfers", "Flow", false),
            new("checkFlowWithEndsSpecialization", "Transfers::flowTransfers", "Flow", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkSuccessionFlowSpecialization", "Transfers::flowTransfersBefore", "SuccessionFlow", false),
            new("checkSuccessionSpecialization", "Occurrences::happensBeforeLinks", "Succession", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>WhileLoopActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] WhileLoopActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkWhileLoopActionUsageSpecialization", "Actions::whileLoopActions", "WhileLoopActionUsage", false),
            new("checkWhileLoopActionUsageSubactionSpecialization", "Actions::Action::whileLoops", "WhileLoopActionUsage", true),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AcceptActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AcceptActionUsageRules =
        [
            new("checkAcceptActionUsageSpecialization", "Actions::acceptActions", "AcceptActionUsage", true),
            new("checkAcceptActionUsageSubactionSpecialization", "Actions::Action::acceptSubactions", "AcceptActionUsage", true),
            new("checkAcceptActionUsageTriggerActionSpecialization", "Actions::TransitionAction::accepter", "AcceptActionUsage", true),
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ActionDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ActionDefinitionRules =
        [
            new("checkActionDefinitionSpecialization", "Actions::Action", "ActionDefinition", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AssignmentActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AssignmentActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkAssignmentActionUsageSpecialization", "Actions::assignmentActions", "AssignmentActionUsage", false),
            new("checkAssignmentActionUsageSubactionSpecialization", "Actions::Action::assignments", "AssignmentActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AssociationStructure</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AssociationStructureRules =
        [
            new("checkAssociationBinarySpecialization", "Links::BinaryLink", "Association", true),
            new("checkAssociationSpecialization", "Links::Link", "Association", false),
            new("checkAssociationStructureBinarySpecialization", "Objects::BinaryLinkObject", "AssociationStructure", true),
            new("checkAssociationStructureSpecialization", "Objects::LinkObject", "AssociationStructure", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ControlNode</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ControlNodeRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkControlNodeSpecialization", "Actions::Action::controls", "ControlNode", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>IfActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] IfActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkIfActionUsageSubactionSpecialization", "Actions::Action::ifSubactions", "IfActionUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Interaction</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] InteractionRules =
        [
            new("checkAssociationBinarySpecialization", "Links::BinaryLink", "Association", true),
            new("checkAssociationSpecialization", "Links::Link", "Association", false),
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ItemDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ItemDefinitionRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkItemDefinitionSpecialization", "Items::Item", "ItemDefinition", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>LoopActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] LoopActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>OperatorExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] OperatorExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>PortDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] PortDefinitionRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkPortDefinitionSpecialization", "Ports::Port", "PortDefinition", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>RenderingUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] RenderingUsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkItemUsageSpecialization", "Items::items", "ItemUsage", false),
            new("checkItemUsageSubitemSpecialization", "Items::Item::subitems", "ItemUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPartUsageSpecialization", "Parts::parts", "PartUsage", false),
            new("checkPartUsageStakeholderSpecialization", "Requirements::RequirementCheck::stakeholders", "PartUsage", true),
            new("checkPartUsageSubpartSpecialization", "Items::Item::subparts", "PartUsage", true),
            new("checkRenderingUsageSpecialization", "Views::renderings", "RenderingUsage", false),
            new("checkRenderingUsageSubrenderingSpecialization", "Views::Rendering::subrenderings", "RenderingUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>SendActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] SendActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkSendActionUsageSpecialization", "Actions::sendActions", "SendActionUsage", false),
            new("checkSendActionUsageSubactionSpecialization", "Actions::Action::acceptSubactions", "SendActionUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>StateUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] StateUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStateUsageExclusiveStateSpecialization", "States::StateAction::exclusiveStates", "StateUsage", true),
            new("checkStateUsageOwnedStateSpecialization", "Parts::Part::ownedStates", "StateUsage", true),
            new("checkStateUsageSpecialization", "States::stateActions", "StateUsage", false),
            new("checkStateUsageSubstateSpecialization", "States::StateAction::substates", "StateUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>TerminateActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] TerminateActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTerminateActionUsageSpecialization", "Actions::terminateActions", "TerminateActionUsage", false),
            new("checkTerminateActionUsageSubactionSpecialization", "Actions::Action::terminateSubactions", "TerminateActionUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>TransitionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] TransitionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTransitionUsageActionSpecialization", "Actions::Action::decisionTransitions", "TransitionUsage", true),
            new("checkTransitionUsageSpecialization", "Actions::transitionActions", "TransitionUsage", false),
            new("checkTransitionUsageStateSpecialization", "States::StateAction::stateTransitions", "TransitionUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>TriggerInvocationExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] TriggerInvocationExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ViewUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ViewUsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkItemUsageSpecialization", "Items::items", "ItemUsage", false),
            new("checkItemUsageSubitemSpecialization", "Items::Item::subitems", "ItemUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPartUsageSpecialization", "Parts::parts", "PartUsage", false),
            new("checkPartUsageStakeholderSpecialization", "Requirements::RequirementCheck::stakeholders", "PartUsage", true),
            new("checkPartUsageSubpartSpecialization", "Items::Item::subparts", "PartUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
            new("checkViewUsageSpecialization", "Views::views", "ViewUsage", false),
            new("checkViewUsageSubviewSpecialization", "Views::View::subviews", "ViewUsage", true),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ActionUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ActionUsageRules =
        [
            new("checkActionUsageOwnedActionSpecialization", "Parts::Part::ownedActions", "ActionUsage", true),
            new("checkActionUsageSpecialization", "Actions::actions", "ActionUsage", false),
            new("checkActionUsageSubactionSpecialization", "Actions::Action::subactions", "ActionUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConnectorAsUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConnectorAsUsageRules =
        [
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ConstructorExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConstructorExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>EnumerationDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] EnumerationDefinitionRules =
        [
            new("checkDataTypeSpecialization", "Base::DataValue", "DataType", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Flow</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] FlowRules =
        [
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFlowSpecialization", "Transfers::transfers", "Flow", false),
            new("checkFlowWithEndsSpecialization", "Transfers::flowTransfers", "Flow", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Invariant</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] InvariantRules =
        [
            new("checkBooleanExpressionSpecialization", "Performances::booleanEvaluations", "BooleanExpression", false),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>InvocationExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] InvocationExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>LiteralBoolean</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] LiteralBooleanRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkLiteralBooleanSpecialization", "Performances::literalBooleanEvaluations", "LiteralBoolean", false),
            new("checkLiteralExpressionSpecialization", "Performances::literalEvaluations", "LiteralExpression", false),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>LiteralInfinity</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] LiteralInfinityRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkLiteralExpressionSpecialization", "Performances::literalEvaluations", "LiteralExpression", false),
            new("checkLiteralInfinitySpecialization", "Performances::literalIntegerEvaluations", "LiteralInfinity", false),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>LiteralInteger</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] LiteralIntegerRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkLiteralExpressionSpecialization", "Performances::literalEvaluations", "LiteralExpression", false),
            new("checkLiteralIntegerSpecialization", "Performances::literalIntegerEvaluations", "LiteralInteger", false),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>LiteralRational</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] LiteralRationalRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkLiteralExpressionSpecialization", "Performances::literalEvaluations", "LiteralExpression", false),
            new("checkLiteralRationalSpecialization", "Performances::literalRationalEvaluations", "LiteralRational", false),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>LiteralString</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] LiteralStringRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkLiteralExpressionSpecialization", "Performances::literalEvaluations", "LiteralExpression", false),
            new("checkLiteralStringSpecialization", "Performances::literalStringEvaluations", "LiteralString", false),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>PartUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] PartUsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkItemUsageSpecialization", "Items::items", "ItemUsage", false),
            new("checkItemUsageSubitemSpecialization", "Items::Item::subitems", "ItemUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPartUsageSpecialization", "Parts::parts", "PartUsage", false),
            new("checkPartUsageStakeholderSpecialization", "Requirements::RequirementCheck::stakeholders", "PartUsage", true),
            new("checkPartUsageSubpartSpecialization", "Items::Item::subparts", "PartUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Predicate</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] PredicateRules =
        [
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkPredicateSpecialization", "Performances::BooleanEvaluation", "Predicate", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AttributeDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AttributeDefinitionRules =
        [
            new("checkDataTypeSpecialization", "Base::DataValue", "DataType", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>BindingConnector</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] BindingConnectorRules =
        [
            new("checkBindingConnectorSpecialization", "Links::selfLinks", "BindingConnector", false),
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>BooleanExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] BooleanExpressionRules =
        [
            new("checkBooleanExpressionSpecialization", "Performances::booleanEvaluations", "BooleanExpression", false),
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>EnumerationUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] EnumerationUsageRules =
        [
            new("checkAttributeUsageSpecialization", "Base::dataValues", "AttributeUsage", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>EventOccurrenceUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] EventOccurrenceUsageRules =
        [
            new("checkEventOccurrenceUsageSpecialization", "Occurrences::Occurrence::timeEnclosedOccurrences", "EventOccurrenceUsage", true),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>FeatureReferenceExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] FeatureReferenceExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Function</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] FunctionRules =
        [
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkFunctionSpecialization", "Performances::Evaluation", "Function", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>InstantiationExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] InstantiationExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ItemUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ItemUsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkItemUsageSpecialization", "Items::items", "ItemUsage", false),
            new("checkItemUsageSubitemSpecialization", "Items::Item::subitems", "ItemUsage", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>LiteralExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] LiteralExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkLiteralExpressionSpecialization", "Performances::literalEvaluations", "LiteralExpression", false),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Metaclass</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] MetaclassRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkMetaclassSpecialization", "Metaobjects::Metaobject", "Metaclass", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>MetadataAccessExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] MetadataAccessExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkMetadataAccessExpressionSpecialization", "Performances::metadataAccessEvaluations", "MetadataAccessExpression", false),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>NullExpression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] NullExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkNullExpressionSpecialization", "Performances::nullEvaluations", "NullExpression", false),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>OccurrenceDefinition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] OccurrenceDefinitionRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkOccurrenceDefinitionIndividualSpecialization", "Occurrences::Life", "OccurrenceDefinition", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>PortUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] PortUsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkPortUsageOwnedPortSpecialization", "Parts::Part::ownedPorts", "PortUsage", true),
            new("checkPortUsageSpecialization", "Ports::ports", "PortUsage", false),
            new("checkPortUsageSubportSpecialization", "Ports::Port::subports", "PortUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Succession</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] SuccessionRules =
        [
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkSuccessionSpecialization", "Occurrences::happensBeforeLinks", "Succession", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Association</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AssociationRules =
        [
            new("checkAssociationBinarySpecialization", "Links::BinaryLink", "Association", true),
            new("checkAssociationSpecialization", "Links::Link", "Association", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>AttributeUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] AttributeUsageRules =
        [
            new("checkAttributeUsageSpecialization", "Base::dataValues", "AttributeUsage", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Behavior</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] BehaviorRules =
        [
            new("checkBehaviorSpecialization", "Performances::Performance", "Behavior", false),
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Connector</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ConnectorRules =
        [
            new("checkConnectorBinaryObjectSpecialization", "Objects::binaryLinkObjects", "Connector", true),
            new("checkConnectorBinarySpecialization", "Links::binaryLinks", "Connector", true),
            new("checkConnectorObjectSpecialization", "Objects::linkObjects", "Connector", true),
            new("checkConnectorSpecialization", "Links::links", "Connector", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Expression</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ExpressionRules =
        [
            new("checkExpressionSpecialization", "Performances::evaluations", "Expression", false),
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>MetadataFeature</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] MetadataFeatureRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkMetadataFeatureSpecialization", "Metaobjects::metaobjects", "MetadataFeature", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>MultiplicityRange</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] MultiplicityRangeRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkMultiplicitySpecialization", "Base::naturals", "Multiplicity", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>OccurrenceUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] OccurrenceUsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkOccurrenceUsageSnapshotSpecialization", "Occurrences::Occurrence::snapshots", "OccurrenceUsage", true),
            new("checkOccurrenceUsageSpecialization", "Occurrences::occurrences", "OccurrenceUsage", false),
            new("checkOccurrenceUsageSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "OccurrenceUsage", true),
            new("checkOccurrenceUsageTimeSliceSpecialization", "Occurrences::Occurrence::timeSlices", "OccurrenceUsage", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>ReferenceUsage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ReferenceUsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Structure</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] StructureRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkStructureSpecialization", "Objects::Object", "Structure", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Class</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ClassRules =
        [
            new("checkClassSpecialization", "Occurrences::Occurrence", "Class", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>DataType</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] DataTypeRules =
        [
            new("checkDataTypeSpecialization", "Base::DataValue", "DataType", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Definition</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] DefinitionRules =
        [
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>FlowEnd</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] FlowEndRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Multiplicity</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] MultiplicityRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkMultiplicitySpecialization", "Base::naturals", "Multiplicity", false),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>PayloadFeature</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] PayloadFeatureRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Step</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] StepRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkStepEnclosedPerformanceSpecialization", "Performances::Performance::enclosedPerformances", "Step", true),
            new("checkStepOwnedPerformanceSpecialization", "Objects::Object::ownedPerformances", "Step", true),
            new("checkStepSpecialization", "Performances::performances", "Step", false),
            new("checkStepSubperformanceSpecialization", "Performances::Performance::subperformances", "Step", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Usage</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] UsageRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Classifier</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] ClassifierRules =
        [
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Feature</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] FeatureRules =
        [
            new("checkFeatureDataValueSpecialization", "Base::dataValues", "Feature", true),
            new("checkFeatureEndSpecialization", "Links::Link::participant", "Feature", true),
            new("checkFeatureObjectSpecialization", "Objects::objects", "Feature", true),
            new("checkFeatureOccurrenceSpecialization", "Occurrences::occurrences", "Feature", true),
            new("checkFeaturePortionSpecialization", "Occurrences::Occurrence::portions", "Feature", true),
            new("checkFeatureSpecialization", "Base::things", "Feature", false),
            new("checkFeatureSubobjectSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkFeatureSuboccurrenceSpecialization", "Occurrences::Occurrence::suboccurrences", "Feature", true),
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

        /// <summary>
        /// The implied library <c>Specializations</c> applying to <c>Type</c>, own and inherited.
        /// </summary>
        private static readonly ImpliedLibrarySpecialization[] TypeRules =
        [
            new("checkTypeSpecialization", "Base::Anything", "Type", false),
        ];

    }
}
