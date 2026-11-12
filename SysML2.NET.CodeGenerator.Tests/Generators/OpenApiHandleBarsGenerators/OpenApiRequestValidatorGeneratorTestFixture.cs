// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiRequestValidatorGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class OpenApiRequestValidatorGeneratorTestFixture
    {
        private OpenApiDocument openApiDocument;
        private DirectoryInfo outputDirectory;
        private OpenApiRequestValidatorGenerator openApiRequestValidatorGenerator;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.openApiDocument = await OpenApiDocumentLoader.LoadAsync();

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.outputDirectory = directoryInfo.CreateSubdirectory(Path.Combine("OpenAPI", "_SysML2.NET.Validation.PSM"));
            this.openApiRequestValidatorGenerator = new OpenApiRequestValidatorGenerator();
        }

        private static IEnumerable<string> ValidatedSchemas()
        {
            var openApiDocument = OpenApiDocumentLoader.LoadAsync().GetAwaiter().GetResult();

            return OpenApiRequestValidatorGenerator.QueryValidatedSchemas(openApiDocument).Select(schema => schema.Key);
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            await Assert.ThatAsync(() => this.openApiRequestValidatorGenerator.GenerateAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.openApiRequestValidatorGenerator.GenerateAsync(this.openApiDocument, null), Throws.TypeOf<ArgumentNullException>());

            await Assert.ThatAsync(() => this.openApiRequestValidatorGenerator.GenerateAsync(this.openApiDocument, this.outputDirectory), Throws.Nothing);
        }

        [Test]
        public void VerifyQueryValidatedSchemas()
        {
            Assert.That(() => OpenApiRequestValidatorGenerator.QueryValidatedSchemas(null), Throws.TypeOf<ArgumentNullException>());

            var schemaNames = OpenApiRequestValidatorGenerator.QueryValidatedSchemas(this.openApiDocument).Select(schema => schema.Key).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(schemaNames, Has.Count.EqualTo(12));
                Assert.That(schemaNames, Does.Contain("ProjectRequest"));
                Assert.That(schemaNames, Does.Not.Contain("ConstraintRequest"));
                Assert.That(schemaNames, Does.Not.Contain("DataRequest"));
                Assert.That(schemaNames, Does.Not.Contain("Project"));
                Assert.That(schemaNames, Does.Not.Contain("PartUsageRequest"));
            }
        }

        [Test]
        [TestCaseSource(nameof(ValidatedSchemas))]
        [Category("Expected")]
        public async Task VerifyGenerateRequestValidatorAsync(string schemaName)
        {
            await Assert.ThatAsync(() => this.openApiRequestValidatorGenerator.GenerateRequestValidatorAsync(null, this.outputDirectory, schemaName), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.openApiRequestValidatorGenerator.GenerateRequestValidatorAsync(this.openApiDocument, null, schemaName), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.openApiRequestValidatorGenerator.GenerateRequestValidatorAsync(this.openApiDocument, this.outputDirectory, " "), Throws.TypeOf<ArgumentException>());

            var generatedCode = await this.openApiRequestValidatorGenerator.GenerateRequestValidatorAsync(this.openApiDocument, this.outputDirectory, schemaName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/OpenAPI/Validation/AutoGenValidator/{schemaName}Validator.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
