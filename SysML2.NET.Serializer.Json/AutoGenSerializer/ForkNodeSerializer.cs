// -------------------------------------------------------------------------------------------------
// <copyright file="ForkNodeSerializer.cs" company="RHEA System S.A.">
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

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO;

    /// <summary>
    /// The purpose of the <see cref="ForkNodeSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class ForkNodeSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IForkNode"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IForkNode"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IForkNode iForkNode))
            {
                throw new ArgumentException("The object shall be an IForkNode", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("ForkNode");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iForkNode.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iForkNode.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iForkNode.Direction.HasValue)
            {
                writer.WriteStringValue(iForkNode.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iForkNode.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iForkNode.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iForkNode.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iForkNode.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iForkNode.IsEnd);

            writer.WritePropertyName("isIndividual");
            writer.WriteBooleanValue(iForkNode.IsIndividual);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iForkNode.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iForkNode.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iForkNode.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iForkNode.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iForkNode.IsUnique);

            writer.WritePropertyName("isVariation");
            writer.WriteBooleanValue(iForkNode.IsVariation);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iForkNode.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iForkNode.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iForkNode.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iForkNode.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("portionKind");
            if (iForkNode.PortionKind.HasValue)
            {
                writer.WriteStringValue(iForkNode.PortionKind.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iForkNode.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
