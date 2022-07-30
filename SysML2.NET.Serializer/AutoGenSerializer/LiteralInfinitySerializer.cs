// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralInfinitySerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="LiteralInfinitySerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class LiteralInfinitySerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILiteralInfinity"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="literalInfinity">
        /// The <see cref="ILiteralInfinity"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ILiteralInfinity iLiteralInfinity, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iLiteralInfinity.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("LiteralInfinity");

            writer.WritePropertyName("direction");
            if (iLiteralInfinity.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralInfinity.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iLiteralInfinity.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iLiteralInfinity.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iLiteralInfinity.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iLiteralInfinity.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iLiteralInfinity.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iLiteralInfinity.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iLiteralInfinity.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iLiteralInfinity.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iLiteralInfinity.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iLiteralInfinity.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iLiteralInfinity.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iLiteralInfinity.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iLiteralInfinity.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iLiteralInfinity.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iLiteralInfinity.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iLiteralInfinity.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
