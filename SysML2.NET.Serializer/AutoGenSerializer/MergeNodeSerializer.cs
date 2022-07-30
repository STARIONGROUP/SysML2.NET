// -------------------------------------------------------------------------------------------------
// <copyright file="MergeNodeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MergeNodeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MergeNodeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMergeNode"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="mergeNode">
        /// The <see cref="IMergeNode"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IMergeNode iMergeNode, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMergeNode.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("MergeNode");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iMergeNode.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iMergeNode.PortionKind.HasValue)
            {
                writer.WriteStringValue(iMergeNode.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iMergeNode.IsVariation);

            writer.WritePropertyName("direction");
            if (iMergeNode.Direction.HasValue)
            {
                writer.WriteStringValue(iMergeNode.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iMergeNode.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iMergeNode.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iMergeNode.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iMergeNode.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iMergeNode.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iMergeNode.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iMergeNode.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iMergeNode.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iMergeNode.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMergeNode.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMergeNode.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iMergeNode.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMergeNode.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iMergeNode.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMergeNode.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iMergeNode.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
