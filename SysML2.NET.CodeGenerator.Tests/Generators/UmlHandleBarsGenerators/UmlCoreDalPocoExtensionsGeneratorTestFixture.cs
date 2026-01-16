// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreDalPocoExtensionsGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreDalPocoExtensionsGeneratorTestFixture
    {
        private DirectoryInfo umlPocoExtensionDirectoryInfo;
        private UmlCoreDalPocoExtensionsGenerator umlCoreDalPocoExtensionsGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.AutoGenPocoExtension");

            this.umlPocoExtensionDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreDalPocoExtensionsGenerator = new UmlCoreDalPocoExtensionsGenerator();
        }

        [Test]
        public async Task VerifyCanGeneratePocoExtensions()
        {
             await Assert.ThatAsync(() => this.umlCoreDalPocoExtensionsGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlPocoExtensionDirectoryInfo), Throws.Nothing);
        }

        [Test]
        [TestCaseSource(typeof(ExpectedConcreteClasses))]
        [Category("Expected")]
        public async Task VerifyExpectedPocoExtensionsMatches(string className)
        {
            var generatedCode = await this.umlCoreDalPocoExtensionsGenerator.GenerateDalPocoExtension(GeneratorSetupFixture.XmiReaderResult,
                this.umlPocoExtensionDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenPocoExtension/{className}Extensions.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
