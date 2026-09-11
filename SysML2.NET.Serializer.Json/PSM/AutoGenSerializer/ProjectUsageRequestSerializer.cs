// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectUsageRequestSerializer.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Extensions.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ProjectUsageRequestSerializer"/> is to provide serialization capabilities
    /// for the <see cref="ProjectUsageRequest"/> class
    /// </summary>
    internal static class ProjectUsageRequestSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ProjectUsageRequest"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ProjectUsageRequest"/> to serialize
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
        /// Thrown when the object is not a <see cref="ProjectUsageRequest"/>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when the serialization mode is not supported
        /// </exception>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            if (obj is not ProjectUsageRequest projectUsageRequest)
            {
                throw new ArgumentException("The object shall be a ProjectUsageRequest", nameof(obj));
            }

            if (serializationModeKind != SerializationModeKind.JSON)
            {
                throw new NotSupportedException($"The {serializationModeKind} serialization mode is not supported");
            }

            writer.WriteStartObject();

            writer.WriteString("@type"u8, "ProjectUsage");

            if (projectUsageRequest.Id.HasValue)
            {
                writer.WriteString("@id"u8, projectUsageRequest.Id.Value);
            }
            else
            {
                writer.WriteNull("@id"u8);
            }

            if (projectUsageRequest.UsedCommit.HasValue)
            {
                writer.WriteStartObject("usedCommit"u8);
                writer.WriteString("@id"u8, projectUsageRequest.UsedCommit.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNull("usedCommit"u8);
            }

            if (projectUsageRequest.UsedProject.HasValue)
            {
                writer.WriteStartObject("usedProject"u8);
                writer.WriteString("@id"u8, projectUsageRequest.UsedProject.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNull("usedProject"u8);
            }

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
