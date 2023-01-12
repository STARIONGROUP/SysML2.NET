// -------------------------------------------------------------------------------------------------
// <copyright file="StateSubactionMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="StateSubactionMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class StateSubactionMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IStateSubactionMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IStateSubactionMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IStateSubactionMembership iStateSubactionMembership))
            {
                throw new ArgumentException("The object shall be an IStateSubactionMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("StateSubactionMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iStateSubactionMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iStateSubactionMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iStateSubactionMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iStateSubactionMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iStateSubactionMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iStateSubactionMembership.IsImpliedIncluded);

            writer.WritePropertyName("kind");
            writer.WriteStringValue(iStateSubactionMembership.Kind.ToString().ToLower());

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iStateSubactionMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iStateSubactionMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iStateSubactionMembership.MemberShortName);
            writer.WritePropertyName("name");
            writer.WriteStringValue(iStateSubactionMembership.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iStateSubactionMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iStateSubactionMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iStateSubactionMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iStateSubactionMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iStateSubactionMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iStateSubactionMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iStateSubactionMembership.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iStateSubactionMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iStateSubactionMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iStateSubactionMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iStateSubactionMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
