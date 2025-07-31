// -------------------------------------------------------------------------------------------------
// <copyright file="CoreDalPocoExtensionsGeneratorTestFixture.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// Suite of tests for the <see cref="CoreDalPocoExtensionsGeneratorTestFixture"/> class
    /// </summary>
    [TestFixture]
    public class CoreDalPocoExtensionsGeneratorTestFixture
    {
        private DirectoryInfo dalFactoryDirectoryInfo;

        private CoreDalPocoExtensionsGenerator dalPocoExtensionsGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);

            var path = Path.Combine("ECore", "_SysML2.NET.Dal.Core.AutoGenPocoExtension");

            this.dalFactoryDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.rootPackage = DataModelLoader.Load();

            this.dalPocoExtensionsGenerator = new CoreDalPocoExtensionsGenerator();
        }

        [Test]
        public void Verify_that_ElementExtensions_is_generated()
        {
            Assert.That(async () => await this.dalPocoExtensionsGenerator.GenerateElementExtensions(rootPackage, dalFactoryDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public void Verify_that_POCO_DalExtensions_are_generated()
        {
            Assert.That(async () => await this.dalPocoExtensionsGenerator.GenerateDalPocoExtensions(rootPackage, dalFactoryDirectoryInfo),
                Throws.Nothing);
        }

        [Test, TestCaseSource(typeof(ExpectedConcreteClasses)), Category("Expected")]
        public async Task Verify_that_POCO_DalExtensions_are_generated_as_expected(string className)
        {
            var generatedCode = await this.dalPocoExtensionsGenerator.GenerateDalPocoExtension(rootPackage, dalFactoryDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/ECore/Core/AutoGenPocoExtension/{className}Extensions.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}