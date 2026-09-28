// -------------------------------------------------------------------------------------------------
// <copyright file="PsmDeSerializationDispatcher.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.PSM
{
    using System.Runtime.Serialization;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Serializer.Json.Utility;

    /// <summary>
    /// Resolves the deserialization method of a payload that may be a Systems Modeling API and Services data
    /// transfer object or a SysML Core element
    /// </summary>
    /// <remarks>
    /// The Data union admits Element alongside the Systems Modeling API and Services types, so a payload is
    /// resolved against both providers.
    /// </remarks>
    internal static class PsmDeSerializationDispatcher
    {
        /// <summary>
        /// Deserializes the json object that the reader is positioned on to an <see cref="IData"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the payload
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
        /// an instance of <see cref="IData"/>
        /// </returns>
        /// <exception cref="SerializationException">
        /// Thrown when the payload carries no <c>@type</c> discriminator
        /// </exception>
        internal static IData ReadData(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory)
        {
            var typeName = ReadTypeName(reader);

            if (ResponseDeSerializationProvider.IsTypeSupported(typeName))
            {
                var response = ResponseDeSerializationProvider.Provide(typeName)(ref reader, serializationModeKind, deserializeDerivedProperties, loggerFactory);

                return response as IData ?? throw new SerializationException($"A {typeName} cannot be carried as a Data payload.");
            }

            return SysML2.NET.Serializer.Json.Core.DTO.DeSerializationProvider.Provide(typeName)(ref reader, serializationModeKind, deserializeDerivedProperties, loggerFactory);
        }

        /// <summary>
        /// Deserializes the json object that the reader is positioned on to an <see cref="IDataRequest"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the payload
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
        /// an instance of <see cref="IDataRequest"/>
        /// </returns>
        /// <exception cref="SerializationException">
        /// Thrown when the payload carries no <c>@type</c> discriminator
        /// </exception>
        internal static IDataRequest ReadDataRequest(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory)
        {
            var typeName = ReadTypeName(reader);

            if (RequestDeSerializationProvider.IsTypeSupported(typeName))
            {
                var request = RequestDeSerializationProvider.Provide(typeName)(ref reader, serializationModeKind, deserializeDerivedProperties, loggerFactory);

                return request as IDataRequest ?? throw new SerializationException($"A {typeName} cannot be carried as a DataRequest payload.");
            }

            var element = SysML2.NET.Serializer.Json.Core.DTO.DeSerializationProvider.Provide(typeName)(ref reader, serializationModeKind, deserializeDerivedProperties, loggerFactory);

            return element as IDataRequest ?? throw new SerializationException($"A {typeName} cannot be carried as a DataRequest payload.");
        }

        /// <summary>
        /// Deserializes the json object that the reader is positioned on to an <see cref="IResponse"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the object
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
        /// an instance of <see cref="IResponse"/>
        /// </returns>
        /// <exception cref="SerializationException">
        /// Thrown when the object carries no <c>@type</c> discriminator
        /// </exception>
        internal static IResponse ReadResponse(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory)
        {
            var typeName = ReadTypeName(reader);

            return ResponseDeSerializationProvider.Provide(typeName)(ref reader, serializationModeKind, deserializeDerivedProperties, loggerFactory);
        }

        /// <summary>
        /// Deserializes the json object that the reader is positioned on to an <see cref="IRequest"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the object
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
        /// an instance of <see cref="IRequest"/>
        /// </returns>
        /// <exception cref="SerializationException">
        /// Thrown when the object carries no <c>@type</c> discriminator
        /// </exception>
        internal static IRequest ReadRequest(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory)
        {
            var typeName = ReadTypeName(reader);

            return RequestDeSerializationProvider.Provide(typeName)(ref reader, serializationModeKind, deserializeDerivedProperties, loggerFactory);
        }

        /// <summary>
        /// Reads ahead for the <c>@type</c> discriminator of the json object that the reader is positioned on
        /// </summary>
        /// <param name="reader">
        /// A copy of the <see cref="Utf8JsonReader"/>, positioned on the <see cref="JsonTokenType.StartObject"/>
        /// </param>
        /// <returns>
        /// The value of the <c>@type</c> property
        /// </returns>
        /// <exception cref="SerializationException">
        /// Thrown when the object carries no <c>@type</c> discriminator
        /// </exception>
        private static string ReadTypeName(Utf8JsonReader reader)
        {
            if (!Utf8JsonReaderHelper.TryPeekTypeName(reader, out var typeName) || string.IsNullOrWhiteSpace(typeName))
            {
                throw new SerializationException("The @type Json property is not available, the payload cannot be deserialized");
            }

            return typeName;
        }
    }
}
