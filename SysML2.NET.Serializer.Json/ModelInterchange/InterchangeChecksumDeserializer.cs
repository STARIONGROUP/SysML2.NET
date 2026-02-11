// -------------------------------------------------------------------------------------------------
// <copyright file="InterchangeChecksumDeserializer.cs" company="Starion Group S.A.">
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
    using System.Buffers;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.ModelInterchange;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// Provides low-level JSON deserialization support for <see cref="InterchangeChecksum"/> objects.
    /// </summary>
    public static class InterchangeChecksumDeserializer
    {
        /// <summary>
        /// Deserializes a JSON object representing an <see cref="InterchangeChecksum"/>
        /// instance from the current position of a <see cref="Utf8JsonReader"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on a <see cref="JsonTokenType.StartObject"/>
        /// token that begins an <see cref="InterchangeChecksum"/> JSON object.
        /// </param>
        /// <param name="loggerFactory">
        /// Optional <see cref="ILoggerFactory"/> used to create a logger for diagnostics.
        /// </param>
        /// <returns>
        /// A populated <see cref="InterchangeChecksum"/> instance.
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown when the JSON structure does not conform to the expected object-based
        /// representation of a checksum descriptor.
        /// </exception>
        /// <remarks>
        /// Recognized properties:
        /// <list type="bullet">
        /// <item>
        /// <description><c>value</c> — mandatory hexadecimal checksum value.</description>
        /// </item>
        /// <item>
        /// <description><c>algorithm</c> — mandatory checksum algorithm identifier (e.g. <c>SHA256</c>).</description>
        /// </item>
        /// </list>
        /// All other properties are ignored.
        /// </remarks>
        public static InterchangeChecksum DeSerialize(ref Utf8JsonReader reader, ILoggerFactory loggerFactory = null)
        {
            var logger = loggerFactory == null ? NullLogger.Instance : loggerFactory.CreateLogger(nameof(InterchangeChecksum));

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            var checksum = new InterchangeChecksum();

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

                if (reader.ValueTextEquals("value"u8))
                {
                    reader.Read();
                    checksum.Value = Utf8JsonReaderHelper.ReadStringOrNull(ref reader) ?? string.Empty;
                    continue;
                }

                if (reader.ValueTextEquals("algorithm"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.String)
                    {
                        if (reader.HasValueSequence)
                        {
                            checksum.Algorithm = ChecksumKindProvider.Parse(reader.ValueSequence);
                        }
                        else
                        {
                            checksum.Algorithm = ChecksumKindProvider.Parse(reader.ValueSpan);
                        }
                    }
                    else if (reader.TokenType == JsonTokenType.Null)
                    {
                        // Leave default; tolerate missing algorithm for forward compatibility.
                    }
                    else
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

            return checksum;
        }
    }
}
