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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Collections.Frozen;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Dependencies;
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
        /// <para>Matches when the feature's <c>ownedRelationship</c> cursor is currently
        /// positioned at an <see cref="IFeatureTyping"/>.</para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="IFeatureTyping"/></returns>
        internal static bool IsValidForTypings(this IFeature feature, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(feature, writerContext) is IFeatureTyping;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the Subsettings rule.
        /// <para><c>Subsettings : Feature = Subsets (',' ownedRelationship += OwnedSubsetting)*</c></para>
        /// <para>Matches when the cursor is at an <see cref="ISubsetting"/> that is NOT one of the
        /// more specific subtypes (<see cref="IRedefinition"/>, <see cref="IReferenceSubsetting"/>,
        /// <see cref="ICrossSubsetting"/>), each of which has its own dedicated rule.</para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is a plain <see cref="ISubsetting"/></returns>
        internal static bool IsValidForSubsettings(this IFeature feature, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(feature, writerContext) is ISubsetting and not IRedefinition and not IReferenceSubsetting and not ICrossSubsetting;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the References rule.
        /// <para><c>References : Feature = REFERENCES ownedRelationship += OwnedReferenceSubsetting</c></para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="IReferenceSubsetting"/></returns>
        internal static bool IsValidForReferences(this IFeature feature, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(feature, writerContext) is IReferenceSubsetting;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the Crosses rule.
        /// <para><c>Crosses : Feature = CROSSES ownedRelationship += OwnedCrossSubsetting</c></para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="ICrossSubsetting"/></returns>
        internal static bool IsValidForCrosses(this IFeature feature, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(feature, writerContext) is ICrossSubsetting;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the ChainingPart rule.
        /// <para><c>ChainingPart : Feature = 'chains' (ownedRelationship += OwnedFeatureChaining | FeatureChain)</c></para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="IFeatureChaining"/></returns>
        internal static bool IsValidForChainingPart(this IFeature feature, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(feature, writerContext) is IFeatureChaining;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the InvertingPart rule.
        /// <para><c>InvertingPart : Feature = 'inverse' 'of' ownedRelationship += OwnedFeatureInverting</c></para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="IFeatureInverting"/></returns>
        internal static bool IsValidForInvertingPart(this IFeature feature, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(feature, writerContext) is IFeatureInverting;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeature"/> is valid for the PositionalArgumentList rule.
        /// <para><c>PositionalArgumentList : Feature = ownedRelationship += ArgumentMember (',' ownedRelationship += ArgumentMember)*</c></para>
        /// <para>Matches when the cursor is positioned at an <see cref="IParameterMembership"/>
        /// (positional arguments) — the alternative <c>NamedArgumentList</c> uses plain
        /// <see cref="IFeatureMembership"/> members.</para>
        /// </summary>
        /// <param name="feature">The <see cref="IFeature"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="IParameterMembership"/></returns>
        internal static bool IsValidForPositionalArgumentList(this IFeature feature, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(feature, writerContext) is IParameterMembership;
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the DisjoiningPart rule.
        /// <para><c>DisjoiningPart : Type = 'disjoint' 'from' ownedRelationship += OwnedDisjoining (',' ownedRelationship += OwnedDisjoining)*</c></para>
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="IDisjoining"/></returns>
        internal static bool IsValidForDisjoiningPart(this IType type, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(type, writerContext) is IDisjoining;
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the UnioningPart rule.
        /// <para><c>UnioningPart : Type = 'unions' ownedRelationship += Unioning (',' ownedRelationship += Unioning)*</c></para>
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="IUnioning"/></returns>
        internal static bool IsValidForUnioningPart(this IType type, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(type, writerContext) is IUnioning;
        }

        /// <summary>
        /// Asserts that the <see cref="IType"/> is valid for the IntersectingPart rule.
        /// <para><c>IntersectingPart : Type = 'intersects' ownedRelationship += Intersecting (',' ownedRelationship += Intersecting)*</c></para>
        /// </summary>
        /// <param name="type">The <see cref="IType"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the cursor's current element is an <see cref="IIntersecting"/></returns>
        internal static bool IsValidForIntersectingPart(this IType type, TextualNotationWriterContext writerContext)
        {
            return QueryCurrentOwnedRelationship(type, writerContext) is IIntersecting;
        }

        /// <summary>
        /// Returns the current element under the <c>ownedRelationship</c> cursor for the given
        /// <paramref name="element"/>, or <c>null</c> when context/cursor cannot be obtained.
        /// </summary>
        /// <param name="element">The <see cref="IFeature"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>The current cursor element, or <c>null</c></returns>
        private static IElement QueryCurrentOwnedRelationship(IElement element, TextualNotationWriterContext writerContext)
        {
            if (element == null || writerContext?.CursorCache == null)
            {
                return null;
            }

            return writerContext.CursorCache.GetOrCreateCursor(element.Id, "ownedRelationship", element.OwnedRelationship).Current;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the BehaviorUsageMember rule.
        /// <para><c>BehaviorUsageMember : FeatureMembership = MemberPrefix ownedRelatedElement += BehaviorUsageElement</c></para>
        /// <para><c>BehaviorUsageElement : Usage = ActionUsage | CalculationUsage | StateUsage | ConstraintUsage | RequirementUsage | ConcernUsage | CaseUsage | AnalysisCaseUsage | VerificationCaseUsage | UseCaseUsage | ViewpointUsage | PerformActionUsage | ExhibitStateUsage | IncludeUseCaseUsage | AssertConstraintUsage | SatisfyRequirementUsage</c></para>
        /// <para>The disjunction below covers these root metaclasses; the remaining grammar alternatives
        /// all inherit transitively (PerformActionUsage/ExhibitStateUsage/IncludeUseCaseUsage are
        /// <see cref="IActionUsage"/> descendants, AssertConstraintUsage inherits <see cref="IConstraintUsage"/>,
        /// SatisfyRequirementUsage inherits <see cref="IRequirementUsage"/>).</para>
        /// <para>Several other <see cref="IActionUsage"/> descendants are NOT BehaviorUsageElement
        /// alternatives — they belong to the sibling <c>ActionNodeMember</c> rule
        /// (<see cref="IControlNode"/>, <see cref="ISendActionUsage"/>, <see cref="IAcceptActionUsage"/>,
        /// <see cref="IAssignmentActionUsage"/>, <see cref="ITerminateActionUsage"/>,
        /// <see cref="IIfActionUsage"/>, <see cref="ILoopActionUsage"/>) or to the
        /// <c>TransitionUsage</c> / <c>TargetTransitionUsage</c> rules
        /// (<see cref="ITransitionUsage"/>). Without the explicit exclusions below, a membership
        /// owning e.g. a <see cref="IControlNode"/> would be silently mis-routed to
        /// <c>BuildBehaviorUsageMember</c> by the dispatchers at
        /// <c>FeatureMembershipTextualNotationBuilder.cs:271</c> and
        /// <c>TypeTextualNotationBuilder.cs:289/318</c>, emitting the literal <c>action</c> keyword
        /// instead of the expected ActionNode keyword (<c>merge</c>, <c>fork</c>, <c>decision</c>,
        /// <c>join</c>, <c>send</c>, <c>accept</c>, <c>assign</c>, <c>terminate</c>, <c>if</c>,
        /// <c>while</c>, <c>for</c>) or the transition keyword.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership owns a behavior-usage element (and that element is not routed by a more specific sibling rule)</returns>
        internal static bool IsValidForBehaviorUsageMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership?.OwnedRelatedElement.Any(element =>
                (element is (IActionUsage or IStateUsage or IConstraintUsage
                    or IRequirementUsage or ICaseUsage) and not IControlNode and not ISendActionUsage and not IAcceptActionUsage and not IAssignmentActionUsage and not ITerminateActionUsage and not IIfActionUsage and not ILoopActionUsage and not ITransitionUsage)) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IConnector"/> is valid for the BinaryConnectorDeclaration rule.
        /// <para><c>BinaryConnectorDeclaration : Connector = … ownedRelationship += ConnectorEndMember 'to' ownedRelationship += ConnectorEndMember</c></para>
        /// <para>NaryConnectorDeclaration has three or more. <c>ConnectorEndMember : EndFeatureMembership</c>
        /// so exactly two <see cref="IEndFeatureMembership"/> entries = binary.</para>
        /// </summary>
        /// <param name="connector">The <see cref="IConnector"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the connector owns exactly two <see cref="IEndFeatureMembership"/> children</returns>
        internal static bool IsValidForBinaryConnectorDeclaration(this IConnector connector, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the connection usage owns exactly two <see cref="IEndFeatureMembership"/> children</returns>
        internal static bool IsValidForBinaryConnectorPart(this IConnectionUsage connectionUsage, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the interface usage owns exactly two <see cref="IEndFeatureMembership"/> children</returns>
        internal static bool IsValidForBinaryInterfacePart(this IInterfaceUsage interfaceUsage, TextualNotationWriterContext writerContext)
        {
            return interfaceUsage?.OwnedRelationship.OfType<IEndFeatureMembership>().Count() == 2;
        }

        /// <summary>
        /// Asserts that the <see cref="IUsage"/> is valid for the NonOccurrenceUsageElement rule.
        /// <para><c>NonOccurrenceUsageElement : Usage = DefaultReferenceUsage | ReferenceUsage |
        /// AttributeUsage | EnumerationUsage | BindingConnectorAsUsage | SuccessionAsUsage | ExtendedUsage</c></para>
        /// <para>None of these alternatives target <see cref="IOccurrenceUsage"/> or its
        /// subclasses. The sibling rule <c>OccurrenceUsageElement</c> handles
        /// <see cref="IOccurrenceUsage"/> instances (items, parts, actions, etc.). Thus the
        /// runtime discriminator is purely class-based: anything that is NOT an
        /// <see cref="IOccurrenceUsage"/> flows to the non-occurrence branch.</para>
        /// </summary>
        /// <param name="usage">The <see cref="IUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the usage is not an <see cref="IOccurrenceUsage"/></returns>
        internal static bool IsValidForNonOccurrenceUsageElement(this IUsage usage, TextualNotationWriterContext writerContext)
        {
            return usage is not IOccurrenceUsage;
        }

        /// <summary>
        /// Asserts that the <see cref="IReferenceUsage"/> is valid for the DefaultReferenceUsage rule.
        /// <para><c>DefaultReferenceUsage : ReferenceUsage = RefPrefix Usage</c> — the form
        /// WITHOUT the <c>'ref'</c> keyword.</para>
        /// <para><c>ReferenceUsage = ( EndUsagePrefix | RefPrefix ) 'ref' Usage</c> — the form
        /// WITH the <c>'ref'</c> keyword, which sets <see cref="IUsage.IsReference"/> to <c>true</c>
        /// via <c>BasicUsagePrefix</c>'s <c>isReference ?= 'ref'</c>.</para>
        /// <para><c>isReference</c> CANNOT discriminate the two: it is derived as
        /// <c>not isComposite</c>, and per the OMG SysML v2 spec, Clause 7.6.4 a reference usage
        /// "is always, by definition, referential" — so the property is vacuously <c>true</c> for
        /// every <see cref="IReferenceUsage"/> and the guard would never select the default form,
        /// forcing a spurious <c>ref</c> onto every parameter and reference member. The same clause
        /// states the declaration "may, but is not required, to include the <c>ref</c> keyword",
        /// which makes the <c>'ref'</c>-less form the canonical one.</para>
        /// <para>What genuinely separates the alternatives is what each can EXPRESS:</para>
        /// <list type="number">
        /// <item><description>only <c>ReferenceUsage</c> opens with <c>EndUsagePrefix</c>, so an end
        /// feature must take that alternative;</description></item>
        /// <item><description>the <c>'ref'</c>-less form is only ever chosen where the OMG SysML v2
        /// spec, Clause 7.6.3 says the keyword adds nothing: "a directed usage is always referential,
        /// whether or not the keyword <c>ref</c> is also given explicitly in its declaration". No such
        /// statement covers the other <c>RefPrefix</c> flags, and the pilot sources bear that out —
        /// <c>in fuelCmd : FuelCmd;</c> drops the keyword while <c>abstract ref :&gt;&gt; trailerHitch[1];</c>
        /// keeps it — so <c>direction</c> is the sole discriminator.</description></item>
        /// </list>
        /// </summary>
        /// <param name="referenceUsage">The <see cref="IReferenceUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the reference usage renders through the <c>'ref'</c>-less default form</returns>
        internal static bool IsValidForDefaultReferenceUsage(this IReferenceUsage referenceUsage, TextualNotationWriterContext writerContext)
        {
            if (referenceUsage.IsEnd)
            {
                return false;
            }

            // A directed usage is always referential (Clause 7.6.3), so the keyword adds nothing.
            if (referenceUsage.Direction.HasValue)
            {
                return true;
            }

            // Otherwise the keyword adds nothing only when there is no declaration for it to qualify: no
            // name and an empty RefPrefix, i.e. the whole usage is a bare redefinition such as
            // `:>> mass = m`. The pilot bears this out — it writes `ref vehicle: VehicleA` (named) and
            // `abstract ref :>> trailerHitch[1]` (RefPrefix carries `abstract`) but plain `:>> mass = m`.
            return string.IsNullOrWhiteSpace(referenceUsage.DeclaredName)
                   && string.IsNullOrWhiteSpace(referenceUsage.DeclaredShortName)
                   && !referenceUsage.IsDerived
                   && !referenceUsage.IsAbstract
                   && !referenceUsage.IsVariation
                   && !referenceUsage.IsConstant;
        }

        /// <summary>
        /// Asserts that the <c>isReference ?= 'ref'</c> keyword of the BasicUsagePrefix rule carries
        /// information for the supplied <see cref="IUsage"/>.
        /// <para><c>BasicUsagePrefix : Usage = RefPrefix ( isReference ?= 'ref' )?</c></para>
        /// <para><see cref="IUsage.isReference"/> is DERIVED (<c>isReference = not isComposite</c>),
        /// so a <c>true</c> value does not imply the keyword was written. The metamodel constraint
        /// <c>validateUsageIsReferential</c> — <c>direction &lt;&gt; null or isEnd or
        /// featuringType-&gt;isEmpty() implies isReference</c> — forces the value in three contexts,
        /// and the OMG SysML v2 spec, Clause 7.6.3 confirms the notation stays silent in them: "a
        /// directed usage is always referential, whether or not the keyword <c>ref</c> is also given
        /// explicitly in its declaration".</para>
        /// <para>A fourth context follows from the OMG SysML v2 spec, Clause 7.9.1: "If an
        /// occurrence definition or usage has nested composite features, then those features must
        /// also be usages of occurrence definitions". Only an <see cref="IOccurrenceUsage"/> can
        /// therefore be composite, which makes every other kind of usage — attributes, reference
        /// usages, binding connectors, successions — necessarily referential and the keyword
        /// redundant.</para>
        /// <para><see cref="IPortUsage"/> narrows it once more via <c>validatePortUsageIsReference</c>
        /// — <c>owningType = null or not owningType.oclIsKindOf(PortDefinition) and not
        /// owningType.oclIsKindOf(PortUsage) implies isReference</c> — so a port is referential unless
        /// it is a subport, and the pilot sources accordingly notate <c>port fuelCmdPort : FuelCmdPort;</c>
        /// with no <c>ref</c>. Together with <c>validateEventOccurrenceUsageIsReference</c> (handled by
        /// the generated type-level exclusion, since <c>EventOccurrenceUsage::isReference</c> defaults
        /// to <c>true</c>) these are the ONLY constraints in the metamodel that force
        /// <c>isReference</c>.</para>
        /// <para>The featuring test uses <see cref="IFeature.owningType"/> rather than
        /// <see cref="IFeature.featuringType"/>: a Usage acquires a featuringType only by being an
        /// owned feature (its owning <c>FeatureMembership</c> IS the <c>TypeFeaturing</c>), which is
        /// exactly what <see cref="IFeature.owningType"/> reports.</para>
        /// </summary>
        /// <param name="usage">The <see cref="IUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True when <c>ref</c> distinguishes this usage from the composite default</returns>
        internal static bool IsValidForUsageIsReference(this IUsage usage, TextualNotationWriterContext writerContext)
        {
            if (usage is not IOccurrenceUsage || usage.Direction.HasValue || usage.IsEnd || usage.owningType == null)
            {
                return false;
            }

            return usage is not IPortUsage || usage.owningType is IPortDefinition or IPortUsage;
        }

        /// <summary>
        /// Asserts that the <see cref="IReferenceUsage"/> is valid for the VariantReference rule.
        /// <para><c>VariantReference : ReferenceUsage = ownedRelationship += OwnedReferenceSubsetting
        /// FeatureSpecialization* UsageBody</c> — a reference-usage form that carries an owned
        /// <see cref="IReferenceSubsetting"/> in its relationships.</para>
        /// <para>The sibling alternative <c>ReferenceUsage = ( EndUsagePrefix | RefPrefix ) 'ref' Usage</c>
        /// does not produce an <see cref="IReferenceSubsetting"/> at the reference-usage's own
        /// relationship level, so the presence of one discriminates the variant form.</para>
        /// </summary>
        /// <param name="referenceUsage">The <see cref="IReferenceUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the reference usage has an owned <see cref="IReferenceSubsetting"/></returns>
        internal static bool IsValidForVariantReference(this IReferenceUsage referenceUsage, TextualNotationWriterContext writerContext)
        {
            return referenceUsage.OwnedRelationship.OfType<IReferenceSubsetting>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IUsage"/> is valid for the StructureUsageElement rule.
        /// <para><c>StructureUsageElement : Usage = OccurrenceUsage | IndividualUsage | PortionUsage
        /// | EventOccurrenceUsage | ItemUsage | PartUsage | ViewUsage | RenderingUsage | PortUsage
        /// | ConnectionUsage | InterfaceUsage | AllocationUsage | Message | FlowUsage | SuccessionFlowUsage</c></para>
        /// <para>Matches any structural-usage metaclass. IndividualUsage/PortionUsage/EventOccurrenceUsage
        /// inherit <see cref="IOccurrenceUsage"/>; Message and SuccessionFlowUsage inherit
        /// <see cref="IFlowUsage"/>.</para>
        /// <para>The BehaviorUsageElement subtypes (<see cref="IActionUsage"/>, <see cref="IConstraintUsage"/>
        /// and their descendants) ALSO inherit <see cref="IOccurrenceUsage"/> via the metaclass hierarchy
        /// (e.g. <c>IConstraintUsage : IOccurrenceUsage</c>, <c>IActionUsage : IOccurrenceUsage</c>),
        /// so the bare <c>is IOccurrenceUsage</c> would silently pull them in. They must be explicitly
        /// excluded so the upstream dispatcher (<c>BuildOccurrenceUsageElement</c>) routes them to
        /// <c>BuildBehaviorUsageElement</c> instead — without this exclusion they fall through the
        /// <c>BuildStructureUsageElement</c> switch to the bare <c>case IOccurrenceUsage → BuildPortionUsage</c>
        /// arm, which mis-renders constraints as <c>#name;</c>.</para>
        /// <para><see cref="IFlowUsage"/> is itself <see cref="IActionUsage"/> (per
        /// <c>IFlowUsage : IConnectorAsUsage, IFlow, IActionUsage</c>), but it is a
        /// StructureUsageElement alternative, so the IActionUsage exclusion makes an explicit
        /// IFlowUsage exception.</para>
        /// </summary>
        /// <param name="usage">The <see cref="IUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the usage is a structural-usage metaclass (and not a behavior-usage subtype routed by a sibling rule)</returns>
        internal static bool IsValidForStructureUsageElement(this IUsage usage, TextualNotationWriterContext writerContext)
        {
            return (usage is (IOccurrenceUsage or IItemUsage or IPartUsage or IViewUsage
                or IRenderingUsage or IPortUsage or IConnectionUsage or IInterfaceUsage
                or IAllocationUsage or IFlowUsage) and not IConstraintUsage and (not IActionUsage or IFlowUsage));
        }

        /// <summary>
        /// Asserts that the <see cref="IOccurrenceUsage"/> is valid for the OccurrenceUsage rule.
        /// <para><c>OccurrenceUsage = OccurrenceUsagePrefix 'occurrence' Usage</c> — the general case
        /// where neither <c>isIndividual</c> nor <c>portionKind</c> has been set by one of the more
        /// specific rules (<c>IndividualUsage</c>, <c>PortionUsage</c>).</para>
        /// <para>The bare-keyword form must NOT match runtime types covered by sibling rules in
        /// <c>StructureUsageElement</c> (<see cref="IItemUsage"/>, <see cref="IPortUsage"/>,
        /// <see cref="IEventOccurrenceUsage"/>) or <c>BehaviorUsageElement</c>
        /// (<see cref="IActionUsage"/>, <see cref="IConstraintUsage"/>) — each of those has its
        /// own dedicated grammar rule and dispatch arm. Without these exclusions, a constraint
        /// or action would be silently rendered as <c>occurrence&#160;name;</c>.</para>
        /// </summary>
        /// <param name="occurrenceUsage">The <see cref="IOccurrenceUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the usage is a plain occurrence (no individual, no portion kind, and not routed by a more specific sibling rule)</returns>
        internal static bool IsValidForOccurrenceUsage(this IOccurrenceUsage occurrenceUsage, TextualNotationWriterContext writerContext)
        {
            return occurrenceUsage is { IsIndividual: false, PortionKind: null } and not IItemUsage and not IPortUsage and not IActionUsage and not IConstraintUsage and not IEventOccurrenceUsage;
        }

        /// <summary>
        /// Asserts that the <see cref="IOccurrenceUsage"/> is valid for the IndividualUsage rule.
        /// <para><c>IndividualUsage : OccurrenceUsage = BasicUsagePrefix isIndividual ?= 'individual' UsageExtensionKeyword* Usage</c></para>
        /// <para><c>PortionUsage</c> can also set <c>isIndividual</c>, so this guard additionally
        /// requires <c>PortionKind</c> to be unset — otherwise the usage is a <c>PortionUsage</c>
        /// and should flow to the default case.</para>
        /// </summary>
        /// <param name="occurrenceUsage">The <see cref="IOccurrenceUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the usage is an individual (not a portion)</returns>
        internal static bool IsValidForIndividualUsage(this IOccurrenceUsage occurrenceUsage, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the definition is a plain occurrence (not an individual)</returns>
        internal static bool IsValidForOccurrenceDefinition(this IOccurrenceDefinition occurrenceDefinition, TextualNotationWriterContext writerContext)
        {
            return occurrenceDefinition is { IsIndividual: false };
        }

        /// <summary>
        /// Asserts that the <see cref="IFlowUsage"/> is valid for the FlowUsage rule (as opposed to
        /// the Message rule, which forces <c>isAbstract = true</c>).
        /// <para><c>FlowUsage = OccurrenceUsagePrefix 'flow' FlowDeclaration DefinitionBody</c></para>
        /// </summary>
        /// <param name="flowUsage">The <see cref="IFlowUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the usage is not abstract (i.e. not a Message)</returns>
        internal static bool IsValidForFlowUsage(this IFlowUsage flowUsage, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the usage is abstract (a Message)</returns>
        internal static bool IsValidForMessage(this IFlowUsage flowUsage, TextualNotationWriterContext writerContext)
        {
            return flowUsage is { IsAbstract: true };
        }

        /// <summary>
        /// Asserts that the <see cref="ITransitionUsage"/> is valid for the GuardedTargetSuccession rule.
        /// <para><c>GuardedTargetSuccession : TransitionUsage = ownedRelationship += GuardExpressionMember 'then' ownedRelationship += TransitionSuccessionMember</c></para>
        /// <para><c>GuardExpressionMember : TransitionFeatureMembership = 'if' { kind = 'guard' } …</c></para>
        /// </summary>
        /// <param name="transitionUsage">The <see cref="ITransitionUsage"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the transition owns a guard-kind transition feature membership</returns>
        internal static bool IsValidForGuardedTargetSuccession(this ITransitionUsage transitionUsage, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership owns an <see cref="IActionUsage"/></returns>
        internal static bool IsValidForActionBodyParameterMember(this IParameterMembership parameterMembership, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the expression's <c>Operator</c> is <c>"if"</c></returns>
        internal static bool IsValidForConditionalExpression(this IOperatorExpression operatorExpression, TextualNotationWriterContext writerContext)
        {
            return operatorExpression?.Operator == "if";
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the ConditionalBinaryOperatorExpression rule
        /// <para><c>operator = ConditionalBinaryOperator</c> where <c>ConditionalBinaryOperator = '??' | 'or' | 'and' | 'implies'</c></para>
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the expression's <c>Operator</c> is one of the conditional binary operators</returns>
        internal static bool IsValidForConditionalBinaryOperatorExpression(this IOperatorExpression operatorExpression, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the expression matches the BinaryOperatorExpression rule</returns>
        internal static bool IsValidForBinaryOperatorExpression(this IOperatorExpression operatorExpression, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the expression matches the UnaryOperatorExpression rule</returns>
        internal static bool IsValidForUnaryOperatorExpression(this IOperatorExpression operatorExpression, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the expression's <c>Operator</c> is a classification-test or cast operator</returns>
        internal static bool IsValidForClassificationExpression(this IOperatorExpression operatorExpression, TextualNotationWriterContext writerContext)
        {
            return operatorExpression?.Operator is not null && ClassificationExpressionOperators.Contains(operatorExpression.Operator);
        }

        /// <summary>
        /// Asserts that the <see cref="IOperatorExpression"/> is valid for the MetaclassificationExpression rule.
        /// <para><c>MetaclassificationExpression : OperatorExpression = ownedRelationship += MetadataArgumentMember
        /// ( operator = MetaclassificationTestOperator ownedRelationship += TypeReferenceMember
        /// | operator = MetaCastOperator ownedRelationship += TypeResultMember )
        /// ownedRelationship += EmptyResultMember</c></para>
        /// <para>Matches when the operator is <c>'@@'</c> (<c>MetaclassificationTestOperator</c>) or
        /// <c>'meta'</c> (<c>MetaCastOperator</c>). Those sets are DISJOINT from
        /// <c>ClassificationExpression</c>'s (<c>'istype' | 'hastype' | '@'</c> and <c>'as'</c>), so the
        /// operator alone discriminates the two rules exactly — no structural inspection of the
        /// <c>MetadataArgumentMember</c> is required, and the switch-dispatch order between them does not
        /// matter.</para>
        /// </summary>
        /// <param name="operatorExpression">The <see cref="IOperatorExpression"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the expression's <c>Operator</c> is a meta-cast or metaclassification-test operator</returns>
        internal static bool IsValidForMetaclassificationExpression(this IOperatorExpression operatorExpression, TextualNotationWriterContext writerContext)
        {
            return operatorExpression?.Operator is not null && MetaclassificationExpressionOperators.Contains(operatorExpression.Operator);
        }

        /// <summary>
        /// Asserts that the <see cref="IExpression"/> is valid for the SequenceExpression rule.
        /// <para><c>SequenceExpression : Expression = '(' SequenceExpressionList ')'</c></para>
        /// <para>SequenceExpression is the wrapping <c>(…)</c> rule that applies to expressions
        /// which are not one of the more specific <c>BaseExpression</c> variants
        /// (<c>NullExpression | LiteralExpression | FeatureReferenceExpression |
        /// MetadataAccessExpression | InvocationExpression | ConstructorExpression | BodyExpression</c>).
        /// The grammar lists SequenceExpression before BaseExpression as an alternative, but at
        /// unparse time we don't have the surface-text parens to discriminate, so this guard
        /// must explicitly exclude the BaseExpression metaclasses to prevent it from swallowing
        /// them.</para>
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>
        /// True when the expression is not null and is not one of the runtime types handled
        /// by <c>BaseExpression</c>'s dispatch (<see cref="INullExpression"/>,
        /// <see cref="ILiteralExpression"/>, <see cref="IFeatureReferenceExpression"/>,
        /// <see cref="IMetadataAccessExpression"/>, <see cref="IInvocationExpression"/>,
        /// <see cref="IConstructorExpression"/>); false otherwise.
        /// </returns>
        internal static bool IsValidForSequenceExpression(this IExpression expression, TextualNotationWriterContext writerContext)
        {
            return expression is not null
                && expression is not INullExpression
                && expression is not ILiteralExpression
                && expression is not IFeatureReferenceExpression
                && expression is not IMetadataAccessExpression
                && expression is not IInvocationExpression
                && expression is not IConstructorExpression;
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True for any non-null expression</returns>
        internal static bool IsValidForFeatureReferenceExpression(this IFeatureReferenceExpression featureReferenceExpression, TextualNotationWriterContext writerContext)
        {
            return featureReferenceExpression is not null;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the InitialNodeMember rule (ActionBodyItem).
        /// <para><c>InitialNodeMember : FeatureMembership = MemberPrefix 'first' memberFeature = [QualifiedName] RelationshipBody</c></para>
        /// <para>The receiver is <see cref="IMembership"/>, NOT <see cref="IFeatureMembership"/> as the
        /// rule's declared target suggests. The rule cannot be satisfied by a
        /// <see cref="IFeatureMembership"/>: that metaclass specializes <c>OwningMembership</c> and so
        /// OWNS its member, whereas <c>memberFeature = [QualifiedName]</c> is a CROSS-REFERENCE to an
        /// element owned elsewhere (<c>first start</c> names <c>Actions::Action::start</c> in the Systems
        /// Library). A plain <see cref="IMembership"/> carrying <see cref="IMembership.MemberElement"/> is
        /// therefore the only metamodel-valid encoding, and it is what the pilot exports. Note also that
        /// <c>memberFeature</c> does not exist as a property anywhere in the metamodel — the rule names a
        /// property the abstract syntax does not define (still true in release 2026-05).</para>
        /// <para>Discriminated from the sibling <c>AliasMember : Membership = MemberPrefix 'alias'
        /// ( '&lt;' memberShortName = NAME '&gt;' )? ( memberName = NAME )? 'for'
        /// memberElement = [QualifiedName] RelationshipBody</c>, which has the same runtime shape, by the
        /// ABSENCE of a member name: an alias exists solely to bind a new name, so a nameless membership
        /// cannot be one. Without that test <c>first start;</c> was emitted as
        /// <c>alias for Actions::Action::start;</c>.</para>
        /// </summary>
        /// <param name="membership">The <see cref="IMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership references its target via cross-reference and binds no name</returns>
        internal static bool IsValidForInitialNodeMember(this IMembership membership, TextualNotationWriterContext writerContext)
        {
            return membership is { MemberElement: not null }
                && membership.OwnedRelatedElement.Count == 0
                && string.IsNullOrWhiteSpace(membership.MemberName)
                && string.IsNullOrWhiteSpace(membership.MemberShortName);
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the ActionTargetSuccessionMember rule (ActionBodyItem).
        /// <para><c>ActionTargetSuccessionMember : FeatureMembership = MemberPrefix ownedRelatedElement += ActionTargetSuccession</c></para>
        /// <para><c>ActionTargetSuccession : Usage = (TargetSuccession:SuccessionAsUsage | GuardedTargetSuccession:TransitionUsage | DefaultTargetSuccession:TransitionUsage) UsageBody</c></para>
        /// <para><c>TargetSuccession : SuccessionAsUsage = ownedRelationship += SourceEndMember 'then' ownedRelationship += ConnectorEndMember</c>,
        /// and <c>ConnectorEnd : ReferenceUsage = … ownedRelationship += OwnedReferenceSubsetting</c> — the target
        /// end therefore ALWAYS names its target. <c>SourceSuccession : SuccessionAsUsage = ownedRelationship += SourceEndMember</c>
        /// has no such end: its target is the body item that follows the <c>then</c>.</para>
        /// <para>Both forms wrap an <see cref="ISuccessionAsUsage"/>, so the presence of a target end carrying an
        /// <see cref="IReferenceSubsetting"/> is what separates them. Without this check the trailing
        /// <c>( ownedRelationship += ActionTargetSuccessionMember )*</c> loop of <c>ActionBodyItem</c> greedily
        /// swallows the SourceSuccessionMember belonging to the NEXT item, emitting a bare <c>then;</c> and
        /// detaching the action it was meant to precede.</para>
        /// <para>A named target end is necessary but NOT sufficient: <c>TargetSuccession</c> leads with a
        /// <c>SourceEndMember</c>, and <c>SourceEnd : ReferenceUsage = ( ownedRelationship += OwnedMultiplicity )?</c>
        /// carries no <see cref="IReferenceSubsetting"/> — its source is IMPLICITLY the preceding body item, which
        /// is precisely why the notation writes only <c>then target;</c>. The standalone
        /// <c>SuccessionAsUsage = … 'first' ConnectorEndMember 'then' ConnectorEndMember</c> names BOTH ends. So a
        /// succession whose source end names a feature must be rendered <c>first source then target;</c> and must
        /// be rejected here; otherwise the loop absorbs it and the explicit source is silently lost
        /// (<c>first continue then engineStarted;</c> collapsed to <c>then engineStarted;</c>).</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership owns a transition usage, or a succession with a named target end and an implicit source</returns>
        internal static bool IsValidForActionTargetSuccessionMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership?.OwnedRelatedElement.Any(element => element switch
            {
                ISuccessionAsUsage succession => HasNamedTargetEnd(succession) && !HasNamedSourceEnd(succession),
                ITransitionUsage => true,
                _ => false,
            }) == true;
        }

        /// <summary>
        /// Determines whether <paramref name="succession"/> leads with a <c>ConnectorEndMember</c> source end —
        /// an end feature owning an <see cref="IReferenceSubsetting"/> that names where the succession starts.
        /// <para>Its absence is the structural marker of <c>SourceEnd : ReferenceUsage =
        /// ( ownedRelationship += OwnedMultiplicity )?</c>, the anonymous leading end shared by
        /// <c>SourceSuccession</c> and <c>TargetSuccession</c>, whose source is the preceding body item rather
        /// than a named feature.</para>
        /// </summary>
        /// <param name="succession">The <see cref="ISuccessionAsUsage"/> to inspect</param>
        /// <returns>True when the leading end names a feature</returns>
        private static bool HasNamedSourceEnd(ISuccessionAsUsage succession)
        {
            return succession.OwnedRelationship
                .OfType<IEndFeatureMembership>()
                .Take(1)
                .SelectMany(endMembership => endMembership.OwnedRelatedElement.OfType<IFeature>())
                .Any(endFeature => endFeature.OwnedRelationship.OfType<IReferenceSubsetting>().Any());
        }

        /// <summary>
        /// Determines whether <paramref name="succession"/> carries a <c>ConnectorEndMember</c> target end —
        /// that is, an end beyond the leading <c>SourceEndMember</c> whose feature owns an
        /// <see cref="IReferenceSubsetting"/> naming the succession target. This is the structural marker of
        /// the <c>TargetSuccession</c> form against the <c>SourceSuccession</c> form.
        /// </summary>
        /// <param name="succession">The <see cref="ISuccessionAsUsage"/> to inspect</param>
        /// <returns>True when a named target end is present</returns>
        private static bool HasNamedTargetEnd(ISuccessionAsUsage succession)
        {
            return succession.OwnedRelationship
                .OfType<IEndFeatureMembership>()
                .Skip(1)
                .SelectMany(endMembership => endMembership.OwnedRelatedElement.OfType<IFeature>())
                .Any(endFeature => endFeature.OwnedRelationship.OfType<IReferenceSubsetting>().Any());
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the ActionBehaviorMember rule (ActionBodyItem).
        /// <para><c>ActionBehaviorMember : FeatureMembership = BehaviorUsageMember | ActionNodeMember</c></para>
        /// <para><c>ActionNodeMember</c> wraps an <c>ActionNode</c> (ControlNode / SendNode / AcceptNode /
        /// AssignmentNode / TerminateNode / IfNode / WhileLoopNode / ForLoopNode — all <see cref="IActionUsage"/>
        /// descendants). BehaviorUsageMember wraps a BehaviorUsageElement (also mostly <see cref="IActionUsage"/>
        /// or its descendants). The broadest accurate predicate is "owns an <see cref="IActionUsage"/>".</para>
        /// <para><see cref="IFlowUsage"/> (and its <see cref="ISuccessionFlowUsage"/> subtype) IS-A
        /// <see cref="IActionUsage"/> in the metamodel, but appears in NEITHER alternative of this rule —
        /// <c>BehaviorUsageElement</c> and <c>ActionNode</c> both exclude it. A flow reaches the body through
        /// <c>NonBehaviorBodyItem → StructureUsageMember → StructureUsageElement</c> instead, so it must be
        /// rejected here or it is emitted as an empty <c>action { }</c> and its declaration is lost. This
        /// mirrors the complementary carve-out in <see cref="IsValidForStructureUsageMember"/>.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership owns an <see cref="IActionUsage"/> that is not an <see cref="IFlowUsage"/></returns>
        internal static bool IsValidForActionBehaviorMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership?.OwnedRelatedElement.OfType<IActionUsage>().Any(actionUsage => actionUsage is not IFlowUsage) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the GuardedSuccessionMember rule (ActionBodyItem).
        /// <para><c>GuardedSuccessionMember : FeatureMembership = MemberPrefix ownedRelatedElement += GuardedSuccession</c></para>
        /// <para><c>GuardedSuccession : TransitionUsage = ('succession' UsageDeclaration)? 'first' … ownedRelationship += GuardExpressionMember 'then' …</c></para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership owns a transition usage with a guard-kind feature</returns>
        internal static bool IsValidForGuardedSuccessionMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership owns an <see cref="ITransitionUsage"/></returns>
        internal static bool IsValidForTransitionUsageMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership?.OwnedRelatedElement.OfType<ITransitionUsage>().Any() == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the TargetTransitionUsageMember rule (StateBodyItem).
        /// <para><c>TargetTransitionUsageMember : FeatureMembership = MemberPrefix ownedRelatedElement += TargetTransitionUsage</c></para>
        /// <para><c>TargetTransitionUsage : TransitionUsage = ownedRelationship += EmptyParameterMember …</c></para>
        /// <para><c>TargetTransitionUsage</c> shares the <see cref="ITransitionUsage"/> metaclass with plain
        /// <c>TransitionUsage</c>, so two SHAPE conditions separate them here. The transition must be
        /// ANONYMOUS — <c>TargetTransitionUsage</c> has no <c>UsageDeclaration</c> slot, so a named
        /// transition cannot round-trip through it — and it must carry an <c>EmptyParameterMember</c>.</para>
        /// <para>Note the emptiness test: an <c>EmptyParameterMember</c> owns an <c>EmptyUsage</c>
        /// (<c>EmptyUsage : ReferenceUsage = {}</c>), so it has ONE owned related element, not zero.</para>
        /// <para>The remaining condition — that the transition's source is the nearest preceding
        /// <c>BehaviorUsageMember</c> — needs sibling context this signature does not carry, so it lives in
        /// <c>BuildStateBodyItemHandCoded</c>.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership owns an anonymous transition usage with an empty parameter.</returns>
        internal static bool IsValidForTargetTransitionUsageMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership?.OwnedRelatedElement.OfType<ITransitionUsage>().Any(transition =>
                string.IsNullOrWhiteSpace(transition.DeclaredName)
                && string.IsNullOrWhiteSpace(transition.DeclaredShortName)
                && transition.OwnedRelationship.OfType<IParameterMembership>().FirstOrDefault() is { } parameterMembership
                && parameterMembership.IsEmptyParameterMember()) == true;
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the EntryActionMember rule (StateBodyItem).
        /// <para><c>EntryActionMember : StateSubactionMembership = MemberPrefix kind = 'entry' …</c></para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership is a <see cref="IStateSubactionMembership"/> with <c>Kind == Entry</c></returns>
        internal static bool IsValidForEntryActionMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
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
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if the membership owns a succession or transition usage</returns>
        internal static bool IsValidForEntryTransitionMemberRule(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership?.OwnedRelatedElement.Any(element => element switch
            {
                // GuardedTargetSuccession REQUIRES a GuardExpressionMember. A guardless TransitionUsage is a
                // TransitionUsageMember (`transition initial then off;`); consuming it here emitted a bare
                // `then;` because neither EntryTransitionMember alternative can express it.
                ITransitionUsage transitionUsage => transitionUsage.OwnedRelationship
                    .OfType<ITransitionFeatureMembership>()
                    .Any(transitionFeature => transitionFeature.Kind == SysML2.NET.Core.Systems.States.TransitionFeatureKind.Guard),
                ISuccessionAsUsage => true,
                _ => false,
            }) == true;
        }
        
        /// <summary>
        /// Asserts that the <see cref="IOwningMembership"/> is valid for the NonFeatureMember rule.
        /// <para><c>NonFeatureMember : OwningMembership = MemberPrefix ownedRelatedElement += MemberElement</c></para>
        /// <para><c>MemberElement = AnnotatingElement | NonFeatureElement</c> — a NonFeatureMember
        /// owns an element that is NOT an <see cref="IFeature"/>.</para>
        /// </summary>
        /// <param name="owningMembership">The <see cref="IOwningMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if no <see cref="IFeature"/> is contained in the <see cref="IRelationship.OwnedRelatedElement"/></returns>
        internal static bool IsValidForNonFeatureMember(this IOwningMembership owningMembership, TextualNotationWriterContext writerContext)
        {
            return !owningMembership.OwnedRelatedElement.OfType<IFeature>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IOwningMembership"/> is valid for the FeatureMember rule.
        /// <para><c>FeatureMember : OwningMembership = TypeFeatureMember | OwnedFeatureMember</c></para>
        /// <para>Both alternatives own an <see cref="IFeature"/>.</para>
        /// </summary>
        /// <param name="owningMembership">The <see cref="IOwningMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if at least one <see cref="IFeature"/> is contained in the <see cref="IRelationship.OwnedRelatedElement"/></returns>
        internal static bool IsValidForFeatureMember(this IOwningMembership owningMembership, TextualNotationWriterContext writerContext)
        {
            return owningMembership.OwnedRelatedElement.OfType<IFeature>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the SourceSuccessionMember rule.
        /// <para><c>SourceSuccessionMember : FeatureMembership = 'then' ownedRelatedElement += SourceSuccession</c>,
        /// and <c>SourceSuccession : SuccessionAsUsage = ownedRelationship += SourceEndMember</c> — a single,
        /// ANONYMOUS end whose target is the body item that follows the <c>then</c>.</para>
        /// <para>A succession whose target end NAMES its target is a different construct: the standalone
        /// <c>SuccessionAsUsage</c> (<c>'first' ConnectorEndMember 'then' ConnectorEndMember</c>), which reaches
        /// an action body through <c>NonBehaviorBodyItem → VariantUsageMember → VariantUsageElement</c>. Both
        /// forms own an <see cref="ISuccessionAsUsage"/>, so without the <see cref="HasNamedTargetEnd"/> test
        /// this guard claimed BOTH — and the enclosing dispatch in <c>BuildActionBodyItemHandCoded</c> then
        /// dropped the standalone form silently, because its lookahead finds neither an ActionBehaviorMember
        /// nor a StructureUsageMember after it and falls through to a bare <c>cursor.Move()</c>. That lost
        /// every <c>first X then Y;</c> statement in the model.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if it owns a succession whose target end is implicit (not named)</returns>
        internal static bool IsValidForSourceSuccessionMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership.OwnedRelatedElement.OfType<ISuccessionAsUsage>().Any(succession => !HasNamedTargetEnd(succession));
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> contains at least one <see cref="IOccurrenceUsage"/>
        /// inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if it contains one <see cref="IOccurrenceUsage"/></returns>
        internal static bool IsValidForOccurrenceUsageMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership.OwnedRelatedElement.OfType<IOccurrenceUsage>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> contains at least one <see cref="IUsage"/>
        /// but no <see cref="IOccurrenceUsage"/> inside the <see cref="IRelationship.OwnedRelatedElement"/> collection
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if it contains one <see cref="IUsage"/> but no <see cref="IOccurrenceUsage"/></returns>
        internal static bool IsValidForNonOccurrenceUsageMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return !featureMembership.IsValidForOccurrenceUsageMember(writerContext) && featureMembership.OwnedRelatedElement.OfType<IUsage>().Any();
        }

        /// <summary>
        /// Asserts that the <see cref="IFeatureMembership"/> is valid for the StructureUsageMember rule.
        /// <para><c>StructureUsageMember : FeatureMembership = MemberPrefix ownedRelatedElement += StructureUsageElement</c></para>
        /// <para><c>StructureUsageElement : Usage =
        /// OccurrenceUsage | IndividualUsage | PortionUsage | EventOccurrenceUsage
        /// | ItemUsage | PartUsage | ViewUsage | RenderingUsage | PortUsage
        /// | ConnectionUsage | InterfaceUsage | AllocationUsage | Message
        /// | FlowUsage | SuccessionFlowUsage</c></para>
        /// <para>Encoded as the disjunction of the <c>StructureUsageElement</c> union expressed via
        /// the corresponding metamodel interfaces. Because the metamodel inheritance chain has
        /// <c>IFlowUsage : IActionUsage</c> (a <c>FlowUsage</c> is structurally an action), but the
        /// KEBNF places <c>FlowUsage</c> under <c>StructureUsageElement</c> and <c>ActionUsage</c>
        /// under <c>BehaviorUsageElement</c>, the simple supertype check
        /// <c>e is IOccurrenceUsage</c> is paired with two exclusion clauses:</para>
        /// <list type="bullet">
        ///   <item><description><c>!(e is IActionUsage and not IFlowUsage)</c> — every
        ///   <see cref="IActionUsage"/> that is NOT an <see cref="IFlowUsage"/> belongs to
        ///   <c>BehaviorUsageElement</c> (ActionUsage, CalculationUsage, StateUsage, CaseUsage,
        ///   AnalysisCaseUsage, VerificationCaseUsage, UseCaseUsage).</description></item>
        ///   <item><description><c>!(e is IConstraintUsage)</c> — <see cref="IConstraintUsage"/>
        ///   and its descendants (RequirementUsage, ConcernUsage) belong to
        ///   <c>BehaviorUsageElement</c>.</description></item>
        /// </list>
        /// <para>Interfaces (not concrete POCO classes) are used so the guard is robust to
        /// alternate <see cref="IOccurrenceUsage"/> implementations (extensions, test doubles)
        /// — the POCO classes in this model do not inherit from each other (only via interface
        /// chains), so an <c>OfType{ConcreteClass}</c> check would silently miss any instance
        /// supplied through a different concrete class but the same interface.</para>
        /// </summary>
        /// <param name="featureMembership">The <see cref="IFeatureMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if any <see cref="IRelationship.OwnedRelatedElement"/> matches the <c>StructureUsageElement</c> union</returns>
        internal static bool IsValidForStructureUsageMember(this IFeatureMembership featureMembership, TextualNotationWriterContext writerContext)
        {
            return featureMembership.OwnedRelatedElement.Any(element =>
                element is IOccurrenceUsage
                && !(element is IActionUsage && element is not IFlowUsage)
                && element is not IConstraintUsage);
        }

        /// <summary>
        /// Asserts that the <see cref="IOwningMembership"/> is valid for the DefinitionMember rule.
        /// <para><c>DefinitionMember : OwningMembership = MemberPrefix ownedRelatedElement += DefinitionElement</c></para>
        /// <para><c>DefinitionElement : Element = Package | LibraryPackage | AnnotatingElement | Dependency
        /// | AttributeDefinition | EnumerationDefinition | OccurrenceDefinition | IndividualDefinition
        /// | ItemDefinition | PartDefinition | ConnectionDefinition | FlowDefinition | InterfaceDefinition
        /// | PortDefinition | ActionDefinition | CalculationDefinition | StateDefinition | ConstraintDefinition
        /// | RequirementDefinition | ConcernDefinition | CaseDefinition | AnalysisCaseDefinition
        /// | VerificationCaseDefinition | UseCaseDefinition | ViewDefinition | ViewpointDefinition
        /// | RenderingDefinition | MetadataDefinition | ExtendedDefinition</c></para>
        /// <para>The four covering supertypes of the union are <see cref="IDefinition"/>, <see cref="IPackage"/>,
        /// <see cref="IAnnotatingElement"/>, <see cref="IDependency"/>. <see cref="IConjugatedPortDefinition"/>
        /// IS-A <see cref="IDefinition"/> but is NOT in the <c>DefinitionElement</c> union — it is only ever the
        /// inner element of a <c>ConjugatedPortDefinitionMember</c> consumed by the parent <c>PortDefinition</c>
        /// rule and is therefore excluded explicitly.</para>
        /// </summary>
        /// <param name="owningMembership">The <see cref="IOwningMembership"/></param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/> (unused for this guard)</param>
        /// <returns>True if at least one <see cref="IRelationship.OwnedRelatedElement"/> is a <c>DefinitionElement</c></returns>
        internal static bool IsValidForDefinitionMember(this IOwningMembership owningMembership, TextualNotationWriterContext writerContext)
        {
            foreach (var ownedRelatedElement in owningMembership.OwnedRelatedElement)
            {
                switch (ownedRelatedElement)
                {
                    case IConjugatedPortDefinition:
                        continue;
                    case IDefinition or IPackage or IAnnotatingElement or IDependency:
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Asserts that the <see cref="IRelationship"/> currently positioned by the cursor matches any
        /// alternative of the <c>DefinitionBodyItem</c> rule.
        /// <para><c>DefinitionBodyItem : Type =
        /// ownedRelationship += DefinitionMember
        /// | ownedRelationship += VariantUsageMember
        /// | ownedRelationship += NonOccurrenceUsageMember
        /// | ( ownedRelationship += SourceSuccessionMember )? ownedRelationship += OccurrenceUsageMember
        /// | ownedRelationship += AliasMember
        /// | ownedRelationship += Import</c></para>
        /// <para>Used by <c>BuildDefinitionBody</c> to bound the KEBNF <c>*</c> quantifier and the
        /// <c>';' | '{' DefinitionBodyItem* '}'</c> choice. Returns false for relationships that the body
        /// must not consume — notably the synthetic <c>ConjugatedPortDefinitionMember</c> (which carries an
        /// <see cref="IConjugatedPortDefinition"/> and is consumed by the parent <c>PortDefinition</c> rule).</para>
        /// </summary>
        /// <param name="relationship">The <see cref="IRelationship"/> at the cursor</param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the relationship matches a <c>DefinitionBodyItem</c> alternative</returns>
        internal static bool IsValidForDefinitionBodyItem(this IRelationship relationship, TextualNotationWriterContext writerContext)
        {
            return relationship switch
            {
                IImport => true,
                IVariantMembership => true,
                IFeatureMembership featureMembership =>
                    featureMembership.IsValidForSourceSuccessionMember(writerContext)
                    || featureMembership.IsValidForOccurrenceUsageMember(writerContext)
                    || featureMembership.IsValidForNonOccurrenceUsageMember(writerContext),
                IOwningMembership owningMembership => owningMembership.IsValidForDefinitionMember(writerContext),
                IMembership => true,
                _ => false,
            };
        }

        /// <summary>
        /// Asserts that the <see cref="IRelationship"/> currently positioned by the cursor matches any
        /// alternative of the <c>InterfaceBodyItem</c> rule.
        /// <para><c>InterfaceBodyItem : Type =
        /// ownedRelationship += DefinitionMember
        /// | ownedRelationship += VariantUsageMember
        /// | ownedRelationship += InterfaceNonOccurrenceUsageMember
        /// | ( ownedRelationship += SourceSuccessionMember )? ownedRelationship += InterfaceOccurrenceUsageMember
        /// | ownedRelationship += AliasMember
        /// | ownedRelationship += Import</c></para>
        /// <para>The shape is identical to <c>DefinitionBodyItem</c> except for the
        /// <c>InterfaceOccurrenceUsageMember</c> / <c>InterfaceNonOccurrenceUsageMember</c> specialisations
        /// — for the boolean-only guard, they share the same underlying
        /// <c>IsValidForOccurrenceUsageMember</c> / <c>IsValidForNonOccurrenceUsageMember</c> predicates.</para>
        /// </summary>
        /// <param name="relationship">The <see cref="IRelationship"/> at the cursor</param>
        /// <param name="writerContext">The active <see cref="TextualNotationWriterContext"/></param>
        /// <returns>True if the relationship matches an <c>InterfaceBodyItem</c> alternative</returns>
        internal static bool IsValidForInterfaceBodyItem(this IRelationship relationship, TextualNotationWriterContext writerContext)
        {
            return relationship.IsValidForDefinitionBodyItem(writerContext);
        }

        /// <summary>
        /// Asserts that the <see cref="IRelationship"/> currently positioned by the cursor is writable as an
        /// <c>ActionBodyItem</c>. Everything is admitted except a content-free anonymous
        /// <see cref="IReferenceUsage"/>: the pilot's model transform attaches one such feature to every
        /// <c>TransitionUsage</c> / action node, and it has no notation — emitting it produces a bare
        /// <c>ref;</c> that the grammar never writes.
        /// </summary>
        /// <param name="relationship">The <see cref="IRelationship"/> positioned by the cursor.</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> for the current write.</param>
        /// <returns><see langword="true"/> when the relationship has notation as an action body item.</returns>
        internal static bool IsValidForActionBodyItem(this IRelationship relationship, TextualNotationWriterContext writerContext)
            => relationship is not IFeatureMembership featureMembership || !IsContentFreeAnonymousReferenceUsage(featureMembership);

        /// <summary>
        /// Asserts that <paramref name="parameterMembership"/> is an <c>EmptyParameterMember</c> — a
        /// <see cref="IParameterMembership"/> owning an <c>EmptyUsage</c> (<c>EmptyUsage : ReferenceUsage = {}</c>):
        /// an anonymous <see cref="IReferenceUsage"/> with no binding, typing or name. The grammar uses such a
        /// member as the PLACEHOLDER for an omitted slot — e.g. the skipped <c>via</c> of <c>send … to …</c> —
        /// so it must never be written as a parameter in its own right.
        /// </summary>
        /// <param name="parameterMembership">The candidate <see cref="IParameterMembership"/>.</param>
        /// <returns><see langword="true"/> when the membership is an omitted-slot placeholder.</returns>
        internal static bool IsEmptyParameterMember(this IParameterMembership parameterMembership)
            => parameterMembership.OwnedRelatedElement.Count == 1
               && parameterMembership.OwnedRelatedElement.OfType<IReferenceUsage>().Any(referenceUsage =>
                      referenceUsage.OwnedRelationship.Count == 0
                      && string.IsNullOrWhiteSpace(referenceUsage.DeclaredName)
                      && string.IsNullOrWhiteSpace(referenceUsage.DeclaredShortName));

        /// <summary>
        /// Determines whether <paramref name="featureMembership"/> owns nothing but a completely
        /// information-free anonymous <see cref="IReferenceUsage"/> — no name, no specialization,
        /// no direction, no end flag.
        /// </summary>
        /// <param name="featureMembership">The candidate <see cref="IFeatureMembership"/>.</param>
        /// <returns><see langword="true"/> when the membership carries no writable content.</returns>
        private static bool IsContentFreeAnonymousReferenceUsage(IFeatureMembership featureMembership)
            => featureMembership.OwnedRelatedElement.Count == 1
               && featureMembership.OwnedRelatedElement.OfType<IReferenceUsage>().Any(referenceUsage =>
                      string.IsNullOrWhiteSpace(referenceUsage.DeclaredName)
                      && string.IsNullOrWhiteSpace(referenceUsage.DeclaredShortName)
                      && referenceUsage.OwnedRelationship.Count == 0
                      && !referenceUsage.Direction.HasValue
                      && !referenceUsage.IsEnd);
    }
}
