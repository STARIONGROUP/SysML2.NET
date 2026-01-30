// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceSubsettingDeSerializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ReferenceSubsettingDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IReferenceSubsetting"/> interface
    /// </summary>
    internal static class ReferenceSubsettingDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IReferenceSubsetting"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IReferenceSubsetting"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="deserializeDerivedProperties">
        /// Asserts that the deserializer should deserialize derived properties if present or if they are ignored
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IReferenceSubsetting"/>
        /// </returns>
        internal static IReferenceSubsetting DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("ReferenceSubsettingDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out var @type))
            {
                throw new InvalidOperationException("The @type property is not available, the ReferenceSubsettingDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "ReferenceSubsetting")
            {
                throw new InvalidOperationException($"The ReferenceSubsettingDeSerializer can only be used to deserialize objects of type IReferenceSubsetting, a {@type.GetString()} was provided");
            }

            var dtoInstance = new SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting();

            if (jsonElement.TryGetProperty("@id"u8, out var idProperty))
            {
                var propertyValue = idProperty.GetString();

                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the ReferenceSubsetting cannot be deserialized");
                }
                else
                {
                    dtoInstance.Id = Guid.Parse(propertyValue);
                }
            }

            if (deserializeDerivedProperties)
            {
                DeserializeDtoIncludingDerivedProperties(dtoInstance, jsonElement, logger);
            }
            else
            {
                DeserializeDtoExcludingDerivedProperties(dtoInstance, jsonElement, logger);
            }

            return dtoInstance;
        }

        /// <summary>
        /// Deserializes properties of a <see cref="ReferenceSubsetting" />
        /// from a <see cref="JsonElement" />, including derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="ReferenceSubsetting"/> instance holding deserialized values
        /// </param>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IReferenceSubsetting"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoIncludingDerivedProperties(SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting dtoInstance, JsonElement jsonElement, ILogger logger)
        {
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
                logger.LogDebug("the aliasIds Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out var declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredName Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out var declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredShortName Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the documentation Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the elementId Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the isImplied Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the isImpliedIncluded Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the isLibraryElement Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("name"u8, out var nameProperty))
            {
                dtoInstance.name = nameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the name Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the ownedAnnotation Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the ownedElement Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the ownedRelatedElement Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the ownedRelationship Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the owner Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the owningMembership Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the owningNamespace Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the owningRelatedElement Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the owningRelationship Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("qualifiedName"u8, out var qualifiedNameProperty))
            {
                dtoInstance.qualifiedName = qualifiedNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the qualifiedName Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("referencedFeature"u8, out var referencedFeatureProperty))
            {
                if (referencedFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ReferencedFeature = Guid.Empty;
                    logger.LogDebug($"the ReferenceSubsetting.ReferencedFeature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (referencedFeatureProperty.TryGetProperty("@id"u8, out var referencedFeatureExternalIdProperty))
                    {
                        var propertyValue = referencedFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ReferencedFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the referencedFeature Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("referencingFeature"u8, out var referencingFeatureProperty))
            {
                if (referencingFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.referencingFeature = Guid.Empty;
                    logger.LogDebug($"the ReferenceSubsetting.referencingFeature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (referencingFeatureProperty.TryGetProperty("@id"u8, out var referencingFeatureExternalIdProperty))
                    {
                        var propertyValue = referencingFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.referencingFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the referencingFeature Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the relatedElement Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("shortName"u8, out var shortNameProperty))
            {
                dtoInstance.shortName = shortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the shortName Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the textualRepresentation Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

        }

        /// <summary>
        /// Deserializes properties of a <see cref="ReferenceSubsetting" />
        /// from a <see cref="JsonElement" />, excluding derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="ReferenceSubsetting"/> instance holding deserialized values
        /// </param>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IReferenceSubsetting"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoExcludingDerivedProperties(SysML2.NET.Core.DTO.Core.Features.ReferenceSubsetting dtoInstance, JsonElement jsonElement, ILogger logger)
        {
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
                logger.LogDebug("the aliasIds Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out var declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredName Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out var declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredShortName Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the elementId Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the isImplied Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the isImpliedIncluded Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the ownedRelatedElement Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the ownedRelationship Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the owningRelatedElement Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
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
                logger.LogDebug("the owningRelationship Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("referencedFeature"u8, out var referencedFeatureProperty))
            {
                if (referencedFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.ReferencedFeature = Guid.Empty;
                    logger.LogDebug($"the ReferenceSubsetting.ReferencedFeature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (referencedFeatureProperty.TryGetProperty("@id"u8, out var referencedFeatureExternalIdProperty))
                    {
                        var propertyValue = referencedFeatureExternalIdProperty.GetString();

                        if (propertyValue != null)
                        {
                            dtoInstance.ReferencedFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug("the referencedFeature Json property was not found in the ReferenceSubsetting: {Id}", dtoInstance.Id);
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
