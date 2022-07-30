// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="ReferenceUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ReferenceUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IReferenceUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="referenceUsage">
        /// The <see cref="IReferenceUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IReferenceUsage iReferenceUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iReferenceUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ReferenceUsage");

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iReferenceUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iReferenceUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iReferenceUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iReferenceUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iReferenceUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iReferenceUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iReferenceUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iReferenceUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iReferenceUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iReferenceUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iReferenceUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iReferenceUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iReferenceUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iReferenceUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iReferenceUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iReferenceUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iReferenceUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iReferenceUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iReferenceUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
