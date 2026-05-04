// -------------------------------------------------------------------------------------------------
// <copyright file="SymbolicKeywordKindExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions
{
    using System;

    using SysML2.NET.LexicalRules;

    /// <summary>
    /// Provides extensions methods for the <see cref="SymbolicKeywordKind"/> enumeration.
    /// Generated from the KEBNF grammar rules that pair a symbolic notation with a keyword
    /// notation (e.g. <c>DEFINED_BY = ':' | 'defined' 'by'</c>).
    /// </summary>
    public static class SymbolicKeywordKindExtensions
    {
        /// <summary>
        /// Gets the associated keyword of a <see cref="SymbolicKeywordKind"/>
        /// </summary>
        /// <param name="symbolicKeywordKind">The <see cref="SymbolicKeywordKind"/> instance</param>
        /// <returns>The associated keyword</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the provided <see cref="SymbolicKeywordKind"/> is not recognized</exception>
        public static string GetKeyword(this SymbolicKeywordKind symbolicKeywordKind)
        {
            return symbolicKeywordKind switch
            {
                SymbolicKeywordKind.DefinedBy => "defined by",
                SymbolicKeywordKind.Specializes => "specializes",
                SymbolicKeywordKind.Subsets => "subsets",
                SymbolicKeywordKind.References => "references",
                SymbolicKeywordKind.Crosses => "crosses",
                SymbolicKeywordKind.Redefines => "redefines",
                _ => throw new ArgumentOutOfRangeException(nameof(symbolicKeywordKind))
            };
        }

        /// <summary>
        /// Gets the associated symbol of a <see cref="SymbolicKeywordKind"/>
        /// </summary>
        /// <param name="symbolicKeywordKind">The <see cref="SymbolicKeywordKind"/> instance</param>
        /// <returns>The associated symbol</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the provided <see cref="SymbolicKeywordKind"/> is not recognized</exception>
        public static string GetSymbol(this SymbolicKeywordKind symbolicKeywordKind)
        {
            return symbolicKeywordKind switch
            {
                SymbolicKeywordKind.DefinedBy => ":",
                SymbolicKeywordKind.Specializes => ":>",
                SymbolicKeywordKind.Subsets => ":>",
                SymbolicKeywordKind.References => "::>",
                SymbolicKeywordKind.Crosses => "=>",
                SymbolicKeywordKind.Redefines => ":>>",
                _ => throw new ArgumentOutOfRangeException(nameof(symbolicKeywordKind))
            };
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
