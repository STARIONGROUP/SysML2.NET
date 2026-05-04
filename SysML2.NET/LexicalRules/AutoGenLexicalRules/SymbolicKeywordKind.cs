// -------------------------------------------------------------------------------------------------
// <copyright file="SymbolicKeywordKind.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.LexicalRules
{
    /// <summary>
    /// Defines keywords that also have a symbolic representation. Generated from KEBNF rules of the
    /// form <c>RULE_NAME = '&lt;symbol&gt;' | '&lt;keyword&gt;'</c>.
    /// </summary>
    public enum SymbolicKeywordKind
    {
        /// <summary>
        /// Declares the DEFINED_BY case (<c>:</c> or <c>defined by</c>)
        /// </summary>
        DefinedBy,

        /// <summary>
        /// Declares the SPECIALIZES case (<c>:&gt;</c> or <c>specializes</c>)
        /// </summary>
        Specializes,

        /// <summary>
        /// Declares the SUBSETS case (<c>:&gt;</c> or <c>subsets</c>)
        /// </summary>
        Subsets,

        /// <summary>
        /// Declares the REFERENCES case (<c>::&gt;</c> or <c>references</c>)
        /// </summary>
        References,

        /// <summary>
        /// Declares the CROSSES case (<c>=&gt;</c> or <c>crosses</c>)
        /// </summary>
        Crosses,

        /// <summary>
        /// Declares the REDEFINES case (<c>:&gt;&gt;</c> or <c>redefines</c>)
        /// </summary>
        Redefines

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
