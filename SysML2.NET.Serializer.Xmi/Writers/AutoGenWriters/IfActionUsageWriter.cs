// -------------------------------------------------------------------------------------------------
// <copyright file="IfActionUsageWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
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
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Serializer.Xmi.Extensions;

    /// <summary>
    /// The purpose of the <see cref="IfActionUsageWriter" /> is to write an instance of <see cref="IIfActionUsage" />
    /// to the XMI document
    /// </summary>
    public class IfActionUsageWriter : XmiDataWriter<IIfActionUsage>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" />
        /// </summary>
        private readonly ILogger<IfActionUsageWriter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IfActionUsageWriter" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        public IfActionUsageWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory) : base(xmiDataWriterFacade, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<IfActionUsageWriter>.Instance : loggerFactory.CreateLogger<IfActionUsageWriter>();
        }

        /// <summary>
        /// Writes the <see cref="IIfActionUsage" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IIfActionUsage" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions" /> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public override void Write(XmlWriter xmlWriter, IIfActionUsage poco, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:IfActionUsage");
            xmlWriter.WriteAttributeString("xmi", "id", null, poco.Id.ToString());

            // Scalar properties as XML attributes (sorted alphabetically)
            if (poco.AliasIds != null && poco.AliasIds.Count > 0)
            {
                xmlWriter.WriteAttributeString("aliasIds", string.Join(" ", poco.AliasIds));
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.crossFeature != null && poco.crossFeature.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("crossFeature", poco.crossFeature.Id.ToString());
                }
            }

            if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                xmlWriter.WriteAttributeString("declaredName", poco.DeclaredName);
            }

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName))
            {
                xmlWriter.WriteAttributeString("declaredShortName", poco.DeclaredShortName);
            }

            if (poco.Direction.HasValue)
            {
                xmlWriter.WriteAttributeString("direction", poco.Direction.Value.ToString().ToLower());
            }

            if (!string.IsNullOrWhiteSpace(poco.ElementId))
            {
                xmlWriter.WriteAttributeString("elementId", poco.ElementId);
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.elseAction != null && poco.elseAction.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("elseAction", poco.elseAction.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.endOwningType != null && poco.endOwningType.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("endOwningType", poco.endOwningType.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.featureTarget != null && poco.featureTarget.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("featureTarget", poco.featureTarget.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.ifArgument != null && poco.ifArgument.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("ifArgument", poco.ifArgument.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.individualDefinition != null && poco.individualDefinition.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("individualDefinition", poco.individualDefinition.Id.ToString());
                }
            }

            if (poco.IsAbstract)
            {
                xmlWriter.WriteAttributeString("isAbstract", "true");
            }

            if (poco.IsComposite)
            {
                xmlWriter.WriteAttributeString("isComposite", "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isConjugated)
                {
                    xmlWriter.WriteAttributeString("isConjugated", "true");
                }
            }

            if (poco.IsConstant)
            {
                xmlWriter.WriteAttributeString("isConstant", "true");
            }

            if (poco.IsDerived)
            {
                xmlWriter.WriteAttributeString("isDerived", "true");
            }

            if (poco.IsEnd)
            {
                xmlWriter.WriteAttributeString("isEnd", "true");
            }

            if (writerOptions.IncludeImplied || poco.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }

            if (poco.IsIndividual)
            {
                xmlWriter.WriteAttributeString("isIndividual", "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    xmlWriter.WriteAttributeString("isLibraryElement", "true");
                }
            }

            if (poco.IsOrdered)
            {
                xmlWriter.WriteAttributeString("isOrdered", "true");
            }

            if (poco.IsPortion)
            {
                xmlWriter.WriteAttributeString("isPortion", "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isReference)
                {
                    xmlWriter.WriteAttributeString("isReference", "true");
                }
            }

            if (poco.IsSufficient)
            {
                xmlWriter.WriteAttributeString("isSufficient", "true");
            }

            if (!poco.IsUnique)
            {
                xmlWriter.WriteAttributeString("isUnique", "false");
            }

            if (poco.IsVariation)
            {
                xmlWriter.WriteAttributeString("isVariation", "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.mayTimeVary)
                {
                    xmlWriter.WriteAttributeString("mayTimeVary", "true");
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.multiplicity != null && poco.multiplicity.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("multiplicity", poco.multiplicity.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    xmlWriter.WriteAttributeString("name", poco.name);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.ownedConjugator != null && poco.ownedConjugator.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("ownedConjugator", poco.ownedConjugator.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.ownedCrossSubsetting != null && poco.ownedCrossSubsetting.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("ownedCrossSubsetting", poco.ownedCrossSubsetting.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.ownedReferenceSubsetting != null && poco.ownedReferenceSubsetting.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("ownedReferenceSubsetting", poco.ownedReferenceSubsetting.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owner != null && poco.owner.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("owner", poco.owner.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningDefinition != null && poco.owningDefinition.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("owningDefinition", poco.owningDefinition.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningNamespace != null && poco.owningNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("owningNamespace", poco.owningNamespace.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningType != null && poco.owningType.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("owningType", poco.owningType.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningUsage != null && poco.owningUsage.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("owningUsage", poco.owningUsage.Id.ToString());
                }
            }

            if (poco.PortionKind.HasValue)
            {
                xmlWriter.WriteAttributeString("portionKind", poco.PortionKind.Value.ToString().ToLower());
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.qualifiedName))
                {
                    xmlWriter.WriteAttributeString("qualifiedName", poco.qualifiedName);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.shortName))
                {
                    xmlWriter.WriteAttributeString("shortName", poco.shortName);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.thenAction != null && poco.thenAction.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    xmlWriter.WriteAttributeString("thenAction", poco.thenAction.Id.ToString());
                }
            }


            // Reference/containment properties as child elements (sorted alphabetically)
            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.actionDefinition != null)
                {
                    foreach (var item in poco.actionDefinition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "actionDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.chainingFeature != null)
                {
                    foreach (var item in poco.chainingFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "chainingFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.crossFeature != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.crossFeature.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.crossFeature, "crossFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.differencingType != null)
                {
                    foreach (var item in poco.differencingType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "differencingType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.directedUsage != null)
                {
                    foreach (var item in poco.directedUsage)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "directedUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.documentation != null)
                {
                    foreach (var item in poco.documentation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "documentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.elseAction != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.elseAction.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.elseAction, "elseAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.endFeature != null)
                {
                    foreach (var item in poco.endFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "endFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.endOwningType != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.endOwningType.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.endOwningType, "endOwningType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.feature != null)
                {
                    foreach (var item in poco.feature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "feature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.featureMembership != null)
                {
                    foreach (var item in poco.featureMembership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "featureMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.featureTarget != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.featureTarget.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.featureTarget, "featureTarget", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.featuringType != null)
                {
                    foreach (var item in poco.featuringType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "featuringType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ifArgument != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.ifArgument.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.ifArgument, "ifArgument", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.importedMembership != null)
                {
                    foreach (var item in poco.importedMembership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "importedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.individualDefinition != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.individualDefinition.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.individualDefinition, "individualDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.inheritedFeature != null)
                {
                    foreach (var item in poco.inheritedFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "inheritedFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.inheritedMembership != null)
                {
                    foreach (var item in poco.inheritedMembership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "inheritedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.input != null)
                {
                    foreach (var item in poco.input)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "input", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.intersectingType != null)
                {
                    foreach (var item in poco.intersectingType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "intersectingType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.member != null)
                {
                    foreach (var item in poco.member)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "member", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.membership != null)
                {
                    foreach (var item in poco.membership)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "membership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.multiplicity != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.multiplicity.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.multiplicity, "multiplicity", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedAction != null)
                {
                    foreach (var item in poco.nestedAction)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedAllocation != null)
                {
                    foreach (var item in poco.nestedAllocation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAllocation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedAnalysisCase != null)
                {
                    foreach (var item in poco.nestedAnalysisCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAnalysisCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedAttribute != null)
                {
                    foreach (var item in poco.nestedAttribute)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAttribute", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedCalculation != null)
                {
                    foreach (var item in poco.nestedCalculation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedCalculation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedCase != null)
                {
                    foreach (var item in poco.nestedCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedConcern != null)
                {
                    foreach (var item in poco.nestedConcern)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConcern", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedConnection != null)
                {
                    foreach (var item in poco.nestedConnection)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConnection", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedConstraint != null)
                {
                    foreach (var item in poco.nestedConstraint)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConstraint", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedEnumeration != null)
                {
                    foreach (var item in poco.nestedEnumeration)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedEnumeration", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedFlow != null)
                {
                    foreach (var item in poco.nestedFlow)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedFlow", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedInterface != null)
                {
                    foreach (var item in poco.nestedInterface)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedInterface", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedItem != null)
                {
                    foreach (var item in poco.nestedItem)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedItem", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedMetadata != null)
                {
                    foreach (var item in poco.nestedMetadata)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedMetadata", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedOccurrence != null)
                {
                    foreach (var item in poco.nestedOccurrence)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedOccurrence", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedPart != null)
                {
                    foreach (var item in poco.nestedPart)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedPart", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedPort != null)
                {
                    foreach (var item in poco.nestedPort)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedPort", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedReference != null)
                {
                    foreach (var item in poco.nestedReference)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedReference", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedRendering != null)
                {
                    foreach (var item in poco.nestedRendering)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedRendering", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedRequirement != null)
                {
                    foreach (var item in poco.nestedRequirement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedRequirement", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedState != null)
                {
                    foreach (var item in poco.nestedState)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedState", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedTransition != null)
                {
                    foreach (var item in poco.nestedTransition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedTransition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedUsage != null)
                {
                    foreach (var item in poco.nestedUsage)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedUseCase != null)
                {
                    foreach (var item in poco.nestedUseCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedUseCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedVerificationCase != null)
                {
                    foreach (var item in poco.nestedVerificationCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedVerificationCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedView != null)
                {
                    foreach (var item in poco.nestedView)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedView", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedViewpoint != null)
                {
                    foreach (var item in poco.nestedViewpoint)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedViewpoint", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.occurrenceDefinition != null)
                {
                    foreach (var item in poco.occurrenceDefinition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "occurrenceDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.output != null)
                {
                    foreach (var item in poco.output)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "output", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedAnnotation", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedConjugator != null)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedConjugator, "ownedConjugator", writerOptions, elementOriginMap, currentFileUri);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedCrossSubsetting != null)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedCrossSubsetting, "ownedCrossSubsetting", writerOptions, elementOriginMap, currentFileUri);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedDifferencing != null)
                {
                    foreach (var item in poco.ownedDifferencing)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedDifferencing", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedDisjoining != null)
                {
                    foreach (var item in poco.ownedDisjoining)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedDisjoining", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedElement != null)
                {
                    foreach (var item in poco.ownedElement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedElement", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedEndFeature != null)
                {
                    foreach (var item in poco.ownedEndFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedEndFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedFeature != null)
                {
                    foreach (var item in poco.ownedFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedFeatureChaining != null)
                {
                    foreach (var item in poco.ownedFeatureChaining)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureChaining", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedFeatureInverting != null)
                {
                    foreach (var item in poco.ownedFeatureInverting)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureInverting", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedFeatureMembership != null)
                {
                    foreach (var item in poco.ownedFeatureMembership)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureMembership", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedImport != null)
                {
                    foreach (var item in poco.ownedImport)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedImport", writerOptions, elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedIntersecting != null)
                {
                    foreach (var item in poco.ownedIntersecting)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedIntersecting", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedMember != null)
                {
                    foreach (var item in poco.ownedMember)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedMember", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedMembership != null)
                {
                    foreach (var item in poco.ownedMembership)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedMembership", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedRedefinition != null)
                {
                    foreach (var item in poco.ownedRedefinition)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRedefinition", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedReferenceSubsetting != null)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedReferenceSubsetting, "ownedReferenceSubsetting", writerOptions, elementOriginMap, currentFileUri);
                }
            }

            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelationship", writerOptions, elementOriginMap, currentFileUri, true);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedSpecialization != null)
                {
                    foreach (var item in poco.ownedSpecialization)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSpecialization", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedSubsetting != null)
                {
                    foreach (var item in poco.ownedSubsetting)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSubsetting", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedTypeFeaturing != null)
                {
                    foreach (var item in poco.ownedTypeFeaturing)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedTypeFeaturing", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedTyping != null)
                {
                    foreach (var item in poco.ownedTyping)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedTyping", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedUnioning != null)
                {
                    foreach (var item in poco.ownedUnioning)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedUnioning", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owner != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owner.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owner, "owner", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningDefinition != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningDefinition.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningDefinition, "owningDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningNamespace != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningType != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningType.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningType, "owningType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningUsage != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningUsage.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningUsage, "owningUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.parameter != null)
                {
                    foreach (var item in poco.parameter)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "parameter", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.textualRepresentation != null)
                {
                    foreach (var item in poco.textualRepresentation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "textualRepresentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.thenAction != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.thenAction.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.thenAction, "thenAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.unioningType != null)
                {
                    foreach (var item in poco.unioningType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "unioningType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.usage != null)
                {
                    foreach (var item in poco.usage)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "usage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.variant != null)
                {
                    foreach (var item in poco.variant)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "variant", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.variantMembership != null)
                {
                    foreach (var item in poco.variantMembership)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "variantMembership", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }


            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes the <see cref="IIfActionUsage" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IIfActionUsage" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        /// <returns>An awaitable <see cref="Task" /></returns>
        public override async Task WriteAsync(XmlWriter xmlWriter, IIfActionUsage poco, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, "sysml:IfActionUsage");
            await xmlWriter.WriteAttributeStringAsync("xmi", "id", null, poco.Id.ToString());

            // Scalar properties as XML attributes (sorted alphabetically)
            if (poco.AliasIds != null && poco.AliasIds.Count > 0)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "aliasIds", null, string.Join(" ", poco.AliasIds));
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.crossFeature != null && poco.crossFeature.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "crossFeature", null, poco.crossFeature.Id.ToString());
                }
            }

            if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "declaredName", null, poco.DeclaredName);
            }

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "declaredShortName", null, poco.DeclaredShortName);
            }

            if (poco.Direction.HasValue)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "direction", null, poco.Direction.Value.ToString().ToLower());
            }

            if (!string.IsNullOrWhiteSpace(poco.ElementId))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "elementId", null, poco.ElementId);
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.elseAction != null && poco.elseAction.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "elseAction", null, poco.elseAction.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.endOwningType != null && poco.endOwningType.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "endOwningType", null, poco.endOwningType.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.featureTarget != null && poco.featureTarget.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "featureTarget", null, poco.featureTarget.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.ifArgument != null && poco.ifArgument.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "ifArgument", null, poco.ifArgument.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.individualDefinition != null && poco.individualDefinition.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "individualDefinition", null, poco.individualDefinition.Id.ToString());
                }
            }

            if (poco.IsAbstract)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isAbstract", null, "true");
            }

            if (poco.IsComposite)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isComposite", null, "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isConjugated)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "isConjugated", null, "true");
                }
            }

            if (poco.IsConstant)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isConstant", null, "true");
            }

            if (poco.IsDerived)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isDerived", null, "true");
            }

            if (poco.IsEnd)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isEnd", null, "true");
            }

            if (writerOptions.IncludeImplied || poco.IsImpliedIncluded)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImpliedIncluded", null, "true");
            }

            if (poco.IsIndividual)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isIndividual", null, "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isLibraryElement)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "isLibraryElement", null, "true");
                }
            }

            if (poco.IsOrdered)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isOrdered", null, "true");
            }

            if (poco.IsPortion)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isPortion", null, "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.isReference)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "isReference", null, "true");
                }
            }

            if (poco.IsSufficient)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isSufficient", null, "true");
            }

            if (!poco.IsUnique)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isUnique", null, "false");
            }

            if (poco.IsVariation)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isVariation", null, "true");
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.mayTimeVary)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "mayTimeVary", null, "true");
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.multiplicity != null && poco.multiplicity.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "multiplicity", null, poco.multiplicity.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "name", null, poco.name);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.ownedConjugator != null && poco.ownedConjugator.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "ownedConjugator", null, poco.ownedConjugator.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.ownedCrossSubsetting != null && poco.ownedCrossSubsetting.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "ownedCrossSubsetting", null, poco.ownedCrossSubsetting.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.ownedReferenceSubsetting != null && poco.ownedReferenceSubsetting.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "ownedReferenceSubsetting", null, poco.ownedReferenceSubsetting.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owner != null && poco.owner.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "owner", null, poco.owner.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningDefinition != null && poco.owningDefinition.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "owningDefinition", null, poco.owningDefinition.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningNamespace != null && poco.owningNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "owningNamespace", null, poco.owningNamespace.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningType != null && poco.owningType.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "owningType", null, poco.owningType.Id.ToString());
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.owningUsage != null && poco.owningUsage.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "owningUsage", null, poco.owningUsage.Id.ToString());
                }
            }

            if (poco.PortionKind.HasValue)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "portionKind", null, poco.PortionKind.Value.ToString().ToLower());
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.qualifiedName))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "qualifiedName", null, poco.qualifiedName);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.shortName))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "shortName", null, poco.shortName);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (writerOptions.WriteIdRefAsAttribute && poco.thenAction != null && poco.thenAction.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "thenAction", null, poco.thenAction.Id.ToString());
                }
            }


            // Reference/containment properties as child elements (sorted alphabetically)
            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.actionDefinition != null)
                {
                    foreach (var item in poco.actionDefinition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "actionDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.chainingFeature != null)
                {
                    foreach (var item in poco.chainingFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "chainingFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.crossFeature != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.crossFeature.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.crossFeature, "crossFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.differencingType != null)
                {
                    foreach (var item in poco.differencingType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "differencingType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.directedUsage != null)
                {
                    foreach (var item in poco.directedUsage)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "directedUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.documentation != null)
                {
                    foreach (var item in poco.documentation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "documentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.elseAction != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.elseAction.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.elseAction, "elseAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.endFeature != null)
                {
                    foreach (var item in poco.endFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "endFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.endOwningType != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.endOwningType.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.endOwningType, "endOwningType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.feature != null)
                {
                    foreach (var item in poco.feature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "feature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.featureMembership != null)
                {
                    foreach (var item in poco.featureMembership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "featureMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.featureTarget != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.featureTarget.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.featureTarget, "featureTarget", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.featuringType != null)
                {
                    foreach (var item in poco.featuringType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "featuringType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ifArgument != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.ifArgument.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.ifArgument, "ifArgument", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.importedMembership != null)
                {
                    foreach (var item in poco.importedMembership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "importedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.individualDefinition != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.individualDefinition.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.individualDefinition, "individualDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.inheritedFeature != null)
                {
                    foreach (var item in poco.inheritedFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "inheritedFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.inheritedMembership != null)
                {
                    foreach (var item in poco.inheritedMembership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "inheritedMembership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.input != null)
                {
                    foreach (var item in poco.input)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "input", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.intersectingType != null)
                {
                    foreach (var item in poco.intersectingType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "intersectingType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.member != null)
                {
                    foreach (var item in poco.member)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "member", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.membership != null)
                {
                    foreach (var item in poco.membership)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "membership", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.multiplicity != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.multiplicity.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.multiplicity, "multiplicity", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedAction != null)
                {
                    foreach (var item in poco.nestedAction)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedAllocation != null)
                {
                    foreach (var item in poco.nestedAllocation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedAllocation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedAnalysisCase != null)
                {
                    foreach (var item in poco.nestedAnalysisCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedAnalysisCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedAttribute != null)
                {
                    foreach (var item in poco.nestedAttribute)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedAttribute", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedCalculation != null)
                {
                    foreach (var item in poco.nestedCalculation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedCalculation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedCase != null)
                {
                    foreach (var item in poco.nestedCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedConcern != null)
                {
                    foreach (var item in poco.nestedConcern)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedConcern", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedConnection != null)
                {
                    foreach (var item in poco.nestedConnection)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedConnection", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedConstraint != null)
                {
                    foreach (var item in poco.nestedConstraint)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedConstraint", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedEnumeration != null)
                {
                    foreach (var item in poco.nestedEnumeration)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedEnumeration", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedFlow != null)
                {
                    foreach (var item in poco.nestedFlow)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedFlow", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedInterface != null)
                {
                    foreach (var item in poco.nestedInterface)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedInterface", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedItem != null)
                {
                    foreach (var item in poco.nestedItem)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedItem", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedMetadata != null)
                {
                    foreach (var item in poco.nestedMetadata)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedMetadata", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedOccurrence != null)
                {
                    foreach (var item in poco.nestedOccurrence)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedOccurrence", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedPart != null)
                {
                    foreach (var item in poco.nestedPart)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedPart", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedPort != null)
                {
                    foreach (var item in poco.nestedPort)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedPort", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedReference != null)
                {
                    foreach (var item in poco.nestedReference)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedReference", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedRendering != null)
                {
                    foreach (var item in poco.nestedRendering)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedRendering", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedRequirement != null)
                {
                    foreach (var item in poco.nestedRequirement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedRequirement", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedState != null)
                {
                    foreach (var item in poco.nestedState)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedState", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedTransition != null)
                {
                    foreach (var item in poco.nestedTransition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedTransition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedUsage != null)
                {
                    foreach (var item in poco.nestedUsage)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedUseCase != null)
                {
                    foreach (var item in poco.nestedUseCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedUseCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedVerificationCase != null)
                {
                    foreach (var item in poco.nestedVerificationCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedVerificationCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedView != null)
                {
                    foreach (var item in poco.nestedView)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedView", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.nestedViewpoint != null)
                {
                    foreach (var item in poco.nestedViewpoint)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "nestedViewpoint", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.occurrenceDefinition != null)
                {
                    foreach (var item in poco.occurrenceDefinition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "occurrenceDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.output != null)
                {
                    foreach (var item in poco.output)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "output", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedAnnotation", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedConjugator != null)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, poco.ownedConjugator, "ownedConjugator", writerOptions, elementOriginMap, currentFileUri);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedCrossSubsetting != null)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, poco.ownedCrossSubsetting, "ownedCrossSubsetting", writerOptions, elementOriginMap, currentFileUri);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedDifferencing != null)
                {
                    foreach (var item in poco.ownedDifferencing)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedDifferencing", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedDisjoining != null)
                {
                    foreach (var item in poco.ownedDisjoining)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedDisjoining", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedElement != null)
                {
                    foreach (var item in poco.ownedElement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "ownedElement", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedEndFeature != null)
                {
                    foreach (var item in poco.ownedEndFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "ownedEndFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedFeature != null)
                {
                    foreach (var item in poco.ownedFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "ownedFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedFeatureChaining != null)
                {
                    foreach (var item in poco.ownedFeatureChaining)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedFeatureChaining", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedFeatureInverting != null)
                {
                    foreach (var item in poco.ownedFeatureInverting)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedFeatureInverting", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedFeatureMembership != null)
                {
                    foreach (var item in poco.ownedFeatureMembership)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedFeatureMembership", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedImport != null)
                {
                    foreach (var item in poco.ownedImport)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedImport", writerOptions, elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedIntersecting != null)
                {
                    foreach (var item in poco.ownedIntersecting)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedIntersecting", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedMember != null)
                {
                    foreach (var item in poco.ownedMember)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "ownedMember", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedMembership != null)
                {
                    foreach (var item in poco.ownedMembership)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedMembership", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedRedefinition != null)
                {
                    foreach (var item in poco.ownedRedefinition)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedRedefinition", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedReferenceSubsetting != null)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, poco.ownedReferenceSubsetting, "ownedReferenceSubsetting", writerOptions, elementOriginMap, currentFileUri);
                }
            }

            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedRelationship", writerOptions, elementOriginMap, currentFileUri, true);
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedSpecialization != null)
                {
                    foreach (var item in poco.ownedSpecialization)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedSpecialization", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedSubsetting != null)
                {
                    foreach (var item in poco.ownedSubsetting)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedSubsetting", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedTypeFeaturing != null)
                {
                    foreach (var item in poco.ownedTypeFeaturing)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedTypeFeaturing", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedTyping != null)
                {
                    foreach (var item in poco.ownedTyping)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedTyping", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.ownedUnioning != null)
                {
                    foreach (var item in poco.ownedUnioning)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "ownedUnioning", writerOptions, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owner != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owner.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.owner, "owner", elementOriginMap, currentFileUri, true);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningDefinition != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningDefinition.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.owningDefinition, "owningDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningNamespace != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningNamespace.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningType != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningType.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.owningType, "owningType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.owningUsage != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.owningUsage.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.owningUsage, "owningUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.parameter != null)
                {
                    foreach (var item in poco.parameter)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "parameter", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.textualRepresentation != null)
                {
                    foreach (var item in poco.textualRepresentation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "textualRepresentation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.thenAction != null)
                {
                    if (!writerOptions.WriteIdRefAsAttribute || !poco.thenAction.QueryIsValidIdRef(elementOriginMap, currentFileUri))
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, poco.thenAction, "thenAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.unioningType != null)
                {
                    foreach (var item in poco.unioningType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "unioningType", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.usage != null)
                {
                    foreach (var item in poco.usage)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "usage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.variant != null)
                {
                    foreach (var item in poco.variant)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, item, "variant", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (writerOptions.IncludeDerivedProperties)
            {
                if (poco.variantMembership != null)
                {
                    foreach (var item in poco.variantMembership)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, item, "variantMembership", writerOptions, elementOriginMap, currentFileUri);
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
