// -------------------------------------------------------------------------------------------------
// <copyright file="BindingConnectorAsUsageWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
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
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="BindingConnectorAsUsageWriter" /> is to write an instance of <see cref="IBindingConnectorAsUsage" />
    /// to the XMI document
    /// </summary>
    public static class BindingConnectorAsUsageWriter
    {
        /// <summary>
        /// Writes the <see cref="IBindingConnectorAsUsage" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IBindingConnectorAsUsage" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="xmiWriterFacade">The <see cref="IXmiDataWriterFacade" /> for writing child elements</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public static void Write(XmlWriter xmlWriter, IBindingConnectorAsUsage poco, string elementName, bool includeDerivedProperties, IXmiDataWriterFacade xmiWriterFacade, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:BindingConnectorAsUsage");
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
            if (poco.IsImplied)
            {
                xmlWriter.WriteAttributeString("isImplied", "true");
            }
            if (poco.IsImpliedIncluded)
            {
                xmlWriter.WriteAttributeString("isImpliedIncluded", "true");
            }
            if (poco.IsOrdered)
            {
                xmlWriter.WriteAttributeString("isOrdered", "true");
            }
            if (poco.IsPortion)
            {
                xmlWriter.WriteAttributeString("isPortion", "true");
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
                if (poco.isReference)
                {
                    xmlWriter.WriteAttributeString("isReference", "true");
                }
                if (poco.mayTimeVary)
                {
                    xmlWriter.WriteAttributeString("mayTimeVary", "true");
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
            if (poco.OwnedRelatedElement != null)
            {
                foreach (var item in poco.OwnedRelatedElement)
                {
                    xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelatedElement", includeDerivedProperties, elementOriginMap, currentFileUri);
                }
            }
            if (poco.OwnedRelationship != null)
            {
                foreach (var item in poco.OwnedRelationship)
                {
                    xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRelationship", includeDerivedProperties, elementOriginMap, currentFileUri);
                }
            }
            if (poco.OwningRelatedElement != null)
            {
                xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.OwningRelatedElement, "owningRelatedElement", elementOriginMap, currentFileUri);
            }
            if (poco.OwningRelationship != null)
            {
                xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.OwningRelationship, "owningRelationship", elementOriginMap, currentFileUri);
            }

            // Derived reference/containment properties as child elements
            if (includeDerivedProperties)
            {
                if (poco.association != null)
                {
                    foreach (var item in poco.association)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "association", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.chainingFeature != null)
                {
                    foreach (var item in poco.chainingFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "chainingFeature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.connectorEnd != null)
                {
                    foreach (var item in poco.connectorEnd)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "connectorEnd", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.crossFeature != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.crossFeature, "crossFeature", elementOriginMap, currentFileUri);
                }
                if (poco.defaultFeaturingType != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.defaultFeaturingType, "defaultFeaturingType", elementOriginMap, currentFileUri);
                }
                if (poco.definition != null)
                {
                    foreach (var item in poco.definition)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "definition", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.differencingType != null)
                {
                    foreach (var item in poco.differencingType)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "differencingType", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.directedFeature != null)
                {
                    foreach (var item in poco.directedFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "directedFeature", elementOriginMap, currentFileUri);
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
                if (poco.endOwningType != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.endOwningType, "endOwningType", elementOriginMap, currentFileUri);
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
                if (poco.featureTarget != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.featureTarget, "featureTarget", elementOriginMap, currentFileUri);
                }
                if (poco.featuringType != null)
                {
                    foreach (var item in poco.featuringType)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "featuringType", elementOriginMap, currentFileUri);
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
                if (poco.nestedAction != null)
                {
                    foreach (var item in poco.nestedAction)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAction", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedAllocation != null)
                {
                    foreach (var item in poco.nestedAllocation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAllocation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedAnalysisCase != null)
                {
                    foreach (var item in poco.nestedAnalysisCase)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAnalysisCase", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedAttribute != null)
                {
                    foreach (var item in poco.nestedAttribute)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedAttribute", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedCalculation != null)
                {
                    foreach (var item in poco.nestedCalculation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedCalculation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedCase != null)
                {
                    foreach (var item in poco.nestedCase)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedCase", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedConcern != null)
                {
                    foreach (var item in poco.nestedConcern)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConcern", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedConnection != null)
                {
                    foreach (var item in poco.nestedConnection)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConnection", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedConstraint != null)
                {
                    foreach (var item in poco.nestedConstraint)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedConstraint", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedEnumeration != null)
                {
                    foreach (var item in poco.nestedEnumeration)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedEnumeration", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedFlow != null)
                {
                    foreach (var item in poco.nestedFlow)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedFlow", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedInterface != null)
                {
                    foreach (var item in poco.nestedInterface)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedInterface", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedItem != null)
                {
                    foreach (var item in poco.nestedItem)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedItem", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedMetadata != null)
                {
                    foreach (var item in poco.nestedMetadata)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedMetadata", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedOccurrence != null)
                {
                    foreach (var item in poco.nestedOccurrence)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedOccurrence", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedPart != null)
                {
                    foreach (var item in poco.nestedPart)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedPart", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedPort != null)
                {
                    foreach (var item in poco.nestedPort)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedPort", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedReference != null)
                {
                    foreach (var item in poco.nestedReference)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedReference", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedRendering != null)
                {
                    foreach (var item in poco.nestedRendering)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedRendering", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedRequirement != null)
                {
                    foreach (var item in poco.nestedRequirement)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedRequirement", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedState != null)
                {
                    foreach (var item in poco.nestedState)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedState", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedTransition != null)
                {
                    foreach (var item in poco.nestedTransition)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedTransition", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedUsage != null)
                {
                    foreach (var item in poco.nestedUsage)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedUsage", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedUseCase != null)
                {
                    foreach (var item in poco.nestedUseCase)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedUseCase", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedVerificationCase != null)
                {
                    foreach (var item in poco.nestedVerificationCase)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedVerificationCase", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedView != null)
                {
                    foreach (var item in poco.nestedView)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedView", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.nestedViewpoint != null)
                {
                    foreach (var item in poco.nestedViewpoint)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "nestedViewpoint", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.output != null)
                {
                    foreach (var item in poco.output)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "output", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedAnnotation != null)
                {
                    foreach (var item in poco.ownedAnnotation)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedAnnotation", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedConjugator != null)
                {
                    xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedConjugator, "ownedConjugator", includeDerivedProperties, elementOriginMap, currentFileUri);
                }
                if (poco.ownedCrossSubsetting != null)
                {
                    xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedCrossSubsetting, "ownedCrossSubsetting", includeDerivedProperties, elementOriginMap, currentFileUri);
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
                if (poco.ownedFeature != null)
                {
                    foreach (var item in poco.ownedFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "ownedFeature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedFeatureChaining != null)
                {
                    foreach (var item in poco.ownedFeatureChaining)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureChaining", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedFeatureInverting != null)
                {
                    foreach (var item in poco.ownedFeatureInverting)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureInverting", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedFeatureMembership != null)
                {
                    foreach (var item in poco.ownedFeatureMembership)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedFeatureMembership", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedImport != null)
                {
                    foreach (var item in poco.ownedImport)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedImport", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedIntersecting != null)
                {
                    foreach (var item in poco.ownedIntersecting)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedIntersecting", includeDerivedProperties, elementOriginMap, currentFileUri);
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
                if (poco.ownedRedefinition != null)
                {
                    foreach (var item in poco.ownedRedefinition)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedRedefinition", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedReferenceSubsetting != null)
                {
                    xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)poco.ownedReferenceSubsetting, "ownedReferenceSubsetting", includeDerivedProperties, elementOriginMap, currentFileUri);
                }
                if (poco.ownedSpecialization != null)
                {
                    foreach (var item in poco.ownedSpecialization)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSpecialization", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedSubsetting != null)
                {
                    foreach (var item in poco.ownedSubsetting)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedSubsetting", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedTypeFeaturing != null)
                {
                    foreach (var item in poco.ownedTypeFeaturing)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedTypeFeaturing", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedTyping != null)
                {
                    foreach (var item in poco.ownedTyping)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedTyping", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.ownedUnioning != null)
                {
                    foreach (var item in poco.ownedUnioning)
                    {
                        xmiWriterFacade.WriteContainedElement(xmlWriter, (IData)item, "ownedUnioning", includeDerivedProperties, elementOriginMap, currentFileUri);
                    }
                }
                if (poco.owner != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owner, "owner", elementOriginMap, currentFileUri);
                }
                if (poco.owningDefinition != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningDefinition, "owningDefinition", elementOriginMap, currentFileUri);
                }
                if (poco.owningFeatureMembership != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningFeatureMembership, "owningFeatureMembership", elementOriginMap, currentFileUri);
                }
                if (poco.owningMembership != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningMembership, "owningMembership", elementOriginMap, currentFileUri);
                }
                if (poco.owningNamespace != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningNamespace, "owningNamespace", elementOriginMap, currentFileUri);
                }
                if (poco.owningType != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningType, "owningType", elementOriginMap, currentFileUri);
                }
                if (poco.owningUsage != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.owningUsage, "owningUsage", elementOriginMap, currentFileUri);
                }
                if (poco.relatedFeature != null)
                {
                    foreach (var item in poco.relatedFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "relatedFeature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.sourceFeature != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.sourceFeature, "sourceFeature", elementOriginMap, currentFileUri);
                }
                if (poco.targetFeature != null)
                {
                    foreach (var item in poco.targetFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "targetFeature", elementOriginMap, currentFileUri);
                    }
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
            }

            xmlWriter.WriteEndElement();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
