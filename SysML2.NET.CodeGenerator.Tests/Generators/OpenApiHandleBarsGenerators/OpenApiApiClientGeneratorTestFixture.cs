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
    using System.Net.Http;
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

            var validOperation = new OpenApiOperation
            {
                OperationId = "getThings",
                Parameters = [CreateArrayParameter(ParameterStyle.Form, false)],
                Responses = new OpenApiResponses { ["200"] = CreateJsonResponse() }
            };

            Assert.That(OpenApiApiClientGenerator.QueryTagModels(CreateDocument(validOperation)), Has.Count.EqualTo(1));

            var twoSuccessResponses = new OpenApiOperation
            {
                OperationId = "getThings",
                Responses = new OpenApiResponses { ["200"] = CreateJsonResponse(), ["201"] = CreateJsonResponse() }
            };

            Assert.That(() => OpenApiApiClientGenerator.QueryTagModels(CreateDocument(twoSuccessResponses)), Throws.TypeOf<InvalidOperationException>());

            var explodedArray = new OpenApiOperation
            {
                OperationId = "getThings",
                Parameters = [CreateArrayParameter(ParameterStyle.Form, true)],
                Responses = new OpenApiResponses { ["200"] = CreateJsonResponse() }
            };

            Assert.That(() => OpenApiApiClientGenerator.QueryTagModels(CreateDocument(explodedArray)), Throws.TypeOf<InvalidOperationException>());

            var pipeDelimitedArray = new OpenApiOperation
            {
                OperationId = "getThings",
                Parameters = [CreateArrayParameter(ParameterStyle.PipeDelimited, false)],
                Responses = new OpenApiResponses { ["200"] = CreateJsonResponse() }
            };

            Assert.That(() => OpenApiApiClientGenerator.QueryTagModels(CreateDocument(pipeDelimitedArray)), Throws.TypeOf<InvalidOperationException>());
        }

        private static OpenApiDocument CreateDocument(OpenApiOperation operation)
        {
            var document = new OpenApiDocument
            {
                Tags = new HashSet<OpenApiTag> { new() { Name = "Things" } },
                Paths = []
            };

            operation.Tags = new HashSet<OpenApiTagReference> { new("Things", document) };
            document.Paths["/things"] = new OpenApiPathItem { Operations = new Dictionary<HttpMethod, OpenApiOperation> { [HttpMethod.Get] = operation } };

            return document;
        }

        private static OpenApiParameter CreateArrayParameter(ParameterStyle style, bool explode)
        {
            return new OpenApiParameter
            {
                Name = "thingId",
                In = ParameterLocation.Query,
                Style = style,
                Explode = explode,
                Schema = new OpenApiSchema { Type = JsonSchemaType.Array, Items = new OpenApiSchema { Type = JsonSchemaType.String } }
            };
        }

        private static OpenApiResponse CreateJsonResponse()
        {
            return new OpenApiResponse { Content = new Dictionary<string, IOpenApiMediaType> { ["application/json"] = new OpenApiMediaType() } };
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
