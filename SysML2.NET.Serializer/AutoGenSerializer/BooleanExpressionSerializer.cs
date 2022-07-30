// -------------------------------------------------------------------------------------------------
// <copyright file="BooleanExpressionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="BooleanExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class BooleanExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IBooleanExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="booleanExpression">
        /// The <see cref="IBooleanExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IBooleanExpression iBooleanExpression, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iBooleanExpression.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("BooleanExpression");

            writer.WritePropertyName("direction");
            if (iBooleanExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iBooleanExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iBooleanExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iBooleanExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iBooleanExpression.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iBooleanExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iBooleanExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iBooleanExpression.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iBooleanExpression.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iBooleanExpression.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iBooleanExpression.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iBooleanExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iBooleanExpression.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iBooleanExpression.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iBooleanExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iBooleanExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iBooleanExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iBooleanExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
