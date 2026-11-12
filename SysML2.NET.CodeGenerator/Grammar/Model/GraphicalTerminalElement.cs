// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalTerminalElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Describes a literal string terminal of a graphical production
    /// </summary>
    public class GraphicalTerminalElement : GraphicalRuleElement
    {
        /// <summary>
        /// Gets or sets the literal text, without its enclosing quotes
        /// </summary>
        public string Value { get; set; }
    }
}
