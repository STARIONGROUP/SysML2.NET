// ------------------------------------------------------------------------------------------------
// <copyright file="RuleElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    using System.Linq;

    /// <summary>
    /// Base class that provides information about element that is part of a rule
    /// </summary>
    public abstract class RuleElement: IPartOfTextualRule
    {
        /// <summary>
        /// Defines all suffix that defines optional elements
        /// </summary>
        private static readonly string[] OptionalSuffix = ["?", "*"];

        /// <summary>
        /// Defines all suffix that defines collection elements
        /// </summary>
        private static readonly string[] CollectionSuffix = ["*", "+"];

        /// <summary>
        /// Gets or sets an optional suffix
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Asserts that the current rule element defines an optional element
        /// </summary>
        public bool IsOptional => !string.IsNullOrEmpty(this.Suffix) && OptionalSuffix.Contains(this.Suffix);

        /// <summary>
        /// Asserts that the current rule element defines an collection element
        /// </summary>
        public bool IsCollection => !string.IsNullOrEmpty(this.Suffix) && CollectionSuffix.Contains(this.Suffix);

        /// <summary>
        /// Gets or sets an optional <see cref="RuleElement" /> that acts as container
        /// </summary>
        public RuleElement Container { get; set; }

        /// <summary>
        /// Gets the <see cref="IPartOfTextualRule.TextualNotationRule" />
        /// </summary>
        public TextualNotationRule TextualNotationRule { get; init; }
    }
}
