// -------------------------------------------------------------------------------------------------
// <copyright file="IfActionUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="IfActionUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class IfActionUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IIfActionUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IIfActionUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IIfActionUsage iIfActionUsage))
            {
                throw new ArgumentException("The object shall be an IIfActionUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("IfActionUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iIfActionUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iIfActionUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iIfActionUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iIfActionUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iIfActionUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iIfActionUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iIfActionUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iIfActionUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iIfActionUsage.IsEnd);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iIfActionUsage.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iIfActionUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iIfActionUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iIfActionUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iIfActionUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iIfActionUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iIfActionUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iIfActionUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iIfActionUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iIfActionUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iIfActionUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind");
            if (iIfActionUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iIfActionUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iIfActionUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
