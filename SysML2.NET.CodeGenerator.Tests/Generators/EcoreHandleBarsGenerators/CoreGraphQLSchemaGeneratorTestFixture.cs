// -------------------------------------------------------------------------------------------------
// <copyright file="CoreGraphQLSchemaGeneratorTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Generators.HandleBarsGenerators
{
    using System.IO;

    using ECoreNetto;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators;

    [TestFixture]
    public class CoreGraphQLSchemaGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private CoreGraphQLSchemaGenerator graphQlSchemaGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);
            dtoDirectoryInfo = directoryInfo.CreateSubdirectory("_SysML2.NET.OGM.Core.AutoGenGraphQLSchema");

            rootPackage = DataModelLoader.Load();

            this.graphQlSchemaGenerator = new CoreGraphQLSchemaGenerator();
        }

        [Test]
        public void verify_GraphQL_Schema_Are_generated()
        {
            Assert.That(async () => await graphQlSchemaGenerator.Generate(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }
    }
}
