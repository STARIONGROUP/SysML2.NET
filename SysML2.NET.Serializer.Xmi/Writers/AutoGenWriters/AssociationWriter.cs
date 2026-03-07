// -------------------------------------------------------------------------------------------------
// <copyright file="AssociationWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="AssociationWriter" /> is to write an instance of <see cref="IAssociation" />
    /// to the XMI document
    /// </summary>
    public class AssociationWriter : XmiDataWriter<IAssociation>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" />
        /// </summary>
        private readonly ILogger<AssociationWriter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociationWriter" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        public AssociationWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory) : base(xmiDataWriterFacade, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<AssociationWriter>.Instance : loggerFactory.CreateLogger<AssociationWriter>();
        }

        /// <summary>
        /// Writes the <see cref="IAssociation" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IAssociation" /> to write</param>
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
        public override void Write(XmlWriter xmlWriter, IAssociation poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:Association");
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

            if (poco.IsAbstract)
            {
                xmlWriter.WriteAttributeString("isAbstract", "true");
            }

            if (includeDerivedProperties)
            {
                if (poco.isConjugated)
                {
                    xmlWriter.WriteAttributeString("isConjugated", "true");
                }
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

            if (poco.IsSufficient)
            {
                xmlWriter.WriteAttributeString("isSufficient", "true");
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
                if (poco.associationEnd != null)
                {
                    foreach (var item in poco.associationEnd)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "associationEnd", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.differencingType != null)
                {
                    foreach (var item in poco.differencingType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "differencingType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.directedFeature != null)
                {
                    foreach (var item in poco.directedFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "directedFeature", elementOriginMap, currentFileUri);
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
                if (poco.feature != null)
                {
                    foreach (var item in poco.feature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "feature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.featureMembership != null)
                {
                    foreach (var item in poco.featureMembership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "featureMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.importedMembership != null)
                {
                    foreach (var item in poco.importedMembership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "importedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.inheritedFeature != null)
                {
                    foreach (var item in poco.inheritedFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "inheritedFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.inheritedMembership != null)
                {
                    foreach (var item in poco.inheritedMembership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "inheritedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.input != null)
                {
                    foreach (var item in poco.input)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "input", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.intersectingType != null)
                {
                    foreach (var item in poco.intersectingType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "intersectingType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.member != null)
                {
                    foreach (var item in poco.member)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "member", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.membership != null)
                {
                    foreach (var item in poco.membership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "membership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.multiplicity != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.multiplicity, "multiplicity", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.output != null)
                {
                    foreach (var item in poco.output)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "output", elementOriginMap, currentFileUri);
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
                if (poco.ownedConjugator != null)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedConjugator, "ownedConjugator", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedDifferencing != null)
                {
                    foreach (var item in poco.ownedDifferencing)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedDifferencing", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedDisjoining != null)
                {
                    foreach (var item in poco.ownedDisjoining)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedDisjoining", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
                if (poco.ownedEndFeature != null)
                {
                    foreach (var item in poco.ownedEndFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedEndFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedFeature != null)
                {
                    foreach (var item in poco.ownedFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedFeatureMembership != null)
                {
                    foreach (var item in poco.ownedFeatureMembership)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureMembership", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedImport != null)
                {
                    foreach (var item in poco.ownedImport)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedImport", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedIntersecting != null)
                {
                    foreach (var item in poco.ownedIntersecting)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedIntersecting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedMember != null)
                {
                    foreach (var item in poco.ownedMember)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedMember", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedMembership != null)
                {
                    foreach (var item in poco.ownedMembership)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedMembership", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedSpecialization != null)
                {
                    foreach (var item in poco.ownedSpecialization)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSpecialization", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedSubclassification != null)
                {
                    foreach (var item in poco.ownedSubclassification)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSubclassification", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedUnioning != null)
                {
                    foreach (var item in poco.ownedUnioning)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedUnioning", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
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
                if (poco.relatedType != null)
                {
                    foreach (var item in poco.relatedType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "relatedType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.sourceType != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.sourceType, "sourceType", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.targetType != null)
                {
                    foreach (var item in poco.targetType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "targetType", elementOriginMap, currentFileUri);
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

            if (includeDerivedProperties)
            {
                if (poco.unioningType != null)
                {
                    foreach (var item in poco.unioningType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "unioningType", elementOriginMap, currentFileUri);
                    }
                }
            }


            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes the <see cref="IAssociation" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IAssociation" /> to write</param>
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
        public override async Task WriteAsync(XmlWriter xmlWriter, IAssociation poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, "sysml:Association");
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

            if (poco.IsAbstract)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isAbstract", null, "true");
            }

            if (includeDerivedProperties)
            {
                if (poco.isConjugated)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "isConjugated", null, "true");
                }
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

            if (poco.IsSufficient)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isSufficient", null, "true");
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
                if (poco.associationEnd != null)
                {
                    foreach (var item in poco.associationEnd)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "associationEnd", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.differencingType != null)
                {
                    foreach (var item in poco.differencingType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "differencingType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.directedFeature != null)
                {
                    foreach (var item in poco.directedFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "directedFeature", elementOriginMap, currentFileUri);
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
                if (poco.feature != null)
                {
                    foreach (var item in poco.feature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "feature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.featureMembership != null)
                {
                    foreach (var item in poco.featureMembership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "featureMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.importedMembership != null)
                {
                    foreach (var item in poco.importedMembership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "importedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.inheritedFeature != null)
                {
                    foreach (var item in poco.inheritedFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "inheritedFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.inheritedMembership != null)
                {
                    foreach (var item in poco.inheritedMembership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "inheritedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.input != null)
                {
                    foreach (var item in poco.input)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "input", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.intersectingType != null)
                {
                    foreach (var item in poco.intersectingType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "intersectingType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.member != null)
                {
                    foreach (var item in poco.member)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "member", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.membership != null)
                {
                    foreach (var item in poco.membership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "membership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.multiplicity != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.multiplicity, "multiplicity", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.output != null)
                {
                    foreach (var item in poco.output)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "output", elementOriginMap, currentFileUri);
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
                if (poco.ownedConjugator != null)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)poco.ownedConjugator, "ownedConjugator", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedDifferencing != null)
                {
                    foreach (var item in poco.ownedDifferencing)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedDifferencing", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedDisjoining != null)
                {
                    foreach (var item in poco.ownedDisjoining)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedDisjoining", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
                if (poco.ownedEndFeature != null)
                {
                    foreach (var item in poco.ownedEndFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedEndFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedFeature != null)
                {
                    foreach (var item in poco.ownedFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedFeatureMembership != null)
                {
                    foreach (var item in poco.ownedFeatureMembership)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedFeatureMembership", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedImport != null)
                {
                    foreach (var item in poco.ownedImport)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedImport", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedIntersecting != null)
                {
                    foreach (var item in poco.ownedIntersecting)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedIntersecting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedMember != null)
                {
                    foreach (var item in poco.ownedMember)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedMember", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedMembership != null)
                {
                    foreach (var item in poco.ownedMembership)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedMembership", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedSpecialization != null)
                {
                    foreach (var item in poco.ownedSpecialization)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedSpecialization", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedSubclassification != null)
                {
                    foreach (var item in poco.ownedSubclassification)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedSubclassification", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedUnioning != null)
                {
                    foreach (var item in poco.ownedUnioning)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedUnioning", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
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
                if (poco.relatedType != null)
                {
                    foreach (var item in poco.relatedType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "relatedType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.sourceType != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.sourceType, "sourceType", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.targetType != null)
                {
                    foreach (var item in poco.targetType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "targetType", elementOriginMap, currentFileUri);
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

            if (includeDerivedProperties)
            {
                if (poco.unioningType != null)
                {
                    foreach (var item in poco.unioningType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "unioningType", elementOriginMap, currentFileUri);
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
