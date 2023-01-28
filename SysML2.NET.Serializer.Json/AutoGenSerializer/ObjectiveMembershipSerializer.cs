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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="ObjectiveMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
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

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ObjectiveMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iObjectiveMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iObjectiveMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iObjectiveMembership.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iObjectiveMembership.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iObjectiveMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iObjectiveMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iObjectiveMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iObjectiveMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iObjectiveMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iObjectiveMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iObjectiveMembership.MemberShortName);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iObjectiveMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iObjectiveMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iObjectiveMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iObjectiveMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iObjectiveMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iObjectiveMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source");
            foreach (var item in iObjectiveMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iObjectiveMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iObjectiveMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iObjectiveMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
