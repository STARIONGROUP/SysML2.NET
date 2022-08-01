// -------------------------------------------------------------------------------------------------
// <copyright file="NullExpressionSerializer.cs" company="RHEA System S.A.">
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

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="NullExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class NullExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="INullExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="INullExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is INullExpression iNullExpression))
            {
                throw new ArgumentException("The object shall be an INullExpression", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("NullExpression");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iNullExpression.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iNullExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iNullExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iNullExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iNullExpression.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iNullExpression.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iNullExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iNullExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iNullExpression.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iNullExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iNullExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iNullExpression.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iNullExpression.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iNullExpression.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iNullExpression.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iNullExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iNullExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iNullExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iNullExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
