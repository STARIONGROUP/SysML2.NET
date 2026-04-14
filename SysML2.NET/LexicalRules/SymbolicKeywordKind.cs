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

namespace SysML2.NET.LexicalRules
{
    /// <summary>
    /// Defines keyword that could also have symbolic representation  
    /// </summary>
    public enum SymbolicKeywordKind
    {
        /// <summary>
        /// Declares the DEFINED_BY case
        /// </summary>
        DefinedBy,
        
        /// <summary>
        /// Declares the SPECIALIZES case
        /// </summary>
        Specializes,
        
        /// <summary>
        /// Declares the SUBSETS case
        /// </summary>
        Subsets,
        
        /// <summary>
        /// Declares the REFERENCES case
        /// </summary>
        References,
        
        /// <summary>
        /// Declares the CROSSES case
        /// </summary>
        Crosses,
        
        /// <summary>
        /// Declares the REDEFINES case
        /// </summary>
        Redefines
    }
}
