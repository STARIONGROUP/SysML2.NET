// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataFeatureSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MetadataFeatureSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IMetadataFeature"/> interface
    /// </summary>
    internal static class MetadataFeatureSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMetadataFeature"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMetadataFeature"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IMetadataFeature iMetadataFeature))
            {
                throw new ArgumentException("The object shall be an IMetadataFeature", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("MetadataFeature"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iMetadataFeature.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iMetadataFeature.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("annotation"u8);
            foreach (var item in iMetadataFeature.Annotation)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iMetadataFeature.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iMetadataFeature.DeclaredShortName);
            writer.WritePropertyName("direction"u8);
            if (iMetadataFeature.Direction.HasValue)
            {
                writer.WriteStringValue(iMetadataFeature.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iMetadataFeature.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsComposite);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsPortion);

            writer.WritePropertyName("isReadOnly"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsReadOnly);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iMetadataFeature.IsUnique);

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iMetadataFeature.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);
            if (iMetadataFeature.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMetadataFeature.OwningRelationship.Value);
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
