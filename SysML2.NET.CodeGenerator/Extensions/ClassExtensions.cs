// ------------------------------------------------------------------------------------------------
// <copyright file="ClassExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Extension methods for <see cref="IClass" /> interface
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        /// Counts and returns to amount of non derived properties
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// the amount of non derived properties
        /// </returns>
        public static int CountAllNonDerivedProperties(this IClass @class)
        {
            return @class.QueryAllNonDerivedProperties().Count;
        }

        /// <summary>
        /// Counts and returns to amount of non derived properties
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// the amount of non derived properties
        /// </returns>
        public static int CountAllNonDerivedNonRedefinedProperties(this IClass @class)
        {
            return @class.QueryAllNonDerivedNonRedefinedProperties().Count;
        }

        /// <summary>
        /// Queries the properties of the class that play one of the given roles when an instance is serialized
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <param name="kinds">
        /// The <see cref="SerializedPropertyKind" />s to select
        /// </param>
        /// <returns>
        /// The selected properties, ordered by name
        /// </returns>
        public static IReadOnlyList<IProperty> QuerySerializedProperties(this IClass @class, params SerializedPropertyKind[] kinds)
        {
            return @class.QueryAllProperties()
                .Where(property => property.QuerySerializedPropertyKind(@class) is { } kind && kinds.Contains(kind))
                .OrderBy(property => property.Name)
                .ToList();
        }

        /// <summary>
        /// Queries the properties of the class that are exchanged only when derived properties are included
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// The <see cref="SerializedPropertyKind.Derived" /> and <see cref="SerializedPropertyKind.DerivedAlias" /> properties
        /// </returns>
        public static IReadOnlyList<IProperty> QueryDerivedSerializedProperties(this IClass @class)
        {
            return @class.QuerySerializedProperties(SerializedPropertyKind.Derived, SerializedPropertyKind.DerivedAlias);
        }

        /// <summary>
        /// Query the name of the internal interface to implement for an <see cref="IClass" />
        /// </summary>
        /// <param name="umlClass">The <see cref="IClass" /></param>
        /// <returns>The name of the internal interface to implement, if any</returns>
        public static string QueryInternalInterfaceName(this IClass umlClass)
        {
            var classifiers = umlClass.QueryAllGeneralClassifiers();

            if (classifiers.Any(x => x.Name == "Relationship"))
            {
                return "IContainedRelationship";
            }

            return classifiers.Any(x => x.Name == "Element") ? "IContainedElement" : string.Empty;
        }

        /// <summary>
        /// Asserts that an <see cref="IClass" /> is a subclass of another
        /// </summary>
        /// <param name="umlClass">The <see cref="IClass" /> to check</param>
        /// <param name="potentialSuperClass">
        /// The <see cref="IClass" /> that is potentially a super class of
        /// <paramref name="umlClass" />
        /// </param>
        /// <returns>
        /// True if the <paramref name="potentialSuperClass" /> is a super class of the <paramref name="umlClass" />
        /// </returns>
        public static bool QueryIsSubclassOf(this IClass umlClass, IClass potentialSuperClass)
        {
            return umlClass.QueryAllGeneralClassifiers().Contains(potentialSuperClass);
        }
    }
}
