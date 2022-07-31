// -------------------------------------------------------------------------------------------------
// <copyright file="MetaclassSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="MetaclassSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MetaclassSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMetaclass"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMetaclass"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IMetaclass iMetaclass))
            {
                throw new ArgumentException("The object shall be an IMetaclass", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMetaclass.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Metaclass");

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iMetaclass.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iMetaclass.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMetaclass.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMetaclass.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iMetaclass.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMetaclass.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iMetaclass.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMetaclass.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iMetaclass.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
