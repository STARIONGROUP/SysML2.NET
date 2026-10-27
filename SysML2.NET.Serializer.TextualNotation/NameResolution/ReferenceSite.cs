// ------------------------------------------------------------------------------------------------
// <copyright file="ReferenceSite.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
