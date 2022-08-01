// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicitySerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="MultiplicitySerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class MultiplicitySerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IMultiplicity"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IMultiplicity"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IMultiplicity iMultiplicity))
            {
                throw new ArgumentException("The object shall be an IMultiplicity", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Multiplicity");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iMultiplicity.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iMultiplicity.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("direction");
            if (iMultiplicity.Direction.HasValue)
            {
                writer.WriteStringValue(iMultiplicity.Direction.Value.ToString().ToUpper());
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iMultiplicity.ElementId);

            writer.WritePropertyName("isAbstract");
            writer.WriteBooleanValue(iMultiplicity.IsAbstract);

            writer.WritePropertyName("isComposite");
            writer.WriteBooleanValue(iMultiplicity.IsComposite);

            writer.WritePropertyName("isDerived");
            writer.WriteBooleanValue(iMultiplicity.IsDerived);

            writer.WritePropertyName("isEnd");
            writer.WriteBooleanValue(iMultiplicity.IsEnd);

            writer.WritePropertyName("isOrdered");
            writer.WriteBooleanValue(iMultiplicity.IsOrdered);

            writer.WritePropertyName("isPortion");
            writer.WriteBooleanValue(iMultiplicity.IsPortion);

            writer.WritePropertyName("isReadOnly");
            writer.WriteBooleanValue(iMultiplicity.IsReadOnly);

            writer.WritePropertyName("isSufficient");
            writer.WriteBooleanValue(iMultiplicity.IsSufficient);

            writer.WritePropertyName("isUnique");
            writer.WriteBooleanValue(iMultiplicity.IsUnique);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iMultiplicity.Name);

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iMultiplicity.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelationship");
            if (iMultiplicity.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iMultiplicity.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iMultiplicity.ShortName);

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
