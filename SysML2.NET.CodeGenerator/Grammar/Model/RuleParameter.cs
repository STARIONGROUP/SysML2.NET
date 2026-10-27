// ------------------------------------------------------------------------------------------------
// <copyright file="RuleParameter.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    /// <summary>
    /// Defines possible parameter that could be added to a <see cref="TextualNotationRule"/>
    /// </summary>
    public class RuleParameter 
    {
        /// <summary>
        /// Gets the parameter's name
        /// </summary>
        public string ParameterName { get; set; }
        
        /// <summary>
        /// Gets the name of the target Element
        /// </summary>
        public string TargetElementName { get; set; }
    }
}
