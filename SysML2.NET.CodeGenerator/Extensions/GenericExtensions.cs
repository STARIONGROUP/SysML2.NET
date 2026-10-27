// ------------------------------------------------------------------------------------------------
// <copyright file="GenericExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
