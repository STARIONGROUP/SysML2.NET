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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="OccurrenceUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class OccurrenceUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IOccurrenceUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IOccurrenceUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IOccurrenceUsage iOccurrenceUsage))
            {
                throw new ArgumentException("The object shall be an IOccurrenceUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("OccurrenceUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iOccurrenceUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iOccurrenceUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iOccurrenceUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iOccurrenceUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iOccurrenceUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iOccurrenceUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iOccurrenceUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iOccurrenceUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iOccurrenceUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iOccurrenceUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iOccurrenceUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iOccurrenceUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iOccurrenceUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iOccurrenceUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iOccurrenceUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iOccurrenceUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iOccurrenceUsage.IsVariation);

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

            writer.WritePropertyName("portionKind");
            if (iOccurrenceUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iOccurrenceUsage.PortionKind.Value.ToString().ToLower());
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
