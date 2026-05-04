// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationValidationExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Collections.Frozen;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Extensions;

    /// <summary>
    /// Extension methods providing IsValidFor guards used in textual notation switch dispatchers.
    /// These allow disambiguation when multiple grammar rule alternatives map to the same UML class.
    /// </summary>
    internal static class TextualNotationValidationExtensions
    {
        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the Typings rule.
        /// <para><c>Typings : Feature = TypedBy (',' ownedRelationship += FeatureTyping)*</c></para>
        /// <para>Matches when the feature owns at least one <see cref="IFeatureTyping"/>.</para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature owns an <see cref="IFeatureTyping"/></returns>
        internal static bool IsValidForTypings(this IFeature feature)
        {
            return feature?.OwnedRelationship.Any(relationship => relationship is IFeatureTyping) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the Subsettings rule.
        /// <para><c>Subsettings : Feature = Subsets (',' ownedRelationship += OwnedSubsetting)*</c></para>
        /// <para>Matches when the feature owns an <see cref="ISubsetting"/> that is a plain subsetting —
        /// i.e. not one of the more specific subtypes (<see cref="IRedefinition"/>,
        /// <see cref="IReferenceSubsetting"/>, <see cref="ICrossSubsetting"/>), each of which has its
        /// own dedicated rule (Redefinitions / References / Crosses) elsewhere in the switch.</para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature owns a plain <see cref="ISubsetting"/></returns>
        internal static bool IsValidForSubsettings(this IFeature feature)
        {
            return feature?.OwnedRelationship.Any(relationship =>
                relationship is ISubsetting and not IRedefinition and not IReferenceSubsetting and not ICrossSubsetting) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the References rule.
        /// <para><c>References : Feature = REFERENCES ownedRelationship += OwnedReferenceSubsetting</c></para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature owns an <see cref="IReferenceSubsetting"/></returns>
        internal static bool IsValidForReferences(this IFeature feature)
        {
            return feature?.OwnedRelationship.Any(relationship => relationship is IReferenceSubsetting) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the Crosses rule.
        /// <para><c>Crosses : Feature = CROSSES ownedRelationship += OwnedCrossSubsetting</c></para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature owns an <see cref="ICrossSubsetting"/></returns>
        internal static bool IsValidForCrosses(this IFeature feature)
        {
            return feature?.OwnedRelationship.Any(relationship => relationship is ICrossSubsetting) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the ChainingPart rule.
        /// <para><c>ChainingPart : Feature = 'chains' (ownedRelationship += OwnedFeatureChaining | FeatureChain)</c></para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature owns an <see cref="IFeatureChaining"/></returns>
        internal static bool IsValidForChainingPart(this IFeature feature)
        {
            return feature?.OwnedRelationship.Any(relationship => relationship is IFeatureChaining) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the InvertingPart rule.
        /// <para><c>InvertingPart : Feature = 'inverse' 'of' ownedRelationship += OwnedFeatureInverting</c></para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature owns an <see cref="IFeatureInverting"/></returns>
        internal static bool IsValidForInvertingPart(this IFeature feature)
        {
            return feature?.OwnedRelationship.Any(relationship => relationship is IFeatureInverting) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the PositionalArgumentList rule.
        /// <para><c>PositionalArgumentList : Feature = e.ownedRelationship += ArgumentMember (',' e.ownedRelationship += ArgumentMember)*</c></para>
        /// <para>Matches when the feature owns an <see cref="IParameterMembership"/> (as opposed to
        /// NamedArgumentList, which owns plain <see cref="IFeatureMembership"/> members without being
        /// <see cref="IParameterMembership"/>).</para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <returns>True if the feature owns an <see cref="IParameterMembership"/></returns>
        internal static bool IsValidForPositionalArgumentList(this IFeature feature)
        {
            return feature?.OwnedRelationship.Any(relationship => relationship is IParameterMembership) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the DisjoiningPart rule.
        /// <para><c>DisjoiningPart : Type = 'disjoint' 'from' ownedRelationship += OwnedDisjoining (',' ownedRelationship += OwnedDisjoining)*</c></para>
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <returns>True if the type owns an <see cref="IDisjoining"/></returns>
        internal static bool IsValidForDisjoiningPart(this IType type)
        {
            return type?.OwnedRelationship.Any(relationship => relationship is IDisjoining) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the UnioningPart rule.
        /// <para><c>UnioningPart : Type = 'unions' ownedRelationship += Unioning (',' ownedRelationship += Unioning)*</c></para>
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <returns>True if the type owns an <see cref="IUnioning"/></returns>
        internal static bool IsValidForUnioningPart(this IType type)
        {
            return type?.OwnedRelationship.Any(relationship => relationship is IUnioning) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the IntersectingPart rule.
        /// <para><c>IntersectingPart : Type = 'intersects' ownedRelationship += Intersecting (',' ownedRelationship += Intersecting)*</c></para>
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <returns>True if the type owns an <see cref="IIntersecting"/></returns>
        internal static bool IsValidForIntersectingPart(this IType type)
        {
            return type?.OwnedRelationship.Any(relationship => relationship is IIntersecting) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the BehaviorUsageMember rule.
        /// <para><c>BehaviorUsageMember : FeatureMembership = MemberPrefix ownedRelatedElement += BehaviorUsageElement</c></para>
        /// <para><c>BehaviorUsageElement : Usage = ActionUsage | CalculationUsage | StateUsage | ConstraintUsage | RequirementUsage | ConcernUsage | CaseUsage | AnalysisCaseUsage | VerificationCaseUsage | UseCaseUsage | ViewpointUsage | PerformActionUsage | ExhibitStateUsage | IncludeUseCaseUsage | AssertConstraintUsage | SatisfyRequirementUsage</c></para>
        /// <para>The disjunction below covers these root metaclasses; the remaining grammar alternatives
        /// all inherit transitively (PerformActionUsage/ExhibitStateUsage/IncludeUseCaseUsage are
        /// <see cref="IActionUsage"/> descendants, AssertConstraintUsage inherits <see cref="IConstraintUsage"/>,
        /// SatisfyRequirementUsage inherits <see cref="IRequirementUsage"/>).</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership owns a behavior-usage element</returns>
        internal static bool IsValidForBehaviorUsageMember(this IFeatureMembership featureMembership)
        {
            return featureMembership?.OwnedRelatedElement.Any(element =>
                element is IActionUsage or IStateUsage or IConstraintUsage
                or IRequirementUsage or ICaseUsage) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IConnector"/> is valid for the BinaryConnectorDeclaration rule.
        /// <para><c>BinaryConnectorDeclaration : Connector = … ownedRelationship += ConnectorEndMember 'to' ownedRelationship += ConnectorEndMember</c></para>
        /// <para>NaryConnectorDeclaration has three or more. <c>ConnectorEndMember : EndFeatureMembership</c>
        /// so exactly two <see cref="IEndFeatureMembership"/> entries = binary.</para>
        /// </summary>
        /// <param name="connector">The <see cref="IConnector"/></param>
        /// <returns>True if the connector owns exactly two <see cref="IEndFeatureMembership"/> children</returns>
        internal static bool IsValidForBinaryConnectorDeclaration(this IConnector connector)
        {
            return connector?.OwnedRelationship.OfType<IEndFeatureMembership>().Count() == 2;
        }

        /// <summary>
        /// Asserts that the <see cref="IConnectionUsage"/> is valid for the BinaryConnectorPart rule.
        /// <para><c>BinaryConnectorPart : ConnectionUsage = ownedRelationship += ConnectorEndMember 'to' ownedRelationship += ConnectorEndMember</c></para>
        /// <para><c>NaryConnectorPart : ConnectionUsage = '(' ownedRelationship += ConnectorEndMember ',' … ')'</c></para>
        /// <para><c>ConnectorEndMember : EndFeatureMembership</c> — so the guard counts
        /// <see cref="IEndFeatureMembership"/> entries: exactly two = binary, otherwise n-ary.</para>
        /// </summary>
        /// <param name="connectionUsage">The <see cref="IConnectionUsage"/></param>
        /// <returns>True if the connection usage owns exactly two <see cref="IEndFeatureMembership"/> children</returns>
        internal static bool IsValidForBinaryConnectorPart(this IConnectionUsage connectionUsage)
        {
            return connectionUsage?.OwnedRelationship.OfType<IEndFeatureMembership>().Count() == 2;
        }

        /// <summary>
        /// Asserts that the <see cref="IInterfaceUsage"/> is valid for the BinaryInterfacePart rule.
        /// <para><c>BinaryInterfacePart : InterfaceUsage = ownedRelationship += InterfaceEndMember 'to' ownedRelationship += InterfaceEndMember</c></para>
        /// <para><c>NaryInterfacePart : InterfaceUsage = '(' ownedRelationship += InterfaceEndMember ',' … ')'</c></para>
        /// <para><c>InterfaceEndMember : EndFeatureMembership</c> — exactly two <see cref="IEndFeatureMembership"/> = binary.</para>
        /// </summary>
        /// <param name="interfaceUsage">The <see cref="IInterfaceUsage"/></param>
        /// <returns>True if the interface usage owns exactly two <see cref="IEndFeatureMembership"/> children</returns>
        internal static bool IsValidForBinaryInterfacePart(this IInterfaceUsage interfaceUsage)
        {
            return interfaceUsage?.OwnedRelationship.OfType<IEndFeatureMembership>().Count() == 2;
        }

        /// <summary>
        /// Asserts that the <see cref="IUsage"/> is valid for the StructureUsageElement rule.
        /// <para><c>StructureUsageElement : Usage = OccurrenceUsage | IndividualUsage | PortionUsage
        /// | EventOccurrenceUsage | ItemUsage | PartUsage | ViewUsage | RenderingUsage | PortUsage
        /// | ConnectionUsage | InterfaceUsage | AllocationUsage | Message | FlowUsage | SuccessionFlowUsage</c></para>
        /// <para>Matches any structural-usage metaclass. IndividualUsage/PortionUsage/EventOccurrenceUsage
        /// inherit <see cref="IOccurrenceUsage"/>; Message and SuccessionFlowUsage inherit
        /// <see cref="IFlowUsage"/>.</para>
        /// </summary>
        /// <param name="usage">The <see cref="IUsage"/></param>
        /// <returns>True if the usage is a structural-usage metaclass</returns>
        internal static bool IsValidForStructureUsageElement(this IUsage usage)
        {
            return usage is IOccurrenceUsage or IItemUsage or IPartUsage or IViewUsage
                or IRenderingUsage or IPortUsage or IConnectionUsage or IInterfaceUsage
                or IAllocationUsage or IFlowUsage;
        }

        /// <summary>
        /// Asserts that the <see cref="IOccurrenceUsage"/> is valid for the OccurrenceUsage rule.
        /// <para><c>OccurrenceUsage = OccurrenceUsagePrefix 'occurrence' Usage</c> — the general case
        /// where neither <c>isIndividual</c> nor <c>portionKind</c> has been set by one of the more
        /// specific rules (<c>IndividualUsage</c>, <c>PortionUsage</c>).</para>
        /// </summary>
        /// <param name="occurrenceUsage">The <see cref="IOccurrenceUsage"/></param>
        /// <returns>True if the usage is a plain occurrence (no individual, no portion kind)</returns>
        internal static bool IsValidForOccurrenceUsage(this IOccurrenceUsage occurrenceUsage)
        {
            return occurrenceUsage is { IsIndividual: false, PortionKind: null };
        }

        /// <summary>
        /// Asserts that the <see cref="IOccurrenceUsage"/> is valid for the IndividualUsage rule.
        /// <para><c>IndividualUsage : OccurrenceUsage = BasicUsagePrefix isIndividual ?= 'individual' UsageExtensionKeyword* Usage</c></para>
        /// <para><c>PortionUsage</c> can also set <c>isIndividual</c>, so this guard additionally
        /// requires <c>PortionKind</c> to be unset — otherwise the usage is a <c>PortionUsage</c>
        /// and should flow to the default case.</para>
        /// </summary>
        /// <param name="occurrenceUsage">The <see cref="IOccurrenceUsage"/></param>
        /// <returns>True if the usage is an individual (not a portion)</returns>
        internal static bool IsValidForIndividualUsage(this IOccurrenceUsage occurrenceUsage)
        {
            return occurrenceUsage is { IsIndividual: true, PortionKind: null };
        }

        /// <summary>
        /// Asserts that the <see cref="IOccurrenceDefinition"/> is valid for the OccurrenceDefinition rule.
        /// <para><c>OccurrenceDefinition = OccurrenceDefinitionPrefix 'occurrence' 'def' Definition</c></para>
        /// <para><c>IndividualDefinition : OccurrenceDefinition = BasicDefinitionPrefix? isIndividual ?= 'individual' …</c></para>
        /// <para>Matches the general occurrence-definition case when <c>IsIndividual</c> is false —
        /// <c>IndividualDefinition</c> is the default fallback.</para>
        /// </summary>
        /// <param name="occurrenceDefinition">The <see cref="IOccurrenceDefinition"/></param>
        /// <returns>True if the definition is a plain occurrence (not an individual)</returns>
        internal static bool IsValidForOccurrenceDefinition(this IOccurrenceDefinition occurrenceDefinition)
        {
            return occurrenceDefinition is { IsIndividual: false };
        }

        /// <summary>
        /// Asserts that the <see cref="IFlowUsage"/> is valid for the FlowUsage rule (as opposed to
        /// the Message rule, which forces <c>isAbstract = true</c>).
        /// <para><c>FlowUsage = OccurrenceUsagePrefix 'flow' FlowDeclaration DefinitionBody</c></para>
        /// </summary>
        /// <param name="flowUsage">The <see cref="IFlowUsage"/></param>
        /// <returns>True if the usage is not abstract (i.e. not a Message)</returns>
        internal static bool IsValidForFlowUsage(this IFlowUsage flowUsage)
        {
            return flowUsage is { IsAbstract: false };
        }

        /// <summary>
        /// Asserts that the <see cref="IFlowUsage"/> is valid for the Message rule.
        /// <para><c>Message : FlowUsage = OccurrenceUsagePrefix 'message' MessageDeclaration DefinitionBody { isAbstract = true }</c></para>
        /// <para>The non-parsing assignment <c>{ isAbstract = true }</c> is the sole runtime
        /// distinguisher between a Message and a plain FlowUsage in the unparse direction.</para>
        /// </summary>
        /// <param name="flowUsage">The <see cref="IFlowUsage"/></param>
        /// <returns>True if the usage is abstract (a Message)</returns>
        internal static bool IsValidForMessage(this IFlowUsage flowUsage)
        {
            return flowUsage is { IsAbstract: true };
        }

        /// <summary>
        /// Asserts that the <see cref="ITransitionUsage"/> is valid for the GuardedTargetSuccession rule.
        /// <para><c>GuardedTargetSuccession : TransitionUsage = ownedRelationship += GuardExpressionMember 'then' ownedRelationship += TransitionSuccessionMember</c></para>
        /// <para><c>GuardExpressionMember : TransitionFeatureMembership = 'if' { kind = 'guard' } …</c></para>
        /// </summary>
        /// <param name="transitionUsage">The <see cref="ITransitionUsage"/></param>
        /// <returns>True if the transition owns a guard-kind transition feature membership</returns>
        internal static bool IsValidForGuardedTargetSuccession(this ITransitionUsage transitionUsage)
        {
            return transitionUsage?.OwnedRelationship.Any(relationship =>
                relationship is ITransitionFeatureMembership { Kind: SysML2.NET.Core.Systems.States.TransitionFeatureKind.Guard }) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IParameterMembership"/> is valid for the ActionBodyParameterMember rule.
        /// <para><c>ActionBodyParameterMember : ParameterMembership = ownedRelatedElement += ActionBodyParameter</c></para>
        /// <para><c>ActionBodyParameter : ActionUsage = ('action' UsageDeclaration?)? '{' ActionBodyItem* '}'</c></para>
        /// </summary>
        /// <param name="parameterMembership">The <see cref="IParameterMembership"/></param>
        /// <returns>True if the membership owns an <see cref="IActionUsage"/></returns>
        internal static bool IsValidForActionBodyParameterMember(this IParameterMembership parameterMembership)
        {
            return parameterMembership?.OwnedRelatedElement.OfType<IActionUsage>().Any() == true;
        }

        /// <summary>
        /// Keywords of the <c>ConditionalBinaryOperator</c> lexical rule
        /// (<c>ConditionalBinaryOperator = '??' | 'or' | 'and' | 'implies'</c>). Matches the
        /// operators consumed by <c>ConditionalBinaryOperatorExpression</c>.
        /// </summary>
        private static readonly FrozenSet<string> ConditionalBinaryOperators =
            new HashSet<string> { "??", "or", "and", "implies" }.ToFrozenSet();

        /// <summary>
        /// Keywords of the <c>BinaryOperator</c> lexical rule. Matches the operators consumed by
        /// <c>BinaryOperatorExpression</c>.
        /// </summary>
        private static readonly FrozenSet<string> BinaryOperators =
            new HashSet<string>
            {
                "|", "&", "xor", "..",
                "==", "!=", "===", "!==",
                "<", ">", "<=", ">=",
                "+", "-", "*", "/",
                "%", "^", "**"
            }.ToFrozenSet();

        /// <summary>
        /// Keywords of the <c>UnaryOperator</c> lexical rule
        /// (<c>UnaryOperator = '+' | '-' | '~' | 'not'</c>). Matches the operators consumed by
        /// <c>UnaryOperatorExpression</c>.
        /// </summary>
        private static readonly FrozenSet<string> UnaryOperators =
            new HashSet<string> { "+", "-", "~", "not" }.ToFrozenSet();

        /// <summary>
        /// Operator keywords that a <c>ClassificationExpression</c> may use: the
        /// <c>ClassificationTestOperator</c> (<c>'istype' | 'hastype' | '@'</c>) plus the
        /// <c>CastOperator</c> (<c>'as'</c>).
        /// </summary>
        private static readonly FrozenSet<string> ClassificationExpressionOperators =
            new HashSet<string> { "istype", "hastype", "@", "as" }.ToFrozenSet();

        /// <summary>
        /// Operator keywords that a <c>MetaclassificationExpression</c> uses uniquely: the
        /// <c>MetaCastOperator</c> (<c>'meta'</c>) and the <c>MetaclassificationTestOperator</c>
        /// (<c>'@@'</c>). Note: <c>istype</c>/<c>hastype</c>/<c>@</c> also appear in the
        /// Metaclassification rule, but they overlap with <c>ClassificationExpression</c>;
        /// runtime disambiguation of that overlap would require structural inspection of the
        /// argument's type (<c>MetadataArgumentMember</c> wrapping a
        /// <c>MetadataAccessExpression</c>), which is left unimplemented for now — callers with
        /// ambiguous operators fall to <c>ClassificationExpression</c> first in the switch.
        /// </summary>
        private static readonly FrozenSet<string> MetaclassificationExpressionOperators =
            new HashSet<string> { "meta", "@@" }.ToFrozenSet();

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the ConditionalExpression rule
        /// <para><c>ConditionalExpression : OperatorExpression = operator='if' …</c></para>
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression's <c>Operator</c> is <c>"if"</c></returns>
        internal static bool IsValidForConditionalExpression(this IOperatorExpression operatorExpression)
        {
            return operatorExpression?.Operator == "if";
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the ConditionalBinaryOperatorExpression rule
        /// <para><c>operator = ConditionalBinaryOperator</c> where <c>ConditionalBinaryOperator = '??' | 'or' | 'and' | 'implies'</c></para>
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression's <c>Operator</c> is one of the conditional binary operators</returns>
        internal static bool IsValidForConditionalBinaryOperatorExpression(this IOperatorExpression operatorExpression)
        {
            return operatorExpression?.Operator is not null && ConditionalBinaryOperators.Contains(operatorExpression.Operator);
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the BinaryOperatorExpression rule
        /// <para><c>BinaryOperatorExpression : OperatorExpression = ownedRelationship += ArgumentMember operator = BinaryOperator ownedRelationship += ArgumentMember ownedRelationship += EmptyResultMember</c></para>
        /// <para>Operator match alone is not sufficient — <c>+</c> and <c>-</c> also appear in <c>UnaryOperator</c>.
        /// The rule emits two <c>ArgumentMember</c> entries (vs. one for unary), so the guard additionally
        /// requires the expression to own at least two <see cref="IParameterMembership"/> arguments.</para>
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression matches the BinaryOperatorExpression rule</returns>
        internal static bool IsValidForBinaryOperatorExpression(this IOperatorExpression operatorExpression)
        {
            if (operatorExpression?.Operator is null || !BinaryOperators.Contains(operatorExpression.Operator))
            {
                return false;
            }

            // Count ArgumentMember entries only — exclude ReturnParameterMembership (the EmptyResultMember),
            // which is itself an IParameterMembership in the metamodel and would otherwise inflate the count.
            return operatorExpression.OwnedRelationship
                .OfType<IParameterMembership>()
                .Count(membership => membership is not IReturnParameterMembership) >= 2;
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the UnaryOperatorExpression rule
        /// <para><c>UnaryOperatorExpression : OperatorExpression = operator = UnaryOperator ownedRelationship += ArgumentMember ownedRelationship += EmptyResultMember</c></para>
        /// <para>Operator match alone is not sufficient — <c>+</c> and <c>-</c> also appear in <c>BinaryOperator</c>.
        /// The rule emits exactly one <c>ArgumentMember</c>, so the guard additionally requires the expression
        /// to own a single <see cref="IParameterMembership"/> argument.</para>
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression matches the UnaryOperatorExpression rule</returns>
        internal static bool IsValidForUnaryOperatorExpression(this IOperatorExpression operatorExpression)
        {
            if (operatorExpression?.Operator is null || !UnaryOperators.Contains(operatorExpression.Operator))
            {
                return false;
            }

            // Count ArgumentMember entries only — exclude ReturnParameterMembership (the EmptyResultMember),
            // which is itself an IParameterMembership in the metamodel.
            return operatorExpression.OwnedRelationship
                .OfType<IParameterMembership>()
                .Count(membership => membership is not IReturnParameterMembership) == 1;
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the ClassificationExpression rule.
        /// <para><c>ClassificationExpression : OperatorExpression = (ownedRelationship += ArgumentMember)?
        /// ( operator = ClassificationTestOperator ownedRelationship += TypeReferenceMember
        /// | operator = CastOperator ownedRelationship += TypeResultMember )
        /// ownedRelationship += EmptyResultMember</c></para>
        /// <para>Matches when the operator is one of <c>'istype'</c>, <c>'hastype'</c>, <c>'@'</c>, or <c>'as'</c>.</para>
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression's <c>Operator</c> is a classification-test or cast operator</returns>
        internal static bool IsValidForClassificationExpression(this IOperatorExpression operatorExpression)
        {
            return operatorExpression?.Operator is not null && ClassificationExpressionOperators.Contains(operatorExpression.Operator);
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the MetaclassificationExpression rule.
        /// <para><c>MetaclassificationExpression : OperatorExpression = ownedRelationship += MetadataArgumentMember
        /// ( operator = ClassificationTestOperator ownedRelationship += TypeReferenceMember
        /// | operator = MetaCastOperator ownedRelationship += TypeResultMember )
        /// ownedRelationship += EmptyResultMember</c></para>
        /// <para>Matches when the operator is uniquely Metaclassification (<c>'meta'</c> or <c>'@@'</c>).
        /// Operators shared with <c>ClassificationExpression</c> (<c>'istype'</c>, <c>'hastype'</c>, <c>'@'</c>)
        /// are left to <c>ClassificationExpression</c>, which appears earlier in the switch dispatch.
        /// True structural disambiguation (inspecting the <c>MetadataArgumentMember</c>'s contents) is
        /// deferred.</para>
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <returns>True if the expression's <c>Operator</c> is a meta-cast or metaclassification-test operator</returns>
        internal static bool IsValidForMetaclassificationExpression(this IOperatorExpression operatorExpression)
        {
            return operatorExpression?.Operator is not null && MetaclassificationExpressionOperators.Contains(operatorExpression.Operator);
        }

        /// <summary>
        /// Asserts that the <see cref="IExpression"/> is valid for the SequenceExpression rule.
        /// <para><c>SequenceExpression = OwnedExpression ','? | SequenceOperatorExpression</c></para>
        /// <para>Acts as a catch-all in the PrimaryExpression switch: by the time a switch reaches this
        /// case, every more-specific PrimaryExpression variant (Classification, Metaclassification,
        /// Conditional*, BinaryOp, UnaryOp, FeatureReference, …) has already failed, so any remaining
        /// <see cref="IExpression"/> is valid as a SequenceExpression. No runtime property further
        /// disambiguates.</para>
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/></param>
        /// <returns>True for any non-null expression</returns>
        internal static bool IsValidForSequenceExpression(this IExpression expression)
        {
            return expression is not null;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureReferenceExpression"/> is valid for the FeatureReferenceExpression rule.
        /// <para><c>FeatureReferenceExpression = ownedRelationship += FeatureChainMember</c></para>
        /// <para><c>BodyExpression : FeatureReferenceExpression = ownedRelationship += ExpressionBodyMember</c></para>
        /// <para>The grammar distinguishes <c>BodyExpression</c> from a plain <c>FeatureReferenceExpression</c>
        /// by the kind of membership it owns, but there is no distinct <c>IBodyExpression</c> metaclass in
        /// the current POCO surface. Without a structural predicate on the owned membership, this guard
        /// returns <c>true</c> for any non-null <see cref="IFeatureReferenceExpression"/>. The switch
        /// dispatcher should place any future BodyExpression case before this guard.</para>
        /// </summary>
        /// <param name="featureReferenceExpression">The <see cref="IFeatureReferenceExpression"/></param>
        /// <returns>True for any non-null expression</returns>
        internal static bool IsValidForFeatureReferenceExpression(this IFeatureReferenceExpression featureReferenceExpression)
        {
            return featureReferenceExpression is not null;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the InitialNodeMember rule (ActionBodyItem).
        /// <para><c>InitialNodeMember : FeatureMembership = MemberPrefix 'first' memberFeature = [QualifiedName] RelationshipBody</c></para>
        /// <para><b>Limitation:</b> the rule is identified by the combination "<c>MemberElement</c>
        /// set via cross-reference (no owned related element)". Other FeatureMembership rules that
        /// also set <c>MemberElement</c> by cross-reference without owning any related element
        /// would match this predicate. In practice InitialNodeMember is the only such case inside
        /// an <c>ActionBodyItem</c> dispatch, which is the only caller.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership references its target via cross-reference only</returns>
        internal static bool IsValidForInitialNodeMember(this IFeatureMembership featureMembership)
        {
            return featureMembership is { MemberElement: not null }
                && featureMembership.OwnedRelatedElement.Count == 0;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the ActionTargetSuccessionMember rule (ActionBodyItem).
        /// <para><c>ActionTargetSuccessionMember : FeatureMembership = MemberPrefix ownedRelatedElement += ActionTargetSuccession</c></para>
        /// <para><c>ActionTargetSuccession : Usage = (TargetSuccession:SuccessionAsUsage | GuardedTargetSuccession:TransitionUsage | DefaultTargetSuccession:TransitionUsage) UsageBody</c></para>
        /// <para><b>Limitation:</b> <c>SourceSuccessionMember</c> also wraps <see cref="ISuccessionAsUsage"/>;
        /// the broader dispatch relies on switch-case ordering (SourceSuccessionMember's guard is
        /// checked first, then ActionTargetSuccessionMember).</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership owns a succession or transition usage</returns>
        internal static bool IsValidForActionTargetSuccessionMember(this IFeatureMembership featureMembership)
        {
            return featureMembership?.OwnedRelatedElement.Any(element => element is ISuccessionAsUsage or ITransitionUsage) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the ActionBehaviorMember rule (ActionBodyItem).
        /// <para><c>ActionBehaviorMember : FeatureMembership = BehaviorUsageMember | ActionNodeMember</c></para>
        /// <para><c>ActionNodeMember</c> wraps an <c>ActionNode</c> (ControlNode / SendNode / AcceptNode /
        /// AssignmentNode / TerminateNode / IfNode / WhileLoopNode / ForLoopNode — all <see cref="IActionUsage"/>
        /// descendants). BehaviorUsageMember wraps a BehaviorUsageElement (also mostly <see cref="IActionUsage"/>
        /// or its descendants). The broadest accurate predicate is "owns an <see cref="IActionUsage"/>".</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership owns an <see cref="IActionUsage"/></returns>
        internal static bool IsValidForActionBehaviorMember(this IFeatureMembership featureMembership)
        {
            return featureMembership?.OwnedRelatedElement.OfType<IActionUsage>().Any() == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the GuardedSuccessionMember rule (ActionBodyItem).
        /// <para><c>GuardedSuccessionMember : FeatureMembership = MemberPrefix ownedRelatedElement += GuardedSuccession</c></para>
        /// <para><c>GuardedSuccession : TransitionUsage = ('succession' UsageDeclaration)? 'first' … ownedRelationship += GuardExpressionMember 'then' …</c></para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership owns a transition usage with a guard-kind feature</returns>
        internal static bool IsValidForGuardedSuccessionMember(this IFeatureMembership featureMembership)
        {
            return featureMembership?.OwnedRelatedElement.OfType<ITransitionUsage>().Any(transition =>
                transition.OwnedRelationship.Any(relationship =>
                    relationship is ITransitionFeatureMembership { Kind: SysML2.NET.Core.Systems.States.TransitionFeatureKind.Guard })) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the TransitionUsageMember rule (StateBodyItem).
        /// <para><c>TransitionUsageMember : FeatureMembership = MemberPrefix ownedRelatedElement += TransitionUsage</c></para>
        /// <para><b>Limitation:</b> <c>TargetTransitionUsage</c> shares the <see cref="ITransitionUsage"/>
        /// metaclass. Distinguishing them at runtime relies on TargetTransitionUsage's signature
        /// feature (a leading empty parameter member) — see <see cref="IsValidForTargetTransitionUsageMember"/>.
        /// Dispatchers should check TargetTransitionUsageMember BEFORE TransitionUsageMember so this
        /// broader predicate only matches the residual transition case.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership owns an <see cref="ITransitionUsage"/></returns>
        internal static bool IsValidForTransitionUsageMember(this IFeatureMembership featureMembership)
        {
            return featureMembership?.OwnedRelatedElement.OfType<ITransitionUsage>().Any() == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the TargetTransitionUsageMember rule (StateBodyItem).
        /// <para><c>TargetTransitionUsageMember : FeatureMembership = MemberPrefix ownedRelatedElement += TargetTransitionUsage</c></para>
        /// <para><c>TargetTransitionUsage : TransitionUsage = ownedRelationship += EmptyParameterMember …</c></para>
        /// <para><b>Limitation:</b> <c>TargetTransitionUsage</c> shares the <see cref="ITransitionUsage"/>
        /// metaclass with plain <c>TransitionUsage</c>. Heuristic distinguisher: the first owned
        /// relationship is an <see cref="IParameterMembership"/> whose parameter carries no owned
        /// related element (the "empty parameter" marker). This is pattern-based and may not
        /// perfectly capture the parse context.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership owns a transition usage whose first parameter is empty</returns>
        internal static bool IsValidForTargetTransitionUsageMember(this IFeatureMembership featureMembership)
        {
            return featureMembership?.OwnedRelatedElement.OfType<ITransitionUsage>().Any(transition =>
                transition.OwnedRelationship.OfType<IParameterMembership>().FirstOrDefault() is IParameterMembership parameterMembership
                && parameterMembership.OwnedRelatedElement.Count == 0) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the EntryActionMember rule (StateBodyItem).
        /// <para><c>EntryActionMember : StateSubactionMembership = MemberPrefix kind = 'entry' …</c></para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership is a <see cref="IStateSubactionMembership"/> with <c>Kind == Entry</c></returns>
        internal static bool IsValidForEntryActionMember(this IFeatureMembership featureMembership)
        {
            return featureMembership is IStateSubactionMembership { Kind: SysML2.NET.Core.Systems.States.StateSubactionKind.Entry };
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the EntryTransitionMember rule (StateBodyItem).
        /// <para><c>EntryTransitionMember : FeatureMembership = MemberPrefix (ownedRelatedElement += GuardedTargetSuccession | 'then' ownedRelatedElement += TargetSuccession) ';'</c></para>
        /// <para>GuardedTargetSuccession is an <see cref="ITransitionUsage"/>; TargetSuccession is an
        /// <see cref="ISuccessionAsUsage"/>.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership owns a succession or transition usage</returns>
        internal static bool IsValidForEntryTransitionMemberRule(this IFeatureMembership featureMembership)
        {
            return featureMembership?.OwnedRelatedElement.Any(element => element is ITransitionUsage or ISuccessionAsUsage) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the DoActionMember rule (StateBodyItem).
        /// <para><c>DoActionMember : StateSubactionMembership = MemberPrefix kind = 'do' …</c></para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership is a <see cref="IStateSubactionMembership"/> with <c>Kind == Do</c></returns>
        internal static bool IsValidForDoActionMember(this IFeatureMembership featureMembership)
        {
            return featureMembership is IStateSubactionMembership { Kind: SysML2.NET.Core.Systems.States.StateSubactionKind.Do };
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the ExitActionMember rule (StateBodyItem).
        /// <para><c>ExitActionMember : StateSubactionMembership = MemberPrefix kind = 'exit' …</c></para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if the membership is a <see cref="IStateSubactionMembership"/> with <c>Kind == Exit</c></returns>
        internal static bool IsValidForExitActionMember(this IFeatureMembership featureMembership)
        {
            return featureMembership is IStateSubactionMembership { Kind: SysML2.NET.Core.Systems.States.StateSubactionKind.Exit };
        }

        /// <summary>
        /// Asserts that the <see cref="IElement"/> matches the lexical <c>BASIC_NAME</c> rule
        /// (i.e. the name would be expressed as a <c>BASIC_NAME</c> rather than an
        /// <c>UNRESTRICTED_NAME</c>). Used to dispatch inside the generated <c>NAME</c>
        /// switch inlined by <c>QualifiedName</c>.
        /// <para>Delegates to <see cref="StringExtensions.QueryIsValidBasicName"/>, which checks
        /// the declared name against the <c>[a-zA-Z_][a-zA-Z0-9_]*</c> pattern and excludes
        /// reserved keywords.</para>
        /// </summary>
        /// <param name="element">The <see cref="IElement"/></param>
        /// <returns>True if the element's declared name is a valid basic name</returns>
        internal static bool IsValidForBASIC_NAME(this IElement element)
        {
            return !string.IsNullOrWhiteSpace(element?.DeclaredName) && element.DeclaredName.QueryIsValidBasicName();
        }

        /// <summary>
        /// Asserts that the <see cref="IOwningMembership"/> contains at least one <see cref="IFeature"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="owningMembership">The <see cref="IOwningMembership"/></param>
        /// <returns>True if one <see cref="IFeature"/> is contained in the <see cref="IRelationship.OwnedRelatedElement"/></returns>
        internal static bool IsValidForNonFeatureMember(this IOwningMembership owningMembership)
        {
            return owningMembership.OwnedRelatedElement.OfType<IFeature>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IOwningMembership"/> does not contain any <see cref="IFeature"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="owningMembership">The <see cref="IOwningMembership"/></param>
        /// <returns>True if no <see cref="IFeature"/> is contained in the <see cref="IRelationship.OwnedRelatedElement"/></returns>
        internal static bool IsValidForFeatureMember(this IOwningMembership owningMembership)
        {
            return !owningMembership.IsValidForNonFeatureMember();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> contains at least one <see cref="ISuccessionAsUsage"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if it contains one <see cref="ISuccessionAsUsage"/></returns>
        internal static bool IsValidForSourceSuccessionMember(this IFeatureMembership featureMembership)
        {
            return featureMembership.OwnedRelatedElement.OfType<ISuccessionAsUsage>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> contains at least one <see cref="IOccurrenceUsage"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if it contains one <see cref="IOccurrenceUsage"/></returns>
        internal static bool IsValidForOccurrenceUsageMember(this IFeatureMembership featureMembership)
        {
            return featureMembership.OwnedRelatedElement.OfType<IOccurrenceUsage>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> contains at least one <see cref="IUsage"/>
        /// but no <see cref="IOccurrenceUsage"/> inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if it contains one <see cref="IUsage"/> but no <see cref="IOccurrenceUsage"/></returns>
        internal static bool IsValidForNonOccurrenceUsageMember(this IFeatureMembership featureMembership)
        {
            return !featureMembership.IsValidForOccurrenceUsageMember() && featureMembership.OwnedRelatedElement.OfType<IUsage>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> has valid element types for StructureUsageMember
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <returns>True if contains any of the required element types</returns>
        internal static bool IsValidForStructureUsageMember(this IFeatureMembership featureMembership)
        {
            return featureMembership.OwnedRelatedElement.OfType<OccurrenceUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<EventOccurrenceUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<ItemUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<PartUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<ViewUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<RenderingUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<PortUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<ConnectionUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<InterfaceUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<AllocationUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<FlowUsage>().Any()
                   || featureMembership.OwnedRelatedElement.OfType<SuccessionFlowUsage>().Any();
        }
    }
}
