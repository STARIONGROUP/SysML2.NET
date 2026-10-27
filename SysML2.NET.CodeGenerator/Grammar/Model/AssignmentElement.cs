// ------------------------------------------------------------------------------------------------
// <copyright file="AssignmentElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Defines the assignment element
    /// </summary>
    public class AssignmentElement: RuleElement
    {
        /// <summary>
        /// Gets or set the property's name
        /// </summary>
        public string Property { get; set; }
        
        /// <summary>
        /// Gets or sets the assignment operator
        /// </summary>
        public string Operator { get; set; }
        
        /// <summary>
        /// Gets or sets the assignment value
        /// </summary>
        public RuleElement Value { get; set; }

        /// <summary>
        /// Gets or sets an optional prefix
        /// </summary>
        public string Prefix { get; set; }
    }
}
