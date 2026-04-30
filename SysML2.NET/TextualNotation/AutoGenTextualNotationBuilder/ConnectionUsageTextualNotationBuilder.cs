// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ConnectionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage" /> element
    /// </summary>
    public static partial class ConnectionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ConnectorPart
        /// <para>ConnectorPart:ConnectionUsage=BinaryConnectorPart|NaryConnectorPart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnectorPart(SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage pocoConnectionUsageBinaryConnectorPart when pocoConnectionUsageBinaryConnectorPart.IsValidForBinaryConnectorPart():
                    BuildBinaryConnectorPart(pocoConnectionUsageBinaryConnectorPart, cursorCache, stringBuilder);
                    break;
                default:
                    BuildNaryConnectorPart(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BinaryConnectorPart
        /// <para>BinaryConnectorPart:ConnectionUsage=ownedRelationship+=ConnectorEndMember'to'ownedRelationship+=ConnectorEndMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBinaryConnectorPart(SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("to ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NaryConnectorPart
        /// <para>NaryConnectorPart:ConnectionUsage='('ownedRelationship+=ConnectorEndMember','ownedRelationship+=ConnectorEndMember(','ownedRelationship+=ConnectorEndMember)*')'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNaryConnectorPart(SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("(");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(", ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership endFeatureMembershipGuard && endFeatureMembershipGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage>().Any())
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }
            stringBuilder.Append(") ");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConnectionUsage
        /// <para>ConnectionUsage=OccurrenceUsagePrefix('connection'UsageDeclarationValuePart?('connect'ConnectorPart)?|'connect'ConnectorPart)UsageBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnectionUsage(SysML2.NET.Core.POCO.Systems.Connections.IConnectionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, cursorCache, stringBuilder);
            BuildConnectionUsageHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');
            UsageTextualNotationBuilder.BuildUsageBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
