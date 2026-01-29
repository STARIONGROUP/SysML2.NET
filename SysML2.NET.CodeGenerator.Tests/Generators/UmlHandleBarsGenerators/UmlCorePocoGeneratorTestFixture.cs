// -------------------------------------------------------------------------------------------------
// <copyright file="UmlCorePocoGeneratorTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Enumeration;
    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Tests.Expected;
    using SysML2.NET.CodeGenerator.Tests.TestFixtureSourceConfiguration;

    [TestFixture(typeof(CoreModelKindConfiguration))]
    [TestFixture(typeof(PimModelKindConfiguration))]
    public class UmlCorePocoGeneratorTestFixture<TModelKindConfiguration>: ModelKindDependentTestFixture<TModelKindConfiguration> where TModelKindConfiguration: IModelKindConfiguration, new()
    {
        private DirectoryInfo umlPocoDirectoryInfo;
        private UmlCorePocoGenerator umlCorePocoGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", $"_SysML2.NET.{this.ModelKind}.AutoGenPoco");

            this.umlPocoDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlCorePocoGenerator = new UmlCorePocoGenerator();
        }

        [Test]
        public async Task Verify_that_PlainOldClrObjects_are_generated()
        {
            await Assert.ThatAsync(() => this.umlCorePocoGenerator.GenerateAsync(this.XmiReaderResult, this.umlPocoDirectoryInfo, this.ModelKind), Throws.Nothing);
        }
        
        [Test]
        [TestCaseSource(nameof(GetAllClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_Interfaces_are_generated(string className)
        {
            var generatedCode = await this.umlCorePocoGenerator.GeneratePocoInterfaceAsync(this.XmiReaderResult,
                this.umlPocoDirectoryInfo,
                className, this.ModelKind);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/{this.ModelKind}/AutoGenPoco/I{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
        
        [Test]
        [TestCaseSource(nameof(GetConcreteClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_classes_are_generated(string className)
        {
            var generatedCode = await this.umlCorePocoGenerator.GeneratePocoClassAsync(this.XmiReaderResult,
                this.umlPocoDirectoryInfo,
                className, this.ModelKind);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/{this.ModelKind}/AutoGenPoco/{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
