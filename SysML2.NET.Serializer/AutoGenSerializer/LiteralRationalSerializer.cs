// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralRationalSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="LiteralRationalSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class LiteralRationalSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILiteralRational"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="literalRational">
        /// The <see cref="ILiteralRational"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ILiteralRational iLiteralRational, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iLiteralRational.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("LiteralRational");

            writer.WritePropertyName("value");
            writer.WriteNumberValue(iLiteralRational.Value);

            writer.WritePropertyName("direction");
            if (iLiteralRational.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralRational.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iLiteralRational.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iLiteralRational.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iLiteralRational.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iLiteralRational.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iLiteralRational.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iLiteralRational.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iLiteralRational.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iLiteralRational.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iLiteralRational.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iLiteralRational.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iLiteralRational.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iLiteralRational.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iLiteralRational.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iLiteralRational.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iLiteralRational.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iLiteralRational.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
