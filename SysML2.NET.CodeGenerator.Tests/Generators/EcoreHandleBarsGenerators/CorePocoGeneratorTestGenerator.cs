// -------------------------------------------------------------------------------------------------
// <copyright file="PocoGeneratorTestGenerator.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2024 RHEA System S.A.
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
    
    [TestFixture]
    public class CorePocoGeneratorTestGenerator
    {
        private DirectoryInfo dtoDirectoryInfo;

        private CorePocoGenerator pocoGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);
            dtoDirectoryInfo = directoryInfo.CreateSubdirectory("_SysML2.NET.Core.AutoGenPoco");

            rootPackage = DataModelLoader.Load();

            this.pocoGenerator = new CorePocoGenerator();
        }

        [Test]
        public void verify_poco_interfaces_are_generated()
        {
            Assert.That(async () => await this.pocoGenerator.GenerateInterfaces(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public void verify_poco_classes_are_generated()
        {
            Assert.That(async () => await this.pocoGenerator.GenerateClasses(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test, TestCaseSource(typeof(Expected.ExpectedConcreteClasses)), Category("Expected")]
        public async Task Verify_that_expected_poco_classes_are_generated_correctly(string className)
        {
            var generatedCode = await this.pocoGenerator.GenerateClass(rootPackage, dtoDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/AutGenPoco/{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }

        [Test, TestCaseSource(typeof(Expected.ExpectedAllClasses)), Category("Expected")]
        public async Task Verify_that_expected_poco_interfaces_are_generated_correctly(string className)
        {
            var generatedCode = await this.pocoGenerator.GenerateInterface(rootPackage, dtoDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/AutGenPoco/I{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
