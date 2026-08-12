// -------------------------------------------------------------------------------------------------
// <copyright file="SqlSchemaHelpers.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Handlebars block helpers for the PostgreSQL schema generator. Each helper emits one
    /// [GENERATED] section of SysML2.NET.CodeGenerator/Templates/Uml/core-sql-schema-2.hbs;
    /// the hand-written sections (PIM, element_version, derived_version, snapshot resolution)
    /// live verbatim in the template. The reference design is
    /// SysML2.NET.CodeGenerator/Sql/schema.golden.sql.
    /// </summary>
    public static class SqlSchemaHelpers
    {
        /// <summary>
        /// Registers the SQL schema helpers
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars" /> context with which the helpers need to be registered
        /// </param>
        public static void RegisterUmlTemplateSqlSchemaHelpers(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("uml_template.SQL2.WriteEnumTypes", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);

                var stringBuilder = new StringBuilder();

                foreach (var enumeration in payload.Enumerations.OrderBy(enumeration => enumeration.Name, StringComparer.Ordinal))
                {
                    var literals = string.Join(", ", enumeration.OwnedLiteral.Select(literal => $"'{literal.Name.ToLowerInvariant()}'"));

                    stringBuilder.AppendLine($"CREATE TYPE sysml2.{enumeration.Name.QuerySqlSnakeCaseName()} AS ENUM ({literals});");
                }

                writer.WriteSafeString(stringBuilder.ToString());
            });

            handlebars.RegisterHelper("uml_template.SQL2.WriteClassKindRows", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);
                var classKinds = QueryClassKinds(payload);

                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine("INSERT INTO sysml2.class_kind (id, name, is_abstract) VALUES");

                var rows = classKinds
                    .Select(classKind => $"    ({classKind.Value}, '{classKind.Key.Name}', {FormatSqlBoolean(classKind.Key.IsAbstract)})");

                stringBuilder.AppendLine(string.Join(",\n", rows) + ";");

                writer.WriteSafeString(stringBuilder.ToString());
            });

            handlebars.RegisterHelper("uml_template.SQL2.WriteClassKindTableRows", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);
                var classKinds = QueryClassKinds(payload);

                var stringBuilder = new StringBuilder();
                var rows = new List<string>();

                foreach (var classKind in classKinds.Where(classKind => !classKind.Key.IsAbstract))
                {
                    rows.Add($"    ({classKind.Value}, 'element_version', 0)");

                    rows.AddRange(classKind.Key.QueryStorageAncestors()
                        .Select((ancestor, ancestorIndex) => $"    ({classKind.Value}, '{ancestor.QuerySqlSubtypeTableName()}', {ancestorIndex + 1})"));
                }

                stringBuilder.AppendLine("INSERT INTO sysml2.class_kind_table (class_kind, table_name, ordinal) VALUES");
                stringBuilder.AppendLine(string.Join(",\n", rows) + ";");

                writer.WriteSafeString(stringBuilder.ToString());
            });

            handlebars.RegisterHelper("uml_template.SQL2.WritePropertyCatalogRows", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);
                var classKinds = QueryClassKinds(payload);
                var declaringClasses = QueryDeclaringClasses(payload);

                var stringBuilder = new StringBuilder();

                foreach (var classKind in classKinds.Where(classKind => !classKind.Key.IsAbstract))
                {
                    var rows = QueryCatalogProperties(classKind.Key)
                        .Select(property => FormatPropertyCatalogRow(classKind.Key, classKind.Value, property, declaringClasses))
                        .ToList();

                    if (rows.Count == 0)
                    {
                        continue;
                    }

                    stringBuilder.AppendLine("INSERT INTO sysml2.property_catalog");
                    stringBuilder.AppendLine("    (class_kind, property_name, location, table_name, column_name, json_key, is_reference, is_collection, is_ordered, lower_bound, upper_bound) VALUES");
                    stringBuilder.AppendLine(string.Join(",\n", rows) + ";");
                    stringBuilder.AppendLine("");
                }

                writer.WriteSafeString(stringBuilder.ToString());
            });

            handlebars.RegisterHelper("uml_template.SQL2.WriteLinkTables", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);

                var stringBuilder = new StringBuilder();

                foreach (var @class in QueryOrderedClasses(payload).Where(@class => @class.QueryStoredMultiOwnProperties().Count != 0))
                {
                    foreach (var property in @class.QueryStoredMultiOwnProperties())
                    {
                        var tableName = @class.QuerySqlLinkTableName(property);
                        var isReference = property.QueryIsReferenceType();
                        var valueColumn = isReference ? "target_identity" : "value";
                        var valueType = isReference ? "uuid" : property.QuerySqlTypeName();
                        // deliberately NO cascade: identity deletion is an explicit, ordered, per-table
                        // procedure (see the data_identity section of the template) — an ON DELETE CASCADE
                        // here would execute per-row deletes filtered on target_identity alone, which no
                        // index leads with
                        var valueConstraint = isReference ? " REFERENCES sysml2.data_identity (id)" : string.Empty;

                        stringBuilder.AppendLine($"CREATE TABLE sysml2.{tableName} (");
                        stringBuilder.AppendLine("    project_id uuid NOT NULL,");
                        stringBuilder.AppendLine("    version_id uuid NOT NULL,");
                        stringBuilder.AppendLine("    ordinal    int  NOT NULL,");
                        stringBuilder.AppendLine($"    {valueColumn} {valueType} NOT NULL{valueConstraint},");
                        stringBuilder.AppendLine("    PRIMARY KEY (project_id, version_id, ordinal),");
                        stringBuilder.AppendLine("    FOREIGN KEY (project_id, version_id)");
                        stringBuilder.AppendLine("        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE");
                        stringBuilder.AppendLine(") PARTITION BY HASH (project_id);");
                        stringBuilder.AppendLine("");

                        if (isReference)
                        {
                            stringBuilder.AppendLine($"CREATE INDEX ix_{tableName}_target");
                            stringBuilder.AppendLine($"    ON sysml2.{tableName} (project_id, target_identity);");
                            stringBuilder.AppendLine("");
                        }
                    }
                }

                writer.WriteSafeString(stringBuilder.ToString());
            });

            handlebars.RegisterHelper("uml_template.SQL2.WriteSubtypeTables", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);

                var stringBuilder = new StringBuilder();

                foreach (var @class in QueryStorageIntroducingClasses(payload))
                {
                    var tableName = @class.QuerySqlSubtypeTableName();

                    stringBuilder.AppendLine($"CREATE TABLE sysml2.{tableName} (");
                    stringBuilder.AppendLine("    project_id uuid NOT NULL,");
                    stringBuilder.AppendLine("    version_id uuid NOT NULL,");

                    foreach (var property in @class.QueryStoredScalarOwnProperties())
                    {
                        stringBuilder.AppendLine($"    {FormatSubtypeColumn(property)},");
                    }

                    stringBuilder.AppendLine("    PRIMARY KEY (project_id, version_id),");
                    stringBuilder.AppendLine("    FOREIGN KEY (project_id, version_id)");
                    stringBuilder.AppendLine("        REFERENCES sysml2.element_version (project_id, version_id) ON DELETE CASCADE");
                    stringBuilder.AppendLine(") PARTITION BY HASH (project_id);");
                    stringBuilder.AppendLine("");

                    foreach (var property in @class.QueryStoredScalarOwnProperties().Where(property => property.QueryIsReferenceType()))
                    {
                        stringBuilder.AppendLine($"CREATE INDEX ix_{tableName}_{property.QuerySqlColumnName()}");
                        stringBuilder.AppendLine($"    ON sysml2.{tableName} (project_id, {property.QuerySqlColumnName()});");
                        stringBuilder.AppendLine("");
                    }
                }

                writer.WriteSafeString(stringBuilder.ToString());
            });

            handlebars.RegisterHelper("uml_template.SQL2.WriteFlatteningViews", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);
                var classKinds = QueryClassKinds(payload);

                var stringBuilder = new StringBuilder();

                foreach (var classKind in classKinds.Where(classKind => !classKind.Key.IsAbstract))
                {
                    var ancestors = classKind.Key.QueryStorageAncestors();

                    stringBuilder.AppendLine($"CREATE VIEW sysml2.v_{classKind.Key.Name.QuerySqlSnakeCaseName()} AS");
                    stringBuilder.AppendLine("    SELECT ev.project_id, ev.version_id, ev.identity_id, ev.commit_id,");
                    stringBuilder.Append("           ev.element_id, ev.declared_name, ev.declared_short_name, ev.is_implied_included, ev.owning_relationship");

                    foreach (var ancestor in ancestors)
                    {
                        var columns = ancestor.QueryStoredScalarOwnProperties()
                            .Select(property => $"{ancestor.QuerySqlSubtypeTableName()}.{property.QuerySqlColumnName()}");

                        stringBuilder.AppendLine(",");
                        stringBuilder.Append($"           {string.Join(", ", columns)}");
                    }

                    stringBuilder.AppendLine("");
                    stringBuilder.AppendLine("    FROM sysml2.element_version ev");

                    foreach (var ancestor in ancestors)
                    {
                        stringBuilder.AppendLine($"    JOIN sysml2.{ancestor.QuerySqlSubtypeTableName()} USING (project_id, version_id)");
                    }

                    stringBuilder.AppendLine($"    WHERE ev.class_kind = {classKind.Value} AND NOT ev.tombstone;");
                    stringBuilder.AppendLine("");
                }

                writer.WriteSafeString(stringBuilder.ToString());
            });

            handlebars.RegisterHelper("uml_template.SQL2.WritePartitionedTableArray", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);

                var linkTables = QueryOrderedClasses(payload)
                    .SelectMany(@class => @class.QueryStoredMultiOwnProperties().Select(property => @class.QuerySqlLinkTableName(property)));

                var subtypeTables = QueryStorageIntroducingClasses(payload)
                    .Select(@class => @class.QuerySqlSubtypeTableName());

                var tableNames = linkTables
                    .Concat(subtypeTables)
                    .Select(tableName => $"        '{tableName}'");

                writer.WriteSafeString(",\n" + string.Join(",\n", tableNames));
            });

            handlebars.RegisterHelper("uml_template.SQL2.WriteModelVersion", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);

                writer.WriteSafeString($"{payload.RootPackage.Name}:{payload.RootPackage.XmiId}");
            });
        }

        /// <summary>
        /// Resolves the <see cref="HandlebarsPayload" /> from the helper's first argument
        /// </summary>
        /// <param name="candidate">
        /// The first helper argument
        /// </param>
        /// <returns>
        /// The <see cref="HandlebarsPayload" />
        /// </returns>
        private static HandlebarsPayload ResolvePayload(object candidate)
        {
            if (candidate is not HandlebarsPayload payload)
            {
                throw new ArgumentException("The SQL schema helpers must be invoked with a HandlebarsPayload argument");
            }

            return payload;
        }

        /// <summary>
        /// Queries the classes of the payload in the deterministic order that assigns class_kind ids
        /// </summary>
        /// <param name="payload">
        /// The subject <see cref="HandlebarsPayload" />
        /// </param>
        /// <returns>
        /// The classes ordered by name
        /// </returns>
        private static IReadOnlyList<IClass> QueryOrderedClasses(HandlebarsPayload payload)
        {
            return payload.Classes
                .OrderBy(@class => @class.Name, StringComparer.Ordinal)
                .ToList();
        }

        /// <summary>
        /// Queries the interned class_kind id of every class. The id is the 1-based position in the
        /// name-ordered class list — deterministic across generator runs for an unchanged metamodel.
        /// </summary>
        /// <param name="payload">
        /// The subject <see cref="HandlebarsPayload" />
        /// </param>
        /// <returns>
        /// The class → id pairs, ordered by id
        /// </returns>
        private static IReadOnlyList<KeyValuePair<IClass, int>> QueryClassKinds(HandlebarsPayload payload)
        {
            return QueryOrderedClasses(payload)
                .Select((@class, classIndex) => new KeyValuePair<IClass, int>(@class, classIndex + 1))
                .ToList();
        }

        /// <summary>
        /// Queries the storage-introducing classes of the payload, ordered shallowest first so that
        /// supertype tables are created before the deeper ones that conceptually extend them
        /// </summary>
        /// <param name="payload">
        /// The subject <see cref="HandlebarsPayload" />
        /// </param>
        /// <returns>
        /// The storage-introducing classes
        /// </returns>
        private static IReadOnlyList<IClass> QueryStorageIntroducingClasses(HandlebarsPayload payload)
        {
            return QueryOrderedClasses(payload)
                .Where(SqlSchemaExtensions.QueryIsStorageIntroducing)
                .OrderBy(@class => @class.QueryAllGeneralClassifiers().Count)
                .ThenBy(@class => @class.Name, StringComparer.Ordinal)
                .ToList();
        }

        /// <summary>
        /// Queries the properties of a class that get a property_catalog row: the full flattened set,
        /// with properties that are redefined in the context of the class collapsed onto their most
        /// specific redefinition (the catalog is keyed by API property NAME)
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// The catalog properties, ordered by name
        /// </returns>
        private static IReadOnlyList<IProperty> QueryCatalogProperties(IClass @class)
        {
            return @class.QueryAllProperties()
                .Where(property => !property.TryQueryRedefinedByProperty(@class, out _))
                .GroupBy(property => property.Name)
                .Select(propertyGroup => propertyGroup.OrderByDescending(property => property.RedefinedProperty.Count).First())
                .OrderBy(property => property.Name, StringComparer.Ordinal)
                .ToList();
        }

        /// <summary>
        /// Queries, for every stored property in the metamodel, the class that DECLARES it. This is
        /// resolved through <see cref="SqlSchemaExtensions.QueryStoredOwnProperties" /> rather than
        /// <see cref="IProperty" />.Owner because association ends (e.g. Membership::memberElement) are
        /// owned by the association, not by the class whose storage they belong to.
        /// </summary>
        /// <param name="payload">
        /// The subject <see cref="HandlebarsPayload" />
        /// </param>
        /// <returns>
        /// A property-XmiId to declaring-class map
        /// </returns>
        private static Dictionary<string, IClass> QueryDeclaringClasses(HandlebarsPayload payload)
        {
            return QueryOrderedClasses(payload)
                .SelectMany(@class => @class.QueryStoredOwnProperties().Select(property => (property.XmiId, Class: @class)))
                .GroupBy(pair => pair.XmiId)
                .ToDictionary(pairGroup => pairGroup.Key, pairGroup => pairGroup.First().Class);
        }

        /// <summary>
        /// Formats one property_catalog VALUES row, resolving the property to its storage location:
        /// derived properties to derived_version (promoted column or derived_json key), stored
        /// multi-valued properties to their link table, stored scalars to the declaring class's subtype
        /// table (Element's scalars to element_version), same-name redefinitions to the storage of
        /// their root.
        /// </summary>
        /// <param name="class">
        /// The <see cref="IClass" /> whose catalog is being emitted
        /// </param>
        /// <param name="classKindId">
        /// The interned class_kind id of the class
        /// </param>
        /// <param name="property">
        /// The subject <see cref="IProperty" />
        /// </param>
        /// <param name="declaringClasses">
        /// The property-XmiId to declaring-class map of the whole metamodel
        /// </param>
        /// <returns>
        /// The formatted VALUES row
        /// </returns>
        private static string FormatPropertyCatalogRow(IClass @class, int classKindId, IProperty property, Dictionary<string, IClass> declaringClasses)
        {
            string location;
            string tableName = null;
            string columnName = null;
            string jsonKey = null;

            if (property.IsDerived || property.IsDerivedUnion)
            {
                location = "derived";
                tableName = "derived_version";

                if (property.TryQueryPromotedDerivedColumn(out var promotedColumn))
                {
                    columnName = promotedColumn;
                }
                else
                {
                    jsonKey = property.Name;
                }
            }
            else
            {
                var root = property.QueryStorageRootProperty();

                if (!declaringClasses.TryGetValue(root.XmiId, out var declaringClass))
                {
                    throw new NotSupportedException($"No declaring class found for the stored property {root.Name} of {@class.Name}");
                }

                if (root.QueryIsEnumerable())
                {
                    location = "link_table";
                    tableName = declaringClass.QuerySqlLinkTableName(root);
                }
                else
                {
                    location = "column";
                    tableName = declaringClass.Name == "Element" ? "element_version" : declaringClass.QuerySqlSubtypeTableName();
                    columnName = root.QuerySqlColumnName();
                }
            }

            var lowerBound = property.QueryIsNullable() ? 0 : 1;
            var upperBound = property.QueryIsEnumerable() ? -1 : 1;

            return $"    ({classKindId}, '{property.Name}', '{location}', {FormatSqlStringOrNull(tableName)}, {FormatSqlStringOrNull(columnName)}, {FormatSqlStringOrNull(jsonKey)}, " +
                   $"{FormatSqlBoolean(property.QueryIsReferenceType())}, {FormatSqlBoolean(property.QueryIsEnumerable())}, {FormatSqlBoolean(property.IsOrdered)}, {lowerBound}, {upperBound})";
        }

        /// <summary>
        /// Formats a single column definition of a subtype table, including nullability from the
        /// property's lower bound, the FK to data_identity for references, and the UML-declared
        /// default value when one exists
        /// </summary>
        /// <param name="property">
        /// The subject scalar stored <see cref="IProperty" />
        /// </param>
        /// <returns>
        /// The column definition, without the trailing comma
        /// </returns>
        private static string FormatSubtypeColumn(IProperty property)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(property.QuerySqlColumnName());
            stringBuilder.Append(' ');
            stringBuilder.Append(property.QuerySqlTypeName());
            stringBuilder.Append(property.QueryIsNullable() ? " NULL" : " NOT NULL");

            var defaultValue = FormatSqlDefaultValue(property);

            if (defaultValue != null)
            {
                stringBuilder.Append($" DEFAULT {defaultValue}");
            }

            if (property.QueryIsReferenceType())
            {
                stringBuilder.Append(" REFERENCES sysml2.data_identity (id)");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Formats the SQL DEFAULT expression for a property's UML-declared default value, or null
        /// when the property declares none
        /// </summary>
        /// <param name="property">
        /// The subject <see cref="IProperty" />
        /// </param>
        /// <returns>
        /// The SQL literal, or null
        /// </returns>
        private static string FormatSqlDefaultValue(IProperty property)
        {
            if (!property.QueryHasDefaultValue())
            {
                return null;
            }

            var defaultValue = property.QueryDefaultValueAsString();

            if (string.IsNullOrWhiteSpace(defaultValue) || defaultValue == "null")
            {
                return null;
            }

            if (property.QueryIsEnum() || property.QueryIsString())
            {
                return $"'{defaultValue.ToLowerInvariant()}'";
            }

            return defaultValue.ToLowerInvariant();
        }

        /// <summary>
        /// Formats a boolean as a SQL literal
        /// </summary>
        /// <param name="value">
        /// The value to format
        /// </param>
        /// <returns>
        /// "true" or "false"
        /// </returns>
        private static string FormatSqlBoolean(bool value)
        {
            return value ? "true" : "false";
        }

        /// <summary>
        /// Formats an optional string as a quoted SQL literal or NULL
        /// </summary>
        /// <param name="value">
        /// The value to format
        /// </param>
        /// <returns>
        /// The quoted literal, or "NULL"
        /// </returns>
        private static string FormatSqlStringOrNull(string value)
        {
            return value == null ? "NULL" : $"'{value}'";
        }
    }
}
