// -------------------------------------------------------------------------------------------------
// <copyright file="ViewDefinitionWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Functions;
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
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="ViewDefinitionWriter" /> is to write an instance of <see cref="IViewDefinition" />
    /// to the XMI document
    /// </summary>
    public class ViewDefinitionWriter : XmiDataWriter<IViewDefinition>
    {
        /// <summary>
        /// The instantiated logger from the injected <see cref="ILoggerFactory" />
        /// </summary>
        private readonly ILogger<ViewDefinitionWriter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewDefinitionWriter" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        public ViewDefinitionWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory) : base(xmiDataWriterFacade, loggerFactory)
        {
            this.logger = loggerFactory == null ? NullLogger<ViewDefinitionWriter>.Instance : loggerFactory.CreateLogger<ViewDefinitionWriter>();
        }

        /// <summary>
        /// Writes the <see cref="IViewDefinition" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IViewDefinition" /> to write</param>
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
        public override void Write(XmlWriter xmlWriter, IViewDefinition poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:ViewDefinition");
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

            if (poco.IsSufficient)
            {
                xmlWriter.WriteAttributeString("isSufficient", "true");
            }

            if (poco.IsVariation)
            {
                xmlWriter.WriteAttributeString("isVariation", "true");
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
                if (poco.ownedAction != null)
                {
                    foreach (var item in poco.ownedAction)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedAllocation != null)
                {
                    foreach (var item in poco.ownedAllocation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedAllocation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedAnalysisCase != null)
                {
                    foreach (var item in poco.ownedAnalysisCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedAnalysisCase", elementOriginMap, currentFileUri);
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
                if (poco.ownedAttribute != null)
                {
                    foreach (var item in poco.ownedAttribute)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedAttribute", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedCalculation != null)
                {
                    foreach (var item in poco.ownedCalculation)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedCalculation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedCase != null)
                {
                    foreach (var item in poco.ownedCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedConcern != null)
                {
                    foreach (var item in poco.ownedConcern)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedConcern", elementOriginMap, currentFileUri);
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
                if (poco.ownedConnection != null)
                {
                    foreach (var item in poco.ownedConnection)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedConnection", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedConstraint != null)
                {
                    foreach (var item in poco.ownedConstraint)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedConstraint", elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedEnumeration != null)
                {
                    foreach (var item in poco.ownedEnumeration)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedEnumeration", elementOriginMap, currentFileUri);
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
                if (poco.ownedFlow != null)
                {
                    foreach (var item in poco.ownedFlow)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedFlow", elementOriginMap, currentFileUri);
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
                if (poco.ownedInterface != null)
                {
                    foreach (var item in poco.ownedInterface)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedInterface", elementOriginMap, currentFileUri);
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
                if (poco.ownedItem != null)
                {
                    foreach (var item in poco.ownedItem)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedItem", elementOriginMap, currentFileUri);
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
                if (poco.ownedMetadata != null)
                {
                    foreach (var item in poco.ownedMetadata)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedMetadata", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedOccurrence != null)
                {
                    foreach (var item in poco.ownedOccurrence)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedOccurrence", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedPart != null)
                {
                    foreach (var item in poco.ownedPart)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedPart", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedPort != null)
                {
                    foreach (var item in poco.ownedPort)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedPort", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedReference != null)
                {
                    foreach (var item in poco.ownedReference)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedReference", elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedRendering != null)
                {
                    foreach (var item in poco.ownedRendering)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedRendering", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedRequirement != null)
                {
                    foreach (var item in poco.ownedRequirement)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedRequirement", elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedState != null)
                {
                    foreach (var item in poco.ownedState)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedState", elementOriginMap, currentFileUri);
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
                if (poco.ownedTransition != null)
                {
                    foreach (var item in poco.ownedTransition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedTransition", elementOriginMap, currentFileUri);
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
                if (poco.ownedUsage != null)
                {
                    foreach (var item in poco.ownedUsage)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedUseCase != null)
                {
                    foreach (var item in poco.ownedUseCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedUseCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedVerificationCase != null)
                {
                    foreach (var item in poco.ownedVerificationCase)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedVerificationCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedView != null)
                {
                    foreach (var item in poco.ownedView)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedView", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedViewpoint != null)
                {
                    foreach (var item in poco.ownedViewpoint)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedViewpoint", elementOriginMap, currentFileUri);
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

            if (poco.OwningRelationship != null)
            {
                this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            if (includeDerivedProperties)
            {
                if (poco.satisfiedViewpoint != null)
                {
                    foreach (var item in poco.satisfiedViewpoint)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "satisfiedViewpoint", elementOriginMap, currentFileUri);
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

            if (includeDerivedProperties)
            {
                if (poco.view != null)
                {
                    foreach (var item in poco.view)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "view", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.viewCondition != null)
                {
                    foreach (var item in poco.viewCondition)
                    {
                        this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "viewCondition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.viewRendering != null)
                {
                    this.XmiDataWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.viewRendering, "viewRendering", elementOriginMap, currentFileUri);
                }
            }


            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously writes the <see cref="IViewDefinition" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IViewDefinition" /> to write</param>
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
        public override async Task WriteAsync(XmlWriter xmlWriter, IViewDefinition poco, string elementName, bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            await xmlWriter.WriteStartElementAsync(null, elementName, null);
            await xmlWriter.WriteAttributeStringAsync("xsi", "type", null, "sysml:ViewDefinition");
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

            if (poco.IsSufficient)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isSufficient", null, "true");
            }

            if (poco.IsVariation)
            {
                await xmlWriter.WriteAttributeStringAsync(null, "isVariation", null, "true");
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
                if (poco.ownedAction != null)
                {
                    foreach (var item in poco.ownedAction)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedAction", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedAllocation != null)
                {
                    foreach (var item in poco.ownedAllocation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedAllocation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedAnalysisCase != null)
                {
                    foreach (var item in poco.ownedAnalysisCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedAnalysisCase", elementOriginMap, currentFileUri);
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
                if (poco.ownedAttribute != null)
                {
                    foreach (var item in poco.ownedAttribute)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedAttribute", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedCalculation != null)
                {
                    foreach (var item in poco.ownedCalculation)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedCalculation", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedCase != null)
                {
                    foreach (var item in poco.ownedCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedConcern != null)
                {
                    foreach (var item in poco.ownedConcern)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedConcern", elementOriginMap, currentFileUri);
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
                if (poco.ownedConnection != null)
                {
                    foreach (var item in poco.ownedConnection)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedConnection", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedConstraint != null)
                {
                    foreach (var item in poco.ownedConstraint)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedConstraint", elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedEnumeration != null)
                {
                    foreach (var item in poco.ownedEnumeration)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedEnumeration", elementOriginMap, currentFileUri);
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
                if (poco.ownedFlow != null)
                {
                    foreach (var item in poco.ownedFlow)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedFlow", elementOriginMap, currentFileUri);
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
                if (poco.ownedInterface != null)
                {
                    foreach (var item in poco.ownedInterface)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedInterface", elementOriginMap, currentFileUri);
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
                if (poco.ownedItem != null)
                {
                    foreach (var item in poco.ownedItem)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedItem", elementOriginMap, currentFileUri);
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
                if (poco.ownedMetadata != null)
                {
                    foreach (var item in poco.ownedMetadata)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedMetadata", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedOccurrence != null)
                {
                    foreach (var item in poco.ownedOccurrence)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedOccurrence", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedPart != null)
                {
                    foreach (var item in poco.ownedPart)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedPart", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedPort != null)
                {
                    foreach (var item in poco.ownedPort)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedPort", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedReference != null)
                {
                    foreach (var item in poco.ownedReference)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedReference", elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedRendering != null)
                {
                    foreach (var item in poco.ownedRendering)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedRendering", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedRequirement != null)
                {
                    foreach (var item in poco.ownedRequirement)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedRequirement", elementOriginMap, currentFileUri);
                    }
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
                if (poco.ownedState != null)
                {
                    foreach (var item in poco.ownedState)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedState", elementOriginMap, currentFileUri);
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
                if (poco.ownedTransition != null)
                {
                    foreach (var item in poco.ownedTransition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedTransition", elementOriginMap, currentFileUri);
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
                if (poco.ownedUsage != null)
                {
                    foreach (var item in poco.ownedUsage)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedUsage", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedUseCase != null)
                {
                    foreach (var item in poco.ownedUseCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedUseCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedVerificationCase != null)
                {
                    foreach (var item in poco.ownedVerificationCase)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedVerificationCase", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedView != null)
                {
                    foreach (var item in poco.ownedView)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedView", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.ownedViewpoint != null)
                {
                    foreach (var item in poco.ownedViewpoint)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "ownedViewpoint", elementOriginMap, currentFileUri);
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

            if (poco.OwningRelationship != null)
            {
                await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            if (includeDerivedProperties)
            {
                if (poco.satisfiedViewpoint != null)
                {
                    foreach (var item in poco.satisfiedViewpoint)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "satisfiedViewpoint", elementOriginMap, currentFileUri);
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

            if (includeDerivedProperties)
            {
                if (poco.view != null)
                {
                    foreach (var item in poco.view)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "view", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.viewCondition != null)
                {
                    foreach (var item in poco.viewCondition)
                    {
                        await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)item, "viewCondition", elementOriginMap, currentFileUri);
                    }
                }
            }

            if (includeDerivedProperties)
            {
                if (poco.viewRendering != null)
                {
                    await this.XmiDataWriterFacade.WriteReferenceElementAsync(xmlWriter, (IData)poco.viewRendering, "viewRendering", elementOriginMap, currentFileUri);
                }
            }


            await xmlWriter.WriteEndElementAsync();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
