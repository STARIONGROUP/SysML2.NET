// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiApiClientGeneratorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Generators.OpenApiHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.OpenApi;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.OpenApiHandleBarsGenerators;

    [TestFixture]
    public class OpenApiApiClientGeneratorTestFixture
    {
        private OpenApiDocument openApiDocument;
        private DirectoryInfo outputDirectory;
        private OpenApiApiClientGenerator openApiApiClientGenerator;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.openApiDocument = await OpenApiDocumentLoader.LoadAsync();

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.outputDirectory = directoryInfo.CreateSubdirectory(Path.Combine("OpenAPI", "_SySML2.NET.REST.AutoGenPsmApiClient"));
            this.openApiApiClientGenerator = new OpenApiApiClientGenerator();
        }

        private static IEnumerable<string> Tags()
        {
            var openApiDocument = OpenApiDocumentLoader.LoadAsync().GetAwaiter().GetResult();

            return OpenApiApiClientGenerator.QueryTagModels(openApiDocument).Select(tagModel => tagModel.Tag);
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            await Assert.ThatAsync(() => this.openApiApiClientGenerator.GenerateAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.openApiApiClientGenerator.GenerateAsync(this.openApiDocument, null), Throws.TypeOf<ArgumentNullException>());

            await Assert.ThatAsync(() => this.openApiApiClientGenerator.GenerateAsync(this.openApiDocument, this.outputDirectory), Throws.Nothing);

            var generatedFileNames = this.outputDirectory.GetFiles("*.cs").Select(file => file.Name);

            Assert.That(generatedFileNames, Is.EquivalentTo(["IPsmApiClient.cs", "ProjectApi.cs", "BranchApi.cs", "TagApi.cs", "CommitApi.cs", "ElementApi.cs", "RelationshipApi.cs", "DiffMergeApi.cs", "QueryApi.cs", "MetaApi.cs"]));
        }

        [Test]
        public void VerifyQueryTagModels()
        {
            Assert.That(() => OpenApiApiClientGenerator.QueryTagModels(null), Throws.TypeOf<ArgumentNullException>());

            var tagModels = OpenApiApiClientGenerator.QueryTagModels(this.openApiDocument);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(tagModels, Has.Count.EqualTo(9));
                Assert.That(tagModels.Select(tagModel => tagModel.Name), Is.EqualTo(["Project", "Branch", "Tag", "Commit", "Element", "Relationship", "DiffMerge", "Query", "Meta"]));
                Assert.That(tagModels.SelectMany(tagModel => tagModel.Operations).ToList(), Has.Count.EqualTo(35));
                Assert.That(tagModels[6].Tag, Is.EqualTo("Diff & Merge"));
                Assert.That(tagModels[6].Operations.Select(operation => operation.HttpMethod), Is.EqualTo(["GET", "POST"]));
            }
        }

        [Test]
        [TestCaseSource(nameof(Tags))]
        [Category("Expected")]
        public async Task VerifyGenerateApiClientPartialAsync(string tag)
        {
            await Assert.ThatAsync(() => this.openApiApiClientGenerator.GenerateApiClientPartialAsync(this.openApiDocument, this.outputDirectory, "Unknown"), Throws.TypeOf<InvalidOperationException>());

            var tagModel = OpenApiApiClientGenerator.QueryTagModels(this.openApiDocument).Single(candidate => candidate.Tag == tag);

            var generatedCode = await this.openApiApiClientGenerator.GenerateApiClientPartialAsync(this.openApiDocument, this.outputDirectory, tag);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/OpenAPI/REST/AutoGenPsmApiClient/{tagModel.Name}Api.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }

        [Test]
        [Category("Expected")]
        public async Task VerifyGenerateApiClientInterfaceAsync()
        {
            await Assert.ThatAsync(() => this.openApiApiClientGenerator.GenerateApiClientInterfaceAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());

            var generatedCode = await this.openApiApiClientGenerator.GenerateApiClientInterfaceAsync(this.openApiDocument, this.outputDirectory);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                "Expected/OpenAPI/REST/AutoGenPsmApiClient/IPsmApiClient.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
