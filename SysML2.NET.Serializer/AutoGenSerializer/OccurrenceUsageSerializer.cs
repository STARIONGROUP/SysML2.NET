// -------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="OccurrenceUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class OccurrenceUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IOccurrenceUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="occurrenceUsage">
        /// The <see cref="IOccurrenceUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IOccurrenceUsage iOccurrenceUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iOccurrenceUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("OccurrenceUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iOccurrenceUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iOccurrenceUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iOccurrenceUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iOccurrenceUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iOccurrenceUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iOccurrenceUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iOccurrenceUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iOccurrenceUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iOccurrenceUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iOccurrenceUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iOccurrenceUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iOccurrenceUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iOccurrenceUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iOccurrenceUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iOccurrenceUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iOccurrenceUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iOccurrenceUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iOccurrenceUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iOccurrenceUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iOccurrenceUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iOccurrenceUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iOccurrenceUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
