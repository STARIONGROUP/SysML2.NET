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
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.Root.Namespaces;
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
        /// Reverse index from a canonical-owning <see cref="INamespace" /> to the set of
        /// "facade" namespaces that DIRECTLY re-export it via a <see cref="INamespaceImport" />.
        /// Populated during the eager <see cref="BuildSimpleNameIndices"/> pass alongside the
        /// per-scope simple-name indices. Single-hop only — no transitive walk.
        /// <para>Used by <see cref="ResolveFresh"/> to shorten library references like
        /// <c>ISQBase::mass</c> to the OMG SST idiomatic facade form <c>ISQ::mass</c>: when a
        /// reference targets an element owned by <c>ISQBase</c>, and a namespace <c>ISQ</c>
        /// directly imports <c>ISQBase</c>, AND <c>ISQ</c> is reachable from the source scope
        /// chain, the writer emits <c>ISQ::simpleName</c> instead of <c>ISQBase::simpleName</c>.
        /// The SST tutorial (Release 2026-03) uses the facade form 17:1 over the implementation
        /// form (<c>ISQ::</c> vs <c>ISQBase::</c>), establishing the canonical idiom.</para>
        /// </summary>
        private readonly Dictionary<INamespace, HashSet<INamespace>> directFacadeIndex = new();

        /// <summary>
        /// Reverse index of ALIAS bindings: scope → (aliased element → the alias names declared for it
        /// in that scope). Populated during the eager <see cref="BuildSimpleNameIndices"/> pass from every
        /// <see cref="IMembership"/> that carries an explicit <see cref="IMembership.MemberName"/> /
        /// <see cref="IMembership.MemberShortName"/> override differing from the target's own names — i.e.
        /// an <c>alias X for Y;</c> declaration.
        /// <para>The forward index already maps <c>"Torque" → TorqueValue</c>, but
        /// <see cref="ResolveFresh"/> probes only the TARGET's own lexical forms, so an alias could never
        /// be found. This reverse map lets a reference to <c>TorqueValue</c> emit the in-scope alias
        /// <c>Torque</c> instead of the qualified <c>ISQMechanics::TorqueValue</c>, matching how the model
        /// was written.</para>
        /// </summary>
        private readonly Dictionary<INamespace, Dictionary<IElement, List<string>>> aliasIndex = new();

        /// <summary>
        /// The root <see cref="INamespace" />s forming the global <see cref="INamespace" /> (KerML 1.0
        /// §8.2.3.5.2), excluding <see cref="RootNamespace" /> itself. Indexed like any other scope, but
        /// only their VISIBLE memberships are admitted — see <see cref="BuildSimpleNameIndices" />.
        /// </summary>
        private readonly List<INamespace> globalNamespaces;

        /// <summary>
        /// Initializes a new <see cref="NameResolutionCache" /> rooted at
        /// <paramref name="rootNamespace" /> and eagerly populates the per-namespace simple-name
        /// index for every namespace reachable from the root.
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace" /> being serialized.</param>
        /// <param name="globalNamespaces">
        /// The other root <see cref="INamespace" />s available to <paramref name="rootNamespace" /> — the
        /// model libraries and any other loaded resource. Per KerML 1.0 §8.2.3.5.2 a root
        /// <see cref="INamespace" /> has an implicit containing <i>global</i> <see cref="INamespace" />
        /// that "includes all the visible Memberships of all other root Namespaces that are available to
        /// the first Namespace", and §8.2.3.5.4 makes resolution in that scope the final step once the
        /// containment chain is exhausted. Supplying them lets the writer emit a name that resolves through
        /// a library root which the model does not itself import (e.g. <c>ISQ::TorqueValue</c> for an
        /// element owned by <c>ISQMechanics</c> and publicly re-exported by <c>ISQ</c>).
        /// <para>Optional: when <see langword="null" /> or empty, resolution is limited to the containment
        /// and import graph of <paramref name="rootNamespace" />, which is always safe — it can only yield
        /// a longer, equally valid name. Obtain the roots from
        /// <c>IDeSerializer.QueryRootNamespaces()</c>.</para>
        /// </summary>
        public NameResolutionCache(INamespace rootNamespace, IEnumerable<INamespace> globalNamespaces = null)
        {
            this.RootNamespace = rootNamespace ?? throw new ArgumentNullException(nameof(rootNamespace));

            this.globalNamespaces = globalNamespaces?
                .Where(candidate => candidate != null && !ReferenceEquals(candidate, rootNamespace))
                .Distinct()
                .ToList() ?? [];

            this.simpleNameIndices = this.BuildSimpleNameIndices(rootNamespace);
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

            // Redefinition-context: when the source is an OwnedRedefinition and the target is its
            // RedefinedFeature, the LOCAL redefining feature must not shadow the redefined target
            // during simple-name lookup. The parser resolves `:>> name` against the type's
            // INHERITED members (the redefining feature isn't a member of the type yet — it's the
            // very feature being defined), so the writer mirrors that by filtering the local
            // redefiner out of every candidate bucket. Bypasses the memo because the redefinition
            // context is per-call, not per (target, sourceLocalScope).
            // <para>EXCEPTION: when the local redefining feature has a DECLARED name equal to the
            // target's name, emitting the bare simple-name form would re-resolve at parse time to
            // the local redefiner itself (the post-parse local member shadows the inherited one),
            // not to the redefined target. KerML §8.2.3.5 requires the qualified form in that
            // case so the round-trip resolves to the SAME element. We detect this collision and
            // fall through to the normal cached path, which produces the qualified form.</para>
            if (sourcePoco is IRedefinition redefinition && ReferenceEquals(target, redefinition.RedefinedFeature))
            {
                var localRedefiner = redefinition.RedefiningFeature;

                if (localRedefiner != null && !RedefinerDeclaredNameCollidesWith(localRedefiner, target))
                {
                    return this.ResolveFresh(target, sourcePoco, sourceLocalScope, escapedName, localRedefiner, QuerySelfBindingScope(sourcePoco));
                }
            }

            var cacheKey = (target.Id, sourceLocalScope?.Id ?? Guid.Empty);

            if (this.resolvedReferences.TryGetValue(cacheKey, out var cached))
            {
                return cached;
            }

            var resolved = this.ResolveFresh(target, sourcePoco, sourceLocalScope, escapedName, localRedefiner: null, QuerySelfBindingScope(sourcePoco));
            this.resolvedReferences[cacheKey] = resolved;
            return resolved;
        }

        /// <summary>
        /// Returns the <see cref="INamespace" /> in which <paramref name="sourcePoco" /> is itself the
        /// name binding for the element being referenced, or <see langword="null" /> when the source is
        /// not a referencing <see cref="IMembership" />.
        /// <para>A non-owning <see cref="IMembership" /> under an expression (e.g. the
        /// <c>FeatureReferenceMember</c> of a <see cref="IFeatureReferenceExpression" />) IS the reference
        /// being emitted — the parser resolves the name against the ENCLOSING lexical scope and only then
        /// creates this membership. Counting it as a binding in its own owner's scope is circular: the
        /// target would always appear uniquely resolvable at depth 0, so a bare simple name is emitted even
        /// when an intervening declaration shadows it (<c>in fuelCmd = fuelCmd</c> instead of
        /// <c>in fuelCmd = 'provide power'::fuelCmd</c>, which would re-parse to the local parameter).</para>
        /// <para>Restricted to memberships with NO explicit name override. Such a membership contributes
        /// the target's OWN name to its scope's index, which is exactly the binding to ignore. A
        /// membership that DOES carry an override is an <c>alias X for Y;</c> declaration: it contributes
        /// only the alias name, so the scope's binding of the target's own name comes from a different
        /// membership and stays valid (<c>alias ThreeDVectorQuantityValue for '3dVectorQuantityValue';</c>
        /// must not degrade to the qualified <c>Quantities::'3dVectorQuantityValue'</c>). The circular
        /// alias-for-itself case is handled separately by <see cref="DeclaresAlias"/>.</para>
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns>The scope whose binding for the target must be ignored, or <see langword="null" />.</returns>
        private static INamespace QuerySelfBindingScope(IElement sourcePoco)
        {
            return sourcePoco is IMembership membership and not IOwningMembership
                   && string.IsNullOrWhiteSpace(membership.MemberName)
                   && string.IsNullOrWhiteSpace(membership.MemberShortName)
                ? membership.OwningRelatedElement as INamespace
                : null;
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
        /// <param name="localRedefiner">The local <see cref="IFeature"/> that acts like redefiner</param>
        /// <param name="selfBindingScope">
        /// The <see cref="INamespace" /> whose binding of <paramref name="target" /> is contributed by the
        /// reference being emitted itself and must therefore be ignored during the scope walk, or
        /// <see langword="null" /> when the source is not a self-binding membership. See
        /// <see cref="QuerySelfBindingScope" />.
        /// </param>
        /// <returns>The resolved emission string.</returns>
        private string ResolveFresh(IElement target, IElement sourcePoco, INamespace sourceLocalScope, string escapedName, IFeature localRedefiner, INamespace selfBindingScope)
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

                if (this.TryResolveSimpleNameAcrossChain(chain, target, rawShortName, escapedShortName, localRedefiner, selfBindingScope, out var matchedShort))
                {
                    return matchedShort;
                }
            }

            var rawName = target.name;

            if (!string.IsNullOrWhiteSpace(rawName)
                && this.TryResolveSimpleNameAcrossChain(chain, target, rawName, escapedName, localRedefiner, selfBindingScope, out var matchedLong))
            {
                return matchedLong;
            }

            // Alias pass — an `alias X for Y;` declared in a reachable scope binds the target under a
            // name it does not carry itself, so the target's own lexical forms above can never find it.
            // Preferred over the facade / qualified forms because it is how the model names the element
            // at this site (e.g. `Torque` rather than `ISQMechanics::TorqueValue`).
            if (this.TryResolveViaAlias(chain, target, sourcePoco, localRedefiner, selfBindingScope, out var matchedAlias))
            {
                return matchedAlias;
            }

            // Facade re-export pass — when the target's owningNamespace is DIRECTLY re-exported
            // by another namespace via NamespaceImport AND that facade is reachable from the
            // source scope chain, prefer the OMG SST canonical form `facade::simpleName` over
            // the implementation-owning form `owner::simpleName`. The SST tutorial (Release
            // 2026-03) uses the facade form 17:1 over the implementation form (e.g.
            // `ISQ::mass` 17 times vs `ISQBase::mass` once), establishing this as the canonical
            // textual idiom. KerML §8.2.3.5.4 leaves the choice between the two formally
            // undetermined, so both forms parse to the same element.
            if (this.TryResolveViaDirectFacade(chain, target, escapedShortName, escapedName, out var matchedFacade))
            {
                return matchedFacade;
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
                    && this.TryResolveSimpleNameAcrossChain(chain, ancestor, ancestorRawName, ancestorSegment, localRedefiner, selfBindingScope, out var matchedAnchor))
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
        /// Determines whether the <paramref name="localRedefiner"/>'s DECLARED name (the
        /// modeller-typed identifier, NOT the effective name derived from the redefinition
        /// chain) equals the <paramref name="target"/>'s effective name. When this returns
        /// <see langword="true"/>, the writer must emit the redefined target as a qualified
        /// name — emitting the bare simple-name form would re-resolve at parse time to the
        /// local redefiner (because the local member, once parsed, shadows the inherited one)
        /// instead of to the redefined target. Per KerML §8.2.3.5 the qualified form
        /// guarantees that the textual round-trip resolves back to the SAME element.
        /// <para>An anonymous redefining feature (e.g. <c>ref :&gt;&gt; driveshaft = …</c>) has
        /// both <c>DeclaredName</c> and <c>DeclaredShortName</c> empty — its effective name is
        /// derived from the redefinition. Such a redefiner cannot collide and the writer can
        /// safely emit the shortened form.</para>
        /// </summary>
        /// <param name="localRedefiner">The redefining feature; must be non-null.</param>
        /// <param name="target">The redefined target.</param>
        /// <returns><see langword="true"/> when the declared simple-name of the redefiner equals the target's effective name.</returns>
        private static bool RedefinerDeclaredNameCollidesWith(IFeature localRedefiner, IElement target)
        {
            var redefinerDeclaredName = localRedefiner.DeclaredName;
            var redefinerDeclaredShortName = localRedefiner.DeclaredShortName;

            if (string.IsNullOrWhiteSpace(redefinerDeclaredName) && string.IsNullOrWhiteSpace(redefinerDeclaredShortName))
            {
                return false;
            }

            var targetName = target.name;
            var targetShortName = target.shortName;

            return (!string.IsNullOrWhiteSpace(redefinerDeclaredName) && string.Equals(redefinerDeclaredName, targetName, StringComparison.Ordinal))
                || (!string.IsNullOrWhiteSpace(redefinerDeclaredName) && string.Equals(redefinerDeclaredName, targetShortName, StringComparison.Ordinal))
                || (!string.IsNullOrWhiteSpace(redefinerDeclaredShortName) && string.Equals(redefinerDeclaredShortName, targetName, StringComparison.Ordinal))
                || (!string.IsNullOrWhiteSpace(redefinerDeclaredShortName) && string.Equals(redefinerDeclaredShortName, targetShortName, StringComparison.Ordinal));
        }

        /// <summary>
        /// Attempts to emit a "facade re-export" form for <paramref name="target"/> — i.e.
        /// <c>facade::simpleName</c> where <c>facade</c> is a namespace that DIRECTLY imports
        /// the target's owning namespace via <see cref="INamespaceImport"/> and is reachable
        /// from the source scope <paramref name="chain"/>. This matches the OMG SST canonical
        /// idiom of <c>ISQ::mass</c> over <c>ISQBase::mass</c> (KerML §8.2.3.5.4 leaves the
        /// choice formally undetermined; the SST tutorial uses the facade form 17:1).
        /// <para>Single-hop only — the SST does not use deep-chain facade names like
        /// <c>SI::mass</c> (SI imports ISQ which imports ISQBase, two hops away).</para>
        /// <para>Tie-break order when multiple facades are reachable: (1) facade whose simple
        /// name resolves uniquely to ITSELF in the scope chain (i.e. a clean anchor); (2)
        /// innermost scope-chain proximity (the facade reachable at the innermost scope wins);
        /// (3) shorter facade name; (4) stable alphabetical.</para>
        /// </summary>
        /// <param name="chain">The source scope chain (innermost first).</param>
        /// <param name="target">The element being referenced.</param>
        /// <param name="escapedShortName">Pre-escaped target shortName (may be <see langword="null"/>).</param>
        /// <param name="escapedName">Pre-escaped target name.</param>
        /// <param name="matched">On a hit, the emitted <c>facade::simpleName</c> string.</param>
        /// <returns><see langword="true"/> when a reachable facade was found and the emission was assembled.</returns>
        private bool TryResolveViaDirectFacade(IReadOnlyList<INamespace> chain, IElement target, string escapedShortName, string escapedName, out string matched)
        {
            matched = null;

            var canonicalOwner = QueryOwningContainer(target) as INamespace;

            if (canonicalOwner == null || !this.directFacadeIndex.TryGetValue(canonicalOwner, out var facades) || facades.Count == 0)
            {
                return false;
            }

            // Prefer the target's shortest emission form, mirroring the depth-0 walk above.
            var targetSimpleName = !string.IsNullOrWhiteSpace(escapedShortName) ? escapedShortName : escapedName;

            if (string.IsNullOrWhiteSpace(targetSimpleName))
            {
                return false;
            }

            INamespace bestFacade = null;
            var bestScopeDepth = int.MaxValue;

            // First pass — prefer facades reachable via a scope in the source chain (their
            // simple name resolves directly in some chain scope's index). This is the
            // strictest reachability and matches the lexical-resolution model.
            for (var scopeDepth = 0; scopeDepth < chain.Count; scopeDepth++)
            {
                var scope = chain[scopeDepth];
                var scopeIndex = this.GetSimpleNameIndex(scope);

                foreach (var facade in facades)
                {
                    var facadeName = !string.IsNullOrWhiteSpace(facade.shortName) ? facade.shortName : facade.name;

                    if (string.IsNullOrWhiteSpace(facadeName) || !scopeIndex.TryGetValue(facadeName, out var facadeBucket) || !facadeBucket.Contains(facade))
                    {
                        continue;
                    }

                    // Innermost-scope win takes priority; within the same scope depth, prefer
                    // the shorter facade name, then stable alphabetical.
                    if (bestFacade == null
                        || scopeDepth < bestScopeDepth
                        || (scopeDepth == bestScopeDepth && CompareFacades(facade, bestFacade) < 0))
                    {
                        bestFacade = facade;
                        bestScopeDepth = scopeDepth;
                    }
                }

                if (bestFacade != null)
                {
                    break;
                }
            }

            // Second pass — KerML §8.2.3.5.4 says name resolution walks all the way out to
            // the global namespace, which contains all loaded library root namespaces. A
            // facade indexed by the cache (even one not lexically owned by a source-chain
            // scope) is therefore reachable for the parser via the global resolution step,
            // and `facade::simpleName` round-trips to the same target element.
            // <para>Restrict to facades whose name is MEANINGFULLY shorter than the canonical
            // owner's — i.e. at most 70% of the owner's length. This matches the OMG SST
            // convention: ISBase (7 chars) → ISQ (3 chars, 43% of ISBase) is a meaningful
            // shortening; but ScalarValues (12 chars) → Collections (11 chars, 92%) is NOT
            // — Collections is structurally a parent wrapper, not a user-facing facade for
            // ScalarValues. Without semantic understanding the canonical owner is preferred
            // in the latter case.</para>
            if (bestFacade == null)
            {
                var canonicalNameForCompare = QueryPreferredEscapedSegment(canonicalOwner);
                var canonicalLength = canonicalNameForCompare?.Length ?? int.MaxValue;
                var meaningfulShorterMax = (int)(canonicalLength * 0.7);

                foreach (var facade in facades)
                {
                    if (!this.simpleNameIndices.ContainsKey(facade))
                    {
                        continue;
                    }

                    var facadeNameForCompare = QueryPreferredEscapedSegment(facade);

                    if (string.IsNullOrWhiteSpace(facadeNameForCompare) || facadeNameForCompare.Length > meaningfulShorterMax)
                    {
                        continue;
                    }

                    if (bestFacade == null || CompareFacades(facade, bestFacade) < 0)
                    {
                        bestFacade = facade;
                    }
                }
            }

            if (bestFacade == null)
            {
                return false;
            }

            var bestFacadeSegment = QueryPreferredEscapedSegment(bestFacade);

            if (string.IsNullOrWhiteSpace(bestFacadeSegment))
            {
                return false;
            }

            matched = bestFacadeSegment + "::" + targetSimpleName;
            return true;
        }

        /// <summary>
        /// Stable ordering for facade candidates at the SAME scope depth: shorter name first,
        /// then ordinal alphabetical. Ensures the writer's output is deterministic across runs
        /// when multiple facades re-export the same owning namespace from the same scope.
        /// </summary>
        /// <param name="left">First candidate.</param>
        /// <param name="right">Second candidate.</param>
        /// <returns>Negative if <paramref name="left"/> sorts first, positive if right, zero if tied.</returns>
        private static int CompareFacades(INamespace left, INamespace right)
        {
            var leftName = !string.IsNullOrWhiteSpace(left.shortName) ? left.shortName : left.name;
            var rightName = !string.IsNullOrWhiteSpace(right.shortName) ? right.shortName : right.name;

            leftName ??= string.Empty;
            rightName ??= string.Empty;

            var lengthCompare = leftName.Length.CompareTo(rightName.Length);

            return lengthCompare != 0 ? lengthCompare : string.CompareOrdinal(leftName, rightName);
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
        /// <param name="localRedefiner">The local <see cref="IFeature"/> that acts as redefiner</param>
        /// <param name="selfBindingScope">
        /// The <see cref="INamespace" /> whose binding of <paramref name="target" /> is contributed by the
        /// reference being emitted itself and must therefore be skipped, or <see langword="null" /> when the
        /// source is not a self-binding membership. See <see cref="QuerySelfBindingScope" />.
        /// </param>
        /// <param name="matched">On a unique-binding hit, the simple-name string to emit.</param>
        /// <returns><see langword="true" /> when the simple name resolves uniquely to the target somewhere in the chain.</returns>
        private bool TryResolveSimpleNameAcrossChain(IReadOnlyList<INamespace> chain, IElement target, string rawName, string escapedName, IFeature localRedefiner, INamespace selfBindingScope, out string matched)
        {
            matched = null;

            if (string.IsNullOrWhiteSpace(rawName))
            {
                return false;
            }

            foreach (var scope in chain)
            {
                var resolution = this.ResolveSimpleNameInScope(scope, target, rawName, localRedefiner, selfBindingScope);

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
        /// <param name="localRedefiner">
        /// Optional feature to filter OUT of the scope's name bucket before leaf reduction —
        /// used by the redefinition-resolution path so the local redefining feature does not
        /// shadow the redefined target. Pass <see langword="null"/> for the normal resolution
        /// path. KerML §8.2.3.5: a redefining feature is not yet a resolvable member of its
        /// owning Type at the redefinition site, so it must not participate in name resolution
        /// when emitting <c>:&gt;&gt; name</c>.
        /// </param>
        /// <param name="selfBindingScope">
        /// Optional <see cref="INamespace" /> in which the reference being emitted is itself the
        /// membership binding <paramref name="target" />. In that scope the target's own entry is
        /// excluded (it does not exist at parse time): <see cref="SimpleNameResolution.NotBound" /> when
        /// nothing else binds the name there, <see cref="SimpleNameResolution.Shadowed" /> when another
        /// element does. Pass <see langword="null"/> when the source is not a self-binding membership.
        /// See <see cref="QuerySelfBindingScope" />.
        /// </param>
        /// <returns>The resolution state.</returns>
        private SimpleNameResolution ResolveSimpleNameInScope(INamespace scope, IElement target, string rawName, IFeature localRedefiner, INamespace selfBindingScope)
        {
            var index = this.GetSimpleNameIndex(scope);

            if (!index.TryGetValue(rawName, out var elements))
            {
                return SimpleNameResolution.NotBound;
            }

            // In the scope where the reference itself is the binding, the target's entry must be
            // ignored: per KerML §8.2.3.5.3 local resolution is membership-based, and the reference's
            // own membership does not exist yet when the parser resolves the written name — honouring
            // it would make every reference trivially resolvable at depth 0 and hide a shadowing
            // declaration further out. Only the TARGET's binding is excluded (mirroring the
            // localRedefiner filter): any other element bound under the same name in that scope still
            // shadows and forces the qualified form. See QuerySelfBindingScope.
            if (selfBindingScope != null && ReferenceEquals(scope, selfBindingScope))
            {
                var isBoundToOtherElement = elements.Any(element =>
                    !ReferenceEquals(element, target) && !ReferenceEquals(element, localRedefiner));

                return isBoundToOtherElement
                    ? SimpleNameResolution.Shadowed
                    : SimpleNameResolution.NotBound;
            }

            // Filter out the local redefining feature so it doesn't shadow the redefined target
            // it points to. When the bucket contains only the local redefiner, treat the name as
            // unbound in this scope and continue the chain walk outward.
            var candidates = elements.Where(element => !ReferenceEquals(element, localRedefiner)).ToList();

            if (candidates.Count == 0)
            {
                return SimpleNameResolution.NotBound;
            }

            if (candidates.Count == 1)
            {
                return ReferenceEquals(candidates[0], target)
                    ? SimpleNameResolution.Matched
                    : SimpleNameResolution.Shadowed;
            }

            candidates = PreferDirectlyOwnedOverInherited(scope, candidates);

            // Reduce to the leaf set: drop any element that is transitively redefined by
            // another element in `candidates`. The shadow set is the union of each candidate's
            // `AllRedefinedFeatures()` closure (excluding the candidate itself, which the
            // operation includes as the seed of the closure). The local redefiner — when
            // present — is excluded from this computation entirely so it neither participates
            // in shadow accumulation nor in the final leaf count.
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
                    return SimpleNameResolution.Shadowed;
                }

                onlyLeaf = element;
            }

            return leafCount == 1 && ReferenceEquals(onlyLeaf, target)
                ? SimpleNameResolution.Matched
                : SimpleNameResolution.Shadowed;
        }

        /// <summary>
        /// Narrows <paramref name="candidates" /> to those declared DIRECTLY in
        /// <paramref name="scope" /> when every other candidate is reachable only by INHERITANCE
        /// into that scope.
        /// <para>Per KerML §8.2.3.5.3 a well-formed <see cref="INamespace" /> has at most one
        /// <see cref="IMembership" /> as the local resolution of a given name, and
        /// <c>Type::inheritedMembership</c> is <c>removeRedefinedFeatures(inheritableMemberships(…))</c>.
        /// So when a scope binds one name to both an owned and an inherited feature, the owned one
        /// redefines the inherited one and the inherited membership is not in scope under that name.</para>
        /// <para>That redefinition is frequently IMPLIED rather than materialised. The OMG SysML v2
        /// spec, Clause 7.17.2 states that "if the required redefinitions are not explicitly declared
        /// for a parameter, then the parameter is considered to implicitly have redefinitions
        /// sufficient to meet the stated requirements", and the pilot's XMI export does not write those
        /// implied Relationships — so <c>action 'provide power' : 'Provide Power' { in fuelCmd; … }</c>
        /// arrives with no <see cref="IRedefinition"/> and the explicit-redefinition leaf reduction
        /// below cannot see the shadowing. Without this step the name looked ambiguous and degraded to
        /// <c>'provide power'::fuelCmd</c> where the canonical source writes <c>fuelCmd</c>.</para>
        /// <para>Applied as a resolution-time preference rather than by dropping inherited entries from
        /// the index: a redefined feature must stay nameable from the redefining declaration itself
        /// (<c>part frontAxleAssembly_c1 :&gt;&gt; frontAxleAssembly</c>, <c>port :&gt;&gt; pe = c1.pb</c>),
        /// where the two names differ so no collision arises and nothing may be shadowed.</para>
        /// </summary>
        /// <param name="scope">The namespace whose index produced <paramref name="candidates" />.</param>
        /// <param name="candidates">The candidates bound to the name being resolved.</param>
        /// <returns>The owned candidates when the preference applies, otherwise <paramref name="candidates" />.</returns>
        private static List<IElement> PreferDirectlyOwnedOverInherited(INamespace scope, List<IElement> candidates)
        {
            if (scope is not IType scopeAsType)
            {
                return candidates;
            }

            List<IType> supertypes;

            try
            {
                supertypes = scopeAsType.AllSupertypes();
            }
            catch (NotSupportedException)
            {
                return candidates;
            }

            var owned = candidates.Where(candidate => IsDirectlyOwnedBy(scope, candidate)).ToList();

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
        /// Determines whether <paramref name="element" /> is the member element of one of
        /// <paramref name="scope" />'s own memberships — that is, declared in the scope rather than
        /// imported into or inherited by it.
        /// </summary>
        /// <param name="scope">The namespace to test.</param>
        /// <param name="element">The candidate member element.</param>
        /// <returns><see langword="true" /> when the scope declares the element itself.</returns>
        private static bool IsDirectlyOwnedBy(INamespace scope, IElement element)
        {
            try
            {
                return scope.ownedMembership.Any(membership => ReferenceEquals(membership.MemberElement, element));
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether <paramref name="sourcePoco" /> is the right-hand side of a chain
        /// accessor — see grammar rules <c>FeatureChainExpression</c> (KerML §8.2.4.X) and
        /// <c>FeatureChain</c> (KerML §8.2.4.3.5). Three patterns match:
        /// <list type="bullet">
        ///   <item><description>An <see cref="IMembership" /> sitting as the chain-accessor RHS
        ///   of a <see cref="IFeatureChainExpression" />.</description></item>
        ///   <item><description>An <see cref="IFeatureChaining" /> at any index after the FIRST
        ///   in its container's <c>OwnedRelationship</c> list.</description></item>
        ///   <item><description>The <see cref="IRedefinition" /> of a flow feature whose
        ///   <see cref="IFlowEnd" /> carries a FlowEndSubsetting — see
        ///   <see cref="IsFlowFeatureAccessor" />.</description></item>
        /// </list>
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns><see langword="true" /> when the source is a chain accessor.</returns>
        private static bool IsChainAccessor(IElement sourcePoco)
        {
            if (sourcePoco is IMembership { OwningRelatedElement: { } membershipOwner } and not IParameterMembership
                && EstablishesRelativeNamespace(membershipOwner))
            {
                return true;
            }

            if (IsFlowFeatureAccessor(sourcePoco))
            {
                return true;
            }

            if (sourcePoco is not IFeatureChaining { OwningRelatedElement: IFeature chainOwner } chaining)
            {
                return false;
            }

            var siblings = chainOwner.OwnedRelationship.OfType<IFeatureChaining>().ToList();
            var index = siblings.IndexOf(chaining);

            if (index > 0)
            {
                return true;
            }

            // The FIRST chaining segment is also a chain accessor when the owned chain Feature is the
            // target member of a construct that establishes a RELATIVE namespace from a preceding
            // expression — the parser resolves even that first segment against the preceding result
            // rather than the lexical scope, so the bare simple name is the correct emission. Two
            // constructs do this, matching the reference implementation's
            // NamespaceUtil.getRelativeNamespaceFor:
            //   - FeatureChainExpression, relative to its argument expression's result (`= a11.b11.c1`);
            //   - AssignmentActionUsage, relative to its targetArgument, per
            //     AssignmentTargetParameter = ( AssignmentTargetBinding '.' )? followed by
            //     FeatureChainMember (`assign trailer.trailerFrame.coupler.hitch := …`). The pilot
            //     guards on a non-null targetArgument, so an assignment WITHOUT a target binding
            //     (`assign a.b := …`) keeps lexical resolution for its first segment; that guard is
            //     mirrored here.
            // A chain owned by a Specialization / ReferenceSubsetting (e.g. a connect end) is not
            // relative and keeps lexical resolution for its first segment.
            return chainOwner.OwningRelationship is IMembership { OwningRelatedElement: { } chainMemberOwner } and not IParameterMembership
                   && EstablishesRelativeNamespace(chainMemberOwner);
        }

        /// <summary>
        /// Determines whether <paramref name="owner" /> establishes a RELATIVE namespace — a scope taken
        /// from the result of a preceding expression instead of from lexical containment. A name resolved
        /// against such a scope is written as a bare simple name.
        /// <para>This mirrors the reference implementation's single decision point,
        /// <c>NamespaceUtil.getRelativeNamespaceFor</c>, which recognises exactly two constructs:</para>
        /// <list type="bullet">
        ///   <item><description><see cref="IFeatureChainExpression" /> — relative to the result of its
        ///   argument expression (<c>a11.b11.c1</c>).</description></item>
        ///   <item><description><see cref="IAssignmentActionUsage" /> — relative to the result of its
        ///   <c>targetArgument</c>, per <c>AssignmentTargetParameter = ( AssignmentTargetBinding '.' )?</c>
        ///   followed by <c>FeatureChainMember</c> (<c>assign trailer.trailerFrame.coupler.hitch := …</c>).
        ///   The reference implementation guards on a non-null target, so an assignment without a binding
        ///   (<c>assign a.b := …</c>) keeps lexical resolution.</description></item>
        /// </list>
        /// <para>The reference implementation additionally guards each arm on the preceding expression being
        /// present. The <see cref="IAssignmentActionUsage" /> guard is reproduced. Its
        /// <see cref="IFeatureChainExpression" /> counterpart (<c>!getArgument().isEmpty()</c>) is NOT: our
        /// <c>argument</c> is derived by matching the instantiated type's inputs against redefining owned
        /// features, and for a FeatureChainExpression the instantiated type is the library function
        /// <c>ControlFunctions::'.'</c>, for which that match yields an empty list — so the guard would
        /// reject every feature-chain expression and regress <c>a11.b11.c1</c> style output. The structural
        /// test alone is safe here: a FeatureChainExpression exists only to chain onto a preceding operand.</para>
        /// <para>Both forms of <c>FeatureChainMember</c> — the owned chain AND the plain
        /// <c>memberElement = [QualifiedName]</c> reference — go through this test, matching the reference
        /// implementation, which routes every <c>Membership</c> through the same relative-namespace lookup.</para>
        /// </summary>
        /// <param name="owner">The element owning the membership at the reference site.</param>
        /// <returns><see langword="true" /> when the owner establishes a relative namespace.</returns>
        private static bool EstablishesRelativeNamespace(IElement owner)
        {
            return owner switch
            {
                IFeatureChainExpression => true,
                IAssignmentActionUsage assignmentActionUsage => assignmentActionUsage.targetArgument != null,
                _ => false,
            };
        }

        /// <summary>
        /// Determines whether <paramref name="sourcePoco" /> is the <c>redefinedFeature</c> reference of a
        /// FlowFeatureRedefinition whose owning <see cref="IFlowEnd" /> also carries a FlowEndSubsetting.
        /// <para>Per <c>FlowEnd = ( ownedRelationship += FlowEndSubsetting )? ownedRelationship += FlowFeatureMember</c>
        /// (SysML 2.0 §8.2.2.16 Flows Textual Notation), the subsetting is the notational prefix — the flow
        /// end reads <c>'generate torque'.engineTorque</c>. The parser resolves the flow feature against the
        /// prefix's type, exactly as for a feature-chain accessor, so the writer must emit the bare simple
        /// name rather than a lexically-qualified one. When the optional subsetting is ABSENT the flow
        /// feature stands alone and keeps ordinary lexical resolution.</para>
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns><see langword="true" /> when the source is a prefixed flow-feature accessor.</returns>
        private static bool IsFlowFeatureAccessor(IElement sourcePoco)
        {
            if (sourcePoco is not IRedefinition { OwningRelatedElement: IFeature flowFeature })
            {
                return false;
            }

            return flowFeature.OwningRelationship is IFeatureMembership { OwningRelatedElement: IFlowEnd flowEnd }
                && flowEnd.OwnedRelationship.OfType<IReferenceSubsetting>().Any();
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
        private Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>> BuildSimpleNameIndices(INamespace rootNamespace)
        {
            var result = new Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>>();
            var pending = new Queue<(INamespace Scope, bool IsGlobal)>();
            var visited = new HashSet<INamespace>();

            pending.Enqueue((rootNamespace, false));

            // The other available root Namespaces form the global Namespace (KerML §8.2.3.5.2). They are
            // indexed as ordinary scopes so their members can be named, and enqueued AFTER the model's own
            // root so containment/import scopes are always visited first.
            foreach (var globalNamespace in this.globalNamespaces)
            {
                pending.Enqueue((globalNamespace, true));
            }

            while (pending.Count != 0)
            {
                var (scope, isGlobal) = pending.Dequeue();

                if (scope == null || !visited.Add(scope))
                {
                    continue;
                }

                var index = new Dictionary<string, HashSet<IElement>>(StringComparer.Ordinal);

                this.BuildOwnedAndImportedEntries(scope, index, pending, isGlobal);

                if (scope is IType type)
                {
                    BuildInheritedEntries(type, index, pending, isGlobal);
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
        /// <param name="pending">Queue of namespaces yet to be indexed, each paired with its global flag.</param>
        /// <param name="isGlobal">
        /// <see langword="true" /> when <paramref name="scope" /> is reached through the global
        /// <see cref="INamespace" /> rather than through the serialized model's own containment / import
        /// graph. KerML §8.2.3.5.2 admits only the VISIBLE memberships of other root namespaces into the
        /// global scope, so non-public entries are excluded there — naming an element bound only by a
        /// private membership would emit text the parser cannot resolve. Scopes within the model itself are
        /// resolved from the inside and therefore see all of their members.
        /// </param>
        private void BuildOwnedAndImportedEntries(INamespace scope, Dictionary<string, HashSet<IElement>> index, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
        {
            try
            {
                foreach (var ownedMember in scope.ownedMembership.Where(ownedMember => IsVisibleWhenGlobal(ownedMember, isGlobal)))
                {
                    AddMembershipEntry(index, ownedMember, pending, isGlobal);
                    this.RecordAliasIfDeclared(scope, ownedMember);
                }
            }
            catch (NotSupportedException)
            {
                // ownedMembership may not be implemented; skip.
            }

            try
            {
                foreach (var ownedImport in scope.ownedImport.Where(ownedImport => IsVisibleWhenGlobal(ownedImport, isGlobal)))
                {
                    switch (ownedImport)
                    {
                        case IMembershipImport { ImportedMembership: { } importedMembership }:
                            AddMembershipEntry(index, importedMembership, pending, isGlobal);
                            this.RecordAliasIfDeclared(scope, importedMembership);
                            // Enqueue the imported member's owning namespace too — for SI::kg
                            // this walks SI, whose own ownedImport chain reaches ISQ and
                            // ISQBase, populating directFacadeIndex so facade-form shortening
                            // (e.g. `ISQ::mass`) becomes available at resolve time.
                            // Isolated try/catch so a NotSupportedException on
                            // MemberElement.owningNamespace doesn't unwind the outer
                            // ownedImport loop and lose every remaining import for this scope.
                            try
                            {
                                if (importedMembership.MemberElement?.owningNamespace is { } memberOwner)
                                {
                                    pending.Enqueue((memberOwner, isGlobal));
                                }
                            }
                            catch (NotSupportedException)
                            {
                                // owningNamespace not implemented for this imported member; skip the owner enqueue.
                            }
                            break;
                        case INamespaceImport { ImportedNamespace: not null } namespaceImport:
                        {
                            pending.Enqueue((namespaceImport.ImportedNamespace, isGlobal));

                            // Record `scope` as a direct (single-hop) facade re-exporter of
                            // `namespaceImport.ImportedNamespace`. Used by ResolveFresh to emit
                            // the OMG SST canonical form `facade::simpleName` instead of
                            // `canonicalOwner::simpleName` when the target lives in the
                            // imported namespace (e.g. `ISQ::mass` over `ISQBase::mass`).
                            this.RecordDirectFacade(namespaceImport.ImportedNamespace, scope);

                            // Isolate the inner ownedMembership walk in its own try/catch so a
                            // NotSupportedException from one imported namespace does not abort
                            // the outer ownedImport loop and lose every remaining import.
                            try
                            {
                                foreach (var importedMember in namespaceImport.ImportedNamespace.ownedMembership.Where(importedMember => IsVisibleWhenGlobal(importedMember, isGlobal)))
                                {
                                    AddMembershipEntry(index, importedMember, pending, isGlobal);

                                    // An imported `alias X for Y;` is reachable by its alias name in the
                                    // IMPORTING scope too, so record it against `scope`, not the source.
                                    this.RecordAliasIfDeclared(scope, importedMember);
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
        /// Records an <c>alias X for Y;</c> binding into <see cref="aliasIndex"/>. A membership declares an
        /// alias when it carries an explicit <see cref="IMembership.MemberName"/> /
        /// <see cref="IMembership.MemberShortName"/> that differs from the member element's own
        /// <see cref="IElement.name"/> / <see cref="IElement.shortName"/> — a membership without an override
        /// merely re-exposes the element under its own name and is not an alias.
        /// </summary>
        /// <param name="scope">The <see cref="INamespace"/> declaring the membership.</param>
        /// <param name="membership">The candidate alias <see cref="IMembership"/>; may be <see langword="null"/>.</param>
        private void RecordAliasIfDeclared(INamespace scope, IMembership membership)
        {
            var target = membership?.MemberElement;

            if (target == null)
            {
                return;
            }

            var aliasNames = new[] { membership.MemberName, membership.MemberShortName }
                .Where(aliasName => !string.IsNullOrWhiteSpace(aliasName)
                    && !string.Equals(aliasName, target.name, StringComparison.Ordinal)
                    && !string.Equals(aliasName, target.shortName, StringComparison.Ordinal))
                .ToList();

            if (aliasNames.Count == 0)
            {
                return;
            }

            if (!this.aliasIndex.TryGetValue(scope, out var scopeAliases))
            {
                scopeAliases = [];
                this.aliasIndex[scope] = scopeAliases;
            }

            if (!scopeAliases.TryGetValue(target, out var existingAliases))
            {
                existingAliases = [];
                scopeAliases[target] = existingAliases;
            }

            existingAliases.AddRange(aliasNames.Where(aliasName => !existingAliases.Contains(aliasName)));
        }

        /// <summary>
        /// Attempts to emit an in-scope ALIAS for <paramref name="target"/> — the name introduced by an
        /// <c>alias X for Y;</c> declaration reachable from the source scope <paramref name="chain"/>. Each
        /// candidate alias name is validated through the SAME first-binding-wins scope walk as an ordinary
        /// simple name (<see cref="TryResolveSimpleNameAcrossChain"/>, per KerML §8.2.3.5.4): a scope closer
        /// to the reference site that binds the alias string to a DIFFERENT element shadows the alias, and
        /// the candidate is rejected — so the emitted text always round-trips to the same element.
        /// </summary>
        /// <param name="chain">The source scope chain (innermost first).</param>
        /// <param name="target">The element being referenced.</param>
        /// <param name="sourcePoco">The reference site's source POCO — used to reject the alias DECLARATION itself.</param>
        /// <param name="localRedefiner">Optional feature to exclude from the scope buckets.</param>
        /// <param name="selfBindingScope">Optional scope whose own binding of the target must be ignored.</param>
        /// <param name="matched">On a hit, the escaped alias name to emit.</param>
        /// <returns><see langword="true"/> when an unambiguous in-scope alias was found.</returns>
        private bool TryResolveViaAlias(IReadOnlyList<INamespace> chain, IElement target, IElement sourcePoco, IFeature localRedefiner, INamespace selfBindingScope, out string matched)
        {
            matched = null;

            var candidateAliasNames = chain
                .Where(scope => this.aliasIndex.ContainsKey(scope))
                .SelectMany(scope => this.aliasIndex[scope].TryGetValue(target, out var aliasNames) ? aliasNames : Enumerable.Empty<string>())
                .Distinct(StringComparer.Ordinal)
                .Where(aliasName => !DeclaresAlias(sourcePoco, target, aliasName));

            foreach (var aliasName in candidateAliasNames)
            {
                var escapedAliasName = aliasName.QueryIsValidBasicName() ? aliasName : aliasName.ToUnrestrictedName();

                if (this.TryResolveSimpleNameAcrossChain(chain, target, aliasName, escapedAliasName, localRedefiner, selfBindingScope, out matched))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether <paramref name="sourcePoco"/> is the very <c>alias</c> declaration that
        /// introduces <paramref name="aliasName"/> for <paramref name="target"/>. Such a declaration must
        /// emit the target's own (qualified) name — resolving it through its own alias would produce the
        /// circular <c>alias Torque for Torque;</c>. The exclusion is needed in addition to
        /// <see cref="QuerySelfBindingScope"/> because an importing scope further out re-exports the same
        /// alias, and that scope is not the declaration's own.
        /// </summary>
        /// <param name="sourcePoco">The reference site's source POCO.</param>
        /// <param name="target">The element being referenced.</param>
        /// <param name="aliasName">The candidate alias name.</param>
        /// <returns><see langword="true"/> when the source is the declaration of this alias.</returns>
        private static bool DeclaresAlias(IElement sourcePoco, IElement target, string aliasName)
        {
            return sourcePoco is IMembership sourceMembership
                && ReferenceEquals(sourceMembership.MemberElement, target)
                && (string.Equals(sourceMembership.MemberName, aliasName, StringComparison.Ordinal)
                    || string.Equals(sourceMembership.MemberShortName, aliasName, StringComparison.Ordinal));
        }

        /// <summary>
        /// Records <paramref name="facade"/> as a direct (single-hop) re-exporter of
        /// <paramref name="canonicalOwner"/>. Called once per <see cref="INamespaceImport"/>
        /// encountered in the eager build pass.
        /// </summary>
        /// <param name="canonicalOwner">The namespace being directly imported.</param>
        /// <param name="facade">The namespace whose <c>ownedImport</c> contains the
        /// <see cref="INamespaceImport"/> targeting <paramref name="canonicalOwner"/>.</param>
        private void RecordDirectFacade(INamespace canonicalOwner, INamespace facade)
        {
            if (!this.directFacadeIndex.TryGetValue(canonicalOwner, out var facades))
            {
                facades = [];
                this.directFacadeIndex[canonicalOwner] = facades;
            }

            facades.Add(facade);
        }

        /// <summary>
        /// Determines whether <paramref name="relationship" /> may contribute a name binding to a scope that
        /// is being indexed as part of the global <see cref="INamespace" />.
        /// <para>KerML 1.0 §8.2.3.5.2 states that the global <see cref="INamespace" /> "includes all the
        /// <i>visible</i> Memberships of all other root Namespaces", and §8.2.3.5.3 defines those as the
        /// public owned memberships, the memberships imported through public <c>Import</c>s and — for a
        /// <see cref="IType" /> — the public inherited memberships. A non-public membership of a library is
        /// therefore NOT reachable from the model being written, so indexing it could shorten a reference to
        /// a name the parser will not resolve.</para>
        /// <para>Within the model's own containment / import graph resolution happens from the INSIDE, where
        /// private and protected members are visible, so the filter applies only when
        /// <paramref name="isGlobal" /> is <see langword="true" />.</para>
        /// </summary>
        /// <param name="relationship">The <see cref="IMembership" /> or <see cref="IImport" /> being considered.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global <see cref="INamespace" />.</param>
        /// <returns><see langword="true" /> when the relationship may contribute a binding.</returns>
        private static bool IsVisibleWhenGlobal(IRelationship relationship, bool isGlobal)
        {
            if (!isGlobal)
            {
                return true;
            }

            return relationship switch
            {
                IMembership membership => membership.Visibility == VisibilityKind.Public,
                IImport import => import.Visibility == VisibilityKind.Public,
                _ => true,
            };
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
        private static void BuildInheritedEntries(IType type, Dictionary<string, HashSet<IElement>> index, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
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
                    pending.Enqueue((supertypeAsNamespace, isGlobal));
                }

                try
                {
                    foreach (var ownedMember in supertype.ownedMembership.Where(ownedMember => IsVisibleWhenGlobal(ownedMember, isGlobal)))
                    {
                        AddMembershipEntry(index, ownedMember, pending, isGlobal);
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
        private static void AddMembershipEntry(Dictionary<string, HashSet<IElement>> index, IMembership membership, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
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
                pending.Enqueue((targetAsNamespace, isGlobal));
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
