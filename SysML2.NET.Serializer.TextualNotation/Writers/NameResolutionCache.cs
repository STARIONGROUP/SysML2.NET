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
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Extensions;

    /// <summary>
    /// Resolves the shortest unambiguous textual name for a reference, mirroring KerML §8.2.3.5.
    /// Holds an eager per-namespace simple-name index built on construction, plus lazy caches for
    /// source scope chains and resolved emissions keyed by <c>(target, sourceLocalScope)</c>.
    /// </summary>
    public sealed class NameResolutionCache
    {
        /// <summary>
        /// Shared empty index returned for unknown / null namespaces.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, HashSet<IElement>> EmptyIndex
            = new Dictionary<string, HashSet<IElement>>(StringComparer.Ordinal);

        /// <summary>
        /// Eager structural cache: namespace → (simple-name → member set).
        /// </summary>
        private readonly Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>> simpleNameIndices;

        /// <summary>
        /// Lazy cache: source-POCO id → its upward containment chain of namespaces.
        /// </summary>
        private readonly Dictionary<Guid, IReadOnlyList<INamespace>> sourceScopeChains
            = new ();

        /// <summary>
        /// Lazy cache: <c>(target.Id, sourceLocalScope.Id)</c> → emitted string.
        /// </summary>
        private readonly Dictionary<(Guid TargetId, Guid SourceScopeId), string> resolvedReferences
            = new ();

        /// <summary>
        /// Reverse index: canonical owning namespace → namespaces that DIRECTLY re-export it via
        /// <see cref="INamespaceImport"/>. Enables the SST facade idiom <c>ISQ::mass</c> over
        /// <c>ISQBase::mass</c> (single hop only).
        /// </summary>
        private readonly Dictionary<INamespace, HashSet<INamespace>> directFacadeIndex = new();

        /// <summary>
        /// Reverse index of <c>alias X for Y;</c> bindings: scope → (aliased element → alias names).
        /// Needed because <see cref="ResolveFresh"/> probes only the target's own lexical forms.
        /// </summary>
        private readonly Dictionary<INamespace, Dictionary<IElement, List<string>>> aliasIndex = new();

        /// <summary>
        /// The other root namespaces forming the global namespace (KerML §8.2.3.5.2); only their
        /// VISIBLE memberships are indexed.
        /// </summary>
        private readonly List<INamespace> globalNamespaces;

        /// <summary>
        /// Initializes the cache and eagerly indexes every namespace reachable from
        /// <paramref name="rootNamespace" />.
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace" /> being serialized.</param>
        /// <param name="globalNamespaces">
        /// The other loaded root namespaces (model libraries), forming the global namespace per
        /// KerML §8.2.3.5.2. Optional — without them resolution falls back to longer, equally valid names.
        /// </param>
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
        /// Gets the root <see cref="INamespace" /> — the fallback local scope when a source POCO has no
        /// resolvable enclosing namespace.
        /// </summary>
        public INamespace RootNamespace { get; }

        /// <summary>
        /// Resolves the textual notation for a reference to <paramref name="target" /> at the site of
        /// <paramref name="sourcePoco" />. Results are memoised per <c>(target, sourceLocalScope)</c>.
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

                // Membership imports keep the full path, using the SHORTEST declared name per
                // segment (`import SI::kg`, not `SI::kilogram` as qualifiedName would give).
                case IMembership membership:
                    return membership.MemberElement != null
                        ? QueryShortQualifiedName(membership.MemberElement)
                        : string.Empty;
            }

            // A namespace import keeps a SELF-CONTAINED path unless the target is reachable by
            // containment: shortest-name resolution would emit a name that only resolves while a
            // SIBLING import of the same namespace remains (`import 'provide power'::*` rather than
            // `import '3a-Function-based Behavior-1'::'provide power'::*`).
            if (sourcePoco is IImport { OwningRelatedElement: { } importOwner }
                && !IsReachableByContainment(target, importOwner))
            {
                return this.QueryImportPath(target);
            }

            var escapedName = target.EscapedName();

            if (string.IsNullOrWhiteSpace(escapedName))
            {
                return target.qualifiedName ?? string.Empty;
            }

            // Chain accessors resolve against the preceding segment's type, so the bare simple
            // name is always correct. Not memoised: the decision depends on sourcePoco.
            if (IsChainAccessor(sourcePoco))
            {
                return ResolveChainAccessor(target, escapedName);
            }

            var sourceLocalScope = this.GetSourceLocalScope(sourcePoco);

            // KerML §8.2.3.5.1: the ONE exception to basic resolution — a Redefinition's redefinedFeature is
            // resolved against the general Type of each ownedSpecialization of the owningType, NOT against the
            // reference site's local namespace. This is what keeps `:>> fuelCmdPort` short while the ordinary
            // local scope has that inherited membership removed (§8.2.3.5.3).
            // Skipped when the redefining feature DECLARES the redefined feature's name: the bare form is
            // legal there (it resolves in the supertype, not locally) but reads as a self-reference, and the
            // pilot always writes `mass :>> Vehicle::mass`. Qualifying is never wrong, so match it.
            if (sourcePoco is IRedefinition { RedefiningFeature: { } redefiningFeature } redefinitionContext
                && ReferenceEquals(target, redefinitionContext.RedefinedFeature)
                && !RedefinerDeclaredNameCollidesWith(redefiningFeature, target)
                && !ReferencedFeatureSharesSimpleName(redefiningFeature, target)
                && this.QueryRedefinedFeatureScope(redefinitionContext, target) is { } redefinitionScope)
            {
                sourceLocalScope = redefinitionScope;
            }

            // A redefinition's redefining feature — and equally a reference subsetting's referencing
            // feature, whose effective name derives FROM the referenced target — is bound in scope
            // under the very name being resolved and must not shadow its own target. Excluded from
            // the lookup unless its DECLARED name collides, in which case the qualified form is
            // required for the round-trip to resolve to the same element (KerML §8.2.3.5).
            var localReferencer = sourcePoco switch
            {
                IRedefinition redefinition when ReferenceEquals(target, redefinition.RedefinedFeature) => redefinition.RedefiningFeature,
                IReferenceSubsetting referenceSubsetting when ReferenceEquals(target, referenceSubsetting.ReferencedFeature) => referenceSubsetting.referencingFeature,
                _ => null,
            };

            if (localReferencer != null && !RedefinerDeclaredNameCollidesWith(localReferencer, target))
            {
                return this.ResolveFresh(target, sourcePoco, sourceLocalScope, escapedName, localReferencer, QuerySelfBindingScope(sourcePoco));
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
        /// Determines whether <paramref name="target" /> is declared by <paramref name="importOwner" /> or by
        /// one of its lexically enclosing namespaces — in which case an import may name it relatively
        /// (<c>import Usages::*</c>, or a sibling <c>import Definitions::*</c>) without depending on any
        /// import.
        /// </summary>
        /// <param name="target">The imported element.</param>
        /// <param name="importOwner">The element owning the import declaration.</param>
        /// <returns><see langword="true" /> when the target is reachable by containment.</returns>
        private static bool IsReachableByContainment(IElement target, IElement importOwner)
        {
            var declaringNamespace = QueryOwningContainer(target);

            if (declaringNamespace == null)
            {
                return false;
            }

            for (var scope = importOwner; scope != null; scope = QueryOwningContainer(scope))
            {
                if (ReferenceEquals(scope, declaringNamespace))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a SELF-CONTAINED path to <paramref name="target" />, anchored at the outermost named
        /// ancestor that binds it directly, so the path never depends on names introduced by imports of the
        /// importing namespace.
        /// <para>Intermediate owner segments the anchor re-exports are collapsed —
        /// <c>'3a-…-1'::Usages::'provide power'</c> becomes <c>'3a-…-1'::'provide power'</c> because
        /// <c>'3a-…-1'</c> publicly imports <c>Usages::*</c>.</para>
        /// </summary>
        /// <param name="target">The imported <see cref="IElement" />; must be non-null.</param>
        /// <returns>The import path, or the target's <c>qualifiedName</c> when no anchor collapses.</returns>
        private string QueryImportPath(IElement target)
        {
            var namedAncestors = new List<IElement>();

            for (var ancestor = QueryOwningContainer(target); ancestor != null; ancestor = QueryOwningContainer(ancestor))
            {
                if (string.IsNullOrWhiteSpace(QueryPreferredEscapedSegment(ancestor)))
                {
                    break;
                }

                namedAncestors.Add(ancestor);
            }

            var targetSegment = QueryPreferredEscapedSegment(target);

            if (targetSegment == null)
            {
                return target.qualifiedName ?? string.Empty;
            }

            // Outermost first: the widest anchor yields the shortest self-contained path.
            namedAncestors.Reverse();

            for (var anchorIndex = 0; anchorIndex < namedAncestors.Count; anchorIndex++)
            {
                if (namedAncestors[anchorIndex] is not INamespace anchor || !this.BindsDirectly(anchor, target, targetSegment))
                {
                    continue;
                }

                var segments = namedAncestors
                    .Take(anchorIndex + 1)
                    .Select(QueryPreferredEscapedSegment)
                    .Append(targetSegment);

                return string.Join("::", segments);
            }

            return namedAncestors.Count == 0
                ? targetSegment
                : target.qualifiedName ?? string.Empty;
        }

        /// <summary>
        /// Determines whether <paramref name="scope" />'s index binds <paramref name="segment" /> uniquely
        /// to <paramref name="target" /> — i.e. the target is nameable directly from that scope.
        /// </summary>
        /// <param name="scope">The candidate anchor namespace.</param>
        /// <param name="target">The element being named.</param>
        /// <param name="segment">The target's escaped simple-name segment.</param>
        /// <returns><see langword="true" /> when the scope binds the segment to exactly the target.</returns>
        private bool BindsDirectly(INamespace scope, IElement target, string segment)
        {
            var rawName = QueryPreferredRawName(target);

            return !string.IsNullOrWhiteSpace(rawName)
                   && this.GetSimpleNameIndex(scope).TryGetValue(rawName, out var bucket)
                   && bucket.Count == 1
                   && bucket.Contains(target)
                   && !string.IsNullOrWhiteSpace(segment);
        }

        /// <summary>
        /// Returns the scope in which <paramref name="sourcePoco" /> is itself the name binding for the
        /// target — a non-owning <see cref="IMembership"/> without a name override IS the reference being
        /// emitted, and its binding does not exist yet at parse time. Its entry must be ignored in that
        /// scope or every reference would resolve trivially at depth 0.
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
        /// First-time resolution: probes the target's own simple names (short first, per the SST
        /// convention), then aliases, then facade re-exports, then owner-chain ancestors as anchors for a
        /// partially-qualified suffix, and finally falls back to <see cref="IElement.qualifiedName" />.
        /// </summary>
        /// <param name="target">The referenced element.</param>
        /// <param name="sourcePoco">The reference site's source POCO.</param>
        /// <param name="sourceLocalScope">The pre-computed local scope (may be <see langword="null" />).</param>
        /// <param name="escapedName">The target's escaped raw <c>name</c>.</param>
        /// <param name="localRedefiner">Local feature to exclude from scope buckets, or <see langword="null" />.</param>
        /// <param name="selfBindingScope">Scope whose binding of the target must be ignored, or <see langword="null" />.</param>
        /// <returns>The resolved emission string.</returns>
        private string ResolveFresh(IElement target, IElement sourcePoco, INamespace sourceLocalScope, string escapedName, IFeature localRedefiner, INamespace selfBindingScope)
        {
            var chain = this.GetSourceScopeChain(sourcePoco, sourceLocalScope);

            var rawShortName = target.shortName;
            string escapedShortName = null;

            if (!string.IsNullOrWhiteSpace(rawShortName))
            {
                escapedShortName = Escape(rawShortName);

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

            // An alias binds the target under a name it does not carry itself, so the probes above
            // can never find it. Preferred over facade/qualified forms — it is how the model names
            // the element at this site.
            if (this.TryResolveViaAlias(chain, target, sourcePoco, localRedefiner, selfBindingScope, out var matchedAlias))
            {
                return matchedAlias;
            }

            // Facade re-export: `ISQ::mass` over `ISQBase::mass` — the SST canonical idiom
            // (KerML §8.2.3.5.4 leaves the choice open; both forms parse to the same element).
            if (this.TryResolveViaDirectFacade(chain, target, escapedShortName, escapedName, out var matchedFacade))
            {
                return matchedFacade;
            }

            // Walk owner-chain ancestors outward; the first that resolves uniquely anchors a
            // partially-qualified suffix down to the target.
            var segmentsDownToTarget = new Stack<string>();

            segmentsDownToTarget.Push(QueryPreferredEscapedSegment(target) ?? string.Empty);

            var ancestor = (IElement)QueryOwningContainer(target);
            var visitedAncestors = new HashSet<IElement>();

            while (ancestor != null && visitedAncestors.Add(ancestor))
            {
                var ancestorSegment = QueryPreferredEscapedSegment(ancestor);

                if (string.IsNullOrWhiteSpace(ancestorSegment))
                {
                    // An unnamed namespace cannot appear inside a QualifiedName; stop and let the
                    // qualifiedName fallback take over.
                    break;
                }

                var ancestorRawName = QueryPreferredRawName(ancestor);

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

            return target.qualifiedName ?? string.Empty;
        }

        /// <summary>
        /// Returns <paramref name="element" />'s <c>owningNamespace</c>, or <see langword="null" /> when
        /// unreachable or the derived property is not implemented.
        /// </summary>
        /// <param name="element">The element whose owner is requested; may be <see langword="null" />.</param>
        /// <returns>The owning namespace or <see langword="null" />.</returns>
        private static INamespace QueryOwningContainer(IElement element)
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
        /// Returns <paramref name="element" />'s <c>owner</c>, or <see langword="null" /> when the derived
        /// property is not implemented.
        /// </summary>
        /// <param name="element">The element whose owner is requested; must be non-null.</param>
        /// <returns>The owner or <see langword="null" />.</returns>
        private static IElement QueryOwnerSafe(IElement element)
        {
            try
            {
                return element.owner;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns <paramref name="type" />'s transitive supertypes, or an empty list when the operation is
        /// not implemented.
        /// </summary>
        /// <param name="type">The type to query; must be non-null.</param>
        /// <returns>The supertypes, possibly empty.</returns>
        private static List<IType> QueryAllSupertypesSafe(IType type)
        {
            try
            {
                return type.AllSupertypes();
            }
            catch (NotSupportedException)
            {
                return [];
            }
        }

        /// <summary>
        /// Escapes <paramref name="rawName" /> per KEBNF: unchanged when it is a basic name, otherwise
        /// quoted as an unrestricted name.
        /// </summary>
        /// <param name="rawName">The raw name; must be non-blank.</param>
        /// <returns>The escaped form.</returns>
        private static string Escape(string rawName)
        {
            return rawName.QueryIsValidBasicName() ? rawName : rawName.ToUnrestrictedName();
        }

        /// <summary>
        /// Returns the element's preferred raw simple name: <c>shortName</c> when non-blank, otherwise
        /// <c>name</c>. May be <see langword="null" /> or blank.
        /// </summary>
        /// <param name="element">The element to name; must be non-null.</param>
        /// <returns>The preferred raw name.</returns>
        private static string QueryPreferredRawName(IElement element)
        {
            return !string.IsNullOrWhiteSpace(element.shortName) ? element.shortName : element.name;
        }

        /// <summary>
        /// Determines whether the local referencer's DECLARED name equals the target's effective name — in
        /// which case the bare simple name would re-resolve to the local member and the qualified form is
        /// required. An anonymous referencer (no declared names) can never collide.
        /// <para>Deliberately DECLARED names only. Widening this to the effective name looks right — an
        /// anonymous redefiner inherits its name from the feature it redefines — but the pilot writes the
        /// bare form for exactly that shape (<c>ref :>> driveshaft</c>, <c>port :>> fuelCmdPort</c>), so
        /// widening over-qualifies 2a / 2c / 3c-2 / 4a.</para>
        /// </summary>
        /// <param name="localRedefiner">The redefining/referencing feature; must be non-null.</param>
        /// <param name="target">The referenced target.</param>
        /// <returns><see langword="true"/> on a collision.</returns>
        private static bool RedefinerDeclaredNameCollidesWith(IFeature localRedefiner, IElement target)
        {
            var declaredNames = new[] { localRedefiner.DeclaredName, localRedefiner.DeclaredShortName }
                .Where(declared => !string.IsNullOrWhiteSpace(declared));

            return declaredNames.Any(declared =>
                string.Equals(declared, target.name, StringComparison.Ordinal)
                || string.Equals(declared, target.shortName, StringComparison.Ordinal));
        }

        /// <summary>
        /// Attempts the facade form <c>facade::simpleName</c>, where the facade DIRECTLY re-exports the
        /// target's owning namespace (single hop, matching the SST idiom <c>ISQ::mass</c>). Pass 1 prefers
        /// a facade resolvable in the source scope chain (innermost first, then <see cref="CompareFacades"/>);
        /// pass 2 accepts any indexed facade whose name is meaningfully shorter than the owner's, since
        /// global resolution (KerML §8.2.3.5.4) still reaches it.
        /// </summary>
        /// <param name="chain">The source scope chain (innermost first).</param>
        /// <param name="target">The element being referenced.</param>
        /// <param name="escapedShortName">Pre-escaped target shortName (may be <see langword="null"/>).</param>
        /// <param name="escapedName">Pre-escaped target name.</param>
        /// <param name="matched">On a hit, the emitted <c>facade::simpleName</c> string.</param>
        /// <returns><see langword="true"/> when a reachable facade was found.</returns>
        private bool TryResolveViaDirectFacade(IReadOnlyList<INamespace> chain, IElement target, string escapedShortName, string escapedName, out string matched)
        {
            matched = null;

            var canonicalOwner = QueryOwningContainer(target);

            if (canonicalOwner == null || !this.directFacadeIndex.TryGetValue(canonicalOwner, out var facades) || facades.Count == 0)
            {
                return false;
            }

            var targetSimpleName = !string.IsNullOrWhiteSpace(escapedShortName) ? escapedShortName : escapedName;

            if (string.IsNullOrWhiteSpace(targetSimpleName))
            {
                return false;
            }

            INamespace bestFacade = null;
            var bestScopeDepth = int.MaxValue;

            for (var scopeDepth = 0; scopeDepth < chain.Count; scopeDepth++)
            {
                var scope = chain[scopeDepth];
                var scopeIndex = this.GetSimpleNameIndex(scope);

                foreach (var facade in facades)
                {
                    var facadeName = QueryPreferredRawName(facade);

                    if (string.IsNullOrWhiteSpace(facadeName) || !scopeIndex.TryGetValue(facadeName, out var facadeBucket) || !facadeBucket.Contains(facade))
                    {
                        continue;
                    }

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

            if (bestFacade == null)
            {
                // "Meaningfully shorter" = at most 70% of the owner's name length. ISQBase → ISQ
                // qualifies; ScalarValues → Collections (a structural parent, not a user-facing
                // facade) does not, and the canonical owner is preferred.
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
        /// Deterministic ordering for facade candidates at the same scope depth: shorter name first, then
        /// ordinal alphabetical.
        /// </summary>
        /// <param name="left">First candidate.</param>
        /// <param name="right">Second candidate.</param>
        /// <returns>Negative if <paramref name="left"/> sorts first, positive if right, zero if tied.</returns>
        private static int CompareFacades(INamespace left, INamespace right)
        {
            var leftName = QueryPreferredRawName(left) ?? string.Empty;
            var rightName = QueryPreferredRawName(right) ?? string.Empty;

            var lengthCompare = leftName.Length.CompareTo(rightName.Length);

            return lengthCompare != 0 ? lengthCompare : string.CompareOrdinal(leftName, rightName);
        }

        /// <summary>
        /// Returns the element's shortest escaped name segment (shortName preferred), or
        /// <see langword="null" /> when neither lexical form is available.
        /// </summary>
        /// <param name="element">The element to name; must be non-null.</param>
        /// <returns>The escaped segment, or <see langword="null" />.</returns>
        private static string QueryPreferredEscapedSegment(IElement element)
        {
            var preferred = QueryPreferredRawName(element);

            return string.IsNullOrWhiteSpace(preferred) ? null : Escape(preferred);
        }

        /// <summary>
        /// Walks <paramref name="chain" /> innermost-out for a scope binding <paramref name="rawName" />
        /// uniquely to <paramref name="target" />. A scope that binds the name to anything else stops the
        /// walk — the parser's resolution would already have claimed the name there.
        /// </summary>
        /// <param name="chain">The pre-built source-scope chain (innermost first).</param>
        /// <param name="target">The referenced element.</param>
        /// <param name="rawName">The simple-name lexical form to probe (may be blank).</param>
        /// <param name="escapedName">The escaped form to emit on a hit.</param>
        /// <param name="localRedefiner">Local feature to exclude from scope buckets, or <see langword="null" />.</param>
        /// <param name="selfBindingScope">Scope whose binding of the target must be ignored, or <see langword="null" />.</param>
        /// <param name="matched">On a hit, the simple-name string to emit.</param>
        /// <returns><see langword="true" /> when the name resolves uniquely to the target.</returns>
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
        /// Resolves a chain accessor's simple name: the target's <c>name</c> first, then <c>shortName</c>,
        /// then <c>qualifiedName</c> as a last resort.
        /// </summary>
        /// <param name="target">The chain-accessor target element.</param>
        /// <param name="escapedName">The pre-computed escaped <c>name</c> form.</param>
        /// <returns>The escaped simple name.</returns>
        private static string ResolveChainAccessor(IElement target, string escapedName)
        {
            if (!string.IsNullOrWhiteSpace(target.name))
            {
                return escapedName;
            }

            var rawShortName = target.shortName;

            if (!string.IsNullOrWhiteSpace(rawShortName))
            {
                return Escape(rawShortName);
            }

            return target.qualifiedName ?? string.Empty;
        }

        /// <summary>
        /// Returns the eagerly-built simple-name index for <paramref name="scope" />, or
        /// <see cref="EmptyIndex" /> when the scope was not reached.
        /// </summary>
        /// <param name="scope">The <see cref="INamespace" /> whose index is requested.</param>
        /// <returns>The simple-name → member-set lookup.</returns>
        private IReadOnlyDictionary<string, HashSet<IElement>> GetSimpleNameIndex(INamespace scope)
        {
            return scope == null ? EmptyIndex : this.simpleNameIndices.GetValueOrDefault(scope, EmptyIndex);
        }

        /// <summary>
        /// Returns the cached upward-walk chain for <paramref name="sourcePoco" />, building it on first
        /// encounter from <paramref name="sourceLocalScope" />.
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
        /// Materialises the <c>owningNamespace</c> chain from <paramref name="start" /> up to the root.
        /// </summary>
        /// <param name="start">The starting namespace.</param>
        /// <returns>The chain.</returns>
        private static IReadOnlyList<INamespace> BuildChain(INamespace start)
        {
            var chain = new List<INamespace>();
            var current = start;

            while (current != null)
            {
                chain.Add(current);
                current = QueryOwningContainer(current);
            }

            return chain;
        }

        /// <summary>
        /// Builds the qualified name of <paramref name="element" /> using the SHORTEST declared name per
        /// segment, escaped for the parser. Used for import declarations, where the pilot emits short
        /// forms (<c>SI::kg</c>) but <c>qualifiedName</c> yields long forms (<c>SI::kilogram</c>).
        /// </summary>
        /// <param name="element">The leaf <see cref="IElement" /> to qualify; must be non-null.</param>
        /// <returns>The short-form qualified name, or empty when no segment carries a usable name.</returns>
        private static string QueryShortQualifiedName(IElement element)
        {
            var segments = new Stack<string>();
            var current = element;

            while (current != null)
            {
                var preferred = QueryPreferredRawName(current);

                if (string.IsNullOrWhiteSpace(preferred))
                {
                    break;
                }

                segments.Push(Escape(preferred));

                current = QueryOwningContainer(current);
            }

            return string.Join("::", segments);
        }

        /// <summary>
        /// Resolves the local scope of <paramref name="sourcePoco" />: the first <see cref="INamespace" />
        /// reached by climbing <c>OwningRelatedElement</c>, <c>owningNamespace</c>, then <c>owner</c>.
        /// An anonymous nested namespace (no upward chain of its own) is skipped via <c>owner</c> so the
        /// reference site's real enclosing scope is found. Falls back to <see cref="RootNamespace" />.
        /// </summary>
        /// <param name="sourcePoco">The source POCO; may be <see langword="null" />.</param>
        /// <returns>The local scope or <see cref="RootNamespace" />.</returns>
        private INamespace GetSourceLocalScope(IElement sourcePoco)
        {
            if (sourcePoco == null)
            {
                return this.RootNamespace;
            }

            if (QueryContextRelationshipLocalScope(sourcePoco) is { } contextScope)
            {
                return contextScope;
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
                    if (QueryOwningContainer(asNamespace) != null || ReferenceEquals(asNamespace, this.RootNamespace))
                    {
                        return asNamespace;
                    }

                    var namespaceOwner = QueryOwnerSafe(asNamespace);

                    if (namespaceOwner == null)
                    {
                        return asNamespace;
                    }

                    current = namespaceOwner;
                    continue;
                }

                var owningNamespace = QueryOwningContainer(current);

                if (owningNamespace != null)
                {
                    return owningNamespace;
                }

                current = QueryOwnerSafe(current);
            }

            return this.RootNamespace;
        }

        /// <summary>
        /// Determines whether <paramref name="redefiningFeature" /> also REFERENCES a different element that
        /// shares a simple name with <paramref name="target" /> — the <c>exhibit X :>> Y</c> shape, where the
        /// reference and the redefinition name distinct elements under one name.
        /// <para>The reference is written bare, so leaving the redefinition bare emits the SAME token twice for
        /// two different elements: they stay distinct only for a reader that applies the §8.2.3.5.1 exception.
        /// Qualifying the redefinition is correct under either reading, and is what the pilot writes.</para>
        /// </summary>
        /// <param name="redefiningFeature">The feature owning the redefinition.</param>
        /// <param name="target">The redefined feature being named.</param>
        /// <returns><see langword="true" /> when the qualified form is required to keep the two distinct.</returns>
        private static bool ReferencedFeatureSharesSimpleName(IFeature redefiningFeature, IElement target)
        {
            var referencedFeature = redefiningFeature.OwnedRelationship
                .OfType<IReferenceSubsetting>()
                .Select(referenceSubsetting => referenceSubsetting.ReferencedFeature)
                .FirstOrDefault(referenced => referenced != null && !ReferenceEquals(referenced, target));

            if (referencedFeature == null)
            {
                return false;
            }

            var referencedNames = new[] { referencedFeature.name, referencedFeature.shortName }
                .Where(candidate => !string.IsNullOrWhiteSpace(candidate));

            return referencedNames.Any(candidate =>
                string.Equals(candidate, target.name, StringComparison.Ordinal)
                || string.Equals(candidate, target.shortName, StringComparison.Ordinal));
        }

        /// <summary>
        /// Returns the scope in which a <see cref="IRedefinition"/>'s <c>redefinedFeature</c> is resolved per
        /// KerML §8.2.3.5.1: the general <c>Type</c> of each <c>ownedSpecialization</c> of the owning feature's
        /// <c>owningType</c>, tried in turn until one binds the name. Falls back to the first such general type
        /// so the qualified form is still anchored correctly.
        /// </summary>
        /// <param name="redefinition">The redefinition at the reference site.</param>
        /// <param name="target">The redefined feature being named.</param>
        /// <returns>The scope, or <see langword="null" /> when the exception does not apply.</returns>
        private INamespace QueryRedefinedFeatureScope(IRedefinition redefinition, IElement target)
        {
            var owningType = redefinition.RedefiningFeature?.owningType;

            if (owningType == null)
            {
                return null;
            }

            List<INamespace> generalScopes;

            try
            {
                generalScopes = [..owningType.ownedSpecialization
                    .Select(specialization => specialization.General)
                    .OfType<INamespace>()
                    .Where(general => !ReferenceEquals(general, owningType))];
            }
            catch (NotSupportedException)
            {
                return null;
            }

            if (generalScopes.Count == 0)
            {
                return null;
            }

            var rawName = QueryPreferredRawName(target);

            var bindingScope = string.IsNullOrWhiteSpace(rawName)
                ? null
                : generalScopes.FirstOrDefault(scope =>
                    this.ResolveSimpleNameInScope(scope, target, rawName, localRedefiner: null, selfBindingScope: null) == SimpleNameResolution.Matched);

            return bindingScope ?? generalScopes[0];
        }

        /// <summary>
        /// Determines the local <see cref="INamespace"/> from the KIND of context relationship, per
        /// KerML §8.2.3.5.2. Only the kinds whose local scope is NOT simply the nearest enclosing namespace
        /// are handled here; everything else falls back to the containment climb.
        /// <para>For a <see cref="ISpecialization"/> the spec anchors resolution at the
        /// <c>owningNamespace</c> of the <c>owningType</c> — one level OUT from the owning feature — so the
        /// owning feature's own and inherited members are NOT in scope.</para>
        /// <para>NOT implemented: the clause also anchors a <see cref="IReferenceSubsetting"/> whose
        /// <c>referencingFeature</c> is an end feature of a <see cref="IConnector"/> at the CONNECTOR's owning
        /// namespace. Applying that emits <c>Actions::Action::start</c> where 3a-1 needs the short <c>start</c>
        /// the pilot writes; the cause is NOT diagnosed, since that namespace inherits <c>start</c> and ought
        /// to resolve it. Meanwhile the climb anchors deeper than the spec allows — at the end feature, so the
        /// end's and the connector's own members are wrongly in scope — which may be masking an indexing gap.
        /// On odd resolution around connector ends, check first whether the connector's owning namespace binds
        /// the name at all.</para>
        /// </summary>
        /// <param name="sourcePoco">The context relationship at the reference site.</param>
        /// <returns>The local scope, or <see langword="null" /> when the generic climb applies.</returns>
        private static INamespace QueryContextRelationshipLocalScope(IElement sourcePoco)
        {
            return sourcePoco switch
            {
                // Connector ends keep the containment climb — see the remark above. This case must precede
                // ISpecialization: a ReferenceSubsetting IS a Specialization and would otherwise be
                // re-anchored by the general rule below.
                IReferenceSubsetting { referencingFeature: { IsEnd: true, owningType: IConnector } } => null,
                ISpecialization specialization => specialization.owningType != null ? QueryOwningContainer(specialization.owningType) : QueryOwningContainer(specialization),
                IConjugation conjugation => conjugation.owningType != null ? QueryOwningContainer(conjugation.owningType) : QueryOwningContainer(conjugation),
                _ => null
            };
        }

        /// <summary>
        /// Tri-state result of probing one scope for one lexical form.
        /// </summary>
        private enum SimpleNameResolution
        {
            /// <summary>Name not bound in this scope — keep walking outward.</summary>
            NotBound,

            /// <summary>Name bound uniquely to the target — emit the simple name.</summary>
            Matched,

            /// <summary>Name bound to something else — stop; the qualified form is required.</summary>
            Shadowed,
        }

        /// <summary>
        /// Probes <paramref name="scope" />'s index for <paramref name="rawName" />. Mirrors the parser's
        /// local resolution: the local referencer and the self-binding entry are excluded, candidates are
        /// reduced to redefinition leaves (KerML §8.2.3.5.3 — at most one Membership per name), and the
        /// name matches only when that leaf set is exactly the target.
        /// </summary>
        /// <param name="scope">The scope whose index is inspected.</param>
        /// <param name="target">The element to look up.</param>
        /// <param name="rawName">The simple-name lexical form to probe; must be non-blank.</param>
        /// <param name="localRedefiner">Feature to exclude from the bucket, or <see langword="null"/>.</param>
        /// <param name="selfBindingScope">Scope whose binding of the target must be ignored, or <see langword="null"/>.</param>
        /// <returns>The resolution state.</returns>
        private SimpleNameResolution ResolveSimpleNameInScope(INamespace scope, IElement target, string rawName, IFeature localRedefiner, INamespace selfBindingScope)
        {
            var index = this.GetSimpleNameIndex(scope);

            if (!index.TryGetValue(rawName, out var elements))
            {
                return SimpleNameResolution.NotBound;
            }

            // The reference's own binding does not exist at parse time; only OTHER elements bound
            // under the name in this scope shadow the target.
            if (selfBindingScope != null && ReferenceEquals(scope, selfBindingScope))
            {
                var isBoundToOtherElement = elements.Any(element =>
                    !ReferenceEquals(element, target) && !ReferenceEquals(element, localRedefiner));

                return isBoundToOtherElement
                    ? SimpleNameResolution.Shadowed
                    : SimpleNameResolution.NotBound;
            }

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

            // Reduce to redefinition leaves: drop every candidate transitively redefined by another.
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
        /// Narrows <paramref name="candidates" /> to the directly-owned ones when every other candidate is
        /// only inherited into <paramref name="scope" /> — an owned feature shadows a same-named inherited
        /// one even when the redefinition is IMPLIED and absent from the XMI (SysML v2 spec, Clause 7.17.2).
        /// Applied at resolve time so a redefined feature stays nameable from its redefining declaration.
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

            var supertypes = QueryAllSupertypesSafe(scopeAsType);
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
        /// Determines whether <paramref name="element" /> is a member element of one of
        /// <paramref name="scope" />'s own memberships (declared, not imported or inherited).
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
        /// Determines whether <paramref name="sourcePoco" /> is the right-hand side of a chain accessor —
        /// a reference the parser resolves against the preceding segment's type instead of the lexical
        /// scope, so the bare simple name is the correct emission.
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

            // The FIRST segment is also a chain accessor when the owning chain Feature is the target
            // member of a construct establishing a RELATIVE namespace (see EstablishesRelativeNamespace).
            // A chain owned by a Specialization / ReferenceSubsetting keeps lexical resolution.
            return chainOwner.OwningRelationship is IMembership { OwningRelatedElement: { } chainMemberOwner } and not IParameterMembership
                   && EstablishesRelativeNamespace(chainMemberOwner);
        }

        /// <summary>
        /// Determines whether <paramref name="owner" /> establishes a RELATIVE namespace — a scope taken
        /// from a preceding expression's result rather than lexical containment. Mirrors the pilot's
        /// <c>NamespaceUtil.getRelativeNamespaceFor</c>: <see cref="IFeatureChainExpression"/> (always) and
        /// <see cref="IAssignmentActionUsage"/> (only with a target binding). The pilot's additional
        /// <c>!getArgument().isEmpty()</c> guard on the expression arm is deliberately NOT mirrored: our
        /// derived <c>argument</c> is empty for a FeatureChainExpression (its instantiated type is the
        /// library function <c>'.'</c>), so the guard would reject every chain.
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
        /// Determines whether <paramref name="sourcePoco" /> is the flow-feature reference of a
        /// <see cref="IFlowEnd" /> that carries a subsetting prefix (<c>'generate torque'.engineTorque</c>)
        /// — the parser resolves the feature against the prefix's type, like a chain accessor. Without the
        /// prefix the flow feature keeps lexical resolution.
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
        /// Eagerly indexes every namespace reachable from <paramref name="rootNamespace" /> via
        /// containment and imports, then the global namespaces (visible memberships only).
        /// </summary>
        /// <param name="rootNamespace">The root namespace.</param>
        /// <returns>The full structural cache.</returns>
        private Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>> BuildSimpleNameIndices(INamespace rootNamespace)
        {
            var result = new Dictionary<INamespace, IReadOnlyDictionary<string, HashSet<IElement>>>();
            var pending = new Queue<(INamespace Scope, bool IsGlobal)>();
            var visited = new HashSet<INamespace>();

            pending.Enqueue((rootNamespace, false));

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
        /// Populates <paramref name="index" /> with <paramref name="scope" />'s owned memberships and
        /// imports; imported namespaces are enqueued for their own indexing. When
        /// <paramref name="isGlobal" /> is set only VISIBLE entries are admitted (KerML §8.2.3.5.2).
        /// </summary>
        /// <param name="scope">The namespace whose entries are populated.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        /// <param name="isGlobal">Whether the scope is reached through the global namespace.</param>
        private void BuildOwnedAndImportedEntries(INamespace scope, Dictionary<string, HashSet<IElement>> index, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
        {
            var ownedMemberships = new List<IMembership>();

            try
            {
                foreach (var ownedMember in scope.ownedMembership.Where(ownedMember => IsVisibleWhenGlobal(ownedMember, isGlobal)))
                {
                    AddMembershipEntry(index, ownedMember, pending, isGlobal);
                    this.RecordAliasIfDeclared(scope, ownedMember);
                    ownedMemberships.Add(ownedMember);
                }
            }
            catch (NotSupportedException)
            {
                // ownedMembership may not be implemented; skip.
            }

            // Per Namespace::importedMemberships, an imported membership that has a distinguishability
            // collision with an OWNED membership is excluded. Snapshot the owned names first so a homonym
            // pulled in by `import Other::*` cannot shadow — or appear to compete with — the owned member.
            var ownedNames = new HashSet<string>(index.Keys, StringComparer.Ordinal);

            try
            {
                foreach (var ownedImport in scope.ownedImport.Where(ownedImport => IsVisibleWhenGlobal(ownedImport, isGlobal)))
                {
                    switch (ownedImport)
                    {
                        case IMembershipImport { ImportedMembership: { } importedMembership }
                            when !CollidesWithOwnedMembership(importedMembership, ownedNames, ownedMemberships):

                            AddMembershipEntry(index, importedMembership, pending, isGlobal);
                            this.RecordAliasIfDeclared(scope, importedMembership);

                            // Enqueue the member's owning namespace so its import chain (e.g.
                            // SI → ISQ → ISQBase) populates directFacadeIndex.
                            if (QueryOwningContainer(importedMembership.MemberElement) is { } memberOwner)
                            {
                                pending.Enqueue((memberOwner, isGlobal));
                            }

                            break;
                        case INamespaceImport { ImportedNamespace: not null } namespaceImport:
                        {
                            pending.Enqueue((namespaceImport.ImportedNamespace, isGlobal));
                            this.RecordDirectFacade(namespaceImport.ImportedNamespace, scope);

                            // Own try/catch so one broken imported namespace does not lose the
                            // remaining imports of this scope.
                            try
                            {
                                foreach (var importedMember in QueryVisibleMemberships(namespaceImport.ImportedNamespace, namespaceImport.IsImportAll, isGlobal, [scope])
                                             .Where(importedMember => !CollidesWithOwnedMembership(importedMember, ownedNames, ownedMemberships)))
                                {
                                    AddMembershipEntry(index, importedMember, pending, isGlobal);

                                    // An imported alias is reachable by its alias name in the
                                    // IMPORTING scope, so record it against `scope`.
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
        /// Enumerates the memberships an <c>import ns::*</c> contributes, following RE-EXPORTS
        /// transitively per <c>NamespaceImport::importedMemberships()</c> → <c>visibleMemberships()</c>:
        /// the owned memberships plus, recursively, whatever the namespace's own PUBLIC imports bring in.
        /// <paramref name="excluded"/> terminates import cycles.
        /// <para>Per <c>visibleMemberships(excluded, isRecursive, includeAll)</c> the visibility filter is
        /// driven by the IMPORT's <c>isImportAll</c> — <c>membershipsOfVisibility(public, …)</c> unless it is
        /// set — not by the importing scope. Each re-exported import contributes under its OWN
        /// <c>isImportAll</c>.</para>
        /// </summary>
        /// <param name="importedNamespace">The namespace named by the import.</param>
        /// <param name="includeAll">The triggering import's <c>isImportAll</c>: admits non-public memberships.</param>
        /// <param name="isGlobal">Whether the importing scope is reached through the global namespace, which admits only visible memberships regardless of <paramref name="includeAll"/> (KerML §8.2.3.5.2).</param>
        /// <param name="excluded">Namespaces already visited on this import chain.</param>
        /// <returns>The memberships contributed to the importing scope.</returns>
        private static IEnumerable<IMembership> QueryVisibleMemberships(INamespace importedNamespace, bool includeAll, bool isGlobal, HashSet<INamespace> excluded)
        {
            if (importedNamespace == null || !excluded.Add(importedNamespace))
            {
                yield break;
            }

            var publicOnly = !includeAll || isGlobal;
            List<IMembership> ownedMemberships;

            try
            {
                ownedMemberships = [..importedNamespace.ownedMembership.Where(ownedMember => !publicOnly || ownedMember.Visibility == VisibilityKind.Public)];
            }
            catch (NotSupportedException)
            {
                ownedMemberships = [];
            }

            foreach (var ownedMembership in ownedMemberships)
            {
                yield return ownedMembership;
            }

            List<IImport> reExports;

            try
            {
                reExports = [..importedNamespace.ownedImport.Where(ownedImport => ownedImport.Visibility == VisibilityKind.Public)];
            }
            catch (NotSupportedException)
            {
                reExports = [];
            }

            foreach (var reExport in reExports)
            {
                switch (reExport)
                {
                    case IMembershipImport { ImportedMembership: { } reExportedMembership }:
                        yield return reExportedMembership;
                        break;

                    case INamespaceImport { ImportedNamespace: { } reExportedNamespace } reExportedImport:

                        foreach (var reExportedMembership in QueryVisibleMemberships(reExportedNamespace, reExportedImport.IsImportAll, isGlobal, excluded))
                        {
                            yield return reExportedMembership;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Records an <c>alias X for Y;</c> binding — a membership whose explicit
        /// <see cref="IMembership.MemberName"/> / <see cref="IMembership.MemberShortName"/> differs from
        /// the member element's own names.
        /// </summary>
        /// <param name="scope">The <see cref="INamespace"/> declaring the membership.</param>
        /// <param name="membership">The candidate alias <see cref="IMembership"/>; may be <see langword="null"/>.</param>
        private void RecordAliasIfDeclared(INamespace scope, IMembership membership)
        {
            if (membership is not { MemberElement: { } target })
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
        /// Attempts to emit an in-scope alias for <paramref name="target"/>. Each candidate is validated
        /// through the same first-binding-wins scope walk as an ordinary simple name, so the emitted text
        /// always round-trips to the same element.
        /// </summary>
        /// <param name="chain">The source scope chain (innermost first).</param>
        /// <param name="target">The element being referenced.</param>
        /// <param name="sourcePoco">The reference site's source POCO — used to reject the alias declaration itself.</param>
        /// <param name="localRedefiner">Feature to exclude from the scope buckets, or <see langword="null"/>.</param>
        /// <param name="selfBindingScope">Scope whose binding of the target must be ignored, or <see langword="null"/>.</param>
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
                if (this.TryResolveSimpleNameAcrossChain(chain, target, aliasName, Escape(aliasName), localRedefiner, selfBindingScope, out matched))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether <paramref name="sourcePoco"/> is the very declaration introducing
        /// <paramref name="aliasName"/> for <paramref name="target"/> — which must emit the target's own
        /// name, not the circular <c>alias Torque for Torque;</c>.
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
        /// <paramref name="canonicalOwner"/>.
        /// </summary>
        /// <param name="canonicalOwner">The namespace being directly imported.</param>
        /// <param name="facade">The namespace importing it.</param>
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
        /// When <paramref name="isGlobal" /> is set, admits only PUBLIC memberships and imports — the
        /// global namespace contains only the visible memberships of other roots (KerML §8.2.3.5.2), so a
        /// name bound privately there would not re-parse. Within the model itself everything is visible.
        /// </summary>
        /// <param name="relationship">The <see cref="IMembership" /> or <see cref="IImport" /> considered.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global namespace.</param>
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
        /// Indexes the entries inherited from <paramref name="type" />'s transitive supertypes. Deliberately
        /// bypasses the <c>RemoveRedefinedFeatures</c> filter so <c>:&gt;&gt;</c> references stay reachable;
        /// namespace supertypes are enqueued as scopes in their own right.
        /// </summary>
        /// <param name="type">The type whose inherited memberships are indexed.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global namespace.</param>
        private static void BuildInheritedEntries(IType type, Dictionary<string, HashSet<IElement>> index, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
        {
            var inheritableSupertypes = QueryAllSupertypesSafe(type)
                .OfType<IType>()
                .Where(candidate => !ReferenceEquals(candidate, type))
                .ToList();

            foreach (var supertypeAsNamespace in inheritableSupertypes.OfType<INamespace>())
            {
                pending.Enqueue((supertypeAsNamespace, isGlobal));
            }

            var featuresRedefinedByOwned = QueryFeaturesRedefinedByOwnedFeatures(type);

            foreach (var supertype in inheritableSupertypes)
            {
                try
                {
                    foreach (var ownedMember in supertype.ownedMembership
                                 .Where(ownedMember => IsVisibleWhenGlobal(ownedMember, isGlobal))
                                 .Where(ownedMember => !IsRedefinedAway(ownedMember, featuresRedefinedByOwned)))
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
        /// Collects the features directly redefined by <paramref name="type" />'s owned features — the
        /// <c>ownedFeature.redefinition.redefinedFeature</c> set of <c>removeRedefinedFeatures</c>.
        /// </summary>
        /// <param name="type">The type whose owned redefinitions are collected.</param>
        /// <returns>The redefined features; empty when unavailable.</returns>
        private static HashSet<IElement> QueryFeaturesRedefinedByOwnedFeatures(IType type)
        {
            try
            {
                return [..type.ownedFeature
                    .SelectMany(ownedFeature => ownedFeature.OwnedRelationship.OfType<IRedefinition>())
                    .Select(redefinition => (IElement)redefinition.RedefinedFeature)
                    .Where(redefined => redefined != null)];
            }
            catch (NotSupportedException)
            {
                return [];
            }
        }

        /// <summary>
        /// Applies condition 2 of <c>Type::removeRedefinedFeatures</c>: an inherited membership drops out of
        /// the local scope when its member element — or anything that element redefines — is redefined by an
        /// owned feature of the inheriting type. The redefinition's own target is still reachable through the
        /// §8.2.3.5.1 supertype scope (see <see cref="QueryRedefinedFeatureScope" />).
        /// </summary>
        /// <param name="membership">The candidate inherited membership.</param>
        /// <param name="featuresRedefinedByOwned">Features redefined by the inheriting type's owned features.</param>
        /// <returns><see langword="true" /> when the membership must not be indexed.</returns>
        private static bool IsRedefinedAway(IMembership membership, HashSet<IElement> featuresRedefinedByOwned)
        {
            if (featuresRedefinedByOwned.Count == 0 || membership.MemberElement is not IFeature memberFeature)
            {
                return false;
            }

            if (featuresRedefinedByOwned.Contains(memberFeature))
            {
                return true;
            }

            try
            {
                return memberFeature.AllRedefinedFeatures().Any(featuresRedefinedByOwned.Contains);
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        /// <summary>
        /// Indexes <paramref name="membership" />'s member element under both lexical forms — the
        /// membership's explicit name overrides when present, else the element's own names — and enqueues
        /// the element when it is itself a namespace.
        /// </summary>
        /// <param name="index">The destination index.</param>
        /// <param name="membership">The membership whose target is indexed.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global namespace.</param>
        private static void AddMembershipEntry(Dictionary<string, HashSet<IElement>> index, IMembership membership, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
        {
            if (membership is not { MemberElement: { } target })
            {
                return;
            }

            var (shortName, longName) = QueryMembershipNames(membership, target);

            AddIndexEntry(index, shortName, target);
            AddIndexEntry(index, longName, target);

            if (target is INamespace targetAsNamespace)
            {
                pending.Enqueue((targetAsNamespace, isGlobal));
            }
        }

        /// <summary>
        /// Returns the two lexical forms a membership binds: the membership's explicit name overrides when
        /// present, else the member element's own names.
        /// </summary>
        /// <param name="membership">The membership doing the binding.</param>
        /// <param name="target">The membership's member element.</param>
        /// <returns>The short and long lexical forms; either may be <see langword="null" />.</returns>
        private static (string ShortName, string LongName) QueryMembershipNames(IMembership membership, IElement target)
        {
            return (!string.IsNullOrWhiteSpace(membership.MemberShortName) ? membership.MemberShortName : target.shortName,
                    !string.IsNullOrWhiteSpace(membership.MemberName) ? membership.MemberName : target.name);
        }

        /// <summary>
        /// Determines whether an IMPORTED membership has a distinguishability collision with an owned
        /// membership of the importing scope, which <c>Namespace::importedMemberships</c> excludes. Without
        /// this the homonym competes with the owned member and forces a needlessly qualified reference
        /// (<c>'Model'::Definitions</c> instead of <c>Definitions</c>).
        /// <para>Delegates to <see cref="IMembership.IsDistinguishableFrom" />: a shared name is NOT
        /// sufficient, since two memberships remain distinguishable when neither member element's metaclass
        /// conforms to the other's. The name check is only a cheap pre-filter — differing names always imply
        /// distinguishable, so it can never reject a genuine collision.</para>
        /// </summary>
        /// <param name="membership">The candidate imported membership.</param>
        /// <param name="ownedNames">Names bound by the scope's owned memberships, used to pre-filter.</param>
        /// <param name="ownedMemberships">The scope's owned memberships, tested on a name hit.</param>
        /// <returns><see langword="true" /> when the imported membership must be excluded.</returns>
        private static bool CollidesWithOwnedMembership(IMembership membership, HashSet<string> ownedNames, List<IMembership> ownedMemberships)
        {
            if (ownedNames.Count == 0 || membership is not { MemberElement: { } target })
            {
                return false;
            }

            var (shortName, longName) = QueryMembershipNames(membership, target);

            var sharesName = (!string.IsNullOrWhiteSpace(shortName) && ownedNames.Contains(shortName))
                             || (!string.IsNullOrWhiteSpace(longName) && ownedNames.Contains(longName));

            if (!sharesName)
            {
                return false;
            }

            try
            {
                return ownedMemberships.Any(owned => !membership.IsDistinguishableFrom(owned));
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        /// <summary>
        /// Adds <paramref name="element" /> to <paramref name="index" /> under
        /// <paramref name="simpleName" /> when the name is non-blank.
        /// </summary>
        /// <param name="index">The destination index.</param>
        /// <param name="simpleName">The simple name to use as the index key.</param>
        /// <param name="element">The element to record.</param>
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
