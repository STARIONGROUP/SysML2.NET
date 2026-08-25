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
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Extensions;
    using SysML2.NET.Semantics.Implied;

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
        private readonly Dictionary<Guid, SourceScopeChain> sourceScopeChains
            = new ();

        /// <summary>
        /// Lazy cache: <c>(target.Id, sourceLocalScope.Id, matchFloorScope.Id)</c> → emitted string.
        /// </summary>
        private readonly Dictionary<(Guid TargetId, Guid SourceScopeId, Guid MatchFloorId), string> resolvedReferences
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
        /// Supplies the implied <c>Relationships</c> (KerML §8.4.2) that a model exported without them
        /// omits, so a name reachable only through one can still be shortened.
        /// </summary>
        private readonly IImpliedRelationshipProvider impliedRelationshipProvider;

        /// <summary>
        /// Elements of the resolution graph keyed by Id, built lazily on the first implied-general
        /// translation. Instance state, never shared: each writer context carries its own cache, so
        /// parallel writers cannot observe or pollute one another.
        /// </summary>
        private Dictionary<Guid, IElement> resolutionGraphElementsById;

        /// <summary>
        /// Initializes the cache and eagerly indexes every namespace reachable from
        /// <paramref name="rootNamespace" />.
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace" /> being serialized.</param>
        /// <param name="globalNamespaces">
        /// The other loaded root namespaces (model libraries), forming the global namespace per
        /// KerML §8.2.3.5.2. Optional — without them resolution falls back to longer, equally valid names.
        /// </param>
        /// <param name="impliedRelationshipProvider">
        /// The provider supplying the implied <c>Relationships</c> a model exported without them omits.
        /// Optional — when absent, resolution sees only the declared <c>Specializations</c>, so a name
        /// reachable ONLY through an implied one degrades to a longer, equally valid form.
        /// </param>
        public NameResolutionCache(INamespace rootNamespace, IEnumerable<INamespace> globalNamespaces = null, IImpliedRelationshipProvider impliedRelationshipProvider = null)
        {
            this.RootNamespace = rootNamespace ?? throw new ArgumentNullException(nameof(rootNamespace));

            this.globalNamespaces = globalNamespaces?
                .Where(candidate => candidate != null && !ReferenceEquals(candidate, rootNamespace))
                .Distinct()
                .ToList() ?? [];

            this.impliedRelationshipProvider = impliedRelationshipProvider ?? NullImpliedRelationshipProvider.Instance;

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
                        ? QueryShortQualifiedName(membership.MemberElement, sourcePoco)
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

            // The innermost scopes of the chain are SHADOW-ONLY when the reference sits in a FeatureValue
            // expression: the two readings of KerML §8.2.3.5.2 disagree about them, so a simple name matched
            // there would not resolve to this target under both. See QueryValueExpressionMatchFloor.
            var matchFloorScope = QueryValueExpressionMatchFloor(sourcePoco) ?? sourceLocalScope;

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

                // The exception REPLACES the local Namespace, so the elected general type is itself the
                // innermost scope a match may come from — no scope below it to hold shadow-only.
                matchFloorScope = redefinitionScope;
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
                return this.ResolveFresh(target, this.BuildReferenceSite(sourcePoco, sourceLocalScope, matchFloorScope, localReferencer), escapedName);
            }

            var cacheKey = (target.Id, sourceLocalScope?.Id ?? Guid.Empty, matchFloorScope?.Id ?? Guid.Empty);

            if (this.resolvedReferences.TryGetValue(cacheKey, out var cached))
            {
                return cached;
            }

            var resolved = this.ResolveFresh(target, this.BuildReferenceSite(sourcePoco, sourceLocalScope, matchFloorScope, localRedefiner: null), escapedName);
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

            // The declaring namespace need not be an ANCESTOR of the import owner. A SIBLING package under a
            // shared enclosing namespace is equally reachable without any import, because containment scoping
            // makes that enclosing namespace's members visible by simple name from within it — so the test is
            // whether the two chains INTERSECT, not whether one contains the other. Comparing only against the
            // import owner's own ancestors made every sibling fall through to the absolute self-contained path
            // (`'10a-Analysis'::VehicleDesignModel::Vehicle` from inside `'10a-Analysis'`), which is redundant
            // self-prefixing. Elements from a separate resource (a library) share no ancestor and still
            // correctly return false.
            for (var scope = importOwner; scope != null; scope = QueryOwningContainer(scope))
            {
                for (var candidate = declaringNamespace; candidate != null; candidate = QueryOwningContainer(candidate))
                {
                    if (ReferenceEquals(scope, candidate))
                    {
                        return true;
                    }
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
                if (namedAncestors[anchorIndex] is not INamespace anchor
                    || !this.BindsDirectly(anchor, target, targetSegment)
                    || !this.IsSuffixVisible(namedAncestors.Take(anchorIndex + 1).Append(target)))
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
        /// to <paramref name="target" />, VISIBLY — i.e. the target is nameable from outside that scope,
        /// which is what a qualified path through it requires (KerML §8.2.3.5.3).
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
                   && !string.IsNullOrWhiteSpace(segment)
                   && this.BindsVisibly(scope, rawName, target);
        }

        /// <summary>
        /// Returns the binding that <paramref name="sourcePoco" /> IS — a non-owning
        /// <see cref="IMembership"/> without a name override is the reference being emitted, and its
        /// binding does not exist yet at parse time. Its entry must be discounted in its own scope or
        /// every such reference would resolve trivially at depth 0.
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns>The self binding, or <see langword="null" /> when the source is not one.</returns>
        private static SelfBinding QuerySelfBinding(IElement sourcePoco)
        {
            return sourcePoco is IMembership membership and not IOwningMembership
                   && string.IsNullOrWhiteSpace(membership.MemberName)
                   && string.IsNullOrWhiteSpace(membership.MemberShortName)
                   && membership.OwningRelatedElement is INamespace bindingScope
                ? new SelfBinding(bindingScope, membership)
                : null;
        }

        /// <summary>
        /// Assembles the <see cref="ReferenceSite" /> for one reference: its scope chain plus the
        /// exclusions every probe of it must honour.
        /// </summary>
        /// <param name="sourcePoco">The POCO bearing the reference.</param>
        /// <param name="sourceLocalScope">The pre-computed local scope (may be <see langword="null" />).</param>
        /// <param name="matchFloorScope">The innermost scope a match may come from (may be <see langword="null" />).</param>
        /// <param name="localRedefiner">Feature to exclude from every bucket, or <see langword="null" />.</param>
        /// <returns>The reference site.</returns>
        private ReferenceSite BuildReferenceSite(IElement sourcePoco, INamespace sourceLocalScope, INamespace matchFloorScope, IFeature localRedefiner)
        {
            return new ReferenceSite(
                sourcePoco,
                this.GetSourceScopeChain(sourcePoco, sourceLocalScope, matchFloorScope),
                localRedefiner,
                QuerySelfBinding(sourcePoco));
        }

        /// <summary>
        /// First-time resolution: probes the target's own simple names (short first, per the SST
        /// convention), then aliases, then facade re-exports, then owner-chain ancestors as anchors for a
        /// partially-qualified suffix, and finally falls back to <see cref="IElement.qualifiedName" />.
        /// </summary>
        /// <param name="target">The referenced element.</param>
        /// <param name="site">The reference site: its scope chain and the exclusions that apply to every probe.</param>
        /// <param name="escapedName">The target's escaped raw <c>name</c>.</param>
        /// <returns>The resolved emission string.</returns>
        private string ResolveFresh(IElement target, ReferenceSite site, string escapedName)
        {
            var rawShortName = target.shortName;
            string escapedShortName = null;

            if (!string.IsNullOrWhiteSpace(rawShortName))
            {
                escapedShortName = Escape(rawShortName);

                if (this.TryResolveSimpleNameAcrossChain(site, target, rawShortName, escapedShortName, accept: null, out var matchedShort))
                {
                    return matchedShort;
                }
            }

            var rawName = target.name;

            if (!string.IsNullOrWhiteSpace(rawName)
                && this.TryResolveSimpleNameAcrossChain(site, target, rawName, escapedName, accept: null, out var matchedLong))
            {
                return matchedLong;
            }

            // An alias binds the target under a name it does not carry itself, so the probes above
            // can never find it. Preferred over facade/qualified forms — it is how the model names
            // the element at this site.
            if (this.TryResolveViaAlias(site, target, out var matchedAlias))
            {
                return matchedAlias;
            }

            // Facade re-export: `ISQ::mass` over `ISQBase::mass` — the SST canonical idiom
            // (KerML §8.2.3.5.4 leaves the choice open; both forms parse to the same element).
            if (this.TryResolveViaDirectFacade(site.Chain, target, escapedShortName, escapedName, out var matchedFacade))
            {
                return matchedFacade;
            }

            // Walk owner-chain ancestors outward; the first that resolves uniquely anchors a
            // partially-qualified suffix down to the target. The suffix is kept as ELEMENTS: every segment
            // of it is resolved by the parser with visible resolution, which has to be verified per hop.
            var pathDownToTarget = new Stack<IElement>();

            pathDownToTarget.Push(target);

            var descendant = target;
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
                    && this.IsSuffixVisible(pathDownToTarget)
                    && this.TryResolveSimpleNameAcrossChain(site, ancestor, ancestorRawName, ancestorSegment, this.BuildAnchorAcceptance(ancestor, descendant), out var matchedAnchor))
                {
                    var builder = new StringBuilder(matchedAnchor);

                    foreach (var segment in pathDownToTarget)
                    {
                        builder.Append("::");
                        builder.Append(QueryPreferredEscapedSegment(segment) ?? string.Empty);
                    }

                    return builder.ToString();
                }

                pathDownToTarget.Push(ancestor);

                descendant = ancestor;
                ancestor = QueryOwningContainer(ancestor);
            }

            var shortQualifiedName = QueryShortQualifiedName(target);

            return string.IsNullOrWhiteSpace(shortQualifiedName)
                ? target.qualifiedName ?? string.Empty
                : shortQualifiedName;
        }

        /// <summary>
        /// Determines whether every hop WITHIN <paramref name="pathDownToTarget" /> resolves visibly — the
        /// hop from the anchor into the path is checked separately, against whatever the anchor segment
        /// actually resolves to (see <see cref="BuildAnchorAcceptance" />).
        /// </summary>
        /// <param name="pathDownToTarget">The suffix elements, outermost first.</param>
        /// <returns><see langword="true" /> when the suffix re-resolves segment by segment.</returns>
        private bool IsSuffixVisible(IEnumerable<IElement> pathDownToTarget)
        {
            IElement predecessor = null;

            foreach (var segmentElement in pathDownToTarget)
            {
                if (predecessor != null)
                {
                    var rawName = QueryPreferredRawName(segmentElement);

                    if (predecessor is not INamespace predecessorScope
                        || string.IsNullOrWhiteSpace(rawName)
                        || !this.BindsVisibly(predecessorScope, rawName, segmentElement))
                    {
                        return false;
                    }
                }

                predecessor = segmentElement;
            }

            return true;
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
        private bool TryResolveViaDirectFacade(SourceScopeChain chain, IElement target, string escapedShortName, string escapedName, out string matched)
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

            // Starts at the match floor: a facade reachable only from a scope the pilot parser never
            // consults would emit a name that does not re-resolve there.
            for (var scopeDepth = chain.MatchFloor; scopeDepth < chain.Scopes.Count; scopeDepth++)
            {
                var scope = chain.Scopes[scopeDepth];
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

            // The facade re-exports the owner, but the target still has to be VISIBLE through it: a
            // non-public member, or one an `import all` pulled in without re-exporting, is not.
            var targetRawName = QueryPreferredRawName(target);

            if (string.IsNullOrWhiteSpace(targetRawName) || !this.BindsVisibly(bestFacade, targetRawName, target))
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
        /// Walks <paramref name="site" />'s scope chain innermost-out for a scope binding
        /// <paramref name="rawName" /> uniquely to <paramref name="target" />. A scope that binds the name
        /// to anything else stops the walk — the parser's resolution would already have claimed the name
        /// there.
        /// <para>Scopes BELOW the chain's match floor are consulted for shadowing only: a hit there is
        /// skipped and the walk continues outward, because the pilot parser does not consult them (see
        /// <see cref="QueryValueExpressionMatchFloor" />) and the emitted name has to resolve to the same
        /// element under both readings.</para>
        /// </summary>
        /// <param name="site">The reference site: its scope chain and the exclusions that apply to every probe.</param>
        /// <param name="target">The referenced element.</param>
        /// <param name="rawName">The simple-name lexical form to probe (may be blank).</param>
        /// <param name="escapedName">The escaped form to emit on a hit.</param>
        /// <param name="accept">Predicate deciding whether a bound element counts as the target, or <see langword="null" /> for reference identity.</param>
        /// <param name="matched">On a hit, the simple-name string to emit.</param>
        /// <returns><see langword="true" /> when the name resolves uniquely to the target.</returns>
        private bool TryResolveSimpleNameAcrossChain(ReferenceSite site, IElement target, string rawName, string escapedName, Func<IElement, bool> accept, out string matched)
        {
            matched = null;

            if (string.IsNullOrWhiteSpace(rawName))
            {
                return false;
            }

            for (var scopeDepth = 0; scopeDepth < site.Chain.Scopes.Count; scopeDepth++)
            {
                var resolution = this.ResolveSimpleNameInScope(site.Chain.Scopes[scopeDepth], target, rawName, site.LocalRedefiner, site.SelfBinding, accept);

                if (resolution == SimpleNameResolution.Shadowed)
                {
                    return false;
                }

                if (resolution == SimpleNameResolution.Matched && scopeDepth >= site.Chain.MatchFloor)
                {
                    matched = escapedName;
                    return true;
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
        /// <param name="matchFloorScope">The innermost scope a match may come from (may be <see langword="null" />).</param>
        /// <returns>The cached chain.</returns>
        private SourceScopeChain GetSourceScopeChain(IElement sourcePoco, INamespace sourceLocalScope, INamespace matchFloorScope)
        {
            if (sourcePoco == null)
            {
                return BuildChain(sourceLocalScope ?? this.RootNamespace, matchFloorScope);
            }

            if (this.sourceScopeChains.TryGetValue(sourcePoco.Id, out var cached))
            {
                return cached;
            }

            var chain = BuildChain(sourceLocalScope ?? this.RootNamespace, matchFloorScope);
            this.sourceScopeChains[sourcePoco.Id] = chain;

            return chain;
        }

        /// <summary>
        /// Materialises the <c>owningNamespace</c> chain from <paramref name="start" /> up to the root, and
        /// locates <paramref name="matchFloorScope" /> in it.
        /// </summary>
        /// <param name="start">The starting namespace.</param>
        /// <param name="matchFloorScope">The innermost scope a match may come from (may be <see langword="null" />).</param>
        /// <returns>The chain.</returns>
        private static SourceScopeChain BuildChain(INamespace start, INamespace matchFloorScope)
        {
            var scopes = new List<INamespace>();
            var current = start;

            while (current != null)
            {
                scopes.Add(current);
                current = QueryOwningContainer(current);
            }

            return new SourceScopeChain(scopes, QueryMatchFloorDepth(scopes, matchFloorScope));
        }

        /// <summary>
        /// Returns the depth in <paramref name="scopes" /> at which a match becomes admissible.
        /// </summary>
        /// <param name="scopes">The chain, innermost first.</param>
        /// <param name="matchFloorScope">The floor scope, or <see langword="null" /> for no floor.</param>
        /// <returns>The depth; 0 admits the whole chain.</returns>
        /// <remarks>
        /// The floor is reached by a CONTAINMENT climb while the chain is materialised through
        /// <c>owningNamespace</c>, so the floor is not guaranteed to sit on the chain — the invocation
        /// redirect of <see cref="QueryExpressionScope" /> can elect a Namespace the chain does not pass
        /// through. Falling back to depth 0 there would silently re-admit the very scopes the floor exists
        /// to exclude, so the floor's own CONTAINERS are tried next: the innermost of them that IS on the
        /// chain sits at or outside the floor, which keeps the constraint at least as strict as intended.
        /// Depth 0 is reached only when the two are genuinely unrelated.
        /// </remarks>
        private static int QueryMatchFloorDepth(List<INamespace> scopes, INamespace matchFloorScope)
        {
            var visited = new HashSet<INamespace>();

            for (var candidate = matchFloorScope; candidate != null && visited.Add(candidate); candidate = QueryParentNamespace(candidate))
            {
                var depth = scopes.IndexOf(candidate);

                if (depth >= 0)
                {
                    return depth;
                }
            }

            return 0;
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
            return QueryShortQualifiedName(element, sourcePoco: null);
        }

        /// <summary>
        /// As <see cref="QueryShortQualifiedName(IElement)" />, but stops the walk at the first namespace that
        /// also encloses <paramref name="sourcePoco" />.
        /// </summary>
        /// <remarks>
        /// A namespace that encloses the reference site is already in scope by containment, so naming it
        /// explicitly is redundant self-prefixing: from inside <c>'10a-Analysis'</c> the pilot writes
        /// <c>import VehicleDesignModel::Vehicle</c>, not
        /// <c>import '10a-Analysis'::VehicleDesignModel::Vehicle</c>. An element in a separate resource (a
        /// library) shares no enclosing namespace with the source, so its path stays fully qualified —
        /// <c>SI::kg</c> is unaffected.
        /// </remarks>
        /// <param name="element">The leaf <see cref="IElement" /> to qualify; must be non-null.</param>
        /// <param name="sourcePoco">The reference site, or <see langword="null" /> to always walk to the root.</param>
        /// <returns>The short-form qualified name, relative to the nearest shared enclosing namespace.</returns>
        private static string QueryShortQualifiedName(IElement element, IElement sourcePoco)
        {
            var enclosingScopes = new List<IElement>();

            // An Import is owned as a RELATIONSHIP, so its owningNamespace does not resolve — the chain has
            // to be entered through OwningRelatedElement, exactly as IsReachableByContainment does.
            var origin = sourcePoco is IImport { OwningRelatedElement: { } importOwner } ? importOwner : sourcePoco;

            for (var scope = origin; scope != null; scope = QueryOwningContainer(scope))
            {
                enclosingScopes.Add(scope);
            }

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

                if (current != null && enclosingScopes.Any(scope => ReferenceEquals(scope, current)))
                {
                    break;
                }
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
        /// Returns the innermost scope of a reference's chain that may produce a MATCH, or
        /// <see langword="null" /> when every scope of the chain may.
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <returns>The floor scope, or <see langword="null" />.</returns>
        /// <remarks>
        /// KerML §8.2.3.5.2 anchors a <see cref="IMembership" /> inside a
        /// <see cref="IFeatureReferenceExpression" /> at the "non-invocation Namespace" — the nearest
        /// containing Namespace that is neither an expression nor a parameter of one. For a
        /// <see cref="IFeatureValue" /> that is the value-carrying Feature ITSELF, so its inherited members
        /// are in scope. The pilot parser (<c>NamespaceUtil.getNonExpressionNamespaceFor</c>) steps one
        /// scope FURTHER out whenever the climb passes a FeatureValue, so those inherited members are NOT.
        /// <para>The disagreement is observable: <c>part :>> subsystemA = subsystem1;</c> resolves under the
        /// spec reading (the redefining Feature inherits the variation's variant Memberships) but is a name
        /// resolution ERROR under the pilot's. This floor keeps such scopes in the chain as SHADOW sources
        /// while barring them from producing a match, so every name emitted resolves to the same element
        /// under both readings.</para>
        /// </remarks>
        private static INamespace QueryValueExpressionMatchFloor(IElement sourcePoco)
        {
            if (sourcePoco is not IMembership sourceMembership)
            {
                return null;
            }

            var subject = sourceMembership;
            var scope = QueryExpressionScope(subject);
            var visited = new HashSet<IMembership>();

            while (scope != null
                   && (subject is IFeatureValue || scope is IInstantiationExpression || scope is IFeatureReferenceExpression))
            {
                subject = QueryOwningMembershipSafe(scope);

                if (subject == null || !visited.Add(subject))
                {
                    break;
                }

                scope = QueryExpressionScope(subject);
            }

            return scope;
        }

        /// <summary>
        /// Returns the namespace containing <paramref name="membership" />, except for a
        /// <see cref="IFeatureValue" /> on a parameter of an <see cref="IInstantiationExpression" />, whose
        /// value expression is resolved against the invocation rather than against the parameter.
        /// </summary>
        /// <param name="membership">The membership whose containing scope is requested.</param>
        /// <returns>The scope, or <see langword="null" /> when the membership has none.</returns>
        private static INamespace QueryExpressionScope(IMembership membership)
        {
            var scope = QueryParentNamespace(membership);

            if (scope == null)
            {
                return null;
            }

            return membership is IFeatureValue && QueryOwningContainer(scope) is IInstantiationExpression invocation
                ? invocation
                : scope;
        }

        /// <summary>
        /// Returns the nearest <see cref="INamespace" /> containing <paramref name="element" />, by
        /// CONTAINMENT rather than by the derived <c>owningNamespace</c> — which is null for a Relationship.
        /// </summary>
        /// <param name="element">The element to climb from; may be <see langword="null" />.</param>
        /// <returns>The containing namespace, or <see langword="null" /> at the top of the containment tree.</returns>
        private static INamespace QueryParentNamespace(IElement element)
        {
            var visited = new HashSet<IElement>();

            for (var current = QueryContainer(element); current != null && visited.Add(current); current = QueryContainer(current))
            {
                if (current is INamespace containingNamespace)
                {
                    return containingNamespace;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the element CONTAINING <paramref name="element" />: the owning related element of a
        /// Relationship — whose <c>owner</c> is null, since it is owned as a relationship rather than as a
        /// member — and the <c>owner</c> of anything else.
        /// </summary>
        /// <param name="element">The element to climb from; may be <see langword="null" />.</param>
        /// <returns>The container, or <see langword="null" /> at the top of the containment tree.</returns>
        private static IElement QueryContainer(IElement element)
        {
            return element switch
            {
                null => null,
                IRelationship { OwningRelatedElement: { } owningRelatedElement } => owningRelatedElement,
                _ => QueryOwnerSafe(element),
            };
        }

        /// <summary>
        /// Returns <paramref name="element" />'s <c>owningMembership</c>, or <see langword="null" /> when
        /// unreachable or the derived property is not implemented.
        /// </summary>
        /// <param name="element">The element whose owning membership is requested; must be non-null.</param>
        /// <returns>The owning membership or <see langword="null" />.</returns>
        private static IMembership QueryOwningMembershipSafe(IElement element)
        {
            try
            {
                return element.owningMembership;
            }
            catch (NotSupportedException)
            {
                return null;
            }
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

            // A model exported without implied Relationships (KerML §8.4.2) omits Specializations the
            // abstract syntax requires — a variant Usage specializing its owning variation, for one. Without
            // them a redefinition of a member inherited THROUGH such a Specialization cannot shorten and
            // degrades to a fully qualified name. Appended last so declared supertypes keep priority.
            generalScopes.AddRange(this.impliedRelationshipProvider.GetImpliedSpecializations(owningType)
                .Select(specialization => this.TranslateToResolutionGraph(specialization.General))
                .OfType<INamespace>()
                .Where(general => !ReferenceEquals(general, owningType) && !generalScopes.Contains(general)));

            if (generalScopes.Count == 0)
            {
                return null;
            }

            var rawName = QueryPreferredRawName(target);

            if (string.IsNullOrWhiteSpace(rawName))
            {
                return generalScopes[0];
            }

            // Only a scope that actually binds the name can be the scope the redefinition's own binding
            // would have occupied. Electing one that does not — which became reachable once implied
            // Specializations joined the candidates — makes the caller treat the name as self-bound there
            // and walk past the scope that really holds it, ending in a needlessly qualified name.
            return generalScopes.FirstOrDefault(scope =>
                this.ResolveSimpleNameInScope(scope, target, rawName, localRedefiner: null, selfBinding: null) == SimpleNameResolution.Matched);
        }

        /// <summary>
        /// Determines the local <see cref="INamespace"/> from the KIND of context relationship, per
        /// KerML §8.2.3.5.2. Only the kinds whose local scope is NOT simply the nearest enclosing namespace
        /// are handled here; everything else falls back to the containment climb.
        /// <para>For a <see cref="ISpecialization"/> the spec anchors resolution at the
        /// <c>owningNamespace</c> of the <c>owningType</c> — one level OUT from the owning feature — so the
        /// owning feature's own and inherited members are NOT in scope.</para>
        /// <para>A <see cref="IReferenceSubsetting"/> whose <c>referencingFeature</c> is an end feature of a
        /// <see cref="IConnector"/> anchors at the CONNECTOR's owning namespace. That namespace inherits the
        /// referenced name only through IMPLIED Specializations, so this anchoring works only because the
        /// simple-name index folds implied generals in — translated into THIS graph first, since the implied
        /// layer may be wired against a separate library load whose instances never satisfy reference
        /// equality here. See <c>TranslateToResolutionGraph</c>; diagnosis in
        /// .team-notes/start-overqualification-diagnosis.md.</para>
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
                IReferenceSubsetting { referencingFeature: { IsEnd: true, owningType: IConnector connector } } => QueryOwningContainer(connector),
                ISpecialization specialization => specialization.owningType != null ? QueryOwningContainer(specialization.owningType) : QueryOwningContainer(specialization),
                IConjugation conjugation => conjugation.owningType != null ? QueryOwningContainer(conjugation.owningType) : QueryOwningContainer(conjugation),
                _ => null
            };
        }

        /// <summary>
        /// What stays fixed while one reference is resolved: the scopes to probe and the two exclusions
        /// that apply to every probe of it. Only the name being looked up varies.
        /// </summary>
        private sealed class ReferenceSite
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ReferenceSite"/> class.
            /// </summary>
            /// <param name="sourcePoco">The POCO bearing the reference.</param>
            /// <param name="chain">The scopes to probe, innermost first, with the match floor.</param>
            /// <param name="localRedefiner">Feature to exclude from every bucket, or <see langword="null" />.</param>
            /// <param name="selfBinding">The binding the reference itself is, or <see langword="null" />.</param>
            internal ReferenceSite(IElement sourcePoco, SourceScopeChain chain, IFeature localRedefiner, SelfBinding selfBinding)
            {
                this.SourcePoco = sourcePoco;
                this.Chain = chain;
                this.LocalRedefiner = localRedefiner;
                this.SelfBinding = selfBinding;
            }

            /// <summary>
            /// Gets the POCO bearing the reference.
            /// </summary>
            internal IElement SourcePoco { get; }

            /// <summary>
            /// Gets the scopes to probe, innermost first, with the depth at which a match is admissible.
            /// </summary>
            internal SourceScopeChain Chain { get; }

            /// <summary>
            /// Gets the Feature excluded from every bucket — the reference's own redefining or
            /// referencing Feature, which must not shadow its own target.
            /// </summary>
            internal IFeature LocalRedefiner { get; }

            /// <summary>
            /// Gets the binding the reference itself is, whose entry does not exist at parse time.
            /// </summary>
            internal SelfBinding SelfBinding { get; }
        }

        /// <summary>
        /// The name binding a reference IS: the Membership being emitted and the scope it binds in.
        /// </summary>
        private sealed class SelfBinding
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SelfBinding"/> class.
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

        /// <summary>
        /// A reference site's scope chain, innermost first, together with the depth at which a MATCH
        /// becomes admissible.
        /// </summary>
        private sealed class SourceScopeChain
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SourceScopeChain"/> class.
            /// </summary>
            /// <param name="scopes">The scopes, innermost first.</param>
            /// <param name="matchFloor">The index of the innermost scope a match may come from.</param>
            internal SourceScopeChain(IReadOnlyList<INamespace> scopes, int matchFloor)
            {
                this.Scopes = scopes;
                this.MatchFloor = matchFloor;
            }

            /// <summary>
            /// Gets the scopes of the chain, innermost first.
            /// </summary>
            internal IReadOnlyList<INamespace> Scopes { get; }

            /// <summary>
            /// Gets the index of the innermost scope a match may come from; scopes below it are consulted
            /// for shadowing only (see <see cref="QueryValueExpressionMatchFloor" />).
            /// </summary>
            internal int MatchFloor { get; }
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
        /// <param name="selfBinding">The binding the reference itself is, or <see langword="null"/>.</param>
        /// <param name="accept">Predicate deciding whether a bound element counts as the target, or <see langword="null"/> for reference identity.</param>
        /// <returns>The resolution state.</returns>
        private SimpleNameResolution ResolveSimpleNameInScope(INamespace scope, IElement target, string rawName, IFeature localRedefiner, SelfBinding selfBinding, Func<IElement, bool> accept = null)
        {
            var index = this.GetSimpleNameIndex(scope);

            if (!index.TryGetValue(rawName, out var elements))
            {
                return SimpleNameResolution.NotBound;
            }

            accept ??= candidate => ReferenceEquals(candidate, target);

            // The reference's own binding does not exist at parse time; only OTHER elements bound
            // under the name in this scope shadow the target.
            if (selfBinding != null && ReferenceEquals(scope, selfBinding.Scope))
            {
                var isBoundToOtherElement = elements.Any(element =>
                    !accept(element) && !ReferenceEquals(element, localRedefiner));

                if (isBoundToOtherElement)
                {
                    return SimpleNameResolution.Shadowed;
                }

                // The index is keyed by ELEMENT, so the reference's own entry is indistinguishable from
                // one the scope holds anyway — inherited, imported or aliased. Only the former is absent
                // at parse time: when another Membership of this scope binds the same name to the same
                // element, the parser resolves the simple name here and the bare form is correct.
                return this.BindsTargetIndependently(scope, rawName, target, selfBinding.Membership)
                    ? SimpleNameResolution.Matched
                    : SimpleNameResolution.NotBound;
            }

            var candidates = elements.Where(element => !ReferenceEquals(element, localRedefiner)).ToList();

            if (candidates.Count == 0)
            {
                return SimpleNameResolution.NotBound;
            }

            if (candidates.Count == 1)
            {
                return accept(candidates[0])
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

            return leafCount == 1 && accept(onlyLeaf)
                ? SimpleNameResolution.Matched
                : SimpleNameResolution.Shadowed;
        }

        /// <summary>
        /// Determines whether <paramref name="scope" /> binds <paramref name="rawName" /> to
        /// <paramref name="target" /> through a Membership OTHER than <paramref name="selfMembership" />,
        /// the reference being emitted.
        /// </summary>
        /// <param name="scope">The scope to inspect.</param>
        /// <param name="rawName">The simple-name lexical form.</param>
        /// <param name="target">The referenced element.</param>
        /// <param name="selfMembership">The Membership that IS the reference.</param>
        /// <returns><see langword="true" /> when the binding survives without the reference itself.</returns>
        /// <remarks>
        /// Answers the question the element-keyed index cannot: <c>first start;</c> declares a Membership
        /// naming the library <c>Actions::Action::start</c> in a scope that ALREADY inherits that same
        /// element through an implied Specialization, so the parser resolves the bare name — while
        /// <c>member Foo::bar;</c> in a scope with no other binding for <c>bar</c> does not.
        /// <para>Deliberately a live query rather than index provenance: it runs only when the probed scope
        /// is the reference's own AND the name is bound there to nothing else, which is rare. It mirrors
        /// the sources of <see cref="BuildOwnedAndImportedEntries" /> and
        /// <see cref="BuildInheritedEntries" />; a source it fails to cover only costs a longer name, never
        /// an invalid one.</para>
        /// </remarks>
        private bool BindsTargetIndependently(INamespace scope, string rawName, IElement target, IMembership selfMembership)
        {
            return this.QueryBindingMemberships(scope, visibleOnly: false)
                .Any(membership => !ReferenceEquals(membership, selfMembership) && BindsName(membership, rawName, target));
        }

        /// <summary>
        /// Determines whether <paramref name="scope" /> binds <paramref name="rawName" /> to
        /// <paramref name="target" /> among its VISIBLE Memberships — the test the parser applies to every
        /// segment of a qualified name after the first.
        /// </summary>
        /// <param name="scope">The scope named by the preceding segment.</param>
        /// <param name="rawName">The segment's simple-name lexical form.</param>
        /// <param name="target">The element the segment must name.</param>
        /// <returns><see langword="true" /> when the segment resolves visibly to the target.</returns>
        /// <remarks>
        /// <c>Namespace::resolve</c> resolves the FIRST segment with <c>resolveLocal</c> — the outward climb
        /// over owned, imported and inherited Memberships of ANY visibility — but every following segment
        /// with <c>resolveVisible</c>, i.e. <c>visibleMemberships(Set{}, false, false)</c>, which is public
        /// only (KerML §8.2.3.5.3). The simple-name index cannot answer this: it is built with the
        /// visibility filter of the path that REACHED the scope, and within the model that admits
        /// everything. Emitting <c>A::b</c> for a <c>b</c> that is private in <c>A</c> would produce a name
        /// no conformant parser resolves.
        /// </remarks>
        private bool BindsVisibly(INamespace scope, string rawName, IElement target)
        {
            return this.QueryBindingMemberships(scope, visibleOnly: true)
                .Any(membership => BindsName(membership, rawName, target));
        }

        /// <summary>
        /// Enumerates the Memberships that give <paramref name="scope" /> its name bindings: owned,
        /// imported, inherited, and those contributed by implied generals.
        /// </summary>
        /// <param name="scope">The scope whose bindings are collected.</param>
        /// <param name="visibleOnly">Whether to keep only the bindings visible OUTSIDE the scope.</param>
        /// <returns>The Memberships, with duplicates possible.</returns>
        private List<IMembership> QueryBindingMemberships(INamespace scope, bool visibleOnly)
        {
            var memberships = new List<IMembership>(QueryOwnedMembershipsSafe(scope).Where(ownedMember => PassesVisibilityFilter(ownedMember, visibleOnly)));

            memberships.AddRange(QueryImportedMembershipsSafe(scope, visibleOnly));

            if (scope is not IType type)
            {
                return memberships;
            }

            memberships.AddRange(QueryInheritedMembershipsSafe(type).Where(inheritedMember => PassesVisibilityFilter(inheritedMember, visibleOnly)));

            var declaredSupertypes = QueryAllSupertypesSafe(type)
                .Where(supertype => !ReferenceEquals(supertype, type))
                .ToList();

            foreach (var impliedGeneral in this.QueryImpliedGeneralClosure(type, declaredSupertypes))
            {
                memberships.AddRange(QueryOwnedMembershipsSafe(impliedGeneral)
                    .Where(ownedMember => ownedMember.Visibility != VisibilityKind.Private && PassesVisibilityFilter(ownedMember, visibleOnly)));

                memberships.AddRange(QueryInheritedMembershipsSafe(impliedGeneral).Where(inheritedMember => PassesVisibilityFilter(inheritedMember, visibleOnly)));
            }

            return memberships;
        }

        /// <summary>
        /// Determines whether <paramref name="membership" /> binds <paramref name="rawName" /> to
        /// <paramref name="target" />, under either lexical form.
        /// </summary>
        /// <param name="membership">The Membership to test.</param>
        /// <param name="rawName">The simple-name lexical form.</param>
        /// <param name="target">The referenced element.</param>
        /// <returns><see langword="true" /> when the membership binds the name to the target.</returns>
        private static bool BindsName(IMembership membership, string rawName, IElement target)
        {
            if (membership?.MemberElement == null || !ReferenceEquals(membership.MemberElement, target))
            {
                return false;
            }

            var (shortName, longName) = QueryMembershipNames(membership, target);

            return string.Equals(shortName, rawName, StringComparison.Ordinal)
                   || string.Equals(longName, rawName, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns <paramref name="scope" />'s <c>ownedMembership</c>, or an empty list when the derived
        /// property is not implemented.
        /// </summary>
        /// <param name="scope">The scope to query; must be non-null.</param>
        /// <returns>The owned memberships, possibly empty.</returns>
        private static List<IMembership> QueryOwnedMembershipsSafe(INamespace scope)
        {
            try
            {
                return scope.ownedMembership;
            }
            catch (NotSupportedException)
            {
                return [];
            }
        }

        /// <summary>
        /// Returns <paramref name="type" />'s <c>inheritedMembership</c>, or an empty list when the derived
        /// property is not implemented.
        /// </summary>
        /// <param name="type">The type to query; must be non-null.</param>
        /// <returns>The inherited memberships, possibly empty.</returns>
        private static List<IMembership> QueryInheritedMembershipsSafe(IType type)
        {
            try
            {
                return type.inheritedMembership;
            }
            catch (NotSupportedException)
            {
                return [];
            }
        }

        /// <summary>
        /// Returns the Memberships <paramref name="scope" />'s own Imports contribute, mirroring
        /// <see cref="BuildOwnedAndImportedEntries" /> minus its collision filter — a colliding import
        /// cannot be the INDEPENDENT binding anyway, since the owned member it collides with is.
        /// </summary>
        /// <param name="scope">The importing scope; must be non-null.</param>
        /// <param name="visibleOnly">Whether to keep only PUBLIC imports, the ones that re-export.</param>
        /// <returns>The imported memberships, possibly empty.</returns>
        private static List<IMembership> QueryImportedMembershipsSafe(INamespace scope, bool visibleOnly)
        {
            var imported = new List<IMembership>();

            try
            {
                foreach (var ownedImport in scope.ownedImport.Where(ownedImport => PassesVisibilityFilter(ownedImport, visibleOnly)))
                {
                    switch (ownedImport)
                    {
                        case IMembershipImport { ImportedMembership: { } importedMembership }:
                            imported.Add(importedMembership);
                            break;
                        case INamespaceImport { ImportedNamespace: not null } namespaceImport:
                            imported.AddRange(QueryVisibleMemberships(namespaceImport.ImportedNamespace, namespaceImport.IsImportAll, false, [scope]));
                            break;
                    }
                }
            }
            catch (NotSupportedException)
            {
                // ownedImport, or a derivation behind one of the imported namespaces, is not implemented.
            }

            return imported;
        }

        /// <summary>
        /// Builds the acceptance predicate for an ancestor ANCHOR in a partially-qualified name: the anchor
        /// segment need not resolve to <paramref name="ancestor" /> itself, as long as what it resolves to
        /// binds the next segment to the same <paramref name="descendant" />.
        /// </summary>
        /// <param name="ancestor">The owner-chain ancestor being probed as an anchor.</param>
        /// <param name="descendant">The element named by the segment immediately below the anchor.</param>
        /// <returns>The predicate.</returns>
        /// <remarks>
        /// A feature that redefines a Type binds that Type's members by inheritance, so it anchors a path
        /// through them exactly as the Type does — <c>subsystemA::subsystem1</c> where <c>subsystemA</c>
        /// resolves to the redefining <c>part :>> subsystemA</c> rather than to the variation it redefines.
        /// Identity of the anchor is therefore too strong a test; what matters is that the WHOLE path still
        /// resolves to the target, which is verified here one segment at a time.
        /// </remarks>
        private Func<IElement, bool> BuildAnchorAcceptance(IElement ancestor, IElement descendant)
        {
            var descendantRawName = QueryPreferredRawName(descendant);

            // Whatever the anchor segment resolves to is what the parser applies visible resolution to for
            // the next segment, so the check runs against the CANDIDATE — for the anchor itself as much as
            // for a Feature that redefines it.
            return candidate => candidate is INamespace candidateScope
                                && !string.IsNullOrWhiteSpace(descendantRawName)
                                && this.BindsVisibly(candidateScope, descendantRawName, descendant)
                                && (ReferenceEquals(candidate, ancestor)
                                    || this.ResolveSimpleNameInScope(candidateScope, descendant, descendantRawName, localRedefiner: null, selfBinding: null) == SimpleNameResolution.Matched);
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
                foreach (var ownedMember in scope.ownedMembership.Where(ownedMember => PassesVisibilityFilter(ownedMember, isGlobal)))
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
                foreach (var ownedImport in scope.ownedImport.Where(ownedImport => PassesVisibilityFilter(ownedImport, isGlobal)))
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

                            // Only a PUBLIC import re-exports what it brings in, so only a public one makes
                            // the importing namespace a usable facade for it (KerML §8.2.3.5.3).
                            if (namespaceImport.Visibility == VisibilityKind.Public)
                            {
                                this.RecordDirectFacade(namespaceImport.ImportedNamespace, scope);
                            }

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
        /// <param name="site">The reference site; its source POCO rejects the alias declaration itself.</param>
        /// <param name="target">The element being referenced.</param>
        /// <param name="matched">On a hit, the escaped alias name to emit.</param>
        /// <returns><see langword="true"/> when an unambiguous in-scope alias was found.</returns>
        private bool TryResolveViaAlias(ReferenceSite site, IElement target, out string matched)
        {
            matched = null;

            var candidateAliasNames = site.Chain.Scopes
                .Where(scope => this.aliasIndex.ContainsKey(scope))
                .SelectMany(scope => this.aliasIndex[scope].TryGetValue(target, out var aliasNames) ? aliasNames : Enumerable.Empty<string>())
                .Distinct(StringComparer.Ordinal)
                .Where(aliasName => !DeclaresAlias(site.SourcePoco, target, aliasName));

            foreach (var aliasName in candidateAliasNames)
            {
                if (this.TryResolveSimpleNameAcrossChain(site, target, aliasName, Escape(aliasName), accept: null, out matched))
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
        /// When <paramref name="publicOnly" /> is set, admits only PUBLIC memberships and imports — the
        /// filter both the global namespace and visible resolution apply. The global namespace contains
        /// only the visible memberships of other roots, and every segment of a qualified name after the
        /// first resolves against the visible memberships of the preceding one (KerML §8.2.3.5.2–.3), so a
        /// name bound privately there would not re-parse. Within a local scope everything is visible.
        /// </summary>
        /// <param name="relationship">The <see cref="IMembership" /> or <see cref="IImport" /> considered.</param>
        /// <param name="publicOnly">Whether only bindings visible OUTSIDE the owning scope are admitted.</param>
        /// <returns><see langword="true" /> when the relationship may contribute a binding.</returns>
        private static bool PassesVisibilityFilter(IRelationship relationship, bool publicOnly)
        {
            if (!publicOnly)
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
        /// Indexes the entries inherited from <paramref name="type" />'s transitive supertypes; namespace
        /// supertypes are enqueued as scopes in their own right.
        /// <para>
        /// Membership indexing delegates to <c>Type::inheritedMembership</c> (KerML §8.3.3.1.10) rather than
        /// re-deriving it. An earlier flattened walk applied <c>removeRedefinedFeatures</c> condition 2 at
        /// the leaf type only — so a membership an intermediate supertype redefined away still reached the
        /// index — and it admitted <c>private</c> supertype members while missing their
        /// <c>public</c>/<c>protected</c> imports. Delegating is spec-faithful on all three counts.
        /// </para>
        /// <para>
        /// The delegation had previously been backed out for COST, when <c>inheritedMembership</c>
        /// recomputed the transitive closure on every access and took the validation fixture from 17 s to
        /// over 4 minutes. The per-query memoisation since added to <c>TypeExtensions</c> removes that
        /// blow-up: the same fixture now runs in 40 s against 18 s for the flattened walk, measured
        /// back-to-back. That remaining ~2x is the price of spec fidelity, and the corpus output is
        /// unchanged by the switch.
        /// </para>
        /// </summary>
        /// <param name="type">The type whose inherited memberships are indexed.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="pending">Queue of namespaces yet to be indexed.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global namespace.</param>
        private void BuildInheritedEntries(IType type, Dictionary<string, HashSet<IElement>> index, Queue<(INamespace Scope, bool IsGlobal)> pending, bool isGlobal)
        {
            var inheritableSupertypes = QueryAllSupertypesSafe(type)
                .OfType<IType>()
                .Where(candidate => !ReferenceEquals(candidate, type))
                .ToList();

            foreach (var supertypeAsNamespace in inheritableSupertypes.OfType<INamespace>())
            {
                pending.Enqueue((supertypeAsNamespace, isGlobal));
            }

            List<IMembership> inheritedMemberships;

            try
            {
                inheritedMemberships = type.inheritedMembership;
            }
            catch (NotSupportedException)
            {
                // Resolving inheritance is atomic: a derivation that is unimplemented ANYWHERE in this
                // Type's transitive supertype closure costs the whole closure, not just the branch that
                // raised. Names that would have resolved through an unaffected supertype then fall back
                // to a longer — never an invalid — form.
                return;
            }

            foreach (var inheritedMember in inheritedMemberships
                         .Where(inheritedMember => PassesVisibilityFilter(inheritedMember, isGlobal)))
            {
                AddMembershipEntry(index, inheritedMember, pending, isGlobal);
            }

            // Implied Specializations are DETACHED — the layer computing them never touches
            // ownedRelationship, so inheritedMembership above cannot see them and a name inherited ONLY
            // through an implied general never reaches the index. Resolution then walks past the scope that
            // really binds the name and emits a needlessly qualified form.
            //
            // These entries are LOOKUP-ONLY. The implied general is deliberately NOT enqueued as a scope:
            // `pending` drives traversal into further namespaces, and an implied general is a library Type,
            // so enqueueing it would drag the model libraries into the walk. Only the members it contributes
            // are indexed, so nothing here can reach the writer.
            foreach (var impliedGeneral in this.QueryImpliedGeneralClosure(type, inheritableSupertypes))
            {
                // `pending` feeds INDEX construction only — it is not the traversal that emits output — so
                // indexing the general as a scope in its own right keeps the fix lookup-only while making
                // the names it owns resolvable.
                pending.Enqueue((impliedGeneral, isGlobal));

                AddImpliedLookupEntries(impliedGeneral, index, isGlobal);
            }
        }

        /// <summary>
        /// Translates a Type produced by the implied-relationship layer into this cache's OWN object graph.
        /// </summary>
        /// <param name="impliedGeneral">The general of an implied Specialization, possibly from a foreign graph.</param>
        /// <returns>The same-Id Type of the resolution graph, or <c>null</c> when the graph does not carry it.</returns>
        /// <remarks>
        /// The implied layer may be wired against a SEPARATE library load — a full, model-independent one —
        /// so the generals it returns can be different POCO instances than the ones this cache resolves
        /// against, even for the same library element (same <c>Id</c>). Indexing a foreign instance is
        /// worse than useless: it can never equal a resolution target by reference, so it answers
        /// <c>Shadowed</c> and STOPS the outward walk that would otherwise have found the local instance.
        /// Translating by Id keeps reference equality authoritative everywhere else. A general the
        /// resolution graph does not carry is dropped: its members can never be targets here.</remarks>
        private IType TranslateToResolutionGraph(IType impliedGeneral)
        {
            if (impliedGeneral == null)
            {
                return null;
            }

            this.resolutionGraphElementsById ??= this.BuildResolutionGraphIndex();

            if (this.resolutionGraphElementsById.TryGetValue(impliedGeneral.Id, out var local))
            {
                return local as IType;
            }

            // The general may belong to THIS graph already — a hand-coded rule computing against the model
            // itself returns resolution-graph instances, which the containment walk below indexes only for
            // library namespaces.
            return this.IsInResolutionGraph(impliedGeneral) ? impliedGeneral : null;
        }

        /// <summary>
        /// Builds the by-Id index of every Element reachable from the global namespaces.
        /// </summary>
        /// <returns>The index.</returns>
        private Dictionary<Guid, IElement> BuildResolutionGraphIndex()
        {
            var elementsById = new Dictionary<Guid, IElement>();
            var pendingElements = new Queue<IElement>();

            foreach (var globalNamespace in this.globalNamespaces)
            {
                pendingElements.Enqueue(globalNamespace);
            }

            while (pendingElements.Count > 0)
            {
                var current = pendingElements.Dequeue();

                // First-wins on a duplicate Id: distinct libraries carry unique Ids, so a collision only
                // occurs when the same library is loaded twice, and the copies are then interchangeable.
                if (!elementsById.TryAdd(current.Id, current))
                {
                    continue;
                }

                foreach (var owned in current.OwnedRelationship.SelectMany(relationship => relationship.OwnedRelatedElement))
                {
                    pendingElements.Enqueue(owned);
                }
            }

            return elementsById;
        }

        /// <summary>
        /// Asserts whether an Element belongs to this cache's own graph, by walking its owners to a known root.
        /// </summary>
        /// <param name="element">The Element to test.</param>
        /// <returns>True when an owner chain reaches the root or a global namespace.</returns>
        private bool IsInResolutionGraph(IElement element)
        {
            for (var current = element; current != null; current = current.owner)
            {
                if (ReferenceEquals(current, this.RootNamespace) || this.globalNamespaces.Contains(current))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns every Type reachable from a Type or its declared supertypes through implied
        /// Specializations, transitively.
        /// </summary>
        /// <param name="type">The Type whose implied generals are collected.</param>
        /// <param name="declaredSupertypes">The declared supertypes, which carry implied Specializations of their own.</param>
        /// <returns>The implied generals, without duplicates.</returns>
        private List<IType> QueryImpliedGeneralClosure(IType type, List<IType> declaredSupertypes)
        {
            var visited = new HashSet<IType>();
            var pendingTypes = new Queue<IType>();

            pendingTypes.Enqueue(type);

            foreach (var declaredSupertype in declaredSupertypes)
            {
                pendingTypes.Enqueue(declaredSupertype);
            }

            var impliedGenerals = new List<IType>();

            while (pendingTypes.Count > 0)
            {
                var current = pendingTypes.Dequeue();

                foreach (var general in this.impliedRelationshipProvider.GetImpliedSpecializations(current)
                             .Select(specialization => this.TranslateToResolutionGraph(specialization.General))
                             .Where(general => general != null && visited.Add(general)))
                {
                    impliedGenerals.Add(general);
                    pendingTypes.Enqueue(general);
                }
            }

            return impliedGenerals;
        }

        /// <summary>
        /// Indexes, for lookup only, the members an implied general contributes.
        /// </summary>
        /// <param name="impliedGeneral">The Type reached through an implied Specialization.</param>
        /// <param name="index">The destination index.</param>
        /// <param name="isGlobal">Whether the owning scope is reached through the global namespace.</param>
        private static void AddImpliedLookupEntries(IType impliedGeneral, Dictionary<string, HashSet<IElement>> index, bool isGlobal)
        {
            if (impliedGeneral == null)
            {
                return;
            }

            // Stricter than the declared-supertype walk on purpose: an implied general is reached without
            // any authored relationship, so its private internals are never exposed, even in a non-global
            // scope where PassesVisibilityFilter alone would admit them.
            var contributed = new List<IMembership>(impliedGeneral.ownedMembership
                .Where(ownedMember => ownedMember.Visibility != VisibilityKind.Private));

            try
            {
                contributed.AddRange(impliedGeneral.inheritedMembership);
            }
            catch (NotSupportedException)
            {
                // Same atomicity as above: an unimplemented derivation costs this general's contribution.
            }

            foreach (var member in contributed.Where(member => PassesVisibilityFilter(member, isGlobal)))
            {
                AddLookupOnlyEntry(index, member);
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
        /// Indexes a Membership for name lookup WITHOUT extending the namespace traversal.
        /// </summary>
        /// <param name="index">The destination index.</param>
        /// <param name="membership">The Membership to index.</param>
        /// <remarks>
        /// The counterpart of <see cref="AddMembershipEntry" />, minus its <c>pending</c> enqueue. Used for
        /// members reached through an IMPLIED Specialization: they must be resolvable by name, but the
        /// library Types they come from must not be pulled into the walk that produces output.
        /// </remarks>
        private static void AddLookupOnlyEntry(Dictionary<string, HashSet<IElement>> index, IMembership membership)
        {
            if (membership is not { MemberElement: { } target })
            {
                return;
            }

            var (shortName, longName) = QueryMembershipNames(membership, target);

            AddIndexEntry(index, shortName, target);
            AddIndexEntry(index, longName, target);
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
