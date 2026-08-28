// -------------------------------------------------------------------------------------------------
// <copyright file="TextuNotationValidationTestFixture.cs" company="Starion Group S.A.">
// 
//    Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.TextualNotation.Tests.Writers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using SysML2.NET.Semantics.Extensions;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Serializer.TextualNotation.Tests.Wrapper;
    using SysML2.NET.Serializer.TextualNotation.Writers;
    using SysML2.NET.Serializer.Xmi;

    [TestFixture]
    public class TextualNotationValidationTestFixture
    {
        /// <summary>
        /// The container supplying the implied-relationship services, built once for the whole fixture.
        /// </summary>
        private ServiceProvider serviceProvider;

        /// <summary>
        /// Builds the semantics container once for the fixture.
        /// </summary>
        /// <remarks>
        /// The library index is built from a FULL, model-independent load: the 8.4.2 constraints target
        /// library Types a given model need not import, so an index built from a model's referenced
        /// Namespaces cannot resolve them. It is shared across every case because the load is the
        /// expensive part — repeating it per case dominates the suite.
        /// </remarks>
        /// <returns>An awaitable task.</returns>
        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Error));

            var libraryRoot = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources");

            var redirectingService = new LibraryRedirectingExternalReferenceService(
                libraryRoot,
                loggerFactory.CreateLogger<LibraryRedirectingExternalReferenceService>());

            var libraryNamespaces = await new ModelLibraryLoader(loggerFactory, redirectingService).LoadAsync(libraryRoot);

            var services = new ServiceCollection();
            services.AddSysML2Semantics(options => options.EnableLibrarySpecializations = true);
            services.AddSingleton<ILibraryTypeIndex>(OwnershipTreeLibraryTypeIndex.Build(libraryNamespaces));

            this.serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Disposes the fixture's container.
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this.serviceProvider?.Dispose();
        }

        [Test]
        [TestCase("01-Parts Tree", "1a-Parts Tree.sysmlx")]
        [TestCase("01-Parts Tree", "1c-Parts Tree Redefinition.sysmlx")]
        [TestCase("01-Parts Tree", "1d-Parts Tree with Reference.sysmlx")]
        [TestCase("02-Parts Interconnection", "2a-Parts Interconnection.sysmlx")]
        [TestCase("02-Parts Interconnection", "2c-Parts Interconnection-Multiple Decompositions.sysmlx")]
        [TestCase("03-Function-based Behavior", "3a-Function-based Behavior-1.sysmlx")]
        [TestCase("03-Function-based Behavior", "3a-Function-based Behavior-2.sysmlx")]
        [TestCase("03-Function-based Behavior", "3a-Function-based Behavior-3.sysmlx")]
        [TestCase("03-Function-based Behavior", "3c-Function-based Behavior-structure mod-1.sysmlx")]
        [TestCase("03-Function-based Behavior", "3c-Function-based Behavior-structure mod-2.sysmlx")]
        [TestCase("03-Function-based Behavior", "3c-Function-based Behavior-structure mod-3.sysmlx")]
        [TestCase("03-Function-based Behavior", "3d-Function-based Behavior-item.sysmlx")]
        [TestCase("03-Function-based Behavior", "3e-Function-based Behavior-item.sysmlx")]
        [TestCase("04-Functional Allocation", "4a-Functional Allocation.sysmlx")]
        [TestCase("05-State-based Behavior", "5-State-based Behavior-1a.sysmlx")]
        [TestCase("05-State-based Behavior", "5-State-based Behavior-1.sysmlx")]
        [TestCase("05-State-based Behavior", "5-State-based Behavior-2.sysmlx")]
        [TestCase("06-Individual and Snapshots", "6-Individual and Snapshots.sysmlx")]
        [TestCase("07-Variant Configuration", "7a-Variant Configuration - General Concept.sysmlx")]
        [TestCase("07-Variant Configuration", "7a1-Variant Configuration - General Concept-a.sysmlx")]
        [TestCase("07-Variant Configuration", "7b-Variant Configurations.sysmlx")]
        [TestCase("08-Requirements", "8-Requirements.sysmlx")]
        [TestCase("09-Verification", "9-Verification-simplified.sysmlx")]
        [TestCase("10-Analysis and Trades", "10a-Analysis.sysmlx")]
        [TestCase("10-Analysis and Trades", "10b-Trade-off Among Alternative Configurations.sysmlx")]
        [TestCase("10-Analysis and Trades", "10c-Fuel Economy Analysis.sysmlx")]
        [TestCase("10-Analysis and Trades", "10d-Dynamics Analysis.sysmlx")]
        [TestCase("12-Dependency Relationships", "12a-Dependency.sysmlx")]
        [TestCase("12-Dependency Relationships", "12b-Allocation-1.sysmlx")]
        [TestCase("12-Dependency Relationships", "12b-Allocation.sysmlx")]
        [TestCase("13-Model Containment", "13a-Model Containment.sysmlx")]
        [TestCase("13-Model Containment", "13b-Safety and Security Features Element Group-1.sysmlx")]
        [TestCase("13-Model Containment", "13b-Safety and Security Features Element Group-2.sysmlx")]
        [TestCase("13-Model Containment", "13b-Safety and Security Features Element Group.sysmlx")]
        [TestCase("14-Language Extensions", "14a-Language Extensions.sysmlx")]
        [TestCase("14-Language Extensions", "14b-Language Extensions.sysmlx")]
        [TestCase("14-Language Extensions", "14c-Language Extensions.sysmlx")]
        public async Task VerifyValidationTextualNotationXmi(string folderName, string fileName)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Warning);
            });

            var libraryRoot = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources");
            
            var redirectingService = new LibraryRedirectingExternalReferenceService(
                libraryRoot,
                loggerFactory.CreateLogger<LibraryRedirectingExternalReferenceService>());

            var deSerializer = new DeSerializer(loggerFactory, redirectingService);

            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Validation", folderName, fileName);

            var readResult = await deSerializer.DeSerializeAsync(new Uri(filePath));
            var rootNamespace = readResult.RootNamespace;

            // The provider memoises per Type, so each case gets its own scope while the library index and
            // the registered guards and rules stay shared across the fixture.
            using var serviceScope = this.serviceProvider.CreateScope();
            var impliedRelationshipProvider = serviceScope.ServiceProvider.GetRequiredService<IImpliedRelationshipProvider>();

            // The referenced namespaces are the roots of the model libraries pulled in while resolving the
            // file's external references. They form the global Namespace (KerML §8.2.3.5.2), so the writer
            // needs them to shorten a reference routed through a library the model does not itself import.
            using var writerContext = new TextualNotationWriterContext(rootNamespace, readResult.ReferencedNamespaces, impliedRelationshipProvider);
            writerContext.EmitOperatorParentheses = true;
            var stringBuilder = new IndentedStringBuilder();

            try
            {
                NamespaceTextualNotationBuilder.BuildRootNamespace(rootNamespace, writerContext, stringBuilder);
            }
            catch (System.Exception exception)
            {
                TestContext.WriteLine($"Builder stopped early due to: {exception.Message}, {exception.StackTrace}");
            }

            var textualNotation = stringBuilder.ToString();

            Assert.That(textualNotation, Is.Not.Empty);
            TestContext.WriteLine("=== Textual Notation Output ===");
            TestContext.WriteLine(textualNotation);
            TestContext.WriteLine("=== End ===");
            
            var expectedFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Expected", folderName, fileName.Replace(".sysmlx", ".sysml"));

            var expectedContent = await File.ReadAllTextAsync(expectedFilePath);
            Assert.That(expectedContent, Is.EqualTo(textualNotation));
        }
    }
}
