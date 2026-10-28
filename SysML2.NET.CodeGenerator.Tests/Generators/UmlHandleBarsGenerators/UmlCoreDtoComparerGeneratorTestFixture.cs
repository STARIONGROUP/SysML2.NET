// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDtoComparerGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreDtoComparerGeneratorTestFixture
    {
        private DirectoryInfo umlDtoComparerDirectoryInfo;
        private UmlCoreDtoComparerGenerator umlCoreDtoComparerGenerator;

        [OneTimeSetUp]
        public void SetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.Extensions.AutoGenDtoComparers");

            this.umlDtoComparerDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreDtoComparerGenerator = new UmlCoreDtoComparerGenerator();
        }

        [Test]
        public async Task Verify_that_DataTransferObjectComparers_are_generated()
        {
            await Assert.ThatAsync(() => this.umlCoreDtoComparerGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult,
                    this.umlDtoComparerDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public async Task Verify_that_DataTransferObjectComparerProvider_is_generated()
        {
            await Assert.ThatAsync(() => this.umlCoreDtoComparerGenerator.GenerateDataTransferObjectComparerProviderAsync(GeneratorSetupFixture.XmiReaderResult,
                    this.umlDtoComparerDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_Classes_are_generated(string className)
        {
            var generatedCode = await this.umlCoreDtoComparerGenerator.GenerateDataTransferObjectComparerAsync(GeneratorSetupFixture.XmiReaderResult,
                this.umlDtoComparerDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenDtoComparers/{className}Comparer.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
