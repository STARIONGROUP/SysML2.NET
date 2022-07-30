// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionItemFlowSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="SuccessionItemFlowSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class SuccessionItemFlowSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISuccessionItemFlow"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="successionItemFlow">
        /// The <see cref="ISuccessionItemFlow"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ISuccessionItemFlow iSuccessionItemFlow, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iSuccessionItemFlow.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("SuccessionItemFlow");

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsDirected);

            writer.WritePropertyName("direction");
            if (iSuccessionItemFlow.Direction.HasValue)
            {
                writer.WriteStringValue(iSuccessionItemFlow.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iSuccessionItemFlow.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iSuccessionItemFlow.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iSuccessionItemFlow.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iSuccessionItemFlow.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iSuccessionItemFlow.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iSuccessionItemFlow.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSuccessionItemFlow.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iSuccessionItemFlow.ShortName);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iSuccessionItemFlow.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iSuccessionItemFlow.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iSuccessionItemFlow.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iSuccessionItemFlow.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
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
