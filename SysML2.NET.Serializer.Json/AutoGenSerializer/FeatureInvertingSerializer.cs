// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureInvertingSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="FeatureInvertingSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FeatureInvertingSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFeatureInverting"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFeatureInverting"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFeatureInverting iFeatureInverting))
            {
                throw new ArgumentException("The object shall be an IFeatureInverting", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFeatureInverting.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("FeatureInverting");

            writer.WritePropertyName("featureInverted");
            writer.WriteStringValue(iFeatureInverting.FeatureInverted);

            writer.WritePropertyName("invertingFeature");
            writer.WriteStringValue(iFeatureInverting.InvertingFeature);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iFeatureInverting.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iFeatureInverting.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iFeatureInverting.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iFeatureInverting.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iFeatureInverting.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFeatureInverting.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFeatureInverting.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iFeatureInverting.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFeatureInverting.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iFeatureInverting.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFeatureInverting.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iFeatureInverting.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
