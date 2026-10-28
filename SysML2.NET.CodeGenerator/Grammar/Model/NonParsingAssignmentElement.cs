// ------------------------------------------------------------------------------------------------
// <copyright file="NonParsingAssignmentElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Defines the non-parsing assignment element
    /// </summary>
    public class NonParsingAssignmentElement : RuleElement
    {
        /// <summary>
        /// Gets or sets the property's name
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the assignment operator
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the assigned value
        /// </summary>
        public string Value { get; set; }
    }
}
