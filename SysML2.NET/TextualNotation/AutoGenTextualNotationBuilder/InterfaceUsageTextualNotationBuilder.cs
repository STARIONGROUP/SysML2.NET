// -------------------------------------------------------------------------------------------------
// <copyright file="InterfaceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="InterfaceUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> element
    /// </summary>
    public static partial class InterfaceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceUsageDeclaration
        /// <para>InterfaceUsageDeclaration:InterfaceUsage=UsageDeclarationValuePart?('connect'InterfacePart)?|InterfacePart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceUsageDeclaration(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildInterfaceUsageDeclarationHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfacePart
        /// <para>InterfacePart:InterfaceUsage=BinaryInterfacePart|NaryInterfacePart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfacePart(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage pocoInterfaceUsageBinaryInterfacePart when pocoInterfaceUsageBinaryInterfacePart.IsValidForBinaryInterfacePart():
                    BuildBinaryInterfacePart(pocoInterfaceUsageBinaryInterfacePart, cursorCache, stringBuilder);
                    break;
                default:
                    BuildNaryInterfacePart(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BinaryInterfacePart
        /// <para>BinaryInterfacePart:InterfaceUsage=ownedRelationship+=InterfaceEndMember'to'ownedRelationship+=InterfaceEndMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBinaryInterfacePart(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildInterfaceEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("to ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildInterfaceEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NaryInterfacePart
        /// <para>NaryInterfacePart:InterfaceUsage='('ownedRelationship+=InterfaceEndMember','ownedRelationship+=InterfaceEndMember(','ownedRelationship+=InterfaceEndMember)*')'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNaryInterfacePart(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("(");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildInterfaceEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(", ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildInterfaceEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership endFeatureMembershipGuard && endFeatureMembershipGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.Ports.IPortUsage>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildInterfaceEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }
            stringBuilder.Append(")");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceUsage
        /// <para>InterfaceUsage=OccurrenceUsagePrefix'interface'InterfaceUsageDeclarationInterfaceBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceUsage(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("interface ");
            BuildInterfaceUsageDeclaration(poco, cursorCache, stringBuilder);
            TypeTextualNotationBuilder.BuildInterfaceBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
