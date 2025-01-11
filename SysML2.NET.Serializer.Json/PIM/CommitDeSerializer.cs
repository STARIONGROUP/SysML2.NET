// -------------------------------------------------------------------------------------------------
// <copyright file="CommitDeSerializer.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="CommitDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="Commit"/> class
    /// </summary>
    internal static class CommitDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="Commit"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="Commit"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="Project"/>
        /// </returns>
        internal static Commit DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("CommitDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the CommitDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "Commit")
            {
                throw new InvalidOperationException($"The CommitDeSerializer can only be used to deserialize objects of type Commit, a {@type.GetString()} was provided");
            }

            logger.Log(LogLevel.Trace, "start deserialization: Commit");

            var dtoInstance = new SysML2.NET.PIM.DTO.Commit();

            if (jsonElement.TryGetProperty("@id"u8, out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the Commit cannot be deserialized");
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
                logger.LogDebug($"the alias Json property was not found in the Commit: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("created"u8, out JsonElement createdProperty))
            {
                dtoInstance.Created = createdProperty.GetDateTime();
            }
            else
            {
                logger.LogDebug($"the created Json property was not found in the Commit: {dtoInstance.Id}");
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
                logger.LogDebug($"the description Json property was not found in the Commit: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("owningProject"u8, out JsonElement owningProjectProperty))
            {
                if (owningProjectProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningProject = Guid.Empty;
                }
                else
                {
                    if (owningProjectProperty.TryGetProperty("@id"u8, out JsonElement owningProjectPropertyIdProperty))
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
                logger.LogDebug($"the owningProject Json property was not found in the Commit: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("previousCommit"u8, out JsonElement previousCommitProperty))
            {
                if (previousCommitProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.PreviousCommit = Guid.Empty;
                }
                else
                {
                    if (previousCommitProperty.TryGetProperty("@id"u8, out JsonElement previousCommitPropertyIdProperty))
                    {
                        var propertyValue = previousCommitPropertyIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.PreviousCommit = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the owningProject Json property was not found in the Commit: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("resourceIdentifier"u8, out JsonElement resourceIdentifierProperty))
            {
                dtoInstance.ResourceIdentifier = resourceIdentifierProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the resourceIdentifier Json property was not found in the Commit: {dtoInstance.Id}");
            }

            logger.Log(LogLevel.Trace, "finish deserialization: Commit");

            return dtoInstance;
        }
    }
}
