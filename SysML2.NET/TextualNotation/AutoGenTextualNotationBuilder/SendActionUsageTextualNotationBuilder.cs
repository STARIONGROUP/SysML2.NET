// -------------------------------------------------------------------------------------------------
// <copyright file="SendActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="SendActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> element
    /// </summary>
    public static partial class SendActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SendNode
        /// <para>SendNode:SendActionUsage=OccurrenceUsagePrefixActionUsageDeclaration?'send'(ownedRelationship+=NodeParameterMemberSenderReceiverPart?|ownedRelationship+=EmptyParameterMemberSenderReceiverPart)?ActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSendNode(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, cursorCache, stringBuilder);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsVariation || poco.IsConstant || poco.IsEnd || poco.isReference || poco.IsIndividual || poco.PortionKind.HasValue || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                ActionUsageTextualNotationBuilder.BuildActionUsageDeclaration(poco, cursorCache, stringBuilder);
            }
            stringBuilder.Append("send ");
            BuildSendNodeHandCoded(poco, cursorCache, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SendNodeDeclaration
        /// <para>SendNodeDeclaration:SendActionUsage=ActionNodeUsageDeclaration?'send'ownedRelationship+=NodeParameterMemberSenderReceiverPart?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSendNodeDeclaration(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                ActionUsageTextualNotationBuilder.BuildActionNodeUsageDeclaration(poco, cursorCache, stringBuilder);
            }
            stringBuilder.Append("send ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildNodeParameterMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsVariation || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.isReference || poco.IsIndividual || poco.PortionKind.HasValue || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                BuildSenderReceiverPart(poco, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SenderReceiverPart
        /// <para>SenderReceiverPart:SendActionUsage='via'ownedRelationship+=NodeParameterMember('to'ownedRelationship+=NodeParameterMember)?|ownedRelationship+=EmptyParameterMember'to'ownedRelationship+=NodeParameterMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSenderReceiverPart(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildSenderReceiverPartHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateSendActionUsage
        /// <para>StateSendActionUsage:SendActionUsage=SendNodeDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStateSendActionUsage(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildSendNodeDeclaration(poco, cursorCache, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionSendActionUsage
        /// <para>TransitionSendActionUsage:SendActionUsage=SendNodeDeclaration('{'ActionBodyItem*'}')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionSendActionUsage(SysML2.NET.Core.POCO.Systems.Actions.ISendActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildSendNodeDeclaration(poco, cursorCache, stringBuilder);

            if (poco.OwnedRelationship.Count != 0 || poco.importedMembership.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsVariation || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.isReference || poco.IsIndividual || poco.PortionKind.HasValue || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    TypeTextualNotationBuilder.BuildActionBodyItem(poco, cursorCache, stringBuilder);
                }

                stringBuilder.AppendLine("}");
            }


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
