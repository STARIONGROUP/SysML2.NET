// -------------------------------------------------------------------------------------------------
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="RequirementConstraintMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
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

            writer.WritePropertyName("@type");
            writer.WriteStringValue("RequirementConstraintMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iRequirementConstraintMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iRequirementConstraintMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iRequirementConstraintMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iRequirementConstraintMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iRequirementConstraintMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iRequirementConstraintMembership.IsImpliedIncluded);

            writer.WritePropertyName("kind");
            writer.WriteStringValue(iRequirementConstraintMembership.Kind.ToString().ToLower());

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iRequirementConstraintMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iRequirementConstraintMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iRequirementConstraintMembership.MemberShortName);
            writer.WritePropertyName("name");
            writer.WriteStringValue(iRequirementConstraintMembership.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iRequirementConstraintMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iRequirementConstraintMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iRequirementConstraintMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iRequirementConstraintMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iRequirementConstraintMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iRequirementConstraintMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iRequirementConstraintMembership.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iRequirementConstraintMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iRequirementConstraintMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iRequirementConstraintMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iRequirementConstraintMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
