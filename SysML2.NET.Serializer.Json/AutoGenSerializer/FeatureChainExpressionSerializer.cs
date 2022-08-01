// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureChainExpressionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="FeatureChainExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class FeatureChainExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IFeatureChainExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFeatureChainExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IFeatureChainExpression iFeatureChainExpression))
            {
                throw new ArgumentException("The object shall be an IFeatureChainExpression", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("FeatureChainExpression");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iFeatureChainExpression.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iFeatureChainExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iFeatureChainExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iFeatureChainExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iFeatureChainExpression.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iFeatureChainExpression.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iFeatureChainExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iFeatureChainExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iFeatureChainExpression.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iFeatureChainExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iFeatureChainExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iFeatureChainExpression.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iFeatureChainExpression.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iFeatureChainExpression.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iFeatureChainExpression.Name);

            writer.WritePropertyName("operator");
            writer.WriteStringValue(iFeatureChainExpression.Operator);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iFeatureChainExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iFeatureChainExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iFeatureChainExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iFeatureChainExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
