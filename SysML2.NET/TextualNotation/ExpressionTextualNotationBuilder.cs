// -------------------------------------------------------------------------------------------------
// <copyright file="ExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// Hand-coded part of the <see cref="ExpressionTextualNotationBuilder" />
    /// </summary>
    public static partial class ExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule BaseExpression
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildBaseExpressionHandCoded(IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildBaseExpressionHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeaturePrefix
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeaturePrefixHandCoded(IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildFeaturePrefixHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryExpression
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildNonFeatureChainPrimaryExpressionHandCoded(IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildNonFeatureChainPrimaryExpressionHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedExpression
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildOwnedExpressionHandCoded(IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildOwnedExpressionHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SequenceExpressionList
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildSequenceExpressionListHandCoded(IExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildSequenceExpressionListHandCoded requires manual implementation");
        }
    }
}
