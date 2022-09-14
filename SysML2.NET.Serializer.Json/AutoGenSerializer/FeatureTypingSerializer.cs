// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureTypingSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="FeatureTypingSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FeatureTypingSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFeatureTyping"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFeatureTyping"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFeatureTyping iFeatureTyping))
            {
                throw new ArgumentException("The object shall be an IFeatureTyping", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("FeatureTyping");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFeatureTyping.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFeatureTyping.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFeatureTyping.ElementId);

            writer.WritePropertyName("general");
            writer.WriteStringValue(iFeatureTyping.General);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iFeatureTyping.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iFeatureTyping.IsImpliedIncluded);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iFeatureTyping.Name);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iFeatureTyping.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFeatureTyping.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iFeatureTyping.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iFeatureTyping.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship");
            if (iFeatureTyping.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFeatureTyping.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iFeatureTyping.ShortName);

            writer.WriteStartArray("source");
            foreach (var item in iFeatureTyping.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("specific");
            writer.WriteStringValue(iFeatureTyping.Specific);

            writer.WriteStartArray("target");
            foreach (var item in iFeatureTyping.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iFeatureTyping.Type);

            writer.WritePropertyName("typedFeature");
            writer.WriteStringValue(iFeatureTyping.TypedFeature);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
