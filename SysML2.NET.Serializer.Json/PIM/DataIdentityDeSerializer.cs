// ------------------------------------------------------------------------------------------------
// <copyright file="DataIdentityDeSerializer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.PIM.DTO
{
    using System;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.PIM.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="DataIdentityDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="DataIdentity"/> class
    /// </summary>
    internal static class DataIdentityDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="DataIdentity"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="DataIdentity"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="DataIdentity"/>
        /// </returns>
        internal static DataIdentity DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("DataIdentityDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the DataIdentityDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "DataIdentity")
            {
                throw new InvalidOperationException($"The DataIdentityDeSerializer only be used to deserialize objects of type DataIdentity, a {@type.GetString()} was provided");
            }

            logger.Log(LogLevel.Trace, "start deserialization: DataIdentity");

            var dtoInstance = new SysML2.NET.PIM.DTO.DataIdentity();
            
            if (jsonElement.TryGetProperty("@id"u8, out JsonElement idPropertyVersionItem))
            {
                var idPropertyVersionItemValue = idPropertyVersionItem.GetString();
                if (idPropertyVersionItemValue == null)
                {
                    throw new JsonException("The @id property is not present, the DataIdentity cannot be deserialized");
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
                    logger.LogDebug("the alias Json property was not found in the DataIdentity: {Id}", dtoInstance.Id);
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
                    logger.LogDebug("the name Json property was not found in the DataIdentity: {Id}", dtoInstance.Id);
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
                    logger.LogDebug("the resourceIdentifier Json property was not found in the DataIdentity: {Id}",dtoInstance.Id);
                }
            }

            logger.Log(LogLevel.Trace, "finish deserialization: DataIdentity");

            return dtoInstance;
        }
    }
}
