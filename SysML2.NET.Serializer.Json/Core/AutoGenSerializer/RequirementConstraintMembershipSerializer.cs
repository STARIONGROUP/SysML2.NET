﻿// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementConstraintMembershipSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="RequirementConstraintMembershipSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IRequirementConstraintMembership"/> interface
    /// </summary>
    internal static class RequirementConstraintMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IRequirementConstraintMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IRequirementConstraintMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IRequirementConstraintMembership iRequirementConstraintMembership))
            {
                throw new ArgumentException("The object shall be an IRequirementConstraintMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("RequirementConstraintMembership"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iRequirementConstraintMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.DeclaredShortName);
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.ElementId);

            writer.WritePropertyName("feature"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.Feature);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iRequirementConstraintMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iRequirementConstraintMembership.IsImpliedIncluded);

            writer.WritePropertyName("kind"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.Kind.ToString().ToLower());

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.MemberElement);

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.MemberName);
            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.MemberShortName);
            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iRequirementConstraintMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iRequirementConstraintMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iRequirementConstraintMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iRequirementConstraintMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iRequirementConstraintMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iRequirementConstraintMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iRequirementConstraintMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);
            foreach (var item in iRequirementConstraintMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.Type);

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iRequirementConstraintMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
