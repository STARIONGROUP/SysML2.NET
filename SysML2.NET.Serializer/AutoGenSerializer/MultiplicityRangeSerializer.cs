// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityRangeSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MultiplicityRangeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MultiplicityRangeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMultiplicityRange"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="multiplicityRange">
        /// The <see cref="IMultiplicityRange"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IMultiplicityRange iMultiplicityRange, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMultiplicityRange.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("MultiplicityRange");

            writer.WritePropertyName("direction");
            if (iMultiplicityRange.Direction.HasValue)
            {
                writer.WriteStringValue(iMultiplicityRange.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iMultiplicityRange.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iMultiplicityRange.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iMultiplicityRange.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iMultiplicityRange.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iMultiplicityRange.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iMultiplicityRange.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iMultiplicityRange.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iMultiplicityRange.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iMultiplicityRange.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMultiplicityRange.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMultiplicityRange.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iMultiplicityRange.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMultiplicityRange.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iMultiplicityRange.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMultiplicityRange.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iMultiplicityRange.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
