// -------------------------------------------------------------------------------------------------
// <copyright file="ItemFeatureSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ItemFeatureSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ItemFeatureSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IItemFeature"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IItemFeature"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IItemFeature iItemFeature))
            {
                throw new ArgumentException("The object shall be an IItemFeature", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iItemFeature.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ItemFeature");

            writer.WritePropertyName("direction");
            if (iItemFeature.Direction.HasValue)
            {
                writer.WriteStringValue(iItemFeature.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iItemFeature.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iItemFeature.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iItemFeature.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iItemFeature.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iItemFeature.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iItemFeature.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iItemFeature.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iItemFeature.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iItemFeature.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iItemFeature.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iItemFeature.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iItemFeature.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iItemFeature.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iItemFeature.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iItemFeature.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iItemFeature.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
