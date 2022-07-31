// -------------------------------------------------------------------------------------------------
// <copyright file="DecisionNodeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="DecisionNodeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class DecisionNodeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IDecisionNode"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IDecisionNode"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IDecisionNode iDecisionNode))
            {
                throw new ArgumentException("The object shall be an IDecisionNode", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iDecisionNode.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("DecisionNode");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iDecisionNode.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iDecisionNode.PortionKind.HasValue)
            {
                writer.WriteStringValue(iDecisionNode.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iDecisionNode.IsVariation);

            writer.WritePropertyName("direction");
            if (iDecisionNode.Direction.HasValue)
            {
                writer.WriteStringValue(iDecisionNode.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iDecisionNode.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iDecisionNode.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iDecisionNode.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iDecisionNode.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iDecisionNode.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iDecisionNode.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iDecisionNode.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iDecisionNode.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iDecisionNode.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iDecisionNode.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iDecisionNode.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iDecisionNode.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iDecisionNode.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iDecisionNode.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iDecisionNode.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iDecisionNode.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
