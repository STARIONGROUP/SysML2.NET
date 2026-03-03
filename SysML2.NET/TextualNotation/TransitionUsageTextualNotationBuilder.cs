// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Hand-coded part of the <see cref="TransitionUsageTextualNotationBuilder"/>
    /// </summary>
    public static partial class TransitionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the conditional part for the GuardedSuccession rule
        /// </summary>
        /// <param name="poco">The <see cref="ITransitionUsage"/></param>
        /// <returns>The assertion of the condition</returns>
        private static bool BuildGroupConditionForGuardedSuccession(ITransitionUsage poco)
        {
            return false;
        }

        /// <summary>
        /// Builds the conditional part for the TransitionUsage rule
        /// </summary>
        /// <param name="poco">The <see cref="ITransitionUsage"/></param>
        /// <returns>The assertion of the condition</returns>
        private static bool BuildGroupConditionForTransitionUsage(ITransitionUsage poco)
        {
            return false;
        }
    }
}
