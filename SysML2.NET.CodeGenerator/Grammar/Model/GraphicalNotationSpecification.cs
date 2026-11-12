// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalNotationSpecification.cs" company="Starion Group S.A.">
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
    /// Provides access to all <see cref="GraphicalNotationRule" /> defined in the graphical notation specification
    /// </summary>
    public class GraphicalNotationSpecification
    {
        /// <summary>
        /// Gets the <see cref="GraphicalNotationRule" />s in declaration order, one per production definition
        /// </summary>
        public List<GraphicalNotationRule> Rules { get; } = [];
    }
}
