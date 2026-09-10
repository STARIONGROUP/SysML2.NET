// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiEnumProviderGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.OpenApi;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.OpenApiHandleBarsGenerators;

    [TestFixture]
    public class OpenApiEnumProviderGeneratorTestFixture
    {
        private OpenApiDocument openApiDocument;
        private DirectoryInfo outputDirectory;
        private OpenApiEnumProviderGenerator openApiEnumProviderGenerator;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.openApiDocument = await OpenApiDocumentLoader.LoadAsync();

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.outputDirectory = directoryInfo.CreateSubdirectory(Path.Combine("OpenAPI", "_SysML2.NET.PSM.AutoGenEnumProvider"));
            this.openApiEnumProviderGenerator = new OpenApiEnumProviderGenerator();
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            Assert.That(() => this.openApiEnumProviderGenerator.GenerateAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => this.openApiEnumProviderGenerator.GenerateAsync(this.openApiDocument, null), Throws.TypeOf<ArgumentNullException>());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => this.openApiEnumProviderGenerator.GenerateEnumerationProviderAsync(null, this.outputDirectory, "CompositeConstraintOperator"), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.openApiEnumProviderGenerator.GenerateEnumerationProviderAsync(this.openApiDocument, null, "CompositeConstraintOperator"), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => this.openApiEnumProviderGenerator.GenerateEnumerationProviderAsync(this.openApiDocument, this.outputDirectory, " "), Throws.TypeOf<ArgumentException>());
            }

            await Assert.ThatAsync(() => this.openApiEnumProviderGenerator.GenerateAsync(this.openApiDocument, this.outputDirectory), Throws.Nothing);

            Assert.That(this.outputDirectory.GetFiles("*Provider.cs"), Has.Length.EqualTo(4));
        }

        [Test]
        [Category("Expected")]
        public async Task VerifyGenerateEnumerationProviderAsync([Values("CompositeConstraintOperator", "CompositeConstraintRequestOperator",
            "PrimitiveConstraintOperator", "PrimitiveConstraintRequestOperator")] string enumerationName)
        {
            var generatedCode = await this.openApiEnumProviderGenerator.GenerateEnumerationProviderAsync(this.openApiDocument, this.outputDirectory, enumerationName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/OpenAPI/PSM/AutoGenEnumProvider/{enumerationName}Provider.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
