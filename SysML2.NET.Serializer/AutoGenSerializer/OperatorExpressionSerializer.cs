// -------------------------------------------------------------------------------------------------
// <copyright file="OperatorExpressionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="OperatorExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class OperatorExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IOperatorExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="operatorExpression">
        /// The <see cref="IOperatorExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IOperatorExpression iOperatorExpression, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iOperatorExpression.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("OperatorExpression");

            writer.WritePropertyName("operator");
            writer.WriteStringValue(iOperatorExpression.Operator);

            writer.WritePropertyName("direction");
            if (iOperatorExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iOperatorExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iOperatorExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iOperatorExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iOperatorExpression.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iOperatorExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iOperatorExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iOperatorExpression.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iOperatorExpression.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iOperatorExpression.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iOperatorExpression.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iOperatorExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iOperatorExpression.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iOperatorExpression.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iOperatorExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iOperatorExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iOperatorExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iOperatorExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
