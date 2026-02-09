// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreXmiReaderGeneratorTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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
    public class UmlCoreXmiReaderGeneratorTestFixture
    {
        private DirectoryInfo umlXmiReaderDirectoryInfo;
        private UmlCoreXmiReaderGenerator umlCoreXmiReaderGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Serializer.Xmi.AutoGenReaders");

            this.umlXmiReaderDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreXmiReaderGenerator = new UmlCoreXmiReaderGenerator();
        }

        [Test]
        public async Task VerifyXmiReadersAreGenerated()
        {
            await Assert.ThatAsync(() => this.umlCoreXmiReaderGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlXmiReaderDirectoryInfo), Throws.Nothing);
        }
        
        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_classes_are_generated(string className)
        {
            var generatedCode = await this.umlCoreXmiReaderGenerator.GenerateXmiReaderClass(GeneratorSetupFixture.XmiReaderResult,
                this.umlXmiReaderDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenReaders/{className}Reader.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
