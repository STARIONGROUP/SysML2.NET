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
        public static bool QueryIsBasicName(this string name)
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
    }
}
