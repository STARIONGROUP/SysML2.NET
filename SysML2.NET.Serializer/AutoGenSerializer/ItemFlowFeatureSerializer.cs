// -------------------------------------------------------------------------------------------------
// <copyright file="ItemFlowFeatureSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ItemFlowFeatureSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ItemFlowFeatureSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IItemFlowFeature"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="itemFlowFeature">
        /// The <see cref="IItemFlowFeature"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IItemFlowFeature iItemFlowFeature, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iItemFlowFeature.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ItemFlowFeature");

            writer.WritePropertyName("direction");
            if (iItemFlowFeature.Direction.HasValue)
            {
                writer.WriteStringValue(iItemFlowFeature.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iItemFlowFeature.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iItemFlowFeature.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iItemFlowFeature.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iItemFlowFeature.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iItemFlowFeature.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iItemFlowFeature.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iItemFlowFeature.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iItemFlowFeature.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iItemFlowFeature.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iItemFlowFeature.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iItemFlowFeature.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iItemFlowFeature.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iItemFlowFeature.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iItemFlowFeature.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iItemFlowFeature.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iItemFlowFeature.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
