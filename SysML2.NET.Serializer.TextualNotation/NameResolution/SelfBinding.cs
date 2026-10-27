// ------------------------------------------------------------------------------------------------
// <copyright file="SelfBinding.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The binding a reference site declares itself, which does not exist while that declaration is
    /// being written.
    /// </summary>
    internal sealed class SelfBinding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfBinding" /> class.
        /// </summary>
        /// <param name="scope">The scope the membership binds in.</param>
        /// <param name="membership">The Membership that IS the reference.</param>
        internal SelfBinding(INamespace scope, IMembership membership)
        {
            this.Scope = scope;
            this.Membership = membership;
        }

        /// <summary>
        /// Gets the scope the membership binds in.
        /// </summary>
        internal INamespace Scope { get; }

        /// <summary>
        /// Gets the Membership that IS the reference, and whose binding therefore does not exist yet
        /// at parse time.
        /// </summary>
        internal IMembership Membership { get; }
    }
}
