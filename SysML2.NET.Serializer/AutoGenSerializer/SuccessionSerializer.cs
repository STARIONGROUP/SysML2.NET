// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="SuccessionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class SuccessionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISuccession"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="succession">
        /// The <see cref="ISuccession"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ISuccession iSuccession, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iSuccession.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Succession");

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iSuccession.IsDirected);

            writer.WritePropertyName("direction");
            if (iSuccession.Direction.HasValue)
            {
                writer.WriteStringValue(iSuccession.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iSuccession.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iSuccession.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iSuccession.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iSuccession.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iSuccession.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iSuccession.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iSuccession.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iSuccession.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iSuccession.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iSuccession.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iSuccession.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iSuccession.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iSuccession.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iSuccession.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSuccession.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iSuccession.ShortName);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iSuccession.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iSuccession.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iSuccession.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iSuccession.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iSuccession.Target)
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
