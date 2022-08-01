// -------------------------------------------------------------------------------------------------
// <copyright file="StateDefinitionDeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="StateDefinitionDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="IStateDefinition"/> interface
    /// </summary>
    internal static class StateDefinitionDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="IStateDefinition"/> from the provided <see cref="JsonElement"/>
        /// </summary>
        /// <param name="jsonElement">
        /// The <see cref="JsonElement"/> that contains the <see cref="IStateDefinition"/> json object
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="IStateDefinition"/>
        /// </returns>
        internal static IStateDefinition DeSerialize(JsonElement jsonElement, SerializationModeKind serializationModeKind, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("StateDefinitionDeSerializer");

            if (!jsonElement.TryGetProperty("@type", out JsonElement typeProperty))
            {
                throw new InvalidOperationException("The @type property is not available, the StateDefinitionDeSerializer cannot be used to deserialize this JsonElement");
            }

            if (typeProperty.GetString() != "StateDefinition")
            {
                throw new InvalidOperationException($"The StateDefinitionDeSerializer can only be used to deserialize objects of type StateDefinition, a {typeProperty.GetString()} was provided");
            }

            var stateDefinitionInstance = new DTO.StateDefinition();

            if (jsonElement.TryGetProperty("@id", out JsonElement idProperty))
            {
                var propertyValue = idProperty.GetString();
                if (propertyValue == null)
                {
                    throw new JsonException("The @id property is not present, the StateDefinition cannot be deserialized");
                }
                else
                {
                    stateDefinitionInstance.Id = Guid.Parse(propertyValue);
                }
            }













            return stateDefinitionInstance;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
