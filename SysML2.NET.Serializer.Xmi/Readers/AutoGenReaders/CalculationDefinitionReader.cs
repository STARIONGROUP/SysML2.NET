// -------------------------------------------------------------------------------------------------
// <copyright file="CalculationDefinitionReader.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="{this.Name}}Reader" /> is to read an instance of <see cref="I{this.Name}}" />
    /// from the XMI document
    /// </summary>
    public class CalculationDefinitionReader : XmiDataReader<ICalculationDefinition>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" /> 
        /// </summary>
        private readonly ILogger<CalculationDefinitionReader> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationDefinitionReader" />
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
        public CalculationDefinitionReader(IXmiDataCache cache, IXmiDataReaderFacade xmiDataReaderFacade, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory, IXmiElementOriginMap elementOriginMap = null) : base(cache, xmiDataReaderFacade, externalReferenceService, loggerFactory, elementOriginMap)
        {
            this.logger = loggerFactory == null ? NullLogger<CalculationDefinitionReader>.Instance : loggerFactory.CreateLogger<CalculationDefinitionReader>();
        }

        /// <summary>
        /// Reads the <see cref="ICalculationDefinition" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>The read <see cref="ICalculationDefinition" /></returns>
        public override ICalculationDefinition Read(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            ICalculationDefinition poco = new SysML2.NET.Core.POCO.Systems.Calculations.CalculationDefinition();

            if (xmiReader.MoveToContent() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading CalculationDefinition at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:CalculationDefinition")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the CalculationDefinitionReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "CalculationDefinition", poco.Id);
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

                var isAbstractXmlAttribute = xmiReader.GetAttribute("isAbstract");

                if (!string.IsNullOrWhiteSpace(isAbstractXmlAttribute))
                {
                    if (bool.TryParse(isAbstractXmlAttribute, out var isAbstractXmlAttributeAsBool))
                    {
                        poco.IsAbstract = isAbstractXmlAttributeAsBool;
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

                var isIndividualXmlAttribute = xmiReader.GetAttribute("isIndividual");

                if (!string.IsNullOrWhiteSpace(isIndividualXmlAttribute))
                {
                    if (bool.TryParse(isIndividualXmlAttribute, out var isIndividualXmlAttributeAsBool))
                    {
                        poco.IsIndividual = isIndividualXmlAttributeAsBool;
                    }
                }

                var isSufficientXmlAttribute = xmiReader.GetAttribute("isSufficient");

                if (!string.IsNullOrWhiteSpace(isSufficientXmlAttribute))
                {
                    if (bool.TryParse(isSufficientXmlAttribute, out var isSufficientXmlAttributeAsBool))
                    {
                        poco.IsSufficient = isSufficientXmlAttributeAsBool;
                    }
                }

                var isVariationXmlAttribute = xmiReader.GetAttribute("isVariation");

                if (!string.IsNullOrWhiteSpace(isVariationXmlAttribute))
                {
                    if (bool.TryParse(isVariationXmlAttribute, out var isVariationXmlAttributeAsBool))
                    {
                        poco.IsVariation = isVariationXmlAttributeAsBool;
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

                            case "isAbstract":
                                {
                                    var isAbstractValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isAbstractValue))
                                    {
                                        if (bool.TryParse(isAbstractValue, out var isAbstractValueAsBool))
                                        {
                                            poco.IsAbstract = isAbstractValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isAbstract' on element {ElementId}", isAbstractValue, poco.Id);
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

                            case "isIndividual":
                                {
                                    var isIndividualValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isIndividualValue))
                                    {
                                        if (bool.TryParse(isIndividualValue, out var isIndividualValueAsBool))
                                        {
                                            poco.IsIndividual = isIndividualValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isIndividual' on element {ElementId}", isIndividualValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isSufficient":
                                {
                                    var isSufficientValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isSufficientValue))
                                    {
                                        if (bool.TryParse(isSufficientValue, out var isSufficientValueAsBool))
                                        {
                                            poco.IsSufficient = isSufficientValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isSufficient' on element {ElementId}", isSufficientValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isVariation":
                                {
                                    var isVariationValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isVariationValue))
                                    {
                                        if (bool.TryParse(isVariationValue, out var isVariationValueAsBool))
                                        {
                                            poco.IsVariation = isVariationValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isVariation' on element {ElementId}", isVariationValue, poco.Id);
                                        }
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

                            default:
                                this.logger.LogWarning("Unexpected element '{LocalName}' encountered while reading CalculationDefinition at line:position {LineNumber}:{LinePosition}", xmiReader.LocalName, xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                                xmiReader.Skip();
                                break;
                        }
                    }
                }
            }

            return poco;
        }

        /// <summary>
        /// Reads asynchronously the <see cref="ICalculationDefinition" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/> with the read <see cref="ICalculationDefinition" /></returns>
        public override async Task<ICalculationDefinition> ReadAsync(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            ICalculationDefinition poco = new SysML2.NET.Core.POCO.Systems.Calculations.CalculationDefinition();

            if (await xmiReader.MoveToContentAsync() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading CalculationDefinition at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:CalculationDefinition")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the CalculationDefinitionReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "CalculationDefinition", poco.Id);
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

                var isAbstractXmlAttribute = xmiReader.GetAttribute("isAbstract");

                if (!string.IsNullOrWhiteSpace(isAbstractXmlAttribute))
                {
                    if (bool.TryParse(isAbstractXmlAttribute, out var isAbstractXmlAttributeAsBool))
                    {
                        poco.IsAbstract = isAbstractXmlAttributeAsBool;
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

                var isIndividualXmlAttribute = xmiReader.GetAttribute("isIndividual");

                if (!string.IsNullOrWhiteSpace(isIndividualXmlAttribute))
                {
                    if (bool.TryParse(isIndividualXmlAttribute, out var isIndividualXmlAttributeAsBool))
                    {
                        poco.IsIndividual = isIndividualXmlAttributeAsBool;
                    }
                }

                var isSufficientXmlAttribute = xmiReader.GetAttribute("isSufficient");

                if (!string.IsNullOrWhiteSpace(isSufficientXmlAttribute))
                {
                    if (bool.TryParse(isSufficientXmlAttribute, out var isSufficientXmlAttributeAsBool))
                    {
                        poco.IsSufficient = isSufficientXmlAttributeAsBool;
                    }
                }

                var isVariationXmlAttribute = xmiReader.GetAttribute("isVariation");

                if (!string.IsNullOrWhiteSpace(isVariationXmlAttribute))
                {
                    if (bool.TryParse(isVariationXmlAttribute, out var isVariationXmlAttributeAsBool))
                    {
                        poco.IsVariation = isVariationXmlAttributeAsBool;
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

                            case "isAbstract":
                                {
                                    var isAbstractValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isAbstractValue))
                                    {
                                        if (bool.TryParse(isAbstractValue, out var isAbstractValueAsBool))
                                        {
                                            poco.IsAbstract = isAbstractValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isAbstract' on element {ElementId}", isAbstractValue, poco.Id);
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

                            case "isIndividual":
                                {
                                    var isIndividualValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isIndividualValue))
                                    {
                                        if (bool.TryParse(isIndividualValue, out var isIndividualValueAsBool))
                                        {
                                            poco.IsIndividual = isIndividualValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isIndividual' on element {ElementId}", isIndividualValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isSufficient":
                                {
                                    var isSufficientValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isSufficientValue))
                                    {
                                        if (bool.TryParse(isSufficientValue, out var isSufficientValueAsBool))
                                        {
                                            poco.IsSufficient = isSufficientValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isSufficient' on element {ElementId}", isSufficientValue, poco.Id);
                                        }
                                    }

                                    break;
                                }

                            case "isVariation":
                                {
                                    var isVariationValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isVariationValue))
                                    {
                                        if (bool.TryParse(isVariationValue, out var isVariationValueAsBool))
                                        {
                                            poco.IsVariation = isVariationValueAsBool;
                                        }
                                        else
                                        {
                                            this.logger.LogWarning("Failed to parse bool value '{Value}' for property 'isVariation' on element {ElementId}", isVariationValue, poco.Id);
                                        }
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

                            default:
                                this.logger.LogWarning("Unexpected element '{LocalName}' encountered while reading CalculationDefinition at line:position {LineNumber}:{LinePosition}", xmiReader.LocalName, xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
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
