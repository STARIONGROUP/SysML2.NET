// -------------------------------------------------------------------------------------------------
// <copyright file="PimEnumGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using Microsoft.Extensions.Logging;
    
    using NUnit.Framework;

    using Serilog;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;

    using uml4net.xmi;
    using uml4net.xmi.Readers;

    [TestFixture]
    public class PimEnumGeneratorTestFixture
    {
        private DirectoryInfo enumerationDirectoryInfo;

        private PimEnumGenerator pimEnumGenerator;

        private ILoggerFactory loggerFactory;

        private XmiReaderResult xmiReaderResult;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            this.loggerFactory = LoggerFactory.Create(builder => { builder.AddSerilog(); });
        }

        [SetUp]
        public void SetUp()
        {
            var rootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            var reader = XmiReaderBuilder.Create()
                .UsingSettings(x => x.LocalReferenceBasePath = rootPath)
                .WithLogger(this.loggerFactory)
                .Build();

            this.xmiReaderResult = reader.Read(Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel",
                "Systems-Modeling-API.xmi"));

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.enumerationDirectoryInfo = directoryInfo.CreateSubdirectory("_SysML2.NET.PIM.AutoGenEnum");

            this.pimEnumGenerator = new PimEnumGenerator();
        }

        [Test]
        public void Verify_that_enumerations_are_generated()
        {
            Assert.That(
                async () => await this.pimEnumGenerator.GenerateAsync(this.xmiReaderResult, this.enumerationDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public async Task Verify_that_expected_enums_are_generated([Values(
            "Operator", "JoinOperator")] string enumName)
        {
            var generatedCode = await this.pimEnumGenerator.GenerateEnumerationAsync(this.xmiReaderResult, this.enumerationDirectoryInfo,
                enumName);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/AutoGenPimEnum/{enumName}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }
    }
}