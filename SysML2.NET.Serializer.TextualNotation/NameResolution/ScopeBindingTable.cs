// ------------------------------------------------------------------------------------------------
// <copyright file="ScopeBindingTable.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The per-scope binding table: raw simple name → bindings with provenance.
    /// </summary>
    internal sealed class ScopeBindingTable
    {
        /// <summary>Gets the bindings keyed by raw simple name.</summary>
        internal Dictionary<string, List<ScopeBinding>> ByName { get; } = new(StringComparer.Ordinal);

        /// <summary>
        /// Adds a binding under <paramref name="rawName" />, ignoring blank names and exact duplicates.
        /// </summary>
        /// <param name="rawName">The raw simple name the binding answers to.</param>
        /// <param name="binding">The binding.</param>
        internal void Add(string rawName, ScopeBinding binding)
        {
            if (string.IsNullOrWhiteSpace(rawName))
            {
                return;
            }

            if (!this.ByName.TryGetValue(rawName, out var bindings))
            {
                this.ByName[rawName] = bindings = [];
            }

            if (!bindings.Any(existing => ReferenceEquals(existing.Membership, binding.Membership)
                                          && ReferenceEquals(existing.Element, binding.Element)
                                          && existing.Layer == binding.Layer))
            {
                bindings.Add(binding);
            }
        }
    }
}
