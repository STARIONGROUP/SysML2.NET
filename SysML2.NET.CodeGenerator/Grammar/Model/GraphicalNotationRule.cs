// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalNotationRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Describes one production definition of the graphical notation
    /// </summary>
    public class GraphicalNotationRule
    {
        /// <summary>
        /// Gets or sets the production name
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the definition is a partial production (<c>=|</c>) whose alternatives
        /// add to other definitions of the same name (SysML 2.0 §8.2.3.1)
        /// </summary>
        public bool IsPartial { get; set; }

        /// <summary>
        /// Gets the <see cref="GraphicalAlternatives" /> defined by the production
        /// </summary>
        public List<GraphicalAlternatives> Alternatives { get; } = [];

        /// <summary>
        /// Queries every element of the production, including those nested in groups
        /// </summary>
        /// <returns>The <see cref="GraphicalRuleElement" />s, each before the elements it contains</returns>
        public IEnumerable<GraphicalRuleElement> QueryAllElements() => QueryAllElements(this.Alternatives);

        /// <summary>
        /// Queries every element of the given alternatives, recursively
        /// </summary>
        /// <param name="alternatives">The <see cref="GraphicalAlternatives" /> to walk</param>
        /// <returns>The <see cref="GraphicalRuleElement" />s, each before the elements it contains</returns>
        private static IEnumerable<GraphicalRuleElement> QueryAllElements(IEnumerable<GraphicalAlternatives> alternatives)
        {
            foreach (var element in alternatives.SelectMany(alternative => alternative.Elements))
            {
                yield return element;

                if (element is not GraphicalGroupElement group)
                {
                    continue;
                }

                foreach (var nestedElement in QueryAllElements(group.Alternatives))
                {
                    yield return nestedElement;
                }
            }
        }
    }
}
