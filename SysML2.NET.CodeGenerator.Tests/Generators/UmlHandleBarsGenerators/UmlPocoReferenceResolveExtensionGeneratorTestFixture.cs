// ------------------------------------------------------------------------------------------------
// <copyright file="UmlPocoReferenceResolveExtensionGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlPocoReferenceResolveExtensionGeneratorTestFixture
    {
        private DirectoryInfo umlPocoReferenceResolveExtensionDirectoryInfo;
        private UmlPocoReferenceResolveExtensionGenerator umlPocoReferenceResolveExtensionGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Serializer.Xmi.AutoGenPocoReferenceResolveExtension");

            this.umlPocoReferenceResolveExtensionDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlPocoReferenceResolveExtensionGenerator = new UmlPocoReferenceResolveExtensionGenerator();
        }

        [Test]
        public async Task VerifyPocoReferenceResolveExtensionAreGenerated()
        {
            await Assert.ThatAsync(() => this.umlPocoReferenceResolveExtensionGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlPocoReferenceResolveExtensionDirectoryInfo), Throws.Nothing);
        }
        
        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task VerifyExpectedClassesMatches(string className)
        {
            var generatedCode = await this.umlPocoReferenceResolveExtensionGenerator.GenerateExtensionClass(GeneratorSetupFixture.XmiReaderResult,
                this.umlPocoReferenceResolveExtensionDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenPocoReferenceResolveExtension/{className}Extensions.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
