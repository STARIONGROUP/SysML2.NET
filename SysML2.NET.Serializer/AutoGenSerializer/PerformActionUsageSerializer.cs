// -------------------------------------------------------------------------------------------------
// <copyright file="PerformActionUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="PerformActionUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class PerformActionUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IPerformActionUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="performActionUsage">
        /// The <see cref="IPerformActionUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IPerformActionUsage iPerformActionUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iPerformActionUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("PerformActionUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iPerformActionUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iPerformActionUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iPerformActionUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iPerformActionUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iPerformActionUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iPerformActionUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iPerformActionUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iPerformActionUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iPerformActionUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iPerformActionUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iPerformActionUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iPerformActionUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iPerformActionUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iPerformActionUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iPerformActionUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iPerformActionUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iPerformActionUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iPerformActionUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iPerformActionUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iPerformActionUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iPerformActionUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iPerformActionUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
