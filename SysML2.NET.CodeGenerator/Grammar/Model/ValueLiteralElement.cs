// ------------------------------------------------------------------------------------------------
// <copyright file="ValueLiteralElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Defines a <see cref="RuleElement"/> that is a value literal 
    /// </summary>
    public class ValueLiteralElement: RuleElement
    {
        /// <summary>
        /// Gets or sets the literal value
        /// </summary>
        public string Value { get; init; }
        
        /// <summary>
        /// Asserts that this value literal is to 
        /// </summary>
        /// <returns></returns>
        public bool QueryIsQualifiedName() => this.Value == "[QualifiedName]";
    }
}
