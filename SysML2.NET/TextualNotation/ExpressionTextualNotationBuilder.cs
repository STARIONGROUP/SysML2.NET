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
        /// Builds the Textual Notation string for the rule SequenceExpressionList
        /// <remarks>SequenceExpressionList:Expression=OwnedExpression','?|SequenceOperatorExpression. SequenceOperatorExpression is an OperatorExpression with operator=','.</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IExpression" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildSequenceExpressionListHandCoded(IExpression poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            // Dispatch: if the poco is an OperatorExpression with operator=',', emit as SequenceOperatorExpression;
            // otherwise emit as OwnedExpression (the optional trailing comma in the grammar is not emitted)
            if (poco is SysML2.NET.Core.POCO.Kernel.Expressions.IOperatorExpression { Operator: "," } operatorExpression)
            {
                OperatorExpressionTextualNotationBuilder.BuildSequenceOperatorExpression(operatorExpression, writerContext, stringBuilder);
            }
            else
            {
                BuildOwnedExpression(poco, writerContext, stringBuilder);
            }
        }
    }
}
