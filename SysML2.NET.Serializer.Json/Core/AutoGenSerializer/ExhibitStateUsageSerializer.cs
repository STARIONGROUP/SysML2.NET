// -------------------------------------------------------------------------------------------------
// <copyright file="ExhibitStateUsageSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Systems.States;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="ExhibitStateUsageSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IExhibitStateUsage"/> interface
    /// </summary>
    internal static class ExhibitStateUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IExhibitStateUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IExhibitStateUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (obj is not IExhibitStateUsage iExhibitStateUsage)
            {
                throw new ArgumentException("The object shall be an IExhibitStateUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("ExhibitStateUsage"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iExhibitStateUsage.Id);

            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iExhibitStateUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iExhibitStateUsage.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iExhibitStateUsage.DeclaredShortName);

            writer.WritePropertyName("direction"u8);

            if (iExhibitStateUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iExhibitStateUsage.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsIndividual);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsOrdered);

            writer.WritePropertyName("isParallel"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsParallel);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsVariable);

            writer.WritePropertyName("isVariation"u8);
            writer.WriteBooleanValue(iExhibitStateUsage.IsVariation);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iExhibitStateUsage.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iExhibitStateUsage.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iExhibitStateUsage.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind"u8);

            if (iExhibitStateUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iExhibitStateUsage.PortionKind.Value.ToString().ToLower());
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
