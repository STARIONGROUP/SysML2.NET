// -------------------------------------------------------------------------------------------------
// <copyright file="ViewRenderingMembershipSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Systems.Views;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ViewRenderingMembershipSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IViewRenderingMembership"/> interface
    /// </summary>
    internal static class ViewRenderingMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IViewRenderingMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IViewRenderingMembership"/> to serialize
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
            if (obj is not IViewRenderingMembership iViewRenderingMembership)
            {
                throw new ArgumentException("The object shall be an IViewRenderingMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("ViewRenderingMembership"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iViewRenderingMembership, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iViewRenderingMembership, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="IViewRenderingMembership"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iViewRenderingMembership">
        /// The <see cref="IViewRenderingMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(IViewRenderingMembership iViewRenderingMembership, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iViewRenderingMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iViewRenderingMembership.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iViewRenderingMembership.DeclaredShortName);

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iViewRenderingMembership.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iViewRenderingMembership.ElementId);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iViewRenderingMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iViewRenderingMembership.IsImpliedIncluded);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iViewRenderingMembership.isLibraryElement);

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.MemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("memberElementId"u8);
            writer.WriteStringValue(iViewRenderingMembership.memberElementId);

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iViewRenderingMembership.MemberName);

            writer.WritePropertyName("membershipOwningNamespace"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.membershipOwningNamespace);
            writer.WriteEndObject();

            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iViewRenderingMembership.MemberShortName);

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iViewRenderingMembership.name);

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iViewRenderingMembership.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iViewRenderingMembership.ownedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedMemberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.ownedMemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("ownedMemberElementId"u8);
            writer.WriteStringValue(iViewRenderingMembership.ownedMemberElementId);

            writer.WritePropertyName("ownedMemberFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.ownedMemberFeature);
            writer.WriteEndObject();

            writer.WritePropertyName("ownedMemberName"u8);
            writer.WriteStringValue(iViewRenderingMembership.ownedMemberName);

            writer.WritePropertyName("ownedMemberShortName"u8);
            writer.WriteStringValue(iViewRenderingMembership.ownedMemberShortName);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iViewRenderingMembership.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iViewRenderingMembership.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("ownedRendering"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.ownedRendering);
            writer.WriteEndObject();

            writer.WritePropertyName("owner"u8);

            if (iViewRenderingMembership.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iViewRenderingMembership.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iViewRenderingMembership.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iViewRenderingMembership.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iViewRenderingMembership.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iViewRenderingMembership.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iViewRenderingMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iViewRenderingMembership.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iViewRenderingMembership.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iViewRenderingMembership.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningType"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.owningType);
            writer.WriteEndObject();

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iViewRenderingMembership.qualifiedName);

            writer.WritePropertyName("referencedRendering"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.referencedRendering);
            writer.WriteEndObject();

            writer.WriteStartArray("relatedElement"u8);

            foreach (var item in iViewRenderingMembership.relatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iViewRenderingMembership.shortName);

            writer.WriteStartArray("source"u8);

            foreach (var item in iViewRenderingMembership.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iViewRenderingMembership.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iViewRenderingMembership.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iViewRenderingMembership.Visibility.ToString().ToLower());

        }

        /// <summary>
        /// Serializes an instance of <see cref="IViewRenderingMembership"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iViewRenderingMembership">
        /// The <see cref="IViewRenderingMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(IViewRenderingMembership iViewRenderingMembership, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iViewRenderingMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iViewRenderingMembership.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iViewRenderingMembership.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iViewRenderingMembership.ElementId);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iViewRenderingMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iViewRenderingMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewRenderingMembership.MemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iViewRenderingMembership.MemberName);

            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iViewRenderingMembership.MemberShortName);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iViewRenderingMembership.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iViewRenderingMembership.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iViewRenderingMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iViewRenderingMembership.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iViewRenderingMembership.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iViewRenderingMembership.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source"u8);

            foreach (var item in iViewRenderingMembership.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iViewRenderingMembership.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iViewRenderingMembership.Visibility.ToString().ToLower());

        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
