// -------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System.Text;

    /// <summary>
    /// String helpers used by the grammar and lexical-rules code-generation passes.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Tests whether a string follows the SysML lexical-rule naming convention: uppercase
        /// letters, digits and underscores only, with at least one letter (e.g.
        /// <c>RESERVED_KEYWORD</c>, <c>DEFINED_BY</c>, <c>STRING_VALUE</c>).
        /// </summary>
        /// <param name="value">The string to test</param>
        /// <returns><c>true</c> when the string matches the all-uppercase snake-case convention</returns>
        public static bool IsAllUpperSnake(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            var hasLetter = false;

            foreach (var character in value)
            {
                if (char.IsLetter(character))
                {
                    hasLetter = true;

                    if (!char.IsUpper(character))
                    {
                        return false;
                    }
                }
                else if (character != '_' && !char.IsDigit(character))
                {
                    return false;
                }
            }

            return hasLetter;
        }

        /// <summary>
        /// Tests whether the supplied string contains any letter.
        /// </summary>
        /// <param name="value">The string to test</param>
        /// <returns><c>true</c> when at least one letter is present</returns>
        public static bool ContainsAnyLetter(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            foreach (var character in value)
            {
                if (char.IsLetter(character))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Converts a <c>SNAKE_CASE</c> identifier into <c>PascalCase</c>
        /// (e.g. <c>DEFINED_BY</c> → <c>DefinedBy</c>).
        /// </summary>
        /// <param name="value">The snake-case identifier</param>
        /// <returns>The PascalCase form</returns>
        public static string ToPascalCaseFromSnake(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var builder = new StringBuilder(value.Length);
            var capitalizeNext = true;

            foreach (var character in value)
            {
                if (character == '_')
                {
                    capitalizeNext = true;
                    continue;
                }

                builder.Append(capitalizeNext ? char.ToUpperInvariant(character) : char.ToLowerInvariant(character));
                capitalizeNext = false;
            }

            return builder.ToString();
        }
    }
}
