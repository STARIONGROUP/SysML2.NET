// -------------------------------------------------------------------------------------------------
// <copyright file="ViewUsageSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Systems.Views;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ViewUsageSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IViewUsage"/> interface
    /// </summary>
    internal static class ViewUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IViewUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IViewUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (obj is not IViewUsage iViewUsage)
            {
                throw new ArgumentException("The object shall be an IViewUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("ViewUsage"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iViewUsage.Id);

            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iViewUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iViewUsage.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iViewUsage.DeclaredShortName);

            writer.WritePropertyName("direction"u8);

            if (iViewUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iViewUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iViewUsage.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iViewUsage.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iViewUsage.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iViewUsage.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iViewUsage.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iViewUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iViewUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual"u8);
            writer.WriteBooleanValue(iViewUsage.IsIndividual);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iViewUsage.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iViewUsage.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iViewUsage.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iViewUsage.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iViewUsage.IsVariable);

            writer.WritePropertyName("isVariation"u8);
            writer.WriteBooleanValue(iViewUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iViewUsage.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iViewUsage.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iViewUsage.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind"u8);

            if (iViewUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iViewUsage.PortionKind.Value.ToString().ToLower());
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
