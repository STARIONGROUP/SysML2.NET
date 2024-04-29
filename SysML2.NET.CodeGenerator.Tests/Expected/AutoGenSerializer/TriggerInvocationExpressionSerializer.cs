// -------------------------------------------------------------------------------------------------
// <copyright file="ElementSerializer.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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

namespace SysML2.NET.Serializer
{
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
        /// <param name="triggerInvocationExpression">
        /// The <see cref="ITriggerInvocationExpression"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(ITriggerInvocationExpression iTriggerInvocationExpression, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iTriggerInvocationExpression.Id);

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("TriggerInvocationExpression");

            writer.WritePropertyName("kind"u8);
            writer.WriteStringValue(iTriggerInvocationExpression.Kind.ToString().ToUpper());

            writer.WritePropertyName("direction"u8);
            if (iTriggerInvocationExpression.Direction.HasValue)
            {
                writer.WriteStringValue(iTriggerInvocationExpression.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsComposite);

            writer.WritePropertyName("isDerived"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsDerived);

            writer.WritePropertyName("isEnd"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsEnd);

            writer.WritePropertyName("isOrdered"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsOrdered);

            writer.WritePropertyName("isPortion"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsPortion);

            writer.WritePropertyName("isReadOnly"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsReadOnly);

            writer.WritePropertyName("isUnique"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsUnique);

            writer.WritePropertyName("isAbstract"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsAbstract);

            writer.WritePropertyName("isSufficient"u8);
            writer.WriteBooleanValue(iTriggerInvocationExpression.IsSufficient);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iTriggerInvocationExpression.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iTriggerInvocationExpression.ElementId);

            writer.WritePropertyName("name"u8);
            writer.WriteStringValue(iTriggerInvocationExpression.Name);

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iTriggerInvocationExpression.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship"u8);
            if (iTriggerInvocationExpression.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iTriggerInvocationExpression.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName"u8);
            writer.WriteStringValue(iTriggerInvocationExpression.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
