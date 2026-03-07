// -------------------------------------------------------------------------------------------------
// <copyright file="OperatorExpressionWriter.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The purpose of the <see cref="OperatorExpressionWriter" /> is to write an instance of <see cref="IOperatorExpression" />
    /// to the XMI document
    /// </summary>
    public static class OperatorExpressionWriter
    {
        /// <summary>
        /// Writes the <see cref="IOperatorExpression" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="poco">The <see cref="IOperatorExpression" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="xmiWriterFacade">The <see cref="IXmiDataWriterFacade" /> for writing child elements</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public static void Write(XmlWriter xmlWriter, IOperatorExpression poco, string elementName, bool includeDerivedProperties, IXmiDataWriterFacade xmiWriterFacade, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null)
        {
            xmlWriter.WriteStartElement(elementName);
            xmlWriter.WriteAttributeString("xsi", "type", null, "sysml:OperatorExpression");
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
            if (poco.IsVariable)
            {
                xmlWriter.WriteAttributeString("isVariable", "true");
            }
            if (!string.IsNullOrWhiteSpace(poco.Operator))
            {
                xmlWriter.WriteAttributeString("operator", poco.Operator);
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
                if (poco.argument != null)
                {
                    foreach (var item in poco.argument)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "argument", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.chainingFeature != null)
                {
                    foreach (var item in poco.chainingFeature)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "chainingFeature", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.crossFeature != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.crossFeature, "crossFeature", elementOriginMap, currentFileUri);
                }
                if (poco.differencingType != null)
                {
                    foreach (var item in poco.differencingType)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "differencingType", elementOriginMap, currentFileUri);
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
                if (poco.function != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.function, "function", elementOriginMap, currentFileUri);
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
                if (poco.instantiatedType != null)
                {
                    xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)poco.instantiatedType, "instantiatedType", elementOriginMap, currentFileUri);
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
                if (poco.textualRepresentation != null)
                {
                    foreach (var item in poco.textualRepresentation)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "textualRepresentation", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.type != null)
                {
                    foreach (var item in poco.type)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "type", elementOriginMap, currentFileUri);
                    }
                }
                if (poco.unioningType != null)
                {
                    foreach (var item in poco.unioningType)
                    {
                        xmiWriterFacade.WriteReferenceElement(xmlWriter, (IData)item, "unioningType", elementOriginMap, currentFileUri);
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
