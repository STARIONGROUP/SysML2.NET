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

namespace SysML2.NET.Serializer.MessagePack
{
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    
    using SysML2.NET.Common;
    using SysML2.NET.Serializer.MessagePack.Core;

    /// <summary>
    /// The purpose of the <see cref="ISerializer"/> is to write an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// as MessagePack to a <see cref="Stream"/>
    /// </summary>
    public class Serializer : SerializerBase, ISerializer
    {
        /// <summary>
        /// The (injected) logger
        /// </summary>
        private readonly ILogger<Serializer> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Serializer"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        /// The (injected) <see cref="ILoggerFactory"/> used to set up logging
        /// </param>
        public Serializer(ILoggerFactory loggerFactory = null)
        {
            this.logger = loggerFactory == null ? NullLogger<Serializer>.Instance : loggerFactory.CreateLogger<Serializer>();
        }

        /// <summary>
        /// Serialize an <see cref="IEnumerable{IData}"/> as MessagePack to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        public void Serialize(IEnumerable<IData> dataItems, Stream stream)
        {
            if (dataItems == null)
            {
                throw new ArgumentNullException(nameof(dataItems));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var options = CreateSerializerOptions();

            var payload = dataItems.ToList().AsReadOnly().ToPayload();

            var sw = Stopwatch.StartNew();

            global::MessagePack.MessagePackSerializer.Serialize(stream, payload, options);

            sw.Stop();

            this.logger.LogDebug("SerializeToStream finished in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// Serialize an <see cref="IData"/> as MessagePack to a target <see cref="IBufferWriter{Byte}"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> that shall be serialized
        /// </param>
        /// <param name="writer">
        /// The target <see cref="IBufferWriter{Byte}"/>.
        /// </param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/>
        /// </param>
        public void SerializeToBufferWriter(IEnumerable<IData> dataItems, IBufferWriter<byte> writer, CancellationToken cancellationToken = default)
        {
            if (dataItems == null)
            {
                throw new ArgumentNullException(nameof(dataItems));
            }

            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            var options = CreateSerializerOptions();

            var payload = dataItems.ToList().AsReadOnly().ToPayload();

            var sw = Stopwatch.StartNew();

            global::MessagePack.MessagePackSerializer.Serialize(writer, payload, options, cancellationToken);

            sw.Stop();

            this.logger.LogDebug("SerializeToBufferWriter finished in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// Asynchronously serialize an <see cref="IEnumerable{IData}"/> as MessagePack to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        public Task SerializeAsync(IEnumerable<IData> dataItems, Stream stream, CancellationToken cancellationToken)
        {
            if (dataItems == null)
            {
                throw new ArgumentNullException(nameof(dataItems));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return this.SerializeInternalAsync(dataItems, stream, cancellationToken);
        }

        /// <summary>
        /// Asynchronously serialize an <see cref="IEnumerable{IData}"/> as MessagePack to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        private async Task SerializeInternalAsync(IEnumerable<IData> dataItems, Stream stream, CancellationToken cancellationToken)
        {
            var options = CreateSerializerOptions();

            var payload = dataItems.ToList().AsReadOnly().ToPayload();

            var sw = Stopwatch.StartNew();

            await global::MessagePack.MessagePackSerializer.SerializeAsync(stream, payload, options, cancellationToken);

            sw.Stop();

            this.logger.LogDebug("SerializeInternalAsync finished in {ElapsedMilliseconds} [ms]", sw.ElapsedMilliseconds);
        }
    }
}
