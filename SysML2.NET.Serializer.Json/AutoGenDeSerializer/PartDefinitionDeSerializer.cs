// -------------------------------------------------------------------------------------------------
// <copyright file="PartDefinitionDeSerializer.cs" company="RHEA System S.A.">
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

    using SysML2.NET.DTO;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// The purpose of the <see cref="PartDefinitionDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IPartDefinition"/> interface
    /// </summary>
    internal static class PartDefinitionDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IPartDefinition"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IPartDefinition"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IPartDefinition"/>
        /// </returns>
        internal static IPartDefinition DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("IPartDefinition.DeSerialize");

            if (!jsonElement.TryGetProperty("@type", out JsonElement typeProperty))
            {
                throw new InvalidOperationException("The @type property is not available, the PartDefinitionDeSerializer cannot be used to deserialize this JsonElement");
            }
            
            if (typeProperty.GetString() != "PartDefinition")
            {
                throw new InvalidOperationException($"The PartDefinitionDeSerializer can only be used to deserialize objects of type PartDefinition, a {typeProperty.GetString()} was provided");
            }

            var partDefinition = new PartDefinition();

            if (jsonElement.TryGetProperty("@id", out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the PartDefinition cannot be deserialized");
                }
                else
                {
                    partDefinition.Id = Guid.Parse(propertyValue);
                }
            }

            if (jsonElement.TryGetProperty("aliasIds", out JsonElement aliasIdsProperty))
            {
                foreach (var item in aliasIdsProperty.EnumerateArray())
                {
                    partDefinition.AliasIds.Add(item.GetString());
                } 
            }
            else
            {
                logger.LogDebug($"the aliasIds Json property was not found in the PartDefinition: {partDefinition.Id}");
            }
            
            if (jsonElement.TryGetProperty("elementId", out JsonElement elementIdProperty))
            {
                var propertyValue = elementIdProperty.GetString();
                if (propertyValue != null)
                {
                    partDefinition.ElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the elementId Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            if (jsonElement.TryGetProperty("isAbstract", out JsonElement isAbstractProperty))
            {
                partDefinition.IsAbstract = isAbstractProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isAbstract Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            if (jsonElement.TryGetProperty("isIndividual", out JsonElement isIndividualProperty))
            {
                partDefinition.IsIndividual = isIndividualProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isIndividual Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            if (jsonElement.TryGetProperty("isSufficient", out JsonElement isSufficientProperty))
            {
                partDefinition.IsSufficient = isSufficientProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isSufficient Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            if (jsonElement.TryGetProperty("isVariation", out JsonElement isVariationProperty))
            {
                partDefinition.IsVariation = isVariationProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isVariation Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            if (jsonElement.TryGetProperty("name", out JsonElement nameProperty))
            {
                var propertyValue = nameProperty.GetString();
                if (propertyValue != null)
                {
                    partDefinition.Name = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the name Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            if (jsonElement.TryGetProperty("ownedRelationship", out JsonElement ownedRelationshipProperty))
            {
                foreach (var item in ownedRelationshipProperty.EnumerateArray())
                {
                    if (item.TryGetProperty("@id", out JsonElement ownedRelationshipIdProperty))
                    {
                        var propertyValue = ownedRelationshipIdProperty.GetString();
                        if (propertyValue != null)
                        {

                            partDefinition.OwnedRelationship.Add(Guid.Parse(propertyValue));
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the ownedRelationship Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            if (jsonElement.TryGetProperty("owningRelationship", out JsonElement owningRelationshipProperty))
            {
                if (owningRelationshipProperty.TryGetProperty("@id",  out JsonElement owningRelationshipIdProperty))
                {
                    var propertyValue = owningRelationshipIdProperty.GetString();
                    if (propertyValue != null)
                    {
                        partDefinition.OwningRelationship = Guid.Parse(propertyValue);
                    }
                }
            }
            else
            {
                logger.LogDebug($"the owningRelationship Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            if (jsonElement.TryGetProperty("shortName", out JsonElement shortNameProperty))
            {
                var propertyValue = shortNameProperty.GetString();
                if (propertyValue != null)
                {
                    partDefinition.ShortName = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the shortName Json property was not found in the PartDefinition: {partDefinition.Id}");
            }

            return partDefinition;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
