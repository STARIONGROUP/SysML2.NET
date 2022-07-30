// -------------------------------------------------------------------------------------------------
// <copyright file="CalculationUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="CalculationUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class CalculationUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ICalculationUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="calculationUsage">
        /// The <see cref="ICalculationUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ICalculationUsage iCalculationUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iCalculationUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("CalculationUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iCalculationUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iCalculationUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iCalculationUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iCalculationUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iCalculationUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iCalculationUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iCalculationUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iCalculationUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iCalculationUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iCalculationUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iCalculationUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iCalculationUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iCalculationUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iCalculationUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iCalculationUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iCalculationUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iCalculationUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iCalculationUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iCalculationUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iCalculationUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iCalculationUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iCalculationUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
