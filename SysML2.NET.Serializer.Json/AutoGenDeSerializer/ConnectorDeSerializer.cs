// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorDeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ConnectorDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IConnector"/> interface
    /// </summary>
    internal static class ConnectorDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IConnector"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IConnector"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IConnector"/>
        /// </returns>
        internal static IConnector DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("ConnectorDeSerializer");

            if (!jsonElement.TryGetProperty("@type", out JsonElement @type))
            {
                throw new InvalidOperationException("The @type property is not available, the ConnectorDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (@type.GetString() != "Connector")
            {
                throw new InvalidOperationException($"The ConnectorDeSerializer can only be used to deserialize objects of type IConnector, a {@type.GetString()} was provided");
            }

            var dtoInstance = new Core.DTO.Connector();

            if (jsonElement.TryGetProperty("@id", out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the Connector cannot be deserialized");
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
                logger.LogDebug($"the aliasIds Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("direction", out JsonElement directionProperty))
            {
                dtoInstance.Direction = FeatureDirectionKindDeSerializer.DeserializeNullable(directionProperty.GetString());
            }
            else
            {
                logger.LogDebug($"the direction Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the elementId Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isAbstract", out JsonElement isAbstractProperty))
            {
                dtoInstance.IsAbstract = isAbstractProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isAbstract Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isComposite", out JsonElement isCompositeProperty))
            {
                dtoInstance.IsComposite = isCompositeProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isComposite Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isDerived", out JsonElement isDerivedProperty))
            {
                dtoInstance.IsDerived = isDerivedProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isDerived Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isDirected", out JsonElement isDirectedProperty))
            {
                dtoInstance.IsDirected = isDirectedProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isDirected Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isEnd", out JsonElement isEndProperty))
            {
                dtoInstance.IsEnd = isEndProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isEnd Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isOrdered", out JsonElement isOrderedProperty))
            {
                dtoInstance.IsOrdered = isOrderedProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isOrdered Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isPortion", out JsonElement isPortionProperty))
            {
                dtoInstance.IsPortion = isPortionProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isPortion Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isReadOnly", out JsonElement isReadOnlyProperty))
            {
                dtoInstance.IsReadOnly = isReadOnlyProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isReadOnly Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isSufficient", out JsonElement isSufficientProperty))
            {
                dtoInstance.IsSufficient = isSufficientProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isSufficient Json property was not found in the Connector: {dtoInstance.Id}");
            }

            if (jsonElement.TryGetProperty("isUnique", out JsonElement isUniqueProperty))
            {
                dtoInstance.IsUnique = isUniqueProperty.GetBoolean();
            }
            else
            {
                logger.LogDebug($"the isUnique Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the name Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the ownedRelatedElement Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the ownedRelationship Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the owningRelatedElement Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the owningRelationship Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the shortName Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the source Json property was not found in the Connector: {dtoInstance.Id}");
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
                logger.LogDebug($"the target Json property was not found in the Connector: {dtoInstance.Id}");
            }


            return dtoInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
