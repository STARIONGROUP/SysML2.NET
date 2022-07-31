// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ConstraintUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ConstraintUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IConstraintUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IConstraintUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IConstraintUsage iConstraintUsage))
            {
                throw new ArgumentException("The object shall be an IConstraintUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iConstraintUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ConstraintUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iConstraintUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iConstraintUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iConstraintUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iConstraintUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iConstraintUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iConstraintUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iConstraintUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iConstraintUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iConstraintUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iConstraintUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iConstraintUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iConstraintUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iConstraintUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iConstraintUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iConstraintUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iConstraintUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iConstraintUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iConstraintUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iConstraintUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iConstraintUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iConstraintUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iConstraintUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
