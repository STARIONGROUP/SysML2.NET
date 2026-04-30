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

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeaturePrefixHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsEnd)
            {
                FeatureTextualNotationBuilder.BuildEndFeaturePrefix(poco, cursorCache, stringBuilder);

                if (ownedRelationshipCursor.Current is IOwningMembership owningMembership
                    && owningMembership.OwnedRelatedElement.OfType<IFeature>().Any()
                    && !owningMembership.OwnedRelatedElement.OfType<IMetadataUsage>().Any())
                {
                    OwningMembershipTextualNotationBuilder.BuildOwnedCrossFeatureMember(owningMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
            }
            else
            {
                FeatureTextualNotationBuilder.BuildBasicFeaturePrefix(poco, cursorCache, stringBuilder);
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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildNonBehaviorBodyItemHandCoded(IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is IImport import)
            {
                ImportTextualNotationBuilder.BuildImport(import, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is IFeatureMembership featureMembership)
            {
                if (featureMembership.IsValidForSourceSuccessionMember())
                {
                    FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is IFeatureMembership structureMembership
                    && structureMembership.IsValidForStructureUsageMember())
                {
                    FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember(structureMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
                else if (ownedRelationshipCursor.Current is IVariantMembership variantMembership)
                {
                    VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
                else if (ownedRelationshipCursor.Current is IFeatureMembership nonOccurrenceMembership
                         && nonOccurrenceMembership.IsValidForNonOccurrenceUsageMember())
                {
                    FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(nonOccurrenceMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
                else if (ownedRelationshipCursor.Current is IOwningMembership owningMembership)
                {
                    if (owningMembership.IsValidForNonFeatureMember())
                    {
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, cursorCache, stringBuilder);
                    }
                    else
                    {
                        MembershipTextualNotationBuilder.BuildAliasMember(owningMembership, cursorCache, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
            else if (ownedRelationshipCursor.Current is IOwningMembership owningMembership)
            {
                if (owningMembership.IsValidForNonFeatureMember())
                {
                    OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, cursorCache, stringBuilder);
                }
                else
                {
                    MembershipTextualNotationBuilder.BuildAliasMember(owningMembership, cursorCache, stringBuilder);
                }

                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is IMembership membership)
            {
                // AliasMember : Membership — plain Membership that is neither OwningMembership nor FeatureMembership
                MembershipTextualNotationBuilder.BuildAliasMember(membership, cursorCache, stringBuilder);
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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildRealValueHandCoded(IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco is ILiteralRational literalRational)
            {
                stringBuilder.Append(literalRational.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
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
    }
}
