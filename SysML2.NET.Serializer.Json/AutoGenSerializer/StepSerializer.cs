// -------------------------------------------------------------------------------------------------
// <copyright file="StepSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="StepSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class StepSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IStep"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IStep"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IStep iStep))
            {
                throw new ArgumentException("The object shall be an IStep", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Step");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iStep.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iStep.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iStep.Direction.HasValue)
            {
                writer.WriteStringValue(iStep.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iStep.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iStep.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iStep.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iStep.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iStep.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iStep.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iStep.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iStep.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iStep.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iStep.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iStep.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iStep.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iStep.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iStep.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iStep.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
