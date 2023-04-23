// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ConjugatedPortTypingSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IConjugatedPortTyping"/> interface
    /// </summary>
    internal static class ConjugatedPortTypingSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IConjugatedPortTyping"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IConjugatedPortTyping"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IConjugatedPortTyping iConjugatedPortTyping))
            {
                throw new ArgumentException("The object shall be an IConjugatedPortTyping", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("ConjugatedPortTyping"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iConjugatedPortTyping.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iConjugatedPortTyping.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("conjugatedPortDefinition"u8);
            writer.WriteStringValue(iConjugatedPortTyping.ConjugatedPortDefinition);

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iConjugatedPortTyping.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iConjugatedPortTyping.DeclaredShortName);
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iConjugatedPortTyping.ElementId);

            writer.WritePropertyName("general"u8);
            writer.WriteStringValue(iConjugatedPortTyping.General);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iConjugatedPortTyping.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iConjugatedPortTyping.IsImpliedIncluded);

            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iConjugatedPortTyping.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iConjugatedPortTyping.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iConjugatedPortTyping.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iConjugatedPortTyping.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iConjugatedPortTyping.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iConjugatedPortTyping.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iConjugatedPortTyping.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("specific"u8);
            writer.WriteStringValue(iConjugatedPortTyping.Specific);

            writer.WriteStartArray("target"u8);
            foreach (var item in iConjugatedPortTyping.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type"u8);
            writer.WriteStringValue(iConjugatedPortTyping.Type);

            writer.WritePropertyName("typedFeature"u8);
            writer.WriteStringValue(iConjugatedPortTyping.TypedFeature);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
