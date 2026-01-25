// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Serializer.MessagePack.Core;

    /// <summary>
    /// The purpose of the <see cref="IDeSerializer"/> is to deserialize a MessagePack <see cref="Stream"/> to
    /// an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// </summary>
    public class DeSerializer : SerializerBase, IDeSerializer
    {
        /// <summary>
        /// The (injected) logger
        /// </summary>
        private readonly ILogger<DeSerializer> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeSerializer"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        /// The (injected) <see cref="ILoggerFactory"/> used to set up logging
        /// </param>
        public DeSerializer(ILoggerFactory loggerFactory = null)
        {
            this.logger = loggerFactory == null ? NullLogger<DeSerializer>.Instance : loggerFactory.CreateLogger<DeSerializer>();
        }

        /// <summary>
        /// Deserializes the JSON stream to an <see cref="IEnumerable{IData}"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
        /// </param>
        /// <param name="serializationTargetKind">
        /// The <see cref="SerializationTargetKind"/> to use
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>
        /// </returns>
        public IEnumerable<IData> DeSerialize(Stream stream, SerializationTargetKind serializationTargetKind)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "Stream may not be null");
            }

            var options = this.CreateSerializerOptions();

            var sw = Stopwatch.StartNew();

            var payload = global::MessagePack.MessagePackSerializer.Deserialize<Payload>(stream, options);

            sw.Stop();

            this.logger.LogDebug("DeserializeAsync finished in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);

            var result = payload.ToDataItems();

            return result;
        }

        /// <summary>
        /// Asynchronously deserializes the JSON stream to an <see cref="IEnumerable{IData}"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
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
        public Task<IEnumerable<IData>> DeSerializeAsync(Stream stream, SerializationTargetKind serializationTargetKind, CancellationToken cancellationToken)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "Stream may not be null");
            }

            return this.DeserializeInternalAsync(stream, serializationTargetKind, cancellationToken);
        }

        /// <summary>
        /// Asynchronously deserializes the JSON stream to an <see cref="IEnumerable{IData}"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
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
        /// <remarks>
        /// This method is used to split asynchronous code from parameter checking.
        /// The <see cref="DeSerializeAsync"/> method is exposed and shall be used.
        /// </remarks>
        public async Task<IEnumerable<IData>> DeserializeInternalAsync(Stream stream, SerializationTargetKind serializationTargetKind, CancellationToken cancellationToken)
        {
            var options = this.CreateSerializerOptions();

            var sw = Stopwatch.StartNew();

            var payload = await global::MessagePack.MessagePackSerializer.DeserializeAsync<Payload>(stream, options, cancellationToken);

            sw.Stop();

            this.logger.LogDebug("DeserializeAsync finished in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);

            var result = payload.ToDataItems();

            return result;
        }
    }
}
