// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCorePocoValidationGeneratorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
