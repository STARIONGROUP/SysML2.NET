// -------------------------------------------------------------------------------------------------
// <copyright file="ScopeBinding.cs" company="Starion Group S.A.">
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
