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
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.PSM.DTO;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;
    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;

    /// <summary>
    /// Builds the part of the model that one scenario step commits: POCOs with deterministic ids, wired by the
    /// caller with <c>AssignOwnership</c>, then converted to DTOs, including derived values, once the step is complete
    /// </summary>
    /// <remarks>
    /// The DTOs are produced from POCOs because a DTO cannot hold the values of members that a derived property
    /// redefines, such as <c>OwningMembership.memberElement</c>, which the payload must still carry.
    /// </remarks>
    public sealed class ModelBuilder
    {
        /// <summary>
        /// The prefix of the names that the ids are derived from
        /// </summary>
        private readonly string scope;

        /// <summary>
        /// The created elements with their conversion, in creation order
        /// </summary>
        private readonly List<(string Name, PocoElement Poco, Func<DtoElement> Convert)> entries = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBuilder"/> class
        /// </summary>
        /// <param name="scope">
        /// The prefix of the names that the ids are derived from, e.g. the step name
        /// </param>
        public ModelBuilder(string scope)
        {
            this.scope = scope;
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
            if (this.entries.Exists(entry => entry.Name == name))
            {
                throw new ArgumentException($"The name {name} is already used in scope {this.scope}", nameof(name));
            }

            var poco = new TPoco
            {
                Id = DeterministicGuid.Create($"{this.scope}/{name}")
            };

            poco.ElementId = poco.Id.ToString();

            this.entries.Add((name, poco, () => toDto(poco, true)));

            return poco;
        }

        /// <summary>
        /// Converts every created POCO to its DTO, including derived values
        /// </summary>
        /// <returns>
        /// The DTOs, in creation order
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when an element cannot be converted, naming that element
        /// </exception>
        public IReadOnlyList<DtoElement> ToDtos()
        {
            return this.entries.Select(entry =>
            {
                try
                {
                    return entry.Convert();
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException($"{this.scope}/{entry.Name} ({entry.Poco.GetType().Name}) cannot be converted to a DTO: {exception.Message}", exception);
                }
            }).ToList();
        }
    }
}
