// -------------------------------------------------------------------------------------------------
// <copyright file="ContainmentPaths.cs" company="Starion Group S.A.">
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
    /// Containment walks over the ownership tree: the Namespace an Element sits in, the chain up to its
    /// root, and the qualified path that chain spells (KerML §7.2.5.3).
    /// </summary>
    internal static class ContainmentPaths
    {
        /// <summary>
        /// Deterministic ordering for facade candidates at the same scope depth: shorter name first, then
        /// ordinal alphabetical.
        /// </summary>
        /// <param name="left">First candidate.</param>
        /// <param name="right">Second candidate.</param>
        /// <returns>Negative if <paramref name="left" /> sorts first, positive if right, zero if tied.</returns>
        internal static int CompareFacades(INamespace left, INamespace right)
        {
            var leftName = SegmentNaming.QueryPreferredRawName(left) ?? string.Empty;
            var rightName = SegmentNaming.QueryPreferredRawName(right) ?? string.Empty;

            var lengthCompare = leftName.Length.CompareTo(rightName.Length);

            return lengthCompare != 0 ? lengthCompare : string.CompareOrdinal(leftName, rightName);
        }

        /// <summary>
        /// Determines whether <paramref name="element" /> is a member element of one of
        /// <paramref name="scope" />'s own memberships (declared, not imported or inherited).
        /// </summary>
        /// <param name="scope">The namespace to test.</param>
        /// <param name="element">The candidate member element.</param>
        /// <returns><see langword="true" /> when the scope declares the element itself.</returns>
        internal static bool IsDirectlyOwnedBy(INamespace scope, IElement element)
        {
            return scope.ownedMembership.Any(membership => ReferenceEquals(membership.MemberElement, element));
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
        internal static bool IsReachableByContainment(IElement target, IElement importOwner)
        {
            var declaringNamespace = target?.owningNamespace;

            if (declaringNamespace == null)
            {
                return false;
            }

            // Full resolution reaches any member of a containing Namespace (§8.2.3.5.4), so the test is
            // whether the two containment chains intersect, not whether one contains the other.
            for (var scope = importOwner; scope != null; scope = QueryEnclosingContainer(scope))
            {
                for (var candidate = declaringNamespace; candidate != null; candidate = candidate.owningNamespace)
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
                _ => element.owner
            };
        }

        /// <summary>
        /// Returns the namespace enclosing <paramref name="element" />, continuing through the CONTAINMENT
        /// tree when <c>owningNamespace</c> is null.
        /// </summary>
        /// <param name="element">The element to climb from; may be <see langword="null" />.</param>
        /// <returns>The enclosing namespace, or <see langword="null" /> at a true root.</returns>
        /// <remarks>
        /// A <c>FilterPackage</c> is an <c>ownedRelatedElement</c> of its <c>Import</c>
        /// (
        /// <c>
        /// NamespaceImport = … | importedNamespace = FilterPackage { ownedRelatedElement +=
        /// importedNamespace }
        /// </c>
        /// ), and an <c>Import</c> is not a <c>Membership</c> — so the FilterPackage
        /// has no <c>owningMembership</c> and therefore no <c>owningNamespace</c>, even though it is plainly
        /// nested in the model, so climbing by <c>owningNamespace</c> alone dead-ends there.
        /// <para>
        /// KerML §7.2.5.3 defines a root namespace as one with no OWNER, which a FilterPackage has;
        /// a true root has neither and still terminates the climb.
        /// </para>
        /// </remarks>
        private static INamespace QueryEnclosingContainer(IElement element)
        {
            if (element?.owningNamespace is { } owningNamespace)
            {
                return owningNamespace;
            }

            var visited = new HashSet<IElement>();

            for (var container = element?.owner; container != null && visited.Add(container); container = container.owner)
            {
                if (container is INamespace enclosingNamespace)
                {
                    return enclosingNamespace;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the nearest <see cref="INamespace" /> containing <paramref name="element" />, by
        /// CONTAINMENT rather than by the derived <c>owningNamespace</c> — which is null for a Relationship.
        /// </summary>
        /// <param name="element">The element to climb from; may be <see langword="null" />.</param>
        /// <returns>The containing namespace, or <see langword="null" /> at the top of the containment tree.</returns>
        internal static INamespace QueryParentNamespace(IElement element)
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
        /// As <see cref="QueryShortQualifiedName(IElement,IElement)" />, but stops the walk at the first namespace that
        /// also encloses <paramref name="sourcePoco" />.
        /// </summary>
        /// <remarks>
        /// A namespace that encloses the reference site is already reached by full resolution
        /// (KerML §8.2.3.5.4), so naming it explicitly is redundant self-prefixing. An element in a
        /// separate resource shares no enclosing namespace with the source, so its path stays qualified —
        /// <c>SI::kg</c> is unaffected.
        /// </remarks>
        /// <param name="element">The leaf <see cref="IElement" /> to qualify; must be non-null.</param>
        /// <param name="sourcePoco">The reference site, or <see langword="null" /> to always walk to the root.</param>
        /// <returns>The short-form qualified name, relative to the nearest shared enclosing namespace.</returns>
        internal static string QueryShortQualifiedName(IElement element, IElement sourcePoco = null)
        {
            var enclosingScopes = new List<IElement>();

            var origin = sourcePoco is IImport { OwningRelatedElement: { } importOwner } ? importOwner : sourcePoco;

            for (var scope = origin; scope != null; scope = scope.owningNamespace)
            {
                enclosingScopes.Add(scope);
            }

            var segments = new Stack<string>();
            var current = element;

            while (current != null)
            {
                var preferred = SegmentNaming.QueryPreferredRawName(current);

                if (string.IsNullOrWhiteSpace(preferred))
                {
                    break;
                }

                segments.Push(SegmentNaming.Escape(preferred));

                current = current.owningNamespace;

                if (current != null && enclosingScopes.Any(scope => ReferenceEquals(scope, current)))
                {
                    break;
                }
            }

            return string.Join("::", segments);
        }
    }
}
