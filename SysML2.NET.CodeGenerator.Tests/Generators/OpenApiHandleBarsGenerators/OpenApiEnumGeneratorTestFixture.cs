// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiEnumGeneratorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Generators.OpenApiHandleBarsGenerators
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.OpenApi;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.OpenApiHandleBarsGenerators;

    [TestFixture]
    public class OpenApiEnumGeneratorTestFixture
    {
        private OpenApiDocument openApiDocument;
        private DirectoryInfo outputDirectory;
        private OpenApiEnumGenerator openApiEnumGenerator;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.openApiDocument = await OpenApiDocumentLoader.LoadAsync();

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.outputDirectory = directoryInfo.CreateSubdirectory(Path.Combine("OpenAPI", "_SysML2.NET.PSM.AutoGenEnum"));
            this.openApiEnumGenerator = new OpenApiEnumGenerator();
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            Assert.That(() => this.openApiEnumGenerator.GenerateAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => this.openApiEnumGenerator.GenerateAsync(this.openApiDocument, null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => OpenApiEnumGenerator.QueryEnumerations(null), Throws.TypeOf<ArgumentNullException>());

            await Assert.ThatAsync(() => this.openApiEnumGenerator.GenerateAsync(this.openApiDocument, this.outputDirectory), Throws.Nothing);

            var enumerationNames = OpenApiEnumGenerator.QueryEnumerations(this.openApiDocument).Select(enumeration => enumeration.Key);

            Assert.That(enumerationNames, Is.EqualTo([
                "CompositeConstraintOperator",
                "CompositeConstraintRequestOperator",
                "PrimitiveConstraintOperator",
                "PrimitiveConstraintRequestOperator"
            ]));
        }

        [Test]
        [Category("Expected")]
        public async Task VerifyGenerateEnumerationAsync([Values("CompositeConstraintOperator", "CompositeConstraintRequestOperator",
            "PrimitiveConstraintOperator", "PrimitiveConstraintRequestOperator")] string enumerationName)
        {
            var generatedCode = await this.openApiEnumGenerator.GenerateEnumerationAsync(this.openApiDocument, this.outputDirectory, enumerationName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/OpenAPI/PSM/AutoGenEnum/{enumerationName}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
