// -------------------------------------------------------------------------------------------------
// <copyright file="DtoDeSerializerGeneratorTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Generators.HandleBarsGenerators
{
    using ECoreNetto;
    using NUnit.Framework;
    using SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Tests.Expected.Ecore.Core;
    using System.IO;
    using System.Threading.Tasks;

    [TestFixture]
    public class CoreJsonDtoDeSerializerGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private CoreJsonDtoDeSerializerGenerator coreJsonDtoDeSerializerGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);

            var path = Path.Combine("ECore", "_SysML2.NET.Serializer.Json.Core.AutoGenDeSerializer");

            this.dtoDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.rootPackage = DataModelLoader.Load();

            this.coreJsonDtoDeSerializerGenerator = new CoreJsonDtoDeSerializerGenerator();
        }

        [Test]
        public void verify_enum_deserializers_are_generated()
        {
            Assert.That(async () => await coreJsonDtoDeSerializerGenerator.GenerateEnumDeSerializers(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public void verify_dto_deserializers_are_generated()
        {
            Assert.That(async () => await coreJsonDtoDeSerializerGenerator.GenerateDtoDeSerializers(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public void verify_DeSerializationProvider_is_generated()
        {
            Assert.That(async () => await coreJsonDtoDeSerializerGenerator.GenerateDeSerializationProvider(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test, TestCaseSource(typeof(ExpectedConcreteClasses)), Category("Expected")]
        public void Verify_that_expected_json_deserializer_classes_are_generated_correctly(string className)
        {
            var generatedCode = this.coreJsonDtoDeSerializerGenerator.GenerateClass(this.rootPackage, className);

            var expected = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/ECore/Core/AutoGenDeSerializer/{className}DeSerializer.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
