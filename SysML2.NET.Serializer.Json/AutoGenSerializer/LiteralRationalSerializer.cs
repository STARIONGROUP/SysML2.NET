// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralRationalSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
    /// The purpose of the <see cref="LiteralRationalSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class LiteralRationalSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILiteralRational"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ILiteralRational"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ILiteralRational iLiteralRational))
            {
                throw new ArgumentException("The object shall be an ILiteralRational", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("LiteralRational");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iLiteralRational.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iLiteralRational.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iLiteralRational.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralRational.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iLiteralRational.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iLiteralRational.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iLiteralRational.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iLiteralRational.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iLiteralRational.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iLiteralRational.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iLiteralRational.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iLiteralRational.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iLiteralRational.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iLiteralRational.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iLiteralRational.IsUnique);

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
            writer.WritePropertyName("value");
            writer.WriteNumberValue(iLiteralRational.Value);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
