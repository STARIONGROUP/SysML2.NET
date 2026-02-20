// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationRule.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Describe the content of a rule 
    /// </summary>
    public class TextualNotationRule
    {
        /// <summary>
        /// Get or set rule's name
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// Gets or set the name of the KerML Element that the rule target
        /// </summary>
        public string TargetElementName { get; set; }
        
        /// <summary>
        /// Gets or set an optional <see cref="RuleParameter"/> 
        /// </summary>
        public RuleParameter Parameter { get; set; }
        
        /// <summary>
        /// Gets the collection of defined <see cref="RuleElement" />
        /// </summary>
        public List<RuleElement> Elements { get; } = [];

        /// <summary>
        /// Gets or sets the raw string that declares the rule
        /// </summary>
        public string RawRule { get; set; }
    }
}
