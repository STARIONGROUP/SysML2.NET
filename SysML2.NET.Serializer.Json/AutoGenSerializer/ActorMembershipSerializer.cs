// -------------------------------------------------------------------------------------------------
// <copyright file="ActorMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ActorMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ActorMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IActorMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IActorMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IActorMembership iActorMembership))
            {
                throw new ArgumentException("The object shall be an IActorMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ActorMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iActorMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iActorMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iActorMembership.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iActorMembership.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iActorMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iActorMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iActorMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iActorMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iActorMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iActorMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iActorMembership.MemberShortName);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iActorMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iActorMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iActorMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iActorMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iActorMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iActorMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source");
            foreach (var item in iActorMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iActorMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iActorMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iActorMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
