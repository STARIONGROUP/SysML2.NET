// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralIntegerSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="LiteralIntegerSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class LiteralIntegerSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILiteralInteger"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ILiteralInteger"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ILiteralInteger iLiteralInteger))
            {
                throw new ArgumentException("The object shall be an ILiteralInteger", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("LiteralInteger");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iLiteralInteger.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iLiteralInteger.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iLiteralInteger.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralInteger.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iLiteralInteger.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iLiteralInteger.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iLiteralInteger.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iLiteralInteger.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iLiteralInteger.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iLiteralInteger.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iLiteralInteger.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iLiteralInteger.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iLiteralInteger.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iLiteralInteger.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iLiteralInteger.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iLiteralInteger.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iLiteralInteger.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iLiteralInteger.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iLiteralInteger.ShortName);

            writer.WritePropertyName("value");
            writer.WriteNumberValue(iLiteralInteger.Value);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
