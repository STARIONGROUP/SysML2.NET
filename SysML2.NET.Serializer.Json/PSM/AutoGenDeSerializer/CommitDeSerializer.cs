// -------------------------------------------------------------------------------------------------
// <copyright file="CommitDeSerializer.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="CommitDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="Commit"/> class
    /// </summary>
    internal static class CommitDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="Commit"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="Commit"/> json object. On return the reader is positioned on the matching
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
        /// an instance of <see cref="Commit"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the serialization mode is not supported
        /// </exception>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static Commit DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            if (serializationModeKind != SerializationModeKind.JSON)
            {
                throw new NotSupportedException($"The {serializationModeKind} serialization mode is not supported");
            }

            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("CommitDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var commit = new Commit();

            var idSeen = false;
            var aliasSeen = false;
            var createdSeen = false;
            var descriptionSeen = false;
            var nameSeen = false;
            var owningProjectSeen = false;
            var previousCommitSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the Commit json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    idSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is null, the Commit cannot be deserialized");
                    }

                    commit.Id = Utf8JsonReaderHelper.ReadGuid(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("alias"u8))
                {
                    aliasSeen = true;
                    reader.Read();

                    commit.Alias = new List<string>();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                        {
                            commit.Alias.Add(reader.GetString());
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("created"u8))
                {
                    createdSeen = true;
                    reader.Read();

                    commit.Created = Utf8JsonReaderHelper.ReadDateTimeIso8601(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("description"u8))
                {
                    descriptionSeen = true;
                    reader.Read();

                    commit.Description = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("name"u8))
                {
                    nameSeen = true;
                    reader.Read();

                    commit.Name = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("owningProject"u8))
                {
                    owningProjectSeen = true;
                    reader.Read();

                    if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningProjectValue))
                    {
                        commit.OwningProject = owningProjectValue;
                    }
                    else if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug("the Commit.owningProject reference is null, the value is left at Guid.Empty");
                    }

                    continue;
                }

                if (reader.ValueTextEquals("previousCommit"u8))
                {
                    previousCommitSeen = true;
                    reader.Read();

                    commit.PreviousCommit = new List<Guid>();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                        {
                            if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var previousCommitItem))
                            {
                                commit.PreviousCommit.Add(previousCommitItem);
                            }
                        }
                    }

                    continue;
                }

                reader.Read();
                reader.Skip();
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                if (!idSeen)
                {
                    logger.LogDebug("the @id Json property was not found in the Commit");
                }

                if (!aliasSeen)
                {
                    logger.LogDebug("the alias Json property was not found in the Commit");
                }

                if (!createdSeen)
                {
                    logger.LogDebug("the created Json property was not found in the Commit");
                }

                if (!descriptionSeen)
                {
                    logger.LogDebug("the description Json property was not found in the Commit");
                }

                if (!nameSeen)
                {
                    logger.LogDebug("the name Json property was not found in the Commit");
                }

                if (!owningProjectSeen)
                {
                    logger.LogDebug("the owningProject Json property was not found in the Commit");
                }

                if (!previousCommitSeen)
                {
                    logger.LogDebug("the previousCommit Json property was not found in the Commit");
                }

            }

            return commit;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
