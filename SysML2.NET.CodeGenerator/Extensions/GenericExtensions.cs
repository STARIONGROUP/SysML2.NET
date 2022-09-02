// -------------------------------------------------------------------------------------------------
// <copyright file="GenericExtensions.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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
    using System;
    using System.Linq;
    
    /// <summary>
    /// DotLiquid filter for generic Code Generation Features
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Capitalize the first letter of a string
        /// </summary>
        /// <param name="input">
        /// The subject input string
        /// </param>
        /// <returns>
        /// Returns a string
        /// </returns>
        public static string CapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("string can't be empty!");
            }

            return string.Concat(input.First().ToString().ToUpper(), input.AsSpan(1));
        }

        /// <summary>
        /// Lower ccase the first letter of a string
        /// </summary>
        /// <param name="input">
        /// The subject input string
        /// </param>
        /// <returns>
        /// Returns a string
        /// </returns>
        public static string LowerCaseFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("string can't be empty!");
            }

            return string.Concat(input.First().ToString().ToLower(), input.AsSpan(1));
        }

        /// <summary>
        /// Prefixes the input string with another
        /// </summary>
        /// <param name="input">
        /// the string that is to be prefixed
        /// </param>
        /// <param name="prefix">
        /// the subject prefix
        /// </param>
        /// <returns></returns>
        public static string Prefix(this string input, string prefix)
        {
            return $"{prefix}{input}";
        }

        /// <summary>
        /// Returns the opposite value of a boolean
        /// </summary>
        /// <param name="value">
        /// the subject boolean
        /// </param>
        /// <returns>
        /// true or false
        /// </returns>
        public static bool FlipIt(bool value)
        {
            return !value;
        }

        /// <summary>
        /// Increments the value with 1
        /// </summary>
        /// <param name="value">
        /// the value that is to be incremented
        /// </param>
        /// <returns>
        /// the value incremented with 1
        /// </returns>
        public static int Increment(int value)
        {
            var result = value + 1;
            return result;
        }

        /// <summary>
        /// Increments the value with 1
        /// </summary>
        /// <param name="value">
        /// the value that is to be incremented
        /// </param>
        /// <returns>
        /// the value incremented with 1
        /// </returns>
        public static int Decrement(int value)
        {
            var result = value - 1;
            return result;
        }
    }
}
