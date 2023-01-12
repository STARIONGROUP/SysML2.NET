// -------------------------------------------------------------------------------------------------
// <copyright file="ReturnParameterMembershipDeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ReturnParameterMembershipDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IReturnParameterMembership"/> interface
    /// </summary>
    internal static class ReturnParameterMembershipDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IReturnParameterMembership"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IReturnParameterMembership"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IReturnParameterMembership"/>
        /// </returns>
        internal static IReturnParameterMembership DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("ReturnParameterMembershipDeSerializer");

            if (!jsonElement.TryGetProperty("@type", out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the ReturnParameterMembershipDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "ReturnParameterMembership")
            {
                throw new InvalidOperationException($"The ReturnParameterMembershipDeSerializer can only be used to deserialize objects of type IReturnParameterMembership, a {@type.GetString()} was provided");
            }

            var dtoInstance = new Core.DTO.ReturnParameterMembership();

            if (jsonElement.TryGetProperty("@id", out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the ReturnParameterMembership cannot be deserialized");
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
                logger.LogDebug($"the aliasIds Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the elementId Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("feature", out JsonElement featureProperty))
            {
                if (featureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.Feature = Guid.Empty;
                    logger.LogDebug($"the ReturnParameterMembership.Feature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (featureProperty.TryGetProperty("@id", out JsonElement featureIdProperty))
                    {
                        var propertyValue = featureIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.Feature = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the feature Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the isImplied Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the isImpliedIncluded Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("memberElement", out JsonElement memberElementProperty))
            {
                if (memberElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.MemberElement = Guid.Empty;
                    logger.LogDebug($"the ReturnParameterMembership.MemberElement property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (memberElementProperty.TryGetProperty("@id", out JsonElement memberElementIdProperty))
                    {
                        var propertyValue = memberElementIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.MemberElement = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the memberElement Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("memberName", out JsonElement memberNameProperty))
            {
                dtoInstance.MemberName = memberNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the memberName Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("memberShortName", out JsonElement memberShortNameProperty))
            {
                dtoInstance.MemberShortName = memberShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the memberShortName Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("name", out JsonElement nameProperty))
            {
                dtoInstance.Name = nameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the name Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the ownedRelatedElement Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the ownedRelationship Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the owningRelatedElement Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the owningRelationship Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("shortName", out JsonElement shortNameProperty))
            {
                dtoInstance.ShortName = shortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the shortName Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the source Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the target Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("type", out JsonElement typeProperty))
            {
                if (typeProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.Type = Guid.Empty;
                    logger.LogDebug($"the ReturnParameterMembership.Type property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (typeProperty.TryGetProperty("@id", out JsonElement typeIdProperty))
                    {
                        var propertyValue = typeIdProperty.GetString();
                        if (propertyValue != null)
                        {
                            dtoInstance.Type = Guid.Parse(propertyValue);
                        }
                    }
                }
            }
            else
            {
                logger.LogDebug($"the type Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("visibility", out JsonElement visibilityProperty))
            {
                dtoInstance.Visibility = VisibilityKindDeSerializer.Deserialize(visibilityProperty.GetString());
            }
            else
            {
                logger.LogDebug($"the visibility Json property was not found in the ReturnParameterMembership: {dtoInstance.Id}");
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
