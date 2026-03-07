// -------------------------------------------------------------------------------------------------
// <copyright file="VerificationCaseDefinitionWriter.cs" company="Starion Group S.A.">
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
    using System.Xml;

    using SysML2.NET.Common;
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
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="VerificationCaseDefinitionWriter" /> is to write an instance of <see cref="IVerificationCaseDefinition" />
    /// to the XMI document
    /// </summary>
    public static class VerificationCaseDefinitionWriter
    {
        /// <summary>
        /// Writes the <see cref="IVerificationCaseDefinition" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IVerificationCaseDefinition" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="xmiWriterFacade">The <see cref="IXmiDataWriterFacade" /> for writing child elements</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public static void Write(XmlWriter xmlWriter, IVerificationCaseDefinition poco, string elementName, bool includeDerivedProperties, IXmiDataWriterFacade xmiWriterFacade, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:VerificationCaseDefinition");
            xmlWriter.WriteAttributeString("xmi", "id", null, poco.Id.ToString());

            // Scalar properties as XML attributes
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
            if (poco.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }
            if (poco.IsIndividual)
            {
                xmlWriter.WriteAttributeString("isIndividual", "true");
            }
            if (poco.IsSufficient)
            {
                xmlWriter.WriteAttributeString("isSufficient", "true");
            }
            if (poco.IsVariation)
            {
                xmlWriter.WriteAttributeString("isVariation", "true");
            }

            // Derived scalar properties
            if (includeDerivedProperties)
            {
                if (poco.isConjugated)
                {
                    xmlWriter.WriteAttributeString("isConjugated", "true");
                }
                if (poco.isLibraryElement)
                {
                    xmlWriter.WriteAttributeString("isLibraryElement", "true");
                }
                if (poco.isModelLevelEvaluable)
                {
                    xmlWriter.WriteAttributeString("isModelLevelEvaluable", "true");
                }
                if (!string.IsNullOrWhiteSpace(poco.name))
                {
                    xmlWriter.WriteAttributeString("name", poco.name);
                }
                if (!string.IsNullOrWhiteSpace(poco.qualifiedName))
                {
                    xmlWriter.WriteAttributeString("qualifiedName", poco.qualifiedName);
                }
                if (!string.IsNullOrWhiteSpace(poco.shortName))
                {
                    xmlWriter.WriteAttributeString("shortName", poco.shortName);
                }
            }

            // Reference/containment properties as child elements
            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelationship", includeDerivedProperties, elementOriginMap, currentFileUri);
                }
            }
            if (poco.OwningRelationship != null)
            {
                xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            // Derived reference/containment properties as child elements
            if (includeDerivedProperties)
            {
                if (poco.action != null)
                {
                    foreach (var item in poco.action)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "action", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.actorParameter != null)
                {
                    foreach (var item in poco.actorParameter)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "actorParameter", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.calculation != null)
                {
                    foreach (var item in poco.calculation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "calculation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.differencingType != null)
                {
                    foreach (var item in poco.differencingType)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "differencingType", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.directedUsage != null)
                {
                    foreach (var item in poco.directedUsage)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "directedUsage", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.documentation != null)
                {
                    foreach (var item in poco.documentation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "documentation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.endFeature != null)
                {
                    foreach (var item in poco.endFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "endFeature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.expression != null)
                {
                    foreach (var item in poco.expression)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "expression", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.feature != null)
                {
                    foreach (var item in poco.feature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "feature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.featureMembership != null)
                {
                    foreach (var item in poco.featureMembership)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "featureMembership", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.importedMembership != null)
                {
                    foreach (var item in poco.importedMembership)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "importedMembership", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.inheritedFeature != null)
                {
                    foreach (var item in poco.inheritedFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "inheritedFeature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.inheritedMembership != null)
                {
                    foreach (var item in poco.inheritedMembership)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "inheritedMembership", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.input != null)
                {
                    foreach (var item in poco.input)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "input", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.intersectingType != null)
                {
                    foreach (var item in poco.intersectingType)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "intersectingType", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.member != null)
                {
                    foreach (var item in poco.member)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "member", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.membership != null)
                {
                    foreach (var item in poco.membership)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "membership", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.multiplicity != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.multiplicity, "multiplicity", elementOriginMap, currentFileUri);
                }
                if (poco.objectiveRequirement != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.objectiveRequirement, "objectiveRequirement", elementOriginMap, currentFileUri);
                }
                if (poco.output != null)
                {
                    foreach (var item in poco.output)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "output", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedAction != null)
                {
                    foreach (var item in poco.ownedAction)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedAction", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedAllocation != null)
                {
                    foreach (var item in poco.ownedAllocation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedAllocation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedAnalysisCase != null)
                {
                    foreach (var item in poco.ownedAnalysisCase)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedAnalysisCase", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedAnnotation", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedAttribute != null)
                {
                    foreach (var item in poco.ownedAttribute)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedAttribute", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedCalculation != null)
                {
                    foreach (var item in poco.ownedCalculation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedCalculation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedCase != null)
                {
                    foreach (var item in poco.ownedCase)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedCase", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedConcern != null)
                {
                    foreach (var item in poco.ownedConcern)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedConcern", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedConjugator != null)
                {
                    xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedConjugator, "ownedConjugator", includeDerivedProperties, elementOriginMap, currentFileUri);
                }
                if (poco.ownedConnection != null)
                {
                    foreach (var item in poco.ownedConnection)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedConnection", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedConstraint != null)
                {
                    foreach (var item in poco.ownedConstraint)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedConstraint", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedDifferencing != null)
                {
                    foreach (var item in poco.ownedDifferencing)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedDifferencing", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedDisjoining != null)
                {
                    foreach (var item in poco.ownedDisjoining)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedDisjoining", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedElement != null)
                {
                    foreach (var item in poco.ownedElement)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedElement", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedEndFeature != null)
                {
                    foreach (var item in poco.ownedEndFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedEndFeature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedEnumeration != null)
                {
                    foreach (var item in poco.ownedEnumeration)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedEnumeration", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedFeature != null)
                {
                    foreach (var item in poco.ownedFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedFeature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedFeatureMembership != null)
                {
                    foreach (var item in poco.ownedFeatureMembership)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureMembership", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedFlow != null)
                {
                    foreach (var item in poco.ownedFlow)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedFlow", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedImport != null)
                {
                    foreach (var item in poco.ownedImport)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedImport", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedInterface != null)
                {
                    foreach (var item in poco.ownedInterface)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedInterface", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedIntersecting != null)
                {
                    foreach (var item in poco.ownedIntersecting)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedIntersecting", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedItem != null)
                {
                    foreach (var item in poco.ownedItem)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedItem", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedMember != null)
                {
                    foreach (var item in poco.ownedMember)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedMember", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedMembership != null)
                {
                    foreach (var item in poco.ownedMembership)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedMembership", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedMetadata != null)
                {
                    foreach (var item in poco.ownedMetadata)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedMetadata", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedOccurrence != null)
                {
                    foreach (var item in poco.ownedOccurrence)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedOccurrence", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedPart != null)
                {
                    foreach (var item in poco.ownedPart)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedPart", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedPort != null)
                {
                    foreach (var item in poco.ownedPort)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedPort", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedReference != null)
                {
                    foreach (var item in poco.ownedReference)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedReference", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedRendering != null)
                {
                    foreach (var item in poco.ownedRendering)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedRendering", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedRequirement != null)
                {
                    foreach (var item in poco.ownedRequirement)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedRequirement", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedSpecialization != null)
                {
                    foreach (var item in poco.ownedSpecialization)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSpecialization", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedState != null)
                {
                    foreach (var item in poco.ownedState)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedState", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedSubclassification != null)
                {
                    foreach (var item in poco.ownedSubclassification)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSubclassification", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedTransition != null)
                {
                    foreach (var item in poco.ownedTransition)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedTransition", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedUnioning != null)
                {
                    foreach (var item in poco.ownedUnioning)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedUnioning", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedUsage != null)
                {
                    foreach (var item in poco.ownedUsage)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedUsage", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedUseCase != null)
                {
                    foreach (var item in poco.ownedUseCase)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedUseCase", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedVerificationCase != null)
                {
                    foreach (var item in poco.ownedVerificationCase)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedVerificationCase", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedView != null)
                {
                    foreach (var item in poco.ownedView)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedView", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedViewpoint != null)
                {
                    foreach (var item in poco.ownedViewpoint)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedViewpoint", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.owner != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owner, "owner", elementOriginMap, currentFileUri);
                }
                if (poco.owningMembership != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningMembership, "owningMembership", elementOriginMap, currentFileUri);
                }
                if (poco.owningNamespace != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                }
                if (poco.parameter != null)
                {
                    foreach (var item in poco.parameter)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "parameter", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.result != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.result, "result", elementOriginMap, currentFileUri);
                }
                if (poco.step != null)
                {
                    foreach (var item in poco.step)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "step", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.subjectParameter != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.subjectParameter, "subjectParameter", elementOriginMap, currentFileUri);
                }
                if (poco.textualRepresentation != null)
                {
                    foreach (var item in poco.textualRepresentation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "textualRepresentation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.unioningType != null)
                {
                    foreach (var item in poco.unioningType)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "unioningType", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.usage != null)
                {
                    foreach (var item in poco.usage)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "usage", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.variant != null)
                {
                    foreach (var item in poco.variant)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "variant", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.variantMembership != null)
                {
                    foreach (var item in poco.variantMembership)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "variantMembership", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.verifiedRequirement != null)
                {
                    foreach (var item in poco.verifiedRequirement)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "verifiedRequirement", elementOriginMap, currentFileUri);
                    }
                }
            }

            xmlWriter.WriteEndElement();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
