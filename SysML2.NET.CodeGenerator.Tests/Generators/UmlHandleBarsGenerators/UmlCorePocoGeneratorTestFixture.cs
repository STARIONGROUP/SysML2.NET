// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCorePocoGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCorePocoGeneratorTestFixture
    {
        private DirectoryInfo umlPocoDirectoryInfo;
        private UmlCorePocoGenerator umlCorePocoGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.AutoGenPoco");

            this.umlPocoDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCorePocoGenerator = new UmlCorePocoGenerator();
        }

        [Test]
        public async Task Verify_that_PlainOldClrObjects_are_generated()
        {
            await Assert.ThatAsync(() => this.umlCorePocoGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlPocoDirectoryInfo), Throws.Nothing);
        }
        
        [Test]
        [TestCaseSource(typeof(ExpectedAllClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_Interfaces_are_generated(string className)
        {
            var generatedCode = await this.umlCorePocoGenerator.GeneratePocoInterfaceAsync(GeneratorSetupFixture.XmiReaderResult,
                this.umlPocoDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenPoco/I{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
        
        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_classes_are_generated(string className)
        {
            var generatedCode = await this.umlCorePocoGenerator.GenerateDataTransferObjectClassAsync(GeneratorSetupFixture.XmiReaderResult,
                this.umlPocoDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenPoco/{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
