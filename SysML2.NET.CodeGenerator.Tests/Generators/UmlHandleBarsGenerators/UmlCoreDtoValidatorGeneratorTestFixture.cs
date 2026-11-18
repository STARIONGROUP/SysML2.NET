// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDtoValidatorGeneratorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Tests.Expected.Ecore.Core;

    using uml4net.StructuredClassifiers;

    [TestFixture]
    public class UmlCoreDtoValidatorGeneratorTestFixture
    {
        private DirectoryInfo outputDirectory;
        private UmlCoreDtoValidatorGenerator umlCoreDtoValidatorGenerator;

        [OneTimeSetUp]
        public void SetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.outputDirectory = directoryInfo.CreateSubdirectory(Path.Combine("UML", "_SysML2.NET.Validation.Core.DTO"));
            this.umlCoreDtoValidatorGenerator = new UmlCoreDtoValidatorGenerator();
        }

        [Test]
        public async Task VerifyGenerateAsync()
        {
            await Assert.ThatAsync(() => this.umlCoreDtoValidatorGenerator.GenerateAsync(null, this.outputDirectory), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.umlCoreDtoValidatorGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, null), Throws.TypeOf<ArgumentNullException>());

            await Assert.ThatAsync(() => this.umlCoreDtoValidatorGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.outputDirectory), Throws.Nothing);

            var expectedFileNames = GeneratorSetupFixture.XmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(package => package.PackagedElement.OfType<IClass>())
                .Where(@class => !@class.IsAbstract)
                .Select(@class => $"{@class.Name}Validator.cs");

            var generatedFileNames = this.outputDirectory.GetFiles("*Validator.cs").Select(file => file.Name).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(generatedFileNames, Is.EquivalentTo(expectedFileNames));
                Assert.That(generatedFileNames, Does.Not.Contain("ElementValidator.cs"));
            }
        }

        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task VerifyGenerateDtoValidatorAsync(string className)
        {
            await Assert.ThatAsync(() => this.umlCoreDtoValidatorGenerator.GenerateDtoValidatorAsync(null, this.outputDirectory, className), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.umlCoreDtoValidatorGenerator.GenerateDtoValidatorAsync(GeneratorSetupFixture.XmiReaderResult, null, className), Throws.TypeOf<ArgumentNullException>());
            await Assert.ThatAsync(() => this.umlCoreDtoValidatorGenerator.GenerateDtoValidatorAsync(GeneratorSetupFixture.XmiReaderResult, this.outputDirectory, " "), Throws.TypeOf<ArgumentException>());

            var generatedCode = await this.umlCoreDtoValidatorGenerator.GenerateDtoValidatorAsync(GeneratorSetupFixture.XmiReaderResult, this.outputDirectory, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenDtoValidators/{className}Validator.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
