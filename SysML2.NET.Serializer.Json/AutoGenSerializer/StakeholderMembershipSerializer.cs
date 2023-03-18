// -------------------------------------------------------------------------------------------------
// <copyright file="StakeholderMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="StakeholderMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class StakeholderMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IStakeholderMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IStakeholderMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IStakeholderMembership iStakeholderMembership))
            {
                throw new ArgumentException("The object shall be an IStakeholderMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("StakeholderMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iStakeholderMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iStakeholderMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName");
            writer.WriteStringValue(iStakeholderMembership.DeclaredName);
            writer.WritePropertyName("declaredShortName");
            writer.WriteStringValue(iStakeholderMembership.DeclaredShortName);
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iStakeholderMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iStakeholderMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iStakeholderMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iStakeholderMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iStakeholderMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iStakeholderMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iStakeholderMembership.MemberShortName);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iStakeholderMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iStakeholderMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iStakeholderMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iStakeholderMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iStakeholderMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iStakeholderMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source");
            foreach (var item in iStakeholderMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iStakeholderMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iStakeholderMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iStakeholderMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
