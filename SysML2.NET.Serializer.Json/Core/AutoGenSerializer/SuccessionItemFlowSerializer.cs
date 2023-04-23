// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionItemFlowSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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
    /// The purpose of the <see cref="SuccessionItemFlowSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="ISuccessionItemFlow"/> interface
    /// </summary>
    internal static class SuccessionItemFlowSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISuccessionItemFlow"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ISuccessionItemFlow"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ISuccessionItemFlow iSuccessionItemFlow))
            {
                throw new ArgumentException("The object shall be an ISuccessionItemFlow", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("SuccessionItemFlow"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iSuccessionItemFlow.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iSuccessionItemFlow.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iSuccessionItemFlow.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iSuccessionItemFlow.DeclaredShortName);
            writer.WritePropertyName("direction"u8);
            if (iSuccessionItemFlow.Direction.HasValue)
            {
                writer.WriteStringValue(iSuccessionItemFlow.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iSuccessionItemFlow.ElementId);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsAbstract);

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsComposite);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsDerived);

            writer.WritePropertyName("isDirected"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsDirected);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsEnd);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsPortion);

            writer.WritePropertyName("isReadOnly"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsReadOnly);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsSufficient);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iSuccessionItemFlow.IsUnique);

            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iSuccessionItemFlow.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iSuccessionItemFlow.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iSuccessionItemFlow.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iSuccessionItemFlow.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iSuccessionItemFlow.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSuccessionItemFlow.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iSuccessionItemFlow.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);
            foreach (var item in iSuccessionItemFlow.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
