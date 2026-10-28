// ------------------------------------------------------------------------------------------------
// <copyright file="NonTerminalElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Describes a non-terminal Element
    /// </summary>
    public class NonTerminalElement: RuleElement
    {
        /// <summary>
        /// Gets or sets the non-terminal element name
        /// </summary>
        public string Name { get; set; }
    }
}
