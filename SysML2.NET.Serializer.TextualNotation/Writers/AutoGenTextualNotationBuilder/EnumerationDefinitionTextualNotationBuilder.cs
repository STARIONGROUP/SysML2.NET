// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationDefinitionTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="EnumerationDefinitionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationDefinition" /> element
    /// </summary>
    public static partial class EnumerationDefinitionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule EnumerationBody
        /// <para>EnumerationBody:EnumerationDefinition=';'|'{'(ownedRelationship+=AnnotatingMember|ownedRelationship+=EnumerationUsageMember)*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationDefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildEnumerationBody(SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationDefinition poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship).Current == null)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                while (ownedRelationshipCursor.Current != null)
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    switch (ownedRelationshipCursor.Current)
                    {
                        case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IVariantMembership variantMembership:
                            VariantMembershipTextualNotationBuilder.BuildEnumerationUsageMember(variantMembership, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            break;
                        case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership:
                            OwningMembershipTextualNotationBuilder.BuildAnnotatingMember(owningMembership, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            break;
                        default:
                            ownedRelationshipCursor.Move();
                            break;
                    }
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "EnumerationBody");
                }

                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EnumerationDefinition
        /// <para>EnumerationDefinition=DefinitionExtensionKeyword*'enum''def'DefinitionDeclarationEnumerationBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationDefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildEnumerationDefinition(SysML2.NET.Core.POCO.Systems.Enumerations.IEnumerationDefinition poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship prefixMetadataMemberGuard0 && prefixMetadataMemberGuard0.IsValidForPrefixMetadataMember(writerContext))
            {
                var positionBeforeItem0 = ownedRelationshipCursor.Position;
                DefinitionTextualNotationBuilder.BuildDefinitionExtensionKeyword(poco, writerContext, stringBuilder);
                ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "DefinitionExtensionKeyword");
            }

            stringBuilder.Append("enum ");
            stringBuilder.Append("def ");
            DefinitionTextualNotationBuilder.BuildDefinitionDeclaration(poco, writerContext, stringBuilder);
            BuildEnumerationBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
