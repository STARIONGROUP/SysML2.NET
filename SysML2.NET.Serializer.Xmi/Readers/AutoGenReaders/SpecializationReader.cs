// -------------------------------------------------------------------------------------------------
// <copyright file="SpecializationReader.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// The purpose of the <see cref="{this.Name}}Reader" /> is to read an instance of <see cref="I{this.Name}}" />
    /// from the XMI document
    /// </summary>
    public class SpecializationReader : XmiDataReader<ISpecialization>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" /> 
        /// </summary>
        private readonly ILogger<SpecializationReader> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecializationReader" />
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
        public SpecializationReader(IXmiDataCache cache, IXmiDataReaderFacade xmiDataReaderFacade, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory) : base(cache, xmiDataReaderFacade, externalReferenceService, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<SpecializationReader>.Instance : loggerFactory.CreateLogger<SpecializationReader>();
        }

        /// <summary>
        /// Reads the <see cref="ISpecialization" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>The read <see cref="ISpecialization" /></returns>
        public override ISpecialization Read(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            ISpecialization poco = new SysML2.NET.Core.POCO.Core.Types.Specialization();

            if (xmiReader.MoveToContent() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading Specialization at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:Specialization")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the SpecializationReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "Specialization", poco.Id);
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

                var generalXmlAttribute = xmiReader.GetAttribute("general");

                if (!string.IsNullOrWhiteSpace(generalXmlAttribute))
                {
                    if (Guid.TryParse(generalXmlAttribute, out var generalXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "general", generalXmlAttributeReference);
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

                var specificXmlAttribute = xmiReader.GetAttribute("specific");

                if (!string.IsNullOrWhiteSpace(specificXmlAttribute))
                {
                    if (Guid.TryParse(specificXmlAttribute, out var specificXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "specific", specificXmlAttributeReference);
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

                            case "general":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        var generalId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "general", generalId);
                                    }
                                    else
                                    {
                                        var generalValue = (IType)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.General = generalValue;
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
                                        var ownedRelationshipId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelationship", ownedRelationshipId);
                                    }
                                    else
                                    {
                                        var ownedRelationshipValue = (IRelationship)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

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
                                        var owningRelatedElementId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelatedElement", owningRelatedElementId);
                                    }
                                    else
                                    {
                                        var owningRelatedElementValue = (IElement)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

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
                                        var owningRelationshipId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipId);
                                    }
                                    else
                                    {
                                        var owningRelationshipValue = (IRelationship)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        ((IContainedElement)poco).OwningRelationship = owningRelationshipValue;
                                    }

                                    break;
                                }

                            case "specific":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        var specificId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "specific", specificId);
                                    }
                                    else
                                    {
                                        var specificValue = (IType)this.XmiDataReaderFacade.QueryXmiData(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.Specific = specificValue;
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
        /// Reads asynchronously the <see cref="ISpecialization" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/> with the read <see cref="ISpecialization" /></returns>
        public override async Task<ISpecialization> ReadAsync(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            ISpecialization poco = new SysML2.NET.Core.POCO.Core.Types.Specialization();

            if (await xmiReader.MoveToContentAsync() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading Specialization at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:Specialization")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the SpecializationReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "Specialization", poco.Id);
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

                var generalXmlAttribute = xmiReader.GetAttribute("general");

                if (!string.IsNullOrWhiteSpace(generalXmlAttribute))
                {
                    if (Guid.TryParse(generalXmlAttribute, out var generalXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "general", generalXmlAttributeReference);
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

                var specificXmlAttribute = xmiReader.GetAttribute("specific");

                if (!string.IsNullOrWhiteSpace(specificXmlAttribute))
                {
                    if (Guid.TryParse(specificXmlAttribute, out var specificXmlAttributeReference))
                    {
                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "specific", specificXmlAttributeReference);
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

                            case "general":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        var generalId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "general", generalId);
                                    }
                                    else
                                    {
                                        var generalValue = (IType)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.General = generalValue;
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
                                        var ownedRelationshipId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddMultipleValueReferencePropertyIdentifiers(poco.Id, "ownedRelationship", ownedRelationshipId);
                                    }
                                    else
                                    {
                                        var ownedRelationshipValue = (IRelationship)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

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
                                        var owningRelatedElementId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelatedElement", owningRelatedElementId);
                                    }
                                    else
                                    {
                                        var owningRelatedElementValue = (IElement)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

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
                                        var owningRelationshipId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "owningRelationship", owningRelationshipId);
                                    }
                                    else
                                    {
                                        var owningRelationshipValue = (IRelationship)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        ((IContainedElement)poco).OwningRelationship = owningRelationshipValue;
                                    }

                                    break;
                                }

                            case "specific":
                                {
                                    var hrefAttribute = xmiReader.GetAttribute("href");

                                    if (!string.IsNullOrWhiteSpace(hrefAttribute))
                                    {
                                        var hrefSplit = hrefAttribute.Split('#');
                                        this.ExternalReferenceService.AddExternalReferenceToProcess(currentLocation, hrefSplit[0]);
                                        var specificId = Guid.Parse(hrefSplit[1]);
                                        this.Cache.AddSingleValueReferencePropertyIdentifier(poco.Id, "specific", specificId);
                                    }
                                    else
                                    {
                                        var specificValue = (IType)await this.XmiDataReaderFacade.QueryXmiDataAsync(xmiReader, this.Cache, currentLocation, this.ExternalReferenceService, this.LoggerFactory);

                                        poco.Specific = specificValue;
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
