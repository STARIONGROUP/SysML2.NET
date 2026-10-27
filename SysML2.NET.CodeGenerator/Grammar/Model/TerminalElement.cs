// ------------------------------------------------------------------------------------------------
// <copyright file="TerminalElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Describes a terminal Element
    /// </summary>
    public class TerminalElement: RuleElement
    {
        /// <summary>
        /// Gets or sets the terminal element value
        /// </summary>
        public string Value { get; set; }
    }
}
