// -------------------------------------------------------------------------------------------------
// <copyright file="ClassKindRegistry.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    using System.Collections.Generic;

    /// <summary>
    /// The APPEND-ONLY registry that freezes the sysml2.class_kind ids and the sysml2.model_version
    /// ordinals across metamodel releases. This file is the source of truth the SQL schema seeds
    /// are emitted from — NOT the UML model: the model only VALIDATES against it.
    ///
    /// Maintenance contract (multi-version support, see SysML2.NET.CodeGenerator/SQLSCHEMA-GUIDE.md):
    ///   * NEVER renumber or delete an entry — persisted element_version.class_kind values and the
    ///     generated ClassKind enum depend on the ids being frozen forever.
    ///   * A new metamodel release appends one ModelVersions entry (next id, new fingerprint) and
    ///     appends its NEW metaclasses after the highest existing class-kind id, alphabetically
    ///     among themselves.
    ///   * A metaclass dropped by a release keeps its entry, closed with RemovedIn = the new
    ///     release's id.
    ///   * The generator fails fast on any drift between the newest registered release and the UML
    ///     model on disk (unregistered class, stale registration, fingerprint mismatch), printing
    ///     the exact entries to append.
    /// </summary>
    public static class ClassKindRegistry
    {
        /// <summary>
        /// Gets the registered metamodel releases, ordered by ordinal. The last entry is the release
        /// the UML model on disk must match.
        /// </summary>
        public static IReadOnlyList<ModelVersionRegistration> ModelVersions { get; } =
        [
            new(1, "sysml-2.0-beta-4", "SysML:_mczcUFn3EfG_XZTXp4TXuA"),
        ];

        /// <summary>
        /// Gets the frozen class-kind registrations, ordered by id.
        /// </summary>
        public static IReadOnlyList<ClassKindRegistration> ClassKinds { get; } =
        [
            new(1, "AcceptActionUsage", false, 1),
            new(2, "ActionDefinition", false, 1),
            new(3, "ActionUsage", false, 1),
            new(4, "ActorMembership", false, 1),
            new(5, "AllocationDefinition", false, 1),
            new(6, "AllocationUsage", false, 1),
            new(7, "AnalysisCaseDefinition", false, 1),
            new(8, "AnalysisCaseUsage", false, 1),
            new(9, "AnnotatingElement", false, 1),
            new(10, "Annotation", false, 1),
            new(11, "AssertConstraintUsage", false, 1),
            new(12, "AssignmentActionUsage", false, 1),
            new(13, "Association", false, 1),
            new(14, "AssociationStructure", false, 1),
            new(15, "AttributeDefinition", false, 1),
            new(16, "AttributeUsage", false, 1),
            new(17, "Behavior", false, 1),
            new(18, "BindingConnector", false, 1),
            new(19, "BindingConnectorAsUsage", false, 1),
            new(20, "BooleanExpression", false, 1),
            new(21, "CalculationDefinition", false, 1),
            new(22, "CalculationUsage", false, 1),
            new(23, "CaseDefinition", false, 1),
            new(24, "CaseUsage", false, 1),
            new(25, "Class", false, 1),
            new(26, "Classifier", false, 1),
            new(27, "CollectExpression", false, 1),
            new(28, "Comment", false, 1),
            new(29, "ConcernDefinition", false, 1),
            new(30, "ConcernUsage", false, 1),
            new(31, "ConjugatedPortDefinition", false, 1),
            new(32, "ConjugatedPortTyping", false, 1),
            new(33, "Conjugation", false, 1),
            new(34, "ConnectionDefinition", false, 1),
            new(35, "ConnectionUsage", false, 1),
            new(36, "Connector", false, 1),
            new(37, "ConnectorAsUsage", true, 1),
            new(38, "ConstraintDefinition", false, 1),
            new(39, "ConstraintUsage", false, 1),
            new(40, "ConstructorExpression", false, 1),
            new(41, "ControlNode", true, 1),
            new(42, "CrossSubsetting", false, 1),
            new(43, "DataType", false, 1),
            new(44, "DecisionNode", false, 1),
            new(45, "Definition", false, 1),
            new(46, "Dependency", false, 1),
            new(47, "Differencing", false, 1),
            new(48, "Disjoining", false, 1),
            new(49, "Documentation", false, 1),
            new(50, "Element", true, 1),
            new(51, "ElementFilterMembership", false, 1),
            new(52, "EndFeatureMembership", false, 1),
            new(53, "EnumerationDefinition", false, 1),
            new(54, "EnumerationUsage", false, 1),
            new(55, "EventOccurrenceUsage", false, 1),
            new(56, "ExhibitStateUsage", false, 1),
            new(57, "Expose", true, 1),
            new(58, "Expression", false, 1),
            new(59, "Feature", false, 1),
            new(60, "FeatureChainExpression", false, 1),
            new(61, "FeatureChaining", false, 1),
            new(62, "FeatureInverting", false, 1),
            new(63, "FeatureMembership", false, 1),
            new(64, "FeatureReferenceExpression", false, 1),
            new(65, "FeatureTyping", false, 1),
            new(66, "FeatureValue", false, 1),
            new(67, "Flow", false, 1),
            new(68, "FlowDefinition", false, 1),
            new(69, "FlowEnd", false, 1),
            new(70, "FlowUsage", false, 1),
            new(71, "ForLoopActionUsage", false, 1),
            new(72, "ForkNode", false, 1),
            new(73, "FramedConcernMembership", false, 1),
            new(74, "Function", false, 1),
            new(75, "IfActionUsage", false, 1),
            new(76, "Import", true, 1),
            new(77, "IncludeUseCaseUsage", false, 1),
            new(78, "IndexExpression", false, 1),
            new(79, "InstantiationExpression", true, 1),
            new(80, "Interaction", false, 1),
            new(81, "InterfaceDefinition", false, 1),
            new(82, "InterfaceUsage", false, 1),
            new(83, "Intersecting", false, 1),
            new(84, "Invariant", false, 1),
            new(85, "InvocationExpression", false, 1),
            new(86, "ItemDefinition", false, 1),
            new(87, "ItemUsage", false, 1),
            new(88, "JoinNode", false, 1),
            new(89, "LibraryPackage", false, 1),
            new(90, "LiteralBoolean", false, 1),
            new(91, "LiteralExpression", false, 1),
            new(92, "LiteralInfinity", false, 1),
            new(93, "LiteralInteger", false, 1),
            new(94, "LiteralRational", false, 1),
            new(95, "LiteralString", false, 1),
            new(96, "LoopActionUsage", true, 1),
            new(97, "Membership", false, 1),
            new(98, "MembershipExpose", false, 1),
            new(99, "MembershipImport", false, 1),
            new(100, "MergeNode", false, 1),
            new(101, "Metaclass", false, 1),
            new(102, "MetadataAccessExpression", false, 1),
            new(103, "MetadataDefinition", false, 1),
            new(104, "MetadataFeature", false, 1),
            new(105, "MetadataUsage", false, 1),
            new(106, "Multiplicity", false, 1),
            new(107, "MultiplicityRange", false, 1),
            new(108, "Namespace", false, 1),
            new(109, "NamespaceExpose", false, 1),
            new(110, "NamespaceImport", false, 1),
            new(111, "NullExpression", false, 1),
            new(112, "ObjectiveMembership", false, 1),
            new(113, "OccurrenceDefinition", false, 1),
            new(114, "OccurrenceUsage", false, 1),
            new(115, "OperatorExpression", false, 1),
            new(116, "OwningMembership", false, 1),
            new(117, "Package", false, 1),
            new(118, "ParameterMembership", false, 1),
            new(119, "PartDefinition", false, 1),
            new(120, "PartUsage", false, 1),
            new(121, "PayloadFeature", false, 1),
            new(122, "PerformActionUsage", false, 1),
            new(123, "PortConjugation", false, 1),
            new(124, "PortDefinition", false, 1),
            new(125, "PortUsage", false, 1),
            new(126, "Predicate", false, 1),
            new(127, "Redefinition", false, 1),
            new(128, "ReferenceSubsetting", false, 1),
            new(129, "ReferenceUsage", false, 1),
            new(130, "Relationship", true, 1),
            new(131, "RenderingDefinition", false, 1),
            new(132, "RenderingUsage", false, 1),
            new(133, "RequirementConstraintMembership", false, 1),
            new(134, "RequirementDefinition", false, 1),
            new(135, "RequirementUsage", false, 1),
            new(136, "RequirementVerificationMembership", false, 1),
            new(137, "ResultExpressionMembership", false, 1),
            new(138, "ReturnParameterMembership", false, 1),
            new(139, "SatisfyRequirementUsage", false, 1),
            new(140, "SelectExpression", false, 1),
            new(141, "SendActionUsage", false, 1),
            new(142, "Specialization", false, 1),
            new(143, "StakeholderMembership", false, 1),
            new(144, "StateDefinition", false, 1),
            new(145, "StateSubactionMembership", false, 1),
            new(146, "StateUsage", false, 1),
            new(147, "Step", false, 1),
            new(148, "Structure", false, 1),
            new(149, "Subclassification", false, 1),
            new(150, "SubjectMembership", false, 1),
            new(151, "Subsetting", false, 1),
            new(152, "Succession", false, 1),
            new(153, "SuccessionAsUsage", false, 1),
            new(154, "SuccessionFlow", false, 1),
            new(155, "SuccessionFlowUsage", false, 1),
            new(156, "TerminateActionUsage", false, 1),
            new(157, "TextualRepresentation", false, 1),
            new(158, "TransitionFeatureMembership", false, 1),
            new(159, "TransitionUsage", false, 1),
            new(160, "TriggerInvocationExpression", false, 1),
            new(161, "Type", false, 1),
            new(162, "TypeFeaturing", false, 1),
            new(163, "Unioning", false, 1),
            new(164, "Usage", false, 1),
            new(165, "UseCaseDefinition", false, 1),
            new(166, "UseCaseUsage", false, 1),
            new(167, "VariantMembership", false, 1),
            new(168, "VerificationCaseDefinition", false, 1),
            new(169, "VerificationCaseUsage", false, 1),
            new(170, "ViewDefinition", false, 1),
            new(171, "ViewRenderingMembership", false, 1),
            new(172, "ViewUsage", false, 1),
            new(173, "ViewpointDefinition", false, 1),
            new(174, "ViewpointUsage", false, 1),
            new(175, "WhileLoopActionUsage", false, 1),
        ];
    }
}
