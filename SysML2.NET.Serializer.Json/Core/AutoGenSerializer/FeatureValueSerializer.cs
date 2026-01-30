// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureValueSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Kernel.FeatureValues;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="FeatureValueSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IFeatureValue"/> interface
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
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind, bool includeDerivedProperties)
        {
            if (obj is not IFeatureValue iFeatureValue)
            {
                throw new ArgumentException("The object shall be an IFeatureValue", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("FeatureValue"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iFeatureValue.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iFeatureValue, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iFeatureValue, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="IFeatureValue"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iFeatureValue">
        /// The <see cref="IFeatureValue"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(IFeatureValue iFeatureValue, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iFeatureValue.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iFeatureValue.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iFeatureValue.DeclaredShortName);

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iFeatureValue.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iFeatureValue.ElementId);

            writer.WritePropertyName("featureWithValue"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iFeatureValue.featureWithValue);
            writer.WriteEndObject();

            writer.WritePropertyName("isDefault"u8);
            writer.WriteBooleanValue(iFeatureValue.IsDefault);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iFeatureValue.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iFeatureValue.IsImpliedIncluded);

            writer.WritePropertyName("isInitial"u8);
            writer.WriteBooleanValue(iFeatureValue.IsInitial);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iFeatureValue.isLibraryElement);

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iFeatureValue.MemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("memberElementId"u8);
            writer.WriteStringValue(iFeatureValue.memberElementId);

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iFeatureValue.MemberName);

            writer.WritePropertyName("membershipOwningNamespace"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iFeatureValue.membershipOwningNamespace);
            writer.WriteEndObject();

            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iFeatureValue.MemberShortName);

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iFeatureValue.name);

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iFeatureValue.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iFeatureValue.ownedElement)
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
            writer.WriteStringValue(iFeatureValue.ownedMemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("ownedMemberElementId"u8);
            writer.WriteStringValue(iFeatureValue.ownedMemberElementId);

            writer.WritePropertyName("ownedMemberName"u8);
            writer.WriteStringValue(iFeatureValue.ownedMemberName);

            writer.WritePropertyName("ownedMemberShortName"u8);
            writer.WriteStringValue(iFeatureValue.ownedMemberShortName);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iFeatureValue.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iFeatureValue.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iFeatureValue.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFeatureValue.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iFeatureValue.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFeatureValue.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iFeatureValue.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFeatureValue.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iFeatureValue.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFeatureValue.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iFeatureValue.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFeatureValue.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iFeatureValue.qualifiedName);

            writer.WriteStartArray("relatedElement"u8);

            foreach (var item in iFeatureValue.relatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iFeatureValue.shortName);

            writer.WriteStartArray("source"u8);

            foreach (var item in iFeatureValue.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iFeatureValue.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iFeatureValue.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("value"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iFeatureValue.value);
            writer.WriteEndObject();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iFeatureValue.Visibility.ToString().ToLower());

        }

        /// <summary>
        /// Serializes an instance of <see cref="IFeatureValue"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iFeatureValue">
        /// The <see cref="IFeatureValue"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(IFeatureValue iFeatureValue, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iFeatureValue.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iFeatureValue.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iFeatureValue.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iFeatureValue.ElementId);

            writer.WritePropertyName("isDefault"u8);
            writer.WriteBooleanValue(iFeatureValue.IsDefault);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iFeatureValue.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iFeatureValue.IsImpliedIncluded);

            writer.WritePropertyName("isInitial"u8);
            writer.WriteBooleanValue(iFeatureValue.IsInitial);

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iFeatureValue.MemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iFeatureValue.MemberName);

            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iFeatureValue.MemberShortName);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iFeatureValue.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iFeatureValue.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iFeatureValue.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFeatureValue.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iFeatureValue.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFeatureValue.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source"u8);

            foreach (var item in iFeatureValue.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iFeatureValue.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iFeatureValue.Visibility.ToString().ToLower());

        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
