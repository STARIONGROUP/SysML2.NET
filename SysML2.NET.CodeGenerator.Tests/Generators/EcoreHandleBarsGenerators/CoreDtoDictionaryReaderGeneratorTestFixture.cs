// -------------------------------------------------------------------------------------------------
// <copyright file="DtoDictionaryReaderGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    /// Suite of tests for the <see cref="CoreDtoDictionaryReaderGenerator"/> class.
    /// </summary>
    [TestFixture]
    public class CoreDtoDictionaryReaderGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private CoreDtoDictionaryReaderGenerator dtoDictionaryReaderGenerator;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            var outputpath = TestContext.CurrentContext.TestDirectory;
            var directoryInfo = new DirectoryInfo(outputpath);
            dtoDirectoryInfo = directoryInfo.CreateSubdirectory("_SysML2.NET.Serializer.Dictionary.Core.AutoGenDictionaryReader");

            rootPackage = DataModelLoader.Load();

            this.dtoDictionaryReaderGenerator = new CoreDtoDictionaryReaderGenerator();
        }

        [Test]
        public void verify_dto_DictionaryReaders_are_generated()
        {
            Assert.That(async () => await this.dtoDictionaryReaderGenerator.GenerateDtoDictionaryReaders(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public void verify_DictionaryReaderProvider_is_generated()
        {
            Assert.That(async () => await this.dtoDictionaryReaderGenerator.GenerateDtoDictionaryReaderProvider(rootPackage, dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test, TestCaseSource(typeof(Expected.ExpectedConcreteClasses)), Category("Expected")]
        public async Task Verify_that_expected_class_are_generated_correctly(string className)
        {
            var generatedCode = await this.dtoDictionaryReaderGenerator.GenerateDtoDictionaryReader(rootPackage, dtoDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/AutoGenDictionaryReader/{className}DictionaryReader.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}
