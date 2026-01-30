// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="MembershipSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IMembership"/> interface
    /// </summary>
    internal static class MembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMembership"/> to serialize
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
            if (obj is not IMembership iMembership)
            {
                throw new ArgumentException("The object shall be an IMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("Membership"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iMembership.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iMembership, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iMembership, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="IMembership"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iMembership">
        /// The <see cref="IMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(IMembership iMembership, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iMembership.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iMembership.DeclaredShortName);

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iMembership.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iMembership.ElementId);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iMembership.IsImpliedIncluded);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iMembership.isLibraryElement);

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iMembership.MemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("memberElementId"u8);
            writer.WriteStringValue(iMembership.memberElementId);

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iMembership.MemberName);

            writer.WritePropertyName("membershipOwningNamespace"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iMembership.membershipOwningNamespace);
            writer.WriteEndObject();

            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iMembership.MemberShortName);

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iMembership.name);

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iMembership.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iMembership.ownedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iMembership.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iMembership.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iMembership.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iMembership.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iMembership.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iMembership.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iMembership.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iMembership.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iMembership.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iMembership.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iMembership.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iMembership.qualifiedName);

            writer.WriteStartArray("relatedElement"u8);

            foreach (var item in iMembership.relatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iMembership.shortName);

            writer.WriteStartArray("source"u8);

            foreach (var item in iMembership.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iMembership.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iMembership.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iMembership.Visibility.ToString().ToLower());

        }

        /// <summary>
        /// Serializes an instance of <see cref="IMembership"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iMembership">
        /// The <see cref="IMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(IMembership iMembership, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iMembership.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iMembership.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iMembership.ElementId);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iMembership.MemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iMembership.MemberName);

            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iMembership.MemberShortName);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iMembership.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iMembership.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iMembership.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iMembership.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iMembership.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source"u8);

            foreach (var item in iMembership.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iMembership.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iMembership.Visibility.ToString().ToLower());

        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
