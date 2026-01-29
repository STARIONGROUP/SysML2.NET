// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreEnumProviderGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreEnumProviderGeneratorTestFixture<TModelKindConfiguration>: ModelKindDependentTestFixture<TModelKindConfiguration> where TModelKindConfiguration: IModelKindConfiguration, new()
    {
        private DirectoryInfo enumerationProviderDirectoryInfo;

        private UmlCoreEnumProviderGenerator umlCoreEnumProviderGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", $"_SysML2.NET.Extensions.{this.ModelKind}.AutoGenEnumProvider");

            this.enumerationProviderDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.umlCoreEnumProviderGenerator = new UmlCoreEnumProviderGenerator();
        }

        [Test]
        public async Task Verify_that_EnumerationProviders_are_generated()
        {
            await Assert.ThatAsync(() => this.umlCoreEnumProviderGenerator.GenerateAsync(this.XmiReaderResult,
                    this.enumerationProviderDirectoryInfo, this.ModelKind),
                Throws.Nothing);
        }

        [Test]
        [TestCaseSource(nameof(GetAllEnums))]
        public async Task Verify_that_expected_enums_are_generated(string enumName)
        {
            var generatedCode = await this.umlCoreEnumProviderGenerator.GenerateEnumerationProviderAsync(this.XmiReaderResult,
                this.enumerationProviderDirectoryInfo,
                enumName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/{this.ModelKind}/AutoGenEnumProvider/{enumName}Provider.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
