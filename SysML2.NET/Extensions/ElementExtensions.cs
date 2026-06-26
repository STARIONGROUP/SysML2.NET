// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;

    /// <summary>
    /// Extension method for the <see cref="IElement"/> interface
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Assigns the containment ownership of the <paramref name="bridgeRelationship"/> to a source <see cref="IElement"/>,
        /// without contributing any element to <see cref="IRelationship.OwnedRelatedElement"/>.
        /// </summary>
        /// <param name="source">The container <see cref="IElement"/> that will own the <paramref name="bridgeRelationship"/></param>
        /// <param name="bridgeRelationship">The <see cref="IRelationship"/> to be owned by the <paramref name="source"/></param>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="bridgeRelationship"/> is null</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="source"/> equals <paramref name="bridgeRelationship"/>,
        /// if <paramref name="bridgeRelationship"/> is an <see cref="IOwningMembership"/> (which always owns its
        /// member element and therefore requires the three-argument overload),
        /// if <paramref name="source"/>'s runtime type does not satisfy the typed property declared by the bridge's
        /// metaclass for its <see cref="IRelationship.OwningRelatedElement"/> (e.g. an <c>IFeatureMembership</c> requires
        /// an <c>IType</c> source; an <c>IMembership</c> requires an <c>INamespace</c> source — per the KerML specification),
        /// or if <paramref name="bridgeRelationship"/> already transitively contains <paramref name="source"/> (which would
        /// produce a containment cycle)</exception>
        /// <remarks>
        /// Use this overload for the "owning reference" pattern, where the <paramref name="source"/> owns the relationship
        /// but the relationship merely references its other related elements rather than containing them. Typical examples
        /// are <c>FeatureTyping</c>, <c>Subsetting</c>, <c>Redefinition</c>, and <c>Conjugation</c>: the typed/specializing
        /// feature owns the relationship, while the referenced type/specialized feature is not an owned related element.
        /// For the "owning containment" pattern (e.g. <c>FeatureMembership</c>, <c>OwningMembership</c>), use the
        /// <see cref="AssignOwnership(IElement, IRelationship, IElement)"/> overload instead.
        /// </remarks>
        public static void AssignOwnership(this IElement source, IRelationship bridgeRelationship)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (bridgeRelationship == null)
            {
                throw new ArgumentNullException(nameof(bridgeRelationship));
            }

            if (source == bridgeRelationship)
            {
                throw new InvalidOperationException("The relationship can not own itself.");
            }

            if (bridgeRelationship is IOwningMembership)
            {
                throw new InvalidOperationException(
                    $"The relationship of type '{bridgeRelationship.GetType().Name}' is an IOwningMembership and always owns a member element. Use the AssignOwnership(IElement, IRelationship, IElement) overload to supply the owned target element.");
            }

            if (!bridgeRelationship.QueryIsValidContainmentOwner(source))
            {
                throw new InvalidOperationException(
                    $"The source of type '{source.GetType().Name}' is not a valid containment owner for the relationship of type '{bridgeRelationship.GetType().Name}'; expected an instance of '{bridgeRelationship.ExpectedContainmentOwnerTypeName()}'.");
            }

            if (IsContainedTransitivelyBy(source, bridgeRelationship))
            {
                throw new InvalidOperationException(
                    $"Containment cycle: the supplied source of type '{source.GetType().Name}' is already transitively contained by the bridge relationship of type '{bridgeRelationship.GetType().Name}'.");
            }

            AssignOwnershipCore(source, bridgeRelationship);
        }

        /// <summary>
        /// Assigns the containment ownership of a target <see cref="IElement"/> to a source <see cref="IElement"/> via the bridge <see cref="IRelationship"/>
        /// </summary>
        /// <param name="source">The container<see cref="IElement"/></param>
        /// <param name="bridgeRelationship">The bridge <see cref="IRelationship"/></param>
        /// <param name="target">The contained <see cref="IElement"/></param>
        /// <exception cref="ArgumentNullException">If one of the parameter is null</exception>
        /// <exception cref="InvalidOperationException">If the <paramref name="source"/> equals the <paramref name="target"/>,
        ///  if the <paramref name="bridgeRelationship"/> equals the <paramref name="target"/>,
        ///  if <paramref name="bridgeRelationship"/> is neither an <see cref="IOwningMembership"/> nor an
        ///  <see cref="IAnnotation"/> (the only relationship metaclasses that subset
        ///  <see cref="IRelationship.OwnedRelatedElement"/> with composite aggregation),
        ///  if <paramref name="source"/>'s runtime type does not satisfy the typed property declared by the bridge's
        ///  metaclass for its <see cref="IRelationship.OwningRelatedElement"/> (e.g. an <c>IFeatureMembership</c>
        ///  requires an <c>IType</c> source; an <c>IMembership</c> requires an <c>INamespace</c> source — per the
        ///  KerML specification),
        ///  if <paramref name="target"/>'s runtime type does not satisfy the typed composite property declared by the
        ///  bridge's metaclass for its <see cref="IRelationship.OwnedRelatedElement"/> (e.g. an
        ///  <c>IFeatureMembership</c> requires an <c>IFeature</c> target),
        ///  or if <paramref name="bridgeRelationship"/> or <paramref name="target"/> already transitively contains
        ///  <paramref name="source"/> (which would produce a containment cycle)</exception>
        /// <remarks>
        /// Use this overload for the "owning containment" pattern, where the <paramref name="bridgeRelationship"/> contains
        /// the <paramref name="target"/> as one of its <see cref="IRelationship.OwnedRelatedElement"/>. The only relationship
        /// metaclasses that can carry an owned related element are <see cref="IOwningMembership"/> (and all its specialisations
        /// such as <c>FeatureMembership</c>, <c>ParameterMembership</c>, <c>VariantMembership</c>, <c>FeatureValue</c>, etc.)
        /// and <see cref="IAnnotation"/>. For the "owning reference" pattern (e.g. <c>FeatureTyping</c>, <c>Subsetting</c>),
        /// use the <see cref="AssignOwnership(IElement, IRelationship)"/> overload instead.
        /// Note: the source is the container element and the target is the containee.
        /// </remarks>
        public static void AssignOwnership(this IElement source, IRelationship bridgeRelationship, IElement target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (bridgeRelationship == null)
            {
                throw new ArgumentNullException(nameof(bridgeRelationship));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (source == bridgeRelationship)
            {
                throw new InvalidOperationException("The relationship can not own itself.");
            }

            if (source == target)
            {
                throw new InvalidOperationException("The parent cannot own itself.");
            }

            if (bridgeRelationship == target)
            {
                throw new InvalidOperationException("The relationship can not own itself.");
            }

            if (bridgeRelationship is not IOwningMembership and not IAnnotation)
            {
                throw new InvalidOperationException(
                    $"The relationship of type '{bridgeRelationship.GetType().Name}' is neither an IOwningMembership nor an IAnnotation and therefore cannot own a related element. Use the AssignOwnership(IElement, IRelationship) overload instead.");
            }

            if (!bridgeRelationship.QueryIsValidContainmentOwner(source))
            {
                throw new InvalidOperationException(
                    $"The source of type '{source.GetType().Name}' is not a valid containment owner for the relationship of type '{bridgeRelationship.GetType().Name}'; expected an instance of '{bridgeRelationship.ExpectedContainmentOwnerTypeName()}'.");
            }

            if (!bridgeRelationship.QueryIsValidForContainment(target))
            {
                throw new InvalidOperationException(
                    $"The target of type '{target.GetType().Name}' is not a valid containment target for the relationship of type '{bridgeRelationship.GetType().Name}'; expected an instance of '{ExpectedContainmentTypeName(bridgeRelationship)}'.");
            }

            if (IsContainedTransitivelyBy(source, bridgeRelationship))
            {
                throw new InvalidOperationException(
                    $"Containment cycle: the supplied source of type '{source.GetType().Name}' is already transitively contained by the bridge relationship of type '{bridgeRelationship.GetType().Name}'.");
            }

            if (IsContainedTransitivelyBy(source, target))
            {
                throw new InvalidOperationException(
                    $"Containment cycle: the supplied source of type '{source.GetType().Name}' is already transitively contained by the target of type '{target.GetType().Name}'.");
            }

            AssignOwnershipCore(source, bridgeRelationship);

            if (target.OwningRelationship != null && target.OwningRelationship != bridgeRelationship)
            {
                ((IContainedRelationship)target.OwningRelationship).OwnedRelatedElement.Remove(target);
            }

            ((IContainedRelationship)bridgeRelationship).OwnedRelatedElement.Add(target);
        }

        /// <summary>
        /// Performs the owner-side mutation that attaches a <paramref name="bridgeRelationship"/> to a <paramref name="source"/>
        /// without applying any of the public-overload guard rules.
        /// </summary>
        /// <param name="source">The container <see cref="IElement"/></param>
        /// <param name="bridgeRelationship">The <see cref="IRelationship"/> to be attached to the <paramref name="source"/></param>
        /// <remarks>
        /// Internal helper shared by both <c>AssignOwnership</c> public overloads. Callers are responsible for argument
        /// validation; this method performs no checks of its own.
        /// </remarks>
        private static void AssignOwnershipCore(IElement source, IRelationship bridgeRelationship)
        {
            if (bridgeRelationship.OwningRelatedElement != null && bridgeRelationship.OwningRelatedElement != source)
            {
                ((IContainedElement)bridgeRelationship.OwningRelatedElement).OwnedRelationship.Remove(bridgeRelationship);
            }

            ((IContainedRelationship)bridgeRelationship).OwningRelatedElement = source;

            if (!source.OwnedRelationship.Contains(bridgeRelationship))
            {
                ((IContainedElement)source).OwnedRelationship.Add(bridgeRelationship);
            }
        }

        /// <summary>
        /// Returns the single element from <paramref name="source"/>, throwing
        /// <see cref="IncompleteModelException"/> if the source is empty and
        /// <see cref="MultiplicityViolationException"/> if it contains more than one element.
        /// </summary>
        /// <typeparam name="T">The element type of the sequence (reference type).</typeparam>
        /// <param name="source">
        /// The (already-projected) sequence of candidate matches — typically the tail of a LINQ chain
        /// such as <c>subject.OwnedRelationship.OfType&lt;IFeatureTyping&gt;().Select(t => t.Type).OfType&lt;TFinal&gt;()</c>.
        /// </param>
        /// <param name="subjectName">
        /// The name of the subject parameter in the calling method (typically <c>nameof(...)</c>),
        /// embedded in the diagnostic message produced on a multiplicity violation.
        /// </param>
        /// <returns>The single element of the sequence.</returns>
        /// <exception cref="IncompleteModelException">
        /// Thrown when <paramref name="source"/> is empty (lower-bound violation — the model is missing
        /// the required element).
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when <paramref name="source"/> contains more than one element (upper-bound violation
        /// against the derived <c>[1..1]</c> property).
        /// </exception>
        /// <remarks>
        /// <para>
        /// Canonical helper for derived <c>[1..1]</c> properties whose projection ends with a final
        /// type-filter or predicate over a structurally <c>[0..*]</c> storage collection. The helper
        /// iterates the sequence once with early-exit on the second match; no full materialisation.
        /// </para>
        /// </remarks>
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
        /// Filters <paramref name="source"/> to elements of type <typeparamref name="TResult"/> and
        /// returns the single match, throwing <see cref="IncompleteModelException"/> if none is
        /// present and <see cref="MultiplicityViolationException"/> if more than one is present.
        /// </summary>
        /// <typeparam name="TResult">The narrowed type to filter to (reference type).</typeparam>
        /// <param name="source">
        /// The source sequence — typically a <c>[0..*]</c> storage collection such as
        /// <see cref="IRelationship.OwnedRelatedElement"/>, <see cref="IElement.OwnedRelationship"/>,
        /// or a derived enumerable such as <c>Feature::type</c>.
        /// </param>
        /// <param name="subjectName">
        /// The name of the subject parameter in the calling method (typically <c>nameof(...)</c>),
        /// embedded in the diagnostic message produced on a multiplicity violation.
        /// </param>
        /// <returns>The single element of type <typeparamref name="TResult"/>.</returns>
        /// <exception cref="IncompleteModelException">
        /// Thrown when no element of type <typeparamref name="TResult"/> is present (lower-bound
        /// violation — the model is missing the required element).
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one element of type <typeparamref name="TResult"/> is present
        /// (upper-bound violation against the derived <c>[1..1]</c> property).
        /// </exception>
        /// <remarks>
        /// Convenience overload that bundles a <c>OfType&lt;TResult&gt;()</c> filter before delegating
        /// to <see cref="SingleStrict{T}(IEnumerable{T}, string)"/>. Use this form when the source is
        /// a heterogeneous storage collection and the caller wants the single element of a particular
        /// narrowed type (e.g. <c>FeatureMembership::ownedMemberFeature</c> = the single <c>IFeature</c>
        /// in <c>OwnedRelatedElement</c>).
        /// </remarks>
        internal static TResult SingleStrict<TResult>(this System.Collections.IEnumerable source, string subjectName)
            where TResult : class
            => source.OfType<TResult>().SingleStrict(subjectName);

        /// <summary>
        /// Returns the at-most-one element from <paramref name="source"/>, throwing
        /// <see cref="MultiplicityViolationException"/> if the source contains more than one element.
        /// </summary>
        /// <typeparam name="T">The element type of the sequence (reference type).</typeparam>
        /// <param name="source">
        /// The (already-projected) sequence of candidate matches — typically the tail of a LINQ chain
        /// such as <c>subject.OwnedRelationship.OfType&lt;IFeatureTyping&gt;().Select(t => t.Type).OfType&lt;TFinal&gt;()</c>.
        /// </param>
        /// <param name="subjectName">
        /// The name of the subject parameter in the calling method (typically <c>nameof(...)</c>),
        /// embedded in the diagnostic message produced on a multiplicity violation.
        /// </param>
        /// <returns>
        /// <c>null</c> when <paramref name="source"/> is empty; the single element when it contains
        /// exactly one.
        /// </returns>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when <paramref name="source"/> contains more than one element (upper-bound violation
        /// against the derived <c>[0..1]</c> property).
        /// </exception>
        /// <remarks>
        /// <para>
        /// Canonical helper for derived <c>[0..1]</c> properties whose projection ends with a final
        /// type-filter or predicate. The helper iterates the sequence once with early-exit on the
        /// second match; no full materialisation.
        /// </para>
        /// <para>
        /// Sibling of <see cref="SingleStrict{T}(IEnumerable{T}, string)"/> — same shape, but with
        /// <c>[0..1]</c> semantics: the empty case returns <c>null</c> rather than throwing, because
        /// the lower bound is 0.
        /// </para>
        /// <para>
        /// Do NOT use this helper when the OCL derivation body explicitly elects the first of many
        /// (<c>->first()</c>, <c>->at(1)</c>); the spec contract is "pick the first if multiple",
        /// not "0 or 1". For those sites, keep <c>FirstOrDefault</c>.
        /// </para>
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
        /// Filters <paramref name="source"/> to elements of type <typeparamref name="TResult"/> and
        /// returns the at-most-one match, throwing <see cref="MultiplicityViolationException"/> if
        /// more than one is present.
        /// </summary>
        /// <typeparam name="TResult">The narrowed type to filter to (reference type).</typeparam>
        /// <param name="source">
        /// The source sequence — typically a <c>[0..*]</c> storage collection such as
        /// <see cref="IRelationship.OwnedRelatedElement"/>, <see cref="IElement.OwnedRelationship"/>,
        /// or a derived enumerable such as <c>Feature::type</c>.
        /// </param>
        /// <param name="subjectName">
        /// The name of the subject parameter in the calling method (typically <c>nameof(...)</c>),
        /// embedded in the diagnostic message produced on a multiplicity violation.
        /// </param>
        /// <returns>
        /// <c>null</c> when no element of type <typeparamref name="TResult"/> is present; the single
        /// match when exactly one is present.
        /// </returns>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one element of type <typeparamref name="TResult"/> is present
        /// (upper-bound violation against the derived <c>[0..1]</c> property).
        /// </exception>
        /// <remarks>
        /// <para>
        /// Convenience overload that bundles a <c>OfType&lt;TResult&gt;()</c> filter before delegating
        /// to <see cref="SingleOrDefaultStrict{T}(IEnumerable{T}, string)"/>. Use this form when the
        /// source is a heterogeneous storage collection and the caller wants the optional single
        /// element of a particular narrowed type (e.g.
        /// <c>ConstraintUsage::constraintDefinition</c> = the single <c>IPredicate</c> in
        /// <c>type</c>).
        /// </para>
        /// <para>
        /// Do NOT use this helper when the OCL derivation body explicitly elects the first of many
        /// (<c>->first()</c>, <c>->at(1)</c>); the spec contract is "pick the first if multiple",
        /// not "0 or 1". For those sites, keep <c>FirstOrDefault</c>.
        /// </para>
        /// </remarks>
        internal static TResult SingleOrDefaultStrict<TResult>(this System.Collections.IEnumerable source, string subjectName)
            where TResult : class
            => source.OfType<TResult>().SingleOrDefaultStrict(subjectName);

        /// <summary>
        /// Determines whether <paramref name="element"/> is currently — directly or transitively — contained by
        /// <paramref name="candidateBridge"/> via the chain of <see cref="IContainedElement.OwningRelationship"/> and
        /// <see cref="IContainedRelationship.OwningRelatedElement"/> pointers.
        /// </summary>
        /// <param name="element">The <see cref="IElement"/> whose ancestor chain is walked</param>
        /// <param name="candidateBridge">The <see cref="IRelationship"/> looked for in the ancestor chain</param>
        /// <returns><c>true</c> if <paramref name="candidateBridge"/> appears as an <see cref="IContainedElement.OwningRelationship"/>
        /// at any level above <paramref name="element"/>; otherwise <c>false</c></returns>
        /// <remarks>
        /// Used by the public <c>AssignOwnership</c> overloads to reject containment cycles before any state mutation.
        /// </remarks>
        private static bool IsContainedTransitivelyBy(IElement element, IRelationship candidateBridge)
        {
            var current = element;
            
            while (current != null)
            {
                var owningRelationship = current.OwningRelationship;
            
                if (owningRelationship == null)
                {
                    return false;
                }

                if (owningRelationship == candidateBridge)
                {
                    return true;
                }

                current = owningRelationship.OwningRelatedElement;
            }

            return false;
        }

        /// <summary>
        /// Determines whether <paramref name="element"/> is currently — directly or transitively — contained by
        /// <paramref name="candidateAncestor"/> via the chain of <see cref="IContainedElement.OwningRelationship"/> and
        /// <see cref="IContainedRelationship.OwningRelatedElement"/> pointers.
        /// </summary>
        /// <param name="element">The <see cref="IElement"/> whose ancestor chain is walked</param>
        /// <param name="candidateAncestor">The <see cref="IElement"/> looked for in the ancestor chain</param>
        /// <returns><c>true</c> if <paramref name="candidateAncestor"/> appears as an ancestor of <paramref name="element"/>;
        /// otherwise <c>false</c></returns>
        /// <remarks>
        /// Used by the public <c>AssignOwnership</c> overloads to reject containment cycles before any state mutation.
        /// </remarks>
        private static bool IsContainedTransitivelyBy(IElement element, IElement candidateAncestor)
        {
            var current = element.OwningRelationship?.OwningRelatedElement;
           
            while (current != null)
            {
                if (current == candidateAncestor)
                {
                    return true;
                }

                current = current.OwningRelationship?.OwningRelatedElement;
            }

            return false;
        }
    }
}
