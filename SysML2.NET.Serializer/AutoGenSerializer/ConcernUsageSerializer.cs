// -------------------------------------------------------------------------------------------------
// <copyright file="ConcernUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ConcernUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ConcernUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IConcernUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="concernUsage">
        /// The <see cref="IConcernUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IConcernUsage iConcernUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iConcernUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ConcernUsage");

            writer.WritePropertyName("reqId");
            writer.WriteStringValue(iConcernUsage.ReqId);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iConcernUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iConcernUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iConcernUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iConcernUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iConcernUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iConcernUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iConcernUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iConcernUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iConcernUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iConcernUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iConcernUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iConcernUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iConcernUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iConcernUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iConcernUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iConcernUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iConcernUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iConcernUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iConcernUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iConcernUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iConcernUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iConcernUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
