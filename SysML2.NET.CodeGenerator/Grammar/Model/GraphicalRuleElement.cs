// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalRuleElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Base class for an element of a graphical production
    /// </summary>
    public abstract class GraphicalRuleElement
    {
        /// <summary>
        /// Gets or sets the optional EBNF suffix (<c>?</c>, <c>*</c> or <c>+</c>)
        /// </summary>
        public string Suffix { get; set; }
    }
}
