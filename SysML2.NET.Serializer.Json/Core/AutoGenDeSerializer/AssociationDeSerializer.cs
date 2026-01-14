// -------------------------------------------------------------------------------------------------
// <copyright file="AssociationDeSerializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Kernel.Associations;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="AssociationDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IAssociation"/> interface
    /// </summary>
    internal static class AssociationDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IAssociation"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IAssociation"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IAssociation"/>
        /// </returns>
        internal static IAssociation DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("AssociationDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out var @type))
            {
                throw new InvalidOperationException("The @type property is not available, the AssociationDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "Association")
            {
                throw new InvalidOperationException($"The AssociationDeSerializer can only be used to deserialize objects of type IAssociation, a {@type.GetString()} was provided");
            }

            var dtoInstance = new SysML2.NET.Core.DTO.Kernel.Associations.Association();

            if (jsonElement.TryGetProperty("@id"u8, out var idProperty))
            {
                var propertyValue = idProperty.GetString();

                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the Association cannot be deserialized");
                }
                else
                {
                    dtoInstance.Id = Guid.Parse(propertyValue);
                }
            }

            if (jsonElement.TryGetProperty("aliasIds"u8, out var aliasIdsProperty))
            {
                foreach (var arrayItem in aliasIdsProperty.EnumerateArray())
                {
                    var propertyValue = arrayItem.GetString();

                    if (propertyValue != null)
                    {
                        dtoInstance.AliasIds.Add(propertyValue);
                    }
                }
            }
            else
            {
                logger.LogDebug("the aliasIds Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("associationEnd"u8, out var associationEndProperty))
            {
                foreach (var arrayItem in associationEndProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var associationEndExternalIdProperty))
                    {
                        var propertyValue = associationEndExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.associationEnd.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the associationEnd Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out var declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredName Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out var declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredShortName Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("differencingType"u8, out var differencingTypeProperty))
            {
                foreach (var arrayItem in differencingTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var differencingTypeExternalIdProperty))
                    {
                        var propertyValue = differencingTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.differencingType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the differencingType Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("directedFeature"u8, out var directedFeatureProperty))
            {
                foreach (var arrayItem in directedFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var directedFeatureExternalIdProperty))
                    {
                        var propertyValue = directedFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.directedFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the directedFeature Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("documentation"u8, out var documentationProperty))
            {
                foreach (var arrayItem in documentationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var documentationExternalIdProperty))
                    {
                        var propertyValue = documentationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.documentation.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the documentation Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("elementId"u8, out var elementIdProperty))
            {
                var propertyValue = elementIdProperty.GetString();

                if (propertyValue != null)
                {
                    dtoInstance.ElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug("the elementId Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("endFeature"u8, out var endFeatureProperty))
            {
                foreach (var arrayItem in endFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var endFeatureExternalIdProperty))
                    {
                        var propertyValue = endFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.endFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the endFeature Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("feature"u8, out var featureProperty))
            {
                foreach (var arrayItem in featureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var featureExternalIdProperty))
                    {
                        var propertyValue = featureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.feature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the feature Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("featureMembership"u8, out var featureMembershipProperty))
            {
                foreach (var arrayItem in featureMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var featureMembershipExternalIdProperty))
                    {
                        var propertyValue = featureMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.featureMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the featureMembership Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("importedMembership"u8, out var importedMembershipProperty))
            {
                foreach (var arrayItem in importedMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var importedMembershipExternalIdProperty))
                    {
                        var propertyValue = importedMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.importedMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the importedMembership Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("inheritedFeature"u8, out var inheritedFeatureProperty))
            {
                foreach (var arrayItem in inheritedFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var inheritedFeatureExternalIdProperty))
                    {
                        var propertyValue = inheritedFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.inheritedFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the inheritedFeature Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("inheritedMembership"u8, out var inheritedMembershipProperty))
            {
                foreach (var arrayItem in inheritedMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var inheritedMembershipExternalIdProperty))
                    {
                        var propertyValue = inheritedMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.inheritedMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the inheritedMembership Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("input"u8, out var inputProperty))
            {
                foreach (var arrayItem in inputProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var inputExternalIdProperty))
                    {
                        var propertyValue = inputExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.input.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the input Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("intersectingType"u8, out var intersectingTypeProperty))
            {
                foreach (var arrayItem in intersectingTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var intersectingTypeExternalIdProperty))
                    {
                        var propertyValue = intersectingTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.intersectingType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the intersectingType Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isAbstract"u8, out var isAbstractProperty))
            {
                if (isAbstractProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsAbstract = isAbstractProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isAbstract Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isConjugated"u8, out var isConjugatedProperty))
            {
                if (isConjugatedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.isConjugated = isConjugatedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isConjugated Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isImplied"u8, out var isImpliedProperty))
            {
                if (isImpliedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImplied = isImpliedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isImplied Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isImpliedIncluded"u8, out var isImpliedIncludedProperty))
            {
                if (isImpliedIncludedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImpliedIncluded = isImpliedIncludedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isImpliedIncluded Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isLibraryElement"u8, out var isLibraryElementProperty))
            {
                if (isLibraryElementProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.isLibraryElement = isLibraryElementProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isLibraryElement Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isSufficient"u8, out var isSufficientProperty))
            {
                if (isSufficientProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsSufficient = isSufficientProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isSufficient Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("member"u8, out var memberProperty))
            {
                foreach (var arrayItem in memberProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var memberExternalIdProperty))
                    {
                        var propertyValue = memberExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.member.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the member Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("membership"u8, out var membershipProperty))
            {
                foreach (var arrayItem in membershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var membershipExternalIdProperty))
                    {
                        var propertyValue = membershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.membership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the membership Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("multiplicity"u8, out var multiplicityProperty))
            {
                if (multiplicityProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.multiplicity = null;
                }
                else
                {
                    if (multiplicityProperty.TryGetProperty("@id"u8, out var multiplicityExternalIdProperty))
                    {
                        var propertyValue = multiplicityExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.multiplicity = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the multiplicity Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("name"u8, out var nameProperty))
            {
                dtoInstance.name = nameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the name Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("output"u8, out var outputProperty))
            {
                foreach (var arrayItem in outputProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var outputExternalIdProperty))
                    {
                        var propertyValue = outputExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.output.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the output Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedAnnotation"u8, out var ownedAnnotationProperty))
            {
                foreach (var arrayItem in ownedAnnotationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedAnnotationExternalIdProperty))
                    {
                        var propertyValue = ownedAnnotationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedAnnotation.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedAnnotation Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedConjugator"u8, out var ownedConjugatorProperty))
            {
                if (ownedConjugatorProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ownedConjugator = null;
                }
                else
                {
                    if (ownedConjugatorProperty.TryGetProperty("@id"u8, out var ownedConjugatorExternalIdProperty))
                    {
                        var propertyValue = ownedConjugatorExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedConjugator = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedConjugator Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedDifferencing"u8, out var ownedDifferencingProperty))
            {
                foreach (var arrayItem in ownedDifferencingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedDifferencingExternalIdProperty))
                    {
                        var propertyValue = ownedDifferencingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedDifferencing.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedDifferencing Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedDisjoining"u8, out var ownedDisjoiningProperty))
            {
                foreach (var arrayItem in ownedDisjoiningProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedDisjoiningExternalIdProperty))
                    {
                        var propertyValue = ownedDisjoiningExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedDisjoining.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedDisjoining Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedElement"u8, out var ownedElementProperty))
            {
                foreach (var arrayItem in ownedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedElementExternalIdProperty))
                    {
                        var propertyValue = ownedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedElement.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedElement Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedEndFeature"u8, out var ownedEndFeatureProperty))
            {
                foreach (var arrayItem in ownedEndFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedEndFeatureExternalIdProperty))
                    {
                        var propertyValue = ownedEndFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedEndFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedEndFeature Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedFeature"u8, out var ownedFeatureProperty))
            {
                foreach (var arrayItem in ownedFeatureProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedFeatureExternalIdProperty))
                    {
                        var propertyValue = ownedFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedFeature.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedFeature Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedFeatureMembership"u8, out var ownedFeatureMembershipProperty))
            {
                foreach (var arrayItem in ownedFeatureMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedFeatureMembershipExternalIdProperty))
                    {
                        var propertyValue = ownedFeatureMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedFeatureMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedFeatureMembership Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedImport"u8, out var ownedImportProperty))
            {
                foreach (var arrayItem in ownedImportProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedImportExternalIdProperty))
                    {
                        var propertyValue = ownedImportExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedImport.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedImport Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedIntersecting"u8, out var ownedIntersectingProperty))
            {
                foreach (var arrayItem in ownedIntersectingProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedIntersectingExternalIdProperty))
                    {
                        var propertyValue = ownedIntersectingExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedIntersecting.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedIntersecting Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMember"u8, out var ownedMemberProperty))
            {
                foreach (var arrayItem in ownedMemberProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedMemberExternalIdProperty))
                    {
                        var propertyValue = ownedMemberExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedMember.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedMember Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedMembership"u8, out var ownedMembershipProperty))
            {
                foreach (var arrayItem in ownedMembershipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedMembershipExternalIdProperty))
                    {
                        var propertyValue = ownedMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedMembership.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedMembership Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedRelatedElement"u8, out var ownedRelatedElementProperty))
            {
                foreach (var arrayItem in ownedRelatedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedRelatedElementExternalIdProperty))
                    {
                        var propertyValue = ownedRelatedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwnedRelatedElement.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedRelatedElement Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedRelationship"u8, out var ownedRelationshipProperty))
            {
                foreach (var arrayItem in ownedRelationshipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedRelationshipExternalIdProperty))
                    {
                        var propertyValue = ownedRelationshipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwnedRelationship.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedRelationship Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedSpecialization"u8, out var ownedSpecializationProperty))
            {
                foreach (var arrayItem in ownedSpecializationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedSpecializationExternalIdProperty))
                    {
                        var propertyValue = ownedSpecializationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedSpecialization.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedSpecialization Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedSubclassification"u8, out var ownedSubclassificationProperty))
            {
                foreach (var arrayItem in ownedSubclassificationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedSubclassificationExternalIdProperty))
                    {
                        var propertyValue = ownedSubclassificationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedSubclassification.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedSubclassification Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedUnioning"u8, out var ownedUnioningProperty))
            {
                foreach (var arrayItem in ownedUnioningProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedUnioningExternalIdProperty))
                    {
                        var propertyValue = ownedUnioningExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ownedUnioning.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the ownedUnioning Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owner"u8, out var ownerProperty))
            {
                if (ownerProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owner = null;
                }
                else
                {
                    if (ownerProperty.TryGetProperty("@id"u8, out var ownerExternalIdProperty))
                    {
                        var propertyValue = ownerExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owner = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owner Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningMembership"u8, out var owningMembershipProperty))
            {
                if (owningMembershipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owningMembership = null;
                }
                else
                {
                    if (owningMembershipProperty.TryGetProperty("@id"u8, out var owningMembershipExternalIdProperty))
                    {
                        var propertyValue = owningMembershipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owningMembership = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningMembership Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningNamespace"u8, out var owningNamespaceProperty))
            {
                if (owningNamespaceProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.owningNamespace = null;
                }
                else
                {
                    if (owningNamespaceProperty.TryGetProperty("@id"u8, out var owningNamespaceExternalIdProperty))
                    {
                        var propertyValue = owningNamespaceExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.owningNamespace = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningNamespace Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningRelatedElement"u8, out var owningRelatedElementProperty))
            {
                if (owningRelatedElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelatedElement = null;
                }
                else
                {
                    if (owningRelatedElementProperty.TryGetProperty("@id"u8, out var owningRelatedElementExternalIdProperty))
                    {
                        var propertyValue = owningRelatedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwningRelatedElement = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningRelatedElement Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningRelationship"u8, out var owningRelationshipProperty))
            {
                if (owningRelationshipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelationship = null;
                }
                else
                {
                    if (owningRelationshipProperty.TryGetProperty("@id"u8, out var owningRelationshipExternalIdProperty))
                    {
                        var propertyValue = owningRelationshipExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.OwningRelationship = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the owningRelationship Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("qualifiedName"u8, out var qualifiedNameProperty))
            {
                dtoInstance.qualifiedName = qualifiedNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the qualifiedName Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("relatedElement"u8, out var relatedElementProperty))
            {
                foreach (var arrayItem in relatedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var relatedElementExternalIdProperty))
                    {
                        var propertyValue = relatedElementExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.relatedElement.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the relatedElement Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("relatedType"u8, out var relatedTypeProperty))
            {
                foreach (var arrayItem in relatedTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var relatedTypeExternalIdProperty))
                    {
                        var propertyValue = relatedTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.relatedType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the relatedType Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("shortName"u8, out var shortNameProperty))
            {
                dtoInstance.shortName = shortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the shortName Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("source"u8, out var sourceProperty))
            {
                foreach (var arrayItem in sourceProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var sourceExternalIdProperty))
                    {
                        var propertyValue = sourceExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.Source.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the source Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("sourceType"u8, out var sourceTypeProperty))
            {
                if (sourceTypeProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.sourceType = null;
                }
                else
                {
                    if (sourceTypeProperty.TryGetProperty("@id"u8, out var sourceTypeExternalIdProperty))
                    {
                        var propertyValue = sourceTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.sourceType = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the sourceType Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("target"u8, out var targetProperty))
            {
                foreach (var arrayItem in targetProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var targetExternalIdProperty))
                    {
                        var propertyValue = targetExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.Target.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the target Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("targetType"u8, out var targetTypeProperty))
            {
                foreach (var arrayItem in targetTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var targetTypeExternalIdProperty))
                    {
                        var propertyValue = targetTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.targetType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the targetType Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("textualRepresentation"u8, out var textualRepresentationProperty))
            {
                foreach (var arrayItem in textualRepresentationProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var textualRepresentationExternalIdProperty))
                    {
                        var propertyValue = textualRepresentationExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.textualRepresentation.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the textualRepresentation Json property was not found in the Association: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("unioningType"u8, out var unioningTypeProperty))
            {
                foreach (var arrayItem in unioningTypeProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var unioningTypeExternalIdProperty))
                    {
                        var propertyValue = unioningTypeExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.unioningType.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the unioningType Json property was not found in the Association: { Id }", dtoInstance.Id);
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
