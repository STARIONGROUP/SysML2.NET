// -------------------------------------------------------------------------------------------------
// <copyright file="Serializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Serializer.Json.PIM.DTO;
    using SysML2.NET.Serializer.Json.Core.DTO;

    using PsmSerializationProvider = SysML2.NET.Serializer.Json.PSM.SerializationProvider;

    /// <summary>
    /// The purpose of the <see cref="Serializer"/> is to write an <see cref="IElement"/> and <see cref="IEnumerable{IElement}"/>
    /// as JSON to a <see cref="Stream"/>
    /// </summary>
    public class Serializer : ISerializer
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
        public void Serialize(IEnumerable<IIdentified> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions)
        {
            using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            writer.WriteStartArray();

            foreach (var dataItem in dataItems)
            {
                if (ApiSerializationProvider.IsTypeSupported(dataItem.GetType()))
                {
                    var serializationAction = ApiSerializationProvider.Provide(dataItem.GetType());
                    serializationAction(dataItem, writer, serializationModeKind, includeDerivedProperties);
                    writer.Flush();
                }
                else
                {
                    var serializationAction = SerializationProvider.Provide(dataItem.GetType());
                    serializationAction(dataItem, writer, serializationModeKind, includeDerivedProperties);
                    writer.Flush();
                }
            }

            writer.WriteEndArray();

            writer.Flush();
        }

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
        public void Serialize(IIdentified dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions)
        {
            using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            if (ApiSerializationProvider.IsTypeSupported(dataItem.GetType()))
            {
                var serializationAction = ApiSerializationProvider.Provide(dataItem.GetType());
                serializationAction(dataItem, writer, serializationModeKind, includeDerivedProperties);
                writer.Flush();
            }
            else 
            {
                var serializationAction = SerializationProvider.Provide(dataItem.GetType());
                serializationAction(dataItem, writer, serializationModeKind, includeDerivedProperties);
                writer.Flush();
            }
        }

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
        public async Task SerializeAsync(IEnumerable<IIdentified> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            await using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            writer.WriteStartArray();

            foreach (var element in dataItems)
            {
                if (ApiSerializationProvider.IsTypeSupported(element.GetType()))
                {
                    var serializationAction = ApiSerializationProvider.Provide(element.GetType());
                    serializationAction(element, writer, serializationModeKind, includeDerivedProperties);
                    await writer.FlushAsync(cancellationToken);
                }
                else
                {
                    var serializationAction = SerializationProvider.Provide(element.GetType());
                    serializationAction(element, writer, serializationModeKind, includeDerivedProperties);
                    await writer.FlushAsync(cancellationToken);
                }
            }

            writer.WriteEndArray();

            await writer.FlushAsync(cancellationToken);
        }

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
        public async Task SerializeAsync(IIdentified dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            await using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            if (ApiSerializationProvider.IsTypeSupported(dataItem.GetType()))
            {
                var serializationAction = ApiSerializationProvider.Provide(dataItem.GetType());
                serializationAction(dataItem, writer, serializationModeKind, includeDerivedProperties);
                await writer.FlushAsync(cancellationToken);
            }
            else
            {
                var serializationAction = SerializationProvider.Provide(dataItem.GetType());
                serializationAction(dataItem, writer, serializationModeKind, includeDerivedProperties);
                await writer.FlushAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Serialize an <see cref="IRequest"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">The <see cref="IRequest"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        public void SerializeRequest(IRequest dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions)
        {
            using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            WriteSingle(dataItem, writer, serializationModeKind, includeDerivedProperties);
        }

        /// <summary>
        /// Serialize an <see cref="IEnumerable{IRequest}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">The <see cref="IEnumerable{IRequest}"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        public void SerializeRequest(IEnumerable<IRequest> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions)
        {
            using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            WriteMany(dataItems, writer, serializationModeKind, includeDerivedProperties);
        }

        /// <summary>
        /// Serialize an <see cref="IResponse"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">The <see cref="IResponse"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        public void SerializeResponse(IResponse dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions)
        {
            using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            WriteSingle(dataItem, writer, serializationModeKind, includeDerivedProperties);
        }

        /// <summary>
        /// Serialize an <see cref="IEnumerable{IResponse}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">The <see cref="IEnumerable{IResponse}"/> that shall be serialized.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        /// <param name="stream">The target <see cref="Stream"/>.</param>
        /// <param name="jsonWriterOptions">The <see cref="JsonWriterOptions"/> to use.</param>
        public void SerializeResponse(IEnumerable<IResponse> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions)
        {
            using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            WriteMany(dataItems, writer, serializationModeKind, includeDerivedProperties);
        }

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
        public async Task SerializeRequestAsync(IRequest dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            await using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            WriteSingle(dataItem, writer, serializationModeKind, includeDerivedProperties);

            await writer.FlushAsync(cancellationToken);
        }

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
        public async Task SerializeRequestAsync(IEnumerable<IRequest> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            await using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            WriteMany(dataItems, writer, serializationModeKind, includeDerivedProperties);

            await writer.FlushAsync(cancellationToken);
        }

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
        public async Task SerializeResponseAsync(IResponse dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            await using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            WriteSingle(dataItem, writer, serializationModeKind, includeDerivedProperties);

            await writer.FlushAsync(cancellationToken);
        }

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
        public async Task SerializeResponseAsync(IEnumerable<IResponse> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            await using var writer = new Utf8JsonWriter(stream, jsonWriterOptions);

            WriteMany(dataItems, writer, serializationModeKind, includeDerivedProperties);

            await writer.FlushAsync(cancellationToken);
        }

        /// <summary>
        /// Writes a single Systems Modeling API and Services data transfer object
        /// </summary>
        /// <param name="dataItem">The object that shall be written.</param>
        /// <param name="writer">The target <see cref="Utf8JsonWriter"/>.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        private static void WriteSingle(object dataItem, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            var serializationAction = PsmSerializationProvider.Provide(dataItem.GetType());

            serializationAction(dataItem, writer, serializationModeKind, includeDerivedProperties);

            writer.Flush();
        }

        /// <summary>
        /// Writes a collection of Systems Modeling API and Services data transfer objects as a JSON array
        /// </summary>
        /// <param name="dataItems">The objects that shall be written.</param>
        /// <param name="writer">The target <see cref="Utf8JsonWriter"/>.</param>
        /// <param name="serializationModeKind">The <see cref="SerializationModeKind"/> to use.</param>
        /// <param name="includeDerivedProperties">Asserts that derived properties should also be part of the serialization.</param>
        private static void WriteMany(IEnumerable<object> dataItems, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            writer.WriteStartArray();

            foreach (var dataItem in dataItems)
            {
                var serializationAction = PsmSerializationProvider.Provide(dataItem.GetType());

                serializationAction(dataItem, writer, serializationModeKind, includeDerivedProperties);
            }

            writer.WriteEndArray();

            writer.Flush();
        }
    }
}
