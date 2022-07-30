// -------------------------------------------------------------------------------------------------
// <copyright file="ItemFlowSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ItemFlowSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ItemFlowSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IItemFlow"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="itemFlow">
        /// The <see cref="IItemFlow"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IItemFlow iItemFlow, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iItemFlow.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ItemFlow");

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iItemFlow.IsDirected);

            writer.WritePropertyName("direction");
            if (iItemFlow.Direction.HasValue)
            {
                writer.WriteStringValue(iItemFlow.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iItemFlow.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iItemFlow.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iItemFlow.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iItemFlow.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iItemFlow.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iItemFlow.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iItemFlow.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iItemFlow.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iItemFlow.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iItemFlow.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iItemFlow.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iItemFlow.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iItemFlow.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iItemFlow.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iItemFlow.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iItemFlow.ShortName);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iItemFlow.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iItemFlow.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iItemFlow.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iItemFlow.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iItemFlow.Target)
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
