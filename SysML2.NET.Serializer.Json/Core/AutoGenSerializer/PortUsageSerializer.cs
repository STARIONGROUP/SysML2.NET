// -------------------------------------------------------------------------------------------------
// <copyright file="PortUsageSerializer.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
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
    using SysML2.NET.Core.DTO.Systems.Ports;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="PortUsageSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IPortUsage"/> interface
    /// </summary>
    internal static class PortUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IPortUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IPortUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (obj is not IPortUsage iPortUsage)
            {
                throw new ArgumentException("The object shall be an IPortUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("PortUsage"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iPortUsage.Id);

            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iPortUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iPortUsage.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iPortUsage.DeclaredShortName);

            writer.WritePropertyName("direction"u8);

            if (iPortUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iPortUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iPortUsage.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iPortUsage.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iPortUsage.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iPortUsage.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iPortUsage.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iPortUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iPortUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual"u8);
            writer.WriteBooleanValue(iPortUsage.IsIndividual);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iPortUsage.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iPortUsage.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iPortUsage.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iPortUsage.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iPortUsage.IsVariable);

            writer.WritePropertyName("isVariation"u8);
            writer.WriteBooleanValue(iPortUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iPortUsage.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iPortUsage.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iPortUsage.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind"u8);

            if (iPortUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iPortUsage.PortionKind.Value.ToString().ToLower());
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
