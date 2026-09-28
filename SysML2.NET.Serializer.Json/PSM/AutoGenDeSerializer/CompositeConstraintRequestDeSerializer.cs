// -------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraintRequestDeSerializer.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Serializer.Json.PSM
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Extensions.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="CompositeConstraintRequestDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="CompositeConstraintRequest"/> class
    /// </summary>
    internal static class CompositeConstraintRequestDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="CompositeConstraintRequest"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="CompositeConstraintRequest"/> json object. On return the reader is positioned on the matching
        /// <see cref="JsonTokenType.EndObject"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="deserializeDerivedProperties">
        /// Asserts that the deserializer should deserialize derived properties if present or if they are ignored
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// an instance of <see cref="CompositeConstraintRequest"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the serialization mode is not supported
        /// </exception>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static CompositeConstraintRequest DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            if (serializationModeKind != SerializationModeKind.JSON)
            {
                throw new NotSupportedException($"The {serializationModeKind} serialization mode is not supported");
            }

            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("CompositeConstraintRequestDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var compositeConstraintRequest = new CompositeConstraintRequest();

            var constraintSeen = false;
            var operatorSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the CompositeConstraintRequest json object.");
                }

                if (reader.ValueTextEquals("constraint"u8))
                {
                    constraintSeen = true;
                    reader.Read();

                    compositeConstraintRequest.Constraint = new List<IConstraint>();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                        {
                            compositeConstraintRequest.Constraint.Add((IConstraint)PsmDeSerializationDispatcher.ReadRequest(ref reader, serializationModeKind, deserializeDerivedProperties, loggerFactory));
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("operator"u8))
                {
                    operatorSeen = true;
                    reader.Read();

                    compositeConstraintRequest.Operator = CompositeConstraintRequestOperatorProvider.Parse(Utf8JsonReaderHelper.ReadStringOrNull(ref reader).AsSpan());

                    continue;
                }

                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!constraintSeen)
                {
                    logger.LogDebug("the constraint Json property was not found in the CompositeConstraintRequest");
                }

                if (!operatorSeen)
                {
                    logger.LogDebug("the operator Json property was not found in the CompositeConstraintRequest");
                }

            }

            return compositeConstraintRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
