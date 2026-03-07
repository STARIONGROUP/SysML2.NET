// -------------------------------------------------------------------------------------------------
// <copyright file="RoundTripTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi.Tests
{
    using System;
    using System.IO;
    using System.Linq;

    using Core.POCO.Root.Namespaces;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using SysML2.NET.Serializer.Xmi.Extensions;
    using SysML2.NET.Serializer.Xmi.Readers;

    [TestFixture]
    public class RoundTripTestFixture
    {
        private ILoggerFactory loggerFactory;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();

            this.loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        }

        [Test]
        public void Verify_that_deserialization_with_origin_map_tracks_elements()
        {
            var xmiDataCache = new XmiDataCache(new PocoReferenceResolveExtensionsFacade(), this.loggerFactory.CreateLogger<XmiDataCache>());
            var deSerializer = new DeSerializer(
                new ExternalReferenceService(this.loggerFactory.CreateLogger<ExternalReferenceService>()),
                new XmiDataReaderFacade(),
                xmiDataCache,
                this.loggerFactory);

            var originMap = new XmiElementOriginMap();

            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            var fileUri = new Uri(filePath);

            var rootNamespace = deSerializer.DeSerialize(fileUri, originMap);

            Assert.That(rootNamespace, Is.Not.Null);

            // Verify that elements were tracked
            var allSourceFiles = originMap.GetAllSourceFiles().ToList();
            Assert.That(allSourceFiles, Is.Not.Empty);

            // Verify that the root namespace was registered
            var rootNsId = originMap.GetRootNamespaceId(fileUri);
            Assert.That(rootNsId, Is.EqualTo(rootNamespace.Id));

            // Verify that elements in the file can be queried
            var elementsInFile = originMap.GetElementsInFile(fileUri).ToList();
            Assert.That(elementsInFile, Is.Not.Empty);
            Assert.That(elementsInFile, Does.Contain(rootNamespace.Id));
        }

        [Test]
        public void Verify_that_serialized_output_is_well_formed_xml_with_correct_root()
        {
            // Step 1: Deserialize original
            var xmiDataCache1 = new XmiDataCache(new PocoReferenceResolveExtensionsFacade(), this.loggerFactory.CreateLogger<XmiDataCache>());
            var deSerializer1 = new DeSerializer(
                new ExternalReferenceService(this.loggerFactory.CreateLogger<ExternalReferenceService>()),
                new XmiDataReaderFacade(),
                xmiDataCache1,
                this.loggerFactory);

            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            var originalNamespace = deSerializer1.DeSerialize(new Uri(filePath));

            Assert.That(originalNamespace, Is.Not.Null);

            // Step 2: Serialize to stream
            var serializer = new Serializer(this.loggerFactory);
            var memoryStream = new MemoryStream();

            serializer.Serialize(originalNamespace, false, false, memoryStream);

            Assert.That(memoryStream.Length, Is.GreaterThan(0));

            // Step 3: Verify the output is well-formed XML with correct structure
            memoryStream.Seek(0, SeekOrigin.Begin);

            var xmlDoc = new System.Xml.XmlDocument();
            Assert.That(() => xmlDoc.Load(memoryStream), Throws.Nothing, "Output should be well-formed XML");

            var rootElement = xmlDoc.DocumentElement;
            Assert.That(rootElement, Is.Not.Null);
            Assert.That(rootElement.LocalName, Is.EqualTo("Namespace"));
            Assert.That(rootElement.GetAttribute("id", "http://www.omg.org/spec/XMI/20131001"), Is.EqualTo(originalNamespace.Id.ToString()));

            // Verify there are child elements (ownedRelationship)
            var childElements = rootElement.ChildNodes.Cast<System.Xml.XmlNode>().Where(n => n.NodeType == System.Xml.XmlNodeType.Element).ToList();
            Assert.That(childElements, Is.Not.Empty, "Should have child elements");
        }

        [Test]
        public void Verify_that_origin_map_captures_multi_file_references()
        {
            var xmiDataCache = new XmiDataCache(new PocoReferenceResolveExtensionsFacade(), this.loggerFactory.CreateLogger<XmiDataCache>());
            var deSerializer = new DeSerializer(
                new ExternalReferenceService(this.loggerFactory.CreateLogger<ExternalReferenceService>()),
                new XmiDataReaderFacade(),
                xmiDataCache,
                this.loggerFactory);

            var originMap = new XmiElementOriginMap();

            // Quantities.sysmlx references other files like ScalarValues.kermlx, ISQBase.sysmlx, etc.
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            var fileUri = new Uri(filePath);

            deSerializer.DeSerialize(fileUri, originMap);

            var allSourceFiles = originMap.GetAllSourceFiles().ToList();

            // Should have multiple source files since Quantities.sysmlx has external references
            Assert.That(allSourceFiles.Count, Is.GreaterThan(1));

            // Each source file should have a root namespace registered
            foreach (var sourceFile in allSourceFiles)
            {
                var rootNsId = originMap.GetRootNamespaceId(sourceFile);
                Assert.That(rootNsId, Is.Not.EqualTo(Guid.Empty), $"Root namespace not registered for {sourceFile}");
            }
        }
    }
}
