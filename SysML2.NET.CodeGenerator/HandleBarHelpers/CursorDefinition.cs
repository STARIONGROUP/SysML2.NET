// -------------------------------------------------------------------------------------------------
// <copyright file="CursorDefinition.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System.Collections.Generic;

    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.Classification;
    using uml4net.Extensions;

    /// <summary>
    /// Models cursor state for enumerable property iteration during code generation
    /// </summary>
    internal class CursorDefinition
    {
        /// <summary>
        /// Gets or sets the <see cref="IProperty" /> that have to have a cursor defined
        /// </summary>
        public IProperty DefinedForProperty { get; init; }

        /// <summary>
        /// Gets the name of the variable defined for the cursor
        /// </summary>
        public string CursorVariableName => $"{this.DefinedForProperty.Name.LowerCaseFirstLetter()}Cursor";

        /// <summary>
        /// Provides a collection of <see cref="AssignmentElement" /> that will use the defined cursor
        /// </summary>
        public HashSet<AssignmentElement> ApplicableRuleElements { get; } = [];

        /// <summary>
        /// Asserts that the current <see cref="CursorDefinition" /> is valid for an <see cref="IProperty" />
        /// </summary>
        /// <param name="property">The specific <see cref="IProperty" /></param>
        /// <returns>True if the <see cref="CursorDefinition" /> is valid for the provided property</returns>
        public bool IsCursorValidForProperty(IProperty property)
        {
            return property == this.DefinedForProperty;
        }
    }
}
