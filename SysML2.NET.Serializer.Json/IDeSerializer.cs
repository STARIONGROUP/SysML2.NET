// -------------------------------------------------------------------------------------------------
// <copyright file="IDeSerializer.cs" company="RHEA System S.A.">
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
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="IDeSerializer"/> is to deserialize a JSON <see cref="Stream"/> to
    /// an <see cref="IElement"/> and <see cref="IEnumerable{IElement}"/>
    /// </summary>
    public interface IDeSerializer
    {
        /// <summary>
        /// Deserializes the JSON stream to an <see cref="IEnumerable{IElement}"/>
        /// </summary>
        /// <param name="stream">
        /// the JSON input stream
        /// </param>
        /// <param name="serializationModeKind">
        /// The <see cref="SerializationModeKind"/> to use
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{IElement}"/>
        /// </returns>
        IEnumerable<IElement> DeSerialize(Stream stream, SerializationModeKind serializationModeKind);

        /// <summary>
        /// Asynchronously deserializes the JSON stream to an <see cref="IEnumerable{IElement}"/>
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
        /// an <see cref="IEnumerable{IElement}"/>
        /// </returns>
        Task<IEnumerable<IElement>> DeSerializeAsync(Stream stream, SerializationModeKind serializationModeKind, CancellationToken cancellationToken);
    }
}
