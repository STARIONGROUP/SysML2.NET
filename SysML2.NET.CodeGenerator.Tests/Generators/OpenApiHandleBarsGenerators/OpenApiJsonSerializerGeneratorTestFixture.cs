// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiJsonSerializerGeneratorTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Generators.OpenApiHandleBarsGenerators;

    [TestFixture]
    public class OpenApiJsonSerializerGeneratorTestFixture
    {
        private OpenApiDocument openApiDocument;
        private DirectoryInfo outputDirectory;
        private OpenApiJsonSerializerGenerator openApiJsonSerializerGenerator;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.openApiDocument = await OpenApiDocumentLoader.LoadAsync();

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.outputDirectory = directoryInfo.CreateSubdirectory(Path.Combine("OpenAPI", "_SysML2.NET.PSM.AutoGenSerializer"));
            this.openApiJsonSerializerGenerator = new OpenApiJsonSerializerGenerator();
        }

        private static IEnumerable<string> InterestingSchemas()
        {
            var openApiDocument = OpenApiDocumentLoader.LoadAsync().GetAwaiter().GetResult();

            var serializableSchemas = OpenApiJsonSerializerGenerator.QuerySerializableSchemas(openApiDocument)
                .Select(schema => schema.Key)
                .ToHashSet(StringComparer.Ordinal);

            return OpenApiSchemaInspector.QueryInterestingSchemas(OpenApiHandleBarsGenerator.QuerySchemas(openApiDocument))
                .Where(serializableSchemas.Contains);
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            await Assert.ThatAsync(() => this.openApiJsonSerializerGenerator.GenerateAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.openApiJsonSerializerGenerator.GenerateAsync(this.openApiDocument, null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => OpenApiJsonSerializerGenerator.QuerySerializableSchemas(null), Throws.TypeOf<ArgumentNullException>());

            await Assert.ThatAsync(() => this.openApiJsonSerializerGenerator.GenerateAsync(this.openApiDocument, this.outputDirectory), Throws.Nothing);

            var serializableSchemas = OpenApiJsonSerializerGenerator.QuerySerializableSchemas(this.openApiDocument);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(serializableSchemas.Select(schema => schema.Key), Does.Not.Contain("Data"));
                Assert.That(serializableSchemas.Select(schema => schema.Key), Does.Contain("PrimitiveConstraint"));
                Assert.That(this.outputDirectory.GetFiles("SerializationProvider.cs"), Has.Length.EqualTo(1));
            }
        }

        [Test]
        [TestCaseSource(nameof(InterestingSchemas))]
        [Category("Expected")]
        public async Task VerifyGenerateSerializerAsync(string schemaName)
        {
            var generatedCode = await this.openApiJsonSerializerGenerator.GenerateSerializerAsync(this.openApiDocument, this.outputDirectory, schemaName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/OpenAPI/PSM/AutoGenSerializer/{schemaName}Serializer.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
