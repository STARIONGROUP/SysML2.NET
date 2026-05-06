// -------------------------------------------------------------------------------------------------
// <copyright file="FlowUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.Flows;

    /// <summary>
    /// Hand-coded part of the <see cref="FlowUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class FlowUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FlowDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// FlowDeclaration : FlowUsage =
        ///     UsageDeclaration ValuePart?
        ///     ( 'of' ownedRelationship += FlowPayloadFeatureMember )?
        ///     ( 'from' ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember )?
        ///   | ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember
        ///
        /// Auto-gen emits OccurrenceUsagePrefix + 'flow ' before and DefinitionBody after.
        /// </remarks>
        private static void BuildFlowDeclarationHandCoded(IFlowUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName);

            if (hasDeclaration || ownedRelationshipCursor.Current is not IEndFeatureMembership)
            {
                // Alt 1: UsageDeclaration ValuePart? ('of' FlowPayloadFeatureMember)? ('from' FlowEndMember 'to' FlowEndMember)?
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);

                if (ownedRelationshipCursor.Current is IFeatureMembership payloadMember
                    && ownedRelationshipCursor.Current is not IEndFeatureMembership)
                {
                    stringBuilder.Append("of ");
                    FeatureMembershipTextualNotationBuilder.BuildFlowPayloadFeatureMember(payloadMember, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstFlowEnd)
                {
                    stringBuilder.Append("from ");
                    EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(firstFlowEnd, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("to ");

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondFlowEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(secondFlowEnd, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
            else
            {
                // Alt 2: FlowEndMember 'to' FlowEndMember
                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstFlowEnd)
                {
                    EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(firstFlowEnd, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("to ");

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondFlowEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildFlowEndMember(secondFlowEnd, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MessageDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Flows.IFlowUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// MessageDeclaration : FlowUsage =
        ///     UsageDeclaration ValuePart?
        ///     ( 'of' ownedRelationship += FlowPayloadFeatureMember )?
        ///     ( 'from' ownedRelationship += MessageEventMember 'to' ownedRelationship += MessageEventMember )?
        ///   | ownedRelationship += MessageEventMember 'to' ownedRelationship += MessageEventMember
        ///
        /// Auto-gen emits OccurrenceUsagePrefix + 'message ' before and DefinitionBody after.
        /// </remarks>
        private static void BuildMessageDeclarationHandCoded(IFlowUsage poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName);

            if (hasDeclaration || ownedRelationshipCursor.Current is not IParameterMembership)
            {
                // Alt 1: UsageDeclaration ValuePart? ('of' FlowPayloadFeatureMember)? ('from' MessageEventMember 'to' MessageEventMember)?
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, writerContext, stringBuilder);
                FeatureTextualNotationBuilder.BuildValuePart(poco, writerContext, stringBuilder);

                if (ownedRelationshipCursor.Current is IFeatureMembership payloadMember
                    && ownedRelationshipCursor.Current is not IParameterMembership)
                {
                    stringBuilder.Append("of ");
                    FeatureMembershipTextualNotationBuilder.BuildFlowPayloadFeatureMember(payloadMember, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is IParameterMembership firstMessageEvent)
                {
                    stringBuilder.Append("from ");
                    ParameterMembershipTextualNotationBuilder.BuildMessageEventMember(firstMessageEvent, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("to ");

                    if (ownedRelationshipCursor.Current is IParameterMembership secondMessageEvent)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildMessageEventMember(secondMessageEvent, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
            else
            {
                // Alt 2: MessageEventMember 'to' MessageEventMember
                if (ownedRelationshipCursor.Current is IParameterMembership firstMessageEvent)
                {
                    ParameterMembershipTextualNotationBuilder.BuildMessageEventMember(firstMessageEvent, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("to ");

                    if (ownedRelationshipCursor.Current is IParameterMembership secondMessageEvent)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildMessageEventMember(secondMessageEvent, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
        }
    }
}
