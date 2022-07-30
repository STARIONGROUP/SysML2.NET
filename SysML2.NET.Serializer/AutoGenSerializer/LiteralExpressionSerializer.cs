// -------------------------------------------------------------------------------------------------
// <copyright file="LiteralExpressionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="LiteralExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class LiteralExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ILiteralExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="literalExpression">
        /// The <see cref="ILiteralExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ILiteralExpression iLiteralExpression, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iLiteralExpression.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("LiteralExpression");

            writer.WritePropertyName("direction");
            if (iLiteralExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iLiteralExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iLiteralExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iLiteralExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iLiteralExpression.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iLiteralExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iLiteralExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iLiteralExpression.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iLiteralExpression.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iLiteralExpression.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iLiteralExpression.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iLiteralExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iLiteralExpression.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iLiteralExpression.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iLiteralExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iLiteralExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iLiteralExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iLiteralExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
