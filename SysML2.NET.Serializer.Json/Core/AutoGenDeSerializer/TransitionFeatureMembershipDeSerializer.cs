// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipDeSerializer.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="TransitionFeatureMembershipDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="ITransitionFeatureMembership"/> interface
    /// </summary>
    internal static class TransitionFeatureMembershipDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="ITransitionFeatureMembership"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="ITransitionFeatureMembership"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="ITransitionFeatureMembership"/>
        /// </returns>
        internal static ITransitionFeatureMembership DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("TransitionFeatureMembershipDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the TransitionFeatureMembershipDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "TransitionFeatureMembership")
            {
                throw new InvalidOperationException($"The TransitionFeatureMembershipDeSerializer can only be used to deserialize objects of type ITransitionFeatureMembership, a {@type.GetString()} was provided");
            }

            var dtoInstance = new SysML2.NET.Core.DTO.TransitionFeatureMembership();

            if (jsonElement.TryGetProperty("@id"u8, out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the TransitionFeatureMembership cannot be deserialized");
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
                logger.LogDebug($"the aliasIds Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out JsonElement declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the declaredName Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out JsonElement declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the declaredShortName Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the elementId Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("feature"u8, out JsonElement featureProperty))
            {
                if (featureProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.Feature = Guid.Empty;
                    logger.LogDebug($"the TransitionFeatureMembership.Feature property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (featureProperty.TryGetProperty("@id"u8, out JsonElement featureIdProperty))
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
                logger.LogDebug($"the feature Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the isImplied Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the isImpliedIncluded Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("kind"u8, out JsonElement kindProperty))
            {
                dtoInstance.Kind = TransitionFeatureKindDeSerializer.Deserialize(kindProperty.GetString());
            }
            else
            {
                logger.LogDebug($"the kind Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("memberElement"u8, out JsonElement memberElementProperty))
            {
                if (memberElementProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.MemberElement = Guid.Empty;
                    logger.LogDebug($"the TransitionFeatureMembership.MemberElement property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (memberElementProperty.TryGetProperty("@id"u8, out JsonElement memberElementIdProperty))
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
                logger.LogDebug($"the memberElement Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("memberName"u8, out JsonElement memberNameProperty))
            {
                dtoInstance.MemberName = memberNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the memberName Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("memberShortName"u8, out JsonElement memberShortNameProperty))
            {
                dtoInstance.MemberShortName = memberShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug($"the memberShortName Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the ownedRelatedElement Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the ownedRelationship Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the owningRelatedElement Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the owningRelationship Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the source Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
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
                logger.LogDebug($"the target Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("type"u8, out JsonElement typeProperty))
            {
                if (typeProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.Type = Guid.Empty;
                    logger.LogDebug($"the TransitionFeatureMembership.Type property was not found in the Json. The value is set to Guid.Empty");
                }
                else
                {
                    if (typeProperty.TryGetProperty("@id"u8, out JsonElement typeIdProperty))
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
                logger.LogDebug($"the type Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("visibility"u8, out JsonElement visibilityProperty))
            {
                dtoInstance.Visibility = VisibilityKindDeSerializer.Deserialize(visibilityProperty.GetString());
            }
            else
            {
                logger.LogDebug($"the visibility Json property was not found in the TransitionFeatureMembership: {dtoInstance.Id}");
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
