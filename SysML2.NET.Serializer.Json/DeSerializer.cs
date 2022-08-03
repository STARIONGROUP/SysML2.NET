﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializer.cs" company="RHEA System S.A.">
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

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
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>
        /// </returns>
        public IEnumerable<IData> DeSerialize(Stream stream, SerializationModeKind serializationModeKind)
        {
            var sw = Stopwatch.StartNew();

            var result = new List<IData>();

            using (var document = JsonDocument.Parse(stream))
            {
                var root = document.RootElement;

                foreach (var jsonElement in root.EnumerateArray())
                {
                    if (jsonElement.TryGetProperty("@type", out var typeElement))
                    {
                        var typeName = typeElement.GetString();
                        
                        var func = DeSerializationProvider.Provide(typeName);
                        var dataItem = func(jsonElement, serializationModeKind, this.loggerFactory);
                        result.Add(dataItem);
                    }
                }
            }

            this.logger.LogInformation($"stream deserialized in {sw.ElapsedMilliseconds} [ms]");

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
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>
        /// </returns>
        public async Task<IEnumerable<IData>> DeSerializeAsync(Stream stream, SerializationModeKind serializationModeKind, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            var result = new List<IData>();

            var jsonDocumentOptions = default(JsonDocumentOptions);

            using (var document = await JsonDocument.ParseAsync(stream, jsonDocumentOptions, cancellationToken))
            {
                var root = document.RootElement;

                foreach (var jsonElement in root.EnumerateArray())
                {
                    if (jsonElement.TryGetProperty("@type", out var typeElement))
                    {
                        var typeName = typeElement.GetString();

                        var func = DeSerializationProvider.Provide(typeName);
                        var dataItem = func(jsonElement, serializationModeKind, this.loggerFactory);
                        result.Add(dataItem);
                    }
                }
            }

            this.logger.LogInformation($"stream deserialized in {sw.ElapsedMilliseconds} [ms]");

            return result;
        }
    }
}
