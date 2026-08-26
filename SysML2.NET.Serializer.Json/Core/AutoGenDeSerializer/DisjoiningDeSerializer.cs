// -------------------------------------------------------------------------------------------------
// <copyright file="DisjoiningDeSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Core.Types;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="DisjoiningDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IDisjoining"/> interface
    /// </summary>
    internal static class DisjoiningDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IDisjoining"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IDisjoining"/> json object. On return the reader is positioned on the matching
        /// <see cref="JsonTokenType.EndObject"/>
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
        /// an instance of <see cref="IDisjoining"/>
        /// </returns>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static IDisjoining DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("DisjoiningDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var dtoInstance = new SysML2.NET.Core.DTO.Core.Types.Disjoining();

            if (deserializeDerivedProperties)
            {
                DeserializeDtoIncludingDerivedProperties(dtoInstance, ref reader, logger);
            }
            else
            {
                DeserializeDtoExcludingDerivedProperties(dtoInstance, ref reader, logger);
            }

            return dtoInstance;
        }

        /// <summary>
        /// Deserializes properties of a <see cref="Disjoining" />
        /// from a <see cref="Utf8JsonReader" />, including derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="Disjoining"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IDisjoining"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoIncludingDerivedProperties(SysML2.NET.Core.DTO.Core.Types.Disjoining dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var aliasIdsSeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var disjoiningTypeSeen = false;
            var documentationSeen = false;
            var elementIdSeen = false;
            var isImpliedSeen = false;
            var isImpliedIncludedSeen = false;
            var isLibraryElementSeen = false;
            var nameSeen = false;
            var ownedAnnotationSeen = false;
            var ownedElementSeen = false;
            var ownedRelatedElementSeen = false;
            var ownedRelationshipSeen = false;
            var ownerSeen = false;
            var owningMembershipSeen = false;
            var owningNamespaceSeen = false;
            var owningRelatedElementSeen = false;
            var owningRelationshipSeen = false;
            var owningTypeSeen = false;
            var qualifiedNameSeen = false;
            var relatedElementSeen = false;
            var shortNameSeen = false;
            var textualRepresentationSeen = false;
            var typeDisjoinedSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the Disjoining json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the Disjoining cannot be deserialized");
                    }

                    dtoInstance.Id = Utf8JsonReaderHelper.ReadGuid(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("aliasIds"u8))
                {
                    aliasIdsSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        var aliasIdsValue = reader.GetString();

                        if (aliasIdsValue != null)
                        {
                            dtoInstance.AliasIds.Add(aliasIdsValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("declaredName"u8))
                {
                    declaredNameSeen = true;
                    reader.Read();

                    dtoInstance.DeclaredName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("declaredShortName"u8))
                {
                    declaredShortNameSeen = true;
                    reader.Read();

                    dtoInstance.DeclaredShortName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("disjoiningType"u8))
                {
                    disjoiningTypeSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.DisjoiningType = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the Disjoining.DisjoiningType property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var disjoiningTypeValue))
                    {
                        dtoInstance.DisjoiningType = disjoiningTypeValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("documentation"u8))
                {
                    documentationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var documentationValue))
                        {
                            dtoInstance.documentation.Add(documentationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("elementId"u8))
                {
                    elementIdSeen = true;
                    reader.Read();

                    var elementIdValue = reader.GetString();

                    if (elementIdValue != null)
                    {
                        dtoInstance.ElementId = elementIdValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isImplied"u8))
                {
                    isImpliedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsImplied = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isImpliedIncluded"u8))
                {
                    isImpliedIncludedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsImpliedIncluded = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isLibraryElement"u8))
                {
                    isLibraryElementSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.isLibraryElement = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("name"u8))
                {
                    nameSeen = true;
                    reader.Read();

                    dtoInstance.name = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("ownedAnnotation"u8))
                {
                    ownedAnnotationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedAnnotationValue))
                        {
                            dtoInstance.ownedAnnotation.Add(ownedAnnotationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedElement"u8))
                {
                    ownedElementSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedElementValue))
                        {
                            dtoInstance.ownedElement.Add(ownedElementValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedRelatedElement"u8))
                {
                    ownedRelatedElementSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRelatedElementValue))
                        {
                            dtoInstance.OwnedRelatedElement.Add(ownedRelatedElementValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedRelationship"u8))
                {
                    ownedRelationshipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRelationshipValue))
                        {
                            dtoInstance.OwnedRelationship.Add(ownedRelationshipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owner"u8))
                {
                    ownerSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owner = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownerValue))
                    {
                        dtoInstance.owner = ownerValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningMembership"u8))
                {
                    owningMembershipSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningMembership = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningMembershipValue))
                    {
                        dtoInstance.owningMembership = owningMembershipValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningNamespace"u8))
                {
                    owningNamespaceSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningNamespace = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningNamespaceValue))
                    {
                        dtoInstance.owningNamespace = owningNamespaceValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningRelatedElement"u8))
                {
                    owningRelatedElementSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.OwningRelatedElement = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningRelatedElementValue))
                    {
                        dtoInstance.OwningRelatedElement = owningRelatedElementValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningRelationship"u8))
                {
                    owningRelationshipSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.OwningRelationship = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningRelationshipValue))
                    {
                        dtoInstance.OwningRelationship = owningRelationshipValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningType"u8))
                {
                    owningTypeSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningType = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningTypeValue))
                    {
                        dtoInstance.owningType = owningTypeValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("qualifiedName"u8))
                {
                    qualifiedNameSeen = true;
                    reader.Read();

                    dtoInstance.qualifiedName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("relatedElement"u8))
                {
                    relatedElementSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var relatedElementValue))
                        {
                            dtoInstance.relatedElement.Add(relatedElementValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("shortName"u8))
                {
                    shortNameSeen = true;
                    reader.Read();

                    dtoInstance.shortName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("textualRepresentation"u8))
                {
                    textualRepresentationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var textualRepresentationValue))
                        {
                            dtoInstance.textualRepresentation.Add(textualRepresentationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("typeDisjoined"u8))
                {
                    typeDisjoinedSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.TypeDisjoined = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the Disjoining.TypeDisjoined property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var typeDisjoinedValue))
                    {
                        dtoInstance.TypeDisjoined = typeDisjoinedValue;
                    }

                    continue;
                }


                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!disjoiningTypeSeen)
                {
                    logger.LogDebug("the disjoiningType Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!documentationSeen)
                {
                    logger.LogDebug("the documentation Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!isImpliedSeen)
                {
                    logger.LogDebug("the isImplied Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!isLibraryElementSeen)
                {
                    logger.LogDebug("the isLibraryElement Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!nameSeen)
                {
                    logger.LogDebug("the name Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!ownedAnnotationSeen)
                {
                    logger.LogDebug("the ownedAnnotation Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!ownedElementSeen)
                {
                    logger.LogDebug("the ownedElement Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!ownedRelatedElementSeen)
                {
                    logger.LogDebug("the ownedRelatedElement Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!ownerSeen)
                {
                    logger.LogDebug("the owner Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!owningMembershipSeen)
                {
                    logger.LogDebug("the owningMembership Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!owningNamespaceSeen)
                {
                    logger.LogDebug("the owningNamespace Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!owningRelatedElementSeen)
                {
                    logger.LogDebug("the owningRelatedElement Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!owningTypeSeen)
                {
                    logger.LogDebug("the owningType Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!qualifiedNameSeen)
                {
                    logger.LogDebug("the qualifiedName Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!relatedElementSeen)
                {
                    logger.LogDebug("the relatedElement Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!shortNameSeen)
                {
                    logger.LogDebug("the shortName Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!textualRepresentationSeen)
                {
                    logger.LogDebug("the textualRepresentation Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!typeDisjoinedSeen)
                {
                    logger.LogDebug("the typeDisjoined Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
            }
        }

        /// <summary>
        /// Deserializes properties of a <see cref="Disjoining" />
        /// from a <see cref="Utf8JsonReader" />, excluding derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="Disjoining"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="IDisjoining"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoExcludingDerivedProperties(SysML2.NET.Core.DTO.Core.Types.Disjoining dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var aliasIdsSeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var disjoiningTypeSeen = false;
            var elementIdSeen = false;
            var isImpliedSeen = false;
            var isImpliedIncludedSeen = false;
            var ownedRelatedElementSeen = false;
            var ownedRelationshipSeen = false;
            var owningRelatedElementSeen = false;
            var owningRelationshipSeen = false;
            var typeDisjoinedSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the Disjoining json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the Disjoining cannot be deserialized");
                    }

                    dtoInstance.Id = Utf8JsonReaderHelper.ReadGuid(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("aliasIds"u8))
                {
                    aliasIdsSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        var aliasIdsValue = reader.GetString();

                        if (aliasIdsValue != null)
                        {
                            dtoInstance.AliasIds.Add(aliasIdsValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("declaredName"u8))
                {
                    declaredNameSeen = true;
                    reader.Read();

                    dtoInstance.DeclaredName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("declaredShortName"u8))
                {
                    declaredShortNameSeen = true;
                    reader.Read();

                    dtoInstance.DeclaredShortName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("disjoiningType"u8))
                {
                    disjoiningTypeSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.DisjoiningType = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the Disjoining.DisjoiningType property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var disjoiningTypeValue))
                    {
                        dtoInstance.DisjoiningType = disjoiningTypeValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("elementId"u8))
                {
                    elementIdSeen = true;
                    reader.Read();

                    var elementIdValue = reader.GetString();

                    if (elementIdValue != null)
                    {
                        dtoInstance.ElementId = elementIdValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isImplied"u8))
                {
                    isImpliedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsImplied = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("isImpliedIncluded"u8))
                {
                    isImpliedIncludedSeen = true;
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        dtoInstance.IsImpliedIncluded = reader.GetBoolean();
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedRelatedElement"u8))
                {
                    ownedRelatedElementSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRelatedElementValue))
                        {
                            dtoInstance.OwnedRelatedElement.Add(ownedRelatedElementValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("ownedRelationship"u8))
                {
                    ownedRelationshipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedRelationshipValue))
                        {
                            dtoInstance.OwnedRelationship.Add(ownedRelationshipValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningRelatedElement"u8))
                {
                    owningRelatedElementSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.OwningRelatedElement = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningRelatedElementValue))
                    {
                        dtoInstance.OwningRelatedElement = owningRelatedElementValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("owningRelationship"u8))
                {
                    owningRelationshipSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.OwningRelationship = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningRelationshipValue))
                    {
                        dtoInstance.OwningRelationship = owningRelationshipValue;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("typeDisjoined"u8))
                {
                    typeDisjoinedSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.TypeDisjoined = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the Disjoining.TypeDisjoined property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var typeDisjoinedValue))
                    {
                        dtoInstance.TypeDisjoined = typeDisjoinedValue;
                    }

                    continue;
                }


                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!disjoiningTypeSeen)
                {
                    logger.LogDebug("the disjoiningType Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!isImpliedSeen)
                {
                    logger.LogDebug("the isImplied Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!ownedRelatedElementSeen)
                {
                    logger.LogDebug("the ownedRelatedElement Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!owningRelatedElementSeen)
                {
                    logger.LogDebug("the owningRelatedElement Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
                if (!typeDisjoinedSeen)
                {
                    logger.LogDebug("the typeDisjoined Json property was not found in the Disjoining: {Id}", dtoInstance.Id);
                }
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
