// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityRangeSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Kernel.Multiplicities;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="MultiplicityRangeSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IMultiplicityRange"/> interface
    /// </summary>
    internal static class MultiplicityRangeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMultiplicityRange"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMultiplicityRange"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (obj is not IMultiplicityRange iMultiplicityRange)
            {
                throw new ArgumentException("The object shall be an IMultiplicityRange", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("MultiplicityRange"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iMultiplicityRange.Id);

            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iMultiplicityRange.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iMultiplicityRange.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iMultiplicityRange.DeclaredShortName);

            writer.WritePropertyName("direction"u8);

            if (iMultiplicityRange.Direction.HasValue)
            {
                writer.WriteStringValue(iMultiplicityRange.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iMultiplicityRange.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iMultiplicityRange.IsVariable);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iMultiplicityRange.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iMultiplicityRange.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iMultiplicityRange.OwningRelationship.Value);
                writer.WriteEndObject();
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
