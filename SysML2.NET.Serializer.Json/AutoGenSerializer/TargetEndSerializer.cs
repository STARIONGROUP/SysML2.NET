// -------------------------------------------------------------------------------------------------
// <copyright file="TargetEndSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="TargetEndSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class TargetEndSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ITargetEnd"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ITargetEnd"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ITargetEnd iTargetEnd))
            {
                throw new ArgumentException("The object shall be an ITargetEnd", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("TargetEnd");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iTargetEnd.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iTargetEnd.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iTargetEnd.Direction.HasValue)
            {
                writer.WriteStringValue(iTargetEnd.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iTargetEnd.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iTargetEnd.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iTargetEnd.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iTargetEnd.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iTargetEnd.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iTargetEnd.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iTargetEnd.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iTargetEnd.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iTargetEnd.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iTargetEnd.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iTargetEnd.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iTargetEnd.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iTargetEnd.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iTargetEnd.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iTargetEnd.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
