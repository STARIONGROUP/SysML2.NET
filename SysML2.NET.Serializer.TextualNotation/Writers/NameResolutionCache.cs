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
    using System.Text;

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

                // Short-circuit 1 — IMembership targets: import declarations keep the full path,
                // but use the SHORTEST declared name available at each owner-chain segment.
                // IElement.qualifiedName walks via EscapedName() which prefers `name` over
                // `shortName`; that is the inverse of what import declarations need. The SysML
                // Textual Notation tutorial and the pilot implementation consistently emit
                // imports using shortNames where declared (e.g. `import SI::kg` not
                // `import SI::kilogram`).
                case IMembership membership:
                    return membership.MemberElement != null
                        ? QueryShortQualifiedName(membership.MemberElement)
                        : string.Empty;
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
        /// First-time resolution: emits the SHORTEST unambiguous qualified-name suffix for
        /// <paramref name="target" /> at <paramref name="sourcePoco" />'s reference site.
        /// Walks the target's owner chain outward (innermost ancestor first) and, at each
        /// step, tries to resolve the anchor's simple name uniquely in the source-scope
        /// chain. The first anchor that resolves uniquely produces the emission anchor; the
        /// suffix from the anchor down to <paramref name="target" /> is then appended via
        /// <c>"::"</c>. Falls back to <see cref="IElement.qualifiedName" /> when no anchor in
        /// the owner chain resolves.
        /// <para>
        /// For the bare <paramref name="target" /> itself (anchor depth 0), the two lexical
        /// forms (<c>shortName</c> and <c>name</c>) are tried in turn — short first per the
        /// SST tutorial / pilot convention. Inner ancestors are tried using whichever single
        /// lexical form is declared on them.
        /// </para>
        /// </summary>
        /// <param name="target">The referenced element.</param>
        /// <param name="sourcePoco">The reference site's source POCO.</param>
        /// <param name="sourceLocalScope">The previously-computed local scope (may be <see langword="null" />).</param>
        /// <param name="escapedName">The target's escaped raw <c>name</c>.</param>
        /// <returns>The resolved emission string.</returns>
        private string ResolveFresh(IElement target, IElement sourcePoco, INamespace sourceLocalScope, string escapedName)
        {
            var chain = this.GetSourceScopeChain(sourcePoco, sourceLocalScope);

            // Depth 0 — try the target's own simple names. shortName first (the SST tutorial
            // / pilot reference output consistently uses short forms for quantity literals
            // like `[kg]` over `[kilogram]`), then long.
            var rawShortName = target.shortName;
            string escapedShortName = null;

            if (!string.IsNullOrWhiteSpace(rawShortName))
            {
                escapedShortName = rawShortName.QueryIsValidBasicName() ? rawShortName : rawShortName.ToUnrestrictedName();

                if (this.TryResolveSimpleNameAcrossChain(chain, target, rawShortName, escapedShortName, out var matchedShort))
                {
                    return matchedShort;
                }
            }

            var rawName = target.name;

            if (!string.IsNullOrWhiteSpace(rawName)
                && this.TryResolveSimpleNameAcrossChain(chain, target, rawName, escapedName, out var matchedLong))
            {
                return matchedLong;
            }

            // Depth ≥ 1 — walk owner-chain ancestors outward and look for the first one that
            // itself resolves uniquely in the source-scope chain. Once found, emit it as the
            // anchor followed by the owner-chain segments down to the target.
            var segmentsDownToTarget = new Stack<string>();

            segmentsDownToTarget.Push(QueryPreferredEscapedSegment(target) ?? string.Empty);

            var ancestor = QueryOwningContainer(target);
            var visitedAncestors = new HashSet<IElement>();

            while (ancestor != null && visitedAncestors.Add(ancestor))
            {
                var ancestorSegment = QueryPreferredEscapedSegment(ancestor);

                if (string.IsNullOrWhiteSpace(ancestorSegment))
                {
                    // An unnamed namespace in the owner chain cannot appear inside a
                    // QualifiedName — emitting `<anchor>::<down-segments>` while skipping the
                    // unnamed gap would produce an unparseable result. Stop the walk and let
                    // the fallback (target.qualifiedName) take over.
                    break;
                }

                var rawAncestorShort = ancestor.shortName;
                var rawAncestorLong = ancestor.name;

                var ancestorRawName = !string.IsNullOrWhiteSpace(rawAncestorShort) ? rawAncestorShort : rawAncestorLong;

                if (!string.IsNullOrWhiteSpace(ancestorRawName)
                    && this.TryResolveSimpleNameAcrossChain(chain, ancestor, ancestorRawName, ancestorSegment, out var matchedAnchor))
                {
                    var builder = new StringBuilder(matchedAnchor);

                    foreach (var segment in segmentsDownToTarget)
                    {
                        builder.Append("::");
                        builder.Append(segment);
                    }

                    return builder.ToString();
                }

                segmentsDownToTarget.Push(ancestorSegment);

                ancestor = QueryOwningContainer(ancestor);
            }

            // Fall back to the fully-qualified name.
            return target.qualifiedName ?? string.Empty;
        }

        /// <summary>
        /// Returns <paramref name="element" />'s <c>owningNamespace</c> when reachable,
        /// otherwise <see langword="null" />. Owner-chain traversal stops on
        /// <see cref="NotSupportedException" /> from unimplemented derived properties — the
        /// same convention used by <see cref="BuildChain" />.
        /// </summary>
        /// <param name="element">The element whose owner is requested; may be <see langword="null" />.</param>
        /// <returns>The owning namespace or <see langword="null" />.</returns>
        private static IElement QueryOwningContainer(IElement element)
        {
            if (element == null)
            {
                return null;
            }

            try
            {
                return element.owningNamespace;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns <paramref name="element" />'s shortest escaped name segment — preferring
        /// <see cref="IElement.shortName" /> over <see cref="IElement.name" />, with KEBNF
        /// unrestricted-name escaping when the chosen segment is not a basic name. Returns
        /// <see langword="null" /> when neither form is available.
        /// </summary>
        /// <param name="element">The element to name; must be non-null.</param>
        /// <returns>The escaped segment, or <see langword="null" />.</returns>
        private static string QueryPreferredEscapedSegment(IElement element)
        {
            var preferred = !string.IsNullOrWhiteSpace(element.shortName)
                ? element.shortName
                : element.name;

            if (string.IsNullOrWhiteSpace(preferred))
            {
                return null;
            }

            return preferred.QueryIsValidBasicName() ? preferred : preferred.ToUnrestrictedName();
        }

        /// <summary>
        /// Walks <paramref name="chain" /> from innermost to outermost looking for a scope
        /// whose simple-name index binds <paramref name="rawName" /> uniquely to
        /// <paramref name="target" />. Stops the walk on the first scope that binds the name
        /// to anything else — the parser's resolution would already have claimed the name in
        /// that scope, so outer scopes are unreachable.
        /// </summary>
        /// <param name="chain">The pre-built source-scope chain (innermost first).</param>
        /// <param name="target">The referenced element.</param>
        /// <param name="rawName">The simple-name lexical form to probe (may be <see langword="null" /> / whitespace).</param>
        /// <param name="escapedName">The escaped form to emit on a hit.</param>
        /// <param name="matched">On a unique-binding hit, the simple-name string to emit.</param>
        /// <returns><see langword="true" /> when the simple name resolves uniquely to the target somewhere in the chain.</returns>
        private bool TryResolveSimpleNameAcrossChain(IReadOnlyList<INamespace> chain, IElement target, string rawName, string escapedName, out string matched)
        {
            matched = null;

            if (string.IsNullOrWhiteSpace(rawName))
            {
                return false;
            }

            foreach (var scope in chain)
            {
                var resolution = this.ResolveSimpleNameInScope(scope, target, rawName);

                switch (resolution)
                {
                    case SimpleNameResolution.Matched:
                        matched = escapedName;
                        return true;
                    case SimpleNameResolution.Shadowed:
                        return false;
                }
            }

            return false;
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
        /// Walks <paramref name="element" /> up via <c>owningNamespace</c> and builds the
        /// qualified name using the SHORTEST declared name at each segment — the
        /// <see cref="IElement.shortName" /> when non-blank, otherwise the
        /// <see cref="IElement.name" />. Each segment is escaped through the KEBNF
        /// unrestricted-name rules (<c>'…'</c> quoting for non-basic identifiers) so the
        /// result is parser-roundtrip safe.
        /// <para>
        /// Used by the <see cref="IMembership" /> short-circuit in <see cref="Resolve" /> for
        /// import declarations, where the pilot implementation's reference text uses short
        /// forms (e.g. <c>SI::kg</c>) but <see cref="IElement.qualifiedName" /> returns the
        /// long form (<c>SI::kilogram</c>) because it goes through <c>EscapedName()</c> which
        /// prefers <c>name</c> over <c>shortName</c>.
        /// </para>
        /// <para>Mirrors the cycle-and-null-safety pattern of <see cref="BuildChain" />: stops
        /// on <see langword="null" /> <c>owningNamespace</c> and swallows
        /// <see cref="NotSupportedException" /> from unimplemented derived properties.</para>
        /// </summary>
        /// <param name="element">The leaf <see cref="IElement" /> to qualify; must be non-null.</param>
        /// <returns>The short-form qualified name (e.g. <c>"SI::kg"</c>), or the empty string
        /// when no segment carries a usable name.</returns>
        private static string QueryShortQualifiedName(IElement element)
        {
            var segments = new Stack<string>();
            var current = element;

            while (current != null)
            {
                var preferred = !string.IsNullOrWhiteSpace(current.shortName)
                    ? current.shortName
                    : current.name;

                if (string.IsNullOrWhiteSpace(preferred))
                {
                    break;
                }

                var escaped = preferred.QueryIsValidBasicName()
                    ? preferred
                    : preferred.ToUnrestrictedName();

                segments.Push(escaped);

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

            return string.Join("::", segments);
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
                if (current is IRelationship { OwningRelatedElement: not null } relationship)
                {
                    current = relationship.OwningRelatedElement;
                    continue;
                }

                if (current is INamespace asNamespace)
                {
                    // A Namespace is the local scope only when it has a proper upward
                    // owningNamespace chain. Anonymous nested namespaces (e.g. an
                    // OwnedFeatureChain Feature owned via Specialization rather than
                    // Membership) have a null `owningNamespace`; returning such a namespace
                    // here gives BuildChain a one-element chain that never reaches the
                    // reference site's enclosing scope, so name resolution falls through
                    // to qualifiedName. In that case keep walking via `owner` (which follows
                    // the owningRelationship → OwningRelatedElement path) to find the
                    // enclosing reference-site namespace.
                    INamespace asNamespaceUpward = null;

                    try
                    {
                        asNamespaceUpward = asNamespace.owningNamespace;
                    }
                    catch (NotSupportedException)
                    {
                        // owningNamespace not implemented — treat as no upward chain.
                    }

                    if (asNamespaceUpward != null || ReferenceEquals(asNamespace, this.RootNamespace))
                    {
                        return asNamespace;
                    }

                    IElement asNamespaceOwner;

                    try
                    {
                        asNamespaceOwner = asNamespace.owner;
                    }
                    catch (NotSupportedException)
                    {
                        asNamespaceOwner = null;
                    }

                    if (asNamespaceOwner == null)
                    {
                        return asNamespace;
                    }

                    current = asNamespaceOwner;
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
        /// Tri-state result of probing a single scope's simple-name index for a single lexical
        /// form. Drives the <see cref="TryResolveSimpleNameAcrossChain" /> walk:
        /// <see cref="Matched" /> stops the walk with a hit; <see cref="Shadowed" /> stops the
        /// walk without a hit (outer scopes are unreachable — the parser's resolution per
        /// KerML §8.2.3.5 stops at the first scope binding the name, and that scope's binding
        /// is not uniquely the target); <see cref="NotBound" /> continues the walk.
        /// </summary>
        private enum SimpleNameResolution
        {
            /// <summary>The simple name is not present in this scope's index — keep walking.</summary>
            NotBound,

            /// <summary>The simple name is bound uniquely to the target in this scope — emit the simple name.</summary>
            Matched,

            /// <summary>The simple name is bound at this scope but not uniquely to the target — fall back to the qualified name.</summary>
            Shadowed,
        }

        /// <summary>
        /// Returns the resolution state for <paramref name="rawName" /> in
        /// <paramref name="scope" />'s simple-name index. The index intentionally indexes BOTH
        /// the leaf inherited member AND its redefined ancestors (so a <c>:&gt; ancestor</c>
        /// reference can still reach them); the parser however applies the
        /// <c>RemoveRedefinedFeatures</c> filter (KerML §8.2.3.5.3 — "in a well-formed
        /// Namespace, there is at most one Membership for any given name") so its local
        /// resolution sees only the LEAF (the most-derived feature in the redefinition
        /// chain). We mirror that filter here: an index entry is reduced to its leaves
        /// (elements not transitively redefined by any other element in the entry), and the
        /// simple name is emitted only when that leaf set is exactly <c>{target}</c>.
        /// </summary>
        /// <param name="scope">The scope whose index is inspected.</param>
        /// <param name="target">The element to look up.</param>
        /// <param name="rawName">The simple-name lexical form to probe; must be non-blank.</param>
        /// <returns>The resolution state.</returns>
        private SimpleNameResolution ResolveSimpleNameInScope(INamespace scope, IElement target, string rawName)
        {
            var index = this.GetSimpleNameIndex(scope);

            if (!index.TryGetValue(rawName, out var elements))
            {
                return SimpleNameResolution.NotBound;
            }

            if (elements.Count == 1)
            {
                return elements.Contains(target)
                    ? SimpleNameResolution.Matched
                    : SimpleNameResolution.Shadowed;
            }

            // Reduce to the leaf set: drop any element that is transitively redefined by
            // another element in `elements`. The shadow set is the union of each candidate's
            // `AllRedefinedFeatures()` closure (excluding the candidate itself, which the
            // operation includes as the seed of the closure).
            var shadowed = new HashSet<IFeature>();

            foreach (var candidate in elements.OfType<IFeature>())
            {
                foreach (var redefined in candidate.AllRedefinedFeatures().Where(redefined => !ReferenceEquals(redefined, candidate)))
                {
                    shadowed.Add(redefined);
                }
            }

            IElement onlyLeaf = null;
            var leafCount = 0;

            foreach (var element in elements.Where(element => element is not IFeature feature || !shadowed.Contains(feature)))
            {
                leafCount++;

                if (leafCount > 1)
                {
                    return SimpleNameResolution.Shadowed;
                }

                onlyLeaf = element;
            }

            return leafCount == 1 && ReferenceEquals(onlyLeaf, target)
                ? SimpleNameResolution.Matched
                : SimpleNameResolution.Shadowed;
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

            foreach (var supertype in supertypes.Where(s => s != null && !ReferenceEquals(s, type)))
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
        /// to <paramref name="index" /> under both its short and long name and enqueues the
        /// target onto <paramref name="pending" /> if it is itself an <see cref="INamespace" />
        /// (so nested namespaces are indexed too).
        /// <para>
        /// Per the metamodel, <see cref="IMembership.MemberShortName" /> and
        /// <see cref="IMembership.MemberName" /> are explicit overrides of the member element's
        /// declared names within the owning namespace. When the membership does not carry an
        /// override (the pilot implementation's XMI never emits <c>memberName</c> /
        /// <c>memberShortName</c> on Membership elements), fall back to the target's own
        /// <see cref="IElement.shortName" /> / <see cref="IElement.name" /> so the simple-name
        /// index remains reachable by simple name (e.g. <c>kg</c>, <c>kilogram</c>) for
        /// references to imported library elements.
        /// </para>
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

            var shortName = !string.IsNullOrWhiteSpace(membership.MemberShortName)
                ? membership.MemberShortName
                : target.shortName;
            var longName = !string.IsNullOrWhiteSpace(membership.MemberName)
                ? membership.MemberName
                : target.name;

            AddIndexEntry(index, shortName, target);
            AddIndexEntry(index, longName, target);

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
