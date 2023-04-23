// -------------------------------------------------------------------------------------------------
// <copyright file="ObjectiveMembershipSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ObjectiveMembershipSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IObjectiveMembership"/> interface
    /// </summary>
    internal static class ObjectiveMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IObjectiveMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IObjectiveMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IObjectiveMembership iObjectiveMembership))
            {
                throw new ArgumentException("The object shall be an IObjectiveMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("ObjectiveMembership"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iObjectiveMembership.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iObjectiveMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iObjectiveMembership.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iObjectiveMembership.DeclaredShortName);
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iObjectiveMembership.ElementId);

            writer.WritePropertyName("feature"u8);
            writer.WriteStringValue(iObjectiveMembership.Feature);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iObjectiveMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iObjectiveMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement"u8);
            writer.WriteStringValue(iObjectiveMembership.MemberElement);

            writer.WritePropertyName("memberName"u8);
            writer.WriteStringValue(iObjectiveMembership.MemberName);
            writer.WritePropertyName("memberShortName"u8);
            writer.WriteStringValue(iObjectiveMembership.MemberShortName);
            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iObjectiveMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iObjectiveMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iObjectiveMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iObjectiveMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iObjectiveMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iObjectiveMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iObjectiveMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);
            foreach (var item in iObjectiveMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type"u8);
            writer.WriteStringValue(iObjectiveMembership.Type);

            writer.WritePropertyName("visibility"u8);
            writer.WriteStringValue(iObjectiveMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
