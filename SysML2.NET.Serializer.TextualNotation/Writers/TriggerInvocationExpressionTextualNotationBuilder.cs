// ------------------------------------------------------------------------------------------------
// <copyright file="TriggerInvocationExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{

    using SysML2.NET.Core.Systems.Actions;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// Hand-coded part of the <see cref="TriggerInvocationExpressionTextualNotationBuilder"/>
    /// </summary>
    public static partial class TriggerInvocationExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule TriggerExpression
        /// <remarks>TriggerExpression:TriggerInvocationExpression=kind=('at'|'after')ownedRelationship+=ArgumentMember|kind='when'ownedRelationship+=ArgumentExpressionMember</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="ITriggerInvocationExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        private static void BuildTriggerExpressionHandCoded(ITriggerInvocationExpression poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            switch (poco.Kind)
            {
                case TriggerKind.At:
                case TriggerKind.After:
                    stringBuilder.Append(poco.Kind.ToString().ToLower());
                    stringBuilder.Append(' ');

                    if (ownedRelationshipCursor.Current is IParameterMembership argumentMember)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildArgumentMember(argumentMember, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                    }

                    break;
                case TriggerKind.When:
                    stringBuilder.Append("when ");

                    if (ownedRelationshipCursor.Current is IParameterMembership argumentExpressionMember)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildArgumentExpressionMember(argumentExpressionMember, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                    }

                    break;
            }
        }
    }
}
