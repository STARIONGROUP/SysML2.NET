// -------------------------------------------------------------------------------------------------
// <copyright file="LifeClassSerializer.cs" company="RHEA System S.A.">
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

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="LifeClassSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class LifeClassSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILifeClass"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ILifeClass"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ILifeClass iLifeClass))
            {
                throw new ArgumentException("The object shall be an ILifeClass", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("LifeClass");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iLifeClass.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iLifeClass.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iLifeClass.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iLifeClass.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iLifeClass.IsSufficient);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iLifeClass.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iLifeClass.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iLifeClass.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iLifeClass.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iLifeClass.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
