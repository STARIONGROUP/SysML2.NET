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

            handlebars.RegisterHelper("uml_template.SQL2.WriteMetamodelCatalogRows", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);

                AssertRegistryInSyncWithModel(payload);

                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine("INSERT INTO sysml2.model_version (id, name, source_fingerprint) VALUES");

                var modelVersionRows = ClassKindRegistry.ModelVersions
                    .Select(modelVersion => $"    ({modelVersion.Id}, '{modelVersion.Name}', '{modelVersion.SourceFingerprint}')");

                stringBuilder.AppendLine(string.Join(",\n", modelVersionRows));
                stringBuilder.AppendLine("ON CONFLICT (id) DO NOTHING;");
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("INSERT INTO sysml2.class_kind (id, name, is_abstract, introduced_in, removed_in) VALUES");

                var classKindRows = ClassKindRegistry.ClassKinds
                    .Select(registration => $"    ({registration.Id}, '{registration.Name}', {FormatSqlBoolean(registration.IsAbstract)}, {registration.IntroducedIn}, {(registration.RemovedIn.HasValue ? registration.RemovedIn.Value.ToString() : "NULL")})");

                stringBuilder.AppendLine(string.Join(",\n", classKindRows));
                stringBuilder.AppendLine("ON CONFLICT (id) DO NOTHING;");

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

                    stringBuilder.AppendLine($"CREATE VIEW sysml2.vw_{classKind.Key.Name.QuerySqlSnakeCaseName()} AS");
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

            handlebars.RegisterHelper("uml_template.SQL2.WriteReferenceValidation", (writer, _, parameters) =>
            {
                var payload = ResolvePayload(parameters[0]);
                var registrationsByName = ClassKindRegistry.ClassKinds.ToDictionary(registration => registration.Name);

                var concreteClassCount = QueryOrderedClasses(payload).Count(@class => !@class.IsAbstract);
                var referenceSources = new List<(string TableName, string ColumnName, string TypeName)>();

                foreach (var @class in QueryOrderedClasses(payload))
                {
                    var scalarTable = @class.Name == "Element" ? "element_version" : @class.QuerySqlSubtypeTableName();

                    referenceSources.AddRange(@class.QueryStoredScalarOwnProperties()
                        .Where(property => property.QueryIsReferenceType())
                        .Select(property => (scalarTable, property.QuerySqlColumnName(), property.QueryTypeName())));

                    referenceSources.AddRange(@class.QueryStoredMultiOwnProperties()
                        .Where(property => property.QueryIsReferenceType())
                        .Select(property => (@class.QuerySqlLinkTableName(property), "target_identity", property.QueryTypeName())));
                }

                var boundedSources = referenceSources
                    .Select(source =>
                    {
                        var allowedTargetKinds = QueryAllowedTargetKinds(payload, registrationsByName, source.TypeName);

                        return (source.TableName, source.ColumnName,
                            AllowedTargetKinds: allowedTargetKinds.Count == concreteClassCount ? null : allowedTargetKinds);
                    })
                    .ToList();

                var fullPassBlocks = boundedSources
                    .Select(source => FormatReferenceValidationBlock(source.TableName, source.ColumnName, source.AllowedTargetKinds));

                var outgoingBlocks = boundedSources
                    .Select(source => FormatOutgoingValidationBlock(source.TableName, source.ColumnName, source.AllowedTargetKinds));

                var incomingBlocks = boundedSources
                    .Select(source => FormatIncomingValidationBlock(source.TableName, source.ColumnName));

                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine("CREATE OR REPLACE FUNCTION sysml2.validate_references_at_commit(");
                stringBuilder.AppendLine("    p_project_id uuid,");
                stringBuilder.AppendLine("    p_commit_id  uuid");
                stringBuilder.AppendLine(")");
                stringBuilder.AppendLine("RETURNS TABLE (");
                stringBuilder.AppendLine("    source_table    text,");
                stringBuilder.AppendLine("    source_column   text,");
                stringBuilder.AppendLine("    source_identity uuid,");
                stringBuilder.AppendLine("    target_identity uuid,");
                stringBuilder.AppendLine("    problem         text");
                stringBuilder.AppendLine(")");
                stringBuilder.AppendLine("LANGUAGE plpgsql");
                stringBuilder.AppendLine("AS $$");
                stringBuilder.AppendLine("BEGIN");
                stringBuilder.AppendLine("    -- Materialize + ANALYZE the snapshot so the planner knows its TRUE cardinality and");
                stringBuilder.AppendLine("    -- can choose per arm between hashing the source (young history) and snapshot-driven");
                stringBuilder.AppendLine("    -- PK probes (deep history) — bounding the pass at O(snapshot x log history) instead");
                stringBuilder.AppendLine("    -- of O(history). A bare function CTE would be estimated at ~1000 rows.");
                stringBuilder.AppendLine("    CREATE TEMP TABLE IF NOT EXISTS validation_snapshot (");
                stringBuilder.AppendLine("        identity_id uuid NOT NULL,");
                stringBuilder.AppendLine("        version_id  uuid NOT NULL");
                stringBuilder.AppendLine("    ) ON COMMIT DROP;");
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("    TRUNCATE validation_snapshot;");
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("    INSERT INTO validation_snapshot (identity_id, version_id)");
                stringBuilder.AppendLine("    SELECT r.identity_id, r.version_id");
                stringBuilder.AppendLine("    FROM sysml2.resolve_commit_state(p_project_id, p_commit_id) r;");
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("    CREATE INDEX IF NOT EXISTS ix_validation_snapshot_version  ON validation_snapshot (version_id);");
                stringBuilder.AppendLine("    CREATE INDEX IF NOT EXISTS ix_validation_snapshot_identity ON validation_snapshot (identity_id);");
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("    ANALYZE validation_snapshot;");
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("    RETURN QUERY");
                stringBuilder.AppendLine(string.Join("\n    UNION ALL\n", fullPassBlocks) + ";");
                stringBuilder.AppendLine("END;");
                stringBuilder.AppendLine("$$;");
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("-- The INCREMENTAL tier: validates only commit p_commit_id's change set — outgoing");
                stringBuilder.AppendLine("-- references of its new versions, plus the reverse direction its tombstones break");
                stringBuilder.AppendLine("-- (a live, UNCHANGED element left referencing a deleted identity). O(change set),");
                stringBuilder.AppendLine("-- independent of history and snapshot size; the full pass above remains the");
                stringBuilder.AppendLine("-- periodic audit that backstops it.");
                stringBuilder.AppendLine("CREATE OR REPLACE FUNCTION sysml2.validate_references_in_commit(");
                stringBuilder.AppendLine("    p_project_id uuid,");
                stringBuilder.AppendLine("    p_commit_id  uuid");
                stringBuilder.AppendLine(")");
                stringBuilder.AppendLine("RETURNS TABLE (");
                stringBuilder.AppendLine("    source_table    text,");
                stringBuilder.AppendLine("    source_column   text,");
                stringBuilder.AppendLine("    source_identity uuid,");
                stringBuilder.AppendLine("    target_identity uuid,");
                stringBuilder.AppendLine("    problem         text");
                stringBuilder.AppendLine(")");
                stringBuilder.AppendLine("LANGUAGE sql");
                stringBuilder.AppendLine("STABLE");
                stringBuilder.AppendLine("AS $$");
                stringBuilder.AppendLine("    SELECT DISTINCT findings.source_table, findings.source_column,");
                stringBuilder.AppendLine("           findings.source_identity, findings.target_identity, findings.problem");
                stringBuilder.AppendLine("    FROM (");
                stringBuilder.AppendLine(string.Join("\n    UNION ALL\n", outgoingBlocks.Concat(incomingBlocks)));
                stringBuilder.AppendLine("    ) AS findings (source_table, source_column, source_identity, target_identity, problem);");
                stringBuilder.AppendLine("$$;");

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
        /// Queries the interned class_kind id of every class in the payload. The ids come from the
        /// append-only <see cref="ClassKindRegistry" /> — frozen once assigned, never positional —
        /// so generated artifacts (seeds, view predicates) stay stable across metamodel releases.
        /// </summary>
        /// <param name="payload">
        /// The subject <see cref="HandlebarsPayload" />
        /// </param>
        /// <returns>
        /// The class → id pairs, ordered by id
        /// </returns>
        private static IReadOnlyList<KeyValuePair<IClass, int>> QueryClassKinds(HandlebarsPayload payload)
        {
            var registrationsByName = ClassKindRegistry.ClassKinds.ToDictionary(registration => registration.Name);

            return QueryOrderedClasses(payload)
                .Select(@class => new KeyValuePair<IClass, int>(@class, registrationsByName[@class.Name].Id))
                .OrderBy(classKind => classKind.Value)
                .ToList();
        }

        /// <summary>
        /// Asserts that the UML model on disk matches the newest release registered in the
        /// append-only <see cref="ClassKindRegistry" />. Any drift fails generation LOUDLY —
        /// silently renumbering class_kind ids would corrupt every populated database and every
        /// consumer of the generated ClassKind enum.
        /// </summary>
        /// <param name="payload">
        /// The subject <see cref="HandlebarsPayload" />
        /// </param>
        private static void AssertRegistryInSyncWithModel(HandlebarsPayload payload)
        {
            var newestVersion = ClassKindRegistry.ModelVersions[^1];
            var fingerprint = $"{payload.RootPackage.Name}:{payload.RootPackage.XmiId}";

            if (fingerprint != newestVersion.SourceFingerprint)
            {
                throw new InvalidOperationException(
                    $"The UML model fingerprint '{fingerprint}' does not match the newest registered model version '{newestVersion.Name}' " +
                    $"('{newestVersion.SourceFingerprint}') in ClassKindRegistry. Append a new ModelVersionRegistration for a new metamodel " +
                    "release, or update the fingerprint in place for an editorial change that adds or removes no metaclasses.");
            }

            var registrationsByName = ClassKindRegistry.ClassKinds.ToDictionary(registration => registration.Name);
            var orderedClasses = QueryOrderedClasses(payload);

            var unregisteredClasses = orderedClasses
                .Where(@class => !registrationsByName.ContainsKey(@class.Name))
                .ToList();

            if (unregisteredClasses.Count != 0)
            {
                var maxId = ClassKindRegistry.ClassKinds.Max(registration => registration.Id);

                var suggestedRegistrations = string.Join("\n", unregisteredClasses
                    .Select((@class, classIndex) => $"    new({maxId + classIndex + 1}, \"{@class.Name}\", {(@class.IsAbstract ? "true" : "false")}, {newestVersion.Id}),"));

                throw new InvalidOperationException(
                    "The UML model contains metaclasses that are not registered in ClassKindRegistry. APPEND them after the highest " +
                    $"existing id — never renumber existing entries:\n{suggestedRegistrations}");
            }

            var modelClassNames = orderedClasses
                .Select(@class => @class.Name)
                .ToHashSet();

            var staleRegistrations = ClassKindRegistry.ClassKinds
                .Where(registration => registration.RemovedIn == null && !modelClassNames.Contains(registration.Name))
                .ToList();

            if (staleRegistrations.Count != 0)
            {
                var staleNames = string.Join(", ", staleRegistrations.Select(registration => registration.Name));

                throw new InvalidOperationException(
                    $"ClassKindRegistry registers metaclasses the UML model no longer contains: {staleNames}. Keep their entries and " +
                    "close them with RemovedIn = the id of the release that dropped them — never delete a registration.");
            }

            var driftedRegistrations = orderedClasses
                .Where(@class => registrationsByName[@class.Name].IsAbstract != @class.IsAbstract)
                .ToList();

            if (driftedRegistrations.Count != 0)
            {
                var driftedNames = string.Join(", ", driftedRegistrations.Select(@class => @class.Name));

                throw new InvalidOperationException(
                    $"The abstractness of {driftedNames} differs between the UML model and ClassKindRegistry — update the registry entries to match.");
            }
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
        /// Queries the interned class_kind ids a reference property of the given declared type may
        /// legally target: the concrete descendants of the declared type, the type itself included
        /// when concrete. Resolved against the append-only <see cref="ClassKindRegistry" />.
        /// </summary>
        /// <param name="payload">
        /// The subject <see cref="HandlebarsPayload" />
        /// </param>
        /// <param name="registrationsByName">
        /// The registry lookup by metaclass name
        /// </param>
        /// <param name="typeName">
        /// The name of the property's declared type, e.g. "Relationship"
        /// </param>
        /// <returns>
        /// The allowed class_kind ids, ordered ascending
        /// </returns>
        private static IReadOnlyList<int> QueryAllowedTargetKinds(HandlebarsPayload payload, Dictionary<string, ClassKindRegistration> registrationsByName, string typeName)
        {
            return QueryOrderedClasses(payload)
                .Where(@class => !@class.IsAbstract)
                .Where(@class => @class.Name == typeName
                    || @class.QueryAllGeneralClassifiers().OfType<IClass>().Any(general => general.Name == typeName))
                .Select(@class => registrationsByName[@class.Name].Id)
                .OrderBy(classKindId => classKindId)
                .ToList();
        }

        /// <summary>
        /// Formats one UNION ALL arm of validate_references_at_commit for a single stored reference
        /// column. Reports 'wrong-type' via the typed identity (checked for cross-project targets
        /// too) and 'dangling' for same-project targets absent from the snapshot; liveness of
        /// cross-project targets is deliberately out of scope (it depends on the used-project
        /// commit, which is service-layer resolution).
        /// </summary>
        /// <param name="tableName">
        /// The source table carrying the reference column
        /// </param>
        /// <param name="columnName">
        /// The reference column
        /// </param>
        /// <param name="allowedTargetKinds">
        /// The legal target class_kind ids, or null when every concrete metaclass is legal (an
        /// Element-typed reference) and the type check is omitted
        /// </param>
        /// <returns>
        /// The formatted SELECT arm, without a trailing separator
        /// </returns>
        private static string FormatReferenceValidationBlock(string tableName, string columnName, IReadOnlyList<int> allowedTargetKinds)
        {
            var stringBuilder = new StringBuilder();

            if (allowedTargetKinds == null)
            {
                stringBuilder.AppendLine($"    SELECT '{tableName}'::text, '{columnName}'::text,");
                stringBuilder.AppendLine($"           snap.identity_id, src.{columnName}, 'dangling'::text");
                stringBuilder.AppendLine($"    FROM sysml2.{tableName} src");
                stringBuilder.AppendLine("    JOIN validation_snapshot snap ON snap.version_id = src.version_id");
                stringBuilder.AppendLine($"    JOIN sysml2.data_identity ti ON ti.id = src.{columnName}");
                stringBuilder.AppendLine($"    LEFT JOIN validation_snapshot live ON live.identity_id = src.{columnName}");
                stringBuilder.AppendLine("    WHERE src.project_id = p_project_id");
                stringBuilder.AppendLine($"      AND src.{columnName} IS NOT NULL");
                stringBuilder.AppendLine("      AND ti.project_id = p_project_id");
                stringBuilder.Append("      AND live.identity_id IS NULL");

                return stringBuilder.ToString();
            }

            var allowedIds = string.Join(", ", allowedTargetKinds);

            stringBuilder.AppendLine($"    SELECT '{tableName}'::text, '{columnName}'::text,");
            stringBuilder.AppendLine($"           snap.identity_id, src.{columnName},");
            stringBuilder.AppendLine($"           CASE WHEN ti.class_kind NOT IN ({allowedIds}) THEN 'wrong-type' ELSE 'dangling' END");
            stringBuilder.AppendLine($"    FROM sysml2.{tableName} src");
            stringBuilder.AppendLine("    JOIN validation_snapshot snap ON snap.version_id = src.version_id");
            stringBuilder.AppendLine($"    JOIN sysml2.data_identity ti ON ti.id = src.{columnName}");
            stringBuilder.AppendLine($"    LEFT JOIN validation_snapshot live ON live.identity_id = src.{columnName}");
            stringBuilder.AppendLine("    WHERE src.project_id = p_project_id");
            stringBuilder.AppendLine($"      AND src.{columnName} IS NOT NULL");
            stringBuilder.AppendLine($"      AND (ti.class_kind NOT IN ({allowedIds})");
            stringBuilder.Append("           OR (ti.project_id = p_project_id AND live.identity_id IS NULL))");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Formats one UNION ALL arm of the OUTGOING half of validate_references_in_commit: the
        /// references carried by the versions the commit itself wrote. Liveness is probed per
        /// target through resolve_element_at_commit (zero rows == not alive), so the arm is
        /// O(change set) — no snapshot materialization.
        /// </summary>
        /// <param name="tableName">
        /// The source table carrying the reference column
        /// </param>
        /// <param name="columnName">
        /// The reference column
        /// </param>
        /// <param name="allowedTargetKinds">
        /// The legal target class_kind ids, or null when every concrete metaclass is legal and the
        /// type check is omitted
        /// </param>
        /// <returns>
        /// The formatted SELECT arm, without a trailing separator
        /// </returns>
        private static string FormatOutgoingValidationBlock(string tableName, string columnName, IReadOnlyList<int> allowedTargetKinds)
        {
            var stringBuilder = new StringBuilder();
            var isCoreTable = tableName == "element_version";
            var sourceAlias = isCoreTable ? "changed" : "src";

            stringBuilder.AppendLine($"    SELECT '{tableName}'::text, '{columnName}'::text,");
            stringBuilder.AppendLine($"           changed.identity_id, {sourceAlias}.{columnName},");

            if (allowedTargetKinds == null)
            {
                stringBuilder.AppendLine("           'dangling'::text");
            }
            else
            {
                stringBuilder.AppendLine($"           CASE WHEN ti.class_kind NOT IN ({string.Join(", ", allowedTargetKinds)}) THEN 'wrong-type' ELSE 'dangling' END");
            }

            stringBuilder.AppendLine("    FROM sysml2.element_version changed");

            if (!isCoreTable)
            {
                stringBuilder.AppendLine($"    JOIN sysml2.{tableName} src");
                stringBuilder.AppendLine("      ON src.project_id = changed.project_id AND src.version_id = changed.version_id");
            }

            stringBuilder.AppendLine($"    JOIN sysml2.data_identity ti ON ti.id = {sourceAlias}.{columnName}");
            stringBuilder.AppendLine("    WHERE changed.project_id = p_project_id");
            stringBuilder.AppendLine("      AND changed.commit_id = p_commit_id");
            stringBuilder.AppendLine("      AND NOT changed.tombstone");
            stringBuilder.AppendLine($"      AND {sourceAlias}.{columnName} IS NOT NULL");

            var livenessCheck =
                $"(ti.project_id = p_project_id\n" +
                $"           AND NOT EXISTS (SELECT 1 FROM sysml2.resolve_element_at_commit(p_project_id, p_commit_id, {sourceAlias}.{columnName})))";

            if (allowedTargetKinds == null)
            {
                stringBuilder.Append($"      AND {livenessCheck}");
            }
            else
            {
                stringBuilder.AppendLine($"      AND (ti.class_kind NOT IN ({string.Join(", ", allowedTargetKinds)})");
                stringBuilder.Append($"           OR {livenessCheck})");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Formats one UNION ALL arm of the INCOMING half of validate_references_in_commit: for
        /// every identity the commit tombstones, the live-but-UNCHANGED holders whose stored
        /// reference now dangles. Driven by the reverse-lookup index on the reference column;
        /// a candidate holder row only counts when it IS its identity's live version at the
        /// commit (probed through resolve_element_at_commit).
        /// </summary>
        /// <param name="tableName">
        /// The source table carrying the reference column
        /// </param>
        /// <param name="columnName">
        /// The reference column
        /// </param>
        /// <returns>
        /// The formatted SELECT arm, without a trailing separator
        /// </returns>
        private static string FormatIncomingValidationBlock(string tableName, string columnName)
        {
            var stringBuilder = new StringBuilder();
            var isCoreTable = tableName == "element_version";

            stringBuilder.AppendLine($"    SELECT '{tableName}'::text, '{columnName}'::text,");
            stringBuilder.AppendLine("           holder.identity_id, dead.identity_id, 'dangling'::text");
            stringBuilder.AppendLine("    FROM sysml2.element_version dead");

            if (isCoreTable)
            {
                stringBuilder.AppendLine("    JOIN sysml2.element_version holder");
                stringBuilder.AppendLine($"      ON holder.project_id = dead.project_id AND holder.{columnName} = dead.identity_id");
            }
            else
            {
                stringBuilder.AppendLine($"    JOIN sysml2.{tableName} src");
                stringBuilder.AppendLine($"      ON src.project_id = dead.project_id AND src.{columnName} = dead.identity_id");
                stringBuilder.AppendLine("    JOIN sysml2.element_version holder");
                stringBuilder.AppendLine("      ON holder.project_id = src.project_id AND holder.version_id = src.version_id");
            }

            stringBuilder.AppendLine("    WHERE dead.project_id = p_project_id");
            stringBuilder.AppendLine("      AND dead.commit_id = p_commit_id");
            stringBuilder.AppendLine("      AND dead.tombstone");
            stringBuilder.AppendLine("      AND EXISTS (SELECT 1 FROM sysml2.resolve_element_at_commit(p_project_id, p_commit_id, holder.identity_id) alive");
            stringBuilder.Append("                  WHERE alive.version_id = holder.version_id)");

            return stringBuilder.ToString();
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

    }
}
