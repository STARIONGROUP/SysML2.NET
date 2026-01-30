// -------------------------------------------------------------------------------------------------
// <copyright file="TextualRepresentationSerializer.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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
    using SysML2.NET.Core.DTO.Root.Annotations;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="TextualRepresentationSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="ITextualRepresentation"/> interface
    /// </summary>
    internal static class TextualRepresentationSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ITextualRepresentation"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ITextualRepresentation"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            if (obj is not ITextualRepresentation iTextualRepresentation)
            {
                throw new ArgumentException("The object shall be an ITextualRepresentation", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("TextualRepresentation"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTextualRepresentation.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iTextualRepresentation, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iTextualRepresentation, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="ITextualRepresentation"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iTextualRepresentation">
        /// The <see cref="ITextualRepresentation"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(ITextualRepresentation iTextualRepresentation, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iTextualRepresentation.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WriteStartArray("annotatedElement"u8);

            foreach (var item in iTextualRepresentation.annotatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("annotation"u8);

            foreach (var item in iTextualRepresentation.annotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("body"u8);
            writer.WriteStringValue(iTextualRepresentation.Body);

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iTextualRepresentation.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iTextualRepresentation.DeclaredShortName);

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iTextualRepresentation.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iTextualRepresentation.ElementId);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iTextualRepresentation.IsImpliedIncluded);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iTextualRepresentation.isLibraryElement);

            writer.WritePropertyName("language"u8);
            writer.WriteStringValue(iTextualRepresentation.Language);

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iTextualRepresentation.name);

            writer.WriteStartArray("ownedAnnotatingRelationship"u8);

            foreach (var item in iTextualRepresentation.ownedAnnotatingRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iTextualRepresentation.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iTextualRepresentation.ownedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iTextualRepresentation.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iTextualRepresentation.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTextualRepresentation.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningAnnotatingRelationship"u8);

            if (iTextualRepresentation.owningAnnotatingRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTextualRepresentation.owningAnnotatingRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iTextualRepresentation.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTextualRepresentation.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iTextualRepresentation.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTextualRepresentation.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iTextualRepresentation.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTextualRepresentation.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iTextualRepresentation.qualifiedName);

            writer.WritePropertyName("representedElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTextualRepresentation.representedElement);
            writer.WriteEndObject();

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iTextualRepresentation.shortName);

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iTextualRepresentation.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

        }

        /// <summary>
        /// Serializes an instance of <see cref="ITextualRepresentation"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iTextualRepresentation">
        /// The <see cref="ITextualRepresentation"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(ITextualRepresentation iTextualRepresentation, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iTextualRepresentation.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("body"u8);
            writer.WriteStringValue(iTextualRepresentation.Body);

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iTextualRepresentation.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iTextualRepresentation.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iTextualRepresentation.ElementId);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iTextualRepresentation.IsImpliedIncluded);

            writer.WritePropertyName("language"u8);
            writer.WriteStringValue(iTextualRepresentation.Language);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iTextualRepresentation.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iTextualRepresentation.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTextualRepresentation.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
