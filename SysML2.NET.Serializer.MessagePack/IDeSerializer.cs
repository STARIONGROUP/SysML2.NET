// ------------------------------------------------------------------------------------------------
// <copyright file="IDeSerializer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Common;
    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// The purpose of the <see cref="IDeSerializer"/> is to deserialize a MessagePack <see cref="Stream"/> to
    /// an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// </summary>
    public interface IDeSerializer
    {
        /// <summary>
        /// Deserializes the MessagePack stream to an <see cref="IEnumerable{IRequest}"/>
        /// </summary>
        /// <param name="stream">the MessagePack input stream</param>
        /// <returns>an <see cref="IEnumerable{IRequest}"/></returns>
        IEnumerable<IRequest> DeSerializeRequest(Stream stream);

        /// <summary>
        /// Deserializes the MessagePack stream to an <see cref="IEnumerable{IResponse}"/>
        /// </summary>
        /// <param name="stream">the MessagePack input stream</param>
        /// <returns>an <see cref="IEnumerable{IResponse}"/></returns>
        IEnumerable<IResponse> DeSerializeResponse(Stream stream);

        /// <summary>
        /// Asynchronously deserializes the MessagePack stream to an <see cref="IEnumerable{IRequest}"/>
        /// </summary>
        /// <param name="stream">the MessagePack input stream</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an <see cref="IEnumerable{IRequest}"/></returns>
        Task<IEnumerable<IRequest>> DeSerializeRequestAsync(Stream stream, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously deserializes the MessagePack stream to an <see cref="IEnumerable{IResponse}"/>
        /// </summary>
        /// <param name="stream">the MessagePack input stream</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation</param>
        /// <returns>an <see cref="IEnumerable{IResponse}"/></returns>
        Task<IEnumerable<IResponse>> DeSerializeResponseAsync(Stream stream, CancellationToken cancellationToken);

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
        IEnumerable<IData> DeSerialize(Stream stream, SerializationTargetKind serializationTargetKind);

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
        Task<IEnumerable<IData>> DeSerializeAsync(Stream stream, SerializationTargetKind serializationTargetKind, CancellationToken cancellationToken);
    }
}
