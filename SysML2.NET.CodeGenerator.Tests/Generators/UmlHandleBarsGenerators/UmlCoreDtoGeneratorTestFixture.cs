// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDtoGeneratorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Tests.Expected.Ecore.Core;

    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    [TestFixture]
    public class UmlCoreDtoGeneratorTestFixture
    {
        private DirectoryInfo umlDtoDirectoryInfo;
        private UmlCoreDtoGenerator umlCoreDtoGenerator;

        [OneTimeSetUp]
        public void SetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.AutoGenDto");

            this.umlDtoDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreDtoGenerator = new UmlCoreDtoGenerator();
        }

        [Test]
        public async Task Verify_that_DataTransferObjects_are_generated()
        {
            await Assert.ThatAsync(() => this.umlCoreDtoGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult,
                    this.umlDtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_Classes_are_generated(string className)
        {
            var generatedCode = await this.umlCoreDtoGenerator.GenerateDataTransferObjectClassAsync(GeneratorSetupFixture.XmiReaderResult,
                this.umlDtoDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenDto/{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(typeof(ExpectedAllClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_Interfaces_are_generated(string className)
        {
            var generatedCode = await this.umlCoreDtoGenerator.GenerateDataTransferObjectInterfaceAsync(GeneratorSetupFixture.XmiReaderResult,
                this.umlDtoDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenDto/I{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
