// -------------------------------------------------------------------------------------------------
// <copyright file="ErrorSerializer.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Json.PSM
{
    using System;
    using System.Text.Json;

    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ErrorSerializer"/> is to provide serialization capabilities
    /// for the <see cref="Error"/> class
    /// </summary>
    internal static class ErrorSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="Error"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="Error"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when the object is not a <see cref="Error"/>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when the serialization mode is not supported
        /// </exception>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            if (obj is not Error error)
            {
                throw new ArgumentException("The object shall be a Error", nameof(obj));
            }

            if (serializationModeKind != SerializationModeKind.JSON)
            {
                throw new NotSupportedException($"The {serializationModeKind} serialization mode is not supported");
            }

            writer.WriteStartObject();

            writer.WriteString("@type"u8, "Error");

            writer.WriteString("description"u8, error.Description);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
