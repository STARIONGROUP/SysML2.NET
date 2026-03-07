// -------------------------------------------------------------------------------------------------
// <copyright file="LibraryPackageWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Serializer.Xmi.Extensions;

    /// <summary>
    /// The purpose of the <see cref="LibraryPackageWriter" /> is to write an instance of <see cref="ILibraryPackage" />
    /// to the XMI document
    /// </summary>
    public class LibraryPackageWriter : XmiDataWriter<ILibraryPackage>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" />
        /// </summary>
        private readonly ILogger<LibraryPackageWriter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryPackageWriter" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        public LibraryPackageWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory) : base(xmiDataWriterFacade, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<LibraryPackageWriter>.Instance : loggerFactory.CreateLogger<LibraryPackageWriter>();
        }

        /// <summary>
        /// Writes the <see cref="ILibraryPackage" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="ILibraryPackage" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions" /> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public override void Write(XmlWriter xmlWriter, ILibraryPackage poco, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:LibraryPackage");
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

            if (writerOptions.IncludeImplied || poco.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    xmlWriter.WriteAttributeString("isLibraryElement", "true");
                }
            }

            if (poco.IsStandard)
            {
                xmlWriter.WriteAttributeString("isStandard", "true");
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
                if (poco.filterCondition != null)
                {
                    foreach (var item in poco.filterCondition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "filterCondition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.importedMembership != null)
                {
                    foreach (var item in poco.importedMembership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "importedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.member != null)
                {
                    foreach (var item in poco.member)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "member", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.membership != null)
                {
                    foreach (var item in poco.membership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "membership", elementOriginMap, currentFileUri);
                    }
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

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedImport != null)
                {
                    foreach (var item in poco.ownedImport)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedImport", writerOptions, elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedMember != null)
                {
                    foreach (var item in poco.ownedMember)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedMember", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedMembership != null)
                {
                    foreach (var item in poco.ownedMembership)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedMembership", writerOptions, elementOriginMap, currentFileUri);
                    }
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
        /// Asynchronously writes the <see cref="ILibraryPackage" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="ILibraryPackage" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        /// <returns>An awaitable <see cref="Task" /></returns>
        public override async Task WriteAsync(XmlWriter xmlWriter, ILibraryPackage poco, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, "sysml:LibraryPackage");
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

            if (writerOptions.IncludeImplied || poco.IsImpliedIncluded)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImpliedIncluded", null, "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "isLibraryElement", null, "true");
                }
            }

            if (poco.IsStandard)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isStandard", null, "true");
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
                if (poco.filterCondition != null)
                {
                    foreach (var item in poco.filterCondition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "filterCondition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.importedMembership != null)
                {
                    foreach (var item in poco.importedMembership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "importedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.member != null)
                {
                    foreach (var item in poco.member)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "member", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.membership != null)
                {
                    foreach (var item in poco.membership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "membership", elementOriginMap, currentFileUri);
                    }
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

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedImport != null)
                {
                    foreach (var item in poco.ownedImport)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedImport", writerOptions, elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedMember != null)
                {
                    foreach (var item in poco.ownedMember)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "ownedMember", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedMembership != null)
                {
                    foreach (var item in poco.ownedMembership)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedMembership", writerOptions, elementOriginMap, currentFileUri);
                    }
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
