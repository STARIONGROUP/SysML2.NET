// -------------------------------------------------------------------------------------------------
// <copyright file="JoinNodeSerializer.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="JoinNodeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class JoinNodeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IJoinNode"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IJoinNode"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IJoinNode iJoinNode))
            {
                throw new ArgumentException("The object shall be an IJoinNode", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("JoinNode");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iJoinNode.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iJoinNode.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iJoinNode.Direction.HasValue)
            {
                writer.WriteStringValue(iJoinNode.Direction.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iJoinNode.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iJoinNode.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iJoinNode.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iJoinNode.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iJoinNode.IsEnd);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iJoinNode.IsImpliedIncluded);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iJoinNode.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iJoinNode.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iJoinNode.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iJoinNode.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iJoinNode.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iJoinNode.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iJoinNode.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iJoinNode.Name);
            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iJoinNode.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iJoinNode.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iJoinNode.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("portionKind");
            if (iJoinNode.PortionKind.HasValue)
            {
                writer.WriteStringValue(iJoinNode.PortionKind.Value.ToString().ToLower());
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iJoinNode.ShortName);
            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
