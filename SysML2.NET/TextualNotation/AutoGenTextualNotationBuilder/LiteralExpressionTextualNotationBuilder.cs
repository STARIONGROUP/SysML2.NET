// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="LiteralExpressionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralExpression" /> element
    /// </summary>
    public static partial class LiteralExpressionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule LiteralExpression
        /// <para>LiteralExpression=LiteralBoolean|LiteralString|LiteralInteger|LiteralReal|LiteralInfinity</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralExpression" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildLiteralExpression(SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralExpression poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralBoolean pocoLiteralBoolean:
                    LiteralBooleanTextualNotationBuilder.BuildLiteralBoolean(pocoLiteralBoolean, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralString pocoLiteralString:
                    LiteralStringTextualNotationBuilder.BuildLiteralString(pocoLiteralString, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralInteger pocoLiteralInteger:
                    LiteralIntegerTextualNotationBuilder.BuildLiteralInteger(pocoLiteralInteger, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralInfinity pocoLiteralInfinity:
                    LiteralInfinityTextualNotationBuilder.BuildLiteralInfinity(pocoLiteralInfinity, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Elements.IElement pocoElement:
                    BuildValue(poco, cursorCache, stringBuilder);

                    break;
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
