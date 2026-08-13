// -------------------------------------------------------------------------------------------------
// <copyright file="OwnershipTreeLibraryTypeIndex.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Indexes model-library Types by qualified name from the library ownership tree.
    /// </summary>
    /// <remarks>
    /// The walk reads the raw ownedRelationship of each Namespace rather than any derived membership
    /// property, so it cannot re-enter name resolution: resolution consults inheritedMembership, which is
    /// what the implied layer supplies. The index is fully populated by <see cref="Build" /> before any
    /// lookup, for the same reason.
    /// </remarks>
    public class OwnershipTreeLibraryTypeIndex : ILibraryTypeIndex
    {
        /// <summary>
        /// The separator between the segments of a qualified name.
        /// </summary>
        private const string QualifiedNameSeparator = "::";

        /// <summary>
        /// The indexed Types, keyed by qualified name.
        /// </summary>
        private readonly Dictionary<string, IType> typesByQualifiedName;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnershipTreeLibraryTypeIndex" /> class.
        /// </summary>
        /// <param name="typesByQualifiedName">The indexed Types, keyed by qualified name.</param>
        private OwnershipTreeLibraryTypeIndex(Dictionary<string, IType> typesByQualifiedName)
        {
            this.typesByQualifiedName = typesByQualifiedName;
        }

        /// <summary>
        /// Gets the number of indexed Types.
        /// </summary>
        public int Count => this.typesByQualifiedName.Count;

        /// <summary>
        /// Builds an index over the supplied library root Namespaces.
        /// </summary>
        /// <param name="libraryNamespaces">The library root Namespaces, typically the referenced Namespaces reported by a deserializer.</param>
        /// <returns>A fully populated index.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="libraryNamespaces" /> is null.</exception>
        public static OwnershipTreeLibraryTypeIndex Build(IEnumerable<INamespace> libraryNamespaces)
        {
            if (libraryNamespaces == null)
            {
                throw new ArgumentNullException(nameof(libraryNamespaces));
            }

            var typesByQualifiedName = new Dictionary<string, IType>(StringComparer.Ordinal);
            var visited = new HashSet<IElement>();

            foreach (var libraryNamespace in libraryNamespaces.Where(libraryNamespace => libraryNamespace != null))
            {
                Index(libraryNamespace, null, typesByQualifiedName, visited);
            }

            return new OwnershipTreeLibraryTypeIndex(typesByQualifiedName);
        }

        /// <summary>
        /// Attempts to resolve the library Type carrying the supplied qualified name.
        /// </summary>
        /// <param name="qualifiedName">The qualified name, for example Occurrences::Occurrence::suboccurrences.</param>
        /// <param name="type">When this method returns true, the resolved Type; otherwise null.</param>
        /// <returns>True when the qualified name resolves to an indexed Type.</returns>
        public bool TryGetType(string qualifiedName, out IType type)
        {
            if (string.IsNullOrWhiteSpace(qualifiedName))
            {
                type = null;

                return false;
            }

            return this.typesByQualifiedName.TryGetValue(qualifiedName, out type);
        }

        /// <summary>
        /// Indexes an Element and, when it is a Namespace, everything it owns.
        /// </summary>
        /// <param name="element">The Element to index.</param>
        /// <param name="parentQualifiedName">The qualified name of the owning Namespace, or null at a root.</param>
        /// <param name="typesByQualifiedName">The index being populated.</param>
        /// <param name="visited">The Elements already walked, guarding against a cyclic ownership graph.</param>
        private static void Index(IElement element, string parentQualifiedName, Dictionary<string, IType> typesByQualifiedName, HashSet<IElement> visited)
        {
            if (!visited.Add(element))
            {
                return;
            }

            var qualifiedName = QueryQualifiedName(element, parentQualifiedName);

            if (element is IType indexableType && qualifiedName != null)
            {
                typesByQualifiedName[qualifiedName] = indexableType;
            }

            if (element is not INamespace owningNamespace)
            {
                return;
            }

            // The raw ownedRelationship is read rather than the derived ownedMembership so the walk stays
            // independent of every derivation the implied layer is meant to feed.
            var ownedElements = owningNamespace.OwnedRelationship
                .OfType<IOwningMembership>()
                .Select(membership => membership.ownedMemberElement)
                .Where(ownedElement => ownedElement != null);

            foreach (var ownedElement in ownedElements)
            {
                Index(ownedElement, qualifiedName ?? parentQualifiedName, typesByQualifiedName, visited);
            }
        }

        /// <summary>
        /// Composes the qualified name of an Element from its owner's qualified name and its declared name.
        /// </summary>
        /// <param name="element">The Element to name.</param>
        /// <param name="parentQualifiedName">The qualified name of the owning Namespace, or null at a root.</param>
        /// <returns>The qualified name, or null when the Element has no declared name.</returns>
        private static string QueryQualifiedName(IElement element, string parentQualifiedName)
        {
            if (string.IsNullOrWhiteSpace(element.DeclaredName))
            {
                return null;
            }

            return string.IsNullOrWhiteSpace(parentQualifiedName)
                ? element.DeclaredName
                : $"{parentQualifiedName}{QualifiedNameSeparator}{element.DeclaredName}";
        }
    }
}
