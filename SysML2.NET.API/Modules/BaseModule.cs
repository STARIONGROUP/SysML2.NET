// -------------------------------------------------------------------------------------------------
// <copyright file="BaseModule.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.API.Modules
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Threading;
    
    using SysML2.NET.Common;
    using SysML2.NET.Serializer.Json;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Abstract super class from which all Modules need to derive
    /// </summary>
    public class BaseModule
    {
        /// <summary>
        /// The (injected) <see cref="ILogger"/>
        /// </summary>
        private readonly ILogger<BaseModule> logger;

        /// <summary>
        /// The (injected) <see cref="ISerializer"/>
        /// </summary>
        protected readonly ISerializer serializer;

        /// <summary>
        /// The (injected) <see cref="ISerializer"/>
        /// </summary>
        protected readonly IDeSerializer deserializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModule"/>
        /// </summary>
        /// <param name="logger">
        /// The (injected) <see cref="ILogger{BaseModule}"/>
        /// </param>
        /// <param name="serializer">
        /// The (injected) <see cref="ISerializer"/>
        /// </param>
        /// <param name="deserializer">
        /// The (injected) <see cref="IDeSerializer"/>
        /// </param>
        protected BaseModule(ILogger<BaseModule> logger, ISerializer serializer, IDeSerializer deserializer)
        {
            this.logger = logger;
            this.serializer = serializer;
            this.deserializer = deserializer;
        }

        /// <summary>
        /// Writes the <see cref="IEnumerable{Thing}"/> to the <see cref="HttpResponse.Body"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> to write to <see cref="HttpResponse"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="response">
        /// The <see cref="HttpResponse"/> to which the <see cref="IEnumerable{Thing}"/> are written
        /// </param>
        /// <param name="jsonWriterOptions">
        /// The <see cref="JsonWriterOptions"/> used for serialization
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that can be used to cancel the operation
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        protected async Task WriteResultsToResponse(IEnumerable<IData> dataItems, SerializationModeKind serializationModeKind, HttpResponse response, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            var dtoTypeName = this.GetType().Name.Replace("Module", string.Empty);
            
            response.ContentType = "application/json";
            this.logger.LogDebug("start serializing {dtoTypeName} objects to result Stream", dtoTypeName);

            var resultStream = new MemoryStream();
            await this.serializer.SerializeAsync(dataItems, serializationModeKind, resultStream, jsonWriterOptions, cancellationToken);

            this.logger.LogDebug("{dtoTypeName} objects serialized to stream in {elapsed} [ms]", dtoTypeName, sw.ElapsedMilliseconds);

            resultStream.Seek(0, SeekOrigin.Begin);

            await resultStream.CopyToAsync(response.Body, cancellationToken);
        }

        /// <summary>
        /// Writes the <see cref="IEnumerable{IData}"/> to the <see cref="HttpResponse.Body"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IEnumerable{IData}"/> to write to <see cref="HttpResponse"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="response">
        /// The <see cref="HttpResponse"/> to which the <see cref="IEnumerable{Thing}"/> are written
        /// </param>
        /// <param name="jsonWriterOptions">
        /// The <see cref="JsonWriterOptions"/> used for serialization
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that can be used to cancel the operation
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        protected async Task WriteResultToResponse(IData dataItem, SerializationModeKind serializationModeKind, HttpResponse response, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            var dtoTypeName = this.GetType().Name.Replace("Module", string.Empty);

            response.ContentType = "application/json";
            this.logger.LogDebug("start serializing {dtoTypeName} objects to result Stream", dtoTypeName);

            var resultStream = new MemoryStream();
            await this.serializer.SerializeAsync(dataItem, serializationModeKind, resultStream, jsonWriterOptions, cancellationToken);

            this.logger.LogDebug("{dtoTypeName} object serialized to stream in {elapsed} [ms]", dtoTypeName, sw.ElapsedMilliseconds);

            resultStream.Seek(0, SeekOrigin.Begin);

            await resultStream.CopyToAsync(response.Body, cancellationToken);
        }
    }
}
