// -------------------------------------------------------------------------------------------------
// <copyright file="AssociationStructureSerializer.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer
{
    using System.Text.Json;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="AssociationStructureSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class AssociationStructureSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IAssociationStructure"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="associationStructure">
        /// The <see cref="IAssociationStructure"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IAssociationStructure iAssociationStructure, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iAssociationStructure.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("AssociationStructure");

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iAssociationStructure.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iAssociationStructure.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iAssociationStructure.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iAssociationStructure.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iAssociationStructure.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iAssociationStructure.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iAssociationStructure.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iAssociationStructure.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iAssociationStructure.ShortName);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iAssociationStructure.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iAssociationStructure.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iAssociationStructure.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iAssociationStructure.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iAssociationStructure.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
