// ------------------------------------------------------------------------------------------------
// <copyright file="UmlPocoExtendGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    [Explicit("The POCO Extend classes are Extension methods to compute derived properties. After generation " +
              "these classes are updated manually and shall not just be overriden")]
    public class UmlPocoExtendGeneratorTestFixture
    {
        private DirectoryInfo umlPocoExtendDirectoryInfo;
        private UmlPocoClassExtensionsGenerator umlPocoClassExtensionsGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.AutoGenPocoExtend");

            this.umlPocoExtendDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlPocoClassExtensionsGenerator = new UmlPocoClassExtensionsGenerator();
        }

        [Test]
        public async Task Verify_that_expected_Extensions_are_generated_for_classes_with_derived_properties()
        {
            await Assert.ThatAsync(() => this.umlPocoClassExtensionsGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlPocoExtendDirectoryInfo), Throws.Nothing);
        }

        [Test]
        [TestCaseSource(typeof(ExpectedAllClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_Extensions_are_generated(string className)
        {
            var generatedCode = await this.umlPocoClassExtensionsGenerator.GenerateExtendClassAsync(GeneratorSetupFixture.XmiReaderResult,
                this.umlPocoExtendDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenPocoExtend/{className}Extensions.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
