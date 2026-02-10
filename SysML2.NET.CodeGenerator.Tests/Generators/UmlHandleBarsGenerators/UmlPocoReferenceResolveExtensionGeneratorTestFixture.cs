// -------------------------------------------------------------------------------------------------
// <copyright file="UmlPocoReferenceResolveExtensionGeneratorTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
