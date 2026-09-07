// -------------------------------------------------------------------------------------------------
// <copyright file="NameResolutionCache.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Semantics.Implied;

    /// <summary>
    /// Resolves the shortest unambiguous textual name for a reference, mirroring KerML §8.2.3.5.
    /// Holds an eager per-namespace simple-name index built on construction, plus lazy caches for
    /// source scope chains and resolved emissions keyed by <c>(target, sourceLocalScope)</c>.
    /// </summary>
    public sealed partial class NameResolutionCache
    {
        /// <summary>
        /// Supplies the implied <c>Relationships</c> (KerML §8.4.2) that a model exported without them
        /// omits, so a name reachable only through one can still be shortened.
        /// </summary>
        private readonly IImpliedRelationshipProvider impliedRelationshipProvider;

        /// <summary>
        /// The bindings every reachable Namespace offers, built once on construction.
        /// </summary>
        private readonly NamespaceBindingIndex index;

        /// <summary>
        /// Lazy cache: <c>(target.Id, sourceLocalScope.Id, matchFloorScope.Id)</c> → emitted string.
        /// </summary>
        private readonly Dictionary<(Guid TargetId, Guid SourceScopeId, Guid MatchFloorId), string> resolvedReferences
            = new();

        /// <summary>
        /// Lazy cache: source-POCO id → its upward containment chain of namespaces.
        /// </summary>
        private readonly Dictionary<Guid, SourceScopeChain> sourceScopeChains
            = new();

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
            if (rootNamespace == null)
            {
                throw new ArgumentNullException(nameof(rootNamespace));
            }

            var otherRootNamespaces = globalNamespaces?
                .Where(candidate => candidate != null && !ReferenceEquals(candidate, rootNamespace))
                .Distinct()
                .ToList() ?? [];

            this.impliedRelationshipProvider = impliedRelationshipProvider ?? NullImpliedRelationshipProvider.Instance;

            this.index = new NamespaceBindingIndexBuilder(rootNamespace, otherRootNamespaces, this.impliedRelationshipProvider).Build();
        }

        /// <summary>
        /// Gets the root <see cref="INamespace" /> — the fallback local scope when a source POCO has no
        /// resolvable enclosing namespace.
        /// </summary>
        private INamespace RootNamespace => this.index.RootNamespace;

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

                case IMembership membership:
                    if (membership.MemberElement == null)
                    {
                        return string.Empty;
                    }

                    if (sourcePoco is IMembershipImport)
                    {
                        // §8.2.3.5.1: a MembershipImport's qualified name identifies the MEMBERSHIP itself,
                        // the one case where it is not the memberElement. The two coincide for an owning
                        // Membership, but an alias binds the element under its own name in its own
                        // Namespace, so naming the element would import a DIFFERENT Membership and bind a
                        // different name into the importing Namespace.
                        var aliasPath = this.ResolveAliasMembership(membership, sourcePoco);

                        if (!string.IsNullOrWhiteSpace(aliasPath))
                        {
                            return aliasPath;
                        }

                        target = membership.MemberElement;
                        break;
                    }

                    var membershipPath = ContainmentPaths.QueryShortQualifiedName(membership.MemberElement, sourcePoco);

                    if (!string.IsNullOrWhiteSpace(membershipPath))
                    {
                        return membershipPath;
                    }

                    // An unnamed member element yields no path here; resolving it as an ordinary target
                    // reaches its effective name, and a QualifiedName segment can never be empty.
                    target = membership.MemberElement;
                    break;
            }

            // A NamespaceImport keeps a self-contained path unless the target is reachable by
            // containment, so the name cannot depend on a sibling import. A MembershipImport names one
            // element and is exempt.
            if (sourcePoco is IImport { OwningRelatedElement: { } importOwner } and not IMembershipImport
                && !ContainmentPaths.IsReachableByContainment(target, importOwner))
            {
                return this.QueryImportPath(target);
            }

            var escapedName = target.EscapedName();

            if (string.IsNullOrWhiteSpace(escapedName))
            {
                var impliedName = this.QueryImpliedEffectiveName(target);

                escapedName = string.IsNullOrWhiteSpace(impliedName) ? null : SegmentNaming.Escape(impliedName);
            }

            if (string.IsNullOrWhiteSpace(escapedName))
            {
                return target.qualifiedName ?? string.Empty;
            }

            if (LocalScopeResolver.IsChainAccessor(sourcePoco))
            {
                return this.ResolveChainAccessor(target, sourcePoco, escapedName);
            }

            var sourceLocalScope = LocalScopeResolver.GetSourceLocalScope(sourcePoco, this.RootNamespace);

            // Inside a FeatureValue expression the innermost scopes are shadow-only, since the readings
            // of §8.2.3.5.2 disagree about them. See LocalScopeResolver.QueryValueExpressionMatchFloor.
            var matchFloorScope = LocalScopeResolver.QueryValueExpressionMatchFloor(sourcePoco) ?? sourceLocalScope;

            // §8.2.3.5.1: a Redefinition's redefinedFeature resolves against the generals of the owning
            // Type, not the local namespace. Skipped when the redefiner declares that same name, where the
            // bare form would read as a self-reference.
            if (sourcePoco is IRedefinition { RedefiningFeature: { } redefiningFeature } redefinitionContext
                && ReferenceEquals(target, redefinitionContext.RedefinedFeature)
                && !LocalScopeResolver.RedefinerDeclaredNameCollidesWith(redefiningFeature, target)
                && !LocalScopeResolver.ReferencedFeatureSharesSimpleName(redefiningFeature, target)
                && this.QueryRedefinedFeatureScope(redefinitionContext, target) is { } redefinitionScope)
            {
                sourceLocalScope = redefinitionScope;

                matchFloorScope = redefinitionScope;
            }

            // A redefining or referencing feature takes its effective name from the target, so it is
            // bound under the name being resolved and must not shadow it (§8.2.3.5).
            var localReferencer = sourcePoco switch
            {
                IRedefinition redefinition when ReferenceEquals(target, redefinition.RedefinedFeature) => redefinition.RedefiningFeature,
                IReferenceSubsetting referenceSubsetting when ReferenceEquals(target, referenceSubsetting.ReferencedFeature) => referenceSubsetting.referencingFeature,
                _ => null
            };

            if (localReferencer != null && !LocalScopeResolver.RedefinerDeclaredNameCollidesWith(localReferencer, target))
            {
                return this.ResolveFresh(target, this.BuildReferenceSite(sourcePoco, sourceLocalScope, matchFloorScope, localReferencer), escapedName);
            }

            var referenceSite = this.BuildReferenceSite(sourcePoco, sourceLocalScope, matchFloorScope, null);

            // The memo key does not capture the self binding, so a self-binding site must not share a
            // memo entry with an ordinary one at the same scope.
            if (referenceSite.SelfBinding != null)
            {
                return this.ResolveFresh(target, referenceSite, escapedName);
            }

            var cacheKey = (target.Id, sourceLocalScope?.Id ?? Guid.Empty, matchFloorScope?.Id ?? Guid.Empty);

            if (this.resolvedReferences.TryGetValue(cacheKey, out var cached))
            {
                return cached;
            }

            var resolved = this.ResolveFresh(target, referenceSite, escapedName);
            this.resolvedReferences[cacheKey] = resolved;
            return resolved;
        }

        /// <summary>
        /// Returns a SELF-CONTAINED path to <paramref name="target" />, anchored at the outermost named
        /// ancestor that binds it directly, so the path never depends on names introduced by imports of the
        /// importing namespace.
        /// <para>
        /// Intermediate owner segments the anchor re-exports through a public import are collapsed,
        /// since the anchor's visible resolution already reaches the target (KerML §8.2.3.5.3).
        /// </para>
        /// </summary>
        /// <param name="target">The imported <see cref="IElement" />; must be non-null.</param>
        /// <returns>The import path, or the target's <c>qualifiedName</c> when no anchor collapses.</returns>
        private string QueryImportPath(IElement target)
        {
            var namedAncestors = new List<IElement>();

            for (var ancestor = target?.owningNamespace; ancestor != null; ancestor = ancestor.owningNamespace)
            {
                if (string.IsNullOrWhiteSpace(SegmentNaming.QueryPreferredEscapedSegment(ancestor)))
                {
                    break;
                }

                namedAncestors.Add(ancestor);
            }

            var targetSegment = SegmentNaming.QueryPreferredEscapedSegment(target);

            if (targetSegment == null)
            {
                return target.qualifiedName ?? string.Empty;
            }

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
                    .Select(SegmentNaming.QueryPreferredEscapedSegment)
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
            var rawName = SegmentNaming.QueryPreferredRawName(target);

            return !string.IsNullOrWhiteSpace(rawName)
                   && this.index.GetSimpleNameIndex(scope).TryGetValue(rawName, out var bucket)
                   && bucket.Count == 1
                   && bucket.Contains(target)
                   && !string.IsNullOrWhiteSpace(segment)
                   && this.BindsVisibly(scope, rawName, target);
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
                LocalScopeResolver.QuerySelfBinding(sourcePoco));
        }

        /// <summary>
        /// First-time resolution, delegated to the layered engine: candidate spellings are generated
        /// shortest-first and the first that re-resolves to the target (KerML §8.2.3.5.1) is emitted.
        /// </summary>
        /// <param name="target">The referenced element.</param>
        /// <param name="site">The reference site: its scope chain and the exclusions that apply to every probe.</param>
        /// <param name="escapedName">The target's escaped raw <c>name</c>.</param>
        /// <returns>The resolved emission string.</returns>
        private string ResolveFresh(IElement target, ReferenceSite site, string escapedName)
        {
            return this.ResolveViaLayeredScopes(target, site, escapedName);
        }

        /// <summary>
        /// Determines whether every hop WITHIN <paramref name="pathDownToTarget" /> resolves visibly — the
        /// hop from the anchor into the path is checked separately, against whatever the anchor segment
        /// actually resolves to.
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
                    var rawName = SegmentNaming.QueryPreferredRawName(segmentElement);

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
        /// Returns the effective name of a Feature that carries none of its own, taken from the Feature it
        /// redefines through an implied Redefinition (KerML §8.4.2).
        /// </summary>
        /// <param name="element">The element to name.</param>
        /// <returns>The implied effective name, or <see langword="null" /> when none applies.</returns>
        private string QueryImpliedEffectiveName(IElement element)
        {
            if (element is not IFeature feature || !string.IsNullOrWhiteSpace(feature.name) || !string.IsNullOrWhiteSpace(feature.shortName))
            {
                return null;
            }

            return this.impliedRelationshipProvider
                .GetImpliedRedefinitions(feature)
                .Select(redefinition => redefinition.RedefinedFeature)
                .Where(redefined => redefined != null && !ReferenceEquals(redefined, feature))
                .Select(SegmentNaming.QueryPreferredRawName)
                .FirstOrDefault(name => !string.IsNullOrWhiteSpace(name));
        }

        /// <summary>
        /// Resolves a chain accessor's simple name: the target's <c>name</c> first, then <c>shortName</c>,
        /// then <c>qualifiedName</c> as a last resort.
        /// </summary>
        /// <param name="target">The chain-accessor target element.</param>
        /// <param name="sourcePoco">The source <see cref="IElement" /></param>
        /// <param name="escapedName">The pre-computed escaped <c>name</c> form.</param>
        /// <returns>The escaped simple name.</returns>
        private string ResolveChainAccessor(IElement target, IElement sourcePoco, string escapedName)
        {
            var rawName = SegmentNaming.QueryPreferredRawName(target);

            if (string.IsNullOrWhiteSpace(rawName))
            {
                rawName = this.QueryImpliedEffectiveName(target);
            }

            if (string.IsNullOrWhiteSpace(rawName))
            {
                return target.qualifiedName ?? string.Empty;
            }

            if (this.BindsRelativeNameElsewhere(sourcePoco, rawName, target))
            {
                // The bare name is proven to bind elsewhere, so it must not be emitted while any qualified
                // form is available; a segment can never be empty, so the bare name remains the floor.
                return SegmentNaming.FirstNonBlank(this.TryBuildGlobalQualifiedName(target), ContainmentPaths.QueryShortQualifiedName(target), target.qualifiedName)
                       ?? SegmentNaming.Escape(rawName);
            }

            return string.IsNullOrWhiteSpace(target.name) ? SegmentNaming.Escape(rawName) : escapedName;
        }

        /// <summary>
        /// Determines whether the relative Namespace a chain accessor resolves against binds
        /// <paramref name="rawName" /> to something other than <paramref name="target" />, which makes the
        /// bare simple name unusable (KerML §8.2.3.5.2).
        /// </summary>
        /// <param name="sourcePoco">The source POCO at the reference site.</param>
        /// <param name="rawName">The simple name that would be emitted.</param>
        /// <param name="target">The referenced element.</param>
        /// <returns><see langword="true" /> only on a definitive mismatch.</returns>
        /// <remarks>
        /// A name the relative Namespace does not bind at all is not a mismatch: the index reaches only what
        /// the model carries, so absence is inconclusive and the bare name stands.
        /// </remarks>
        private bool BindsRelativeNameElsewhere(IElement sourcePoco, string rawName, IElement target)
        {
            if (LocalScopeResolver.QueryRelativeNamespace(sourcePoco) is not { } relativeNamespace)
            {
                return false;
            }

            var outcome = this.index.ResolveNameInScope(relativeNamespace, rawName, null, null, false);

            return outcome.Kind switch
            {
                LayeredOutcomeKind.Bound => !ReferenceEquals(outcome.Element, target),
                LayeredOutcomeKind.Ambiguous => true,
                _ => false
            };
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
                return LocalScopeResolver.BuildChain(sourceLocalScope ?? this.RootNamespace, matchFloorScope);
            }

            if (this.sourceScopeChains.TryGetValue(sourcePoco.Id, out var cached))
            {
                return cached;
            }

            var chain = LocalScopeResolver.BuildChain(sourceLocalScope ?? this.RootNamespace, matchFloorScope);
            this.sourceScopeChains[sourcePoco.Id] = chain;

            return chain;
        }

        /// <summary>
        /// Names an ALIAS <see cref="IMembership" /> through the alias itself, as
        /// <c>{owning namespace}::{memberName}</c>.
        /// </summary>
        /// <remarks>
        /// Returns <see langword="null" /> for an owning Membership, where the Membership and its
        /// <c>memberElement</c> denote the same binding and the ordinary element path already names it.
        /// </remarks>
        /// <param name="membership">The Membership being imported.</param>
        /// <param name="sourcePoco">The POCO at whose syntactic position the reference appears.</param>
        /// <returns>The alias path, or <see langword="null" /> when the Membership is not an alias.</returns>
        private string ResolveAliasMembership(IMembership membership, IElement sourcePoco)
        {
            if (ReferenceEquals(membership, membership.MemberElement.owningMembership))
            {
                return null;
            }

            var aliasName = !string.IsNullOrWhiteSpace(membership.MemberName)
                ? membership.MemberName
                : membership.MemberShortName;

            if (string.IsNullOrWhiteSpace(aliasName) || membership.membershipOwningNamespace == null)
            {
                return null;
            }

            var namespacePath = this.Resolve(membership.membershipOwningNamespace, sourcePoco);

            return string.IsNullOrWhiteSpace(namespacePath)
                ? SegmentNaming.Escape(aliasName)
                : $"{namespacePath}::{SegmentNaming.Escape(aliasName)}";
        }

        /// <summary>
        /// Returns the scope in which a <see cref="IRedefinition" />'s <c>redefinedFeature</c> is resolved per
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

            generalScopes =
            [
                ..owningType.ownedSpecialization
                    .Select(specialization => specialization.General)
                    .OfType<INamespace>()
                    .Where(general => !ReferenceEquals(general, owningType))
            ];

            // A model exported without implied Relationships (§8.4.2) omits Specializations the abstract
            // syntax requires. Appended last so declared supertypes keep priority.
            generalScopes.AddRange(this.impliedRelationshipProvider.GetImpliedSpecializations(owningType)
                .Select(specialization => this.index.ResolutionGraph.TranslateToResolutionGraph(specialization.General))
                .OfType<INamespace>()
                .Where(general => !ReferenceEquals(general, owningType) && !generalScopes.Contains(general)));

            if (generalScopes.Count == 0)
            {
                return null;
            }

            var rawName = SegmentNaming.QueryPreferredRawName(target);

            if (string.IsNullOrWhiteSpace(rawName))
            {
                return generalScopes[0];
            }

            // §8.2.3.5.1 stops at the first general where "a resolution is found" — a resolution being one
            // whose Element also has the proper type for the context, here Redefinition::redefinedFeature,
            // so any Feature qualifies and a non-Feature binding does not stop the walk. Selecting the
            // first scope binding the name to the TARGET instead would skip an earlier general that
            // shadows it, and a bare name emitted on that basis re-reads as the shadowing feature.
            return generalScopes.FirstOrDefault(scope =>
                this.ResolveSimpleNameInScope(scope, target, rawName, null, null, static element => element is IFeature)
                    == SimpleNameResolution.Matched);
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
        /// <param name="localRedefiner">Feature to exclude from the bucket, or <see langword="null" />.</param>
        /// <param name="selfBinding">The binding the reference itself is, or <see langword="null" />.</param>
        /// <param name="accept">Predicate deciding whether a bound element counts as the target, or <see langword="null" /> for reference identity.</param>
        /// <returns>The resolution state.</returns>
        private SimpleNameResolution ResolveSimpleNameInScope(INamespace scope, IElement target, string rawName, IFeature localRedefiner, SelfBinding selfBinding, Func<IElement, bool> accept = null)
        {
            var simpleNameIndex = this.index.GetSimpleNameIndex(scope);

            if (!simpleNameIndex.TryGetValue(rawName, out var elements))
            {
                return SimpleNameResolution.NotBound;
            }

            accept ??= candidate => ReferenceEquals(candidate, target);

            if (selfBinding != null && ReferenceEquals(scope, selfBinding.Scope))
            {
                var isBoundToOtherElement = elements.Any(element =>
                    !accept(element) && !ReferenceEquals(element, localRedefiner));

                if (isBoundToOtherElement)
                {
                    return SimpleNameResolution.Shadowed;
                }

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

            candidates = NamespaceBindingIndex.PreferDirectlyOwnedOverInherited(scope, candidates);

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
        /// A live query rather than index provenance; a source it fails to cover costs a longer name, never
        /// an invalid one.
        /// </remarks>
        private bool BindsTargetIndependently(INamespace scope, string rawName, IElement target, IMembership selfMembership)
        {
            return this.QueryBindingMemberships(scope, false)
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
            return this.QueryBindingMemberships(scope, true)
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
            var memberships = new List<IMembership>(scope.ownedMembership.Where(ownedMember => ImportExpansion.PassesVisibilityFilter(ownedMember, visibleOnly)));

            memberships.AddRange(ImportExpansion.QueryImportedMembershipsSafe(scope, visibleOnly));

            if (scope is not IType type)
            {
                return memberships;
            }

            memberships.AddRange(ImportExpansion.QueryInheritedMemberships(type).Where(inheritedMember => ImportExpansion.PassesVisibilityFilter(inheritedMember, visibleOnly)));

            var declaredSupertypes = type.AllSupertypes()
                .Where(supertype => !ReferenceEquals(supertype, type))
                .ToList();

            foreach (var impliedGeneral in this.index.ResolutionGraph.QueryImpliedGeneralClosure(type, declaredSupertypes))
            {
                memberships.AddRange(impliedGeneral.ownedMembership
                    .Where(ownedMember => ownedMember.Visibility != VisibilityKind.Private && ImportExpansion.PassesVisibilityFilter(ownedMember, visibleOnly)));

                memberships.AddRange(ImportExpansion.QueryInheritedMemberships(impliedGeneral).Where(inheritedMember => ImportExpansion.PassesVisibilityFilter(inheritedMember, visibleOnly)));
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

            var (shortName, longName) = SegmentNaming.QueryMembershipNames(membership, target);

            return string.Equals(shortName, rawName, StringComparison.Ordinal)
                   || string.Equals(longName, rawName, StringComparison.Ordinal);
        }
    }
}
