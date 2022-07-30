// -------------------------------------------------------------------------------------------------
// <copyright file="CalculationDefinitionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="CalculationDefinitionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class CalculationDefinitionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ICalculationDefinition"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="calculationDefinition">
        /// The <see cref="ICalculationDefinition"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ICalculationDefinition iCalculationDefinition, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iCalculationDefinition.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("CalculationDefinition");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iCalculationDefinition.IsIndividual);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iCalculationDefinition.IsVariation);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iCalculationDefinition.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iCalculationDefinition.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iCalculationDefinition.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iCalculationDefinition.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iCalculationDefinition.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iCalculationDefinition.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iCalculationDefinition.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iCalculationDefinition.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iCalculationDefinition.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
