// ------------------------------------------------------------------------------------------------
// <copyright file="OclErratum.cs" company="Starion Group S.A.">
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
    /// A single correction applied to an OCL body carried by the UML XMI.
    /// </summary>
    public sealed class OclErratum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OclErratum" /> class.
        /// </summary>
        /// <param name="original">The exact text the XMI carries, including any surrounding quotes.</param>
        /// <param name="replacement">The text it is corrected to.</param>
        /// <param name="justification">The evidence that the original is a defect rather than intent.</param>
        /// <exception cref="ArgumentException">Thrown when any argument is null or whitespace.</exception>
        public OclErratum(string original, string replacement, string justification)
        {
            if (string.IsNullOrWhiteSpace(original))
            {
                throw new ArgumentException("The original text is required.", nameof(original));
            }

            if (string.IsNullOrWhiteSpace(replacement))
            {
                throw new ArgumentException("The replacement text is required.", nameof(replacement));
            }

            if (string.IsNullOrWhiteSpace(justification))
            {
                throw new ArgumentException("A justification is required so the correction can be audited.", nameof(justification));
            }

            this.Original = original;
            this.Replacement = replacement;
            this.Justification = justification;
        }

        /// <summary>
        /// Gets the exact text the XMI carries.
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
