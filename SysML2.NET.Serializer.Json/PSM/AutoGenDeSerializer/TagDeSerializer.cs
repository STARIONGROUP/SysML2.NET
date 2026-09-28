// -------------------------------------------------------------------------------------------------
// <copyright file="TagDeSerializer.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="TagDeSerializer"/> is to provide deserialization capabilities
    /// for the <see cref="Tag"/> class
    /// </summary>
    internal static class TagDeSerializer
    {
        /// <summary>
        /// Deserializes an instance of <see cref="Tag"/> from the provided <see cref="Utf8JsonReader"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the
        /// <see cref="Tag"/> json object. On return the reader is positioned on the matching
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
        /// an instance of <see cref="Tag"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the serialization mode is not supported
        /// </exception>
        /// <remarks>
        /// The <c>@type</c> property is the discriminator that the caller dispatched on, so it is skipped rather
        /// than re-validated here
        /// </remarks>
        internal static Tag DeSerialize(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory = null)
        {
            if (serializationModeKind != SerializationModeKind.JSON)
            {
                throw new NotSupportedException($"The {serializationModeKind} serialization mode is not supported");
            }

            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("TagDeSerializer");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var tag = new Tag();

            var idSeen = false;
            var aliasSeen = false;
            var createdSeen = false;
            var deletedSeen = false;
            var descriptionSeen = false;
            var nameSeen = false;
            var owningProjectSeen = false;
            var referencedCommitSeen = false;
            var taggedCommitSeen = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected a property name in the Tag json object.");
                }

                if (reader.ValueTextEquals("@id"u8))
                {
                    idSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        throw new JsonException("The @id property is null, the Tag cannot be deserialized");
                    }

                    tag.Id = Utf8JsonReaderHelper.ReadGuid(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("alias"u8))
                {
                    aliasSeen = true;
                    reader.Read();

                    tag.Alias = new List<string>();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.ExpectArrayStart(ref reader);

                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                        {
                            tag.Alias.Add(reader.GetString());
                        }
                    }

                    continue;
                }

                if (reader.ValueTextEquals("created"u8))
                {
                    createdSeen = true;
                    reader.Read();

                    tag.Created = Utf8JsonReaderHelper.ReadDateTimeIso8601(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("deleted"u8))
                {
                    deletedSeen = true;
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        tag.Deleted = null;
                    }
                    else
                    {
                        tag.Deleted = Utf8JsonReaderHelper.ReadDateTimeIso8601(ref reader);
                    }

                    continue;
                }

                if (reader.ValueTextEquals("description"u8))
                {
                    descriptionSeen = true;
                    reader.Read();

                    tag.Description = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("name"u8))
                {
                    nameSeen = true;
                    reader.Read();

                    tag.Name = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

                    continue;
                }

                if (reader.ValueTextEquals("owningProject"u8))
                {
                    owningProjectSeen = true;
                    reader.Read();

                    if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var owningProjectValue))
                    {
                        tag.OwningProject = owningProjectValue;
                    }
                    else if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug("the Tag.owningProject reference is null, the value is left at Guid.Empty");
                    }

                    continue;
                }

                if (reader.ValueTextEquals("referencedCommit"u8))
                {
                    referencedCommitSeen = true;
                    reader.Read();

                    if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var referencedCommitValue))
                    {
                        tag.ReferencedCommit = referencedCommitValue;
                    }
                    else if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug("the Tag.referencedCommit reference is null, the value is left at Guid.Empty");
                    }

                    continue;
                }

                if (reader.ValueTextEquals("taggedCommit"u8))
                {
                    taggedCommitSeen = true;
                    reader.Read();

                    if (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var taggedCommitValue))
                    {
                        tag.TaggedCommit = taggedCommitValue;
                    }
                    else if (logger.IsEnabled(LogLevel.Debug))
                    {
                        logger.LogDebug("the Tag.taggedCommit reference is null, the value is left at Guid.Empty");
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
                    logger.LogDebug("the @id Json property was not found in the Tag");
                }

                if (!aliasSeen)
                {
                    logger.LogDebug("the alias Json property was not found in the Tag");
                }

                if (!createdSeen)
                {
                    logger.LogDebug("the created Json property was not found in the Tag");
                }

                if (!deletedSeen)
                {
                    logger.LogDebug("the deleted Json property was not found in the Tag");
                }

                if (!descriptionSeen)
                {
                    logger.LogDebug("the description Json property was not found in the Tag");
                }

                if (!nameSeen)
                {
                    logger.LogDebug("the name Json property was not found in the Tag");
                }

                if (!owningProjectSeen)
                {
                    logger.LogDebug("the owningProject Json property was not found in the Tag");
                }

                if (!referencedCommitSeen)
                {
                    logger.LogDebug("the referencedCommit Json property was not found in the Tag");
                }

                if (!taggedCommitSeen)
                {
                    logger.LogDebug("the taggedCommit Json property was not found in the Tag");
                }

            }

            return tag;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
