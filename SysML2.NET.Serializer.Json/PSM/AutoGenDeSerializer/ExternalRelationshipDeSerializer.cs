// -------------------------------------------------------------------------------------------------
// <copyright file="ExternalRelationshipDeSerializer.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Serializer.Json.PSM
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Extensions.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="ExternalRelationshipDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="ExternalRelationship"/> class
    /// </summary>
    internal static class ExternalRelationshipDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="ExternalRelationship"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="ExternalRelationship"/> json object. On return the reader is positioned on the matching
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
        /// an instance of <see cref="ExternalRelationship"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the serialization mode is not supported
        /// </exception>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static ExternalRelationship DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            if (serializationModeKind != SerializationModeKind.JSON)
            {
                throw new NotSupportedException($"The {serializationModeKind} serialization mode is not supported");
            }

            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("ExternalRelationshipDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var externalRelationship = new ExternalRelationship();

            var idSeen = false;
            var elementEndSeen = false;
            var externalDataEndSeen = false;
            var languageSeen = false;
            var specificationSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the ExternalRelationship json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    idSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is null, the ExternalRelationship cannot be deserialized");
                    }

                    externalRelationship.Id = Utf8JsonReaderHelper.ReadGuid(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("elementEnd"u8))
                {
                    elementEndSeen = true;
                    reader.Read();

                    if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var elementEndValue))
                    {
                        externalRelationship.ElementEnd = elementEndValue;
                    }
                    else if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug("the ExternalRelationship.elementEnd reference is null, the value is left at Guid.Empty");
                    }

                    continue;
                }

                if (reader.ValueTextEquals("externalDataEnd"u8))
                {
                    externalDataEndSeen = true;
                    reader.Read();

                    if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var externalDataEndValue))
                    {
                        externalRelationship.ExternalDataEnd = externalDataEndValue;
                    }
                    else if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug("the ExternalRelationship.externalDataEnd reference is null, the value is left at Guid.Empty");
                    }

                    continue;
                }

                if (reader.ValueTextEquals("language"u8))
                {
                    languageSeen = true;
                    reader.Read();

                    externalRelationship.Language = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("specification"u8))
                {
                    specificationSeen = true;
                    reader.Read();

                    externalRelationship.Specification = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

                    continue;
                }

                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!idSeen)
                {
                    logger.LogDebug("the @id Json property was not found in the ExternalRelationship");
                }

                if (!elementEndSeen)
                {
                    logger.LogDebug("the elementEnd Json property was not found in the ExternalRelationship");
                }

                if (!externalDataEndSeen)
                {
                    logger.LogDebug("the externalDataEnd Json property was not found in the ExternalRelationship");
                }

                if (!languageSeen)
                {
                    logger.LogDebug("the language Json property was not found in the ExternalRelationship");
                }

                if (!specificationSeen)
                {
                    logger.LogDebug("the specification Json property was not found in the ExternalRelationship");
                }

            }

            return externalRelationship;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
