// -------------------------------------------------------------------------------------------------
// <copyright file="SqlSchemaBenchmarkTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Npgsql;

    using NpgsqlTypes;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.Core;
    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The full .NET benchmark harness for the SQL persistence schema — the follow-up gate of
    /// SysML2.NET.CodeGenerator/SQLSCHEMA.md: three projects with AUTHENTIC serializer payloads
    /// sharing the hash partitions, a commit-history replay, mass branch creation, a root-rename
    /// derived burst with concurrent read-latency measurement, a UUIDv4-vs-v7 bulk-insert A/B,
    /// and pgstattuple / wait-event longevity checks.
    ///
    /// Per TESTING.md section 10: latencies and throughput are REPORTED via TestContext.Out and
    /// deliberately never asserted (thresholds are flaky across machines); assertions cover only
    /// deterministic invariants — row counts, non-null reads, page sizes, and plan shape (the
    /// section 16.5 function-inlining guard).
    ///
    /// Scale knobs (environment variables; defaults are shape-faithful at reduced scale):
    ///   SYSML2_BENCH_ELEMENTS  elements in the giant project        (default 100000; gate 1000000)
    ///   SYSML2_BENCH_COMMITS   replayed history depth               (default 2000;   gate 20000)
    ///   SYSML2_BENCH_BRANCHES  branches created at the checkpoint   (default 500)
    ///
    /// Run:  dotnet test SysML2.NET.CodeGenerator.Tests/SysML2.NET.CodeGenerator.Tests.csproj
    ///         --filter "TestCategory=Benchmark" --logger "console;verbosity=detailed"
    /// </summary>
    [TestFixture]
    [Category("Benchmark")]
    [NonParallelizable]
    public class SqlSchemaBenchmarkTestFixture
    {
        /// <summary>
        /// The number of element versions each replayed commit rewrites
        /// </summary>
        private const int ChangeSetSize = 50;

        /// <summary>
        /// Elements in the giant project (env SYSML2_BENCH_ELEMENTS)
        /// </summary>
        private static readonly int GiantElements = EnvInt("SYSML2_BENCH_ELEMENTS", 200_000);

        /// <summary>
        /// Elements in each of the two co-tenant projects sharing the giant's hash partitions
        /// </summary>
        private static readonly int CoTenantElements = Math.Max(1_000, GiantElements / 10);

        /// <summary>
        /// Depth of the replayed commit history on the giant project (env SYSML2_BENCH_COMMITS)
        /// </summary>
        private static readonly int ReplayCommits = EnvInt("SYSML2_BENCH_COMMITS", 2_000);

        /// <summary>
        /// Branches created at the last checkpoint (env SYSML2_BENCH_BRANCHES)
        /// </summary>
        private static readonly int BranchCount = EnvInt("SYSML2_BENCH_BRANCHES", 500);

        /// <summary>
        /// Rows per mode in the UUIDv4-vs-v7 bulk-insert A/B
        /// </summary>
        private static readonly int AbRows = Math.Max(10_000, GiantElements / 2);

        private PostgreSqlSchemaTestHost host;
        private string connectionString;

        private readonly Guid giantProject = Guid.NewGuid();
        private readonly Guid coTenantA = Guid.NewGuid();
        private readonly Guid coTenantB = Guid.NewGuid();
        private readonly Guid mainBranch = Guid.NewGuid();

        private Guid seedCommit;
        private Guid headCommit;
        private Guid lastCheckpointCommit;
        private Guid midHistoryCommit;
        private Guid[] giantIdentities;
        private Guid[] giantPackageIdentities;
        private Guid[] checkpointBranches;
        private DateTime clock;

        private readonly List<string> setupReport = [];
        private readonly List<double> replayLatenciesMs = [];
        private readonly ConcurrentDictionary<string, int> burstWaitEvents = new();
        private double burstReadBaselineMs;
        private double burstReadDuringMs;
        private double burstSeconds;

        /// <summary>
        /// Reads an integer scale knob from the environment
        /// </summary>
        private static int EnvInt(string name, int fallback)
        {
            return int.TryParse(Environment.GetEnvironmentVariable(name), out var value) && value > 0 ? value : fallback;
        }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.host = new PostgreSqlSchemaTestHost();
            await this.host.StartAsync();
            this.connectionString = this.host.ConnectionString;

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory)
                .CreateSubdirectory(Path.Combine("UML", "_SysML2.NET.SqlBenchmark"));
            var generator = new SQLSchemaGenerator();
            var schema = await generator.GenerateSqlSchemaAsync(GeneratorSetupFixture.XmiReaderResult, directoryInfo);
            await this.host.ExecuteScriptAsync(schema);
            await this.host.ExecuteScriptAsync("CREATE EXTENSION IF NOT EXISTS pgstattuple;");

            this.clock = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var loadWatch = Stopwatch.StartNew();
            this.seedCommit = await this.LoadProjectAsync(this.giantProject, "BenchGiant", GiantElements, withOwnedRelationships: true);
            await this.LoadProjectAsync(this.coTenantA, "BenchCoTenantA", CoTenantElements, withOwnedRelationships: false);
            await this.LoadProjectAsync(this.coTenantB, "BenchCoTenantB", CoTenantElements, withOwnedRelationships: false);
            loadWatch.Stop();

            var totalRows = (2 * GiantElements) + (2 * CoTenantElements);
            this.setupReport.Add($"bulk load: {totalRows} elements incl. {GiantElements} memberships (authentic serializer payloads) in {loadWatch.Elapsed.TotalSeconds:F1} s = {totalRows / Math.Max(0.001, loadWatch.Elapsed.TotalSeconds):F0} elements/s");

            await this.ReplayHistoryAsync();
            await this.CreateBranchFleetAsync();
            await this.host.ExecuteScriptAsync("ANALYZE;");
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            if (this.host != null)
            {
                await this.host.DisposeAsync();
            }
        }

        /// <summary>
        /// Serializes a DTO with the production JSON serializer — the authentic stored_json payload
        /// </summary>
        private static string SerializeAuthentic(Core.DTO.Root.Elements.IElement element)
        {
            using var stream = new MemoryStream();
            new Serializer().Serialize(element, SerializationModeKind.JSON, false, stream, default(JsonWriterOptions));
            return Encoding.UTF8.GetString(stream.ToArray());
        }

        /// <summary>
        /// Bulk-loads one project: identities, element versions with serializer payloads, subtype
        /// rows for the PartUsage share, derived rows, and (optionally) owned-relationship link rows.
        /// Returns the seed commit id.
        /// </summary>
        private async Task<Guid> LoadProjectAsync(Guid projectId, string name, int elementCount, bool withOwnedRelationships)
        {
            var commitId = Guid.CreateVersion7();
            var identities = new Guid[elementCount];
            var versions = new Guid[elementCount];
            var kinds = new ClassKind[elementCount];

            for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
            {
                identities[elementIndex] = Guid.NewGuid();
                versions[elementIndex] = Guid.CreateVersion7();
                kinds[elementIndex] = elementIndex % 5 < 3 ? ClassKind.Package : ClassKind.PartUsage;
            }

            if (projectId == this.giantProject)
            {
                this.giantIdentities = identities;
                this.giantPackageIdentities = identities.Where((_, i) => kinds[i] == ClassKind.Package).ToArray();
            }

            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            await using (var batch = new NpgsqlBatch(connection))
            {
                var projectCommand = new NpgsqlBatchCommand("INSERT INTO sysml2.project (id, name, created) VALUES ($1, $2, $3)");
                projectCommand.Parameters.Add(new NpgsqlParameter { Value = projectId, NpgsqlDbType = NpgsqlDbType.Uuid });
                projectCommand.Parameters.Add(new NpgsqlParameter { Value = name, NpgsqlDbType = NpgsqlDbType.Text });
                projectCommand.Parameters.Add(new NpgsqlParameter { Value = this.clock, NpgsqlDbType = NpgsqlDbType.TimestampTz });
                batch.BatchCommands.Add(projectCommand);

                var commitCommand = new NpgsqlBatchCommand("INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id) VALUES ($1, $2, $3, 'bulk import', 1)");
                commitCommand.Parameters.Add(new NpgsqlParameter { Value = commitId, NpgsqlDbType = NpgsqlDbType.Uuid });
                commitCommand.Parameters.Add(new NpgsqlParameter { Value = projectId, NpgsqlDbType = NpgsqlDbType.Uuid });
                commitCommand.Parameters.Add(new NpgsqlParameter { Value = this.clock, NpgsqlDbType = NpgsqlDbType.TimestampTz });
                batch.BatchCommands.Add(commitCommand);

                await batch.ExecuteNonQueryAsync();
            }

            this.clock = this.clock.AddSeconds(1);

            await using (var importer = await connection.BeginBinaryImportAsync(
                "COPY sysml2.data_identity (id, project_id, class_kind) FROM STDIN (FORMAT BINARY)"))
            {
                for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
                {
                    await importer.StartRowAsync();
                    importer.Write(identities[elementIndex], NpgsqlDbType.Uuid);
                    importer.Write(projectId, NpgsqlDbType.Uuid);
                    importer.Write((short)kinds[elementIndex], NpgsqlDbType.Smallint);
                }

                await importer.CompleteAsync();
            }

            await using (var importer = await connection.BeginBinaryImportAsync(
                "COPY sysml2.element_version (project_id, version_id, identity_id, commit_id, class_kind, tombstone, element_id, declared_name, is_implied_included, stored_json) FROM STDIN (FORMAT BINARY)"))
            {
                for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
                {
                    var elementName = $"{name}Element{elementIndex}";

                    var payload = kinds[elementIndex] == ClassKind.Package
                        ? SerializeAuthentic(new Package { Id = identities[elementIndex], ElementId = identities[elementIndex].ToString(), DeclaredName = elementName })
                        : SerializeAuthentic(new PartUsage { Id = identities[elementIndex], ElementId = identities[elementIndex].ToString(), DeclaredName = elementName, IsComposite = true, IsUnique = true });

                    await importer.StartRowAsync();
                    importer.Write(projectId, NpgsqlDbType.Uuid);
                    importer.Write(versions[elementIndex], NpgsqlDbType.Uuid);
                    importer.Write(identities[elementIndex], NpgsqlDbType.Uuid);
                    importer.Write(commitId, NpgsqlDbType.Uuid);
                    importer.Write((short)kinds[elementIndex], NpgsqlDbType.Smallint);
                    importer.Write(false, NpgsqlDbType.Boolean);
                    importer.Write(identities[elementIndex].ToString(), NpgsqlDbType.Text);
                    importer.Write(elementName, NpgsqlDbType.Text);
                    importer.Write(false, NpgsqlDbType.Boolean);
                    importer.Write(payload, NpgsqlDbType.Jsonb);
                }

                await importer.CompleteAsync();
            }

            foreach (var (table, columns, writeRow) in new (string, string, Action<NpgsqlBinaryImporter, int>)[]
            {
                ("type_version", "project_id, version_id, is_abstract, is_sufficient", (imp, _) => { imp.Write(false, NpgsqlDbType.Boolean); imp.Write(false, NpgsqlDbType.Boolean); }),
                ("feature_version", "project_id, version_id, is_composite, is_unique", (imp, _) => { imp.Write(true, NpgsqlDbType.Boolean); imp.Write(true, NpgsqlDbType.Boolean); }),
                ("usage_version", "project_id, version_id, is_variation", (imp, _) => imp.Write(false, NpgsqlDbType.Boolean)),
                ("occurrence_usage_version", "project_id, version_id, is_individual", (imp, _) => imp.Write(false, NpgsqlDbType.Boolean))
            })
            {
                await using var importer = await connection.BeginBinaryImportAsync(
                    $"COPY sysml2.{table} ({columns}) FROM STDIN (FORMAT BINARY)");

                for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
                {
                    if (kinds[elementIndex] != ClassKind.PartUsage)
                    {
                        continue;
                    }

                    await importer.StartRowAsync();
                    importer.Write(projectId, NpgsqlDbType.Uuid);
                    importer.Write(versions[elementIndex], NpgsqlDbType.Uuid);
                    writeRow(importer, elementIndex);
                }

                await importer.CompleteAsync();
            }

            await using (var importer = await connection.BeginBinaryImportAsync(
                "COPY sysml2.derived_version (project_id, derived_id, identity_id, commit_id, owner, qualified_name, name, derived_json) FROM STDIN (FORMAT BINARY)"))
            {
                for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
                {
                    var elementName = $"{name}Element{elementIndex}";
                    var owner = elementIndex == 0 ? (Guid?)null : identities[elementIndex / 2];
                    var qualifiedName = $"{name}::{elementName}";

                    await importer.StartRowAsync();
                    importer.Write(projectId, NpgsqlDbType.Uuid);
                    importer.Write(Guid.CreateVersion7(), NpgsqlDbType.Uuid);
                    importer.Write(identities[elementIndex], NpgsqlDbType.Uuid);
                    importer.Write(commitId, NpgsqlDbType.Uuid);

                    if (owner.HasValue)
                    {
                        importer.Write(owner.Value, NpgsqlDbType.Uuid);
                    }
                    else
                    {
                        importer.WriteNull();
                    }

                    importer.Write(qualifiedName, NpgsqlDbType.Text);
                    importer.Write(elementName, NpgsqlDbType.Text);
                    importer.Write($"{{\"qualifiedName\":\"{qualifiedName}\",\"name\":\"{elementName}\",\"owner\":{(owner.HasValue ? $"\"{owner}\"" : "null")},\"isLibraryElement\":false,\"documentation\":[],\"ownedElement\":[]}}", NpgsqlDbType.Jsonb);
                }

                await importer.CompleteAsync();
            }

            if (withOwnedRelationships)
            {
                // real models are roughly half memberships: every content element gets its
                // OwningMembership — a Relationship metaclass, the only legal target of an
                // Element::ownedRelationship reference (the typed validation enforces this)
                var membershipIdentities = new Guid[elementCount];
                var membershipVersions = new Guid[elementCount];

                for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
                {
                    membershipIdentities[elementIndex] = Guid.NewGuid();
                    membershipVersions[elementIndex] = Guid.CreateVersion7();
                }

                await using (var importer = await connection.BeginBinaryImportAsync(
                    "COPY sysml2.data_identity (id, project_id, class_kind) FROM STDIN (FORMAT BINARY)"))
                {
                    for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
                    {
                        await importer.StartRowAsync();
                        importer.Write(membershipIdentities[elementIndex], NpgsqlDbType.Uuid);
                        importer.Write(projectId, NpgsqlDbType.Uuid);
                        importer.Write((short)ClassKind.OwningMembership, NpgsqlDbType.Smallint);
                    }

                    await importer.CompleteAsync();
                }

                await using (var importer = await connection.BeginBinaryImportAsync(
                    "COPY sysml2.element_version (project_id, version_id, identity_id, commit_id, class_kind, tombstone, element_id, is_implied_included, stored_json) FROM STDIN (FORMAT BINARY)"))
                {
                    for (var elementIndex = 0; elementIndex < elementCount; elementIndex++)
                    {
                        var payload = SerializeAuthentic(new OwningMembership { Id = membershipIdentities[elementIndex], ElementId = membershipIdentities[elementIndex].ToString() });

                        await importer.StartRowAsync();
                        importer.Write(projectId, NpgsqlDbType.Uuid);
                        importer.Write(membershipVersions[elementIndex], NpgsqlDbType.Uuid);
                        importer.Write(membershipIdentities[elementIndex], NpgsqlDbType.Uuid);
                        importer.Write(commitId, NpgsqlDbType.Uuid);
                        importer.Write((short)ClassKind.OwningMembership, NpgsqlDbType.Smallint);
                        importer.Write(false, NpgsqlDbType.Boolean);
                        importer.Write(membershipIdentities[elementIndex].ToString(), NpgsqlDbType.Text);
                        importer.Write(false, NpgsqlDbType.Boolean);
                        importer.Write(payload, NpgsqlDbType.Jsonb);
                    }

                    await importer.CompleteAsync();
                }

                await using (var importer = await connection.BeginBinaryImportAsync(
                    "COPY sysml2.element_owned_relationship (project_id, version_id, ordinal, target_identity) FROM STDIN (FORMAT BINARY)"))
                {
                    for (var elementIndex = 1; elementIndex < elementCount; elementIndex++)
                    {
                        await importer.StartRowAsync();
                        importer.Write(projectId, NpgsqlDbType.Uuid);
                        importer.Write(versions[elementIndex], NpgsqlDbType.Uuid);
                        importer.Write(0, NpgsqlDbType.Integer);
                        importer.Write(membershipIdentities[elementIndex], NpgsqlDbType.Uuid);
                    }

                    await importer.CompleteAsync();
                }
            }

            await using (var command = new NpgsqlCommand(
                "INSERT INTO sysml2.branch (id, project_id, name, head_commit_id) VALUES ($1, $2, 'main', $3);", connection))
            {
                command.Parameters.Add(new NpgsqlParameter { Value = projectId == this.giantProject ? this.mainBranch : Guid.NewGuid(), NpgsqlDbType = NpgsqlDbType.Uuid });
                command.Parameters.Add(new NpgsqlParameter { Value = projectId, NpgsqlDbType = NpgsqlDbType.Uuid });
                command.Parameters.Add(new NpgsqlParameter { Value = commitId, NpgsqlDbType = NpgsqlDbType.Uuid });
                await command.ExecuteNonQueryAsync();
            }

            return commitId;
        }

        /// <summary>
        /// Replays a linear commit history on the giant project: each commit rewrites
        /// <see cref="ChangeSetSize" /> Package elements (stored + derived), moves the main branch
        /// head, and checkpoints at a fixed cadence
        /// </summary>
        private async Task ReplayHistoryAsync()
        {
            var cadence = Math.Max(1, ReplayCommits / 4);
            var checkpointTimes = new List<double>();
            var parent = this.seedCommit;

            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            static NpgsqlBatchCommand BatchCommand(NpgsqlBatch batch, string sql, params (object Value, NpgsqlDbType Type)[] parameters)
            {
                var command = new NpgsqlBatchCommand(sql);

                foreach (var (value, type) in parameters)
                {
                    command.Parameters.Add(new NpgsqlParameter { Value = value, NpgsqlDbType = type });
                }

                batch.BatchCommands.Add(command);
                return command;
            }

            for (var commitIndex = 0; commitIndex < ReplayCommits; commitIndex++)
            {
                var commitId = Guid.CreateVersion7();
                this.clock = this.clock.AddSeconds(1);

                var versionIds = new Guid[ChangeSetSize];
                var derivedIds = new Guid[ChangeSetSize];
                var changedIdentities = new Guid[ChangeSetSize];
                var names = new string[ChangeSetSize];
                var storedPayloads = new string[ChangeSetSize];
                var derivedPayloads = new string[ChangeSetSize];

                // rotating window keeps the change set DISTINCT within one commit — the spec's
                // one-version-per-element-per-commit invariant (ux_element_version_identity_commit)
                var windowStart = (commitIndex * ChangeSetSize) % this.giantPackageIdentities.Length;

                for (var rowIndex = 0; rowIndex < ChangeSetSize; rowIndex++)
                {
                    versionIds[rowIndex] = Guid.CreateVersion7();
                    derivedIds[rowIndex] = Guid.CreateVersion7();
                    changedIdentities[rowIndex] = this.giantPackageIdentities[(windowStart + rowIndex) % this.giantPackageIdentities.Length];
                    names[rowIndex] = $"Renamed{commitIndex}_{rowIndex}";
                    storedPayloads[rowIndex] = SerializeAuthentic(new Package { Id = changedIdentities[rowIndex], ElementId = changedIdentities[rowIndex].ToString(), DeclaredName = names[rowIndex] });
                    derivedPayloads[rowIndex] = $"{{\"qualifiedName\":\"BenchGiant::{names[rowIndex]}\",\"name\":\"{names[rowIndex]}\",\"owner\":null}}";
                }

                var watch = Stopwatch.StartNew();

                await using (var transaction = await connection.BeginTransactionAsync())
                {
                    await using var batch = new NpgsqlBatch(connection, transaction);

                    BatchCommand(batch,
                        "INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id) VALUES ($1, $2, $3, 'replay', 1)",
                        (commitId, NpgsqlDbType.Uuid), (this.giantProject, NpgsqlDbType.Uuid), (this.clock, NpgsqlDbType.TimestampTz));

                    BatchCommand(batch,
                        "INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES ($1, $2, 0)",
                        (commitId, NpgsqlDbType.Uuid), (parent, NpgsqlDbType.Uuid));

                    BatchCommand(batch,
                        "INSERT INTO sysml2.element_version (project_id, version_id, identity_id, commit_id, class_kind, tombstone, element_id, declared_name, is_implied_included, stored_json) " +
                        $"SELECT $1, v, i, $2, {(short)ClassKind.Package}, false, i::text, n, false, s::jsonb FROM unnest($3::uuid[], $4::uuid[], $5::text[], $6::text[]) AS u(v, i, n, s)",
                        (this.giantProject, NpgsqlDbType.Uuid), (commitId, NpgsqlDbType.Uuid),
                        (versionIds, NpgsqlDbType.Array | NpgsqlDbType.Uuid), (changedIdentities, NpgsqlDbType.Array | NpgsqlDbType.Uuid),
                        (names, NpgsqlDbType.Array | NpgsqlDbType.Text), (storedPayloads, NpgsqlDbType.Array | NpgsqlDbType.Text));

                    BatchCommand(batch,
                        "INSERT INTO sysml2.derived_version (project_id, derived_id, identity_id, commit_id, qualified_name, name, derived_json) " +
                        "SELECT $1, d, i, $2, n, n, s::jsonb FROM unnest($3::uuid[], $4::uuid[], $5::text[], $6::text[]) AS u(d, i, n, s)",
                        (this.giantProject, NpgsqlDbType.Uuid), (commitId, NpgsqlDbType.Uuid),
                        (derivedIds, NpgsqlDbType.Array | NpgsqlDbType.Uuid), (changedIdentities, NpgsqlDbType.Array | NpgsqlDbType.Uuid),
                        (names, NpgsqlDbType.Array | NpgsqlDbType.Text), (derivedPayloads, NpgsqlDbType.Array | NpgsqlDbType.Text));

                    BatchCommand(batch,
                        "UPDATE sysml2.branch SET head_commit_id = $1 WHERE id = $2",
                        (commitId, NpgsqlDbType.Uuid), (this.mainBranch, NpgsqlDbType.Uuid));

                    await batch.ExecuteNonQueryAsync();
                    await transaction.CommitAsync();
                }

                watch.Stop();
                this.replayLatenciesMs.Add(watch.Elapsed.TotalMilliseconds);
                parent = commitId;

                if (commitIndex == ReplayCommits / 2)
                {
                    this.midHistoryCommit = commitId;
                }

                if ((commitIndex + 1) % cadence == 0)
                {
                    var checkpointWatch = Stopwatch.StartNew();

                    await using var checkpointCommand = new NpgsqlCommand("SELECT sysml2.build_commit_checkpoint($1, $2);", connection);
                    checkpointCommand.CommandTimeout = 600;
                    checkpointCommand.Parameters.Add(new NpgsqlParameter { Value = this.giantProject, NpgsqlDbType = NpgsqlDbType.Uuid });
                    checkpointCommand.Parameters.Add(new NpgsqlParameter { Value = commitId, NpgsqlDbType = NpgsqlDbType.Uuid });
                    await checkpointCommand.ExecuteScalarAsync();

                    checkpointWatch.Stop();
                    checkpointTimes.Add(checkpointWatch.Elapsed.TotalSeconds);
                    this.lastCheckpointCommit = commitId;
                }
            }

            this.headCommit = parent;

            var sorted = this.replayLatenciesMs.OrderBy(x => x).ToList();
            this.setupReport.Add($"history replay: {ReplayCommits} commits x {ChangeSetSize}-row change sets — commit txn median {sorted[sorted.Count / 2]:F1} ms, p95 {sorted[(int)(sorted.Count * 0.95)]:F1} ms");
            this.setupReport.Add($"checkpoint builds ({checkpointTimes.Count}, cadence {cadence}): {string.Join(", ", checkpointTimes.Select(t => $"{t:F1}s"))}");
        }

        /// <summary>
        /// Creates the branch fleet at the last checkpoint — each an O(1) overlay branch
        /// </summary>
        private async Task CreateBranchFleetAsync()
        {
            this.checkpointBranches = Enumerable.Range(0, BranchCount).Select(_ => Guid.NewGuid()).ToArray();

            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            var watch = Stopwatch.StartNew();

            for (var branchIndex = 0; branchIndex < BranchCount; branchIndex++)
            {
                await using var command = new NpgsqlCommand(
                    "INSERT INTO sysml2.branch (id, project_id, name, head_commit_id, base_commit_id) VALUES ($1, $2, $3, $4, $4);", connection);
                command.Parameters.Add(new NpgsqlParameter { Value = this.checkpointBranches[branchIndex], NpgsqlDbType = NpgsqlDbType.Uuid });
                command.Parameters.Add(new NpgsqlParameter { Value = this.giantProject, NpgsqlDbType = NpgsqlDbType.Uuid });
                command.Parameters.Add(new NpgsqlParameter { Value = $"bench-branch-{branchIndex}", NpgsqlDbType = NpgsqlDbType.Text });
                command.Parameters.Add(new NpgsqlParameter { Value = this.lastCheckpointCommit, NpgsqlDbType = NpgsqlDbType.Uuid });
                await command.ExecuteNonQueryAsync();
            }

            watch.Stop();
            this.setupReport.Add($"branch fleet: {BranchCount} overlay branches created in {watch.Elapsed.TotalMilliseconds:F0} ms ({watch.Elapsed.TotalMilliseconds / BranchCount:F2} ms/branch)");
        }

        /// <summary>
        /// Runs one scalar-returning SQL statement N times and returns the per-call latencies
        /// </summary>
        private async Task<List<double>> MeasureScalarAsync(string sql, Func<int, object[]> parameterFactory, int iterations)
        {
            var latencies = new List<double>(iterations);

            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                await using var command = new NpgsqlCommand(sql, connection);
                command.CommandTimeout = 600;

                foreach (var value in parameterFactory(iteration))
                {
                    command.Parameters.Add(new NpgsqlParameter { Value = value });
                }

                var watch = Stopwatch.StartNew();
                var result = await command.ExecuteScalarAsync();
                watch.Stop();
                latencies.Add(watch.Elapsed.TotalMilliseconds);

                Assert.That(result, Is.Not.Null, $"scalar came back null for: {sql}");
            }

            return latencies;
        }

        /// <summary>
        /// Formats a latency sample as min / median / p95
        /// </summary>
        private static string Stats(List<double> latencies)
        {
            var sorted = latencies.OrderBy(x => x).ToList();
            return $"min {sorted[0]:F2} ms, median {sorted[sorted.Count / 2]:F2} ms, p95 {sorted[(int)(sorted.Count * 0.95)]:F2} ms (n={sorted.Count})";
        }

        [Test]
        [Order(1)]
        public async Task Report_load_replay_and_branch_fleet()
        {
            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(
                "SELECT (SELECT count(*) FROM sysml2.element_version WHERE project_id = $1)," +
                "       (SELECT count(*) FROM sysml2.derived_version WHERE project_id = $1)," +
                "       (SELECT count(*) FROM sysml2.branch WHERE project_id = $1)", connection);
            command.Parameters.Add(new NpgsqlParameter { Value = this.giantProject, NpgsqlDbType = NpgsqlDbType.Uuid });

            await using var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(reader.GetInt64(0), Is.EqualTo((2L * GiantElements) + ((long)ReplayCommits * ChangeSetSize)), "element_version rows = content + memberships + replay");
                Assert.That(reader.GetInt64(1), Is.EqualTo(GiantElements + ((long)ReplayCommits * ChangeSetSize)), "derived_version rows = bulk + replay");
                Assert.That(reader.GetInt64(2), Is.EqualTo(BranchCount + 1), "branch fleet + main");
            }

            TestContext.Out.WriteLine($"scale: giant {GiantElements} elements + 2 co-tenants x {CoTenantElements}, {ReplayCommits} replay commits, {BranchCount} branches");

            foreach (var line in this.setupReport)
            {
                TestContext.Out.WriteLine(line);
            }
        }

        [Test]
        [Order(2)]
        public async Task Report_read_latencies()
        {
            var random = new Random(7);
            var branch = this.checkpointBranches[0];

            var headReads = await this.MeasureScalarAsync(
                "SELECT sysml2.get_element_at_branch_head($1, $2);",
                _ => [branch, this.giantIdentities[random.Next(this.giantIdentities.Length)]],
                200);

            var historicalReads = await this.MeasureScalarAsync(
                "SELECT sysml2.get_element_at_commit($1, $2, $3);",
                _ => [this.giantProject, this.midHistoryCommit, this.giantIdentities[random.Next(this.giantIdentities.Length)]],
                50);

            TestContext.Out.WriteLine($"single-element head read (checkpoint branch): {Stats(headReads)}");
            TestContext.Out.WriteLine($"single-element historical read (mid-history): {Stats(historicalReads)}");
        }

        [Test]
        [Order(3)]
        public async Task Fold_and_set_read_resolve_the_whole_model()
        {
            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            var foldWatch = Stopwatch.StartNew();
            long foldCount;

            await using (var command = new NpgsqlCommand("SELECT count(*) FROM sysml2.resolve_commit_state($1, $2);", connection))
            {
                command.CommandTimeout = 600;
                command.Parameters.Add(new NpgsqlParameter { Value = this.giantProject, NpgsqlDbType = NpgsqlDbType.Uuid });
                command.Parameters.Add(new NpgsqlParameter { Value = this.headCommit, NpgsqlDbType = NpgsqlDbType.Uuid });
                foldCount = (long)await command.ExecuteScalarAsync();
            }

            foldWatch.Stop();

            var setWatch = Stopwatch.StartNew();
            long setCount;

            await using (var command = new NpgsqlCommand("SELECT count(*) FROM sysml2.get_elements_at_branch_head($1);", connection))
            {
                command.CommandTimeout = 600;
                command.Parameters.Add(new NpgsqlParameter { Value = this.checkpointBranches[0], NpgsqlDbType = NpgsqlDbType.Uuid });
                setCount = (long)await command.ExecuteScalarAsync();
            }

            setWatch.Stop();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(foldCount, Is.EqualTo(2L * GiantElements), "the fold resolves every live element (content + memberships) exactly once");
                Assert.That(setCount, Is.EqualTo(2L * GiantElements), "the branch-head set read returns the whole model");
            }

            TestContext.Out.WriteLine($"full fold at head ({ReplayCommits - (ReplayCommits / Math.Max(1, ReplayCommits / 4) * (ReplayCommits / 4)) } commits past checkpoint window): {foldWatch.Elapsed.TotalMilliseconds:F0} ms for {foldCount} elements");
            TestContext.Out.WriteLine($"branch-head set read (payloads included): {setWatch.Elapsed.TotalMilliseconds:F0} ms for {setCount} elements");
        }

        [Test]
        [Order(4)]
        public async Task Keyset_page_is_index_priced_and_inlining_holds()
        {
            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            var branch = this.checkpointBranches[1];
            var after = this.giantIdentities.OrderBy(g => g).ElementAt(this.giantIdentities.Length / 2);

            var pageSql =
                $"SELECT h.identity_id FROM sysml2.get_elements_at_branch_head('{branch}') h " +
                $"WHERE h.identity_id > '{after}' ORDER BY h.identity_id LIMIT 100";

            var watch = Stopwatch.StartNew();
            var rows = 0;

            await using (var command = new NpgsqlCommand(pageSql, connection))
            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    rows++;
                }
            }

            watch.Stop();

            var plan = new StringBuilder();

            await using (var command = new NpgsqlCommand($"EXPLAIN {pageSql}", connection))
            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    plan.AppendLine(reader.GetString(0));
                }
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(rows, Is.EqualTo(100), "the keyset page returns exactly LIMIT rows");
                Assert.That(plan.ToString(), Does.Not.Contain("Function Scan on get_elements_at_branch_head"),
                    "SQL-function inlining broke - every page silently degrades to materialize-then-limit (guide section 16.5)");
            }

            TestContext.Out.WriteLine($"keyset page (100 rows, mid-model cursor): {watch.Elapsed.TotalMilliseconds:F2} ms; plan is inlined (no Function Scan)");
        }

        [Test]
        [Order(5)]
        public async Task Reference_validation_is_clean_at_both_tiers()
        {
            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            var incrementalWatch = Stopwatch.StartNew();
            long incrementalProblems;

            await using (var command = new NpgsqlCommand("SELECT count(*) FROM sysml2.validate_references_in_commit($1, $2);", connection))
            {
                command.CommandTimeout = 600;
                command.Parameters.Add(new NpgsqlParameter { Value = this.giantProject, NpgsqlDbType = NpgsqlDbType.Uuid });
                command.Parameters.Add(new NpgsqlParameter { Value = this.headCommit, NpgsqlDbType = NpgsqlDbType.Uuid });
                incrementalProblems = (long)await command.ExecuteScalarAsync();
            }

            incrementalWatch.Stop();

            var fullWatch = Stopwatch.StartNew();
            long fullProblems;

            await using (var command = new NpgsqlCommand("SELECT count(*) FROM sysml2.validate_references_at_commit($1, $2);", connection))
            {
                command.CommandTimeout = 600;
                command.Parameters.Add(new NpgsqlParameter { Value = this.giantProject, NpgsqlDbType = NpgsqlDbType.Uuid });
                command.Parameters.Add(new NpgsqlParameter { Value = this.headCommit, NpgsqlDbType = NpgsqlDbType.Uuid });
                fullProblems = (long)await command.ExecuteScalarAsync();
            }

            fullWatch.Stop();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(incrementalProblems, Is.Zero, "the replayed history contains no dangling or wrong-type references");
                Assert.That(fullProblems, Is.Zero, "the full audit agrees");
            }

            TestContext.Out.WriteLine($"incremental validation ({ChangeSetSize}-row change set): {incrementalWatch.Elapsed.TotalMilliseconds:F0} ms");
            TestContext.Out.WriteLine($"full reference audit ({GiantElements} elements): {fullWatch.Elapsed.TotalSeconds:F1} s");
        }

        [Test]
        [Order(6)]
        public async Task Root_rename_burst_with_concurrent_reads()
        {
            var baseline = await this.MeasureScalarAsync(
                "SELECT sysml2.get_element_at_branch_head($1, $2);",
                i => [this.checkpointBranches[2], this.giantIdentities[i % this.giantIdentities.Length]],
                200);
            this.burstReadBaselineMs = baseline.OrderBy(x => x).ElementAt(baseline.Count / 2);

            var burstCommit = Guid.CreateVersion7();
            this.clock = this.clock.AddSeconds(1);
            var readLatencies = new ConcurrentBag<double>();

            using var burstDone = new CancellationTokenSource();

            var samplerTask = Task.Run(async () =>
            {
                await using var connection = new NpgsqlConnection(this.connectionString);
                await connection.OpenAsync();

                while (!burstDone.IsCancellationRequested)
                {
                    await using var command = new NpgsqlCommand(
                        "SELECT coalesce(wait_event_type || ':' || wait_event, 'CPU') FROM pg_stat_activity WHERE state = 'active' AND pid <> pg_backend_pid();", connection);

                    await using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        this.burstWaitEvents.AddOrUpdate(reader.GetString(0), 1, (_, count) => count + 1);
                    }

                    try
                    {
                        await Task.Delay(50, burstDone.Token);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                }
            });

            var readerTasks = Enumerable.Range(0, 4).Select(readerIndex => Task.Run(async () =>
            {
                var random = new Random(readerIndex);

                await using var connection = new NpgsqlConnection(this.connectionString);
                await connection.OpenAsync();

                while (!burstDone.IsCancellationRequested)
                {
                    await using var command = new NpgsqlCommand("SELECT sysml2.get_element_at_branch_head($1, $2);", connection);
                    command.Parameters.Add(new NpgsqlParameter { Value = this.checkpointBranches[2], NpgsqlDbType = NpgsqlDbType.Uuid });
                    command.Parameters.Add(new NpgsqlParameter { Value = this.giantIdentities[random.Next(this.giantIdentities.Length)], NpgsqlDbType = NpgsqlDbType.Uuid });

                    var watch = Stopwatch.StartNew();
                    var result = await command.ExecuteScalarAsync();
                    watch.Stop();

                    Assert.That(result, Is.Not.Null, "a reader observed a missing element during the derived burst");
                    readLatencies.Add(watch.Elapsed.TotalMilliseconds);
                }
            })).ToArray();

            var burstWatch = Stopwatch.StartNew();

            await using (var connection = new NpgsqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                await using var transaction = await connection.BeginTransactionAsync();

                await using (var command = new NpgsqlCommand(
                    "INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id) VALUES ($1, $2, $3, 'root rename burst', 1)", connection, transaction))
                {
                    command.Parameters.Add(new NpgsqlParameter { Value = burstCommit, NpgsqlDbType = NpgsqlDbType.Uuid });
                    command.Parameters.Add(new NpgsqlParameter { Value = this.giantProject, NpgsqlDbType = NpgsqlDbType.Uuid });
                    command.Parameters.Add(new NpgsqlParameter { Value = this.clock, NpgsqlDbType = NpgsqlDbType.TimestampTz });
                    await command.ExecuteNonQueryAsync();
                }

                await using (var command = new NpgsqlCommand(
                    "INSERT INTO sysml2.commit_parent (commit_id, parent_commit_id, ordinal) VALUES ($1, $2, 0)", connection, transaction))
                {
                    command.Parameters.Add(new NpgsqlParameter { Value = burstCommit, NpgsqlDbType = NpgsqlDbType.Uuid });
                    command.Parameters.Add(new NpgsqlParameter { Value = this.headCommit, NpgsqlDbType = NpgsqlDbType.Uuid });
                    await command.ExecuteNonQueryAsync();
                }

                await using (var importer = await connection.BeginBinaryImportAsync(
                    "COPY sysml2.derived_version (project_id, derived_id, identity_id, commit_id, qualified_name, name, derived_json) FROM STDIN (FORMAT BINARY)"))
                {
                    for (var elementIndex = 0; elementIndex < this.giantIdentities.Length; elementIndex++)
                    {
                        await importer.StartRowAsync();
                        importer.Write(this.giantProject, NpgsqlDbType.Uuid);
                        importer.Write(Guid.CreateVersion7(), NpgsqlDbType.Uuid);
                        importer.Write(this.giantIdentities[elementIndex], NpgsqlDbType.Uuid);
                        importer.Write(burstCommit, NpgsqlDbType.Uuid);
                        importer.Write($"RenamedRoot::Element{elementIndex}", NpgsqlDbType.Text);
                        importer.Write($"Element{elementIndex}", NpgsqlDbType.Text);
                        importer.Write($"{{\"qualifiedName\":\"RenamedRoot::Element{elementIndex}\",\"name\":\"Element{elementIndex}\",\"owner\":null}}", NpgsqlDbType.Jsonb);
                    }

                    await importer.CompleteAsync();
                }

                await transaction.CommitAsync();
            }

            burstWatch.Stop();
            burstDone.Cancel();
            await Task.WhenAll(readerTasks.Append(samplerTask));

            this.burstSeconds = burstWatch.Elapsed.TotalSeconds;
            var during = readLatencies.OrderBy(x => x).ToList();
            this.burstReadDuringMs = during.Count > 0 ? during[during.Count / 2] : 0;
            this.headCommit = burstCommit;

            Assert.That(readLatencies, Is.Not.Empty, "readers must have run during the burst");

            TestContext.Out.WriteLine($"root-rename derived burst: {this.giantIdentities.Length} derived rows in {this.burstSeconds:F1} s = {this.giantIdentities.Length / Math.Max(0.001, this.burstSeconds):F0} rows/s (single txn, GIN maintained)");
            TestContext.Out.WriteLine($"concurrent head reads: median {this.burstReadDuringMs:F2} ms during burst vs {this.burstReadBaselineMs:F2} ms baseline ({during.Count} reads, none blocked, none null)");
        }

        [Test]
        [Order(7)]
        public async Task Uuid_v4_versus_v7_bulk_insert()
        {
            var results = new List<string>();

            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            foreach (var (label, factory) in new (string, Func<Guid>)[] { ("v4", Guid.NewGuid), ("v7", Guid.CreateVersion7) })
            {
                var projectId = Guid.NewGuid();

                var commitId = Guid.CreateVersion7();

                await using (var command = new NpgsqlCommand(
                    "INSERT INTO sysml2.project (id, name, created) VALUES ($1, $2, $3)", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter { Value = projectId, NpgsqlDbType = NpgsqlDbType.Uuid });
                    command.Parameters.Add(new NpgsqlParameter { Value = $"BenchAb{label}", NpgsqlDbType = NpgsqlDbType.Text });
                    command.Parameters.Add(new NpgsqlParameter { Value = this.clock, NpgsqlDbType = NpgsqlDbType.TimestampTz });
                    await command.ExecuteNonQueryAsync();
                }

                await using (var command = new NpgsqlCommand(
                    "INSERT INTO sysml2.commit (id, project_id, created, description, model_version_id) VALUES ($1, $2, $3, 'ab', 1)", connection))
                {
                    command.Parameters.Add(new NpgsqlParameter { Value = commitId, NpgsqlDbType = NpgsqlDbType.Uuid });
                    command.Parameters.Add(new NpgsqlParameter { Value = projectId, NpgsqlDbType = NpgsqlDbType.Uuid });
                    command.Parameters.Add(new NpgsqlParameter { Value = this.clock, NpgsqlDbType = NpgsqlDbType.TimestampTz });
                    await command.ExecuteNonQueryAsync();
                }
                var identities = Enumerable.Range(0, AbRows).Select(_ => Guid.NewGuid()).ToArray();

                await using (var importer = await connection.BeginBinaryImportAsync(
                    "COPY sysml2.data_identity (id, project_id, class_kind) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var identity in identities)
                    {
                        await importer.StartRowAsync();
                        importer.Write(identity, NpgsqlDbType.Uuid);
                        importer.Write(projectId, NpgsqlDbType.Uuid);
                        importer.Write((short)ClassKind.Package, NpgsqlDbType.Smallint);
                    }

                    await importer.CompleteAsync();
                }

                var watch = Stopwatch.StartNew();

                await using (var importer = await connection.BeginBinaryImportAsync(
                    "COPY sysml2.element_version (project_id, version_id, identity_id, commit_id, class_kind, tombstone, element_id, is_implied_included, stored_json) FROM STDIN (FORMAT BINARY)"))
                {
                    for (var rowIndex = 0; rowIndex < AbRows; rowIndex++)
                    {
                        await importer.StartRowAsync();
                        importer.Write(projectId, NpgsqlDbType.Uuid);
                        importer.Write(factory(), NpgsqlDbType.Uuid);
                        importer.Write(identities[rowIndex], NpgsqlDbType.Uuid);
                        importer.Write(commitId, NpgsqlDbType.Uuid);
                        importer.Write((short)ClassKind.Package, NpgsqlDbType.Smallint);
                        importer.Write(false, NpgsqlDbType.Boolean);
                        importer.Write(identities[rowIndex].ToString(), NpgsqlDbType.Text);
                        importer.Write(false, NpgsqlDbType.Boolean);
                        importer.Write("{\"@type\":\"Package\"}", NpgsqlDbType.Jsonb);
                    }

                    await importer.CompleteAsync();
                }

                watch.Stop();

                var leaf = (string)(await new NpgsqlCommand(
                    $"SELECT tableoid::regclass::text FROM sysml2.element_version WHERE project_id = '{projectId}' LIMIT 1", connection).ExecuteScalarAsync())!;

                string indexStats;

                await using (var command = new NpgsqlCommand(
                    $"SELECT round(avg_leaf_density::numeric, 1) || '% density, ' || round(leaf_fragmentation::numeric, 1) || '% fragmentation' FROM pgstatindex('{leaf}_pkey')", connection))
                {
                    indexStats = (string)(await command.ExecuteScalarAsync())!;
                }

                results.Add($"{label}: {AbRows} rows in {watch.Elapsed.TotalSeconds:F1} s = {AbRows / Math.Max(0.001, watch.Elapsed.TotalSeconds):F0} rows/s; PK leaf {leaf}: {indexStats}");
            }

            foreach (var line in results)
            {
                TestContext.Out.WriteLine($"uuid A/B {line}");
            }

            Assert.That(results, Has.Count.EqualTo(2));
        }

        [Test]
        [Order(8)]
        public async Task Longevity_bloat_and_wait_events()
        {
            await using var connection = new NpgsqlConnection(this.connectionString);
            await connection.OpenAsync();

            var giantLeaf = (string)(await new NpgsqlCommand(
                $"SELECT tableoid::regclass::text FROM sysml2.element_version WHERE project_id = '{this.giantProject}' LIMIT 1", connection).ExecuteScalarAsync())!;

            await using (var command = new NpgsqlCommand(
                $"SELECT 'element_version leaf {giantLeaf}: ' || round(dead_tuple_percent::numeric, 2) || '% dead, ' || round(approx_free_percent::numeric, 1) || '% free' FROM pgstattuple_approx('{giantLeaf}')" +
                " UNION ALL SELECT 'branch: ' || round(dead_tuple_percent::numeric, 2) || '% dead (head CAS churn)' FROM pgstattuple('sysml2.branch')", connection))
            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    TestContext.Out.WriteLine($"pgstattuple: {reader.GetString(0)}");
                }
            }

            await using (var command = new NpgsqlCommand(
                "SELECT relname || ': ' || seq_scan FROM pg_stat_user_tables WHERE relname LIKE 'element_version_p%' AND seq_scan > 0 ORDER BY seq_scan DESC LIMIT 5", connection))
            await using (var reader = await command.ExecuteReaderAsync())
            {
                var any = false;

                while (await reader.ReadAsync())
                {
                    any = true;
                    TestContext.Out.WriteLine($"seq scans (the section 15.15 signal): {reader.GetString(0)}");
                }

                if (!any)
                {
                    TestContext.Out.WriteLine("seq scans on element_version leaves: none - all access index-priced");
                }
            }

            var topWaits = this.burstWaitEvents.OrderByDescending(pair => pair.Value).Take(5)
                .Select(pair => $"{pair.Key} x{pair.Value}");
            TestContext.Out.WriteLine($"top wait events sampled during the derived burst: {string.Join(", ", topWaits)}");

            Assert.That(this.burstWaitEvents, Is.Not.Empty, "the wait-event sampler must have captured the burst");
        }
    }
}
