// -------------------------------------------------------------------------------------------------
// <copyright file="StateUsageReader.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
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
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// The purpose of the <see cref="{this.Name}}Reader" /> is to read an instance of <see cref="I{this.Name}}" />
    /// from the XMI document
    /// </summary>
    public class StateUsageReader : XmiDataReader<IStateUsage>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" /> 
        /// </summary>
        private readonly ILogger<StateUsageReader> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateUsageReader" />
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
        public StateUsageReader(IXmiDataCache cache, IXmiDataReaderFacade xmiDataReaderFacade, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory) : base(cache, xmiDataReaderFacade, externalReferenceService, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<StateUsageReader>.Instance : loggerFactory.CreateLogger<StateUsageReader>();
        }

        /// <summary>
        /// Reads the <see cref="IStateUsage" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>The read <see cref="IStateUsage" /></returns>
        public override IStateUsage Read(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            IStateUsage poco = new SysML2.NET.Core.POCO.Systems.States.StateUsage();

            if (xmiReader.MoveToContent() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading StateUsage at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:StateUsage")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the StateUsageReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "StateUsage", poco.Id);
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

                var directionXmlAttribute = xmiReader.GetAttribute("direction");

                if (!string.IsNullOrWhiteSpace(directionXmlAttribute))
                {
                    if (Enum.TryParse(directionXmlAttribute, true, out FeatureDirectionKind directionXmlAttributeAsEnum))
                    {
                        poco.Direction = directionXmlAttributeAsEnum;
                    }
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

                var isCompositeXmlAttribute = xmiReader.GetAttribute("isComposite");

                if (!string.IsNullOrWhiteSpace(isCompositeXmlAttribute))
                {
                    if (bool.TryParse(isCompositeXmlAttribute, out var isCompositeXmlAttributeAsBool))
                    {
                        poco.IsComposite = isCompositeXmlAttributeAsBool;
                    }
                }

                var isConstantXmlAttribute = xmiReader.GetAttribute("isConstant");

                if (!string.IsNullOrWhiteSpace(isConstantXmlAttribute))
                {
                    if (bool.TryParse(isConstantXmlAttribute, out var isConstantXmlAttributeAsBool))
                    {
                        poco.IsConstant = isConstantXmlAttributeAsBool;
                    }
                }

                var isDerivedXmlAttribute = xmiReader.GetAttribute("isDerived");

                if (!string.IsNullOrWhiteSpace(isDerivedXmlAttribute))
                {
                    if (bool.TryParse(isDerivedXmlAttribute, out var isDerivedXmlAttributeAsBool))
                    {
                        poco.IsDerived = isDerivedXmlAttributeAsBool;
                    }
                }

                var isEndXmlAttribute = xmiReader.GetAttribute("isEnd");

                if (!string.IsNullOrWhiteSpace(isEndXmlAttribute))
                {
                    if (bool.TryParse(isEndXmlAttribute, out var isEndXmlAttributeAsBool))
                    {
                        poco.IsEnd = isEndXmlAttributeAsBool;
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

                var isOrderedXmlAttribute = xmiReader.GetAttribute("isOrdered");

                if (!string.IsNullOrWhiteSpace(isOrderedXmlAttribute))
                {
                    if (bool.TryParse(isOrderedXmlAttribute, out var isOrderedXmlAttributeAsBool))
                    {
                        poco.IsOrdered = isOrderedXmlAttributeAsBool;
                    }
                }

                var isParallelXmlAttribute = xmiReader.GetAttribute("isParallel");

                if (!string.IsNullOrWhiteSpace(isParallelXmlAttribute))
                {
                    if (bool.TryParse(isParallelXmlAttribute, out var isParallelXmlAttributeAsBool))
                    {
                        poco.IsParallel = isParallelXmlAttributeAsBool;
                    }
                }

                var isPortionXmlAttribute = xmiReader.GetAttribute("isPortion");

                if (!string.IsNullOrWhiteSpace(isPortionXmlAttribute))
                {
                    if (bool.TryParse(isPortionXmlAttribute, out var isPortionXmlAttributeAsBool))
                    {
                        poco.IsPortion = isPortionXmlAttributeAsBool;
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

                var isUniqueXmlAttribute = xmiReader.GetAttribute("isUnique");

                if (!string.IsNullOrWhiteSpace(isUniqueXmlAttribute))
                {
                    if (bool.TryParse(isUniqueXmlAttribute, out var isUniqueXmlAttributeAsBool))
                    {
                        poco.IsUnique = isUniqueXmlAttributeAsBool;
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
                }

                var portionKindXmlAttribute = xmiReader.GetAttribute("portionKind");

                if (!string.IsNullOrWhiteSpace(portionKindXmlAttribute))
                {
                    if (Enum.TryParse(portionKindXmlAttribute, true, out PortionKind portionKindXmlAttributeAsEnum))
                    {
                        poco.PortionKind = portionKindXmlAttributeAsEnum;
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

                            case "direction":
                                {
                                    var directionValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(directionValue))
                                    {
                                        poco.Direction = (FeatureDirectionKind)Enum.Parse(typeof(FeatureDirectionKind), directionValue, true);
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
                                        poco.IsAbstract = bool.Parse(isAbstractValue);
                                    }

                                    break;
                                }

                            case "isComposite":
                                {
                                    var isCompositeValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isCompositeValue))
                                    {
                                        poco.IsComposite = bool.Parse(isCompositeValue);
                                    }

                                    break;
                                }

                            case "isConstant":
                                {
                                    var isConstantValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isConstantValue))
                                    {
                                        poco.IsConstant = bool.Parse(isConstantValue);
                                    }

                                    break;
                                }

                            case "isDerived":
                                {
                                    var isDerivedValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isDerivedValue))
                                    {
                                        poco.IsDerived = bool.Parse(isDerivedValue);
                                    }

                                    break;
                                }

                            case "isEnd":
                                {
                                    var isEndValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isEndValue))
                                    {
                                        poco.IsEnd = bool.Parse(isEndValue);
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

                            case "isIndividual":
                                {
                                    var isIndividualValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isIndividualValue))
                                    {
                                        poco.IsIndividual = bool.Parse(isIndividualValue);
                                    }

                                    break;
                                }

                            case "isOrdered":
                                {
                                    var isOrderedValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isOrderedValue))
                                    {
                                        poco.IsOrdered = bool.Parse(isOrderedValue);
                                    }

                                    break;
                                }

                            case "isParallel":
                                {
                                    var isParallelValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isParallelValue))
                                    {
                                        poco.IsParallel = bool.Parse(isParallelValue);
                                    }

                                    break;
                                }

                            case "isPortion":
                                {
                                    var isPortionValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isPortionValue))
                                    {
                                        poco.IsPortion = bool.Parse(isPortionValue);
                                    }

                                    break;
                                }

                            case "isSufficient":
                                {
                                    var isSufficientValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isSufficientValue))
                                    {
                                        poco.IsSufficient = bool.Parse(isSufficientValue);
                                    }

                                    break;
                                }

                            case "isUnique":
                                {
                                    var isUniqueValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isUniqueValue))
                                    {
                                        poco.IsUnique = bool.Parse(isUniqueValue);
                                    }

                                    break;
                                }

                            case "isVariation":
                                {
                                    var isVariationValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isVariationValue))
                                    {
                                        poco.IsVariation = bool.Parse(isVariationValue);
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

                            case "portionKind":
                                {
                                    var portionKindValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(portionKindValue))
                                    {
                                        poco.PortionKind = (PortionKind)Enum.Parse(typeof(PortionKind), portionKindValue, true);
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
        /// Reads asynchronously the <see cref="IStateUsage" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/> with the read <see cref="IStateUsage" /></returns>
        public override async Task<IStateUsage> ReadAsync(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            IStateUsage poco = new SysML2.NET.Core.POCO.Systems.States.StateUsage();

            if (await xmiReader.MoveToContentAsync() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading StateUsage at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:StateUsage")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the StateUsageReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "StateUsage", poco.Id);
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

                var directionXmlAttribute = xmiReader.GetAttribute("direction");

                if (!string.IsNullOrWhiteSpace(directionXmlAttribute))
                {
                    if (Enum.TryParse(directionXmlAttribute, true, out FeatureDirectionKind directionXmlAttributeAsEnum))
                    {
                        poco.Direction = directionXmlAttributeAsEnum;
                    }
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

                var isCompositeXmlAttribute = xmiReader.GetAttribute("isComposite");

                if (!string.IsNullOrWhiteSpace(isCompositeXmlAttribute))
                {
                    if (bool.TryParse(isCompositeXmlAttribute, out var isCompositeXmlAttributeAsBool))
                    {
                        poco.IsComposite = isCompositeXmlAttributeAsBool;
                    }
                }

                var isConstantXmlAttribute = xmiReader.GetAttribute("isConstant");

                if (!string.IsNullOrWhiteSpace(isConstantXmlAttribute))
                {
                    if (bool.TryParse(isConstantXmlAttribute, out var isConstantXmlAttributeAsBool))
                    {
                        poco.IsConstant = isConstantXmlAttributeAsBool;
                    }
                }

                var isDerivedXmlAttribute = xmiReader.GetAttribute("isDerived");

                if (!string.IsNullOrWhiteSpace(isDerivedXmlAttribute))
                {
                    if (bool.TryParse(isDerivedXmlAttribute, out var isDerivedXmlAttributeAsBool))
                    {
                        poco.IsDerived = isDerivedXmlAttributeAsBool;
                    }
                }

                var isEndXmlAttribute = xmiReader.GetAttribute("isEnd");

                if (!string.IsNullOrWhiteSpace(isEndXmlAttribute))
                {
                    if (bool.TryParse(isEndXmlAttribute, out var isEndXmlAttributeAsBool))
                    {
                        poco.IsEnd = isEndXmlAttributeAsBool;
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

                var isOrderedXmlAttribute = xmiReader.GetAttribute("isOrdered");

                if (!string.IsNullOrWhiteSpace(isOrderedXmlAttribute))
                {
                    if (bool.TryParse(isOrderedXmlAttribute, out var isOrderedXmlAttributeAsBool))
                    {
                        poco.IsOrdered = isOrderedXmlAttributeAsBool;
                    }
                }

                var isParallelXmlAttribute = xmiReader.GetAttribute("isParallel");

                if (!string.IsNullOrWhiteSpace(isParallelXmlAttribute))
                {
                    if (bool.TryParse(isParallelXmlAttribute, out var isParallelXmlAttributeAsBool))
                    {
                        poco.IsParallel = isParallelXmlAttributeAsBool;
                    }
                }

                var isPortionXmlAttribute = xmiReader.GetAttribute("isPortion");

                if (!string.IsNullOrWhiteSpace(isPortionXmlAttribute))
                {
                    if (bool.TryParse(isPortionXmlAttribute, out var isPortionXmlAttributeAsBool))
                    {
                        poco.IsPortion = isPortionXmlAttributeAsBool;
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

                var isUniqueXmlAttribute = xmiReader.GetAttribute("isUnique");

                if (!string.IsNullOrWhiteSpace(isUniqueXmlAttribute))
                {
                    if (bool.TryParse(isUniqueXmlAttribute, out var isUniqueXmlAttributeAsBool))
                    {
                        poco.IsUnique = isUniqueXmlAttributeAsBool;
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
                }

                var portionKindXmlAttribute = xmiReader.GetAttribute("portionKind");

                if (!string.IsNullOrWhiteSpace(portionKindXmlAttribute))
                {
                    if (Enum.TryParse(portionKindXmlAttribute, true, out PortionKind portionKindXmlAttributeAsEnum))
                    {
                        poco.PortionKind = portionKindXmlAttributeAsEnum;
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

                            case "direction":
                                {
                                    var directionValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(directionValue))
                                    {
                                        poco.Direction = (FeatureDirectionKind)Enum.Parse(typeof(FeatureDirectionKind), directionValue, true);
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
                                        poco.IsAbstract = bool.Parse(isAbstractValue);
                                    }

                                    break;
                                }

                            case "isComposite":
                                {
                                    var isCompositeValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isCompositeValue))
                                    {
                                        poco.IsComposite = bool.Parse(isCompositeValue);
                                    }

                                    break;
                                }

                            case "isConstant":
                                {
                                    var isConstantValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isConstantValue))
                                    {
                                        poco.IsConstant = bool.Parse(isConstantValue);
                                    }

                                    break;
                                }

                            case "isDerived":
                                {
                                    var isDerivedValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isDerivedValue))
                                    {
                                        poco.IsDerived = bool.Parse(isDerivedValue);
                                    }

                                    break;
                                }

                            case "isEnd":
                                {
                                    var isEndValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isEndValue))
                                    {
                                        poco.IsEnd = bool.Parse(isEndValue);
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

                            case "isIndividual":
                                {
                                    var isIndividualValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isIndividualValue))
                                    {
                                        poco.IsIndividual = bool.Parse(isIndividualValue);
                                    }

                                    break;
                                }

                            case "isOrdered":
                                {
                                    var isOrderedValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isOrderedValue))
                                    {
                                        poco.IsOrdered = bool.Parse(isOrderedValue);
                                    }

                                    break;
                                }

                            case "isParallel":
                                {
                                    var isParallelValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isParallelValue))
                                    {
                                        poco.IsParallel = bool.Parse(isParallelValue);
                                    }

                                    break;
                                }

                            case "isPortion":
                                {
                                    var isPortionValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isPortionValue))
                                    {
                                        poco.IsPortion = bool.Parse(isPortionValue);
                                    }

                                    break;
                                }

                            case "isSufficient":
                                {
                                    var isSufficientValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isSufficientValue))
                                    {
                                        poco.IsSufficient = bool.Parse(isSufficientValue);
                                    }

                                    break;
                                }

                            case "isUnique":
                                {
                                    var isUniqueValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isUniqueValue))
                                    {
                                        poco.IsUnique = bool.Parse(isUniqueValue);
                                    }

                                    break;
                                }

                            case "isVariation":
                                {
                                    var isVariationValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(isVariationValue))
                                    {
                                        poco.IsVariation = bool.Parse(isVariationValue);
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

                            case "portionKind":
                                {
                                    var portionKindValue = await xmiReader.ReadElementContentAsStringAsync();

                                    if (!string.IsNullOrWhiteSpace(portionKindValue))
                                    {
                                        poco.PortionKind = (PortionKind)Enum.Parse(typeof(PortionKind), portionKindValue, true);
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
