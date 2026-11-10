// ------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;

    /// <summary>
    /// Extension methods for the <see cref="IElement"/> interface
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Ownership assignment for an <see cref="IElement"/>
        /// </summary>
        /// <param name="source">The owning <see cref="IElement"/></param>
        extension(IElement source)
        {
            /// <summary>
            /// Makes the <paramref name="source"/> own the <paramref name="bridgeRelationship"/>, which merely references its other related elements.
            /// </summary>
            /// <param name="bridgeRelationship">The <see cref="IRelationship"/> to own</param>
            /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="bridgeRelationship"/> is null</exception>
            /// <exception cref="InvalidOperationException">If the ownership breaks a containment rule, or the relationship is an <see cref="IOwningMembership"/></exception>
            public void AssignOwnership(IRelationship bridgeRelationship)
            {
                AssertCanOwnRelationship(source, bridgeRelationship);

                if (bridgeRelationship is IOwningMembership)
                {
                    throw new InvalidOperationException(
                        $"The relationship of type '{bridgeRelationship.GetType().Name}' is an IOwningMembership and always owns a member element. Use the AssignOwnership(IElement, IRelationship, IElement) overload to supply the owned target element.");
                }

                AppendOwnedRelationship(source, bridgeRelationship);
            }

            /// <summary>
            /// Makes the <paramref name="source"/> own the <paramref name="bridgeRelationship"/>, which in turn owns the <paramref name="target"/>.
            /// </summary>
            /// <param name="bridgeRelationship">The <see cref="IRelationship"/> to own</param>
            /// <param name="target">The <see cref="IElement"/> owned by the <paramref name="bridgeRelationship"/></param>
            /// <exception cref="ArgumentNullException">If one of the parameters is null</exception>
            /// <exception cref="InvalidOperationException">If the ownership breaks a containment rule, or an <see cref="IOwningMembership"/> already owns another element</exception>
            /// <remarks>
            /// An element the <paramref name="bridgeRelationship"/> already owns keeps its position.
            /// </remarks>
            public void AssignOwnership(IRelationship bridgeRelationship, IElement target)
            {
                AssertCanOwnRelationship(source, bridgeRelationship);
                AssertCanOwnRelatedElement(bridgeRelationship, target);

                if (source == target)
                {
                    throw new InvalidOperationException("The parent cannot own itself.");
                }

                if (IsContainedTransitivelyBy(source, target))
                {
                    throw new InvalidOperationException(
                        $"Containment cycle: the supplied source of type '{source.GetType().Name}' is already transitively contained by the target of type '{target.GetType().Name}'.");
                }

                AppendOwnedRelationship(source, bridgeRelationship);
                AppendOwnedRelatedElement(bridgeRelationship, target);
            }
        }

        /// <summary>
        /// Appends the <paramref name="element"/> to the owned related elements of the <paramref name="owner"/>, unless already there.
        /// </summary>
        /// <param name="owner">The owning <see cref="IRelationship"/></param>
        /// <param name="element">The <see cref="IElement"/> to own</param>
        internal static void AppendOwnedRelatedElement(IRelationship owner, IElement element)
        {
            Append(((IContainedRelationship)owner).OwnedRelatedElement, element);
        }

        /// <summary>
        /// Appends the <paramref name="relationship"/> to the owned relationships of the <paramref name="owner"/>, unless already there.
        /// </summary>
        /// <param name="owner">The owning <see cref="IElement"/></param>
        /// <param name="relationship">The <see cref="IRelationship"/> to own</param>
        internal static void AppendOwnedRelationship(IElement owner, IRelationship relationship)
        {
            Append(((IContainedElement)owner).OwnedRelationship, relationship);
        }

        /// <summary>
        /// Asserts that the <paramref name="relationship"/> may own the <paramref name="element"/>.
        /// </summary>
        /// <param name="relationship">The owning <see cref="IRelationship"/></param>
        /// <param name="element">The <see cref="IElement"/> to own</param>
        /// <exception cref="ArgumentNullException">If <paramref name="relationship"/> or <paramref name="element"/> is null</exception>
        /// <exception cref="InvalidOperationException">If a containment rule is broken</exception>
        internal static void AssertCanOwnRelatedElement(IRelationship relationship, IElement element)
        {
            AssertCanContain(relationship, element);

            // OwningMembership::ownedMemberElement [1..1] has no first-of-many derivation, unlike Annotation::ownedAnnotatingElement
            if (relationship is IOwningMembership && relationship.OwnedRelatedElement.Any(ownedElement => ownedElement != element))
            {
                throw new InvalidOperationException($"The relationship of type '{relationship.GetType().Name}' already owns another element; remove it before assigning a new one.");
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="relationship"/> may own exactly the <paramref name="elements"/>.
        /// </summary>
        /// <param name="relationship">The owning <see cref="IRelationship"/></param>
        /// <param name="elements">The <see cref="IElement"/>s to own</param>
        /// <exception cref="ArgumentNullException">If an argument or an element is null</exception>
        /// <exception cref="ArgumentException">If an element is listed twice</exception>
        /// <exception cref="InvalidOperationException">If a containment rule is broken</exception>
        internal static void AssertCanOwnRelatedElements(IRelationship relationship, IReadOnlyList<IElement> elements)
        {
            if (relationship == null)
            {
                throw new ArgumentNullException(nameof(relationship));
            }

            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            foreach (var element in elements)
            {
                AssertCanContain(relationship, element);
            }

            AssertDistinct(elements, nameof(elements));

            if (relationship is IOwningMembership && elements.Count > 1)
            {
                throw new InvalidOperationException($"The relationship of type '{relationship.GetType().Name}' owns a single member element; {elements.Count} were supplied.");
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="source"/> may own exactly the <paramref name="relationships"/>.
        /// </summary>
        /// <param name="source">The owning <see cref="IElement"/></param>
        /// <param name="relationships">The <see cref="IRelationship"/>s to own</param>
        /// <exception cref="ArgumentNullException">If an argument or a relationship is null</exception>
        /// <exception cref="ArgumentException">If a relationship is listed twice</exception>
        /// <exception cref="InvalidOperationException">If a containment rule is broken</exception>
        internal static void AssertCanOwnRelationships(IElement source, IReadOnlyList<IRelationship> relationships)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (relationships == null)
            {
                throw new ArgumentNullException(nameof(relationships));
            }

            foreach (var relationship in relationships)
            {
                AssertCanOwnRelationship(source, relationship);
            }

            AssertDistinct(relationships, nameof(relationships));
        }

        /// <summary>
        /// Asserts that the <paramref name="source"/> may own the <paramref name="relationship"/>.
        /// </summary>
        /// <param name="source">The owning <see cref="IElement"/></param>
        /// <param name="relationship">The <see cref="IRelationship"/> to own</param>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="relationship"/> is null</exception>
        /// <exception cref="InvalidOperationException">If a containment rule is broken</exception>
        internal static void AssertCanOwnRelationship(IElement source, IRelationship relationship)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (relationship == null)
            {
                throw new ArgumentNullException(nameof(relationship));
            }

            if (source == relationship)
            {
                throw new InvalidOperationException("The relationship can not own itself.");
            }

            if (!relationship.QueryIsValidContainmentOwner(source))
            {
                throw new InvalidOperationException(
                    $"The source of type '{source.GetType().Name}' is not a valid containment owner for the relationship of type '{relationship.GetType().Name}'; expected an instance of '{relationship.ExpectedContainmentOwnerTypeName()}'.");
            }

            if (IsContainedTransitivelyBy(source, relationship))
            {
                throw new InvalidOperationException(
                    $"Containment cycle: the supplied source of type '{source.GetType().Name}' is already transitively contained by the bridge relationship of type '{relationship.GetType().Name}'.");
            }
        }

        /// <summary>
        /// Places the <paramref name="element"/> at <paramref name="index"/> in the owned related elements of the <paramref name="owner"/>.
        /// </summary>
        /// <param name="owner">The owning <see cref="IRelationship"/></param>
        /// <param name="element">The <see cref="IElement"/> to own</param>
        /// <param name="index">The zero-based position afterwards</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> is not a valid position</exception>
        internal static void InsertOwnedRelatedElement(IRelationship owner, IElement element, int index)
        {
            Attach(((IContainedRelationship)owner).OwnedRelatedElement, element, index);
        }

        /// <summary>
        /// Places the <paramref name="relationship"/> at <paramref name="index"/> in the owned relationships of the <paramref name="owner"/>.
        /// </summary>
        /// <param name="owner">The owning <see cref="IElement"/></param>
        /// <param name="relationship">The <see cref="IRelationship"/> to own</param>
        /// <param name="index">The zero-based position afterwards</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> is not a valid position</exception>
        internal static void InsertOwnedRelationship(IElement owner, IRelationship relationship, int index)
        {
            Attach(((IContainedElement)owner).OwnedRelationship, relationship, index);
        }

        /// <summary>
        /// Returns the single element of <paramref name="source"/>, for a derived <c>[1..1]</c> property.
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="source">The candidate matches</param>
        /// <param name="subjectName">The subject name used in the exception messages</param>
        /// <returns>The single element</returns>
        /// <exception cref="IncompleteModelException">If <paramref name="source"/> is empty</exception>
        /// <exception cref="MultiplicityViolationException">If <paramref name="source"/> holds more than one element</exception>
        internal static T SingleStrict<T>(this IEnumerable<T> source, string subjectName)
            where T : class
        {
            T found = null;

            foreach (var item in source)
            {
                if (found is not null)
                {
                    throw new MultiplicityViolationException($"{subjectName} contains more than one element of type {typeof(T).Name}");
                }

                found = item;
            }

            return found ?? throw new IncompleteModelException($"{subjectName} must have an element of type {typeof(T).Name}");
        }

        /// <summary>
        /// Returns the single element of type <typeparamref name="TResult"/> in <paramref name="source"/>, for a derived <c>[1..1]</c> property.
        /// </summary>
        /// <typeparam name="TResult">The type to filter on</typeparam>
        /// <param name="source">The candidate matches</param>
        /// <param name="subjectName">The subject name used in the exception messages</param>
        /// <returns>The single element of type <typeparamref name="TResult"/></returns>
        /// <exception cref="IncompleteModelException">If no element of type <typeparamref name="TResult"/> is present</exception>
        /// <exception cref="MultiplicityViolationException">If more than one element of type <typeparamref name="TResult"/> is present</exception>
        internal static TResult SingleStrict<TResult>(this System.Collections.IEnumerable source, string subjectName)
            where TResult : class
            => source.OfType<TResult>().SingleStrict(subjectName);

        /// <summary>
        /// Returns the element of <paramref name="source"/>, if any, for a derived <c>[0..1]</c> property.
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="source">The candidate matches</param>
        /// <param name="subjectName">The subject name used in the exception message</param>
        /// <returns>The single element, or <c>null</c> when <paramref name="source"/> is empty</returns>
        /// <exception cref="MultiplicityViolationException">If <paramref name="source"/> holds more than one element</exception>
        /// <remarks>
        /// Not for an OCL derivation that elects the first of many (<c>->first()</c>, <c>->at(1)</c>); keep <c>FirstOrDefault</c> there.
        /// </remarks>
        internal static T SingleOrDefaultStrict<T>(this IEnumerable<T> source, string subjectName)
            where T : class
        {
            T found = null;

            foreach (var item in source)
            {
                if (found is not null)
                {
                    throw new MultiplicityViolationException($"{subjectName} contains more than one element of type {typeof(T).Name}");
                }

                found = item;
            }

            return found;
        }

        /// <summary>
        /// Returns the element of type <typeparamref name="TResult"/> in <paramref name="source"/>, if any, for a derived <c>[0..1]</c> property.
        /// </summary>
        /// <typeparam name="TResult">The type to filter on</typeparam>
        /// <param name="source">The candidate matches</param>
        /// <param name="subjectName">The subject name used in the exception message</param>
        /// <returns>The single element of type <typeparamref name="TResult"/>, or <c>null</c> when there is none</returns>
        /// <exception cref="MultiplicityViolationException">If more than one element of type <typeparamref name="TResult"/> is present</exception>
        /// <remarks>
        /// Not for an OCL derivation that elects the first of many (<c>->first()</c>, <c>->at(1)</c>); keep <c>FirstOrDefault</c> there.
        /// </remarks>
        internal static TResult SingleOrDefaultStrict<TResult>(this System.Collections.IEnumerable source, string subjectName)
            where TResult : class
            => source.OfType<TResult>().SingleOrDefaultStrict(subjectName);

        /// <summary>
        /// Makes the owned related elements of the <paramref name="owner"/> exactly the <paramref name="orderedElements"/>, without checking the containment rules.
        /// </summary>
        /// <param name="owner">The owning <see cref="IRelationship"/></param>
        /// <param name="orderedElements">The <see cref="IElement"/>s to own, in order</param>
        internal static void SynchronizeOwnedRelatedElements(IRelationship owner, IEnumerable<IElement> orderedElements)
        {
            Synchronize(((IContainedRelationship)owner).OwnedRelatedElement, orderedElements);
        }

        /// <summary>
        /// Makes the owned relationships of the <paramref name="owner"/> exactly the <paramref name="orderedRelationships"/>, without checking the containment rules.
        /// </summary>
        /// <param name="owner">The owning <see cref="IElement"/></param>
        /// <param name="orderedRelationships">The <see cref="IRelationship"/>s to own, in order</param>
        internal static void SynchronizeOwnedRelationships(IElement owner, IEnumerable<IRelationship> orderedRelationships)
        {
            Synchronize(((IContainedElement)owner).OwnedRelationship, orderedRelationships);
        }

        /// <summary>
        /// Appends the <paramref name="child"/> to the <paramref name="ownedList"/>, unless already there.
        /// </summary>
        /// <typeparam name="TParent">The owner type</typeparam>
        /// <typeparam name="TChild">The owned type</typeparam>
        /// <param name="ownedList">The containment list</param>
        /// <param name="child">The element to own</param>
        private static void Append<TParent, TChild>(ContainerList<TParent, TChild> ownedList, TChild child)
            where TParent : IElement
            where TChild : IElement
        {
            if (!ownedList.Contains(child))
            {
                Attach(ownedList, child, ownedList.Count);
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="relationship"/> may contain the <paramref name="element"/>, whatever it already owns.
        /// </summary>
        /// <param name="relationship">The owning <see cref="IRelationship"/></param>
        /// <param name="element">The <see cref="IElement"/> to own</param>
        /// <exception cref="ArgumentNullException">If <paramref name="relationship"/> or <paramref name="element"/> is null</exception>
        /// <exception cref="InvalidOperationException">If the element is the relationship, has the wrong type, or contains the relationship</exception>
        private static void AssertCanContain(IRelationship relationship, IElement element)
        {
            if (relationship == null)
            {
                throw new ArgumentNullException(nameof(relationship));
            }

            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (relationship == element)
            {
                throw new InvalidOperationException("The relationship can not own itself.");
            }

            if (!relationship.QueryIsValidForContainment(element))
            {
                throw new InvalidOperationException(
                    $"The target of type '{element.GetType().Name}' is not a valid containment target for the relationship of type '{relationship.GetType().Name}'; expected an instance of '{relationship.ExpectedContainmentTypeName()}'.");
            }

            if (IsContainedTransitivelyBy(relationship, element))
            {
                throw new InvalidOperationException(
                    $"Containment cycle: the relationship of type '{relationship.GetType().Name}' is already transitively contained by the target of type '{element.GetType().Name}'.");
            }
        }

        /// <summary>
        /// Asserts that no element is listed twice in the <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="items">The items to check</param>
        /// <param name="parameterName">The name of the parameter that supplied the <paramref name="items"/></param>
        /// <exception cref="ArgumentException">If an element is listed twice</exception>
        private static void AssertDistinct<T>(IReadOnlyList<T> items, string parameterName) where T : IElement
        {
            if (items.Distinct().Count() != items.Count)
            {
                throw new ArgumentException("An owned element can be listed only once.", parameterName);
            }
        }

        /// <summary>
        /// Places the <paramref name="child"/> at <paramref name="index"/> in the <paramref name="ownedList"/>, detaching it from any other owner first.
        /// </summary>
        /// <typeparam name="TParent">The owner type</typeparam>
        /// <typeparam name="TChild">The owned type</typeparam>
        /// <param name="ownedList">The containment list</param>
        /// <param name="child">The element to own</param>
        /// <param name="index">The zero-based position afterwards</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="index"/> is not a valid position</exception>
        private static void Attach<TParent, TChild>(ContainerList<TParent, TChild> ownedList, TChild child, int index)
            where TParent : IElement
            where TChild : IElement
        {
            var currentIndex = ownedList.IndexOf(child);
            var lastValidIndex = currentIndex >= 0 ? ownedList.Count - 1 : ownedList.Count;

            if (index < 0 || index > lastValidIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"The index {index} is outside the range [0, {lastValidIndex}].");
            }

            if (currentIndex >= 0)
            {
                ownedList.Move(currentIndex, index);
                return;
            }

            DetachFromOwner(child);
            ownedList.Insert(index, child);
        }

        /// <summary>
        /// Removes the <paramref name="element"/> from whichever owner holds it.
        /// </summary>
        /// <param name="element">The <see cref="IElement"/> to detach</param>
        private static void DetachFromOwner(IElement element)
        {
            if (element.OwningRelationship != null && !((IContainedRelationship)element.OwningRelationship).OwnedRelatedElement.Remove(element))
            {
                ((IContainedElement)element).OwningRelationship = null;
            }

            if (element is IRelationship { OwningRelatedElement: not null } relationship
                && !((IContainedElement)relationship.OwningRelatedElement).OwnedRelationship.Remove(relationship))
            {
                ((IContainedRelationship)relationship).OwningRelatedElement = null;
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="candidateAncestor"/> transitively owns the <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The <see cref="IElement"/> whose ownership chain is walked</param>
        /// <param name="candidateAncestor">The <see cref="IElement"/> looked for in that chain</param>
        /// <returns><c>true</c> if the <paramref name="candidateAncestor"/> is in the chain</returns>
        private static bool IsContainedTransitivelyBy(IElement element, IElement candidateAncestor)
        {
            for (var current = OwnerOf(element); current != null; current = OwnerOf(current))
            {
                if (current == candidateAncestor)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the direct owner of the <paramref name="element"/>: its owning relationship, or for a relationship its owning related element.
        /// </summary>
        /// <param name="element">The owned <see cref="IElement"/></param>
        /// <returns>The direct owner, or <c>null</c></returns>
        private static IElement OwnerOf(IElement element)
        {
            return (IElement)element.OwningRelationship ?? (element as IRelationship)?.OwningRelatedElement;
        }

        /// <summary>
        /// Makes the <paramref name="ownedList"/> exactly the <paramref name="orderedChildren"/>, in order.
        /// </summary>
        /// <typeparam name="TParent">The owner type</typeparam>
        /// <typeparam name="TChild">The owned type</typeparam>
        /// <param name="ownedList">The containment list</param>
        /// <param name="orderedChildren">The elements to own, in order</param>
        private static void Synchronize<TParent, TChild>(ContainerList<TParent, TChild> ownedList, IEnumerable<TChild> orderedChildren)
            where TParent : IElement
            where TChild : IElement
        {
            var children = orderedChildren.Distinct().ToList();

            foreach (var removedChild in ownedList.Except(children).ToList())
            {
                ownedList.Remove(removedChild);
            }

            for (var childIndex = 0; childIndex < children.Count; childIndex++)
            {
                Attach(ownedList, children[childIndex], childIndex);
            }
        }
    }
}
