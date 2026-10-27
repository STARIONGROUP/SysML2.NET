// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreEnumGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreEnumGeneratorTestFixture
    {
        private DirectoryInfo enumerationDirectoryInfo;
        private UmlCoreEnumGenerator coreEnumGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.AutoGenEnum");

            this.enumerationDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.coreEnumGenerator = new UmlCoreEnumGenerator();
        }

        [Test]
        public async Task Verify_that_enumerations_are_generated()
        {
            await Assert.ThatAsync(() => this.coreEnumGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.enumerationDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        [Category("Expected")]
        public async Task Verify_that_expected_enums_are_generated([Values("VisibilityKind", "TransitionFeatureKind")] string enumName)
        {
            var generatedCode = await this.coreEnumGenerator.GenerateEnumerationAsync(GeneratorSetupFixture.XmiReaderResult, this.enumerationDirectoryInfo,
                enumName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenEnum/{enumName}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
