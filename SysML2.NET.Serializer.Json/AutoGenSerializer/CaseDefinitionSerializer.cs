// -------------------------------------------------------------------------------------------------
// <copyright file="CaseDefinitionSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="CaseDefinitionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class CaseDefinitionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ICaseDefinition"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ICaseDefinition"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ICaseDefinition iCaseDefinition))
            {
                throw new ArgumentException("The object shall be an ICaseDefinition", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("CaseDefinition");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iCaseDefinition.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iCaseDefinition.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iCaseDefinition.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iCaseDefinition.IsAbstract);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iCaseDefinition.IsIndividual);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iCaseDefinition.IsSufficient);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iCaseDefinition.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iCaseDefinition.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iCaseDefinition.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iCaseDefinition.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iCaseDefinition.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iCaseDefinition.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
