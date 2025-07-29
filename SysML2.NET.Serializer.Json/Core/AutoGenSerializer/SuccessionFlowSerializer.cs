// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionFlowSerializer.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// The purpose of the <see cref="SuccessionFlowSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="ISuccessionFlow"/> interface
    /// </summary>
    internal static class SuccessionFlowSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISuccessionFlow"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ISuccessionFlow"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ISuccessionFlow iSuccessionFlow))
            {
                throw new ArgumentException("The object shall be an ISuccessionFlow", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("SuccessionFlow"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iSuccessionFlow.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iSuccessionFlow.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iSuccessionFlow.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iSuccessionFlow.DeclaredShortName);
            writer.WritePropertyName("direction"u8);
            if (iSuccessionFlow.Direction.HasValue)
            {
                writer.WriteStringValue(iSuccessionFlow.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iSuccessionFlow.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsComposite);

            writer.WritePropertyName("isConstant"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsConstant);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsEnd);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsPortion);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsUnique);

            writer.WritePropertyName("isVariable"u8);
            writer.WriteBooleanValue(iSuccessionFlow.IsVariable);

            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iSuccessionFlow.OwnedRelatedElement)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iSuccessionFlow.OwnedRelationship)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iSuccessionFlow.OwningRelatedElement.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iSuccessionFlow.OwningRelatedElement.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iSuccessionFlow.OwningRelationship.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(iSuccessionFlow.OwningRelationship.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iSuccessionFlow.Source)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);
            foreach (var item in iSuccessionFlow.Target)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@id"u8);
                writer.WriteStringValue(item);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
