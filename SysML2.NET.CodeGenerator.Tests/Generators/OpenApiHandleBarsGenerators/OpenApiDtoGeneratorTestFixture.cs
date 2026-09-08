// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiDtoGeneratorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
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
    public class OpenApiDtoGeneratorTestFixture
    {
        private OpenApiDocument openApiDocument;
        private DirectoryInfo outputDirectory;
        private OpenApiDtoGenerator openApiDtoGenerator;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.openApiDocument = await OpenApiDocumentLoader.LoadAsync();

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.outputDirectory = directoryInfo.CreateSubdirectory(Path.Combine("OpenAPI", "_SysML2.NET.PSM.DTO"));
            this.openApiDtoGenerator = new OpenApiDtoGenerator();
        }

        private static IEnumerable<string> InterestingSchemas()
        {
            var openApiDocument = OpenApiDocumentLoader.LoadAsync().GetAwaiter().GetResult();

            return OpenApiSchemaInspector.QueryInterestingSchemas(OpenApiHandleBarsGenerator.QuerySchemas(openApiDocument));
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            await Assert.ThatAsync(() => this.openApiDtoGenerator.GenerateAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.openApiDtoGenerator.GenerateAsync(this.openApiDocument, null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => OpenApiHandleBarsGenerator.QuerySchemas(null), Throws.TypeOf<ArgumentNullException>());

            await Assert.ThatAsync(() => this.openApiDtoGenerator.GenerateAsync(this.openApiDocument, this.outputDirectory), Throws.Nothing);
        }

        [Test]
        [TestCaseSource(nameof(InterestingSchemas))]
        [Category("Expected")]
        public async Task VerifyGenerateDataTransferObjectAsync(string schemaName)
        {
            var schema = OpenApiHandleBarsGenerator.QuerySchemas(this.openApiDocument).Single(candidate => candidate.Key == schemaName);

            var isUnion = schema.Value.QueryIsUnion();

            var generatedCode = isUnion
                ? await this.openApiDtoGenerator.GenerateDataTransferObjectInterfaceAsync(this.openApiDocument, this.outputDirectory, schemaName)
                : await this.openApiDtoGenerator.GenerateDataTransferObjectClassAsync(this.openApiDocument, this.outputDirectory, schemaName);

            var fileName = isUnion ? OpenApiSchemaExtensions.QueryInterfaceName(schemaName) : schemaName;

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/OpenAPI/PSM/AutoGenDto/{fileName}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
