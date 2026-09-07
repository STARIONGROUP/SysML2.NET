// -------------------------------------------------------------------------------------------------
// <copyright file="SourceScopeChain.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The scopes a reference probes, innermost first, with the depth below which a match is barred.
    /// </summary>
    internal sealed class SourceScopeChain
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceScopeChain" /> class.
        /// </summary>
        /// <param name="scopes">The scopes, innermost first.</param>
        /// <param name="matchFloor">The index of the innermost scope a match may come from.</param>
        internal SourceScopeChain(IReadOnlyList<INamespace> scopes, int matchFloor)
        {
            this.Scopes = scopes;
            this.MatchFloor = matchFloor;
        }

        /// <summary>
        /// Gets the scopes of the chain, innermost first.
        /// </summary>
        internal IReadOnlyList<INamespace> Scopes { get; }

        /// <summary>
        /// Gets the index of the innermost scope a match may come from; scopes below it are consulted
        /// for shadowing only.
        /// </summary>
        internal int MatchFloor { get; }
    }
}
