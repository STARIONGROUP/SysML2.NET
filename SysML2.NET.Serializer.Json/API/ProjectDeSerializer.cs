// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectDeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ProjectDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="Project"/> class
    /// </summary>
    internal static class ProjectDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="Project"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="Project"/> json object
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
        internal static Project DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("ProjectDeSerializer");

            if (!jsonElement.TryGetProperty("@type", out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the ProjectDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "Project")
            {
                throw new InvalidOperationException($"The ProjectDeSerializer can only be used to deserialize objects of type Project, a {@type.GetString()} was provided");
            }

            logger.Log(LogLevel.Trace, "start deserialization: Project");

            var dtoInstance = new SysML2.NET.API.DTO.Project();

            if (jsonElement.TryGetProperty("@id", out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the Project cannot be deserialized");
                }
                else
                {
                    dtoInstance.Id = Guid.Parse(propertyValue);
                }
            }

            if (jsonElement.TryGetProperty("alias", out JsonElement aliasIdsProperty))
            {
                foreach (var arrayItem in aliasIdsProperty.EnumerateArray())
                {
                    var propertyValue = arrayItem.GetString();
                    if (propertyValue != null)
                    {
                        dtoInstance.Alias.Add(propertyValue);
                    }
                }
            }
            else
            {
                logger.LogDebug($"the alias Json property was not found in the Project: {dtoInstance.Id}");
            }
            
            if (jsonElement.TryGetProperty("defaultBranch", out JsonElement defaultBranchProperty))
            {
                if (defaultBranchProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.DefaultBranch = Guid.Empty;
                }
                else
                {
                    if (defaultBranchProperty.TryGetProperty("@id", out JsonElement defaultBranchPropertyIdProperty))
                    {
                        var propertyValue = defaultBranchPropertyIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.DefaultBranch = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the defaultBranch Json property was not found in the Project: {dtoInstance.Id}");
            }
            
            if (jsonElement.TryGetProperty("description", out JsonElement descriptionProperty))
            {
                var propertyValue = descriptionProperty.GetString();
                if (propertyValue != null)
                {
                    dtoInstance.Description = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the name Json property was not found in the Project: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("name", out JsonElement nameProperty))
            {
                var propertyValue = nameProperty.GetString();
                if (propertyValue != null)
                {
                    dtoInstance.Name = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the name Json property was not found in the Project: {dtoInstance.Id}");
            }

            logger.Log(LogLevel.Trace, "finish deserialization: Project");

            return dtoInstance;
        }
    }
}
