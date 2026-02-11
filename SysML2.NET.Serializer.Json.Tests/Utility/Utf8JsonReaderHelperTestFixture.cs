// -------------------------------------------------------------------------------------------------
// <copyright file="Utf8JsonReaderHelperTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// 
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.Utility.Tests
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Serializer.Json.Utility;

    [TestFixture]
    public sealed class Utf8JsonReaderHelperTestFixture
    {
        [Test]
        public void Verify_that_Expect_does_not_throw_when_token_matches()
        {
            var reader = CreateReader("true");

            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.True);

            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.True));
        }

        [Test]
        public void Verify_that_Expect_throws_when_token_does_not_match()
        {
            Assert.That(
                InvokeExpectWithFalse,
                Throws.TypeOf<JsonException>().With.Message.Contains("Expected"));

            static void InvokeExpectWithFalse()
            {
                var reader = CreateReader("false");
                Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.True);
            }
        }

        [Test]
        public void Verify_that_ReadStringOrNull_returns_null_when_token_is_null()
        {
            var reader = CreateReader("null");

            var value = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

            Assert.That(value, Is.Null);
        }

        [Test]
        public void Verify_that_ReadStringOrNull_returns_string_when_token_is_string()
        {
            var reader = CreateReader("\"hello\"");

            var value = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);

            Assert.That(value, Is.EqualTo("hello"));
        }

        [Test]
        public void Verify_that_ReadStringOrNull_throws_when_token_is_not_string_or_null()
        {
            Assert.That(
                InvokeReadStringOrNullWithNumber,
                Throws.TypeOf<JsonException>().With.Message.Contains("Expected string or null"));

            static void InvokeReadStringOrNullWithNumber()
            {
                var reader = CreateReader("123");
                _ = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);
            }
        }

        [Test]
        public void Verify_that_ReadBoolOrNull_returns_null_when_token_is_null()
        {
            var reader = CreateReader("null");

            var value = Utf8JsonReaderHelper.ReadBoolOrNull(ref reader);

            Assert.That(value, Is.Null);
        }

        [Test]
        public void Verify_that_ReadBoolOrNull_returns_true_when_token_is_true()
        {
            var reader = CreateReader("true");

            var value = Utf8JsonReaderHelper.ReadBoolOrNull(ref reader);

            Assert.That(value, Is.True);
        }

        [Test]
        public void Verify_that_ReadBoolOrNull_returns_false_when_token_is_false()
        {
            var reader = CreateReader("false");

            var value = Utf8JsonReaderHelper.ReadBoolOrNull(ref reader);

            Assert.That(value, Is.False);
        }

        [Test]
        public void Verify_that_ReadBoolOrNull_throws_when_token_is_not_bool_or_null()
        {
            Assert.That(
                InvokeReadBoolOrNullWithString,
                Throws.TypeOf<JsonException>().With.Message.Contains("Expected bool or null"));

            static void InvokeReadBoolOrNullWithString()
            {
                var reader = CreateReader("\"true\"");
                _ = Utf8JsonReaderHelper.ReadBoolOrNull(ref reader);
            }
        }

        [Test]
        public void Verify_that_ReadDateTimeIso8601_parses_roundtrip_kind()
        {
            // Use a value that encodes offset to make RoundtripKind relevant.
            var iso = "2026-02-15T12:34:56.789+01:00";
            var reader = CreateReader($"\"{iso}\"");

            var dt = Utf8JsonReaderHelper.ReadDateTimeIso8601(ref reader);

            var expected = DateTime.Parse(iso, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            Assert.That(dt, Is.EqualTo(expected));
        }

        [TestCase("null")]
        [TestCase("\"\"")]
        [TestCase("\"   \"")]
        public void Verify_that_ReadDateTimeIso8601_throws_when_value_is_null_or_whitespace(string json)
        {
            Assert.That(
                () => InvokeReadDateTimeIso8601(json),
                Throws.TypeOf<JsonException>().With.Message.Contains("Expected ISO 8601 date-time string"));
        }

        private static void InvokeReadDateTimeIso8601(string json)
        {
            var reader = CreateReader(json);
            _ = Utf8JsonReaderHelper.ReadDateTimeIso8601(ref reader);
        }

        [Test]
        public void Verify_that_ReadDateTimeIso8601_throws_when_value_is_not_a_valid_datetime()
        {
            Assert.That(
                InvokeReadDateTimeIso8601WithInvalidDate,
                Throws.TypeOf<FormatException>());

            static void InvokeReadDateTimeIso8601WithInvalidDate()
            {
                var reader = CreateReader("\"not-a-date\"");
                _ = Utf8JsonReaderHelper.ReadDateTimeIso8601(ref reader);
            }
        }

        [Test]
        public void Verify_that_ReadUriOrNull_returns_null_when_token_is_null()
        {
            var reader = CreateReader("null");

            var uri = Utf8JsonReaderHelper.ReadUriOrNull(ref reader);

            Assert.That(uri, Is.Null);
        }

        [TestCase("\"\"")]
        [TestCase("\"   \"")]
        public void Verify_that_ReadUriOrNull_returns_null_when_string_is_empty_or_whitespace(string json)
        {
            var reader = CreateReader(json);

            var uri = Utf8JsonReaderHelper.ReadUriOrNull(ref reader);

            Assert.That(uri, Is.Null);
        }

        [Test]
        public void Verify_that_ReadUriOrNull_parses_absolute_uri()
        {
            var reader = CreateReader("\"https://example.com/a/b\"");

            var uri = Utf8JsonReaderHelper.ReadUriOrNull(ref reader);

            Assert.That(uri, Is.Not.Null);
            Assert.That(uri.IsAbsoluteUri, Is.True);
            Assert.That(uri.AbsoluteUri, Is.EqualTo("https://example.com/a/b"));
        }

        [Test]
        public void Verify_that_ReadUriOrNull_parses_relative_uri()
        {
            var reader = CreateReader("\"./folder/file.kerml\"");

            var uri = Utf8JsonReaderHelper.ReadUriOrNull(ref reader);

            Assert.That(uri, Is.Not.Null);
            Assert.That(uri.IsAbsoluteUri, Is.False);
            Assert.That(uri.OriginalString, Is.EqualTo("./folder/file.kerml"));
        }
        
        [Test]
        public void Verify_that_SkipValue_skips_nested_value_and_positions_reader_on_next_token()
        {
            // We position the reader on the value token (StartObject) of property "a", then skip it,
            // and validate that we end up on the PropertyName token for "b".
            var json = "{\"a\":{\"x\":[1,2,{\"y\":3}]},\"b\":42}";
            var reader = CreateReader(json);

            // StartObject (root)
            Utf8JsonReaderHelper.Expect(ref reader, JsonTokenType.StartObject);

            // PropertyName "a"
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.PropertyName));
            Assert.That(reader.GetString(), Is.EqualTo("a"));

            // Value for "a" (StartObject)
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.StartObject));

            Utf8JsonReaderHelper.SkipValue(ref reader);

            // After Skip(), reader is positioned on the last token of the skipped value (EndObject).
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.EndObject));

            // Next token should be PropertyName "b"
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.PropertyName));
            Assert.That(reader.GetString(), Is.EqualTo("b"));

            // And its value
            Assert.That(reader.Read(), Is.True);
            Assert.That(reader.TokenType, Is.EqualTo(JsonTokenType.Number));
            Assert.That(reader.GetInt32(), Is.EqualTo(42));
        }

        private static Utf8JsonReader CreateReader(string json)
        {
            var bytes = Encoding.UTF8.GetBytes(json);

            var reader = new Utf8JsonReader(bytes, new JsonReaderOptions
            {
                CommentHandling = JsonCommentHandling.Disallow,
                AllowTrailingCommas = false
            });

            Assert.That(reader.Read(), Is.True, "Test JSON must produce at least one token.");
            return reader;
        }
    }
}
