// ------------------------------------------------------------------------------------------------
// <copyright file="Alternatives.cs" company="Starion Group S.A.">
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
    /// Provides mapping data class for the alternative grammar part
    /// </summary>
    public class Alternatives: IPartOfTextualRule
    {
        /// <summary>
        /// Gets a collection of <see cref="RuleElement" /> that is part of the <see cref="Alternatives" />
        /// </summary>
        public List<RuleElement> Elements { get; } = [];

        /// <summary>
        /// Gets the <see cref="IPartOfTextualRule.TextualNotationRule" />
        /// </summary>
        public TextualNotationRule TextualNotationRule { get; init; }

        /// <summary>
        /// Asserts that the current <see cref="Alternatives"/> contains <see cref="AssignmentElement"/> that are part of a collection iteration that could be called from
        /// another rule
        /// </summary>
        /// <returns>The computation of the assertion</returns>
        internal bool ContainsAssignmentRequiringDispatch()
        {
            var assignments = this.Elements.OfType<AssignmentElement>().Where(x => x.Operator == "+=").ToList();
            var groupElements = this.Elements.OfType<GroupElement>().Where(x => x.IsCollection).ToList();

            if (assignments.Count == 0)
            {
                return false;
            }

            return groupElements.Count == 0;
        }
    }
}
