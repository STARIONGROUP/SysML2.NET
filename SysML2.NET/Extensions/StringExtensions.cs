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

namespace SysML2.NET.Extensions
{
    using System.Text;
    using System.Text.RegularExpressions;

    using SysML2.NET.LexicalRules;

    /// <summary>
    /// Provides extensions methods for the <see cref="string"/> class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Verifies that a name can be qualified as basic name. cfr. Section 7.2.2 of the SysML 2 Specification for the full definition
        /// </summary>
        /// <param name="name">The name to verifies</param>
        /// <returns><c>true</c> if the name is a basic name, <c>false</c> otherwise</returns>
        public static bool QueryIsValidBasicName(this string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z_][a-zA-Z0-9_]*$") && !Keywords.ReservedKeywords.Contains(name);
        }

        /// <summary>
        /// Converts a non-basic name to an unrestricted name. cfr. Section 8.2.2.3 of KerML specification
        /// </summary>
        /// <param name="name">A non-basic name</param>
        /// <returns>The unrestricted name</returns>
        public static string ToUnrestrictedName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "''";
            }

            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append("'");
            
            foreach (var character in name)
            {
                switch (character)
                {
                    case '\'': stringBuilder.Append(@"\'"); break;
                    case '\"': stringBuilder.Append(@"\"""); break;
                    case '\b': stringBuilder.Append(@"\b"); break;
                    case '\f': stringBuilder.Append(@"\f"); break;
                    case '\t': stringBuilder.Append(@"\t"); break;
                    case '\n': stringBuilder.Append(@"\n"); break;
                    case '\\': stringBuilder.Append(@"\\"); break;
                    default:
                        stringBuilder.Append(character);
                        break;
                }
            }

            stringBuilder.Append("'");
            return stringBuilder.ToString();
        }
        
        /// <summary>
        /// Finds the index of the last <c>::</c> separator in a qualified name that is not inside
        /// an unrestricted name (single-quoted segment).
        /// </summary>
        /// <param name="qualifiedName">
        /// The qualified name string
        /// </param>
        /// <returns>
        /// The index of the last <c>::</c> separator, or -1 if the name has only one segment
        /// </returns>
        public static int FindLastQualifiedNameSeparatorIndex(this string qualifiedName)
        {
            var lastSeparatorIndex = -1;
            var inQuote = false;

            for (var i = 0; i < qualifiedName.Length; i++)
            {
                var c = qualifiedName[i];

                switch (c)
                {
                    case '\\' when inQuote && i + 1 < qualifiedName.Length:
                        i++;
                        continue;
                    case '\'':
                        inQuote = !inQuote;
                        continue;
                }

                if (!inQuote && c == ':' && i + 1 < qualifiedName.Length && qualifiedName[i + 1] == ':')
                {
                    lastSeparatorIndex = i;
                    i++;
                }
            }

            return lastSeparatorIndex;
        }

        /// <summary>
        /// If the given name segment is an unrestricted name (surrounded by single quotes),
        /// unescape it by removing the quotes and replacing escape sequences. Otherwise,
        /// return the name as-is.
        /// </summary>
        /// <param name="nameSegment">
        /// A single name segment from a qualified name
        /// </param>
        /// <returns>
        /// The unescaped name
        /// </returns>
        public static string UnescapeUnrestrictedName(this string nameSegment)
        {
            if (nameSegment.Length < 2 || nameSegment[0] != '\'' || nameSegment[^1] != '\'')
            {
                return nameSegment;
            }

            var inner = nameSegment.Substring(1, nameSegment.Length - 2);

            var sb = new StringBuilder(inner.Length);

            for (var i = 0; i < inner.Length; i++)
            {
                if (inner[i] == '\\' && i + 1 < inner.Length)
                {
                    i++;

                    switch (inner[i])
                    {
                        case '\'': sb.Append('\''); break;
                        case '"': sb.Append('"'); break;
                        case 'b': sb.Append('\b'); break;
                        case 'f': sb.Append('\f'); break;
                        case 't': sb.Append('\t'); break;
                        case 'n': sb.Append('\n'); break;
                        case '\\': sb.Append('\\'); break;
                        default: sb.Append(inner[i]); break;
                    }
                }
                else
                {
                    sb.Append(inner[i]);
                }
            }

            return sb.ToString();
        }
    }
}
