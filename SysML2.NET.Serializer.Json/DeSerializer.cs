// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Serializer.Json.Core.DTO;
    using SysML2.NET.Serializer.Json.PIM.DTO;

    /// <summary>
    /// The purpose of the <see cref="DeSerializer"/> is to deserialize a JSON <see cref="Stream"/> to
    /// an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// </summary>
    /// <remarks>
    /// The JSON payload is read into a pooled buffer and then walked with a <see cref="Utf8JsonReader"/>. No
    /// <see cref="JsonDocument"/> is materialized for the core payload at any point; each element is handed to
    /// its generated deserializer as a reader positioned on its opening brace.
    /// </remarks>
    public class DeSerializer : IDeSerializer
    {
        /// <summary>
        /// The size, in bytes, of the buffer that is rented when the length of the input stream is unknown
        /// </summary>
        private const int DefaultBufferSize = 81920;

        /// <summary>
        /// The maximum length, in bytes, that the pooled input buffer may grow to
        /// </summary>
        private const int MaxBufferLength = 0x7FFFFFC7;

        /// <summary>
        /// The (injected) <see cref="ILoggerFactory"/> used to setup logging
        /// </summary>
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// The <see cref="ILogger"/> used to log
        /// </summary>
        private readonly ILogger<DeSerializer> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeSerializer"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        /// The (injected) <see cref="ILoggerFactory"/> used to setup logging
        /// </param>
        public DeSerializer(ILoggerFactory loggerFactory = null)
        {
            this.loggerFactory = loggerFactory;

            this.logger = this.loggerFactory == null ? NullLogger<DeSerializer>.Instance : this.loggerFactory.CreateLogger<DeSerializer>();
        }

        /// <summary>
        /// Deserializes the JSON stream to an <see cref="IEnumerable{IData}"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="serializationTargetKind">
        /// The <see cref="SerializationTargetKind"/> to use
        /// </param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>
        /// </returns>
        public IEnumerable<IData> DeSerialize(Stream stream, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties)
        {
            var sw = Stopwatch.StartNew();

            var buffer = ReadToPooledBuffer(stream, out var length);

            try
            {
                var result = this.DeSerializeUtf8Json(buffer, length, serializationModeKind, serializationTargetKind, deserializeDerivedProperties);

                this.logger.LogInformation("stream deserialized in {ElapsedTime} [ms]", sw.ElapsedMilliseconds);

                return result;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Asynchronously deserializes the JSON stream to an <see cref="IEnumerable{IData}"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="serializationTargetKind">
        /// The <see cref="SerializationTargetKind"/> to use
        /// </param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>
        /// </returns>
        public async Task<IEnumerable<IData>> DeSerializeAsync(Stream stream, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            var (buffer, length) = await ReadToPooledBufferAsync(stream, cancellationToken);

            try
            {
                var result = this.DeSerializeUtf8Json(buffer, length, serializationModeKind, serializationTargetKind, deserializeDerivedProperties);

                this.logger.LogInformation("stream deserialized asynchronously in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);

                return result;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Deserializes the UTF-8 encoded JSON payload to a <see cref="List{IData}"/>
        /// </summary>
        /// <param name="utf8Json">
        /// the buffer that contains the UTF-8 encoded JSON payload
        /// </param>
        /// <param name="length">
        /// the number of bytes of <paramref name="utf8Json"/> that make up the payload
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="serializationTargetKind">
        /// The <see cref="SerializationTargetKind"/> to use
        /// </param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>
        /// a <see cref="List{IData}"/>
        /// </returns>
        /// <remarks>
        /// No <see cref="JsonDocument"/> is materialized at any point. Each element is handed to its generated
        /// deserializer as a <see cref="Utf8JsonReader"/> positioned on its <see cref="JsonTokenType.StartObject"/>,
        /// and the deserializer consumes it through to the matching <see cref="JsonTokenType.EndObject"/>.
        /// </remarks>
        private List<IData> DeSerializeUtf8Json(byte[] utf8Json, int length, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties)
        {
            var offset = HasUtf8ByteOrderMark(utf8Json, length) ? 3 : 0;

            var reader = new Utf8JsonReader(new ReadOnlySpan<byte>(utf8Json, offset, length - offset));

            var result = new List<IData>();

            if (!reader.Read())
            {
                throw new JsonException("The input does not contain any JSON tokens.");
            }

            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    result.Add(this.DeserializeObject(ref reader, serializationModeKind, serializationTargetKind, deserializeDerivedProperties));
                    break;

                case JsonTokenType.StartArray:

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        result.Add(this.DeserializeObject(ref reader, serializationModeKind, serializationTargetKind, deserializeDerivedProperties));
                    }

                    break;

                default:
                    throw new SerializationException();
            }

            if (reader.Read())
            {
                throw new JsonException("Additional text encountered after the top level JSON value.");
            }

            return result;
        }

        /// <summary>
        /// Deserializes the json object that the <see cref="Utf8JsonReader"/> is positioned on to an <see cref="IData"/> object
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the json
        /// object to deserialize. On return the reader is positioned on the matching <see cref="JsonTokenType.EndObject"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="serializationTargetKind">
        /// The <see cref="SerializationTargetKind"/> to use
        /// </param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>
        /// an instance of <see cref="IData"/>
        /// </returns>
        private IData DeserializeObject(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new ArgumentException($"The {nameof(reader)} must be positioned on a JsonTokenType.StartObject", nameof(reader));
            }

            if (!TryPeekTypeName(reader, out var typeName))
            {
                throw new SerializationException("The @type Json property is not available, the DeSerializer cannot be used to deserialize this JsonElement");
            }

            if (serializationTargetKind == SerializationTargetKind.PSM && ApiDeSerializationProvider.IsTypeSupported(typeName))
            {
                using var document = JsonDocument.ParseValue(ref reader);

                return ApiDeSerializationProvider.Provide(typeName)(document.RootElement, serializationModeKind, deserializeDerivedProperties, this.loggerFactory);
            }

            var func = DeSerializationProvider.Provide(typeName);

            return func(ref reader, serializationModeKind, deserializeDerivedProperties, this.loggerFactory);
        }

        /// <summary>
        /// Reads ahead for the <c>@type</c> discriminator of the json object that the reader is positioned on
        /// </summary>
        /// <param name="reader">
        /// A copy of the <see cref="Utf8JsonReader"/>, positioned on the <see cref="JsonTokenType.StartObject"/>
        /// of the json object
        /// </param>
        /// <param name="typeName">
        /// The value of the <c>@type</c> property, which is null when the property is present but null
        /// </param>
        /// <returns>
        /// true when the object carries a <c>@type</c> property, false otherwise
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the <c>@type</c> property is neither a string nor null
        /// </exception>
        /// <remarks>
        /// The reader is taken by value on purpose: <see cref="Utf8JsonReader"/> is a struct, so the copy is a
        /// free snapshot and the caller's reader stays parked on the <see cref="JsonTokenType.StartObject"/>.
        /// The SysML v2 API does not guarantee that <c>@type</c> comes first — it is the first property of the
        /// elements payload but the second of the projects payload — so the scan has to tolerate any position.
        /// </remarks>
        private static bool TryPeekTypeName(Utf8JsonReader reader, out string typeName)
        {
            typeName = null;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.ValueTextEquals("@type"u8))
                {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.Null)
                    {
                        return true;
                    }

                    if (reader.TokenType != JsonTokenType.String)
                    {
                        throw new InvalidOperationException($"The requested operation requires an element of type 'String', but the target element has type '{reader.TokenType}'.");
                    }

                    typeName = reader.GetString();

                    return true;
                }

                reader.Read();
                reader.Skip();
            }

            return false;
        }

        /// <summary>
        /// Reads the complete <see cref="Stream"/> into a buffer rented from the <see cref="ArrayPool{Byte}"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
        /// </param>
        /// <param name="length">
        /// the number of bytes that were read from the <paramref name="stream"/>
        /// </param>
        /// <returns>
        /// the rented buffer, which the caller is responsible for returning to the <see cref="ArrayPool{Byte}"/>
        /// </returns>
        private static byte[] ReadToPooledBuffer(Stream stream, out int length)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(ComputeInitialBufferLength(stream));
            var total = 0;

            while (true)
            {
                if (total == buffer.Length)
                {
                    buffer = Grow(buffer, total);
                }

                var read = stream.Read(buffer, total, buffer.Length - total);

                if (read == 0)
                {
                    break;
                }

                total += read;
            }

            length = total;

            return buffer;
        }

        /// <summary>
        /// Asynchronously reads the complete <see cref="Stream"/> into a buffer rented from the <see cref="ArrayPool{Byte}"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// the rented buffer, which the caller is responsible for returning to the <see cref="ArrayPool{Byte}"/>, and
        /// the number of bytes that were read from the <paramref name="stream"/>
        /// </returns>
        private static async Task<(byte[] Buffer, int Length)> ReadToPooledBufferAsync(Stream stream, CancellationToken cancellationToken)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(ComputeInitialBufferLength(stream));
            var total = 0;

            while (true)
            {
                if (total == buffer.Length)
                {
                    buffer = Grow(buffer, total);
                }

                var read = await stream.ReadAsync(buffer.AsMemory(total, buffer.Length - total), cancellationToken);

                if (read == 0)
                {
                    break;
                }

                total += read;
            }

            return (buffer, total);
        }

        /// <summary>
        /// Computes the length of the buffer to rent for the provided <see cref="Stream"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
        /// </param>
        /// <returns>
        /// the length of the buffer to rent
        /// </returns>
        /// <remarks>
        /// One byte more than the remaining length is requested so that the read loop observes the end of the
        /// stream without having to grow the buffer.
        /// </remarks>
        private static int ComputeInitialBufferLength(Stream stream)
        {
            if (!stream.CanSeek)
            {
                return DefaultBufferSize;
            }

            var remaining = stream.Length - stream.Position;

            return remaining <= 0 || remaining >= MaxBufferLength ? DefaultBufferSize : (int)remaining + 1;
        }

        /// <summary>
        /// Replaces the provided buffer with a larger one rented from the <see cref="ArrayPool{Byte}"/>
        /// </summary>
        /// <param name="buffer">
        /// the buffer that is full and needs to be replaced
        /// </param>
        /// <param name="count">
        /// the number of bytes of <paramref name="buffer"/> that need to be preserved
        /// </param>
        /// <returns>
        /// the larger buffer, which contains the first <paramref name="count"/> bytes of <paramref name="buffer"/>
        /// </returns>
        /// <exception cref="SerializationException">
        /// Thrown when the buffer cannot grow any further
        /// </exception>
        private static byte[] Grow(byte[] buffer, int count)
        {
            if (buffer.Length >= MaxBufferLength)
            {
                throw new SerializationException("The JSON payload exceeds the maximum length that can be deserialized.");
            }

            var grownLength = buffer.Length >= MaxBufferLength / 2 ? MaxBufferLength : buffer.Length * 2;

            var grown = ArrayPool<byte>.Shared.Rent(grownLength);

            Buffer.BlockCopy(buffer, 0, grown, 0, count);

            ArrayPool<byte>.Shared.Return(buffer);

            return grown;
        }

        /// <summary>
        /// Asserts whether the payload starts with a UTF-8 byte order mark
        /// </summary>
        /// <param name="utf8Json">
        /// the buffer that contains the UTF-8 encoded JSON payload
        /// </param>
        /// <param name="length">
        /// the number of bytes of <paramref name="utf8Json"/> that make up the payload
        /// </param>
        /// <returns>
        /// true when the payload starts with a UTF-8 byte order mark, false otherwise
        /// </returns>
        /// <remarks>
        /// <see cref="Utf8JsonReader"/> does not skip a byte order mark, whereas <see cref="JsonDocument.Parse(Stream, JsonDocumentOptions)"/> does.
        /// </remarks>
        private static bool HasUtf8ByteOrderMark(byte[] utf8Json, int length)
        {
            return length >= 3 && utf8Json[0] == 0xEF && utf8Json[1] == 0xBB && utf8Json[2] == 0xBF;
        }
    }
}
