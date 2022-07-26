// -------------------------------------------------------------------------------------------------
// <copyright file="EnumGeneratorTestFixture.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Generators
{
    using System.IO;
    using System.Threading.Tasks;
    using ECoreNetto;
    using NUnit.Framework;
    using SysML2.NET.CodeGenerator.Generators;

    [TestFixture]

    public class EnumGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private EnumGenerator enumGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);
            this.dtoDirectoryInfo = directoryInfo.CreateSubdirectory("AutoGenEnum");

            this.rootPackage = DataModelLoader.Load();

            this.enumGenerator = new EnumGenerator();
        }

        [Test]
        public void verify_enum_are_generated()
        {
            Assert.That(async () => await this.enumGenerator.Generate(this.rootPackage, this.dtoDirectoryInfo),
                Throws.Nothing);
        }
    }
}
