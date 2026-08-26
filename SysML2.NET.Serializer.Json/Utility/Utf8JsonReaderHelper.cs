// -------------------------------------------------------------------------------------------------
// <copyright file="Utf8JsonReaderHelper.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.Utility
{
    using System;
    using System.Globalization;
    using System.Text.Json;
    
    /// <summary>
    /// Provides low-level, allocation-minimizing helper methods for working directly
    /// with <see cref="Utf8JsonReader"/> in streaming JSON deserializers.
    /// </summary>
    public static class Utf8JsonReaderHelper
    {
        /// <summary>
        /// Ensures that the current token of the <see cref="Utf8JsonReader"/> matches
        /// the expected <see cref="JsonTokenType"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the token to validate.
        /// </param>
        /// <param name="tokenType">
        /// The expected <see cref="JsonTokenType"/>.
        /// </param>
        /// <exception cref="JsonException">
        /// Thrown when the current token does not match the expected token type.
        /// </exception>
        /// <remarks>
        /// This method is typically used immediately after advancing the reader
        /// (for example, when entering an object or array) to fail fast on malformed JSON.
        /// </remarks>
        public static void Expect(ref Utf8JsonReader reader, JsonTokenType tokenType)
        {
            if (reader.TokenType != tokenType)
            {
                throw new JsonException($"Expected {tokenType}, got {reader.TokenType}.");
            }
        }

        /// <summary>
        /// Reads the current JSON value as a string or <see langword="null"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token.
        /// </param>
        /// <returns>
        /// The string value if the token is <see cref="JsonTokenType.String"/>,
        /// or <see langword="null"/> if the token is <see cref="JsonTokenType.Null"/>.
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown when the token is neither a string nor <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This helper avoids repeated token checks in generated deserializers
        /// and enforces a strict <c>string | null</c> contract.
        /// </remarks>
        public static string ReadStringOrNull(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            
            if (reader.TokenType != JsonTokenType.String) throw new JsonException("Expected string or null.");
            
            return reader.GetString();
        }

        /// <summary>
        /// Reads the current JSON value as a nullable boolean.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token.
        /// </param>
        /// <returns>
        /// <see langword="true"/> or <see langword="false"/> when the token represents
        /// a JSON boolean, or <see langword="null"/> when the token is <see cref="JsonTokenType.Null"/>.
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown when the token is neither a boolean nor <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Intended for optional boolean properties where absence is semantically
        /// different from an explicit <c>false</c>.
        /// </remarks>
        public static bool? ReadBoolOrNull(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            
            if (reader.TokenType == JsonTokenType.True) return true;
            
            if (reader.TokenType == JsonTokenType.False) return false;
            
            throw new JsonException("Expected bool or null.");
        }
        
        /// <summary>
        /// Reads the current JSON value as an ISO 8601 date-time string and parses it
        /// into a <see cref="DateTime"/> using round-trip semantics.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> parsed using <see cref="DateTimeStyles.RoundtripKind"/>.
        /// </returns>
        /// <exception cref="JsonException">
        /// Thrown when the value is <see langword="null"/>, empty, or not a valid ISO 8601 date-time string.
        /// </exception>
        public static DateTime ReadDateTimeIso8601(ref Utf8JsonReader reader)
        {
            var s = ReadStringOrNull(ref reader);
            
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new JsonException("Expected ISO 8601 date-time string.");
            }

            return DateTime.Parse(s, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        }

        /// <summary>
        /// Reads the current JSON value as a <see cref="Uri"/> or <see langword="null"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token.
        /// </param>
        /// <returns>
        /// A <see cref="Uri"/> created from the string value, or <see langword="null"/>
        /// if the token is <see cref="JsonTokenType.Null"/> or an empty string.
        /// </returns>
        /// <exception cref="UriFormatException">
        /// Thrown when the string value cannot be parsed as a URI.
        /// </exception>
        public static Uri ReadUriOrNull(ref Utf8JsonReader reader)
        {
            var s = ReadStringOrNull(ref reader);
            
            if (string.IsNullOrWhiteSpace(s)) return null;

            return new Uri(s, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Skips the current JSON value, including any nested objects or arrays.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token to skip.
        /// </param>
        /// <remarks>
        /// This method is used to safely ignore unknown or unsupported properties
        /// while remaining forward-compatible with newer schema versions.
        /// </remarks>
        public static void SkipValue(ref Utf8JsonReader reader) => reader.Skip();

        /// <summary>
        /// Reads the current JSON value as a <see cref="Guid"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/> that the current value represents.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the current token is not a <see cref="JsonTokenType.String"/>.
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown when the string value is not a recognized <see cref="Guid"/> representation.
        /// </exception>
        /// <remarks>
        /// <see cref="Utf8JsonReader.TryGetGuid(out Guid)"/> parses the 16 bytes straight out of the UTF-8
        /// payload and only recognizes the hyphenated "D" format that the SysML v2 API emits. The
        /// <see cref="Guid.Parse(string)"/> fall-back preserves support for the remaining formats at the cost
        /// of transcoding the value, so that no payload that used to deserialize starts failing.
        /// </remarks>
        public static Guid ReadGuid(ref Utf8JsonReader reader)
        {
            return reader.TryGetGuid(out var value) ? value : Guid.Parse(reader.GetString());
        }

        /// <summary>
        /// Reads a reference value, which the SysML v2 JSON representation writes either as
        /// <c>null</c> or as an object carrying a single <c>@id</c> property.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token. On return the reader is
        /// positioned on the last token of that value, so that the caller's loop advances normally.
        /// </param>
        /// <param name="value">
        /// The identifier that the reference carries, or <see cref="Guid.Empty"/> when the method returns
        /// <see langword="false"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> when an <c>@id</c> was read, <see langword="false"/> when the reference is
        /// <c>null</c> or carries no non-null <c>@id</c>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the value is neither an object nor <c>null</c>, mirroring what
        /// <see cref="JsonElement.TryGetProperty(System.ReadOnlySpan{byte}, out JsonElement)"/> does for a
        /// non-object element.
        /// </exception>
        public static bool TryReadReferenceIdentifier(ref Utf8JsonReader reader, out Guid value)
        {
            value = Guid.Empty;

            if (reader.TokenType == JsonTokenType.Null)
            {
                return false;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new InvalidOperationException($"The requested operation requires an element of type 'Object', but the target element has type '{reader.TokenType}'.");
            }

            var found = false;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (!found && reader.ValueTextEquals("@id"u8))
                {
                    reader.Read();

                    if (reader.TokenType != JsonTokenType.Null)
                    {
                        value = ReadGuid(ref reader);
                        found = true;
                    }

                    continue;
                }

                reader.Read();
                reader.Skip();
            }

            return found;
        }

        /// <summary>
        /// Asserts that the reader is positioned on the start of an array.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the value token.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the current token is not <see cref="JsonTokenType.StartArray"/>, mirroring what
        /// <see cref="JsonElement.EnumerateArray"/> does for a non-array element.
        /// </exception>
        public static void ExpectArrayStart(ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new InvalidOperationException($"The requested operation requires an element of type 'Array', but the target element has type '{reader.TokenType}'.");
            }
        }
    }
}
