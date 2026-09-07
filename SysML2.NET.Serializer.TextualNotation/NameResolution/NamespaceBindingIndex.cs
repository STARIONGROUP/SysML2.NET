// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceBindingIndex.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;

    /// <summary>
    /// The bindings every indexed Namespace offers, and the lookups that answer "what does this name bind
    /// to in this scope" (KerML §8.2.3.5.3).
    /// </summary>
    internal sealed class NamespaceBindingIndex
    {
        /// <summary>
        /// Shared empty index returned for unknown or null Namespaces.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, HashSet<IElement>> EmptyIndex
            = new Dictionary<string, HashSet<IElement>>(StringComparer.Ordinal);

        /// <summary>
        /// The layer probing order: owned shadows inherited shadows imported.
        /// </summary>
        private static readonly BindingLayer[] LayerPrecedence = [BindingLayer.Owned, BindingLayer.Inherited, BindingLayer.Imported];

        /// <summary>
        /// Reverse index of alias bindings: scope to (aliased element to alias names).
        /// </summary>
        private readonly Dictionary<INamespace, Dictionary<IElement, List<string>>> aliasIndex;

        /// <summary>
        /// Reverse index: owning Namespace to the Namespaces re-exporting it through a public import.
        /// </summary>
        private readonly Dictionary<INamespace, HashSet<INamespace>> directFacadeIndex;

        /// <summary>
        /// The other root Namespaces forming the global Namespace.
        /// </summary>
        private readonly List<INamespace> globalNamespaces;

        /// <summary>
        /// The layered binding tables, one per indexed scope.
        /// </summary>
        private readonly Dictionary<INamespace, ScopeBindingTable> layeredBindings;

        /// <summary>
        /// Namespace to (simple name to member set), populated on first probe of each scope.
        /// </summary>
        private readonly Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>> simpleNameIndices = new();

        /// <summary>
        /// Builds a scope's bindings on its first probe.
        /// </summary>
        private readonly NamespaceBindingIndexBuilder scopeIndexBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceBindingIndex" /> class.
        /// </summary>
        /// <param name="rootNamespace">The indexed root Namespace.</param>
        /// <param name="globalNamespaces">The other root Namespaces forming the global Namespace.</param>
        /// <param name="scopeIndexBuilder">The builder invoked to index a scope on its first probe.</param>
        /// <param name="layeredBindings">Namespace to layered binding table, filled by <paramref name="scopeIndexBuilder" />.</param>
        /// <param name="directFacadeIndex">Owning Namespace to its re-exporting Namespaces.</param>
        /// <param name="aliasIndex">Scope to its alias bindings, filled by <paramref name="scopeIndexBuilder" />.</param>
        /// <param name="resolutionGraph">Maps implied generals onto the graph being written.</param>
        internal NamespaceBindingIndex(
            INamespace rootNamespace,
            List<INamespace> globalNamespaces,
            NamespaceBindingIndexBuilder scopeIndexBuilder,
            Dictionary<INamespace, ScopeBindingTable> layeredBindings,
            Dictionary<INamespace, HashSet<INamespace>> directFacadeIndex,
            Dictionary<INamespace, Dictionary<IElement, List<string>>> aliasIndex,
            ResolutionGraph resolutionGraph)
        {
            this.RootNamespace = rootNamespace;
            this.globalNamespaces = globalNamespaces;
            this.scopeIndexBuilder = scopeIndexBuilder;
            this.layeredBindings = layeredBindings;
            this.directFacadeIndex = directFacadeIndex;
            this.aliasIndex = aliasIndex;
            this.ResolutionGraph = resolutionGraph;
        }

        /// <summary>
        /// Gets the indexed root Namespace.
        /// </summary>
        internal INamespace RootNamespace { get; }

        /// <summary>
        /// Gets the map from implied generals onto the graph being written.
        /// </summary>
        internal ResolutionGraph ResolutionGraph { get; }

        /// <summary>
        /// Returns the alias names <paramref name="scope" /> binds <paramref name="target" /> under.
        /// </summary>
        /// <param name="scope">The scope declaring the aliases.</param>
        /// <param name="target">The aliased element.</param>
        /// <param name="aliasNames">The alias names, when any.</param>
        /// <returns><see langword="true" /> when the scope aliases the target.</returns>
        internal bool TryGetAliases(INamespace scope, IElement target, out List<string> aliasNames)
        {
            aliasNames = null;
            this.EnsureIndexed(scope);

            return this.aliasIndex.TryGetValue(scope, out var scopeAliases) && scopeAliases.TryGetValue(target, out aliasNames);
        }

        /// <summary>
        /// Indexes <paramref name="scope" /> on its first probe.
        /// </summary>
        /// <param name="scope">The scope being probed; may be <see langword="null" />.</param>
        private void EnsureIndexed(INamespace scope)
        {
            if (scope == null || this.simpleNameIndices.ContainsKey(scope))
            {
                return;
            }

            this.simpleNameIndices[scope] = this.scopeIndexBuilder.BuildScopeIndex(scope);
        }

        /// <summary>
        /// Returns the Namespaces that re-export <paramref name="owner" /> through a public import.
        /// </summary>
        /// <param name="owner">The owning Namespace.</param>
        /// <returns>The re-exporting Namespaces; empty when there are none.</returns>
        internal IEnumerable<INamespace> QueryFacades(INamespace owner)
        {
            return this.directFacadeIndex.TryGetValue(owner, out var facades) ? facades : [];
        }

        /// <summary>
        /// Returns the simple-name index for <paramref name="scope" />, building it on first probe, or
        /// <see cref="EmptyIndex" /> for <see langword="null" />.
        /// </summary>
        /// <param name="scope">The <see cref="INamespace" /> whose index is requested.</param>
        /// <returns>The simple-name → member-set lookup.</returns>
        internal IReadOnlyDictionary<string, HashSet<IElement>> GetSimpleNameIndex(INamespace scope)
        {
            this.EnsureIndexed(scope);

            return scope == null ? EmptyIndex : this.simpleNameIndices.GetValueOrDefault(scope, EmptyIndex);
        }

        /// <summary>
        /// Probes one raw simple name in one scope, layer by layer in shadowing order, electing the
        /// redefinition leaf among a layer's peers.
        /// </summary>
        /// <param name="scope">The scope to probe.</param>
        /// <param name="rawName">The raw simple name.</param>
        /// <param name="excludedMembership">The reference's own Membership, absent at parse time; may be <see langword="null" />.</param>
        /// <param name="localRedefiner">The redefining feature excluded from every probe; may be <see langword="null" />.</param>
        /// <param name="insideView">Whether the scope is seen from INSIDE (containment), which admits non-public bindings.</param>
        /// <returns>The outcome kind and, when bound, the elected element.</returns>
        internal (LayeredOutcomeKind Kind, IElement Element) ResolveNameInScope(INamespace scope, string rawName, IMembership excludedMembership, IFeature localRedefiner, bool insideView)
        {
            this.EnsureIndexed(scope);

            if (scope == null
                || !this.layeredBindings.TryGetValue(scope, out var table)
                || !table.ByName.TryGetValue(rawName, out var bindings))
            {
                return (LayeredOutcomeKind.NotBound, null);
            }

            foreach (var layer in LayerPrecedence)
            {
                var candidates = bindings
                    .Where(binding => binding.Layer == layer
                                      && !ReferenceEquals(binding.Membership, excludedMembership)
                                      && !ReferenceEquals(binding.Element, localRedefiner)
                                      && (insideView || IsExternallyVisible(binding)))
                    .Select(binding => binding.Element)
                    .Distinct()
                    .ToList();

                if (candidates.Count == 0)
                {
                    continue;
                }

                var elected = ElectRedefinitionLeaf(scope, candidates);

                return elected == null ? (LayeredOutcomeKind.Ambiguous, null) : (LayeredOutcomeKind.Bound, elected);
            }

            return (LayeredOutcomeKind.NotBound, null);
        }

        internal IEnumerable<INamespace> EnumerateGlobalScopes()
        {
            yield return this.RootNamespace;

            foreach (var globalNamespace in this.globalNamespaces)
            {
                yield return globalNamespace;
            }
        }

        /// <summary>
        /// Narrows <paramref name="candidates" /> to the directly-owned ones when every other candidate is
        /// only inherited into <paramref name="scope" /> — an owned feature shadows a same-named inherited
        /// one even when the redefinition is IMPLIED and absent from the XMI (SysML v2 spec, Clause 7.17.2).
        /// Applied at resolve time so a redefined feature stays nameable from its redefining declaration.
        /// </summary>
        /// <param name="scope">The namespace whose index produced <paramref name="candidates" />.</param>
        /// <param name="candidates">The candidates bound to the name being resolved.</param>
        /// <returns>The owned candidates when the preference applies, otherwise <paramref name="candidates" />.</returns>
        internal static List<IElement> PreferDirectlyOwnedOverInherited(INamespace scope, List<IElement> candidates)
        {
            if (scope is not IType scopeAsType)
            {
                return candidates;
            }

            var supertypes = scopeAsType.AllSupertypes();
            var owned = candidates.Where(candidate => ContainmentPaths.IsDirectlyOwnedBy(scope, candidate)).ToList();

            if (owned.Count == 0 || owned.Count == candidates.Count)
            {
                return candidates;
            }

            var inheritedOnly = candidates
                .Where(candidate => !owned.Contains(candidate))
                .All(candidate => candidate is IFeature { owningType: { } declaringType } && supertypes.Contains(declaringType));

            return inheritedOnly ? owned : candidates;
        }

        /// <summary>
        /// Elects the single redefinition leaf among same-name peers: directly-owned members win over
        /// inherited homonyms, then every feature transitively redefined by another candidate is dropped.
        /// Returns <see langword="null" /> when more than one leaf survives — a reader cannot pick one, so
        /// no spelling relying on this scope is safe.
        /// </summary>
        /// <param name="scope">The scope the candidates were found in.</param>
        /// <param name="candidates">The distinct candidate elements.</param>
        /// <returns>The elected element, or <see langword="null" />.</returns>
        private static IElement ElectRedefinitionLeaf(INamespace scope, List<IElement> candidates)
        {
            if (candidates.Count == 1)
            {
                return candidates[0];
            }

            candidates = PreferDirectlyOwnedOverInherited(scope, candidates);

            if (candidates.Count == 1)
            {
                return candidates[0];
            }

            var shadowed = new HashSet<IFeature>();

            foreach (var candidate in candidates.OfType<IFeature>())
            {
                foreach (var redefined in candidate.AllRedefinedFeatures().Where(redefined => !ReferenceEquals(redefined, candidate)))
                {
                    shadowed.Add(redefined);
                }
            }

            IElement onlyLeaf = null;
            var leafCount = 0;

            foreach (var element in candidates.Where(element => element is not IFeature feature || !shadowed.Contains(feature)))
            {
                leafCount++;

                if (leafCount > 1)
                {
                    return null;
                }

                onlyLeaf = element;
            }

            return leafCount == 1 ? onlyLeaf : null;
        }

        private static bool IsExternallyVisible(ScopeBinding binding)
        {
            return binding.Membership is { Visibility: VisibilityKind.Public }
                   && (binding.Layer != BindingLayer.Imported || binding.ViaPublicImport);
        }
    }
}
