// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceImportReader.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi.Readers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Extensions.Core;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="{this.Name}}Reader" /> is to read an instance of <see cref="I{this.Name}}" />
    /// from the XMI document
    /// </summary>
    public class NamespaceImportReader : XmiDataReader<INamespaceImport>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" /> 
        /// </summary>
        private readonly ILogger<NamespaceImportReader> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceImportReader" />
        /// </summary>
        /// <param name="cache">
        /// The injected <see cref="IXmiDataCache" /> used cache to store and resolve <see cref="IData" />
        /// </param>
        /// <param name="xmiDataReaderFacade">
        /// The injected <see cref="IXmiDataReaderFacade" /> that provides
        /// <see cref="XmiDataReader{TData}" /> resolve
        /// </param>
        /// <param name="externalReferenceService">The injected <see cref="IExternalReferenceService"/> used to register and process external references</param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> used to track element-to-file associations</param>
        public NamespaceImportReader(IXmiDataCache cache, IXmiDataReaderFacade xmiDataReaderFacade, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory, IXmiElementOriginMap elementOriginMap = null) : base(cache, xmiDataReaderFacade, externalReferenceService, loggerFactory, elementOriginMap)
        {
            this.logger = loggerFactory == null ? NullLogger<NamespaceImportReader>.Instance : loggerFactory.CreateLogger<NamespaceImportReader>();
        }

        /// <summary>
        /// Reads the <see cref="INamespaceImport" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>The read <see cref="INamespaceImport" /></returns>
        public override INamespaceImport Read(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            INamespaceImport poco = new SysML2.NET.Core.POCO.Root.Namespaces.NamespaceImport();

            if (xmiReader.MoveToContent() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading NamespaceImport at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:NamespaceImport")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the NamespaceImportReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "NamespaceImport", poco.Id);
                }

                this.ElementOriginMap?.Register(poco.Id, currentLocation);

                var aliasIdsXmlAttribute = xmiReader.GetAttribute("aliasIds");

                if (!string.IsNullOrWhiteSpace(aliasIdsXmlAttribute))
                {
                    foreach (var aliasIdsXmlAttributeValue in aliasIdsXmlAttribute.Split(SplitMultiReference, StringSplitOptions.RemoveEmptyEntries))
                    {
                        poco.AliasIds.Add(aliasIdsXmlAttributeValue);
                    }
                }

                var declaredNameXmlAttribute = xmiReader.GetAttribute("declaredName");

                if (!string.IsNullOrWhiteSpace(declaredNameXmlAttribute))
                {
                    poco.DeclaredName = declaredNameXmlAttribute;
                }

                var declaredShortNameXmlAttribute = xmiReader.GetAttribute("declaredShortName");

                if (!string.IsNullOrWhiteSpace(declaredShortNameXmlAttribute))
                {
                    poco.DeclaredShortName = declaredShortNameXmlAttribute;
                }

                var elementIdXmlAttribute = xmiReader.GetAttribute("elementId");

                if (!string.IsNullOrWhiteSpace(elementIdXmlAttribute))
                {
                    poco.ElementId = elementIdXmlAttribute;
                }

                var importedNamespaceXmlAttribute = xmiReader.GetAttribute("importedNamespace");

                if (!string.IsNullOrWhiteSpace(importedNamespaceXmlAttribute))
                {
                    if (Guid.TryParse(importedNamespaceXmlAttribute, out var importedNamespaceXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "importedNamespace", importedNamespaceXmlAttributeReference);
                    }
                    else
                    {
                        this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'importedNamespace' on element {ElementId}", importedNamespaceXmlAttribute, poco.Id);
                    }
                }

                var isImpliedXmlAttribute = xmiReader.GetAttribute("isImplied");

                if (!string.IsNullOrWhiteSpace(isImpliedXmlAttribute))
                {
                    if (bool.TryParse(isImpliedXmlAttribute, out var isImpliedXmlAttributeAsBool))
                    {
                        poco.IsImplied = isImpliedXmlAttributeAsBool;
                    }
                }

                var isImpliedIncludedXmlAttribute = xmiReader.GetAttribute("isImpliedIncluded");

                if (!string.IsNullOrWhiteSpace(isImpliedIncludedXmlAttribute))
                {
                    if (bool.TryParse(isImpliedIncludedXmlAttribute, out var isImpliedIncludedXmlAttributeAsBool))
                    {
                        poco.IsImpliedIncluded = isImpliedIncludedXmlAttributeAsBool;
                    }
                }

                var isImportAllXmlAttribute = xmiReader.GetAttribute("isImportAll");

                if (!string.IsNullOrWhiteSpace(isImportAllXmlAttribute))
                {
                    if (bool.TryParse(isImportAllXmlAttribute, out var isImportAllXmlAttributeAsBool))
                    {
                        poco.IsImportAll = isImportAllXmlAttributeAsBool;
                    }
                }

                var isRecursiveXmlAttribute = xmiReader.GetAttribute("isRecursive");

                if (!string.IsNullOrWhiteSpace(isRecursiveXmlAttribute))
                {
                    if (bool.TryParse(isRecursiveXmlAttribute, out var isRecursiveXmlAttributeAsBool))
                    {
                        poco.IsRecursive = isRecursiveXmlAttributeAsBool;
                    }
                }

                var ownedRelatedElementXmlAttribute = xmiReader.GetAttribute("ownedRelatedElement");

                if (!string.IsNullOrWhiteSpace(ownedRelatedElementXmlAttribute))
                {
                    var ownedRelatedElementXmlAttributeReferences = new List<Guid>();

                    foreach (var ownedRelatedElementXmlAttributeValue in ownedRelatedElementXmlAttribute.Split(SplitMultiReference, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (Guid.TryParse(ownedRelatedElementXmlAttributeValue, out var ownedRelatedElementXmlAttributeReference))
                        {
                            ownedRelatedElementXmlAttributeReferences.Add(ownedRelatedElementXmlAttributeReference);
                        }
                        else
                        {
                            this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'ownedRelatedElement' on element {ElementId}", ownedRelatedElementXmlAttributeValue, poco.Id);
                        }
                    }

                    if (ownedRelatedElementXmlAttributeReferences.Count != 0)
                    {
                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelatedElement", ownedRelatedElementXmlAttributeReferences);
                    }
                }

                var ownedRelationshipXmlAttribute = xmiReader.GetAttribute("ownedRelationship");

                if (!string.IsNullOrWhiteSpace(ownedRelationshipXmlAttribute))
                {
                    var ownedRelationshipXmlAttributeReferences = new List<Guid>();

                    foreach (var ownedRelationshipXmlAttributeValue in ownedRelationshipXmlAttribute.Split(SplitMultiReference, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (Guid.TryParse(ownedRelationshipXmlAttributeValue, out var ownedRelationshipXmlAttributeReference))
                        {
                            ownedRelationshipXmlAttributeReferences.Add(ownedRelationshipXmlAttributeReference);
                        }
                        else
                        {
                            this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'ownedRelationship' on element {ElementId}", ownedRelationshipXmlAttributeValue, poco.Id);
                        }
                    }

                    if (ownedRelationshipXmlAttributeReferences.Count != 0)
                    {
                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelationship", ownedRelationshipXmlAttributeReferences);
                    }
                }

                var owningRelatedElementXmlAttribute = xmiReader.GetAttribute("owningRelatedElement");

                if (!string.IsNullOrWhiteSpace(owningRelatedElementXmlAttribute))
                {
                    if (Guid.TryParse(owningRelatedElementXmlAttribute, out var owningRelatedElementXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelatedElement", owningRelatedElementXmlAttributeReference);
                    }
                    else
                    {
                        this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'owningRelatedElement' on element {ElementId}", owningRelatedElementXmlAttribute, poco.Id);
                    }
                }

                var owningRelationshipXmlAttribute = xmiReader.GetAttribute("owningRelationship");

                if (!string.IsNullOrWhiteSpace(owningRelationshipXmlAttribute))
                {
                    if (Guid.TryParse(owningRelationshipXmlAttribute, out var owningRelationshipXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipXmlAttributeReference);
                    }
                    else
                    {
                        this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'owningRelationship' on element {ElementId}", owningRelationshipXmlAttribute, poco.Id);
                    }
                }

                var visibilityXmlAttribute = xmiReader.GetAttribute("visibility");

                if (!string.IsNullOrWhiteSpace(visibilityXmlAttribute))
                {
                    if (VisibilityKindProvider.TryParse(visibilityXmlAttribute.AsSpan(), out var visibilityXmlAttributeAsEnum))
                    {
                        poco.Visibility = visibilityXmlAttributeAsEnum;
                    }
                }

                while (xmiReader.Read())
                {
                    if (xmiReader.NodeType == XmlNodeType.Element)
                    {
                        switch (xmiReader.LocalName)
                        {
                            case "aliasIds":
                                {
                                    var aliasIdsValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(aliasIdsValue))
                                    {
                                        poco.AliasIds.Add(aliasIdsValue);
                                    }

                                    break;
                                }

                            case "declaredName":
                                {
                                    var declaredNameValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(declaredNameValue))
                                    {
                                        poco.DeclaredName = declaredNameValue;
                                    }

                                    break;
                                }

                            case "declaredShortName":
                                {
                                    var declaredShortNameValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(declaredShortNameValue))
                                    {
                                        poco.DeclaredShortName = declaredShortNameValue;
                                    }

                                    break;
                                }

                            case "elementId":
                                {
                                    var elementIdValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(elementIdValue))
                                    {
                                        poco.ElementId = elementIdValue;
                                    }

                                    break;
                                }

                            case "importedNamespace":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var importedNamespaceId))
                                        {
                                            this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "importedNamespace", importedNamespaceId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'importedNamespace' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var importedNamespaceValue = (INamespace)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        poco.ImportedNamespace = importedNamespaceValue;
                                    }

                                    break;
                                }

                            case "isImplied":
                                {
                                    var isImpliedValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isImpliedValue))
                                    {
                                        if (bool.TryParse(isImpliedValue, out var isImpliedValueAsBool))
                                        {
                                            poco.IsImplied = isImpliedValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isImplied' on element {ElementId}", isImpliedValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isImpliedIncluded":
                                {
                                    var isImpliedIncludedValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isImpliedIncludedValue))
                                    {
                                        if (bool.TryParse(isImpliedIncludedValue, out var isImpliedIncludedValueAsBool))
                                        {
                                            poco.IsImpliedIncluded = isImpliedIncludedValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isImpliedIncluded' on element {ElementId}", isImpliedIncludedValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isImportAll":
                                {
                                    var isImportAllValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isImportAllValue))
                                    {
                                        if (bool.TryParse(isImportAllValue, out var isImportAllValueAsBool))
                                        {
                                            poco.IsImportAll = isImportAllValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isImportAll' on element {ElementId}", isImportAllValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isRecursive":
                                {
                                    var isRecursiveValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isRecursiveValue))
                                    {
                                        if (bool.TryParse(isRecursiveValue, out var isRecursiveValueAsBool))
                                        {
                                            poco.IsRecursive = isRecursiveValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isRecursive' on element {ElementId}", isRecursiveValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "ownedRelatedElement":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var ownedRelatedElementId))
                                        {
                                            this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelatedElement", ownedRelatedElementId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'ownedRelatedElement' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var ownedRelatedElementValue = (IElement)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        ((IContainedRelationship)poco).OwnedRelatedElement.Add(ownedRelatedElementValue);
                                    }

                                    break;
                                }

                            case "ownedRelationship":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var ownedRelationshipId))
                                        {
                                            this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelationship", ownedRelationshipId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'ownedRelationship' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var ownedRelationshipValue = (IRelationship)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        ((IContainedElement)poco).OwnedRelationship.Add(ownedRelationshipValue);
                                    }

                                    break;
                                }

                            case "owningRelatedElement":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var owningRelatedElementId))
                                        {
                                            this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelatedElement", owningRelatedElementId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'owningRelatedElement' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var owningRelatedElementValue = (IElement)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        ((IContainedRelationship)poco).OwningRelatedElement = owningRelatedElementValue;
                                    }

                                    break;
                                }

                            case "owningRelationship":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var owningRelationshipId))
                                        {
                                            this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'owningRelationship' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var owningRelationshipValue = (IRelationship)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        ((IContainedElement)poco).OwningRelationship = owningRelationshipValue;
                                    }

                                    break;
                                }

                            case "visibility":
                                {
                                    var visibilityValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(visibilityValue))
                                    {
                                        if (VisibilityKindProvider.TryParse(visibilityValue.AsSpan(), out var visibilityValueAsEnum))
                                        {
                                            poco.Visibility = visibilityValueAsEnum;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse enum value '{Value}' for property 'visibility' on element {ElementId}", visibilityValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            default:
                                this.logger.LogWarning("Unexpected element '{LocalName}' encountered while reading NamespaceImport at line:position {LineNumber}:{LinePosition}", xmiReader.LocalName, xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                                xmiReader.Skip();
                                break;
                        }
                    }
                }
            }

            return poco;
        }

        /// <summary>
        /// Reads asynchronously the <see cref="INamespaceImport" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/> with the read <see cref="INamespaceImport" /></returns>
        public override async Task<INamespaceImport> ReadAsync(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            INamespaceImport poco = new SysML2.NET.Core.POCO.Root.Namespaces.NamespaceImport();

            if (await xmiReader.MoveToContentAsync() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading NamespaceImport at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:NamespaceImport")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the NamespaceImportReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "NamespaceImport", poco.Id);
                }

                this.ElementOriginMap?.Register(poco.Id, currentLocation);

                var aliasIdsXmlAttribute = xmiReader.GetAttribute("aliasIds");

                if (!string.IsNullOrWhiteSpace(aliasIdsXmlAttribute))
                {
                    foreach (var aliasIdsXmlAttributeValue in aliasIdsXmlAttribute.Split(SplitMultiReference, StringSplitOptions.RemoveEmptyEntries))
                    {
                        poco.AliasIds.Add(aliasIdsXmlAttributeValue);
                    }
                }

                var declaredNameXmlAttribute = xmiReader.GetAttribute("declaredName");

                if (!string.IsNullOrWhiteSpace(declaredNameXmlAttribute))
                {
                    poco.DeclaredName = declaredNameXmlAttribute;
                }

                var declaredShortNameXmlAttribute = xmiReader.GetAttribute("declaredShortName");

                if (!string.IsNullOrWhiteSpace(declaredShortNameXmlAttribute))
                {
                    poco.DeclaredShortName = declaredShortNameXmlAttribute;
                }

                var elementIdXmlAttribute = xmiReader.GetAttribute("elementId");

                if (!string.IsNullOrWhiteSpace(elementIdXmlAttribute))
                {
                    poco.ElementId = elementIdXmlAttribute;
                }

                var importedNamespaceXmlAttribute = xmiReader.GetAttribute("importedNamespace");

                if (!string.IsNullOrWhiteSpace(importedNamespaceXmlAttribute))
                {
                    if (Guid.TryParse(importedNamespaceXmlAttribute, out var importedNamespaceXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "importedNamespace", importedNamespaceXmlAttributeReference);
                    }
                    else
                    {
                        this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'importedNamespace' on element {ElementId}", importedNamespaceXmlAttribute, poco.Id);
                    }
                }

                var isImpliedXmlAttribute = xmiReader.GetAttribute("isImplied");

                if (!string.IsNullOrWhiteSpace(isImpliedXmlAttribute))
                {
                    if (bool.TryParse(isImpliedXmlAttribute, out var isImpliedXmlAttributeAsBool))
                    {
                        poco.IsImplied = isImpliedXmlAttributeAsBool;
                    }
                }

                var isImpliedIncludedXmlAttribute = xmiReader.GetAttribute("isImpliedIncluded");

                if (!string.IsNullOrWhiteSpace(isImpliedIncludedXmlAttribute))
                {
                    if (bool.TryParse(isImpliedIncludedXmlAttribute, out var isImpliedIncludedXmlAttributeAsBool))
                    {
                        poco.IsImpliedIncluded = isImpliedIncludedXmlAttributeAsBool;
                    }
                }

                var isImportAllXmlAttribute = xmiReader.GetAttribute("isImportAll");

                if (!string.IsNullOrWhiteSpace(isImportAllXmlAttribute))
                {
                    if (bool.TryParse(isImportAllXmlAttribute, out var isImportAllXmlAttributeAsBool))
                    {
                        poco.IsImportAll = isImportAllXmlAttributeAsBool;
                    }
                }

                var isRecursiveXmlAttribute = xmiReader.GetAttribute("isRecursive");

                if (!string.IsNullOrWhiteSpace(isRecursiveXmlAttribute))
                {
                    if (bool.TryParse(isRecursiveXmlAttribute, out var isRecursiveXmlAttributeAsBool))
                    {
                        poco.IsRecursive = isRecursiveXmlAttributeAsBool;
                    }
                }

                var ownedRelatedElementXmlAttribute = xmiReader.GetAttribute("ownedRelatedElement");

                if (!string.IsNullOrWhiteSpace(ownedRelatedElementXmlAttribute))
                {
                    var ownedRelatedElementXmlAttributeReferences = new List<Guid>();

                    foreach (var ownedRelatedElementXmlAttributeValue in ownedRelatedElementXmlAttribute.Split(SplitMultiReference, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (Guid.TryParse(ownedRelatedElementXmlAttributeValue, out var ownedRelatedElementXmlAttributeReference))
                        {
                            ownedRelatedElementXmlAttributeReferences.Add(ownedRelatedElementXmlAttributeReference);
                        }
                        else
                        {
                            this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'ownedRelatedElement' on element {ElementId}", ownedRelatedElementXmlAttributeValue, poco.Id);
                        }
                    }

                    if (ownedRelatedElementXmlAttributeReferences.Count != 0)
                    {
                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelatedElement", ownedRelatedElementXmlAttributeReferences);
                    }
                }

                var ownedRelationshipXmlAttribute = xmiReader.GetAttribute("ownedRelationship");

                if (!string.IsNullOrWhiteSpace(ownedRelationshipXmlAttribute))
                {
                    var ownedRelationshipXmlAttributeReferences = new List<Guid>();

                    foreach (var ownedRelationshipXmlAttributeValue in ownedRelationshipXmlAttribute.Split(SplitMultiReference, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (Guid.TryParse(ownedRelationshipXmlAttributeValue, out var ownedRelationshipXmlAttributeReference))
                        {
                            ownedRelationshipXmlAttributeReferences.Add(ownedRelationshipXmlAttributeReference);
                        }
                        else
                        {
                            this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'ownedRelationship' on element {ElementId}", ownedRelationshipXmlAttributeValue, poco.Id);
                        }
                    }

                    if (ownedRelationshipXmlAttributeReferences.Count != 0)
                    {
                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelationship", ownedRelationshipXmlAttributeReferences);
                    }
                }

                var owningRelatedElementXmlAttribute = xmiReader.GetAttribute("owningRelatedElement");

                if (!string.IsNullOrWhiteSpace(owningRelatedElementXmlAttribute))
                {
                    if (Guid.TryParse(owningRelatedElementXmlAttribute, out var owningRelatedElementXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelatedElement", owningRelatedElementXmlAttributeReference);
                    }
                    else
                    {
                        this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'owningRelatedElement' on element {ElementId}", owningRelatedElementXmlAttribute, poco.Id);
                    }
                }

                var owningRelationshipXmlAttribute = xmiReader.GetAttribute("owningRelationship");

                if (!string.IsNullOrWhiteSpace(owningRelationshipXmlAttribute))
                {
                    if (Guid.TryParse(owningRelationshipXmlAttribute, out var owningRelationshipXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipXmlAttributeReference);
                    }
                    else
                    {
                        this.logger.LogWarning("Failed to parse GUID reference value '{Value}' for property 'owningRelationship' on element {ElementId}", owningRelationshipXmlAttribute, poco.Id);
                    }
                }

                var visibilityXmlAttribute = xmiReader.GetAttribute("visibility");

                if (!string.IsNullOrWhiteSpace(visibilityXmlAttribute))
                {
                    if (VisibilityKindProvider.TryParse(visibilityXmlAttribute.AsSpan(), out var visibilityXmlAttributeAsEnum))
                    {
                        poco.Visibility = visibilityXmlAttributeAsEnum;
                    }
                }

                while (await xmiReader.ReadAsync())
                {
                    if (xmiReader.NodeType == XmlNodeType.Element)
                    {
                        switch (xmiReader.LocalName)
                        {
                            case "aliasIds":
                                {
                                    var aliasIdsValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(aliasIdsValue))
                                    {
                                        poco.AliasIds.Add(aliasIdsValue);
                                    }

                                    break;
                                }

                            case "declaredName":
                                {
                                    var declaredNameValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(declaredNameValue))
                                    {
                                        poco.DeclaredName = declaredNameValue;
                                    }

                                    break;
                                }

                            case "declaredShortName":
                                {
                                    var declaredShortNameValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(declaredShortNameValue))
                                    {
                                        poco.DeclaredShortName = declaredShortNameValue;
                                    }

                                    break;
                                }

                            case "elementId":
                                {
                                    var elementIdValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(elementIdValue))
                                    {
                                        poco.ElementId = elementIdValue;
                                    }

                                    break;
                                }

                            case "importedNamespace":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var importedNamespaceId))
                                        {
                                            this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "importedNamespace", importedNamespaceId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'importedNamespace' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var importedNamespaceValue = (INamespace)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        poco.ImportedNamespace = importedNamespaceValue;
                                    }

                                    break;
                                }

                            case "isImplied":
                                {
                                    var isImpliedValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isImpliedValue))
                                    {
                                        if (bool.TryParse(isImpliedValue, out var isImpliedValueAsBool))
                                        {
                                            poco.IsImplied = isImpliedValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isImplied' on element {ElementId}", isImpliedValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isImpliedIncluded":
                                {
                                    var isImpliedIncludedValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isImpliedIncludedValue))
                                    {
                                        if (bool.TryParse(isImpliedIncludedValue, out var isImpliedIncludedValueAsBool))
                                        {
                                            poco.IsImpliedIncluded = isImpliedIncludedValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isImpliedIncluded' on element {ElementId}", isImpliedIncludedValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isImportAll":
                                {
                                    var isImportAllValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isImportAllValue))
                                    {
                                        if (bool.TryParse(isImportAllValue, out var isImportAllValueAsBool))
                                        {
                                            poco.IsImportAll = isImportAllValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isImportAll' on element {ElementId}", isImportAllValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isRecursive":
                                {
                                    var isRecursiveValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isRecursiveValue))
                                    {
                                        if (bool.TryParse(isRecursiveValue, out var isRecursiveValueAsBool))
                                        {
                                            poco.IsRecursive = isRecursiveValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isRecursive' on element {ElementId}", isRecursiveValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "ownedRelatedElement":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var ownedRelatedElementId))
                                        {
                                            this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelatedElement", ownedRelatedElementId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'ownedRelatedElement' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var ownedRelatedElementValue = (IElement)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        ((IContainedRelationship)poco).OwnedRelatedElement.Add(ownedRelatedElementValue);
                                    }

                                    break;
                                }

                            case "ownedRelationship":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var ownedRelationshipId))
                                        {
                                            this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelationship", ownedRelationshipId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'ownedRelationship' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var ownedRelationshipValue = (IRelationship)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        ((IContainedElement)poco).OwnedRelationship.Add(ownedRelationshipValue);
                                    }

                                    break;
                                }

                            case "owningRelatedElement":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var owningRelatedElementId))
                                        {
                                            this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelatedElement", owningRelatedElementId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'owningRelatedElement' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var owningRelatedElementValue = (IElement)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        ((IContainedRelationship)poco).OwningRelatedElement = owningRelatedElementValue;
                                    }

                                    break;
                                }

                            case "owningRelationship":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        if (Guid.TryParse(hrefSplit[1], out var owningRelationshipId))
                                        {
                                            this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipId);
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse href GUID value '{HrefValue}' for property 'owningRelationship' on element {ElementId}", hrefSplit[1], poco.Id);
                                        }
                                    }
                                    else
                                    {
                                        var owningRelationshipValue = (IRelationship)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory, elementOriginMap: this.ElementOriginMap);

                                        ((IContainedElement)poco).OwningRelationship = owningRelationshipValue;
                                    }

                                    break;
                                }

                            case "visibility":
                                {
                                    var visibilityValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(visibilityValue))
                                    {
                                        if (VisibilityKindProvider.TryParse(visibilityValue.AsSpan(), out var visibilityValueAsEnum))
                                        {
                                            poco.Visibility = visibilityValueAsEnum;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse enum value '{Value}' for property 'visibility' on element {ElementId}", visibilityValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            default:
                                this.logger.LogWarning("Unexpected element '{LocalName}' encountered while reading NamespaceImport at line:position {LineNumber}:{LinePosition}", xmiReader.LocalName, xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                                await xmiReader.SkipAsync();
                                break;
                        }
                    }
                }
            }

            return poco;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
