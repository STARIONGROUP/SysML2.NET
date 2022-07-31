// -------------------------------------------------------------------------------------------------
// <copyright file="DtoSerializerGeneratorTestFixture.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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
    using System.IO;

    using ECoreNetto;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators;

    [TestFixture]
    public class DtoSerializerGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private DtoSerializerGenerator dtoSerializerGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);
            dtoDirectoryInfo = directoryInfo.CreateSubdirectory("AutoGenSerializer");

            rootPackage = DataModelLoader.Load();

            dtoSerializerGenerator = new DtoSerializerGenerator();
        }

        [Test]
        public void verify_dto_serializers_are_generated()
        {
            Assert.That(async () => await dtoSerializerGenerator.GenerateSerializers(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public void verify_SerializationProvider_is_generated()
        {
            Assert.That(async () => await dtoSerializerGenerator.GenerateSerializationProvider(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }
    }
}
