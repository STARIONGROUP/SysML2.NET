// -------------------------------------------------------------------------------------------------
// <copyright file="TypeFeaturingSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="TypeFeaturingSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class TypeFeaturingSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ITypeFeaturing"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ITypeFeaturing"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ITypeFeaturing iTypeFeaturing))
            {
                throw new ArgumentException("The object shall be an ITypeFeaturing", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("TypeFeaturing");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iTypeFeaturing.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iTypeFeaturing.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iTypeFeaturing.ElementId);

            writer.WritePropertyName("featureOfType");
            writer.WriteStringValue(iTypeFeaturing.FeatureOfType);

            writer.WritePropertyName("featuringType");
            writer.WriteStringValue(iTypeFeaturing.FeaturingType);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iTypeFeaturing.Name);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iTypeFeaturing.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iTypeFeaturing.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iTypeFeaturing.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iTypeFeaturing.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship");
            if (iTypeFeaturing.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iTypeFeaturing.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iTypeFeaturing.ShortName);

            writer.WriteStartArray("source");
            foreach (var item in iTypeFeaturing.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iTypeFeaturing.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
