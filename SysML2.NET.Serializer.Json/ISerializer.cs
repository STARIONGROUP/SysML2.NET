// -------------------------------------------------------------------------------------------------
// <copyright file="ISerializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Common;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ISerializer"/> is to write an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// as JSON to a <see cref="Stream"/>
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize an <see cref="IEnumerable{IData}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> that shall be serialized
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="jsonWriterOptions">
        /// The <see cref="JsonWriterOptions"/> to use
        /// </param>
        void Serialize(IEnumerable<IData> dataItems, SerializationModeKind serializationModeKind, Stream stream, JsonWriterOptions jsonWriterOptions);

        /// <summary>
        /// Serialize an <see cref="IData"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IData"/> that shall be serialized
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="jsonWriterOptions">
        /// The <see cref="JsonWriterOptions"/> to use
        /// </param>
        void Serialize(IData dataItem, SerializationModeKind serializationModeKind, Stream stream, JsonWriterOptions jsonWriterOptions);

        /// <summary>
        /// Asynchronously serialize an <see cref="IEnumerable{IData}"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> that shall be serialized
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
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
        Task SerializeAsync(IEnumerable<IData> dataItems, SerializationModeKind serializationModeKind, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously serialize an <see cref="IData"/> as JSON to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IData"/> that shall be serialized
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
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
        Task SerializeAsync(IData dataItem, SerializationModeKind serializationModeKind, Stream stream, JsonWriterOptions jsonWriterOptions, CancellationToken cancellationToken);
    }
}
