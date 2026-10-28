// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreXmiWriterGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreXmiWriterGeneratorTestFixture
    {
        private DirectoryInfo umlXmiWriterDirectoryInfo;
        private UmlCoreXmiWriterGenerator umlCoreXmiWriterGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Serializer.Xmi.AutoGenWriters");

            this.umlXmiWriterDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreXmiWriterGenerator = new UmlCoreXmiWriterGenerator();
        }

        [Test]
        public async Task VerifyXmiWritersAreGenerated()
        {
            await Assert.ThatAsync(() => this.umlCoreXmiWriterGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlXmiWriterDirectoryInfo), Throws.Nothing);
        }

        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_classes_are_generated(string className)
        {
            var generatedCode = await this.umlCoreXmiWriterGenerator.GenerateXmiWriterClass(GeneratorSetupFixture.XmiReaderResult,
                this.umlXmiWriterDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenWriters/{className}Writer.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
