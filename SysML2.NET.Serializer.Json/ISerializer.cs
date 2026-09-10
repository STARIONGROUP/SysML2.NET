// -------------------------------------------------------------------------------------------------
// <copyright file="ISerializer.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Common;
    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// The purpose of the <see cref="ISerializer"/> is to write an <see cref="IIdentified"/> and <see cref="IEnumerable{IIdentified}"/>
    /// as JSON to a <see cref="Stream"/>
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize an <see cref="IEnumerable{IIdentified}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IIdentified}"/> that shall be serialized
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="jsonWriterOptions">
        /// The <see cref="JsonWriterOptions"/> to use
        /// </param>
        void Serialize(IEnumerable<IIdentified> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions);

        /// <summary>
        /// Serialize an <see cref="IIdentified"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IIdentified"/> that shall be serialized
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="jsonWriterOptions">
        /// The <see cref="JsonWriterOptions"/> to use
        /// </param>
        void Serialize(IIdentified dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions);

        /// <summary>
        /// Asynchronously serialize an <see cref="IEnumerable{IIdentified}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IIdentified}"/> that shall be serialized
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="jsonWriterOptions">
        /// The <see cref="JsonWriterOptions"/> to use
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        Task SerializeAsync(IEnumerable<IIdentified> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously serialize an <see cref="IIdentified"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IIdentified"/> that shall be serialized
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="jsonWriterOptions">
        /// The <see cref="JsonWriterOptions"/> to use
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        Task SerializeAsync(IIdentified dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken);

        /// <summary>
        /// Serialize an <see cref="IRequest"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">The <see cref="IRequest"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        void SerializeRequest(IRequest dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions);

        /// <summary>
        /// Serialize an <see cref="IEnumerable{IRequest}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">The <see cref="IEnumerable{IRequest}"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        void SerializeRequest(IEnumerable<IRequest> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions);

        /// <summary>
        /// Serialize an <see cref="IResponse"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">The <see cref="IResponse"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        void SerializeResponse(IResponse dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions);

        /// <summary>
        /// Serialize an <see cref="IEnumerable{IResponse}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">The <see cref="IEnumerable{IResponse}"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        void SerializeResponse(IEnumerable<IResponse> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions);

        /// <summary>
        /// Asynchronously serialize an <see cref="IRequest"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">The <see cref="IRequest"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
        /// <returns>an awaitable <see cref="Task"/>.</returns>
        Task SerializeRequestAsync(IRequest dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously serialize an <see cref="IEnumerable{IRequest}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">The <see cref="IEnumerable{IRequest}"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
        /// <returns>an awaitable <see cref="Task"/>.</returns>
        Task SerializeRequestAsync(IEnumerable<IRequest> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously serialize an <see cref="IResponse"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">The <see cref="IResponse"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
        /// <returns>an awaitable <see cref="Task"/>.</returns>
        Task SerializeResponseAsync(IResponse dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously serialize an <see cref="IEnumerable{IResponse}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">The <see cref="IEnumerable{IResponse}"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
        /// <returns>an awaitable <see cref="Task"/>.</returns>
        Task SerializeResponseAsync(IEnumerable<IResponse> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken);
    }
}
