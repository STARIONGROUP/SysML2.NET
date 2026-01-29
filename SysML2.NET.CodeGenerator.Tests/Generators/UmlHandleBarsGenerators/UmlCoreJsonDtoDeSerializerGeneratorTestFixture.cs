// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCoreJsonDtoSerializerGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    public class UmlCoreJsonDtoDeSerializerGeneratorTestFixture<TModelKindConfiguration>: ModelKindDependentTestFixture<TModelKindConfiguration> where TModelKindConfiguration: IModelKindConfiguration, new()
    {
        private DirectoryInfo umlDtoDeserializerDirectoryInfo;
        private UmlCoreJsonDtoDeSerializerGenerator umlCoreDtoDeSerializerGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML",$"_SysML2.NET.Serializer.Json.{this.ModelKind}.AutoGenDeSerializer");

            this.umlDtoDeserializerDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCoreDtoDeSerializerGenerator = new UmlCoreJsonDtoDeSerializerGenerator();
        }

        [Test]
        public async Task VerifyCanGenerateDtoJsonDeSerializer()
        {
            await Assert.ThatAsync(() => this.umlCoreDtoDeSerializerGenerator.GenerateAsync(this.XmiReaderResult, this.umlDtoDeserializerDirectoryInfo, this.ModelKind), Throws.Nothing);
        }
        
        [Test, TestCaseSource(nameof(GetConcreteClasses)), Category("Expected")]
        public async Task VerifyMatchesExpectedDeSerializerClass(string className)
        {
            var generatedCode = await this.umlCoreDtoDeSerializerGenerator.GenerateDtoDeSerializerClass(this.XmiReaderResult, this.umlDtoDeserializerDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/UML/{this.ModelKind}/AutoGenDeSerializer/{className}DeSerializer.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
