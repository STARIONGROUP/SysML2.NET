// ------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreJsonDtoSerializerGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreJsonDtoSerializerGeneratorTestFixture
    {
        private DirectoryInfo umlDtoSerializerDirectoryInfo;
        private UmlCoreJsonDtoSerializerGenerator umlCoreDtoSerializerGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Serializer.Json.Core.AutoGenSerializer");

            this.umlDtoSerializerDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreDtoSerializerGenerator = new UmlCoreJsonDtoSerializerGenerator();
        }

        [Test]
        public async Task VerifyCanGenerateDtoJsonSerializer()
        {
            await Assert.ThatAsync(() => this.umlCoreDtoSerializerGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlDtoSerializerDirectoryInfo), Throws.Nothing);
        }
        
        [Test, TestCaseSource(typeof(ExpectedConcreteClasses)), Category("Expected")]
        public async Task VerifyMatchesExpectedSerializerClass(string className)
        {
            var generatedCode = await this.umlCoreDtoSerializerGenerator.GenerateDtoSerializerClass(GeneratorSetupFixture.XmiReaderResult, this.umlDtoSerializerDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/UML/Core/AutoGenSerializer/{className}Serializer.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
