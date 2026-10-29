// ------------------------------------------------------------------------------------------------
// <copyright file="IDeSerializer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Common;
    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// The purpose of the <see cref="IDeSerializer"/> is to deserialize a JSON <see cref="Stream"/> to
    /// an <see cref="IIdentified"/> and <see cref="IEnumerable{IIdentified}"/>
    /// </summary>
    public interface IDeSerializer
    {
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
        IEnumerable<IIdentified> DeSerialize(Stream stream, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties);

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
        Task<IEnumerable<IIdentified>> DeSerializeAsync(Stream stream, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, bool deserializeDerivedProperties, CancellationToken cancellationToken);

        /// <summary>
        /// Deserializes the JSON stream to a single <typeparamref name="T"/> request
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IRequest"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not valid JSON, declares an unknown <c>@type</c>, or is not a <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException">Thrown when an object carries no <c>@type</c> discriminator or the payload exceeds the maximum length</exception>
        /// <exception cref="InvalidOperationException">Thrown when a property value has a JSON token type that the property does not accept</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="serializationModeKind"/> is not supported</exception>
        T DeSerializeRequest<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties) where T : IRequest;

        /// <summary>
        /// Deserializes the JSON stream to a collection of <typeparamref name="T"/> requests
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IRequest"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not valid JSON, declares an unknown <c>@type</c>, or is not a <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException">Thrown when an object carries no <c>@type</c> discriminator or the payload exceeds the maximum length</exception>
        /// <exception cref="InvalidOperationException">Thrown when a property value has a JSON token type that the property does not accept</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="serializationModeKind"/> is not supported</exception>
        IEnumerable<T> DeSerializeRequests<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties) where T : IRequest;

        /// <summary>
        /// Deserializes the JSON stream to a single <typeparamref name="T"/> response
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IResponse"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not valid JSON, declares an unknown <c>@type</c>, or is not a <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException">Thrown when an object carries no <c>@type</c> discriminator or the payload exceeds the maximum length</exception>
        /// <exception cref="InvalidOperationException">Thrown when a property value has a JSON token type that the property does not accept</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="serializationModeKind"/> is not supported</exception>
        T DeSerializeResponse<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties) where T : IResponse;

        /// <summary>
        /// Deserializes the JSON stream to a collection of <typeparamref name="T"/> responses
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IResponse"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not valid JSON, declares an unknown <c>@type</c>, or is not a <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException">Thrown when an object carries no <c>@type</c> discriminator or the payload exceeds the maximum length</exception>
        /// <exception cref="InvalidOperationException">Thrown when a property value has a JSON token type that the property does not accept</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="serializationModeKind"/> is not supported</exception>
        IEnumerable<T> DeSerializeResponses<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties) where T : IResponse;

        /// <summary>
        /// Asynchronously deserializes the JSON stream to a single <typeparamref name="T"/> request
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IRequest"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not valid JSON, declares an unknown <c>@type</c>, or is not a <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException">Thrown when an object carries no <c>@type</c> discriminator or the payload exceeds the maximum length</exception>
        /// <exception cref="InvalidOperationException">Thrown when a property value has a JSON token type that the property does not accept</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="serializationModeKind"/> is not supported</exception>
        Task<T> DeSerializeRequestAsync<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, CancellationToken cancellationToken) where T : IRequest;

        /// <summary>
        /// Asynchronously deserializes the JSON stream to a collection of <typeparamref name="T"/> requests
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IRequest"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not valid JSON, declares an unknown <c>@type</c>, or is not a <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException">Thrown when an object carries no <c>@type</c> discriminator or the payload exceeds the maximum length</exception>
        /// <exception cref="InvalidOperationException">Thrown when a property value has a JSON token type that the property does not accept</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="serializationModeKind"/> is not supported</exception>
        Task<IEnumerable<T>> DeSerializeRequestsAsync<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, CancellationToken cancellationToken) where T : IRequest;

        /// <summary>
        /// Asynchronously deserializes the JSON stream to a single <typeparamref name="T"/> response
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IResponse"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an instance of <typeparamref name="T"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not valid JSON, declares an unknown <c>@type</c>, or is not a <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException">Thrown when an object carries no <c>@type</c> discriminator or the payload exceeds the maximum length</exception>
        /// <exception cref="InvalidOperationException">Thrown when a property value has a JSON token type that the property does not accept</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="serializationModeKind"/> is not supported</exception>
        Task<T> DeSerializeResponseAsync<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, CancellationToken cancellationToken) where T : IResponse;

        /// <summary>
        /// Asynchronously deserializes the JSON stream to a collection of <typeparamref name="T"/> responses
        /// </summary>
        /// <typeparam name="T">The expected <see cref="IResponse"/> type.</typeparam>
        /// <param name="stream">the JSON input stream</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use</param>
        /// <param name="deserializeDerivedProperties">Asserts that the deserializer should deserialize derived properties if present or if they are ignored</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="JsonException">Thrown when the payload is not valid JSON, declares an unknown <c>@type</c>, or is not a <typeparamref name="T"/></exception>
        /// <exception cref="SerializationException">Thrown when an object carries no <c>@type</c> discriminator or the payload exceeds the maximum length</exception>
        /// <exception cref="InvalidOperationException">Thrown when a property value has a JSON token type that the property does not accept</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="serializationModeKind"/> is not supported</exception>
        Task<IEnumerable<T>> DeSerializeResponsesAsync<T>(Stream stream, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, CancellationToken cancellationToken) where T : IResponse;
    }
}
