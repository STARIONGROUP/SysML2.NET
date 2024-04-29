// -------------------------------------------------------------------------------------------------
// <copyright file="BranchDeserializer.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2024 Starion Group S.A.
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
    /// The purpose of the <see cref="BranchDeserializer"/> is to provide deserialization capabilities
    /// for the <see cref="Branch"/> class
    /// </summary>
    internal static class BranchDeserializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="Branch"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="Branch"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="Tag"/>
        /// </returns>
        internal static Branch DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("BranchDeserializer");

            if (!jsonElement.TryGetProperty("@type"u8, out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the BranchDeserializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "Branch")
            {
                throw new InvalidOperationException($"The BranchDeserializer can only be used to deserialize objects of type Branch, a {@type.GetString()} was provided");
            }

            logger.Log(LogLevel.Trace, "start deserialization: Branch");

            var dtoInstance = new SysML2.NET.PIM.DTO.Branch();

            if (jsonElement.TryGetProperty("@id"u8, out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the Branch cannot be deserialized");
                }

                dtoInstance.Id = Guid.Parse(propertyValue);
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
                logger.LogDebug($"the alias Json property was not found in the Branch: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("created"u8, out JsonElement createdProperty))
            {
                dtoInstance.Created = createdProperty.GetDateTime();
            }
            else
            {
                logger.LogDebug($"the created Json property was not found in the Branch: {dtoInstance.Id}");
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
                logger.LogDebug($"the name Json property was not found in the Branch: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("head"u8, out JsonElement headProperty))
            {
                if (headProperty.ValueKind == JsonValueKind.Null)
                {
                    logger.LogWarning($"the head Json property was null which should not be allowed: Branch {dtoInstance.Id}");
                }
                else
                {
                    if (headProperty.TryGetProperty("@id"u8, out JsonElement headIdProperty))
                    {
                        var propertyValue = headIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.Head = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the head Json property was not found in the Branch: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("name"u8, out JsonElement nameProperty))
            {
                var propertyValue = nameProperty.GetString();
                if (propertyValue != null)
                {
                    dtoInstance.Name = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the name Json property was not found in the Branch: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("owningProject"u8, out JsonElement owningProjectProperty))
            {
                if (owningProjectProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningProject = Guid.Empty;
                }
                else
                {
                    if (owningProjectProperty.TryGetProperty("@id", out JsonElement owningProjectPropertyIdProperty))
                    {
                        var propertyValue = owningProjectPropertyIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.OwningProject = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the owningProject Json property was not found in the Branch: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("resourceIdentifier"u8, out JsonElement resourceIdentifierProperty))
            {
                dtoInstance.ResourceIdentifier = resourceIdentifierProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the resourceIdentifier Json property was not found in the Branch: {dtoInstance.Id}");
            }

            logger.Log(LogLevel.Trace, "finish deserialization: Branch");

            return dtoInstance;
        }
    }
}
