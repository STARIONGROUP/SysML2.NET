// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializer.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2024 RHEA System S.A.
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
    public class DeSerializer : IDeSerializer
    {
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
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>
        /// </returns>
        public IEnumerable<IData> DeSerialize(Stream stream, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind)
        {
            var sw = Stopwatch.StartNew();

            var result = new List<IData>();

            using (var document = JsonDocument.Parse(stream))
            {
                var root = document.RootElement;

                switch (root.ValueKind)
                {
                    case JsonValueKind.Object:
                        result.Add(this.DeserializeObject(root, serializationModeKind, serializationTargetKind));
                        break;
                    case JsonValueKind.Array:
                        result.AddRange(this.DeserializeArray(root, serializationModeKind, serializationTargetKind));
                        break;
                    default:
                        throw new SerializationException();
                }
            }

            this.logger.LogInformation("stream deserialized in {ElapsedTime} [ms]", sw.ElapsedMilliseconds);

            return result;
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
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>
        /// </returns>
        public async Task<IEnumerable<IData>> DeSerializeAsync(Stream stream, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            var result = new List<IData>();

            var jsonDocumentOptions = default(JsonDocumentOptions);

            using (var document = await JsonDocument.ParseAsync(stream, jsonDocumentOptions, cancellationToken))
            {
                var root = document.RootElement;

                switch (root.ValueKind)
                {
                    case JsonValueKind.Object:
                        result.Add(this.DeserializeObject(root, serializationModeKind, serializationTargetKind));
                        break;
                    case JsonValueKind.Array:
                        result.AddRange(this.DeserializeArray(root, serializationModeKind, serializationTargetKind));
                        break;
                    default:
                        throw new SerializationException();
                }
            }

            this.logger.LogInformation($"stream deserialized asynchronously in {sw.ElapsedMilliseconds} [ms]");

            return result;
        }

        /// <summary>
        /// Deserializes an <see cref="JsonElement"/> of type <see cref="JsonValueKind.Object"/> to an <see cref="IData"/> object
        /// </summary>
        /// <param name="jsonObject">
        /// the subject <see cref="JsonElement"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="serializationTargetKind">
        /// The <see cref="SerializationTargetKind"/> to use
        /// </param>
        /// <returns>
        /// an instance of <see cref="IData"/>
        /// </returns>
        private IData DeserializeObject(JsonElement jsonObject, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind)
        {
            if (jsonObject.ValueKind != JsonValueKind.Object)
            {
                throw new ArgumentException($"The {nameof(jsonObject)} must be of type JsonValueKind.Object", nameof(jsonObject));
            }

            if (jsonObject.TryGetProperty("@type", out var typeElement))
            {
                var typeName = typeElement.GetString();

                Func<JsonElement, SerializationModeKind, ILoggerFactory, IData> func;

                if (serializationTargetKind == SerializationTargetKind.PSM)
                {
                    if (ApiDeSerializationProvider.IsTypeSupported(typeName))
                    {
                        func = ApiDeSerializationProvider.Provide(typeName);
                        return func(jsonObject, serializationModeKind, this.loggerFactory);
                    }
                    else
                    {
                        func = DeSerializationProvider.Provide(typeName);
                        return func(jsonObject, serializationModeKind, this.loggerFactory);
                    }
                }

                func = DeSerializationProvider.Provide(typeName);
                return func(jsonObject, serializationModeKind, this.loggerFactory);
            }

            throw new SerializationException("The @type Json property is not available, the DeSerializer cannot be used to deserialize this JsonElement");
        }

        /// <summary>
        /// Deserializes an <see cref="JsonElement"/> of type <see cref="JsonValueKind.Array"/> to an <see cref="IEnumerable{IData}"/> object
        /// </summary>
        /// <param name="jsonArray">
        /// the subject <see cref="JsonElement"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="serializationTargetKind">
        /// The <see cref="SerializationTargetKind"/> to use
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>
        /// </returns>
        private IEnumerable<IData> DeserializeArray(JsonElement jsonArray, SerializationModeKind serializationModeKind, SerializationTargetKind serializationTargetKind)
        {
            if (jsonArray.ValueKind != JsonValueKind.Array)
            {
                throw new ArgumentException($"The {nameof(jsonArray)} must be of type JsonValueKind.Array", nameof(jsonArray));
            }

            var result = new List<IData>();

            foreach (var jsonElement in jsonArray.EnumerateArray())
            {
                var dataItem = this.DeserializeObject(jsonElement, serializationModeKind, serializationTargetKind);
                result.Add(dataItem);
            }

            return result;
        }
    }
}
