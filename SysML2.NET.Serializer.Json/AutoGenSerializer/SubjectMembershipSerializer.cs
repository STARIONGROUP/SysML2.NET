// -------------------------------------------------------------------------------------------------
// <copyright file="SubjectMembershipSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="SubjectMembershipSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class SubjectMembershipSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISubjectMembership"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ISubjectMembership"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ISubjectMembership iSubjectMembership))
            {
                throw new ArgumentException("The object shall be an ISubjectMembership", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("SubjectMembership");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iSubjectMembership.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iSubjectMembership.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iSubjectMembership.ElementId);

            writer.WritePropertyName("feature");
            writer.WriteStringValue(iSubjectMembership.Feature);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iSubjectMembership.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iSubjectMembership.IsImpliedIncluded);

            writer.WritePropertyName("memberElement");
            writer.WriteStringValue(iSubjectMembership.MemberElement);

            writer.WritePropertyName("memberName");
            writer.WriteStringValue(iSubjectMembership.MemberName);
            writer.WritePropertyName("memberShortName");
            writer.WriteStringValue(iSubjectMembership.MemberShortName);
            writer.WritePropertyName("name");
            writer.WriteStringValue(iSubjectMembership.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iSubjectMembership.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iSubjectMembership.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iSubjectMembership.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iSubjectMembership.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iSubjectMembership.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSubjectMembership.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iSubjectMembership.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iSubjectMembership.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iSubjectMembership.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iSubjectMembership.Type);

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iSubjectMembership.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
