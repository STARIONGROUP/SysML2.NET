// ------------------------------------------------------------------------------------------------
// <copyright file="IPartOfTextualRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Asserts that the current element is part of <see cref="TextualNotationRule"/>
    /// </summary>
    public interface IPartOfTextualRule
    {
        /// <summary>
        /// Gets the <see cref="TextualNotationRule" />
        /// </summary>
        TextualNotationRule TextualNotationRule { get; }
    }
}
