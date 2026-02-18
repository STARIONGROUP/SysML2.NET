// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentElement.cs" company="Starion Group S.A.">
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
        /// Getss or sets the assignment value
        /// </summary>
        public RuleElement Value { get; set; }
    }
}
