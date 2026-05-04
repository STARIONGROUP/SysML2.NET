// -------------------------------------------------------------------------------------------------
// <copyright file="RuleGenerationContext.cs" company="Starion Group S.A.">
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

    using System.Linq;

    using SysML2.NET.CodeGenerator.Grammar.Model;

    using uml4net.CommonStructure;

    /// <summary>
    /// Provides context over the rule generation history
    /// </summary>
    internal class RuleGenerationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleGenerationContext" /> class.
        /// </summary>
        /// <param name="namedElementToGenerate">The <see cref="INamedElement" /> used in the current generation</param>
        public RuleGenerationContext(INamedElement namedElementToGenerate)
        {
            this.NamedElementToGenerate = namedElementToGenerate;
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="CursorDefinition" />
        /// </summary>
        public List<CursorDefinition> DefinedCursors { get; set; }

        /// <summary>
        /// Gets the collection of all available <see cref="TextualNotationRule" />
        /// </summary>
        public List<TextualNotationRule> AllRules { get; } = [];

        /// <summary>
        /// Gets or sets the <see cref="RuleElement" /> that called other rule
        /// </summary>
        public RuleElement CallerRule { get; set; }

        /// <summary>
        /// Gets the <see cref="INamedElement" /> that is used in the current generation
        /// </summary>
        public INamedElement NamedElementToGenerate { get; }

        /// <summary>
        /// Looks up a <see cref="TextualNotationRule" /> by name from <see cref="AllRules" />.
        /// </summary>
        /// <param name="ruleName">The grammar rule name to find</param>
        /// <returns>The matching rule, or <c>null</c> if not found</returns>
        public TextualNotationRule FindRule(string ruleName)
        {
            return this.AllRules.SingleOrDefault(x => x.RuleName == ruleName);
        }

        /// <summary>
        /// Gets or sets the current name of the variable to process
        /// </summary>
        public string CurrentVariableName { get; set; }

        /// <summary>
        /// Gets or sets the sibling elements of the current processing context,
        /// used to determine whether a trailing space should be suppressed.
        /// </summary>
        public IReadOnlyList<RuleElement> CurrentSiblingElements { get; set; }

        /// <summary>
        /// Gets or sets the index of the element currently being processed
        /// within <see cref="CurrentSiblingElements" />.
        /// </summary>
        public int CurrentElementIndex { get; set; }

        /// <summary>
        /// Gets the set of HandCoded method names that have already been emitted during the
        /// current rule's code generation. Used to suppress duplicate HandCoded fallback calls
        /// when multiple unresolvable grammar segments of the same rule each fall back to
        /// <c>Build{Rule}HandCoded</c> (e.g., a group and a trailing collection non-terminal).
        /// </summary>
        public HashSet<string> EmittedHandCodedCalls { get; } = [];

        /// <summary>
        /// Determines whether the next sibling element is a terminal that uses <c>AppendLine</c>
        /// (e.g., <c>{</c>, <c>}</c>, <c>;</c>), in which case a trailing space would be unnecessary.
        /// </summary>
        /// <returns><c>true</c> if the next sibling is a newline terminal; <c>false</c> otherwise</returns>
        public bool IsNextElementNewLineTerminal()
        {
            if (this.CurrentSiblingElements == null)
            {
                return false;
            }

            var nextIndex = this.CurrentElementIndex + 1;

            if (nextIndex >= this.CurrentSiblingElements.Count)
            {
                return false;
            }

            return this.CurrentSiblingElements[nextIndex] is TerminalElement { Value: "{" or "}" or ";" };
        }

        /// <summary>
        /// Determines whether the current element is the last in the sibling list,
        /// meaning no more content follows and a trailing space would be unnecessary.
        /// </summary>
        /// <returns><c>true</c> if this is the last element; <c>false</c> otherwise</returns>
        public bool IsLastElement()
        {
            if (this.CurrentSiblingElements == null)
            {
                return false;
            }

            return this.CurrentElementIndex + 1 >= this.CurrentSiblingElements.Count;
        }
    }
}
