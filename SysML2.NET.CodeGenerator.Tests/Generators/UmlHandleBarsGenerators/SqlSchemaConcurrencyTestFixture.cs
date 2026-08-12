// -------------------------------------------------------------------------------------------------
// <copyright file="SqlSchemaConcurrencyTestFixture.cs" company="Starion Group S.A.">
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
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Npgsql;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;

    /// <summary>
    /// The .NET form of the multi-user suite (SysML2.NET.CodeGenerator/Sql/schema.concurrency.*):
    /// parallel writers race the normative §18.2 compare-and-swap commit protocol through
    /// sysml2.bench_try_commit against the LIVE generated schema on a PostgreSQL 18 Testcontainer,
    /// and every scenario ends by running the C1–C5 invariant verifier. The pgbench scripts'
    /// \set loops are replaced by Tasks; the SQL under test is identical.
    /// </summary>
    [TestFixture]
    [Category("Integration")]
    public class SqlSchemaConcurrencyTestFixture
    {
        private const int WriterCount = 16;
        private const int AttemptsPerWriter = 250;
        private const int ReaderCount = 8;
        private const int ReadsPerReader = 200;

        private PostgreSqlSchemaTestHost host;
        private string verifyScript;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var scriptDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "Sql");
            var setupScript = await File.ReadAllTextAsync(Path.Combine(scriptDirectory, "schema.concurrency.setup.sql"));
            this.verifyScript = await File.ReadAllTextAsync(Path.Combine(scriptDirectory, "schema.concurrency.verify.sql"));

            this.host = new PostgreSqlSchemaTestHost();
            await this.host.StartAsync();

            var outputDirectory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory)
                .CreateSubdirectory(Path.Combine("UML", "_SysML2.NET.SqlConcurrency"));

            var generatedSchema = await new SQLSchemaGenerator()
                .GenerateSqlSchemaAsync(GeneratorSetupFixture.XmiReaderResult, outputDirectory);

            await this.host.ExecuteScriptAsync(generatedSchema);
            await this.host.ExecuteScriptAsync(setupScript);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await this.host.DisposeAsync();
        }

        [Test]
        public async Task HotBranchRace_SixteenWriters_ExactlyOneWinnerPerHead()
        {
            var results = await Task.WhenAll(Enumerable.Range(0, WriterCount)
                .Select(_ => Task.Run(() => this.RunWriterAsync(1, AttemptsPerWriter))));

            var wins = results.Sum(result => result.Wins);
            var losses = results.Sum(result => result.Losses);

            TestContext.Out.WriteLine($"hot branch: {wins} wins, {losses} CAS losses out of {WriterCount * AttemptsPerWriter} attempts");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(wins + losses, Is.EqualTo(WriterCount * AttemptsPerWriter));
                Assert.That(wins, Is.GreaterThan(0));
                Assert.That(losses, Is.GreaterThan(0), "a 16-writer race on one branch without a single CAS conflict means the CAS was not contended at all");
            }

            await this.AssertInvariantsHoldAsync();
        }

        [Test]
        public async Task SpreadBranches_OneWriterPerBranch_NoConflicts()
        {
            var results = await Task.WhenAll(Enumerable.Range(0, WriterCount)
                .Select(writerIndex => Task.Run(() => this.RunWriterAsync(writerIndex + 1, AttemptsPerWriter))));

            var wins = results.Sum(result => result.Wins);
            var losses = results.Sum(result => result.Losses);

            TestContext.Out.WriteLine($"spread: {wins} wins, {losses} CAS losses out of {WriterCount * AttemptsPerWriter} attempts");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(wins, Is.EqualTo(WriterCount * AttemptsPerWriter), "an exclusive writer per branch must win every attempt — contention is branch-local");
                Assert.That(losses, Is.Zero);
            }

            await this.AssertInvariantsHoldAsync();
        }

        [Test]
        public async Task ReadsDuringWriteStorm_ReadersNeverBlocked()
        {
            var writerTasks = Enumerable.Range(0, WriterCount)
                .Select(_ => Task.Run(() => this.RunWriterAsync(1, AttemptsPerWriter)))
                .ToList();

            var readerResults = await Task.WhenAll(Enumerable.Range(0, ReaderCount)
                .Select(_ => Task.Run(() => this.RunReaderAsync(ReadsPerReader))));

            await Task.WhenAll(writerTasks);

            var reads = readerResults.Sum(result => result.Reads);
            var nonNullReads = readerResults.Sum(result => result.NonNullReads);
            var averageLatency = readerResults.Average(result => result.AverageLatencyMilliseconds);

            // reported, deliberately not asserted: latency thresholds are flaky across machines
            TestContext.Out.WriteLine($"reads under write storm: {reads} reads, average latency {averageLatency:F2} ms");

            Assert.That(nonNullReads, Is.EqualTo(reads), "every seeded element must resolve to a payload while the write storm runs");

            await this.AssertInvariantsHoldAsync();
        }

        /// <summary>
        /// Runs one writer: a private connection issuing the given number of commit-protocol
        /// attempts against the branch, tallying CAS wins and losses from the function's result
        /// </summary>
        /// <param name="branchIndex">
        /// The 1-based bench branch index
        /// </param>
        /// <param name="attempts">
        /// The number of attempts to issue
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" /> carrying the win/loss tally
        /// </returns>
        private async Task<(int Wins, int Losses)> RunWriterAsync(int branchIndex, int attempts)
        {
            var wins = 0;
            var losses = 0;

            await using var connection = new NpgsqlConnection(this.host.ConnectionString);
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand("SELECT sysml2.bench_try_commit($1, $2)", connection);
            var branchParameter = new NpgsqlParameter<int> { TypedValue = branchIndex };
            var seedParameter = new NpgsqlParameter<long> { TypedValue = 0 };
            command.Parameters.Add(branchParameter);
            command.Parameters.Add(seedParameter);
            await command.PrepareAsync();

            for (var attemptIndex = 0; attemptIndex < attempts; attemptIndex++)
            {
                seedParameter.TypedValue = Random.Shared.NextInt64(1, 1_000_000_000);

                if ((bool)await command.ExecuteScalarAsync())
                {
                    wins++;
                }
                else
                {
                    losses++;
                }
            }

            return (wins, losses);
        }

        /// <summary>
        /// Runs one reader: a private connection issuing branch-head element reads on the hot
        /// branch, tallying successful non-null payloads and the average latency
        /// </summary>
        /// <param name="reads">
        /// The number of reads to issue
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task" /> carrying the read tally and average latency
        /// </returns>
        private async Task<(int Reads, int NonNullReads, double AverageLatencyMilliseconds)> RunReaderAsync(int reads)
        {
            var nonNullReads = 0;

            await using var connection = new NpgsqlConnection(this.host.ConnectionString);
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand("SELECT sysml2.bench_read($1)", connection);
            var indexParameter = new NpgsqlParameter<int> { TypedValue = 0 };
            command.Parameters.Add(indexParameter);
            await command.PrepareAsync();

            var stopwatch = Stopwatch.StartNew();

            for (var readIndex = 0; readIndex < reads; readIndex++)
            {
                indexParameter.TypedValue = Random.Shared.Next(0, 1000);

                if (await command.ExecuteScalarAsync() is string)
                {
                    nonNullReads++;
                }
            }

            stopwatch.Stop();

            return (reads, nonNullReads, stopwatch.Elapsed.TotalMilliseconds / reads);
        }

        /// <summary>
        /// Runs schema.concurrency.verify.sql (invariants C1–C5) and asserts every declared
        /// invariant PASSes — the count is read from the script itself, so extending the verifier
        /// never requires touching this fixture
        /// </summary>
        /// <returns>
        /// an awaitable <see cref="Task" />
        /// </returns>
        private async Task AssertInvariantsHoldAsync()
        {
            var expectedPassCount = Regex.Matches(this.verifyScript, "RAISE NOTICE 'PASS").Count;

            var notices = await this.host.ExecuteScriptCollectingNoticesAsync(this.verifyScript);
            var passNotices = notices.Where(notice => notice.StartsWith("PASS", StringComparison.Ordinal)).ToList();

            TestContext.Out.WriteLine(string.Join(Environment.NewLine, passNotices));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(notices.Where(notice => notice.Contains("FAIL")), Is.Empty);
                Assert.That(passNotices, Has.Count.EqualTo(expectedPassCount));
            }
        }
    }
}
