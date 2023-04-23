﻿// -------------------------------------------------------------------------------------------------
// <copyright file="PortConjugationSerializer.cs" company="RHEA System S.A.">
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
    /// The purpose of the <see cref="PortConjugationSerializer"/> is to provide serialization capabilities
    /// capabilities for the <see cref="IPortConjugation"/> interface
    /// </summary>
    internal static class PortConjugationSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="IPortConjugation"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IPortConjugation"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        /// <param name="serializationModeKind">
        /// enumeration specifying what kind of serialization shall be used
        /// </param>
        internal static void Serialize(object obj, Utf8JsonWriter writer, SerializationModeKind serializationModeKind)
        {
            if (!(obj is IPortConjugation iPortConjugation))
            {
                throw new ArgumentException("The object shall be an IPortConjugation", nameof(obj));
            }

            writer.WriteStartObject();

            writer.WritePropertyName("@type"u8);
            writer.WriteStringValue("PortConjugation"u8);

            writer.WritePropertyName("@id"u8);
            writer.WriteStringValue(iPortConjugation.Id);

            writer.WriteStartArray("aliasIds"u8);
            foreach (var item in iPortConjugation.AliasIds)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("conjugatedType"u8);
            writer.WriteStringValue(iPortConjugation.ConjugatedType);

            writer.WritePropertyName("declaredName"u8);
            writer.WriteStringValue(iPortConjugation.DeclaredName);
            writer.WritePropertyName("declaredShortName"u8);
            writer.WriteStringValue(iPortConjugation.DeclaredShortName);
            writer.WritePropertyName("elementId"u8);
            writer.WriteStringValue(iPortConjugation.ElementId);

            writer.WritePropertyName("isImplied"u8);
            writer.WriteBooleanValue(iPortConjugation.IsImplied);

            writer.WritePropertyName("isImpliedIncluded"u8);
            writer.WriteBooleanValue(iPortConjugation.IsImpliedIncluded);

            writer.WritePropertyName("originalPortDefinition"u8);
            writer.WriteStringValue(iPortConjugation.OriginalPortDefinition);

            writer.WritePropertyName("originalType"u8);
            writer.WriteStringValue(iPortConjugation.OriginalType);

            writer.WriteStartArray("ownedRelatedElement"u8);
            foreach (var item in iPortConjugation.OwnedRelatedElement)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("ownedRelationship"u8);
            foreach (var item in iPortConjugation.OwnedRelationship)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WritePropertyName("owningRelatedElement"u8);
            if (iPortConjugation.OwningRelatedElement.HasValue)
            {
                writer.WriteStringValue(iPortConjugation.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WritePropertyName("owningRelationship"u8);
            if (iPortConjugation.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(iPortConjugation.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
            writer.WriteStartArray("source"u8);
            foreach (var item in iPortConjugation.Source)
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();

            writer.WriteStartArray("target"u8);
            foreach (var item in iPortConjugation.Target)
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