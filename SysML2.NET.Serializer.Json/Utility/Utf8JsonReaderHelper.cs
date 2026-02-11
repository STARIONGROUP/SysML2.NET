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
    }
}
