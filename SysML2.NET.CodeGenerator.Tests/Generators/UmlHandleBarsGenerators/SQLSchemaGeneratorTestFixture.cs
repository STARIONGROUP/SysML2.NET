// -------------------------------------------------------------------------------------------------
// <copyright file="SQLSchemaGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using System.IO;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;

    [TestFixture]
    public class SQLSchemaGeneratorTestFixture
    {
        private DirectoryInfo sqlSchemaDirectoryInfo;
        private SQLSchemaGenerator sqlSchemaGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Sql");

            this.sqlSchemaDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.sqlSchemaGenerator = new SQLSchemaGenerator();
        }

        [Test]
        public async Task Verify_that_sql_schema_is_generated()
        {
            var generatedSchema = await this.sqlSchemaGenerator.GenerateSqlSchemaAsync(GeneratorSetupFixture.XmiReaderResult, this.sqlSchemaDirectoryInfo);

            Assert.That(generatedSchema, Is.Not.Null.And.Not.Empty);

            Assert.That(generatedSchema, Does.Contain("CREATE TABLE sysml2.element_version"));
            Assert.That(generatedSchema, Does.Contain("CREATE TABLE sysml2.derived_version"));
            Assert.That(generatedSchema, Does.Contain("CREATE TYPE sysml2.visibility_kind AS ENUM ('private', 'protected', 'public');"));
            Assert.That(generatedSchema, Does.Contain("CREATE TABLE sysml2.feature_v"));
            Assert.That(generatedSchema, Does.Contain("CREATE TABLE sysml2.element_owned_relationship"));
            Assert.That(generatedSchema, Does.Contain("CREATE VIEW sysml2.vw_part_usage"));
            Assert.That(generatedSchema, Does.Contain("CREATE TABLE sysml2.model_version"));
            Assert.That(generatedSchema, Does.Contain("INSERT INTO sysml2.model_version (id, name, source_fingerprint) VALUES"));
            Assert.That(generatedSchema, Does.Contain("INSERT INTO sysml2.class_kind (id, name, is_abstract, introduced_in, removed_in) VALUES"));
            Assert.That(generatedSchema, Does.Contain("ON CONFLICT (id) DO NOTHING;"), "the registry seeds must be idempotent");
            Assert.That(generatedSchema, Does.Contain("(120, 'PartUsage', false, 1, NULL)"), "class_kind ids must come from the frozen registry");
            Assert.That(generatedSchema, Does.Contain("model_version_id smallint   NOT NULL REFERENCES sysml2.model_version (id)"));
            Assert.That(generatedSchema, Does.Contain("FOREIGN KEY (identity_id, class_kind) REFERENCES sysml2.data_identity (id, class_kind)"), "the typed-identity composite FK must anchor every version");
            Assert.That(generatedSchema, Does.Contain("CREATE TRIGGER trg_commit_immutable"), "commits are immutable per Clause 7.1.2 - the DAG acyclicity proof and the fold rely on commit.created being frozen");
            Assert.That(generatedSchema, Does.Contain("CREATE TABLE sysml2.query"), "the stored Query record (Clause 7) must have persistence backing the /queries routes");
            Assert.That(generatedSchema, Does.Contain("ON sysml2.branch (project_id, name) WHERE deleted IS NULL"), "ref names must be unique among LIVE refs only - a plain UNIQUE would block name reuse after the spec's recorded-event soft delete");
            Assert.That(generatedSchema, Does.Contain("CREATE OR REPLACE FUNCTION sysml2.validate_references_at_commit("));
            Assert.That(generatedSchema, Does.Contain("CREATE OR REPLACE FUNCTION sysml2.validate_references_in_commit("));
            Assert.That(generatedSchema, Does.Contain("ANALYZE validation_snapshot;"), "the full pass must feed the planner true snapshot cardinality");
            Assert.That(generatedSchema, Does.Contain("'wrong-type'"), "the reference validation must type-check via the typed identity");
            Assert.That(generatedSchema, Does.Not.Contain("CREATE TABLE sysml2.property_catalog"), "the property->storage routing lives in generated C#, not in the database");
            Assert.That(generatedSchema, Does.Not.Contain("CREATE TABLE sysml2.class_kind_table"), "subtype-table participation lives in generated C#, not in the database");
            Assert.That(generatedSchema, Does.Not.Contain("{{"), "no unresolved handlebars expressions may survive generation");
        }
    }
}
