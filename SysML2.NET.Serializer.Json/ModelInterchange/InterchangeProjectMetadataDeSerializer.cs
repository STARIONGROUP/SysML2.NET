// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeProjectMetadataDeSerializer.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Json.ModelInterchange
{
    using System;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.ModelInterchange;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// Provides low-level JSON deserialization support for the
    /// <see cref="InterchangeProjectMetadata"/> descriptor stored in <c>.meta.json</c>,
    /// as defined by the KerML Project Archive (<c>.kpar</c>) specification.
    /// </summary>
    /// <remarks>
    /// This deserializer operates directly on a forward-only <see cref="Utf8JsonReader"/>
    /// and does not rely on JSON annotations, reflection, or intermediate DOM representations.
    ///
    /// <para>
    /// The implementation is intentionally tolerant: unknown properties are skipped to allow
    /// forward compatibility with future versions of the interchange specification.
    /// </para>
    /// </remarks>
    public static class InterchangeProjectMetadataDeSerializer
    {
        /// <summary>
        /// Deserializes a JSON object representing an <see cref="InterchangeProjectMetadata"/>
        /// instance from the current position of a <see cref="Utf8JsonReader"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on a <see cref="JsonTokenType.StartObject"/>
        /// token that begins an <see cref="InterchangeProjectMetadata"/> JSON object.
        /// </param>
        /// <param name="loggerFactory">
        /// Optional <see cref="ILoggerFactory"/> used to create a logger for diagnostics.
        /// </param>
        /// <returns>
        /// A populated <see cref="InterchangeProjectMetadata"/> instance.
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown when the JSON structure does not conform to the expected object-based
        /// representation of interchange project metadata.
        /// </exception>
        /// <remarks>
        /// The following properties are recognized when present:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// <c>index</c> — mandatory JSON object mapping names to archive-relative model paths.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <c>created</c> — mandatory ISO 8601 date-time string.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <c>metamodel</c> — optional IRI/URI identifying the metamodel (language).
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <c>includesDerived</c> — optional boolean indicating whether derived property values are included.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <c>includesImplied</c> — optional boolean indicating whether implied relationships are included.
        /// </description>
        /// </item>
        /// </list>
        ///
        /// All other properties are ignored.
        /// </remarks>
        public static InterchangeProjectMetadata DeSerialize(ref Utf8JsonReader reader, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(nameof(InterchangeProjectMetadataDeSerializer));

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var metadata = new InterchangeProjectMetadata();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected property name.");
                }

                if (reader.ValueTextEquals("index"u8))
                {
                    reader.Read();
                    metadata.Index.Clear();

                    if (reader.TokenType == JsonTokenType.StartObject)
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndObject)
                            {
                                break;
                            }

                            if (reader.TokenType != JsonTokenType.PropertyName)
                            {
                                throw new JsonException("Expected property name in index object.");
                            }

                            var key = reader.GetString();

                            reader.Read();
                            var value = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

                            if (key is not null && value is not null)
                            {
                                metadata.Index[key] = value;
                            }
                        }
                    }
                    else if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.SkipValue(ref reader);
                    }

                    continue;
                }

                if (reader.ValueTextEquals("created"u8))
                {
                    reader.Read();
                    metadata.Created = Utf8JsonReaderHelper.ReadDateTimeIso8601(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("metamodel"u8))
                {
                    reader.Read();
                    metadata.Metamodel =
                        Utf8JsonReaderHelper.ReadUriOrNull(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("includesDerived"u8))
                {
                    reader.Read();
                    var value = Utf8JsonReaderHelper.ReadBoolOrNull(ref reader);
                    if (value.HasValue)
                    {
                        metadata.IncludesDerived = value.Value;
                    }

                    continue;
                }

                if (reader.ValueTextEquals("includesImplied"u8))
                {
                    reader.Read();
                    var value = Utf8JsonReaderHelper.ReadBoolOrNull(ref reader);
                    if (value.HasValue)
                    {
                        metadata.IncludesImplied = value.Value;
                    }

                    continue;
                }
                
                if (reader.ValueTextEquals("checksum"u8))
                {
                    reader.Read();
                    metadata.Checksum.Clear();

                    if (reader.TokenType == JsonTokenType.StartObject)
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndObject)
                            {
                                break;
                            }

                            if (reader.TokenType != JsonTokenType.PropertyName)
                            {
                                throw new JsonException("Expected property name in checksum object.");
                            }

                            var path = reader.GetString();

                            reader.Read();

                            if (reader.TokenType == JsonTokenType.StartObject)
                            {
                                var checksum = InterchangeChecksumDeserializer.DeSerialize(ref reader, loggerFactory);

                                if (path is not null)
                                {
                                    metadata.Checksum[path] = checksum;
                                }
                            }
                            else if (reader.TokenType == JsonTokenType.Null)
                            {
                                // tolerate null checksum entry
                            }
                            else
                            {
                                Utf8JsonReaderHelper.SkipValue(ref reader);
                            }
                        }
                    }
                    else if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.SkipValue(ref reader);
                    }

                    continue;
                }

                // Unknown property => skip value for forward compatibility
                var propertyName = reader.GetString();

                reader.Read();
                Utf8JsonReaderHelper.SkipValue(ref reader);

                logger.LogDebug("The property {Property} is unknown and skipped", propertyName);
            }

            return metadata;
        }
    }
}
