// -------------------------------------------------------------------------------------------------
// <copyright file="UmlPocoExtendGeneratorTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Tests.Expected.Ecore.Core;

    [TestFixture]
    [Explicit("The POCO Extend classes are Extension methods to compute derived properties. After generation " +
              "these classes are updated manually and shall not just be overriden")]
    public class UmlPocoExtendGeneratorTestFixture
    {
        private DirectoryInfo umlPocoExtendDirectoryInfo;
        private UmlPocoClassExtensionsGenerator umlPocoClassExtensionsGenerator;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.AutoGenPocoExtend");

            this.umlPocoExtendDirectoryInfo = directoryInfo.CreateSubdirectory(path);
            this.umlPocoClassExtensionsGenerator = new UmlPocoClassExtensionsGenerator();
        }

        [Test]
        public async Task Verify_that_expected_Extensions_are_generated_for_classes_with_derived_properties()
        {
            await Assert.ThatAsync(() => this.umlPocoClassExtensionsGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.umlPocoExtendDirectoryInfo), Throws.Nothing);
        }

        [Test]
        [TestCaseSource(typeof(ExpectedAllClasses))]
        [Category("Expected")]
        public async Task Verify_that_expected_Extensions_are_generated(string className)
        {
            var generatedCode = await this.umlPocoClassExtensionsGenerator.GenerateExtendClassAsync(GeneratorSetupFixture.XmiReaderResult,
                this.umlPocoExtendDirectoryInfo,
                className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                $"Expected/UML/Core/AutoGenPocoExtend/{className}Extensions.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
