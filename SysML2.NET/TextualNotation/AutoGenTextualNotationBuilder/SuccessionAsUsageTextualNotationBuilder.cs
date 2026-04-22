// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionAsUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="SuccessionAsUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage" /> element
    /// </summary>
    public static partial class SuccessionAsUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SourceSuccession
        /// <para>SourceSuccession:SuccessionAsUsage=ownedRelationship+=SourceEndMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSourceSuccession(SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildSourceEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TargetSuccession
        /// <para>TargetSuccession:SuccessionAsUsage=ownedRelationship+=SourceEndMember'then'ownedRelationship+=ConnectorEndMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTargetSuccession(SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildSourceEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("then ");

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
        /// Builds the Textual Notation string for the rule SuccessionAsUsage
        /// <para>SuccessionAsUsage=UsagePrefix('succession'UsageDeclaration)?'first's.ownedRelationship+=ConnectorEndMember'then's.ownedRelationship+=ConnectorEndMemberUsageBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSuccessionAsUsage(SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            UsageTextualNotationBuilder.BuildUsagePrefix(poco, cursorCache, stringBuilder);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.OwnedRelatedElement.Count != 0 || poco.IsOrdered)
            {
                stringBuilder.Append("succession ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("first ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("then ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership elementAsEndFeatureMembership)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(elementAsEndFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            UsageTextualNotationBuilder.BuildUsageBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
