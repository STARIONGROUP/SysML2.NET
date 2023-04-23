// -------------------------------------------------------------------------------------------------
// <copyright file="ItemFeatureSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

namespace SysML2.NET.Core.DTO.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ItemFeatureSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IItemFeature"/> interface
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

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("ItemFeature"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iItemFeature.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iItemFeature.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iItemFeature.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iItemFeature.DeclaredShortName);
            writer.WritePropertyName("direction"u8);
            if (iItemFeature.Direction.HasValue)
            {
                writer.WriteStringValue(iItemFeature.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iItemFeature.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iItemFeature.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iItemFeature.IsComposite);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iItemFeature.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iItemFeature.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iItemFeature.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iItemFeature.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iItemFeature.IsPortion);

            writer.WritePropertyName("isReadOnly"u8);
            writer.WriteBooleanValue(iItemFeature.IsReadOnly);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iItemFeature.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iItemFeature.IsUnique);

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iItemFeature.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);
            if (iItemFeature.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iItemFeature.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
