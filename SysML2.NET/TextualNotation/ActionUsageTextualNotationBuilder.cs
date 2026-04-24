// -------------------------------------------------------------------------------------------------
// <copyright file="ActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// Hand-coded part of the <see cref="ActionUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class ActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the <c>StateActionUsage</c> rule.
        /// <para><c>StateActionUsage : ActionUsage =
        /// EmptyActionUsage ';'
        /// | StatePerformActionUsage
        /// | StateAcceptActionUsage
        /// | StateSendActionUsage
        /// | StateAssignmentActionUsage</c></para>
        /// <para>Dispatches on the concrete runtime type of the action usage and delegates to the
        /// pre-existing <c>BuildState*ActionUsage</c> generated builders, each of which already
        /// composes its sub-rule's full body (declaration + action body).</para>
        /// <para>All four specific interfaces (<c>IPerformActionUsage</c>, <c>IAcceptActionUsage</c>,
        /// <c>ISendActionUsage</c>, <c>IAssignmentActionUsage</c>) are direct siblings under
        /// <c>IActionUsage</c>, so the <c>switch</c> ordering is semantically safe. A usage that is
        /// none of these falls through to <c>BuildEmptyActionUsage</c> followed by the terminating
        /// <c>';'</c>.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IActionUsage"/> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache"/> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        private static void BuildStateActionUsageHandCoded(IActionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case IPerformActionUsage performActionUsage:
                    PerformActionUsageTextualNotationBuilder.BuildStatePerformActionUsage(performActionUsage, cursorCache, stringBuilder);
                    break;

                case IAcceptActionUsage acceptActionUsage:
                    AcceptActionUsageTextualNotationBuilder.BuildStateAcceptActionUsage(acceptActionUsage, cursorCache, stringBuilder);
                    break;

                case ISendActionUsage sendActionUsage:
                    SendActionUsageTextualNotationBuilder.BuildStateSendActionUsage(sendActionUsage, cursorCache, stringBuilder);
                    break;

                case IAssignmentActionUsage assignmentActionUsage:
                    AssignmentActionUsageTextualNotationBuilder.BuildStateAssignmentActionUsage(assignmentActionUsage, cursorCache, stringBuilder);
                    break;

                default:
                    BuildEmptyActionUsage(poco, cursorCache, stringBuilder);
                    stringBuilder.AppendLine(";");
                    break;
            }
        }
    }
}
