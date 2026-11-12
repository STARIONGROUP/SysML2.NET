// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalAlternatives.cs" company="Starion Group S.A.">
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
    /// Describes one alternative of a graphical production or group, as a sequence of elements
    /// </summary>
    public class GraphicalAlternatives
    {
        /// <summary>
        /// Gets the <see cref="GraphicalRuleElement" />s of the alternative, in sequence
        /// </summary>
        public List<GraphicalRuleElement> Elements { get; } = [];
    }
}
