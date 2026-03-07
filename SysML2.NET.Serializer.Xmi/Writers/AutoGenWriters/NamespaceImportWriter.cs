// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceImportWriter.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi.Writers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Serializer.Xmi.Extensions;

    /// <summary>
    /// The purpose of the <see cref="NamespaceImportWriter" /> is to write an instance of <see cref="INamespaceImport" />
    /// to the XMI document
    /// </summary>
    public class NamespaceImportWriter : XmiDataWriter<INamespaceImport>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" />
        /// </summary>
        private readonly ILogger<NamespaceImportWriter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceImportWriter" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        public NamespaceImportWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory) : base(xmiDataWriterFacade, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<NamespaceImportWriter>.Instance : loggerFactory.CreateLogger<NamespaceImportWriter>();
        }

        /// <summary>
        /// Writes the <see cref="INamespaceImport" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="INamespaceImport" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions" /> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public override void Write(XmlWriter xmlWriter, INamespaceImport poco, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:NamespaceImport");
            xmlWriter.WriteAttributeString("xmi", "id", null, poco.Id.ToString());

            // Scalar properties as XML attributes (sorted alphabetically)
            if (poco.AliasIds != null && poco.AliasIds.Count > 0)
            {
                xmlWriter.WriteAttributeString("aliasIds", string.Join(" ", poco.AliasIds));
            }

            if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                xmlWriter.WriteAttributeString("declaredName", poco.DeclaredName);
            }

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName))
            {
                xmlWriter.WriteAttributeString("declaredShortName", poco.DeclaredShortName);
            }

            if (!string.IsNullOrWhiteSpace(poco.ElementId))
            {
                xmlWriter.WriteAttributeString("elementId", poco.ElementId);
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.importedElement != null && poco.importedElement.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("importedElement", poco.importedElement.Id.ToString());
                }
            }

            if (writerOptions.WriteIdRefAsAttribute && poco.ImportedNamespace != null && poco.ImportedNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
            {
                xmlWriter.WriteAttributeString("importedNamespace", poco.ImportedNamespace.Id.ToString());
            }

            if (poco.IsImplied)
            {
                xmlWriter.WriteAttributeString("isImplied", "true");
            }

            if (writerOptions.IncludeImplied || poco.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }

            if (poco.IsImportAll)
            {
                xmlWriter.WriteAttributeString("isImportAll", "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    xmlWriter.WriteAttributeString("isLibraryElement", "true");
                }
            }

            if (poco.IsRecursive)
            {
                xmlWriter.WriteAttributeString("isRecursive", "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    xmlWriter.WriteAttributeString("name", poco.name);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owner != null && poco.owner.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("owner", poco.owner.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningNamespace != null && poco.owningNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("owningNamespace", poco.owningNamespace.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.qualifiedName))
                {
                    xmlWriter.WriteAttributeString("qualifiedName", poco.qualifiedName);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.shortName))
                {
                    xmlWriter.WriteAttributeString("shortName", poco.shortName);
                }
            }

            if (poco.Visibility != VisibilityKind.Private)
            {
                xmlWriter.WriteAttributeString("visibility", poco.Visibility.ToString().ToLower());
            }


            // Reference/containment properties as child elements (sorted alphabetically)
            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.documentation != null)
                {
                    foreach (var item in poco.documentation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "documentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.importedElement != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.importedElement.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.importedElement, "importedElement", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (poco.ImportedNamespace != null)
            {
                if (!writerOptions.WriteIdRefAsAttribute || !poco.ImportedNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.ImportedNamespace, "importedNamespace", elementOriginMap, currentFileUri);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedAnnotation", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedElement != null)
                {
                    foreach (var item in poco.ownedElement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedElement", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (poco.OwnedRelatedElement != null)
            {
                foreach (var item in poco.OwnedRelatedElement)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelatedElement", writerOptions, elementOriginMap, currentFileUri, true);
                }
            }

            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelationship", writerOptions, elementOriginMap, currentFileUri, true);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owner != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owner.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owner, "owner", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningNamespace != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.relatedElement != null)
                {
                    foreach (var item in poco.relatedElement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "relatedElement", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.textualRepresentation != null)
                {
                    foreach (var item in poco.textualRepresentation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "textualRepresentation", elementOriginMap, currentFileUri);
                    }
                }
            }


            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes the <see cref="INamespaceImport" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="INamespaceImport" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        /// <returns>An awaitable <see cref="Task" /></returns>
        public override async Task WriteAsync(XmlWriter xmlWriter, INamespaceImport poco, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, "sysml:NamespaceImport");
            await xmlWriter.WriteAttributeStringAsync("xmi", "id", null, poco.Id.ToString());

            // Scalar properties as XML attributes (sorted alphabetically)
            if (poco.AliasIds != null && poco.AliasIds.Count > 0)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "aliasIds", null, string.Join(" ", poco.AliasIds));
            }

            if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "declaredName", null, poco.DeclaredName);
            }

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "declaredShortName", null, poco.DeclaredShortName);
            }

            if (!string.IsNullOrWhiteSpace(poco.ElementId))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "elementId", null, poco.ElementId);
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.importedElement != null && poco.importedElement.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "importedElement", null, poco.importedElement.Id.ToString());
                }
            }

            if (writerOptions.WriteIdRefAsAttribute && poco.ImportedNamespace != null && poco.ImportedNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "importedNamespace", null, poco.ImportedNamespace.Id.ToString());
            }

            if (poco.IsImplied)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImplied", null, "true");
            }

            if (writerOptions.IncludeImplied || poco.IsImpliedIncluded)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImpliedIncluded", null, "true");
            }

            if (poco.IsImportAll)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImportAll", null, "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "isLibraryElement", null, "true");
                }
            }

            if (poco.IsRecursive)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isRecursive", null, "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "name", null, poco.name);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owner != null && poco.owner.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "owner", null, poco.owner.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningNamespace != null && poco.owningNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "owningNamespace", null, poco.owningNamespace.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.qualifiedName))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "qualifiedName", null, poco.qualifiedName);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.shortName))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "shortName", null, poco.shortName);
                }
            }

            if (poco.Visibility != VisibilityKind.Private)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "visibility", null, poco.Visibility.ToString().ToLower());
            }


            // Reference/containment properties as child elements (sorted alphabetically)
            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.documentation != null)
                {
                    foreach (var item in poco.documentation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "documentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.importedElement != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.importedElement.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.importedElement, "importedElement", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (poco.ImportedNamespace != null)
            {
                if (!writerOptions.WriteIdRefAsAttribute || !poco.ImportedNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.ImportedNamespace, "importedNamespace", elementOriginMap, currentFileUri);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedAnnotation", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedElement != null)
                {
                    foreach (var item in poco.ownedElement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "ownedElement", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (poco.OwnedRelatedElement != null)
            {
                foreach (var item in poco.OwnedRelatedElement)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedRelatedElement", writerOptions, elementOriginMap, currentFileUri, true);
                }
            }

            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedRelationship", writerOptions, elementOriginMap, currentFileUri, true);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owner != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owner.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.owner, "owner", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningNamespace != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.relatedElement != null)
                {
                    foreach (var item in poco.relatedElement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "relatedElement", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.textualRepresentation != null)
                {
                    foreach (var item in poco.textualRepresentation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "textualRepresentation", elementOriginMap, currentFileUri);
                    }
                }
            }


            await xmlWriter.WriteEndElementAsync();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
