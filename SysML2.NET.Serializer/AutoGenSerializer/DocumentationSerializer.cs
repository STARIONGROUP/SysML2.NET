// -------------------------------------------------------------------------------------------------
// <copyright file="DocumentationSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="DocumentationSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class DocumentationSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IDocumentation"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="documentation">
        /// The <see cref="IDocumentation"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IDocumentation iDocumentation, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iDocumentation.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Documentation");

            writer.WritePropertyName("body");
            writer.WriteStringValue(iDocumentation.Body);

            writer.WritePropertyName("locale");
            writer.WriteStringValue(iDocumentation.Locale);

            writer.WriteStartArray("annotation");
            foreach (var item in iDocumentation.Annotation)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iDocumentation.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iDocumentation.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iDocumentation.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iDocumentation.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iDocumentation.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iDocumentation.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iDocumentation.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
