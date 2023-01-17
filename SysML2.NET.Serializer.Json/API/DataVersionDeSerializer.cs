// -------------------------------------------------------------------------------------------------
// <copyright file="DataVersionDeSerializer.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.Serializer.Json.API
{
    using System;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.API.DTO;

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
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="DataVersion"/>
        /// </returns>
        internal static DataVersion DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("DataVersionDeSerializer");

            logger.Log(LogLevel.Trace, "start deserialization: DataVersion");

            var dtoInstance = new SysML2.NET.API.DTO.DataVersion();

            if (jsonElement.TryGetProperty("@id", out JsonElement idPropertyVersionItem))
            {
                var idPropertyVersionItemValue = idPropertyVersionItem.GetString();
                if (idPropertyVersionItemValue == null)
                {
                    throw new JsonException("The @id property is not present, the DataVersion cannot be deserialized");
                }

                dtoInstance.Id = Guid.Parse(idPropertyVersionItemValue);
            }
            
            if (jsonElement.TryGetProperty("identity", out JsonElement identityObject))
            {
                dtoInstance.Identity = DataIdentityDeSerializer.DeSerialize(identityObject, serializationModeKind, loggerFactory);
            }
            else
            {
                logger.LogDebug($"the identity property was not found in the DataVersion: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("payload", out JsonElement payloadObject))
            {
                if (payloadObject.TryGetProperty("@type", out var typeElement))
                {
                    var typeName = typeElement.GetString();

                    var func = DeSerializationProvider.Provide(typeName);
                    dtoInstance.Payload = func(payloadObject, serializationModeKind, loggerFactory);
                }
            }
            else
            {
                logger.LogDebug($"the payload Json property was not found in the DataVersion: {dtoInstance.Id}");
            }

            logger.Log(LogLevel.Trace, "finish deserialization: DataVersion");

            return dtoInstance;
        }
    }
}
