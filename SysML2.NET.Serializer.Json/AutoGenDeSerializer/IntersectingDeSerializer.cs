﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IntersectingDeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="IntersectingDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IIntersecting"/> interface
    /// </summary>
    internal static class IntersectingDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IIntersecting"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IIntersecting"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IIntersecting"/>
        /// </returns>
        internal static IIntersecting DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("IntersectingDeSerializer");

            if (!jsonElement.TryGetProperty("@type", out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the IntersectingDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "Intersecting")
            {
                throw new InvalidOperationException($"The IntersectingDeSerializer can only be used to deserialize objects of type IIntersecting, a {@type.GetString()} was provided");
            }

            var dtoInstance = new Core.DTO.Intersecting();

            if (jsonElement.TryGetProperty("@id", out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the Intersecting cannot be deserialized");
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
                logger.LogDebug($"the aliasIds Json property was not found in the Intersecting: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("declaredName", out JsonElement declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the declaredName Json property was not found in the Intersecting: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("declaredShortName", out JsonElement declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the declaredShortName Json property was not found in the Intersecting: {dtoInstance.Id}");
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
                logger.LogDebug($"the elementId Json property was not found in the Intersecting: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("intersectingType", out JsonElement intersectingTypeProperty))
            {
                if (intersectingTypeProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.IntersectingType = Guid.Empty;
                    logger.LogDebug($"the Intersecting.IntersectingType property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (intersectingTypeProperty.TryGetProperty("@id", out JsonElement intersectingTypeIdProperty))
                    {
                        var propertyValue = intersectingTypeIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.IntersectingType = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the intersectingType Json property was not found in the Intersecting: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isImplied", out JsonElement isImpliedProperty))
            {
                if (isImpliedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImplied = isImpliedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug($"the isImplied Json property was not found in the Intersecting: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isImpliedIncluded", out JsonElement isImpliedIncludedProperty))
            {
                if (isImpliedIncludedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImpliedIncluded = isImpliedIncludedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug($"the isImpliedIncluded Json property was not found in the Intersecting: {dtoInstance.Id}");
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
                logger.LogDebug($"the ownedRelatedElement Json property was not found in the Intersecting: {dtoInstance.Id}");
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
                logger.LogDebug($"the ownedRelationship Json property was not found in the Intersecting: {dtoInstance.Id}");
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
                logger.LogDebug($"the owningRelatedElement Json property was not found in the Intersecting: {dtoInstance.Id}");
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
                logger.LogDebug($"the owningRelationship Json property was not found in the Intersecting: {dtoInstance.Id}");
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
                logger.LogDebug($"the source Json property was not found in the Intersecting: {dtoInstance.Id}");
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
                logger.LogDebug($"the target Json property was not found in the Intersecting: {dtoInstance.Id}");
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
