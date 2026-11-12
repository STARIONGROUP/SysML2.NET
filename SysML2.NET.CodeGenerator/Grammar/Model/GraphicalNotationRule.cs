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
    }
}
