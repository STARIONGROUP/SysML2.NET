// -------------------------------------------------------------------------------------------------
// <copyright file="AllocationUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="AllocationUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class AllocationUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IAllocationUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IAllocationUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IAllocationUsage iAllocationUsage))
            {
                throw new ArgumentException("The object shall be an IAllocationUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iAllocationUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("AllocationUsage");

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iAllocationUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iAllocationUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iAllocationUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iAllocationUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iAllocationUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iAllocationUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iAllocationUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iAllocationUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iAllocationUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iAllocationUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iAllocationUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iAllocationUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iAllocationUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iAllocationUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iAllocationUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iAllocationUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iAllocationUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iAllocationUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iAllocationUsage.ShortName);

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iAllocationUsage.IsDirected);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iAllocationUsage.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iAllocationUsage.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iAllocationUsage.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iAllocationUsage.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iAllocationUsage.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iAllocationUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iAllocationUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iAllocationUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------