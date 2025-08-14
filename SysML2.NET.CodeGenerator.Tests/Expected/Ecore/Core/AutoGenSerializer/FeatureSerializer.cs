// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureSerializer.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="FeatureSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IFeature"/> interface
    /// </summary>
    internal static class FeatureSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFeature"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFeature"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFeature iFeature))
            {
                throw new ArgumentException("The object shall be an IFeature", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("Feature"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iFeature.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iFeature.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iFeature.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iFeature.DeclaredShortName);
            writer.WritePropertyName("direction"u8);
            if (iFeature.Direction.HasValue)
            {
                writer.WriteStringValue(iFeature.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iFeature.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iFeature.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iFeature.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iFeature.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iFeature.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iFeature.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iFeature.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iFeature.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iFeature.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iFeature.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iFeature.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iFeature.IsVariable);

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iFeature.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);
            if (iFeature.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFeature.OwningRelationship.Value);
                writer.WriteEndObject();
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
