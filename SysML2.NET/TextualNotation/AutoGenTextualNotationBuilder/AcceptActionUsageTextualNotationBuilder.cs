// -------------------------------------------------------------------------------------------------
// <copyright file="AcceptActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="AcceptActionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> element
    /// </summary>
    public static partial class AcceptActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule AcceptNode
        /// <para>AcceptNode:AcceptActionUsage=OccurrenceUsagePrefixAcceptNodeDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAcceptNode(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, cursorCache, stringBuilder);
            BuildAcceptNodeDeclaration(poco, cursorCache, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AcceptNodeDeclaration
        /// <para>AcceptNodeDeclaration:AcceptActionUsage=ActionNodeUsageDeclaration?'accept'AcceptParameterPart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAcceptNodeDeclaration(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                ActionUsageTextualNotationBuilder.BuildActionNodeUsageDeclaration(poco, cursorCache, stringBuilder);
            }
            stringBuilder.Append("accept ");
            BuildAcceptParameterPart(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AcceptParameterPart
        /// <para>AcceptParameterPart:AcceptActionUsage=ownedRelationship+=PayloadParameterMember('via'ownedRelationship+=NodeParameterMember)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAcceptParameterPart(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildPayloadParameterMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (ownedRelationshipCursor.Current != null)
            {
                stringBuilder.Append("via ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildNodeParameterMember(elementAsParameterMembership, cursorCache, stringBuilder);
                    }
                }
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateAcceptActionUsage
        /// <para>StateAcceptActionUsage:AcceptActionUsage=AcceptNodeDeclarationActionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStateAcceptActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildAcceptNodeDeclaration(poco, cursorCache, stringBuilder);
            TypeTextualNotationBuilder.BuildActionBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TriggerAction
        /// <para>TriggerAction:AcceptActionUsage=AcceptParameterPart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTriggerAction(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildAcceptParameterPart(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionAcceptActionUsage
        /// <para>TransitionAcceptActionUsage:AcceptActionUsage=AcceptNodeDeclaration('{'ActionBodyItem*'}')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionAcceptActionUsage(SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildAcceptNodeDeclaration(poco, cursorCache, stringBuilder);

            if (poco.OwnedRelationship.Count != 0 || poco.importedMembership.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsVariation || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.isReference || poco.IsIndividual || poco.PortionKind.HasValue || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    TypeTextualNotationBuilder.BuildActionBodyItem(poco, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                stringBuilder.AppendLine("}");
            }


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
