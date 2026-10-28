// ------------------------------------------------------------------------------------------------
// <copyright file="SerializationModeKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json
{
    /// <summary>
    /// an enumeration that denotes what kind of serialization shall be used
    /// </summary>
    public enum SerializationModeKind
    {
        /// <summary>
        /// Serialize to/from JSON
        /// </summary>
        JSON,

        /// <summary>
        /// Serialize to/from JSON-LD
        /// </summary>
        JSONLD
    }
}
