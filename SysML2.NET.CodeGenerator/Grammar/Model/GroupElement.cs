// ------------------------------------------------------------------------------------------------
// <copyright file="GroupElement.cs" company="Starion Group S.A.">
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
    /// Defines a group of <see cref="RuleElement"/>
    /// </summary>
    public class GroupElement: RuleElement
    {
        /// <summary>
        /// Gets the collection <see cref="Alternatives"/> that are part of the <see cref="GroupElement" /> 
        /// </summary>
        public List<Alternatives> Alternatives { get; } = [];
    }
}
