// -------------------------------------------------------------------------------------------------
// <copyright file="StateUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="StateUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class StateUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IStateUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IStateUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IStateUsage iStateUsage))
            {
                throw new ArgumentException("The object shall be an IStateUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iStateUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("StateUsage");

            writer.WritePropertyName("isParallel");
            writer.WriteBooleanValue(iStateUsage.IsParallel);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iStateUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iStateUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iStateUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iStateUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iStateUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iStateUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iStateUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iStateUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iStateUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iStateUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iStateUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iStateUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iStateUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iStateUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iStateUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iStateUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iStateUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iStateUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iStateUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iStateUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iStateUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iStateUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
