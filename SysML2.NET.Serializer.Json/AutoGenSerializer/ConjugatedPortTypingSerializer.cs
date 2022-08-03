// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingSerializer.cs" company="RHEA System S.A.">
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

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="ConjugatedPortTypingSerializer"/> is to provide serialization
    /// and deserialization capabilities
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

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ConjugatedPortTyping");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iConjugatedPortTyping.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iConjugatedPortTyping.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iConjugatedPortTyping.ElementId);

            writer.WritePropertyName("general");
            writer.WriteStringValue(iConjugatedPortTyping.General);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iConjugatedPortTyping.Name);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iConjugatedPortTyping.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iConjugatedPortTyping.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iConjugatedPortTyping.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iConjugatedPortTyping.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship");
            if (iConjugatedPortTyping.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iConjugatedPortTyping.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portDefinition");
            writer.WriteStringValue(iConjugatedPortTyping.PortDefinition);

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iConjugatedPortTyping.ShortName);

            writer.WriteStartArray("source");
            foreach (var item in iConjugatedPortTyping.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("specific");
            writer.WriteStringValue(iConjugatedPortTyping.Specific);

            writer.WriteStartArray("target");
            foreach (var item in iConjugatedPortTyping.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("type");
            writer.WriteStringValue(iConjugatedPortTyping.Type);

            writer.WritePropertyName("typedFeature");
            writer.WriteStringValue(iConjugatedPortTyping.TypedFeature);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
