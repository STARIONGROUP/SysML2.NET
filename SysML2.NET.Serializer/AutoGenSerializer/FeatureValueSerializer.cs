// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureValueSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="FeatureValueSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FeatureValueSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFeatureValue"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFeatureValue"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFeatureValue iFeatureValue))
            {
                throw new ArgumentException("The object shall be an IFeatureValue", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFeatureValue.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("FeatureValue");

            writer.WritePropertyName("featureWithValue");
            writer.WriteStringValue(iFeatureValue.FeatureWithValue);

            writer.WritePropertyName("isDefault");
            writer.WriteBooleanValue(iFeatureValue.IsDefault);

            writer.WritePropertyName("isInitial");
            writer.WriteBooleanValue(iFeatureValue.IsInitial);

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iFeatureValue.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iFeatureValue.MemberName);

            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iFeatureValue.MemberShortName);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iFeatureValue.Visibility.ToString().ToUpper());

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iFeatureValue.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iFeatureValue.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iFeatureValue.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iFeatureValue.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iFeatureValue.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFeatureValue.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFeatureValue.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iFeatureValue.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFeatureValue.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iFeatureValue.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFeatureValue.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iFeatureValue.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
