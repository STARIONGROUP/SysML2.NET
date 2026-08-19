// -------------------------------------------------------------------------------------------------
// <copyright file="PostgreSqlSchemaTestHost.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Npgsql;

    using NUnit.Framework;

    using Testcontainers.PostgreSql;

    /// <summary>
    /// Hosts a disposable PostgreSQL 18 Testcontainer for the SQL-schema integration fixtures,
    /// configured per the deployment requirement of SysML2.NET.CodeGenerator/SQLSCHEMA.md
    /// (max_locks_per_transaction=4096 — whole-schema DDL fails on the default of 64). Skips the
    /// owning fixture with <see cref="Assert.Ignore(string)" /> when Docker is not available.
    /// </summary>
    public sealed class PostgreSqlSchemaTestHost : IAsyncDisposable
    {
        /// <summary>
        /// The PostgreSQL Testcontainer, null until <see cref="StartAsync" /> succeeds
        /// </summary>
        private PostgreSqlContainer container;

        /// <summary>
        /// Gets the connection string of the started container
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Starts the container, ignoring the owning fixture when Docker is not available
        /// </summary>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public async Task StartAsync()
        {
            try
            {
                // the postgres image's entrypoint prepends "postgres" when the first
                // command argument starts with '-', so this is `docker run postgres:18 -c ...`
                var builder = new PostgreSqlBuilder("postgres:18")
                    .WithCommand("-c", "max_locks_per_transaction=4096");

                var dockerEndpoint = ResolveDockerEndpoint();

                if (dockerEndpoint != null)
                {
                    builder = builder.WithDockerEndpoint(dockerEndpoint);
                }

                this.container = builder.Build();

                await this.container.StartAsync();
            }
            catch (Exception exception)
            {
                Assert.Ignore($"Docker is not available for the SQL-schema integration tests: {exception.Message}");
            }

            this.ConnectionString = this.container.GetConnectionString();
        }

        /// <summary>
        /// Resolves the Docker endpoint when Testcontainers' own probe would miss it: an explicit
        /// DOCKER_HOST always wins; on Windows, Docker Desktop 4.x exposes the
        /// dockerDesktopLinuxEngine named pipe instead of the legacy docker_engine one the default
        /// probe targets. Returns null to let Testcontainers use its own resolution strategies.
        /// </summary>
        /// <returns>
        /// The endpoint to pass to WithDockerEndpoint, or null
        /// </returns>
        private static string ResolveDockerEndpoint()
        {
            var configuredHost = Environment.GetEnvironmentVariable("DOCKER_HOST");

            if (!string.IsNullOrWhiteSpace(configuredHost))
            {
                return configuredHost;
            }

            if (OperatingSystem.IsWindows() && File.Exists(@"\\.\pipe\dockerDesktopLinuxEngine"))
            {
                return "npipe://./pipe/dockerDesktopLinuxEngine";
            }

            return null;
        }

        /// <summary>
        /// Executes a multi-statement SQL script (dollar-quoted DO/CREATE FUNCTION bodies included)
        /// as a single command
        /// </summary>
        /// <param name="sql">
        /// The script text
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        public async Task ExecuteScriptAsync(string sql)
        {
            await using var connection = new NpgsqlConnection(this.ConnectionString);
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(sql, connection);
            command.CommandTimeout = 600;
            await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Executes a multi-statement SQL script and collects the RAISE NOTICE messages it emits —
        /// the PASS/FAIL channel of schema.smoke.sql and schema.concurrency.verify.sql
        /// </summary>
        /// <param name="sql">
        /// The script text
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" /> carrying the notice messages in emission order
        /// </returns>
        public async Task<IReadOnlyList<string>> ExecuteScriptCollectingNoticesAsync(string sql)
        {
            var notices = new List<string>();

            await using var connection = new NpgsqlConnection(this.ConnectionString);
            connection.Notice += (_, noticeArguments) => notices.Add(noticeArguments.Notice.MessageText);
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(sql, connection);
            command.CommandTimeout = 600;
            await command.ExecuteNonQueryAsync();

            return notices;
        }

        /// <summary>
        /// Disposes the container
        /// </summary>
        /// <returns>
        /// an awaitable <see cref="ValueTask" />
        /// </returns>
        public async ValueTask DisposeAsync()
        {
            if (this.container != null)
            {
                await this.container.DisposeAsync();
            }
        }
    }
}
