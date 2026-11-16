// ------------------------------------------------------------------------------------------------
// <copyright file="DtoMetadata.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using SysML2.NET.Decorators;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;

    /// <summary>
    /// Reads the metamodel facts that the generated DTOs carry in their <see cref="PropertyAttribute"/> and
    /// <see cref="ClassAttribute"/> decorators, so that coverage is computed from the generated code itself
    /// </summary>
    /// <remarks>
    /// Reflection is used here, in test code only, so that the coverage lists follow the metamodel without upkeep.
    /// </remarks>
    public static class DtoMetadata
    {
        /// <summary>
        /// The namespace prefix of the generated Core DTOs
        /// </summary>
        private const string DtoNamespace = "SysML2.NET.Core.DTO";

        /// <summary>
        /// The non-derived properties per DTO class, cached
        /// </summary>
        private static readonly ConcurrentDictionary<Type, IReadOnlyList<MetamodelProperty>> PropertiesByClass = new();

        /// <summary>
        /// Gets every concrete DTO class of a metaclass
        /// </summary>
        public static IReadOnlyList<Type> ConcreteClasses { get; } = typeof(DtoElement).Assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && IsDtoType(type) && typeof(DtoElement).IsAssignableFrom(type))
            .OrderBy(type => type.Name)
            .ToList();

        /// <summary>
        /// Gets every non-derived property of the metamodel, each once, on the DTO interface that declares it
        /// </summary>
        public static IReadOnlyList<MetamodelProperty> DeclaredProperties { get; } = typeof(DtoElement).Assembly.GetTypes()
            .Where(type => type.IsInterface && IsDtoType(type) && (type == typeof(DtoElement) || typeof(DtoElement).IsAssignableFrom(type)))
            .SelectMany(DeclaredNonDerivedProperties)
            .OrderBy(property => property.Name)
            .ToList();

        /// <summary>
        /// Gets the JSON name of every derived property of the metamodel, e.g. <c>owner</c> or <c>qualifiedName</c>
        /// </summary>
        public static IReadOnlySet<string> DerivedPropertyNames { get; } = typeof(DtoElement).Assembly.GetTypes()
            .Where(type => type.IsInterface && IsDtoType(type) && (type == typeof(DtoElement) || typeof(DtoElement).IsAssignableFrom(type)))
            .SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            .Where(property => property.GetCustomAttribute<PropertyAttribute>() is { IsDerived: true })
            .Select(property => $"{char.ToLowerInvariant(property.Name[0])}{property.Name[1..]}")
            .ToHashSet(StringComparer.Ordinal);

        /// <summary>
        /// Gets the non-derived properties that a DTO class implements, read through their declaring interfaces
        /// </summary>
        /// <param name="dtoType">
        /// The DTO class
        /// </param>
        /// <returns>
        /// The <see cref="MetamodelProperty"/>s of <paramref name="dtoType"/>
        /// </returns>
        public static IReadOnlyList<MetamodelProperty> PropertiesOf(Type dtoType)
        {
            return PropertiesByClass.GetOrAdd(dtoType, type => DeclaredProperties.Where(property => property.Interface.IsAssignableFrom(type)).ToList());
        }

        /// <summary>
        /// Asserts whether two property values are equal, comparing collections element by element
        /// </summary>
        /// <param name="first">
        /// The first value
        /// </param>
        /// <param name="second">
        /// The second value
        /// </param>
        /// <returns>
        /// <c>true</c> when the values are equal
        /// </returns>
        public static bool ValueEquals(object first, object second)
        {
            if (first is IEnumerable firstSequence and not string && second is IEnumerable secondSequence and not string)
            {
                return firstSequence.Cast<object>().SequenceEqual(secondSequence.Cast<object>());
            }

            return Equals(first, second);
        }

        /// <summary>
        /// Gets the non-derived properties that a DTO interface declares itself
        /// </summary>
        /// <param name="dtoInterface">
        /// The DTO interface
        /// </param>
        /// <returns>
        /// The declared <see cref="MetamodelProperty"/>s
        /// </returns>
        private static IEnumerable<MetamodelProperty> DeclaredNonDerivedProperties(Type dtoInterface)
        {
            return dtoInterface.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(property => property.GetCustomAttribute<PropertyAttribute>() is { IsDerived: false })
                .Select(property => new MetamodelProperty(dtoInterface, property));
        }

        /// <summary>
        /// Asserts whether a type is one of the generated Core DTO types
        /// </summary>
        /// <param name="type">
        /// The type
        /// </param>
        /// <returns>
        /// <c>true</c> for a type in a <c>SysML2.NET.Core.DTO</c> namespace
        /// </returns>
        private static bool IsDtoType(Type type) => type.Namespace?.StartsWith(DtoNamespace, StringComparison.Ordinal) == true;
    }
}
