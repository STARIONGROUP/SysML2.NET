// -------------------------------------------------------------------------------------------------
// <copyright file="ImportExpansion.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;

    /// <summary>
    /// Expands a Namespace's Imports into the Memberships they contribute, applying the visibility,
    /// recursion, filtering and distinguishability rules of KerML §7.2.5.4 and §8.3.2.4.
    /// </summary>
    internal static class ImportExpansion
    {
        /// <summary>
        /// Applies the <c>filterConditions</c> of <paramref name="filteringNamespace" /> to
        /// <paramref name="memberships" /> (SysML 2.0 §7.5.4); a namespace that declares none is transparent.
        /// </summary>
        /// <param name="filteringNamespace">The namespace whose filter conditions apply.</param>
        /// <param name="memberships">The candidate memberships.</param>
        /// <returns>The memberships that satisfy every condition.</returns>
        private static IEnumerable<IMembership> ApplyFilterConditions(INamespace filteringNamespace, IEnumerable<IMembership> memberships)
        {
            if (filteringNamespace is not IPackage filteringPackage)
            {
                return memberships;
            }

            return filteringPackage.filterCondition.Count == 0
                ? memberships
                : SelectIncludedMemberships(filteringPackage, memberships);
        }

        /// <summary>
        /// Determines whether <paramref name="membership" /> binds any of <paramref name="names" />.
        /// </summary>
        /// <param name="membership">The membership to test.</param>
        /// <param name="names">The names to test against.</param>
        /// <returns><see langword="true" /> when at least one bound name is in the set.</returns>
        private static bool BindsAnyName(IMembership membership, HashSet<string> names)
        {
            if (membership is not { MemberElement: { } target })
            {
                return false;
            }

            var (shortName, longName) = SegmentNaming.QueryMembershipNames(membership, target);

            return (!string.IsNullOrWhiteSpace(shortName) && names.Contains(shortName))
                   || (!string.IsNullOrWhiteSpace(longName) && names.Contains(longName));
        }

        /// <summary>
        /// Determines whether an IMPORTED membership has a distinguishability collision with an owned
        /// membership of the importing scope, which <c>Namespace::importedMemberships</c> excludes. Without
        /// this the homonym competes with the owned member and forces a needlessly qualified reference.
        /// <para>
        /// Delegates to <see cref="IMembership.IsDistinguishableFrom" />; the name check is only a
        /// pre-filter, since differing names always imply distinguishable.
        /// </para>
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

            var (shortName, longName) = SegmentNaming.QueryMembershipNames(membership, target);

            var sharesName = (!string.IsNullOrWhiteSpace(shortName) && ownedNames.Contains(shortName))
                             || (!string.IsNullOrWhiteSpace(longName) && ownedNames.Contains(longName));

            return sharesName && ownedMemberships.Any(owned => !membership.IsDistinguishableFrom(owned));
        }

        /// <summary>
        /// Enumerates the unfiltered visible memberships of <paramref name="importedNamespace" />.
        /// </summary>
        /// <param name="importedNamespace">The namespace named by the import.</param>
        /// <param name="includeAll">The triggering import's <c>isImportAll</c>.</param>
        /// <param name="isRecursive">The triggering import's <c>isRecursive</c>.</param>
        /// <param name="isGlobal">Whether the importing scope is reached through the global namespace.</param>
        /// <param name="excluded">Namespaces already visited on this import chain.</param>
        /// <returns>The memberships, before filter conditions.</returns>
        private static IEnumerable<IMembership> EnumerateVisibleMemberships(INamespace importedNamespace, bool includeAll, bool isRecursive, bool isGlobal, HashSet<INamespace> excluded)
        {
            if (importedNamespace == null || !excluded.Add(importedNamespace))
            {
                yield break;
            }

            var publicOnly = !includeAll || isGlobal;

            List<IMembership> ownedMemberships = [..importedNamespace.ownedMembership.Where(ownedMember => !publicOnly || ownedMember.Visibility == VisibilityKind.Public)];

            if (ownedMemberships == null)
            {
                throw new ArgumentNullException(nameof(ownedMemberships));
            }

            foreach (var ownedMembership in ownedMemberships)
            {
                yield return ownedMembership;
            }

            List<IImport> reExports = [..importedNamespace.ownedImport.Where(ownedImport => ownedImport.Visibility == VisibilityKind.Public)];

            foreach (var reExport in reExports)
            {
                switch (reExport)
                {
                    case IMembershipImport { ImportedMembership: { } reExportedMembership }:
                        yield return reExportedMembership;

                        break;

                    case INamespaceImport { ImportedNamespace: { } reExportedNamespace } reExportedImport:

                        foreach (var reExportedMembership in QueryVisibleMemberships(reExportedNamespace, reExportedImport.IsImportAll, reExportedImport.IsRecursive, isGlobal, excluded))
                        {
                            yield return reExportedMembership;
                        }

                        break;
                }
            }

            if (!isRecursive)
            {
                yield break;
            }

            foreach (var ownedSubNamespace in ownedMemberships.Select(ownedMembership => ownedMembership.MemberElement).OfType<INamespace>())
            {
                foreach (var descendantMembership in QueryVisibleMemberships(ownedSubNamespace, includeAll, true, isGlobal, excluded))
                {
                    yield return descendantMembership;
                }
            }
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
        internal static bool PassesVisibilityFilter(IRelationship relationship, bool publicOnly)
        {
            if (!publicOnly)
            {
                return true;
            }

            return relationship switch
            {
                IMembership membership => membership.Visibility == VisibilityKind.Public,
                IImport import => import.Visibility == VisibilityKind.Public,
                _ => true
            };
        }

        /// <summary>
        /// Returns every name bound by <paramref name="memberships" />.
        /// </summary>
        /// <param name="memberships">The memberships to collect names from.</param>
        /// <returns>The bound names.</returns>
        private static HashSet<string> QueryBoundNames(List<IMembership> memberships)
        {
            var names = new HashSet<string>(StringComparer.Ordinal);

            foreach (var membership in memberships.Where(membership => membership?.MemberElement != null))
            {
                var (shortName, longName) = SegmentNaming.QueryMembershipNames(membership, membership.MemberElement);

                foreach (var boundName in new[] { shortName, longName }.Where(name => !string.IsNullOrWhiteSpace(name)))
                {
                    names.Add(boundName);
                }
            }

            return names;
        }

        /// <summary>
        /// Returns the names bound by more than one imported <see cref="IMembership" /> to DIFFERENT member
        /// elements. KerML §7.2.5.4 hides every membership involved in such a conflict, so none of them can
        /// be named without qualification.
        /// </summary>
        /// <param name="contributions">The candidate imported contributions.</param>
        /// <returns>The hidden names, possibly empty.</returns>
        private static HashSet<string> QueryHiddenImportedNames(List<(IImport Import, IMembership Membership)> contributions)
        {
            var boundElements = new Dictionary<string, IElement>(StringComparer.Ordinal);
            var hiddenNames = new HashSet<string>(StringComparer.Ordinal);

            foreach (var (_, membership) in contributions.Where(contribution => contribution.Membership?.MemberElement != null))
            {
                var target = membership.MemberElement;
                var (shortName, longName) = SegmentNaming.QueryMembershipNames(membership, target);

                foreach (var boundName in new[] { shortName, longName }.Where(name => !string.IsNullOrWhiteSpace(name)))
                {
                    if (!boundElements.TryGetValue(boundName, out var alreadyBound))
                    {
                        boundElements[boundName] = target;
                    }
                    else if (!ReferenceEquals(alreadyBound, target))
                    {
                        hiddenNames.Add(boundName);
                    }
                }
            }

            return hiddenNames;
        }

        /// <summary>
        /// Enumerates the memberships one <c>ownedImport</c> contributes to its importing namespace, per
        /// <c>MembershipImport::importedMemberships()</c> and <c>NamespaceImport::importedMemberships()</c>
        /// (KerML §8.3.2.4.4, §8.3.2.4.6).
        /// </summary>
        /// <param name="ownedImport">The import to expand.</param>
        /// <param name="isGlobal">Whether the importing scope is reached through the global namespace.</param>
        /// <param name="excluded">Namespaces already visited on this import chain.</param>
        /// <returns>The memberships the import contributes.</returns>
        private static IEnumerable<IMembership> QueryImportContribution(IImport ownedImport, bool isGlobal, HashSet<INamespace> excluded)
        {
            switch (ownedImport)
            {
                case IMembershipImport { ImportedMembership: { } importedMembership } membershipImport:

                    yield return importedMembership;

                    if (membershipImport.IsRecursive && importedMembership.MemberElement is INamespace importedElement)
                    {
                        foreach (var descendantMembership in QueryVisibleMemberships(importedElement, membershipImport.IsImportAll, true, isGlobal, excluded))
                        {
                            yield return descendantMembership;
                        }
                    }

                    break;

                case INamespaceImport { ImportedNamespace: { } importedNamespace } namespaceImport:

                    foreach (var importedMember in QueryVisibleMemberships(importedNamespace, namespaceImport.IsImportAll, namespaceImport.IsRecursive, isGlobal, excluded))
                    {
                        yield return importedMember;
                    }

                    break;
            }
        }

        /// <summary>
        /// Returns what a scope's <c>ownedImports</c> contribute to its <c>importedMemberships</c>, less the
        /// two exclusions of KerML §7.2.5.4: a membership colliding with an owned member, and one colliding
        /// with a visible membership from another imported namespace.
        /// </summary>
        /// <param name="scope">The importing namespace.</param>
        /// <param name="visibleOnly">Whether only publicly visible imports are considered.</param>
        /// <param name="isGlobal">Whether the importing scope is reached through the global namespace.</param>
        /// <param name="ownedNames">Names bound by the scope's owned memberships.</param>
        /// <param name="ownedMemberships">The scope's owned memberships.</param>
        /// <returns>The surviving contributions, each paired with the import that produced it.</returns>
        internal static List<(IImport Import, IMembership Membership)> QueryImportedContributions(INamespace scope, bool visibleOnly, bool isGlobal, HashSet<string> ownedNames, List<IMembership> ownedMemberships)
        {
            var contributions = new List<(IImport Import, IMembership Membership)>();

            foreach (var ownedImport in scope.ownedImport.Where(ownedImport => PassesVisibilityFilter(ownedImport, visibleOnly)))
            {
                contributions.AddRange(QueryImportContribution(ownedImport, isGlobal, [scope])
                    .Where(membership => !CollidesWithOwnedMembership(membership, ownedNames, ownedMemberships))
                    .Select(membership => (ownedImport, membership)));
            }

            // A Package's own filterConditions filter ALL of its imports (SysML 2.0 §7.5.4).
            var included = new HashSet<IMembership>(ApplyFilterConditions(scope, contributions.Select(contribution => contribution.Membership)));

            contributions = [..contributions.Where(contribution => included.Contains(contribution.Membership))];

            var hiddenNames = QueryHiddenImportedNames(contributions);

            return hiddenNames.Count == 0
                ? contributions
                : [..contributions.Where(contribution => !BindsAnyName(contribution.Membership, hiddenNames))];
        }

        /// <summary>
        /// Returns the Memberships <paramref name="scope" />'s own Imports contribute, mirroring
        /// <see cref="BuildOwnedAndImportedEntries" /> minus its collision filter — a colliding import
        /// cannot be the INDEPENDENT binding anyway, since the owned member it collides with is.
        /// </summary>
        /// <param name="scope">The importing scope; must be non-null.</param>
        /// <param name="visibleOnly">Whether to keep only PUBLIC imports, the ones that re-export.</param>
        /// <returns>The imported memberships, possibly empty.</returns>
        internal static List<IMembership> QueryImportedMembershipsSafe(INamespace scope, bool visibleOnly)
        {
            var imported = new List<IMembership>();

            var ownedMemberships = scope.ownedMembership;
            var ownedNames = QueryBoundNames(ownedMemberships);

            imported.AddRange(QueryImportedContributions(scope, visibleOnly, false, ownedNames, ownedMemberships)
                .Select(contribution => contribution.Membership));

            return imported;
        }

        /// <summary>
        /// Enumerates the memberships an <c>import ns::*</c> contributes, following RE-EXPORTS
        /// transitively per <c>NamespaceImport::importedMemberships()</c> → <c>visibleMemberships()</c>:
        /// the owned memberships plus, recursively, whatever the namespace's own PUBLIC imports bring in.
        /// <paramref name="excluded" /> terminates import cycles.
        /// <para>
        /// The visibility filter is driven by the import's <c>isImportAll</c>, not by the importing
        /// scope; each re-exported import contributes under its own.
        /// </para>
        /// </summary>
        /// <param name="importedNamespace">The namespace named by the import.</param>
        /// <param name="includeAll">The triggering import's <c>isImportAll</c>: admits non-public memberships.</param>
        /// <param name="isRecursive">The triggering import's <c>isRecursive</c>: also descends into visible owned sub-namespaces.</param>
        /// <param name="isGlobal">
        /// Whether the importing scope is reached through the global namespace, which admits only visible memberships regardless of
        /// <paramref name="includeAll" /> (KerML §8.2.3.5.2).
        /// </param>
        /// <param name="excluded">Namespaces already visited on this import chain.</param>
        /// <returns>The memberships contributed to the importing scope.</returns>
        private static IEnumerable<IMembership> QueryVisibleMemberships(INamespace importedNamespace, bool includeAll, bool isRecursive, bool isGlobal, HashSet<INamespace> excluded)
        {
            return ApplyFilterConditions(importedNamespace, EnumerateVisibleMemberships(importedNamespace, includeAll, isRecursive, isGlobal, excluded));
        }

        /// <summary>
        /// Keeps the memberships whose member element satisfies every <c>filterCondition</c> of
        /// <paramref name="filteringPackage" />.
        /// </summary>
        /// <param name="filteringPackage">The package whose conditions apply.</param>
        /// <param name="memberships">The candidate memberships.</param>
        /// <returns>The surviving memberships.</returns>
        /// <remarks>
        /// A condition whose expression cannot be evaluated leaves the membership neither known to bind nor
        /// known to be absent, so it is kept: dropping it would hide a name that may legitimately bind here.
        /// </remarks>
        private static IEnumerable<IMembership> SelectIncludedMemberships(IPackage filteringPackage, IEnumerable<IMembership> memberships)
        {
            foreach (var membership in memberships.Where(membership => membership?.MemberElement != null))
            {
                bool isIncluded;

                try
                {
                    isIncluded = filteringPackage.IncludeAsMember(membership.MemberElement);
                }
                catch (NotSupportedException)
                {
                    isIncluded = true;
                }

                if (isIncluded)
                {
                    yield return membership;
                }
            }
        }

        /// <summary>
        /// Returns <paramref name="type" />'s <c>inheritedMembership</c>, or an empty list when a filter
        /// condition on a supertype's imports cannot be evaluated.
        /// </summary>
        /// <param name="type">The Type to query.</param>
        /// <returns>The inherited Memberships, possibly empty.</returns>
        /// <remarks>
        /// The derivation reaches <c>Package::importedMemberships</c>, which evaluates
        /// <c>filterConditions</c>; a condition whose Expression has no model-level evaluation throws.
        /// </remarks>
        internal static List<IMembership> QueryInheritedMemberships(IType type)
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
    }
}
