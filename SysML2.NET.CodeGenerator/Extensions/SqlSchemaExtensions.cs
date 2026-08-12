// -------------------------------------------------------------------------------------------------
// <copyright file="SqlSchemaExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Extension methods used by the SQL schema generator (SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema-2.hbs)
    /// to derive the PostgreSQL persistence schema from the UML metamodel. The schema stores only
    /// non-derived, non-redefining properties; everything derived is materialized separately at
    /// commit time (see SysML2.NET.CodeGenerator/Sql/schema.golden.sql for the reference design).
    /// </summary>
    public static class SqlSchemaExtensions
    {
        /// <summary>
        /// The derived properties that are promoted to real columns on the derived_version table
        /// (key: UML property name, value: SQL column name). All other derived properties live in
        /// the derived_json document.
        /// </summary>
        private static readonly Dictionary<string, string> PromotedDerivedColumns = new()
        {
            ["owner"] = "owner",
            ["owningNamespace"] = "owning_namespace",
            ["qualifiedName"] = "qualified_name",
            ["name"] = "name",
            ["shortName"] = "short_name",
            ["isLibraryElement"] = "is_library_element"
        };

        /// <summary>
        /// Converts a camelCase or PascalCase UML name to the snake_case form used for SQL identifiers
        /// </summary>
        /// <param name="name">
        /// The UML name to convert, e.g. "OccurrenceUsage" or "declaredShortName"
        /// </param>
        /// <returns>
        /// The snake_case form, e.g. "occurrence_usage" or "declared_short_name"
        /// </returns>
        public static string QuerySqlSnakeCaseName(this string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);

            var stringBuilder = new StringBuilder();

            foreach (var (character, characterIndex) in name.Select((character, characterIndex) => (character, characterIndex)))
            {
                if (char.IsUpper(character) && characterIndex > 0)
                {
                    stringBuilder.Append('_');
                }

                stringBuilder.Append(char.ToLowerInvariant(character));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Queries the non-derived properties DECLARED by the class itself — the flattened property set
        /// minus everything the direct generalizations already carry. This deliberately does NOT use
        /// OwnedAttribute: reference properties that are association ends (e.g. Membership::memberElement)
        /// are owned by the association, not the class, and would be silently dropped.
        ///
        /// Same-name redefinitions (e.g. CollectExpression::operator redefining OperatorExpression::operator)
        /// are excluded — they resolve to the storage of the property they redefine. A redefinition under a
        /// NEW name (e.g. Membership::memberElement redefining Relationship::target) is a distinct API
        /// property and introduces storage of its own, exactly as it does in the generated DTOs.
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// The stored properties declared by the class, ordered by name
        /// </returns>
        public static IReadOnlyList<IProperty> QueryStoredOwnProperties(this IClass @class)
        {
            ArgumentNullException.ThrowIfNull(@class);

            var inheritedPropertyIds = @class.Generalization
                .Select(generalization => generalization.General)
                .OfType<IClass>()
                .SelectMany(general => general.QueryAllProperties())
                .Select(property => property.XmiId)
                .ToHashSet();

            return @class.QueryAllProperties()
                .Where(property => !inheritedPropertyIds.Contains(property.XmiId))
                .Where(property => !property.IsDerived && !property.IsDerivedUnion && !property.QueryIsSameNameRedefinition())
                .OrderBy(property => property.Name, StringComparer.Ordinal)
                .ToList();
        }

        /// <summary>
        /// Asserts whether the property is a same-name redefinition — a redeclaration of an inherited
        /// property under the same name, carrying no storage of its own
        /// </summary>
        /// <param name="property">
        /// The subject <see cref="IProperty" />
        /// </param>
        /// <returns>
        /// True when the property redefines a property of the same name
        /// </returns>
        public static bool QueryIsSameNameRedefinition(this IProperty property)
        {
            ArgumentNullException.ThrowIfNull(property);

            return property.RedefinedProperty.Any(redefined => redefined.Name == property.Name);
        }

        /// <summary>
        /// Queries the single-valued stored properties declared by the class itself — the properties that
        /// become columns on the class's subtype table
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// The single-valued stored properties declared by the class, ordered by name
        /// </returns>
        public static IReadOnlyList<IProperty> QueryStoredScalarOwnProperties(this IClass @class)
        {
            return @class.QueryStoredOwnProperties()
                .Where(property => !property.QueryIsEnumerable())
                .ToList();
        }

        /// <summary>
        /// Queries the multi-valued stored properties declared by the class itself — the properties that
        /// become ordered link tables
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// The multi-valued stored properties declared by the class, ordered by name
        /// </returns>
        public static IReadOnlyList<IProperty> QueryStoredMultiOwnProperties(this IClass @class)
        {
            return @class.QueryStoredOwnProperties()
                .Where(property => property.QueryIsEnumerable())
                .ToList();
        }

        /// <summary>
        /// Asserts whether the class gets a subtype table of its own. Element is excluded because its
        /// stored scalars are folded into the element_version core table (every element has them, so a
        /// join would be pure overhead).
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// True when the class introduces at least one single-valued stored property and is not Element
        /// </returns>
        public static bool QueryIsStorageIntroducing(this IClass @class)
        {
            ArgumentNullException.ThrowIfNull(@class);

            return @class.Name != "Element" && @class.QueryStoredScalarOwnProperties().Count != 0;
        }

        /// <summary>
        /// Queries the storage-introducing classes in the class's generalization closure (the class itself
        /// included), ordered shallowest supertype first. The generalization graph is a DAG (multiple
        /// inheritance), so this is a closure over <see cref="ClassifierExtensions.QueryAllGeneralClassifiers" />,
        /// not a walk up a chain.
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// The storage-introducing classes whose subtype tables an instance of this class participates in
        /// </returns>
        public static IReadOnlyList<IClass> QueryStorageAncestors(this IClass @class)
        {
            ArgumentNullException.ThrowIfNull(@class);

            var closure = @class.QueryAllGeneralClassifiers()
                .OfType<IClass>()
                .Union([@class]);

            return closure
                .Where(ancestor => ancestor.QueryIsStorageIntroducing())
                .OrderBy(ancestor => ancestor.QueryAllGeneralClassifiers().Count)
                .ThenBy(ancestor => ancestor.Name, StringComparer.Ordinal)
                .ToList();
        }

        /// <summary>
        /// Queries the SQL name of the class's subtype table, e.g. "occurrence_usage_version" for OccurrenceUsage
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// The subtype table name
        /// </returns>
        public static string QuerySqlSubtypeTableName(this IClass @class)
        {
            ArgumentNullException.ThrowIfNull(@class);

            return $"{@class.Name.QuerySqlSnakeCaseName()}_version";
        }

        /// <summary>
        /// Queries the SQL name of the link table for a multi-valued stored property, e.g.
        /// "element_owned_relationship" for Element::ownedRelationship
        /// </summary>
        /// <param name="class">
        /// The <see cref="IClass" /> declaring the property
        /// </param>
        /// <param name="property">
        /// The multi-valued stored <see cref="IProperty" />
        /// </param>
        /// <returns>
        /// The link table name
        /// </returns>
        public static string QuerySqlLinkTableName(this IClass @class, IProperty property)
        {
            ArgumentNullException.ThrowIfNull(@class);
            ArgumentNullException.ThrowIfNull(property);

            return $"{@class.Name.QuerySqlSnakeCaseName()}_{property.Name.QuerySqlSnakeCaseName()}";
        }

        /// <summary>
        /// Queries the SQL column name for a property, e.g. "is_implied_included" for isImpliedIncluded
        /// </summary>
        /// <param name="property">
        /// The subject <see cref="IProperty" />
        /// </param>
        /// <returns>
        /// The snake_case column name
        /// </returns>
        public static string QuerySqlColumnName(this IProperty property)
        {
            ArgumentNullException.ThrowIfNull(property);

            return property.Name.QuerySqlSnakeCaseName();
        }

        /// <summary>
        /// Queries the PostgreSQL type of a single-valued stored property. References map to uuid
        /// (they target data_identity), enums to the generated enum type, primitives to their SQL
        /// counterpart.
        /// </summary>
        /// <param name="property">
        /// The subject <see cref="IProperty" />
        /// </param>
        /// <returns>
        /// The PostgreSQL type name
        /// </returns>
        public static string QuerySqlTypeName(this IProperty property)
        {
            ArgumentNullException.ThrowIfNull(property);

            if (property.QueryIsEnum())
            {
                return $"sysml2.{property.QueryTypeName().QuerySqlSnakeCaseName()}";
            }

            if (property.QueryIsReferenceType())
            {
                return "uuid";
            }

            return property.QueryTypeName() switch
            {
                "Boolean" => "boolean",
                "Integer" => "integer",
                "Real" => "double precision",
                "String" => "text",
                _ => throw new NotSupportedException($"No SQL type mapping for UML type {property.QueryTypeName()} of property {property.Name}")
            };
        }

        /// <summary>
        /// Resolves a property to the property whose storage it occupies: a same-name redefinition (e.g.
        /// CollectExpression::operator) resolves transitively to the root property it redefines
        /// (OperatorExpression::operator); any other property — including redefinitions under a new name —
        /// resolves to itself.
        /// </summary>
        /// <param name="property">
        /// The subject <see cref="IProperty" />
        /// </param>
        /// <returns>
        /// The property that owns the storage
        /// </returns>
        public static IProperty QueryStorageRootProperty(this IProperty property)
        {
            ArgumentNullException.ThrowIfNull(property);

            var current = property;

            while (current.QueryIsSameNameRedefinition())
            {
                current = current.RedefinedProperty.First(redefined => redefined.Name == current.Name);
            }

            return current;
        }

        /// <summary>
        /// Tries to query the derived_version column that a derived property is promoted to. Only the
        /// six hot Element-level derived properties are promoted; the rest live in derived_json.
        /// </summary>
        /// <param name="property">
        /// The subject derived <see cref="IProperty" />
        /// </param>
        /// <param name="columnName">
        /// The derived_version column name when promoted
        /// </param>
        /// <returns>
        /// True when the property is promoted to a real column
        /// </returns>
        public static bool TryQueryPromotedDerivedColumn(this IProperty property, out string columnName)
        {
            ArgumentNullException.ThrowIfNull(property);

            return PromotedDerivedColumns.TryGetValue(property.Name, out columnName);
        }
    }
}
