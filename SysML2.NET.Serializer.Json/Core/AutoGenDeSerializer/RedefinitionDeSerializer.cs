// -------------------------------------------------------------------------------------------------
// <copyright file="RedefinitionDeSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="RedefinitionDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IRedefinition"/> interface
    /// </summary>
    internal static class RedefinitionDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IRedefinition"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IRedefinition"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IRedefinition"/>
        /// </returns>
        internal static IRedefinition DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("RedefinitionDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the RedefinitionDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "Redefinition")
            {
                throw new InvalidOperationException($"The RedefinitionDeSerializer can only be used to deserialize objects of type IRedefinition, a {@type.GetString()} was provided");
            }

            var dtoInstance = new SysML2.NET.Core.DTO.Redefinition();

            if (jsonElement.TryGetProperty("@id"u8, out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the Redefinition cannot be deserialized");
                }
                else
                {
                    dtoInstance.Id = Guid.Parse(propertyValue);
                }
            }

            if (jsonElement.TryGetProperty("aliasIds"u8, out JsonElement aliasIdsProperty))
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
                logger.LogDebug($"the aliasIds Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out JsonElement declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the declaredName Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out JsonElement declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the declaredShortName Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("elementId"u8, out JsonElement elementIdProperty))
            {
                var propertyValue = elementIdProperty.GetString();
                if (propertyValue != null)
                {
                    dtoInstance.ElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug($"the elementId Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("general"u8, out JsonElement generalProperty))
            {
                if (generalProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.General = Guid.Empty;
                    logger.LogDebug($"the Redefinition.General property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (generalProperty.TryGetProperty("@id"u8, out JsonElement generalIdProperty))
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
                logger.LogDebug($"the general Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isImplied"u8, out JsonElement isImpliedProperty))
            {
                if (isImpliedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImplied = isImpliedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug($"the isImplied Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isImpliedIncluded"u8, out JsonElement isImpliedIncludedProperty))
            {
                if (isImpliedIncludedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImpliedIncluded = isImpliedIncludedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug($"the isImpliedIncluded Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("ownedRelatedElement"u8, out JsonElement ownedRelatedElementProperty))
            {
                foreach (var arrayItem in ownedRelatedElementProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out JsonElement ownedRelatedElementIdProperty))
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
                logger.LogDebug($"the ownedRelatedElement Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("ownedRelationship"u8, out JsonElement ownedRelationshipProperty))
            {
                foreach (var arrayItem in ownedRelationshipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out JsonElement ownedRelationshipIdProperty))
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
                logger.LogDebug($"the ownedRelationship Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("owningRelatedElement"u8, out JsonElement owningRelatedElementProperty))
            {
                if (owningRelatedElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelatedElement = null;
                }
                else
                {
                    if (owningRelatedElementProperty.TryGetProperty("@id"u8, out JsonElement owningRelatedElementIdProperty))
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
                logger.LogDebug($"the owningRelatedElement Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("owningRelationship"u8, out JsonElement owningRelationshipProperty))
            {
                if (owningRelationshipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelationship = null;
                }
                else
                {
                    if (owningRelationshipProperty.TryGetProperty("@id"u8, out JsonElement owningRelationshipIdProperty))
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
                logger.LogDebug($"the owningRelationship Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("redefinedFeature"u8, out JsonElement redefinedFeatureProperty))
            {
                if (redefinedFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.RedefinedFeature = Guid.Empty;
                    logger.LogDebug($"the Redefinition.RedefinedFeature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (redefinedFeatureProperty.TryGetProperty("@id"u8, out JsonElement redefinedFeatureIdProperty))
                    {
                        var propertyValue = redefinedFeatureIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.RedefinedFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the redefinedFeature Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("redefiningFeature"u8, out JsonElement redefiningFeatureProperty))
            {
                if (redefiningFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.RedefiningFeature = Guid.Empty;
                    logger.LogDebug($"the Redefinition.RedefiningFeature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (redefiningFeatureProperty.TryGetProperty("@id"u8, out JsonElement redefiningFeatureIdProperty))
                    {
                        var propertyValue = redefiningFeatureIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.RedefiningFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the redefiningFeature Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("source"u8, out JsonElement sourceProperty))
            {
                foreach (var arrayItem in sourceProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out JsonElement sourceIdProperty))
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
                logger.LogDebug($"the source Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("specific"u8, out JsonElement specificProperty))
            {
                if (specificProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.Specific = Guid.Empty;
                    logger.LogDebug($"the Redefinition.Specific property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (specificProperty.TryGetProperty("@id"u8, out JsonElement specificIdProperty))
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
                logger.LogDebug($"the specific Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("subsettedFeature"u8, out JsonElement subsettedFeatureProperty))
            {
                if (subsettedFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.SubsettedFeature = Guid.Empty;
                    logger.LogDebug($"the Redefinition.SubsettedFeature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (subsettedFeatureProperty.TryGetProperty("@id"u8, out JsonElement subsettedFeatureIdProperty))
                    {
                        var propertyValue = subsettedFeatureIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.SubsettedFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the subsettedFeature Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("subsettingFeature"u8, out JsonElement subsettingFeatureProperty))
            {
                if (subsettingFeatureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.SubsettingFeature = Guid.Empty;
                    logger.LogDebug($"the Redefinition.SubsettingFeature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (subsettingFeatureProperty.TryGetProperty("@id"u8, out JsonElement subsettingFeatureIdProperty))
                    {
                        var propertyValue = subsettingFeatureIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.SubsettingFeature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the subsettingFeature Json property was not found in the Redefinition: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("target"u8, out JsonElement targetProperty))
            {
                foreach (var arrayItem in targetProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out JsonElement targetIdProperty))
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
                logger.LogDebug($"the target Json property was not found in the Redefinition: {dtoInstance.Id}");
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
