// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotationSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer
{
    using System.Text.Json;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="AnnotationSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class AnnotationSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IAnnotation"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="annotation">
        /// The <see cref="IAnnotation"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IAnnotation iAnnotation, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iAnnotation.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Annotation");

            writer.WritePropertyName("annotatedElement");
            writer.WriteStringValue(iAnnotation.AnnotatedElement);

            writer.WritePropertyName("annotatingElement");
            writer.WriteStringValue(iAnnotation.AnnotatingElement);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iAnnotation.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iAnnotation.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iAnnotation.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iAnnotation.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iAnnotation.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iAnnotation.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iAnnotation.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iAnnotation.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iAnnotation.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iAnnotation.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iAnnotation.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iAnnotation.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
