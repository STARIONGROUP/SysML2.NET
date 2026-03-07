// -------------------------------------------------------------------------------------------------
// <copyright file="Serializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Serializer.Xmi.Writers;

    /// <summary>
    /// The purpose of the <see cref="ISerializer"/> is to write an <see cref="INamespace"/>
    /// as XMI to a <see cref="Stream"/>
    /// </summary>
    public class Serializer : ISerializer
    {
        private const string XmiNamespace = "http://www.omg.org/spec/XMI/20131001";
        
        private const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
        
        private const string SysmlNamespace = "https://www.omg.org/spec/SysML/20240201";

        private readonly ILogger<Serializer> logger;
        
        private readonly ILoggerFactory loggerFactory;
        
        private readonly IXmiDataWriterFacade xmiWriterFacade;

        /// <summary>Initializes a new instance of the <see cref="Serializer"></see> class.</summary>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory " /> used to set up logging</param>
        public Serializer(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
            this.logger = this.loggerFactory.CreateLogger<Serializer>();
            this.xmiWriterFacade = new XmiDataWriterFacade(this.loggerFactory);
        }

        /// <summary>
        /// Serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        public void Serialize(INamespace @namespace, bool includeDerivedProperties, bool includesImplied, Stream stream)
        {
            this.Serialize(@namespace, includeDerivedProperties,includesImplied, stream, null, null);
        }

        /// <summary>
        /// Serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>,
        /// using an origin map for href reconstruction
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="elementOriginMap">
        /// The optional <see cref="IXmiElementOriginMap"/> for href reconstruction
        /// </param>
        /// <param name="currentFileUri">
        /// The optional <see cref="Uri"/> of the current output file for relative href computation
        /// </param>
        public void Serialize(INamespace @namespace, bool includeDerivedProperties, bool includesImplied, Stream stream, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (@namespace == null)
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = false,
                Encoding = new System.Text.UTF8Encoding(false)
            };

            using var xmlWriter = XmlWriter.Create(stream, settings);

            this.logger.LogInformation("Starting XMI serialization");

            xmlWriter.WriteStartDocument();

            this.WriteNamespaceElement(xmlWriter, @namespace, includeDerivedProperties, elementOriginMap, currentFileUri, includesImplied);

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            this.logger.LogInformation("XMI serialization completed");
        }

        /// <summary>
        /// Asynchronously serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag.
        /// </param>
        public Task SerializeAsync(INamespace @namespace, bool includeDerivedProperties, bool includesImplied, Stream stream, CancellationToken cancellationToken)
        {
            return Task.Run(() => this.Serialize(@namespace, includeDerivedProperties, includesImplied, stream), cancellationToken);
        }

        /// <summary>
        /// Serialize an <see cref="INamespace"/> to multiple XMI files based on the element origin map
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace"/> containing all elements</param>
        /// <param name="elementOriginMap">The <see cref="IXmiElementOriginMap"/> tracking element-to-file associations</param>
        /// <param name="outputDirectory">The target directory for output files</param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>
        public void Serialize(INamespace rootNamespace, IXmiElementOriginMap elementOriginMap, DirectoryInfo outputDirectory, bool includeDerivedProperties, bool includesImplied)
        {
            if (rootNamespace == null)
            {
                throw new ArgumentNullException(nameof(rootNamespace));
            }

            if (elementOriginMap == null)
            {
                throw new ArgumentNullException(nameof(elementOriginMap));
            }

            if (outputDirectory == null)
            {
                throw new ArgumentNullException(nameof(outputDirectory));
            }

            if (!outputDirectory.Exists)
            {
                outputDirectory.Create();
            }

            var sourceFiles = elementOriginMap.GetAllSourceFiles().ToList();

            this.logger.LogInformation("Starting multi-file XMI serialization for {FileCount} files", sourceFiles.Count);

            foreach (var sourceFile in sourceFiles)
            {
                var rootNamespaceId = elementOriginMap.GetRootNamespaceId(sourceFile);

                if (rootNamespaceId == Guid.Empty)
                {
                    this.logger.LogWarning("No root namespace found for source file {SourceFile}", sourceFile);
                    continue;
                }

                // Compute relative path from original source structure
                var fileName = Path.GetFileName(sourceFile.LocalPath);
                var outputPath = Path.Combine(outputDirectory.FullName, fileName);
                var outputUri = new Uri(outputPath);

                this.logger.LogInformation("Writing {FileName}", fileName);

                // Find the namespace POCO for this file's root namespace
                var fileRootNamespace = FindNamespaceById(rootNamespace, rootNamespaceId);

                if (fileRootNamespace == null)
                {
                    this.logger.LogWarning("Could not find namespace with id {NamespaceId} for file {SourceFile}", rootNamespaceId, sourceFile);
                    continue;
                }

                using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
                this.Serialize(fileRootNamespace, includeDerivedProperties, includesImplied, fileStream, elementOriginMap, outputUri);
            }

            this.logger.LogInformation("Multi-file XMI serialization completed");
        }

        private void WriteNamespaceElement(XmlWriter xmlWriter, INamespace @namespace, bool includeDerivedProperties, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool includesImplied)
        {
            xmlWriter.WriteStartElement("sysml", "Namespace", SysmlNamespace);
            xmlWriter.WriteAttributeString("xmlns", "xmi", null, XmiNamespace);
            xmlWriter.WriteAttributeString("xmlns", "xsi", null, XsiNamespace);
            xmlWriter.WriteAttributeString("xmi", "id", XmiNamespace, @namespace.Id.ToString());

            // Write scalar properties
            if (@namespace.AliasIds != null && @namespace.AliasIds.Count > 0)
            {
                xmlWriter.WriteAttributeString("aliasIds", string.Join(" ", @namespace.AliasIds));
            }

            if (!string.IsNullOrEmpty(@namespace.DeclaredName))
            {
                xmlWriter.WriteAttributeString("declaredName", @namespace.DeclaredName);
            }

            if (!string.IsNullOrEmpty(@namespace.DeclaredShortName))
            {
                xmlWriter.WriteAttributeString("declaredShortName", @namespace.DeclaredShortName);
            }

            if (!string.IsNullOrEmpty(@namespace.ElementId))
            {
                xmlWriter.WriteAttributeString("elementId", @namespace.ElementId);
            }

            if (includesImplied || @namespace.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }

            // Write owned relationships as child elements
            if (@namespace.OwnedRelationship != null)
            {
                foreach (var relationship in @namespace.OwnedRelationship)
                {
                    if (!includesImplied && relationship is IRelationship { IsImplied: true })
                    {
                        continue;
                    }

                    if (relationship is IData relationshipData)
                    {
                        if (elementOriginMap != null && currentFileUri != null)
                        {
                            var childSourceFile = elementOriginMap.GetSourceFile(relationshipData.Id);

                            if (childSourceFile != null && childSourceFile != currentFileUri)
                            {
                                var relativePath = currentFileUri.MakeRelativeUri(childSourceFile);
                                var href = $"{Uri.UnescapeDataString(relativePath.ToString())}#{relationshipData.Id}";

                                xmlWriter.WriteStartElement("ownedRelationship");
                                xmlWriter.WriteAttributeString("href", href);
                                xmlWriter.WriteEndElement();
                                continue;
                            }
                        }

                        this.xmiWriterFacade.Write(xmlWriter, relationshipData, "ownedRelationship", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            xmlWriter.WriteEndElement();
        }

        private static INamespace FindNamespaceById(INamespace root, Guid id)
        {
            if (root.Id == id)
            {
                return root;
            }

            if (root.OwnedRelationship == null)
            {
                return null;
            }

            foreach (var relationship in root.OwnedRelationship)
            {
                if (relationship is IElement element)
                {
                    if (element.OwnedRelationship != null)
                    {
                        foreach (var child in element.OwnedRelationship)
                        {
                            if (child is INamespace childNamespace)
                            {
                                var found = FindNamespaceById(childNamespace, id);

                                if (found != null)
                                {
                                    return found;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
