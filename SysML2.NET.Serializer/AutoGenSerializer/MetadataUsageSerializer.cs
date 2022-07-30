// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MetadataUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MetadataUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMetadataUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="metadataUsage">
        /// The <see cref="IMetadataUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IMetadataUsage iMetadataUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMetadataUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("MetadataUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iMetadataUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iMetadataUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iMetadataUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iMetadataUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iMetadataUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iMetadataUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iMetadataUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iMetadataUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iMetadataUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iMetadataUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iMetadataUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iMetadataUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iMetadataUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iMetadataUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iMetadataUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMetadataUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMetadataUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iMetadataUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMetadataUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iMetadataUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMetadataUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iMetadataUsage.ShortName);

            writer.WriteStartArray("annotation");
            foreach (var item in iMetadataUsage.Annotation)
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
