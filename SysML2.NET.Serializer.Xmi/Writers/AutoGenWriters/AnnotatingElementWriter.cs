// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="AnnotatingElementWriter" /> is to write an instance of <see cref="IAnnotatingElement" />
    /// to the XMI document
    /// </summary>
    public class AnnotatingElementWriter : XmiDataWriter<IAnnotatingElement>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" />
        /// </summary>
        private readonly ILogger<AnnotatingElementWriter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnotatingElementWriter" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        public AnnotatingElementWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory) : base(xmiDataWriterFacade, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<AnnotatingElementWriter>.Instance : loggerFactory.CreateLogger<AnnotatingElementWriter>();
        }

        /// <summary>
        /// Writes the <see cref="IAnnotatingElement" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IAnnotatingElement" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public override void Write(XmlWriter xmlWriter, IAnnotatingElement poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:AnnotatingElement");
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

            if (includesImplied || poco.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }

            if (includeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    xmlWriter.WriteAttributeString("isLibraryElement", "true");
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    xmlWriter.WriteAttributeString("name", poco.name);
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.qualifiedName))
                {
                    xmlWriter.WriteAttributeString("qualifiedName", poco.qualifiedName);
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.shortName))
                {
                    xmlWriter.WriteAttributeString("shortName", poco.shortName);
                }
            }


            // Reference/containment properties as child elements (sorted alphabetically)
            if (includeDerivedProperties)
            {
                if (poco.annotatedElement != null)
                {
                    foreach (var item in poco.annotatedElement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "annotatedElement", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.annotation != null)
                {
                    foreach (var item in poco.annotation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "annotation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.documentation != null)
                {
                    foreach (var item in poco.documentation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "documentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedAnnotatingRelationship != null)
                {
                    foreach (var item in poco.ownedAnnotatingRelationship)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedAnnotatingRelationship", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedAnnotation", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedElement != null)
                {
                    foreach (var item in poco.ownedElement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedElement", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelationship", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owner != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owner, "owner", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owningAnnotatingRelationship != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningAnnotatingRelationship, "owningAnnotatingRelationship", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owningMembership != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningMembership, "owningMembership", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owningNamespace != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                }
            }

            if (poco.OwningRelationship != null)
            {
                this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            if (includeDerivedProperties)
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
        /// Asynchronously writes the <see cref="IAnnotatingElement" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IAnnotatingElement" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>        
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        /// <returns>An awaitable <see cref="Task" /></returns>
        public override async Task WriteAsync(XmlWriter xmlWriter, IAnnotatingElement poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, "sysml:AnnotatingElement");
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

            if (includesImplied || poco.IsImpliedIncluded)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImpliedIncluded", null, "true");
            }

            if (includeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "isLibraryElement", null, "true");
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "name", null, poco.name);
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.qualifiedName))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "qualifiedName", null, poco.qualifiedName);
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.shortName))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "shortName", null, poco.shortName);
                }
            }


            // Reference/containment properties as child elements (sorted alphabetically)
            if (includeDerivedProperties)
            {
                if (poco.annotatedElement != null)
                {
                    foreach (var item in poco.annotatedElement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "annotatedElement", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.annotation != null)
                {
                    foreach (var item in poco.annotation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "annotation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.documentation != null)
                {
                    foreach (var item in poco.documentation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "documentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedAnnotatingRelationship != null)
                {
                    foreach (var item in poco.ownedAnnotatingRelationship)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedAnnotatingRelationship", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedAnnotation", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedElement != null)
                {
                    foreach (var item in poco.ownedElement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedElement", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedRelationship", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owner != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.owner, "owner", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owningAnnotatingRelationship != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.owningAnnotatingRelationship, "owningAnnotatingRelationship", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owningMembership != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.owningMembership, "owningMembership", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owningNamespace != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                }
            }

            if (poco.OwningRelationship != null)
            {
                await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            if (includeDerivedProperties)
            {
                if (poco.textualRepresentation != null)
                {
                    foreach (var item in poco.textualRepresentation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "textualRepresentation", elementOriginMap, currentFileUri);
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
