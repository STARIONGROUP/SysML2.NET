// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorAsUsageSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="ConnectorAsUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ConnectorAsUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IConnectorAsUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IConnectorAsUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IConnectorAsUsage iConnectorAsUsage))
            {
                throw new ArgumentException("The object shall be an IConnectorAsUsage", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ConnectorAsUsage");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iConnectorAsUsage.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iConnectorAsUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iConnectorAsUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iConnectorAsUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iConnectorAsUsage.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iConnectorAsUsage.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iConnectorAsUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iConnectorAsUsage.IsDerived);

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iConnectorAsUsage.IsDirected);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iConnectorAsUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iConnectorAsUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iConnectorAsUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iConnectorAsUsage.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iConnectorAsUsage.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iConnectorAsUsage.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iConnectorAsUsage.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iConnectorAsUsage.Name);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iConnectorAsUsage.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iConnectorAsUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iConnectorAsUsage.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iConnectorAsUsage.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("owningRelationship");
            if (iConnectorAsUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iConnectorAsUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iConnectorAsUsage.ShortName);

            writer.WriteStartArray("source");
            foreach (var item in iConnectorAsUsage.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iConnectorAsUsage.Target)
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
