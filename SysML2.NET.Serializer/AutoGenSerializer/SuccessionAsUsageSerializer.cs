// -------------------------------------------------------------------------------------------------
// <copyright file="SuccessionAsUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="SuccessionAsUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class SuccessionAsUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISuccessionAsUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="successionAsUsage">
        /// The <see cref="ISuccessionAsUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ISuccessionAsUsage iSuccessionAsUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iSuccessionAsUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("SuccessionAsUsage");

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iSuccessionAsUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iSuccessionAsUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iSuccessionAsUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iSuccessionAsUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iSuccessionAsUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iSuccessionAsUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iSuccessionAsUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSuccessionAsUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iSuccessionAsUsage.ShortName);

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iSuccessionAsUsage.IsDirected);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iSuccessionAsUsage.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iSuccessionAsUsage.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iSuccessionAsUsage.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iSuccessionAsUsage.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iSuccessionAsUsage.Target)
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
