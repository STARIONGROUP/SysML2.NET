// -------------------------------------------------------------------------------------------------
// <copyright file="DocumentationWriter.cs" company="Starion Group S.A.">
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
    using System.Xml;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="DocumentationWriter" /> is to write an instance of <see cref="IDocumentation" />
    /// to the XMI document
    /// </summary>
    public static class DocumentationWriter
    {
        /// <summary>
        /// Writes the <see cref="IDocumentation" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IDocumentation" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="xmiWriterFacade">The <see cref="IXmiDataWriterFacade" /> for writing child elements</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public static void Write(XmlWriter xmlWriter, IDocumentation poco, string elementName, bool includeDerivedProperties, IXmiDataWriterFacade xmiWriterFacade, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:Documentation");
            xmlWriter.WriteAttributeString("xmi", "id", null, poco.Id.ToString());

            // Scalar properties as XML attributes
            if (poco.AliasIds != null && poco.AliasIds.Count > 0)
            {
                xmlWriter.WriteAttributeString("aliasIds", string.Join(" ", poco.AliasIds));
            }
            if (!string.IsNullOrWhiteSpace(poco.Body))
            {
                xmlWriter.WriteAttributeString("body", poco.Body);
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
            if (poco.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }
            if (!string.IsNullOrWhiteSpace(poco.Locale))
            {
                xmlWriter.WriteAttributeString("locale", poco.Locale);
            }

            // Derived scalar properties
            if (includeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    xmlWriter.WriteAttributeString("isLibraryElement", "true");
                }
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    xmlWriter.WriteAttributeString("name", poco.name);
                }
                if (!string.IsNullOrWhiteSpace(poco.qualifiedName))
                {
                    xmlWriter.WriteAttributeString("qualifiedName", poco.qualifiedName);
                }
                if (!string.IsNullOrWhiteSpace(poco.shortName))
                {
                    xmlWriter.WriteAttributeString("shortName", poco.shortName);
                }
            }

            // Reference/containment properties as child elements
            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelationship", includeDerivedProperties, elementOriginMap, currentFileUri);
                }
            }
            if (poco.OwningRelationship != null)
            {
                xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            // Derived reference/containment properties as child elements
            if (includeDerivedProperties)
            {
                if (poco.annotation != null)
                {
                    foreach (var item in poco.annotation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "annotation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.documentation != null)
                {
                    foreach (var item in poco.documentation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "documentation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.documentedElement != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.documentedElement, "documentedElement", elementOriginMap, currentFileUri);
                }
                if (poco.ownedAnnotatingRelationship != null)
                {
                    foreach (var item in poco.ownedAnnotatingRelationship)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedAnnotatingRelationship", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedAnnotation", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedElement != null)
                {
                    foreach (var item in poco.ownedElement)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedElement", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.owner != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owner, "owner", elementOriginMap, currentFileUri);
                }
                if (poco.owningAnnotatingRelationship != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningAnnotatingRelationship, "owningAnnotatingRelationship", elementOriginMap, currentFileUri);
                }
                if (poco.owningMembership != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningMembership, "owningMembership", elementOriginMap, currentFileUri);
                }
                if (poco.owningNamespace != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                }
                if (poco.textualRepresentation != null)
                {
                    foreach (var item in poco.textualRepresentation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "textualRepresentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            xmlWriter.WriteEndElement();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
