// -------------------------------------------------------------------------------------------------
// <copyright file="ReservedCSharpNameMapper.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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
    /// <summary>
    /// The purpose of the <see cref="ReservedCSharpNameMapper"/> is to map strings that are keywords used in C#
    /// </summary>
    public static class ReservedCSharpNameMapper
    {
        /// <summary>
        /// Maps a reserved keyword in c# to a string that can be used in code
        /// </summary>
        /// <param name="input">
        /// The input string
        /// </param>
        /// <returns></returns>
        public static string Map(string input)
        {
            switch (input)
            {
                case "in":
                    return "@in";
                case "out":
                    return "@out";
                case "ref":
                    return "@ref";
                case "var":
                    return "@var";
                case "<":
                    return "LT";
                case "<=":
                    return "LTEQ";
                case ">":
                    return "GT";
                case ">=":
                    return "GTEQ";
                case "=":
                    return "EQ";
                case "true":
                    return "True";
                case "false":
                    return "False";
                default:
                    return input;
            }
        }

        /// <summary>
        /// Queries whether the input string is a reserved keyword in C#
        /// </summary>
        /// <param name="input">
        /// The string that is to be queried
        /// </param>
        /// <returns>
        /// true when reserved, false if not
        /// </returns>
        public static bool QueryIsReserved(string input)
        {
            switch (input)
            {
                case "in":
                case "out":
                case "ref":
                case "var":
                case "<":
                case "<=":
                case ">":
                case ">=":
                case "=":
                case "true":
                case "false":
                    return true;
                default:
                    return false;
            }
        }
    }
}
