// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralBooleanSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="LiteralBooleanSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class LiteralBooleanSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILiteralBoolean"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ILiteralBoolean"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ILiteralBoolean iLiteralBoolean))
            {
                throw new ArgumentException("The object shall be an ILiteralBoolean", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("LiteralBoolean");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iLiteralBoolean.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iLiteralBoolean.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iLiteralBoolean.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralBoolean.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iLiteralBoolean.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iLiteralBoolean.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iLiteralBoolean.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iLiteralBoolean.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iLiteralBoolean.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iLiteralBoolean.IsImpliedIncluded);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iLiteralBoolean.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iLiteralBoolean.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iLiteralBoolean.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iLiteralBoolean.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iLiteralBoolean.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iLiteralBoolean.Name);
            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iLiteralBoolean.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iLiteralBoolean.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iLiteralBoolean.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iLiteralBoolean.ShortName);
            writer.WritePropertyName("value");
            writer.WriteBooleanValue(iLiteralBoolean.Value);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
