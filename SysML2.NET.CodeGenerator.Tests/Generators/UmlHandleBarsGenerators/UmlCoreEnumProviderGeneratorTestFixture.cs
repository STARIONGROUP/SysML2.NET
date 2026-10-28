// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreEnumProviderGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreEnumProviderGeneratorTestFixture
    {
        private DirectoryInfo enumerationProviderDirectoryInfo;

        private UmlCoreEnumProviderGenerator umlCoreEnumProviderGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Extensions.Core.AutoGenEnumProvider");

            this.enumerationProviderDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.umlCoreEnumProviderGenerator = new UmlCoreEnumProviderGenerator();
        }

        [Test]
        public async Task Verify_that_EnumerationProviders_are_generated()
        {
            await Assert.ThatAsync(() => this.umlCoreEnumProviderGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult,
                    this.enumerationProviderDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        [Category("Expected")]
        public async Task Verify_that_expected_enums_are_generated([Values("VisibilityKind", "TransitionFeatureKind")] string enumName)
        {
            var generatedCode = await this.umlCoreEnumProviderGenerator.GenerateEnumerationProviderAsync(GeneratorSetupFixture.XmiReaderResult,
                this.enumerationProviderDirectoryInfo,
                enumName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenEnumProvider/{enumName}Provider.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
