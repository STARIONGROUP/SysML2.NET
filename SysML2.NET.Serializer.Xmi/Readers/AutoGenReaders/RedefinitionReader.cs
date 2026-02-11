// -------------------------------------------------------------------------------------------------
// <copyright file="RedefinitionReader.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;

    /// <summary>
    /// The purpose of the <see cref="{this.Name}}Reader" /> is to read an instance of <see cref="I{this.Name}}" />
    /// from the XMI document
    /// </summary>
    public class RedefinitionReader : XmiDataReader<IRedefinition>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" /> 
        /// </summary>
        private readonly ILogger<RedefinitionReader> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedefinitionReader" />
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
        public RedefinitionReader(IXmiDataCache cache, IXmiDataReaderFacade xmiDataReaderFacade, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory) : base(cache, xmiDataReaderFacade, externalReferenceService, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<RedefinitionReader>.Instance : loggerFactory.CreateLogger<RedefinitionReader>();
        }

        /// <summary>
        /// Reads the <see cref="IRedefinition" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>The read <see cref="IRedefinition" /></returns>
        public override IRedefinition Read(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            IRedefinition poco = new SysML2.NET.Core.POCO.Core.Features.Redefinition();

            if (xmiReader.MoveToContent() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading Redefinition at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:Redefinition")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the RedefinitionReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "Redefinition", poco.Id);
                }

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
                }

                var owningRelationshipXmlAttribute = xmiReader.GetAttribute("owningRelationship");

                if (!string.IsNullOrWhiteSpace(owningRelationshipXmlAttribute))
                {
                    if (Guid.TryParse(owningRelationshipXmlAttribute, out var owningRelationshipXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipXmlAttributeReference);
                    }
                }

                var redefinedFeatureXmlAttribute = xmiReader.GetAttribute("redefinedFeature");

                if (!string.IsNullOrWhiteSpace(redefinedFeatureXmlAttribute))
                {
                    if (Guid.TryParse(redefinedFeatureXmlAttribute, out var redefinedFeatureXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "redefinedFeature", redefinedFeatureXmlAttributeReference);
                    }
                }

                var redefiningFeatureXmlAttribute = xmiReader.GetAttribute("redefiningFeature");

                if (!string.IsNullOrWhiteSpace(redefiningFeatureXmlAttribute))
                {
                    if (Guid.TryParse(redefiningFeatureXmlAttribute, out var redefiningFeatureXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "redefiningFeature", redefiningFeatureXmlAttributeReference);
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

                            case "isImplied":
                                {
                                    var isImpliedValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isImpliedValue))
                                    {
                                        poco.IsImplied = bool.Parse(isImpliedValue);
                                    }

                                    break;
                                }

                            case "isImpliedIncluded":
                                {
                                    var isImpliedIncludedValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isImpliedIncludedValue))
                                    {
                                        poco.IsImpliedIncluded = bool.Parse(isImpliedIncludedValue);
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
                                        var ownedRelatedElementId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelatedElement", ownedRelatedElementId);
                                    }
                                    else
                                    {
                                        var ownedRelatedElementValue = (IElement)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.OwnedRelatedElement.Add(ownedRelatedElementValue);
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
                                        var ownedRelationshipId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelationship", ownedRelationshipId);
                                    }
                                    else
                                    {
                                        var ownedRelationshipValue = (IRelationship)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.OwnedRelationship.Add(ownedRelationshipValue);
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
                                        var owningRelatedElementId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelatedElement", owningRelatedElementId);
                                    }
                                    else
                                    {
                                        var owningRelatedElementValue = (IElement)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.OwningRelatedElement = owningRelatedElementValue;
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
                                        var owningRelationshipId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipId);
                                    }
                                    else
                                    {
                                        var owningRelationshipValue = (IRelationship)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.OwningRelationship = owningRelationshipValue;
                                    }

                                    break;
                                }

                            case "redefinedFeature":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        var redefinedFeatureId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "redefinedFeature", redefinedFeatureId);
                                    }
                                    else
                                    {
                                        var redefinedFeatureValue = (IFeature)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.RedefinedFeature = redefinedFeatureValue;
                                    }

                                    break;
                                }

                            case "redefiningFeature":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        var redefiningFeatureId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "redefiningFeature", redefiningFeatureId);
                                    }
                                    else
                                    {
                                        var redefiningFeatureValue = (IFeature)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.RedefiningFeature = redefiningFeatureValue;
                                    }

                                    break;
                                }

                        }
                    }
                }
            }

            return poco;
        }

        /// <summary>
        /// Reads asynchronously the <see cref="IRedefinition" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/> with the read <see cref="IRedefinition" /></returns>
        public override async Task<IRedefinition> ReadAsync(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            IRedefinition poco = new SysML2.NET.Core.POCO.Core.Features.Redefinition();

            if (await xmiReader.MoveToContentAsync() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading Redefinition at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:Redefinition")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the RedefinitionReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "Redefinition", poco.Id);
                }

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
                }

                var owningRelationshipXmlAttribute = xmiReader.GetAttribute("owningRelationship");

                if (!string.IsNullOrWhiteSpace(owningRelationshipXmlAttribute))
                {
                    if (Guid.TryParse(owningRelationshipXmlAttribute, out var owningRelationshipXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipXmlAttributeReference);
                    }
                }

                var redefinedFeatureXmlAttribute = xmiReader.GetAttribute("redefinedFeature");

                if (!string.IsNullOrWhiteSpace(redefinedFeatureXmlAttribute))
                {
                    if (Guid.TryParse(redefinedFeatureXmlAttribute, out var redefinedFeatureXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "redefinedFeature", redefinedFeatureXmlAttributeReference);
                    }
                }

                var redefiningFeatureXmlAttribute = xmiReader.GetAttribute("redefiningFeature");

                if (!string.IsNullOrWhiteSpace(redefiningFeatureXmlAttribute))
                {
                    if (Guid.TryParse(redefiningFeatureXmlAttribute, out var redefiningFeatureXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "redefiningFeature", redefiningFeatureXmlAttributeReference);
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

                            case "isImplied":
                                {
                                    var isImpliedValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isImpliedValue))
                                    {
                                        poco.IsImplied = bool.Parse(isImpliedValue);
                                    }

                                    break;
                                }

                            case "isImpliedIncluded":
                                {
                                    var isImpliedIncludedValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isImpliedIncludedValue))
                                    {
                                        poco.IsImpliedIncluded = bool.Parse(isImpliedIncludedValue);
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
                                        var ownedRelatedElementId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelatedElement", ownedRelatedElementId);
                                    }
                                    else
                                    {
                                        var ownedRelatedElementValue = (IElement)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.OwnedRelatedElement.Add(ownedRelatedElementValue);
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
                                        var ownedRelationshipId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelationship", ownedRelationshipId);
                                    }
                                    else
                                    {
                                        var ownedRelationshipValue = (IRelationship)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.OwnedRelationship.Add(ownedRelationshipValue);
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
                                        var owningRelatedElementId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelatedElement", owningRelatedElementId);
                                    }
                                    else
                                    {
                                        var owningRelatedElementValue = (IElement)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.OwningRelatedElement = owningRelatedElementValue;
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
                                        var owningRelationshipId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipId);
                                    }
                                    else
                                    {
                                        var owningRelationshipValue = (IRelationship)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.OwningRelationship = owningRelationshipValue;
                                    }

                                    break;
                                }

                            case "redefinedFeature":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        var redefinedFeatureId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "redefinedFeature", redefinedFeatureId);
                                    }
                                    else
                                    {
                                        var redefinedFeatureValue = (IFeature)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.RedefinedFeature = redefinedFeatureValue;
                                    }

                                    break;
                                }

                            case "redefiningFeature":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        var redefiningFeatureId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "redefiningFeature", redefiningFeatureId);
                                    }
                                    else
                                    {
                                        var redefiningFeatureValue = (IFeature)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.RedefiningFeature = redefiningFeatureValue;
                                    }

                                    break;
                                }

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
