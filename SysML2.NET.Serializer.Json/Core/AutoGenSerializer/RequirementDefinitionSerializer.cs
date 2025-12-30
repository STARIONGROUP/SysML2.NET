// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementDefinitionSerializer.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Systems.Requirements;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="RequirementDefinitionSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IRequirementDefinition"/> interface
    /// </summary>
    internal static class RequirementDefinitionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IRequirementDefinition"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IRequirementDefinition"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (obj is not IRequirementDefinition iRequirementDefinition)
            {
                throw new ArgumentException("The object shall be an IRequirementDefinition", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("RequirementDefinition"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iRequirementDefinition.Id);

            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iRequirementDefinition.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iRequirementDefinition.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iRequirementDefinition.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iRequirementDefinition.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iRequirementDefinition.IsAbstract);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iRequirementDefinition.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual"u8);
            writer.WriteBooleanValue(iRequirementDefinition.IsIndividual);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iRequirementDefinition.IsSufficient);

            writer.WritePropertyName("isVariation"u8);
            writer.WriteBooleanValue(iRequirementDefinition.IsVariation);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iRequirementDefinition.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iRequirementDefinition.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iRequirementDefinition.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteEndObject();
        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
