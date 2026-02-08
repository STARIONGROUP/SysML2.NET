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
    using SysML2.NET.Serializer.Json.PIM.DTO;
    using SysML2.NET.Serializer.Json.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="Serializer"/> is to write an <see cref="IElement"/> and <see cref="IEnumerable{IElement}"/>
    /// as JSON to a <see cref="Stream"/>
    /// </summary>
    public class Serializer : ISerializer
    {
        /// <summary>
        /// Serialize an <see cref="IEnumerable{IData}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IElIDataement}"/> that shall be serialized
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
        public void Serialize(IEnumerable<IData> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions)
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
        /// Serialize an <see cref="IData"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IData"/> that shall be serialized
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
        public void Serialize(IData dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions)
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
        /// Asynchronously serialize an <see cref="IEnumerable{IData}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> that shall be serialized
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
        public async Task SerializeAsync(IEnumerable<IData> dataItems, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
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
        /// Asynchronously serialize an <see cref="IData"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IData"/> that shall be serialized
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
        public async Task SerializeAsync(IData dataItem, SerializationModeKind serializationModeKind, bool includeDerivedProperties, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
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
    }
}
