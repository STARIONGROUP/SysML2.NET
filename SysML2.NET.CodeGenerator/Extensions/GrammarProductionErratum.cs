// -------------------------------------------------------------------------------------------------
// <copyright file="GrammarProductionErratum.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;

    /// <summary>
    /// A single correction applied to the text of a KEBNF production before it is parsed.
    /// </summary>
    public sealed class GrammarProductionErratum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GrammarProductionErratum" /> class.
        /// </summary>
        /// <param name="ruleName">The rule the correction belongs to, used when reporting staleness.</param>
        /// <param name="original">The exact production text the grammar carries.</param>
        /// <param name="replacement">The text it is corrected to.</param>
        /// <param name="justification">The evidence that the original is a defect rather than intent.</param>
        /// <exception cref="ArgumentException">Thrown when any argument is null or whitespace.</exception>
        public GrammarProductionErratum(string ruleName, string original, string replacement, string justification)
        {
            if (string.IsNullOrWhiteSpace(ruleName))
            {
                throw new ArgumentException("The rule name is required.", nameof(ruleName));
            }

            if (string.IsNullOrWhiteSpace(original))
            {
                throw new ArgumentException("The original production text is required.", nameof(original));
            }

            if (string.IsNullOrWhiteSpace(replacement))
            {
                throw new ArgumentException("The replacement production text is required.", nameof(replacement));
            }

            if (string.IsNullOrWhiteSpace(justification))
            {
                throw new ArgumentException("A justification is required so the correction can be audited.", nameof(justification));
            }

            this.RuleName = ruleName;
            this.Original = original;
            this.Replacement = replacement;
            this.Justification = justification;
        }

        /// <summary>
        /// Gets the rule the correction belongs to.
        /// </summary>
        public string RuleName { get; }

        /// <summary>
        /// Gets the exact production text the grammar carries.
        /// </summary>
        public string Original { get; }

        /// <summary>
        /// Gets the text it is corrected to.
        /// </summary>
        public string Replacement { get; }

        /// <summary>
        /// Gets the evidence that the original is a defect rather than intent.
        /// </summary>
        public string Justification { get; }
    }
}
