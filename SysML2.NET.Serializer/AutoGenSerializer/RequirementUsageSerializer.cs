// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="RequirementUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class RequirementUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IRequirementUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="requirementUsage">
        /// The <see cref="IRequirementUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IRequirementUsage iRequirementUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iRequirementUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("RequirementUsage");

            writer.WritePropertyName("reqId");
            writer.WriteStringValue(iRequirementUsage.ReqId);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iRequirementUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iRequirementUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iRequirementUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iRequirementUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iRequirementUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iRequirementUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iRequirementUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iRequirementUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iRequirementUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iRequirementUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iRequirementUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iRequirementUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iRequirementUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iRequirementUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iRequirementUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iRequirementUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iRequirementUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iRequirementUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iRequirementUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iRequirementUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iRequirementUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iRequirementUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
