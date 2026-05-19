// -------------------------------------------------------------------------------------------------
// <copyright file="NameResolutionCache.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    /// <summary>
    /// Performant single-place cache for textual-notation qualified-name resolution.
    /// <para>Owns three caches:</para>
    /// <list type="number">
    ///   <item><description>Eager structural: per-namespace simple-name → member set, populated
    ///   by a single walk of the model on construction. Keys are the namespaces reachable
    ///   transitively from the root namespace via containment and imports.</description></item>
    ///   <item><description>Lazy source-scope chain: per-source-POCO upward walk result. The
    ///   chain is materialised on first encounter of the source and cached for subsequent
    ///   references rooted at the same source.</description></item>
    ///   <item><description>Lazy resolved emission: per-<c>(target, sourceLocalScope)</c>
    ///   pair, the final string to emit (bare simple name, escaped unrestricted name, or full
    ///   qualified name). Reused on every subsequent reference that hits the same pair.</description></item>
    /// </list>
    /// <para>Resolution policy mirrors KerML §8.2.3.5 with two short-circuits:</para>
    /// <list type="bullet">
    ///   <item><description><see cref="IMembership"/> targets (import declarations) keep their full
    ///   qualified path — the path identifies WHAT is being imported.</description></item>
    ///   <item><description>Chain accessors (the <c>.X</c> of a feature-chain expression / multi-segment
    ///   feature chain) resolve <c>X</c> against the target's own owning namespace so the bare
    ///   simple name is emitted, since the parser establishes the resolution scope from the
    ///   preceding chain segment's type.</description></item>
    /// </list>
    /// </summary>
    public sealed class NameResolutionCache
    {
        /// <summary>
        /// Shared empty index returned for unknown / null namespaces.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, HashSet<IElement>> EmptyIndex
            = new Dictionary<string, HashSet<IElement>>(StringComparer.Ordinal);

        /// <summary>
        /// Eager structural cache: namespace → (simple-name → member set). Populated once on
        /// construction by <see cref="BuildSimpleNameIndices" />.
        /// </summary>
        private readonly Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>> simpleNameIndices;

        /// <summary>
        /// Lazy upward-walk cache: source-POCO id → containment-chain of <see cref="INamespace" />s
        /// terminated at the root namespace (or wherever the upward walk first hits an empty
        /// <c>owningNamespace</c>). Populated by <see cref="GetSourceScopeChain" />.
        /// </summary>
        private readonly Dictionary<Guid, IReadOnlyList<INamespace>> sourceScopeChains
            = new ();

        /// <summary>
        /// Lazy resolved-emission cache: <c>(target.Id, sourceLocalScope.Id)</c> → emitted string.
        /// </summary>
        private readonly Dictionary<(Guid TargetId, Guid SourceScopeId), string> resolvedReferences
            = new ();

        /// <summary>
        /// Initializes a new <see cref="NameResolutionCache" /> rooted at
        /// <paramref name="rootNamespace" /> and eagerly populates the per-namespace simple-name
        /// index for every namespace reachable from the root.
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace" /> being serialized.</param>
        public NameResolutionCache(INamespace rootNamespace)
        {
            this.RootNamespace = rootNamespace ?? throw new ArgumentNullException(nameof(rootNamespace));
            this.simpleNameIndices = BuildSimpleNameIndices(rootNamespace);
        }

        /// <summary>
        /// Gets the root <see cref="INamespace" /> the cache was rooted at — used as the fallback
        /// local scope when a source POCO has no resolvable enclosing namespace.
        /// </summary>
        public INamespace RootNamespace { get; }

        /// <summary>
        /// Resolves the textual notation for a reference to <paramref name="target" /> at the
        /// site of <paramref name="sourcePoco" />. The result is cached by
        /// <c>(target.Id, sourceLocalScope.Id)</c> — repeat calls with the same pair are O(1)
        /// dictionary lookups.
        /// </summary>
        /// <param name="target">The referenced <see cref="IElement" />; may be <see langword="null" />.</param>
        /// <param name="sourcePoco">The POCO at whose syntactic position the reference appears.</param>
        /// <returns>The string to emit; empty when <paramref name="target" /> is <see langword="null" />.</returns>
        public string Resolve(IElement target, IElement sourcePoco)
        {
            switch (target)
            {
                case null:
                    return string.Empty;

                // Short-circuit 1 — IMembership targets: import declarations keep their full path.
                case IMembership membership:
                    return membership.MemberElement?.qualifiedName ?? string.Empty;
            }

            // Short-circuit 2 — no usable simple name on the target: emit the qualified name.
            var escapedName = target.EscapedName();

            if (string.IsNullOrWhiteSpace(escapedName))
            {
                return target.qualifiedName ?? string.Empty;
            }

            // Short-circuit 3 — chain accessor: the parser establishes the resolution scope
            // from the preceding chain segment's type at parse time, so the simple name is
            // sufficient. We do NOT cache this because the chain accessor's resolution is a
            // function of (target, sourcePoco) — and sourcePoco changes per reference site.
            // The work is cheap (one type test + EscapedName already computed above).
            if (IsChainAccessor(sourcePoco))
            {
                return ResolveChainAccessor(target, escapedName);
            }

            // Memoised path — look up by (target.Id, sourceLocalScope.Id).
            var sourceLocalScope = this.GetSourceLocalScope(sourcePoco);
            var cacheKey = (target.Id, sourceLocalScope?.Id ?? Guid.Empty);

            if (this.resolvedReferences.TryGetValue(cacheKey, out var cached))
            {
                return cached;
            }

            var resolved = this.ResolveFresh(target, sourcePoco, sourceLocalScope, escapedName);
            this.resolvedReferences[cacheKey] = resolved;
            return resolved;
        }

        /// <summary>
        /// First-time resolution: walks the cached source-scope chain and probes each scope's
        /// simple-name index. Falls back to <see cref="IElement.qualifiedName" />.
        /// </summary>
        /// <param name="target">The referenced element.</param>
        /// <param name="sourcePoco">The reference site's source POCO.</param>
        /// <param name="sourceLocalScope">The previously-computed local scope (may be <see langword="null" />).</param>
        /// <param name="escapedName">The target's escaped raw <c>name</c>.</param>
        /// <returns>The resolved emission string.</returns>
        private string ResolveFresh(IElement target, IElement sourcePoco, INamespace sourceLocalScope, string escapedName)
        {
            var rawName = target.name;
            var rawShortName = target.shortName;

            var escapedShortName = string.IsNullOrWhiteSpace(rawShortName)
                ? null
                : (rawShortName.QueryIsValidBasicName() ? rawShortName : rawShortName.ToUnrestrictedName());

            // Walk the cached source-scope chain. First hit wins.
            foreach (var scope in this.GetSourceScopeChain(sourcePoco, sourceLocalScope))
            {
                if (this.TryResolveSimpleNameInScope(scope, target, rawName, escapedName, rawShortName, escapedShortName, out var matched))
                {
                    return matched;
                }
            }

            // Fall back to the fully-qualified name.
            return target.qualifiedName ?? string.Empty;
        }

        /// <summary>
        /// Returns the resolved simple name for a chain accessor: tries the target's own
        /// <c>name</c> first, then its <c>shortName</c>. Both go through the same KEBNF
        /// basic-name / unrestricted-name escape as a non-chain reference.
        /// </summary>
        /// <param name="target">The chain-accessor target element.</param>
        /// <param name="escapedName">The pre-computed escaped <c>name</c> form (long-form preference).</param>
        /// <returns>The escaped simple name, or <c>target.qualifiedName</c> as a last resort.</returns>
        private static string ResolveChainAccessor(IElement target, string escapedName)
        {
            if (!string.IsNullOrWhiteSpace(target.name))
            {
                return escapedName;
            }

            var rawShortName = target.shortName;

            if (!string.IsNullOrWhiteSpace(rawShortName))
            {
                return rawShortName.QueryIsValidBasicName() ? rawShortName : rawShortName.ToUnrestrictedName();
            }

            return target.qualifiedName ?? string.Empty;
        }

        /// <summary>
        /// Returns the per-scope simple-name index that was built eagerly on construction.
        /// Returns <see cref="EmptyIndex" /> for namespaces the eager pass did not reach.
        /// </summary>
        /// <param name="scope">The <see cref="INamespace" /> whose index is requested.</param>
        /// <returns>The simple-name → member-set lookup.</returns>
        private IReadOnlyDictionary<string, HashSet<IElement>> GetSimpleNameIndex(INamespace scope)
        {
            return scope == null ? EmptyIndex : this.simpleNameIndices.GetValueOrDefault(scope, EmptyIndex);
        }

        /// <summary>
        /// Returns the cached upward-walk chain for <paramref name="sourcePoco" />, building it
        /// on first encounter. The chain starts at <paramref name="sourceLocalScope" /> (the
        /// reference's local scope) and walks <c>owningNamespace</c> up to the root.
        /// </summary>
        /// <param name="sourcePoco">The source POCO bearing the reference; may be <see langword="null" />.</param>
        /// <param name="sourceLocalScope">The pre-computed local scope (may be <see langword="null" />).</param>
        /// <returns>The cached chain.</returns>
        private IReadOnlyList<INamespace> GetSourceScopeChain(IElement sourcePoco, INamespace sourceLocalScope)
        {
            if (sourcePoco == null)
            {
                return BuildChain(sourceLocalScope ?? this.RootNamespace);
            }

            if (this.sourceScopeChains.TryGetValue(sourcePoco.Id, out var cached))
            {
                return cached;
            }

            var chain = BuildChain(sourceLocalScope ?? this.RootNamespace);
            this.sourceScopeChains[sourcePoco.Id] = chain;

            return chain;
        }

        /// <summary>
        /// Walks <paramref name="start" /> up via <c>owningNamespace</c> and materialises the
        /// chain. Stops at the first <c>NotSupportedException</c> or null.
        /// </summary>
        /// <param name="start">The starting namespace.</param>
        /// <returns>The chain.</returns>
        private static IReadOnlyList<INamespace> BuildChain(INamespace start)
        {
            if (start == null)
            {
                return [];
            }

            var chain = new List<INamespace>();
            var current = start;

            while (current != null)
            {
                chain.Add(current);

                INamespace next;

                try
                {
                    next = current.owningNamespace;
                }
                catch (NotSupportedException)
                {
                    break;
                }

                current = next;
            }

            return chain;
        }

        /// <summary>
        /// Resolves the local scope of <paramref name="sourcePoco" />: the first
        /// <see cref="INamespace" /> reached by climbing
        /// <see cref="IRelationship.OwningRelatedElement" />, then
        /// <see cref="IElement.owningNamespace" />, then <see cref="IElement.owner" />. Falls
        /// back to <see cref="RootNamespace" /> when nothing is reachable.
        /// </summary>
        /// <param name="sourcePoco">The source POCO; may be <see langword="null" />.</param>
        /// <returns>The local scope or <see cref="RootNamespace" />.</returns>
        private INamespace GetSourceLocalScope(IElement sourcePoco)
        {
            if (sourcePoco == null)
            {
                return this.RootNamespace;
            }

            var visited = new HashSet<IElement>();
            var current = sourcePoco;

            while (current != null && visited.Add(current))
            {
                switch (current)
                {
                    case INamespace asNamespace:
                        return asNamespace;
                    case IRelationship { OwningRelatedElement: not null } relationship:
                        current = relationship.OwningRelatedElement;
                        continue;
                }

                INamespace owningNs = null;

                try
                {
                    owningNs = current.owningNamespace;
                }
                catch (NotSupportedException)
                {
                    // owningNamespace not implemented — fall through to owner walk.
                }

                if (owningNs != null)
                {
                    return owningNs;
                }

                IElement nextOwner;

                try
                {
                    nextOwner = current.owner;
                }
                catch (NotSupportedException)
                {
                    nextOwner = null;
                }

                current = nextOwner;
            }

            return this.RootNamespace;
        }

        /// <summary>
        /// Tests the simple-name index of <paramref name="scope" /> for a hit on
        /// <paramref name="target" />. Tries the raw <c>name</c> key first, then the raw
        /// <c>shortName</c> key. On a hit, <paramref name="matchedSimpleName" /> receives the
        /// corresponding escaped form.
        /// </summary>
        /// <param name="scope">The scope whose index is inspected.</param>
        /// <param name="target">The element to look up.</param>
        /// <param name="rawName">The target's raw <c>name</c>; may be <see langword="null" /> / whitespace.</param>
        /// <param name="escapedName">The escaped form emitted on a long-form hit.</param>
        /// <param name="rawShortName">The target's raw <c>shortName</c>; may be <see langword="null" /> / whitespace.</param>
        /// <param name="escapedShortName">The escaped form emitted on a short-form hit.</param>
        /// <param name="matchedSimpleName">On <see langword="true" />, the simple name to emit.</param>
        /// <returns><see langword="true" /> when a hit was found.</returns>
        private bool TryResolveSimpleNameInScope(INamespace scope, IElement target, string rawName, string escapedName, string rawShortName, string escapedShortName, out string matchedSimpleName)
        {
            var index = this.GetSimpleNameIndex(scope);

            if (!string.IsNullOrWhiteSpace(rawName)
                && index.TryGetValue(rawName, out var elements)
                && elements.Contains(target))
            {
                matchedSimpleName = escapedName;
                return true;
            }

            if (!string.IsNullOrWhiteSpace(rawShortName)
                && index.TryGetValue(rawShortName, out var shortElements)
                && shortElements.Contains(target))
            {
                matchedSimpleName = escapedShortName;
                return true;
            }

            matchedSimpleName = null;
            return false;
        }

        /// <summary>
        /// Determines whether <paramref name="sourcePoco" /> is the right-hand side of a chain
        /// accessor — see grammar rules <c>FeatureChainExpression</c> (KerML §8.2.4.X) and
        /// <c>FeatureChain</c> (KerML §8.2.4.3.5). Two patterns match:
        /// <list type="bullet">
        ///   <item><description>An <see cref="IMembership" /> sitting as the chain-accessor RHS
        ///   of a <see cref="IFeatureChainExpression" />.</description></item>
        ///   <item><description>An <see cref="IFeatureChaining" /> at any index after the FIRST
        ///   in its container's <c>OwnedRelationship</c> list.</description></item>
        /// </list>
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns><see langword="true" /> when the source is a chain accessor.</returns>
        private static bool IsChainAccessor(IElement sourcePoco)
        {
            if (sourcePoco is IMembership { OwningRelatedElement: IFeatureChainExpression } and not IParameterMembership)
            {
                return true;
            }

            if (sourcePoco is not IFeatureChaining { OwningRelatedElement: IFeature chainOwner } chaining)
            {
                return false;
            }

            var siblings = chainOwner.OwnedRelationship.OfType<IFeatureChaining>().ToList();
            var index = siblings.IndexOf(chaining);
            return index > 0;
        }

        /// <summary>
        /// Eagerly walks the model rooted at <paramref name="rootNamespace" /> and builds the
        /// simple-name index for every reachable <see cref="INamespace" />. Reachability follows
        /// <see cref="INamespace.ownedMembership" />.<see cref="IMembership.MemberElement" /> for
        /// nested namespaces, plus direct <c>ownedImport</c> targets (so imported namespaces are
        /// also indexed once).
        /// </summary>
        /// <param name="rootNamespace">The root namespace.</param>
        /// <returns>The full structural cache.</returns>
        private static Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>> BuildSimpleNameIndices(INamespace rootNamespace)
        {
            var result = new Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>>();
            var pending = new Queue<INamespace>();
            var visited = new HashSet<INamespace>();

            pending.Enqueue(rootNamespace);

            while (pending.Count != 0)
            {
                var scope = pending.Dequeue();

                if (scope == null || !visited.Add(scope))
                {
                    continue;
                }

                var index = new Dictionary<string, HashSet<IElement>>(StringComparer.Ordinal);

                BuildOwnedAndImportedEntries(scope, index, pending);

                if (scope is IType type)
                {
                    BuildInheritedEntries(type, index, pending);
                }

                result[scope] = index;
            }

            return result;
        }

        /// <summary>
        /// Populates <paramref name="index" /> with entries for <paramref name="scope" />'s
        /// owned memberships and direct imports. Imported namespaces are enqueued onto
        /// <paramref name="pending" /> so they too are indexed in the eager pass.
        /// </summary>
        /// <param name="scope">The namespace whose entries are populated.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        private static void BuildOwnedAndImportedEntries(INamespace scope, Dictionary<string, HashSet<IElement>> index, Queue<INamespace> pending)
        {
            try
            {
                foreach (var ownedMember in scope.ownedMembership)
                {
                    AddMembershipEntry(index, ownedMember, pending);
                }
            }
            catch (NotSupportedException)
            {
                // ownedMembership may not be implemented; skip.
            }

            try
            {
                foreach (var ownedImport in scope.ownedImport)
                {
                    switch (ownedImport)
                    {
                        case IMembershipImport { ImportedMembership: { } importedMembership }:
                            AddMembershipEntry(index, importedMembership, pending);
                            break;
                        case INamespaceImport { ImportedNamespace: not null } namespaceImport:
                        {
                            pending.Enqueue(namespaceImport.ImportedNamespace);

                            // Isolate the inner ownedMembership walk in its own try/catch so a
                            // NotSupportedException from one imported namespace does not abort
                            // the outer ownedImport loop and lose every remaining import.
                            try
                            {
                                foreach (var importedMember in namespaceImport.ImportedNamespace.ownedMembership)
                                {
                                    AddMembershipEntry(index, importedMember, pending);
                                }
                            }
                            catch (NotSupportedException)
                            {
                                // ownedMembership not implemented for this imported namespace; skip it.
                            }

                            break;
                        }
                    }
                }
            }
            catch (NotSupportedException)
            {
                // ownedImport may not be implemented; skip.
            }
        }

        /// <summary>
        /// Indexes the entries inherited from the transitive supertypes of
        /// <paramref name="type" />. Bypasses the <c>RemoveRedefinedFeatures</c> filter so
        /// references such as <c>:&gt;&gt; elements</c> remain reachable.
        /// </summary>
        /// <param name="type">The type whose inherited memberships are indexed.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed (so supertypes that are
        /// also namespaces get their own index built).</param>
        private static void BuildInheritedEntries(IType type, Dictionary<string, HashSet<IElement>> index, Queue<INamespace> pending)
        {
            List<IType> supertypes;

            try
            {
                supertypes = type.AllSupertypes();
            }
            catch (NotSupportedException)
            {
                return;
            }

            foreach (var supertype in supertypes.Where(supertype => supertype != null && !ReferenceEquals(supertype, type)))
            {
                if (supertype is INamespace supertypeAsNamespace)
                {
                    pending.Enqueue(supertypeAsNamespace);
                }

                try
                {
                    foreach (var ownedMember in supertype.ownedMembership)
                    {
                        AddMembershipEntry(index, ownedMember, pending);
                    }
                }
                catch (NotSupportedException)
                {
                    // ownedMembership not implemented for this supertype; skip.
                }
            }
        }

        /// <summary>
        /// Adds the <see cref="IMembership.MemberElement" /> of <paramref name="membership" />
        /// to <paramref name="index" /> under both its <see cref="IMembership.MemberShortName" />
        /// and <see cref="IMembership.MemberName" />, and enqueues the target onto
        /// <paramref name="pending" /> if it is itself an <see cref="INamespace" /> (so nested
        /// namespaces are indexed too).
        /// </summary>
        /// <param name="index">The destination index.</param>
        /// <param name="membership">The membership whose target is indexed.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        private static void AddMembershipEntry(Dictionary<string, HashSet<IElement>> index, IMembership membership, Queue<INamespace> pending)
        {
            var target = membership?.MemberElement;

            if (target == null)
            {
                return;
            }

            AddIndexEntry(index, membership.MemberShortName, target);
            AddIndexEntry(index, membership.MemberName, target);

            if (target is INamespace targetAsNamespace)
            {
                pending.Enqueue(targetAsNamespace);
            }
        }

        /// <summary>
        /// Adds <paramref name="element" /> to <paramref name="index" /> under
        /// <paramref name="simpleName" /> when the name is non-blank.
        /// </summary>
        /// <param name="index">The destination index.</param>
        /// <param name="simpleName">The simple name to use as the index key.</param>
        /// <param name="element">The element to record under <paramref name="simpleName" />.</param>
        private static void AddIndexEntry(Dictionary<string, HashSet<IElement>> index, string simpleName, IElement element)
        {
            if (string.IsNullOrWhiteSpace(simpleName) || element == null)
            {
                return;
            }

            if (!index.TryGetValue(simpleName, out var bucket))
            {
                bucket = [];
                index[simpleName] = bucket;
            }

            bucket.Add(element);
        }
    }
}
