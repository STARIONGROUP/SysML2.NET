// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipExposeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MembershipExposeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MembershipExposeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMembershipExpose"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMembershipExpose"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IMembershipExpose iMembershipExpose))
            {
                throw new ArgumentException("The object shall be an IMembershipExpose", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("MembershipExpose");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMembershipExpose.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMembershipExpose.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMembershipExpose.ElementId);

            writer.WritePropertyName("importedMembership");
            writer.WriteStringValue(iMembershipExpose.ImportedMembership);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iMembershipExpose.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iMembershipExpose.IsImpliedIncluded);

            writer.WritePropertyName("isImportAll");
            writer.WriteBooleanValue(iMembershipExpose.IsImportAll);

            writer.WritePropertyName("isRecursive");
            writer.WriteBooleanValue(iMembershipExpose.IsRecursive);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iMembershipExpose.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iMembershipExpose.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMembershipExpose.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iMembershipExpose.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iMembershipExpose.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iMembershipExpose.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMembershipExpose.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iMembershipExpose.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iMembershipExpose.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iMembershipExpose.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iMembershipExpose.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
