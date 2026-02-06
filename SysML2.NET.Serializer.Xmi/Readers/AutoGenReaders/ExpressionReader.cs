// -------------------------------------------------------------------------------------------------
// <copyright file="ExpressionReader.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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
    using System.Xml;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// The purpose of the <see cref="{this.Name}}Reader" /> is to read an instance of <see cref="I{this.Name}}" />
    /// from the XMI document
    /// </summary>
    public class ExpressionReader : XmiDataReader<IExpression>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" /> 
        /// </summary>
        private readonly ILogger<ExpressionReader> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionReader" />
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
        public ExpressionReader(IXmiDataCache cache, IXmiDataReaderFacade xmiDataReaderFacade, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory) : base(cache, xmiDataReaderFacade, externalReferenceService, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<ExpressionReader>.Instance : loggerFactory.CreateLogger<ExpressionReader>();
        }

        /// <summary>
        /// Reads the <see cref="IExpression" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>The read <see cref="IExpression" /></returns>
        public override IExpression Read(XmlReader xmiReader, Uri currentLocation)
        {
            if (xmiReader == null)
            {
                throw new ArgumentNullException(nameof(xmiReader));
            }

            var xmlLineInfo = xmiReader as IXmlLineInfo;

            IExpression poco = new SysML2.NET.Core.POCO.Kernel.Functions.Expression();

            if (xmiReader.MoveToContent() == XmlNodeType.Element)
            {
                this.logger.LogTrace("reading Expression at line:position {LineNumber}:{LinePosition}", xmlLineInfo?.LineNumber, xmlLineInfo?.LinePosition);
                var xsiType = xmiReader.GetAttribute("xsi:type");

                if (!string.IsNullOrEmpty(xsiType) && xsiType != "sysml:Expression")
                {
                    throw new InvalidOperationException($"The xsi:type {xsiType} is not supported by the ExpressionReader");
                }

                var xmiId = xmiReader.GetAttribute("xmi:id");

                if (!Guid.TryParse(xmiId, out var guid))
                {
                    throw new InvalidOperationException($"The xmi:id {xmiId} could not be parsed");
                }

                poco.Id = guid;

                if (!this.Cache.TryAdd(poco) && this.logger.IsEnabled(LogLevel.Critical))
                {
                    this.logger.LogCritical("Failed to add element type [{Poco}] with id [{Id}] as it was already in the Cache. The XMI document seems to have duplicate xmi:id values", "Expression", poco.Id);
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

                var isOrderedXmlAttribute = xmiReader.GetAttribute("isOrdered");

                if (!string.IsNullOrWhiteSpace(isOrderedXmlAttribute))
                {
                    if (bool.TryParse(isOrderedXmlAttribute, out var isOrderedXmlAttributeAsBool))
                    {
                        poco.IsOrdered = isOrderedXmlAttributeAsBool;
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

                var isVariableXmlAttribute = xmiReader.GetAttribute("isVariable");

                if (!string.IsNullOrWhiteSpace(isVariableXmlAttribute))
                {
                    if (bool.TryParse(isVariableXmlAttribute, out var isVariableXmlAttributeAsBool))
                    {
                        poco.IsVariable = isVariableXmlAttributeAsBool;
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

                            case "isOrdered":
                                {
                                    var isOrderedValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isOrderedValue))
                                    {
                                        poco.IsOrdered = bool.Parse(isOrderedValue);
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

                            case "isVariable":
                                {
                                    var isVariableValue = xmiReader.ReadElementContentAsString();

                                    if (!string.IsNullOrWhiteSpace(isVariableValue))
                                    {
                                        poco.IsVariable = bool.Parse(isVariableValue);
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
