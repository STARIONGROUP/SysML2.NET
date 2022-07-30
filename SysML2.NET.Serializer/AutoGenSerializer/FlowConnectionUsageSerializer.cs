// -------------------------------------------------------------------------------------------------
// <copyright file="FlowConnectionUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="FlowConnectionUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FlowConnectionUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFlowConnectionUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="flowConnectionUsage">
        /// The <see cref="IFlowConnectionUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IFlowConnectionUsage iFlowConnectionUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFlowConnectionUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("FlowConnectionUsage");

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iFlowConnectionUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iFlowConnectionUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFlowConnectionUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFlowConnectionUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iFlowConnectionUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFlowConnectionUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iFlowConnectionUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFlowConnectionUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iFlowConnectionUsage.ShortName);

            writer.WritePropertyName("isDirected");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsDirected);

            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iFlowConnectionUsage.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iFlowConnectionUsage.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iFlowConnectionUsage.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WriteStartArray("source");
            foreach (var item in iFlowConnectionUsage.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target");
            foreach (var item in iFlowConnectionUsage.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iFlowConnectionUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iFlowConnectionUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iFlowConnectionUsage.PortionKind.Value.ToString().ToUpper());
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
