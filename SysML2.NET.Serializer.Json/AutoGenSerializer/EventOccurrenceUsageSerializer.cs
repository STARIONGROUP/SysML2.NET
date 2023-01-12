// -------------------------------------------------------------------------------------------------
// <copyright file="EventOccurrenceUsageSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="EventOccurrenceUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class EventOccurrenceUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IEventOccurrenceUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IEventOccurrenceUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IEventOccurrenceUsage iEventOccurrenceUsage))
            {
                throw new ArgumentException("The object shall be an IEventOccurrenceUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("EventOccurrenceUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iEventOccurrenceUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iEventOccurrenceUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iEventOccurrenceUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iEventOccurrenceUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iEventOccurrenceUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iEventOccurrenceUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iEventOccurrenceUsage.Name);
            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iEventOccurrenceUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iEventOccurrenceUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iEventOccurrenceUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("portionKind");
            if (iEventOccurrenceUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iEventOccurrenceUsage.PortionKind.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iEventOccurrenceUsage.ShortName);
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
