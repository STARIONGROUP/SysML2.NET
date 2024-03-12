// -------------------------------------------------------------------------------------------------
// <copyright file="SatisfyRequirementUsageSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
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

namespace SysML2.NET.Serializer.Json.Core.DTO
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="SatisfyRequirementUsageSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="ISatisfyRequirementUsage"/> interface
    /// </summary>
    internal static class SatisfyRequirementUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISatisfyRequirementUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ISatisfyRequirementUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ISatisfyRequirementUsage iSatisfyRequirementUsage))
            {
                throw new ArgumentException("The object shall be an ISatisfyRequirementUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("SatisfyRequirementUsage"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iSatisfyRequirementUsage.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iSatisfyRequirementUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iSatisfyRequirementUsage.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iSatisfyRequirementUsage.DeclaredShortName);
            writer.WritePropertyName("direction"u8);
            if (iSatisfyRequirementUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iSatisfyRequirementUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iSatisfyRequirementUsage.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsComposite);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsIndividual);

            writer.WritePropertyName("isNegated"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsNegated);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsPortion);

            writer.WritePropertyName("isReadOnly"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsUnique);

            writer.WritePropertyName("isVariation"u8);
            writer.WriteBooleanValue(iSatisfyRequirementUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iSatisfyRequirementUsage.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);
            if (iSatisfyRequirementUsage.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iSatisfyRequirementUsage.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("portionKind"u8);
            if (iSatisfyRequirementUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iSatisfyRequirementUsage.PortionKind.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("reqId"u8);
            writer.WriteStringValue(iSatisfyRequirementUsage.ReqId);
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
