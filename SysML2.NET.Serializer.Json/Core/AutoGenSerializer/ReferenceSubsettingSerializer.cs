// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceSubsettingSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Core.Features;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ReferenceSubsettingSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IReferenceSubsetting"/> interface
    /// </summary>
    internal static class ReferenceSubsettingSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IReferenceSubsetting"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IReferenceSubsetting"/> to serialize
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
            if (obj is not IReferenceSubsetting iReferenceSubsetting)
            {
                throw new ArgumentException("The object shall be an IReferenceSubsetting", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("ReferenceSubsetting"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iReferenceSubsetting, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iReferenceSubsetting, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="IReferenceSubsetting"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iReferenceSubsetting">
        /// The <see cref="IReferenceSubsetting"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(IReferenceSubsetting iReferenceSubsetting, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iReferenceSubsetting.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iReferenceSubsetting.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iReferenceSubsetting.DeclaredShortName);

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iReferenceSubsetting.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iReferenceSubsetting.ElementId);

            writer.WritePropertyName("general"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.General);
            writer.WriteEndObject();

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iReferenceSubsetting.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iReferenceSubsetting.IsImpliedIncluded);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iReferenceSubsetting.isLibraryElement);

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iReferenceSubsetting.name);

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iReferenceSubsetting.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iReferenceSubsetting.ownedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iReferenceSubsetting.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iReferenceSubsetting.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iReferenceSubsetting.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningFeature"u8);

            if (iReferenceSubsetting.owningFeature.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.owningFeature.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iReferenceSubsetting.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iReferenceSubsetting.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iReferenceSubsetting.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iReferenceSubsetting.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningType"u8);

            if (iReferenceSubsetting.owningType.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.owningType.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iReferenceSubsetting.qualifiedName);

            writer.WritePropertyName("referencedFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.ReferencedFeature);
            writer.WriteEndObject();

            writer.WritePropertyName("referencingFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.referencingFeature);
            writer.WriteEndObject();

            writer.WriteStartArray("relatedElement"u8);

            foreach (var item in iReferenceSubsetting.relatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iReferenceSubsetting.shortName);

            writer.WriteStartArray("source"u8);

            foreach (var item in iReferenceSubsetting.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("specific"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.Specific);
            writer.WriteEndObject();

            writer.WritePropertyName("subsettedFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.SubsettedFeature);
            writer.WriteEndObject();

            writer.WritePropertyName("subsettingFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.SubsettingFeature);
            writer.WriteEndObject();

            writer.WriteStartArray("target"u8);

            foreach (var item in iReferenceSubsetting.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iReferenceSubsetting.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

        }

        /// <summary>
        /// Serializes an instance of <see cref="IReferenceSubsetting"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iReferenceSubsetting">
        /// The <see cref="IReferenceSubsetting"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(IReferenceSubsetting iReferenceSubsetting, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iReferenceSubsetting.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iReferenceSubsetting.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iReferenceSubsetting.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iReferenceSubsetting.ElementId);

            writer.WritePropertyName("general"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.General);
            writer.WriteEndObject();

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iReferenceSubsetting.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iReferenceSubsetting.IsImpliedIncluded);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iReferenceSubsetting.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iReferenceSubsetting.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iReferenceSubsetting.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iReferenceSubsetting.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iReferenceSubsetting.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("referencedFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.ReferencedFeature);
            writer.WriteEndObject();

            writer.WriteStartArray("source"u8);

            foreach (var item in iReferenceSubsetting.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("specific"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.Specific);
            writer.WriteEndObject();

            writer.WritePropertyName("subsettedFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.SubsettedFeature);
            writer.WriteEndObject();

            writer.WritePropertyName("subsettingFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iReferenceSubsetting.SubsettingFeature);
            writer.WriteEndObject();

            writer.WriteStartArray("target"u8);

            foreach (var item in iReferenceSubsetting.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
