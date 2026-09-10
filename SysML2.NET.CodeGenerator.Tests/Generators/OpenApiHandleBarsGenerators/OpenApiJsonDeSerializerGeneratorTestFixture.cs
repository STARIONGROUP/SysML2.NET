// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiJsonDeSerializerGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class OpenApiJsonDeSerializerGeneratorTestFixture
    {
        private OpenApiDocument openApiDocument;
        private DirectoryInfo outputDirectory;
        private OpenApiJsonDeSerializerGenerator openApiJsonDeSerializerGenerator;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.openApiDocument = await OpenApiDocumentLoader.LoadAsync();

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.outputDirectory = directoryInfo.CreateSubdirectory(Path.Combine("OpenAPI", "_SysML2.NET.PSM.AutoGenDeSerializer"));
            this.openApiJsonDeSerializerGenerator = new OpenApiJsonDeSerializerGenerator();
        }

        private static IEnumerable<string> InterestingSchemas()
        {
            var openApiDocument = OpenApiDocumentLoader.LoadAsync().GetAwaiter().GetResult();

            var deSerializableSchemas = OpenApiJsonDeSerializerGenerator.QueryDeSerializableSchemas(openApiDocument)
                .Select(schema => schema.Key)
                .ToHashSet(StringComparer.Ordinal);

            return OpenApiSchemaInspector.QueryInterestingSchemas(OpenApiHandleBarsGenerator.QuerySchemas(openApiDocument))
                .Where(deSerializableSchemas.Contains);
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            await Assert.ThatAsync(() => this.openApiJsonDeSerializerGenerator.GenerateAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.openApiJsonDeSerializerGenerator.GenerateAsync(this.openApiDocument, null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => OpenApiJsonDeSerializerGenerator.QueryDeSerializableSchemas(null), Throws.TypeOf<ArgumentNullException>());

            await Assert.ThatAsync(() => this.openApiJsonDeSerializerGenerator.GenerateAsync(this.openApiDocument, this.outputDirectory), Throws.Nothing);

            var deSerializableSchemas = OpenApiJsonDeSerializerGenerator.QueryDeSerializableSchemas(this.openApiDocument);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(deSerializableSchemas.Select(schema => schema.Key), Does.Not.Contain("Data"));
                Assert.That(deSerializableSchemas.Select(schema => schema.Key), Does.Contain("PrimitiveConstraint"));
                Assert.That(this.outputDirectory.GetFiles("RequestDeSerializationProvider.cs"), Has.Length.EqualTo(1));
                Assert.That(this.outputDirectory.GetFiles("ResponseDeSerializationProvider.cs"), Has.Length.EqualTo(1));
            }
        }

        [Test]
        [TestCaseSource(nameof(InterestingSchemas))]
        [Category("Expected")]
        public async Task VerifyGenerateDeSerializerAsync(string schemaName)
        {
            var generatedCode = await this.openApiJsonDeSerializerGenerator.GenerateDeSerializerAsync(this.openApiDocument, this.outputDirectory, schemaName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/OpenAPI/PSM/AutoGenDeSerializer/{schemaName}DeSerializer.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
