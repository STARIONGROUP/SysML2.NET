// ------------------------------------------------------------------------------------------------
// <copyright file="Assembler.cs" company="Starion Group S.A.">
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
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Extensions;

    /// <summary>
    /// The purpose of the <see cref="IAssembler"/> is to assemble a SysML2.NET POCO object graph from a
    /// list of data-transfer-objects (DTO)
    /// </summary>
    public class Assembler : IAssembler
    {
        /// <summary>
        /// The <see cref="ILogger"/> used to log
        /// </summary>
        private readonly ILogger<Assembler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Assembler"/> class.
        /// </summary>
        public Assembler(ILoggerFactory loggerFactory = null)
        {
            this.logger = loggerFactory == null ? NullLogger<Assembler>.Instance : loggerFactory.CreateLogger<Assembler>();

            this.Cache = new ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>>();
        }

        /// <summary>
        /// Gets the Cache that contains all the <see cref="Core.POCO.Root.Elements.IElement"/>s
        /// </summary>
        public ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>> Cache { get; private set; }

        /// <summary>
        /// Synchronize the Cache based on the provided <paramref name="dtos"/>, deleting nothing
        /// </summary>
        /// <param name="dtos">
        /// the DTOs used to update the cache with
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dtos"/> is null
        /// </exception>
        public void Synchronize(IEnumerable<Core.DTO.Root.Elements.IElement> dtos)
        {
            this.Synchronize(dtos, []);
        }

        /// <summary>
        /// Synchronize the Cache based on the provided <paramref name="dtos"/> and remove the <paramref name="deletedIdentifiers"/> from it
        /// </summary>
        /// <param name="dtos">
        /// the DTOs used to update the cache with
        /// </param>
        /// <param name="deletedIdentifiers">
        /// the identifiers of the deleted elements, such as those of the commit change entries that carry no payload
        /// </param>
        /// <remarks>
        /// When <paramref name="dtos"/> carries more than one DTO with the same identifier, the first occurrence
        /// is the one that is added to the Cache and every subsequent duplicate is ignored. Only the
        /// <paramref name="deletedIdentifiers"/> leave the Cache: an element that a DTO no longer lists as owned is detached, not deleted.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dtos"/> or <paramref name="deletedIdentifiers"/> is null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when an identifier is both carried by a DTO and listed as deleted
        /// </exception>
        public void Synchronize(IEnumerable<Core.DTO.Root.Elements.IElement> dtos, IEnumerable<Guid> deletedIdentifiers)
        {
            if (dtos == null)
            {
                throw new ArgumentNullException(nameof(dtos), $"The {nameof(dtos)} may not be null");
            }

            if (deletedIdentifiers == null)
            {
                throw new ArgumentNullException(nameof(deletedIdentifiers), $"The {nameof(deletedIdentifiers)} may not be null");
            }

            // the DTOs are walked several times, materialize once so that a lazy sequence is not re-evaluated on every pass
            var elements = dtos as IReadOnlyList<Core.DTO.Root.Elements.IElement> ?? dtos.ToList();
            var deleted = new HashSet<Guid>(deletedIdentifiers);
            var contradiction = deleted.Count == 0 ? null : elements.FirstOrDefault(dto => deleted.Contains(dto.Id));

            if (contradiction != null)
            {
                throw new ArgumentException($"The element with identifier {contradiction.Id} is both provided and deleted", nameof(deletedIdentifiers));
            }

            var sw = Stopwatch.StartNew();

            var deletedCount = this.RemoveDeletedElements(deleted);

            if (this.logger.IsEnabled(LogLevel.Debug))
            {
                this.logger.LogDebug("A total of {DeletedCount} POCOs have been removed from the Cache in {Elapsed} [ms]", deletedCount, sw.ElapsedMilliseconds);
            }

            sw.Restart();
            this.logger.LogDebug("Update value properties of existing POCOs");

            foreach (var dto in elements)
            {
                if (this.Cache.TryGetValue(dto.Id, out var lazyPoco))
                {
                    lazyPoco.Value.UpdateValueProperties(dto);
                }
            }

            if (this.logger.IsEnabled(LogLevel.Debug))
            {
                this.logger.LogDebug("Value properties updated in {Elapsed} [ms]", sw.ElapsedMilliseconds);
            }

            sw.Restart();
            this.logger.LogDebug("Add new POCOs to dictionary based on DTOs");

            var elementFactory = new ElementFactory();
            var addedCount = 0;

            foreach (var dto in elements.Where(element => !this.Cache.ContainsKey(element.Id)))
            {
                var poco = elementFactory.Create(dto);

                this.Cache.AddOrUpdate(poco.Id, new Lazy<Core.POCO.Root.Elements.IElement>(() => poco), (key, oldValue) => oldValue);

                addedCount++;

                if (this.logger.IsEnabled(LogLevel.Trace))
                {
                    this.logger.LogTrace("{PocoType}:{Identifier} added to Cache", poco.GetType().Name, poco.Id);
                }
            }

            if (this.logger.IsEnabled(LogLevel.Debug))
            {
                this.logger.LogDebug("A total of {AddedCount} POCOs have been added to the Cache in {Elapsed} [ms]", addedCount, sw.ElapsedMilliseconds);
            }

            sw.Restart();
            this.logger.LogDebug("Update POCO reference properties");

            foreach (var dto in elements)
            {
                if (this.Cache.TryGetValue(dto.Id, out var lazyPoco))
                {
                    lazyPoco.Value.UpdateReferenceProperties(dto, this.Cache);
                }
            }

            if (this.logger.IsEnabled(LogLevel.Debug))
            {
                this.logger.LogDebug("POCO reference properties updated in {Elapsed} [ms]", sw.ElapsedMilliseconds);
            }

            sw.Stop();
        }

        /// <summary>
        /// Detaches the <paramref name="element"/> from whichever owner currently holds it.
        /// </summary>
        /// <param name="element">The POCO to detach</param>
        private static void DetachFromOwner(Core.POCO.Root.Elements.IElement element)
        {
            element.OwningRelationship?.RemoveOwnedRelatedElement(element);

            if (element is Core.POCO.Root.Elements.IRelationship { OwningRelatedElement: not null } relationship)
            {
                relationship.OwningRelatedElement.RemoveOwnedRelationship(relationship);
            }
        }

        /// <summary>
        /// Removes the elements identified by <paramref name="deletedIdentifiers"/> from the Cache and from their owners.
        /// </summary>
        /// <param name="deletedIdentifiers">The identifiers of the deleted elements</param>
        /// <returns>The number of POCOs removed from the Cache</returns>
        private int RemoveDeletedElements(HashSet<Guid> deletedIdentifiers)
        {
            var deletedCount = 0;

            foreach (var identifier in deletedIdentifiers)
            {
                if (!this.Cache.TryRemove(identifier, out var deletedLazyPoco))
                {
                    if (this.logger.IsEnabled(LogLevel.Warning))
                    {
                        this.logger.LogWarning("The element with identifier {Identifier} was not deleted as it could not be found in the cache", identifier);
                    }

                    continue;
                }

                DetachFromOwner(deletedLazyPoco.Value);
                deletedCount++;

                if (this.logger.IsEnabled(LogLevel.Trace))
                {
                    this.logger.LogTrace("{PocoType} with identifier {Identifier} was deleted", deletedLazyPoco.Value.GetType().Name, identifier);
                }
            }

            return deletedCount;
        }
    }
}
