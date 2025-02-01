// -------------------------------------------------------------------------------------------------
// <copyright file="PimDtoGeneratorTestFixture.cs" company="Starion Group S.A.">
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
    using System.Linq;
    using System.Threading.Tasks;
    using CodeGenerator.Generators.UmlHandleBarsGenerators;
    
    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using Serilog;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi;
    using uml4net.xmi.Readers;

    [TestFixture]
    public class PimDtoGeneratorTestFixture
    {
        private DirectoryInfo dtoDirectoryInfo;

        private PimDtoGenerator pimDtoGenerator;

        private ILoggerFactory loggerFactory;

        private XmiReaderResult xmiReaderResult;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            this.loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog();
            });
        }

        [SetUp]
        public void SetUp()
        {
            var rootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            var reader = XmiReaderBuilder.Create()
                .UsingSettings(x => x.LocalReferenceBasePath = rootPath)
                .WithLogger(this.loggerFactory)
                .Build();

            this.xmiReaderResult = reader.Read(Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel", "Systems-Modeling-API.xmi"));

            var directoryInfo = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);

            this.dtoDirectoryInfo = directoryInfo.CreateSubdirectory("_SysML2.NET.PIM.AutoGenDto");

            this.pimDtoGenerator = new PimDtoGenerator();

            PropertyExtensions.AddOrOverwriteCSharpTypeMappings(("ISO8601DateTime", "DateTime"));
            PropertyExtensions.AddOrOverwriteCSharpTypeMappings(("UUID", "Guid"));
            PropertyExtensions.AddOrOverwriteCSharpTypeMappings(("IRI", "Uri"));
        }

        [TearDown]
        public void TearDown()
        {
            // Reset the custom datatype mappings, because Extensions.PropertyExtensions is static and settings will be shared between unit tests
            PropertyExtensions.ResetCSharpTypeMappingsToDefault();
        }

        [Test]
        public async Task Verify_that_concrete_classes_are_generated([Values(
            "Branch","Commit",
            "CompositeConstraint", "DataDifference",
            "DataVersion", "ExternalData",
            "Project")] string className)
        {
            
            var generatedCode = await this.pimDtoGenerator.GenerateClassAsync(this.xmiReaderResult, this.dtoDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/autoGenPimDto/{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }

        [Test]
        public async Task Verify_that_all_concrete_classes_are_generated()
        {
            Assert.That(async () => await this.pimDtoGenerator.GenerateClassesAsync(this.xmiReaderResult, this.dtoDirectoryInfo),
                Throws.Nothing);
        }

        [Test]
        public async Task Verify_that_interfaces_are_generated([Values(
            "Branch","Commit",
            "CompositeConstraint", "DataDifference",
            "DataVersion", "ExternalData", "ExternalRelationship",
            "Project", "Record")] string className)
        {

            var generatedCode = await this.pimDtoGenerator.GenerateInterfaceAsync(this.xmiReaderResult, this.dtoDirectoryInfo, className);

            var expected = await File.ReadAllTextAsync(Path.Combine(TestContext.CurrentContext.TestDirectory, $"Expected/autoGenPimDto/I{className}.cs"));

            Assert.That(generatedCode, Is.EqualTo(expected));
        }

        [Test]
        public void Verify_that_all_interfaces_are_generated()
        {
            Assert.That(async () => await this.pimDtoGenerator.GenerateInterfacesAsync(this.xmiReaderResult, this.dtoDirectoryInfo),
                Throws.Nothing);
        }
    }
}
