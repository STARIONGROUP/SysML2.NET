// -------------------------------------------------------------------------------------------------
// <copyright file="TextualRepresentationDeSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Root.Annotations;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="TextualRepresentationDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="ITextualRepresentation"/> interface
    /// </summary>
    internal static class TextualRepresentationDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="ITextualRepresentation"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="ITextualRepresentation"/> json object. On return the reader is positioned on the matching
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
        /// an instance of <see cref="ITextualRepresentation"/>
        /// </returns>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static ITextualRepresentation DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("TextualRepresentationDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var dtoInstance = new SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation();

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
        /// Deserializes properties of a <see cref="TextualRepresentation" />
        /// from a <see cref="Utf8JsonReader" />, including derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="TextualRepresentation"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="ITextualRepresentation"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoIncludingDerivedProperties(SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var aliasIdsSeen = false;
            var annotationSeen = false;
            var bodySeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var documentationSeen = false;
            var elementIdSeen = false;
            var isImpliedIncludedSeen = false;
            var isLibraryElementSeen = false;
            var languageSeen = false;
            var nameSeen = false;
            var ownedAnnotatingRelationshipSeen = false;
            var ownedAnnotationSeen = false;
            var ownedElementSeen = false;
            var ownedRelationshipSeen = false;
            var ownerSeen = false;
            var owningAnnotatingRelationshipSeen = false;
            var owningMembershipSeen = false;
            var owningNamespaceSeen = false;
            var owningRelationshipSeen = false;
            var qualifiedNameSeen = false;
            var representedElementSeen = false;
            var shortNameSeen = false;
            var textualRepresentationSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the TextualRepresentation json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the TextualRepresentation cannot be deserialized");
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

                if (reader.ValueTextEquals("annotation"u8))
                {
                    annotationSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var annotationValue))
                        {
                            dtoInstance.annotation.Add(annotationValue);
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("body"u8))
                {
                    bodySeen = true;
                    reader.Read();

                    var bodyValue = reader.GetString();

                    if (bodyValue != null)
                    {
                        dtoInstance.Body = bodyValue;
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

                if (reader.ValueTextEquals("language"u8))
                {
                    languageSeen = true;
                    reader.Read();

                    var languageValue = reader.GetString();

                    if (languageValue != null)
                    {
                        dtoInstance.Language = languageValue;
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

                if (reader.ValueTextEquals("ownedAnnotatingRelationship"u8))
                {
                    ownedAnnotatingRelationshipSeen = true;
                    reader.Read();

                    Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var ownedAnnotatingRelationshipValue))
                        {
                            dtoInstance.ownedAnnotatingRelationship.Add(ownedAnnotatingRelationshipValue);
                        }
                    }

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

                if (reader.ValueTextEquals("owningAnnotatingRelationship"u8))
                {
                    owningAnnotatingRelationshipSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.owningAnnotatingRelationship = null;
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningAnnotatingRelationshipValue))
                    {
                        dtoInstance.owningAnnotatingRelationship = owningAnnotatingRelationshipValue;
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

                if (reader.ValueTextEquals("qualifiedName"u8))
                {
                    qualifiedNameSeen = true;
                    reader.Read();

                    dtoInstance.qualifiedName = reader.GetString();

                    continue;
                }

                if (reader.ValueTextEquals("representedElement"u8))
                {
                    representedElementSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        dtoInstance.representedElement = Guid.Empty;

                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug("the TextualRepresentation.representedElement property was not found in the Json. The value is set to Guid.Empty");
                        }
                    }
                    else if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var representedElementValue))
                    {
                        dtoInstance.representedElement = representedElementValue;
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


                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!annotationSeen)
                {
                    logger.LogDebug("the annotation Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!bodySeen)
                {
                    logger.LogDebug("the body Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!documentationSeen)
                {
                    logger.LogDebug("the documentation Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!isLibraryElementSeen)
                {
                    logger.LogDebug("the isLibraryElement Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!languageSeen)
                {
                    logger.LogDebug("the language Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!nameSeen)
                {
                    logger.LogDebug("the name Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!ownedAnnotatingRelationshipSeen)
                {
                    logger.LogDebug("the ownedAnnotatingRelationship Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!ownedAnnotationSeen)
                {
                    logger.LogDebug("the ownedAnnotation Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!ownedElementSeen)
                {
                    logger.LogDebug("the ownedElement Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!ownerSeen)
                {
                    logger.LogDebug("the owner Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!owningAnnotatingRelationshipSeen)
                {
                    logger.LogDebug("the owningAnnotatingRelationship Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!owningMembershipSeen)
                {
                    logger.LogDebug("the owningMembership Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!owningNamespaceSeen)
                {
                    logger.LogDebug("the owningNamespace Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!qualifiedNameSeen)
                {
                    logger.LogDebug("the qualifiedName Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!representedElementSeen)
                {
                    logger.LogDebug("the representedElement Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!shortNameSeen)
                {
                    logger.LogDebug("the shortName Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!textualRepresentationSeen)
                {
                    logger.LogDebug("the textualRepresentation Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
            }
        }

        /// <summary>
        /// Deserializes properties of a <see cref="TextualRepresentation" />
        /// from a <see cref="Utf8JsonReader" />, excluding derived properties
        /// </summary>
        /// <param name="dtoInstance">
        /// The <see cref="TextualRepresentation"/> instance holding deserialized values
        /// </param>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="ITextualRepresentation"/> json object
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> to produce logging statement
        /// </param>
        private static void DeserializeDtoExcludingDerivedProperties(SysML2.NET.Core.DTO.Root.Annotations.TextualRepresentation dtoInstance, ref Utf8JsonReader reader, ILogger logger)
        {
            var aliasIdsSeen = false;
            var bodySeen = false;
            var declaredNameSeen = false;
            var declaredShortNameSeen = false;
            var elementIdSeen = false;
            var isImpliedIncludedSeen = false;
            var languageSeen = false;
            var ownedRelationshipSeen = false;
            var owningRelationshipSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the TextualRepresentation json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is not present, the TextualRepresentation cannot be deserialized");
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

                if (reader.ValueTextEquals("body"u8))
                {
                    bodySeen = true;
                    reader.Read();

                    var bodyValue = reader.GetString();

                    if (bodyValue != null)
                    {
                        dtoInstance.Body = bodyValue;
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

                if (reader.ValueTextEquals("language"u8))
                {
                    languageSeen = true;
                    reader.Read();

                    var languageValue = reader.GetString();

                    if (languageValue != null)
                    {
                        dtoInstance.Language = languageValue;
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


                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!aliasIdsSeen)
                {
                    logger.LogDebug("the aliasIds Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!bodySeen)
                {
                    logger.LogDebug("the body Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!declaredNameSeen)
                {
                    logger.LogDebug("the declaredName Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!declaredShortNameSeen)
                {
                    logger.LogDebug("the declaredShortName Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!elementIdSeen)
                {
                    logger.LogDebug("the elementId Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!isImpliedIncludedSeen)
                {
                    logger.LogDebug("the isImpliedIncluded Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!languageSeen)
                {
                    logger.LogDebug("the language Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!ownedRelationshipSeen)
                {
                    logger.LogDebug("the ownedRelationship Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
                if (!owningRelationshipSeen)
                {
                    logger.LogDebug("the owningRelationship Json property was not found in the TextualRepresentation: {Id}", dtoInstance.Id);
                }
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
