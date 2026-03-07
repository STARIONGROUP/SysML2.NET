// -------------------------------------------------------------------------------------------------
// <copyright file="CaseUsageWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
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
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="CaseUsageWriter" /> is to write an instance of <see cref="ICaseUsage" />
    /// to the XMI document
    /// </summary>
    public class CaseUsageWriter : XmiDataWriter<ICaseUsage>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" />
        /// </summary>
        private readonly ILogger<CaseUsageWriter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseUsageWriter" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        public CaseUsageWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory) : base(xmiDataWriterFacade, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<CaseUsageWriter>.Instance : loggerFactory.CreateLogger<CaseUsageWriter>();
        }

        /// <summary>
        /// Writes the <see cref="ICaseUsage" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="ICaseUsage" /> to write</param>
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
        public override void Write(XmlWriter xmlWriter, ICaseUsage poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:CaseUsage");
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

            xmlWriter.WriteAttributeString("direction", poco.Direction.ToString());

            if (!string.IsNullOrWhiteSpace(poco.ElementId))
            {
                xmlWriter.WriteAttributeString("elementId", poco.ElementId);
            }

            if (poco.IsAbstract)
            {
                xmlWriter.WriteAttributeString("isAbstract", "true");
            }

            if (poco.IsComposite)
            {
                xmlWriter.WriteAttributeString("isComposite", "true");
            }

            if (includeDerivedProperties)
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

            if (includesImplied || poco.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }

            if (poco.IsIndividual)
            {
                xmlWriter.WriteAttributeString("isIndividual", "true");
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
                if (poco.isModelLevelEvaluable)
                {
                    xmlWriter.WriteAttributeString("isModelLevelEvaluable", "true");
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

            if (includeDerivedProperties)
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

            if (poco.IsUnique)
            {
                xmlWriter.WriteAttributeString("isUnique", "true");
            }

            if (poco.IsVariation)
            {
                xmlWriter.WriteAttributeString("isVariation", "true");
            }

            if (includeDerivedProperties)
            {
                if (poco.mayTimeVary)
                {
                    xmlWriter.WriteAttributeString("mayTimeVary", "true");
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    xmlWriter.WriteAttributeString("name", poco.name);
                }
            }

            xmlWriter.WriteAttributeString("portionKind", poco.PortionKind.ToString());

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
                if (poco.actionDefinition != null)
                {
                    foreach (var item in poco.actionDefinition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "actionDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.actorParameter != null)
                {
                    foreach (var item in poco.actorParameter)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "actorParameter", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.caseDefinition != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.caseDefinition, "caseDefinition", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.chainingFeature != null)
                {
                    foreach (var item in poco.chainingFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "chainingFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.crossFeature != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.crossFeature, "crossFeature", elementOriginMap, currentFileUri);
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
                if (poco.directedUsage != null)
                {
                    foreach (var item in poco.directedUsage)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "directedUsage", elementOriginMap, currentFileUri);
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
                if (poco.endFeature != null)
                {
                    foreach (var item in poco.endFeature)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "endFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.endOwningType != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.endOwningType, "endOwningType", elementOriginMap, currentFileUri);
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
                if (poco.featureTarget != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.featureTarget, "featureTarget", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.featuringType != null)
                {
                    foreach (var item in poco.featuringType)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "featuringType", elementOriginMap, currentFileUri);
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
                if (poco.individualDefinition != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.individualDefinition, "individualDefinition", elementOriginMap, currentFileUri);
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
                if (poco.nestedAction != null)
                {
                    foreach (var item in poco.nestedAction)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedAllocation != null)
                {
                    foreach (var item in poco.nestedAllocation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAllocation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedAnalysisCase != null)
                {
                    foreach (var item in poco.nestedAnalysisCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAnalysisCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedAttribute != null)
                {
                    foreach (var item in poco.nestedAttribute)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAttribute", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedCalculation != null)
                {
                    foreach (var item in poco.nestedCalculation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedCalculation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedCase != null)
                {
                    foreach (var item in poco.nestedCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedConcern != null)
                {
                    foreach (var item in poco.nestedConcern)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConcern", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedConnection != null)
                {
                    foreach (var item in poco.nestedConnection)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConnection", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedConstraint != null)
                {
                    foreach (var item in poco.nestedConstraint)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConstraint", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedEnumeration != null)
                {
                    foreach (var item in poco.nestedEnumeration)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedEnumeration", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedFlow != null)
                {
                    foreach (var item in poco.nestedFlow)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedFlow", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedInterface != null)
                {
                    foreach (var item in poco.nestedInterface)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedInterface", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedItem != null)
                {
                    foreach (var item in poco.nestedItem)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedItem", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedMetadata != null)
                {
                    foreach (var item in poco.nestedMetadata)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedMetadata", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedOccurrence != null)
                {
                    foreach (var item in poco.nestedOccurrence)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedOccurrence", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedPart != null)
                {
                    foreach (var item in poco.nestedPart)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedPart", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedPort != null)
                {
                    foreach (var item in poco.nestedPort)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedPort", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedReference != null)
                {
                    foreach (var item in poco.nestedReference)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedReference", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedRendering != null)
                {
                    foreach (var item in poco.nestedRendering)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedRendering", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedRequirement != null)
                {
                    foreach (var item in poco.nestedRequirement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedRequirement", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedState != null)
                {
                    foreach (var item in poco.nestedState)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedState", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedTransition != null)
                {
                    foreach (var item in poco.nestedTransition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedTransition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedUsage != null)
                {
                    foreach (var item in poco.nestedUsage)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedUseCase != null)
                {
                    foreach (var item in poco.nestedUseCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedUseCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedVerificationCase != null)
                {
                    foreach (var item in poco.nestedVerificationCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedVerificationCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedView != null)
                {
                    foreach (var item in poco.nestedView)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedView", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedViewpoint != null)
                {
                    foreach (var item in poco.nestedViewpoint)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedViewpoint", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.objectiveRequirement != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.objectiveRequirement, "objectiveRequirement", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.occurrenceDefinition != null)
                {
                    foreach (var item in poco.occurrenceDefinition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "occurrenceDefinition", elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedCrossSubsetting != null)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedCrossSubsetting, "ownedCrossSubsetting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
                if (poco.ownedFeatureChaining != null)
                {
                    foreach (var item in poco.ownedFeatureChaining)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureChaining", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedFeatureInverting != null)
                {
                    foreach (var item in poco.ownedFeatureInverting)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureInverting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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

            if (includeDerivedProperties)
            {
                if (poco.ownedRedefinition != null)
                {
                    foreach (var item in poco.ownedRedefinition)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRedefinition", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedReferenceSubsetting != null)
                {
                    this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedReferenceSubsetting, "ownedReferenceSubsetting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
                if (poco.ownedSubsetting != null)
                {
                    foreach (var item in poco.ownedSubsetting)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSubsetting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedTypeFeaturing != null)
                {
                    foreach (var item in poco.ownedTypeFeaturing)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedTypeFeaturing", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedTyping != null)
                {
                    foreach (var item in poco.ownedTyping)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedTyping", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
                if (poco.owningDefinition != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningDefinition, "owningDefinition", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owningFeatureMembership != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningFeatureMembership, "owningFeatureMembership", elementOriginMap, currentFileUri);
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
                if (poco.owningUsage != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningUsage, "owningUsage", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.parameter != null)
                {
                    foreach (var item in poco.parameter)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "parameter", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.result != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.result, "result", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.subjectParameter != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.subjectParameter, "subjectParameter", elementOriginMap, currentFileUri);
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

            if (includeDerivedProperties)
            {
                if (poco.usage != null)
                {
                    foreach (var item in poco.usage)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "usage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.variant != null)
                {
                    foreach (var item in poco.variant)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "variant", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.variantMembership != null)
                {
                    foreach (var item in poco.variantMembership)
                    {
                        this.XmiDataWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "variantMembership", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }


            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes the <see cref="ICaseUsage" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="ICaseUsage" /> to write</param>
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
        public override async Task WriteAsync(XmlWriter xmlWriter, ICaseUsage poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, "sysml:CaseUsage");
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

            await xmlWriter.WriteAttributeStringAsync(null, "direction", null, poco.Direction.ToString());

            if (!string.IsNullOrWhiteSpace(poco.ElementId))
            {
                await xmlWriter.WriteAttributeStringAsync(null, "elementId", null, poco.ElementId);
            }

            if (poco.IsAbstract)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isAbstract", null, "true");
            }

            if (poco.IsComposite)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isComposite", null, "true");
            }

            if (includeDerivedProperties)
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

            if (includesImplied || poco.IsImpliedIncluded)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isImpliedIncluded", null, "true");
            }

            if (poco.IsIndividual)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isIndividual", null, "true");
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
                if (poco.isModelLevelEvaluable)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "isModelLevelEvaluable", null, "true");
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

            if (includeDerivedProperties)
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

            if (poco.IsUnique)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isUnique", null, "true");
            }

            if (poco.IsVariation)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isVariation", null, "true");
            }

            if (includeDerivedProperties)
            {
                if (poco.mayTimeVary)
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "mayTimeVary", null, "true");
                }
            }

            if (includeDerivedProperties)
            {
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    await xmlWriter.WriteAttributeStringAsync(null, "name", null, poco.name);
                }
            }

            await xmlWriter.WriteAttributeStringAsync(null, "portionKind", null, poco.PortionKind.ToString());

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
                if (poco.actionDefinition != null)
                {
                    foreach (var item in poco.actionDefinition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "actionDefinition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.actorParameter != null)
                {
                    foreach (var item in poco.actorParameter)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "actorParameter", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.caseDefinition != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.caseDefinition, "caseDefinition", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.chainingFeature != null)
                {
                    foreach (var item in poco.chainingFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "chainingFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.crossFeature != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.crossFeature, "crossFeature", elementOriginMap, currentFileUri);
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
                if (poco.directedUsage != null)
                {
                    foreach (var item in poco.directedUsage)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "directedUsage", elementOriginMap, currentFileUri);
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
                if (poco.endFeature != null)
                {
                    foreach (var item in poco.endFeature)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "endFeature", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.endOwningType != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.endOwningType, "endOwningType", elementOriginMap, currentFileUri);
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
                if (poco.featureTarget != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.featureTarget, "featureTarget", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.featuringType != null)
                {
                    foreach (var item in poco.featuringType)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "featuringType", elementOriginMap, currentFileUri);
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
                if (poco.individualDefinition != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.individualDefinition, "individualDefinition", elementOriginMap, currentFileUri);
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
                if (poco.nestedAction != null)
                {
                    foreach (var item in poco.nestedAction)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedAllocation != null)
                {
                    foreach (var item in poco.nestedAllocation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedAllocation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedAnalysisCase != null)
                {
                    foreach (var item in poco.nestedAnalysisCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedAnalysisCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedAttribute != null)
                {
                    foreach (var item in poco.nestedAttribute)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedAttribute", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedCalculation != null)
                {
                    foreach (var item in poco.nestedCalculation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedCalculation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedCase != null)
                {
                    foreach (var item in poco.nestedCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedConcern != null)
                {
                    foreach (var item in poco.nestedConcern)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedConcern", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedConnection != null)
                {
                    foreach (var item in poco.nestedConnection)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedConnection", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedConstraint != null)
                {
                    foreach (var item in poco.nestedConstraint)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedConstraint", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedEnumeration != null)
                {
                    foreach (var item in poco.nestedEnumeration)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedEnumeration", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedFlow != null)
                {
                    foreach (var item in poco.nestedFlow)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedFlow", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedInterface != null)
                {
                    foreach (var item in poco.nestedInterface)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedInterface", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedItem != null)
                {
                    foreach (var item in poco.nestedItem)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedItem", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedMetadata != null)
                {
                    foreach (var item in poco.nestedMetadata)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedMetadata", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedOccurrence != null)
                {
                    foreach (var item in poco.nestedOccurrence)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedOccurrence", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedPart != null)
                {
                    foreach (var item in poco.nestedPart)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedPart", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedPort != null)
                {
                    foreach (var item in poco.nestedPort)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedPort", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedReference != null)
                {
                    foreach (var item in poco.nestedReference)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedReference", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedRendering != null)
                {
                    foreach (var item in poco.nestedRendering)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedRendering", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedRequirement != null)
                {
                    foreach (var item in poco.nestedRequirement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedRequirement", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedState != null)
                {
                    foreach (var item in poco.nestedState)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedState", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedTransition != null)
                {
                    foreach (var item in poco.nestedTransition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedTransition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedUsage != null)
                {
                    foreach (var item in poco.nestedUsage)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedUseCase != null)
                {
                    foreach (var item in poco.nestedUseCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedUseCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedVerificationCase != null)
                {
                    foreach (var item in poco.nestedVerificationCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedVerificationCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedView != null)
                {
                    foreach (var item in poco.nestedView)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedView", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.nestedViewpoint != null)
                {
                    foreach (var item in poco.nestedViewpoint)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "nestedViewpoint", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.objectiveRequirement != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.objectiveRequirement, "objectiveRequirement", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.occurrenceDefinition != null)
                {
                    foreach (var item in poco.occurrenceDefinition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "occurrenceDefinition", elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedCrossSubsetting != null)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)poco.ownedCrossSubsetting, "ownedCrossSubsetting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
                if (poco.ownedFeatureChaining != null)
                {
                    foreach (var item in poco.ownedFeatureChaining)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedFeatureChaining", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedFeatureInverting != null)
                {
                    foreach (var item in poco.ownedFeatureInverting)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedFeatureInverting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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

            if (includeDerivedProperties)
            {
                if (poco.ownedRedefinition != null)
                {
                    foreach (var item in poco.ownedRedefinition)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedRedefinition", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedReferenceSubsetting != null)
                {
                    await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)poco.ownedReferenceSubsetting, "ownedReferenceSubsetting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
                if (poco.ownedSubsetting != null)
                {
                    foreach (var item in poco.ownedSubsetting)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedSubsetting", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedTypeFeaturing != null)
                {
                    foreach (var item in poco.ownedTypeFeaturing)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedTypeFeaturing", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedTyping != null)
                {
                    foreach (var item in poco.ownedTyping)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "ownedTyping", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
                if (poco.owningDefinition != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.owningDefinition, "owningDefinition", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.owningFeatureMembership != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.owningFeatureMembership, "owningFeatureMembership", elementOriginMap, currentFileUri);
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
                if (poco.owningUsage != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.owningUsage, "owningUsage", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.parameter != null)
                {
                    foreach (var item in poco.parameter)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "parameter", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.result != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.result, "result", elementOriginMap, currentFileUri);
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.subjectParameter != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.subjectParameter, "subjectParameter", elementOriginMap, currentFileUri);
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

            if (includeDerivedProperties)
            {
                if (poco.usage != null)
                {
                    foreach (var item in poco.usage)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "usage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.variant != null)
                {
                    foreach (var item in poco.variant)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "variant", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.variantMembership != null)
                {
                    foreach (var item in poco.variantMembership)
                    {
                        await this.XmiDataWriterFacade.WriteContainedElementAsync(xmlWriter, (IData)item, "variantMembership", includeDerivedProperties, includesImplied, elementOriginMap, currentFileUri);
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
