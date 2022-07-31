// -------------------------------------------------------------------------------------------------
// <copyright file="Serializer.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.DTO;
    using SysML2.NET.Serializer.Json.AutoGenSerializer;

    /// <summary>
    /// The purpose of the <see cref="Serializer"/> is to write an <see cref="IEnumerable{T}"/> of
    /// <see cref="Element"/> as JSON to a <see cref="Stream"/>
    /// </summary>
    public class Serializer : ISerializer
    {
        public void Serialize(IEnumerable<IElement> elements, SerializationModeKind serializationModeKind, Stream stream, JsonWriterOptions jsonWriterOptions)
        {
            using (var writer = new Utf8JsonWriter(stream, jsonWriterOptions))
            {
                writer.WriteStartArray();

                foreach (var element in elements)
                {
                    var serializationAction = SerializationProvider.Provide(element.GetType());
                    serializationAction(element, writer, serializationModeKind);
                    writer.Flush();
                }

                writer.WriteEndArray();

                writer.Flush();
            }
        }

        public void Serialize(IElement element, SerializationModeKind serializationModeKind, Stream stream, JsonWriterOptions jsonWriterOptions)
        {
            throw new NotImplementedException();

            //using (var writer = new Utf8JsonWriter(stream, jsonWriterOptions))
            //{
            //    element.Serialize(writer, serializationModeKind);
            //    writer.Flush();
            //}
        }

        public async Task SerializeAsync(IEnumerable<IElement> elements, SerializationModeKind serializationModeKind, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //using (var writer = new Utf8JsonWriter(stream, jsonWriterOptions))
            //{
            //    writer.WriteStartArray();

            //    foreach (var element in elements)
            //    {
            //        element.Serialize(writer, serializationModeKind);
            //        await writer.FlushAsync(cancellationToken);
            //    }

            //    writer.WriteEndArray();

            //    await writer.FlushAsync(cancellationToken);
            //}
        }

        public async Task SerializeAsync(IElement element, SerializationModeKind serializationModeKind, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            //using (var writer = new Utf8JsonWriter(stream, jsonWriterOptions))
            //{
            //    element.Serialize(writer, serializationModeKind);
            //    await writer.FlushAsync(cancellationToken);
            //}
        }
    }
}