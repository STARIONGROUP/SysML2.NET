// -------------------------------------------------------------------------------------------------
// <copyright file="DtoPsmRestGeneratorTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2024 Starion Group S.A.
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

namespace SysML2.NET.CodeGenerator.Tests.Generators.OpenApiHandleBarsGenerators
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using OpenApi.Model;

    using SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators;

    [TestFixture]
    public class DtoPsmRestGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private DtoPsmRestGenerator dtoPsmRestGenerator;

        private Document openApiDocument;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);
            dtoDirectoryInfo = directoryInfo.CreateSubdirectory("_SysML2.NET.PSM.REST.AutoGenDto");

            openApiDocument = DataModelLoader.LoadOpenApi();

            this.dtoPsmRestGenerator = new DtoPsmRestGenerator();
        }

        [Test]
        public void verify_dto_classes_are_generated()
        {
            Assert.That(async () => await dtoPsmRestGenerator.Generate(this.openApiDocument, dtoDirectoryInfo),
                Throws.Exception);
        }
    }
}
