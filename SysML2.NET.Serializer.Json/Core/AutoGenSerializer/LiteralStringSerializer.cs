// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralStringSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="LiteralStringSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="ILiteralString"/> interface
    /// </summary>
    internal static class LiteralStringSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILiteralString"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ILiteralString"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (obj is not ILiteralString iLiteralString)
            {
                throw new ArgumentException("The object shall be an ILiteralString", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("LiteralString"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iLiteralString.Id);

            writer.WriteStartArray("aliasIds"u8);

            foreach (var item in iLiteralString.AliasIds)
            {
                writer.WriteStringValue(item);
            }

            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iLiteralString.DeclaredName);

            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iLiteralString.DeclaredShortName);

            writer.WritePropertyName("direction"u8);

            if (iLiteralString.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralString.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iLiteralString.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iLiteralString.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iLiteralString.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iLiteralString.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iLiteralString.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iLiteralString.IsEnd);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iLiteralString.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iLiteralString.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iLiteralString.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iLiteralString.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iLiteralString.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iLiteralString.IsVariable);

            writer.WriteStartArray("ownedRelationship"u8);

            foreach (var item in iLiteralString.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);

            if (iLiteralString.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iLiteralString.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("value"u8);
            writer.WriteStringValue(iLiteralString.Value);

            writer.WriteEndObject();
        }
    }
}
// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
