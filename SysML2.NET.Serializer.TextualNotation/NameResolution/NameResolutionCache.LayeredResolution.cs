// -------------------------------------------------------------------------------------------------
// <copyright file="NameResolutionCache.LayeredResolution.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Resolves the textual form of a reference by generating candidate qualified names and accepting the
    /// first that re-resolves to the target under KerML §8.2.3.5 basic name resolution.
    /// <para>
    /// Bindings are held per Namespace with the source they enter through — owned, inherited or
    /// imported (§8.2.3.5.3) — because visibility and the choice between competing bindings depend on
    /// that source.
    /// </para>
    /// </summary>
    public sealed partial class NameResolutionCache
    {
        /// <summary>
        /// Verifies one candidate spelling exactly the way a reader resolves it: the first segment walks
        /// the site's scope chain inward-out (an inner binding SHADOWS — a hit that is not the expected
        /// element fails the candidate rather than continuing outward), the remaining segments descend
        /// name-entered namespaces seeing public bindings only, and the final element must be the target.
        /// The site's match floor applies to where the first segment may match, exactly as before.
        /// </summary>
        /// <param name="site">The reference site.</param>
        /// <param name="candidate">The candidate spelling.</param>
        /// <param name="target">The referenced element.</param>
        /// <returns><see langword="true" /> when the candidate re-resolves to the target.</returns>
        private bool TryVerifyCandidate(ReferenceSite site, NameCandidate candidate, IElement target)
        {
            var expectedFirst = candidate.Anchor ?? target;

            var referenceOwnMembership = site.SourcePoco as IMembership;

            for (var depth = 0; depth < site.Chain.Scopes.Count; depth++)
            {
                var scope = site.Chain.Scopes[depth];

                var excluded = site.SelfBinding != null && ReferenceEquals(scope, site.SelfBinding.Scope)
                    ? site.SelfBinding.Membership
                    : referenceOwnMembership;

                var outcome = this.index.ResolveNameInScope(scope, candidate.RawSegments[0], excluded, site.LocalRedefiner, true);

                switch (outcome.Kind)
                {
                    case LayeredOutcomeKind.NotBound:
                        continue;

                    case LayeredOutcomeKind.Ambiguous:
                        return false;

                    // Scopes below the floor are shadow-only: a match there neither accepts nor stops the walk.
                    case LayeredOutcomeKind.Bound when depth < site.Chain.MatchFloor:
                        continue;

                    default:
                        // §8.2.3.5.1 step 3: the qualification part need only resolve to a Namespace the last segment
                        // reaches the target from.
                        if (candidate.RawSegments.Count == 1)
                        {
                            return ReferenceEquals(outcome.Element, expectedFirst);
                        }

                        return this.TryDescendSegments(outcome.Element, candidate, target);
                }
            }

            // Full resolution from a root Namespace continues in the global Namespace (§8.2.3.5.4).
            foreach (var globalScope in this.index.EnumerateGlobalScopes())
            {
                var outcome = this.index.ResolveNameInScope(globalScope, candidate.RawSegments[0], null, null, false);

                if (outcome.Kind == LayeredOutcomeKind.Bound && ReferenceEquals(outcome.Element, expectedFirst))
                {
                    return this.TryDescendSegments(outcome.Element, candidate, target);
                }
            }

            return false;
        }

        /// <summary>
        /// Descends a candidate's remaining segments from the resolved first element, each hop probing the
        /// current namespace from OUTSIDE (public bindings only).
        /// </summary>
        /// <param name="first">The element the first segment resolved to.</param>
        /// <param name="candidate">The candidate being verified.</param>
        /// <param name="target">The referenced element the last segment must yield.</param>
        /// <returns><see langword="true" /> when the descent ends on the target.</returns>
        private bool TryDescendSegments(IElement first, NameCandidate candidate, IElement target)
        {
            var current = first;

            for (var segmentIndex = 1; segmentIndex < candidate.RawSegments.Count; segmentIndex++)
            {
                if (current is not INamespace currentNamespace)
                {
                    return false;
                }

                var step = this.index.ResolveNameInScope(currentNamespace, candidate.RawSegments[segmentIndex], null, null, false);

                if (step.Kind != LayeredOutcomeKind.Bound)
                {
                    return false;
                }

                current = step.Element;
            }

            return ReferenceEquals(current, target);
        }

        /// <summary>
        /// Builds the global-scope-qualified form <c>$::A::B::C</c> for <paramref name="target" />, or
        /// <see langword="null" /> when the containment path is unnameable or does not resolve.
        /// </summary>
        /// <param name="target">The referenced element.</param>
        /// <returns>The qualified name, or <see langword="null" />.</returns>
        /// <remarks>
        /// KerML §8.2.3.5.1 step 2 resolves the segment after <c>$</c> in the global Namespace, and step 3
        /// then descends by visible resolution — so this form reaches any element a root Namespace exposes,
        /// whatever the reference site shadows. It is the last resort, tried only once every relative
        /// candidate has failed.
        /// </remarks>
        private string TryBuildGlobalQualifiedName(IElement target)
        {
            var containmentChain = new List<IElement>();
            var current = target;
            var visited = new HashSet<IElement>();

            while (current != null && visited.Add(current))
            {
                containmentChain.Add(current);
                current = current.owningNamespace;
            }

            containmentChain.Reverse();

            // A root Namespace is unnamed and contributes no segment (KerML §7.2.5.3).
            if (containmentChain.Count != 0 && containmentChain[0].owningNamespace == null)
            {
                containmentChain.RemoveAt(0);
            }

            if (containmentChain.Count == 0)
            {
                return null;
            }

            var rawSegments = new List<string>();
            var escapedSegments = new List<string>();

            foreach (var rawName in containmentChain.Select(SegmentNaming.QueryPreferredRawName))
            {
                if (string.IsNullOrWhiteSpace(rawName))
                {
                    return null;
                }

                rawSegments.Add(rawName);
                escapedSegments.Add(SegmentNaming.Escape(rawName));
            }

            var topLevelElement = containmentChain[0];
            var candidate = new NameCandidate(rawSegments, escapedSegments, topLevelElement);

            foreach (var globalScope in this.index.EnumerateGlobalScopes())
            {
                var head = this.index.ResolveNameInScope(globalScope, rawSegments[0], null, null, false);

                if (head.Kind == LayeredOutcomeKind.Bound
                    && ReferenceEquals(head.Element, topLevelElement)
                    && this.TryDescendSegments(topLevelElement, candidate, target))
                {
                    return $"$::{candidate.Text}";
                }
            }

            return null;
        }

        /// <summary>
        /// Generates the candidate spellings for a target at a site, in the established preference order:
        /// bare short name, bare name, alias names visible on the chain, facade-qualified forms, then
        /// ancestor-anchored suffixes from the nearest ancestor outward. Every candidate is only EMITTED
        /// after <see cref="TryVerifyCandidate" /> proves it re-resolves to the target.
        /// </summary>
        /// <param name="target">The referenced element.</param>
        /// <param name="site">The reference site.</param>
        /// <returns>The candidates, most preferred first.</returns>
        private IEnumerable<NameCandidate> GenerateCandidates(IElement target, ReferenceSite site)
        {
            var rawShortName = target.shortName;

            if (!string.IsNullOrWhiteSpace(rawShortName))
            {
                yield return new NameCandidate([rawShortName], [SegmentNaming.Escape(rawShortName)], null);
            }

            var rawName = target.name;

            if (!string.IsNullOrWhiteSpace(rawName))
            {
                yield return new NameCandidate([rawName], [SegmentNaming.Escape(rawName)], null);
            }

            foreach (var scope in site.Chain.Scopes)
            {
                if (!this.index.TryGetAliases(scope, target, out var aliasNames))
                {
                    continue;
                }

                foreach (var aliasName in aliasNames)
                {
                    yield return new NameCandidate([aliasName], [SegmentNaming.Escape(aliasName)], null);
                }
            }

            // A Namespace publicly importing the owner re-exports it (§8.2.3.5.3) and can anchor the
            // qualification part. The specification ranks neither spelling, so the shorter name wins.
            if (target.owningNamespace is { } owner)
            {
                var targetSegmentRaw = SegmentNaming.QueryPreferredRawName(target);
                var targetSegment = SegmentNaming.QueryPreferredEscapedSegment(target);
                var ownerRaw = SegmentNaming.QueryPreferredRawName(owner);
                var ownerEscaped = SegmentNaming.QueryPreferredEscapedSegment(owner);
                var meaningfulShorterMax = (int)((ownerEscaped?.Length ?? int.MaxValue) * 0.7);

                if (!string.IsNullOrWhiteSpace(targetSegmentRaw))
                {
                    var facades = this.index.QueryFacades(owner)
                        .OrderBy(facade => facade, Comparer<INamespace>.Create(ContainmentPaths.CompareFacades))
                        .ToList();

                    foreach (var facade in facades.Where(facade => (SegmentNaming.QueryPreferredEscapedSegment(facade)?.Length ?? int.MaxValue) <= meaningfulShorterMax))
                    {
                        var facadeRaw = SegmentNaming.QueryPreferredRawName(facade);

                        if (!string.IsNullOrWhiteSpace(facadeRaw))
                        {
                            yield return new NameCandidate([facadeRaw, targetSegmentRaw], [SegmentNaming.QueryPreferredEscapedSegment(facade), targetSegment], facade);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(ownerRaw) && !string.IsNullOrWhiteSpace(ownerEscaped))
                    {
                        yield return new NameCandidate([ownerRaw, targetSegmentRaw], [ownerEscaped, targetSegment], owner);
                    }

                    foreach (var facade in facades.Where(facade => (SegmentNaming.QueryPreferredEscapedSegment(facade)?.Length ?? int.MaxValue) > meaningfulShorterMax))
                    {
                        var facadeRaw = SegmentNaming.QueryPreferredRawName(facade);

                        if (!string.IsNullOrWhiteSpace(facadeRaw))
                        {
                            yield return new NameCandidate([facadeRaw, targetSegmentRaw], [SegmentNaming.QueryPreferredEscapedSegment(facade), targetSegment], facade);
                        }
                    }
                }
            }

            var rawPathDown = new List<string>();
            var escapedPathDown = new List<string>();

            var pathRaw = SegmentNaming.QueryPreferredRawName(target);
            var pathEscaped = SegmentNaming.QueryPreferredEscapedSegment(target);

            if (string.IsNullOrWhiteSpace(pathRaw))
            {
                yield break;
            }

            rawPathDown.Add(pathRaw);
            escapedPathDown.Add(pathEscaped);

            var ancestor = (IElement)target.owningNamespace;
            var visitedAncestors = new HashSet<IElement>();

            if (ancestor != null && visitedAncestors.Add(ancestor))
            {
                var ownerWalkRaw = SegmentNaming.QueryPreferredRawName(ancestor);
                var ownerWalkEscaped = SegmentNaming.QueryPreferredEscapedSegment(ancestor);

                if (string.IsNullOrWhiteSpace(ownerWalkRaw) || string.IsNullOrWhiteSpace(ownerWalkEscaped))
                {
                    yield break;
                }

                rawPathDown.Add(ownerWalkRaw);
                escapedPathDown.Add(ownerWalkEscaped);
                ancestor = ancestor.owningNamespace;
            }

            while (ancestor != null && visitedAncestors.Add(ancestor))
            {
                var ancestorRaw = SegmentNaming.QueryPreferredRawName(ancestor);
                var ancestorEscaped = SegmentNaming.QueryPreferredEscapedSegment(ancestor);

                if (string.IsNullOrWhiteSpace(ancestorRaw) || string.IsNullOrWhiteSpace(ancestorEscaped))
                {
                    yield break;
                }

                var rawSegments = new List<string> { ancestorRaw };
                rawSegments.AddRange(Enumerable.Reverse(rawPathDown));

                var escapedSegments = new List<string> { ancestorEscaped };
                escapedSegments.AddRange(Enumerable.Reverse(escapedPathDown));

                yield return new NameCandidate(rawSegments, escapedSegments, ancestor);

                rawPathDown.Add(ancestorRaw);
                escapedPathDown.Add(ancestorEscaped);

                ancestor = ancestor.owningNamespace;
            }
        }

        /// <summary>
        /// Resolves the shortest spelling that a parser-faithful reader resolves back to
        /// <paramref name="target" /> at <paramref name="site" />, falling back to the qualified forms —
        /// never empty for a non-null target.
        /// </summary>
        /// <param name="target">The referenced element.</param>
        /// <param name="site">The reference site.</param>
        /// <param name="escapedName">The pre-computed escaped <c>name</c> of the target.</param>
        /// <returns>The spelling to emit.</returns>
        private string ResolveViaLayeredScopes(IElement target, ReferenceSite site, string escapedName)
        {
            var verifiedText = this.GenerateCandidates(target, site)
                .Where(candidate => this.TryVerifyCandidate(site, candidate, target))
                .Select(candidate => candidate.Text)
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(verifiedText))
            {
                return verifiedText;
            }

            if (this.TryBuildGlobalQualifiedName(target) is { } globalQualifiedName)
            {
                return globalQualifiedName;
            }

            var shortQualifiedName = ContainmentPaths.QueryShortQualifiedName(target);

            if (!string.IsNullOrWhiteSpace(shortQualifiedName))
            {
                return shortQualifiedName;
            }

            if (!string.IsNullOrWhiteSpace(target.qualifiedName))
            {
                return target.qualifiedName;
            }

            return escapedName ?? string.Empty;
        }
    }
}
