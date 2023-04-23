// -------------------------------------------------------------------------------------------------
// <copyright file="InteractionSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.DTO.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="InteractionSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IInteraction"/> interface
    /// </summary>
    internal static class InteractionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IInteraction"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IInteraction"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IInteraction iInteraction))
            {
                throw new ArgumentException("The object shall be an IInteraction", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("Interaction"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iInteraction.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iInteraction.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iInteraction.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iInteraction.DeclaredShortName);
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iInteraction.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iInteraction.IsAbstract);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iInteraction.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iInteraction.IsImpliedIncluded);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iInteraction.IsSufficient);

            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iInteraction.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iInteraction.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iInteraction.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iInteraction.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iInteraction.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iInteraction.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iInteraction.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);
            foreach (var item in iInteraction.Target)
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
