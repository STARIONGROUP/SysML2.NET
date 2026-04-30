// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionFlowTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Interactions;

    /// <summary>
    /// Hand-coded part of the <see cref="SuccessionFlowTextualNotationBuilder" />
    /// </summary>
    public static partial class SuccessionFlowTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FlowDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Interactions.ISuccessionFlow" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// FlowDeclaration : Flow =
        ///     FeatureDeclaration ValuePart?
        ///     ( 'of' ownedRelationship += PayloadFeatureMember )?
        ///     ( 'from' ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember )?
        ///   | ( isSufficient ?= 'all' )?
        ///     ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember
        ///
        /// Auto-gen emits FeaturePrefix + 'succession flow ' before and TypeBody after.
        /// Same grammar as Flow's FlowDeclaration; ISuccessionFlow extends IFlow.
        /// </remarks>
        private static void BuildFlowDeclarationHandCoded(ISuccessionFlow poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName);

            if (hasDeclaration || ownedRelationshipCursor.Current is not IEndFeatureMembership)
            {
                // Alt 1: FeatureDeclaration ValuePart? ('of' PayloadFeatureMember)? ('from' FlowEndMember 'to' FlowEndMember)?
                FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, cursorCache, stringBuilder);
                FeatureTextualNotationBuilder.BuildValuePart(poco, cursorCache, stringBuilder);

                // 'of' PayloadFeatureMember? — IFeatureMembership but NOT IEndFeatureMembership
                if (ownedRelationshipCursor.Current is IFeatureMembership payloadMember
                    && ownedRelationshipCursor.Current is not IEndFeatureMembership)
                {
                    stringBuilder.Append("of ");
                    FeatureMembershipTextualNotationBuilder.BuildPayloadFeatureMember(payloadMember, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                // 'from' FlowEndMember 'to' FlowEndMember?
                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstFlowEnd)
                {
                    stringBuilder.Append("from ");
                    EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(firstFlowEnd, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("to ");

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondFlowEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(secondFlowEnd, cursorCache, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
            else
            {
                // Alt 2: (isSufficient?='all')? FlowEndMember 'to' FlowEndMember
                if (poco.IsSufficient)
                {
                    stringBuilder.Append("all ");
                }

                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstFlowEnd)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(firstFlowEnd, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("to ");

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondFlowEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(secondFlowEnd, cursorCache, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
        }
    }
}
