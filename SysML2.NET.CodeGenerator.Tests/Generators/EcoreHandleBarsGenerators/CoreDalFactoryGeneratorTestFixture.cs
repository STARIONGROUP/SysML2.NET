// -------------------------------------------------------------------------------------------------
// <copyright file="CoreDalFactoryGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using System.IO;
    using System.Threading.Tasks;

    using ECoreNetto;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators;

    /// <summary>
    /// Suite of tests for the <see cref="CoreDalFactoryGenerator"/> class
    /// </summary>
    [TestFixture]
    public class CoreDalFactoryGeneratorTestFixture
    {
        private DirectoryInfo dalFactoryDirectoryInfo;

        private CoreDalFactoryGenerator dalFactoryGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);
            dalFactoryDirectoryInfo = directoryInfo.CreateSubdirectory("_SysML2.NET.Dal.Core.AutoGenElementFactory");

            rootPackage = DataModelLoader.Load();

            dalFactoryGenerator = new CoreDalFactoryGenerator();
        }

        [Test]
        public void Verify_that_POCO_Element_factory_is_generated()
        {
            Assert.That(async () => await this.dalFactoryGenerator.GenerateElementDalFactory(rootPackage, dalFactoryDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public void Verify_that_POCO_factories_are_generated()
        {
            Assert.That(async () => await this.dalFactoryGenerator.GeneratePocoFactories(rootPackage, dalFactoryDirectoryInfo),
                Throws.Nothing);
        }

        [Test, TestCaseSource(typeof(Expected.ExpectedConcreteClasses)), Category("Expected")]
        public async Task Verify_that_POCO_factories_are_generated_as_expected(string className)
        {
            var generatedCode = await this.dalFactoryGenerator.GeneratePocoFactory(rootPackage, dalFactoryDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/AutoGenElementFactory/{className}Factory.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
