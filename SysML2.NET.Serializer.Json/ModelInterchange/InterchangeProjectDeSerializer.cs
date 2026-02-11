﻿// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeProjectDeSerializer.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Text.Json;
    
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    
    using SysML2.NET.ModelInterchange;
    using SysML2.NET.Serializer.Json.Utility;
    
    /// <summary>
    /// Provides low-level JSON deserialization support for the
    /// <see cref="InterchangeProject"/> descriptor defined by the KerML
    /// Project Archive (<c>.kpar</c>) specification.
    /// </summary>
    /// <remarks>
    ///  The implementation is intentionally tolerant:
    /// unknown properties are skipped to allow forward compatibility
    /// with future versions of the interchange specification.
    /// </remarks>
    public static class InterchangeProjectDeSerializer
    {
        /// <summary>
        /// Deserializes a JSON object representing an
        /// <see cref="InterchangeProject"/> from the current position of
        /// a <see cref="Utf8JsonReader"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on a
        /// <see cref="JsonTokenType.StartObject"/> token that begins
        /// an <see cref="InterchangeProject"/> JSON object.
        /// </param>
        /// <param name="loggerFactory">
        /// The <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        /// <returns>
        /// A instance of <see cref="InterchangeProject"/>.
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown when the JSON structure does not conform to the expected
        /// object-based representation of an interchange project.
        /// </exception>
        public static InterchangeProject DeSerialize(ref Utf8JsonReader reader, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger("InterchangeProjectDeSerializer");
            
            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var project = new InterchangeProject();
            var usage = new List<InterchangeProjectUsage>();

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

                if (reader.ValueTextEquals("name"u8))
                {
                    reader.Read();
                    project.Name = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("description"u8))
                {
                    reader.Read();
                    project.Description = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("version"u8))
                {
                    reader.Read();
                    project.Version = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("license"u8))
                {
                    reader.Read();
                    project.License = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("maintainer"u8))
                {
                    reader.Read();
                    project.Maintainer.Clear();

                    if (reader.TokenType == JsonTokenType.StartArray)
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndArray)
                            {
                                break;
                            }

                            if (reader.TokenType == JsonTokenType.String)
                            {
                                var s = reader.GetString();
                                if (s is not null)
                                {
                                    project.Maintainer.Add(s);
                                }
                            }
                            else if (reader.TokenType != JsonTokenType.Null)
                            {
                                throw new JsonException("Expected string in maintainer array.");
                            }
                        }
                    }
                    else if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.SkipValue(ref reader);
                    }

                    continue;
                }

                if (reader.ValueTextEquals("website"u8))
                {
                    reader.Read();
                    project.Website = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);
                    continue;
                }

                if (reader.ValueTextEquals("topic"u8))
                {
                    reader.Read();
                    project.Topic.Clear();

                    if (reader.TokenType == JsonTokenType.StartArray)
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndArray)
                            {
                                break;
                            }

                            if (reader.TokenType == JsonTokenType.String)
                            {
                                var s = reader.GetString();
                                if (s is not null)
                                {
                                    project.Topic.Add(s);
                                }
                            }
                            else if (reader.TokenType != JsonTokenType.Null)
                            {
                                throw new JsonException("Expected string in topic array.");
                            }
                        }
                    }
                    else if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.SkipValue(ref reader);
                    }

                    continue;
                }

                if (reader.ValueTextEquals("usage"u8))
                {
                    reader.Read();
                    usage.Clear();

                    if (reader.TokenType == JsonTokenType.StartArray)
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndArray)
                            {
                                break;
                            }

                            if (reader.TokenType == JsonTokenType.Null)
                            {
                                continue;
                            }

                            var u = InterchangeProjectUsageDeSerializer.DeSerialize(ref reader, loggerFactory);
                            usage.Add(u);
                        }
                    }
                    else if (reader.TokenType != JsonTokenType.Null)
                    {
                        Utf8JsonReaderHelper.SkipValue(ref reader);
                    }

                    project.Usage = usage;
                    continue;
                }

                reader.Read();
                Utf8JsonReaderHelper.SkipValue(ref reader);
                
                logger.LogDebug("The property {Property} is unknown and skipped", reader.GetString());
            }

            return project;
        }
    }
}
