// ------------------------------------------------------------------------------------------------
// <copyright file="RelationshipOwnershipExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Extension methods that move, remove or replace the owned related elements of an <see cref="IRelationship"/>.
    /// </summary>
    public static class RelationshipOwnershipExtensions
    {
        /// <param name="relationship">The <see cref="IRelationship"/> that owns the related elements</param>
        extension(IRelationship relationship)
        {
            /// <summary>
            /// Moves an <see cref="IElement"/> to a position in the <see cref="IRelationship.OwnedRelatedElement"/> of the <paramref name="relationship"/>,
            /// detaching it from its current owning relationship when that is another one.
            /// </summary>
            /// <param name="element">The <see cref="IElement"/> to move</param>
            /// <param name="newIndex">The zero-based position the <paramref name="element"/> occupies after the move</param>
            /// <exception cref="ArgumentNullException">If <paramref name="relationship"/> or <paramref name="element"/> is null</exception>
            /// <exception cref="InvalidOperationException">If the <paramref name="relationship"/> may not own the <paramref name="element"/>,
            /// as for <see cref="ElementExtensions.AssignOwnership(IElement, IRelationship, IElement)"/></exception>
            /// <exception cref="ArgumentOutOfRangeException">If <paramref name="newIndex"/> is not a valid position, in which case nothing changes</exception>
            public void MoveOwnedRelatedElement(IElement element, int newIndex)
            {
                ElementExtensions.AssertCanOwnRelatedElement(relationship, element);
                ElementExtensions.InsertOwnedRelatedElement(relationship, element, newIndex);
            }

            /// <summary>
            /// Removes an <see cref="IElement"/> from the <see cref="IRelationship.OwnedRelatedElement"/> of the <paramref name="relationship"/>.
            /// </summary>
            /// <param name="element">The owned <see cref="IElement"/> to remove</param>
            /// <exception cref="ArgumentNullException">If <paramref name="relationship"/> or <paramref name="element"/> is null</exception>
            /// <exception cref="InvalidOperationException">If <paramref name="element"/> is not owned by <paramref name="relationship"/></exception>
            public void RemoveOwnedRelatedElement(IElement element)
            {
                if (relationship == null)
                {
                    throw new ArgumentNullException(nameof(relationship));
                }

                if (element == null)
                {
                    throw new ArgumentNullException(nameof(element));
                }

                if (!((IContainedRelationship)relationship).OwnedRelatedElement.Remove(element))
                {
                    throw new InvalidOperationException(
                        $"The element of type '{element.GetType().Name}' is not owned by the relationship of type '{relationship.GetType().Name}'.");
                }
            }

            /// <summary>
            /// Makes the <see cref="IRelationship.OwnedRelatedElement"/> of the <paramref name="relationship"/> exactly the <paramref name="orderedElements"/>,
            /// detaching those it no longer lists and taking the others over from their current owning relationships.
            /// </summary>
            /// <param name="orderedElements">The <see cref="IElement"/>s to own, in order</param>
            /// <exception cref="ArgumentNullException">If <paramref name="relationship"/>, <paramref name="orderedElements"/> or one of them is null</exception>
            /// <exception cref="ArgumentException">If <paramref name="orderedElements"/> lists an element more than once</exception>
            /// <exception cref="InvalidOperationException">If the <paramref name="relationship"/> may not own one of the <paramref name="orderedElements"/>,
            /// or is an <see cref="IOwningMembership"/> given more than one element, in which case nothing changes</exception>
            public void SetOwnedRelatedElements(IEnumerable<IElement> orderedElements)
            {
                var elements = orderedElements?.ToList();

                ElementExtensions.AssertCanOwnRelatedElements(relationship, elements);
                ElementExtensions.SynchronizeOwnedRelatedElements(relationship, elements);
            }
        }
    }
}
