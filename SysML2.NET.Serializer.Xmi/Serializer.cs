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
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.Logging;

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
        /// <summary>
        /// Gets the URI of the XMI namespace location
        /// </summary>
        private const string XmiNamespace = "http://www.omg.org/spec/XMI/20131001";
        
        /// <summary>
        /// Gets the URI of the XSI namespace location
        /// </summary>
        private const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
        
        /// <summary>
        /// Gets the URI of the SysML namespace location
        /// </summary>
        private const string SysmlNamespace = "https://www.omg.org/spec/SysML/20250201";

        /// <summary>
        /// Gets the <see cref="ILogger{TCategoryName}"/> instance to perform logging statement
        /// </summary>
        private readonly ILogger<Serializer> logger;
        
        /// <summary>
        /// Gets the <see cref="IXmiDataWriterFacade"/> to dispatch <see cref="IElement"/> writing
        /// </summary>
        private readonly XmiDataWriterFacade xmiWriterFacade;

        /// <summary>Initializes a new instance of the <see cref="Serializer"></see> class.</summary>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory " /> used to set up logging</param>
        public Serializer(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<Serializer>();
            
            this.xmiWriterFacade = new XmiDataWriterFacade(loggerFactory);
        }

        /// <summary>
        /// Serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        public void Serialize(INamespace @namespace, XmiWriterOptions writerOptions, Stream stream)
        {
            this.Serialize(@namespace, writerOptions, stream, null, null);
        }

        /// <summary>
        /// Serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>,
        /// using an origin map for href reconstruction
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="elementOriginMap">
        /// The optional <see cref="IXmiElementOriginMap"/> for href reconstruction
        /// </param>
        /// <param name="currentFileUri">
        /// The optional <see cref="Uri"/> of the current output file for relative href computation
        /// </param>
        public void Serialize(INamespace @namespace, XmiWriterOptions writerOptions, Stream stream, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (@namespace == null)
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var sw = Stopwatch.StartNew();
            
            this.logger.LogInformation("Setting up XMI serialization");
            
            writerOptions ??= new XmiWriterOptions();
            
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

            this.WriteNamespaceElement(xmlWriter, @namespace, elementOriginMap, currentFileUri, writerOptions);

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            this.logger.LogInformation("XMI serialization completed in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);
        }

                /// <summary>
        /// Serialize an <see cref="INamespace"/> to multiple XMI files based on the element origin map
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace"/> containing all elements</param>
        /// <param name="elementOriginMap">The <see cref="IXmiElementOriginMap"/> tracking element-to-file associations</param>
        /// <param name="outputDirectory">The target directory for output files</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        public void Serialize(INamespace rootNamespace, IXmiElementOriginMap elementOriginMap, DirectoryInfo outputDirectory, XmiWriterOptions writerOptions)
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

            var sw = Stopwatch.StartNew();
            
            this.logger.LogInformation("Setting up multi-file XMI serialization");
            
            writerOptions ??= new XmiWriterOptions();

            if (!outputDirectory.Exists)
            {
                outputDirectory.Create();
            }

            var sourceFiles = elementOriginMap.GetAllSourceFiles().ToList();

            this.logger.LogInformation("Starting multi-file XMI serialization for {FileCount} files", sourceFiles.Count);

            // Build a flat index of all namespaces for O(1) lookup
            var namespaceIndex = new System.Collections.Generic.Dictionary<Guid, INamespace>();
            BuildNamespaceIndex(rootNamespace, namespaceIndex);

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

                this.logger.LogInformation("Writing XMI at: {FileName}", fileName);

                // Find the namespace POCO for this file's root namespace
                if (!namespaceIndex.TryGetValue(rootNamespaceId, out var fileRootNamespace))
                {
                    this.logger.LogWarning("Could not find namespace with id {NamespaceId} for file {SourceFile}", rootNamespaceId, sourceFile);
                    continue;
                }

                using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096);
                this.Serialize(fileRootNamespace, writerOptions, fileStream, elementOriginMap, outputUri);
            }

            this.logger.LogInformation("Multi-file XMI serialization completed in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);
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
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        public async Task SerializeAsync(INamespace @namespace, XmiWriterOptions writerOptions, Stream stream, CancellationToken cancellationToken)
        {
            await this.SerializeAsync(@namespace, writerOptions, stream, null, null, cancellationToken);
        }

        /// <summary>
        /// Asynchronously serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>,
        /// using an origin map for href reconstruction
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="elementOriginMap">
        /// The optional <see cref="IXmiElementOriginMap"/> for href reconstruction
        /// </param>
        /// <param name="currentFileUri">
        /// The optional <see cref="Uri"/> of the current output file for relative href computation
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        public async Task SerializeAsync(INamespace @namespace, XmiWriterOptions writerOptions, Stream stream, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, CancellationToken cancellationToken)
        {
            if (@namespace == null)
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var sw = Stopwatch.StartNew();
            
            this.logger.LogInformation("Setting up asynchronous XMI serialization");
            
            writerOptions ??= new XmiWriterOptions();

            var settings = new XmlWriterSettings
            {
                Async = true,
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n",
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = false,
                Encoding = new System.Text.UTF8Encoding(false)
            };

            using var xmlWriter = XmlWriter.Create(stream, settings);

            this.logger.LogInformation("Starting asynchronous XMI serialization");

            await xmlWriter.WriteStartDocumentAsync();

            await this.WriteNamespaceElementAsync(xmlWriter, @namespace, elementOriginMap, currentFileUri, writerOptions);

            await xmlWriter.WriteEndDocumentAsync();
            await xmlWriter.FlushAsync();

            this.logger.LogInformation("Asynchronous XMI serialization completed in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// Asynchronously serialize an <see cref="INamespace"/> to multiple XMI files based on the element origin map
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace"/> containing all elements</param>
        /// <param name="elementOriginMap">The <see cref="IXmiElementOriginMap"/> tracking element-to-file associations</param>
        /// <param name="outputDirectory">The target directory for output files</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        public async Task SerializeAsync(INamespace rootNamespace, IXmiElementOriginMap elementOriginMap, DirectoryInfo outputDirectory, XmiWriterOptions writerOptions, CancellationToken cancellationToken)
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

            var sw = Stopwatch.StartNew();

            this.logger.LogInformation("Setting up asynchronous multi-file XMI serialization");

            writerOptions ??= new XmiWriterOptions();

            if (!outputDirectory.Exists)
            {
                outputDirectory.Create();
            }

            var sourceFiles = elementOriginMap.GetAllSourceFiles().ToList();

            this.logger.LogInformation("Starting asynchronous multi-file XMI serialization for {FileCount} files", sourceFiles.Count);

            // Build a flat index of all namespaces for O(1) lookup
            var namespaceIndex = new System.Collections.Generic.Dictionary<Guid, INamespace>();
            BuildNamespaceIndex(rootNamespace, namespaceIndex);

            foreach (var sourceFile in sourceFiles)
            {
                cancellationToken.ThrowIfCancellationRequested();

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

                this.logger.LogInformation("Writing XMI at: {FileName}", fileName);

                // Find the namespace POCO for this file's root namespace
                if (!namespaceIndex.TryGetValue(rootNamespaceId, out var fileRootNamespace))
                {
                    this.logger.LogWarning("Could not find namespace with id {NamespaceId} for file {SourceFile}", rootNamespaceId, sourceFile);
                    continue;
                }
                
                await using var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
                await this.SerializeAsync(fileRootNamespace, writerOptions, fileStream, elementOriginMap, outputUri, cancellationToken);
            }

            this.logger.LogInformation("Asynchronous multi-file XMI serialization completed in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);
        }
        
        /// <summary>
        /// Writes the XML content for the root <see cref="INamespace"/>
        /// </summary>
        /// <param name="xmlWriter">The <see cref="XmlWriter"/> instance that provides XMI write features</param>
        /// <param name="namespace">The root <see cref="INamespace"/> to write</param>
        /// <param name="elementOriginMap">The <see cref="IXmiElementOriginMap"/> tracking element-to-file associations</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> that locates the output file</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        private void WriteNamespaceElement(XmlWriter xmlWriter, INamespace @namespace, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, XmiWriterOptions writerOptions)
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

            if (writerOptions.IncludeImplied || @namespace.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }

            // Write owned relationships as child elements
            if (@namespace.OwnedRelationship != null)
            {
                foreach (var relationship in @namespace.OwnedRelationship)
                {
                    if (!writerOptions.IncludeImplied  && relationship is { IsImplied: true })
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

                        this.xmiWriterFacade.Write(xmlWriter, relationshipData, "ownedRelationship", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes the XML content for the root <see cref="INamespace"/>
        /// </summary>
        /// <param name="xmlWriter">The <see cref="XmlWriter"/> instance that provides XMI write features</param>
        /// <param name="namespace">The root <see cref="INamespace"/> to write</param>
        /// <param name="elementOriginMap">The <see cref="IXmiElementOriginMap"/> tracking element-to-file associations</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> that locates the output file</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        private async Task WriteNamespaceElementAsync(XmlWriter xmlWriter, INamespace @namespace, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, XmiWriterOptions writerOptions)
        {
            await xmlWriter.WriteStartElementAsync("sysml", "Namespace", SysmlNamespace);
            await xmlWriter.WriteAttributeStringAsync("xmlns", "xmi", null, XmiNamespace);
            await xmlWriter.WriteAttributeStringAsync("xmlns", "xsi", null, XsiNamespace);
            await xmlWriter.WriteAttributeStringAsync("xmi", "id", XmiNamespace, @namespace.Id.ToString());

            // Write scalar properties
            if (@namespace.AliasIds != null && @namespace.AliasIds.Count > 0)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "aliasIds", null, string.Join(" ", @namespace.AliasIds));
            }

            if (!string.IsNullOrEmpty(@namespace.DeclaredName))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "declaredName", null, @namespace.DeclaredName);
            }

            if (!string.IsNullOrEmpty(@namespace.DeclaredShortName))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "declaredShortName", null, @namespace.DeclaredShortName);
            }

            if (!string.IsNullOrEmpty(@namespace.ElementId))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "elementId", null, @namespace.ElementId);
            }

            if (writerOptions.IncludeImplied || @namespace.IsImpliedIncluded)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImpliedIncluded", null, "true");
            }

            // Write owned relationships as child elements
            if (@namespace.OwnedRelationship != null)
            {
                foreach (var relationship in @namespace.OwnedRelationship)
                {
                    if (!writerOptions.IncludeImplied && relationship is { IsImplied: true })
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

                                await xmlWriter.WriteStartElementAsync(null, "ownedRelationship", null);
                                await xmlWriter.WriteAttributeStringAsync(null, "href", null, href);
                                await xmlWriter.WriteEndElementAsync();
                                continue;
                            }
                        }

                        await this.xmiWriterFacade.WriteAsync(xmlWriter, relationshipData, "ownedRelationship", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            await xmlWriter.WriteEndElementAsync();
        }

        /// <summary>
        /// Builds a flat index of all <see cref="INamespace"/> instances reachable from the root
        /// </summary>
        /// <param name="root">The root <see cref="INamespace"/> used as starting point</param>
        /// <param name="index">The dictionary to populate with namespace id to instance mappings</param>
        private static void BuildNamespaceIndex(INamespace root, System.Collections.Generic.Dictionary<Guid, INamespace> index)
        {
            index[root.Id] = root;

            if (root.OwnedRelationship == null)
            {
                return;
            }

            foreach (var relationship in root.OwnedRelationship)
            {
                if (relationship is IElement element && element.OwnedRelationship != null)
                {
                    foreach (var child in element.OwnedRelationship)
                    {
                        if (child is INamespace childNamespace)
                        {
                            BuildNamespaceIndex(childNamespace, index);
                        }
                    }
                }
            }
        }
    }
}
