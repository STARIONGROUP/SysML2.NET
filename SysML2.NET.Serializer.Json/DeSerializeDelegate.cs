// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializeDelegate.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System.Text.Json;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;

    /// <summary>
    /// Deserializes a single <see cref="IData"/> from the provided <see cref="Utf8JsonReader"/>.
    /// </summary>
    /// <param name="reader">
    /// The <see cref="Utf8JsonReader"/> positioned on the <see cref="JsonTokenType.StartObject"/> of the json
    /// object to deserialize. On return the reader is positioned on the matching
    /// <see cref="JsonTokenType.EndObject"/>.
    /// </param>
    /// <param name="serializationModeKind">
    /// The <see cref="SerializationModeKind"/> to use.
    /// </param>
    /// <param name="deserializeDerivedProperties">
    /// Asserts that the deserializer should deserialize derived properties if present or if they are ignored.
    /// </param>
    /// <param name="loggerFactory">
    /// The <see cref="ILoggerFactory"/> used to setup logging.
    /// </param>
    /// <returns>
    /// An instance of <see cref="IData"/>.
    /// </returns>
    /// <remarks>
    /// A named delegate is required rather than a <see cref="System.Func{T1,T2,T3,T4,TResult}"/> because
    /// <see cref="System.Func{T1,T2,T3,T4,TResult}"/> cannot express a <see langword="ref"/> parameter, and the
    /// reader has to be passed by reference so that the caller observes the advanced position.
    /// </remarks>
    internal delegate IData DeSerializeDelegate(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory);
}
