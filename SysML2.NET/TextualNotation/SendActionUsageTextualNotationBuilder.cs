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

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// Hand-coded part of the <see cref="SendActionUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class SendActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the inner <c>(…)?</c> optional group of <c>SendNode</c>.
        /// <para><c>( ownedRelationship += NodeParameterMember SenderReceiverPart?
        /// | ownedRelationship += EmptyParameterMember SenderReceiverPart )? ActionBody</c></para>
        /// <para>The AutoGen caller emits <c>OccurrenceUsagePrefix</c>, optional <c>ActionUsageDeclaration</c>,
        /// <c>'send'</c> before this HandCoded, and <c>ActionBody</c> after. This method handles the
        /// optional group by checking whether the cursor holds a <c>IParameterMembership</c>: if its
        /// <c>OwnedRelatedElement</c> is empty → Alt 2 (EmptyParameterMember + mandatory SenderReceiverPart);
        /// otherwise → Alt 1 (NodeParameterMember + optional SenderReceiverPart).</para>
        /// </summary>
        /// <param name="poco">The <see cref="ISendActionUsage" /> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildSendNodeHandCoded(ISendActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is IParameterMembership { OwnedRelatedElement.Count: 0 } emptyMembership)
            {
                ParameterMembershipTextualNotationBuilder.BuildEmptyParameterMember(emptyMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
                BuildSenderReceiverPart(poco, cursorCache, stringBuilder);
            }
            else if (ownedRelationshipCursor.Current is IParameterMembership nodeMembership)
            {
                ParameterMembershipTextualNotationBuilder.BuildNodeParameterMember(nodeMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();

                if (ownedRelationshipCursor.Current is IParameterMembership)
                {
                    BuildSenderReceiverPart(poco, cursorCache, stringBuilder);
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the <c>SenderReceiverPart</c> rule.
        /// <para><c>SenderReceiverPart : SendActionUsage =
        /// 'via' ownedRelationship += NodeParameterMember
        /// ( 'to' ownedRelationship += NodeParameterMember )?
        /// | ownedRelationship += EmptyParameterMember
        /// 'to' ownedRelationship += NodeParameterMember</c></para>
        /// <para>Alt 1 starts with <c>'via'</c> and the cursor holds a non-empty membership. Alt 2 starts
        /// with an empty membership (no <c>'via'</c>). Both consume one or two <c>IParameterMembership</c>
        /// elements from the shared cursor.</para>
        /// </summary>
        /// <param name="poco">The <see cref="ISendActionUsage" /> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildSenderReceiverPartHandCoded(ISendActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is IParameterMembership { OwnedRelatedElement.Count: 0 } emptyMembership)
            {
                // Alt 2: EmptyParameterMember 'to' NodeParameterMember
                ParameterMembershipTextualNotationBuilder.BuildEmptyParameterMember(emptyMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();

                stringBuilder.Append("to ");

                if (ownedRelationshipCursor.Current is IParameterMembership toMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildNodeParameterMember(toMembership, cursorCache, stringBuilder);
                }

                ownedRelationshipCursor.Move();
            }
            else if (ownedRelationshipCursor.Current is IParameterMembership viaMembership)
            {
                // Alt 1: 'via' NodeParameterMember ('to' NodeParameterMember)?
                stringBuilder.Append("via ");
                ParameterMembershipTextualNotationBuilder.BuildNodeParameterMember(viaMembership, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();

                if (ownedRelationshipCursor.Current is IParameterMembership toMembership)
                {
                    stringBuilder.Append("to ");
                    ParameterMembershipTextualNotationBuilder.BuildNodeParameterMember(toMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
            }
        }
    }
}
