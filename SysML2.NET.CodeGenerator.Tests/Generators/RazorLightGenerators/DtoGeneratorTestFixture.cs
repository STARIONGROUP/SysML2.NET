// -------------------------------------------------------------------------------------------------
// <copyright file="DtoGeneratorTestFixture.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Generators.RazorLightGenerators
{
    using System.IO;
    using ECoreNetto;
    using NUnit.Framework;
    using SysML2.NET.CodeGenerator.Generators.RazorLightGenerators;

    [TestFixture]
    public class DtoGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private DtoGenerator dtoGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);
            dtoDirectoryInfo = directoryInfo.CreateSubdirectory("AutoGenDto");

            rootPackage = DataModelLoader.Load();

            dtoGenerator = new DtoGenerator();
        }

        [Test]
        public void verify_dto_are_generated()
        {
            Assert.Ignore();

            Assert.That(async () => await dtoGenerator.Generate(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }
    }
}