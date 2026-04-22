// -------------------------------------------------------------------------------------------------
// <copyright file="TriggerInvocationExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildTriggerExpressionHandCoded(ITriggerInvocationExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            switch (poco.Kind)
            {
                case TriggerKind.At:
                case TriggerKind.After:
                    stringBuilder.Append(poco.Kind.ToString().ToLower());
                    stringBuilder.Append(' ');

                    if (ownedRelationshipCursor.Current is IParameterMembership argumentMember)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildArgumentMember(argumentMember, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                    }

                    break;
                case TriggerKind.When:
                    stringBuilder.Append("when ");

                    if (ownedRelationshipCursor.Current is IParameterMembership argumentExpressionMember)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildArgumentExpressionMember(argumentExpressionMember, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                    }

                    break;
            }
        }
    }
}
