// -------------------------------------------------------------------------------------------------
// <copyright file="SelectExpressionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="SelectExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class SelectExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISelectExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="selectExpression">
        /// The <see cref="ISelectExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ISelectExpression iSelectExpression, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iSelectExpression.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("SelectExpression");

            writer.WritePropertyName("operator");
            writer.WriteStringValue(iSelectExpression.Operator);

            writer.WritePropertyName("direction");
            if (iSelectExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iSelectExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iSelectExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iSelectExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iSelectExpression.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iSelectExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iSelectExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iSelectExpression.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iSelectExpression.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iSelectExpression.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iSelectExpression.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iSelectExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iSelectExpression.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iSelectExpression.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iSelectExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iSelectExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSelectExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iSelectExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
