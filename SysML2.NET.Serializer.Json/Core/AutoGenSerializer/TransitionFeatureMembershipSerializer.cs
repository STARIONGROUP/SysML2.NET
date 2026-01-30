// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Systems.States;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="TransitionFeatureMembershipSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="ITransitionFeatureMembership"/> interface
    /// </summary>
    internal static class TransitionFeatureMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ITransitionFeatureMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ITransitionFeatureMembership"/> to serialize
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
            if (obj is not ITransitionFeatureMembership iTransitionFeatureMembership)
            {
                throw new ArgumentException("The object shall be an ITransitionFeatureMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("TransitionFeatureMembership"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.Id);

            if (includeDerivedProperties)
            {
                SerializeAsJsonWithDerivedProperties(iTransitionFeatureMembership, writer);
            }
            else
            {
                SerializeAsJsonWithoutDerivedProperties(iTransitionFeatureMembership, writer);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Serializes an instance of <see cref="ITransitionFeatureMembership"/> using an <see cref="Utf8JsonWriter"/> as JSON including derived properties
        /// </summary>
        /// <param name=" iTransitionFeatureMembership">
        /// The <see cref="ITransitionFeatureMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithDerivedProperties(ITransitionFeatureMembership iTransitionFeatureMembership, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iTransitionFeatureMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.DeclaredShortName);

            writer.WriteStartArray("documentation"u8);

            foreach (var item in iTransitionFeatureMembership.documentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.ElementId);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iTransitionFeatureMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iTransitionFeatureMembership.IsImpliedIncluded);

            writer.WritePropertyName("isLibraryElement"u8);
            writer.WriteBooleanValue(iTransitionFeatureMembership.isLibraryElement);

            writer.WritePropertyName("kind"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.Kind.ToString().ToLower());

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.MemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("memberElementId"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.memberElementId);

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.MemberName);

            writer.WritePropertyName("membershipOwningNamespace"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.membershipOwningNamespace);
            writer.WriteEndObject();

            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.MemberShortName);

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.name);

            writer.WriteStartArray("ownedAnnotation"u8);

            foreach (var item in iTransitionFeatureMembership.ownedAnnotation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedElement"u8);

            foreach (var item in iTransitionFeatureMembership.ownedElement)
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
            writer.WriteStringValue(iTransitionFeatureMembership.ownedMemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("ownedMemberElementId"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.ownedMemberElementId);

            writer.WritePropertyName("ownedMemberFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.ownedMemberFeature);
            writer.WriteEndObject();

            writer.WritePropertyName("ownedMemberName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.ownedMemberName);

            writer.WritePropertyName("ownedMemberShortName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.ownedMemberShortName);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iTransitionFeatureMembership.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iTransitionFeatureMembership.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owner"u8);

            if (iTransitionFeatureMembership.owner.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTransitionFeatureMembership.owner.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningMembership"u8);

            if (iTransitionFeatureMembership.owningMembership.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTransitionFeatureMembership.owningMembership.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningNamespace"u8);

            if (iTransitionFeatureMembership.owningNamespace.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTransitionFeatureMembership.owningNamespace.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iTransitionFeatureMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTransitionFeatureMembership.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iTransitionFeatureMembership.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTransitionFeatureMembership.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningType"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.owningType);
            writer.WriteEndObject();

            writer.WritePropertyName("qualifiedName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.qualifiedName);

            writer.WriteStartArray("relatedElement"u8);

            foreach (var item in iTransitionFeatureMembership.relatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.shortName);

            writer.WriteStartArray("source"u8);

            foreach (var item in iTransitionFeatureMembership.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iTransitionFeatureMembership.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("textualRepresentation"u8);

            foreach (var item in iTransitionFeatureMembership.textualRepresentation)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("transitionFeature"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.transitionFeature);
            writer.WriteEndObject();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.Visibility.ToString().ToLower());

        }

        /// <summary>
        /// Serializes an instance of <see cref="ITransitionFeatureMembership"/> using an <see cref="Utf8JsonWriter"/> as JSON excluding derived properties
        /// </summary>
        /// <param name=" iTransitionFeatureMembership">
        /// The <see cref="ITransitionFeatureMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        private static void SerializeAsJsonWithoutDerivedProperties(ITransitionFeatureMembership iTransitionFeatureMembership, Utf8JsonWriter writer)
        {
            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iTransitionFeatureMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.ElementId);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iTransitionFeatureMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iTransitionFeatureMembership.IsImpliedIncluded);

            writer.WritePropertyName("kind"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.Kind.ToString().ToLower());

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStartObject();
            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.MemberElement);
            writer.WriteEndObject();

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.MemberName);

            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.MemberShortName);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iTransitionFeatureMembership.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iTransitionFeatureMembership.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iTransitionFeatureMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTransitionFeatureMembership.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iTransitionFeatureMembership.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iTransitionFeatureMembership.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source"u8);

            foreach (var item in iTransitionFeatureMembership.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iTransitionFeatureMembership.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iTransitionFeatureMembership.Visibility.ToString().ToLower());

        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
