// -------------------------------------------------------------------------------------------------
// <copyright file="ClassKindEnumGeneratorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.Core;

    [TestFixture]
    public class ClassKindEnumGeneratorTestFixture
    {
        private DirectoryInfo enumerationDirectoryInfo;
        private ClassKindEnumGenerator classKindEnumGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            var path = Path.Combine("UML", "_SysML2.NET.Core.AutoGenEnum");

            this.enumerationDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.classKindEnumGenerator = new ClassKindEnumGenerator();
        }

        [Test]
        public async Task Verify_that_classkind_enum_is_generated()
        {
            await Assert.ThatAsync(() => this.classKindEnumGenerator.GenerateAsync(GeneratorSetupFixture.XmiReaderResult, this.enumerationDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        [Category("Expected")]
        public async Task Verify_that_expected_classkind_enum_is_generated()
        {
            var generatedCode = await this.classKindEnumGenerator.GenerateClassKindEnumAsync(GeneratorSetupFixture.XmiReaderResult, this.enumerationDirectoryInfo);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory,
                "Expected/UML/Core/AutoGenEnum/ClassKind.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }

        [Test]
        public void Verify_that_compiled_classkind_enum_matches_registry()
        {
            var enumValues = Enum.GetValues<ClassKind>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(enumValues, Has.Length.EqualTo(ClassKindRegistry.ClassKinds.Count),
                    "the compiled ClassKind enum in SysML2.NET/Core/AutoGenEnum/ClassKind.cs is out of sync with ClassKindRegistry — regenerate and copy it");

                foreach (var registration in ClassKindRegistry.ClassKinds)
                {
                    Assert.That(Enum.GetName((ClassKind)registration.Id), Is.EqualTo(registration.Name));
                }
            }
        }
    }
}
