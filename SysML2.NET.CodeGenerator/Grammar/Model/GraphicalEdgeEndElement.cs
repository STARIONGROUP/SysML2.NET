// ------------------------------------------------------------------------------------------------
// <copyright file="GraphicalEdgeEndElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Describes an element connected by a graphical line, written <c>&amp;element</c> (SysML 2.0 §8.2.3.1)
    /// </summary>
    public class GraphicalEdgeEndElement : GraphicalRuleElement
    {
        /// <summary>
        /// Gets or sets the name of the connected production
        /// </summary>
        public string Name { get; set; }
    }
}
