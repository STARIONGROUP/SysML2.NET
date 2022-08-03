// -------------------------------------------------------------------------------------------------
// <copyright file="SubclassificationDeSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="SubclassificationDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="ISubclassification"/> interface
    /// </summary>
    internal static class SubclassificationDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="ISubclassification"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="ISubclassification"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="ISubclassification"/>
        /// </returns>
        internal static ISubclassification DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("SubclassificationDeSerializer");

            if (!jsonElement.TryGetProperty("@type", out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the SubclassificationDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "Subclassification")
            {
                throw new InvalidOperationException($"The SubclassificationDeSerializer can only be used to deserialize objects of type ISubclassification, a {@type.GetString()} was provided");
            }

            var dtoInstance = new Core.DTO.Subclassification();

            if (jsonElement.TryGetProperty("@id", out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the Subclassification cannot be deserialized");
                }
                else
                {
                    dtoInstance.Id = Guid.Parse(propertyValue);
                }
            }

            if (jsonElement.TryGetProperty("aliasIds", out JsonElement aliasIdsProperty))
            {
                foreach (var arrayItem in aliasIdsProperty.EnumerateArray())
                {
                    var propertyValue = arrayItem.GetString();
                    if (propertyValue != null)
                    {
                        dtoInstance.AliasIds.Add(propertyValue);
                    }
                }
            }
            else
            {
                logger.LogDebug($"the aliasIds Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("elementId", out JsonElement elementIdProperty))
            {
                var propertyValue = elementIdProperty.GetString();
                if (propertyValue != null)
                {
                    dtoInstance.ElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the elementId Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("general", out JsonElement generalProperty))
            {
                if (generalProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.General = Guid.Empty;
                    logger.LogDebug($"the Subclassification.General property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (generalProperty.TryGetProperty("@id", out JsonElement generalIdProperty))
                    {
                        var propertyValue = generalIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.General = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the general Json property was not found in the Subclassification: {dtoInstance.Id}");
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
                logger.LogDebug($"the name Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("ownedRelatedElement", out JsonElement ownedRelatedElementProperty))
            {
                foreach (var arrayItem in ownedRelatedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id", out JsonElement ownedRelatedElementIdProperty))
                    {
                        var propertyValue = ownedRelatedElementIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.OwnedRelatedElement.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the ownedRelatedElement Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("ownedRelationship", out JsonElement ownedRelationshipProperty))
            {
                foreach (var arrayItem in ownedRelationshipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id", out JsonElement ownedRelationshipIdProperty))
                    {
                        var propertyValue = ownedRelationshipIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.OwnedRelationship.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the ownedRelationship Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("owningRelatedElement", out JsonElement owningRelatedElementProperty))
            {
                if (owningRelatedElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelatedElement = null;
                }
                else
                {
                    if (owningRelatedElementProperty.TryGetProperty("@id", out JsonElement owningRelatedElementIdProperty))
                    {
                        var propertyValue = owningRelatedElementIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.OwningRelatedElement = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the owningRelatedElement Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("owningRelationship", out JsonElement owningRelationshipProperty))
            {
                if (owningRelationshipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelationship = null;
                }
                else
                {
                    if (owningRelationshipProperty.TryGetProperty("@id", out JsonElement owningRelationshipIdProperty))
                    {
                        var propertyValue = owningRelationshipIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.OwningRelationship = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the owningRelationship Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("shortName", out JsonElement shortNameProperty))
            {
                var propertyValue = shortNameProperty.GetString();
                if (propertyValue != null)
                {
                    dtoInstance.ShortName = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the shortName Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("source", out JsonElement sourceProperty))
            {
                foreach (var arrayItem in sourceProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id", out JsonElement sourceIdProperty))
                    {
                        var propertyValue = sourceIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.Source.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the source Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("specific", out JsonElement specificProperty))
            {
                if (specificProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.Specific = Guid.Empty;
                    logger.LogDebug($"the Subclassification.Specific property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (specificProperty.TryGetProperty("@id", out JsonElement specificIdProperty))
                    {
                        var propertyValue = specificIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.Specific = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the specific Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("subclassifier", out JsonElement subclassifierProperty))
            {
                if (subclassifierProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.Subclassifier = Guid.Empty;
                    logger.LogDebug($"the Subclassification.Subclassifier property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (subclassifierProperty.TryGetProperty("@id", out JsonElement subclassifierIdProperty))
                    {
                        var propertyValue = subclassifierIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.Subclassifier = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the subclassifier Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("superclassifier", out JsonElement superclassifierProperty))
            {
                if (superclassifierProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.Superclassifier = Guid.Empty;
                    logger.LogDebug($"the Subclassification.Superclassifier property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (superclassifierProperty.TryGetProperty("@id", out JsonElement superclassifierIdProperty))
                    {
                        var propertyValue = superclassifierIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.Superclassifier = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the superclassifier Json property was not found in the Subclassification: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("target", out JsonElement targetProperty))
            {
                foreach (var arrayItem in targetProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id", out JsonElement targetIdProperty))
                    {
                        var propertyValue = targetIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.Target.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the target Json property was not found in the Subclassification: {dtoInstance.Id}");
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
