// ------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using SysML2.NET.Extensions.Core.DTO.Comparers;
    using SysML2.NET.PSM.DTO;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;
    using DtoRelationship = SysML2.NET.Core.DTO.Root.Elements.IRelationship;
    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;

    /// <summary>
    /// Builds the model of the scenario step by step: POCOs with deterministic ids, wired by the caller with
    /// <c>AssignOwnership</c>, then converted to DTOs, including derived values, once a step is complete
    /// </summary>
    /// <remarks>
    /// The DTOs are produced from POCOs because a DTO cannot hold the values of members that a derived property
    /// redefines, such as <c>OwningMembership.memberElement</c>, which the payload must still carry. One builder holds
    /// the whole model, so a later step can wire into the elements of an earlier one.
    /// </remarks>
    public sealed class ModelBuilder
    {
        /// <summary>
        /// The settable string and reference list properties per DTO class, which the Dal conversion shares with the
        /// POCO (#428) and a memberwise copy shares with the DTO it copies
        /// </summary>
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> ListsByClass = new();

        /// <summary>
        /// The memberwise copy of <see cref="object"/>, which copies a DTO of any class
        /// </summary>
        private static readonly MethodInfo CopyMethod = typeof(object).GetMethod(nameof(MemberwiseClone), BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        /// The prefix of the names that the ids of the current step are derived from
        /// </summary>
        private string scope;

        /// <summary>
        /// The created elements with their conversion, in creation order
        /// </summary>
        private readonly List<(string Name, PocoElement Poco, Func<bool, DtoElement> Convert)> entries = [];

        /// <summary>
        /// The names of the elements that the last <see cref="ToDtos"/> converted without their derived values
        /// </summary>
        private List<string> withoutDerivedValues = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBuilder"/> class
        /// </summary>
        /// <param name="scope">
        /// The prefix of the names that the ids of the first step are derived from, e.g. the step name
        /// </param>
        public ModelBuilder(string scope)
        {
            this.scope = scope;
        }

        /// <summary>
        /// Gets the names of the elements that the last <see cref="ToDtos"/> converted without their derived values,
        /// because computing them failed; derived values are not sent, so the payload is unaffected
        /// </summary>
        public IReadOnlyList<string> WithoutDerivedValues => this.withoutDerivedValues;

        /// <summary>
        /// Gets every created element, in creation order, with the scoped name its id is derived from
        /// </summary>
        public IReadOnlyList<(string Name, PocoElement Element)> Elements => [.. this.entries.Select(entry => (entry.Name, entry.Poco))];

        /// <summary>
        /// Selects the elements that a commit must send: those that are new, or whose DTO differs from the one of the
        /// previous commit under <c>ComparerProvider</c>
        /// </summary>
        /// <param name="previous">
        /// The model at the previous commit
        /// </param>
        /// <param name="current">
        /// The model as it must be after the commit
        /// </param>
        /// <returns>
        /// The new and changed elements of <paramref name="current"/>, in its order
        /// </returns>
        public static IReadOnlyList<DtoElement> Changed(IReadOnlyList<DtoElement> previous, IReadOnlyList<DtoElement> current)
        {
            var previousById = previous.ToDictionary(element => element.Id);

            return current
                .Where(element => !previousById.TryGetValue(element.Id, out var before) || before.GetType() != element.GetType() || !ComparerProvider.Resolve(element).Equals(before, element))
                .ToList();
        }

        /// <summary>
        /// Copies a DTO, with its own string and reference lists, so that a change to the copy leaves the expected model
        /// it came from intact
        /// </summary>
        /// <typeparam name="TElement">
        /// The type of the DTO
        /// </typeparam>
        /// <param name="dto">
        /// The DTO
        /// </param>
        /// <returns>
        /// The copy
        /// </returns>
        public static TElement Copy<TElement>(TElement dto) where TElement : DtoElement
        {
            ArgumentNullException.ThrowIfNull(dto);

            return (TElement)Detach((DtoElement)CopyMethod.Invoke(dto, null));
        }

        /// <summary>
        /// Finds the elements that an element owns, directly or indirectly, through its owned relationships and their
        /// owned related elements, both composite in KerML
        /// </summary>
        /// <param name="model">
        /// The model
        /// </param>
        /// <param name="ownerId">
        /// The owning element
        /// </param>
        /// <returns>
        /// The owned elements, without the owner
        /// </returns>
        public static IReadOnlyList<DtoElement> OwnedTree(IReadOnlyList<DtoElement> model, Guid ownerId)
        {
            ArgumentNullException.ThrowIfNull(model);

            var byId = model.ToDictionary(element => element.Id);
            var found = new List<DtoElement>();
            var visited = new HashSet<Guid> { ownerId };
            var pending = new Queue<Guid>([ownerId]);

            while (pending.Count > 0)
            {
                if (!byId.TryGetValue(pending.Dequeue(), out var element))
                {
                    continue;
                }

                IEnumerable<Guid> owned = element is DtoRelationship relationship ? [.. element.OwnedRelationship, .. relationship.OwnedRelatedElement] : element.OwnedRelationship;

                foreach (var id in owned.Where(visited.Add))
                {
                    pending.Enqueue(id);

                    if (byId.TryGetValue(id, out var child))
                    {
                        found.Add(child);
                    }
                }
            }

            return found;
        }

        /// <summary>
        /// Creates the commit request that creates or replaces the given elements
        /// </summary>
        /// <param name="elements">
        /// The DTOs to commit
        /// </param>
        /// <param name="description">
        /// The description of the commit
        /// </param>
        /// <returns>
        /// The <see cref="CommitRequest"/>, one change per element
        /// </returns>
        public static CommitRequest ToCommitRequest(IEnumerable<DtoElement> elements, string description)
        {
            return new CommitRequest
            {
                Description = description,
                Change = elements.Select(element => new DataVersionRequest
                {
                    Identity = new DataIdentityRequest { Id = element.Id },
                    Payload = element
                }).ToList()
            };
        }

        /// <summary>
        /// Starts the next step: the ids of the elements created from now on are derived from names in
        /// <paramref name="stepScope"/>
        /// </summary>
        /// <param name="stepScope">
        /// The prefix of the names of the step, e.g. <c>core</c>
        /// </param>
        public void BeginStep(string stepScope)
        {
            this.scope = stepScope;
        }

        /// <summary>
        /// Creates a POCO with a deterministic <c>Id</c> and <c>ElementId</c>, and records its conversion to a DTO
        /// </summary>
        /// <typeparam name="TPoco">
        /// The POCO type
        /// </typeparam>
        /// <param name="name">
        /// The name the id is derived from, unique within the scope
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TPoco"/>, e.g. <c>PackageExtensions.ToDto</c>
        /// </param>
        /// <returns>
        /// The created POCO
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="name"/> is already used in the scope
        /// </exception>
        public TPoco Create<TPoco>(string name, Func<TPoco, bool, DtoElement> toDto) where TPoco : PocoElement, new()
        {
            var scopedName = $"{this.scope}/{name}";

            if (this.entries.Exists(entry => entry.Name == scopedName))
            {
                throw new ArgumentException($"The name {name} is already used in scope {this.scope}", nameof(name));
            }

            var poco = new TPoco
            {
                Id = DeterministicGuid.Create(scopedName)
            };

            poco.ElementId = poco.Id.ToString();

            this.entries.Add((scopedName, poco, includeDerivedProperties => toDto(poco, includeDerivedProperties)));

            return poco;
        }

        /// <summary>
        /// Finds a created element by the scoped name its id is derived from, e.g. <c>core/f</c>
        /// </summary>
        /// <typeparam name="TPoco">
        /// The POCO type of the element
        /// </typeparam>
        /// <param name="scopedName">
        /// The scope and name of the element
        /// </param>
        /// <returns>
        /// The element
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when no element of <typeparamref name="TPoco"/> was created under <paramref name="scopedName"/>
        /// </exception>
        public TPoco Find<TPoco>(string scopedName) where TPoco : PocoElement
        {
            return this.entries.Find(entry => entry.Name == scopedName).Poco is TPoco element
                ? element
                : throw new ArgumentException($"No {typeof(TPoco).Name} was created as {scopedName}", nameof(scopedName));
        }

        /// <summary>
        /// Converts every created POCO to its DTO, including derived values; an element whose derived values cannot be
        /// computed, such as an operator expression without the model library, is converted without them and listed
        /// in <see cref="WithoutDerivedValues"/>
        /// </summary>
        /// <remarks>
        /// Every DTO gets its own copy of the string lists that the Dal conversion shares with the POCO (#428), so that
        /// a DTO stays the snapshot of the model at its commit when a later step changes the POCO.
        /// </remarks>
        /// <returns>
        /// The DTOs, in creation order
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when an element cannot be converted, naming that element
        /// </exception>
        public IReadOnlyList<DtoElement> ToDtos()
        {
            var withoutDerived = new List<string>();

            var dtos = this.entries.Select(entry =>
            {
                try
                {
                    return Detach(entry.Convert(true));
                }
                catch (Exception)
                {
                    withoutDerived.Add(entry.Name);
                }

                try
                {
                    return Detach(entry.Convert(false));
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException($"{entry.Name} ({entry.Poco.GetType().Name}) cannot be converted to a DTO: {exception.Message}", exception);
                }
            }).ToList();

            this.withoutDerivedValues = withoutDerived;

            return dtos;
        }

        /// <summary>
        /// Gives a DTO its own copy of every string and reference list it holds
        /// </summary>
        /// <param name="dto">
        /// The DTO
        /// </param>
        /// <returns>
        /// The same DTO
        /// </returns>
        private static DtoElement Detach(DtoElement dto)
        {
            var lists = ListsByClass.GetOrAdd(dto.GetType(), type => type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => (property.PropertyType == typeof(List<string>) || property.PropertyType == typeof(List<Guid>)) && property.CanWrite && property.SetMethod is { IsPublic: true } or { IsAssembly: true })
                .ToArray());

            foreach (var property in lists)
            {
                switch (property.GetValue(dto))
                {
                    case List<string> strings:
                        property.SetValue(dto, new List<string>(strings));
                        break;
                    case List<Guid> references:
                        property.SetValue(dto, new List<Guid>(references));
                        break;
                }
            }

            return dto;
        }
    }
}
