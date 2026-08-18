// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageNavigation.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// The target-parameter navigations shared by the AssignmentActionUsage redefinition constraints.
    /// </summary>
    /// <remarks>
    /// Three constraints walk the same chain from an assignment action's first input parameter —
    /// <c>inputParameter(1).ownedFeature-&gt;first()</c> for the starting-at Feature and one hop further for
    /// the accessed Feature. Each hop is guarded by a <c>notEmpty()</c> in the OCL, so a partially-built
    /// action yields null rather than throwing.
    /// </remarks>
    internal static class AssignmentActionUsageNavigation
    {
        /// <summary>
        /// Returns the starting-at Feature: the first owned Feature of the target parameter.
        /// </summary>
        /// <param name="assignmentActionUsage">The assignment action to navigate from.</param>
        /// <returns>The Feature, or <c>null</c> when the chain is incomplete.</returns>
        internal static IFeature QueryStartingAt(IAssignmentActionUsage assignmentActionUsage)
        {
            // inputParameter is a 1-based metamodel operation, so the OCL argument passes through unchanged.
            var targetParameter = assignmentActionUsage.InputParameter(1);

            return targetParameter?.ownedFeature.FirstOrDefault();
        }

        /// <summary>
        /// Returns the accessed Feature: the first owned Feature of the starting-at Feature.
        /// </summary>
        /// <param name="assignmentActionUsage">The assignment action to navigate from.</param>
        /// <returns>The Feature, or <c>null</c> when the chain is incomplete.</returns>
        internal static IFeature QueryAccessedFeature(IAssignmentActionUsage assignmentActionUsage)
        {
            return QueryStartingAt(assignmentActionUsage)?.ownedFeature.FirstOrDefault();
        }
    }
}
