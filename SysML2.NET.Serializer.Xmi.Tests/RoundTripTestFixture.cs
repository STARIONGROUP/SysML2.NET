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
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Org.XmlUnit.Builder;
    using Org.XmlUnit.Diff;

    using SysML2.NET.Serializer.Xmi.Writers;

    [TestFixture]
    public class RoundTripTestFixture
    {
        private ILoggerFactory loggerFactory;
        
        private XmiWriterOptions writerOptions = new();

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
            var deSerializer = new DeSerializer(this.loggerFactory);

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
            var deSerializer1 = new DeSerializer(this.loggerFactory);

            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            var originalNamespace = deSerializer1.DeSerialize(new Uri(filePath));

            Assert.That(originalNamespace, Is.Not.Null);

            // Step 2: Serialize to stream
            var serializer = new Serializer(this.loggerFactory);
            var memoryStream = new MemoryStream();

            serializer.Serialize(originalNamespace, this.writerOptions, memoryStream);

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
            var deSerializer = new DeSerializer(this.loggerFactory);

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

        [Test]
        public void VerifySerializedXmlCorrespondToOriginalFile()
        {
            var deSerializer = new DeSerializer(this.loggerFactory);

            var originMap = new XmiElementOriginMap();

            // Quantities.sysmlx references other files like ScalarValues.kermlx, ISQBase.sysmlx, etc.
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            var fileUri = new Uri(filePath);

            var quantityNamespace = deSerializer.DeSerialize(fileUri, originMap);
            
            var serializer = new Serializer(this.loggerFactory);
            var outputFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "SerializedQuantities.sysmlx");
            
            var fileStream = new FileStream(outputFile, FileMode.Create);
            
            serializer.Serialize(quantityNamespace, this.writerOptions, fileStream, originMap, new Uri(filePath));
            fileStream.Close();

            var content = File.ReadAllText(outputFile);
            
            var xmlDiff = DiffBuilder.Compare(Input.FromFile(filePath))
                .WithTest(Input.FromString(content.Replace("http://www.omg.org/spec/XMI/20131001", "http://www.omg.org/XMI")))
                .IgnoreWhitespace() 
                .IgnoreComments() 
                .WithAttributeFilter(attr => attr.LocalName != "version")
                .WithDifferenceEvaluator((comparison, outcome) =>
                {
                    switch (comparison.Type)
                    {
                        case ComparisonType.XML_ENCODING:
                        case ComparisonType.XML_VERSION:
                            return ComparisonResult.EQUAL;
                        case ComparisonType.ATTR_VALUE:
                        {
                            var control = comparison.ControlDetails.Value?.ToString()?.Replace("%20", " ");
                            var test = comparison.TestDetails.Value?.ToString()?.Replace("%20", " ");

                            if (control == test)
                            {
                                return ComparisonResult.EQUAL;
                            }

                            break;
                        }
                        
                        case ComparisonType.ATTR_NAME_LOOKUP:
                        {
                            if (outcome != ComparisonResult.EQUAL && comparison.ControlDetails.Target is XmlElement controlElement)
                            {
                                var testElement = (XmlElement)comparison.TestDetails.Target;

                                if (controlElement.HasAttribute("isComposite") && !testElement.HasAttribute("isComposite"))
                                {
                                    return controlElement.GetAttribute("isComposite") == "false" ? ComparisonResult.EQUAL : ComparisonResult.DIFFERENT;
                                }

                                if (controlElement.HasAttribute("declaredName") && !testElement.HasAttribute("declaredName"))
                                {
                                    return controlElement.GetAttribute("declaredName") == "" ? ComparisonResult.EQUAL : ComparisonResult.DIFFERENT;
                                }
                            }

                            break;
                        }

                        case ComparisonType.ELEMENT_NUM_ATTRIBUTES:
                        {
                            var controlCount = Convert.ToInt32(comparison.ControlDetails.Value);
                            var testCount = Convert.ToInt32(comparison.TestDetails.Value);

                            if (Math.Abs(controlCount - testCount) == 1)
                            {
                                var controlElement =(XmlElement)comparison.ControlDetails.Target;
                                var testElement = (XmlElement)comparison.TestDetails.Target;

                                if (controlElement.HasAttribute("isComposite") && !testElement.HasAttribute("isComposite"))
                                {
                                    return controlElement.GetAttribute("isComposite") == "false" ? ComparisonResult.EQUAL : ComparisonResult.DIFFERENT;
                                }
                                
                                if (controlElement.HasAttribute("declaredName") && !testElement.HasAttribute("declaredName"))
                                {
                                    return controlElement.GetAttribute("declaredName") == "" ? ComparisonResult.EQUAL : ComparisonResult.DIFFERENT;
                                }
                            }

                            break;
                        }
                    }
                    
                    return outcome;
                })
                .CheckForIdentical()
                .Build();
            
            Assert.That(xmlDiff.HasDifferences, Is.False);
        }

        [Test]
        public async Task Verify_that_multi_file_SerializeAsync_writes_expected_files()
        {
            var deSerializer = new DeSerializer(this.loggerFactory);
            var originMap = new XmiElementOriginMap();

            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            var fileUri = new Uri(filePath);

            var rootNamespace = await deSerializer.DeSerializeAsync(fileUri, originMap);

            var outputDirPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "AsyncMultiFileOutput");
            var outputDir = new DirectoryInfo(outputDirPath);

            try
            {
                var serializer = new Serializer(this.loggerFactory);

                await serializer.SerializeAsync(rootNamespace, originMap, outputDir, this.writerOptions, CancellationToken.None);

                Assert.That(outputDir.Exists, Is.True, "Output directory should exist");

                var outputFiles = outputDir.GetFiles("*", SearchOption.AllDirectories);
                Assert.That(outputFiles, Is.Not.Empty, "Output directory should contain files");

                // The serializer only writes files for namespaces it can resolve from the deserialized graph.
                // Referenced external files in the origin map may not have their namespaces available.
                Assert.That(outputFiles.Length, Is.GreaterThanOrEqualTo(1), "At least the root namespace file should be written");
                Assert.That(outputFiles.Length, Is.LessThanOrEqualTo(originMap.GetAllSourceFiles().Count()), "Should not write more files than source files in origin map");

                foreach (var outputFile in outputFiles)
                {
                    Assert.That(outputFile.Length, Is.GreaterThan(0), $"File {outputFile.Name} should not be empty");

                    var xmlDoc = new XmlDocument();
                    Assert.That(() => xmlDoc.Load(outputFile.FullName), Throws.Nothing, $"File {outputFile.Name} should be well-formed XML");

                    var rootElement = xmlDoc.DocumentElement;
                    Assert.That(rootElement, Is.Not.Null, $"File {outputFile.Name} should have a root element");
                    Assert.That(rootElement.LocalName, Is.EqualTo("Namespace"), $"File {outputFile.Name} should have root element 'Namespace'");
                    Assert.That(rootElement.GetAttribute("id", "http://www.omg.org/spec/XMI/20131001"), Is.Not.Empty, $"File {outputFile.Name} should have an xmi:id attribute");
                }
            }
            finally
            {
                if (Directory.Exists(outputDirPath))
                {
                    Directory.Delete(outputDirPath, true);
                }
            }
        }
    }
}
