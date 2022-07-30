// -------------------------------------------------------------------------------------------------
// <copyright file="InterfaceDefinitionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="InterfaceDefinitionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class InterfaceDefinitionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IInterfaceDefinition"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="interfaceDefinition">
        /// The <see cref="IInterfaceDefinition"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IInterfaceDefinition iInterfaceDefinition, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iInterfaceDefinition.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("InterfaceDefinition");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iInterfaceDefinition.IsIndividual);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iInterfaceDefinition.IsVariation);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iInterfaceDefinition.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iInterfaceDefinition.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iInterfaceDefinition.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iInterfaceDefinition.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iInterfaceDefinition.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iInterfaceDefinition.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iInterfaceDefinition.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iInterfaceDefinition.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iInterfaceDefinition.ShortName);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iInterfaceDefinition.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iInterfaceDefinition.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iInterfaceDefinition.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iInterfaceDefinition.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iInterfaceDefinition.Target)
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
