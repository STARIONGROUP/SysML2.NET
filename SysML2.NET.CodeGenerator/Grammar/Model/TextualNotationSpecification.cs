// ------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationSpecification.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
