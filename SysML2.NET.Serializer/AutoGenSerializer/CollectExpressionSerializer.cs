// -------------------------------------------------------------------------------------------------
// <copyright file="CollectExpressionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="CollectExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class CollectExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ICollectExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ICollectExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ICollectExpression iCollectExpression))
            {
                throw new ArgumentException("The object shall be an ICollectExpression", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iCollectExpression.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("CollectExpression");

            writer.WritePropertyName("operator");
            writer.WriteStringValue(iCollectExpression.Operator);

            writer.WritePropertyName("direction");
            if (iCollectExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iCollectExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iCollectExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iCollectExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iCollectExpression.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iCollectExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iCollectExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iCollectExpression.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iCollectExpression.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iCollectExpression.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iCollectExpression.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iCollectExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iCollectExpression.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iCollectExpression.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iCollectExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iCollectExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iCollectExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iCollectExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
