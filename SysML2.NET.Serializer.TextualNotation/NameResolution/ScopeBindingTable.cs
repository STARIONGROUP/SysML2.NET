// -------------------------------------------------------------------------------------------------
// <copyright file="ScopeBindingTable.cs" company="Starion Group S.A.">
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
