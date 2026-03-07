// -------------------------------------------------------------------------------------------------
// <copyright file="ReturnParameterMembershipWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="ReturnParameterMembershipWriter" /> is to write an instance of <see cref="IReturnParameterMembership" />
    /// to the XMI document
    /// </summary>
    public class ReturnParameterMembershipWriter : XmiDataWriter<IReturnParameterMembership>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" />
        /// </summary>
        private readonly ILogger<ReturnParameterMembershipWriter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnParameterMembershipWriter" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        public ReturnParameterMembershipWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory) : base(xmiDataWriterFacade, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<ReturnParameterMembershipWriter>.Instance : loggerFactory.CreateLogger<ReturnParameterMembershipWriter>();
        }

        /// <summary>
        /// Writes the <see cref="IReturnParameterMembership" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IReturnParameterMembership" /> to write</param>
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
        public override void Write(XmlWriter xmlWriter, IReturnParameterMembership poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:ReturnParameterMembership");
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

            if (poco.IsImplied)
            {
                xmlWriter.WriteAttributeString("isImplied", "true");
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
                if (!string.IsNullOrWhiteSpace(poco.ownedMemberElementId))
                {
                    xmlWriter.WriteAttributeString("ownedMemberElementId", poco.ownedMemberElementId);
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.ownedMemberName))
                {
                    xmlWriter.WriteAttributeString("ownedMemberName", poco.ownedMemberName);
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.ownedMemberShortName))
                {
                    xmlWriter.WriteAttributeString("ownedMemberShortName", poco.ownedMemberShortName);
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

            xmlWriter.WriteAttributeString("visibility", poco.Visibility.ToString());


            // Reference/containment properties as child elements (sorted alphabetically)
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

            if (includeDerivedProperties)
            {
                if (poco.ownedMemberParameter != null)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedMemberParameter, "ownedMemberParameter", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                }
            }

            if (poco.OwnedRelatedElement != null)
            {
                foreach (var item in poco.OwnedRelatedElement)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelatedElement", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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

            if (poco.OwningRelatedElement != null)
            {
                this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.OwningRelatedElement, "owningRelatedElement", elementOriginMap, currentFileUri);
            }

            if (poco.OwningRelationship != null)
            {
                this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            if (includeDerivedProperties)
            {
                if (poco.owningType != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningType, "owningType", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.relatedElement != null)
                {
                    foreach (var item in poco.relatedElement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "relatedElement", elementOriginMap, currentFileUri);
                    }
                }
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
        /// Asynchronously writes the <see cref="IReturnParameterMembership" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IReturnParameterMembership" /> to write</param>
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
        public override async Task WriteAsync(XmlWriter xmlWriter, IReturnParameterMembership poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, "sysml:ReturnParameterMembership");
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

            if (poco.IsImplied)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImplied", null, "true");
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
                if (!string.IsNullOrWhiteSpace(poco.ownedMemberElementId))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "ownedMemberElementId", null, poco.ownedMemberElementId);
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.ownedMemberName))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "ownedMemberName", null, poco.ownedMemberName);
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.ownedMemberShortName))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "ownedMemberShortName", null, poco.ownedMemberShortName);
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

            await xmlWriter.WriteAttributeStringAsync(null, "visibility", null, poco.Visibility.ToString());


            // Reference/containment properties as child elements (sorted alphabetically)
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

            if (includeDerivedProperties)
            {
                if (poco.ownedMemberParameter != null)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)poco.ownedMemberParameter, "ownedMemberParameter", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                }
            }

            if (poco.OwnedRelatedElement != null)
            {
                foreach (var item in poco.OwnedRelatedElement)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedRelatedElement", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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

            if (poco.OwningRelatedElement != null)
            {
                await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.OwningRelatedElement, "owningRelatedElement", elementOriginMap, currentFileUri);
            }

            if (poco.OwningRelationship != null)
            {
                await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            if (includeDerivedProperties)
            {
                if (poco.owningType != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.owningType, "owningType", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.relatedElement != null)
                {
                    foreach (var item in poco.relatedElement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "relatedElement", elementOriginMap, currentFileUri);
                    }
                }
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
