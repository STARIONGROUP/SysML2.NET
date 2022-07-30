// -------------------------------------------------------------------------------------------------
// <copyright file="WhileLoopActionUsageSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="WhileLoopActionUsageSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class WhileLoopActionUsageSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IWhileLoopActionUsage"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="whileLoopActionUsage">
        /// The <see cref="IWhileLoopActionUsage"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        public static void Serialize(IWhileLoopActionUsage iWhileLoopActionUsage, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iWhileLoopActionUsage.Id);

            writer.WritePropertyName("@type");
            writer.WriteStringValue("WhileLoopActionUsage");

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsIndividual);

            writer.WritePropertyName("portionKind");
            if (iWhileLoopActionUsage.PortionKind.HasValue)
            {
                writer.WriteStringValue(iWhileLoopActionUsage.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsVariation);

            writer.WritePropertyName("direction");
            if (iWhileLoopActionUsage.Direction.HasValue)
            {
                writer.WriteStringValue(iWhileLoopActionUsage.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsReadOnly);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsUnique);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsAbstract);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iWhileLoopActionUsage.IsSufficient);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iWhileLoopActionUsage.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iWhileLoopActionUsage.ElementId);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iWhileLoopActionUsage.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iWhileLoopActionUsage.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iWhileLoopActionUsage.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iWhileLoopActionUsage.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iWhileLoopActionUsage.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
