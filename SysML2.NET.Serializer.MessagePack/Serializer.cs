// -------------------------------------------------------------------------------------------------
// <copyright file="Serializer.cs" company="Starion Group S.A.">
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
    using global::MessagePack;
    using global::MessagePack.Resolvers;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using SysML2.NET.Common;
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The purpose of the <see cref="ISerializer"/> is to write an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// as MessagePack to a <see cref="Stream"/>
    /// </summary>
    public class Serializer : ISerializer
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
        public Serializer(ILoggerFactory loggerFactory)
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

            throw new System.NotImplementedException();
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

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Serialize an <see cref="IData"/> as MessagePack to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IData"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        public void Serialize(IData dataItem, Stream stream)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException(nameof(dataItem));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            throw new System.NotImplementedException();
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
            var options = this.CreateSerializerOptions();

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Asynchronously serialize an <see cref="IData"/> as MessagePack to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IData"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        public Task SerializeAsync(IData dataItem, Stream stream, CancellationToken cancellationToken)
        {
            if (dataItem == null)
            {
                throw new ArgumentNullException(nameof(dataItem));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates an instance of the <see cref="MessagePackSerializerOptions"/> 
        /// </summary>
        /// <returns>
        /// An instance of <see cref="MessagePackSerializerOptions"/>
        /// </returns>
        private MessagePackSerializerOptions CreateSerializerOptions()
        {
            // TODO: implement

            //var formatterResolver = CompositeResolver.Create(
            //    ThingFormatterResolver.Instance,
            //    StandardResolver.Instance);

            var options = MessagePackSerializerOptions.Standard
                //.WithResolver(formatterResolver)
                .WithOldSpec(false);

            return options;
        }
    }
}
