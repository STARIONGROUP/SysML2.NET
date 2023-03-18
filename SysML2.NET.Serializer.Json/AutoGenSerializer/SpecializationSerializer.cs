// -------------------------------------------------------------------------------------------------
// <copyright file="SpecializationSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="SpecializationSerializer"/> is to provide serialization
    /// and deserialization capabilities
    /// </summary>
    internal static class SpecializationSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="ISpecialization"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="ISpecialization"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is ISpecialization iSpecialization))
            {
                throw new ArgumentException("The object shall be an ISpecialization", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type");
            writer.WriteStringValue("Specialization");

            writer.WritePropertyName("@id");
            writer.WriteStringValue(iSpecialization.Id);

            writer.WriteStartArray("aliasIds");
            foreach (var item in iSpecialization.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("elementId");
            writer.WriteStringValue(iSpecialization.ElementId);

            writer.WritePropertyName("general");
            writer.WriteStringValue(iSpecialization.General);

            writer.WritePropertyName("isImplied");
            writer.WriteBooleanValue(iSpecialization.IsImplied);

            writer.WritePropertyName("isImpliedIncluded");
            writer.WriteBooleanValue(iSpecialization.IsImpliedIncluded);

            writer.WritePropertyName("name");
            writer.WriteStringValue(iSpecialization.Name);
            writer.WriteStartArray("ownedRelatedElement");
            foreach (var item in iSpecialization.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship");
            foreach (var item in iSpecialization.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement");
            if (iSpecialization.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iSpecialization.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship");
            if (iSpecialization.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iSpecialization.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(iSpecialization.ShortName);
            writer.WriteStartArray("source");
            foreach (var item in iSpecialization.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("specific");
            writer.WriteStringValue(iSpecialization.Specific);

            writer.WriteStartArray("target");
            foreach (var item in iSpecialization.Target)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
