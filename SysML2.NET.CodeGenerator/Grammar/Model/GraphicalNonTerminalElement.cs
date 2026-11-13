// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalNonTerminalElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Describes a reference from a graphical production to another production
    /// </summary>
    public class GraphicalNonTerminalElement : GraphicalRuleElement
    {
        /// <summary>
        /// Gets or sets the referenced production name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the name contains an uppercase letter, and so references a textual
        /// notation rule rather than a graphical production (SysML 2.0 §8.2.3.1)
        /// </summary>
        public bool IsTextualReference { get; set; }
    }
}
