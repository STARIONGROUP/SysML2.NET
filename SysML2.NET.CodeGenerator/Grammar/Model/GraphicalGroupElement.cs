// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalGroupElement.cs" company="Starion Group S.A.">
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
    /// Describes a parenthesized group of <see cref="GraphicalAlternatives" />
    /// </summary>
    public class GraphicalGroupElement : GraphicalRuleElement
    {
        /// <summary>
        /// Gets the <see cref="GraphicalAlternatives" /> of the group
        /// </summary>
        public List<GraphicalAlternatives> Alternatives { get; } = [];
    }
}
