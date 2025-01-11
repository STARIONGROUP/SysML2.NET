// -------------------------------------------------------------------------------------------------
// <copyright file="DataIdentityDeSerializer.cs" company="Starion Group S.A.">
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
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="DataIdentity"/>
        /// </returns>
        internal static DataIdentity DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
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
                logger.LogDebug($"the alias Json property was not found in the DataIdentity: {dtoInstance.Id}");
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
                logger.LogDebug($"the name Json property was not found in the DataIdentity: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("resourceIdentifier"u8, out JsonElement resourceIdentifierProperty))
            {
                dtoInstance.ResourceIdentifier = resourceIdentifierProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the resourceIdentifier Json property was not found in the DataIdentity: {dtoInstance.Id}");
            }

            logger.Log(LogLevel.Trace, "finish deserialization: DataIdentity");

            return dtoInstance;
        }
    }
}
