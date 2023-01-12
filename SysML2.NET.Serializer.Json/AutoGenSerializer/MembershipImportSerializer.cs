// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipImportSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MembershipImportSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MembershipImportSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMembershipImport"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMembershipImport"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IMembershipImport iMembershipImport))
            {
                throw new ArgumentException("The object shall be an IMembershipImport", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("MembershipImport");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMembershipImport.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMembershipImport.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMembershipImport.ElementId);

            writer.WritePropertyName("importedMembership");
            writer.WriteStringValue(iMembershipImport.ImportedMembership);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iMembershipImport.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iMembershipImport.IsImpliedIncluded);

            writer.WritePropertyName("isImportAll");
            writer.WriteBooleanValue(iMembershipImport.IsImportAll);

            writer.WritePropertyName("isRecursive");
            writer.WriteBooleanValue(iMembershipImport.IsRecursive);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iMembershipImport.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iMembershipImport.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMembershipImport.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iMembershipImport.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iMembershipImport.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iMembershipImport.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMembershipImport.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iMembershipImport.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iMembershipImport.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iMembershipImport.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("visibility");
            writer.WriteStringValue(iMembershipImport.Visibility.ToString().ToLower());

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
