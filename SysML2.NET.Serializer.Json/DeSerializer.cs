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
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Serializer.Json.Core.DTO;
    using SysML2.NET.Serializer.Json.PIM.DTO;
    using SysML2.NET.Serializer.Json.PSM;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// The purpose of the <see cref="DeSerializer"/> is to deserialize a JSON <see cref="Stream"/> to
    /// an <see cref="IIdentified"/> and <see cref="IEnumerable{IIdentified}"/>
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
        /// Deserializes the JSON stream to an <see cref="IEnumerable{IIdentified}"/>
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
        /// an <see cref="IEnumerable{IIdentified}"/>
        /// </returns>
        public IEnumerable<IIdentified> DeSerialize(Stream stream, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties)
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
        /// Asynchronously deserializes the JSON stream to an <see cref="IEnumerable{IIdentified}"/>
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
        /// an <see cref="IEnumerable{IIdentified}"/>
        /// </returns>
        public async Task<IEnumerable<IIdentified>> DeSerializeAsync(Stream stream, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties, CancellationToken cancellationToken)
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
        /// Deserializes the JSON stream to a single <typeparamref name="T"/> request
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IRequest"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        public T DeSerializeRequest<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties) where T : IRequest
        {
            return ReadPooled(stream, (byte[] buffer, int length) => this.ReadSingle<T>(buffer, length, serializationModeKind, deserializeDerivedProperties, true));
        }

        /// <summary>
        /// Deserializes the JSON stream to a collection of <typeparamref name="T"/> requests
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IRequest"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        public IEnumerable<T> DeSerializeRequests<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties) where T : IRequest
        {
            return ReadPooled(stream, (byte[] buffer, int length) => this.ReadMany<T>(buffer, length, serializationModeKind, deserializeDerivedProperties, true));
        }

        /// <summary>
        /// Deserializes the JSON stream to a single <typeparamref name="T"/> response
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IResponse"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        public T DeSerializeResponse<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties) where T : IResponse
        {
            return ReadPooled(stream, (byte[] buffer, int length) => this.ReadSingle<T>(buffer, length, serializationModeKind, deserializeDerivedProperties, false));
        }

        /// <summary>
        /// Deserializes the JSON stream to a collection of <typeparamref name="T"/> responses
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IResponse"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        public IEnumerable<T> DeSerializeResponses<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties) where T : IResponse
        {
            return ReadPooled(stream, (byte[] buffer, int length) => this.ReadMany<T>(buffer, length, serializationModeKind, deserializeDerivedProperties, false));
        }

        /// <summary>
        /// Asynchronously deserializes the JSON stream to a single <typeparamref name="T"/> request
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IRequest"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        public Task<T> DeSerializeRequestAsync<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, CancellationToken cancellationToken) where T : IRequest
        {
            return ReadPooledAsync(stream, cancellationToken, (byte[] buffer, int length) => this.ReadSingle<T>(buffer, length, serializationModeKind, deserializeDerivedProperties, true));
        }

        /// <summary>
        /// Asynchronously deserializes the JSON stream to a collection of <typeparamref name="T"/> requests
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IRequest"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        public async Task<IEnumerable<T>> DeSerializeRequestsAsync<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, CancellationToken cancellationToken) where T : IRequest
        {
            return await ReadPooledAsync(stream, cancellationToken, (byte[] buffer, int length) => this.ReadMany<T>(buffer, length, serializationModeKind, deserializeDerivedProperties, true));
        }

        /// <summary>
        /// Asynchronously deserializes the JSON stream to a single <typeparamref name="T"/> response
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IResponse"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        public Task<T> DeSerializeResponseAsync<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, CancellationToken cancellationToken) where T : IResponse
        {
            return ReadPooledAsync(stream, cancellationToken, (byte[] buffer, int length) => this.ReadSingle<T>(buffer, length, serializationModeKind, deserializeDerivedProperties, false));
        }

        /// <summary>
        /// Asynchronously deserializes the JSON stream to a collection of <typeparamref name="T"/> responses
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IResponse"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        public async Task<IEnumerable<T>> DeSerializeResponsesAsync<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, CancellationToken cancellationToken) where T : IResponse
        {
            return await ReadPooledAsync(stream, cancellationToken, (byte[] buffer, int length) => this.ReadMany<T>(buffer, length, serializationModeKind, deserializeDerivedProperties, false));
        }

        /// <summary>
        /// Reads a single Systems Modeling API and Services object from the UTF-8 encoded JSON payload
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="utf8Json">the buffer that contains the UTF-8 encoded JSON payload</param>
        /// <param name="length">the number of bytes of <paramref name="utf8Json"/> that make up the payload</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="isRequestFamily">Asserts that the payload is resolved against the request family</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not a single object of the expected type</exception>
        private T ReadSingle<T>(byte[] utf8Json, int length, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, bool isRequestFamily)
        {
            var reader = CreateReader(utf8Json, length);

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"Expected a single JSON object, got {reader.TokenType}.");
            }

            var result = this.ReadFamilyObject<T>(ref reader, serializationModeKind, deserializeDerivedProperties, isRequestFamily);

            if (reader.Read())
            {
                throw new JsonException("Additional text encountered after the top level JSON value.");
            }

            return result;
        }

        /// <summary>
        /// Reads a collection of Systems Modeling API and Services objects from the UTF-8 encoded JSON payload
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="utf8Json">the buffer that contains the UTF-8 encoded JSON payload</param>
        /// <param name="length">the number of bytes of <paramref name="utf8Json"/> that make up the payload</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="isRequestFamily">Asserts that the payload is resolved against the request family</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is neither an object nor an array</exception>
        private IEnumerable<T> ReadMany<T>(byte[] utf8Json, int length, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, bool isRequestFamily)
        {
            var reader = CreateReader(utf8Json, length);

            var result = new List<T>();

            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    result.Add(this.ReadFamilyObject<T>(ref reader, serializationModeKind, deserializeDerivedProperties, isRequestFamily));
                    break;

                case JsonTokenType.StartArray:

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        result.Add(this.ReadFamilyObject<T>(ref reader, serializationModeKind, deserializeDerivedProperties, isRequestFamily));
                    }

                    break;

                default:
                    throw new JsonException($"Expected a JSON object or array, got {reader.TokenType}.");
            }

            if (reader.Read())
            {
                throw new JsonException("Additional text encountered after the top level JSON value.");
            }

            return result;
        }

        /// <summary>
        /// Reads the json object that the reader is positioned on and asserts that it is a <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/></param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="isRequestFamily">Asserts that the payload is resolved against the request family</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        /// <exception cref="JsonException">Thrown when the resolved object is not a <typeparamref name="T"/></exception>
        private T ReadFamilyObject<T>(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, bool isRequestFamily)
        {
            object resolved = isRequestFamily
                ? PsmDeSerializationDispatcher.ReadRequest(ref reader, serializationModeKind, deserializeDerivedProperties, this.loggerFactory)
                : PsmDeSerializationDispatcher.ReadResponse(ref reader, serializationModeKind, deserializeDerivedProperties, this.loggerFactory);

            if (resolved is not T expected)
            {
                throw new JsonException($"Expected a {typeof(T).Name}, the payload declares a {resolved.GetType().Name}.");
            }

            return expected;
        }

        /// <summary>
        /// Creates a <see cref="Utf8JsonReader"/> over the payload and advances it to the first token
        /// </summary>
        /// <param name="utf8Json">the buffer that contains the UTF-8 encoded JSON payload</param>
        /// <param name="length">the number of bytes of <paramref name="utf8Json"/> that make up the payload</param>
        /// <returns>a <see cref="Utf8JsonReader"/> positioned on the first token</returns>
        /// <exception cref="JsonException">Thrown when the payload contains no token</exception>
        private static Utf8JsonReader CreateReader(byte[] utf8Json, int length)
        {
            var offset = HasUtf8ByteOrderMark(utf8Json, length) ? 3 : 0;

            var reader = new Utf8JsonReader(new ReadOnlySpan<byte>(utf8Json, offset, length - offset));

            if (!reader.Read())
            {
                throw new JsonException("The input does not contain any JSON tokens.");
            }

            return reader;
        }

        /// <summary>
        /// Reads the stream into a pooled buffer, applies the read function and returns the buffer to the pool
        /// </summary>
        /// <typeparam name="TResult">The type that the read function returns.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="read">the function that reads the payload</param>
        /// <returns>the result of the read function</returns>
        private static TResult ReadPooled<TResult>(Stream stream, Func<byte[], int, TResult> read)
        {
            var buffer = ReadToPooledBuffer(stream, out var length);

            try
            {
                return read(buffer, length);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Asynchronously reads the stream into a pooled buffer, applies the read function and returns the buffer
        /// </summary>
        /// <typeparam name="TResult">The type that the read function returns.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <param name="read">the function that reads the payload</param>
        /// <returns>the result of the read function</returns>
        private static async Task<TResult> ReadPooledAsync<TResult>(Stream stream, CancellationToken cancellationToken, Func<byte[], int, TResult> read)
        {
            var (buffer, length) = await ReadToPooledBufferAsync(stream, cancellationToken);

            try
            {
                return read(buffer, length);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Deserializes the UTF-8 encoded JSON payload to a <see cref="List{IIdentified}"/>
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
        /// a <see cref="List{IIdentified}"/>
        /// </returns>
        /// <remarks>
        /// No <see cref="JsonDocument"/> is materialized at any point. Each element is handed to its generated
        /// deserializer as a <see cref="Utf8JsonReader"/> positioned on its <see cref="JsonTokenType.StartObject"/>,
        /// and the deserializer consumes it through to the matching <see cref="JsonTokenType.EndObject"/>.
        /// </remarks>
        private List<IIdentified> DeSerializeUtf8Json(byte[] utf8Json, int length, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties)
        {
            var reader = CreateReader(utf8Json, length);

            var result = new List<IIdentified>();

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
        /// Deserializes the json object that the <see cref="Utf8JsonReader"/> is positioned on to an <see cref="IIdentified"/> object
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
        /// an instance of <see cref="IIdentified"/>
        /// </returns>
        private IIdentified DeserializeObject(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new ArgumentException($"The {nameof(reader)} must be positioned on a JsonTokenType.StartObject", nameof(reader));
            }

            if (!Utf8JsonReaderHelper.TryPeekTypeName(reader, out var typeName))
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
