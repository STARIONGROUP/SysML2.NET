// -------------------------------------------------------------------------------------------------
// <copyright file="DataTypeSerializer.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer
{
    using System.Text.Json;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="DataTypeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class DataTypeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IDataType"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="dataType">
        /// The <see cref="IDataType"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IDataType iDataType, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iDataType.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("DataType");

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iDataType.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iDataType.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iDataType.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iDataType.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iDataType.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iDataType.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iDataType.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iDataType.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iDataType.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
