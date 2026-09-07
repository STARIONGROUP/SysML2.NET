// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceSite.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// What stays fixed while one reference is resolved: the scopes to probe and the exclusions that
    /// apply to every probe of it.
    /// </summary>
    internal sealed class ReferenceSite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceSite" /> class.
        /// </summary>
        /// <param name="sourcePoco">The POCO bearing the reference.</param>
        /// <param name="chain">The scopes to probe, innermost first, with the match floor.</param>
        /// <param name="localRedefiner">Feature to exclude from every bucket, or <see langword="null" />.</param>
        /// <param name="selfBinding">The binding the reference itself is, or <see langword="null" />.</param>
        internal ReferenceSite(IElement sourcePoco, SourceScopeChain chain, IFeature localRedefiner, SelfBinding selfBinding)
        {
            this.SourcePoco = sourcePoco;
            this.Chain = chain;
            this.LocalRedefiner = localRedefiner;
            this.SelfBinding = selfBinding;
        }

        /// <summary>
        /// Gets the POCO bearing the reference.
        /// </summary>
        internal IElement SourcePoco { get; }

        /// <summary>
        /// Gets the scopes to probe, innermost first, with the depth at which a match is admissible.
        /// </summary>
        internal SourceScopeChain Chain { get; }

        /// <summary>
        /// Gets the Feature excluded from every bucket — the reference's own redefining or
        /// referencing Feature, which must not shadow its own target.
        /// </summary>
        internal IFeature LocalRedefiner { get; }

        /// <summary>
        /// Gets the binding the reference itself is, whose entry does not exist at parse time.
        /// </summary>
        internal SelfBinding SelfBinding { get; }
    }
}
