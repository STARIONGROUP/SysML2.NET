// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCorePocoValidationGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCorePocoValidationGeneratorTestFixture
    {
        private DirectoryInfo outputDirectory;
        private UmlCorePocoValidationGenerator generator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.AutoGenExtensions.ElementExtensions");

            this.outputDirectory = directoryInfo.CreateSubdirectory(path);
            this.generator = new UmlCorePocoValidationGenerator();
        }

        [Test]
        public async Task VerifyElementExtensionsAreGenerated()
        {
            await Assert.ThatAsync(
                () => this.generator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.outputDirectory),
                Throws.Nothing);
        }

        [Test]
        [Category("Expected")]
        public async Task VerifyExpectedElementExtensionsMatches()
        {
            var generatedCode = await this.generator.GenerateElementExtensions(
                GeneratorSetupFixture.XmiReaderResult,
                this.outputDirectory);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                "Expected/UML/Core/AutoGenExtensions/ElementExtensions.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
