// ------------------------------------------------------------------------------------------------
// <copyright file="ScopeBinding.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// One name binding with full provenance.
    /// </summary>
    internal readonly struct ScopeBinding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeBinding"/> struct.
        /// </summary>
        /// <param name="membership">The Membership that introduces the binding.</param>
        /// <param name="element">The bound element.</param>
        /// <param name="layer">The layer the binding entered through.</param>
        /// <param name="viaPublicImport">Whether an imported binding arrived through a PUBLIC import.</param>
        internal ScopeBinding(IMembership membership, IElement element, BindingLayer layer, bool viaPublicImport)
        {
            this.Membership = membership;
            this.Element = element;
            this.Layer = layer;
            this.ViaPublicImport = viaPublicImport;
        }

        /// <summary>Gets the Membership that introduces the binding.</summary>
        internal IMembership Membership { get; }

        /// <summary>Gets the bound element.</summary>
        internal IElement Element { get; }

        /// <summary>Gets the layer the binding entered through.</summary>
        internal BindingLayer Layer { get; }

        /// <summary>Gets a value indicating whether an imported binding arrived through a PUBLIC import.</summary>
        internal bool ViaPublicImport { get; }
    }
}
