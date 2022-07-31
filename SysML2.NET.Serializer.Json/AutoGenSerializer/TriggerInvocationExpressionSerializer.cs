// -------------------------------------------------------------------------------------------------
// <copyright file="TriggerInvocationExpressionSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="TriggerInvocationExpressionSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class TriggerInvocationExpressionSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ITriggerInvocationExpression"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ITriggerInvocationExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ITriggerInvocationExpression iTriggerInvocationExpression))
            {
                throw new ArgumentException("The object shall be an ITriggerInvocationExpression", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iTriggerInvocationExpression.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("TriggerInvocationExpression");

            writer.WritePropertyName("kind");
            writer.WriteStringValue(iTriggerInvocationExpression.Kind.ToString().ToUpper());

            writer.WritePropertyName("direction");
            if (iTriggerInvocationExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iTriggerInvocationExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iTriggerInvocationExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iTriggerInvocationExpression.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iTriggerInvocationExpression.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iTriggerInvocationExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iTriggerInvocationExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iTriggerInvocationExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iTriggerInvocationExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------