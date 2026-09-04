// -------------------------------------------------------------------------------------------------
// <copyright file="LayeredOutcomeKind.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    /// <summary>
    /// The outcome kind of probing one name in one scope.
    /// </summary>
    internal enum LayeredOutcomeKind
    {
        /// <summary>The scope does not bind the name; the outward walk may continue.</summary>
        NotBound,

        /// <summary>The scope binds the name to exactly one election winner.</summary>
        Bound,

        /// <summary>The scope binds the name to several peers; a reader cannot pick one.</summary>
        Ambiguous
    }
}
