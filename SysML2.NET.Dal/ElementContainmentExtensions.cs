// ------------------------------------------------------------------------------------------------
// <copyright file="ElementContainmentExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Extensions;

    using ContainmentExtensions = SysML2.NET.Extensions.ElementExtensions;

    /// <summary>
    /// Extension methods that bring the containment of a POCO in line with its DTO.
    /// </summary>
    public static class ElementContainmentExtensions
    {
        /// <param name="poco">The POCO whose containment is updated</param>
        extension(IElement poco)
        {
            /// <summary>
            /// Updates the owned relationships, owned related elements and owner of the <paramref name="poco"/> from the <paramref name="dto"/>,
            /// resolving identifiers through the <paramref name="cache"/>; elements no longer owned are detached, never deleted.
            /// </summary>
            /// <param name="dto">The DTO the <paramref name="poco"/> is updated from</param>
            /// <param name="cache">The cache that resolves identifiers to POCOs; identifiers it does not contain are ignored</param>
            /// <remarks>
            /// An owner named by the DTO takes the <paramref name="poco"/> over from its current owner. A DTO without an owner only
            /// detaches the <paramref name="poco"/> from an owner that has left the <paramref name="cache"/>: an owner still cached
            /// keeps the <paramref name="poco"/> until its own DTO drops it, so the result does not depend on the order the DTOs are applied in.
            /// The DTO is mirrored without the containment checks of <see cref="ElementOwnershipExtensions"/>, so a model the source holds is never refused.
            /// </remarks>
            /// <exception cref="ArgumentNullException">Thrown when <paramref name="poco"/>, <paramref name="dto"/> or <paramref name="cache"/> is null</exception>
            /// <exception cref="InvalidOperationException">Thrown when an identifier of the <paramref name="dto"/> resolves to a POCO of the wrong kind</exception>
            public void UpdateContainment(Core.DTO.Root.Elements.IElement dto, ConcurrentDictionary<Guid, Lazy<IElement>> cache)
            {
                if (poco == null)
                {
                    throw new ArgumentNullException(nameof(poco));
                }

                if (dto == null)
                {
                    throw new ArgumentNullException(nameof(dto));
                }

                if (cache == null)
                {
                    throw new ArgumentNullException(nameof(cache));
                }

                ContainmentExtensions.SynchronizeOwnedRelationships(poco, Resolve<IRelationship>(dto.OwnedRelationship, cache));

                if (poco is IRelationship relationship && dto is Core.DTO.Root.Elements.IRelationship relationshipDto)
                {
                    ContainmentExtensions.SynchronizeOwnedRelatedElements(relationship, Resolve<IElement>(relationshipDto.OwnedRelatedElement, cache));
                    UpdateOwningRelatedElement(relationship, relationshipDto.OwningRelatedElement, cache);
                }

                UpdateOwningRelationship(poco, dto.OwningRelationship, cache);
            }
        }

        /// <summary>
        /// Returns the cached POCO identified by <paramref name="identifier"/>, typed as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected POCO type</typeparam>
        /// <param name="identifier">The identifier to resolve</param>
        /// <param name="cache">The cache that resolves identifiers to POCOs</param>
        /// <returns>The POCO, or <c>null</c> when the <paramref name="cache"/> does not contain it</returns>
        /// <exception cref="InvalidOperationException">Thrown when the cached POCO is not a <typeparamref name="T"/></exception>
        private static T Resolve<T>(Guid identifier, ConcurrentDictionary<Guid, Lazy<IElement>> cache) where T : class, IElement
        {
            if (!cache.TryGetValue(identifier, out var lazyPoco))
            {
                return null;
            }

            return lazyPoco.Value as T
                   ?? throw new InvalidOperationException($"The element with identifier {identifier} is a {lazyPoco.Value.GetType().Name}, expected a {typeof(T).Name}");
        }

        /// <summary>
        /// Returns the cached POCOs of type <typeparamref name="T"/> identified by <paramref name="identifiers"/>, in order.
        /// </summary>
        /// <typeparam name="T">The expected POCO type</typeparam>
        /// <param name="identifiers">The identifiers to resolve</param>
        /// <param name="cache">The cache that resolves identifiers to POCOs</param>
        /// <returns>The resolved POCOs; identifiers missing from the <paramref name="cache"/> are skipped</returns>
        /// <exception cref="InvalidOperationException">Thrown when a cached POCO is not a <typeparamref name="T"/></exception>
        private static List<T> Resolve<T>(IEnumerable<Guid> identifiers, ConcurrentDictionary<Guid, Lazy<IElement>> cache) where T : class, IElement
        {
            return identifiers.Select(identifier => Resolve<T>(identifier, cache)).Where(resolved => resolved != null).ToList();
        }

        /// <summary>
        /// Applies the <c>owningRelatedElement</c> held by a relationship DTO to its POCO.
        /// </summary>
        /// <param name="relationship">The relationship POCO</param>
        /// <param name="owningRelatedElementId">The identifier of the owner held by the DTO, if any</param>
        /// <param name="cache">The cache that resolves identifiers to POCOs</param>
        private static void UpdateOwningRelatedElement(IRelationship relationship, Guid? owningRelatedElementId, ConcurrentDictionary<Guid, Lazy<IElement>> cache)
        {
            var currentOwner = relationship.OwningRelatedElement;

            if (owningRelatedElementId.HasValue)
            {
                var owner = Resolve<IElement>(owningRelatedElementId.Value, cache);

                if (owner != null && owner != currentOwner)
                {
                    ContainmentExtensions.AppendOwnedRelationship(owner, relationship);
                }

                return;
            }

            if (currentOwner != null && !cache.ContainsKey(currentOwner.Id))
            {
                currentOwner.RemoveOwnedRelationship(relationship);
            }
        }

        /// <summary>
        /// Applies the <c>owningRelationship</c> held by a DTO to its POCO.
        /// </summary>
        /// <param name="element">The POCO</param>
        /// <param name="owningRelationshipId">The identifier of the owning relationship held by the DTO, if any</param>
        /// <param name="cache">The cache that resolves identifiers to POCOs</param>
        private static void UpdateOwningRelationship(IElement element, Guid? owningRelationshipId, ConcurrentDictionary<Guid, Lazy<IElement>> cache)
        {
            var currentOwner = element.OwningRelationship;

            if (owningRelationshipId.HasValue)
            {
                var owner = Resolve<IRelationship>(owningRelationshipId.Value, cache);

                if (owner != null && owner != currentOwner)
                {
                    ContainmentExtensions.AppendOwnedRelatedElement(owner, element);
                }

                return;
            }

            if (currentOwner != null && !cache.ContainsKey(currentOwner.Id))
            {
                currentOwner.RemoveOwnedRelatedElement(element);
            }
        }
    }
}
