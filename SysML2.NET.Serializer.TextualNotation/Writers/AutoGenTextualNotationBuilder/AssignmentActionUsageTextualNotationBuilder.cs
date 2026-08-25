// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="AssignmentActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage" /> element
    /// </summary>
    public static partial class AssignmentActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentNode
        /// <para>AssignmentNode:AssignmentActionUsage=OccurrenceUsagePrefixAssignmentNodeDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildAssignmentNode(SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, writerContext, stringBuilder);
            ActionUsageTextualNotationBuilder.BuildAssignmentNodeDeclaration(poco, writerContext, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateAssignmentActionUsage
        /// <para>StateAssignmentActionUsage:AssignmentActionUsage=AssignmentNodeDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildStateAssignmentActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            ActionUsageTextualNotationBuilder.BuildAssignmentNodeDeclaration(poco, writerContext, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, writerContext, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionAssignmentActionUsage
        /// <para>TransitionAssignmentActionUsage:AssignmentActionUsage=AssignmentNodeDeclaration('{'ActionBodyItem*'}')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that accumulates the entire textual notation with indentation</param>
        public static void BuildTransitionAssignmentActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IAssignmentActionUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            ActionUsageTextualNotationBuilder.BuildAssignmentNodeDeclaration(poco, writerContext, stringBuilder);
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship optionalBodyCandidate && optionalBodyCandidate.IsValidForActionBodyItem(writerContext))
            {
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");
                stringBuilder.IncreaseIndent();
                while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Elements.IRelationship ownedRelationshipBodyItem && ownedRelationshipBodyItem.IsValidForActionBodyItem(writerContext))
                {
                    var positionBeforeItem0 = ownedRelationshipCursor.Position;
                    TypeTextualNotationBuilder.BuildActionBodyItem(poco, writerContext, stringBuilder);
                    ownedRelationshipCursor.AssertAdvancedSince(positionBeforeItem0, "ActionBodyItem");
                }

                stringBuilder.DecreaseIndent();
                stringBuilder.AppendLine("}");
            }


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
