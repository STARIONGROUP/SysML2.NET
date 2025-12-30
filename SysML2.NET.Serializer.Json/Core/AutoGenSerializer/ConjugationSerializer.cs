// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugationSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Core.Types;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ConjugationSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IConjugation"/> interface
    /// </summary>
    internal static class ConjugationSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IConjugation"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IConjugation"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (obj is not IConjugation iConjugation)
            {
                throw new ArgumentException("The object shall be an IConjugation", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("Conjugation"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iConjugation.Id);

            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iConjugation.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iConjugation.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iConjugation.DeclaredShortName);

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iConjugation.ElementId);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iConjugation.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iConjugation.IsImpliedIncluded);

            writer.WriteStartArray("ownedRelatedElement"u8);

            foreach (var item in iConjugation.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iConjugation.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);

            if (iConjugation.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iConjugation.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship"u8);

            if (iConjugation.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iConjugation.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source"u8);

            foreach (var item in iConjugation.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);

            foreach (var item in iConjugation.Target)
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
