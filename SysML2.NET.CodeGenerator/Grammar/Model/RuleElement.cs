// -------------------------------------------------------------------------------------------------
// <copyright file="RuleElement.cs" company="Starion Group S.A.">
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
    using System.Linq;

    /// <summary>
    /// Base class that provides information about element that is part of a rule
    /// </summary>
    public abstract class RuleElement
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
    }
}
