// -------------------------------------------------------------------------------------------------
// <copyright file="ControlNodeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ControlNodeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ControlNodeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IControlNode"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="controlNode">
        /// The <see cref="IControlNode"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IControlNode iControlNode, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iControlNode.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ControlNode");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iControlNode.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iControlNode.PortionKind.HasValue)
            {
                writer.WriteStringValue(iControlNode.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iControlNode.IsVariation);

            writer.WritePropertyName("direction");
            if (iControlNode.Direction.HasValue)
            {
                writer.WriteStringValue(iControlNode.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iControlNode.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iControlNode.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iControlNode.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iControlNode.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iControlNode.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iControlNode.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iControlNode.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iControlNode.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iControlNode.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iControlNode.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iControlNode.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iControlNode.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iControlNode.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iControlNode.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iControlNode.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iControlNode.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
