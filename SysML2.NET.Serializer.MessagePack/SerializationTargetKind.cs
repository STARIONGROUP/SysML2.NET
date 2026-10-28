// ------------------------------------------------------------------------------------------------
// <copyright file="SerializationTargetKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack
{
    using System;

    /// <summary>
    /// an enumeration that denotes what kind of serialization target shall be used
    /// </summary>
    [Flags]
    public enum SerializationTargetKind
    {
        /// <summary>
        /// (de)serialization to and from SysML Core concepts
        /// </summary>
        CORE = 0,

        /// <summary>
        /// (de)serialization to and from SysML Core concepts as well as REST API concepts
        /// </summary>
        PSM = 1,
    }
}