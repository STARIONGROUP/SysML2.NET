// -------------------------------------------------------------------------------------------------
// <copyright file="SqlSchemaSmokeTestFixture.cs" company="Starion Group S.A.">
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
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;

    /// <summary>
    /// Runs SysML2.NET.CodeGenerator/Sql/schema.smoke.sql against the LIVE generator output — the
    /// schema is generated from the UML model in-process and installed on a PostgreSQL 18
    /// Testcontainer, so this fixture also catches registry/template drift the string assertions
    /// of <see cref="SQLSchemaGeneratorTestFixture" /> cannot.
    /// </summary>
    [TestFixture]
    [Category("Integration")]
    public class SqlSchemaSmokeTestFixture
    {
        private PostgreSqlSchemaTestHost host;
        private string smokeScriptPath;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.smokeScriptPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Sql", "schema.smoke.sql");

            this.host = new PostgreSqlSchemaTestHost();
            await this.host.StartAsync();

            var outputDirectory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory)
                .CreateSubdirectory(Path.Combine("UML", "_SysML2.NET.SqlSmoke"));

            var generatedSchema = await new SQLSchemaGenerator()
                .GenerateSqlSchemaAsync(GeneratorSetupFixture.XmiReaderResult, outputDirectory);

            await this.host.ExecuteScriptAsync(generatedSchema);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await this.host.DisposeAsync();
        }

        [Test]
        public async Task Verify_that_smoke_test_passes_against_generated_schema()
        {
            var smokeScript = await File.ReadAllTextAsync(this.smokeScriptPath);

            // self-calibrating: the script itself declares how many PASS notices it must emit,
            // so extending the smoke test never requires touching this fixture
            var expectedPassCount = Regex.Matches(smokeScript, "RAISE NOTICE 'PASS").Count;

            var notices = await this.host.ExecuteScriptCollectingNoticesAsync(smokeScript);
            var passNotices = notices.Where(notice => notice.StartsWith("PASS", StringComparison.Ordinal)).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(expectedPassCount, Is.GreaterThan(0), "the smoke script must declare PASS notices");
                Assert.That(notices.Where(notice => notice.Contains("FAIL")), Is.Empty);
                Assert.That(passNotices, Has.Count.EqualTo(expectedPassCount));
                Assert.That(passNotices, Has.Some.StartWith("PASS 2a"), "the derived-state axiom assertion must be present");
                Assert.That(passNotices, Has.Some.StartWith("PASS 11a"), "the multi-version registry assertion must be present");
                Assert.That(passNotices, Has.Some.StartWith("PASS 13c"), "the incremental reference-validation assertion must be present");
            }
        }
    }
}
