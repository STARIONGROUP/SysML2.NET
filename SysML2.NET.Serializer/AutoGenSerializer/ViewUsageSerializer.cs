// -------------------------------------------------------------------------------------------------
// <copyright file="ViewUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ViewUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ViewUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IViewUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="viewUsage">
        /// The <see cref="IViewUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IViewUsage iViewUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iViewUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ViewUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iViewUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iViewUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iViewUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iViewUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iViewUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iViewUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iViewUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iViewUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iViewUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iViewUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iViewUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iViewUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iViewUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iViewUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iViewUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iViewUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iViewUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iViewUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iViewUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iViewUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iViewUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iViewUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
