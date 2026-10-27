// ------------------------------------------------------------------------------------------------
// <copyright file="GrammarErratum.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;

    /// <summary>
    /// A single correction applied to a rule carried by the KEBNF grammar.
    /// </summary>
    public sealed class GrammarErratum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GrammarErratum" /> class.
        /// </summary>
        /// <param name="ruleName">The exact rule name the grammar carries.</param>
        /// <param name="targetElementName">The metaclass the rule targets, which the grammar omits.</param>
        /// <param name="justification">The evidence that the omission is a defect rather than intent.</param>
        /// <exception cref="ArgumentException">Thrown when any argument is null or whitespace.</exception>
        public GrammarErratum(string ruleName, string targetElementName, string justification)
        {
            if (string.IsNullOrWhiteSpace(ruleName))
            {
                throw new ArgumentException("The rule name is required.", nameof(ruleName));
            }

            if (string.IsNullOrWhiteSpace(targetElementName))
            {
                throw new ArgumentException("The target element name is required.", nameof(targetElementName));
            }

            if (string.IsNullOrWhiteSpace(justification))
            {
                throw new ArgumentException("A justification is required so the correction can be audited.", nameof(justification));
            }

            this.RuleName = ruleName;
            this.TargetElementName = targetElementName;
            this.Justification = justification;
        }

        /// <summary>
        /// Gets the exact rule name the grammar carries.
        /// </summary>
        public string RuleName { get; }

        /// <summary>
        /// Gets the metaclass the rule targets.
        /// </summary>
        public string TargetElementName { get; }

        /// <summary>
        /// Gets the evidence that the omission is a defect rather than intent.
        /// </summary>
        public string Justification { get; }
    }
}
