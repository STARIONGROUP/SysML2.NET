// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreEnumGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.CodeGenerator.Tests.TestFixtureSourceConfiguration;

    [TestFixture(typeof(CoreModelKindConfiguration))]
    [TestFixture(typeof(PimModelKindConfiguration))]
    public class UmlCoreEnumGeneratorTestFixture<TModelKindConfiguration>: ModelKindDependentTestFixture<TModelKindConfiguration> where TModelKindConfiguration: IModelKindConfiguration, new()
    {
        private DirectoryInfo enumerationDirectoryInfo;
        private UmlCoreEnumGenerator coreEnumGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", $"_SysML2.NET.{this.ModelKind}.AutoGenEnum");

            this.enumerationDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.coreEnumGenerator = new UmlCoreEnumGenerator();
        }

        [Test]
        public async Task Verify_that_enumerations_are_generated()
        {
            await Assert.ThatAsync(() => this.coreEnumGenerator.GenerateAsync(this.XmiReaderResult, this.enumerationDirectoryInfo, this.ModelKind),
                Throws.Nothing);
        }

        [Test]
        [TestCaseSource(nameof(GetAllEnums))]
        public async Task Verify_that_expected_enums_are_generated(string enumName)
        {
            var generatedCode = await this.coreEnumGenerator.GenerateEnumerationAsync(this.XmiReaderResult, this.enumerationDirectoryInfo,
                enumName, this.ModelKind);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/{this.ModelKind}/AutoGenEnum/{enumName}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
