// ------------------------------------------------------------------------------------------------
// <copyright file="DeSerializeResponseDelegate.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.PSM
{
    using System.Text.Json;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.PSM;
    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// Deserializes a single <see cref="IResponse"/> from the provided <see cref="Utf8JsonReader"/>.
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
    /// An instance of <see cref="IResponse"/>.
    /// </returns>
    /// <remarks>
    /// A named delegate is required rather than a <see cref="System.Func{T1,T2,T3,T4,TResult}"/> because
    /// <see cref="System.Func{T1,T2,T3,T4,TResult}"/> cannot express a <see langword="ref"/> parameter.
    /// </remarks>
    internal delegate IResponse DeSerializeResponseDelegate(ref Utf8JsonReader reader, SerializationModeKind serializationModeKind, bool deserializeDerivedProperties, ILoggerFactory loggerFactory);
}
