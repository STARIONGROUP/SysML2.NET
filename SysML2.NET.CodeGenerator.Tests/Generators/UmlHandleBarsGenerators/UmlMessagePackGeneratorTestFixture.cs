// -------------------------------------------------------------------------------------------------
// <copyright file="UmlMessagePackGeneratorTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2025 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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
    using NUnit.Framework;
    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.Tests.Expected.Ecore.Core;
    using System.IO;
    using System.Threading.Tasks;

    [TestFixture]
    public class UmlMessagePackGeneratorTestFixture
    {
        private DirectoryInfo umlMessagePackPayloadDirectoryInfo;
        private DirectoryInfo umlMessagePackFormatterDirectoryInfo;
        private DirectoryInfo umlMessagePackDataResolverGetFormatterHelperDirectoryInfo;
        private UmlMessagePackGenerator umlMessagePackGenerator;

        [OneTimeSetUp]
        public void SetUp()
        {
            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.umlMessagePackPayloadDirectoryInfo = directoryInfo.CreateSubdirectory(Path.Combine("UML", "_SysML2.NET.Serializer.MessagePack.Core.AutoGenMessagePackPayload"));
            this.umlMessagePackFormatterDirectoryInfo = directoryInfo.CreateSubdirectory(Path.Combine("UML", "_SysML2.NET.Serializer.MessagePack.Core.AutoGenMessagePackFormatter"));
            this.umlMessagePackDataResolverGetFormatterHelperDirectoryInfo = directoryInfo.CreateSubdirectory(Path.Combine("UML", "_SysML2.NET.Serializer.MessagePack.Core.AutoGenDataResolverGetFormatterHelper"));

            this.umlMessagePackGenerator = new UmlMessagePackGenerator();
        }

        [Test]
        public async Task Verify_that_Payload_is_generated()
        {
            await Assert.ThatAsync(() => this.umlMessagePackGenerator.GenerateMessagePackPayload(GeneratorSetupFixture.XmiReaderResult,
                    this.umlMessagePackPayloadDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public async Task Verify_that_DataResolverGetFormatterHelper_is_generated()
        {
            await Assert.ThatAsync(() => this.umlMessagePackGenerator.GenerateDataResolverGetFormatterHelper(GeneratorSetupFixture.XmiReaderResult,
                    this.umlMessagePackDataResolverGetFormatterHelperDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public async Task Verify_that_PayloadFactory_is_generated()
        {
            await Assert.ThatAsync(() => this.umlMessagePackGenerator.GenerateMessagePackPayloadFactory(GeneratorSetupFixture.XmiReaderResult,
                    this.umlMessagePackPayloadDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public async Task Verify_that_PayloadMessagePackFormatter_is_generated()
        {
            await Assert.ThatAsync(() => this.umlMessagePackGenerator.GenerateMessagePackPayloadMessagePackFormatter(GeneratorSetupFixture.XmiReaderResult,
                    this.umlMessagePackPayloadDirectoryInfo),
                Throws.Nothing);
        }


        [Test, TestCaseSource(typeof(ExpectedConcreteClasses)), Category("Expected")]
        public async Task Verify_that_generated_Matches_Expected_MessagePackFormatter_Per_Class(string className)
        {
            var generatedCode = await this.umlMessagePackGenerator.GenerateMessagePackFormatter(GeneratorSetupFixture.XmiReaderResult, this.umlMessagePackFormatterDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/UML/Core/AutoGenMessagePackFormatter/{className}MessagePackFormatter.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }

        [Test]
        public async Task Verify_that_MessagePackFormatters_are_generated()
        {
            await Assert.ThatAsync(() => this.umlMessagePackGenerator.GenerateMessagePackFormatters(GeneratorSetupFixture.XmiReaderResult,
                    this.umlMessagePackFormatterDirectoryInfo),
                Throws.Nothing);
        }
    }
}