// -------------------------------------------------------------------------------------------------
// <copyright file="DtoDictionaryWriterGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    /// Suite of tests for the <see cref="CoreDtoDictionaryWriterGenerator"/> class.
    /// </summary>
    [Explicit("we are about to drop Ecore based code gen, these are present for reference only until permanently removed")]
    [TestFixture]
    public class CoreDtoDictionaryWriterGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private CoreDtoDictionaryWriterGenerator dtoDictionaryWriterGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);

            var path = Path.Combine("ECore", "_SysML2.NET.Serializer.Dictionary.Core.AutoGenDictionaryWriter");

            this.dtoDirectoryInfo = directoryInfo.CreateSubdirectory(path);

            this.rootPackage = DataModelLoader.Load();

            this.dtoDictionaryWriterGenerator = new CoreDtoDictionaryWriterGenerator();
        }

        [Test]
        public void verify_dto_DictionaryWriters_are_generated()
        {
            Assert.That(async () => await this.dtoDictionaryWriterGenerator.GenerateDtoDictionaryWriters(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public void verify_DictionaryWritersProvider_is_generated()
        {
            Assert.That(async () => await this.dtoDictionaryWriterGenerator.GenerateDtoDictionaryWriterProvider(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test, TestCaseSource(typeof(ExpectedConcreteClasses)), Category("Expected")]
        public async Task Verify_that_expected_class_are_generated_correctly(string className)
        {
            var generatedCode = await this.dtoDictionaryWriterGenerator.GenerateDtoDictionaryWriter(rootPackage, dtoDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/ECore/Core/AutoGenDictionaryWriter/{className}DictionaryWriter.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
