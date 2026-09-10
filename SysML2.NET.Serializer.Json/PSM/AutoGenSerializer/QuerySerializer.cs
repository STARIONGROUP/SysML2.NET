// -------------------------------------------------------------------------------------------------
// <copyright file="QuerySerializer.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="QuerySerializer"/> is to provide serialization capabilities
    /// for the <see cref="Query"/> class
    /// </summary>
    internal static class QuerySerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="Query"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="Query"/> to serialize
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
        /// Thrown when the object is not a <see cref="Query"/>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when the serialization mode is not supported
        /// </exception>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            if (obj is not Query query)
            {
                throw new ArgumentException("The object shall be a Query", nameof(obj));
            }

            if (serializationModeKind != SerializationModeKind.JSON)
            {
                throw new NotSupportedException($"The {serializationModeKind} serialization mode is not supported");
            }

            writer.WriteStartObject();

            writer.WriteString("@type"u8, "Query");

            writer.WriteString("@id"u8, query.Id);

            writer.WriteStartArray("alias"u8);

            if (query.Alias != null)
            {
                foreach (var item in query.Alias)
                {
                    writer.WriteStringValue(item);
                }
            }

            writer.WriteEndArray();

            writer.WriteString("description"u8, query.Description);

            writer.WriteString("name"u8, query.Name);

            writer.WriteStartObject("owningProject"u8);
            writer.WriteString("@id"u8, query.OwningProject);
            writer.WriteEndObject();

            writer.WriteStartArray("select"u8);

            if (query.Select != null)
            {
                foreach (var item in query.Select)
                {
                    writer.WriteStringValue(item);
                }
            }

            writer.WriteEndArray();

            if (query.Where != null)
            {
                writer.WritePropertyName("where"u8);
                PsmSerializationDispatcher.Provide(query.Where.GetType())(query.Where, writer, serializationModeKind, includeDerivedProperties);
            }
            else
            {
                writer.WriteNull("where"u8);
            }

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
