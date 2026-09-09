// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationSpecification.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides access to all <see cref="TextualNotationRule" /> defined into the textual notation specification
    /// </summary>
    public class TextualNotationSpecification
    {
        /// <summary>
        /// Gets the collection of all <see cref="TextualNotationRule" />
        /// </summary>
        public List<TextualNotationRule> Rules { get; } = [];

        /// <summary>
        /// Computes the names of rules that have no incoming reference — directly or transitively —
        /// from the rule named <paramref name="rootRuleName"/>, and are therefore unreachable when
        /// generating from that root.
        /// </summary>
        /// <param name="rootRuleName">The name of the root rule (e.g. <c>RootNamespace</c>)</param>
        /// <returns>The set of unreachable rule names</returns>
        /// <exception cref="ArgumentException">If no rule named <paramref name="rootRuleName"/> exists</exception>
        public IReadOnlySet<string> ComputeUnreachableRuleNames(string rootRuleName)
        {
            var rootRule = this.Rules.SingleOrDefault(x => x.RuleName == rootRuleName);

            if (rootRule == null)
            {
                throw new ArgumentException($"No rule named '{rootRuleName}' exists in this specification.", nameof(rootRuleName));
            }

            var reachableRuleNames = rootRule.QueryReachableRuleNames(this.Rules);

            return this.Rules.Select(x => x.RuleName).Where(name => !reachableRuleNames.Contains(name)).ToHashSet();
        }
    }
}
