// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDalPocoExtensionsGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreDalPocoFactoryGeneratorTestFixture
    {
        private DirectoryInfo umlPocoFactoryDirectoryInfo;
        private UmlCoreDalFactoryGenerator umlCoreDalPocoExtensionsGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Dal.AutoGenElementFactory");

            this.umlPocoFactoryDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreDalPocoExtensionsGenerator = new UmlCoreDalFactoryGenerator();
        }

        [Test]
        public async Task VerifyCanGeneratePocoFactory()
        {
             await Assert.ThatAsync(() => this.umlCoreDalPocoExtensionsGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlPocoFactoryDirectoryInfo), Throws.Nothing);
        }

        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task VerifyExpectedPocoFactoryMatches(string className)
        {
            var generatedCode = await this.umlCoreDalPocoExtensionsGenerator.GenerateDalPocoFactory(GeneratorSetupFixture.XmiReaderResult,
                this.umlPocoFactoryDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenElementFactory/{className}Factory.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
