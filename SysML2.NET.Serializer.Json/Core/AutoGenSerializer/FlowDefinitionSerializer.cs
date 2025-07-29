// -------------------------------------------------------------------------------------------------
// <copyright file="FlowDefinitionSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="FlowDefinitionSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IFlowDefinition"/> interface
    /// </summary>
    internal static class FlowDefinitionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFlowDefinition"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFlowDefinition"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFlowDefinition iFlowDefinition))
            {
                throw new ArgumentException("The object shall be an IFlowDefinition", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("FlowDefinition"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iFlowDefinition.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iFlowDefinition.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iFlowDefinition.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iFlowDefinition.DeclaredShortName);
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iFlowDefinition.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iFlowDefinition.IsAbstract);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iFlowDefinition.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iFlowDefinition.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual"u8);
            writer.WriteBooleanValue(iFlowDefinition.IsIndividual);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iFlowDefinition.IsSufficient);

            writer.WritePropertyName("isVariation"u8);
            writer.WriteBooleanValue(iFlowDefinition.IsVariation);

            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iFlowDefinition.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iFlowDefinition.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iFlowDefinition.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFlowDefinition.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iFlowDefinition.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iFlowDefinition.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iFlowDefinition.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);
            foreach (var item in iFlowDefinition.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
