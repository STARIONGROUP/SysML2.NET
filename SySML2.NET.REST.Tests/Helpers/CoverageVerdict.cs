// ------------------------------------------------------------------------------------------------
// <copyright file="CoverageVerdict.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Common;
    using SysML2.NET.Core;

    /// <summary>
    /// Computes what the scenario has not yet covered, from what the server returned and the requests the client sent,
    /// against lists taken from the generated DTOs and the OpenAPI document
    /// </summary>
    public sealed class CoverageVerdict
    {
        /// <summary>
        /// A fresh instance per DTO class, holding the default value of every property
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> Defaults = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverageVerdict"/> class
        /// </summary>
        /// <param name="readBack">
        /// Every data item the server returned, with the commit it was read at
        /// </param>
        /// <param name="calls">
        /// Every request the client sent
        /// </param>
        /// <param name="openApi">
        /// The operations of the OpenAPI document
        /// </param>
        public CoverageVerdict(IReadOnlyCollection<(Guid CommitId, IData Data)> readBack, IReadOnlyCollection<EndpointCall> calls, OpenApiDocument openApi)
        {
            var elements = readBack.Where(entry => entry.Data != null).ToList();

            var seenClasses = elements.Select(entry => entry.Data.GetType()).ToHashSet();
            this.MissingMetaclasses = DtoMetadata.ConcreteClasses.Where(type => !seenClasses.Contains(type)).Select(type => type.Name).ToList();

            this.UnsetProperties = DtoMetadata.DeclaredProperties
                .Where(property => !elements.Exists(entry => property.Interface.IsInstanceOfType(entry.Data) && !DtoMetadata.ValueEquals(property.ValueOf(entry.Data), property.ValueOf(DefaultOf(entry.Data.GetType())))))
                .Select(property => property.Name)
                .ToList();

            var versions = elements.GroupBy(entry => entry.Data.Id).ToList();

            this.UnchangedProperties = DtoMetadata.DeclaredProperties
                .Where(property => !versions.Exists(element => property.Interface.IsInstanceOfType(element.First().Data) && element.Select(entry => property.ValueOf(entry.Data)).Skip(1).Any(value => !DtoMetadata.ValueEquals(value, property.ValueOf(element.First().Data)))))
                .Select(property => property.Name)
                .ToList();

            this.MissingLiterals = MissingEnumerationLiterals(elements.Select(entry => entry.Data)).ToList();

            this.UncoveredOperations = openApi.Operations
                .SelectMany(operation =>
                {
                    var operationCalls = calls.Where(call => call.Operation == operation).ToList();
                    var missing = new List<string>();

                    if (!operationCalls.Exists(call => call.IsSuccess))
                    {
                        missing.Add($"{operation} (no 2xx)");
                    }

                    if (operation.DeclaresClientError && !operationCalls.Exists(call => call.IsClientError))
                    {
                        missing.Add($"{operation} (no 4xx)");
                    }

                    return missing;
                })
                .ToList();

            this.UnmatchedRequests = calls.Where(call => call.Operation == null).Select(call => call.RequestLine).Distinct().ToList();
        }

        /// <summary>
        /// Gets the concrete metaclasses of which the server returned no instance
        /// </summary>
        public IReadOnlyList<string> MissingMetaclasses { get; }

        /// <summary>
        /// Gets the non-derived properties that the server never returned with a non-default value
        /// </summary>
        public IReadOnlyList<string> UnsetProperties { get; }

        /// <summary>
        /// Gets the non-derived properties that the server never returned with two different values for one element
        /// </summary>
        public IReadOnlyList<string> UnchangedProperties { get; }

        /// <summary>
        /// Gets the enumeration literals that the server never returned
        /// </summary>
        /// <remarks>
        /// The default literal of a non-nullable property counts as returned, as it cannot be told apart from a value
        /// the server omitted.
        /// </remarks>
        public IReadOnlyList<string> MissingLiterals { get; }

        /// <summary>
        /// Gets the OpenAPI operations not yet called with a success answer, or with a client error answer where the
        /// document declares one
        /// </summary>
        public IReadOnlyList<string> UncoveredOperations { get; }

        /// <summary>
        /// Gets the requests that addressed no operation of the OpenAPI document
        /// </summary>
        public IReadOnlyList<string> UnmatchedRequests { get; }

        /// <summary>
        /// Gets a fresh instance of a DTO class, whose property values are the defaults
        /// </summary>
        /// <param name="dtoType">
        /// The DTO class
        /// </param>
        /// <returns>
        /// The fresh instance
        /// </returns>
        private static object DefaultOf(Type dtoType) => Defaults.GetOrAdd(dtoType, type => Activator.CreateInstance(type));

        /// <summary>
        /// Computes the literals of the metamodel enumerations that no returned element holds; the enumerations are
        /// those typing a non-derived property, which excludes <see cref="ClassKind"/>
        /// </summary>
        /// <param name="elements">
        /// The returned elements
        /// </param>
        /// <returns>
        /// The missing literals as <c>Enumeration.literal</c>
        /// </returns>
        private static IEnumerable<string> MissingEnumerationLiterals(IEnumerable<IData> elements)
        {
            var enumerationProperties = DtoMetadata.DeclaredProperties
                .Where(property => (Nullable.GetUnderlyingType(property.Property.PropertyType) ?? property.Property.PropertyType).IsEnum)
                .ToList();

            var enumerations = enumerationProperties
                .Select(property => Nullable.GetUnderlyingType(property.Property.PropertyType) ?? property.Property.PropertyType)
                .Distinct();

            var observed = elements
                .SelectMany(element => enumerationProperties.Where(property => property.Interface.IsInstanceOfType(element)).Select(property => property.ValueOf(element)))
                .Where(value => value != null)
                .ToHashSet();

            return enumerations
                .SelectMany(enumeration => Enum.GetValues(enumeration).Cast<object>())
                .Where(literal => !observed.Contains(literal))
                .Select(literal => $"{literal.GetType().Name}.{literal}");
        }
    }
}
