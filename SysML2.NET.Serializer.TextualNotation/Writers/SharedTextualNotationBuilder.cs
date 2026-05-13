// -------------------------------------------------------------------------------------------------
// <copyright file="SharedTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Metadata;

    /// <summary>
    /// Hand-coded part of the <see cref="SharedTextualNotationBuilder" />.
    /// Hosts the <c>Build{Rule}HandCoded</c> methods for shared no-target rules whose body
    /// cannot be fully generated and requires a manual implementation.
    /// </summary>
    public static partial class SharedTextualNotationBuilder
    {
        /// <summary>
        /// Gets the name of the <see cref="IElement.OwnedRelationship" /> as const
        /// </summary>
        private const string OwnedRelationshipPropertyName = "ownedRelationship";
        
        /// <summary>
        /// Builds the Textual Notation string for the rule FeaturePrefix.
        /// <para><c>FeaturePrefix = ( EndFeaturePrefix (ownedRelationship += OwnedCrossFeatureMember)?
        /// | BasicFeaturePrefix ) (ownedRelationship += PrefixMetadataMember)*</c></para>
        /// <para>The AutoGen caller processes the trailing <c>(ownedRelationship += PrefixMetadataMember)*</c>
        /// loop; this HandCoded method handles the inner alternation between <c>EndFeaturePrefix</c>
        /// (dispatching to <see cref="FeatureTextualNotationBuilder.BuildEndFeaturePrefix"/>, then
        /// optionally consuming the cross-feature member from the cursor) and <c>BasicFeaturePrefix</c>
        /// (dispatching to <see cref="FeatureTextualNotationBuilder.BuildBasicFeaturePrefix"/>).</para>
        /// </summary>
        /// <param name="poco">The <see cref="IFeature" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeaturePrefixHandCoded(IFeature poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, OwnedRelationshipPropertyName, poco.OwnedRelationship);

            if (poco.IsEnd)
            {
                FeatureTextualNotationBuilder.BuildEndFeaturePrefix(poco, writerContext, stringBuilder);

                if (ownedRelationshipCursor.Current is IOwningMembership owningMembership
                    && owningMembership.OwnedRelatedElement.OfType<IFeature>().Any()
                    && !owningMembership.OwnedRelatedElement.OfType<IMetadataUsage>().Any())
                {
                    OwningMembershipTextualNotationBuilder.BuildOwnedCrossFeatureMember(owningMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
            }
            else
            {
                FeatureTextualNotationBuilder.BuildBasicFeaturePrefix(poco, writerContext, stringBuilder);
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonBehaviorBodyItem.
        /// <para><c>NonBehaviorBodyItem =
        /// ownedRelationship += Import
        /// | ownedRelationship += AliasMember
        /// | ownedRelationship += DefinitionMember
        /// | ownedRelationship += VariantUsageMember
        /// | ownedRelationship += NonOccurrenceUsageMember
        /// | ( ownedRelationship += SourceSuccessionMember )?
        ///   ownedRelationship += StructureUsageMember</c></para>
        /// <para>Cursor-element dispatcher: inspects <c>cursor.Current</c>'s runtime type and delegates
        /// to the corresponding membership builder. The last alternative has an optional
        /// <c>SourceSuccessionMember</c> prefix before the <c>StructureUsageMember</c>.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IElement" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildNonBehaviorBodyItemHandCoded(IElement poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, OwnedRelationshipPropertyName, poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is IImport import)
            {
                ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is IFeatureMembership featureMembership)
            {
                if (featureMembership.IsValidForSourceSuccessionMember(writerContext))
                {
                    FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is IFeatureMembership structureMembership
                    && structureMembership.IsValidForStructureUsageMember(writerContext))
                {
                    FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember(structureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
                else if (ownedRelationshipCursor.Current is IVariantMembership variantMembership)
                {
                    VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
                else if (ownedRelationshipCursor.Current is IFeatureMembership nonOccurrenceMembership
                         && nonOccurrenceMembership.IsValidForNonOccurrenceUsageMember(writerContext))
                {
                    FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(nonOccurrenceMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
                else if (ownedRelationshipCursor.Current is IOwningMembership owningMembership)
                {
                    if (owningMembership.IsValidForNonFeatureMember(writerContext))
                    {
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                    }
                    else
                    {
                        MembershipTextualNotationBuilder.BuildAliasMember(owningMembership, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
            else if (ownedRelationshipCursor.Current is IOwningMembership owningMembership)
            {
                if (owningMembership.IsValidForNonFeatureMember(writerContext))
                {
                    OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                }
                else
                {
                    MembershipTextualNotationBuilder.BuildAliasMember(owningMembership, writerContext, stringBuilder);
                }

                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is IMembership membership)
            {
                // AliasMember : Membership — plain Membership that is neither OwningMembership nor FeatureMembership
                MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RealValue (the value of a
        /// <c>LiteralReal</c>), which the grammar expresses as an <see cref="IExpression" />.
        /// <para>In the unparse direction, the real numeric value is stored as a property on the
        /// <see cref="IExpression" /> POCO; this method simply emits it as a string.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IExpression" /> that holds the real value expression</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildRealValueHandCoded(IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            if (poco is ILiteralRational literalRational)
            {
                stringBuilder.Append(literalRational.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the shared two-ended connector declaration template
        /// used by <c>BindingConnectorDeclaration</c> (with <c>'of'</c>/<c>'='</c>) and
        /// <c>SuccessionDeclaration</c> (with <c>'first'</c>/<c>'then'</c>).
        /// <para><c>BindingConnectorDeclaration : BindingConnector =
        /// FeatureDeclaration ( 'of' ownedRelationship += ConnectorEndMember '=' ownedRelationship += ConnectorEndMember )?
        /// | ( isSufficient ?= 'all' )? ( 'of'? ownedRelationship += ConnectorEndMember '=' ownedRelationship += ConnectorEndMember )?</c></para>
        /// <para><c>SuccessionDeclaration : Succession =
        /// FeatureDeclaration ( 'first' ownedRelationship += ConnectorEndMember 'then' ownedRelationship += ConnectorEndMember )?
        /// | ( isSufficient ?= 'all' )? ( 'first'? ownedRelationship += ConnectorEndMember 'then' ownedRelationship += ConnectorEndMember )?</c></para>
        /// <para>Both rules share the same two-alternative shape; only the two end-keyword literals differ.
        /// Alt 1 (declaration present) emits <c>FeatureDeclaration</c> then the two
        /// <c>ConnectorEndMember</c> ends bracketed by the two keywords. Alt 2 (declaration absent)
        /// emits the optional <c>'all'</c> sufficient flag then the same two-ended payload.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IFeature"/> (a <c>BindingConnector</c> or <c>Succession</c>) from which the rule should be built</param>
        /// <param name="firstEndKeyword">Keyword literal that precedes the first <c>ConnectorEndMember</c> (e.g. <c>"of "</c> or <c>"first "</c>)</param>
        /// <param name="secondEndKeyword">Keyword literal that precedes the second <c>ConnectorEndMember</c> (e.g. <c>"= "</c> or <c>"then "</c>)</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> used to get access to the cursor cache for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        internal static void BuildTwoEndedConnectorDeclarationHandCoded(IFeature poco, string firstEndKeyword, string secondEndKeyword, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, OwnedRelationshipPropertyName, poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName)
                                 || ownedRelationshipCursor.Current is ISpecialization
                                 || ownedRelationshipCursor.Current is IConjugation;

            if (hasDeclaration)
            {
                // Alt 1: FeatureDeclaration (firstKw ConnectorEndMember secondKw ConnectorEndMember)?
                FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, writerContext, stringBuilder);

                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstEnd)
                {
                    stringBuilder.Append(firstEndKeyword);
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(firstEnd, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append(secondEndKeyword);

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(secondEnd, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
            else
            {
                // Alt 2: (isSufficient?='all')? (firstKw? ConnectorEndMember secondKw ConnectorEndMember)?
                if (poco.IsSufficient)
                {
                    stringBuilder.Append("all ");
                }

                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstEnd)
                {
                    stringBuilder.Append(firstEndKeyword);
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(firstEnd, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append(secondEndKeyword);

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(secondEnd, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the shared <c>FlowDeclaration</c> body used by both
        /// <c>Flow</c> and <c>SuccessionFlow</c>.
        /// <para><c>FlowDeclaration : Flow =
        /// FeatureDeclaration ValuePart?
        /// ( 'of' ownedRelationship += PayloadFeatureMember )?
        /// ( 'from' ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember )?
        /// | ( isSufficient ?= 'all' )? ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember</c></para>
        /// <para><c>ISuccessionFlow</c> extends <c>IFlow</c>, so a single helper typed on <c>IFlow</c> serves
        /// both rules. Auto-gen emits <c>FeaturePrefix + 'flow '</c> (or <c>'succession flow '</c>) before
        /// and <c>TypeBody</c> after this method.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IFlow"/> from which the rule should be built</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> used to get access to the cursor cache for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        internal static void BuildFlowDeclarationHandCoded(IFlow poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, OwnedRelationshipPropertyName, poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName);

            if (hasDeclaration || ownedRelationshipCursor.Current is not IEndFeatureMembership)
            {
                // Alt 1: FeatureDeclaration ValuePart? ('of' PayloadFeatureMember)? ('from' FlowEndMember 'to' FlowEndMember)?
                FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, writerContext, stringBuilder);
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);

                // 'of' PayloadFeatureMember? — IFeatureMembership but NOT IEndFeatureMembership
                if (ownedRelationshipCursor.Current is IFeatureMembership payloadMember
                    && ownedRelationshipCursor.Current is not IEndFeatureMembership)
                {
                    stringBuilder.Append("of ");
                    FeatureMembershipTextualNotationBuilder.BuildPayloadFeatureMember(payloadMember, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                // 'from' FlowEndMember 'to' FlowEndMember?
                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstFlowEnd)
                {
                    stringBuilder.Append("from ");
                    EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(firstFlowEnd, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("to ");

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondFlowEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(secondFlowEnd, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
            else
            {
                // Alt 2: (isSufficient?='all')? FlowEndMember 'to' FlowEndMember
                if (poco.IsSufficient)
                {
                    stringBuilder.Append("all ");
                }

                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstFlowEnd)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(firstFlowEnd, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("to ");

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondFlowEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(secondFlowEnd, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
        }

        /// <summary>
        /// Processes one cursor element with the trailing <c>NonBehaviorBodyItem</c> alternatives that
        /// are shared between <c>ActionBodyItem</c> and <c>StateBodyItem</c>.
        /// <para>Dispatches on <c>cursor.Current</c>'s runtime type and delegates to the corresponding
        /// membership builder: <c>Import</c>, <c>VariantUsageMember</c>, <c>StructureUsageMember</c>,
        /// <c>NonOccurrenceUsageMember</c>, <c>DefinitionMember</c> (for <see cref="IOwningMembership"/>),
        /// <c>AliasMember</c> (for plain <see cref="IMembership"/>). The cursor is always advanced — when
        /// no alternative matches, the default branch simply moves it.</para>
        /// <para>Callers invoke this helper from the <c>default:</c> branch of their behavior-specific
        /// outer switch so that all behavior-specific cases (e.g. <c>InitialNodeMember</c>,
        /// <c>ActionBehaviorMember</c>, <c>EntryActionMember</c>) are matched first.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IType"/> from which the rule should be built</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> used to get access to the cursor cache for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        internal static void BuildActionOrStateBodyItemNonBehaviorTailHandCoded(IType poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, OwnedRelationshipPropertyName, poco.OwnedRelationship);

            switch (ownedRelationshipCursor.Current)
            {
                case IImport import:
                    ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;

                case IVariantMembership variantMembership:
                    VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;

                case IFeatureMembership featureMembershipForStructure when featureMembershipForStructure.IsValidForStructureUsageMember(writerContext):
                    FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember(featureMembershipForStructure, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;

                case IFeatureMembership featureMembershipForNonOccurrence when featureMembershipForNonOccurrence.IsValidForNonOccurrenceUsageMember(writerContext):
                    FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(featureMembershipForNonOccurrence, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;

                case IOwningMembership owningMembership:
                    OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;

                case IMembership membership:
                    MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;

                default:
                    ownedRelationshipCursor.Move();
                    break;
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the shared <c>DefinitionBodyItem</c> / <c>InterfaceBodyItem</c>
        /// rule body. Both rules share an identical structural shape; only the inner
        /// <c>OccurrenceUsageMember</c> / <c>NonOccurrenceUsageMember</c> builders differ, so they are
        /// supplied as delegates by the caller.
        /// <para><c>DefinitionBodyItem : Type = ownedRelationship += DefinitionMember
        /// | ownedRelationship += VariantUsageMember
        /// | ownedRelationship += NonOccurrenceUsageMember
        /// | ( ownedRelationship += SourceSuccessionMember )? ownedRelationship += OccurrenceUsageMember
        /// | ownedRelationship += AliasMember
        /// | ownedRelationship += Import</c></para>
        /// <para><c>InterfaceBodyItem : Type = ownedRelationship += DefinitionMember
        /// | ownedRelationship += VariantUsageMember
        /// | ownedRelationship += InterfaceNonOccurrenceUsageMember
        /// | ( ownedRelationship += SourceSuccessionMember )? ownedRelationship += InterfaceOccurrenceUsageMember
        /// | ownedRelationship += AliasMember
        /// | ownedRelationship += Import</c></para>
        /// </summary>
        /// <param name="poco">The <see cref="IType"/> from which the rule should be built</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> used to get access to the cursor cache for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        /// <param name="buildOccurrenceUsageMember">Delegate that builds the <c>OccurrenceUsageMember</c> alternative — pass <c>FeatureMembershipTextualNotationBuilder.BuildOccurrenceUsageMember</c> for <c>DefinitionBodyItem</c> and <c>BuildInterfaceOccurrenceUsageMember</c> for <c>InterfaceBodyItem</c></param>
        /// <param name="buildNonOccurrenceUsageMember">Delegate that builds the <c>NonOccurrenceUsageMember</c> alternative — pass <c>FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember</c> for <c>DefinitionBodyItem</c> and <c>BuildInterfaceNonOccurrenceUsageMember</c> for <c>InterfaceBodyItem</c></param>
        internal static void BuildDefinitionOrInterfaceBodyItemHandCoded(
            IType poco,
            TextualNotationWriterContext writerContext,
            StringBuilder stringBuilder,
            Action<IFeatureMembership, TextualNotationWriterContext, StringBuilder> buildOccurrenceUsageMember,
            Action<IFeatureMembership, TextualNotationWriterContext, StringBuilder> buildNonOccurrenceUsageMember)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, OwnedRelationshipPropertyName, poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    case IVariantMembership variantMembership:
                        VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForSuccession when featureMembershipForSuccession.IsValidForSourceSuccessionMember(writerContext):
                    {
                        var nextElement = ownedRelationshipCursor.GetNext(1);

                        if (nextElement is IFeatureMembership nextFeatureMembership && nextFeatureMembership.IsValidForOccurrenceUsageMember(writerContext))
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            buildOccurrenceUsageMember((IFeatureMembership)ownedRelationshipCursor.Current, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }
                        else
                        {
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case IFeatureMembership featureMembershipForOccurrence when featureMembershipForOccurrence.IsValidForOccurrenceUsageMember(writerContext):
                        buildOccurrenceUsageMember(featureMembershipForOccurrence, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForNonOccurrence when featureMembershipForNonOccurrence.IsValidForNonOccurrenceUsageMember(writerContext):
                        buildNonOccurrenceUsageMember(featureMembershipForNonOccurrence, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    default:
                        ownedRelationshipCursor.Move();
                        break;
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the shared <c>FeatureSpecializationPart</c> body,
        /// also reused by <c>PayloadFeatureSpecializationPart</c>.
        /// <para><c>FeatureSpecializationPart : Feature = FeatureSpecialization+ MultiplicityPart? FeatureSpecialization*
        /// | MultiplicityPart FeatureSpecialization*</c></para>
        /// <para><c>PayloadFeatureSpecializationPart : Feature = ( FeatureSpecialization )+ MultiplicityPart? FeatureSpecialization*
        /// | MultiplicityPart FeatureSpecialization+</c></para>
        /// <para>The only grammar difference between the two rules (Alt 2's <c>+</c> vs <c>*</c>) is a
        /// parse-time validation concern, not a serialization difference, so a single helper serves
        /// both. Branches on whether the cursor starts on an <see cref="ISpecialization"/> or not,
        /// mirroring the grammar's two alternatives; <c>while</c>-loops on
        /// <c>cursor.Current is ISpecialization</c> drive the <c>+</c>/<c>*</c> quantifiers.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IFeature"/> from which the rule should be built</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> used to get access to the cursor cache for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        internal static void BuildFeatureSpecializationPartHandCoded(IFeature poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, OwnedRelationshipPropertyName, poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is ISpecialization)
            {
                // Alt 1: FeatureSpecialization+ MultiplicityPart? FeatureSpecialization*
                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecialization(poco, writerContext, stringBuilder);
                }

                var multiplicityElementPresent = ownedRelationshipCursor.Current is IOwningMembership owningMembership
                    && owningMembership.OwnedRelatedElement.OfType<IMultiplicity>().Any();

                if (multiplicityElementPresent || poco.IsOrdered || !poco.IsUnique)
                {
                    FeatureTextualNotationBuilder.BuildMultiplicityPart(poco, writerContext, stringBuilder);
                }

                // Trailing FeatureSpecialization* — runs regardless of whether MultiplicityPart fired,
                // per the grammar's three-segment shape `FeatureSpecialization+ MultiplicityPart? FeatureSpecialization*`.
                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecialization(poco, writerContext, stringBuilder);
                }
            }
            else
            {
                // Alt 2: MultiplicityPart FeatureSpecialization*
                FeatureTextualNotationBuilder.BuildMultiplicityPart(poco, writerContext, stringBuilder);

                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecialization(poco, writerContext, stringBuilder);
                }
            }
        }

        /// <summary>
        /// Appends a body text formatted as a REGULAR_COMMENT (<c>/* ... */</c>).
        /// <para>The grammar defines <c>REGULAR_COMMENT = '/*' COMMENT_TEXT '*/'</c>.
        /// During parsing, the <c>/*</c> and <c>*/</c> delimiters are stripped and each
        /// line's leading <c> * </c> prefix is removed. This method reverses that
        /// transformation for serialization.</para>
        /// </summary>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> to append to</param>
        /// <param name="body">The body text to format as a REGULAR_COMMENT</param>
        internal static void AppendRegularComment(StringBuilder stringBuilder, string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                stringBuilder.AppendLine("/* */");
                return;
            }

            var lines = body.Split('\n');

            if (lines.Length == 1)
            {
                stringBuilder.Append("/* ");
                stringBuilder.Append(lines[0].TrimEnd('\r'));
                stringBuilder.AppendLine(" */");
                return;
            }

            stringBuilder.AppendLine("/*");

            foreach (var rawLine in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var line = rawLine.TrimEnd('\r');
                stringBuilder.Append(" * ");
                stringBuilder.AppendLine(line);
            }

            stringBuilder.AppendLine(" */");
            stringBuilder.AppendLine();
        }

        /// <summary>
        /// Appends the shortest resolvable name of the <paramref name="target"/>
        /// <see cref="IElement"/> for the textual notation, per KerML specification section
        /// 8.2.3.5.
        /// <para>The local scope of the reference is derived as
        /// <c>sourcePoco?.owningNamespace ?? writerContext.ContextNamespace</c>. The resolver
        /// walks <em>up</em> from that local scope through each containing namespace, asking
        /// the per-scope simple-name index cached on <paramref name="writerContext"/> whether
        /// the target's <see cref="IElement.EscapedName"/> resolves locally. The first hit
        /// wins. If no scope along the chain resolves the simple name to the target, the full
        /// <see cref="IElement.qualifiedName"/> is emitted as a fallback.</para>
        /// <para>Membership references in import declarations bypass shortening — the path
        /// identifies WHAT is imported and must remain fully qualified.</para>
        /// </summary>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> to append to</param>
        /// <param name="target">The referenced <see cref="IElement"/> whose name is appended</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> providing the cache + root</param>
        /// <param name="sourcePoco">
        /// The <see cref="IElement"/> at whose syntactic position the reference appears (typically the
        /// relationship POCO whose property is being unparsed). Its <c>owningNamespace</c> is the
        /// local scope of the reference. Pass the enclosing-builder's <c>poco</c> at every call site.
        /// </param>
        internal static void AppendQualifiedName(StringBuilder stringBuilder, IElement target, TextualNotationWriterContext writerContext, IElement sourcePoco)
        {
            if (target == null)
            {
                return;
            }

            if (target is IMembership membership)
            {
                // Membership references (used in import declarations) must retain
                // their full qualified name — the path identifies WHAT is being imported.
                stringBuilder.Append(membership.MemberElement?.qualifiedName);
                return;
            }

            var simpleName = target.EscapedName();

            if (string.IsNullOrWhiteSpace(simpleName))
            {
                stringBuilder.Append(target.qualifiedName ?? string.Empty);
                return;
            }

            var scope = QueryLocalScope(sourcePoco) ?? writerContext?.ContextNamespace;

            while (scope != null)
            {
                var index = writerContext.GetOrBuildSimpleNameIndex(scope);

                if (index.TryGetValue(simpleName, out var elements) && elements.Contains(target))
                {
                    stringBuilder.Append(simpleName);
                    return;
                }

                INamespace nextScope;

                try
                {
                    nextScope = scope.owningNamespace;
                }
                catch (NotSupportedException)
                {
                    // owningNamespace not implemented for this scope — stop the upward walk
                    // and fall through to the qualifiedName fallback below.
                    break;
                }

                scope = nextScope;
            }

            stringBuilder.Append(target.qualifiedName ?? string.Empty);
        }

        /// <summary>
        /// Returns the local <see cref="INamespace"/> where a reference rooted at
        /// <paramref name="sourcePoco"/> syntactically appears, by climbing the source's
        /// containment chain until a Namespace is reached.
        /// <para>The chain is followed in this priority order at each hop:</para>
        /// <list type="number">
        ///   <item><description>The current element itself, if it is already an <see cref="INamespace"/>.</description></item>
        ///   <item><description>The <see cref="IRelationship.OwningRelatedElement"/> when the
        ///   current element is an <see cref="IRelationship"/>. Required for relationship POCOs
        ///   such as <see cref="IRedefinition"/> / <see cref="ISubsetting"/> / <see cref="IFeatureTyping"/>
        ///   whose <see cref="IElement.owningNamespace"/> is null because they are owned via
        ///   <c>ownedRelationship</c>, not via a membership.</description></item>
        ///   <item><description><see cref="IElement.owningNamespace"/> when non-null.</description></item>
        ///   <item><description><see cref="IElement.owner"/> as a final fallback.</description></item>
        /// </list>
        /// <para>A visited set guards against accidental cycles in malformed models.</para>
        /// </summary>
        /// <param name="sourcePoco">The source <see cref="IElement"/> bearing the reference, or <see langword="null"/>.</param>
        /// <returns>The local <see cref="INamespace"/>, or <see langword="null"/> when none can be derived.</returns>
        private static INamespace QueryLocalScope(IElement sourcePoco)
        {
            if (sourcePoco == null)
            {
                return null;
            }

            var visited = new HashSet<IElement>();
            var current = sourcePoco;

            while (current != null && visited.Add(current))
            {
                if (current is INamespace asNamespace)
                {
                    return asNamespace;
                }

                if (current is IRelationship relationship && relationship.OwningRelatedElement != null)
                {
                    current = relationship.OwningRelatedElement;
                    continue;
                }

                INamespace owningNs = null;

                try
                {
                    owningNs = current.owningNamespace;
                }
                catch (NotSupportedException)
                {
                    // owningNamespace not implemented — fall through to owner walk.
                }

                if (owningNs != null)
                {
                    return owningNs;
                }

                IElement nextOwner = null;

                try
                {
                    nextOwner = current.owner;
                }
                catch (NotSupportedException)
                {
                    nextOwner = null;
                }

                current = nextOwner;
            }

            return null;
        }
    }
}
