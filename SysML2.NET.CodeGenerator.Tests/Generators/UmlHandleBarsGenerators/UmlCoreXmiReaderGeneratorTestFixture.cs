// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreXmiReaderGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.CodeGenerator.Tests.Expected.Ecore.Core;

    [TestFixture]
    public class UmlCoreXmiReaderGeneratorTestFixture
    {
        private DirectoryInfo umlXmiReaderDirectoryInfo;
        private UmlCoreXmiReaderGenerator umlCoreXmiReaderGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Serializer.Xmi.AutoGenReaders");

            this.umlXmiReaderDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreXmiReaderGenerator = new UmlCoreXmiReaderGenerator();
        }

        [Test]
        public async Task VerifyXmiReadersAreGenerated()
        {
            await Assert.ThatAsync(() => this.umlCoreXmiReaderGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlXmiReaderDirectoryInfo), Throws.Nothing);
        }
        
        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_classes_are_generated(string className)
        {
            var generatedCode = await this.umlCoreXmiReaderGenerator.GenerateXmiReaderClass(GeneratorSetupFixture.XmiReaderResult,
                this.umlXmiReaderDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenReaders/{className}Reader.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
