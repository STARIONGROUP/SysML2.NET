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
    public class UmlCoreJsonDtoDeSerializerGeneratorTestFixture
    {
        private DirectoryInfo umlDtoDeserializerDirectoryInfo;
        private UmlCoreJsonDtoDeSerializerGenerator umlCoreDtoDeSerializerGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Serializer.Json.Core.AutoGenDeSerializer");

            this.umlDtoDeserializerDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreDtoDeSerializerGenerator = new UmlCoreJsonDtoDeSerializerGenerator();
        }

        [Test]
        public async Task VerifyCanGenerateDtoJsonDeSerializer()
        {
            await Assert.ThatAsync(() => this.umlCoreDtoDeSerializerGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlDtoDeserializerDirectoryInfo), Throws.Nothing);
        }
        
        [Test, TestCaseSource(typeof(ExpectedConcreteClasses)), Category("Expected")]
        public async Task VerifyMatchesExpectedDeSerializerClass(string className)
        {
            var generatedCode = await this.umlCoreDtoDeSerializerGenerator.GenerateDtoDeSerializerClass(GeneratorSetupFixture.XmiReaderResult, this.umlDtoDeserializerDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/UML/Core/AutoGenDeSerializer/{className}DeSerializer.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
