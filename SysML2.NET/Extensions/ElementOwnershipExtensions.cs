// ------------------------------------------------------------------------------------------------
// <copyright file="ElementOwnershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Collections;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Extension methods that move, remove or replace the <see cref="IElement.OwnedRelationship"/> of an <see cref="IElement"/>.
    /// </summary>
    public static class ElementOwnershipExtensions
    {
        /// <param name="source">The <see cref="IElement"/> that owns the relationships</param>
        extension(IElement source)
        {
            /// <summary>
            /// Moves a <see cref="IRelationship"/> to a position in the <see cref="IElement.OwnedRelationship"/> of the <paramref name="source"/>,
            /// detaching it from its current owner when that is another element.
            /// </summary>
            /// <param name="relationship">The <see cref="IRelationship"/> to move</param>
            /// <param name="newIndex">The zero-based position the <paramref name="relationship"/> occupies after the move</param>
            /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="relationship"/> is null</exception>
            /// <exception cref="InvalidOperationException">If the <paramref name="source"/> may not own the <paramref name="relationship"/>,
            /// as for <see cref="ElementExtensions.AssignOwnership(IElement, IRelationship)"/></exception>
            /// <exception cref="ArgumentOutOfRangeException">If <paramref name="newIndex"/> is not a valid position, in which case nothing changes</exception>
            public void MoveOwnedRelationship(IRelationship relationship, int newIndex)
            {
                ElementExtensions.AssertCanOwnRelationship(source, relationship);
                ElementExtensions.InsertOwnedRelationship(source, relationship, newIndex);
            }

            /// <summary>
            /// Removes a <see cref="IRelationship"/> from the <see cref="IElement.OwnedRelationship"/> of the <paramref name="source"/>; the
            /// relationship keeps its own owned related elements.
            /// </summary>
            /// <param name="relationship">The owned <see cref="IRelationship"/> to remove</param>
            /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="relationship"/> is null</exception>
            /// <exception cref="InvalidOperationException">If <paramref name="relationship"/> is not owned by <paramref name="source"/></exception>
            public void RemoveOwnedRelationship(IRelationship relationship)
            {
                OwnedRelationshipsContaining(source, relationship).Remove(relationship);
            }

            /// <summary>
            /// Makes the <see cref="IElement.OwnedRelationship"/> of the <paramref name="source"/> exactly the <paramref name="orderedRelationships"/>,
            /// detaching those it no longer lists and taking the others over from their current owners.
            /// </summary>
            /// <param name="orderedRelationships">The <see cref="IRelationship"/>s to own, in order</param>
            /// <exception cref="ArgumentNullException">If <paramref name="source"/>, <paramref name="orderedRelationships"/> or one of them is null</exception>
            /// <exception cref="ArgumentException">If <paramref name="orderedRelationships"/> lists a relationship more than once</exception>
            /// <exception cref="InvalidOperationException">If the <paramref name="source"/> may not own one of the <paramref name="orderedRelationships"/>,
            /// as for <see cref="ElementExtensions.AssignOwnership(IElement, IRelationship)"/>, in which case nothing changes</exception>
            public void SetOwnedRelationships(IEnumerable<IRelationship> orderedRelationships)
            {
                var relationships = orderedRelationships?.ToList();

                ElementExtensions.AssertCanOwnRelationships(source, relationships);
                ElementExtensions.SynchronizeOwnedRelationships(source, relationships);
            }
        }

        /// <summary>
        /// Returns the containment list of the <paramref name="source"/> after asserting that it owns the <paramref name="relationship"/>.
        /// </summary>
        /// <param name="source">The owning <see cref="IElement"/></param>
        /// <param name="relationship">The <see cref="IRelationship"/> expected to be owned</param>
        /// <returns>The <see cref="ContainerList{TParent, TChild}"/> backing <see cref="IElement.OwnedRelationship"/></returns>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="relationship"/> is null</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="relationship"/> is not owned by <paramref name="source"/></exception>
        private static ContainerList<IElement, IRelationship> OwnedRelationshipsContaining(IElement source, IRelationship relationship)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (relationship == null)
            {
                throw new ArgumentNullException(nameof(relationship));
            }

            var ownedRelationships = ((IContainedElement)source).OwnedRelationship;

            return ownedRelationships.Contains(relationship)
                ? ownedRelationships
                : throw new InvalidOperationException(
                    $"The relationship of type '{relationship.GetType().Name}' is not owned by the element of type '{source.GetType().Name}'.");
        }
    }
}
