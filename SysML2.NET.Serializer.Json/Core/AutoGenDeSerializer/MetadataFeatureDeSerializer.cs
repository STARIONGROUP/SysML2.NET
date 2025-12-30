// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataFeatureDeSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Kernel.Metadata;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="MetadataFeatureDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IMetadataFeature"/> interface
    /// </summary>
    internal static class MetadataFeatureDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IMetadataFeature"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IMetadataFeature"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IMetadataFeature"/>
        /// </returns>
        internal static IMetadataFeature DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("MetadataFeatureDeSerializer");

            if (!jsonElement.TryGetProperty("@type"u8, out var @type))
            {
                throw new InvalidOperationException("The @type property is not available, the MetadataFeatureDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "MetadataFeature")
            {
                throw new InvalidOperationException($"The MetadataFeatureDeSerializer can only be used to deserialize objects of type IMetadataFeature, a {@type.GetString()} was provided");
            }

            IMetadataFeature dtoInstance = new SysML2.NET.Core.DTO.Kernel.Metadata.MetadataFeature();

            if (jsonElement.TryGetProperty("@id"u8, out var idProperty))
            {
                var propertyValue = idProperty.GetString();

                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the MetadataFeature cannot be deserialized");
                }
                else
                {
                    dtoInstance.Id = Guid.Parse(propertyValue);
                }
            }

            if (jsonElement.TryGetProperty("aliasIds"u8, out var aliasIdsProperty))
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
                logger.LogDebug("the aliasIds Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredName"u8, out var declaredNameProperty))
            {
                dtoInstance.DeclaredName = declaredNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredName Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("declaredShortName"u8, out var declaredShortNameProperty))
            {
                dtoInstance.DeclaredShortName = declaredShortNameProperty.GetString();
            }
            else
            {
                logger.LogDebug("the declaredShortName Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("direction"u8, out var directionProperty))
            {
                dtoInstance.Direction = FeatureDirectionKindDeSerializer.DeserializeNullable(directionProperty.GetString());
            }
            else
            {
                logger.LogDebug("the direction Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("elementId"u8, out var elementIdProperty))
            {
                var propertyValue = elementIdProperty.GetString();

                if (propertyValue != null)
                {
                    dtoInstance.ElementId = propertyValue;
                }
            }
            else
            {
                logger.LogDebug("the elementId Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isAbstract"u8, out var isAbstractProperty))
            {
                if (isAbstractProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsAbstract = isAbstractProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isAbstract Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isComposite"u8, out var isCompositeProperty))
            {
                if (isCompositeProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsComposite = isCompositeProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isComposite Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isConstant"u8, out var isConstantProperty))
            {
                if (isConstantProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsConstant = isConstantProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isConstant Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isDerived"u8, out var isDerivedProperty))
            {
                if (isDerivedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsDerived = isDerivedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isDerived Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isEnd"u8, out var isEndProperty))
            {
                if (isEndProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsEnd = isEndProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isEnd Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isImpliedIncluded"u8, out var isImpliedIncludedProperty))
            {
                if (isImpliedIncludedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsImpliedIncluded = isImpliedIncludedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isImpliedIncluded Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isOrdered"u8, out var isOrderedProperty))
            {
                if (isOrderedProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsOrdered = isOrderedProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isOrdered Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isPortion"u8, out var isPortionProperty))
            {
                if (isPortionProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsPortion = isPortionProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isPortion Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isSufficient"u8, out var isSufficientProperty))
            {
                if (isSufficientProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsSufficient = isSufficientProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isSufficient Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isUnique"u8, out var isUniqueProperty))
            {
                if (isUniqueProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsUnique = isUniqueProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isUnique Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("isVariable"u8, out var isVariableProperty))
            {
                if (isVariableProperty.ValueKind != JsonValueKind.Null)
                {
                    dtoInstance.IsVariable = isVariableProperty.GetBoolean();
                }
            }
            else
            {
                logger.LogDebug("the isVariable Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("ownedRelationship"u8, out var ownedRelationshipProperty))
            {
                foreach (var arrayItem in ownedRelationshipProperty.EnumerateArray())
                {
                    if (arrayItem.TryGetProperty("@id"u8, out var ownedRelationshipIdProperty))
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
                logger.LogDebug("the ownedRelationship Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }

            if (jsonElement.TryGetProperty("owningRelationship"u8, out var owningRelationshipProperty))
            {
                if (owningRelationshipProperty.ValueKind == JsonValueKind.Null)
                {
                    dtoInstance.OwningRelationship = null;
                }
                else
                {
                    if (owningRelationshipProperty.TryGetProperty("@id"u8, out var owningRelationshipIdProperty))
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
                logger.LogDebug("the owningRelationship Json property was not found in the MetadataFeature: { Id }", dtoInstance.Id);
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
