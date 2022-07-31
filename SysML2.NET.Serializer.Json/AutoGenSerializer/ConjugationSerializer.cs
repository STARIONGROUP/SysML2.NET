// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugationSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ConjugationSerializer"/> is to provide serialization
    /// and deserialization capabilities
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
            if (!(obj is IConjugation iConjugation))
            {
                throw new ArgumentException("The object shall be an IConjugation", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iConjugation.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Conjugation");

            writer.WritePropertyName("conjugatedType");
            writer.WriteStringValue(iConjugation.ConjugatedType);

            writer.WritePropertyName("originalType");
            writer.WriteStringValue(iConjugation.OriginalType);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iConjugation.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iConjugation.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iConjugation.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iConjugation.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iConjugation.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("aliasIds");
            foreach (var item in iConjugation.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iConjugation.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iConjugation.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iConjugation.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iConjugation.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iConjugation.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iConjugation.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
