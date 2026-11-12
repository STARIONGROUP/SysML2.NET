// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalImageElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Describes a graphical shape or line of a production, given as an embedded image
    /// </summary>
    public class GraphicalImageElement : GraphicalRuleElement
    {
        /// <summary>
        /// Gets or sets the image path, relative to the grammar file
        /// </summary>
        public string Source { get; set; }
    }
}
