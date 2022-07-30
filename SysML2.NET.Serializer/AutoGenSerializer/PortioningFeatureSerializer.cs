// -------------------------------------------------------------------------------------------------
// <copyright file="PortioningFeatureSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="PortioningFeatureSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class PortioningFeatureSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IPortioningFeature"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="portioningFeature">
        /// The <see cref="IPortioningFeature"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IPortioningFeature iPortioningFeature, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iPortioningFeature.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("PortioningFeature");

            writer.WritePropertyName("direction");
            if (iPortioningFeature.Direction.HasValue)
            {
                writer.WriteStringValue(iPortioningFeature.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iPortioningFeature.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iPortioningFeature.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iPortioningFeature.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iPortioningFeature.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iPortioningFeature.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iPortioningFeature.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iPortioningFeature.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iPortioningFeature.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iPortioningFeature.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iPortioningFeature.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iPortioningFeature.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iPortioningFeature.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iPortioningFeature.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iPortioningFeature.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iPortioningFeature.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iPortioningFeature.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
