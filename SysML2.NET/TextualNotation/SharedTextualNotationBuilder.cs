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
    using System;
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
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeaturePrefixHandCoded(IFeature poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

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
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is IImport import)
            {
                ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is IFeatureMembership featureMembership)
            {
                if (featureMembership.IsValidForSourceSuccessionMember())
                {
                    FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is IFeatureMembership structureMembership
                    && structureMembership.IsValidForStructureUsageMember())
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
                         && nonOccurrenceMembership.IsValidForNonOccurrenceUsageMember())
                {
                    FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(nonOccurrenceMembership, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
                else if (ownedRelationshipCursor.Current is IOwningMembership owningMembership)
                {
                    if (owningMembership.IsValidForNonFeatureMember())
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
                if (owningMembership.IsValidForNonFeatureMember())
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
        /// Appends the shortest resolvable name of the <see cref="IElement"/> for the textual notation,
        /// per KerML specification section 8.2.3.5.
        /// <para>When the <paramref name="writerContext"/> carries a non-null
        /// <see cref="TextualNotationWriterContext.ContextNamespace"/>, the method walks up the
        /// namespace chain to find whether the element's simple name resolves via imports
        /// (per 8.2.3.5.4 Full Resolution). If so, the simple name is used; otherwise the full
        /// <c>qualifiedName</c> is emitted.</para>
        /// </summary>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> to append to</param>
        /// <param name="poco">The <see cref="IElement"/> that needs to have its name appended</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> providing the serialization context</param>
        internal static void AppendQualifiedName(StringBuilder stringBuilder, IElement poco, TextualNotationWriterContext writerContext)
        {
            if (poco is IMembership membership)
            {
                // Membership references (used in import declarations) must retain
                // their full qualified name — the path identifies WHAT is being imported.
                stringBuilder.Append(membership.MemberElement.qualifiedName);
            }
            else
            {
                // Element references (used in type/feature references) can be shortened
                // when the element is resolvable by its simple name via imports (8.2.3.5).
                stringBuilder.Append(QueryShortestResolvableName(poco, writerContext?.ContextNamespace));
            }
        }

        /// <summary>
        /// Determines the shortest name that will resolve to the given <paramref name="element"/>
        /// per the KerML name resolution rules (8.2.3.5).
        /// <para>Per 8.2.3.5.4, full resolution walks up from the local namespace through
        /// containing namespaces to the global namespace. If the element's simple name resolves
        /// via the <c>membership</c> (which includes imported memberships) of any namespace in
        /// that chain, the simple name is sufficient.</para>
        /// </summary>
        /// <param name="element">The referenced <see cref="IElement"/> to resolve</param>
        /// <param name="contextNamespace">
        /// The root <see cref="INamespace"/> being serialized, or <c>null</c> to fall back to the full qualified name
        /// </param>
        /// <returns>The shortest name that resolves to the element</returns>
        private static string QueryShortestResolvableName(IElement element, INamespace contextNamespace)
        {
            if (element == null)
            {
                return string.Empty;
            }

            var simpleName = element.EscapedName();

            if (!string.IsNullOrWhiteSpace(simpleName) && contextNamespace != null)
            {
                // Per 8.2.3.5.4, full resolution walks up from the local namespace through
                // containing namespaces. The contextNamespace is the root namespace being
                // serialized; imports that make the simple name resolvable may be on any
                // descendant namespace (e.g. a LibraryPackage nested inside the root).
                // We check the context namespace and all its descendant namespaces.
                if (QueryIsResolvableInNamespaceTree(contextNamespace, element, simpleName))
                {
                    return simpleName;
                }
            }

            return element.qualifiedName ?? string.Empty;
        }

        /// <summary>
        /// Recursively checks whether the given <paramref name="element"/> is resolvable by its
        /// <paramref name="simpleName"/> within the <paramref name="namespace"/> or any of its
        /// descendant namespaces (per 8.2.3.5.4 full resolution, which walks up from the local
        /// namespace — checking descendants ensures we cover all import scopes in the output model).
        /// </summary>
        /// <param name="namespace">The namespace to check (including descendants)</param>
        /// <param name="element">The element to find</param>
        /// <param name="simpleName">The simple name to match</param>
        /// <returns><c>true</c> if the element is found by simple name in the namespace tree</returns>
        private static bool QueryIsResolvableInNamespaceTree(INamespace @namespace, IElement element, string simpleName)
        {
            try
            {
                if (QueryIsResolvableBySimpleName(@namespace, element, simpleName))
                {
                    return true;
                }
            }
            catch (NotSupportedException)
            {
                // membership may not be fully implemented for this namespace
            }

            try
            {
                foreach (var childNamespace in @namespace.ownedMember.OfType<INamespace>())
                {
                    if (QueryIsResolvableInNamespaceTree(childNamespace, element, simpleName))
                    {
                        return true;
                    }
                }
            }
            catch (NotSupportedException)
            {
                // ownedMember may not be fully implemented for this namespace
            }

            return false;
        }

        /// <summary>
        /// Checks whether the given <paramref name="element"/> is resolvable by its
        /// <paramref name="simpleName"/> within the <paramref name="namespace"/>'s
        /// memberships (owned, imported, and inherited).
        /// </summary>
        /// <param name="namespace">The namespace to check</param>
        /// <param name="element">The element to find</param>
        /// <param name="simpleName">The simple name to match</param>
        /// <returns><c>true</c> if the element is found by simple name in the namespace</returns>
        private static bool QueryIsResolvableBySimpleName(INamespace @namespace, IElement element, string simpleName)
        {
            // Check ownedMembership first (non-derived, always available)
            foreach (var ownedMember in @namespace.ownedMembership)
            {
                if (ownedMember is IOwningMembership owningMembership
                    && owningMembership.OwnedRelatedElement.Contains(element))
                {
                    return true;
                }

                if (ownedMember.MemberElement == element)
                {
                    return true;
                }
            }

            // Check imports: walk ownedImport (ownedRelationship filtered to IImport) to find
            // MembershipImports that reference the element directly by its membership.
            foreach (var import in @namespace.ownedImport)
            {
                if (import is IMembershipImport membershipImport
                    && membershipImport.ImportedMembership is IMembership importedMembership
                    && importedMembership.MemberElement == element)
                {
                    return true;
                }

                if (import is INamespaceImport namespaceImport)
                {
                    var importedNs = namespaceImport.ImportedNamespace;

                    if (importedNs != null)
                    {
                        foreach (var visibleMember in importedNs.ownedMembership)
                        {
                            if (visibleMember.MemberElement == element)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
