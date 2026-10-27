// ------------------------------------------------------------------------------------------------
// <copyright file="DataVersionDeSerializer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.PIM.DTO
{
    using System;
    using System.Text;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.PIM.DTO;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.Json.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="DataVersionDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="DataVersion"/> class
    /// </summary>
    internal static class DataVersionDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="DataVersion"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="DataVersion"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="DataVersion"/>
        /// </returns>
        internal static DataVersion DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("DataVersionDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the DataVersionDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "DataVersion")
            {
                throw new InvalidOperationException($"The DataVersionDeSerializer can only be used to deserialize objects of type DataVersion, a {@type.GetString()} was provided");
            }

            logger.Log(LogLevel.Trace, "start deserialization: DataVersion");

            var dtoInstance = new DataVersion();

            if (jsonElement.TryGetProperty("@id"u8, out JsonElement idPropertyVersionItem))
            {
                var idPropertyVersionItemValue = idPropertyVersionItem.GetString();
                if (idPropertyVersionItemValue == null)
                {
                    throw new JsonException("The @id property is not present, the DataVersion cannot be deserialized");
                }

                dtoInstance.Id = Guid.Parse(idPropertyVersionItemValue);
            }

            if (jsonElement.TryGetProperty("alias"u8, out JsonElement aliasProperty))
            {
                foreach (var item in aliasProperty.EnumerateArray())
                {
                    dtoInstance.Alias.Add(item.GetString());
                }
            }
            else
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("the alias Json property was not found in the DataVersion: {Id}", dtoInstance.Id);
                }
            }

            if (jsonElement.TryGetProperty("description"u8, out JsonElement descriptionProperty))
            {
                var propertyValue = descriptionProperty.GetString();
                if (propertyValue != null)
                {
                    dtoInstance.Description = propertyValue;
                }
            }
            else
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("the name Json property was not found in the DataVersion: {Id}", dtoInstance.Id);
                }
            }

            if (jsonElement.TryGetProperty("commit"u8, out JsonElement commitProperty)
                && commitProperty.ValueKind != JsonValueKind.Null
                && commitProperty.TryGetProperty("@id"u8, out JsonElement commitIdProperty))
            {
                var propertyValue = commitIdProperty.GetString();
                if (propertyValue != null)
                {
                    dtoInstance.Commit = Guid.Parse(propertyValue);
                }
            }
            else
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("the commit Json property was not found in the DataVersion: {Id}", dtoInstance.Id);
                }
            }

            if (jsonElement.TryGetProperty("identity"u8, out JsonElement identityObject) && identityObject.ValueKind != JsonValueKind.Null)
            {
                dtoInstance.Identity = DataIdentityDeSerializer.DeSerialize(identityObject, serializationModeKind, deserializeDerivedProperties, loggerFactory);
            }
            else
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("the identity property was not found in the DataVersion: {Id}", dtoInstance.Id);
                }
            }

            // a null payload is meaningful: it represents a deletion (OMG Systems Modeling API and
            // Services v1.0, Clause 7.1.2) — the Payload property stays null
            if (jsonElement.TryGetProperty("payload"u8, out JsonElement payloadObject) && payloadObject.ValueKind != JsonValueKind.Null)
            {
                if (payloadObject.TryGetProperty("@type"u8, out var typeElement))
                {
                    var typeName = typeElement.GetString();

                    var func = DeSerializationProvider.Provide(typeName);

                    // the core deserializers read straight from the utf-8 payload, so the nested element is
                    // re-encoded to hand them a reader positioned on its opening brace
                    var payloadReader = new Utf8JsonReader(Encoding.UTF8.GetBytes(payloadObject.GetRawText()));
                    payloadReader.Read();

                    dtoInstance.Payload = func(ref payloadReader, serializationModeKind, deserializeDerivedProperties, loggerFactory);
                }
            }
            else
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("the payload Json property was not found in the DataVersion: {Id}", dtoInstance.Id);
                }
            }

            if (jsonElement.TryGetProperty("resourceIdentifier"u8, out JsonElement resourceIdentifierProperty))
            {
                dtoInstance.ResourceIdentifier = resourceIdentifierProperty.GetString();
            }
            else
            {
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("the resourceIdentifier Json property was not found in the DataVersion: {Id}", dtoInstance.Id);
                }
            }

            logger.Log(LogLevel.Trace, "finish deserialization: DataVersion");

            return dtoInstance;
        }
    }
}
