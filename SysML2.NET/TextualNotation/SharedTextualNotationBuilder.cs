// -------------------------------------------------------------------------------------------------
// <copyright file="SharedTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Hand-coded part of the <see cref="SharedTextualNotationBuilder" />.
    /// Hosts the <c>Build{Rule}HandCoded</c> stubs for shared no-target rules (and lexical
    /// sub-rules they transitively inline) whose body cannot be fully generated and requires
    /// a manual implementation.
    /// </summary>
    public static partial class SharedTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the lexical rule BASIC_INITIAL_CHARACTER.
        /// Inlined into shared rules that reference <c>NAME</c> (e.g. <c>QualifiedName</c>).
        /// </summary>
        /// <param name="poco">The <see cref="IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildBASIC_INITIAL_CHARACTERHandCoded(IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildBASIC_INITIAL_CHARACTERHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the lexical rule BASIC_NAME.
        /// Inlined into shared rules that reference <c>NAME</c> (e.g. <c>QualifiedName</c>).
        /// </summary>
        /// <param name="poco">The <see cref="IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildBASIC_NAMEHandCoded(IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildBASIC_NAMEHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeaturePrefix.
        /// </summary>
        /// <param name="poco">The <see cref="IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeaturePrefixHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildFeaturePrefixHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonBehaviorBodyItem.
        /// </summary>
        /// <param name="poco">The <see cref="IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildNonBehaviorBodyItemHandCoded(IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildNonBehaviorBodyItemHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RealValue (the value of a
        /// <c>LiteralReal</c>), which the grammar expresses as an <see cref="IExpression" />.
        /// </summary>
        /// <param name="poco">The <see cref="IExpression" /> that holds the real value expression</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildRealValueHandCoded(IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildRealValueHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the lexical rule UNRESTRICTED_NAME.
        /// Inlined into shared rules that reference <c>NAME</c> (e.g. <c>QualifiedName</c>).
        /// </summary>
        /// <param name="poco">The <see cref="IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildUNRESTRICTED_NAMEHandCoded(IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildUNRESTRICTED_NAMEHandCoded requires manual implementation");
        }

        /// <summary>
        /// Evaluates the condition under which the optional <c>$::</c> prefix of a
        /// <c>QualifiedName</c> must be emitted. The generated <c>BuildQualifiedName</c>
        /// guards the prefix with this predicate.
        /// </summary>
        /// <param name="poco">The <see cref="IElement" /> being serialised</param>
        /// <returns>True when the <c>$::</c> prefix should be emitted</returns>
        private static bool BuildGroupConditionForQualifiedName(IElement poco)
        {
            throw new System.NotSupportedException("BuildGroupConditionForQualifiedName requires manual implementation");
        }
    }
}
