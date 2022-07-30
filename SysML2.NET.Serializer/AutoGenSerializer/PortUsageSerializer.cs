// -------------------------------------------------------------------------------------------------
// <copyright file="PortUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="PortUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class PortUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IPortUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="portUsage">
        /// The <see cref="IPortUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IPortUsage iPortUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iPortUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("PortUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iPortUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iPortUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iPortUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iPortUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iPortUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iPortUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iPortUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iPortUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iPortUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iPortUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iPortUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iPortUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iPortUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iPortUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iPortUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iPortUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iPortUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iPortUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iPortUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iPortUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iPortUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iPortUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
